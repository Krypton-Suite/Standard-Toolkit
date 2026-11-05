<#
.SYNOPSIS
    Checks that Play music and the stereo file move the VU meter demo.

.DESCRIPTION
    Hosts KryptonVuMeterDemo, turns on Play music, and fails when the analog
    target stays at rest. Then plays the stereo file and fails when the analog
    and segmented meters do not follow different channels. Both loops play
    through the default speakers. Screenshot capture leaves playback off.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-VuMeterMusic.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Gauges.dll'))
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)
[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonVuMeterDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonVuMeterDemo was not found.'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$form = [System.Activator]::CreateInstance($formType)
try {
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 40, 40
    $form.Show()
    [System.Windows.Forms.Application]::DoEvents()

    $length = $formType.GetField('kcmbLength', $flags).GetValue($form)
    $length.SelectedIndex = 0
    $music = $formType.GetField('kchkMusic', $flags).GetValue($form)
    $analog = $formType.GetField('kvumAnalog', $flags).GetValue($form)
    $label = $formType.GetField('klblLevel', $flags).GetValue($form)
    $music.Checked = $true
    [System.Windows.Forms.Application]::DoEvents()
    if (-not $music.Checked) {
        throw "Play music did not stay on. $($label.Values.Text)"
    }

    $peak = [double]$analog.Value
    $deadline = [DateTime]::UtcNow.AddSeconds(7)
    while ([DateTime]::UtcNow -lt $deadline) {
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 40
        $level = [double]$analog.Value
        if ($level -gt $peak) {
            $peak = $level
        }
    }

    if ($peak -lt -1) {
        throw "Play music left the analog meter at $peak VU."
    }

    Write-Host "Play music peak target $peak VU"
    $music.Checked = $false
    [System.Windows.Forms.Application]::DoEvents()

    $stereo = $formType.GetField('kchkStereo', $flags).GetValue($form)
    $leftMeter = $formType.GetField('kvumLeft', $flags).GetValue($form)
    $rightMeter = $formType.GetField('kvumRight', $flags).GetValue($form)
    $stereo.Checked = $true
    [System.Windows.Forms.Application]::DoEvents()
    if (-not $stereo.Checked) {
        throw "Stereo file did not stay on. $($label.Values.Text)"
    }

    $leftPeak = [double]$leftMeter.Value
    $rightPeak = [double]$rightMeter.Value
    $maxGap = 0.0
    $deadline = [DateTime]::UtcNow.AddSeconds(7)
    while ([DateTime]::UtcNow -lt $deadline) {
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 40
        $left = [double]$leftMeter.Value
        $right = [double]$rightMeter.Value
        if ($left -gt $leftPeak) {
            $leftPeak = $left
        }

        if ($right -gt $rightPeak) {
            $rightPeak = $right
        }

        $gap = [Math]::Abs($left - $right)
        if ($gap -gt $maxGap) {
            $maxGap = $gap
        }
    }

    if ($leftPeak -lt -1 -or $rightPeak -lt -1 -or $maxGap -lt 10) {
        throw "Stereo file left=$leftPeak right=$rightPeak gap=$maxGap."
    }

    Write-Host "Stereo file left $leftPeak right $rightPeak gap $maxGap VU"
    $stereo.Checked = $false
    [System.Windows.Forms.Application]::DoEvents()
}
finally {
    $form.Close()
    $form.Dispose()
}
