#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws a context title.
/// </summary>
internal class ViewDrawRibbonContextTitle : ViewLeaf,
    IPaletteRibbonBack,
    IContentValues
{

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private ContextTabSet? _context;
    private readonly IPaletteRibbonBack _inherit;
    private readonly ContextToContent _contentProvider;
    private IDisposable? _mementoBack;
    private IDisposable? _mementoContentText;
    private IDisposable? _mementoContentShadow1;
    private IDisposable? _mementoContentShadow2;
    private Rectangle _textRect;
    private readonly int _textSideGap; // = 4;
    private readonly int _textBottomGap; // = 3;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonContextTitle class.
    /// </summary>
    /// <param name="ribbon">Source ribbon control.</param>
    /// <param name="inherit">Source for inheriting the ribbon background colors.</param>
    public ViewDrawRibbonContextTitle([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] IPaletteRibbonBack inherit)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(inherit != null);

        _textSideGap = (int)(4 * FactorDpiX);
        _textBottomGap = (int)(3 * FactorDpiY);

        // Remember incoming references
        _inherit = inherit!;
        _ribbon = ribbon!;

        // Use a class to convert from ribbon tab to content interface
        _contentProvider = new ContextToContent(_ribbon.StateCommon.RibbonGeneral);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonContextTitle:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementoBack != null)
            {
                _mementoBack.Dispose();
                _mementoBack = null;
            }

            if (_mementoContentText != null)
            {
                _mementoContentText.Dispose();
                _mementoContentText = null;
            }

            if (_mementoContentShadow1 != null)
            {
                _mementoContentShadow1.Dispose();
                _mementoContentShadow1 = null;
            }

            if (_mementoContentShadow2 != null)
            {
                _mementoContentShadow2.Dispose();
                _mementoContentShadow2 = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region ContextTabSet
    /// <summary>
    /// Gets and sets the context to display.
    /// </summary>
    public ContextTabSet? ContextTabSet
    {
        get => _context;

        set
        {
            _context = value;

            // Update the component we are associated with
            Component = _context?.Context;
        }
    }
    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets the visible state of the element.
    /// </summary>
    public override bool Visible
    {
        get => _ribbon.Visible && base.Visible;
        set => base.Visible = value;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) =>
        // We have no preference, just set our size to whatever is needed
        Size.Empty;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        ClientRectangle = context!.DisplayRectangle;

        // We always extend an extra pixel downwards to draw over the containers border
        var adjustRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientWidth, ClientHeight + 1);

        // Get the client rect of the parent
        Rectangle parentRect = Parent!.ClientRectangle;

        // If we are only partially visible on the right hand side
        if ((adjustRect.X < parentRect.Right) && (adjustRect.Right >= parentRect.Right))
        {
            // Truncate on the right hand side to the parent
            adjustRect.Width = parentRect.Right - adjustRect.X;
        }

        // If we are only partially visible on the left hand side
        if ((adjustRect.Right > parentRect.X) && (adjustRect.X < parentRect.X))
        {
            // Truncate on the left hand side to the parent
            adjustRect.Width = adjustRect.Right - parentRect.X;
            adjustRect.X = parentRect.X;
        }

        // Use adjusted rectangle as our client rectangle
        ClientRectangle = adjustRect;

        // Use the font height to decide on the text rectangle
        var fontHeight = _ribbon.CalculatedValues.DrawFontHeight;
        _textRect = new Rectangle(ClientLocation.X + _textSideGap,
            ClientLocation.Y + (ClientHeight - fontHeight - _textBottomGap),
            ClientWidth - (_textSideGap * 2),
            fontHeight);

        // Remember to dispose of old memento
        if (_mementoContentText != null)
        {
            _mementoContentText.Dispose();
            _mementoContentText = null;
        }

        if (_mementoContentShadow1 != null)
        {
            _mementoContentShadow1.Dispose();
            _mementoContentShadow1 = null;
        }

        if (_mementoContentShadow2 != null)
        {
            _mementoContentShadow2.Dispose();
            _mementoContentShadow2 = null;
        }

        // Office 2010 draws a shadow effect of the text
        if (_ribbon.RibbonShape is PaletteRibbonShape.Office2010 or PaletteRibbonShape.VisualStudio2010 or PaletteRibbonShape.Office2013 or PaletteRibbonShape.Microsoft365 or PaletteRibbonShape.VisualStudio)
        {
            var shadowTextRect1 = _textRect with { X = _textRect.X - 1, Y = _textRect.Y + 1 };
            var shadowTextRect2 = _textRect with { X = _textRect.X + 1, Y = _textRect.Y + 1 };

            _contentProvider.OverrideTextColor = Color.FromArgb(128, ControlPaint.Dark(GetRibbonBackColor1(PaletteState.Normal)));

            _contentProvider.OverrideTextHint = PaletteTextHint.SingleBitPerPixelGridFit;

            _mementoContentShadow1 = context.Renderer.RenderStandardContent.LayoutContent(context, shadowTextRect1,
                _contentProvider, this,
                VisualOrientation.Top,
                PaletteState.Normal);

            _mementoContentShadow2 = context.Renderer.RenderStandardContent.LayoutContent(context, shadowTextRect2,
                _contentProvider, this,
                VisualOrientation.Top,
                PaletteState.Normal);
            _contentProvider.OverrideTextColor = Color.Empty;
        }

        // Use the renderer to layout the text
        _mementoContentText = context.Renderer.RenderStandardContent.LayoutContent(context, _textRect,
            _contentProvider, this,
            VisualOrientation.Top,
            PaletteState.Normal);

        _contentProvider.OverrideTextHint = PaletteTextHint.Inherit;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Office 2010 draws a shadow effect of the text
        if ((_ribbon.RibbonShape == PaletteRibbonShape.Office2010) && (_mementoContentShadow1 != null))
        {
            PaletteState state = _ribbon.Enabled ? PaletteState.Normal : PaletteState.Disabled;

            // Use renderer to draw the tab background
            _mementoBack = context.Renderer.RenderRibbon.DrawRibbonTabContextTitle(_ribbon.RibbonShape, context, ClientRectangle, _ribbon.StateCommon.RibbonGeneral, this, _mementoBack!);

            var shadowTextRect1 = _textRect with { X = _textRect.X - 1, Y = _textRect.Y + 1 };
            var shadowTextRect2 = _textRect with { X = _textRect.X + 1, Y = _textRect.Y + 1 };

            _contentProvider.OverrideTextColor = Color.FromArgb(128, ControlPaint.Dark(GetRibbonBackColor1(PaletteState.Normal)));

            _contentProvider.OverrideTextHint = PaletteTextHint.SingleBitPerPixelGridFit;

            context.Renderer.RenderStandardContent.DrawContent(context, shadowTextRect1,
                _contentProvider, _mementoContentShadow1,
                VisualOrientation.Top,
                state, true);

            context.Renderer.RenderStandardContent.DrawContent(context, shadowTextRect2,
                _contentProvider, _mementoContentShadow2!,
                VisualOrientation.Top,
                state, true);

            _contentProvider.OverrideTextColor = Color.Empty;

            // Use renderer to draw the text content
            if (_mementoContentText != null)
            {
                context.Renderer.RenderStandardContent.DrawContent(context, _textRect,
                    _contentProvider, _mementoContentText,
                    VisualOrientation.Top,
                    state, true);
            }

            _contentProvider.OverrideTextHint = PaletteTextHint.Inherit;
        }
        else
        {
            PaletteState state = _ribbon.Enabled ? PaletteState.Normal : PaletteState.Disabled;

            // Use renderer to draw the tab background
            _mementoBack = context.Renderer.RenderRibbon.DrawRibbonTabContextTitle(_ribbon.RibbonShape, context, ClientRectangle, _ribbon.StateCommon.RibbonGeneral, this, _mementoBack!);

            // Use renderer to draw the text content
            if (_mementoContentText != null)
            {
                context.Renderer.RenderStandardContent.DrawContent(context, _textRect,
                    _contentProvider, _mementoContentText,
                    VisualOrientation.Top,
                    state, true);
            }
        }
    }
    #endregion

    #region IPaletteRibbonBack
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => PaletteRibbonColorStyle.RibbonGroupAreaBorder;

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

    private Color CheckForContextColor() =>
        // We need an associated context
        _context?.ContextColor ?? Color.Empty;

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the image used for the ribbon tab.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Image.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be interpreted as transparent.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Transparent Color.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetShortText() => _context?.ContextTitle ?? string.Empty;

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}