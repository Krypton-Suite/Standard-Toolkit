#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Derived class from KryptonForm for use in KryptonTaskDialog.
/// </summary>
public class KryptonTaskDialogKryptonForm : KryptonForm
{
    #region Static
    private const Keys KEYS_ALT_F4 = Keys.Alt | Keys.F4;
    #endregion

    #region public
    /// <summary>
    /// Disable close on ALT+F4
    /// </summary>
    [DefaultValue(false)]
    public bool IgnoreAltF4 { get; set; }
    #endregion

    #region Identity
    public KryptonTaskDialogKryptonForm()
    {
        IgnoreAltF4 = false;
    }
    #endregion

    #region Protected override
    /// <inheritdoc/>>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Intercept ALT+F4
        return IgnoreAltF4 && keyData == KEYS_ALT_F4;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Always hide the form while the user operates it.
        // Else let it close itself.
        if (Visible && e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = false;
            Hide();
        }
        else
        {
            DialogResult = DialogResult.None;
        }

        base.OnFormClosing(e);
    }  
    #endregion
}