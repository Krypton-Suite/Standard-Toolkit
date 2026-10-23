#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Return inherited values unless empty in which case return the context color.
/// </summary>
public class PaletteRibbonContextDouble : IPaletteRibbonBack,
    IPaletteRibbonText
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private PaletteRibbonDoubleInheritOverride _inherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonContextDouble class.
    /// </summary>
    /// <param name="ribbon">Reference to ribbon control.</param>
    public PaletteRibbonContextDouble([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon is not null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        LightBackground = false;
    }
    #endregion

    #region RibbonTab
    /// <summary>
    /// Gets and sets the associated ribbon tab.
    /// </summary>
    public KryptonRibbonTab? RibbonTab { get; set; }

    #endregion

    #region LightBackground
    /// <summary>
    /// Gets and sets a value indicating if the text is being drawn on a light coloured background.
    /// </summary>
    public bool LightBackground { get; set; }

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(PaletteRibbonDoubleInheritOverride inherit) => _inherit = inherit;
    #endregion

    #region BackColorStyle
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => _inherit.GetRibbonBackColorStyle(state);

    #endregion

    #region BackColor1
    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor1(PaletteState state)
    {
        Color retColor = _inherit.GetRibbonBackColor1(state);

        // If empty then try and recover the context specific color
        if (retColor == Color.Empty)
        {
            retColor = CheckForContextColor();
        }

        return retColor;
    }
    #endregion

    #region BackColor2
    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor2(PaletteState state)
    {
        Color retColor = _inherit.GetRibbonBackColor2(state);

        // If empty then try and recover the context specific color
        if (retColor == Color.Empty)
        {
            retColor = CheckForContextColor();
        }

        return retColor;
    }
    #endregion

    #region BackColor3
    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor3(PaletteState state)
    {
        Color retColor = _inherit.GetRibbonBackColor3(state);

        // If empty then try and recover the context specific color
        if (retColor == Color.Empty)
        {
            retColor = CheckForContextColor();
        }

        return retColor;
    }
    #endregion

    #region BackColor4
    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor4(PaletteState state)
    {
        Color retColor = _inherit.GetRibbonBackColor4(state);

        // If empty then try and recover the context specific color
        if (retColor == Color.Empty)
        {
            retColor = CheckForContextColor();
        }

        return retColor;
    }
    #endregion

    #region BackColor5
    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor5(PaletteState state)
    {
        Color retColor = _inherit.GetRibbonBackColor5(state);

        // If empty then try and recover the context specific color
        if (retColor == Color.Empty)
        {
            retColor = CheckForContextColor();
        }

        return retColor;
    }
    #endregion

    #region TextColor
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTextColor(PaletteState state)
    {
        return _inherit.GetRibbonTextColor(state);

        // #1399 Disable this override on the theme color arrays and accepting the normal return color
        // The approach below goes outside of the theme color array and causes problems.

        //// If empty then try and recover the context specific color
        //if (retColor == Color.Empty)
        //{
        //    retColor = CheckForContextColor();
        //}
        //else if ((state == PaletteState.Normal) && LightBackground)
        //{
        //    // With a light background we force the color to be dark in normal state so it stands out
        //    return Color.FromArgb(Math.Min(retColor.R, (byte)60),
        //                          Math.Min(retColor.G, (byte)60),
        //                          Math.Min(retColor.B, (byte)60));
        //}

        //return retColor;
    }
    #endregion

    #region Implementation
    private Color CheckForContextColor()
    {
        // We need an associated ribbon tab
        // Does the ribbon tab have a context setting?
        if (!string.IsNullOrEmpty(RibbonTab?.ContextName))
        {
            // Find the context definition for this context
            KryptonRibbonContext? ribbonContext = _ribbon.RibbonContexts[RibbonTab!.ContextName];

            // Should always work, but you never know!
            if (ribbonContext != null)
            {
                return ribbonContext.ContextColor;
            }
        }

        return Color.Empty;
    }
    #endregion
}