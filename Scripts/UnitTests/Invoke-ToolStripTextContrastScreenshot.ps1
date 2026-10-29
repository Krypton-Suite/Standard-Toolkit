<#
.SYNOPSIS
    Hosts Bug4373ToolStripTextContrastDemo and writes theme PNGs for issue #4373.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows Bug4373ToolStripTextContrastDemo,
    applies Office 2010 White, Office 2007 Black, Office 2010 Black, and Visual Studio 2010,
    and writes Documents/PR/4373-toolstrip-text-*.png. Those files are local PR assets and are not committed.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ToolStripTextContrastScreenshot.ps1
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
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Bug4373ToolStripTextContrastDemo')
if (-not $formType) {
    throw 'Type TestForm.Bug4373ToolStripTextContrastDemo was not found in TestForm.exe.'
}

$shots = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Office2010White; Name = '4373-toolstrip-text-office2010-white.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Office2007Black; Name = '4373-toolstrip-text-office2007-black.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Office2010Black; Name = '4373-toolstrip-text-office2010-black.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::VisualStudio2010Render2010; Name = '4373-toolstrip-text-vs2010-2010.png' }
)

function Save-FormShot {
    param(
        [System.Windows.Forms.Form]$Window,
        [string]$Path
    )

    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 600
    [System.Windows.Forms.Application]::DoEvents()
    if ($Window.Handle -ne [IntPtr]::Zero) {
        [void][UnitTestNative]::SetForegroundWindow($Window.Handle)
        [System.Windows.Forms.Application]::DoEvents()
    }
    $bounds = $Window.Bounds
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

$working = [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea
$origin = New-Object System.Drawing.Point ($working.Left + 40), ($working.Top + 40)

$manager = New-Object Krypton.Toolkit.KryptonManager
try {
    foreach ($shot in $shots) {
        $manager.GlobalPaletteMode = $shot.Mode
        $form = [System.Activator]::CreateInstance($formType)
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = $origin
        $form.TopMost = $true
        $form.Show()
        $form.Activate()
        $form.BringToFront()
        $path = Join-Path $OutputDir $shot.Name
        Save-FormShot -Window $form -Path $path
        $form.TopMost = $false
        $form.Close()
        $form.Dispose()
        [System.Windows.Forms.Application]::DoEvents()
    }
}
finally {
    $manager.Dispose()
}

Write-Host "Captured #4373 screenshots under $OutputDir"
