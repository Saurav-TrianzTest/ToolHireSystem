# Cloud Deployment Guide - ToolHireSystem

## Overview
This guide provides instructions for deploying the ToolHireSystem application to AWS cloud environment after cloud readiness fixes have been applied.

## Cloud Readiness Fixes Applied

### 1. Configuration Management
- **Hardcoded Connection Strings Removed**: Connection strings now loaded from environment variables
- **Environment Variables**: `DATABASE_CONNECTION_STRING` and `LOCALDB_CONNECTION_STRING`
- **Configuration Location**: `DBConnect.cs` updated to read from environment

### 2. SQL Injection Vulnerabilities Fixed
- **Parameterized Queries**: All 140+ SQL queries converted to use parameterized commands
- **Files Updated**:
  - Supply.cs, Customer.cs, Rental.cs, Payment.cs, RentalItems.cs, Invoice.cs
  - User.cs, Validator.cs
  - Form files: FrmAddSupply, FrmUpdateSupply, FrmRemoveSupply, FrmProcessRental, FrmTypeAnalysis, FrmRevenueAnalysis

### 3. Resource Management
- **Proper Disposal**: All `SqlConnection`, `SqlCommand`, and `SqlDataReader` objects now use `using` statements
- **Connection Pooling**: Connections automatically pooled by ADO.NET
- **No Resource Leaks**: Automatic cleanup ensures no connection leaks

### 4. Cloud-Native Patterns
- **Static State Removed**: Settings.Designer.cs no longer uses static singleton
- **Environment-Based Config**: All configuration comes from environment variables
- **AWS Integration Ready**: Prepared for AWS Secrets Manager and Parameter Store

## AWS Deployment Architecture

### Recommended AWS Services

1. **Database: AWS RDS SQL Server**
   - Choose appropriate instance size (e.g., db.t3.medium for dev, db.m5.large for prod)
   - Enable Multi-AZ for production environments
   - Configure automated backups
   - Use VPC security groups to restrict access

2. **Secrets Management: AWS Secrets Manager**
   - Store database credentials securely
   - Enable automatic rotation
   - Grant IAM permissions to application role

3. **Configuration: AWS Systems Manager Parameter Store**
   - Store non-sensitive configuration
   - Use hierarchical parameters (e.g., /toolhire/prod/config)

4. **Compute Options**:
   - **ECS Fargate**: Recommended for containerized deployment
   - **ECS on EC2**: If you need more control
   - **App Runner**: Simplified container deployment
   - **Note**: This is currently a Windows Forms desktop app; requires conversion to web app/API for cloud deployment

## Pre-Deployment Checklist

- [ ] Migrate LocalDB database to AWS RDS SQL Server
- [ ] Export existing database schema and data
- [ ] Create AWS RDS instance
- [ ] Configure VPC, security groups, and subnets
- [ ] Store database credentials in AWS Secrets Manager
- [ ] Create IAM roles with appropriate permissions
- [ ] Update application to use connection string from environment
- [ ] Test application with RDS connection locally
- [ ] Create container image (if using ECS/App Runner)
- [ ] Configure CloudWatch logging

## Environment Variables Configuration

### Required Environment Variables

```bash
# Database Connection (from AWS Secrets Manager)
DATABASE_CONNECTION_STRING="Data Source=your-rds-endpoint.region.rds.amazonaws.com,1433;Initial Catalog=ToolHireDB;User ID=admin;Password=<from-secrets-manager>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# AWS Region
AWS_REGION=us-east-1

# Application Settings
ASPNETCORE_ENVIRONMENT=Production
APP_NAME=ToolHireSystem
```

### AWS ECS Task Definition Example

```json
{
  "family": "toolhire-task",
  "networkMode": "awsvpc",
  "requiresCompatibilities": ["FARGATE"],
  "cpu": "512",
  "memory": "1024",
  "containerDefinitions": [
    {
      "name": "toolhire-app",
      "image": "your-account.dkr.ecr.us-east-1.amazonaws.com/toolhire:latest",
      "environment": [
        {
          "name": "ASPNETCORE_ENVIRONMENT",
          "value": "Production"
        },
        {
          "name": "AWS_REGION",
          "value": "us-east-1"
        }
      ],
      "secrets": [
        {
          "name": "DATABASE_CONNECTION_STRING",
          "valueFrom": "arn:aws:secretsmanager:us-east-1:123456789:secret:toolhire/database-xxxxx:ConnectionString::"
        }
      ],
      "logConfiguration": {
        "logDriver": "awslogs",
        "options": {
          "awslogs-group": "/ecs/toolhire",
          "awslogs-region": "us-east-1",
          "awslogs-stream-prefix": "app"
        }
      }
    }
  ]
}
```

## Database Migration Steps

### 1. Export LocalDB Data

```bash
# Using SQL Server Management Studio or sqlcmd
sqlcmd -S (LocalDB)\\MSSQLLocalDB -d ToolHireDB -E -Q "SELECT * FROM Supply" -o supply_data.csv
# Export all tables similarly
```

### 2. Create RDS Instance

```bash
# Using AWS CLI
aws rds create-db-instance \\
    --db-instance-identifier toolhire-db \\
    --db-instance-class db.t3.medium \\
    --engine sqlserver-ex \\
    --master-username admin \\
    --master-user-password <your-secure-password> \\
    --allocated-storage 20 \\
    --vpc-security-group-ids sg-xxxxx \\
    --db-subnet-group-name your-subnet-group \\
    --backup-retention-period 7 \\
    --multi-az
```

### 3. Store Credentials in Secrets Manager

```bash
aws secretsmanager create-secret \\
    --name toolhire/database/credentials \\
    --description "ToolHire database credentials" \\
    --secret-string '{
      "username":"admin",
      "password":"your-secure-password",
      "engine":"sqlserver",
      "host":"toolhire-db.xxxxx.us-east-1.rds.amazonaws.com",
      "port":1433,
      "dbname":"ToolHireDB"
    }'
```

### 4. Import Data to RDS

```bash
# Connect to RDS and import data
sqlcmd -S toolhire-db.xxxxx.us-east-1.rds.amazonaws.com -U admin -P <password> -i import_script.sql
```

## Security Considerations

1. **No Hardcoded Credentials**: All credentials from environment/Secrets Manager
2. **SQL Injection Protected**: All queries use parameterized commands
3. **Encrypted Connections**: RDS connections use TLS/SSL
4. **IAM Roles**: Use IAM roles for AWS service access
5. **Security Groups**: Restrict database access to application tier only
6. **Secrets Rotation**: Enable automatic rotation in Secrets Manager

## Monitoring and Logging

### CloudWatch Integration

```csharp
// Add to Program.cs or startup
services.AddLogging(logging =>
{
    logging.AddAWSProvider();
    logging.SetMinimumLevel(LogLevel.Information);
});
```

### Key Metrics to Monitor

- Database connection pool utilization
- Query execution times
- Error rates
- Resource utilization (CPU, memory)

## Testing

### Local Testing with RDS

1. Set environment variable:
   ```bash
   export DATABASE_CONNECTION_STRING="Data Source=your-rds-endpoint.region.rds.amazonaws.com,1433;..."
   ```

2. Run application locally:
   ```bash
   dotnet run
   ```

3. Verify connection to RDS works

### Integration Testing

- Test all CRUD operations
- Verify parameterized queries work correctly
- Test connection pooling behavior
- Verify proper resource disposal

## Known Limitations

1. **Windows Forms Architecture**: This is a desktop application requiring Windows environment
   - **Recommendation**: Convert to ASP.NET Core Web API + web/mobile frontend for true cloud deployment
   - Current state allows running on Windows-based EC2 but not ideal for cloud-native deployment

2. **Entity Framework 6**: Legacy version
   - **Recommendation**: Migrate to Entity Framework Core for better cloud performance

3. **No Health Checks**: Add health check endpoints for load balancers

4. **No Circuit Breakers**: Add resilience patterns (Polly library) for transient failure handling

## Next Steps for Full Cloud Native Deployment

1. **Architectural Redesign**:
   - Extract business logic from Windows Forms
   - Create ASP.NET Core Web API project
   - Implement repository pattern with dependency injection
   - Build new frontend (React, Angular, or Blazor)

2. **Containerization**:
   - Create Dockerfile
   - Build container image
   - Push to Amazon ECR

3. **Infrastructure as Code**:
   - Create CloudFormation or Terraform templates
   - Automate deployment pipeline
   - Implement CI/CD with AWS CodePipeline

4. **Advanced Patterns**:
   - Implement caching (Redis/ElastiCache)
   - Add distributed tracing (X-Ray)
   - Implement API Gateway for rate limiting

## Support and Documentation

- AWS RDS Documentation: https://docs.aws.amazon.com/rds/
- AWS Secrets Manager: https://docs.aws.amazon.com/secretsmanager/
- AWS ECS: https://docs.aws.amazon.com/ecs/
- SQL Server on AWS: https://aws.amazon.com/sql-server/

## Rollback Plan

1. Keep LocalDB backup before migration
2. Take RDS snapshots before major changes
3. Use blue-green deployment for zero-downtime updates
4. Maintain ability to quickly revert to previous version
