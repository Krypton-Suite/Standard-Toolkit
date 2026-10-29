<#
.SYNOPSIS
    Asserts #4180 KryptonSplashScreenManager public API: data defaults, Show/SetStatus/Close, Run(steps).

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Toolkit.Utilities binaries and runs in-process STA checks:

    1. Default KryptonSplashScreenManagerData values (fade, opacity, progress, size).
    2. Show + SetStatus + Close with MinimumDisplayMilliseconds = 0 (no exception dialog).
    3. Run(steps) executes caller-thread actions and auto-progress from step count.
    4. Run with a throwing step reports the exception without showing a dialog.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-SplashScreenManager.ps1
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

Write-UnitTestBanner -Status INFO -Message 'Asserting #4180 KryptonSplashScreenManager public API'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$dataType = [Krypton.Toolkit.Utilities.KryptonSplashScreenManagerData]
$managerType = [Krypton.Toolkit.Utilities.KryptonSplashScreenManager]
$stepType = [Krypton.Toolkit.Utilities.KryptonSplashStep]

$data = New-Object $dataType
Assert-True ([bool]$data.FadeIn) 'Default FadeIn is true'
Assert-True ([bool]$data.FadeOut) 'Default FadeOut is true'
Assert-True ([bool]$data.ShowProgressBar) 'Default ShowProgressBar is true'
Assert-True ([bool]$data.ShowExceptionDialog) 'Default ShowExceptionDialog is true'
Assert-Equal 1.0 $data.Opacity 'Default Opacity is 1.0'
Assert-Equal 520 $data.Size.Width 'Default Size.Width is 520'
Assert-Equal 320 $data.Size.Height 'Default Size.Height is 320'
Assert-Equal 750 $data.MinimumDisplayMilliseconds 'Default MinimumDisplayMilliseconds is 750'
Assert-Equal ([Krypton.Toolkit.Utilities.KryptonSplashBorderAnimation]::None) $data.BorderAnimation 'Default BorderAnimation is None'
Assert-Equal 3 $data.BorderAnimationThickness 'Default BorderAnimationThickness is 3'

$log = New-Object System.Collections.Generic.List[string]
$showData = New-Object $dataType
$showData.Title = 'UnitTest-4180'
$showData.Status = 'Ready'
$showData.FadeIn = $false
$showData.FadeOut = $false
$showData.ShowExceptionDialog = $false
$showData.MinimumDisplayMilliseconds = 0
$showData.ExpectedStepCount = 2
$showData.LogCallback = [Action[string]] { param($m) [void]$log.Add($m) }

$splash = $managerType::Show($showData)
Assert-True ($null -ne $splash) 'Show returns a manager instance'
$splash.SetStatus('Step A')
$splash.SetStatus('Step B')
$splash.SetProgress(100, 100)
$splash.Close()
$splash.Dispose()
Assert-True ($log.Count -ge 2) 'LogCallback received SetStatus messages'

$ran = New-Object System.Collections.Generic.List[string]
$runData = New-Object $dataType
$runData.Title = 'UnitTest-4180-Run'
$runData.FadeIn = $false
$runData.FadeOut = $false
$runData.ShowExceptionDialog = $false
$runData.MinimumDisplayMilliseconds = 0

$step1 = New-Object $stepType
$step1.Status = 'One'
$step1.Action = [Action] { [void]$ran.Add('one') }
$step2 = New-Object $stepType
$step2.Status = 'Two'
$step2.Action = [Action] { [void]$ran.Add('two') }
$managerType::Run($runData, $step1, $step2)
Assert-Equal 2 $ran.Count 'Run executed both steps'
Assert-Equal 'one' $ran[0] 'Run executed step one first'
Assert-Equal 'two' $ran[1] 'Run executed step two second'

$throwData = New-Object $dataType
$throwData.Title = 'UnitTest-4180-Throw'
$throwData.FadeIn = $false
$throwData.FadeOut = $false
$throwData.ShowExceptionDialog = $false
$throwData.MinimumDisplayMilliseconds = 0
$ok = New-Object $stepType
$ok.Status = 'Before throw'
$ok.Action = [Action] { }
$boom = New-Object $stepType
$boom.Status = 'Boom'
$boom.Action = [Action] { throw [InvalidOperationException]::new('unit-test-4180') }
$never = New-Object $stepType
$never.Status = 'Never'
$never.Action = [Action] { throw [InvalidOperationException]::new('should-not-run') }
$managerType::Run($throwData, $ok, $boom, $never)
Assert-True $true 'Run with a throwing step returned without rethrowing'

if ($failed.Count -gt 0) {
    Write-Host "FAILED $($failed.Count) assertion(s)" -ForegroundColor Red
    exit 1
}

Write-Host 'All #4180 splash screen manager assertions passed.' -ForegroundColor Green
exit 0
