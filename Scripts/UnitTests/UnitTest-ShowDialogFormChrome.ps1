<#
.SYNOPSIS
    Asserts #4584 bordered KryptonForm applies custom chrome before first show.

.DESCRIPTION
    Creates Sizable and FixedDialog KryptonForm instances, creates the HWND without
    Show, and asserts VisualForm custom chrome is already enabled (the #2922/#4584
    early-chrome path). Then ShowDialog completes without error.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ShowDialogFormChrome.ps1
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

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$testFormAsm = Join-Path $bin 'TestForm.exe'
if (-not (Test-Path -LiteralPath $testFormAsm)) {
    throw "TestForm.exe not found in $bin. Build TestForm first."
}
[void][System.Reflection.Assembly]::LoadFrom($testFormAsm)

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

function Get-VisualFormChromeEnabled {
    param([System.Windows.Forms.Form]$Form)

    $flags = [System.Reflection.BindingFlags]'Instance, NonPublic'
    $type = $Form.GetType()
    while ($null -ne $type -and $type.Name -ne 'VisualForm') {
        $type = $type.BaseType
    }
    if ($null -eq $type) {
        throw 'VisualForm base type not found.'
    }

    $field = $type.GetField('_useThemeFormChromeBorderWidth', $flags)
    if ($null -eq $field) {
        throw '_useThemeFormChromeBorderWidth field not found on VisualForm.'
    }

    return [bool]$field.GetValue($Form)
}

function Test-BorderedEarlyChrome {
    param(
        [System.Windows.Forms.FormBorderStyle]$BorderStyle,
        [string]$Label
    )

    $form = New-Object Krypton.Toolkit.KryptonForm
    try {
        $form.FormBorderStyle = $BorderStyle
        $form.Text = "Early chrome $Label"
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = New-Object System.Drawing.Point 40, 40
        $form.Size = New-Object System.Drawing.Size 360, 200
        $form.ShowInTaskbar = $false

        # Create HWND without showing — OnHandleCreated must apply chrome (#4584).
        $handle = $form.Handle
        Assert-True ($handle -ne [IntPtr]::Zero) "$Label handle created"
        Assert-True (-not $form.Visible) "$Label not visible before Show"
        Assert-True (Get-VisualFormChromeEnabled -Form $form) "$Label VisualForm custom chrome enabled before first paint"

        $owner = New-Object System.Windows.Forms.Form
        try {
            $owner.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
            $owner.Location = New-Object System.Drawing.Point 20, 20
            $owner.Size = New-Object System.Drawing.Size 200, 120
            $owner.Show()
            [System.Windows.Forms.Application]::DoEvents()

            $form.add_Shown({
                param($sender, $e)
                $sender.DialogResult = [System.Windows.Forms.DialogResult]::OK
                $sender.Close()
            })

            $result = $form.ShowDialog($owner)
            Assert-True ($result -eq [System.Windows.Forms.DialogResult]::OK) "$Label ShowDialog completed"
            Assert-True (Get-VisualFormChromeEnabled -Form $form) "$Label custom chrome still enabled after ShowDialog"
        }
        finally {
            $owner.Close()
            $owner.Dispose()
        }
    }
    finally {
        $form.Close()
        $form.Dispose()
    }
}

[System.Windows.Forms.Application]::EnableVisualStyles()

Test-BorderedEarlyChrome -BorderStyle ([System.Windows.Forms.FormBorderStyle]::Sizable) -Label 'Sizable'
Test-BorderedEarlyChrome -BorderStyle ([System.Windows.Forms.FormBorderStyle]::FixedDialog) -Label 'FixedDialog'

# Smoke: demo type loads and can open a dialog programmatically.
$demo = [System.Activator]::CreateInstance([TestForm.Bug4584KryptonFormShowDialogFlickerDemo])
try {
    $demo.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $demo.Location = New-Object System.Drawing.Point 60, 60
    $demo.Show()
    [System.Windows.Forms.Application]::DoEvents()
    Assert-True ($demo.Visible) 'Bug4584 demo host visible'
}
finally {
    $demo.Close()
    $demo.Dispose()
}

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'All #4584 ShowDialog early-chrome assertions passed.' -ForegroundColor Green
exit 0
