<#
.SYNOPSIS
    Hosts KryptonAnalogueGaugeDemo and captures light/dark stills for the local PR draft.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), shows the analogue gauge demo under
    Microsoft 365 Blue and Microsoft 365 Black Dark Mode, and writes
    Documents/PR/4656-analogue-gauges-default-light.png and …-dark.png via PrintWindow.
    Loads Krypton.Themes.dll for the dark extra palette. Local PR assets only; do not commit them.

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
if (-not (Test-Path -LiteralPath $OutputDirectory)) {
    New-Item -ItemType Directory -Path $OutputDirectory | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
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

$shots = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue; File = '4656-analogue-gauges-default-light.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode; File = '4656-analogue-gauges-default-dark.png' }
)

$manager = New-Object Krypton.Toolkit.KryptonManager
try {
    foreach ($shot in $shots) {
        $manager.GlobalPaletteMode = $shot.Mode
        $form = [System.Activator]::CreateInstance($formType)
        try {
            $manager.GlobalPaletteMode = $shot.Mode
            $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
            $form.Location = New-Object System.Drawing.Point 40, 40
            $form.TopMost = $true
            $form.Show()
            $form.Refresh()
            $form.Activate()
            $form.BringToFront()
            [void][UnitTestNative]::SetForegroundWindow($form.Handle)
            [System.Windows.Forms.Application]::DoEvents()
            Start-Sleep -Milliseconds 500
            [System.Windows.Forms.Application]::DoEvents()

            $outPath = Join-Path $OutputDirectory $shot.File
            Save-UnitTestWindowPng -Form $form -Path $outPath -SettleMs 250
            Write-Output $outPath
        }
        finally {
            if ($form -ne $null) {
                $form.Close()
                $form.Dispose()
            }
            [System.Windows.Forms.Application]::DoEvents()
        }
    }
}
finally {
    $manager.Dispose()
}
