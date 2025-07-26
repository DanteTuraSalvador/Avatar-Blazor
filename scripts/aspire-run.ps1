# Skills Manager - .NET Aspire Launch Script
# PowerShell script to run the application with .NET Aspire

param(
    [string]$Environment = "Development",
    [switch]$Build,
    [switch]$Clean
)

Write-Host "🚀 Skills Manager - .NET Aspire" -ForegroundColor Cyan
Write-Host "=============================================" -ForegroundColor Cyan

# Set working directory to project root
$ProjectRoot = Split-Path -Parent $PSScriptRoot
Set-Location $ProjectRoot

if ($Clean) {
    Write-Host "🧹 Cleaning solution..." -ForegroundColor Yellow
    dotnet clean
}

if ($Build) {
    Write-Host "🔨 Building solution..." -ForegroundColor Yellow
    dotnet build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Build failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "✅ Build completed successfully!" -ForegroundColor Green
}

Write-Host "🚀 Starting .NET Aspire AppHost..." -ForegroundColor Yellow
Write-Host "This will start:" -ForegroundColor Cyan
Write-Host "  • Skills Manager Web App" -ForegroundColor White
Write-Host "  • SQL Server Database" -ForegroundColor White
Write-Host "  • Redis Cache" -ForegroundColor White
Write-Host "  • Aspire Dashboard" -ForegroundColor White
Write-Host ""

# Set environment variable
$env:ASPNETCORE_ENVIRONMENT = $Environment

# Run the Aspire AppHost
dotnet run --project src/Avatar.AppHost/Avatar.AppHost.csproj

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Aspire application completed!" -ForegroundColor Green
} else {
    Write-Host "❌ Aspire application failed!" -ForegroundColor Red
    exit 1
}
