<#
.SYNOPSIS
    Hosts TransferProgressPanelDemo in DualSeries layout and captures light/dark stills.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), seeds speed/progress samples on the
    dual-series chart, and writes Documents/PR/4669-transfer-dual-graph-*-light.png and
    *-dark.png via PrintWindow. Local PR assets only; do not commit them.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-TransferDualGraphScreenshot.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
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
$formType = $asm.GetType('TestForm.TransferProgressPanelDemo')
if (-not $formType) {
    throw 'Type TestForm.TransferProgressPanelDemo was not found.'
}

$paletteType = [Krypton.Toolkit.PaletteMode]
$layoutType = [Krypton.Toolkit.Utilities.TransferProgressChartLayout]
$manager = New-Object Krypton.Toolkit.KryptonManager
$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
[void][Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()

function Seed-DualSamples {
    param($Form, $FormType)

    $panel = $FormType.GetField('transferProgressPanel', $flags).GetValue($Form)
    $layoutCombo = $FormType.GetField('kcmbLayout', $flags).GetValue($Form)
    $status = $FormType.GetField('kwlblStatus', $flags).GetValue($Form)
    $dual = [Enum]::Parse($layoutType, 'DualSeries')
    $panel.ChartLayout = $dual
    if ($null -ne $layoutCombo) {
        $layoutCombo.SelectedItem = $dual
    }
    $panel.ShowProgressGraph = $true
    $panel.Clear()
    $panel.Resume()

    [long]$received = 0
    [long]$total = 10000000
    $rand = New-Object System.Random 42
    for ($i = 0; $i -lt 40; $i++) {
        $delta = 180000 + $rand.Next(0, 80000)
        $received += $delta
        if ($received -gt $total) { $received = $total }
        $panel.SetProgress($received, $total)
        $panel.AddSample([double]($delta / 0.28))
    }

    if ($null -ne $status) {
        $status.Text = ("Received {0:N0} of {1:N0} bytes. Layout={2}; Mode={3}; ShowProgress={4}; Theme={5}." -f `
            $received, $total, $panel.ChartLayout, $panel.ProgressSampleMode, $panel.ShowProgressGraph, [Krypton.Toolkit.KryptonManager]::CurrentGlobalPaletteMode)
    }

    [System.Windows.Forms.Application]::DoEvents()
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
        Seed-DualSamples -Form $form -FormType $formType
        Start-Sleep -Milliseconds 250
        $path = Join-Path $OutputDirectory ("4669-transfer-dual-graph-default-$Suffix.png")
        Save-UnitTestWindowPng -Form $form -Path $path -SettleMs 200
        Write-Host "Wrote $path"
    }
    finally {
        $form.Close()
        $form.Dispose()
    }
}

if (-not (Test-Path -LiteralPath $OutputDirectory)) {
    New-Item -ItemType Directory -Path $OutputDirectory | Out-Null
}

Capture-Theme -ModeName 'Microsoft365Blue' -Suffix 'light'
Capture-Theme -ModeName 'Microsoft365BlackDarkMode' -Suffix 'dark'
Write-Host 'Transfer dual-graph screenshots complete.'
