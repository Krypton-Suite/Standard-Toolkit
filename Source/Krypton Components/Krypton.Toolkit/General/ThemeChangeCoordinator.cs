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

    /// <summary>
    /// True while a palette/theme change is in progress.
    /// </summary>
    public static volatile bool InProgress;

    public static void Begin()
    {
        _nestDepth++;
        if (_nestDepth > 1)
        {
            InProgress = true;
            return;
        }

        InProgress = true;
        foreach (Form form in Application.OpenForms)
        {
            try
            {
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
                        PI.SendMessage(form.Handle, PI.SETREDRAW, (IntPtr)1, IntPtr.Zero);
                        form.ResumeLayout(true);
                        form.Invalidate(true);
                        // Force non-client (chrome) redraw to immediately reflect new palette
                        PI.RedrawWindow(form.Handle, IntPtr.Zero, IntPtr.Zero,
                            PI.RDW_INVALIDATE | PI.RDW_UPDATENOW | PI.RDW_FRAME);
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
        }
    }
}
