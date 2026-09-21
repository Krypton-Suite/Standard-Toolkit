<#
.SYNOPSIS
    Opens TextBoxInputModeDemo and captures a PNG for the #4417 PR description.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows TextBoxInputModeDemo with sample
    text in each InputMode box, captures the window, and writes
    Documents/PR/4417-textbox-input-mode-demo.png (or -OutputPath).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-TextBoxInputModeScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4417-textbox-input-mode-v105-demo.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.TextBoxInputModeDemo')
if (-not $formType) {
    throw 'Type TestForm.TextBoxInputModeDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 600

# Seed visible sample text via reflection on private designer fields.
$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$formType.GetField('ktbAny', $flags).GetValue($form).Text = 'Ab12-Xy!'
$formType.GetField('ktbDigits', $flags).GetValue($form).Text = '12345'
$formType.GetField('ktbLetters', $flags).GetValue($form).Text = 'Hello'
$formType.GetField('ktbAlphanumeric', $flags).GetValue($form).Text = 'Code42'
$formType.GetField('ktbLive', $flags).GetValue($form).Text = '99'
$formType.GetField('tbNative', $flags).GetValue($form).Text = 'Ab12-Xy! 99'
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400

$bounds = $form.Bounds
$bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
$g.Dispose()
$bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
$form.Close()
$form.Dispose()
Write-Host "Wrote $OutputPath"
