# New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
# Modifications by Peter Wagner (aka PWagner1), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
#
# Partially updates BSD license header "Modifications by ..." contributor lines.
# Replaces the contributor list after the fixed PWagner1 / Smurf-IV prefix, preserving
# each file's year range and "All rights reserved." suffix.
# Example:
#   pwsh .github/scripts/Update-LicenseHeaders.ps1 -DryRun

param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path,
    [switch]$DryRun
)

$ErrorActionPreference = 'Stop'

$ModificationsMarker = 'Modifications by Peter Wagner (aka '
$CanonicalPrefix = 'Modifications by Peter Wagner (aka PWagner1), Simon Coghlan (aka Smurf-IV)'
$CanonicalContributors = ', Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs)'

$linePattern = [regex]::new(
    '^(?<leading>.*?)(?<prefix>Modifications by Peter Wagner \(aka (?:Wagnerp|PWagner1)\), Simon Coghlan \(aka Smurf-IV\))(?<old>,.*?)(?<suffix> et al\.\s+\d{4}\s*-\s*\d{4}\.\s*All rights reserved\.\s*)$',
    [System.Text.RegularExpressions.RegexOptions]::Compiled
)

$scanExtensions = @('.cs', '.licenseheader', '.yml', '.yaml', '.ps1')
$excludeDirectoryNames = @('Bin', 'obj', '.git', 'node_modules', 'packages')

function Test-ExcludedDirectory {
    param([string]$DirectoryPath)

    foreach ($segment in $DirectoryPath.Split([IO.Path]::DirectorySeparatorChar, [IO.Path]::AltDirectorySeparatorChar)) {
        if ($excludeDirectoryNames -contains $segment) {
            return $true
        }
    }

    return $false
}

function Update-LicenseHeaderLine {
    param([string]$Line)

    $match = $linePattern.Match($Line)
    if (-not $match.Success) {
        return $Line
    }

    if ($match.Groups['prefix'].Value -eq $CanonicalPrefix -and $match.Groups['old'].Value -eq $CanonicalContributors) {
        return $Line
    }

    return $match.Groups['leading'].Value + $CanonicalPrefix + $CanonicalContributors + $match.Groups['suffix'].Value
}

$utf8Bom = New-Object System.Text.UTF8Encoding($true)
$filesScanned = 0
$filesChanged = 0
$linesChanged = 0
$changedFiles = [System.Collections.Generic.List[string]]::new()

$searchRoot = Join-Path $RepositoryRoot 'Source'
$searchRoots = @(
    $searchRoot
    (Join-Path $RepositoryRoot '.github')
    (Join-Path $RepositoryRoot 'Scripts')
)

foreach ($root in $searchRoots) {
    if (-not (Test-Path -LiteralPath $root)) {
        continue
    }

    Get-ChildItem -LiteralPath $root -Recurse -File | ForEach-Object {
        if (Test-ExcludedDirectory -DirectoryPath $_.DirectoryName) {
            return
        }

        if ($scanExtensions -notcontains $_.Extension) {
            return
        }

        $filesScanned++
        $original = [System.IO.File]::ReadAllText($_.FullName)
        if ($original -notmatch [regex]::Escape($ModificationsMarker)) {
            return
        }

        $newLineEnding = if ($original -match "`r`n") { "`r`n" } else { "`n" }
        $lines = $original -split "`r`n|`n"
        $fileLinesChanged = 0

        for ($i = 0; $i -lt $lines.Count; $i++) {
            $updatedLine = Update-LicenseHeaderLine -Line $lines[$i]
            if ($updatedLine -ne $lines[$i]) {
                $lines[$i] = $updatedLine
                $fileLinesChanged++
            }
        }

        if ($fileLinesChanged -eq 0) {
            return
        }

        $linesChanged += $fileLinesChanged
        $filesChanged++
        $relativePath = [IO.Path]::GetRelativePath($RepositoryRoot, $_.FullName)
        [void]$changedFiles.Add($relativePath)

        Write-Host ("{0}: {1} line(s) updated" -f $relativePath, $fileLinesChanged)

        if (-not $DryRun) {
            $updatedContent = ($lines -join $newLineEnding)
            if ($original.EndsWith($newLineEnding) -and -not $updatedContent.EndsWith($newLineEnding)) {
                $updatedContent += $newLineEnding
            }

            [System.IO.File]::WriteAllText($_.FullName, $updatedContent, $utf8Bom)
        }
    }
}

Write-Host ''
Write-Host ('Scanned {0} file(s); {1} file(s) would change ({2} line(s)).' -f $filesScanned, $filesChanged, $linesChanged)
if ($DryRun -and $filesChanged -gt 0) {
    Write-Host 'Dry run only — no files were modified.'
}

if ($filesChanged -gt 0) {
    $summaryRoot = if (-not [string]::IsNullOrWhiteSpace($env:RUNNER_TEMP)) {
        $env:RUNNER_TEMP
    } elseif (-not [string]::IsNullOrWhiteSpace($env:GITHUB_WORKSPACE)) {
        $env:GITHUB_WORKSPACE
    } else {
        [IO.Path]::GetTempPath()
    }
    $summaryPath = Join-Path $summaryRoot 'license-header-scan-summary.txt'

    $summary = @(
        "files_scanned=$filesScanned"
        "files_changed=$filesChanged"
        "lines_changed=$linesChanged"
        "dry_run=$($DryRun.IsPresent)"
    )
    foreach ($path in $changedFiles) {
        $summary += "changed=$path"
    }

    $summaryText = ($summary -join "`n") + "`n"
    [System.IO.File]::WriteAllText($summaryPath, $summaryText, [System.Text.UTF8Encoding]::new($false))
    Write-Host "Summary written to $summaryPath"
}

if ($filesChanged -gt 0 -and -not $DryRun) {
    exit 0
}

exit 0
