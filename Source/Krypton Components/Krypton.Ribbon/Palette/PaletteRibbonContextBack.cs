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
public class PaletteRibbonContextBack : IPaletteRibbonBack
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private IPaletteRibbonBack _inherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonContextBack class.
    /// </summary>
    /// <param name="ribbon">Reference to ribbon control.</param>
    public PaletteRibbonContextBack([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon is not null);

        if (ribbon is null)
        {
            throw new ArgumentNullException(nameof(ribbon));
        }

        _ribbon = ribbon;
    }
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteRibbonBack inherit) => _inherit = inherit;
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
        else
        {
            if (state is PaletteState.ContextNormal or PaletteState.ContextTracking or PaletteState.ContextPressed)
            {
                // For context drawing we merge the incoming color and the context color
                Color contextColor = CheckForContextColor();
                return CommonHelper.MergeColors(retColor, 0.5f, contextColor, 0.5f);
            }
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
    #region Implementation
    private Color CheckForContextColor()
    {
        // We need an associated ribbon tab
        // Does the ribbon tab have a context setting?
        if (_ribbon is not null && _ribbon.SelectedTab is not null)
        {
            KryptonRibbonTab selectedTab = _ribbon.SelectedTab;
            if (!string.IsNullOrEmpty(selectedTab?.ContextName))
            {
                // Find the context definition for this context
                if (selectedTab != null)
                {
                    KryptonRibbonContext? ribbonContext = _ribbon.RibbonContexts[selectedTab.ContextName];

                    // Should always work, but you never know!
                    if (ribbonContext != null)
                    {
                        return ribbonContext.ContextColor;
                    }
                }
            }
        }

        return Color.Empty;
    }
    #endregion
}