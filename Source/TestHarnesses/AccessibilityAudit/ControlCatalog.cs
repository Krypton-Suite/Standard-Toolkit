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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Krypton.Docking;
using Krypton.Navigator;
using Krypton.Ribbon;
using Krypton.Toolkit;
using Krypton.Utilities;
using Krypton.Workspace;

namespace AccessibilityAudit;

/// <summary>
/// Shared discovery and hidden host for the accessibility, DPI, and touch suites.
/// </summary>
internal static class ControlCatalog
{
    internal static IReadOnlyList<Type> Discover()
    {
        AuditLog.Write("[INFO] DiscoverControls");
        Assembly[] assemblies =
        {
            typeof(KryptonManager).Assembly,
            typeof(KryptonRibbon).Assembly,
            typeof(KryptonNavigator).Assembly,
            typeof(KryptonWorkspace).Assembly,
            typeof(KryptonDockingManager).Assembly,
            typeof(KryptonCommandLinkButton).Assembly
        };

        var seen = new HashSet<string>(StringComparer.Ordinal);
        var types = new List<Type>();
        foreach (Assembly assembly in assemblies)
        {
            AuditLog.Write("[INFO] Discover " + assembly.GetName().Name);
            Type[] exported;
            try
            {
                exported = assembly.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                exported = ex.Types.Where(type => type != null).Cast<Type>().ToArray();
            }

            foreach (Type type in exported)
            {
                if (type.IsAbstract || type.ContainsGenericParameters || !typeof(Control).IsAssignableFrom(type))
                {
                    continue;
                }

                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    continue;
                }

                string name = type.FullName ?? type.Name;
                if (seen.Add(name))
                {
                    types.Add(type);
                }
            }
        }

        types.Sort((left, right) => StringComparer.Ordinal.Compare(left.FullName, right.FullName));
        return types;
    }

    internal static void CreateChild(Control control)
    {
        MethodInfo? create = typeof(Control).GetMethod(
            "CreateControl",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            null,
            Type.EmptyTypes,
            null);
        if (create != null)
        {
            create.Invoke(control, null);
            return;
        }

        _ = control.Handle;
    }

    internal static Exception Unwrap(Exception exception)
    {
        while (exception is TargetInvocationException && exception.InnerException != null)
        {
            exception = exception.InnerException;
        }

        return exception;
    }

    /// <summary>
    /// Hidden host. CreateControl is protected, so the audit calls it from this subclass.
    /// </summary>
    internal sealed class AuditHost : Form
    {
        public AuditHost()
        {
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = new System.Drawing.Point(-32000, -32000);
            Size = new System.Drawing.Size(240, 180);
            Opacity = 0;
            CreateControl();
        }
    }
}
