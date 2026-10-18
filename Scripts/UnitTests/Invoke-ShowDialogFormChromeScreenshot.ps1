# UnitTest-CI: exclude
#Requires -Version 5.1
<#
.SYNOPSIS
    Hosts Bug4584KryptonFormShowDialogFlickerDemo and writes PNG/GIF PR assets.

.DESCRIPTION
    Loads Debug TestForm in-process (STA). Captures the host, a Sizable sample dialog
    with Krypton chrome, and a short open GIF (PrintWindow frames). Assets are local
    under Documents/PR/ and are not committed.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ShowDialogFormChromeScreenshot.ps1
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
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$working = [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea
$hostForm = New-Object TestForm.Bug4584KryptonFormShowDialogFlickerDemo
try {
    $hostForm.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $hostForm.Location = New-Object System.Drawing.Point ($working.Left + 80), ($working.Top + 80)
    $hostForm.Show()
    $hostForm.TopMost = $true
    $hostForm.Activate()
    $hostForm.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($hostForm.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400

    $hostPng = Join-Path $OutputDir '4584-showdialog-flicker-host.png'
    Save-UnitTestWindowPng -Form $hostForm -Path $hostPng -SettleMs 200
    Write-Host "Wrote $hostPng"

    $dialog = New-Object Krypton.Toolkit.KryptonForm
    try {
        $dialog.Text = 'Sample Sizable dialog (#4584)'
        $dialog.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::Sizable
        $dialog.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $dialog.Size = New-Object System.Drawing.Size 420, 220
        $dialog.ShowInTaskbar = $false
        $dialog.Owner = $hostForm
        $dialogX = $hostForm.Left + [int](($hostForm.Width - 420) / 2)
        $dialogY = $hostForm.Top + [int](($hostForm.Height - 220) / 2)
        $dialog.Location = New-Object System.Drawing.Point $dialogX, $dialogY

        $lbl = New-Object Krypton.Toolkit.KryptonWrapLabel
        $lbl.Dock = [System.Windows.Forms.DockStyle]::Fill
        $lbl.Text = 'Krypton chrome must be present from the first visible frame (issue #4584).'
        $dialog.Controls.Add($lbl)

        $gifDir = Join-Path $env:TEMP ('krypton-4584-gif-' + [Guid]::NewGuid().ToString('N'))
        New-Item -ItemType Directory -Path $gifDir | Out-Null
        $gifFrames = New-Object System.Collections.Generic.List[string]

        # Capture host, then dialog as it becomes visible (non-modal so we can frame-grab).
        $prePath = Join-Path $gifDir 'frame000.png'
        Save-UnitTestWindowPng -Form $hostForm -Path $prePath -SettleMs 50
        $gifFrames.Add($prePath)

        $dialog.Show($hostForm)
        $dialog.TopMost = $true
        $dialog.Activate()
        $dialog.BringToFront()
        [void][UnitTestNative]::SetForegroundWindow($dialog.Handle)
        [System.Windows.Forms.Application]::DoEvents()

        foreach ($i in 1..6) {
            Start-Sleep -Milliseconds 40
            [System.Windows.Forms.Application]::DoEvents()
            $framePath = Join-Path $gifDir ('frame{0:D3}.png' -f $i)
            Save-UnitTestWindowPng -Form $dialog -Path $framePath -SettleMs 0
            $gifFrames.Add($framePath)
        }

        $dialogPng = Join-Path $OutputDir '4584-showdialog-flicker-sizable.png'
        Save-UnitTestWindowPng -Form $dialog -Path $dialogPng -SettleMs 200
        Write-Host "Wrote $dialogPng"

        $gifPath = Join-Path $OutputDir '4584-showdialog-flicker-open.gif'
        try {
            Add-Type -AssemblyName PresentationCore
            $encoder = New-Object System.Windows.Media.Imaging.GifBitmapEncoder
            foreach ($framePath in $gifFrames) {
                $bytes = [System.IO.File]::ReadAllBytes($framePath)
                $ms = New-Object System.IO.MemoryStream
                $ms.Write($bytes, 0, $bytes.Length)
                $ms.Position = 0
                $decoder = [System.Windows.Media.Imaging.BitmapDecoder]::Create(
                    $ms,
                    [System.Windows.Media.Imaging.BitmapCreateOptions]::PreservePixelFormat,
                    [System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
                $meta = New-Object System.Windows.Media.Imaging.BitmapMetadata 'gif'
                $meta.SetQuery('/grctlext/Delay', [uint16]12)
                $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($decoder.Frames[0], $null, $meta, $null))
                $ms.Dispose()
            }
            $gifStream = [System.IO.File]::Open($gifPath, [System.IO.FileMode]::Create)
            $encoder.Save($gifStream)
            $gifStream.Dispose()
            Write-Host "Wrote $gifPath"
        }
        catch {
            Write-Warning "GIF encode failed: $($_.Exception.Message)"
        }
        finally {
            Remove-Item -LiteralPath $gifDir -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
    finally {
        $dialog.Close()
        $dialog.Dispose()
    }
}
finally {
    $hostForm.Close()
    $hostForm.Dispose()
}
