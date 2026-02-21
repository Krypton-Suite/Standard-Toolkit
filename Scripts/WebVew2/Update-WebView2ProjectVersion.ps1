# Update-WebView2ProjectVersion.ps1
# Script to update the project file with the latest WebView2 SDK version

param(
    [string]$ProjectPath = "Source/Krypton Components/Krypton.Utilities/Krypton.Utilities.csproj",
    [switch]$WhatIf = $false
)

function Get-LatestWebView2Version {
    try {
        $packageId = "Microsoft.Web.WebView2"
        $apiUrl = "https://api.nuget.org/v3-flatcontainer/$packageId/index.json"
        
        Write-Host "Fetching latest version information for $packageId..." -ForegroundColor Green
        
        $response = Invoke-RestMethod -Uri $apiUrl -Method Get
        
        # Only stable versions (no prerelease identifiers)
        $stableVersions = $response.versions | Where-Object { $_ -notmatch '[-]' } | 
                         Sort-Object { [Version]$_ } -Descending
        $latestVersion = $stableVersions[0]
        
        if ($latestVersion) {
            Write-Host "Latest stable version: $latestVersion" -ForegroundColor Yellow
            return $latestVersion
        } else {
            Write-Error "No stable version found"
            return $null
        }
    }
    catch {
        Write-Error "Failed to fetch version information: $($_.Exception.Message)"
        return $null
    }
}

function Update-ProjectFile {
    param(
        [string]$ProjectPath,
        [string]$NewVersion,
        [switch]$WhatIf
    )
    
    if (-not (Test-Path $ProjectPath)) {
        Write-Error "Project file not found: $ProjectPath"
        return $false
    }
    
    $content = Get-Content $ProjectPath -Raw
    
    # Check if project file uses floating versions (1.0.*)
    if ($content -match 'Version="1\.0\.\*"') {
        Write-Host "Project file uses floating version (1.0.*) - no manual update needed." -ForegroundColor Green
        Write-Host "NuGet will automatically resolve to the latest 1.0.x version on restore." -ForegroundColor Yellow
        return $false
    }
    
    # Update .NET Framework references (for file-based references, if any)
    $netFrameworkPattern = 'Microsoft\.Web\.WebView2\.Core, Version=([^,]+), Culture=neutral'
    $netFrameworkReplacement = "Microsoft.Web.WebView2.Core, Version=$NewVersion, Culture=neutral"
    
    $updatedContent = $content -replace $netFrameworkPattern, $netFrameworkReplacement
    
    # Also check for PackageReference with specific versions
    $packageRefPattern = '<PackageReference Include="Microsoft\.Web\.WebView2" Version="([^"]+)"'
    if ($content -match $packageRefPattern) {
        $oldVersion = $matches[1]
        if ($oldVersion -ne $NewVersion -and $oldVersion -ne "1.0.*") {
            $updatedContent = $updatedContent -replace "($packageRefPattern)", "<PackageReference Include=`"Microsoft.Web.WebView2`" Version=`"$NewVersion`""
        }
    }
    
    if ($updatedContent -ne $content) {
        if ($WhatIf) {
            Write-Host "Would update project file with version $NewVersion" -ForegroundColor Cyan
        } else {
            Set-Content $ProjectPath $updatedContent -NoNewline
            Write-Host "Updated project file with version $NewVersion" -ForegroundColor Green
        }
        return $true
    } else {
        Write-Warning "No version references found to update in project file (or already using floating versions)"
        return $false
    }
}

# Main execution
$latestVersion = Get-LatestWebView2Version

if ($latestVersion) {
    $updated = Update-ProjectFile -ProjectPath $ProjectPath -NewVersion $latestVersion -WhatIf:$WhatIf
    
    if ($updated -and -not $WhatIf) {
        Write-Host "Project file updated successfully!" -ForegroundColor Green
        Write-Host "You may need to rebuild the solution for changes to take effect." -ForegroundColor Yellow
    }
} else {
    Write-Error "Could not determine latest WebView2 version"
    exit 1
}
