# Get-LatestWebView2Version.ps1
# Script to get the latest stable version of Microsoft.Web.WebView2 from NuGet

param(
    [switch]$Prerelease = $false
)

try {
    # Use NuGet API to get package information
    $packageId = "Microsoft.Web.WebView2"
    
    Write-Host "Fetching latest stable version information for $packageId..." -ForegroundColor Green
    
    # Try multiple approaches to get the latest stable version
    $latestVersion = $null
    
    # Method 1: Try the search API first (most reliable for latest versions)
    try {
        Write-Host "Trying search API (most reliable)..." -ForegroundColor Cyan
        $searchUrl = "https://azuresearch-usnc.nuget.org/query?q=$packageId&prerelease=false&semVerLevel=2.0.0&take=1"
        $searchResponse = Invoke-RestMethod -Uri $searchUrl -Method Get
        
        if ($searchResponse.data -and $searchResponse.data.Count -gt 0) {
            $packageData = $searchResponse.data[0]
            $latestVersion = $packageData.version
            Write-Host "Found latest stable version via search API: $latestVersion" -ForegroundColor Green
        }
    } catch {
        Write-Host "Search API failed: $($_.Exception.Message)" -ForegroundColor Yellow
    }
    
    # Method 2: Try alternative search endpoint if primary failed
    if (-not $latestVersion) {
        try {
            Write-Host "Trying alternative search endpoint..." -ForegroundColor Cyan
            $altSearchUrl = "https://azuresearch-ussc.nuget.org/query?q=$packageId&prerelease=false&semVerLevel=2.0.0&take=1"
            $altSearchResponse = Invoke-RestMethod -Uri $altSearchUrl -Method Get
            
            if ($altSearchResponse.data -and $altSearchResponse.data.Count -gt 0) {
                $packageData = $altSearchResponse.data[0]
                $latestVersion = $packageData.version
                Write-Host "Found latest stable version via alternative search API: $latestVersion" -ForegroundColor Green
            }
        } catch {
            Write-Host "Alternative search API failed: $($_.Exception.Message)" -ForegroundColor Yellow
        }
    }
    
    # Method 3: Try the flatcontainer API (may have stale data)
    if (-not $latestVersion) {
        try {
            $apiUrl = "https://api.nuget.org/v3-flatcontainer/$packageId/index.json"
            Write-Host "Trying flatcontainer API (may have stale data)..." -ForegroundColor Cyan
            $response = Invoke-RestMethod -Uri $apiUrl -Method Get
            
            if ($response.versions) {
                # Filter for stable versions only (no prerelease identifiers)
                $stableVersions = $response.versions | Where-Object { $_ -notmatch '[-]' -and $_ -match '^\d+\.\d+\.\d+$' }
                if ($stableVersions) {
                    # Sort by version number and get the latest
                    $latestVersion = $stableVersions | Sort-Object { [Version]$_ } -Descending | Select-Object -First 1
                    Write-Host "Found latest stable version via flatcontainer API: $latestVersion" -ForegroundColor Green
                }
            }
        } catch {
            Write-Host "Flatcontainer API failed: $($_.Exception.Message)" -ForegroundColor Yellow
        }
    }
    
    # Method 4: Try registration API (with better error handling)
    if (-not $latestVersion) {
        try {
            Write-Host "Trying registration API..." -ForegroundColor Cyan
            $regUrl = "https://api.nuget.org/v3/registration5-semver1/$($packageId.ToLower())/index.json"
            $regResponse = Invoke-RestMethod -Uri $regUrl -Method Get
            
            if ($regResponse.items -and $regResponse.items.Count -gt 0) {
                # Filter out prerelease versions and sort by version
                $stableItems = $regResponse.items | Where-Object { 
                    $_.upper -notmatch '[-]' -and $_.upper -match '^\d+\.\d+\.\d+$' 
                }
                if ($stableItems) {
                    $latestItem = $stableItems | Sort-Object { [Version]$_.upper } -Descending | Select-Object -First 1
                    if ($latestItem.items -and $latestItem.items.Count -gt 0) {
                        $latestVersion = $latestItem.items[0].catalogEntry.version
                        Write-Host "Found latest stable version via registration API: $latestVersion" -ForegroundColor Green
                    }
                }
            }
        } catch {
            Write-Host "Registration API failed: $($_.Exception.Message)" -ForegroundColor Yellow
        }
    }
    
    if ($latestVersion) {
        Write-Host "Latest version: $latestVersion" -ForegroundColor Yellow
        return $latestVersion
    } else {
        Write-Error "No suitable version found"
        return $null
    }
}
catch {
    Write-Error "Failed to fetch version information: $($_.Exception.Message)"
    return $null
}
