#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace AccessibilityAudit;

internal static class DpiAudit
{
    private const float Scale = 1.5f;

    private const float MinimumGrowth = 1.2f;

    internal static SizeAuditResult Run()
    {
        var result = new SizeAuditResult("DPI audit");
        AuditLog.Write("[INFO] DPI audit starting");

        using (var host = new ControlCatalog.AuditHost())
        {
            AssertDpiFactor(host, result);

            foreach (Type type in ControlCatalog.Discover())
            {
                result.Visited++;
                AuditLog.Write("[INFO] DPI " + (type.FullName ?? type.Name));
                AuditOne(host, type, result);
            }
        }

        result.FailureKeys.Sort(StringComparer.Ordinal);
        result.HardFailures.Sort(StringComparer.Ordinal);
        return result;
    }

    private static void AssertDpiFactor(ControlCatalog.AuditHost host, SizeAuditResult result)
    {
        float expected = host.DeviceDpi / 96f;
        float actual = KryptonManager.GetDpiFactor();
        if (Math.Abs(actual - expected) > 0.01f)
        {
            result.HardFailures.Add(
                "dpi-factor|actual=" + actual.ToString("0.###") + "|expected=" + expected.ToString("0.###"));
        }

        int scaled = KryptonManager.ScaleValueByDpi(16);
        int expectedScaled = (int)Math.Round(16 * expected);
        if (scaled != expectedScaled)
        {
            result.HardFailures.Add(
                "scale-value|actual=" + scaled + "|expected=" + expectedScaled);
        }
    }

    private static void AuditOne(ControlCatalog.AuditHost host, Type type, SizeAuditResult result)
    {
        Control? control = null;
        string fullName = type.FullName ?? type.Name;
        try
        {
            control = (Control?)Activator.CreateInstance(type);
            if (control == null)
            {
                result.AddFailure(fullName + "|scale-threw|NullInstance");
                return;
            }

            host.Controls.Add(control);
            ControlCatalog.CreateChild(control);

            int width = control.Width;
            int height = control.Height;
            control.Scale(new SizeF(Scale, Scale));

            bool grew = width > 0 && height > 0
                && control.Width + 0.01f >= width * MinimumGrowth
                && control.Height + 0.01f >= height * MinimumGrowth;
            if (width > 0 && height > 0 && !grew)
            {
                result.AddFailure(fullName + "|scale-ignored");
            }

            result.Notes.Add(fullName + "|before=" + width + "x" + height + "|after=" + control.Width + "x" + control.Height);
        }
        catch (Exception ex)
        {
            Exception root = ControlCatalog.Unwrap(ex);
            result.AddFailure(fullName + "|scale-threw|" + root.GetType().Name);
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
}
