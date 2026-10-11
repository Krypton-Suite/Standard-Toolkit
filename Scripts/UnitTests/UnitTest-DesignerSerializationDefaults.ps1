<#
.SYNOPSIS
    Asserts #4325: freshly constructed toolbox controls have no designer "Modified" storage.

.DESCRIPTION
    Instantiates public Toolbox parameterless types from Krypton assemblies and walks
    TypeDescriptor properties via a C# helper (PropertyDescriptorCollection indexing is
    unreliable from Windows PowerShell). Nested Storage objects whose IsDefault is false
    appear as "Modified" in the Visual Studio property grid.

    Exit code 0 on success; non-zero on failure.
    Requires STA (Invoke-AllUnitTests launches include scripts with -STA).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DesignerSerializationDefaults.ps1
#>
# UnitTest-CI: include
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [switch]$ReportOnly
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

$formsAsm = [System.Windows.Forms.Control].Assembly.Location
$drawingAsm = [System.Drawing.Color].Assembly.Location
$componentModelAsm = [System.ComponentModel.Component].Assembly.Location

Add-Type -ReferencedAssemblies @($formsAsm, $drawingAsm, $componentModelAsm) -TypeDefinition @'
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

public static class DesignerDefaultAudit
{
    private static readonly HashSet<string> SkipLeaf = new HashSet<string>(StringComparer.Ordinal)
    {
        "Name", "Location", "Size", "Bounds", "ClientSize", "MaximumSize", "MinimumSize",
        "TabIndex", "TabStop", "WindowTarget", "DataBindings", "Controls", "Site", "Container",
        "Handle", "Region", "AccessibilityObject", "CompanyName", "ProductName", "ProductVersion",
        "Tag", "Cursor", "ImeMode", "Parent", "TopLevelControl", "BindingContext", "ContextMenuStrip",
        "Anchor", "Dock", "Margin", "Padding", "AutoScrollMargin", "AutoScrollMinSize",
        "Owner", "Capacity", "UniqueName", "DefaultDataSourceUpdateMode", "CurrentPath"
    };

    public static List<string> Walk(object instance, string path)
    {
        var hits = new List<string>();
        var visited = new HashSet<int>();
        WalkCore(instance, path, 0, visited, hits);
        return hits;
    }

    private static void WalkCore(object instance, string path, int depth, HashSet<int> visited, List<string> hits)
    {
        if (instance == null || depth > 12)
        {
            return;
        }

        int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(instance);
        if (!visited.Add(id))
        {
            return;
        }

        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(instance);
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor pd = props[i];
            if (pd.DesignTimeOnly)
            {
                continue;
            }

            DesignerSerializationVisibility vis = pd.SerializationVisibility;
            if (vis == DesignerSerializationVisibility.Hidden)
            {
                continue;
            }

            if (!pd.IsBrowsable && vis != DesignerSerializationVisibility.Content)
            {
                continue;
            }

            string childPath = string.IsNullOrEmpty(path) ? pd.Name : path + "." + pd.Name;
            if (childPath.IndexOf("DataBindings", StringComparison.Ordinal) >= 0
                || childPath.IndexOf("ToolkitStrings", StringComparison.Ordinal) >= 0)
            {
                continue;
            }
            object value;
            try
            {
                value = pd.GetValue(instance);
            }
            catch
            {
                continue;
            }

            if (vis == DesignerSerializationVisibility.Content)
            {
                bool? isDefault = TryIsDefault(value);
                if (isDefault == false)
                {
                    string shown = null;
                    try { shown = value != null ? value.ToString() : null; }
                    catch { shown = null; }
                    if (string.IsNullOrEmpty(shown))
                    {
                        shown = "IsDefault=false";
                    }
                    hits.Add(childPath + " => " + shown);
                }

                if (value != null && !(value is string) && !value.GetType().IsPrimitive)
                {
                    WalkCore(value, childPath, depth + 1, visited, hits);
                }

                continue;
            }

            if (SkipLeaf.Contains(pd.Name))
            {
                continue;
            }

            bool should;
            try
            {
                should = pd.ShouldSerializeValue(instance);
            }
            catch
            {
                continue;
            }

            if (should)
            {
                string display = null;
                try { display = value != null ? value.ToString() : null; }
                catch { display = null; }
                hits.Add(childPath + " ShouldSerialize=true value=" + display);
            }
        }
    }

    private static bool? TryIsDefault(object value)
    {
        if (value == null)
        {
            return true;
        }

        PropertyInfo prop = value.GetType().GetProperty("IsDefault", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
        if (prop == null || prop.PropertyType != typeof(bool))
        {
            return null;
        }

        try
        {
            return (bool)prop.GetValue(value, null);
        }
        catch
        {
            return null;
        }
    }

    public static bool ShouldSerialize(object instance, string propertyName)
    {
        PropertyDescriptor pd = TypeDescriptor.GetProperties(instance)[propertyName];
        if (pd == null)
        {
            throw new InvalidOperationException("Missing property " + propertyName + " on " + instance.GetType().FullName);
        }

        return pd.ShouldSerializeValue(instance);
    }
}
'@

foreach ($name in @(
        'Krypton.Interop.dll',
        'Krypton.Toolkit.dll',
        'Krypton.Themes.dll',
        'Krypton.Ribbon.dll',
        'Krypton.Navigator.dll',
        'Krypton.Workspace.dll',
        'Krypton.Docking.dll',
        'Krypton.Toolkit.Utilities.dll',
        'Krypton.Navigator.Utilities.dll'
    )) {
    $path = Join-Path $bin $name
    if (Test-Path -LiteralPath $path) {
        [void][System.Reflection.Assembly]::LoadFrom($path)
    }
}

# #4466: factory strings and empty colours must not look modified to the designer.
$serializationFailures = New-Object System.Collections.Generic.List[string]
function Add-SerializeFailure([string]$name, [bool]$shouldSerialize) {
    if ($shouldSerialize) {
        $script:serializationFailures.Add("$name ShouldSerialize=true")
    }
}

$toolkitAsm = [AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GetName().Name -eq 'Krypton.Toolkit' } | Select-Object -First 1
if ($null -eq $toolkitAsm) {
    $serializationFailures.Add('Krypton.Toolkit was not loaded')
}
else {
    $globalStrings = $toolkitAsm.CreateInstance('Krypton.Toolkit.KryptonGlobalToolkitStrings')
    $messageBox = $globalStrings.MessageBoxStrings
    $preview = $toolkitAsm.CreateInstance('Krypton.Toolkit.KryptonPrintPreviewDialogStrings')
    Add-SerializeFailure 'ToolkitStrings.MessageBoxStrings' ([DesignerDefaultAudit]::ShouldSerialize($globalStrings, 'MessageBoxStrings'))
    Add-SerializeFailure 'MessageBoxStrings.MoreDetails' ([DesignerDefaultAudit]::ShouldSerialize($messageBox, 'MoreDetails'))
    Add-SerializeFailure 'MessageBoxStrings.LessDetails' ([DesignerDefaultAudit]::ShouldSerialize($messageBox, 'LessDetails'))
    Add-SerializeFailure 'ToolkitStrings.SplashScreenStrings' ([DesignerDefaultAudit]::ShouldSerialize($globalStrings, 'SplashScreenStrings'))
    Add-SerializeFailure 'ToolkitStrings.PrintPreviewDialogStrings' ([DesignerDefaultAudit]::ShouldSerialize($globalStrings, 'PrintPreviewDialogStrings'))
    if (-not $preview.IsDefault) {
        $serializationFailures.Add('KryptonPrintPreviewDialogStrings.IsDefault is false after Reset')
    }
    if ($preview.ZoomInButtonText -ne 'Zoom &In' -or $preview.ZoomOutButtonText -ne 'Zoom &Out' -or $preview.PaginationPartOneText -ne 'Page' -or $preview.PaginationPartTwoText -ne 'of') {
        $serializationFailures.Add("Print preview defaults are ZoomIn='$($preview.ZoomInButtonText)' ZoomOut='$($preview.ZoomOutButtonText)' Page1='$($preview.PaginationPartOneText)' Page2='$($preview.PaginationPartTwoText)'")
    }

    $progress = $toolkitAsm.CreateInstance('Krypton.Toolkit.KryptonProgressBar')
    try {
        Add-SerializeFailure 'KryptonProgressBar.TextShadowColor' ([DesignerDefaultAudit]::ShouldSerialize($progress, 'TextShadowColor'))
        Add-SerializeFailure 'KryptonProgressBar.TextBackdropColor' ([DesignerDefaultAudit]::ShouldSerialize($progress, 'TextBackdropColor'))
    }
    finally {
        $progress.Dispose()
    }

    $button = $toolkitAsm.CreateInstance('Krypton.Toolkit.KryptonButton')
    try {
        Add-SerializeFailure 'KryptonButton.Values.DropDownArrowColor' ([DesignerDefaultAudit]::ShouldSerialize($button.Values, 'DropDownArrowColor'))
    }
    finally {
        $button.Dispose()
    }
}

$hits = New-Object System.Collections.Generic.List[string]
$created = 0
$skipped = 0
$errors = New-Object System.Collections.Generic.List[string]

$assemblies = [System.AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GetName().Name -like 'Krypton.*' }

foreach ($asm in $assemblies) {
    $types = @()
    try {
        $types = $asm.GetTypes()
    }
    catch [System.Reflection.ReflectionTypeLoadException] {
        $types = $_.Exception.Types | Where-Object { $_ }
    }

    foreach ($type in $types) {
        if (-not $type.IsClass -or $type.IsAbstract -or $type.IsGenericType -or -not $type.IsPublic) {
            continue
        }

        if (-not $type.IsSubclassOf([System.ComponentModel.Component])) {
            continue
        }

        $tb = $type.GetCustomAttributes([System.ComponentModel.ToolboxItemAttribute], $true) | Select-Object -First 1
        if ($null -ne $tb -and $null -eq $tb.ToolboxItemType) {
            continue
        }

        $ctor = $type.GetConstructor([Type[]]@())
        if ($null -eq $ctor) {
            $skipped++
            continue
        }

        $instance = $null
        try {
            $instance = [Activator]::CreateInstance($type)
            $created++
        }
        catch {
            $skipped++
            continue
        }

        try {
            $walked = [DesignerDefaultAudit]::Walk($instance, $type.Name)
            foreach ($hit in $walked) {
                [void]$hits.Add($hit)
            }
        }
        catch {
            $errors.Add("$($type.FullName): $($_.Exception.Message)")
        }
        finally {
            if ($instance -is [System.IDisposable]) {
                try { $instance.Dispose() } catch { }
            }
        }
    }
}

Write-Host "Instantiated $created toolbox components; skipped $skipped."
if ($errors.Count -gt 0) {
    Write-Host "Walk errors: $($errors.Count)" -ForegroundColor Yellow
    $errors | ForEach-Object { Write-Host "  $_" }
}

$unique = @($hits | Sort-Object -Unique)
Write-Host "Dirty designer paths: $($unique.Count)"
$unique | ForEach-Object { Write-Host "  $_" }

$modified = @($unique | Where-Object { $_ -match ' => Modified$' -or $_ -match ' => IsDefault=false$' })
Write-Host "Modified storage paths: $($modified.Count)"

# Core toolbox controls that must not show Modified after a fresh drop.
$corePrefixes = @(
    'KryptonButton.',
    'KryptonDropButton.',
    'KryptonSplitButton.',
    'KryptonCheckButton.',
    'KryptonColorButton.',
    'KryptonCheckBox.',
    'KryptonRadioButton.',
    'KryptonLabel.',
    'KryptonLinkLabel.',
    'KryptonWrapLabel.',
    'KryptonTextBox.',
    'KryptonRichTextBox.',
    'KryptonMaskedTextBox.',
    'KryptonComboBox.',
    'KryptonNumericUpDown.',
    'KryptonDomainUpDown.',
    'KryptonListBox.',
    'KryptonCheckedListBox.',
    'KryptonTreeView.',
    'KryptonListView.',
    'KryptonPanel.',
    'KryptonGroup.',
    'KryptonGroupBox.',
    'KryptonHeaderGroup.',
    'KryptonSplitContainer.',
    'KryptonTrackBar.',
    'KryptonRating.',
    'KryptonMonthCalendar.',
    'KryptonDateTimePicker.',
    'KryptonForm.',
    'KryptonBorderEdge.',
    'KryptonSeparator.',
    'KryptonProgressBar.',
    'KryptonDataGridView.',
    'KryptonHeader.',
    'KryptonBreadCrumb.',
    'KryptonHScrollBar.',
    'KryptonVScrollBar.',
    'KryptonThemeComboBox.',
    'KryptonLinkWrapLabel.',
    'KryptonTagInput.',
    'KryptonTagInputControl.',
    'KryptonLoadingCircle.'
)

$coreModified = @($modified | Where-Object {
        $line = $_
        $corePrefixes | Where-Object { $line.StartsWith($_) }
    })

if ($ReportOnly) {
    Write-Host "ReportOnly: not asserting."
    exit 0
}

$failed = $false
if ($errors.Count -gt 0) {
    Write-Host "FAIL: designer walk threw for $($errors.Count) type(s)." -ForegroundColor Red
    $failed = $true
}

if ($coreModified.Count -gt 0) {
    Write-Host "FAIL: core toolbox controls have designer Modified storage (#4325):" -ForegroundColor Red
    $coreModified | ForEach-Object { Write-Host "  $_" }
    $failed = $true
}

if ($serializationFailures.Count -gt 0) {
    Write-Host "FAIL: factory values are still treated as designer-modified (#4466):" -ForegroundColor Red
    $serializationFailures | ForEach-Object { Write-Host "  $_" }
    $failed = $true
}

if ($failed) {
    exit 1
}

Write-Host "PASS: core toolbox controls have no designer Modified storage." -ForegroundColor Green
exit 0
