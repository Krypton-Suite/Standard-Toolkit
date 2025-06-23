#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Intercepts unwanted messages on the KryptonRichTextBox used in MessageBox forms
/// </summary>
internal class KryptonMessageBoxNativeWindow : NativeWindow
{
    private const int WM_SETFOCUS = 0x0007;
    private const int WM_KILLFOCUS = 0x0008;
    private const int WM_MOUSEWHEEL = 0x020A;

    protected override void WndProc(ref Message m)
    {
        // Prevent the user from entering the control
        if (m.Msg == WM_SETFOCUS)
        {
            m.Msg = WM_KILLFOCUS;
        }

        // Disable zoom, eat the message
        if (m.Msg == WM_MOUSEWHEEL)
        {
            return;
        }

        base.WndProc(ref m);
    }
}