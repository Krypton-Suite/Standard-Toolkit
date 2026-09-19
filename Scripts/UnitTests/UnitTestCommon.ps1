# Shared helpers for Scripts/UnitTests/*.ps1
# Dot-source from a unit-test script after setting $script:RepoRoot if needed.

function Get-UnitTestRepoRoot {
    param([string]$StartPath = $PSScriptRoot)
    $dir = Resolve-Path -LiteralPath $StartPath
    while ($dir) {
        if (Test-Path -LiteralPath (Join-Path $dir 'AGENTS.md')) {
            return [string]$dir
        }
        $parent = Split-Path -Parent $dir
        if (-not $parent -or $parent -eq $dir) {
            break
        }
        $dir = $parent
    }
    throw "Could not locate repository root from '$StartPath'."
}

function Get-UnitTestBinDir {
    param(
        [string]$RepoRoot,
        [string]$Configuration = 'Debug',
        [string]$TargetFramework = 'net472',
        [string]$BinDir
    )

    if ($BinDir) {
        return (Resolve-Path -LiteralPath $BinDir).Path
    }

    $path = Join-Path $RepoRoot "Bin\$Configuration\$TargetFramework"
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Bin directory not found: $path. Build TestForm first."
    }
    return (Resolve-Path -LiteralPath $path).Path
}

function Register-UnitTestAssemblyResolver {
    param([string]$BinDir)

    $script:UnitTestBinDir = $BinDir
    $script:UnitTestResolving = @{}

    [System.AppDomain]::CurrentDomain.add_AssemblyResolve({
        param($sender, $e)
        $name = ($e.Name -split ',')[0]
        foreach ($a in [System.AppDomain]::CurrentDomain.GetAssemblies()) {
            if ($a.GetName().Name -eq $name) {
                return $a
            }
        }
        if ($script:UnitTestResolving.ContainsKey($name)) {
            return $null
        }
        $script:UnitTestResolving[$name] = $true
        foreach ($ext in '.dll', '.exe') {
            $candidate = Join-Path $script:UnitTestBinDir ($name + $ext)
            if (Test-Path -LiteralPath $candidate) {
                return [System.Reflection.Assembly]::LoadFile($candidate)
            }
        }
        return $null
    })
}

function Initialize-UnitTestNativeInput {
    if (-not ('UnitTestNative' -as [type])) {
        Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class UnitTestNative
{
    [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")] public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
    public const uint LEFTDOWN = 0x0002;
    public const uint LEFTUP = 0x0004;
    public const uint RIGHTDOWN = 0x0008;
    public const uint RIGHTUP = 0x0010;
}
"@
    }

    [void][UnitTestNative]::SetProcessDPIAware()
}

function Initialize-UnitTestCaptureNative {
    if (-not ('UnitTestCaptureNative' -as [type])) {
        Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class UnitTestCaptureNative
{
    // PW_RENDERFULLCONTENT (2): capture DWM/composition content for modern WinForms chrome.
    public const uint PW_RENDERFULLCONTENT = 2;
    [DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
}
"@
    }
}

function Save-UnitTestWindowPng {
    <#
    .SYNOPSIS
        Captures a WinForms window to PNG without grabbing the IDE/Cursor overlay.

    .DESCRIPTION
        Prefer PrintWindow(PW_RENDERFULLCONTENT) for the HWND so CopyFromScreen cannot
        capture Cursor, Visual Studio, or another occluding window. Use -InflateX/-InflateY
        only when a popup must be included outside the form bounds (then TopMost +
        SetForegroundWindow + CopyFromScreen). Always verify the PNG shows the demo.
    #>
    [CmdletBinding(DefaultParameterSetName = 'Form')]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'Form')]
        [System.Windows.Forms.Form]$Form,

        [Parameter(Mandatory = $true, ParameterSetName = 'Handle')]
        [System.IntPtr]$Handle,

        [Parameter(Mandatory = $true)]
        [string]$Path,

        [int]$InflateX = 0,
        [int]$InflateY = 0,
        [int]$SettleMs = 400
    )

    Add-Type -AssemblyName System.Drawing
    Add-Type -AssemblyName System.Windows.Forms
    Initialize-UnitTestNativeInput
    Initialize-UnitTestCaptureNative

    $hwnd = if ($PSCmdlet.ParameterSetName -eq 'Form') { $Form.Handle } else { $Handle }
    if ($hwnd -eq [IntPtr]::Zero) {
        throw 'Window handle is zero; Show the form before capturing.'
    }

    $wasTopMost = $false
    if ($PSCmdlet.ParameterSetName -eq 'Form') {
        $wasTopMost = [bool]$Form.TopMost
        $Form.TopMost = $true
        if (-not $Form.Visible) {
            $Form.Show()
        }
        $Form.Activate()
        $Form.BringToFront()
    }

    [void][UnitTestNative]::SetForegroundWindow($hwnd)
    [System.Windows.Forms.Application]::DoEvents()
    if ($SettleMs -gt 0) {
        Start-Sleep -Milliseconds $SettleMs
    }
    [System.Windows.Forms.Application]::DoEvents()

    $outDir = Split-Path -Parent $Path
    if ($outDir -and -not (Test-Path -LiteralPath $outDir)) {
        New-Item -ItemType Directory -Path $outDir | Out-Null
    }

    try {
        if ($InflateX -eq 0 -and $InflateY -eq 0) {
            # HWND capture: safe when Cursor/IDE covers the same screen region.
            $rect = New-Object UnitTestCaptureNative+RECT
            if (-not [UnitTestCaptureNative]::GetWindowRect($hwnd, [ref]$rect)) {
                throw 'GetWindowRect failed.'
            }

            $width = $rect.Right - $rect.Left
            $height = $rect.Bottom - $rect.Top
            if ($width -le 0 -or $height -le 0) {
                throw "Invalid window size ${width}x${height}."
            }

            $bmp = New-Object System.Drawing.Bitmap $width, $height
            $g = [System.Drawing.Graphics]::FromImage($bmp)
            $hdc = $g.GetHdc()
            try {
                [void][UnitTestCaptureNative]::PrintWindow(
                    $hwnd,
                    $hdc,
                    [UnitTestCaptureNative]::PW_RENDERFULLCONTENT)
                $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
            }
            finally {
                $g.ReleaseHdc($hdc)
                $g.Dispose()
                $bmp.Dispose()
            }
        }
        else {
            # Expanded screen blit for popups outside the form; requires TopMost + foreground.
            if ($PSCmdlet.ParameterSetName -ne 'Form') {
                throw 'InflateX/InflateY require -Form so Bounds and TopMost can be applied.'
            }

            $bounds = $Form.Bounds
            $x = [Math]::Max(0, $bounds.X - $InflateX)
            $y = [Math]::Max(0, $bounds.Y - $InflateY)
            $w = $bounds.Width + (2 * $InflateX)
            $h = $bounds.Height + (2 * $InflateY)
            $capture = [System.Drawing.Rectangle]::new($x, $y, $w, $h)
            $bmp = New-Object System.Drawing.Bitmap $capture.Width, $capture.Height
            $g = [System.Drawing.Graphics]::FromImage($bmp)
            try {
                $g.CopyFromScreen($capture.Location, [System.Drawing.Point]::Empty, $capture.Size)
                $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
            }
            finally {
                $g.Dispose()
                $bmp.Dispose()
            }
        }
    }
    finally {
        if (($PSCmdlet.ParameterSetName -eq 'Form') -and (-not $wasTopMost)) {
            $Form.TopMost = $false
        }
    }

    Write-Host "Wrote $Path"
}

function Get-UnitTestCiMarker {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path
    )

    # Read a prefix only - markers must appear near the top of the script.
    $lines = Get-Content -LiteralPath $Path -TotalCount 80 -ErrorAction Stop
    foreach ($line in $lines) {
        if ($line -match '^\s*#\s*UnitTest-CI\s*:\s*(include|exclude)\s*$') {
            return $Matches[1].ToLowerInvariant()
        }
    }

    return $null
}

function Get-UnitTestScriptClassification {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Root
    )

    # CI assert scripts use the UnitTest-*.ps1 prefix (helpers are Start-/Invoke-/Get-/Convert-*).
    $files = @(Get-ChildItem -LiteralPath $Root -Filter 'UnitTest-*.ps1' -File -Recurse |
        Where-Object { $_.Name -ne 'Invoke-AllUnitTests.ps1' })

    $included = New-Object System.Collections.Generic.List[System.IO.FileInfo]
    $excluded = New-Object System.Collections.Generic.List[System.IO.FileInfo]
    $undeclared = New-Object System.Collections.Generic.List[System.IO.FileInfo]

    foreach ($file in $files) {
        $marker = Get-UnitTestCiMarker -Path $file.FullName
        switch ($marker) {
            'include' { $included.Add($file) }
            'exclude' { $excluded.Add($file) }
            default { $undeclared.Add($file) }
        }
    }

    return [pscustomobject]@{
        Included   = $included.ToArray()
        Excluded   = $excluded.ToArray()
        Undeclared = $undeclared.ToArray()
    }
}

function Write-GitHubError {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message,
        [string]$File
    )

    if ($env:GITHUB_ACTIONS -ne 'true') {
        return
    }

    if ($File) {
        Write-Host "::error file=$File::$Message"
    }
    else {
        Write-Host "::error::$Message"
    }
}

function Write-GitHubNotice {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message,
        [string]$File
    )

    if ($env:GITHUB_ACTIONS -ne 'true') {
        return
    }

    if ($File) {
        Write-Host "::notice file=$File::$Message"
    }
    else {
        Write-Host "::notice::$Message"
    }
}

function Write-GitHubWarning {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message,
        [string]$File
    )

    if ($env:GITHUB_ACTIONS -ne 'true') {
        return
    }

    if ($File) {
        Write-Host "::warning file=$File::$Message"
    }
    else {
        Write-Host "::warning::$Message"
    }
}

function Write-UnitTestBanner {
    param(
        [Parameter(Mandatory = $true)]
        [ValidateSet('PASS', 'FAIL', 'SKIP', 'INFO')]
        [string]$Status,
        [Parameter(Mandatory = $true)]
        [string]$Message
    )

    $color = switch ($Status) {
        'PASS' { 'Green' }
        'FAIL' { 'Red' }
        'SKIP' { 'Yellow' }
        default { 'Cyan' }
    }

    $prefix = switch ($Status) {
        'PASS' { 'PASS' }
        'FAIL' { 'FAIL' }
        'SKIP' { 'SKIP' }
        default { 'INFO' }
    }

    Write-Host "[$prefix] $Message" -ForegroundColor $color
}

function Write-UnitTestJobSummary {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Title,
        [Parameter(Mandatory = $true)]
        [string]$Overall,
        [Parameter(Mandatory = $true)]
        [AllowEmptyCollection()]
        [string[]]$Passed,
        [Parameter(Mandatory = $true)]
        [AllowEmptyCollection()]
        [string[]]$Failed,
        [Parameter(Mandatory = $true)]
        [AllowEmptyCollection()]
        [string[]]$Skipped,
        [int]$UndeclaredCount = 0,
        [string]$BinPath = ''
    )

    $lines = New-Object System.Collections.Generic.List[string]
    [void]$lines.Add("## $Title")
    [void]$lines.Add('')
    [void]$lines.Add("**Result:** $Overall")
    if ($BinPath) {
        [void]$lines.Add("")
        [void]$lines.Add("- Bin: ``$BinPath``")
    }
    [void]$lines.Add('')
    [void]$lines.Add('| Script | Status |')
    [void]$lines.Add('| --- | --- |')
    foreach ($item in ($Passed | Sort-Object)) {
        [void]$lines.Add("| ``$item`` | PASS |")
    }
    foreach ($item in ($Failed | Sort-Object)) {
        [void]$lines.Add("| ``$item`` | FAIL |")
    }
    foreach ($item in ($Skipped | Sort-Object)) {
        [void]$lines.Add("| ``$item`` | SKIP (exclude) |")
    }
    [void]$lines.Add('')
    [void]$lines.Add("- Passed: **$($Passed.Count)**")
    [void]$lines.Add("- Failed: **$($Failed.Count)**")
    [void]$lines.Add("- Skipped (exclude): **$($Skipped.Count)**")
    [void]$lines.Add("- Undeclared markers: **$UndeclaredCount**")

    $markdown = ($lines -join "`n") + "`n"

    if ($env:GITHUB_STEP_SUMMARY) {
        Add-Content -LiteralPath $env:GITHUB_STEP_SUMMARY -Value $markdown -Encoding utf8
    }

    Write-Host ""
    Write-Host "----- Unit test summary -----" -ForegroundColor Cyan
    Write-Host $markdown.TrimEnd()
    Write-Host "-----------------------------" -ForegroundColor Cyan
}

function Invoke-UnitTestDrag {
    param(
        [int]$FromX,
        [int]$FromY,
        [int]$ToX,
        [int]$ToY,
        [int]$Steps = 24,
        [int]$StepDelayMs = 50
    )

    Write-Host "drag from $FromX,$FromY to $ToX,$ToY"
    [UnitTestNative]::SetCursorPos($FromX, $FromY)
    Start-Sleep -Milliseconds 300
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTDOWN, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 250
    for ($i = 1; $i -le $Steps; $i++) {
        $x = [int]($FromX + ($ToX - $FromX) * $i / $Steps)
        $y = [int]($FromY + ($ToY - $FromY) * $i / $Steps)
        [UnitTestNative]::SetCursorPos($x, $y)
        Start-Sleep -Milliseconds $StepDelayMs
    }
    Start-Sleep -Milliseconds 400
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTUP, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 1200
}
