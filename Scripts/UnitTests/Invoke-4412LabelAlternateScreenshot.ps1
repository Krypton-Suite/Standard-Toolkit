# UnitTest-CI: exclude
#Requires -Version 5.1
<#
.SYNOPSIS
    Capture Normal vs Alternate KryptonLabel colours for issue #4412.

.DESCRIPTION
    Hosts a small KryptonForm with NormalControl / AlternateControl / AlternatePanel
    labels plus a status strip, then saves a PrintWindow PNG via Save-UnitTestWindowPng.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-4412LabelAlternateScreenshot.ps1
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
if (-not $OutputPath)
{
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4412-label-alternate-status-strip-text-demo.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir))
{
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themes = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themes)
{
    [void][System.Reflection.Assembly]::LoadFrom($themes)
}

Initialize-UnitTestNativeInput
[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = New-Object Krypton.Toolkit.KryptonForm
$form.Text = '4412 LabelAlternate StatusStripText'
$form.ClientSize = New-Object System.Drawing.Size(520, 220)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(80, 80)

$strip = New-Object Krypton.Toolkit.KryptonStatusStrip
$strip.Dock = [System.Windows.Forms.DockStyle]::Bottom
[void]$strip.Items.Add('Status strip sample')

$panel = New-Object Krypton.Toolkit.KryptonPanel
$panel.Dock = [System.Windows.Forms.DockStyle]::Fill
$panel.Padding = New-Object System.Windows.Forms.Padding(12)

$lblN = New-Object Krypton.Toolkit.KryptonLabel
$lblN.Text = 'NormalControl label'
$lblN.LabelStyle = [Krypton.Toolkit.LabelStyle]::NormalControl
$lblN.Location = New-Object System.Drawing.Point(12, 20)
$lblN.AutoSize = $true

$lblA = New-Object Krypton.Toolkit.KryptonLabel
$lblA.Text = 'AlternateControl label (StatusStripText)'
$lblA.LabelStyle = [Krypton.Toolkit.LabelStyle]::AlternateControl
$lblA.Location = New-Object System.Drawing.Point(12, 56)
$lblA.AutoSize = $true

$lblP = New-Object Krypton.Toolkit.KryptonLabel
$lblP.Text = 'AlternatePanel label (StatusStripText)'
$lblP.LabelStyle = [Krypton.Toolkit.LabelStyle]::AlternatePanel
$lblP.Location = New-Object System.Drawing.Point(12, 92)
$lblP.AutoSize = $true

$hint = New-Object Krypton.Toolkit.KryptonLabel
$hint.Text = 'Alternate styles should match status-strip text colour.'
$hint.LabelStyle = [Krypton.Toolkit.LabelStyle]::NormalControl
$hint.Location = New-Object System.Drawing.Point(12, 128)
$hint.AutoSize = $true

$panel.Controls.AddRange(@($lblN, $lblA, $lblP, $hint))
$form.Controls.Add($panel)
$form.Controls.Add($strip)

$form.Show()
$form.TopMost = $true
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 500
[System.Windows.Forms.Application]::DoEvents()

# DrawToBitmap is more reliable than PrintWindow for layered KryptonForm chrome.
$form.Refresh()
[System.Windows.Forms.Application]::DoEvents()
$bmp = New-Object System.Drawing.Bitmap $form.Width, $form.Height
$form.DrawToBitmap($bmp, (New-Object System.Drawing.Rectangle 0, 0, $form.Width, $form.Height))
$bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()

$form.Close()
$form.Dispose()
Write-Host "Wrote $OutputPath"
