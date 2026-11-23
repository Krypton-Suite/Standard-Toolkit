#Requires -Version 5.1
<#
.SYNOPSIS
  Restores ThrowIfNull + assignment pairs to ?? ThrowArgumentNullException<T> coalesce form.
.DESCRIPTION
  Convert-ThrowHelpers.ps1 incorrectly rewrote
    target = param ?? throw new ArgumentNullException(nameof(param));
  as
    ThrowHelper.ThrowIfNull(param);
    target = param;
  Smurf-IV review: preserve equivalent ArgumentNull coalesce shape.
#>
[CmdletBinding()]
param(
    [string]$Root = 'z:\Development\Krypton\Standard-Toolkit\Source\Krypton Components',
    [switch]$WhatIf
)

$ErrorActionPreference = 'Stop'
$utf8Bom = New-Object System.Text.UTF8Encoding $true
$typePattern = '[\w.?<>\[\], ]+'

function Normalize-Type([string]$type) {
    $normalized = $type.Replace('?', '').Trim().TrimEnd(',')
    $normalized = [regex]::Replace($normalized, '\[[^\]]*\]\s*', '')
    foreach ($keyword in @('override', 'new', 'static', 'virtual', 'sealed', 'readonly')) {
        $normalized = $normalized -replace "\b$keyword\b\s+", ''
    }
    return $normalized.Trim()
}

function Find-MemberType([string]$text, [string]$memberName) {
    $escaped = [regex]::Escape($memberName)
    $fieldRx = [regex]"(?:private|protected|internal|public)\s+(?:(?:readonly|static|volatile)\s+)*($typePattern)\s+$escaped\s*[;=]"
    $m = $fieldRx.Match($text)
    if ($m.Success) { return Normalize-Type $m.Groups[1].Value }

    $propRx = [regex]"(?:public|protected|internal|override)\s+($typePattern)\s+$escaped\s*\{"
    $m = $propRx.Match($text)
    if ($m.Success) { return Normalize-Type $m.Groups[1].Value }

    return $null
}

function Find-ParameterType([string]$text, [string]$paramName, [int]$nearIndex) {
    $start = [Math]::Max(0, $nearIndex - 8000)
    $window = $text.Substring($start, [Math]::Min($text.Length - $start, $nearIndex - $start + 200))
    $escaped = [regex]::Escape($paramName)
    $rx = [regex]"(?:\[[^\]]*\]\s*)*(?:ref\s+|in\s+|out\s+)?($typePattern)\??\s+$escaped\b"
    $ms = $rx.Matches($window)
    if ($ms.Count -eq 0) { return $null }
    return Normalize-Type $ms[$ms.Count - 1].Groups[1].Value
}

function Restore-File([string]$path) {
    if ($path -match '\\obj\\' -or [IO.Path]::GetFileName($path) -eq 'ThrowHelper.cs') { return 0 }

    $bytes = [IO.File]::ReadAllBytes($path)
    $text = [Text.Encoding]::UTF8.GetString($bytes)
    if ($text.Length -gt 0 -and [int][char]$text[0] -eq 0xFEFF) { $text = $text.Substring(1) }

    $original = $text
    $changes = 0
    $rx = [regex]'(?m)^(?<indent>[ \t]*)ThrowHelper\.ThrowIfNull\((?<param>[A-Za-z_][A-Za-z0-9_]*)\);\r?\n\k<indent>(?<target>[A-Za-z_][A-Za-z0-9_]*)\s*=\s*\k<param>\s*;'

    $matches = @($rx.Matches($text))
    for ($i = $matches.Count - 1; $i -ge 0; $i--) {
        $m = $matches[$i]
        $indent = $m.Groups['indent'].Value
        $param = $m.Groups['param'].Value
        $target = $m.Groups['target'].Value
        $type = Find-MemberType $text $target
        if (-not $type) { $type = Find-ParameterType $text $param $m.Index }
        if (-not $type) {
            Write-Warning "Skip $path : cannot infer type for '$target' (param '$param')"
            continue
        }

        $replacement = "${indent}${target} = ${param} ?? ThrowHelper.ThrowArgumentNullException<${type}>(nameof(${param}));"
        $text = $text.Substring(0, $m.Index) + $replacement + $text.Substring($m.Index + $m.Length)
        $changes++
    }

    if ($changes -gt 0 -and $text -ne $original) {
        if (-not $WhatIf) {
            $text = $text -replace "`r`n", "`n" -replace "`n", "`r`n"
            [IO.File]::WriteAllText($path, $text, $utf8Bom)
        }
    }

    return $changes
}

$totalFiles = 0
$totalChanges = 0
Get-ChildItem -Path $Root -Filter *.cs -Recurse | ForEach-Object {
    $n = Restore-File $_.FullName
    if ($n -gt 0) {
        $totalFiles++
        $totalChanges += $n
        $rel = $_.FullName.Substring($Root.Length).TrimStart('\')
        Write-Host ("{0,4}  {1}" -f $n, $rel)
    }
}

$verb = if ($WhatIf) { 'Would restore' } else { 'Restored' }
Write-Host "$verb $totalChanges coalesce sites in $totalFiles files"
