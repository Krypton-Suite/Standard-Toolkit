# Rewrites project template PackageReference IDs to match the templates release channel.
# Used by .github/workflows/templates-release.yml and for local VSIX builds.
#
# Channel mapping (see https://github.com/Krypton-Suite/Standard-Toolkit/issues/3566):
#   stable / current / dev-*  -> Krypton.Standard.Toolkit
#   canary                    -> Krypton.Standard.Toolkit.Canary
#   alpha                     -> Krypton.Standard.Toolkit.Nightly

[CmdletBinding()]
param(
    [string]$Channel = 'stable',
    [string]$RepoRoot = (Get-Location).Path
)

$ErrorActionPreference = 'Stop'

$packageId = switch ($Channel.ToLowerInvariant()) {
    'canary' { 'Krypton.Standard.Toolkit.Canary' }
    'alpha' { 'Krypton.Standard.Toolkit.Nightly' }
    default { 'Krypton.Standard.Toolkit' }
}

$templateCsprojs = @(
    (Join-Path $RepoRoot 'Templates\ProjectTemplates\KryptonWinFormsApp\KryptonWinFormsApp.csproj'),
    (Join-Path $RepoRoot 'Templates\ProjectTemplates\KryptonRibbonWinFormsApp\KryptonRibbonWinFormsApp.csproj')
)

$utf8NoBom = New-Object System.Text.UTF8Encoding $false
$pattern = 'Include="Krypton\.Standard\.Toolkit(?:\.Canary|\.Nightly)?"'
$targetInclude = "Include=`"$packageId`""

foreach ($path in $templateCsprojs) {
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Missing template project file: $path"
    }

    $text = [System.IO.File]::ReadAllText($path, $utf8NoBom)
    if ($text -match [regex]::Escape($targetInclude)) {
        Write-Host "Already set to '$packageId' in: $path"
        continue
    }

    $updated = [regex]::Replace($text, $pattern, $targetInclude)
    if ($updated -eq $text) {
        throw "PackageReference for Krypton.Standard.Toolkit was not found in: $path"
    }

    [System.IO.File]::WriteAllText($path, $updated, $utf8NoBom)
}

Write-Host "Applied templates NuGet package '$packageId' (channel: $Channel)."
