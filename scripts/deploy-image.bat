@echo off
setlocal enabledelayedexpansion

set PROJECT_NAME=uclav
set SERVICE_NAME=uclav-service
set TASK_FAMILY=uclav-task
set CONTAINER_NAME=uclav
set CONTAINER_PORT=8080

echo ========================================
echo AWS ECS Fargate Deployment Script
echo ========================================
echo.

REM Gather deployment configuration
echo --- AWS Configuration ---
set /p AWS_REGION="Enter AWS region (e.g., us-east-1): "
set /p CLUSTER_NAME="Enter ECS cluster name (e.g., my-ecs-cluster): "

echo.
echo --- Network Configuration ---
set /p VPC_ID="Enter VPC ID (e.g., vpc-0abc123def456): "
set /p SUBNET_IDS="Enter Subnet IDs comma-separated (e.g., subnet-0abc123,subnet-0def456): "
set /p SECURITY_GROUP="Enter Security Group ID (e.g., sg-0abc123def): "

REM Split subnet IDs
for /f "tokens=1,2 delims=," %%a in ("%SUBNET_IDS%") do (
    set SUBNET_1=%%a
    set SUBNET_2=%%b
)
if "!SUBNET_2!"=="" set SUBNET_2=!SUBNET_1!

echo.
echo --- Container Configuration ---
set /p IMAGE_URI="Enter Docker image URI (e.g., 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest): "

echo.
echo --- Load Balancer Configuration ---
set /p NEED_LB="Do you need a load balancer for this service? (y/n): "

set TARGET_GROUP_ARN=
if /i "!NEED_LB!"=="y" (
    echo Creating Application Load Balancer and Target Group...
    
    set ALB_NAME=!PROJECT_NAME!-alb
    set TG_NAME=!PROJECT_NAME!-tg
    
    echo Creating target group: !TG_NAME!
    for /f "tokens=*" %%i in ('aws elbv2 create-target-group --name !TG_NAME! --protocol HTTP --port !CONTAINER_PORT! --vpc-id !VPC_ID! --target-type ip --health-check-enabled --health-check-protocol HTTP --health-check-path /health --health-check-interval-seconds 30 --health-check-timeout-seconds 5 --healthy-threshold-count 2 --unhealthy-threshold-count 3 --region !AWS_REGION! --query "TargetGroups[0].TargetGroupArn" --output text 2^>nul') do set TARGET_GROUP_ARN=%%i
    
    if "!TARGET_GROUP_ARN!"=="" (
        echo Target group may already exist. Fetching existing target group...
        for /f "tokens=*" %%i in ('aws elbv2 describe-target-groups --names !TG_NAME! --region !AWS_REGION! --query "TargetGroups[0].TargetGroupArn" --output text 2^>nul') do set TARGET_GROUP_ARN=%%i
    )
    
    if "!TARGET_GROUP_ARN!"=="" (
        echo ERROR: Failed to create or retrieve target group
        exit /b 1
    )
    
    echo Target Group ARN: !TARGET_GROUP_ARN!
    
    echo Creating Application Load Balancer: !ALB_NAME!
    for /f "tokens=*" %%i in ('aws elbv2 create-load-balancer --name !ALB_NAME! --subnets !SUBNET_1! !SUBNET_2! --security-groups !SECURITY_GROUP! --scheme internet-facing --type application --ip-address-type ipv4 --region !AWS_REGION! --query "LoadBalancers[0].LoadBalancerArn" --output text 2^>nul') do set ALB_ARN=%%i
    
    if "!ALB_ARN!"=="" (
        echo Load balancer may already exist. Fetching existing load balancer...
        for /f "tokens=*" %%i in ('aws elbv2 describe-load-balancers --names !ALB_NAME! --region !AWS_REGION! --query "LoadBalancers[0].LoadBalancerArn" --output text 2^>nul') do set ALB_ARN=%%i
    )
    
    if "!ALB_ARN!"=="" (
        echo WARNING: Failed to create or retrieve load balancer, but continuing with target group
    ) else (
        echo Load Balancer ARN: !ALB_ARN!
        
        echo Creating ALB listener...
        aws elbv2 create-listener --load-balancer-arn !ALB_ARN! --protocol HTTP --port 80 --default-actions Type=forward,TargetGroupArn=!TARGET_GROUP_ARN! --region !AWS_REGION! >nul 2>&1
        
        for /f "tokens=*" %%i in ('aws elbv2 describe-load-balancers --load-balancer-arns !ALB_ARN! --region !AWS_REGION! --query "LoadBalancers[0].DNSName" --output text') do set ALB_DNS=%%i
    )
    
    echo Load balancer setup complete
)

echo.
echo ========================================
echo Starting ECS Deployment
echo ========================================

REM Get AWS Account ID
echo Retrieving AWS Account ID...
for /f "tokens=*" %%i in ('aws sts get-caller-identity --query Account --output text') do set ACCOUNT_ID=%%i
echo Account ID: !ACCOUNT_ID!

REM Check/create ECS cluster
echo.
echo Checking ECS cluster...
aws ecs describe-clusters --clusters !CLUSTER_NAME! --region !AWS_REGION! >nul 2>&1
if !ERRORLEVEL! neq 0 (
    echo Cluster does not exist. Creating ECS cluster: !CLUSTER_NAME!
    aws ecs create-cluster --cluster-name !CLUSTER_NAME! --region !AWS_REGION!
    echo Cluster created successfully
)

REM Create CloudWatch log group
echo.
echo Creating CloudWatch log group...
aws logs create-log-group --log-group-name /ecs/!PROJECT_NAME! --region !AWS_REGION! 2>nul

REM Prepare task definition
echo.
echo Preparing task definition...
copy ecs\task-definition.json ecs\task-definition-processed.json >nul

powershell -Command "(Get-Content ecs\task-definition-processed.json) -replace '{{IMAGE_URI}}', '%IMAGE_URI%' | Set-Content ecs\task-definition-processed.json"
powershell -Command "(Get-Content ecs\task-definition-processed.json) -replace '{{AWS_REGION}}', '%AWS_REGION%' | Set-Content ecs\task-definition-processed.json"
powershell -Command "(Get-Content ecs\task-definition-processed.json) -replace '{{ACCOUNT_ID}}', '%ACCOUNT_ID%' | Set-Content ecs\task-definition-processed.json"

REM Register task definition
echo Registering task definition...
for /f "tokens=*" %%i in ('aws ecs register-task-definition --cli-input-json file://ecs/task-definition-processed.json --region !AWS_REGION! --query "taskDefinition.taskDefinitionArn" --output text') do set TASK_DEF_ARN=%%i

if "!TASK_DEF_ARN!"=="" (
    echo ERROR: Failed to register task definition
    exit /b 1
)

echo Task definition registered: !TASK_DEF_ARN!

REM Prepare service definition
echo.
echo Preparing service definition...
copy ecs\service-definition.json ecs\service-definition-processed.json >nul

powershell -Command "(Get-Content ecs\service-definition-processed.json) -replace '{{CLUSTER_NAME}}', '%CLUSTER_NAME%' | Set-Content ecs\service-definition-processed.json"
powershell -Command "(Get-Content ecs\service-definition-processed.json) -replace '{{SUBNET_1}}', '%SUBNET_1%' | Set-Content ecs\service-definition-processed.json"
powershell -Command "(Get-Content ecs\service-definition-processed.json) -replace '{{SUBNET_2}}', '%SUBNET_2%' | Set-Content ecs\service-definition-processed.json"
powershell -Command "(Get-Content ecs\service-definition-processed.json) -replace '{{SECURITY_GROUP}}', '%SECURITY_GROUP%' | Set-Content ecs\service-definition-processed.json"

if /i "!NEED_LB!"=="y" (
    if not "!TARGET_GROUP_ARN!"=="" (
        powershell -Command "(Get-Content ecs\service-definition-processed.json) -replace '{{TARGET_GROUP_ARN}}', '%TARGET_GROUP_ARN%' | Set-Content ecs\service-definition-processed.json"
    )
) else (
    powershell -Command "(Get-Content ecs\service-definition-processed.json) | Where-Object {$_ -notmatch 'healthCheckGracePeriodSeconds|loadBalancers'} | Set-Content ecs\service-definition-processed.json"
)

REM Check if service exists
echo.
echo Checking if service exists...
for /f "tokens=*" %%i in ('aws ecs describe-services --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION! --query "services[0].serviceName" --output text 2^>nul') do set EXISTING_SERVICE=%%i

if "!EXISTING_SERVICE!"=="None" (
    echo Service does not exist. Creating new service...
    aws ecs create-service --cli-input-json file://ecs/service-definition-processed.json --region !AWS_REGION! >nul
    
    if !ERRORLEVEL! neq 0 (
        echo ERROR: Failed to create service
        exit /b 1
    )
    echo Service created successfully
) else (
    echo Service exists. Updating service...
    aws ecs update-service --cluster !CLUSTER_NAME! --service !SERVICE_NAME! --task-definition !TASK_DEF_ARN! --desired-count 2 --region !AWS_REGION! >nul
    
    if !ERRORLEVEL! neq 0 (
        echo ERROR: Failed to update service
        exit /b 1
    )
    echo Service updated successfully
)

REM Wait for service stability
echo.
echo Waiting for service to stabilize (this may take several minutes)...
aws ecs wait services-stable --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION!

if !ERRORLEVEL! neq 0 (
    echo WARNING: Service did not stabilize within expected time. Check ECS console for details.
) else (
    echo Service is stable
)

REM Verify deployment
echo.
echo ========================================
echo Deployment Summary
echo ========================================

for /f "tokens=*" %%i in ('aws ecs describe-services --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION! --query "services[0].runningCount" --output text') do set RUNNING_TASKS=%%i

echo Service Name: !SERVICE_NAME!
echo Cluster: !CLUSTER_NAME!
echo Running Tasks: !RUNNING_TASKS!
echo Task Definition: !TASK_DEF_ARN!
echo Image: !IMAGE_URI!
echo CloudWatch Logs: /ecs/!PROJECT_NAME!

if /i "!NEED_LB!"=="y" (
    if not "!ALB_DNS!"=="" (
        echo.
        echo Load Balancer Details:
        echo DNS Name: !ALB_DNS!
        echo Application URL: http://!ALB_DNS!
        echo Health Check Endpoint: http://!ALB_DNS!/health
    )
)

echo.
echo ========================================
echo Deployment Complete!
echo ========================================
echo.
echo To view logs:
echo   aws logs tail /ecs/!PROJECT_NAME! --follow --region !AWS_REGION!
echo.
echo To check service status:
echo   aws ecs describe-services --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION!
echo.

if /i "!NEED_LB!"=="y" (
    if not "!ALB_DNS!"=="" (
        echo Access your application at: http://!ALB_DNS!
    )
) else (
    echo No load balancer configured. Access tasks directly via their public IPs.
)
echo.

endlocal
