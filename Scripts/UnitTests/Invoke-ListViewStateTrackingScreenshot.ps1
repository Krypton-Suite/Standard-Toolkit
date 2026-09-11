<#
.SYNOPSIS
    Hosts Bug4336ListViewStateTrackingDemo and captures hover PNGs for issue #4336.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows the #4336 demo, moves the cursor
    onto a KryptonListView row, and writes Documents/PR/4336-listview-statetracking-*.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ListViewStateTrackingScreenshot.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Bug4336ListViewStateTrackingDemo')
if (-not $formType) {
    throw 'Type TestForm.Bug4336ListViewStateTrackingDemo was not found in TestForm.exe.'
}

function Save-FormShot {
    param(
        [System.Windows.Forms.Form]$Form,
        [string]$Path,
        [int]$InflateX = 0,
        [int]$InflateY = 0
    )

    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    [System.Windows.Forms.Application]::DoEvents()
    $bounds = $Form.Bounds
    $x = [Math]::Max(0, $bounds.X - $InflateX)
    $y = [Math]::Max(0, $bounds.Y - $InflateY)
    $w = $bounds.Width + (2 * $InflateX)
    $h = $bounds.Height + (2 * $InflateY)
    $capture = [System.Drawing.Rectangle]::new($x, $y, $w, $h)
    $bmp = New-Object System.Drawing.Bitmap $capture.Width, $capture.Height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    try {
        $g.CopyFromScreen($capture.Location, [System.Drawing.Point]::Empty, $capture.Size)
        $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $g.Dispose()
        $bmp.Dispose()
    }

    Write-Host "Wrote $Path"
}

function Move-CursorOntoFirstKryptonItem {
    param($Form)

    $klvField = $formType.GetField('klvKrypton', [System.Reflection.BindingFlags]'Instance,NonPublic')
    $klv = $klvField.GetValue($Form)
    $inner = $klv.ListView
    if ($inner.Items.Count -lt 1) {
        throw 'KryptonListView has no items to hover.'
    }

    $itemIndex = [Math]::Min(2, $inner.Items.Count - 1)
    $rect = $inner.GetItemRect($itemIndex)
    if ($rect.Width -le 0 -or $rect.Height -le 0) {
        throw "KryptonListView item $itemIndex has an empty rectangle."
    }

    $client = New-Object System.Drawing.Point ([int]($rect.X + 40), [int]($rect.Y + ($rect.Height / 2)))
    $screen = $inner.PointToScreen($client)
    [void][UnitTestNative]::SetForegroundWindow($Form.Handle)
    [void][UnitTestNative]::SetCursorPos($screen.X, $screen.Y)
    # SetCursorPos alone may not raise WM_MOUSEMOVE; a 1-pixel move does.
    [UnitTestNative]::mouse_event(0x0001, 1, 0, 0, [IntPtr]::Zero)
    [void][UnitTestNative]::SetCursorPos($screen.X, $screen.Y)
    [System.Windows.Forms.Application]::DoEvents()
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 700
[System.Windows.Forms.Application]::DoEvents()

Move-CursorOntoFirstKryptonItem -Form $form
Save-FormShot -Form $form -Path (Join-Path $OutputDir '4336-listview-statetracking-default.png')

# Wait for KryptonToolTip show delay (500ms) plus paint.
Start-Sleep -Milliseconds 800
[System.Windows.Forms.Application]::DoEvents()
Save-FormShot -Form $form -Path (Join-Path $OutputDir '4336-listview-statetracking-tooltip.png') -InflateX 48 -InflateY 48

$contrastField = $formType.GetField('kchkContrastTracking', [System.Reflection.BindingFlags]'Instance,NonPublic')
$contrast = $contrastField.GetValue($form)
$contrast.Checked = $true
[System.Windows.Forms.Application]::DoEvents()
Move-CursorOntoFirstKryptonItem -Form $form
Save-FormShot -Form $form -Path (Join-Path $OutputDir '4336-listview-statetracking-orange.png')

$form.Close()
$form.Dispose()
