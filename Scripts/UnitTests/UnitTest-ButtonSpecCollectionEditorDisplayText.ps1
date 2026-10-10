<#
.SYNOPSIS
    Asserts the ButtonSpec collection editor lists the designer component name.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and checks KryptonDesignerButtonSpecAnyCollectionEditor
    display text (#4536). A sited ButtonSpecAny shows Site.Name (buttonSpecAny1).
    An unsited spec shows the type name. UniqueName stays the persistence key. Text is the button caption.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ButtonSpecCollectionEditorDisplayText.ps1
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
Add-Type -AssemblyName System.Design

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))

if (-not ('ButtonSpecDisplayTextProbe' -as [type])) {
    $design = [System.Reflection.Assembly]::Load('System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a')
    $drawing = [System.Reflection.Assembly]::Load('System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a')
    $forms = [System.Reflection.Assembly]::Load('System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089')
    $refs = @(
        $design.Location,
        $drawing.Location,
        $forms.Location,
        (Join-Path $bin 'Krypton.Interop.dll'),
        (Join-Path $bin 'Krypton.Resources.dll'),
        (Join-Path $bin 'Krypton.Toolkit.dll')
    )
    Add-Type -ReferencedAssemblies $refs -TypeDefinition @'
using System;
using System.ComponentModel;
using System.Reflection;
using Krypton.Toolkit;

public sealed class ButtonSpecDisplayTextSite : ISite
{
    private readonly IComponent _component;
    private string _name;

    public ButtonSpecDisplayTextSite(IComponent component, string name)
    {
        _component = component;
        _name = name;
    }

    public IComponent Component
    {
        get { return _component; }
    }

    public IContainer Container
    {
        get { return null; }
    }

    public bool DesignMode
    {
        get { return true; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public object GetService(Type serviceType)
    {
        return null;
    }
}

public sealed class ButtonSpecDisplayTextProbe
{
    public string SitedLabel;
    public string UnsitedLabel;
    public string UniqueName;

    public static ButtonSpecDisplayTextProbe Run()
    {
        const string guidName = "2dea0edf052e49c18c582399c403b6b7";
        var editor = new KryptonDesignerButtonSpecAnyCollectionEditor();
        var method = FindDisplayText(editor.GetType());
        var sited = new ButtonSpecAny();
        var unsited = new ButtonSpecAny();
        try
        {
            sited.UniqueName = guidName;
            sited.Text = "Save";
            sited.Site = new ButtonSpecDisplayTextSite(sited, "buttonSpecAny1");
            unsited.UniqueName = guidName;
            unsited.Text = "Save";

            var probe = new ButtonSpecDisplayTextProbe();
            probe.SitedLabel = (string)method.Invoke(editor, new object[] { sited });
            probe.UnsitedLabel = (string)method.Invoke(editor, new object[] { unsited });
            probe.UniqueName = sited.UniqueName;
            return probe;
        }
        finally
        {
            sited.Dispose();
            unsited.Dispose();
        }
    }

    private static MethodInfo FindDisplayText(Type type)
    {
        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        var current = type;
        while (current != null && current != typeof(object))
        {
            var method = current.GetMethod("GetDesignerDisplayText", flags, null, new Type[] { typeof(object) }, null);
            if (method != null)
            {
                return method;
            }

            current = current.BaseType;
        }

        throw new MissingMethodException(type.FullName, "GetDesignerDisplayText");
    }
}
'@
}

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

Write-UnitTestBanner -Status INFO -Message 'Asserting ButtonSpec collection editor display text'

$probe = [ButtonSpecDisplayTextProbe]::Run()
Assert-True ($probe.SitedLabel -eq 'buttonSpecAny1') "Sited ButtonSpecAny label is buttonSpecAny1 (actual: $($probe.SitedLabel))"
Assert-True ($probe.UniqueName -eq '2dea0edf052e49c18c582399c403b6b7') 'UniqueName remains the persistence key'
Assert-True ($probe.UnsitedLabel -eq 'ButtonSpecAny') "Unsited ButtonSpecAny label is the type name (actual: $($probe.UnsitedLabel))"

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "$($failed.Count) assertion(s) failed"
    exit 1
}

Write-UnitTestBanner -Status PASS -Message 'ButtonSpec collection editor display text assertions passed'
exit 0
