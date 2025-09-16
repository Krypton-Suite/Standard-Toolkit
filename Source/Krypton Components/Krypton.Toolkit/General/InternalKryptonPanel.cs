#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internal KryptonPanel that prevents designer selection to ensure clicks pass through to the parent KryptonForm.
/// </summary>
internal class InternalKryptonPanel : KryptonPanel
{
    protected override void WndProc(ref Message m)
    {
        // In designer mode, prevent this panel from being selected
        // This ensures clicks pass through to the parent KryptonForm
        if (Site?.DesignMode == true)
        {
            // For mouse messages, pass them to the parent form instead
            if (m.Msg == PI.WM_.LBUTTONDOWN || m.Msg == PI.WM_.RBUTTONDOWN || 
                m.Msg == PI.WM_.LBUTTONUP || m.Msg == PI.WM_.RBUTTONUP)
            {
                // Forward the message to the parent form using SendMessage
                var parentForm = FindForm();
                if (parentForm != null)
                {
                    var parentPoint = PointToScreen(new Point(m.LParam.ToInt32() & 0xFFFF, m.LParam.ToInt32() >> 16));
                    var parentClientPoint = parentForm.PointToClient(parentPoint);
                    var parentLParam = (parentClientPoint.Y << 16) | (parentClientPoint.X & 0xFFFF);
                    
                    // Send the message directly to the parent form's window handle
                    PI.SendMessage(parentForm.Handle, m.Msg, m.WParam, new IntPtr(parentLParam));
                    return;
                }
            }
        }
        
        base.WndProc(ref m);
    }
}