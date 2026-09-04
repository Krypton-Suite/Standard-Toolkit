<#
.SYNOPSIS
    Hosts ThemeCatalogDemo, hovers a KryptonThemeListView item, and captures live-preview stills/GIF.

.DESCRIPTION
    Loads Debug TestForm assemblies in-process (STA), shows ThemeCatalogDemo, moves the
    pointer over a non-selected list item so LivePreviewOnHover applies that theme, then
    moves away to restore. Writes Documents/PR/3870-theme-listview-*.png and a short GIF.

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
$gifPath = Join-Path $prDir '3870-theme-listview-hover.gif'

Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName PresentationCore
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
$form.Show()
$form.Activate()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 600
[System.Windows.Forms.Application]::DoEvents()

$listField = $formType.GetField('_themeListView', [System.Reflection.BindingFlags]'Instance,NonPublic')
$listView = $listField.GetValue($form)
if ($listView.Items.Count -lt 2) {
    throw 'KryptonThemeListView has fewer than two themes.'
}

$hoverIndex = 0
$selected = $listView.SelectedIndex
if ($selected -eq 0) {
    $hoverIndex = 1
}

$itemRect = $listView.GetItemRect($hoverIndex)
$itemCentre = New-Object System.Drawing.Point (
    [int]($itemRect.X + ($itemRect.Width / 2)),
    [int]($itemRect.Y + ($itemRect.Height / 2)))
$screenPt = $listView.PointToScreen($itemCentre)

$committedBmp = Capture-WindowPng -Form $form -Path $committedPath
Write-Host "Wrote $committedPath"

Move-Pointer -X $screenPt.X -Y $screenPt.Y
Start-Sleep -Milliseconds 250
[System.Windows.Forms.Application]::DoEvents()
$hoverBmp = Capture-WindowPng -Form $form -Path $hoverPath
Write-Host "Wrote $hoverPath"

$away = New-Object System.Drawing.Point ($form.Bounds.Right + 40, $form.Bounds.Bottom + 40)
Move-Pointer -X $away.X -Y $away.Y
Start-Sleep -Milliseconds 250
[System.Windows.Forms.Application]::DoEvents()
$restoreBmp = Capture-WindowPng -Form $form -Path (Join-Path $prDir '3870-theme-listview-restored.png')

$encoder = New-Object System.Windows.Media.Imaging.GifBitmapEncoder
foreach ($bmp in @($committedBmp, $hoverBmp, $restoreBmp)) {
    $ms = New-Object System.IO.MemoryStream
    $bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Png)
    $ms.Position = 0
    $decoder = New-Object System.Windows.Media.Imaging.PngBitmapDecoder(
        $ms,
        [System.Windows.Media.Imaging.BitmapCreateOptions]::PreservePixelFormat,
        [System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
    $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($decoder.Frames[0]))
    $ms.Dispose()
}

$gifStream = [System.IO.File]::Open($gifPath, [System.IO.FileMode]::Create)
$encoder.Save($gifStream)
$gifStream.Dispose()
Write-Host "Wrote $gifPath"

$committedBmp.Dispose()
$hoverBmp.Dispose()
$restoreBmp.Dispose()
$form.Close()
$form.Dispose()
