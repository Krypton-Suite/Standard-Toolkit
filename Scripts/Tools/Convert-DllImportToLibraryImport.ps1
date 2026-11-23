# Converts LibraryImport-compatible [DllImport] methods to dual-path declarations.
param(
    [Parameter(Mandatory = $true)]
    [string]$Path,
    [string[]]$UnsupportedTypeNames = @(
        'CHOOSEFONT', 'POINTC', 'PAINTSTRUCT', 'MONITORINFO', 'OSVERSIONINFOEX',
        'SHFILEINFO', 'LOGFONT', 'SafeModuleHandle', 'NMHEADER', 'ICONINFO', 'DTTOPTS'
    )
)

$ErrorActionPreference = 'Stop'
$pathFull = (Resolve-Path -LiteralPath $Path).Path
$lines = [System.Collections.Generic.List[string]]::new()
$lines.AddRange([System.IO.File]::ReadAllLines($pathFull))

function Test-SkipSignature {
    param([string]$text)
    if ($text -match 'StringBuilder') { return $true }
    if ($text -match '\bHandleRef\b') { return $true }
    if ($text -match 'BestFitMapping|ThrowOnUnmappableChar') { return $true }
    if ($text -match 'IntPtr\[\]|byte\[\]|char\[\]|string\[\]') { return $true }
    if ($text -match '\bSafeModuleHandle\b') { return $true }
    foreach ($t in $UnsupportedTypeNames) {
        if ($text -match "\b$([regex]::Escape($t))\b") { return $true }
    }
    return $false
}

function Convert-DllImportAttribute {
    param([string]$attrText)
    if ($attrText -notmatch '\[DllImport\((.+)\)\]') { return $null }
    $inside = $Matches[1]

    $parts = [System.Collections.Generic.List[string]]::new()
    $current = ''
    $inQuote = $false
    $quoteChar = [char]0
    for ($ci = 0; $ci -lt $inside.Length; $ci++) {
        $ch = $inside[$ci]
        if ($inQuote) {
            $current += $ch
            if ($ch -eq $quoteChar) { $inQuote = $false }
            continue
        }
        if ($ch -eq '"' -or $ch -eq [char]39) {
            $inQuote = $true
            $quoteChar = $ch
            $current += $ch
            continue
        }
        if ($ch -eq ',') {
            $parts.Add($current.Trim())
            $current = ''
            continue
        }
        $current += $ch
    }
    if ($current.Trim().Length -gt 0) { $parts.Add($current.Trim()) }

    $library = $parts[0]
    $entryPoint = $null
    $setLastError = $false
    $stringMarshalling = $null

    for ($p = 1; $p -lt $parts.Count; $p++) {
        $part = $parts[$p]
        if ($part -match 'EntryPoint\s*=\s*(.+)$') { $entryPoint = $Matches[1].Trim() }
        elseif ($part -match 'SetLastError\s*=\s*true') { $setLastError = $true }
        elseif ($part -match 'CharSet\s*=\s*CharSet\.(Unicode|Ansi|Auto)') {
            if ($Matches[1] -eq 'Ansi') { $stringMarshalling = 'StringMarshalling.Utf8' }
            else { $stringMarshalling = 'StringMarshalling.Utf16' }
        }
    }

    $args = New-Object System.Collections.Generic.List[string]
    $args.Add($library)
    if ($null -ne $entryPoint) { $args.Add("EntryPoint = $entryPoint") }
    if ($setLastError) { $args.Add('SetLastError = true') }
    if ($null -ne $stringMarshalling) { $args.Add("StringMarshalling = $stringMarshalling") }
    return "[LibraryImport($($args -join ', '))]"
}

function Get-Indent {
    param([string]$line)
    if ($line -match '^(\s*)') { return $Matches[1] }
    return ''
}

function Fix-LibraryImportSignatureText {
    param([string]$text, [string]$indent)

    # Strip In/Out attrs
    $text = [regex]::Replace($text, '\[\s*In\s*,\s*Out\s*\]\s*', '')
    $text = [regex]::Replace($text, '\[\s*Out\s*\]\s*', '')
    $text = [regex]::Replace($text, '\[\s*In\s*\]\s*', '')
    $text = $text.Trim()

    # Split method head vs parameter list (ignore parens inside attributes)
    $openIdx = -1
    $depth = 0
    $inAttr = $false
    for ($si = 0; $si -lt $text.Length; $si++) {
        $ch = $text[$si]
        if ($ch -eq '[') { $inAttr = $true; continue }
        if ($ch -eq ']') { $inAttr = $false; continue }
        if ($inAttr) { continue }
        if ($ch -eq '(') {
            if ($depth -eq 0) {
                # Prefer the parenthesis that introduces the method parameter list
                # (comes after the method name)
                $openIdx = $si
                break
            }
            $depth++
        }
    }
    if ($openIdx -lt 0) { return @("$indent$text") }

    $head = $text.Substring(0, $openIdx)
    $tail = $text.Substring($openIdx) # includes ( ... );

    # Attribute bool parameters (case-sensitive; do not touch BOOL enum)
    $tail = [regex]::Replace($tail, '(?<!MarshalAs\(UnmanagedType\.Bool\)\s)\bbool\s+(\w+)', '[MarshalAs(UnmanagedType.Bool)] bool $1')

    $needReturnMarshal = $false
    if ([regex]::IsMatch($head, '\bbool\b') -and -not [regex]::IsMatch($head, '\[return:\s*MarshalAs')) {
        $needReturnMarshal = $true
    }

    $result = [System.Collections.Generic.List[string]]::new()
    if ($needReturnMarshal) {
        $result.Add("$indent[return: MarshalAs(UnmanagedType.Bool)]")
    }
    if ([regex]::IsMatch($head, '^\s*(\[return:[^\]]+\])\s*(.+)$')) {
        $m = [regex]::Match($head, '^\s*(\[return:[^\]]+\])\s*(.+)$')
        $result.Add("$indent$($m.Groups[1].Value)")
        $result.Add("$indent$($m.Groups[2].Value.Trim())$tail")
    }
    else {
        $result.Add("$indent$($head.Trim())$tail")
    }
    return $result
}

$converted = 0
$skipped = 0
$output = [System.Collections.Generic.List[string]]::new()
$i = 0

while ($i -lt $lines.Count) {
    $line = $lines[$i]

    if ($line -match '^\s*\[DllImport\(' -and $line -notmatch '^\s*//') {
        $preAttrs = [System.Collections.Generic.List[string]]::new()
        while ($output.Count -gt 0) {
            $prev = $output[$output.Count - 1]
            $pt = $prev.TrimStart()
            if ($pt.StartsWith('#')) { break }
            if ($pt.StartsWith('[') -or $pt.StartsWith('//') -or $pt.Length -eq 0) {
                $preAttrs.Insert(0, $prev)
                $output.RemoveAt($output.Count - 1)
            }
            else { break }
        }

        $blockLines = [System.Collections.Generic.List[string]]::new()
        foreach ($pa in $preAttrs) { $blockLines.Add($pa) }

        $j = $i
        $dllAttrText = ''
        $dllComplete = $false
        $signatureStarted = $false

        while ($j -lt $lines.Count) {
            $cur = $lines[$j]
            $blockLines.Add($cur)

            if (-not $dllComplete) {
                if ($dllAttrText.Length -eq 0) { $dllAttrText = $cur.Trim() }
                else { $dllAttrText = $dllAttrText.TrimEnd().TrimEnd(',') + ' ' + $cur.Trim() }
                if ($dllAttrText -match '\]\s*$') { $dllComplete = $true }
                $j++
                continue
            }

            if ($cur -match '\bextern\b') { $signatureStarted = $true }
            if ($signatureStarted -and $cur -match ';') { break }
            $j++
        }

        $blockText = ($blockLines -join "`n")

        if (-not $dllComplete -or (Test-SkipSignature -text $blockText)) {
            foreach ($bl in $blockLines) { $output.Add($bl) }
            $skipped++
            $i = $j + 1
            continue
        }

        $libraryImport = Convert-DllImportAttribute -attrText $dllAttrText
        if ($null -eq $libraryImport) {
            foreach ($bl in $blockLines) { $output.Add($bl) }
            $skipped++
            $i = $j + 1
            continue
        }

        $beforeDll = [System.Collections.Generic.List[string]]::new()
        $afterDll = [System.Collections.Generic.List[string]]::new()
        $sigLines = [System.Collections.Generic.List[string]]::new()
        $phase = 'before'

        foreach ($bl in $blockLines) {
            if ($phase -eq 'before') {
                if ($bl -match '\[DllImport\(') {
                    $phase = 'dll'
                    if ($bl -match '\]\s*$') { $phase = 'after' }
                    continue
                }
                $beforeDll.Add($bl)
                continue
            }
            if ($phase -eq 'dll') {
                if ($bl -match '\]\s*$') { $phase = 'after' }
                continue
            }
            if ($phase -eq 'after') {
                if ($bl -match '^\s*\[') { $afterDll.Add($bl); continue }
                if ($bl.Trim().Length -eq 0) { continue }
                $phase = 'sig'
                $sigLines.Add($bl)
                continue
            }
            $sigLines.Add($bl)
        }

        if ($sigLines.Count -eq 0) {
            foreach ($bl in $blockLines) { $output.Add($bl) }
            $skipped++
            $i = $j + 1
            continue
        }

        $indent = Get-Indent -line $sigLines[0]

        # Move [return: MarshalAs] from afterDll into signature fixing if present
        $afterKeep = [System.Collections.Generic.List[string]]::new()
        $returnAttr = $null
        foreach ($aa in $afterDll) {
            if ($aa -match '\[return:\s*MarshalAs') { $returnAttr = $aa.Trim(); continue }
            $afterKeep.Add($aa)
        }
        foreach ($ba in @($beforeDll.ToArray())) {
            if ($ba -match '\[return:\s*MarshalAs') {
                $returnAttr = $ba.Trim()
                [void]$beforeDll.Remove($ba)
            }
        }

        $sigJoined = (($sigLines | ForEach-Object { $_.Trim() }) -join ' ')
        $sigJoined = $sigJoined -replace '\bextern\b', 'partial'
        if ($null -ne $returnAttr) {
            $sigJoined = "$returnAttr $sigJoined"
        }

        $libSigFixed = Fix-LibraryImportSignatureText -text $sigJoined -indent $indent

        $output.Add("${indent}#if NET8_0_OR_GREATER")
        foreach ($ba in $beforeDll) {
            if ($ba.Trim().Length -eq 0) { continue }
            $output.Add($ba)
        }
        $output.Add("${indent}$libraryImport")
        foreach ($aa in $afterKeep) { $output.Add($aa) }
        foreach ($sl in $libSigFixed) { $output.Add($sl) }
        $output.Add("${indent}#else")
        foreach ($bl in $blockLines) { $output.Add($bl) }
        $output.Add("${indent}#endif")

        $converted++
        $i = $j + 1
        continue
    }

    $output.Add($line)
    $i++
}

$text = ($output -join "`r`n") + "`r`n"
$text = [regex]::Replace($text, '(?m)^(\s*)public class Dwm\b', '${1}public partial class Dwm')

[System.IO.File]::WriteAllText($pathFull, $text)
Write-Host "Converted=$converted Skipped=$skipped File=$pathFull"
