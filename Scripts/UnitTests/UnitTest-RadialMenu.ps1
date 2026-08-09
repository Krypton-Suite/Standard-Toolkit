<#
.SYNOPSIS
    Asserts #4172 KryptonRadialMenu public API: items, bridge, live sync, PreferRadial, show/close.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Toolkit.Utilities binaries and runs in-process STA checks:

    1. Default appearance values (Sweep, shadow, StartAngle, MaxVisibleItems, HitPadding, image size).
    2. Item PerformClick / CheckOnClick / ItemClick; ResolveImage from Image and KryptonCommand.
    3. Slider SetNormalizedValue raises ValueChanged; TextItem / CalendarItem construct.
    4. ImportFrom maps Item, LinkLabel, TextBox→TextItem, ComboBox, ProgressBar, MonthCalendar→CalendarItem,
       ColorColumns; skips Separator / Heading.
    5. Live sync re-projects when the root Items collection changes; property sync updates Text on Tag sources.
    6. PreferRadialContextMenus registers / clears KryptonContextMenu.AlternativeShow.
    7. ShowPopup / Close (including animated close) without crash on an off-screen form.
    8. KryptonRadialMenuPresenter.GetOrCreateProjection caches live-synced projections.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-RadialMenu.ps1
#>
# UnitTest-CI: include
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
Register-UnitTestAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

$failed = New-Object System.Collections.Generic.List[string]

function Assert-True {
    param([bool]$Condition, [string]$Message)
    if (-not $Condition) {
        $failed.Add($Message)
        Write-Host "FAIL: $Message" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

function Assert-Equal {
    param($Expected, $Actual, [string]$Message)
    $ok = $false
    if ($Expected -is [ValueType] -and $Actual -is [ValueType] -and
        ($Expected -is [double] -or $Expected -is [float] -or $Expected -is [decimal] -or
         $Actual -is [double] -or $Actual -is [float] -or $Actual -is [decimal])) {
        $ok = [math]::Abs([double]$Expected - [double]$Actual) -lt 0.0001
    }
    else {
        $ok = [object]::Equals($Expected, $Actual)
    }

    if (-not $ok) {
        $failed.Add("$Message (expected='$Expected' actual='$Actual')")
        Write-Host "FAIL: $Message (expected='$Expected' actual='$Actual')" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

function Get-NetObject {
    param(
        [Parameter(Mandatory = $false)]
        [AllowNull()]
        [object]$Value
    )
    if ($null -eq $Value) { return $null }
    if ($Value -is [System.Management.Automation.PSObject]) {
        return $Value.PSObject.BaseObject
    }
    return $Value
}

function Get-RadialItemTypeName {
    param($Item)
    $net = Get-NetObject $Item
    if ($null -eq $net) { return '<null>' }
    return $net.GetType().Name
}

Write-UnitTestBanner -Status INFO -Message 'Asserting #4172 KryptonRadialMenu public API'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = New-Object System.Windows.Forms.Form
$form.Text = 'UnitTest-4172-RadialMenu'
$form.ShowInTaskbar = $false
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(-32000, -32000)
$form.Size = New-Object System.Drawing.Size(400, 300)
[void]$form.Show()
[System.Windows.Forms.Application]::DoEvents()

# ----- Defaults -----
$menu = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenu]))
Assert-Equal ([Krypton.Toolkit.Utilities.KryptonRadialMenuAnimationStyle]::Sweep) $menu.AnimationStyle 'Default AnimationStyle is Sweep'
Assert-Equal 220 $menu.AnimationDuration 'Default AnimationDuration is 220'
Assert-equal 24 $menu.ItemImageSize 'Default ItemImageSize is 24'
Assert-True ([bool]$menu.ShowShadow) 'Default ShowShadow is true'
Assert-True ([bool]$menu.ShowCheckedGlyph) 'Default ShowCheckedGlyph is true'
Assert-Equal ([Krypton.Toolkit.Utilities.KryptonRadialMenuDisplayStyle]::ImageAboveText) $menu.DisplayStyle 'Default DisplayStyle is ImageAboveText'
Assert-Equal (-90) ([float]$menu.StartAngle) 'Default StartAngle is -90'
Assert-Equal 0 $menu.MaxVisibleItems 'Default MaxVisibleItems is 0 (unlimited)'
Assert-Equal 4 ([float]$menu.HitPadding) 'Default HitPadding is 4'

# ----- Item click / check / ResolveImage -----
$script:itemClickCount = 0
$script:leafClickCount = 0
$itemClickHandler = [System.EventHandler[Krypton.Toolkit.Utilities.KryptonRadialMenuItemClickEventArgs]]{
    param($s, $e) $script:itemClickCount++
}
$menu.add_ItemClick($itemClickHandler)

$leaf = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuItem], [object[]]@('Leaf')))
$leaf.CheckOnClick = $true
$leafClickHandler = [System.EventHandler]{ param($s, $e) $script:leafClickCount++ }
$leaf.add_Click($leafClickHandler)
[void]$menu.Items.Add($leaf)

# Raise ItemClick the same way the popup does before PerformClick.
$menu.GetType().GetMethod('RaiseItemClick', [System.Reflection.BindingFlags]'Instance,NonPublic').Invoke($menu, @($leaf))
$leaf.PerformClick()
Assert-equal 1 $script:itemClickCount 'RaiseItemClick notifies menu ItemClick'
Assert-Equal 1 $script:leafClickCount 'PerformClick raises item Click'
Assert-True ([bool]$leaf.Checked) 'CheckOnClick toggles Checked on PerformClick'

$bmp = New-Object System.Drawing.Bitmap 16, 16
try {
    $leaf.Image = $bmp
    Assert-True ($null -ne $leaf.ResolveImage) 'ResolveImage returns explicit Image'

    $command = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonCommand]))
    $command.Text = 'Cmd'
    $commandBmp = New-Object System.Drawing.Bitmap 12, 12
    try {
        $command.ImageSmall = $commandBmp
        $cmdItem = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuItem], [object[]]@('Bound')))
        $cmdItem.KryptonCommand = $command
        Assert-True ($null -ne $cmdItem.ResolveImage) 'ResolveImage falls back to KryptonCommand.ImageSmall'
        Assert-Equal 'Cmd' $cmdItem.ResolveText 'ResolveText prefers KryptonCommand.Text'
    }
    finally {
        $commandBmp.Dispose()
        $command.Dispose()
    }
}
finally {
    $bmp.Dispose()
}

# ----- Slider -----
$slider = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuSliderItem]))
$slider.Minimum = 0
$slider.Maximum = 100
$slider.Value = 10
$script:sliderChanged = 0
$sliderHandler = [System.EventHandler]{ param($s, $e) $script:sliderChanged++ }
$slider.add_ValueChanged($sliderHandler)
$slider.SetNormalizedValue(0.5)
Assert-Equal 50 ([int]$slider.Value) 'SetNormalizedValue(0.5) maps to mid Value'
Assert-True ($script:sliderChanged -ge 1) 'SetNormalizedValue raises ValueChanged'

# ----- Text / Calendar items -----
$textItem = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuTextItem]))
$textItem.Label = 'Note'
$textItem.Text = 'Draft'
Assert-Equal 'Note' $textItem.Label 'TextItem Label round-trips'
Assert-Equal 'Draft' $textItem.Text 'TextItem Text round-trips'

$calendarItem = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuCalendarItem]))
$calendarItem.SelectedDate = [datetime]'2026-08-09'
Assert-Equal ([datetime]'2026-08-09').Date $calendarItem.SelectedDate.Date 'CalendarItem SelectedDate round-trips'

# ----- Bridge ImportFrom -----
$ctx = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenu]))
$group = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuItems]))
[void]$group.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuItem], [object[]]@('Open')))))
[void]$group.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuLinkLabel], [object[]]@('Docs')))))
[void]$group.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuSeparator]))))
[void]$group.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuHeading], [object[]]@('Section')))))

$textBox = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuTextBox]))
$textBox.Text = 'Hello radial'
[void]$group.Items.Add($textBox)

$combo = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuComboBox]))
[void]$combo.Items.Add('Alpha')
[void]$combo.Items.Add('Beta')
[void]$group.Items.Add($combo)

$progress = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuProgressBar]))
$progress.Minimum = 0
$progress.Maximum = 100
$progress.Value = 42
[void]$group.Items.Add($progress)

[void]$group.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuMonthCalendar]))))
[void]$group.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuColorColumns], [object[]]@([Krypton.Toolkit.ColorScheme]::Basic16)))))
[void]$ctx.Items.Add($group)

$imported = Get-NetObject ([Krypton.Toolkit.Utilities.KryptonRadialMenu]::FromContextMenu($ctx))
$types = @()
foreach ($entry in $imported.Items) {
    $types += (Get-RadialItemTypeName $entry)
}

Assert-True ($types -contains 'KryptonRadialMenuItem') 'Import maps command / link / editors to radial items'
Assert-True ($types -contains 'KryptonRadialMenuColorPaletteItem') 'Import maps ColorColumns to ColorPaletteItem'
Assert-True ($types -contains 'KryptonRadialMenuTextItem') 'Import maps TextBox to TextItem'
Assert-True ($types -contains 'KryptonRadialMenuCalendarItem') 'Import maps MonthCalendar to CalendarItem'
Assert-Equal 7 $imported.Items.Count 'Import skips Separator and Heading (7 projected items)'

$texts = @()
$importedTextValue = $null
foreach ($entry in $imported.Items) {
    $net = Get-NetObject $entry
    if ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuItem]) {
        $texts += [string]$net.Text
    }
    elseif ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuColorPaletteItem]) {
        $texts += [string]$net.Text
    }
    elseif ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuTextItem]) {
        $importedTextValue = [string]$net.Text
        $texts += [string]$net.Label
    }
    elseif ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuCalendarItem]) {
        $texts += [string]$net.Text
    }
}

Assert-True ($texts -contains 'Open') 'Imported Open command item'
Assert-True ($texts -contains 'Docs') 'Imported LinkLabel item'
Assert-Equal 'Hello radial' $importedTextValue 'Imported TextBox value onto TextItem.Text'
Assert-True ($texts -contains 'Combo' -or ($texts | Where-Object { $_ -like 'Alpha*' -or $_ -eq 'Combo' -or $_ -eq 'Beta' })) 'Imported ComboBox parent'
Assert-True ($texts -contains '42/100') 'Imported ProgressBar display text'
Assert-True ($texts -contains 'Colors') 'Imported ColorColumns as Colors'
Assert-True ($texts -contains 'Date') 'Imported MonthCalendar as CalendarItem'

$comboRadial = $null
foreach ($entry in $imported.Items) {
    $net = Get-NetObject $entry
    if ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuItem] -and $net.Items.Count -gt 0 -and $net.Text -ne 'Open') {
        $child0 = Get-NetObject $net.Items[0]
        if ($child0 -is [Krypton.Toolkit.Utilities.KryptonRadialMenuItem] -and $child0.Text -eq 'Alpha') {
            $comboRadial = $net
            break
        }
    }
}
Assert-True ($null -ne $comboRadial) 'Imported ComboBox exposes Alpha/Beta children'
Assert-Equal 2 $comboRadial.Items.Count 'ComboBox radial parent has two children'

# ----- Live sync -----
$live = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenu]))
$liveCtx = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenu]))
$live.ImportFrom($liveCtx, $true)
Assert-Equal 0 $live.Items.Count 'Live import starts empty'

$syncedSource = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuItem], [object[]]@('Synced')))
[void]$liveCtx.Items.Add($syncedSource)
Assert-equal 1 $live.Items.Count 'Live sync re-imports when root Items gains an entry'
$synced = Get-NetObject $live.Items[0]
Assert-Equal 'Synced' $synced.Text 'Live-synced item text is Synced'

# Property-level live sync (Tag source → radial twin without full rebuild).
$syncedSource.Text = 'Synced-Updated'
[System.Windows.Forms.Application]::DoEvents()
Assert-Equal 'Synced-Updated' $synced.Text 'PropertyChanged sync updates radial Text from context-menu source'

$menu.MaxVisibleItems = 4
Assert-Equal 4 $menu.MaxVisibleItems 'MaxVisibleItems accepts paging window'
$menu.StartAngle = 0
Assert-equal 0 ([float]$menu.StartAngle) 'StartAngle can be set to 0 (east)'
$menu.StartAngle = -90
$menu.MaxVisibleItems = 0

# ----- PreferRadial hook -----
$prevPrefer = [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus
try {
    [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus = $false
    Assert-True ($null -eq [Krypton.Toolkit.KryptonContextMenu]::AlternativeShow) 'PreferRadial false clears AlternativeShow'
    [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus = $true
    Assert-True ($null -ne [Krypton.Toolkit.KryptonContextMenu]::AlternativeShow) 'PreferRadial true registers AlternativeShow'
    [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus = $false
    Assert-True ($null -eq [Krypton.Toolkit.KryptonContextMenu]::AlternativeShow) 'PreferRadial false clears AlternativeShow when it owned the hook'

    $presenterCtx = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenu]))
    [void]$presenterCtx.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuItem], [object[]]@('P')))))
    $proj1 = Get-NetObject ([Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::GetOrCreateProjection($presenterCtx))
    $proj2 = Get-NetObject ([Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::GetOrCreateProjection($presenterCtx))
    Assert-True ([object]::ReferenceEquals($proj1, $proj2)) 'Presenter caches one projection per context menu'
    Assert-Equal 1 $proj1.Items.Count 'Presenter projection imports source items'
}
finally {
    [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus = $prevPrefer
}

# ----- Show / Close -----
$popupMenu = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenu]))
$popupMenu.AnimationStyle = [Krypton.Toolkit.Utilities.KryptonRadialMenuAnimationStyle]::None
$popupItem = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuItem], [object[]]@('PopupLeaf')))
[void]$popupMenu.Items.Add($popupItem)
$screenPt = New-Object System.Drawing.Point (-31000, -31000)
$shown = [bool]$popupMenu.ShowPopup($form, $screenPt, $false)
Assert-True $shown 'ShowPopup returns true'
Assert-True ([bool]$popupMenu.Visible) 'Menu reports Visible after ShowPopup'
[System.Windows.Forms.Application]::DoEvents()
$popupMenu.Close()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 100
[System.Windows.Forms.Application]::DoEvents()
Assert-True (-not [bool]$popupMenu.Visible) 'Menu is not Visible after Close'

# Animated close should not throw.
$animMenu = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenu]))
$animMenu.AnimationStyle = [Krypton.Toolkit.Utilities.KryptonRadialMenuAnimationStyle]::FadeScale
$animMenu.AnimationDuration = 80
[void]$animMenu.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenuItem], [object[]]@('AnimLeaf')))))
Assert-True ([bool]$animMenu.ShowPopup($form, $screenPt, $false)) 'Animated ShowPopup returns true'
[System.Windows.Forms.Application]::DoEvents()
$animCloseOk = $true
try {
    $animMenu.Close()
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200
    [System.Windows.Forms.Application]::DoEvents()
}
catch {
    $animCloseOk = $false
    $failed.Add("Animated Close threw: $($_.Exception.Message)")
    Write-Host "FAIL: Animated Close threw: $($_.Exception.Message)" -ForegroundColor Red
}
if ($animCloseOk) {
    Write-Host 'PASS: Animated Close completes without throw' -ForegroundColor Green
}
Assert-True (-not [bool]$animMenu.Visible) 'Menu is not Visible after animated Close'

# Cleanup
$menu.remove_ItemClick($itemClickHandler)
$leaf.remove_Click($leafClickHandler)
$slider.remove_ValueChanged($sliderHandler)
$menu.Dispose()
$imported.Dispose()
$live.Dispose()
$popupMenu.Dispose()
$animMenu.Dispose()
$ctx.Dispose()
$liveCtx.Dispose()
$form.Close()
$form.Dispose()

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message ("#4172 assertions failed ($($failed.Count))")
    exit 1
}

Write-UnitTestBanner -Status PASS -Message '#4172 KryptonRadialMenu assertions passed'
exit 0
