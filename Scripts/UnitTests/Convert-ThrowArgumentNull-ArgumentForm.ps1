#Requires -Version 5.1
<#
.SYNOPSIS
  Converts ThrowArgumentNullException&lt;T&gt;(nameof(x)) coalesce sites to argument form.
.DESCRIPTION
  value ?? ThrowHelper.ThrowArgumentNullException&lt;T&gt;(nameof(value))
    -> value ?? ThrowHelper.ThrowArgumentNullException(value)
  (sender as T) ?? ThrowHelper.ThrowArgumentNullException&lt;T&gt;(nameof(sender))
    -> (sender as T) ?? ThrowHelper.ThrowArgumentNullException(sender as T, nameof(sender))
  string requires an explicit type argument to avoid the void(string) overload.
#>
[CmdletBinding()]
param(
    [string]$Root = 'z:\Development\Krypton\Standard-Toolkit\Source\Krypton Components'
)

$ErrorActionPreference = 'Stop'
$utf8Bom = New-Object System.Text.UTF8Encoding $true

function Find-GenericClose([string]$text, [int]$openAngle) {
    $depth = 0
    for ($i = $openAngle; $i -lt $text.Length; $i++) {
        $c = $text[$i]
        if ($c -eq '<') { $depth++ }
        elseif ($c -eq '>') {
            $depth--
            if ($depth -eq 0) { return $i }
        }
    }
    return -1
}

function Convert-File([string]$path) {
    if ($path -match '\\obj\\' -or [IO.Path]::GetFileName($path) -eq 'ThrowHelper.cs') { return 0 }

    $bytes = [IO.File]::ReadAllBytes($path)
    $text = [Text.Encoding]::UTF8.GetString($bytes)
    if ($text.Length -gt 0 -and [int][char]$text[0] -eq 0xFEFF) { $text = $text.Substring(1) }
    $original = $text
    $changes = 0

    $needle = 'ThrowHelper.ThrowArgumentNullException<'
    $searchFrom = 0
    $edits = New-Object System.Collections.Generic.List[object]

    while ($true) {
        $idx = $text.IndexOf($needle, $searchFrom)
        if ($idx -lt 0) { break }

        $typeStart = $idx + $needle.Length
        $typeOpen = $typeStart - 1 # points at '<'
        $typeClose = Find-GenericClose $text $typeOpen
        if ($typeClose -lt 0) { $searchFrom = $typeStart; continue }

        $typeName = $text.Substring($typeStart, $typeClose - $typeStart).Trim()
        $afterType = $typeClose + 1
        while ($afterType -lt $text.Length -and [char]::IsWhiteSpace($text[$afterType])) { $afterType++ }
        if ($afterType -ge $text.Length -or $text[$afterType] -ne '(') { $searchFrom = $typeClose + 1; continue }

        $argsOpen = $afterType
        $argsClose = $argsOpen
        $depth = 0
        for ($i = $argsOpen; $i -lt $text.Length; $i++) {
            if ($text[$i] -eq '(') { $depth++ }
            elseif ($text[$i] -eq ')') {
                $depth--
                if ($depth -eq 0) { $argsClose = $i; break }
            }
        }
        $args = $text.Substring($argsOpen + 1, $argsClose - $argsOpen - 1).Trim()
        $nameMatch = [regex]::Match($args, '^nameof\(([A-Za-z_][A-Za-z0-9_]*)\)$')
        if (-not $nameMatch.Success) { $searchFrom = $argsClose + 1; continue }

        $paramName = $nameMatch.Groups[1].Value

        # Find ?? immediately before this ThrowHelper call
        $j = $idx - 1
        while ($j -ge 0 -and ($text[$j] -eq ' ' -or $text[$j] -eq "`t" -or $text[$j] -eq "`r" -or $text[$j] -eq "`n")) { $j-- }
        if ($j -lt 1 -or $text.Substring($j - 1, 2) -ne '??') { $searchFrom = $argsClose + 1; continue }
        $qqEnd = $j + 1
        $qqStart = $j - 1

        # Left expression before ??
        $k = $qqStart - 1
        while ($k -ge 0 -and ($text[$k] -eq ' ' -or $text[$k] -eq "`t")) { $k-- }
        $leftEnd = $k + 1
        $parenDepth = 0
        $leftStart = 0
        for ($p = $leftEnd - 1; $p -ge 0; $p--) {
            $c = $text[$p]
            if ($c -eq ')' -or $c -eq ']' -or $c -eq '}') { $parenDepth++ }
            elseif ($c -eq '(' -or $c -eq '[' -or $c -eq '{') {
                if ($parenDepth -eq 0) { $leftStart = $p + 1; break }
                $parenDepth--
            }
            elseif ($parenDepth -eq 0 -and ($c -eq '=' -or $c -eq ';' -or $c -eq ',' -or $c -eq '?' -or $c -eq ':' -or $c -eq "`n" -or $c -eq "`r")) {
                # stop at = but not == or => or <= >= !=
                if ($c -eq '=') {
                    $prev = if ($p -gt 0) { $text[$p - 1] } else { [char]0 }
                    $next = if ($p + 1 -lt $text.Length) { $text[$p + 1] } else { [char]0 }
                    if ($prev -eq '=' -or $prev -eq '!' -or $prev -eq '<' -or $prev -eq '>') { continue }
                    if ($next -eq '=' -or $next -eq '>') { continue }
                }
                if ($c -eq '?' -and $p + 1 -lt $text.Length -and $text[$p + 1] -eq '?') { continue }
                $leftStart = $p + 1
                break
            }
        }
        $leftExpr = $text.Substring($leftStart, $leftEnd - $leftStart).Trim()

        if ($typeName -eq 'string') {
            $replacement = "ThrowHelper.ThrowArgumentNullException<string>($paramName)"
        }
        elseif ($leftExpr -eq $paramName) {
            $replacement = "ThrowHelper.ThrowArgumentNullException($paramName)"
        }
        elseif ($leftExpr -match '\bas\b') {
            $replacement = "ThrowHelper.ThrowArgumentNullException($leftExpr, nameof($paramName))"
        }
        else {
            # Fallback: pass the left expression for type inference; keep nameof for ParamName
            $replacement = "ThrowHelper.ThrowArgumentNullException($leftExpr, nameof($paramName))"
        }

        $edits.Add([pscustomobject]@{
            Start = $idx
            Length = ($argsClose + 1) - $idx
            Replacement = $replacement
        })
        $searchFrom = $argsClose + 1
    }

    for ($e = $edits.Count - 1; $e -ge 0; $e--) {
        $edit = $edits[$e]
        $text = $text.Substring(0, $edit.Start) + $edit.Replacement + $text.Substring($edit.Start + $edit.Length)
        $changes++
    }

    if ($changes -gt 0 -and $text -ne $original) {
        $text = $text -replace "`r`n", "`n" -replace "`n", "`r`n"
        [IO.File]::WriteAllText($path, $text, $utf8Bom)
    }
    return $changes
}

$totalFiles = 0
$totalChanges = 0
Get-ChildItem -Path $Root -Filter *.cs -Recurse | ForEach-Object {
    $n = Convert-File $_.FullName
    if ($n -gt 0) {
        $totalFiles++
        $totalChanges += $n
        $rel = $_.FullName.Substring($Root.Length).TrimStart('\')
        Write-Host ("{0,4}  {1}" -f $n, $rel)
    }
}
Write-Host "Converted $totalChanges sites in $totalFiles files"
