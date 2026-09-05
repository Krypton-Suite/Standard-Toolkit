# Requires STA (WinForms TypeDescriptor). Fresh Toolbox controls must not show
# nested Storage as "Modified" (issue #4325).
# Usage (from repo root):
#   powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DesignerSerializationDefaults.ps1

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
if (-not (Test-Path -LiteralPath (Join-Path $repoRoot 'Source'))) {
    $repoRoot = Split-Path -Parent $PSScriptRoot
}

$tfm = 'net472'
$config = 'Debug'
$toolkitDll = Join-Path $repoRoot "Bin\$config\$tfm\Krypton.Toolkit.dll"
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

Get-ChildItem -LiteralPath $binDir -Filter '*.dll' | ForEach-Object {
    try { [void][System.Reflection.Assembly]::LoadFrom($_.FullName) } catch { }
}

Add-Type -Path $toolkitDll

$auditor = @'
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

public static class DesignerDefaultAuditor
{
    static readonly Hashtable SkipNames = CreateSkipNames();
    static readonly string[] CorePrefixes = new string[]
    {
        "KryptonButton.",
        "KryptonDropButton.",
        "KryptonCheckButton.",
        "KryptonColorButton.",
        "KryptonCommandLinkButton.",
        "KryptonCheckBox.",
        "KryptonRadioButton.",
        "KryptonLabel.",
        "KryptonLinkLabel.",
        "KryptonWrapLabel.",
        "KryptonLinkWrapLabel.",
        "KryptonComboBox.",
        "KryptonTextBox.",
        "KryptonMaskedTextBox.",
        "KryptonRichTextBox.",
        "KryptonNumericUpDown.",
        "KryptonDomainUpDown.",
        "KryptonProgressBar.",
        "KryptonTrackBar.",
        "KryptonPanel.",
        "KryptonGroup.",
        "KryptonGroupBox.",
        "KryptonHeader.",
        "KryptonHeaderGroup.",
        "KryptonBorderEdge.",
        "KryptonSeparator.",
        "KryptonSplitContainer.",
        "KryptonBreadCrumb.",
        "KryptonDateTimePicker.",
        "KryptonMonthCalendar.",
        "KryptonTreeView.",
        "KryptonListBox.",
        "KryptonCheckedListBox.",
        "KryptonListView.",
        "KryptonDataGridView.",
        "KryptonPropertyGrid.",
        "KryptonToggleSwitch.",
        "KryptonHScrollBar.",
        "KryptonVScrollBar.",
        "KryptonScrollBar.",
        "KryptonForm."
    };

    static Hashtable CreateSkipNames()
    {
        Hashtable table = new Hashtable(StringComparer.Ordinal);
        string[] names = new string[]
        {
            "Name", "Location", "Size", "Bounds", "ClientSize", "MaximumSize", "MinimumSize",
            "TabIndex", "TabStop", "WindowTarget", "DataBindings", "Controls", "Site", "Container",
            "Handle", "Region", "AccessibilityObject", "CompanyName", "ProductName", "ProductVersion",
            "Tag", "Cursor", "ImeMode", "Parent", "TopLevelControl", "BindingContext", "ContextMenuStrip",
            "Anchor", "Dock", "Margin", "Padding", "AutoScrollMargin", "AutoScrollMinSize",
            "Owner", "Capacity", "UniqueName", "DefaultDataSourceUpdateMode", "CurrentPath",
        "Visible", "TransparencyKey", "UseCompatibleTextRendering"
        };
        for (int i = 0; i < names.Length; i++)
        {
            table[names[i]] = true;
        }
        return table;
    }

    public static int Run(Assembly toolkit)
    {
        ArrayList modified = new ArrayList();
        ArrayList dirty = new ArrayList();
        ArrayList instantiateFails = new ArrayList();
        int instantiated = 0;
        Type[] types;
        try
        {
            types = toolkit.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            types = ex.Types;
        }

        for (int t = 0; t < types.Length; t++)
        {
            Type type = types[t];
            if (type == null || !type.IsPublic || type.IsAbstract || type.IsGenericType)
            {
                continue;
            }
            if (!typeof(Component).IsAssignableFrom(type))
            {
                continue;
            }
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                continue;
            }

            bool toolboxFalse = false;
            object[] toolbox = type.GetCustomAttributes(typeof(ToolboxItemAttribute), true);
            if (toolbox != null)
            {
                for (int a = 0; a < toolbox.Length; a++)
                {
                    ToolboxItemAttribute tia = (ToolboxItemAttribute)toolbox[a];
                    if (tia.Equals(ToolboxItemAttribute.None))
                    {
                        toolboxFalse = true;
                    }
                }
            }

            bool isForm = typeof(Form).IsAssignableFrom(type) && type.Name == "KryptonForm";
            bool isControl = typeof(Control).IsAssignableFrom(type);
            if (!isForm)
            {
                if (toolboxFalse)
                {
                    continue;
                }
                if (!isControl)
                {
                    continue;
                }
                if (typeof(Form).IsAssignableFrom(type))
                {
                    continue;
                }
            }

            string name = type.Name;
            if (name.IndexOf("Dialog", StringComparison.Ordinal) >= 0
                || name.IndexOf("Manager", StringComparison.Ordinal) >= 0
                || name.IndexOf("Palette", StringComparison.Ordinal) >= 0
                || (name.IndexOf("Command", StringComparison.Ordinal) >= 0 && name != "KryptonCommandLinkButton")
                || name == "KryptonContextMenu"
                || name == "KryptonToastNotificationManager"
                || name == "KryptonCheckSet")
            {
                continue;
            }

            object instance;
            try
            {
                instance = Activator.CreateInstance(type);
                instantiated++;
            }
            catch (Exception ex)
            {
                instantiateFails.Add(name + ": " + Unwrap(ex));
                continue;
            }

            try
            {
                Walk(name, instance, modified, dirty, new Hashtable(), 0);
            }
            catch (Exception ex)
            {
                instantiateFails.Add(name + " walk: " + Unwrap(ex));
            }

            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                try { disposable.Dispose(); }
                catch { }
            }
        }

        modified.Sort(StringComparer.Ordinal);
        dirty.Sort(StringComparer.Ordinal);

        Console.WriteLine("Instantiated: " + instantiated);
        Console.WriteLine("Modified storage paths: " + modified.Count);
        for (int i = 0; i < modified.Count; i++)
        {
            Console.WriteLine("  " + modified[i]);
        }
        Console.WriteLine("Dirty designer paths: " + dirty.Count);
        for (int i = 0; i < dirty.Count; i++)
        {
            Console.WriteLine("  " + dirty[i]);
        }
        if (instantiateFails.Count > 0)
        {
            Console.WriteLine("Instantiate/walk failures: " + instantiateFails.Count);
            for (int i = 0; i < instantiateFails.Count; i++)
            {
                Console.WriteLine("  " + instantiateFails[i]);
            }
        }

        ArrayList coreHits = new ArrayList();
        for (int i = 0; i < modified.Count; i++)
        {
            string line = (string)modified[i];
            if (IsCore(line))
            {
                coreHits.Add("Modified " + line);
            }
        }
        for (int i = 0; i < dirty.Count; i++)
        {
            string line = (string)dirty[i];
            if (IsCore(line))
            {
                coreHits.Add("Dirty " + line);
            }
        }

        if (coreHits.Count > 0)
        {
            Console.WriteLine("FAIL: core Toolbox drops still look modified (" + coreHits.Count + ")");
            for (int i = 0; i < coreHits.Count; i++)
            {
                Console.WriteLine("  " + coreHits[i]);
            }
            return 1;
        }

        Console.WriteLine("PASS: core Toolbox drops have no unexpected Modified/ShouldSerialize hits.");
        return 0;
    }

    static bool IsCore(string path)
    {
        for (int i = 0; i < CorePrefixes.Length; i++)
        {
            string prefix = CorePrefixes[i];
            if (path.StartsWith(prefix, StringComparison.Ordinal) || path == prefix.TrimEnd('.'))
            {
                return true;
            }
        }
        return false;
    }

    static void Walk(string path, object instance, ArrayList modified, ArrayList dirty, Hashtable seen, int depth)
    {
        if (instance == null || depth > 12)
        {
            return;
        }
        if (seen.Contains(instance))
        {
            return;
        }
        seen.Add(instance, true);

        PropertyInfo isDefaultProp = instance.GetType().GetProperty("IsDefault", BindingFlags.Instance | BindingFlags.Public);
        if (isDefaultProp != null && isDefaultProp.PropertyType == typeof(bool) && isDefaultProp.GetIndexParameters().Length == 0)
        {
            try
            {
                object val = isDefaultProp.GetValue(instance, null);
                if (val is bool && !((bool)val))
                {
                    modified.Add(path + " => Modified");
                }
            }
            catch
            {
            }
        }

        PropertyDescriptorCollection props;
        try
        {
            props = TypeDescriptor.GetProperties(instance);
        }
        catch
        {
            return;
        }

        for (int p = 0; p < props.Count; p++)
        {
            PropertyDescriptor pd = props[p];
            if (pd == null || SkipNames.Contains(pd.Name))
            {
                continue;
            }
            if (pd.IsReadOnly && pd.SerializationVisibility != DesignerSerializationVisibility.Content)
            {
                continue;
            }
            if (pd.SerializationVisibility == DesignerSerializationVisibility.Hidden)
            {
                continue;
            }

            string childPath = path + "." + pd.Name;
            if (childPath.IndexOf("DataBindings", StringComparison.Ordinal) >= 0
                || childPath.IndexOf("ToolkitStrings", StringComparison.Ordinal) >= 0
                || childPath.IndexOf("Strings.", StringComparison.Ordinal) >= 0
                || childPath.EndsWith(".RootItem.ShortText", StringComparison.Ordinal)
                || childPath.EndsWith(".Text", StringComparison.Ordinal) && path.IndexOf("RichTextBox", StringComparison.Ordinal) >= 0
                || childPath.IndexOf("KryptonContextMenu", StringComparison.Ordinal) >= 0)
            {
                continue;
            }

            object child = null;
            try
            {
                child = pd.GetValue(instance);
            }
            catch
            {
                continue;
            }

            if (pd.SerializationVisibility == DesignerSerializationVisibility.Content && child != null && !pd.PropertyType.IsValueType && pd.PropertyType != typeof(string))
            {
                Walk(childPath, child, modified, dirty, seen, depth + 1);
                continue;
            }

            if (pd.SerializationVisibility == DesignerSerializationVisibility.Visible)
            {
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
                    dirty.Add(childPath + " = " + FormatValue(child));
                }
            }
        }
    }

    static string FormatValue(object value)
    {
        if (value == null)
        {
            return "null";
        }
        string text = Convert.ToString(value);
        if (text == null)
        {
            return value.GetType().Name;
        }
        if (text.Length > 80)
        {
            text = text.Substring(0, 80) + "...";
        }
        return text.Replace("\r", " ").Replace("\n", " ");
    }

    static string Unwrap(Exception ex)
    {
        Exception inner = ex;
        while (inner.InnerException != null)
        {
            inner = inner.InnerException;
        }
        return inner.GetType().Name + ": " + inner.Message;
    }
}
'@

$refs = @(
    'System.Windows.Forms.dll',
    'System.Drawing.dll',
    'System.dll',
    'System.Core.dll'
)
Add-Type -TypeDefinition $auditor -ReferencedAssemblies $refs -ErrorAction Stop

$code = [DesignerDefaultAuditor]::Run([Krypton.Toolkit.KryptonButton].Assembly)
exit $code
