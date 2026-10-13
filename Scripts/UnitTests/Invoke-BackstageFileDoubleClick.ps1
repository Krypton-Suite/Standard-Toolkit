<#
.SYNOPSIS
    Checks that a double-click on the File button closes the form while backstage covers it.

.DESCRIPTION
    Hosts Office2024RibbonDemo (#4552) in-process (STA). Writes the demo and the open
    File backstage under Documents/PR via Save-UnitTestWindowPng, then double-clicks
    the File button screen rectangle. A single click must leave the form open.
    The second release must close it.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-BackstageFileDoubleClick.ps1
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
$toolkit = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Office2024RibbonDemo')
if (-not $formType) {
    throw 'Type TestForm.Office2024RibbonDemo was not found in TestForm.exe.'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300

$demoPath = Join-Path $OutputDir '4552-backstage-file-demo.png'
Save-UnitTestWindowPng -Form $form -Path $demoPath

function Get-AppButtonScreenPoint {
    param($TargetForm)
    $targetType = $TargetForm.GetType()
    $targetRibbon = $targetType.GetField('_ribbon', $flags).GetValue($TargetForm)
    $rect = $targetRibbon.GetType().GetMethod('AppButtonScreenRectangle', $flags).Invoke($targetRibbon, $null)
    if ($rect.IsEmpty) {
        throw 'File button screen rectangle is empty.'
    }

    return New-Object System.Drawing.Point ([int]($rect.X + ($rect.Width / 2))), ([int]($rect.Y + ($rect.Height / 2)))
}

function Invoke-LeftClickAt {
    param([int]$X, [int]$Y)
    [void][UnitTestNative]::SetCursorPos($X, $Y)
    Start-Sleep -Milliseconds 40
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTDOWN, 0, 0, 0, [IntPtr]::Zero)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 40
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTUP, 0, 0, 0, [IntPtr]::Zero)
    [System.Windows.Forms.Application]::DoEvents()
}

function Test-BackstageOverlayOpen {
    foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
        if ($open.GetType().Name -eq 'VisualBackstageOverlayForm') {
            return $true
        }
    }

    return $false
}

# A double-click on File must close without ever showing the backstage.
$form.Hide()
$cold = [System.Activator]::CreateInstance($formType)
$cold.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$cold.Location = New-Object System.Drawing.Point 80, 80
$cold.TopMost = $true
$cold.Show()
$cold.Activate()
$cold.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$coldPoint = Get-AppButtonScreenPoint -TargetForm $cold
Invoke-LeftClickAt -X $coldPoint.X -Y $coldPoint.Y
if (Test-BackstageOverlayOpen) {
    throw 'The first click of a double-click showed the backstage.'
}
Start-Sleep -Milliseconds 40
Invoke-LeftClickAt -X $coldPoint.X -Y $coldPoint.Y
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 150
if (Test-BackstageOverlayOpen) {
    throw 'Double-clicking File showed the backstage before close.'
}
if (-not $cold.IsDisposed -and $cold.Visible) {
    throw 'Double-click on File did not close the form.'
}
Write-Host 'File double-click closed the form without showing the backstage.'
if (-not $cold.IsDisposed) {
    $cold.Dispose()
}

# Office 2010 uses the same close, still without flashing the backstage.
$office2010 = [System.Activator]::CreateInstance($formType)
$office2010.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$office2010.Location = New-Object System.Drawing.Point 80, 80
$office2010.TopMost = $true
$office2010.Show()
$office2010.Activate()
$office2010.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
$modeType = $toolkit.GetType('Krypton.Toolkit.PaletteMode')
$apply = $formType.GetMethod('ApplyTheme')
[void]$apply.Invoke($office2010, @([Enum]::Parse($modeType, 'Office2010Blue')))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
$officePoint = Get-AppButtonScreenPoint -TargetForm $office2010
Invoke-LeftClickAt -X $officePoint.X -Y $officePoint.Y
Start-Sleep -Milliseconds 40
Invoke-LeftClickAt -X $officePoint.X -Y $officePoint.Y
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 150
if (Test-BackstageOverlayOpen) {
    throw 'Double-clicking the Office 2010 File tab showed the backstage.'
}
if (-not $office2010.IsDisposed -and $office2010.Visible) {
    throw 'Double-click on the Office 2010 File tab did not close the form.'
}
Write-Host 'Office 2010 File double-click closed the form without showing the backstage.'
if (-not $office2010.IsDisposed) {
    $office2010.Dispose()
}
$form.Show()
$form.Activate()
[System.Windows.Forms.Application]::DoEvents()

$showBackstage = $formType.GetMethod('ShowFileBackstage')
[void]$showBackstage.Invoke($form, @())
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300

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

$openPath = Join-Path $OutputDir '4552-backstage-file-open.png'
Save-UnitTestWindowPng -Form $overlay -Path $openPath

$form.TopMost = $false
$overlay.TopMost = $true
$overlay.Activate()
$overlay.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($overlay.Handle)
[System.Windows.Forms.Application]::DoEvents()

$ribbon = $formType.GetField('_ribbon', $flags).GetValue($form)
$appRect = $ribbon.GetType().GetMethod('AppButtonScreenRectangle', $flags).Invoke($ribbon, $null)
if ($appRect.IsEmpty) {
    throw 'File button screen rectangle is empty.'
}

$cx = [int]($appRect.X + ($appRect.Width / 2))
$cy = [int]($appRect.Y + ($appRect.Height / 2))
if (-not $overlay.Bounds.Contains($cx, $cy)) {
    throw "File button $cx,$cy is outside the backstage overlay $($overlay.Bounds)."
}

# One release on the covered File button must not close the form.
Invoke-LeftClickAt -X $cx -Y $cy
if ($form.IsDisposed -or -not $form.Visible) {
    throw 'A single click on File closed the form.'
}

$gap = [System.Windows.Forms.SystemInformation]::DoubleClickTime + 80
Start-Sleep -Milliseconds $gap
[System.Windows.Forms.Application]::DoEvents()

# Two releases inside the double-click time close the owner form.
Invoke-LeftClickAt -X $cx -Y $cy
Start-Sleep -Milliseconds 60
Invoke-LeftClickAt -X $cx -Y $cy
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
[System.Windows.Forms.Application]::DoEvents()

if (-not $form.IsDisposed -and $form.Visible) {
    throw 'Double-click on File did not close the form.'
}

Write-Host 'File double-click closed the form.'
if (-not $form.IsDisposed) {
    $form.Dispose()
}
