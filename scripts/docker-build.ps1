# Skills Manager - Docker Build Script
# PowerShell script to build and run the application with Docker

param(
    [string]$Environment = "Development",
    [switch]$Build,
    [switch]$Run,
    [switch]$Stop,
    [switch]$Clean,
    [switch]$Logs
)

Write-Host "🐳 Skills Manager - Docker Operations" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan

# Set working directory to project root
$ProjectRoot = Split-Path -Parent $PSScriptRoot
Set-Location $ProjectRoot

if ($Build) {
    Write-Host "🔨 Building Docker images..." -ForegroundColor Yellow
    docker-compose build --no-cache
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Docker images built successfully!" -ForegroundColor Green
    } else {
        Write-Host "❌ Docker build failed!" -ForegroundColor Red
        exit 1
    }
}

if ($Run) {
    Write-Host "🚀 Starting services..." -ForegroundColor Yellow
    docker-compose up -d
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Services started successfully!" -ForegroundColor Green
        Write-Host "🌐 Application available at: http://localhost:8080" -ForegroundColor Cyan
        Write-Host "📊 SQL Server available at: localhost:1433" -ForegroundColor Cyan
        Write-Host "🔴 Redis available at: localhost:6379" -ForegroundColor Cyan
    } else {
        Write-Host "❌ Failed to start services!" -ForegroundColor Red
        exit 1
    }
}

if ($Stop) {
    Write-Host "🛑 Stopping services..." -ForegroundColor Yellow
    docker-compose down
    Write-Host "✅ Services stopped!" -ForegroundColor Green
}

if ($Clean) {
    Write-Host "🧹 Cleaning up Docker resources..." -ForegroundColor Yellow
    docker-compose down -v --remove-orphans
    docker system prune -f
    Write-Host "✅ Cleanup completed!" -ForegroundColor Green
}

if ($Logs) {
    Write-Host "📋 Showing service logs..." -ForegroundColor Yellow
    docker-compose logs -f
}

if (-not ($Build -or $Run -or $Stop -or $Clean -or $Logs)) {
    Write-Host "Usage: .\docker-build.ps1 [-Build] [-Run] [-Stop] [-Clean] [-Logs]" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Examples:" -ForegroundColor Cyan
    Write-Host "  .\docker-build.ps1 -Build -Run    # Build and run"
    Write-Host "  .\docker-build.ps1 -Stop          # Stop services"
    Write-Host "  .\docker-build.ps1 -Clean         # Clean up"
    Write-Host "  .\docker-build.ps1 -Logs          # View logs"
}
