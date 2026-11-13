#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace AccessibilityAudit;

internal sealed class ControlAuditResult
{
    public int Visited { get; set; }

    public List<string> FailureKeys { get; } = new List<string>();

    public List<string> HardRoleFailures { get; } = new List<string>();

    public List<string> Notes { get; } = new List<string>();
}

internal static class ControlAudit
{
    private static readonly Dictionary<string, AccessibleRole> RequiredRoles = new Dictionary<string, AccessibleRole>(StringComparer.Ordinal)
    {
        { "Krypton.Toolkit.KryptonTextBox", AccessibleRole.Text },
        { "Krypton.Toolkit.KryptonRichTextBox", AccessibleRole.Text },
        { "Krypton.Toolkit.KryptonMaskedTextBox", AccessibleRole.Text },
        // Up-down controls delegate to the inner control. On modern .NET that role is SpinButton.
        // The provider falls back to Text only when the inner role is Default or None.
        { "Krypton.Toolkit.KryptonNumericUpDown", AccessibleRole.SpinButton },
        { "Krypton.Toolkit.KryptonDomainUpDown", AccessibleRole.SpinButton },
        { "Krypton.Toolkit.KryptonComboBox", AccessibleRole.ComboBox },
        { "Krypton.Toolkit.KryptonListBox", AccessibleRole.List },
        { "Krypton.Toolkit.KryptonCheckedListBox", AccessibleRole.List },
        { "Krypton.Toolkit.KryptonListView", AccessibleRole.List },
        { "Krypton.Toolkit.KryptonLinkWrapLabel", AccessibleRole.Link }
    };

    internal static ControlAuditResult Run()
    {
        AuditLog.Write("[INFO] Control audit starting");
        var result = new ControlAuditResult();
        using (var manager = new KryptonManager())
        {
            manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue;
        }

        using (var host = new ControlCatalog.AuditHost())
        {
            foreach (Type type in ControlCatalog.Discover())
            {
                result.Visited++;
                AuditLog.Write("[INFO] Control " + (type.FullName ?? type.Name));
                AuditOne(host, type, result);
            }
        }

        result.FailureKeys.Sort(StringComparer.Ordinal);
        result.HardRoleFailures.Sort(StringComparer.Ordinal);
        return result;
    }

    private static void AuditOne(ControlCatalog.AuditHost host, Type type, ControlAuditResult result)
    {
        Control? control = null;
        string fullName = type.FullName ?? type.Name;
        try
        {
            control = (Control?)Activator.CreateInstance(type);
            if (control == null)
            {
                AddFailure(result, fullName + "|construct-threw|NullInstance");
                return;
            }

            control.AccessibleName = type.Name;
            host.Controls.Add(control);
            ControlCatalog.CreateChild(control);

            AccessibleObject? accessible = control.AccessibilityObject;
            if (accessible == null)
            {
                AddFailure(result, fullName + "|accessible-null");
                return;
            }

            AccessibleRole role = accessible.Role;
            if (role == AccessibleRole.None)
            {
                AddFailure(result, fullName + "|role-none");
            }
            else if (role == AccessibleRole.Default)
            {
                AddFailure(result, fullName + "|role-default");
            }

            if (!string.Equals(accessible.Name, control.AccessibleName, StringComparison.Ordinal))
            {
                AddFailure(result, fullName + "|name-not-surfaced");
            }

            if (RequiredRoles.TryGetValue(fullName, out AccessibleRole required) && role != required)
            {
                result.HardRoleFailures.Add(fullName + "|role-mismatch|" + role + "|expected-" + required);
            }

            result.Notes.Add(fullName + "|role=" + role + "|name=" + (accessible.Name ?? string.Empty));
        }
        catch (Exception ex)
        {
            Exception root = ControlCatalog.Unwrap(ex);
            AddFailure(result, fullName + "|construct-threw|" + root.GetType().Name);
            if (RequiredRoles.ContainsKey(fullName))
            {
                result.HardRoleFailures.Add(fullName + "|role-mismatch|construct-threw|expected-" + RequiredRoles[fullName]);
            }
        }
        finally
        {
            if (control != null)
            {
                try
                {
                    host.Controls.Remove(control);
                }
                catch (Exception)
                {
                    // Keep auditing the remaining controls.
                }

                try
                {
                    control.Dispose();
                }
                catch (Exception)
                {
                    // Keep auditing the remaining controls.
                }
            }
        }
    }

    private static void AddFailure(ControlAuditResult result, string key)
    {
        if (!result.FailureKeys.Contains(key))
        {
            result.FailureKeys.Add(key);
        }
    }

    }
