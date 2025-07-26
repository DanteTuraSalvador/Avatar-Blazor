# Skills Builder Test
Write-Host "🧪 Testing Skills Builder Page" -ForegroundColor Cyan

$url = "http://localhost:5093/skills-builder"
Write-Host "Testing: $url" -ForegroundColor Yellow

try {
    $response = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 5
    if ($response.StatusCode -eq 200) {
        Write-Host "  ✅ SUCCESS - Status: $($response.StatusCode), Size: $($response.Content.Length) bytes" -ForegroundColor Green
        
        # Check if the page contains expected content
        $content = $response.Content
        if ($content -match "Skills Builder" -and $content -match "team members") {
            Write-Host "  ✅ Page content looks correct" -ForegroundColor Green
        } else {
            Write-Host "  ⚠️ Page content may be incomplete" -ForegroundColor Yellow
        }
    } else {
        Write-Host "  ⚠️ WARNING - Status: $($response.StatusCode)" -ForegroundColor Yellow
    }
}
catch {
    Write-Host "  ❌ ERROR - $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Write-Host "🎯 Skills Builder Test Complete!" -ForegroundColor Cyan
