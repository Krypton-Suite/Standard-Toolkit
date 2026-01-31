#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxBitmap(typeof(ToolStrip)), Description(@"A standard tool strip equipped with the Krypton theme."), ToolboxItem(true)]
public class KryptonToolStrip : ToolStrip,
    IFocusLostMenuItem
{
    #region Fields
    private bool _disposed;
    #endregion

    #region Constructor
    public KryptonToolStrip()
    {
        _disposed = false;

        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;

        // Register with the FocusLostMenuHelper
        Register(this);
    }
    #endregion

    #region Override
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            // Deregister from the FocusLostMenuHelper
            Deregister(this);
        }

        base.Dispose(disposing);
    }
    #endregion

    #region IFocusLostMenuItem
    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ProcessItem()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] is ToolStripDropDownButton dropDownItem
                && dropDownItem.DropDown.Visible)
            {
                dropDownItem.DropDown.Close(ToolStripDropDownCloseReason.AppFocusChange);
                return;
            }
        }
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Register(IFocusLostMenuItem item)
    {
        FocusLostMenuHelper.Register(item);
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deregister(IFocusLostMenuItem item)
    {
        FocusLostMenuHelper.Deregister(item);
    }
    #endregion

}
