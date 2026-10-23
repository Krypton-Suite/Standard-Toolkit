<#
.SYNOPSIS
    Drags between two caption-relative points on the hosted NavigatorFormIntegrationDemo.

.DESCRIPTION
    Uses UI Automation to locate the host process window, then synthesises a left-button
    drag. Captures before / during / after screenshots into the bin folder (or -OutputDir).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\Invoke-CaptionTabDrag.ps1 `
        -HostPid 12345 -FromX 200 -FromY 14 -ToX 80 -ToY 14 -Tag join
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [int]$HostPid,

    [Parameter(Mandatory = $true)]
    [int]$FromX,

    [Parameter(Mandatory = $true)]
    [int]$FromY,

    [Parameter(Mandatory = $true)]
    [int]$ToX,

    [Parameter(Mandatory = $true)]
    [int]$ToY,

    [string]$Tag = 'drag',
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
    $OutputDir = $bin
}
if (-not (Test-Path -LiteralPath $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput

$root = [System.Windows.Automation.AutomationElement]::RootElement
$cond = New-Object System.Windows.Automation.PropertyCondition(
    [System.Windows.Automation.AutomationElement]::ProcessIdProperty, $HostPid)

$demo = $null
foreach ($w in $root.FindAll([System.Windows.Automation.TreeScope]::Children, $cond)) {
    if ($w.Current.BoundingRectangle.Width -gt 300) {
        $demo = $w
    }
}
if (-not $demo) {
    throw "No demo window found for process $HostPid."
}

$dr = $demo.Current.BoundingRectangle
[void][UnitTestNative]::SetForegroundWindow([IntPtr]$demo.Current.NativeWindowHandle)
Start-Sleep -Milliseconds 500

function Save-Shot {
    param([string]$Name, [int]$Height)
    $width = [int]$dr.Width
    $bmp = New-Object System.Drawing.Bitmap($width, $Height)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.CopyFromScreen([int]$dr.X, [int]$dr.Y, 0, 0, (New-Object System.Drawing.Size($width, $Height)))
    $g.Dispose()
    $path = Join-Path $OutputDir $Name
    $bmp.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "saved $path"
}

function Get-StatusText {
    $textCond = New-Object System.Windows.Automation.PropertyCondition(
        [System.Windows.Automation.AutomationElement]::ControlTypeProperty,
        [System.Windows.Automation.ControlType]::Text)
    $all = $demo.FindAll([System.Windows.Automation.TreeScope]::Descendants, $textCond)
    foreach ($t in $all) {
        if ($t.Current.Name -like '*PageGroup*') {
            return $t.Current.Name
        }
    }
    return '(status not found)'
}

Write-Host "STATUS BEFORE:" (Get-StatusText)
Save-Shot -Name "shot-$Tag-before.png" -Height 140

$sx = [int]($dr.X + $FromX)
$sy = [int]($dr.Y + $FromY)
$ex = [int]($dr.X + $ToX)
$ey = [int]($dr.Y + $ToY)
Invoke-UnitTestDrag -FromX $sx -FromY $sy -ToX $ex -ToY $ey

# Mid-drag shot is approximate; capture after for the authoritative state.
Save-Shot -Name "shot-$Tag-after.png" -Height 460
Write-Host "STATUS AFTER:" (Get-StatusText)

$wins = $root.FindAll([System.Windows.Automation.TreeScope]::Children, $cond)
Write-Host "windows now:" $wins.Count
foreach ($w in $wins) {
    Write-Host "  - '$($w.Current.Name)' rect=$($w.Current.BoundingRectangle)"
}
