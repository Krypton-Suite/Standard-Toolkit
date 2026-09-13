<#
.SYNOPSIS
    Shows a basic toast with and without a close box and captures PNGs for #4419.

.DESCRIPTION
    Loads Krypton.Toolkit.Utilities in-process (STA), shows two short-lived basic toasts
    (no close box / with close box), and writes Documents/PR/4419-toast-dpi-*.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ToastDpiScreenshot.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

[System.Windows.Forms.Application]::EnableVisualStyles()

function Capture-Toast {
    param(
        [bool]$ShowCloseBox,
        [string]$OutputPath
    )

    $dataType = [Krypton.Toolkit.Utilities.KryptonBasicToastData]
    $data = [Activator]::CreateInstance($dataType)
    $data.NotificationTitle = if ($ShowCloseBox) { 'Toast with close box' } else { 'Borderless toast (no close box)' }
    $data.NotificationContent = 'DPI scaling check for screen-edge inset and height padding (#4419).'
    $data.CountDownSeconds = 8
    $data.ShowCloseBox = $ShowCloseBox
    $data.TopMost = $true
    $data.NotificationIcon = [Krypton.Toolkit.Utilities.KryptonToastIcon]::Information

    [Krypton.Toolkit.Utilities.KryptonToast]::ShowBasicNotification($data)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 800
    [System.Windows.Forms.Application]::DoEvents()

    $toast = [System.Windows.Forms.Application]::OpenForms | Where-Object {
        $_.GetType().Name -like 'VisualToastBasic*'
    } | Select-Object -First 1

    if (-not $toast) {
        throw "Toast form was not found (ShowCloseBox=$ShowCloseBox)."
    }

    $bounds = $toast.Bounds
    $bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
    $g.Dispose()
    $bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "Wrote $OutputPath"

    $toast.Close()
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200
}

$hostForm = New-Object System.Windows.Forms.Form
$hostForm.Text = 'Toast DPI capture host'
$hostForm.StartPosition = 'Manual'
$hostForm.Location = New-Object System.Drawing.Point 40, 40
$hostForm.Size = New-Object System.Drawing.Size 320, 120
$hostForm.Show()
$hostForm.Activate()
[System.Windows.Forms.Application]::DoEvents()

Capture-Toast -ShowCloseBox $false -OutputPath (Join-Path $OutputDirectory '4419-toast-dpi-borderless.png')
Capture-Toast -ShowCloseBox $true -OutputPath (Join-Path $OutputDirectory '4419-toast-dpi-closebox.png')

$hostForm.Close()
$hostForm.Dispose()
