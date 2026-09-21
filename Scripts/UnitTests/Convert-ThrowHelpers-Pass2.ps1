#Requires -Version 5.1
<#
.SYNOPSIS
  Second-pass ThrowHelper conversion for remaining ?? and => throw expression sites.
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
        if ($c -eq [char]'"' -or $c -eq [char]"'") { $inStr = $c; continue }
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

function Test-IsPlausibleType([string]$t) {
    if ([string]::IsNullOrWhiteSpace($t)) { return $false }
    $t = $t.Trim().TrimEnd('?')
    $skip = @(
        'void','var','return','if','while','for','switch','using','fixed','foreach',
        'class','struct','enum','interface','record','get','set','new','this','base',
        'true','false','null','details','parameter','value','result','temp','item','items'
    )
    if ($skip -contains $t) { return $false }
    if ($t -in @('bool','byte','sbyte','short','ushort','int','uint','long','ulong','float','double','decimal','char','string','object')) {
        return $true
    }
    # Type names must start with an uppercase letter (avoids comment words like "details").
    return ($t[0] -cmatch '[A-Z]')
}

function Infer-TypeFromPropertyOrField([string]$text, [string]$name) {
    if ([string]::IsNullOrWhiteSpace($name)) { return $null }
    $rx = [regex]("(?:public|protected|internal|private)\s+(?:(?:static|readonly|override|virtual|sealed|new|partial|volatile)\s+)*([\w.?]+(?:\s*<[^>]+>)?)\s+$([regex]::Escape($name))\s*[\{;=]")
    $m = $rx.Match($text)
    if (-not $m.Success) { return $null }
    $t = $m.Groups[1].Value.Trim().TrimEnd('?')
    if (Test-IsPlausibleType $t) { return $t }
    return $null
}

function Infer-TypeFromMethod([string]$text, [int]$throwIdx) {
    # Palette switch expressions can be >4k characters; look far enough for the owning method.
    $start = [Math]::Max(0, $throwIdx - 80000)
    $window = $text.Substring($start, $throwIdx - $start)

    # Property/indexer: public override ViewBase this[int index]
    $idx = [regex]::Match($window, '(?:public|protected|internal|private)\s+(?:(?:static|override|virtual|sealed|new|partial)\s+)*([\w.?]+(?:\s*<[^>]+>)?)\s+this\s*\[')
    if ($idx.Success) {
        $ret = $idx.Groups[1].Value.Trim().TrimEnd('?')
        if (Test-IsPlausibleType $ret) { return $ret }
    }

    # Method/property getter return type — last signature before throw
    # Include expression-bodied members: Type Name =>
    $rx = [regex]'(?:public|protected|internal|private)\s+(?:(?:static|override|virtual|sealed|new|partial|async)\s+)*([\w.?]+(?:\s*<[^>]+>)?)\s+(\w+)\s*(?:[\(\{]|=>)'
    $ms = $rx.Matches($window)
    if ($ms.Count -gt 0) {
        $ret = $ms[$ms.Count - 1].Groups[1].Value.Trim().TrimEnd('?')
        if (Test-IsPlausibleType $ret) { return $ret }
    }

    return $null
}

function Infer-TypeFromSwitch([string]$text, [int]$throwIdx) {
    $pos = $text.LastIndexOf('switch', $throwIdx)
    if ($pos -lt 0) { return $null }
    $brace = $text.IndexOf('{', $pos, $throwIdx - $pos)
    if ($brace -lt 0) { return $null }
    $depth = 0
    $end = $brace
    for ($i = $brace; $i -lt [Math]::Min($text.Length, $throwIdx + 200); $i++) {
        if ($text[$i] -eq '{') { $depth++ }
        elseif ($text[$i] -eq '}') {
            $depth--
            if ($depth -eq 0) { $end = $i; break }
        }
    }
    $block = $text.Substring($brace, [Math]::Max(0, $end - $brace + 1))
    $types = @{}
    foreach ($m in [regex]::Matches($block, '=>\s*([A-Za-z_][A-Za-z0-9_]*(?:\.[A-Za-z_][A-Za-z0-9_]*)+)\b')) {
        $expr = $m.Groups[1].Value
        if ($expr.StartsWith('nameof') -or $expr.StartsWith('throw') -or $expr.StartsWith('ThrowHelper')) { continue }
        $parts = $expr.Split('.')
        if ($parts.Length -ge 2) {
            $typeName = $parts[$parts.Length - 2]
            if ((Test-IsPlausibleType $typeName) -and $typeName -notin @('SharedStaticVariables','BaseColors','Resources','SystemColors','SharedStaticConstants','SharedStaticFunctions')) {
                if ($types.ContainsKey($typeName)) { $types[$typeName]++ } else { $types[$typeName] = 1 }
            }
        }
    }
    if ($types.Count -gt 0) {
        return ($types.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First 1).Key
    }
    if ($block -match '=>\s*true\b' -or $block -match '=>\s*false\b') { return 'bool' }
    if ($block -match '=>\s*\d+f\b') { return 'float' }
    if ($block -match '=>\s*\d+\.\d+') { return 'double' }
    if ($block -match '=>\s*\d+\b') { return 'int' }
    if ($block -match '=>\s*null\b') {
        # nullable reference — prefer method return
        return $null
    }
    return $null
}

function Infer-CoalesceType([string]$text, [int]$leftStart, [int]$leftEnd, [int]$throwIdx) {
    $left = $text.Substring($leftStart, $leftEnd - $leftStart).Trim()

    # (Type)expr or (Type?)expr
    $cast = [regex]::Match($left, '^\(\s*([\w.?]+(?:\s*<[^>]+>)?)\s*\)')
    if ($cast.Success) {
        $t = $cast.Groups[1].Value.Trim().TrimEnd('?')
        if (Test-IsPlausibleType $t) { return $t }
    }

    # expr as Type
    $as = [regex]::Match($left, '\bas\s+([\w.?]+(?:\s*<[^>]+>)?)\s*$')
    if ($as.Success) {
        $t = $as.Groups[1].Value.Trim().TrimEnd('?')
        if (Test-IsPlausibleType $t) { return $t }
    }

    # Prefer declared assignment / property type over method return (ternary coalesce can sit
    # far from "Type name =" and method return is often the wrong outer method type).
    $before = $text.Substring([Math]::Max(0, $leftStart - 800), [Math]::Min(800, $leftStart))
    $decls = [regex]::Matches($before, '([\w.?]+(?:\s*<[^>]+>)?)\s+(\w+)\s*=')
    for ($i = $decls.Count - 1; $i -ge 0; $i--) {
        $cand = $decls[$i].Groups[1].Value.Trim().TrimEnd('?')
        if (Test-IsPlausibleType $cand) { return $cand }
    }

    $propAssign = [regex]::Match($before, '(\w+)\s*=\s*$')
    if ($propAssign.Success) {
        $fromProp = Infer-TypeFromPropertyOrField $text $propAssign.Groups[1].Value
        if ($fromProp) { return $fromProp }
    }

    $fromMethod = Infer-TypeFromMethod $text $throwIdx
    if ($fromMethod) { return $fromMethod }

    # Common member suffixes
    if ($left -match '\.(ThreeState|Checked|ShowCheckBox|ShowUpDown|AutoShift|UseColumnTextForButtonValue)\s*$') { return 'bool' }
    if ($left -match '\.(MaxDropDownItems|DropDownHeight|DropDownWidth)\s*$') { return 'int' }
    if ($left -match '\.(DropDownStyle)$') { return 'ComboBoxStyle' }
    if ($left -match '\.(AutoCompleteMode)$') { return 'AutoCompleteMode' }
    if ($left -match '\.(AutoCompleteSource)$') { return 'AutoCompleteSource' }
    if ($left -match '\.(ButtonStyle)$') { return 'ButtonStyle' }
    if ($left -match '\.(Format)$') { return 'DateTimePickerFormat' }
    if ($left -match '\.(CustomFormat|CustomNullText|CalendarTodayText|Mask|DisplayMember|ValueMember|Text)\s*$') { return 'string' }

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

function Convert-File([string]$path) {
    $bytes = [IO.File]::ReadAllBytes($path)
    $text = [Text.Encoding]::UTF8.GetString($bytes)
    if ($text.Length -gt 0 -and [int][char]$text[0] -eq 0xFEFF) { $text = $text.Substring(1) }
    $original = $text
    $changes = 0

    $rx = [regex]'throw\s+new\s+(NullReferenceException|ArgumentOutOfRangeException|ArgumentException|InvalidOperationException|NotSupportedException|NotImplementedException|ObjectDisposedException|InvalidCastException|Win32Exception)\s*\('
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
        if ($null -eq $ctx) { continue }

        $t = $null
        if ($ctx -eq 'arrow') {
            $t = Infer-TypeFromSwitch $text $throwStart
            if (-not $t) { $t = Infer-TypeFromMethod $text $throwStart }
            # Assignment typed switch: MessageButton x = expr switch { ... => throw }
            if (-not $t) {
                $pre = $text.Substring([Math]::Max(0, $throwStart - 600), [Math]::Min(600, $throwStart))
                $am = [regex]::Match($pre, '([\w.?]+(?:\s*<[^>]+>)?)\s+(\w+)\s*=\s*(?:[\s\S]*?)\bswitch\b[\s\S]*$')
                if ($am.Success) {
                    $cand = $am.Groups[1].Value.Trim().TrimEnd('?')
                    if (Test-IsPlausibleType $cand) { $t = $cand }
                }
            }
            # Property setter: set => throw
            if (-not $t) {
                $pre = $text.Substring([Math]::Max(0, $throwStart - 400), [Math]::Min(400, $throwStart))
                if ($pre -match '\bset\s*=>') {
                    # setters are void — use object as unused typed return for expression body
                    $t = 'object'
                }
            }
            if (-not $t -and $args -eq '') { $t = Infer-TypeFromMethod $text $throwStart }
            if (-not $t) { continue }
            if (-not (Test-IsPlausibleType $t)) { continue }
            if ([string]::IsNullOrWhiteSpace($args)) {
                $replacement = "ThrowHelper.$helper<$t>()"
            } else {
                $replacement = "ThrowHelper.$helper<$t>($args)"
            }
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
            $t = Infer-CoalesceType $text $leftStart $leftEnd $throwStart
            if (-not $t -or -not (Test-IsPlausibleType $t)) { continue }
            $end = $close + 1
            $replacement = "$leftExpr ?? ThrowHelper.$helper<$t>($args)"
            $text = $text.Substring(0, $leftStart) + $replacement + $text.Substring($end)
            $changes++
        }
    }

    if ($text -ne $original) {
        $text = $text -replace "`r`n", "`n" -replace "`n", "`r`n"
        [IO.File]::WriteAllText($path, $text, $utf8Bom)
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
        Write-Host ("{0,4}  {1}" -f $n, $_.FullName.Substring($Root.Length).TrimStart('\'))
    }
}
Write-Host "Done: $totalChanges replacements in $totalFiles files"
