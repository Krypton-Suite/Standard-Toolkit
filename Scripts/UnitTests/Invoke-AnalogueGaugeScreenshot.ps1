<#
.SYNOPSIS
    Hosts KryptonAnalogueGaugeDemo and captures a still for the local PR draft.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), shows the analogue gauge demo,
    and writes Documents/PR/4656-analogue-gauges-default.png via PrintWindow.
    Local PR assets only; do not commit them.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-AnalogueGaugeScreenshot.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Gauges.dll'))
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)
[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonAnalogueGaugeDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonAnalogueGaugeDemo was not found in TestForm.'
}

$form = [System.Activator]::CreateInstance($formType)
try {
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 40, 40
    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500
    [System.Windows.Forms.Application]::DoEvents()

    $outPath = Join-Path $OutputDirectory '4656-analogue-gauges-default.png'
    Save-UnitTestWindowPng -Form $form -Path $outPath -SettleMs 250
    Write-Output $outPath
}
finally {
    if ($form -ne $null) {
        $form.Close()
        $form.Dispose()
    }
}
