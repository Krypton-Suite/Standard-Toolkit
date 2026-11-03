<#
.SYNOPSIS
    Hosts KryptonSVGPictureBoxDemo and writes a PrintWindow PNG.
    Set KRYPTON_SVG_RASTERIZER to Direct2D or Skia before the form is created when a specific path is required.
    The net472 build can load Skia. net10 and net11 ignore Skia and use Direct2D.
    Adds the shared output x64 folder to PATH so a forced Skia path can load libSkiaSharp.dll.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), shows the SVG picture box demo,
    and writes Documents/PR/4657-krypton-svg-picture-box.png via Save-UnitTestWindowPng.
    Adds the shared output x64 folder to PATH so a forced Skia path can load libSkiaSharp.dll.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SvgPictureBoxScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4657-krypton-svg-picture-box.png'
}

$nativeDir = Join-Path $bin 'x64'
if (Test-Path -LiteralPath $nativeDir) {
    $env:PATH = $nativeDir + [System.IO.Path]::PathSeparator + $env:PATH
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Drawing.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonSVGPictureBoxDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonSVGPictureBoxDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 600
[System.Windows.Forms.Application]::DoEvents()

Save-UnitTestWindowPng -Form $form -Path $OutputPath -SettleMs 200

$form.Close()
$form.Dispose()
Write-Output "Wrote $OutputPath"
