# Hoists [DefaultDllImportSearchPaths(DllImportSearchPath.System32)] before #if NET8_0_OR_GREATER
# dual-path LibraryImport / DllImport blocks in PlatformInvoke.cs.
param(
    [Parameter(Mandatory = $true)]
    [string]$Path
)

$ErrorActionPreference = 'Stop'
$searchPathLine = '[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]'
$pathFull = (Resolve-Path -LiteralPath $Path).Path
$lines = [System.IO.File]::ReadAllLines($pathFull)
$result = New-Object System.Collections.Generic.List[string]

function Test-IsSearchPathLine {
    param([string]$Line)
    return ($Line.Trim() -eq $searchPathLine)
}

$i = 0
while ($i -lt $lines.Count) {
    $line = $lines[$i]
    if ($line -match '^\s*#if NET8_0_OR_GREATER\s*$') {
        $ifIndent = ($line -replace '#if.*', '')
        $ifStart = $i
        $elseIdx = -1
        $endifIdx = -1
        $depth = 1
        for ($j = $i + 1; $j -lt $lines.Count; $j++) {
            if ($lines[$j] -match '^\s*#if\b') { $depth++ }
            elseif ($lines[$j] -match '^\s*#endif\b') {
                $depth--
                if ($depth -eq 0) { $endifIdx = $j; break }
            }
            elseif ($depth -eq 1 -and $lines[$j] -match '^\s*#else\b') { $elseIdx = $j }
        }

        if ($elseIdx -gt 0 -and $endifIdx -gt 0) {
            $ifBody = $lines[($ifStart + 1)..($elseIdx - 1)]
            $elseBody = $lines[($elseIdx + 1)..($endifIdx - 1)]
            $isDualPathImport = ($ifBody -match '\[LibraryImport\b') -and ($elseBody -match '\[DllImport\b')
            $hasSearchPath = (($ifBody | Where-Object { Test-IsSearchPathLine $_ }).Count -gt 0) -or
                (($elseBody | Where-Object { Test-IsSearchPathLine $_ }).Count -gt 0)

            if ($isDualPathImport -and $hasSearchPath) {
                $attrIndent = if ([string]::IsNullOrEmpty($ifIndent)) { '    ' } else { $ifIndent }
                $attrLine = "$attrIndent$searchPathLine"
                $alreadyHoisted = ($ifStart -gt 0) -and (Test-IsSearchPathLine $lines[$ifStart - 1])
                if (-not $alreadyHoisted) {
                    $result.Add($attrLine)
                }
                $result.Add($line)
                foreach ($bodyLine in $ifBody) {
                    if (-not (Test-IsSearchPathLine $bodyLine)) { $result.Add($bodyLine) }
                }
                $result.Add($lines[$elseIdx])
                foreach ($bodyLine in $elseBody) {
                    if (-not (Test-IsSearchPathLine $bodyLine)) { $result.Add($bodyLine) }
                }
                $result.Add($lines[$endifIdx])
                $i = $endifIdx + 1
                continue
            }
        }
    }

    $result.Add($line)
    $i++
}

[System.IO.File]::WriteAllLines($pathFull, $result)
Write-Host "Hoisted DefaultDllImportSearchPaths in dual-path blocks: $pathFull"
