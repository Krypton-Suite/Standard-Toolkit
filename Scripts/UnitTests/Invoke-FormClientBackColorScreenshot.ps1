<#
.SYNOPSIS
    Captures FormClientBackColorDemo light/dark PNGs for issue #4685.

.DESCRIPTION
    Hosts TestForm.FormClientBackColorDemo under Microsoft365Blue and
    Microsoft365BlackDarkMode with manager PanelAlternate (form Inherit).
    Writes Documents/PR/4685-form-client-backcolor-*.png via Save-UnitTestWindowPng.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-FormClientBackColorScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net48',
    [string]$BinDir,
    [string]$OutDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutDir) {
    $OutDir = Join-Path $repoRoot 'Documents\PR'
}
if (-not (Test-Path -LiteralPath $OutDir)) {
    New-Item -ItemType Directory -Path $OutDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesPath = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesPath) {
    [void][System.Reflection.Assembly]::LoadFrom($themesPath)
}
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))
[void][Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()

[System.Windows.Forms.Application]::EnableVisualStyles()

$manager = New-Object Krypton.Toolkit.KryptonManager
$modes = @(
    @{ Name = 'light'; Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue },
    @{ Name = 'dark'; Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode }
)

foreach ($entry in $modes) {
    $manager.GlobalPaletteMode = $entry.Mode
    [Krypton.Toolkit.KryptonManager]::UseKryptonColorsOnForm = $true
    [Krypton.Toolkit.KryptonManager]::PanelStyle = [Krypton.Toolkit.PaletteBackStyle]::PanelClient

    $form = New-Object TestForm.FormClientBackColorDemo
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400

    $defaultPath = Join-Path $OutDir ("4685-form-client-backcolor-default-{0}.png" -f $entry.Name)
    Save-UnitTestWindowPng -Form $form -Path $defaultPath -SettleMs 300
    Write-Output "Wrote $defaultPath"

    # Manager PanelAlternate while form stays Inherit (index 1 on manager combo, index 0 on form combo)
    $mgrCombo = $form.Controls.Find('kcmbManagerPanelStyle', $true) | Select-Object -First 1
    $formCombo = $form.Controls.Find('kcmbPanelStyle', $true) | Select-Object -First 1
    if ($null -eq $mgrCombo -or $null -eq $formCombo) {
        throw 'Panel style combos not found on FormClientBackColorDemo.'
    }
    $formCombo.SelectedIndex = 0
    $mgrCombo.SelectedIndex = 1
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 250

    $altPath = Join-Path $OutDir ("4685-form-client-backcolor-alternate-{0}.png" -f $entry.Name)
    Save-UnitTestWindowPng -Form $form -Path $altPath -SettleMs 300
    Write-Output "Wrote $altPath"

    $form.Close()
    $form.Dispose()
}

Write-Output 'Done.'