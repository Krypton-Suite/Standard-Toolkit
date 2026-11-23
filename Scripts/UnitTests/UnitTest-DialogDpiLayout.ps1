<#
.SYNOPSIS
    Asserts #4465: dialog layout releases fixed chrome and clamps to the working area.

.DESCRIPTION
    Loads Krypton.Toolkit and checks KryptonDialogLayout plus KryptonInputBox:
    a fixed border becomes sizable, an oversized window is clamped to the working area,
    and a long input prompt produces a taller client than a one-line prompt.

    Exit code 0 on success; non-zero on failure.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DialogDpiLayout.ps1
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

$toolkitPath = Join-Path $bin 'Krypton.Toolkit.dll'
if (-not (Test-Path -LiteralPath $toolkitPath)) {
    throw "Krypton.Toolkit.dll was not found at '$toolkitPath'."
}

[void][System.Reflection.Assembly]::LoadFrom($toolkitPath)

$layoutType = [Krypton.Toolkit.KryptonManager].Assembly.GetType('Krypton.Toolkit.KryptonDialogLayout')
if ($null -eq $layoutType) {
    throw 'KryptonDialogLayout was not found in Krypton.Toolkit.'
}

$flags = [System.Reflection.BindingFlags]'Public,NonPublic,Static'
$release = $layoutType.GetMethod('ReleaseFixedChrome', $flags)
$clamp = $layoutType.GetMethod('ClampToWorkingArea', $flags)
if ($null -eq $release -or $null -eq $clamp) {
    throw 'KryptonDialogLayout methods were not found.'
}

$fixed = New-Object System.Windows.Forms.Form
$fixed.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::FixedDialog
$fixed.MaximumSize = New-Object System.Drawing.Size 400, 300
$release.Invoke($null, [object[]]([System.Windows.Forms.Form]$fixed))
if ($fixed.FormBorderStyle -ne [System.Windows.Forms.FormBorderStyle]::Sizable) {
    throw "ReleaseFixedChrome left the border as $($fixed.FormBorderStyle)."
}

if (-not $fixed.MaximumSize.IsEmpty) {
    throw 'ReleaseFixedChrome did not clear MaximumSize.'
}

$fixed.Dispose()

$huge = New-Object System.Windows.Forms.Form
$huge.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$huge.Location = New-Object System.Drawing.Point 40, 40
$huge.ClientSize = New-Object System.Drawing.Size 8000, 6000
$huge.Show()
try {
    [System.Windows.Forms.Application]::DoEvents()
    $working = [System.Windows.Forms.Screen]::FromControl($huge).WorkingArea
    $clamp.Invoke($null, [object[]]([System.Windows.Forms.Form]$huge, $null, $false))
    if ($huge.Width -gt $working.Width -or $huge.Height -gt $working.Height) {
        throw "Clamped window $($huge.Width)x$($huge.Height) is larger than the working area $($working.Width)x$($working.Height)."
    }

    if ($huge.Left -lt $working.Left -or $huge.Top -lt $working.Top) {
        throw 'Clamped window sits outside the working area.'
    }
}
finally {
    $huge.Close()
    $huge.Dispose()
}

function New-InputForm([string]$prompt) {
    $data = New-Object Krypton.Toolkit.KryptonInputBoxData
    $data.Caption = 'Layout check'
    $data.Prompt = $prompt
    $form = New-Object Krypton.Toolkit.VisualInputBoxForm $data
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 60, 60
    $form.Show()
    [System.Windows.Forms.Application]::DoEvents()
    return $form
}

$shortForm = New-InputForm 'Name'
$longPrompt = ('This prompt is long enough to wrap onto several lines. ' * 12)
$longForm = New-InputForm $longPrompt
try {
    $working = [System.Windows.Forms.Screen]::FromControl($longForm).WorkingArea
    if ($longForm.ClientSize.Height -le $shortForm.ClientSize.Height) {
        throw "Long prompt height $($longForm.ClientSize.Height) was not taller than the short prompt $($shortForm.ClientSize.Height)."
    }

    if ($longForm.Width -gt $working.Width -or $longForm.Height -gt $working.Height) {
        throw "Input box $($longForm.Width)x$($longForm.Height) exceeds the working area $($working.Width)x$($working.Height)."
    }

    $label = $longForm.GetType().GetField('_labelPrompt', [System.Reflection.BindingFlags]'Instance,NonPublic').GetValue($longForm)
    $labelWidth = [Math]::Max(1, $label.ClientSize.Width)
    $measureSize = New-Object System.Drawing.Size $labelWidth, ([int]::MaxValue)
    $needed = [System.Windows.Forms.TextRenderer]::MeasureText(
        [string]$label.Text,
        [System.Drawing.Font]$label.Font,
        $measureSize,
        ([System.Windows.Forms.TextFormatFlags]::WordBreak -bor [System.Windows.Forms.TextFormatFlags]::TextBoxControl))
    if ($label.Height + 2 -lt $needed.Height) {
        throw "Prompt label height $($label.Height) clips measured text height $($needed.Height)."
    }

    $flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
    $ok = $longForm.GetType().GetField('_buttonOk', $flags).GetValue($longForm)
    $cancel = $longForm.GetType().GetField('_buttonCancel', $flags).GetValue($longForm)
    $gap = $cancel.Left - $ok.Right
    if ($gap -lt 8 -or $gap -gt 12) {
        throw "OK/Cancel gap is $gap pixels."
    }

    if ([Math]::Abs($ok.Width - $cancel.Width) -gt 1) {
        throw "OK width $($ok.Width) and Cancel width $($cancel.Width) do not match."
    }
}
finally {
    $shortForm.Close()
    $longForm.Close()
    $shortForm.Dispose()
    $longForm.Dispose()
}

Write-Output 'Dialog DPI layout checks passed.'
