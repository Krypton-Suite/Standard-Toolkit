# New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
# Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
#
# Local dry-run helper for .github/workflows/scan-code-todos.yml.
# Example:
#   pwsh .github/scripts/Scan-CodeTodos.ps1
#   pwsh .github/scripts/Scan-CodeTodos.ps1 -IncludeAllMarkers -GroupDuplicates

param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path,

    [switch]$IncludeAllMarkers,

    [switch]$GroupDuplicates
)

$ErrorActionPreference = 'Stop'

if ($IncludeAllMarkers) {
    $pattern = '\b(?:TODO|ToDo|FIXME|HACK)\b(?!\s*\])(?:\s*\(\s*issue\s*\))?[:\s]*(.*)$'
}
else {
    $pattern = '(?i)\b(?:TODO|ToDo)\s*\(\s*issue\s*\)\s*[:\s]*(.*)$'
}

$searchRoots = @(
    (Join-Path $RepositoryRoot 'Source'),
    (Join-Path $RepositoryRoot '.github\workflows'),
    (Join-Path $RepositoryRoot 'Documents')
)

$linkedIssuePattern = '(?:#\d{3,}|issues/\d{3,})'
$scanResults = @()

foreach ($root in $searchRoots) {
    if (-not (Test-Path -LiteralPath $root)) {
        continue
    }

    Get-ChildItem -LiteralPath $root -Recurse -File |
        Where-Object {
            $_.FullName -notmatch '\\(obj|bin|Bin)\\' -and
            $_.Name -ne 'scan-code-todos.yml'
        } |
        ForEach-Object {
            $file = $_
            $lineNumber = 0
            Get-Content -LiteralPath $file.FullName | ForEach-Object {
                $lineNumber++
                if ($_ -cmatch $pattern) {
                    if ($_ -match $linkedIssuePattern) {
                        return
                    }

                    $relativePath = $file.FullName.Substring($RepositoryRoot.Length).TrimStart('\', '/').Replace('\', '/')
                    $commentText = $Matches[1].Trim()
                    if ([string]::IsNullOrWhiteSpace($commentText)) {
                        return
                    }

                    $normalizedText = ($commentText -replace '\s+', ' ').Trim().ToLowerInvariant()
                    $locationKey = "$relativePath`:$lineNumber`:$normalizedText"
                    $locationHash = [System.BitConverter]::ToString(
                        [System.Security.Cryptography.SHA256]::Create().ComputeHash(
                            [System.Text.Encoding]::UTF8.GetBytes($locationKey)
                        )
                    ).Replace('-', '').ToLowerInvariant()

                    $groupKey = if ($GroupDuplicates) {
                        [System.BitConverter]::ToString(
                            [System.Security.Cryptography.SHA256]::Create().ComputeHash(
                                [System.Text.Encoding]::UTF8.GetBytes($normalizedText)
                            )
                        ).Replace('-', '').ToLowerInvariant()
                    }
                    else {
                        $locationHash
                    }

                    $scanResults += [pscustomobject]@{
                        FilePath       = $relativePath
                        LineNumber     = $lineNumber
                        CommentText    = $commentText
                        NormalizedText = $normalizedText
                        LocationHash   = $locationHash
                        GroupKey       = $groupKey
                    }
                }
            }
        }
}

Write-Host "Scan mode: $(if ($IncludeAllMarkers) { 'all markers' } else { 'opt-in (ToDo(issue))' })"
Write-Host "Matches: $($scanResults.Count)"

if ($GroupDuplicates) {
    $groups = $scanResults | Group-Object -Property GroupKey
    Write-Host "Grouped issues: $($groups.Count)"
    $groups |
        Sort-Object Count -Descending |
        Select-Object -First 25 |
        ForEach-Object {
            $sample = $_.Group[0]
            Write-Host ("[{0}] {1} ({2} location(s))" -f $_.Count, $sample.CommentText, $_.Count)
            foreach ($item in $_.Group | Select-Object -First 3) {
                Write-Host ("  - {0}:{1}" -f $item.FilePath, $item.LineNumber)
            }
            if ($_.Count -gt 3) {
                Write-Host ("  - ... and {0} more" -f ($_.Count - 3))
            }
        }
}
else {
    $scanResults |
        Sort-Object FilePath, LineNumber |
        Select-Object -First 50 |
        ForEach-Object {
            Write-Host ("{0}:{1}  {2}" -f $_.FilePath, $_.LineNumber, $_.CommentText)
        }

    if ($scanResults.Count -gt 50) {
        Write-Host "... and $($scanResults.Count - 50) more"
    }
}
