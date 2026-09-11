<#
.SYNOPSIS
    Starts SchemeStripTextDemo and writes default / contrast PNGs for issue #1100.

.DESCRIPTION
    Launches Debug TestForm.exe with --demo SchemeStripTextDemo (does not LoadFrom toolkit
    assemblies). Captures Microsoft 365 Blue defaults, Contrast demo, the File dropdown, and
    KryptonContextMenu. Writes PNGs under Documents/PR/ (or -OutputDir). Those files are
    local PR assets and are not committed.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SchemeStripTextScreenshot.ps1
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

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes
Initialize-UnitTestNativeInput

$exe = Join-Path $bin 'TestForm.exe'
if (-not (Test-Path -LiteralPath $exe)) {
    throw "TestForm.exe not found in $bin. Build TestForm first."
}

function Wait-AutomationWindow {
    param(
        [string]$Name,
        [int]$TimeoutMs = 25000
    )

    $root = [System.Windows.Automation.AutomationElement]::RootElement
    $cond = New-Object System.Windows.Automation.PropertyCondition (
        [System.Windows.Automation.AutomationElement]::NameProperty, $Name)
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    while ($sw.ElapsedMilliseconds -lt $TimeoutMs) {
        $el = $root.FindFirst([System.Windows.Automation.TreeScope]::Children, $cond)
        if ($el) {
            return $el
        }
        Start-Sleep -Milliseconds 200
    }

    throw "Window '$Name' was not found within ${TimeoutMs}ms."
}

function Find-NamedElement {
    param(
        [System.Windows.Automation.AutomationElement]$Root,
        [string]$Name
    )

    $cond = New-Object System.Windows.Automation.PropertyCondition (
        [System.Windows.Automation.AutomationElement]::NameProperty, $Name)
    return $Root.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $cond)
}

function Invoke-NamedElement {
    param(
        [System.Windows.Automation.AutomationElement]$Root,
        [string]$Name
    )

    $el = Find-NamedElement -Root $Root -Name $Name
    if (-not $el) {
        throw "Automation element '$Name' was not found."
    }

    $pattern = [System.Windows.Automation.InvokePattern]::Pattern
    $inv = $el.GetCurrentPattern($pattern)
    $inv.Invoke()
}

function Save-ElementShot {
    param(
        [System.Windows.Automation.AutomationElement]$Window,
        [string]$Path,
        [int]$InflateX = 0,
        [int]$InflateY = 0
    )

    $rect = $Window.Current.BoundingRectangle
    $x = [Math]::Max(0, [int]$rect.X - $InflateX)
    $y = [Math]::Max(0, [int]$rect.Y - $InflateY)
    $w = [int]$rect.Width + (2 * $InflateX)
    $h = [int]$rect.Height + (2 * $InflateY)
    $bounds = [System.Drawing.Rectangle]::new($x, $y, $w, $h)
    $bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    try {
        $g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
        $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $g.Dispose()
        $bmp.Dispose()
    }

    Write-Host "Wrote $Path"
}

$proc = Start-Process -FilePath $exe -ArgumentList @('--demo', 'SchemeStripTextDemo') -WorkingDirectory $bin -PassThru
try {
    $window = Wait-AutomationWindow -Name 'Scheme Strip Text Colors (Issue #1100)'
    [void][UnitTestNative]::SetForegroundWindow([IntPtr]$proc.MainWindowHandle)
    Start-Sleep -Milliseconds 600

    $defaultPath = Join-Path $OutputDir '1100-scheme-strip-text-default.png'
    $contrastPath = Join-Path $OutputDir '1100-scheme-strip-text-contrast.png'
    $menuPath = Join-Path $OutputDir '1100-scheme-strip-text-contrast-menu.png'
    $contextPath = Join-Path $OutputDir '1100-scheme-strip-text-contrast-context.png'

    Save-ElementShot -Window $window -Path $defaultPath

    Invoke-NamedElement -Root $window -Name 'Contrast demo'
    Start-Sleep -Milliseconds 500
    $window = Wait-AutomationWindow -Name 'Scheme Strip Text Colors (Issue #1100)'
    Save-ElementShot -Window $window -Path $contrastPath

    Invoke-NamedElement -Root $window -Name 'File'
    Start-Sleep -Milliseconds 400
    Save-ElementShot -Window $window -Path $menuPath -InflateX 24 -InflateY 48

    $file = Find-NamedElement -Root $window -Name 'File'
    if ($file) {
        try {
            $expand = [System.Windows.Automation.ExpandCollapsePattern]::Pattern
            $file.GetCurrentPattern($expand).Collapse()
        }
        catch {
            [System.Windows.Forms.SendKeys]::SendWait('{ESC}')
        }
    }
    Start-Sleep -Milliseconds 200

    Invoke-NamedElement -Root $window -Name 'KryptonContextMenu'
    Start-Sleep -Milliseconds 500
    Save-ElementShot -Window $window -Path $contextPath -InflateX 80 -InflateY 80
}
finally {
    if ($proc -and -not $proc.HasExited) {
        $proc.Kill()
        $proc.WaitForExit(5000) | Out-Null
    }
}

Write-Host "Captured #1100 screenshots under $OutputDir"
