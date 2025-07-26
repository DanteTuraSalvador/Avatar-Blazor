# Start Database Services in Docker
# This script starts only the database services in Docker containers
# while allowing the web application to run locally

Write-Host "🐳 Starting Database Services in Docker..." -ForegroundColor Cyan
Write-Host ""

# Check if Docker is running
try {
    docker info | Out-Null
    Write-Host "✅ Docker is running" -ForegroundColor Green
}
catch {
    Write-Host "❌ Docker is not running. Please start Docker Desktop first." -ForegroundColor Red
    exit 1
}

# Start database services
Write-Host "🚀 Starting SQL Server and Redis..." -ForegroundColor Yellow
docker-compose -f docker-compose.db-only.yml up -d

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ Database services started successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "📊 Services running:" -ForegroundColor Cyan
    Write-Host "  • SQL Server: localhost:1433" -ForegroundColor White
    Write-Host "  • Redis Cache: localhost:6379" -ForegroundColor White
    Write-Host ""
    Write-Host "🔗 Connection String:" -ForegroundColor Cyan
    Write-Host "  Server=localhost,1433;Database=SkillsManagerDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true" -ForegroundColor White
    Write-Host ""
    Write-Host "🌐 Now run your web application locally:" -ForegroundColor Yellow
    Write-Host "  dotnet run --project src/Avatar.Web" -ForegroundColor White
    Write-Host ""
    Write-Host "🛑 To stop database services:" -ForegroundColor Yellow
    Write-Host "  docker-compose -f docker-compose.db-only.yml down" -ForegroundColor White
}
else {
    Write-Host "❌ Failed to start database services" -ForegroundColor Red
    exit 1
}
