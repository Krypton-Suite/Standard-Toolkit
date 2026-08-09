<#
.SYNOPSIS
    Asserts #4172 KryptonRadialMenu public API: items, bridge, live sync, show/close, presenter.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Toolkit.Utilities binaries and runs in-process STA checks:

    1. Default appearance values (Sweep, shadow, checked glyph, image size).
    2. Item PerformClick / CheckOnClick / ItemClick; ResolveImage from Image and KryptonCommand.
    3. Slider SetNormalizedValue raises ValueChanged.
    4. ImportFrom maps Item, LinkLabel, TextBox, ComboBox, ProgressBar, MonthCalendar, ColorColumns;
       skips Separator / Heading.
    5. Live sync re-projects when the root context-menu Items collection changes.
    6. ShowPopup / Close visibility on an off-screen form.
    7. KryptonRadialMenuPresenter.GetOrCreateProjection caches live-synced projections.

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
    if (-not [object]::Equals($Expected, $Actual)) {
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
Assert-Equal 1 $script:itemClickCount 'RaiseItemClick notifies menu ItemClick'
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
Assert-Equal 7 $imported.Items.Count 'Import skips Separator and Heading (7 projected items)'

$texts = @()
foreach ($entry in $imported.Items) {
    $net = Get-NetObject $entry
    if ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuItem]) {
        $texts += [string]$net.Text
    }
    elseif ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuColorPaletteItem]) {
        $texts += [string]$net.Text
    }
}

Assert-True ($texts -contains 'Open') 'Imported Open command item'
Assert-True ($texts -contains 'Docs') 'Imported LinkLabel item'
Assert-True ($texts -contains 'Hello radial') 'Imported TextBox display text'
Assert-True ($texts -contains 'Combo' -or ($texts | Where-Object { $_ -like 'Alpha*' -or $_ -eq 'Combo' -or $_ -eq 'Beta' })) 'Imported ComboBox parent'
Assert-True ($texts -contains '42/100') 'Imported ProgressBar display text'
Assert-True ($texts -contains 'Colors') 'Imported ColorColumns as Colors'

$comboRadial = $null
foreach ($entry in $imported.Items) {
    $net = Get-NetObject $entry
    if ($net -is [Krypton.Toolkit.Utilities.KryptonRadialMenuItem] -and $net.Items.Count -gt 0 -and $net.Text -ne 'Open') {
        # Combo parent has Alpha/Beta children; MonthCalendar/TextBox do not.
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

[void]$liveCtx.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuItem], [object[]]@('Synced')))))
Assert-equal 1 $live.Items.Count 'Live sync re-imports when root Items gains an entry'
$synced = Get-NetObject $live.Items[0]
Assert-equal 'Synced' $synced.Text 'Live-synced item text is Synced'

# ----- Show / Close -----
$popupMenu = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonRadialMenu]))
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

# ----- Presenter cache -----
$prevPrefer = [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus
try {
    [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus = $false
    $presenterCtx = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenu]))
    [void]$presenterCtx.Items.Add((Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonContextMenuItem], [object[]]@('P')))))
    $proj1 = Get-NetObject ([Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::GetOrCreateProjection($presenterCtx))
    $proj2 = Get-NetObject ([Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::GetOrCreateProjection($presenterCtx))
    Assert-True ([object]::ReferenceEquals($proj1, $proj2)) 'Presenter caches one projection per context menu'
    Assert-equal 1 $proj1.Items.Count 'Presenter projection imports source items'
}
finally {
    [Krypton.Toolkit.Utilities.KryptonRadialMenuPresenter]::PreferRadialContextMenus = $prevPrefer
}

# Cleanup
$menu.remove_ItemClick($itemClickHandler)
$leaf.remove_Click($leafClickHandler)
$slider.remove_ValueChanged($sliderHandler)
$menu.Dispose()
$imported.Dispose()
$live.Dispose()
$popupMenu.Dispose()
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
