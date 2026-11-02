<#
.SYNOPSIS
    Hosts CaptionTextBoxDemo and captures the caption editor.

.DESCRIPTION
    Loads TestForm in-process (STA) and writes a PNG of the Office 2024 caption with a button
    on the left, a combo in the centre, and the search box and text box on the right. The editors are owned windows, so the capture
    inflates by a few pixels and uses CopyFromScreen inside Save-UnitTestWindowPng.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-CaptionTextBoxScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDirectory
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path $repoRoot 'Documents\PR'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.CaptionTextBoxDemo')
if (-not $formType) {
    throw 'Type TestForm.CaptionTextBoxDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
for ($i = 0; $i -lt 8; $i++) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
}

$eachPath = Join-Path $OutputDirectory '4550-caption-each-edge.png'
Save-UnitTestWindowPng -Form $form -Path $eachPath -InflateX 4 -InflateY 4 -SettleMs 300

$form.Close()
$form.Dispose()
Write-Output $eachPath
