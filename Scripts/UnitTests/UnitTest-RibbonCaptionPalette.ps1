<#
.SYNOPSIS
    Asserts #3859 / #4061: ribbon caption chrome updates when the global palette changes.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Ribbon binaries and runs in-process STA checks:

    1. Host a KryptonForm with an integrated KryptonRibbon (QAT Above, File app button visible).
    2. Switch to Office 2007 Blue: RibbonShape is Office2007 and AllowIconDisplay is false.
    3. Switch to Microsoft 365 Blue: RibbonShape is Microsoft365 and AllowIconDisplay is true.
    4. Confirm the caption area reports using custom chrome after the swap (no resize).

    Requires an STA apartment (use powershell -STA).

    # UnitTest-CI: include

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-RibbonCaptionPalette.ps1
#>
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))

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

function Invoke-Idle {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
    [System.Windows.Forms.Application]::DoEvents()
}

$form = $null
$manager = New-Object Krypton.Toolkit.KryptonManager
$previousMode = $manager.GlobalPaletteMode
try {
    $form = New-Object Krypton.Toolkit.KryptonForm
    $form.Text = 'Ribbon caption palette'
    $form.Size = New-Object System.Drawing.Size(900, 420)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point(80, 80)
    $form.ShowIcon = $true

    $ribbon = New-Object Krypton.Ribbon.KryptonRibbon
    $ribbon.Dock = [System.Windows.Forms.DockStyle]::Top
    $ribbon.QATLocation = [Krypton.Ribbon.QATLocation]::Above
    $ribbon.RibbonFileAppButton.AppButtonVisible = $true
    $ribbon.InsertStandardQATItems()

    $tab = New-Object Krypton.Ribbon.KryptonRibbonTab
    $tab.Text = 'Home'
    $group = New-Object Krypton.Ribbon.KryptonRibbonGroup
    $group.TextLine1 = 'Clipboard'
    $tab.Groups.Add($group)
    $ribbon.RibbonTabs.Add($tab)

    $form.Controls.Add($ribbon)
    $form.Show()
    $form.Activate()
    Invoke-Idle

    $manager.GlobalPaletteMode = [Krypton.Toolkit.PaletteMode]::Office2007Blue
    Invoke-Idle

    $shape2007 = $ribbon.StateCommon.RibbonGeneral.GetRibbonShape()
    Assert-Equal ([Krypton.Toolkit.PaletteRibbonShape]::Office2007) $shape2007 'Office 2007 Blue uses Office2007 ribbon shape'
    Assert-Equal $false $form.AllowIconDisplay 'Office 2007 + File app button hides the form icon without a resize'

    $manager.GlobalPaletteMode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue
    Invoke-Idle

    $shape365 = $ribbon.StateCommon.RibbonGeneral.GetRibbonShape()
    Assert-Equal ([Krypton.Toolkit.PaletteRibbonShape]::Microsoft365) $shape365 'Microsoft 365 Blue uses Microsoft365 ribbon shape'
    Assert-Equal $true $form.AllowIconDisplay 'Microsoft 365 shows the form icon after the palette change without a resize'
    Assert-Equal ([Krypton.Ribbon.QATLocation]::Above) $ribbon.QATLocation 'QATLocation remains Above after the palette change'
}
finally {
    $manager.GlobalPaletteMode = $previousMode
    $manager.Dispose()
    if ($form) {
        $form.Close()
        $form.Dispose()
    }
}

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'Ribbon caption palette assertions passed.' -ForegroundColor Green
exit 0
