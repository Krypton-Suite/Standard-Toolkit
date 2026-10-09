# UnitTest-CI: exclude
#Requires -Version 5.1
<#
.SYNOPSIS
    Hosts Bug4413DisabledCaptionGlyphDemo and writes a PNG for the V105 LTS backport.

.DESCRIPTION
    Loads Debug TestForm in-process (STA) and saves Documents/PR/4413-disabled-caption-glyph-v105-demo.png.
    PrintWindow returns a black buffer for this form, so the capture uses DrawToBitmap.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-4413DisabledCaptionScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputPath) {
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4413-disabled-caption-glyph-v105-demo.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
$utilities = Join-Path $bin 'Krypton.Toolkit.Utilities.dll'
if (Test-Path -LiteralPath $utilities) {
    [void][System.Reflection.Assembly]::LoadFrom($utilities)
}
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$formType = $asm.GetType('TestForm.Bug4413DisabledCaptionGlyphDemo')
if (-not $formType) {
    throw 'Type TestForm.Bug4413DisabledCaptionGlyphDemo was not found in TestForm.exe.'
}

$working = [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea
$form = [System.Activator]::CreateInstance($formType)
try {
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point ($working.Left + 80), ($working.Top + 80)
    $form.Show()
    $form.TopMost = $true
    $form.Activate()
    $form.BringToFront()
    if ($form.Handle -ne [IntPtr]::Zero) {
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    }
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 600
    $form.Refresh()
    [System.Windows.Forms.Application]::DoEvents()
    $bmp = New-Object System.Drawing.Bitmap $form.Width, $form.Height
    try {
        $form.DrawToBitmap($bmp, (New-Object System.Drawing.Rectangle 0, 0, $form.Width, $form.Height))
        $bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $bmp.Dispose()
    }
    Write-Host "Wrote $OutputPath"
}
finally {
    $form.Close()
    $form.Dispose()
}
