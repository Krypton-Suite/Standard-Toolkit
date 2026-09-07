<#
.SYNOPSIS
    Hosts KryptonSplitButtonDemo and captures a PNG for the local PR description.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows KryptonSplitButtonDemo on-screen,
    and writes Documents/PR/4366-krypton-split-button-default.png (or -OutputPath).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SplitButtonScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4366-krypton-split-button-default.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonSplitButtonDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonSplitButtonDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 800
[System.Windows.Forms.Application]::DoEvents()

$ksplit = $form.Controls.Find('ksplit', $true)
if ($ksplit -and $ksplit.Count -gt 0) {
    $ksplit[0].PerformDropDown()
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    [System.Windows.Forms.Application]::DoEvents()
}

$bounds = $form.Bounds
$padBottom = 140
$capture = [System.Drawing.Rectangle]::new(
    $bounds.X,
    $bounds.Y,
    $bounds.Width,
    $bounds.Height + $padBottom)
$bmp = New-Object System.Drawing.Bitmap $capture.Width, $capture.Height
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.CopyFromScreen($capture.Location, [System.Drawing.Point]::Empty, $capture.Size)
$g.Dispose()
$bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
$form.Close()
$form.Dispose()

Write-Host "Wrote $OutputPath"
