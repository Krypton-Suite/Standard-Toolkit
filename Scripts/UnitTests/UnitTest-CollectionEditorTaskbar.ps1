<#
.SYNOPSIS
    Asserts Krypton collection editor dialogs stay off the taskbar.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and checks issue #4547.
    The ButtonSpec collection editor dialog and the check-button collection form must set
    ShowInTaskbar to false. On .NET Framework that clears WS_EX_APPWINDOW, which is what
    forces an owned dialog onto the taskbar. WS_EX_TOOLWINDOW is not required and would
    shrink the Krypton caption. The windows are created but not shown.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-CollectionEditorTaskbar.ps1
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

if (-not ('CollectionEditorTaskbarProbe' -as [type])) {
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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Krypton.Toolkit;

public sealed class CollectionEditorTaskbarResult
{
    public string Name;
    public bool ShowInTaskbar;
    public bool AppWindow;
    public string Error;
}

public static class CollectionEditorTaskbarProbe
{
    private const int WS_EX_APPWINDOW = 0x00040000;

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    public static CollectionEditorTaskbarResult[] Run()
    {
        return new[]
        {
            ProbeEditor(),
            ProbeType("Krypton.Toolkit", "Krypton.Toolkit.VisualCheckButtonCollectionForm")
        };
    }

    private static CollectionEditorTaskbarResult ProbeEditor()
    {
        var result = new CollectionEditorTaskbarResult();
        result.Name = "KryptonDesignerButtonSpecAnyCollectionEditor";
        var editor = new KryptonDesignerButtonSpecAnyCollectionEditor();
        var service = new TaskbarEditorService();
        try
        {
            editor.EditValue(null, service, null);
            if (!service.Captured)
            {
                result.Error = "EditValue did not show a dialog.";
                return result;
            }

            result.ShowInTaskbar = service.ShowInTaskbar;
            result.AppWindow = service.AppWindow;
            return result;
        }
        catch (Exception ex)
        {
            result.Error = ex.GetBaseException().Message;
            return result;
        }
    }

    private static CollectionEditorTaskbarResult ProbeType(string assemblyName, string typeName)
    {
        var result = new CollectionEditorTaskbarResult();
        result.Name = typeName;
        Form form = null;
        try
        {
            var assembly = Assembly.Load(assemblyName);
            var type = assembly.GetType(typeName, true);
            form = (Form)Activator.CreateInstance(type, true);
            ReadStyles(form, result);
            return result;
        }
        catch (Exception ex)
        {
            result.Error = ex.GetBaseException().Message;
            return result;
        }
        finally
        {
            if (form != null)
            {
                form.Dispose();
            }
        }
    }

    private static void ReadStyles(Form form, CollectionEditorTaskbarResult result)
    {
        int exStyle = GetWindowLong(form.Handle, -20);
        result.ShowInTaskbar = form.ShowInTaskbar;
        result.AppWindow = (exStyle & WS_EX_APPWINDOW) != 0;
    }

    private sealed class TaskbarEditorService : IWindowsFormsEditorService, IServiceProvider
    {
        public bool Captured;
        public bool ShowInTaskbar;
        public bool AppWindow;

        public void CloseDropDown()
        {
        }

        public void DropDownControl(Control control)
        {
        }

        public DialogResult ShowDialog(Form dialog)
        {
            int exStyle = GetWindowLong(dialog.Handle, -20);
            Captured = true;
            ShowInTaskbar = dialog.ShowInTaskbar;
            AppWindow = (exStyle & WS_EX_APPWINDOW) != 0;
            return DialogResult.Cancel;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IWindowsFormsEditorService))
            {
                return this;
            }

            return null;
        }
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

Write-UnitTestBanner -Status INFO -Message 'Asserting collection editor taskbar styles'

foreach ($probe in [CollectionEditorTaskbarProbe]::Run()) {
    if ($probe.Error) {
        Assert-True $false "$($probe.Name) probe failed: $($probe.Error)"
        continue
    }

    Assert-True (-not $probe.ShowInTaskbar) "$($probe.Name) ShowInTaskbar is false"
    Assert-True (-not $probe.AppWindow) "$($probe.Name) window omits WS_EX_APPWINDOW"
}

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "$($failed.Count) assertion(s) failed"
    exit 1
}

Write-UnitTestBanner -Status PASS -Message 'Collection editor taskbar assertions passed'
exit 0
