#Requires -Version 5.1
<#
.SYNOPSIS
  Converts throw new Exception(...) to ThrowHelper across Krypton Components.
.DESCRIPTION
  Handles statement throws, ?? coalesce (ThrowIfNull rewrite or typed helpers),
  and switch-expression arms (typed ThrowArgumentOutOfRangeException&lt;T&gt;).
#>
[CmdletBinding()]
param(
    [string]$Root = 'z:\Development\Krypton\Standard-Toolkit\Source\Krypton Components'
)

$ErrorActionPreference = 'Stop'
$utf8Bom = New-Object System.Text.UTF8Encoding $true

function Find-MatchingParen([string]$text, [int]$openIdx) {
    $depth = 0
    $inStr = $null
    for ($i = $openIdx; $i -lt $text.Length; $i++) {
        $c = $text[$i]
        if ($null -ne $inStr) {
            if ($c -eq [char]'\' -and $inStr -ne '@') { $i++; continue }
            if ($c -eq $inStr) { $inStr = $null }
            continue
        }
        if ($c -eq [char]'"' -or $c -eq [char]"'") {
            $inStr = $c
            continue
        }
        if ($c -eq [char]'(') { $depth++ }
        elseif ($c -eq [char]')') {
            $depth--
            if ($depth -eq 0) { return $i }
        }
    }
    return -1
}

function Get-ExpressionContext([string]$text, [int]$throwIdx) {
    $j = $throwIdx - 1
    while ($j -ge 0 -and ($text[$j] -eq ' ' -or $text[$j] -eq "`t" -or $text[$j] -eq "`r" -or $text[$j] -eq "`n")) { $j-- }
    if ($j -ge 1 -and $text.Substring($j - 1, 2) -eq '=>') { return 'arrow' }
    if ($j -ge 1 -and $text.Substring($j - 1, 2) -eq '??') { return 'coalesce' }
    return $null
}

function Get-HelperName([string]$exc) {
    switch ($exc) {
        'ArgumentNullException' { 'ThrowArgumentNullException' }
        'NullReferenceException' { 'ThrowNullReferenceException' }
        'ArgumentOutOfRangeException' { 'ThrowArgumentOutOfRangeException' }
        'ArgumentException' { 'ThrowArgumentException' }
        'InvalidOperationException' { 'ThrowInvalidOperationException' }
        'NotSupportedException' { 'ThrowNotSupportedException' }
        'NotImplementedException' { 'ThrowNotImplementedException' }
        'ObjectDisposedException' { 'ThrowObjectDisposedException' }
        'InvalidCastException' { 'ThrowInvalidCastException' }
        'Win32Exception' { 'ThrowWin32Exception' }
        default { $null }
    }
}

function Find-SwitchBlock([string]$text, [int]$throwIdx) {
    $pos = $text.LastIndexOf('switch', $throwIdx)
    if ($pos -lt 0) { return $null }
    $brace = $text.IndexOf('{', $pos, $throwIdx - $pos)
    if ($brace -lt 0) { return $null }
    $depth = 0
    for ($i = $brace; $i -lt $text.Length; $i++) {
        if ($text[$i] -eq '{') { $depth++ }
        elseif ($text[$i] -eq '}') {
            $depth--
            if ($depth -eq 0) { return $text.Substring($brace, $i - $brace + 1) }
        }
    }
    return $text.Substring($brace, [Math]::Min(4000, $throwIdx - $brace))
}

function Infer-TypeFromSwitch([string]$block) {
    $types = @{}
    $matches = [regex]::Matches($block, '=>\s*([A-Za-z_][A-Za-z0-9_]*(?:\.[A-Za-z_][A-Za-z0-9_]*)+)\b')
    foreach ($m in $matches) {
        $expr = $m.Groups[1].Value
        if ($expr.StartsWith('nameof') -or $expr.StartsWith('throw') -or $expr.StartsWith('ThrowHelper')) { continue }
        $parts = $expr.Split('.')
        if ($parts.Length -ge 2) {
            $typeName = $parts[$parts.Length - 2]
            if ($typeName[0] -cmatch '[A-Z]') {
                if ($types.ContainsKey($typeName)) { $types[$typeName]++ } else { $types[$typeName] = 1 }
            }
        }
    }
    if ($types.Count -eq 0) {
        if ($block -match '=>\s*true\b' -or $block -match '=>\s*false\b') { return 'bool' }
        if ($block -match '=>\s*\d+f\b') { return 'float' }
        if ($block -match '=>\s*\d+\.\d+') { return 'double' }
        if ($block -match '=>\s*\d+\b') { return 'int' }
        return $null
    }
    return ($types.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First 1).Key
}

function Infer-TypeFromMethod([string]$text, [int]$throwIdx) {
    $start = [Math]::Max(0, $throwIdx - 80000)
    $window = $text.Substring($start, $throwIdx - $start)
    $rx = [regex]'(?:public|protected|internal|private)\s+(?:(?:static|override|virtual|sealed|new|partial)\s+)*([\w.?]+(?:\s*<[^>]+>)?)\s+\w+\s*\('
    $ms = $rx.Matches($window)
    if ($ms.Count -eq 0) { return $null }
    $ret = $ms[$ms.Count - 1].Groups[1].Value.Trim().TrimEnd('?')
    $skip = @('void','if','while','for','switch','using','fixed','foreach')
    if ($skip -contains $ret) { return $null }
    return $ret
}

function Find-FieldType([string]$text, [string]$fieldName) {
    $rx = [regex]("(?:private|protected|internal|public)\s+(?:(?:readonly|static|volatile)\s+)*([\w.?]+(?:\s*<[^>]+>)?)\s+$([regex]::Escape($fieldName))\s*[;=]")
    $m = $rx.Match($text)
    if ($m.Success) { return $m.Groups[1].Value.Replace('?', '').Trim() }
    return $null
}

function Convert-File([string]$path) {
    $bytes = [System.IO.File]::ReadAllBytes($path)
    $text = [System.Text.Encoding]::UTF8.GetString($bytes)
    if ($text.Length -gt 0 -and [int][char]$text[0] -eq 0xFEFF) { $text = $text.Substring(1) }
    # Also handle UTF8 BOM already stripped by decoder sometimes
    $original = $text
    $changes = 0

    $rx = [regex]'throw\s+new\s+(ArgumentNullException|NullReferenceException|ArgumentOutOfRangeException|ArgumentException|InvalidOperationException|NotSupportedException|NotImplementedException|ObjectDisposedException|InvalidCastException|Win32Exception)\s*\('
    $ms = @($rx.Matches($text))
    for ($mi = $ms.Count - 1; $mi -ge 0; $mi--) {
        $m = $ms[$mi]
        $exc = $m.Groups[1].Value
        $helper = Get-HelperName $exc
        $openParen = $m.Index + $m.Length - 1
        $close = Find-MatchingParen $text $openParen
        if ($close -lt 0) { continue }
        $args = $text.Substring($openParen + 1, $close - $openParen - 1).Trim()
        $throwStart = $m.Index
        $ctx = Get-ExpressionContext $text $throwStart

        if ($null -eq $ctx) {
            $end = $close + 1
            while ($end -lt $text.Length -and ($text[$end] -eq ' ' -or $text[$end] -eq "`t")) { $end++ }
            if ($end -lt $text.Length -and $text[$end] -eq ';') { $end++ }
            # Non-void methods need a return for definite-assignment; use typed helper when possible.
            $ret = Infer-TypeFromMethod $text $throwStart
            if ($ret -and $ret -ne 'void') {
                $replacement = "return ThrowHelper.$helper<$ret>($args);"
            } else {
                $replacement = "ThrowHelper.$helper($args);"
            }
            $text = $text.Substring(0, $throwStart) + $replacement + $text.Substring($end)
            $changes++
            continue
        }

        if ($ctx -eq 'arrow') {
            $t = $null
            $block = Find-SwitchBlock $text $throwStart
            if ($block) { $t = Infer-TypeFromSwitch $block }
            if (-not $t) { $t = Infer-TypeFromMethod $text $throwStart }
            if (-not $t) { continue }
            $replacement = "ThrowHelper.$helper<$t>($args)"
            $text = $text.Substring(0, $throwStart) + $replacement + $text.Substring($close + 1)
            $changes++
            continue
        }

        if ($ctx -eq 'coalesce') {
            $j = $throwStart - 1
            while ($j -ge 0 -and ($text[$j] -eq ' ' -or $text[$j] -eq "`t" -or $text[$j] -eq "`r" -or $text[$j] -eq "`n")) { $j-- }
            if ($j -lt 1 -or $text.Substring($j - 1, 2) -ne '??') { continue }
            $qqStart = $j - 1
            $k = $qqStart - 1
            while ($k -ge 0 -and ($text[$k] -eq ' ' -or $text[$k] -eq "`t")) { $k-- }
            $leftEnd = $k + 1
            $depth = 0
            $leftStart = $leftEnd
            for ($p = $leftEnd - 1; $p -ge 0; $p--) {
                $c = $text[$p]
                if ($c -eq ')' -or $c -eq ']' -or $c -eq '}') { $depth++ }
                elseif ($c -eq '(' -or $c -eq '[' -or $c -eq '{') {
                    if ($depth -eq 0) { $leftStart = $p + 1; break }
                    $depth--
                }
                elseif ($depth -eq 0 -and ($c -eq ';' -or $c -eq '{' -or $c -eq '}' -or $c -eq ',')) {
                    $leftStart = $p + 1; break
                }
                elseif ($depth -eq 0 -and $c -eq '=') {
                    $prev = if ($p -gt 0) { $text[$p - 1] } else { '' }
                    $nxt = if ($p + 1 -lt $text.Length) { $text[$p + 1] } else { '' }
                    if ($prev -eq '=' -or $prev -eq '!' -or $prev -eq '<' -or $prev -eq '>') { continue }
                    if ($nxt -eq '=' -or $nxt -eq '>') { continue }
                    $leftStart = $p + 1; break
                }
                elseif ($depth -eq 0 -and $c -eq "`n") { $leftStart = $p + 1; break }
            }
            $leftExpr = $text.Substring($leftStart, $leftEnd - $leftStart).Trim()
            $end = $close + 1

            $asMatch = [regex]::Match($leftExpr, '\bas\s+([A-Za-z_][A-Za-z0-9_.]*)\s*$')
            if ($asMatch.Success) {
                $t = $asMatch.Groups[1].Value
                $replacement = "$leftExpr ?? ThrowHelper.$helper<$t>($args)"
                $text = $text.Substring(0, $leftStart) + $replacement + $text.Substring($end)
                $changes++
                continue
            }

            if ($leftExpr -match '^[A-Za-z_][A-Za-z0-9_]*$' -and $exc -eq 'ArgumentNullException') {
                $before = $text.Substring(0, $leftStart)
                $all = [regex]::Matches($before, '(^|\r?\n)([ \t]*)([A-Za-z_][A-Za-z0-9_]*)\s*=\s*$')
                if ($all.Count -gt 0) {
                    # Prefer argument + CallerArgumentExpression form (no type arg / nameof).
                    if ($leftExpr -eq 'string' -or (Find-FieldType $text $all[$all.Count - 1].Groups[3].Value) -eq 'string') {
                        $replacement = "$leftExpr ?? ThrowHelper.$helper<string>($leftExpr)"
                    } else {
                        $replacement = "$leftExpr ?? ThrowHelper.$helper($leftExpr)"
                    }
                    $text = $text.Substring(0, $leftStart) + $replacement + $text.Substring($end)
                    $changes++
                    continue
                }
            }

            if ($exc -eq 'ArgumentNullException' -and $leftExpr -match '\bas\b') {
                $nameArg = if ($args -match 'nameof\(([A-Za-z_][A-Za-z0-9_]*)\)') { $Matches[1] } else { $null }
                if ($nameArg) {
                    $replacement = "$leftExpr ?? ThrowHelper.$helper($leftExpr, nameof($nameArg))"
                    $text = $text.Substring(0, $leftStart) + $replacement + $text.Substring($end)
                    $changes++
                    continue
                }
            }

            if ($exc -eq 'ArgumentNullException' -and $leftExpr -match '^[A-Za-z_][A-Za-z0-9_]*$') {
                $replacement = "$leftExpr ?? ThrowHelper.$helper($leftExpr)"
                $text = $text.Substring(0, $leftStart) + $replacement + $text.Substring($end)
                $changes++
                continue
            }

            $t = $null
            if ($leftExpr -match '^[A-Za-z_][A-Za-z0-9_]*$') {
                $t = Find-FieldType $text $leftExpr
            }
            if (-not $t) {
                $before = $text.Substring(0, $leftStart).TrimEnd()
                $tm = [regex]::Match($before, '([A-Za-z_][A-Za-z0-9_]*)\s*=\s*$')
                if ($tm.Success) { $t = Find-FieldType $text $tm.Groups[1].Value }
            }
            if (-not $t) { continue }
            $replacement = "$leftExpr ?? ThrowHelper.$helper<$t>($args)"
            $text = $text.Substring(0, $leftStart) + $replacement + $text.Substring($end)
            $changes++
        }
    }

    if ($text -ne $original) {
        $text = $text -replace "`r`n", "`n" -replace "`n", "`r`n"
        [System.IO.File]::WriteAllText($path, $text, $utf8Bom)
    }
    return $changes
}

$totalFiles = 0
$totalChanges = 0
Get-ChildItem -Path $Root -Filter *.cs -Recurse | Where-Object {
    $_.Name -ne 'ThrowHelper.cs' -and $_.FullName -notmatch '\\obj\\'
} | ForEach-Object {
    $n = Convert-File $_.FullName
    if ($n -gt 0) {
        $totalFiles++
        $totalChanges += $n
        $rel = $_.FullName.Substring($Root.Length).TrimStart('\')
        Write-Host ("{0,4}  {1}" -f $n, $rel)
    }
}
Write-Host "Done: $totalChanges replacements in $totalFiles files"
