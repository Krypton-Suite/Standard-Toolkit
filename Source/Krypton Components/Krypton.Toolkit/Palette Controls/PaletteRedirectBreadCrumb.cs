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

/// <summary>
/// Redirect back/border/content based on the incoming grid state and style.
/// </summary>
public class PaletteRedirectBreadCrumb : PaletteRedirect
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBreadCrumb class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectBreadCrumb(PaletteBase target)
        : base(target)
    {
        Left = false;
        Right = false;
        TopBottom = true;
    }
    #endregion

    #region Left
    /// <summary>
    /// Gets and sets if the left border should be removed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Left { get; set; }

    #endregion

    #region Right
    /// <summary>
    /// Gets and sets if the right border should be removed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Right { get; set; }

    #endregion

    #region TopBottom
    /// <summary>
    /// Gets and sets if the top and bottom borders should be removed.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public bool TopBottom { get; set; }

    #endregion

    #region GetBorderDrawBorders
    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        PaletteDrawBorders borders = base.GetBorderDrawBorders(style, state);

        // We are only interested in bread crums buttons
        if (style == PaletteBorderStyle.ButtonBreadCrumb)
        {
            if (Left)
            {
                borders &= ~PaletteDrawBorders.Left;
            }

            if (Right)
            {
                borders &= ~PaletteDrawBorders.Right;
            }

            if (TopBottom)
            {
                borders &= ~PaletteDrawBorders.TopBottom;
            }
        }

        return borders;
    }
    #endregion
}