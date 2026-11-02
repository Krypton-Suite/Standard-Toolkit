<#
.SYNOPSIS
    Hosts ThemeCatalogDemo, applies a KryptonThemeListView hover preview, and captures stills.

.DESCRIPTION
    Loads Debug TestForm assemblies in-process (STA), shows ThemeCatalogDemo, applies
    Microsoft 365 - Black as a live hover preview, then restores the committed theme.
    Writes Documents/PR/3870-theme-listview-committed.png, -hover.png, and -restored.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ThemeListViewHoverScreenshot.ps1
#>
# UnitTest-CI: exclude
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
$prDir = Join-Path $repoRoot 'Documents\PR'
if (-not (Test-Path -LiteralPath $prDir)) {
    New-Item -ItemType Directory -Path $prDir | Out-Null
}

$committedPath = Join-Path $prDir '3870-theme-listview-committed.png'
$hoverPath = Join-Path $prDir '3870-theme-listview-hover.png'

Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

function Capture-WindowPng {
    param(
        [System.Windows.Forms.Form]$Form,
        [string]$Path
    )
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200
    [System.Windows.Forms.Application]::DoEvents()
    $bounds = $Form.Bounds
    $bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
    $g.Dispose()
    $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    return $bmp
}

function Move-Pointer {
    param([int]$X, [int]$Y)
    [void][UnitTestNative]::SetCursorPos($X, $Y)
    # SetCursorPos does not raise WM_MOUSEMOVE; jiggle one device pixel.
    [UnitTestNative]::mouse_event(0x0001, 1, 0, 0, [IntPtr]::Zero)
    [UnitTestNative]::mouse_event(0x0001, [uint32]::MaxValue, 0, 0, [IntPtr]::Zero)
    [System.Windows.Forms.Application]::DoEvents()
}

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.ThemeCatalogDemo')
if (-not $formType) {
    throw 'Type TestForm.ThemeCatalogDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 600
[System.Windows.Forms.Application]::DoEvents()

$listField = $formType.GetField('_themeListView', [System.Reflection.BindingFlags]'Instance,NonPublic')
$listView = $listField.GetValue($form).psobject.BaseObject
if ($listView.Items.Count -lt 2) {
    throw 'KryptonThemeListView has fewer than two themes.'
}

$hoverIndex = -1
for ($i = 0; $i -lt $listView.Items.Count; $i++) {
    if ($listView.Items[$i].Text -eq 'Microsoft 365 - Black') {
        $hoverIndex = $i
        break
    }
}
if ($hoverIndex -lt 0) {
    $hoverIndex = 0
    $selected = $listView.SelectedIndex
    if ($selected -eq 0) {
        $hoverIndex = 1
    }
}

$listView.Items[$hoverIndex].EnsureVisible()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
[System.Windows.Forms.Application]::DoEvents()

$inner = $listView.ListView
$itemRect = $listView.GetItemRect($hoverIndex)
$itemCentre = New-Object System.Drawing.Point (
    [int]($itemRect.X + ($itemRect.Width / 2)),
    [int]($itemRect.Y + ($itemRect.Height / 2)))
$screenPt = $inner.PointToScreen($itemCentre)

$committedMode = [Krypton.Toolkit.KryptonManager]::CurrentGlobalPaletteMode
Write-Host "Committed palette: $committedMode selected=$($listView.SelectedIndex)"
$committedBmp = Capture-WindowPng -Form $form -Path $committedPath
Write-Host "Wrote $committedPath"

$itemName = [string]$listView.Items[$hoverIndex].Text
Write-Host "Hover target index=$hoverIndex name=$itemName"
$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$core = $listView.GetType().GetMethod('ApplyThemeNameCore', $flags)
$capture = $listView.GetType().GetMethod('CaptureCommittedTheme', $flags)
$liveField = $listView.GetType().GetField('_livePreviewing', $flags)
$appliedField = $listView.GetType().GetField('_appliedHoverName', $flags)
$localField = $listView.GetType().GetField('_isLocalUpdate', $flags)
$timerField = $listView.GetType().GetField('_hoverTimer', $flags)
[void]$capture.Invoke($listView, $null)
$localField.SetValue($listView, $true)
try {
    $applied = $core.Invoke($listView, [object[]](, [string]$itemName))
    Write-Host "ApplyThemeNameCore returned $applied mode=$([Krypton.Toolkit.KryptonManager]::CurrentGlobalPaletteMode)"
}
finally {
    $localField.SetValue($listView, $false)
}
$appliedField.SetValue($listView, $itemName)
$hoverTimer = $timerField.GetValue($listView)
if ($hoverTimer) {
    $hoverTimer.Stop()
}
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 120
[System.Windows.Forms.Application]::DoEvents()
$hoverMode = [Krypton.Toolkit.KryptonManager]::CurrentGlobalPaletteMode
Write-Host "Hover palette: $hoverMode live=$($liveField.GetValue($listView))"
$hoverBmp = Capture-WindowPng -Form $form -Path $hoverPath
Write-Host "Wrote $hoverPath"

Move-Pointer -X $screenPt.X -Y $screenPt.Y
Start-Sleep -Milliseconds 80
[System.Windows.Forms.Application]::DoEvents()
$awayX = [int]($form.Bounds.Right + 80)
$awayY = [int]($form.Bounds.Bottom + 80)
Move-Pointer -X $awayX -Y $awayY
$restore = $listView.GetType().GetMethod('RestoreCommittedTheme', $flags)
[void]$restore.Invoke($listView, $null)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 120
[System.Windows.Forms.Application]::DoEvents()
$restoreMode = [Krypton.Toolkit.KryptonManager]::CurrentGlobalPaletteMode
Write-Host "Restored palette: $restoreMode"
$restoreBmp = Capture-WindowPng -Form $form -Path (Join-Path $prDir '3870-theme-listview-restored.png')
Write-Host "Wrote restored PNG"

$committedBmp.Dispose()
$hoverBmp.Dispose()
$restoreBmp.Dispose()
$form.Close()
$form.Dispose()
