<#
.SYNOPSIS
    Discovers and runs all CI-oriented Scripts/UnitTests assert scripts (UnitTest-*.ps1).

.DESCRIPTION
    Future-proof contract (see Scripts/UnitTests/README.md):

    * Discover every UnitTest-*.ps1 under Scripts/UnitTests (recursive).
    * Each UnitTest-*.ps1 MUST declare a comment marker near the top:
        # UnitTest-CI: include   - run in CI / Invoke-AllUnitTests
        # UnitTest-CI: exclude   - interactive; never auto-run
    * -Strict (or env UNITTEST_CI=1) fails when any UnitTest-*.ps1 lacks a marker,
      or when zero include scripts are discovered.
    * Each include script runs in a fresh STA powershell child with
      -Configuration / -TargetFramework / -BinDir forwarded when present.
    * Emits clear PASS/FAIL/SKIP banners, GitHub Actions notices/errors, and a job summary.

    Exit code is the number of failing scripts (0 = all passed).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -File .\Scripts\UnitTests\Invoke-AllUnitTests.ps1 -Strict
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,

    # Fail when markers are missing or no CI tests are discovered (CI should pass -Strict).
    [switch]$Strict,

    # Optional per-script timeout in seconds (0 = no timeout).
    [int]$TimeoutSeconds = 0
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

if ($env:UNITTEST_CI -eq '1') {
    $Strict = $true
}

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir

Write-UnitTestBanner -Status INFO -Message "Unit tests starting (Configuration=$Configuration, TFM=$TargetFramework, Strict=$Strict)"
Write-UnitTestBanner -Status INFO -Message "Bin: $bin"
Write-Host ""

$classification = Get-UnitTestScriptClassification -Root $PSScriptRoot
$passedNames = New-Object System.Collections.Generic.List[string]
$failedNames = New-Object System.Collections.Generic.List[string]
$skippedNames = New-Object System.Collections.Generic.List[string]
foreach ($item in $classification.Excluded) {
    $skippedNames.Add($item.Name)
}

if ($classification.Undeclared.Count -gt 0) {
    $list = ($classification.Undeclared | ForEach-Object { $_.Name }) -join ', '
    $message = "UnitTest-*.ps1 scripts missing '# UnitTest-CI: include|exclude' marker: $list"
    if ($Strict) {
        Write-UnitTestBanner -Status FAIL -Message $message
        Write-GitHubError -Message $message
        Write-UnitTestJobSummary -Title 'Krypton Unit Tests' -Overall 'FAIL' `
            -Passed @() -Failed @($message) -Skipped $skippedNames.ToArray() `
            -UndeclaredCount $classification.Undeclared.Count -BinPath $bin
        exit 1
    }

    Write-UnitTestBanner -Status SKIP -Message "$message (pass -Strict / UNITTEST_CI=1 to fail)"
    Write-GitHubWarning -Message $message
}

if ($skippedNames.Count -gt 0) {
    Write-UnitTestBanner -Status SKIP -Message ("UnitTest-CI:exclude - " + ($skippedNames -join ', '))
}

$ciTests = @($classification.Included | Sort-Object FullName)
if ($ciTests.Count -eq 0) {
    $message = "No UnitTest-CI:include UnitTest-*.ps1 scripts found under $PSScriptRoot"
    if ($Strict) {
        Write-UnitTestBanner -Status FAIL -Message $message
        Write-GitHubError -Message $message
        Write-UnitTestJobSummary -Title 'Krypton Unit Tests' -Overall 'FAIL' `
            -Passed @() -Failed @($message) -Skipped $skippedNames.ToArray() `
            -UndeclaredCount $classification.Undeclared.Count -BinPath $bin
        exit 1
    }

    Write-UnitTestBanner -Status SKIP -Message $message
    Write-UnitTestJobSummary -Title 'Krypton Unit Tests' -Overall 'PASS (nothing to run)' `
        -Passed @() -Failed @() -Skipped $skippedNames.ToArray() `
        -UndeclaredCount $classification.Undeclared.Count -BinPath $bin
    exit 0
}

Write-UnitTestBanner -Status INFO -Message "Running $($ciTests.Count) include script(s)"
Write-Host ""

foreach ($test in $ciTests) {
    $relative = $test.FullName.Substring($PSScriptRoot.Length).TrimStart('\', '/')
    Write-Host "===== $relative =====" -ForegroundColor Cyan

    $argList = @(
        '-NoProfile'
        '-ExecutionPolicy', 'Bypass'
        '-STA'
        '-File', $test.FullName
        '-Configuration', $Configuration
        '-TargetFramework', $TargetFramework
        '-BinDir', $(if ($BinDir) { $BinDir } else { $bin })
    )

    $proc = $null
    try {
        # Prefer ProcessStartInfo over Start-Process: Windows PowerShell leaves
        # Start-Process.ExitCode null after WaitForExit when streams are redirected
        # unless -Wait was also passed (which cannot express a soft timeout cleanly).
        $startInfo = New-Object System.Diagnostics.ProcessStartInfo
        $startInfo.FileName = 'powershell.exe'
        $startInfo.Arguments = ($argList | ForEach-Object {
            if ($_ -match '[\s"]') { '"{0}"' -f ($_ -replace '\\', '\\' -replace '"', '\"') } else { $_ }
        }) -join ' '
        $startInfo.UseShellExecute = $false
        $startInfo.RedirectStandardOutput = $true
        $startInfo.RedirectStandardError = $true
        $startInfo.CreateNoWindow = $true

        $proc = New-Object System.Diagnostics.Process
        $proc.StartInfo = $startInfo
        [void]$proc.Start()

        $stdoutTask = $proc.StandardOutput.ReadToEndAsync()
        $stderrTask = $proc.StandardError.ReadToEndAsync()

        $timedOut = $false
        if ($TimeoutSeconds -gt 0) {
            if (-not $proc.WaitForExit($TimeoutSeconds * 1000)) {
                $timedOut = $true
                try { $proc.Kill() } catch { }
                [void]$proc.WaitForExit()
            }
        }
        else {
            $proc.WaitForExit()
        }

        $stdout = $stdoutTask.GetAwaiter().GetResult()
        $stderr = $stderrTask.GetAwaiter().GetResult()
        if (-not [string]::IsNullOrEmpty($stdout)) {
            foreach ($line in ($stdout -split "`r?`n")) {
                Write-Host $line
            }
        }
        if (-not [string]::IsNullOrEmpty($stderr)) {
            foreach ($line in ($stderr -split "`r?`n")) {
                Write-Host $line
            }
        }

        if ($timedOut) {
            $failedNames.Add("$relative (timeout after ${TimeoutSeconds}s)")
            Write-UnitTestBanner -Status FAIL -Message "$relative - FAIL (timeout after ${TimeoutSeconds}s)"
            Write-GitHubError -Message "Unit test timed out after ${TimeoutSeconds}s" -File $relative
            Write-Host ""
            continue
        }

        $code = $proc.ExitCode

        if ($code -ne 0) {
            $failedNames.Add("$relative (exit $code)")
            Write-UnitTestBanner -Status FAIL -Message "$relative - FAIL (exit $code)"
            Write-GitHubError -Message "Unit test failed with exit code $code" -File $relative
        }
        else {
            $passedNames.Add($relative)
            Write-UnitTestBanner -Status PASS -Message "$relative - PASS"
            Write-GitHubNotice -Message "Unit test passed" -File $relative
        }
    }
    finally {
        if ($proc) {
            if (-not $proc.HasExited) {
                try { $proc.Kill() } catch { }
            }
            $proc.Dispose()
        }
    }

    Write-Host ""
}

$overall = if ($failedNames.Count -gt 0) { 'FAIL' } else { 'PASS' }
Write-UnitTestJobSummary -Title 'Krypton Unit Tests' -Overall $overall `
    -Passed $passedNames.ToArray() `
    -Failed $failedNames.ToArray() `
    -Skipped $skippedNames.ToArray() `
    -UndeclaredCount $classification.Undeclared.Count `
    -BinPath $bin

if ($failedNames.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "Unit tests FAILED - $($passedNames.Count) passed, $($failedNames.Count) failed"
    exit $failedNames.Count
}

Write-UnitTestBanner -Status PASS -Message "Unit tests PASSED - $($passedNames.Count) passed, 0 failed"
exit 0
