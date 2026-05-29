#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonPanelDesigner : ScrollableControlDesigner
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPanelDesigner class.
    /// </summary>
    public KryptonPanelDesigner() =>
        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

    #endregion

    #region Public Overrides
    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the panel specific list
                new KryptonPanelActionList(this)
            };

            return actionLists;
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Receives a call when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.
    /// </summary>
    /// <param name="pe">A PaintEventArgs the designer can use to draw on the control.</param>
    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
        // Let base class paint first
        base.OnPaintAdornments(pe);

        // Always draw a border around the panel
        DrawBorder(pe.Graphics);
    }
    #endregion

    #region Implementation
    private void DrawBorder(Graphics graphics)
    {
        // Create a pen for drawing
        using var borderPen = new Pen(SystemColors.ControlDarkDark);
        // Always draw the border dashed
        borderPen.DashStyle = DashStyle.Dash;

        // Get the client rectangle
        Rectangle clientRect = Control.ClientRectangle;

        // Reduce by 1 in width and height
        clientRect.Width--;
        clientRect.Height--;

        // Perform actual draw
        graphics.DrawRectangle(borderPen, clientRect);
    }
    #endregion
}