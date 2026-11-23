<#
.SYNOPSIS
    Opens Bug4432ButtonSpecFillHeightDemo and captures a PNG for the PR description.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows the #4432 demo, and writes
    Documents/PR/4432-buttonspec-fillheight-default.png (or -OutputPath).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ButtonSpecFillHeightScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4432-buttonspec-fillheight-default.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

$toolkitPath = Join-Path $bin 'Krypton.Toolkit.dll'
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}
[void][System.Reflection.Assembly]::LoadFrom($toolkitPath)
$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Bug4432ButtonSpecFillHeightDemo')
if (-not $formType) {
    throw 'Type TestForm.Bug4432ButtonSpecFillHeightDemo was not found.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 700
[System.Windows.Forms.Application]::DoEvents()

# Prefer DrawToBitmap so capture is not affected by overlapping desktop windows.
$bounds = $form.Bounds
$bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
$form.DrawToBitmap($bmp, (New-Object System.Drawing.Rectangle 0, 0, $bounds.Width, $bounds.Height))
$bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
$form.Close()
$form.Dispose()

Write-Output "Wrote $OutputPath"
