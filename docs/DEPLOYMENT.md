# UCLAV Application - AWS ECS Fargate Deployment Guide

## Table of Contents

1. [Overview](#overview)
2. [Prerequisites](#prerequisites)
3. [Local Development Setup](#local-development-setup)
4. [Docker Deployment](#docker-deployment)
5. [AWS ECS Fargate Prerequisites](#aws-ecs-fargate-prerequisites)
6. [ECS Fargate Setup](#ecs-fargate-setup)
7. [ECS Task Definition Explained](#ecs-task-definition-explained)
8. [ECS Service Configuration](#ecs-service-configuration)
9. [Deployment Walkthrough](#deployment-walkthrough)
10. [Troubleshooting](#troubleshooting)
11. [Scaling and Management](#scaling-and-management)
12. [Security Considerations](#security-considerations)
13. [Monitoring and Logging](#monitoring-and-logging)

---

## Overview

This guide provides comprehensive instructions for deploying the UCLAV .NET 8.0 ASP.NET Core application to AWS ECS Fargate. The application is containerized using Docker and deployed using AWS ECS with Fargate launch type for serverless container orchestration.

**Technology Stack:**
- .NET 8.0
- ASP.NET Core Web API
- Docker
- AWS ECS Fargate
- AWS Application Load Balancer
- AWS CloudWatch Logs

**Deployment Architecture:**
- Containerized .NET application running on ECS Fargate
- Application Load Balancer for traffic distribution
- CloudWatch for logging and monitoring
- VPC with public/private subnets
- Security groups for network access control

---

## Prerequisites

### Required Software

1. **Docker Desktop** (v20.10+)
   - Download: https://www.docker.com/products/docker-desktop
   - Verify: `docker --version`

2. **AWS CLI** (v2.x)
   - Download: https://aws.amazon.com/cli/
   - Verify: `aws --version`
   - Configure: `aws configure`

3. **.NET SDK 8.0**
   - Download: https://dotnet.microsoft.com/download/dotnet/8.0
   - Verify: `dotnet --version`

### AWS Account Requirements

1. **AWS Account** with appropriate permissions
2. **IAM User** with the following managed policies:
   - `AmazonECS_FullAccess`
   - `AmazonEC2ContainerRegistryFullAccess`
   - `IAMFullAccess` (for creating service roles)
   - `AmazonVPCFullAccess`
   - `ElasticLoadBalancingFullAccess`
   - `CloudWatchLogsFullAccess`

3. **AWS CLI Configured** with access keys:
   ```bash
   aws configure
   # Enter: Access Key ID, Secret Access Key, Default region, Output format
   ```

---

## Local Development Setup

### Build and Run Locally

1. **Clone the repository** (if applicable):
   ```bash
   git clone <repository-url>
   cd UCLAV
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the application**:
   ```bash
   dotnet build -c Release
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

5. **Test the application**:
   - Open browser: `http://localhost:8080`
   - Health check: `http://localhost:8080/health`

### Environment Configuration

- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development overrides
- `appsettings.Production.json`: Production overrides

**Key Configuration Settings:**
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://+:8080"
      }
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

---

## Docker Deployment

### Build Docker Image Locally

1. **Build the image**:
   ```bash
   docker build -t uclav-app:latest -f Dockerfile .
   ```

2. **Run the container locally**:
   ```bash
   docker run -d -p 8080:8080 --name uclav-app uclav-app:latest
   ```

3. **Test the containerized application**:
   ```bash
   curl http://localhost:8080/health
   ```

4. **View container logs**:
   ```bash
   docker logs -f uclav-app
   ```

5. **Stop and remove container**:
   ```bash
   docker stop uclav-app
   docker rm uclav-app
   ```

### Using Docker Compose

1. **Start the application**:
   ```bash
   docker-compose up -d
   ```

2. **View logs**:
   ```bash
   docker-compose logs -f
   ```

3. **Stop the application**:
   ```bash
   docker-compose down
   ```

---

## AWS ECS Fargate Prerequisites

### 1. VPC and Networking Setup

**Create VPC** (if not exists):
```bash
aws ec2 create-vpc --cidr-block 10.0.0.0/16 --region us-east-1
```

**Create Public Subnets** (minimum 2 for ALB):
```bash
# Subnet 1
aws ec2 create-subnet --vpc-id vpc-xxxxx --cidr-block 10.0.1.0/24 --availability-zone us-east-1a

# Subnet 2
aws ec2 create-subnet --vpc-id vpc-xxxxx --cidr-block 10.0.2.0/24 --availability-zone us-east-1b
```

**Create Internet Gateway**:
```bash
aws ec2 create-internet-gateway
aws ec2 attach-internet-gateway --vpc-id vpc-xxxxx --internet-gateway-id igw-xxxxx
```

**Configure Route Table**:
```bash
aws ec2 create-route --route-table-id rtb-xxxxx --destination-cidr-block 0.0.0.0/0 --gateway-id igw-xxxxx
```

### 2. Security Group Configuration

**Create Security Group**:
```bash
aws ec2 create-security-group \
  --group-name uclav-sg \
  --description "Security group for UCLAV ECS tasks" \
  --vpc-id vpc-xxxxx
```

**Add Inbound Rules**:
```bash
# Allow HTTP from ALB
aws ec2 authorize-security-group-ingress \
  --group-id sg-xxxxx \
  --protocol tcp \
  --port 8080 \
  --source-group sg-alb-xxxxx

# Allow HTTP from anywhere (if no ALB)
aws ec2 authorize-security-group-ingress \
  --group-id sg-xxxxx \
  --protocol tcp \
  --port 8080 \
  --cidr 0.0.0.0/0
```

### 3. IAM Roles Setup

**ECS Task Execution Role** (required for Fargate):
```bash
aws iam create-role \
  --role-name ecsTaskExecutionRole \
  --assume-role-policy-document file://ecs-task-execution-role-trust-policy.json

aws iam attach-role-policy \
  --role-name ecsTaskExecutionRole \
  --policy-arn arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy
```

**Trust Policy Document** (`ecs-task-execution-role-trust-policy.json`):
```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "ecs-tasks.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
```

**ECS Task Role** (optional, for application permissions):
```bash
aws iam create-role \
  --role-name ecsTaskRole \
  --assume-role-policy-document file://ecs-task-role-trust-policy.json

# Attach policies as needed (S3, DynamoDB, etc.)
```

### 4. CloudWatch Log Group

**Create Log Group**:
```bash
aws logs create-log-group --log-group-name /ecs/uclav --region us-east-1
```

---

## ECS Fargate Setup

### 1. Create ECS Cluster

```bash
aws ecs create-cluster --cluster-name uclav-cluster --region us-east-1
```

### 2. Create ECR Repository

```bash
aws ecr create-repository --repository-name uclav --region us-east-1
```

### 3. Build and Push Docker Image

**Using provided scripts**:

**Linux/macOS**:
```bash
chmod +x scripts/build-push.sh
./scripts/build-push.sh
```

**Windows**:
```cmd
scripts\build-push.bat
```

**Manual Build and Push**:
```bash
# Authenticate Docker to ECR
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 123456789.dkr.ecr.us-east-1.amazonaws.com

# Build image
docker build -t uclav:latest -f Dockerfile .

# Tag image
docker tag uclav:latest 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest

# Push image
docker push 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest
```

---

## ECS Task Definition Explained

### Task Definition Structure

The task definition (`ecs/task-definition.json`) defines how your container runs on ECS Fargate.

**Key Components:**

1. **Launch Type Configuration**:
   ```json
   "requiresCompatibilities": ["FARGATE"],
   "networkMode": "awsvpc"
   ```
   - `FARGATE`: Serverless launch type
   - `awsvpc`: Required network mode for Fargate (each task gets its own ENI)

2. **CPU and Memory**:
   ```json
   "cpu": "512",
   "memory": "1024"
   ```
   - **Valid Fargate CPU/Memory Combinations**:
     - CPU: 256 (.25 vCPU) → Memory: 512, 1024, 2048 MB
     - CPU: 512 (.5 vCPU) → Memory: 1024, 2048, 3072, 4096 MB
     - CPU: 1024 (1 vCPU) → Memory: 2048-8192 MB
     - CPU: 2048 (2 vCPU) → Memory: 4096-16384 MB
     - CPU: 4096 (4 vCPU) → Memory: 8192-30720 MB

3. **Execution Role**:
   ```json
   "executionRoleArn": "arn:aws:iam::123456789:role/ecsTaskExecutionRole"
   ```
   - Allows ECS to pull images from ECR
   - Allows ECS to send logs to CloudWatch

4. **Task Role** (optional):
   ```json
   "taskRoleArn": "arn:aws:iam::123456789:role/ecsTaskRole"
   ```
   - Provides permissions to the application (S3, DynamoDB, etc.)

5. **Container Definition**:
   ```json
   "containerDefinitions": [
     {
       "name": "uclav",
       "image": "123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest",
       "essential": true,
       "portMappings": [
         {"containerPort": 8080, "protocol": "tcp"}
       ]
     }
   ]
   ```

6. **Environment Variables**:
   ```json
   "environment": [
     {"name": "ASPNETCORE_ENVIRONMENT", "value": "Production"},
     {"name": "ASPNETCORE_URLS", "value": "http://+:8080"}
   ]
   ```

7. **Logging Configuration**:
   ```json
   "logConfiguration": {
     "logDriver": "awslogs",
     "options": {
       "awslogs-group": "/ecs/uclav",
       "awslogs-region": "us-east-1",
       "awslogs-stream-prefix": "ecs"
     }
   }
   ```

---

## ECS Service Configuration

### Service Definition Structure

The service definition (`ecs/service-definition.json`) manages task lifecycle and load balancing.

**Key Components:**

1. **Service Configuration**:
   ```json
   {
     "serviceName": "uclav-service",
     "cluster": "uclav-cluster",
     "taskDefinition": "uclav-task",
     "desiredCount": 2,
     "launchType": "FARGATE"
   }
   ```

2. **Network Configuration**:
   ```json
   "networkConfiguration": {
     "awsvpcConfiguration": {
       "subnets": ["subnet-xxxxx", "subnet-yyyyy"],
       "securityGroups": ["sg-xxxxx"],
       "assignPublicIp": "ENABLED"
     }
   }
   ```
   - `assignPublicIp`: "ENABLED" for public subnets without NAT Gateway

3. **Deployment Configuration**:
   ```json
   "deploymentConfiguration": {
     "maximumPercent": 200,
     "minimumHealthyPercent": 50
   }
   ```
   - `maximumPercent: 200`: Allows rolling deployments (up to 2x desired count)
   - `minimumHealthyPercent: 50`: Maintains at least 50% capacity during deployment

4. **Load Balancer Configuration** (optional):
   ```json
   "loadBalancers": [
     {
       "targetGroupArn": "arn:aws:elasticloadbalancing:...",
       "containerName": "uclav",
       "containerPort": 8080
     }
   ],
   "healthCheckGracePeriodSeconds": 300
   ```

5. **Service Tags**:
   ```json
   "tags": [
     {"key": "Environment", "value": "production"},
     {"key": "Application", "value": "uclav"}
   ]
   ```
   - **IMPORTANT**: Use `tags`, NOT `serviceTags` (invalid parameter)

---

## Deployment Walkthrough

### Step 1: Build and Push Docker Image

**Linux/macOS**:
```bash
cd /path/to/UCLAV
chmod +x scripts/build-push.sh
./scripts/build-push.sh
```

**Windows**:
```cmd
cd C:\path\to\UCLAV
scripts\build-push.bat
```

**Script Prompts**:
1. Select registry type (1. AWS ECR, 2. Docker Hub)
2. Enter AWS region (e.g., us-east-1)
3. Enter AWS Account ID
4. Enter ECR repository name (default: uclav)
5. Enter image tag (default: latest)

**Expected Output**:
```
========================================
SUCCESS!
========================================
Image pushed successfully: 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest

Use this image URI for deployment:
123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest
========================================
```

### Step 2: Deploy to ECS Fargate

**Linux/macOS**:
```bash
chmod +x scripts/deploy-image.sh
./scripts/deploy-image.sh
```

**Windows**:
```cmd
scripts\deploy-image.bat
```

**Script Prompts**:
1. Enter AWS region (e.g., us-east-1)
2. Enter ECS cluster name (e.g., uclav-cluster)
3. Enter VPC ID (e.g., vpc-0abc123def456)
4. Enter Subnet IDs comma-separated (e.g., subnet-0abc123,subnet-0def456)
5. Enter Security Group ID (e.g., sg-0abc123def)
6. Enter Docker image URI (e.g., 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest)
7. Do you need a load balancer? (y/n)

**Expected Output**:
```
========================================
Deployment Complete!
========================================

Service Name: uclav-service
Cluster: uclav-cluster
Running Tasks: 2
Task Definition: arn:aws:ecs:us-east-1:123456789:task-definition/uclav-task:1
Image: 123456789.dkr.ecr.us-east-1.amazonaws.com/uclav:latest
CloudWatch Logs: /ecs/uclav

Load Balancer Details:
DNS Name: uclav-alb-123456789.us-east-1.elb.amazonaws.com
Application URL: http://uclav-alb-123456789.us-east-1.elb.amazonaws.com
Health Check Endpoint: http://uclav-alb-123456789.us-east-1.elb.amazonaws.com/health
```

### Step 3: Verify Deployment

1. **Check service status**:
   ```bash
   aws ecs describe-services --cluster uclav-cluster --services uclav-service --region us-east-1
   ```

2. **View running tasks**:
   ```bash
   aws ecs list-tasks --cluster uclav-cluster --service-name uclav-service --region us-east-1
   ```

3. **Test application**:
   ```bash
   curl http://uclav-alb-123456789.us-east-1.elb.amazonaws.com/health
   ```

4. **View logs**:
   ```bash
   aws logs tail /ecs/uclav --follow --region us-east-1
   ```

---

## Troubleshooting

### Common Issues

#### 1. Task Fails to Start

**Symptom**: Tasks transition to STOPPED state immediately.

**Possible Causes**:
- Invalid CPU/memory combination
- Image pull failure (ECR permissions)
- Container health check failure
- Missing IAM execution role permissions

**Solutions**:
```bash
# Check task stopped reason
aws ecs describe-tasks --cluster uclav-cluster --tasks <task-id> --region us-east-1

# Check CloudWatch logs
aws logs tail /ecs/uclav --follow --region us-east-1

# Verify execution role has ECR and CloudWatch permissions
aws iam get-role --role-name ecsTaskExecutionRole
```

#### 2. Service Not Reaching Steady State

**Symptom**: Service continuously starts and stops tasks.

**Possible Causes**:
- Application crashes on startup
- Health check failures
- Insufficient resources

**Solutions**:
```bash
# Check service events
aws ecs describe-services --cluster uclav-cluster --services uclav-service --region us-east-1

# View application logs
aws logs tail /ecs/uclav --follow --region us-east-1

# Increase health check grace period
# Update service-definition.json: "healthCheckGracePeriodSeconds": 600
```

#### 3. Cannot Access Application via ALB

**Symptom**: Load balancer returns 503 or times out.

**Possible Causes**:
- Security group rules blocking traffic
- Target group health check failing
- Tasks not registered with target group

**Solutions**:
```bash
# Check target group health
aws elbv2 describe-target-health --target-group-arn <target-group-arn> --region us-east-1

# Verify security group allows traffic from ALB
aws ec2 describe-security-groups --group-ids <sg-id> --region us-east-1

# Check target group health check configuration
aws elbv2 describe-target-groups --target-group-arns <target-group-arn> --region us-east-1
```

#### 4. High Memory Usage

**Symptom**: Tasks OOM killed or restarted frequently.

**Solutions**:
- Increase task memory in task definition
- Optimize .NET application memory usage
- Configure garbage collection settings

**Environment Variable for GC**:
```json
{
  "name": "DOTNET_GCHeapCount",
  "value": "2"
}
```

#### 5. Network Connectivity Issues

**Symptom**: Tasks cannot connect to external services (database, APIs).

**Solutions**:
- Verify security group allows outbound traffic
- Check VPC route tables and NAT Gateway configuration
- Verify DNS resolution

```bash
# Test from ECS Exec (if enabled)
aws ecs execute-command --cluster uclav-cluster --task <task-id> --container uclav --interactive --command "/bin/bash"
```

---

## Scaling and Management

### Manual Scaling

**Update desired count**:
```bash
aws ecs update-service \
  --cluster uclav-cluster \
  --service uclav-service \
  --desired-count 4 \
  --region us-east-1
```

### Auto Scaling

**1. Register scalable target**:
```bash
aws application-autoscaling register-scalable-target \
  --service-namespace ecs \
  --resource-id service/uclav-cluster/uclav-service \
  --scalable-dimension ecs:service:DesiredCount \
  --min-capacity 2 \
  --max-capacity 10 \
  --region us-east-1
```

**2. Create scaling policy (Target Tracking)**:
```bash
aws application-autoscaling put-scaling-policy \
  --service-namespace ecs \
  --resource-id service/uclav-cluster/uclav-service \
  --scalable-dimension ecs:service:DesiredCount \
  --policy-name cpu-scaling-policy \
  --policy-type TargetTrackingScaling \
  --target-tracking-scaling-policy-configuration file://scaling-policy.json \
  --region us-east-1
```

**Scaling Policy** (`scaling-policy.json`):
```json
{
  "TargetValue": 70.0,
  "PredefinedMetricSpecification": {
    "PredefinedMetricType": "ECSServiceAverageCPUUtilization"
  },
  "ScaleInCooldown": 300,
  "ScaleOutCooldown": 60
}
```

### Blue/Green Deployments

**Using AWS CodeDeploy**:
1. Create CodeDeploy application and deployment group
2. Configure deployment to use ECS blue/green
3. Deploy new task definition revision via CodeDeploy

**Benefits**:
- Zero-downtime deployments
- Automatic rollback on failure
- Traffic shifting strategies

---

## Security Considerations

### 1. Container Security

- **Non-root user**: Dockerfile creates and uses `appuser`
- **Minimal base image**: Uses official Microsoft aspnet runtime image
- **No secrets in image**: Use environment variables or AWS Secrets Manager

### 2. Network Security

- **Security groups**: Restrict inbound traffic to necessary ports
- **Private subnets**: Deploy tasks in private subnets with NAT Gateway
- **VPC endpoints**: Use VPC endpoints for AWS services (ECR, CloudWatch, S3)

### 3. Secrets Management

**Using AWS Secrets Manager**:
```json
"secrets": [
  {
    "name": "DATABASE_PASSWORD",
    "valueFrom": "arn:aws:secretsmanager:us-east-1:123456789:secret:db-password"
  }
]
```

**Task execution role requires**:
```json
{
  "Effect": "Allow",
  "Action": [
    "secretsmanager:GetSecretValue"
  ],
  "Resource": "arn:aws:secretsmanager:us-east-1:123456789:secret:*"
}
```

### 4. IAM Best Practices

- Use task roles for application permissions (least privilege)
- Rotate access keys regularly
- Enable MFA for AWS console access
- Use IAM roles for EC2 instances (if using EC2 launch type)

---

## Monitoring and Logging

### CloudWatch Logs

**View logs**:
```bash
# Tail logs in real-time
aws logs tail /ecs/uclav --follow --region us-east-1

# Filter logs by pattern
aws logs filter-log-events \
  --log-group-name /ecs/uclav \
  --filter-pattern "ERROR" \
  --region us-east-1
```

### CloudWatch Metrics

**Key ECS Metrics**:
- `CPUUtilization`: Task CPU usage
- `MemoryUtilization`: Task memory usage
- `RunningTasksCount`: Number of running tasks

**View metrics**:
```bash
aws cloudwatch get-metric-statistics \
  --namespace AWS/ECS \
  --metric-name CPUUtilization \
  --dimensions Name=ServiceName,Value=uclav-service Name=ClusterName,Value=uclav-cluster \
  --start-time 2024-01-01T00:00:00Z \
  --end-time 2024-01-02T00:00:00Z \
  --period 3600 \
  --statistics Average \
  --region us-east-1
```

### Application Insights (Optional)

**Add Application Insights to .NET application**:

1. Install NuGet package:
   ```bash
   dotnet add package Microsoft.ApplicationInsights.AspNetCore
   ```

2. Configure in `Program.cs`:
   ```csharp
   builder.Services.AddApplicationInsightsTelemetry();
   ```

3. Set instrumentation key in environment variables:
   ```json
   {
     "name": "APPLICATIONINSIGHTS_CONNECTION_STRING",
     "value": "InstrumentationKey=xxxx-xxxx-xxxx"
   }
   ```

### CloudWatch Alarms

**Create alarm for high CPU**:
```bash
aws cloudwatch put-metric-alarm \
  --alarm-name uclav-high-cpu \
  --alarm-description "Alert when CPU exceeds 80%" \
  --metric-name CPUUtilization \
  --namespace AWS/ECS \
  --statistic Average \
  --period 300 \
  --threshold 80 \
  --comparison-operator GreaterThanThreshold \
  --evaluation-periods 2 \
  --dimensions Name=ServiceName,Value=uclav-service Name=ClusterName,Value=uclav-cluster \
  --region us-east-1
```

---

## Additional Resources

- [AWS ECS Developer Guide](https://docs.aws.amazon.com/ecs/)
- [AWS Fargate Documentation](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/AWS_Fargate.html)
- [.NET on AWS](https://aws.amazon.com/developer/language/net/)
- [Docker Best Practices](https://docs.docker.com/develop/dev-best-practices/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)

---

## Support

For issues or questions:
1. Check CloudWatch logs: `/ecs/uclav`
2. Review ECS service events
3. Consult AWS documentation
4. Contact your AWS support team

---

**Document Version**: 1.0  
**Last Updated**: 2024-01-22  
**Target Platform**: AWS ECS Fargate  
**Application**: UCLAV .NET 8.0 ASP.NET Core
