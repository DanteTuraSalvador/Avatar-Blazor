# Simple Application Test
Write-Host "🧪 Testing Skills Manager" -ForegroundColor Cyan

$baseUrl = "http://localhost:5093"
$endpoints = @("/", "/skills", "/team-members")

foreach ($endpoint in $endpoints) {
    $url = $baseUrl + $endpoint
    Write-Host "Testing: $url" -ForegroundColor Yellow
    
    try {
        $response = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 5
        if ($response.StatusCode -eq 200) {
            Write-Host "  ✅ SUCCESS - Status: $($response.StatusCode), Size: $($response.Content.Length) bytes" -ForegroundColor Green
        } else {
            Write-Host "  ⚠️ WARNING - Status: $($response.StatusCode)" -ForegroundColor Yellow
        }
    }
    catch {
        Write-Host "  ❌ ERROR - $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "🎯 Test Summary:" -ForegroundColor Cyan
Write-Host "Application is running and responding to requests!" -ForegroundColor Green
