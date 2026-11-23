#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Coordinates palette/theme swaps so painting is paused once and resumed once across the app.
/// </summary>
internal static class ThemeChangeCoordinator
{
    private static int _nestDepth;
    public static Form? Initiator { get; private set; }
    private static readonly Dictionary<IntPtr, uint> _prevExStyles = new Dictionary<IntPtr, uint>();
    private static IMessageFilter? _filter;

    /// <summary>
    /// True while a palette/theme change is in progress.
    /// </summary>
    public static volatile bool InProgress;

    public static void Begin() => Begin(null);

    public static void Begin(Form? initiator)
    {
        _nestDepth++;
        if (_nestDepth > 1)
        {
            InProgress = true;
            return;
        }

        InProgress = true;
        Initiator = initiator;
        // Install a transient message filter to swallow risky combo notifications on non-initiator forms
        if (_filter == null)
        {
            _filter = new ComboCommandFilter();
            try { Application.AddMessageFilter(_filter); } catch { /* ignore */ }
        }
        foreach (Form form in Application.OpenForms)
        {
            try
            {
                if (form.IsHandleCreated)
                {
                    // Temporarily enable WS_EX_COMPOSITED to reduce child flicker during the swap
                    var h = form.Handle;
                    uint ex = PI.GetWindowLong(h, PI.GWL_.EXSTYLE);
                    if ((ex & PI.WS_EX_.COMPOSITED) == 0)
                    {
                        _prevExStyles[h] = ex;
                        PI.SetWindowLong(h, PI.GWL_.EXSTYLE, ex | PI.WS_EX_.COMPOSITED);
                        // Apply style change without moving/resizing
                        PI.SetWindowPos(h, IntPtr.Zero, 0, 0, 0, 0,
                            PI.SWP_.NOACTIVATE | PI.SWP_.NOMOVE | PI.SWP_.NOZORDER | PI.SWP_.NOSIZE | PI.SWP_.NOOWNERZORDER | PI.SWP_.FRAMECHANGED);
                    }
                }
                // Keep redraw enabled for the initiating form so its WM_COMMAND completes safely
                if (ReferenceEquals(form, initiator))
                {
                    continue;
                }

                if (form.IsHandleCreated)
                {
                    PI.SendMessage(form.Handle, PI.SETREDRAW, IntPtr.Zero, IntPtr.Zero);
                    form.SuspendLayout();
                }
            }
            catch
            {
                // Ignore and continue for robustness during handle churn
            }
        }
    }

    public static void End()
    {
        if (_nestDepth == 0)
        {
            return;
        }

        _nestDepth--;
        if (_nestDepth > 0)
        {
            return;
        }

        try
        {
            foreach (Form form in Application.OpenForms)
            {
                try
                {
                    if (form.IsHandleCreated)
                    {
                        // Re-enable redraw and resume a single repaint of the form and its children
                        PI.SendMessage(form.Handle, PI.SETREDRAW, (IntPtr)1, IntPtr.Zero);
                        form.ResumeLayout(true);
                        // Mark the entire subtree as needing paint, then update immediately including the frame
                        form.Invalidate(true);
                        // Composite one-pass update including children and frame to reduce flicker
                        PI.RedrawWindow(form.Handle, IntPtr.Zero, IntPtr.Zero,
                            PI.RDW_INVALIDATE | PI.RDW_ALLCHILDREN | PI.RDW_UPDATENOW | PI.RDW_FRAME);

                        // Restore original extended style if we changed it
                        var h = form.Handle;
                        if (_prevExStyles.TryGetValue(h, out uint prev))
                        {
                            PI.SetWindowLong(h, PI.GWL_.EXSTYLE, prev);
                            PI.SetWindowPos(h, IntPtr.Zero, 0, 0, 0, 0,
                                PI.SWP_.NOACTIVATE | PI.SWP_.NOMOVE | PI.SWP_.NOZORDER | PI.SWP_.NOSIZE | PI.SWP_.NOOWNERZORDER | PI.SWP_.FRAMECHANGED);
                        }
                    }
                }
                catch
                {
                    // Ignore per-form failures; continue resuming others
                }
            }
        }
        finally
        {
            InProgress = false;
            Initiator = null;
            _prevExStyles.Clear();
            if (_filter != null)
            {
                try { Application.RemoveMessageFilter(_filter); } catch { /* ignore */ }
                _filter = null;
            }
        }
    }

    /// <summary>
    /// Swallows selection-related WM_COMMAND notifications from ComboBox controls on non-initiator forms during theme swaps.
    /// Centralizes handling to avoid per-control re-entrancy.
    /// </summary>
    private sealed class ComboCommandFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            if (!InProgress)
            {
                return false;
            }

            if (m.Msg != (int)PI.WM_.COMMAND)
            {
                return false;
            }

            // Control notifications set lParam to control HWND; menu commands set lParam = 0
            if (m.LParam == IntPtr.Zero)
            {
                return false;
            }

            var ctl = Control.FromHandle(m.LParam);
            if (ctl == null)
            {
                return false;
            }

            var ownerForm = ctl.FindForm();
            if (ownerForm == null || ReferenceEquals(ownerForm, Initiator))
            {
                return false;
            }

            int code = PI.HIWORD(m.WParam);
            // CBN_* selection-related notifications
            if (code is 0x0001 /*SELCHANGE*/ or 0x0009 /*SELENDOK*/ or 0x000A /*SELENDCANCEL*/)
            {
#if DEBUG
                DebugLogger.WriteLine($"Filter swallow WM_COMMAND CBN_* on form '{ownerForm.Text}' while swapping.");
#endif
                return true; // swallow
            }

            return false;
        }

    }
}
