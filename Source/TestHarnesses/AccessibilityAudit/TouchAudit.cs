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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace AccessibilityAudit;

internal static class TouchAudit
{
    private const int SmDigitizer = 94;

    private const int NidReady = 0x80;

    private const float Scale = 1.5f;

    private const float MinimumGrowth = 1.2f;

    internal static SizeAuditResult Run()
    {
        var result = new SizeAuditResult("Touchscreen audit");
        AuditLog.Write("[INFO] Touchscreen audit starting");
        TouchSettings saved = TouchSettings.Capture();
        try
        {
            RunHardChecks(result);

            KryptonManager.AutomaticallyDetectTouchscreen = false;
            KryptonManager.UseTouchscreenFontScaling = false;
            KryptonManager.TouchscreenScaleFactorValue = Scale;
            KryptonManager.UseTouchscreenSupport = false;

            using var host = new ControlCatalog.AuditHost();
            foreach (Type type in ControlCatalog.Discover())
            {
                result.Visited++;
                AuditLog.Write("[INFO] Touch " + (type.FullName ?? type.Name));
                AuditOne(host, type, result);
            }
        }
        finally
        {
            saved.Restore();
        }

        result.FailureKeys.Sort(StringComparer.Ordinal);
        result.HardFailures.Sort(StringComparer.Ordinal);
        return result;
    }

    private static void RunHardChecks(SizeAuditResult result)
    {
        bool digitizerReady = (GetSystemMetrics(SmDigitizer) & NidReady) != 0;
        bool reported = false;
        try
        {
            reported = KryptonManager.IsTouchscreenAvailable();
        }
        catch (Exception ex)
        {
            result.HardFailures.Add("touch-detect-threw|" + ex.GetType().Name);
        }

        if (reported != digitizerReady)
        {
            result.HardFailures.Add(
                "touch-detect|reported=" + reported + "|digitizerReady=" + digitizerReady);
        }

        try
        {
            if (KryptonManager.GetMaximumTouchContacts() < 0)
            {
                result.HardFailures.Add("touch-contacts|negative");
            }
        }
        catch (Exception ex)
        {
            result.HardFailures.Add("touch-contacts-threw|" + ex.GetType().Name);
        }

        try
        {
            KryptonManager.AutomaticallyDetectTouchscreen = true;
            if (KryptonManager.UseTouchscreenSupport != KryptonManager.IsTouchscreenAvailable())
            {
                result.HardFailures.Add(
                    "touch-auto|support=" + KryptonManager.UseTouchscreenSupport
                    + "|available=" + KryptonManager.IsTouchscreenAvailable());
            }

            KryptonManager.AutomaticallyDetectTouchscreen = false;
        }
        catch (Exception ex)
        {
            result.HardFailures.Add("touch-auto-threw|" + ex.GetType().Name);
            try
            {
                KryptonManager.AutomaticallyDetectTouchscreen = false;
            }
            catch (Exception)
            {
                // The failure is already recorded.
            }
        }

        int raised = 0;
        EventHandler handler = (_, _) => raised++;
        KryptonManager.GlobalTouchscreenSupportChanged += handler;
        try
        {
            bool original = KryptonManager.UseTouchscreenSupport;
            KryptonManager.UseTouchscreenSupport = !original;
            if (raised < 1)
            {
                result.HardFailures.Add("touch-event|not-raised");
            }

            KryptonManager.TouchscreenScaleFactorValue = Scale;
            KryptonManager.UseTouchscreenSupport = true;
            if (Math.Abs(KryptonManager.TouchscreenScaleFactor - Scale) > 0.01f)
            {
                result.HardFailures.Add("touch-factor|actual=" + KryptonManager.TouchscreenScaleFactor.ToString("0.###") + "|expected=" + Scale.ToString("0.###"));
            }

            float combined = KryptonManager.GetCombinedScaleFactorX();
            float expectedCombined = KryptonManager.GetDpiFactorX() * Scale;
            if (Math.Abs(combined - expectedCombined) > 0.05f)
            {
                result.HardFailures.Add(
                    "touch-combined|actual=" + combined.ToString("0.###") + "|expected=" + expectedCombined.ToString("0.###"));
            }

            KryptonManager.UseTouchscreenSupport = false;
            if (Math.Abs(KryptonManager.TouchscreenScaleFactor - 1f) > 0.01f)
            {
                result.HardFailures.Add("touch-factor-off|actual=" + KryptonManager.TouchscreenScaleFactor.ToString("0.###"));
            }
        }
        catch (Exception ex)
        {
            result.HardFailures.Add("touch-scale-threw|" + ex.GetType().Name);
        }
        finally
        {
            KryptonManager.GlobalTouchscreenSupportChanged -= handler;
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
                result.AddFailure(fullName + "|construct-threw|NullInstance");
                return;
            }

            host.Controls.Add(control);
            ControlCatalog.CreateChild(control);

            KryptonManager.UseTouchscreenFontScaling = false;
            KryptonManager.TouchscreenScaleFactorValue = Scale;
            KryptonManager.UseTouchscreenSupport = false;
            Size off = control.GetPreferredSize(Size.Empty);

            KryptonManager.UseTouchscreenSupport = true;
            Size on = control.GetPreferredSize(Size.Empty);

            bool offEmpty = off.Width <= 0 || off.Height <= 0;
            bool onEmpty = on.Width <= 0 || on.Height <= 0;
            if (offEmpty || onEmpty)
            {
                result.Skipped++;
                result.Notes.Add(fullName + "|skipped|off=" + off.Width + "x" + off.Height + "|on=" + on.Width + "x" + on.Height);
                return;
            }

            bool grew = on.Width + 0.01f >= off.Width * MinimumGrowth
                && on.Height + 0.01f >= off.Height * MinimumGrowth;
            if (!grew)
            {
                result.AddFailure(fullName + "|touch-size-unchanged");
            }

            result.Notes.Add(fullName + "|off=" + off.Width + "x" + off.Height + "|on=" + on.Width + "x" + on.Height);
        }
        catch (Exception ex)
        {
            Exception root = ControlCatalog.Unwrap(ex);
            result.AddFailure(fullName + "|construct-threw|" + root.GetType().Name);
        }
        finally
        {
            KryptonManager.UseTouchscreenSupport = false;
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

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    private sealed class TouchSettings
    {
        private bool _support;
        private bool _fontScaling;
        private float _scale;
        private bool _auto;

        public static TouchSettings Capture()
        {
            return new TouchSettings
            {
                _support = KryptonManager.UseTouchscreenSupport,
                _fontScaling = KryptonManager.UseTouchscreenFontScaling,
                _scale = KryptonManager.TouchscreenScaleFactorValue,
                _auto = KryptonManager.AutomaticallyDetectTouchscreen
            };
        }

        public void Restore()
        {
            try
            {
                KryptonManager.AutomaticallyDetectTouchscreen = false;
                KryptonManager.UseTouchscreenFontScaling = _fontScaling;
                if (_scale > 0)
                {
                    KryptonManager.TouchscreenScaleFactorValue = _scale;
                }

                KryptonManager.UseTouchscreenSupport = _support;
                KryptonManager.AutomaticallyDetectTouchscreen = _auto;
            }
            catch (Exception ex)
            {
                AuditLog.Write("[WARN] Could not restore touch settings: " + ex.Message);
            }
        }
    }
}
