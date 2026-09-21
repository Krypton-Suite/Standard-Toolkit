<#
.SYNOPSIS
    Hosts a tabbed FloatingToolbarGroup and captures the container form.

.DESCRIPTION
    Loads Krypton assemblies in-process (STA), builds two floatable toolbars in a
    tabbed FloatingToolbarGroup, shows VisualFloatingToolbarTabbedContainerForm,
    and writes Documents/PR/4410-tabbed-floating-toolbar-page-content.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-TabbedFloatingToolbarScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputPath) {
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4410-tabbed-floating-toolbar-page-content.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

[void][UnitTestNative]::SetProcessDPIAware()
[System.Windows.Forms.Application]::EnableVisualStyles()

$toolbarType = [Type]::GetType('Krypton.Toolkit.Utilities.KryptonFloatableToolStrip, Krypton.Toolkit.Utilities', $true)
$groupType = [Type]::GetType('Krypton.Toolkit.Utilities.FloatingToolbarGroup, Krypton.Toolkit.Utilities', $true)

$tb1 = [System.Activator]::CreateInstance($toolbarType)
$tb1.Name = 'Toolbar1'
$tb1.FloatingToolBarWindowText = 'Standard Toolbar'
$tb1.Items.Add('Open') | Out-Null
$tb1.Items.Add('Save') | Out-Null

$tb2 = [System.Activator]::CreateInstance($toolbarType)
$tb2.Name = 'Toolbar2'
$tb2.FloatingToolBarWindowText = 'Formatting Toolbar'
$tb2.Items.Add('Bold') | Out-Null
$tb2.Items.Add('Italic') | Out-Null

$group = [System.Activator]::CreateInstance($groupType, @('Demo Group'))
$group.AddToolbar($tb1)
$group.AddToolbar($tb2)
$group.IsTabbed = $true

$form = $group.TabbedContainerForm
if (-not $form) {
    throw 'Tabbed container form was not created.'
}

$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 40, 40
$form.Size = New-Object System.Drawing.Size 520, 280
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 1000
[System.Windows.Forms.Application]::DoEvents()
# Prefer first tab so the Standard Toolbar strip is on-screen.
if ($form.Navigator -and $form.Navigator.Pages.Count -gt 0) {
    $form.Navigator.SelectedIndex = 0
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 300
    [System.Windows.Forms.Application]::DoEvents()
}

# Confirm at least one page has child controls (the fix under test).
$nav = $form.Navigator
if (-not $nav -or $nav.Pages.Count -lt 1) {
    throw 'Navigator pages were not populated.'
}
$page0 = $nav.Pages[0]
if ($page0.Controls.Count -lt 1) {
    throw 'Page has no child controls; page.Controls.Add(panel) likely missing.'
}

$bounds = $form.Bounds
$bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
$g.Dispose()
$bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
$form.Close()
$form.Dispose()

Write-Host "Wrote $OutputPath"
