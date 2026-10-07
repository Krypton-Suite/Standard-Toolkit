# Resolves the GitHub release page for the newest Visual Studio templates build
# on a channel (stable, canary, alpha, rc, current).
# Dated tags (templates-stable-yyyyMMdd-HHmm) are preferred. The legacy fixed tag
# (templates-stable) is used when no dated release exists yet.
# Prints a URL on stdout. Falls back to the repository releases page.

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $Channel
)

$server = if ($env:GITHUB_SERVER_URL) { $env:GITHUB_SERVER_URL.TrimEnd('/') } else { 'https://github.com' }
$repo = $env:GITHUB_REPOSITORY
$fallback = if ($repo) { "$server/$repo/releases" } else { "$server/Krypton-Suite/Standard-Toolkit/releases" }

if ([string]::IsNullOrWhiteSpace($repo)) {
    Write-Output $fallback
    return
}

$headers = @{
    Accept                 = 'application/vnd.github+json'
    'User-Agent'           = 'krypton-toolkit-release'
    'X-GitHub-Api-Version' = '2022-11-28'
}
if (-not [string]::IsNullOrWhiteSpace($env:GITHUB_TOKEN)) {
    $headers.Authorization = "Bearer $($env:GITHUB_TOKEN)"
}

$apiRoot = if ($env:GITHUB_API_URL) { $env:GITHUB_API_URL.TrimEnd('/') } else { 'https://api.github.com' }
$url = $fallback

try {
    $releases = Invoke-RestMethod -Uri "$apiRoot/repos/$repo/releases?per_page=100" -Headers $headers
    $datedPrefix = "templates-$Channel-"
    $dated = @($releases | Where-Object { $_.tag_name -like "$datedPrefix*" } | Select-Object -First 1)
    if ($dated.Count -gt 0 -and $dated[0].html_url) {
        $url = $dated[0].html_url
    }
    else {
        $legacy = @($releases | Where-Object { $_.tag_name -eq "templates-$Channel" } | Select-Object -First 1)
        if ($legacy.Count -gt 0 -and $legacy[0].html_url) {
            $url = $legacy[0].html_url
        }
    }
}
catch {
    Write-Warning "Could not resolve Visual Studio templates release for channel '$Channel': $_"
}

Write-Output $url
