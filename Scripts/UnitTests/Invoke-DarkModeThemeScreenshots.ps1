<#
.SYNOPSIS
    Captures ControlsTest under several dark palettes.

.DESCRIPTION
    Loads TestForm in-process (STA), applies each PaletteMode, and writes a PrintWindow PNG
    next to the local PR description. Office glass and family colours are the point of the shots.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-DarkModeThemeScreenshots.ps1 -TargetFramework net8.0-windows
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net8.0-windows',
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

$themes = @(
    @{ Mode = 'Office2010BlackDarkMode'; File = '4483-dark-office2010-black.png' },
    @{ Mode = 'VisualStudio2022Dark'; File = '4483-dark-visualstudio2022.png' },
    @{ Mode = 'Office2007BlueDarkMode'; File = '4483-dark-office2007-blue.png' },
    @{ Mode = 'Office2007SilverDarkMode'; File = '4483-dark-office2007-silver.png' },
    @{ Mode = 'SparkleBlueDarkMode'; File = '4483-dark-sparkle-blue.png' },
    @{ Mode = 'MaterialDark'; File = '4483-dark-material.png' },
    @{ Mode = 'MacOSDark'; File = '4483-dark-macos.png' }
)

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
Initialize-UnitTestCaptureNative
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))
[System.Windows.Forms.Application]::EnableVisualStyles()

$formType = $asm.GetType('TestForm.ControlsTest')
if (-not $formType) {
    throw 'Type TestForm.ControlsTest was not found in TestForm.exe.'
}

$paletteType = [Krypton.Toolkit.PaletteMode]
$manager = New-Object Krypton.Toolkit.KryptonManager
foreach ($theme in $themes) {
    $mode = [Enum]::Parse($paletteType, $theme.Mode)
    $manager.GlobalPaletteMode = $mode
    $form = [System.Activator]::CreateInstance($formType)
    $manager.GlobalPaletteMode = $mode
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 40, 40
    $form.TopMost = $true
    $form.Show()
    $form.Refresh()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    for ($i = 0; $i -lt 15; $i++) {
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 40
    }
    $path = Join-Path $OutputDirectory $theme.File
    $hwnd = $form.Handle
    $rect = New-Object UnitTestCaptureNative+RECT
    if (-not [UnitTestCaptureNative]::GetWindowRect($hwnd, [ref]$rect)) {
        throw 'GetWindowRect failed.'
    }

    $width = $rect.Right - $rect.Left
    $height = $rect.Bottom - $rect.Top
    $bmp = New-Object System.Drawing.Bitmap $width, $height
    $graphics = [System.Drawing.Graphics]::FromImage($bmp)
    $hdc = $graphics.GetHdc()
    try {
        [void][UnitTestCaptureNative]::PrintWindow($hwnd, $hdc, [UnitTestCaptureNative]::PW_RENDERFULLCONTENT)
    }
    finally {
        $graphics.ReleaseHdc($hdc)
        $graphics.Dispose()
    }

    $probe = $bmp.GetPixel(200, 200)
    $bmp.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Output ("{0} probe={1},{2},{3}" -f $path, $probe.R, $probe.G, $probe.B)
    $form.Close()
    $form.Dispose()
    [System.Windows.Forms.Application]::DoEvents()
}
