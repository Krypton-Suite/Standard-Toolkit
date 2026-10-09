<#
.SYNOPSIS
    Hosts Office2024RibbonDemo and captures the Office 2024 themes, Microsoft 365 Blue, and a beveled Blue Dark group area.

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

$hoverMode = [Enum]::Parse($modeType, 'Office2024Black')
[void]$apply.Invoke($form, @($hoverMode))
[System.Windows.Forms.Application]::DoEvents()
$hoverPoint = $form.PointToScreen((New-Object System.Drawing.Point 168, 18))
[void][UnitTestNative]::SetCursorPos($hoverPoint.X, $hoverPoint.Y)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$hoverPath = Join-Path $OutputDir '4496-office-2024-black-hover.png'
Save-UnitTestWindowPng -Form $form -Path $hoverPath
Write-Host "Wrote $hoverPath"
$setMarker = $formType.GetMethod('SetTabMarker')
$markerType = $toolkit.GetType('Krypton.Toolkit.PaletteRibbonTabMarker')
$pill = [Enum]::Parse($markerType, 'Pill')
$line = [Enum]::Parse($markerType, 'Line')
[void]$setMarker.Invoke($form, @($pill))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$pillPath = Join-Path $OutputDir '4496-office-2024-black-pills.png'
Save-UnitTestWindowPng -Form $form -Path $pillPath
Write-Host "Wrote $pillPath"
[void]$setMarker.Invoke($form, @($line))
$away = $form.PointToScreen((New-Object System.Drawing.Point 40, 320))
[void][UnitTestNative]::SetCursorPos($away.X, $away.Y)
[System.Windows.Forms.Application]::DoEvents()

$setBevel = $formType.GetMethod('SetGroupAreaBevel')
$bevelMode = [Enum]::Parse($modeType, 'Office2024BlueDarkMode')
[void]$apply.Invoke($form, @($bevelMode))
[void]$setBevel.Invoke($form, @($true))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$bevelPath = Join-Path $OutputDir '4496-office-2024-blue-dark-bevel.png'
Save-UnitTestWindowPng -Form $form -Path $bevelPath
Write-Host "Wrote $bevelPath"

$setTitles = $formType.GetMethod('SetShowContextTitles')
[void]$setTitles.Invoke($form, @($true))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$titlePath = Join-Path $OutputDir '4496-office-2024-blue-dark-context-title.png'
Save-UnitTestWindowPng -Form $form -Path $titlePath
Write-Host "Wrote $titlePath"
[void]$setTitles.Invoke($form, @($false))
[System.Windows.Forms.Application]::DoEvents()

$setBevelSize = $formType.GetMethod('SetGroupAreaBevelSize')
[void]$setBevelSize.Invoke($form, @(8))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$widePath = Join-Path $OutputDir '4496-office-2024-blue-dark-bevel-wide.png'
Save-UnitTestWindowPng -Form $form -Path $widePath
Write-Host "Wrote $widePath"
[void]$setBevelSize.Invoke($form, @(2))
[void]$setBevel.Invoke($form, @($false))
$setContext = $formType.GetMethod('SelectContextualTab')
$contextMode = [Enum]::Parse($modeType, 'Office2024Black')
[void]$apply.Invoke($form, @($contextMode))
[void]$setContext.Invoke($form, @())
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$contextPath = Join-Path $OutputDir '4496-office-2024-black-context.png'
Save-UnitTestWindowPng -Form $form -Path $contextPath
Write-Host "Wrote $contextPath"

$gapMode = [Enum]::Parse($modeType, 'Office2024BlueDarkMode')
[void]$apply.Invoke($form, @($gapMode))
$setGap = $formType.GetMethod('SetGroupAreaGap')
[void]$setGap.Invoke($form, @(16))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$gapPath = Join-Path $OutputDir '4496-office-2024-blue-dark-gap.png'
Save-UnitTestWindowPng -Form $form -Path $gapPath
Write-Host "Wrote $gapPath"

$form.Close()
$form.Dispose()
