#requires -Version 7.0
<#
.SYNOPSIS
    Scans repository text files for UTF-8 encoding corruption (mojibake and replacement characters).

.DESCRIPTION
    Detects common corruption introduced when UTF-8 text is misinterpreted or when the UTF-8 BOM
    is stripped from files that contain non-ASCII characters (for example © in license headers,
    accented contributor names, or en dashes in comments).

    Dot-source this script, then call Test-SourceEncodingIntegrity.

.PARAMETER RepoRoot
    Repository root. Defaults to GITHUB_WORKSPACE or the current location.

.PARAMETER ChangedPaths
    Optional relative paths to scan. When omitted with `-ScanAll`, scans Source/, Documents/, .github/, and Scripts/.
    When supplied (including an empty list), only those paths are scanned.

.PARAMETER ScanAll
    Scan all eligible files under the default roots instead of a supplied path list.

.PARAMETER FailOnIssues
    When set, writes a workflow error and exits with code 1 if any issue is found.
#>

function Get-SourceEncodingScanExtensions {
    return [string[]]@(
        '.cs',
        '.licenseheader',
        '.yml',
        '.yaml',
        '.md',
        '.resx',
        '.cmd',
        '.ps1',
        '.editorconfig'
    )
}

function New-SourceEncodingExtensionSet {
    $extensions = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($extension in Get-SourceEncodingScanExtensions) {
        [void]$extensions.Add($extension)
    }

    return $extensions
}

function Test-SourceEncodingExcludedPath {
    param([string]$FullName)

    if ($FullName -match '[\\/]bin[\\/]|[\\/]obj[\\/]|[\\/]Artefacts[\\/]|[\\/]\.git[\\/]') {
        return $true
    }

    return $FullName -match '[\\/]Scripts[\\/]CI[\\/]Test-SourceEncodingIntegrity\.ps1$'
}

function Get-SourceEncodingCorruptionPatterns {
    $mojibakeEnDash = -join [char[]]@(0x00E2, 0x20AC, 0x201C)
    $mojibakeEmDash = -join [char[]]@(0x00E2, 0x20AC, 0x201D)
    $mojibakeEllipsis = -join [char[]]@(0x00E2, 0x20AC, 0x00A6)
    $mojibakeCopyright = -join [char[]]@(0x00C2, 0x00A9)
    $mojibakeAviles = 'Avil' + [char]0x00C3 + [char]0x00A9 + 's'

    @(
        @{
            Id          = 'replacement-character'
            Description = 'UTF-8 replacement character (U+FFFD) — often from a corrupted non-ASCII character in a license header or comment'
            Pattern     = [char]0xFFFD
        },
        @{
            Id          = 'mojibake-copyright'
            Description = 'Mojibake copyright symbol (C2+A9 bytes instead of UTF-8 copyright sign)'
            Pattern     = $mojibakeCopyright
        },
        @{
            Id          = 'mojibake-e-acute-aviles'
            Description = 'Mojibake in contributor name (Avil plus C3+A9 instead of Aviles with e-acute)'
            Pattern     = $mojibakeAviles
        },
        @{
            Id          = 'mojibake-en-dash'
            Description = 'Mojibake en dash (E2+80+9x mis-decoded three-character sequence)'
            Pattern     = $mojibakeEnDash
        },
        @{
            Id          = 'mojibake-em-dash'
            Description = 'Mojibake em dash (E2+80+9x mis-decoded three-character sequence)'
            Pattern     = $mojibakeEmDash
        },
        @{
            Id          = 'mojibake-ellipsis'
            Description = 'Mojibake ellipsis (E2+80+A6 mis-decoded three-character sequence)'
            Pattern     = $mojibakeEllipsis
        }
    )
}

function Test-SourceEncodingPathEligible {
    param(
        [string]$RelativePath,
        [System.Collections.Generic.HashSet[string]]$Extensions
    )

    if ([string]::IsNullOrWhiteSpace($RelativePath)) {
        return $false
    }

    $normalized = $RelativePath.Replace('\', '/')
    if ($normalized -notmatch '^(Source/|Documents/|\.github/|Scripts/)') {
        return $false
    }

    $fileName = [System.IO.Path]::GetFileName($RelativePath)
    if ($fileName -eq '.editorconfig') {
        return $true
    }

    return $Extensions.Contains([System.IO.Path]::GetExtension($RelativePath))
}

function Get-SourceEncodingScanPaths {
    param(
        [string]$RepoRoot,
        [string[]]$ChangedPaths,
        [switch]$ScanAll
    )

    $extensions = New-SourceEncodingExtensionSet

    if (-not $ScanAll) {
        $paths = [System.Collections.Generic.List[string]]::new()
        foreach ($relativePath in $ChangedPaths) {
            $relativePath = $relativePath.Trim().Replace('\', '/')
            if (-not (Test-SourceEncodingPathEligible -RelativePath $relativePath -Extensions $extensions)) {
                continue
            }

            $fullPath = Join-Path $RepoRoot ($relativePath -replace '/', [System.IO.Path]::DirectorySeparatorChar)
            if (Test-Path -LiteralPath $fullPath -PathType Leaf) {
                $paths.Add($fullPath)
            }
        }

        return $paths
    }

    $scanRoots = @(
        (Join-Path $RepoRoot 'Source'),
        (Join-Path $RepoRoot 'Documents'),
        (Join-Path $RepoRoot '.github'),
        (Join-Path $RepoRoot 'Scripts')
    )

    $paths = [System.Collections.Generic.List[string]]::new()
    foreach ($root in $scanRoots) {
        if (-not (Test-Path -LiteralPath $root)) {
            continue
        }

        Get-ChildItem -Path $root -Recurse -File | ForEach-Object {
            if (Test-SourceEncodingExcludedPath $_.FullName) {
                return
            }

            $relativePath = $_.FullName.Substring($RepoRoot.Length).TrimStart('\', '/').Replace('\', '/')
            if (Test-SourceEncodingPathEligible -RelativePath $relativePath -Extensions $extensions) {
                $paths.Add($_.FullName)
            }
        }
    }

    return $paths
}

function Test-SourceFileEncoding {
    param(
        [Parameter(Mandatory = $true)]
        [string]$FullPath,

        [Parameter(Mandatory = $true)]
        [string]$RepoRoot
    )

    $issues = [System.Collections.Generic.List[object]]::new()
    if (-not (Test-Path -LiteralPath $FullPath -PathType Leaf)) {
        return $issues
    }

    $bytes = [System.IO.File]::ReadAllBytes($FullPath)
    if ($bytes.Length -eq 0) {
        return $issues
    }

    $bomSkip = 0
    if ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
        $bomSkip = 3
    }

    $text = [System.Text.Encoding]::UTF8.GetString($bytes, $bomSkip, $bytes.Length - $bomSkip)
    if ([string]::IsNullOrEmpty($text)) {
        return $issues
    }

    $relativePath = $FullPath.Substring($RepoRoot.Length).TrimStart('\', '/').Replace('\', '/')
    $lineNumber = 0

    foreach ($line in $text -split '\r?\n', [System.StringSplitOptions]::None) {
        $lineNumber++

        foreach ($patternInfo in Get-SourceEncodingCorruptionPatterns) {
            $pattern = $patternInfo.Pattern
            $matched = if ($pattern -is [char]) {
                $line.Contains($pattern)
            }
            else {
                $line.Contains($pattern)
            }

            if (-not $matched) {
                continue
            }

            $preview = $line.Trim()
            if ($preview.Length -gt 120) {
                $preview = $preview.Substring(0, 117) + '...'
            }

            $issues.Add([PSCustomObject]@{
                    RelativePath = $relativePath
                    LineNumber   = $lineNumber
                    IssueId      = $patternInfo.Id
                    Description  = $patternInfo.Description
                    Preview      = $preview
                })
        }
    }

    return $issues
}

function Test-SourceEncodingIntegrity {
    param(
        [string]$RepoRoot = $(if ($env:GITHUB_WORKSPACE) { $env:GITHUB_WORKSPACE } else { (Get-Location).Path }),
        [string[]]$ChangedPaths = @(),
        [switch]$ScanAll,
        [switch]$FailOnIssues
    )

    $RepoRoot = (Resolve-Path -LiteralPath $RepoRoot).Path
    $scanPaths = Get-SourceEncodingScanPaths -RepoRoot $RepoRoot -ChangedPaths $ChangedPaths -ScanAll:$ScanAll
    $allIssues = [System.Collections.Generic.List[object]]::new()

    foreach ($fullPath in $scanPaths) {
        $fileIssues = Test-SourceFileEncoding -FullPath $fullPath -RepoRoot $RepoRoot
        foreach ($issue in $fileIssues) {
            $allIssues.Add($issue)
        }
    }

    $fileCount = ($allIssues | Select-Object -ExpandProperty RelativePath -Unique).Count
    $issueCount = $allIssues.Count

    if ($issueCount -eq 0) {
        Write-Host "Source encoding check passed ($($scanPaths.Count) file(s) scanned, 0 issue(s))."
        return 0
    }

    Write-Host "Source encoding check found $issueCount issue(s) in $fileCount file(s)."

    foreach ($issue in $allIssues) {
        $message = "$($issue.Description) :: $($issue.Preview)"
        Write-Host "::error file=$($issue.RelativePath),line=$($issue.LineNumber),title=$($issue.IssueId):: $message"
    }

    if ($env:GITHUB_STEP_SUMMARY) {
        $summary = @(
            "## Source encoding check failed",
            "",
            "Found **$issueCount** corruption marker(s) in **$fileCount** file(s).",
            "",
            "| File | Line | Issue | Preview |",
            "| --- | ---: | --- | --- |"
        )

        foreach ($issue in $allIssues) {
            $escapedPreview = ($issue.Preview -replace '\|', '\|')
            $summary += "| ``$($issue.RelativePath)`` | $($issue.LineNumber) | $($issue.Description) | $escapedPreview |"
        }

        $summary += ""
        $summary += "Restore the intended UTF-8 characters (for example ``©``, ``Avilés``, ``–``) and preserve UTF-8 BOM on affected files where the repository already used it."

        $summary -join "`n" | Out-File -FilePath $env:GITHUB_STEP_SUMMARY -Encoding utf8 -Append
    }

    if ($FailOnIssues) {
        exit 1
    }

    return $issueCount
}

if ($MyInvocation.InvocationName -ne '.') {
    param(
        [string[]]$ChangedPaths = @(),
        [switch]$ScanAll,
        [switch]$FailOnIssues
    )

    if (-not $ScanAll -and $ChangedPaths.Count -eq 0) {
        $ScanAll = $true
    }

    $null = Test-SourceEncodingIntegrity -ChangedPaths $ChangedPaths -ScanAll:$ScanAll -FailOnIssues:$FailOnIssues
}
