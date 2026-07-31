<#
.SYNOPSIS
    Tears out a caption tab and drags it back to verify remerge into the source window.

.DESCRIPTION
    Expects Start-NavigatorFormIntegrationHost.ps1 to already be running. Tears out the
    rightmost demo tab (Settings), then drags it onto the main caption strip. With
    "Close empty window" enabled, a successful remerge leaves a single window.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\Verification\Test-NavigatorCaptionTabRemerge.ps1 -HostPid 12345
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [int]$HostPid,

    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'VerificationCommon.ps1')

$repoRoot = Get-VerificationRepoRoot
$bin = Get-VerificationBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
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
Initialize-VerificationNativeInput

$root = [System.Windows.Automation.AutomationElement]::RootElement
$cond = New-Object System.Windows.Automation.PropertyCondition(
    [System.Windows.Automation.AutomationElement]::ProcessIdProperty, $HostPid)

function Get-DemoWindows {
    $list = @()
    foreach ($w in $root.FindAll([System.Windows.Automation.TreeScope]::Children, $cond)) {
        $r = $w.Current.BoundingRectangle
        if ($r.Width -gt 200 -and $r.Height -gt 100) {
            $list += $w
        }
    }
    return $list
}

function Save-Shot {
    param(
        [System.Windows.Automation.AutomationElement]$Win,
        [string]$Name,
        [int]$Height
    )
    $dr = $Win.Current.BoundingRectangle
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

$wins = Get-DemoWindows
if ($wins.Count -lt 1) {
    throw "No demo window found for process $HostPid."
}

$main = $wins[0]
$mr = $main.Current.BoundingRectangle
[void][VerificationNative]::SetForegroundWindow([IntPtr]$main.Current.NativeWindowHandle)
Start-Sleep -Milliseconds 500

Write-Host '=== BEFORE TEAR-OUT ==='
Write-Host "windows: $($wins.Count) main=$mr"
Save-Shot -Win $main -Name 'shot-remerge-1-before.png' -Height 140

# Tear out Settings (rightmost tab ~ x=200 relative to caption)
Invoke-VerificationDrag `
    -FromX ([int]($mr.X + 200)) -FromY ([int]($mr.Y + 14)) `
    -ToX ([int]($mr.X + 950)) -ToY ([int]($mr.Y + 400))

$wins = Get-DemoWindows
Write-Host '=== AFTER TEAR-OUT ==='
Write-Host "windows: $($wins.Count)"
foreach ($w in $wins) {
    $r = $w.Current.BoundingRectangle
    Write-Host "  - rect=$r"
    Save-Shot -Win $w -Name ("shot-remerge-2-torn-{0}.png" -f [int]$r.X) -Height 160
}

if ($wins.Count -lt 2) {
    Write-Host 'FAIL: tear-out did not create a second window'
    exit 2
}

$floated = $null
$main2 = $null
foreach ($w in $wins) {
    $r = $w.Current.BoundingRectangle
    if ($r.X -gt ($mr.X + 200)) {
        $floated = $w
    }
    else {
        $main2 = $w
    }
}
if (-not $floated) {
    $floated = $wins | Sort-Object { $_.Current.BoundingRectangle.X } -Descending | Select-Object -First 1
}
if (-not $main2) {
    $main2 = $wins | Where-Object { -not [object]::ReferenceEquals($_, $floated) } | Select-Object -First 1
}

$fr = $floated.Current.BoundingRectangle
$m2 = $main2.Current.BoundingRectangle
Write-Host "floated=$fr"
Write-Host "main2=$m2"

[void][VerificationNative]::SetForegroundWindow([IntPtr]$floated.Current.NativeWindowHandle)
Start-Sleep -Milliseconds 400

Invoke-VerificationDrag `
    -FromX ([int]($fr.X + 60)) -FromY ([int]($fr.Y + 14)) `
    -ToX ([int]($m2.X + 90)) -ToY ([int]($m2.Y + 14))

$wins = Get-DemoWindows
Write-Host '=== AFTER REMERGE ==='
Write-Host "windows: $($wins.Count)"
foreach ($w in $wins) {
    $r = $w.Current.BoundingRectangle
    Write-Host "  - rect=$r"
    Save-Shot -Win $w -Name ("shot-remerge-3-after-{0}.png" -f [int]$r.X) -Height 180
}

if ($wins.Count -eq 1) {
    Write-Host 'PASS: remerged into single window (floated closed)'
    exit 0
}

Write-Host 'CHECK: expected a single window after remerge'
exit 3
