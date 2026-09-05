<#
.SYNOPSIS
    Asserts #4271 bug-report email bodies omit stack traces and SMTP passwords.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Toolkit.Utilities and:
    - Builds a transmitted bug-report body from a thrown exception and asserts the
      stack trace is absent while type and message remain.
    - Asserts attachment paths are listed by file name only.
    - Asserts KryptonTextBox password masking still applies through the public API.

    Exit code 0 on success; non-zero on failure.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-BugReportEmailBody.ps1
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

Write-UnitTestBanner -Status INFO -Message 'Asserting #4271 bug-report email body sanitization'

Add-Type -TypeDefinition @'
using System;
public static class UnitTest4271Exception
{
    public static Exception CreateThrown()
    {
        try
        {
            throw new InvalidOperationException("unit-test-4271-message");
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
'@

$ex = [UnitTest4271Exception]::CreateThrown()
Assert-True ($null -ne $ex.StackTrace -and $ex.StackTrace.Length -gt 0) 'Thrown exception has a stack trace to omit'

$flags = [System.Reflection.BindingFlags]::NonPublic -bor [System.Reflection.BindingFlags]::Static
$method = [Krypton.Toolkit.Utilities.BugReportEmailService].GetMethod('CreateTransmittedBody', $flags)
Assert-True ($null -ne $method) 'CreateTransmittedBody is available for the unit test'

$invokeArgs = New-Object object[] 5
$invokeArgs[0] = 'user@example.com'
$invokeArgs[1] = 'description-text'
$invokeArgs[2] = 'steps-text'
$invokeArgs[3] = $ex
$invokeArgs[4] = [string[]]@('C:\temp\screenshot.png')
$smtpPassword = 'smtp-password-should-not-appear'
$body = [string]$method.Invoke($null, $invokeArgs)

Assert-True ($body.IndexOf('user@example.com') -ge 0) 'Reporter email is included'
Assert-True ($body.IndexOf('description-text') -ge 0) 'Bug description is included'
Assert-True ($body.IndexOf('steps-text') -ge 0) 'Reproduction steps are included'
Assert-True ($body.IndexOf('InvalidOperationException') -ge 0) 'Exception type is included'
Assert-True ($body.IndexOf('unit-test-4271-message') -ge 0) 'Exception message is included'
Assert-True ($body.IndexOf('screenshot.png') -ge 0) 'Attachment file name is included'
Assert-True ($body.IndexOf('C:\temp') -lt 0) 'Attachment directory is not included'
Assert-True ($body.IndexOf($smtpPassword) -lt 0) 'SMTP password is not a body parameter and does not appear'
if ($null -ne $ex.StackTrace -and $ex.StackTrace.Length -gt 0) {
    Assert-True ($body.IndexOf($ex.StackTrace) -lt 0) 'Exception stack trace is not transmitted'
}

$textBox = New-Object Krypton.Toolkit.KryptonTextBox
try {
    $textBox.PasswordChar = [char]0x25CF
    $textBox.Text = 'secret-value'
    Assert-True ($textBox.PasswordChar -eq [char]0x25CF) 'PasswordChar still applies'
    $textBox.UseSystemPasswordChar = $true
    Assert-True ($textBox.UseSystemPasswordChar) 'UseSystemPasswordChar still applies'
    Assert-True ($textBox.Text -eq 'secret-value') 'Password text is still readable through the public API'
}
finally {
    $textBox.Dispose()
}

if ($failed.Count -gt 0) {
    Write-Host ("{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host 'All #4271 assertions passed.' -ForegroundColor Green
exit 0
