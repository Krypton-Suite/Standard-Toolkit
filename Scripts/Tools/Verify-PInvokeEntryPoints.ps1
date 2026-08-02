<#
.SYNOPSIS
    Verifies that every [LibraryImport] declaration resolves to a real native export.

.DESCRIPTION
    The [LibraryImport] source generator does NOT perform the ANSI/Unicode entry point
    probing that [DllImport] does. An import named 'GetWindowLong' therefore fails at
    runtime with EntryPointNotFoundException even though the DllImport equivalent
    silently resolved 'GetWindowLongW'.

    This script parses the interop sources, resolves each LibraryImport entry point
    against the real module export table via GetProcAddress, and reports any that are
    missing (suggesting a 'W' variant when one exists).

.EXAMPLE
    pwsh -File Scripts\Tools\Verify-PInvokeEntryPoints.ps1
#>
[CmdletBinding()]
param(
    [string] $Root
)

$ErrorActionPreference = 'Stop'

if (-not $Root) {
    $Root = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
}

Add-Type -Namespace Native -Name Loader -MemberDefinition @'
[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
public static extern System.IntPtr LoadLibraryA(string name);

[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true)]
public static extern System.IntPtr GetProcAddress(System.IntPtr module, string name);
'@

$moduleCache = @{}
function Get-NativeModule([string] $fileName) {
    if (-not $moduleCache.ContainsKey($fileName)) {
        $moduleCache[$fileName] = [Native.Loader]::LoadLibraryA($fileName)
    }
    return $moduleCache[$fileName]
}

function Test-Export([string] $fileName, [string] $entryPoint) {
    $module = Get-NativeModule $fileName
    if ($module -eq [IntPtr]::Zero) { return $null }   # module not present on this machine
    return ([Native.Loader]::GetProcAddress($module, $entryPoint) -ne [IntPtr]::Zero)
}

# Map the Libraries.* constants to their file names.
$libraryMap = @{}
$sources = Get-ChildItem -Path (Join-Path $Root 'Source') -Filter '*.cs' -Recurse -File
foreach ($file in $sources) {
    $text = Get-Content -LiteralPath $file.FullName -Raw
    if (-not $text) { continue }
    foreach ($match in [regex]::Matches($text, 'public\s+const\s+string\s+(\w+)\s*=\s*@?"([^"]+)"')) {
        $libraryMap[$match.Groups[1].Value] = $match.Groups[2].Value
    }
}

$attributePattern = '\[LibraryImport\(\s*(?:Libraries\.)?(?<lib>\w+)(?<args>[^\]]*)\]'
$entryPointPattern = 'EntryPoint\s*=\s*@?"(?<ep>[^"]+)"'
$methodPattern = 'static\s+partial\s+[\w\.\<\>\[\]\?]+\s+(?<name>\w+)\s*\('

$problems = @()
$checked = 0

foreach ($file in $sources) {
    $lines = Get-Content -LiteralPath $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $attributeMatch = [regex]::Match($lines[$i], $attributePattern)
        if (-not $attributeMatch.Success) { continue }

        $libToken = $attributeMatch.Groups['lib'].Value
        $fileName = if ($libraryMap.ContainsKey($libToken)) { $libraryMap[$libToken] } else { $libToken }

        $entryPoint = $null
        $epMatch = [regex]::Match($attributeMatch.Groups['args'].Value, $entryPointPattern)
        if ($epMatch.Success) { $entryPoint = $epMatch.Groups['ep'].Value }

        # Walk forward to the generated method signature for the implicit entry point name.
        $signatureLine = $null
        for ($j = $i + 1; $j -lt [Math]::Min($i + 8, $lines.Count); $j++) {
            $sigMatch = [regex]::Match($lines[$j], $methodPattern)
            if ($sigMatch.Success) { $signatureLine = $j; if (-not $entryPoint) { $entryPoint = $sigMatch.Groups['name'].Value }; break }
        }
        if (-not $entryPoint) { continue }

        # Ordinal imports (EntryPoint = "#94") are resolved by number, not by name.
        if ($entryPoint.StartsWith('#')) { continue }

        $checked++
        $exists = Test-Export $fileName $entryPoint
        if ($null -eq $exists) { continue }   # module unavailable, cannot judge
        if ($exists) { continue }

        $suggestion = ''
        foreach ($suffix in 'W', 'A') {
            if (Test-Export $fileName ($entryPoint + $suffix)) { $suggestion = $entryPoint + $suffix; break }
        }

        $problems += [pscustomobject]@{
            File       = $file.FullName.Substring($Root.Length).TrimStart('\')
            Line       = $i + 1
            Library    = $fileName
            EntryPoint = $entryPoint
            Suggestion = $suggestion
        }
    }
}

Write-Host "Checked $checked LibraryImport entry points."
if ($problems.Count -eq 0) {
    Write-Host 'All entry points resolved.' -ForegroundColor Green
    exit 0
}

$problems | Format-Table -AutoSize
Write-Host "$($problems.Count) unresolved entry point(s)." -ForegroundColor Red
exit 1
