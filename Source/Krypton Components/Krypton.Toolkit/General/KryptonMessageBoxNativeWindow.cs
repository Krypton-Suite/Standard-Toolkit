#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Intercepts unwanted messages on the KryptonRichTextBox used in MessageBox forms
/// </summary>
internal class KryptonMessageBoxNativeWindow : NativeWindow
{
    protected override void WndProc(ref Message m)
    {
        // Prevent the user from entering the control
        if (m.Msg == PI.WM_.SETFOCUS)
        {
            m.Msg = PI.WM_.KILLFOCUS;
        }

        // Disable zoom, eat the message
        if (m.Msg == PI.WM_.MOUSEWHEEL)
        {
            return;
        }

        base.WndProc(ref m);
    }
}