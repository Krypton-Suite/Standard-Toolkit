<#
.SYNOPSIS
    Hosts Office2024RibbonDemo and captures the Office 2024 themes plus Microsoft 365 Blue.

.DESCRIPTION
    Loads TestForm in-process (STA) and writes Documents/PR/4496-office-2024-*.png
    via Save-UnitTestWindowPng (PrintWindow).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-Office2024RibbonScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDir) {
    $OutputDir = Join-Path $repoRoot 'Documents\PR'
}
if (-not (Test-Path -LiteralPath $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
$toolkit = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Office2024RibbonDemo')
if (-not $formType) {
    throw 'Type TestForm.Office2024RibbonDemo was not found in TestForm.exe.'
}

$modeType = $toolkit.GetType('Krypton.Toolkit.PaletteMode')
$apply = $formType.GetMethod('ApplyTheme')
$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()

$captures = @(
    @{ Mode = 'Office2024Blue'; File = '4496-office-2024-blue.png' },
    @{ Mode = 'Office2024BlueDarkMode'; File = '4496-office-2024-blue-dark.png' },
    @{ Mode = 'Office2024BlueLightMode'; File = '4496-office-2024-blue-light.png' },
    @{ Mode = 'Office2024Silver'; File = '4496-office-2024-silver.png' },
    @{ Mode = 'Office2024SilverDarkMode'; File = '4496-office-2024-silver-dark.png' },
    @{ Mode = 'Office2024SilverLightMode'; File = '4496-office-2024-silver-light.png' },
    @{ Mode = 'Office2024White'; File = '4496-office-2024-white.png' },
    @{ Mode = 'Office2024LightGray'; File = '4496-office-2024-light-gray.png' },
    @{ Mode = 'Office2024DarkGray'; File = '4496-office-2024-dark-gray.png' },
    @{ Mode = 'Office2024Black'; File = '4496-office-2024-black.png' },
    @{ Mode = 'Office2024BlackDarkMode'; File = '4496-office-2024-black-dark.png' },
    @{ Mode = 'Office2024BlackDarkModeAlternate'; File = '4496-office-2024-black-dark-alternate.png' },
    @{ Mode = 'Microsoft365Blue'; File = '4496-office-2024-microsoft365-blue.png' }
)

foreach ($capture in $captures) {
    $mode = [Enum]::Parse($modeType, $capture.Mode)
    [void]$apply.Invoke($form, @($mode))
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 300
    $path = Join-Path $OutputDir $capture.File
    Save-UnitTestWindowPng -Form $form -Path $path
    Write-Host "Wrote $path"
}

$form.Close()
$form.Dispose()
