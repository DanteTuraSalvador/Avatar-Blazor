# Skills Manager - Deployment Guide

This document provides comprehensive instructions for deploying the Skills Manager using multiple deployment options.

## 🔥 **Recommended: Hybrid Development Setup**

**Database in Docker + Web App Local = Best Development Experience**

### Benefits
- ✅ **Fast development** - Local web app with hot reload
- ✅ **Production-like database** - SQL Server in Docker
- ✅ **Easy debugging** - Full IDE support with breakpoints
- ✅ **No Telerik issues** - Avoids Docker build problems
- ✅ **Isolated database** - Clean, consistent data environment

### Quick Start (Hybrid)

1. **Start Database Services:**
   ```powershell
   .\scripts\start-db.ps1
   ```

2. **Run Web Application:**
   ```powershell
   dotnet run --project src/Avatar.Web
   ```

3. **Access Application:**
   - **Web App**: http://localhost:5093
   - **Skills Builder**: http://localhost:5093/skills-builder ✨
   - **SQL Server**: localhost:1433 (sa/YourStrong@Passw0rd)
   - **Redis**: localhost:6379

4. **Stop Database Services:**
   ```powershell
   .\scripts\stop-db.ps1
   ```

### Visual Studio Setup (Hybrid)

1. **Start Database Services:**
   ```powershell
   .\scripts\start-db.ps1
   ```

2. **Open Solution:**
   ```bash
   start Avatar.sln
   ```

3. **Set Startup Project:**
   - Right-click `Avatar.Web` → Set as Startup Project

4. **Start Debugging:**
   - Press **F5** for debugging
   - Or **Ctrl+F5** for running without debugging

## 🐳 Docker Deployment

### Prerequisites
- Docker Desktop installed and running
- Docker Compose v2.0+
- 4GB+ available RAM

### Quick Start with Docker

1. **Build and Run (Development)**
   ```powershell
   .\scripts\docker-build.ps1 -Build -Run
   ```

2. **Access the Application**
   - **Web App**: http://localhost:8080
   - **SQL Server**: localhost:1433 (sa/YourStrong@Passw0rd)
   - **Redis**: localhost:6379

3. **View Logs**
   ```powershell
   .\scripts\docker-build.ps1 -Logs
   ```

4. **Stop Services**
   ```powershell
   .\scripts\docker-build.ps1 -Stop
   ```

### Manual Docker Commands

```bash
# Build images
docker-compose build

# Start services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down

# Clean up (removes volumes)
docker-compose down -v --remove-orphans
```

### Production Deployment

For production, use the nginx profile:
```bash
docker-compose --profile production up -d
```

## 🚀 .NET Aspire Deployment

### Prerequisites
- .NET 8.0 SDK
- .NET Aspire workload installed
- Docker Desktop (for containers)

### Install .NET Aspire Workload
```powershell
dotnet workload update
dotnet workload install aspire
```

### Quick Start with Aspire

1. **Build and Run**
   ```powershell
   .\scripts\aspire-run.ps1 -Build
   ```

2. **Access the Application**
   - **Aspire Dashboard**: https://localhost:15888
   - **Web App**: https://localhost:7139
   - **SQL Server**: Managed by Aspire
   - **Redis**: Managed by Aspire

### Manual Aspire Commands

```bash
# Build solution
dotnet build

# Run Aspire AppHost
dotnet run --project src/Avatar.AppHost/Avatar.AppHost.csproj
```

## 🔧 Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Application environment | Development |
| `ConnectionStrings__DefaultConnection` | Database connection | In-Memory |
| `ASPNETCORE_URLS` | Application URLs | http://+:8080 |

### Docker Environment Files

Create `.env` file for custom configuration:
```env
ASPNETCORE_ENVIRONMENT=Production
SA_PASSWORD=YourCustomPassword123!
REDIS_PASSWORD=YourRedisPassword123!
```

## 📊 Monitoring and Health Checks

### Health Check Endpoints
- **Application Health**: `/health`
- **Database Health**: `/health/db`
- **Ready Check**: `/health/ready`
- **Live Check**: `/health/live`

### Aspire Dashboard Features
- **Service Discovery**: View all running services
- **Telemetry**: Logs, metrics, and traces
- **Health Monitoring**: Real-time health status
- **Configuration**: Environment variables and settings

## 🔒 Security Considerations

### Production Security
1. **Change Default Passwords**
   ```yaml
   environment:
     - SA_PASSWORD=YourSecurePassword123!
   ```

2. **Use Secrets Management**
   ```yaml
   secrets:
     db_password:
       file: ./secrets/db_password.txt
   ```

3. **Enable HTTPS**
   ```yaml
   environment:
     - ASPNETCORE_URLS=https://+:443;http://+:80
   ```

## 🚀 Scaling and Performance

### Docker Scaling
```bash
# Scale web application
docker-compose up -d --scale skills-manager=3

# Use load balancer
docker-compose --profile production up -d
```

### Aspire Scaling
Aspire automatically handles:
- Service discovery
- Load balancing
- Health monitoring
- Telemetry collection

## 🛠 Troubleshooting

### Common Docker Issues

1. **Port Conflicts**
   ```bash
   # Check port usage
   netstat -an | findstr :8080
   
   # Change ports in docker-compose.yml
   ports:
     - "8081:8080"
   ```

2. **Memory Issues**
   ```bash
   # Increase Docker memory limit
   # Docker Desktop > Settings > Resources > Memory
   ```

3. **Database Connection**
   ```bash
   # Check SQL Server logs
   docker-compose logs sqlserver
   
   # Test connection
   docker exec -it skills-manager-db /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd
   ```

### Common Aspire Issues

1. **Workload Not Installed**
   ```bash
   dotnet workload install aspire
   ```

2. **Port Conflicts**
   ```bash
   # Check launchSettings.json
   # Modify ports if needed
   ```

3. **Service Discovery**
   ```bash
   # Check Aspire dashboard for service status
   # Verify service references in AppHost
   ```

## 📝 Development Workflow

### Docker Development
1. Make code changes
2. Rebuild: `.\scripts\docker-build.ps1 -Build`
3. Test: Access http://localhost:8080
4. Debug: `.\scripts\docker-build.ps1 -Logs`

### Aspire Development
1. Make code changes
2. Aspire auto-reloads (hot reload enabled)
3. Monitor: Check Aspire dashboard
4. Debug: View telemetry and logs

## 🎯 Next Steps

1. **Set up CI/CD Pipeline**
2. **Configure Production Database**
3. **Implement Authentication**
4. **Add Monitoring and Alerting**
5. **Set up Backup Strategy**
