# Downloads Microsoft.Web.WebView2 into Krypton.Toolkit.Utilities\Lib\WebView2 for CI builds.
#
# Must be committed on alpha (nightly) and Canary (canary release). build.yml checks out the triggering ref.
# -Prerelease: required for alpha/canary/nightly (net11.0-windows). Stable WebView2 packages do not support .NET 11 yet.
# -ResolveVersionOnly -WriteGitHubOutputVersion: emit version= for actions/cache keys (nightly/canary).
# -Version: skip NuGet resolution when the workflow already resolved the version.

[CmdletBinding()]
param(
    [switch]$Prerelease,
    [string]$Version,
    [switch]$ResolveVersionOnly,
    [switch]$WriteGitHubOutputVersion,
    [string]$RepoRoot,
    [string]$CacheRoot
)

$ErrorActionPreference = 'Stop'

$PackageId = 'Microsoft.Web.WebView2'
$DefaultFallbackVersion = '1.0.3595.46'
$LibRelativePath = 'Source\Krypton Components\Krypton.Toolkit.Utilities\Lib\WebView2'
$RequiredDllNames = @(
    'Microsoft.Web.WebView2.Core.dll',
    'Microsoft.Web.WebView2.WinForms.dll',
    'WebView2Loader.dll'
)

function Get-WebView2NuGetVersion {
    param(
        [bool]$UsePrerelease,
        [string]$FallbackVersion
    )

    $prereleaseParam = if ($UsePrerelease) { 'true' } else { 'false' }
    Write-Host "Querying NuGet for WebView2 (prerelease=$prereleaseParam)..."

    $latestVersion = $null
    try {
        $searchResponse = Invoke-RestMethod `
            -Uri "https://azuresearch-usnc.nuget.org/query?q=$PackageId&prerelease=$prereleaseParam&semVerLevel=2.0.0&take=1" `
            -Method Get
        if ($searchResponse.data -and $searchResponse.data.Count -gt 0) {
            $latestVersion = $searchResponse.data[0].version
        }
    }
    catch {
        Write-Warning "NuGet search API failed: $_"
    }

    if (-not $latestVersion) {
        try {
            $response = Invoke-RestMethod -Uri "https://api.nuget.org/v3-flatcontainer/$PackageId/index.json" -Method Get
            if ($UsePrerelease) {
                $allVersions = $response.versions | Where-Object { $_ -match '^\d+\.\d+\.\d+' }
                if ($allVersions) {
                    $latestVersion = $allVersions | Sort-Object { [System.Version]($_ -replace '-.*', '') } -Descending | Select-Object -First 1
                }
            }
            else {
                $stableVersions = $response.versions | Where-Object { $_ -notmatch '[-]' -and $_ -match '^\d+\.\d+\.\d+$' }
                if ($stableVersions) {
                    $latestVersion = $stableVersions | Sort-Object { [System.Version]$_ } -Descending | Select-Object -First 1
                }
            }
        }
        catch {
            Write-Warning "NuGet flatcontainer fallback failed: $_"
        }
    }

    if (-not $latestVersion) {
        Write-Warning "Using fallback WebView2 version: $FallbackVersion"
        $latestVersion = $FallbackVersion
    }

    return $latestVersion
}

function Invoke-PopulateWebView2Lib {
    param(
        [string]$ResolvedVersion,
        [string]$Root,
        [string]$TempDir
    )

    $libDir = Join-Path $Root $LibRelativePath
    New-Item -ItemType Directory -Force -Path $libDir | Out-Null
    New-Item -ItemType Directory -Force -Path $TempDir | Out-Null

    $nupkg = Join-Path $TempDir "$PackageId.$ResolvedVersion.nupkg"
    $extractDir = Join-Path $TempDir 'extract'
    $expectedExtractedDll = Join-Path $extractDir 'build\native\WebView2Loader.dll'

    if ((Test-Path -LiteralPath $TempDir) -and -not (Test-Path -LiteralPath $expectedExtractedDll)) {
        Write-Warning 'Detected incomplete WebView2 cache state, rebuilding cache folder.'
        Remove-Item -LiteralPath $TempDir -Recurse -Force -ErrorAction SilentlyContinue
        New-Item -ItemType Directory -Force -Path $TempDir | Out-Null
    }

    if (-not (Test-Path -LiteralPath $expectedExtractedDll)) {
        Write-Host 'Downloading package...'
        Invoke-WebRequest `
            -Uri "https://www.nuget.org/api/v2/package/$PackageId/$ResolvedVersion" `
            -OutFile $nupkg

        Write-Host 'Extracting package...'
        Expand-Archive $nupkg $extractDir -Force
    }
    else {
        Write-Host 'Using cached WebView2 package extraction.'
    }

    Write-Host 'Copying required DLLs...'
    foreach ($file in $RequiredDllNames) {
        $found = Get-ChildItem $extractDir -Recurse -Filter $file | Select-Object -First 1
        if ($found) {
            Copy-Item $found.FullName $libDir -Force
            Write-Host "Copied $file"
        }
        else {
            Write-Warning "$file not found in package"
        }
    }

    Write-Host 'WebView2 population complete.'
}

if (-not $RepoRoot) {
    $RepoRoot = $env:GITHUB_WORKSPACE
    if (-not $RepoRoot) {
        $RepoRoot = (Get-Location).Path
    }
}

if (-not $CacheRoot) {
    $runnerTemp = $env:RUNNER_TEMP
    if (-not $runnerTemp) {
        $runnerTemp = [System.IO.Path]::GetTempPath()
    }
    $CacheRoot = Join-Path $runnerTemp 'webview2'
}

$resolvedVersion = $Version
if ([string]::IsNullOrWhiteSpace($resolvedVersion)) {
    $resolvedVersion = Get-WebView2NuGetVersion -UsePrerelease ([bool]$Prerelease) -FallbackVersion $DefaultFallbackVersion
}

$channelLabel = if ($Prerelease) { 'preview/prerelease' } else { 'latest stable' }
Write-Host "Using WebView2 ($channelLabel): $resolvedVersion"

if ($WriteGitHubOutputVersion) {
    if (-not $env:GITHUB_OUTPUT) {
        Write-Error 'GITHUB_OUTPUT is not set; cannot write version output.'
    }
    "version=$resolvedVersion" | Out-File -FilePath $env:GITHUB_OUTPUT -Encoding utf8 -Append
}

if ($ResolveVersionOnly) {
    return
}

Invoke-PopulateWebView2Lib -ResolvedVersion $resolvedVersion -Root $RepoRoot -TempDir $CacheRoot
