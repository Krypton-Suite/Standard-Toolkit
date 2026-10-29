<#
.SYNOPSIS
    Hosts Office2024RibbonDemo and captures the Office 2024 themes, Microsoft 365 Blue, a beveled Blue Dark group area, and compact ribbon (#4533).

.DESCRIPTION
    Loads TestForm in-process (STA) and writes Documents/PR/4496-office-2024-*.png
    via Save-UnitTestWindowPng (PrintWindow). Compact mode also writes
    4533-compact-ribbon-switch.gif while the groups area eases.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-Office2024RibbonScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir,
    [switch]$CompactOnly,
    [switch]$OverflowOnly
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
function Import-UnitTestAssembly([string]$Path) {
    $bytes = [System.IO.File]::ReadAllBytes($Path)
    return [System.Reflection.Assembly]::Load($bytes)
}
$toolkit = Import-UnitTestAssembly (Join-Path $bin 'Krypton.Toolkit.dll')
[void](Import-UnitTestAssembly (Join-Path $bin 'Krypton.Ribbon.dll'))
[void](Import-UnitTestAssembly (Join-Path $bin 'Krypton.Themes.dll'))
$asm = Import-UnitTestAssembly (Join-Path $bin 'TestForm.exe')

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

if ((-not $CompactOnly) -and (-not $OverflowOnly)) {
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
$blueMode = [Enum]::Parse($modeType, 'Office2024Blue')
[void]$apply.Invoke($form, @($blueMode))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$bluePillPath = Join-Path $OutputDir '4496-office-2024-blue-pills.png'
Save-UnitTestWindowPng -Form $form -Path $bluePillPath
Write-Host "Wrote $bluePillPath"
$setGlow = $formType.GetMethod('SetTabMarkerGlow')
[void]$setMarker.Invoke($form, @($line))
[void]$setGlow.Invoke($form, @($true))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$glowPath = Join-Path $OutputDir '4496-office-2024-blue-glow.png'
Save-UnitTestWindowPng -Form $form -Path $glowPath
Write-Host "Wrote $glowPath"
[void]$setMarker.Invoke($form, @($pill))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$glowPillPath = Join-Path $OutputDir '4496-office-2024-blue-glow-pill.png'
Save-UnitTestWindowPng -Form $form -Path $glowPillPath
Write-Host "Wrote $glowPillPath"
[void]$setGlow.Invoke($form, @($false))
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
}

$setCompact = $formType.GetMethod('SetCompactMode')
[void]$formType.GetMethod('SetGroupAreaBevel').Invoke($form, @($false))
[void]$formType.GetMethod('SetGroupAreaGap').Invoke($form, @(0))
$markerType = $toolkit.GetType('Krypton.Toolkit.PaletteRibbonTabMarker')
$lineMarker = [Enum]::Parse($markerType, 'Line')
[void]$formType.GetMethod('SetTabMarker').Invoke($form, @($lineMarker))
[void]$formType.GetMethod('SetTabMarkerGlow').Invoke($form, @($false))
[void]$formType.GetMethod('SetShowContextTitles').Invoke($form, @($false))
[void]$formType.GetMethod('SelectHomeTab').Invoke($form, @())
$compactBlue = [Enum]::Parse($modeType, 'Office2024Blue')
[void]$apply.Invoke($form, @($compactBlue))
$form.ClientSize = New-Object System.Drawing.Size 1100, 760
[void]$formType.GetMethod('SetAnimateCompactMode').Invoke($form, @($false))
if ($OverflowOnly) {
    [void]$setCompact.Invoke($form, @($true))
    [System.Windows.Forms.Application]::DoEvents()
}
else {
[void]$setCompact.Invoke($form, @($false))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$classicPath = Join-Path $OutputDir '4533-compact-ribbon-classic.png'
Save-UnitTestWindowPng -Form $form -Path $classicPath
Write-Host "Wrote $classicPath"

[void]$setCompact.Invoke($form, @($true))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$compactPath = Join-Path $OutputDir '4533-compact-ribbon-compact.png'
Save-UnitTestWindowPng -Form $form -Path $compactPath
Write-Host "Wrote $compactPath"

$setLocation = $formType.GetMethod('SetCompactButtonLocation')
$locType = $setLocation.GetParameters()[0].ParameterType
[void]$setLocation.Invoke($form, @([Enum]::Parse($locType, 'GroupsTrailing')))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
$groupsArrowPath = Join-Path $OutputDir '4533-compact-ribbon-arrow-groups.png'
Save-UnitTestWindowPng -Form $form -Path $groupsArrowPath
Write-Host "Wrote $groupsArrowPath"
[void]$setLocation.Invoke($form, @([Enum]::Parse($locType, 'Tabs')))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
$tabsArrowPath = Join-Path $OutputDir '4533-compact-ribbon-arrow-tabs.png'
Save-UnitTestWindowPng -Form $form -Path $tabsArrowPath
Write-Host "Wrote $tabsArrowPath"
[void]$setLocation.Invoke($form, @([Enum]::Parse($locType, 'BelowGroups')))
[System.Windows.Forms.Application]::DoEvents()

# Height animation. PrintWindow is slower than the live tween, so the GIF
# steps AnimatedGroupsHeight through the same values the timer eases.
$ribbonFlags = [System.Reflection.BindingFlags]'Instance,NonPublic,Public'
$ribbon = $formType.GetField('_ribbon', $ribbonFlags).GetValue($form)
$calc = $ribbon.GetType().GetProperty('CalculatedValues', $ribbonFlags).GetValue($ribbon)
$groupsHeightProp = $calc.GetType().GetProperty('GroupsHeight')
$animatedHeightProp = $calc.GetType().GetProperty('AnimatedGroupsHeight', $ribbonFlags)
$needPaint = $ribbon.GetType().GetMethod('PerformNeedPaint')
function Get-CompactGroupsHeight([bool]$compact) {
    $animatedHeightProp.SetValue($calc, $null)
    [void]$setCompact.Invoke($form, @($compact))
    [System.Windows.Forms.Application]::DoEvents()
    return [int]$groupsHeightProp.GetValue($calc)
}
$classicGroupsHeight = Get-CompactGroupsHeight $false
$compactGroupsHeight = Get-CompactGroupsHeight $true
$gifFrames = New-Object System.Collections.Generic.List[string]
$gifDir = Join-Path $env:TEMP 'krypton-4533-compact-frames'
if (Test-Path -LiteralPath $gifDir) {
    Remove-Item -LiteralPath $gifDir -Recurse -Force
}
New-Item -ItemType Directory -Path $gifDir | Out-Null
function Save-CompactHeightFrame([bool]$compact, [int]$fromHeight, [int]$toHeight, [double]$t) {
    $animatedHeightProp.SetValue($calc, $null)
    [void]$setCompact.Invoke($form, @($compact))
    $eased = 1.0 - [Math]::Pow(1.0 - $t, 3)
    $height = [int][Math]::Round($fromHeight + (($toHeight - $fromHeight) * $eased))
    $animatedHeightProp.SetValue($calc, $height)
    [void]$needPaint.Invoke($ribbon, @($true))
    [System.Windows.Forms.Application]::DoEvents()
    $framePath = Join-Path $gifDir ('frame{0:D3}.png' -f $gifFrames.Count)
    Save-UnitTestWindowPng -Form $form -Path $framePath
    $gifFrames.Add($framePath)
}
foreach ($step in 0..6) {
    Save-CompactHeightFrame $true $classicGroupsHeight $compactGroupsHeight ($step / 6.0)
}
foreach ($step in 0..6) {
    Save-CompactHeightFrame $false $compactGroupsHeight $classicGroupsHeight ($step / 6.0)
}
$animatedHeightProp.SetValue($calc, $null)
[void]$needPaint.Invoke($ribbon, @($true))
$gifPath = Join-Path $OutputDir '4533-compact-ribbon-switch.gif'
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
        $meta.SetQuery('/grctlext/Delay', [uint16]8)
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
[void]$formType.GetMethod('SetAnimateCompactMode').Invoke($form, @($false))
[void]$setCompact.Invoke($form, @($true))
[System.Windows.Forms.Application]::DoEvents()

$compactBlack = [Enum]::Parse($modeType, 'Office2024Black')
[void]$apply.Invoke($form, @($compactBlack))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$blackPath = Join-Path $OutputDir '4533-compact-ribbon-black.png'
Save-UnitTestWindowPng -Form $form -Path $blackPath
Write-Host "Wrote $blackPath"
[void]$apply.Invoke($form, @($compactBlue))
[System.Windows.Forms.Application]::DoEvents()

$shapeShots = @(
    @{ Mode = 'Office2007Blue'; File = '4533-compact-ribbon-office2007.png' },
    @{ Mode = 'Office2010Blue'; File = '4533-compact-ribbon-office2010.png' },
    @{ Mode = 'Office2013White'; File = '4533-compact-ribbon-office2013.png' },
    @{ Mode = 'SparkleBlue'; File = '4533-compact-ribbon-sparkle.png' },
    @{ Mode = 'VisualStudio2010Render2010'; File = '4533-compact-ribbon-visualstudio2010.png' }
)
foreach ($shot in $shapeShots) {
    $shapeMode = [Enum]::Parse($modeType, $shot.Mode)
    [void]$apply.Invoke($form, @($shapeMode))
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 250
    $shapePath = Join-Path $OutputDir $shot.File
    Save-UnitTestWindowPng -Form $form -Path $shapePath
    Write-Host "Wrote $shapePath"
}
[void]$apply.Invoke($form, @($compactBlue))
[System.Windows.Forms.Application]::DoEvents()
}

[void]$formType.GetMethod('SetHideRibbonSize').Invoke($form, @(80, 80))
$form.ClientSize = New-Object System.Drawing.Size 240, 760
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$narrowPath = Join-Path $OutputDir '4533-compact-ribbon-narrow.png'
Save-UnitTestWindowPng -Form $form -Path $narrowPath
Write-Host "Wrote $narrowPath"

$form.ClientSize = New-Object System.Drawing.Size 120, 760
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$overflowPath = Join-Path $OutputDir '4533-compact-ribbon-overflow.png'
Save-UnitTestWindowPng -Form $form -Path $overflowPath
Write-Host "Wrote $overflowPath"

$ribbon = $formType.GetField('_ribbon', [System.Reflection.BindingFlags]'Instance,NonPublic').GetValue($form)
$manager = $ribbon.GetType().GetProperty('ViewRibbonManager', [System.Reflection.BindingFlags]'Instance,NonPublic').GetValue($ribbon)
$script:CompactOverflowButton = $null
function Find-CompactOverflowButton([object]$view) {
    if (($null -eq $view) -or ($null -ne $script:CompactOverflowButton)) { return }
    if ($view.GetType().Name -eq 'ViewDrawRibbonCompactOverflow') {
        if ($view.Visible -and ($view.ClientRectangle.Width -gt 0)) {
            $script:CompactOverflowButton = $view
            return
        }
    }
    if ($view.GetType().Name -eq 'ViewLayoutRibbonScrollPort') {
        $filler = $view.GetType().GetField('_viewFiller', [System.Reflection.BindingFlags]'Instance,NonPublic').GetValue($view)
        Find-CompactOverflowButton $filler
    }
    if ($null -ne $script:CompactOverflowButton) { return }
    $countProp = $view.GetType().GetProperty('Count')
    if ($null -eq $countProp) { return }
    $count = [int]$countProp.GetValue($view)
    $itemProp = $view.GetType().GetProperty('Item')
    for ($i = 0; $i -lt $count; $i++) {
        Find-CompactOverflowButton $itemProp.GetValue($view, @($i))
        if ($null -ne $script:CompactOverflowButton) { return }
    }
}
Find-CompactOverflowButton $manager.Root
$overflowButton = $script:CompactOverflowButton
if ($null -eq $overflowButton) {
    $lines = New-Object System.Collections.Generic.List[string]
    function Walk-RibbonViews([object]$view, [string]$prefix) {
        if ($null -eq $view) { return }
        $name = $view.GetType().Name
        $rect = $view.ClientRectangle
        if (($name -eq 'ViewDrawRibbonCompactOverflow') -or ($name -eq 'ViewLayoutRibbonGroups') -or ($name -eq 'ViewLayoutRibbonScrollPort') -or ($prefix.Length -le 4)) {
            $lines.Add(("{0}{1} vis={2} rect={3},{4},{5},{6}" -f $prefix, $name, $view.Visible, $rect.X, $rect.Y, $rect.Width, $rect.Height))
        }
        if ($name -eq 'ViewLayoutRibbonScrollPort') {
            $filler = $view.GetType().GetField('_viewFiller', [System.Reflection.BindingFlags]'Instance,NonPublic').GetValue($view)
            if ($null -ne $filler) {
                $fillerCount = [int]$filler.GetType().GetProperty('Count').GetValue($filler)
                $fillerItem = $filler.GetType().GetProperty('Item')
                $lines.Add(("  filler {0} count={1} vis={2}" -f $filler.GetType().Name, $fillerCount, $filler.Visible))
                for ($n = 0; $n -lt $fillerCount; $n++) {
                    $child = $fillerItem.GetValue($filler, @($n))
                    $childRect = $child.ClientRectangle
                    $lines.Add(("    {0} vis={1} rect={2},{3},{4},{5}" -f $child.GetType().Name, $child.Visible, $childRect.X, $childRect.Y, $childRect.Width, $childRect.Height))
                }
            }
        }
        $countProp = $view.GetType().GetProperty('Count')
        if ($null -eq $countProp) { return }
        if ($prefix.Length -gt 12) { return }
        $count = [int]$countProp.GetValue($view)
        $itemProp = $view.GetType().GetProperty('Item')
        for ($i = 0; $i -lt $count; $i++) {
            Walk-RibbonViews $itemProp.GetValue($view, @($i)) ($prefix + ' ')
        }
    }
    Walk-RibbonViews $manager.Root ''
    throw ("Compact overflow ellipsis was not visible.`n" + ($lines -join "`n"))
}
$buttonRect = $overflowButton.ClientRectangle
$owner = $overflowButton.OwningControl
if ($null -eq $owner) { $owner = $ribbon }
[void]$formType.GetMethod('SetAnimateCompactMode').Invoke($form, @($true))
$form.TopMost = $true
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
$screenRect = $owner.RectangleToScreen($buttonRect)
$clickX = [int]($screenRect.X + ($screenRect.Width / 2))
$clickY = [int]($screenRect.Y + ($screenRect.Height / 2))
$popupManagerType = $toolkit.GetType('Krypton.Toolkit.VisualPopupManager')
$popupManager = $popupManagerType.GetProperty('Singleton').GetValue($null)
$popupFrames = New-Object System.Collections.Generic.List[string]
$popupFrameDir = Join-Path $env:TEMP 'krypton-4533-overflow-frames'
if (Test-Path -LiteralPath $popupFrameDir) { Remove-Item -LiteralPath $popupFrameDir -Recurse -Force }
New-Item -ItemType Directory -Path $popupFrameDir | Out-Null
Add-Type -AssemblyName System.Drawing
function Save-OverflowMotionFrame([int]$index) {
    $path = Join-Path $popupFrameDir ('step{0:D3}.png' -f $index)
    Save-UnitTestWindowPng -Handle $popup.Handle -Path $path
    $popupFrames.Add($path)
}
[void][UnitTestNative]::SetCursorPos($clickX, $clickY)
Start-Sleep -Milliseconds 80
[UnitTestNative]::mouse_event([UnitTestNative]::LEFTDOWN, 0, 0, 0, [IntPtr]::Zero)
[System.Windows.Forms.Application]::DoEvents()
[UnitTestNative]::mouse_event([UnitTestNative]::LEFTUP, 0, 0, 0, [IntPtr]::Zero)
[System.Windows.Forms.Application]::DoEvents()
$popup = $popupManager.CurrentPopup
if ($null -eq $popup) {
    throw ("Compact overflow popup did not open. owner={0} rect={1},{2},{3},{4} click={5},{6}" -f $owner.GetType().Name, $buttonRect.X, $buttonRect.Y, $buttonRect.Width, $buttonRect.Height, $clickX, $clickY)
}
$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$timer = $popup.GetType().GetField('_animationTimer', $flags).GetValue($popup)
if ($null -ne $timer) { $timer.Stop() }
$applyFrame = $popup.GetType().GetMethod('ApplyFrame', $flags)
$fullHeight = [int]$popup.GetType().GetField('_fullSize', $flags).GetValue($popup).Height
foreach ($step in @(1, 8, 16, 24, $fullHeight)) {
    [void]$applyFrame.Invoke($popup, @([int]$step))
    [System.Windows.Forms.Application]::DoEvents()
    Save-OverflowMotionFrame $popupFrames.Count
}
$popupPath = Join-Path $OutputDir '4533-compact-ribbon-overflow-popup.png'
Save-UnitTestWindowPng -Handle $popup.Handle -Path $popupPath
Write-Host "Wrote $popupPath"
foreach ($step in @($fullHeight, 24, 16, 8, 1)) {
    [void]$applyFrame.Invoke($popup, @([int]$step))
    [System.Windows.Forms.Application]::DoEvents()
    Save-OverflowMotionFrame $popupFrames.Count
}
if ($popupFrames.Count -gt 1) {
    try {
        Add-Type -AssemblyName System.Drawing
        $maxW = 1
        $maxH = 1
        foreach ($framePath in $popupFrames) {
            $probe = [System.Drawing.Image]::FromFile($framePath)
            if ($probe.Width -gt $maxW) { $maxW = $probe.Width }
            if ($probe.Height -gt $maxH) { $maxH = $probe.Height }
            $probe.Dispose()
        }
        $fill = [System.Drawing.Color]::FromArgb(255, 232, 240, 248)
        $fullFrame = $popupFrames | Where-Object { $_ -like '*step004.png' } | Select-Object -First 1
        if ($fullFrame) {
            $sample = New-Object System.Drawing.Bitmap $fullFrame
            $fill = $sample.GetPixel([Math]::Max(0, $sample.Width - 16), [Math]::Max(0, $sample.Height - 8))
            $sample.Dispose()
        }
        Add-Type -AssemblyName PresentationCore
        $encoder = New-Object System.Windows.Media.Imaging.GifBitmapEncoder
        foreach ($framePath in $popupFrames) {
            $canvas = New-Object System.Drawing.Bitmap $maxW, $maxH
            $graphics = [System.Drawing.Graphics]::FromImage($canvas)
            $graphics.Clear($fill)
            $source = [System.Drawing.Image]::FromFile($framePath)
            $graphics.DrawImage($source, 0, 0, $source.Width, $source.Height)
            $source.Dispose()
            $graphics.Dispose()
            $ms = New-Object System.IO.MemoryStream
            $canvas.Save($ms, [System.Drawing.Imaging.ImageFormat]::Png)
            $canvas.Dispose()
            $ms.Position = 0
            $decoder = [System.Windows.Media.Imaging.BitmapDecoder]::Create(
                $ms,
                [System.Windows.Media.Imaging.BitmapCreateOptions]::PreservePixelFormat,
                [System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
            $meta = New-Object System.Windows.Media.Imaging.BitmapMetadata 'gif'
            $meta.SetQuery('/grctlext/Delay', [uint16]8)
            $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($decoder.Frames[0], $null, $meta, $null))
            $ms.Dispose()
        }
        $gifPath = Join-Path $OutputDir '4533-compact-ribbon-overflow.gif'
        $gifStream = [System.IO.File]::Open($gifPath, [System.IO.FileMode]::Create)
        $encoder.Save($gifStream)
        $gifStream.Dispose()
        Write-Host "Wrote $gifPath"
    }
    catch {
        Write-Warning "Overflow GIF encode failed: $($_.Exception.Message)"
    }
}
[void]$popupManagerType.GetMethod('EndAllTracking').Invoke($popupManager, @())
[System.Windows.Forms.Application]::DoEvents()

$form.ClientSize = New-Object System.Drawing.Size 1100, 760
$compact365 = [Enum]::Parse($modeType, 'Microsoft365Blue')
[void]$apply.Invoke($form, @($compact365))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$compact365Path = Join-Path $OutputDir '4533-compact-ribbon-microsoft365.png'
Save-UnitTestWindowPng -Form $form -Path $compact365Path
Write-Host "Wrote $compact365Path"
[void]$setCompact.Invoke($form, @($false))

if ((-not $CompactOnly) -and (-not $OverflowOnly)) {
$blueBack = [Enum]::Parse($modeType, 'Office2024Blue')
[void]$apply.Invoke($form, @($blueBack))
[System.Windows.Forms.Application]::DoEvents()
$showBackstage = $formType.GetMethod('ShowFileBackstage')
[void]$showBackstage.Invoke($form, @())
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400
$overlay = $null
foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
    if ($open.GetType().Name -eq 'VisualBackstageOverlayForm') {
        $overlay = $open
        break
    }
}
if ($null -eq $overlay) {
    throw 'Backstage overlay was not shown.'
}
$backstagePath = Join-Path $OutputDir '4496-office-2024-blue-backstage.png'
Save-UnitTestWindowPng -Form $overlay -Path $backstagePath
Write-Host "Wrote $backstagePath"
}

$form.Close()
$form.Dispose()
