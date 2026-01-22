#!/bin/bash
set -e
set -o pipefail

PROJECT_NAME="uclav"
SERVICE_NAME="uclav-service"
TASK_FAMILY="uclav-task"
CONTAINER_NAME="uclav"
CONTAINER_PORT=8080

echo "========================================"
echo "AWS ECS Fargate Deployment Script"
echo "========================================"
echo ""

# Gather deployment configuration
echo "--- AWS Configuration ---"
read -p "Enter AWS region (e.g., us-east-1): " AWS_REGION
read -p "Enter ECS cluster name (e.g., my-ecs-cluster): " CLUSTER_NAME

echo ""
echo "--- Network Configuration ---"
read -p "Enter VPC ID (e.g., vpc-0abc123def456): " VPC_ID
read -p "Enter Subnet IDs comma-separated (e.g., subnet-0abc123,subnet-0def456): " SUBNET_IDS
read -p "Enter Security Group ID (e.g., sg-0abc123def): " SECURITY_GROUP

# Split subnet IDs
IFS=',' read -ra SUBNETS <<< "$SUBNET_IDS"
SUBNET_1="${SUBNETS[0]}"
SUBNET_2="${SUBNETS[1]:-$SUBNET_1}"

echo ""
echo "--- Container Configuration ---"
read -p "Enter Docker image URI (e.g., 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest): " IMAGE_URI

echo ""
echo "--- Load Balancer Configuration ---"
read -p "Do you need a load balancer for this service? (y/n): " NEED_LB

TARGET_GROUP_ARN=""
if [[ "$NEED_LB" =~ ^[Yy]$ ]]; then
    echo "Creating Application Load Balancer and Target Group..."
    
    # Create Target Group with ip target type (required for Fargate awsvpc)
    ALB_NAME="$PROJECT_NAME-alb"
    TG_NAME="$PROJECT_NAME-tg"
    
    echo "Creating target group: $TG_NAME"
    TARGET_GROUP_ARN=$(aws elbv2 create-target-group \
        --name "$TG_NAME" \
        --protocol HTTP \
        --port "$CONTAINER_PORT" \
        --vpc-id "$VPC_ID" \
        --target-type ip \
        --health-check-enabled \
        --health-check-protocol HTTP \
        --health-check-path /health \
        --health-check-interval-seconds 30 \
        --health-check-timeout-seconds 5 \
        --healthy-threshold-count 2 \
        --unhealthy-threshold-count 3 \
        --region "$AWS_REGION" \
        --query 'TargetGroups[0].TargetGroupArn' \
        --output text 2>/dev/null || echo "")
    
    if [ -z "$TARGET_GROUP_ARN" ]; then
        echo "Target group may already exist. Fetching existing target group..."
        TARGET_GROUP_ARN=$(aws elbv2 describe-target-groups \
            --names "$TG_NAME" \
            --region "$AWS_REGION" \
            --query 'TargetGroups[0].TargetGroupArn' \
            --output text 2>/dev/null || echo "")
    fi
    
    if [ -z "$TARGET_GROUP_ARN" ]; then
        echo "ERROR: Failed to create or retrieve target group"
        exit 1
    fi
    
    echo "Target Group ARN: $TARGET_GROUP_ARN"
    
    # Create ALB
    echo "Creating Application Load Balancer: $ALB_NAME"
    ALB_ARN=$(aws elbv2 create-load-balancer \
        --name "$ALB_NAME" \
        --subnets "$SUBNET_1" "$SUBNET_2" \
        --security-groups "$SECURITY_GROUP" \
        --scheme internet-facing \
        --type application \
        --ip-address-type ipv4 \
        --region "$AWS_REGION" \
        --query 'LoadBalancers[0].LoadBalancerArn' \
        --output text 2>/dev/null || echo "")
    
    if [ -z "$ALB_ARN" ]; then
        echo "Load balancer may already exist. Fetching existing load balancer..."
        ALB_ARN=$(aws elbv2 describe-load-balancers \
            --names "$ALB_NAME" \
            --region "$AWS_REGION" \
            --query 'LoadBalancers[0].LoadBalancerArn' \
            --output text 2>/dev/null || echo "")
    fi
    
    if [ -z "$ALB_ARN" ]; then
        echo "WARNING: Failed to create or retrieve load balancer, but continuing with target group"
    else
        echo "Load Balancer ARN: $ALB_ARN"
        
        # Create listener
        echo "Creating ALB listener..."
        aws elbv2 create-listener \
            --load-balancer-arn "$ALB_ARN" \
            --protocol HTTP \
            --port 80 \
            --default-actions Type=forward,TargetGroupArn="$TARGET_GROUP_ARN" \
            --region "$AWS_REGION" >/dev/null 2>&1 || echo "Listener may already exist"
        
        # Get ALB DNS name
        ALB_DNS=$(aws elbv2 describe-load-balancers \
            --load-balancer-arns "$ALB_ARN" \
            --region "$AWS_REGION" \
            --query 'LoadBalancers[0].DNSName' \
            --output text)
    fi
    
    echo "Load balancer setup complete"
fi

echo ""
echo "========================================"
echo "Starting ECS Deployment"
echo "========================================"

# Get AWS Account ID
echo "Retrieving AWS Account ID..."
ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
echo "Account ID: $ACCOUNT_ID"

# Check/create ECS cluster
echo ""
echo "Checking ECS cluster..."
aws ecs describe-clusters --clusters "$CLUSTER_NAME" --region "$AWS_REGION" >/dev/null 2>&1 || {
    echo "Cluster does not exist. Creating ECS cluster: $CLUSTER_NAME"
    aws ecs create-cluster --cluster-name "$CLUSTER_NAME" --region "$AWS_REGION"
    echo "Cluster created successfully"
}

# Create CloudWatch log group
echo ""
echo "Creating CloudWatch log group..."
aws logs create-log-group --log-group-name "/ecs/$PROJECT_NAME" --region "$AWS_REGION" 2>/dev/null || echo "Log group already exists"

# Prepare task definition
echo ""
echo "Preparing task definition..."
cp ecs/task-definition.json ecs/task-definition-processed.json

sed -i "s|{{IMAGE_URI}}|$IMAGE_URI|g" ecs/task-definition-processed.json
sed -i "s|{{AWS_REGION}}|$AWS_REGION|g" ecs/task-definition-processed.json
sed -i "s|{{ACCOUNT_ID}}|$ACCOUNT_ID|g" ecs/task-definition-processed.json

# Register task definition
echo "Registering task definition..."
TASK_DEF_ARN=$(aws ecs register-task-definition \
    --cli-input-json file://ecs/task-definition-processed.json \
    --region "$AWS_REGION" \
    --query 'taskDefinition.taskDefinitionArn' \
    --output text)

if [ -z "$TASK_DEF_ARN" ]; then
    echo "ERROR: Failed to register task definition"
    exit 1
fi

echo "Task definition registered: $TASK_DEF_ARN"

# Prepare service definition
echo ""
echo "Preparing service definition..."
cp ecs/service-definition.json ecs/service-definition-processed.json

sed -i "s|{{CLUSTER_NAME}}|$CLUSTER_NAME|g" ecs/service-definition-processed.json
sed -i "s|{{SUBNET_1}}|$SUBNET_1|g" ecs/service-definition-processed.json
sed -i "s|{{SUBNET_2}}|$SUBNET_2|g" ecs/service-definition-processed.json
sed -i "s|{{SECURITY_GROUP}}|$SECURITY_GROUP|g" ecs/service-definition-processed.json

if [[ "$NEED_LB" =~ ^[Yy]$ ]] && [ -n "$TARGET_GROUP_ARN" ]; then
    sed -i "s|{{TARGET_GROUP_ARN}}|$TARGET_GROUP_ARN|g" ecs/service-definition-processed.json
else
    # Remove loadBalancers section if no load balancer
    sed -i '/"healthCheckGracePeriodSeconds"/d' ecs/service-definition-processed.json
    sed -i '/"loadBalancers"/,/\],/d' ecs/service-definition-processed.json
fi

# Check if service exists
echo ""
echo "Checking if service exists..."
EXISTING_SERVICE=$(aws ecs describe-services \
    --cluster "$CLUSTER_NAME" \
    --services "$SERVICE_NAME" \
    --region "$AWS_REGION" \
    --query 'services[0].serviceName' \
    --output text 2>/dev/null || echo "None")

if [ "$EXISTING_SERVICE" = "None" ] || [ "$EXISTING_SERVICE" = "" ]; then
    echo "Service does not exist. Creating new service..."
    aws ecs create-service \
        --cli-input-json file://ecs/service-definition-processed.json \
        --region "$AWS_REGION" >/dev/null
    
    if [ $? -ne 0 ]; then
        echo "ERROR: Failed to create service"
        exit 1
    fi
    echo "Service created successfully"
else
    echo "Service exists. Updating service..."
    aws ecs update-service \
        --cluster "$CLUSTER_NAME" \
        --service "$SERVICE_NAME" \
        --task-definition "$TASK_DEF_ARN" \
        --desired-count 2 \
        --region "$AWS_REGION" >/dev/null
    
    if [ $? -ne 0 ]; then
        echo "ERROR: Failed to update service"
        exit 1
    fi
    echo "Service updated successfully"
fi

# Wait for service stability
echo ""
echo "Waiting for service to stabilize (this may take several minutes)..."
aws ecs wait services-stable \
    --cluster "$CLUSTER_NAME" \
    --services "$SERVICE_NAME" \
    --region "$AWS_REGION"

if [ $? -ne 0 ]; then
    echo "WARNING: Service did not stabilize within expected time. Check ECS console for details."
else
    echo "Service is stable"
fi

# Verify deployment
echo ""
echo "========================================"
echo "Deployment Summary"
echo "========================================"

RUNNING_TASKS=$(aws ecs describe-services \
    --cluster "$CLUSTER_NAME" \
    --services "$SERVICE_NAME" \
    --region "$AWS_REGION" \
    --query 'services[0].runningCount' \
    --output text)

echo "Service Name: $SERVICE_NAME"
echo "Cluster: $CLUSTER_NAME"
echo "Running Tasks: $RUNNING_TASKS"
echo "Task Definition: $TASK_DEF_ARN"
echo "Image: $IMAGE_URI"
echo "CloudWatch Logs: /ecs/$PROJECT_NAME"

if [[ "$NEED_LB" =~ ^[Yy]$ ]] && [ -n "$ALB_DNS" ]; then
    echo ""
    echo "Load Balancer Details:"
    echo "DNS Name: $ALB_DNS"
    echo "Application URL: http://$ALB_DNS"
    echo "Health Check Endpoint: http://$ALB_DNS/health"
fi

echo ""
echo "========================================"
echo "Deployment Complete!"
echo "========================================"
echo ""
echo "To view logs:"
echo "  aws logs tail /ecs/$PROJECT_NAME --follow --region $AWS_REGION"
echo ""
echo "To check service status:"
echo "  aws ecs describe-services --cluster $CLUSTER_NAME --services $SERVICE_NAME --region $AWS_REGION"
echo ""

if [[ "$NEED_LB" =~ ^[Yy]$ ]] && [ -n "$ALB_DNS" ]; then
    echo "Access your application at: http://$ALB_DNS"
else
    echo "No load balancer configured. Access tasks directly via their public IPs."
fi
echo ""
