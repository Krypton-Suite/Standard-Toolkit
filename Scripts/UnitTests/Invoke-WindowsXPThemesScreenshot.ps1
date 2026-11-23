# UnitTest-CI: exclude
<#
.SYNOPSIS
    Captures Windows XP Luna Blue/Olive/Silver theme screenshots for PR drafts.
.DESCRIPTION
    Loads Debug TestForm and hosts WindowsXPThemesDemo for Luna Blue, Olive, and Silver.
    Saves PrintWindow PNGs under Documents/PR/, plus one Blue capture with the theme
    combo dropped open so the Luna highlight is visible.
.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-WindowsXPThemesScreenshot.ps1
#>
#Requires -Version 5.1
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')
$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput

foreach ($name in @('Krypton.Interop.dll', 'Krypton.Resources.dll', 'Krypton.Toolkit.dll', 'Krypton.Themes.dll')) {
    $path = Join-Path $bin $name
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Missing $path - build Krypton.Themes Debug $TargetFramework first."
    }
    [void][Reflection.Assembly]::LoadFrom($path)
}

$outDir = Join-Path $repoRoot 'Documents\PR'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

function Save-UnitTestComboPopupPng {
    param(
        [System.Windows.Forms.Form]$Form,
        [string]$Path
    )

    Initialize-UnitTestCaptureNative
    $rect = New-Object UnitTestCaptureNative+RECT
    if (-not [UnitTestCaptureNative]::GetWindowRect($Form.Handle, [ref]$rect)) {
        throw 'GetWindowRect failed.'
    }

    $width = $rect.Right - $rect.Left
    $height = $rect.Bottom - $rect.Top
    $bmp = New-Object System.Drawing.Bitmap $width, $height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    try {
        $g.CopyFromScreen($rect.Left, $rect.Top, 0, 0, (New-Object System.Drawing.Size($width, $height)))
    }
    finally {
        $g.Dispose()
    }

    $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "Wrote $Path"
}

$testFormPath = Join-Path $bin 'TestForm.exe'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    throw "Missing $testFormPath - build TestForm Debug $TargetFramework first."
}

[void][Reflection.Assembly]::LoadFrom($testFormPath)
$demoType = [Type]::GetType('TestForm.WindowsXPThemesDemo, TestForm', $true)
$comboField = $demoType.GetField('cmbTheme', [Reflection.BindingFlags]'Instance, NonPublic')
if ($null -eq $comboField) {
    throw 'WindowsXPThemesDemo.cmbTheme was not found.'
}

$modes = @(
    @{ File = 'windows-xp-luna-blue.png'; Index = 0; OpenDropDown = $false },
    @{ File = 'windows-xp-luna-olive.png'; Index = 1; OpenDropDown = $false },
    @{ File = 'windows-xp-luna-silver.png'; Index = 2; OpenDropDown = $false },
    @{ File = 'windows-xp-royale.png'; Index = 3; OpenDropDown = $false },
    @{ File = 'windows-xp-royale-noir.png'; Index = 4; OpenDropDown = $false },
    @{ File = 'windows-xp-zune.png'; Index = 5; OpenDropDown = $false },
    @{ File = 'windows-xp-luna-blue-dropdown.png'; Index = 0; OpenDropDown = $true }
)

[System.Windows.Forms.Application]::EnableVisualStyles()

foreach ($entry in $modes) {
    $form = [Activator]::CreateInstance($demoType)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point(80, 80)
    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 300

    $combo = $comboField.GetValue($form)
    $combo.SelectedIndex = $entry.Index
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200

    $out = Join-Path $outDir $entry.File
    if ($entry.OpenDropDown) {
        # Activate first. A later Activate closes the native combo list.
        $form.Activate()
        $form.BringToFront()
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 200
        [void]$combo.Focus()
        $combo.DroppedDown = $true
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 300
        Save-UnitTestComboPopupPng -Form $form -Path $out
    }
    else {
        Save-UnitTestWindowPng -Form $form -Path $out
    }

    Write-Host "Saved $out"
    if ($entry.OpenDropDown) {
        $combo.DroppedDown = $false
    }

    $form.Close()
    $form.Dispose()
}