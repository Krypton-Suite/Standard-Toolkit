<#
.SYNOPSIS
    Asserts KryptonTagInputControl public API: defaults, add/remove, duplicates, max tags, events, category colours.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Toolkit.Utilities binaries and runs in-process STA checks.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-TagInput.ps1
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

function Get-NetObject {
    param(
        [AllowNull()]
        [object]$Value
    )
    if ($null -eq $Value) { return $null }
    if ($Value -is [System.Management.Automation.PSObject]) {
        return $Value.PSObject.BaseObject
    }
    return $Value
}

Write-UnitTestBanner -Status INFO -Message 'Asserting KryptonTagInputControl public API'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = New-Object System.Windows.Forms.Form
$form.Text = 'UnitTest-TagInput'
$form.ShowInTaskbar = $false
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(-32000, -32000)
$form.Size = New-Object System.Drawing.Size(400, 200)
[void]$form.Show()
[System.Windows.Forms.Application]::DoEvents()

$control = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.Utilities.KryptonTagInputControl]))
$form.Controls.Add($control)
[System.Windows.Forms.Application]::DoEvents()

$values = Get-NetObject $control.Values
Assert-True $values.IsDefault 'Fresh Values.IsDefault is true'
Assert-Equal 120 $values.InputWidth 'Default InputWidth is 120'
Assert-Equal 0 $values.MaxTags 'Default MaxTags is 0'
Assert-True $values.CommitOnEnter 'Default CommitOnEnter is true'
Assert-True $values.CommitOnComma 'Default CommitOnComma is true'
Assert-True (-not $values.AllowDuplicates) 'Default AllowDuplicates is false'
Assert-True $values.AllowCustomTags 'Default AllowCustomTags is true'
Assert-True $values.ShowRemoveButton 'Default ShowRemoveButton is true'
Assert-Equal 0 $control.Tags.Count 'Fresh Tags collection is empty'

$added = New-Object System.Collections.Generic.List[string]
$removed = New-Object System.Collections.Generic.List[string]
$changed = 0
$control.add_TagAdded({ param($s, $e) [void]$added.Add($e.Tag) })
$control.add_TagRemoved({ param($s, $e) [void]$removed.Add($e.Tag) })
$control.add_TagsChanged({ $script:changed++ })
$control.add_TagAdding({
        param($s, $e)
        if ([string]::Equals($e.Tag, 'reject', [StringComparison]::OrdinalIgnoreCase)) {
            $e.Cancel = $true
        }
    })

Assert-True $control.AddTag('Bug') 'AddTag Bug succeeds'
Assert-True (-not $control.AddTag('bug')) 'Duplicate Bug is rejected case-insensitively'
Assert-True (-not $control.AddTag('')) 'Empty AddTag is rejected'
Assert-True (-not $control.AddTag('reject')) 'TagAdding can cancel'
Assert-True $control.AddTag('Feature') 'AddTag Feature succeeds'
Assert-Equal 2 $control.Tags.Count 'Two tags after Bug and Feature'
Assert-Equal 'Bug' $control.Tags[0] 'First tag is Bug'
Assert-Equal 'Feature' $control.Tags[1] 'Second tag is Feature'
Assert-Equal 2 $added.Count 'TagAdded fired twice'
Assert-True ($changed -ge 2) 'TagsChanged fired for adds'

$values.AllowDuplicates = $true
Assert-True $control.AddTag('Bug') 'Duplicate allowed when AllowDuplicates is true'
Assert-Equal 3 $control.Tags.Count 'Three tags after allowing duplicates'

Assert-True $control.RemoveTag('feature') 'RemoveTag is case-insensitive'
Assert-Equal 2 $control.Tags.Count 'Two tags after removing Feature'
Assert-Equal 1 $removed.Count 'TagRemoved fired once'

$control.ClearTags()
Assert-Equal 0 $control.Tags.Count 'ClearTags empties the collection'

$values.AllowDuplicates = $false
$values.MaxTags = 1
Assert-True $control.AddTag('Only') 'First tag under MaxTags=1 succeeds'
Assert-True (-not $control.AddTag('Two')) 'Second tag under MaxTags=1 is rejected'

[string[]]$suggestionItems = @('Alpha', 'Beta')
$control.SetSuggestions($suggestionItems)
Assert-Equal 2 $control.Suggestions.Count 'SetSuggestions replaces the list'
$values.AllowCustomTags = $false
$values.MaxTags = 0
$control.ClearTags()
Assert-True (-not $control.AddTag('Gamma')) 'Custom tag rejected when AllowCustomTags is false'
Assert-True $control.AddTag('Alpha') 'Suggestion Alpha accepted when AllowCustomTags is false'

$control.SetCategoryColor('Alpha', [System.Drawing.Color]::SteelBlue)
$color = [System.Drawing.Color]::Empty
$got = $control.TryGetCategoryColor('alpha', [ref]$color)
Assert-True $got 'TryGetCategoryColor is case-insensitive'
Assert-Equal ([System.Drawing.Color]::SteelBlue.ToArgb()) $color.ToArgb() 'Category colour round-trips'

$values.CueHintText = 'Add a tag'
Assert-True (-not $values.IsDefault) 'Values.IsDefault is false after CueHintText change'
$values.Reset()
Assert-True $values.IsDefault 'Values.Reset restores IsDefault'

$form.Close()
$form.Dispose()

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "$($failed.Count) assertion(s) failed"
    $failed | ForEach-Object { Write-Host "  $_" -ForegroundColor Red }
    exit 1
}

Write-UnitTestBanner -Status PASS -Message 'KryptonTagInputControl API assertions passed'
exit 0
