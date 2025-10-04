# Get-LatestWebView2Version.ps1
# Script to get the latest stable version of Microsoft.Web.WebView2 from NuGet

param(
    [switch]$Prerelease = $false
)

try {
    # Use NuGet API to get package information
    $packageId = "Microsoft.Web.WebView2"
    $apiUrl = "https://api.nuget.org/v3-flatcontainer/$packageId/index.json"
    
    Write-Host "Fetching latest version information for $packageId..." -ForegroundColor Green
    
    # Get package versions
    try {
        $response = Invoke-RestMethod -Uri $apiUrl -Method Get
        
        if ($Prerelease) {
            # Include prerelease versions - sort by version number only
            $versions = $response.versions | Where-Object { $_ -match '^\d+\.\d+\.\d+' } | 
                       Sort-Object { [Version]($_ -replace '-.*$', '') } -Descending
            $latestVersion = $versions[0]
        } else {
            # Only stable versions (no prerelease identifiers)
            $stableVersions = $response.versions | Where-Object { $_ -notmatch '[-]' } | 
                             Sort-Object { [Version]$_ } -Descending
            $latestVersion = $stableVersions[0]
        }
    } catch {
        Write-Host "Primary API failed: $($_.Exception.Message)" -ForegroundColor Yellow
        Write-Host "Trying alternative search API..." -ForegroundColor Yellow
        
        # Try alternative approach using NuGet search API
        $searchUrl = "https://azuresearch-usnc.nuget.org/query?q=$packageId&prerelease=false&semVerLevel=2.0.0"
        $searchResponse = Invoke-RestMethod -Uri $searchUrl -Method Get
        
        if ($searchResponse.data -and $searchResponse.data.Count -gt 0) {
            $packageData = $searchResponse.data[0]
            $latestVersion = $packageData.version
            Write-Host "Found version using search API: $latestVersion" -ForegroundColor Green
        } else {
            throw "No package data found in search results"
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
