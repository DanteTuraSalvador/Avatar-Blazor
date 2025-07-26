# Skills Manager - Performance Test
# PowerShell script to test application performance

param(
    [string]$BaseUrl = "http://localhost:5093",
    [int]$Iterations = 5
)

Write-Host "🚀 Skills Manager - Performance Test" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "Base URL: $BaseUrl" -ForegroundColor White
Write-Host "Iterations: $Iterations" -ForegroundColor White
Write-Host ""

# Test endpoints
$endpoints = @(
    @{ Name = "Home Page"; Url = "$BaseUrl/" },
    @{ Name = "Skills Page"; Url = "$BaseUrl/skills" },
    @{ Name = "Team Members Page"; Url = "$BaseUrl/team-members" }
)

$results = @()

foreach ($endpoint in $endpoints) {
    Write-Host "Testing: $($endpoint.Name)" -ForegroundColor Yellow
    
    $times = @()
    $successCount = 0
    
    for ($i = 1; $i -le $Iterations; $i++) {
        try {
            $stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
            
            $response = Invoke-WebRequest -Uri $endpoint.Url -UseBasicParsing -TimeoutSec 10
            
            $stopwatch.Stop()
            $responseTime = $stopwatch.ElapsedMilliseconds
            
            if ($response.StatusCode -eq 200) {
                $times += $responseTime
                $successCount++
                Write-Host "  ✅ Iteration $i`: $responseTime ms" -ForegroundColor Green
            } else {
                Write-Host "  ❌ Iteration $i`: HTTP $($response.StatusCode)" -ForegroundColor Red
            }
        }
        catch {
            Write-Host "  ❌ Iteration $i`: Error - $($_.Exception.Message)" -ForegroundColor Red
        }
        
        Start-Sleep -Milliseconds 100
    }
    
    if ($times.Count -gt 0) {
        $avgTime = ($times | Measure-Object -Average).Average
        $minTime = ($times | Measure-Object -Minimum).Minimum
        $maxTime = ($times | Measure-Object -Maximum).Maximum

        $result = @{
            Endpoint = $endpoint.Name
            Url = $endpoint.Url
            SuccessRate = [math]::Round(($successCount / $Iterations) * 100, 2)
            AverageTime = [math]::Round($avgTime, 2)
            MinTime = $minTime
            MaxTime = $maxTime
            TotalRequests = $Iterations
            SuccessfulRequests = $successCount
        }

        $results += $result
        
        Write-Host "  📊 Results:" -ForegroundColor Cyan
        Write-Host "     Success Rate: $($result.SuccessRate)%" -ForegroundColor White
        Write-Host "     Average Time: $($result.AverageTime) ms" -ForegroundColor White
        Write-Host "     Min Time: $($result.MinTime) ms" -ForegroundColor White
        Write-Host "     Max Time: $($result.MaxTime) ms" -ForegroundColor White
    } else {
        Write-Host "  ❌ No successful requests" -ForegroundColor Red
    }
    
    Write-Host ""
}

# Summary
Write-Host "📋 Performance Test Summary" -ForegroundColor Cyan
Write-Host "===========================" -ForegroundColor Cyan

foreach ($result in $results) {
    Write-Host "$($result.Endpoint):" -ForegroundColor Yellow
    Write-Host "  Success Rate: $($result.SuccessRate)%" -ForegroundColor White
    Write-Host "  Average Response Time: $($result.AverageTime) ms" -ForegroundColor White
    
    if ($result.AverageTime -lt 100) {
        Write-Host "  Performance: ✅ Excellent" -ForegroundColor Green
    } elseif ($result.AverageTime -lt 500) {
        Write-Host "  Performance: ✅ Good" -ForegroundColor Green
    } elseif ($result.AverageTime -lt 1000) {
        Write-Host "  Performance: ⚠️ Acceptable" -ForegroundColor Yellow
    } else {
        Write-Host "  Performance: ❌ Needs Improvement" -ForegroundColor Red
    }
    Write-Host ""
}

# Overall assessment
$overallSuccess = ($results | Measure-Object -Property SuccessRate -Average).Average
$overallAvgTime = ($results | Measure-Object -Property AverageTime -Average).Average

Write-Host "🎯 Overall Assessment:" -ForegroundColor Cyan
Write-Host "  Overall Success Rate: $([math]::Round($overallSuccess, 2))%" -ForegroundColor White
Write-Host "  Overall Average Time: $([math]::Round($overallAvgTime, 2)) ms" -ForegroundColor White

if ($overallSuccess -ge 95 -and $overallAvgTime -lt 500) {
    Write-Host "  Status: ✅ Application is performing excellently!" -ForegroundColor Green
} elseif ($overallSuccess -ge 90 -and $overallAvgTime -lt 1000) {
    Write-Host "  Status: ✅ Application is performing well!" -ForegroundColor Green
} elseif ($overallSuccess -ge 80) {
    Write-Host "  Status: ⚠️ Application performance is acceptable" -ForegroundColor Yellow
} else {
    Write-Host "  Status: ❌ Application performance needs attention" -ForegroundColor Red
}

Write-Host ""
Write-Host "🎊 Performance test completed!" -ForegroundColor Cyan
