<#
.SYNOPSIS
    Hosts KryptonEqualizerDemo and captures light/dark stills for the PR draft.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), shows the equalizer demo,
    and writes Documents/PR/4660-krypton-equalizer-*-light.png and *-dark.png via PrintWindow.
    Local PR assets only; do not commit them.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-EqualizerScreenshot.ps1
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
$themesPath = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesPath) {
    [void][System.Reflection.Assembly]::LoadFrom($themesPath)
}

$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)
[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonEqualizerDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonEqualizerDemo was not found.'
}

$paletteType = [Krypton.Toolkit.PaletteMode]
$manager = New-Object Krypton.Toolkit.KryptonManager
$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
[void][Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()

function Set-DemoLevels {
    param($Form, $FormType)
    $bars = $FormType.GetField('keqBars', $flags).GetValue($Form)
    $segments = $FormType.GetField('keqSegments', $flags).GetValue($Form)
    $levels = @(0.25, 0.4, 0.55, 0.7, 0.85, 0.65, 0.5, 0.35, 0.45, 0.3)
    $bars.SetLevels([double[]]$levels)
    $segments.SetLevels([double[]]$levels)
    $bars.Values.PeakHold = $true
    $segments.Values.PeakHold = $true
    $bars.Values.Bands[2].Gain = 4.0
    $bars.Values.Bands[6].Gain = -3.0
}

function Capture-Theme {
    param(
        [string]$ModeName,
        [string]$Suffix
    )

    $mode = [Enum]::Parse($paletteType, $ModeName)
    $manager.GlobalPaletteMode = $mode
    $form = [System.Activator]::CreateInstance($formType)
    try {
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = New-Object System.Drawing.Point 60, 60
        $form.TopMost = $true
        $form.Show()
        $form.Activate()
        $form.BringToFront()
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 350
        Set-DemoLevels -Form $form -FormType $formType
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 250
        $path = Join-Path $OutputDirectory ("4660-krypton-equalizer-default-$Suffix.png")
        Save-UnitTestWindowPng -Form $form -Path $path -SettleMs 200
        Write-Host "Wrote $path"
    }
    finally {
        $form.Close()
        $form.Dispose()
    }
}

Capture-Theme -ModeName 'Microsoft365Blue' -Suffix 'light'
Capture-Theme -ModeName 'Microsoft365BlackDarkMode' -Suffix 'dark'
Write-Host 'Equalizer screenshots complete.'
