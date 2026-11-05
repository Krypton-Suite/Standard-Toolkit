<#
.SYNOPSIS
    Times Krypton startup through the first KryptonForm Shown event (#4249).

.DESCRIPTION
    Loads Krypton.Toolkit from the Debug output folder and reports milliseconds for the first
    touch of KryptonManager (type init, including the default palette) and for constructing a
    small form until Shown. Reports whether Krypton.Themes.dll was loaded at init, inside Shown,
    and after one idle pump. Pass -PaletteMode to switch the global theme after init and before
    the form is shown, and to print that switch time. Machine-dependent; not a CI assert.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-StartupTiming.ps1

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-StartupTiming.ps1 -PaletteMode Office2007Blue
#>
# UnitTest-CI: exclude
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$PaletteMode
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

$toolkitPath = Join-Path $bin 'Krypton.Toolkit.dll'
if (-not (Test-Path -LiteralPath $toolkitPath)) {
    throw "Krypton.Toolkit.dll not found in $bin. Build TestForm first."
}

# LoadFrom keeps Assembly.Location so Krypton.Resources.dll resolves beside the toolkit.
[void][System.Reflection.Assembly]::LoadFrom($toolkitPath)

function Test-ThemesAssemblyLoaded {
    foreach ($assembly in [System.AppDomain]::CurrentDomain.GetAssemblies()) {
        if ($assembly.GetName().Name -eq 'Krypton.Themes') {
            return $true
        }
    }

    return $false
}

$init = [System.Diagnostics.Stopwatch]::StartNew()
$null = [Krypton.Toolkit.KryptonManager]::CurrentGlobalPalette
$init.Stop()
$themesAfterInit = Test-ThemesAssemblyLoaded

$themeSwitchMs = 0
$paletteIsCore = $true
if ($PaletteMode) {
    $mode = [Krypton.Toolkit.PaletteMode]::$PaletteMode
    $paletteIsCore = [Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($mode)
    $manager = New-Object Krypton.Toolkit.KryptonManager
    $switch = [System.Diagnostics.Stopwatch]::StartNew()
    $manager.GlobalPaletteMode = $mode
    $switch.Stop()
    $themeSwitchMs = $switch.ElapsedMilliseconds
}

$form = New-Object Krypton.Toolkit.KryptonForm
$shown = $false
$form.add_Shown({
    $script:StartupFormShown = $true
    $script:ThemesLoadedAtShown = Test-ThemesAssemblyLoaded
})

$show = [System.Diagnostics.Stopwatch]::StartNew()
try {
    $form.Text = 'Startup timing'
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.ClientSize = New-Object System.Drawing.Size 480, 240
    $form.ShowInTaskbar = $false

    $label = New-Object Krypton.Toolkit.KryptonLabel
    $label.Text = 'Startup'
    $label.Location = New-Object System.Drawing.Point 16, 16
    $form.Controls.Add($label)

    $button = New-Object Krypton.Toolkit.KryptonButton
    $button.Text = 'OK'
    $button.Location = New-Object System.Drawing.Point 16, 48
    $form.Controls.Add($button)

    $form.Show()
    $guard = [System.Diagnostics.Stopwatch]::StartNew()
    while (-not $script:StartupFormShown) {
        [System.Windows.Forms.Application]::DoEvents()
        if ($guard.ElapsedMilliseconds -gt 15000) {
            throw 'KryptonForm.Shown did not run within 15 seconds.'
        }
    }
}
finally {
    $show.Stop()
}

if (-not $script:StartupFormShown) {
    $form.Close()
    $form.Dispose()
    throw 'KryptonForm.Shown did not run.'
}

# DoEvents waits for Shown but does not raise Application.Idle.
# RaiseIdle is the same event the UI message loop raises once the queue is empty.
[System.Windows.Forms.Application]::RaiseIdle([System.EventArgs]::Empty)
$themesAfterIdle = Test-ThemesAssemblyLoaded

$form.Close()
$form.Dispose()
[System.Windows.Forms.Application]::DoEvents()

$themesDll = Join-Path $bin 'Krypton.Themes.dll'
Write-Host ("ManagerInitMs: {0}" -f $init.ElapsedMilliseconds)
if ($PaletteMode) {
    Write-Host ("PaletteMode: {0}" -f $PaletteMode)
    Write-Host ("ThemeSwitchMs: {0}" -f $themeSwitchMs)
}
Write-Host ("FormShownMs: {0}" -f $show.ElapsedMilliseconds)
Write-Host ("TotalMs: {0}" -f ($init.ElapsedMilliseconds + $show.ElapsedMilliseconds))
Write-Host ("ThemesLoadedAfterInit: {0}" -f $themesAfterInit)
Write-Host ("ThemesLoadedAtShown: {0}" -f $script:ThemesLoadedAtShown)
Write-Host ("ThemesLoadedAfterIdle: {0}" -f $themesAfterIdle)

# An extra PaletteMode loads Krypton.Themes.dll as soon as it is requested. A core mode must not.
if ($paletteIsCore -and ($themesAfterInit -or $script:ThemesLoadedAtShown)) {
    throw 'Krypton.Themes.dll loaded before the first form finished Shown.'
}

if ((Test-Path -LiteralPath $themesDll) -and -not $themesAfterIdle) {
    throw "Krypton.Themes.dll was not loaded after idle. Expected it beside the toolkit at $themesDll."
}

exit 0
