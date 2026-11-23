#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(KryptonTextBox)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class KryptonToolStripTextBox : ToolStripControlHost
{
    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTextBox? KryptonTextBox => Control as KryptonTextBox;

    #endregion

    #region Constructor
    public KryptonToolStripTextBox() : base(CreateControlInstance())
    {

    }
    #endregion

    #region Overrides
    protected override void OnSubscribeControlEvents(Control? control)
    {
        if (control is KryptonTextBox kryptonTextBox)
        {
            kryptonTextBox.TextAlignChanged += TextAlignChanged;

            kryptonTextBox.TextChanged += Text_Changed;

            kryptonTextBox.FontChanged += FontChanged;
        }

        base.OnSubscribeControlEvents(control);
    }

    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        if (control is KryptonTextBox kryptonTextBox)
        {
            kryptonTextBox.TextAlignChanged -= TextAlignChanged;

            kryptonTextBox.TextChanged -= Text_Changed;

            kryptonTextBox.FontChanged -= FontChanged;
        }

        base.OnUnsubscribeControlEvents(control);
    }
    #endregion

    #region Methods
    private static Control CreateControlInstance()
    {
        KryptonTextBox kryptonTextBox = new KryptonTextBox();

        //TODO: Add other initialization code here.


        return kryptonTextBox;
    }
    #endregion

    #region Event Handlers
    private void Text_Changed(object? sender, EventArgs e)
    {
        OnTextChanged(e);
    }

    private void TextAlignChanged(object? sender, EventArgs e)
    {
        // Hosted KryptonTextBox alignment changed; no additional host-side work required.
    }

    private void FontChanged(object? sender, EventArgs e)
    {
        // Hosted KryptonTextBox font changed; no additional host-side work required.
    }
    #endregion
}