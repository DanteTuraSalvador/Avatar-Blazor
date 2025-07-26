# Stop Database Services in Docker
Write-Host "🛑 Stopping Database Services..." -ForegroundColor Yellow

docker-compose -f docker-compose.db-only.yml down

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Database services stopped successfully!" -ForegroundColor Green
}
else {
    Write-Host "❌ Failed to stop database services" -ForegroundColor Red
}
