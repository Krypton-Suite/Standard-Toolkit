# Submits the stable Krypton templates VSIX to the Visual Studio Marketplace.
# No-ops with a warning when VSMARKETPLACE_PAT is empty, so a release can succeed
# before the publisher token exists.
# VSMARKETPLACE_PRIVATE=false makes the listing public. Any other value keeps it private.

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $VsixPath,

    [Parameter(Mandatory = $true)]
    [string] $RepoRoot
)

$ErrorActionPreference = 'Stop'

if ([string]::IsNullOrWhiteSpace($env:VSMARKETPLACE_PAT)) {
    Write-Warning "VSMARKETPLACE_PAT is not set. The stable VSIX was not submitted to the Visual Studio Marketplace."
    return
}

if (-not (Test-Path -LiteralPath $VsixPath)) {
    throw "VSIX was not found: $VsixPath"
}

$manifestPath = Join-Path $RepoRoot 'Templates\Marketplace\publishManifest.json'
if (-not (Test-Path -LiteralPath $manifestPath)) {
    throw "Missing Marketplace publish manifest: $manifestPath"
}

$privateJson = if ($env:VSMARKETPLACE_PRIVATE -eq 'false') { 'false' } else { 'true' }
$manifest = [System.IO.File]::ReadAllText($manifestPath)
$manifest = $manifest.TrimStart([char]0xFEFF)
$updated = [regex]::Replace($manifest, '"private"\s*:\s*(true|false)', ('"private": ' + $privateJson))
if ($updated -eq $manifest) {
    throw "publishManifest.json is missing a private property."
}

$utf8NoBom = New-Object System.Text.UTF8Encoding $false
[System.IO.File]::WriteAllText($manifestPath, $updated, $utf8NoBom)

$publisher = (& (Join-Path $RepoRoot 'Scripts\CI\Get-VsixPublisher.ps1') | Select-Object -Last 1)
if ([string]::IsNullOrWhiteSpace($publisher)) {
    throw "Get-VsixPublisher.ps1 did not return a path."
}

Write-Host "Using VsixPublisher: $publisher"
Write-Host "Marketplace listing private: $privateJson"
Write-Host "Publishing VSIX: $VsixPath"

& $publisher publish -payload $VsixPath -publishManifest $manifestPath -personalAccessToken $env:VSMARKETPLACE_PAT
if ($LASTEXITCODE -ne 0) {
    throw "VsixPublisher exited with code $LASTEXITCODE."
}
