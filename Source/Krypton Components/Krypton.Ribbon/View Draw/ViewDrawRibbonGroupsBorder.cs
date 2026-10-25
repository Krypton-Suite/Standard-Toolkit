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
/// Draws the border around the groups inside the groups area.
/// </summary>
internal class ViewDrawRibbonGroupsBorder : ViewComposite,
    IPaletteRibbonBack
{
    #region Instance Fields
    private readonly Padding _borderPadding2007; // = new(3, 3, 3, 2);
    private readonly Padding _borderPadding2010; // = new(1, 1, 1, 3);
    private readonly Padding _borderPadding2013; // = new(1, 1, 1, 0);
    private readonly Padding _borderPadding365; // = new(1, 1, 1, 0);
    private readonly Padding _borderPaddingVisualStudio2010;
    private readonly Padding _borderPaddingVisualStudio;
    private IPaletteRibbonBack _inherit;
    private IDisposable? _memento;
    private readonly bool _borderOutside;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupsBorder class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="borderOutside">Should border be placed outside the contents.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
    public ViewDrawRibbonGroupsBorder([DisallowNull] KryptonRibbon? ribbon,
        bool borderOutside,
        [DisallowNull] NeedPaintHandler? needPaintDelegate)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(needPaintDelegate is not null);

        // Remember incoming references
        Ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        NeedPaintDelegate = needPaintDelegate ?? throw new ArgumentNullException(nameof(needPaintDelegate));
        _borderOutside = borderOutside;
        _borderPadding2007 = new Padding((int)(3 * FactorDpiX), (int)(3 * FactorDpiY), (int)(3 * FactorDpiX), (int)(2 * FactorDpiY));
        _borderPadding2010 = new Padding((int)(1 * FactorDpiX), (int)(1 * FactorDpiY), (int)(1 * FactorDpiX), (int)(3 * FactorDpiY));
        _borderPaddingVisualStudio2010 = new Padding((int)(1 * FactorDpiX), (int)(1 * FactorDpiY), (int)(1 * FactorDpiX), (int)(3 * FactorDpiY));
        _borderPadding2013 = new Padding((int)(1 * FactorDpiX), (int)(1 * FactorDpiY), (int)(1 * FactorDpiX), 0);
        _borderPadding365 = new Padding((int)(1 * FactorDpiX), (int)(1 * FactorDpiY), (int)(1 * FactorDpiX), 0);
        _borderPaddingVisualStudio = new Padding((int)(1 * FactorDpiX), (int)(1 * FactorDpiY), (int)(1 * FactorDpiX), 0);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupsBorder:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose of old memento first
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region BorderPadding
    /// <summary>
    /// Gets the border padding applied to the view element.
    /// </summary>
    public Padding BorderPadding
    {
        get
        {
            if (Ribbon == null)
            {
                return Padding.Empty;
            }

            return Ribbon.RibbonShape switch
            {
                PaletteRibbonShape.Office2010 => _borderPadding2010,
                PaletteRibbonShape.VisualStudio2010 => _borderPaddingVisualStudio2010,
                PaletteRibbonShape.Office2013 => _borderPadding2013,
                PaletteRibbonShape.Microsoft365 => _borderPadding365,
                PaletteRibbonShape.VisualStudio => _borderPaddingVisualStudio,
                _ => _borderPadding2007
            };
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Get size of the contained items
        Size preferredSize = base.GetPreferredSize(context);

        // Do we need to add on our own border size
        if (!_borderOutside)
        {
            // Add on the border padding
            preferredSize = CommonHelper.ApplyPadding(Orientation.Horizontal, preferredSize, BorderPadding);
        }

        // Override the height to the correct fixed value
        preferredSize.Height = Ribbon.CalculatedValues.GroupsHeight;

        return preferredSize;
    }


    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Do we need to add on our own border size
        if (!_borderOutside)
        {
            // Reduce the display rectangle by the border
            context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, context.DisplayRectangle, BorderPadding);
        }

        // Let children be laid out inside our space
        base.Layout(context);

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // If there is a selected tab and it is a context tab use the context specific palette
        if (!string.IsNullOrEmpty(Ribbon.SelectedTab?.ContextName))
        {
            _inherit = Ribbon.StateContextCheckedNormal.RibbonGroupArea;
            ElementState = PaletteState.ContextCheckedNormal;
        }
        else
        {
            _inherit = Ribbon.StateCheckedNormal.RibbonGroupArea;
            ElementState = PaletteState.CheckedNormal;
        }

        Rectangle drawRect = ClientRectangle;

        // If we need to show the border outside of the client area?
        if (_borderOutside)
        {
            Padding borderPadding = BorderPadding;
            drawRect.X -= borderPadding.Left;
            drawRect.Y -= borderPadding.Top;
            drawRect.Width += borderPadding.Horizontal;
            drawRect.Height += borderPadding.Vertical;
        }
        else if (Ribbon.RibbonShape == PaletteRibbonShape.Office2010)
        {
            // Prevent the left and right edges from being drawn
            drawRect.X -= 1;
            drawRect.Width += 2;
        }

        // Use renderer to draw the tab background
        _memento = context.Renderer.RenderRibbon.DrawRibbonBack(Ribbon.RibbonShape, context, drawRect, State, this, VisualOrientation.Top, _memento);
    }
    #endregion

    #region IPaletteRibbonBack
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => _inherit.GetRibbonBackColorStyle(state);

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
            retColor = CheckForContextColor(state);
        }

        return retColor;
    }

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
            retColor = CheckForContextColor(state);
        }

        return retColor;
    }

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
            retColor = CheckForContextColor(state);
        }

        return retColor;
    }

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
            retColor = CheckForContextColor(state);
        }

        return retColor;
    }

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
            retColor = CheckForContextColor(state);
        }

        return retColor;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets access the source ribbon control.
    /// </summary>
    protected KryptonRibbon Ribbon { get; }

    /// <summary>
    /// Gets access the paint delegate.
    /// </summary>
    protected NeedPaintHandler NeedPaintDelegate { get; }

    #endregion

    #region Implementation
    private Color CheckForContextColor(PaletteState state)
    {
        // We need an associated ribbon tab
        // Does the ribbon tab have a context setting?
        if (!string.IsNullOrEmpty(Ribbon.SelectedTab?.ContextName))
        {
            // Find the context definition for this context
            if (Ribbon.SelectedTab != null)
            {
                KryptonRibbonContext? ribbonContext = Ribbon.RibbonContexts[Ribbon.SelectedTab.ContextName];

                // Should always work, but you never know!
                if (ribbonContext != null)
                {
                    // Return the context specific color
                    return ribbonContext.ContextColor;
                }
            }
        }

        return Color.Empty;
    }
    #endregion
}