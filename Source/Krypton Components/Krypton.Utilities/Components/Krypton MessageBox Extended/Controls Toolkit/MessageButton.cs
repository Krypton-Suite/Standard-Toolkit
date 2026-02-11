

namespace Krypton.Utilities;

[ToolboxItem(false)]
[DesignTimeVisible(false)]
internal class MessageButton : InternalKryptonButton
{

    #region Instance Fields

    #endregion

    #region Identity
    public MessageButton()
    {
        IgnoreAltF4 = false;
        Visible = false;
        Enabled = false;
    }

    /// <summary>
    /// Gets and sets the ignoring of Alt+F4
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool IgnoreAltF4 { get; set; }

    #endregion

    #region Protected
    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows Message to process. </param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.KEYDOWN:
            case PI.WM_.SYSKEYDOWN:
                if (IgnoreAltF4)
                {
                    // Extract the keys being pressed
                    Keys keys = (Keys)(int)m.WParam.ToInt64();

                    // If the user standard combination ALT + F4
                    if (keys == Keys.F4 && (ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        // Eat the message, so standard window proc does not close the window
                        return;
                    }
                }
                break;
        }

        base.WndProc(ref m);
    }
    #endregion
}