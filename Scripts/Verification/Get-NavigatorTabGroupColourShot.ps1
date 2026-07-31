<#
.SYNOPSIS
    Captures the CaptionIntegrated tab strip with two differently-coloured groups so the
    group-colour treatment (header wash + accent bar, member underline) can be eyeballed.

.DESCRIPTION
    Hosts TestForm's NavigatorFormIntegrationDemo, adds a second 'Personal' group on the
    'Settings' page (the demo seeds a 'Work' group on Home/Reports), brings the window to the
    foreground, screenshots the caption band, and writes a 3x nearest-neighbour zoom crop of
    the tab strip. Screenshots are written next to the built binaries and are NOT checked in.

.PARAMETER OutputDir
    Where to write the PNGs. Defaults to the resolved Bin\Debug\net472 directory.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\Verification\Get-NavigatorTabGroupColourShot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir
)

. (Join-Path $PSScriptRoot 'VerificationCommon.ps1')

$repoRoot = Get-VerificationRepoRoot
$bin = Get-VerificationBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDir) { $OutputDir = $bin }
Register-VerificationAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$form = [System.Activator]::CreateInstance($asm.GetType('TestForm.NavigatorFormIntegrationDemo'))
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(80, 80)

$form.add_Shown({
    $flags = [System.Reflection.BindingFlags]::NonPublic -bor [System.Reflection.BindingFlags]::Instance
    $integ = $form.GetType().GetField('kryptonNavigatorFormIntegrator1', $flags).GetValue($form)
    $nav = $form.GetType().GetField('kryptonNavigator1', $flags).GetValue($form)
    if ($nav.Pages.Count -ge 3) {
        [void]$integ.CreateGroup('Personal', [System.Drawing.Color]::Tomato, $nav.Pages[2])
    }
    $form.TopMost = $true
    $form.Activate()
    [System.Windows.Forms.Application]::DoEvents()
    $form.Refresh()
    Start-Sleep -Milliseconds 800
    [System.Windows.Forms.Application]::DoEvents()

    $b = $form.Bounds
    $band = New-Object System.Drawing.Bitmap([int]$b.Width, 130)
    $g = [System.Drawing.Graphics]::FromImage($band)
    $g.CopyFromScreen([int]$b.X, [int]$b.Y, 0, 0, (New-Object System.Drawing.Size([int]$b.Width, 130)))
    $g.Dispose()
    $bandPath = Join-Path $OutputDir 'nav-tabgroup-colour.png'
    $band.Save($bandPath, [System.Drawing.Imaging.ImageFormat]::Png)

    $crop = New-Object System.Drawing.Bitmap(320, 34)
    $cg = [System.Drawing.Graphics]::FromImage($crop)
    $cg.DrawImage($band, (New-Object System.Drawing.Rectangle(0, 0, 320, 34)), (New-Object System.Drawing.Rectangle(0, 0, 320, 34)), [System.Drawing.GraphicsUnit]::Pixel)
    $cg.Dispose()
    $zoom = New-Object System.Drawing.Bitmap(960, 102)
    $zg = [System.Drawing.Graphics]::FromImage($zoom)
    $zg.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::NearestNeighbor
    $zg.DrawImage($crop, 0, 0, 960, 102)
    $zg.Dispose()
    $zoomPath = Join-Path $OutputDir 'nav-tabgroup-colour-zoom.png'
    $zoom.Save($zoomPath, [System.Drawing.Imaging.ImageFormat]::Png)
    $crop.Dispose(); $zoom.Dispose(); $band.Dispose()

    Write-Host "saved $bandPath"
    Write-Host "saved $zoomPath"
    $form.Close()
})

[System.Windows.Forms.Application]::Run($form)
