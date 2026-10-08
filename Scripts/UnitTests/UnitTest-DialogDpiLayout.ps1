# UnitTest-CI: include
# Requires -STA. Checks #4465 dialog chrome, working-area clamp, and input-box growth.
# Usage (from repo root):
#   powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DialogDpiLayout.ps1

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
if (-not (Test-Path -LiteralPath (Join-Path $repoRoot 'Source'))) {
    $repoRoot = Split-Path -Parent $PSScriptRoot
}

$tfm = 'net472'
if ($args -contains '-TargetFramework') {
    $tfm = $args[$args.IndexOf('-TargetFramework') + 1]
}

$toolkitDll = Join-Path $repoRoot "Bin\Debug\$tfm\Krypton.Toolkit.dll"
if (-not (Test-Path -LiteralPath $toolkitDll)) {
    Write-Error "Build Debug $tfm first. Missing: $toolkitDll"
}

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$binDir = Split-Path -Parent $toolkitDll
$resolveHandler = [System.ResolveEventHandler] {
    param($sender, $e)
    $simple = (New-Object System.Reflection.AssemblyName $e.Name).Name
    $candidate = Join-Path $binDir ($simple + '.dll')
    if (Test-Path -LiteralPath $candidate) {
        return [System.Reflection.Assembly]::LoadFrom($candidate)
    }
    return $null
}
[AppDomain]::CurrentDomain.add_AssemblyResolve($resolveHandler)
[void][System.Reflection.Assembly]::LoadFrom($toolkitDll)
$toolkitAssembly = [AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GetName().Name -eq 'Krypton.Toolkit' } | Select-Object -First 1
$layout = $toolkitAssembly.GetTypes() | Where-Object { $_.FullName -eq 'Krypton.Toolkit.KryptonDialogLayout' } | Select-Object -First 1
if (-not $layout) {
    throw 'KryptonDialogLayout was not found in Krypton.Toolkit.'
}
$release = $layout.GetMethod('ReleaseFixedChrome')
$clamp = $layout.GetMethod('ClampToWorkingArea')

$fixed = New-Object System.Windows.Forms.Form
$fixed.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::FixedDialog
$fixed.MaximumSize = New-Object System.Drawing.Size 400, 300
[void]$release.Invoke($null, [object[]]([System.Windows.Forms.Form]$fixed))
if ($fixed.FormBorderStyle -ne [System.Windows.Forms.FormBorderStyle]::Sizable) {
    throw "ReleaseFixedChrome left border as $($fixed.FormBorderStyle)."
}
if (-not $fixed.MaximumSize.IsEmpty) {
    throw "ReleaseFixedChrome left MaximumSize $($fixed.MaximumSize)."
}
$fixed.Dispose()

$huge = New-Object System.Windows.Forms.Form
$huge.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$huge.Location = New-Object System.Drawing.Point -100, -100
$huge.Size = New-Object System.Drawing.Size 8000, 6000
$huge.Show()
[System.Windows.Forms.Application]::DoEvents()
[void]$clamp.Invoke($null, [object[]]([System.Windows.Forms.Form]$huge, $null, $false))
$working = [System.Windows.Forms.Screen]::FromControl($huge).WorkingArea
if ($huge.Width -gt $working.Width -or $huge.Height -gt $working.Height) {
    throw "Clamped form $($huge.Width)x$($huge.Height) exceeds working area $($working.Width)x$($working.Height)."
}
if ($huge.Left -lt $working.Left -or $huge.Top -lt $working.Top) {
    throw "Clamped form location $($huge.Location) is outside $($working)."
}
$huge.Close()
$huge.Dispose()

function New-InputForm([string]$prompt) {
    $data = New-Object Krypton.Toolkit.KryptonInputBoxData
    $data.Caption = 'DPI-aware input'
    $data.Prompt = $prompt
    $data.CueText = 'Type a response'
    $form = New-Object Krypton.Toolkit.VisualInputBoxForm $data
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.Show()
    [System.Windows.Forms.Application]::DoEvents()
    return $form
}

$shortForm = New-InputForm 'Name'
$longPrompt = 'This prompt is long enough to wrap. The dialog grows with the text and stays inside the working area. The response box and the OK and Cancel buttons stay visible.'
$longForm = New-InputForm $longPrompt
try {
    if ($longForm.ClientSize.Height -le $shortForm.ClientSize.Height) {
        throw "Long prompt height $($longForm.ClientSize.Height) was not taller than the short prompt $($shortForm.ClientSize.Height)."
    }

    $area = [System.Windows.Forms.Screen]::FromControl($longForm).WorkingArea
    if ($longForm.Width -gt $area.Width -or $longForm.Height -gt $area.Height) {
        throw "Input box $($longForm.Width)x$($longForm.Height) exceeds the working area."
    }

    $flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
    $label = $longForm.GetType().GetField('_labelPrompt', $flags).GetValue($longForm)
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
