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
/// View element that can draw a panel (background but no border)
/// </summary>
public class ViewDrawPanel : ViewComposite
{
    #region Instance Fields
    internal IPaletteBack _paletteBack;
    private IDisposable? _memento;
    private RightToLeft _rightToLeft;
    private bool _rightToLeftLayout;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawPanel class.
    /// </summary>
    public ViewDrawPanel()
    {
        VisualOrientation = VisualOrientation.Top;
        IgnoreRender = false;
        _rightToLeft = RightToLeft.No;
        _rightToLeftLayout = false;
    }
        
    /// <summary>
    /// Initialize a new instance of the ViewDrawPanel class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    public ViewDrawPanel([DisallowNull] IPaletteBack paletteBack)
    {
        Debug.Assert(paletteBack != null);
        _paletteBack = paletteBack!;
        VisualOrientation = VisualOrientation.Top;
        _rightToLeft = RightToLeft.No;
        _rightToLeftLayout = false;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawPanel:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion
        
    #region IgnoreRender
    /// <summary>
    /// Gets and sets the rendering status.
    /// </summary>
    public bool IgnoreRender { get; set; }

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the orientation of the panel.
    /// </summary>
    public VisualOrientation VisualOrientation { get; set; }

    #endregion

    #region RTL Support
    /// <summary>
    /// Gets and sets the RightToLeft setting for RTL-aware rendering.
    /// </summary>
    public RightToLeft RightToLeft
    {
        get => _rightToLeft;
        set => _rightToLeft = value;
    }

    /// <summary>
    /// Gets and sets the RightToLeftLayout setting for RTL-aware rendering.
    /// </summary>
    public bool RightToLeftLayout
    {
        get => _rightToLeftLayout;
        set => _rightToLeftLayout = value;
    }

    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the source palettes for drawing.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    public void SetPalettes([DisallowNull] IPaletteBack paletteBack)
    {
        Debug.Assert(paletteBack != null);

        // Use newly provided palettes
        _paletteBack = paletteBack!;
    }

    /// <summary>
    /// Gets the palette used for drawing the panel.
    /// </summary>
    /// <returns></returns>
    public IPaletteBack GetPalette() => _paletteBack;

    #endregion

    #region Eval
    /// <summary>
    /// Evaluate the need for drawing transparent areas.
    /// </summary>
    /// <param name="context">Evaluation context.</param>
    /// <returns>True if transparent areas exist; otherwise false.</returns>
    public override bool EvalTransparentPaint([DisallowNull] ViewContext context)
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

        // Ask the renderer to evaluate the given palette
        return context!.Renderer.EvalTransparentPaint(_paletteBack, State);
    }
    #endregion

    #region Layout

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Cache RTL settings from the control
        if (context.Control != null)
        {
            _rightToLeft = context.Control.RightToLeft;
            _rightToLeftLayout = CommonHelper.GetRightToLeftLayout(context.Control);
        }

        // We take on all the available display area
        if (context.Control is KryptonForm)
        {
            Rectangle contextDisplayRectangle = context.DisplayRectangle;
            contextDisplayRectangle.Width -= 10;
            context.DisplayRectangle = contextDisplayRectangle;
        }

        ClientRectangle = context.DisplayRectangle;

        // Apply RTL layout adjustments if needed
        if (_rightToLeft == RightToLeft.Yes && _rightToLeftLayout)
        {
            ApplyRTLLayoutAdjustments(context);
        }

        // Let child elements layout
        base.Layout(context);
    }

    /// <summary>
    /// Apply RTL layout adjustments to child elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    private void ApplyRTLLayoutAdjustments(ViewLayoutContext context)
    {
        // For RTL layout, we need to adjust the display rectangle
        // This is a basic implementation - more sophisticated RTL handling
        // would be needed for complex layouts
        if (context.DisplayRectangle.Width > 0)
        {
            // In RTL mode, we might need to adjust the layout context
            // This is a placeholder for more advanced RTL layout logic
            var adjustedRect = context.DisplayRectangle;
            // Apply RTL-specific adjustments here if needed
            context.DisplayRectangle = adjustedRect;
        }
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context) 
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        if (!IgnoreRender)
        {
            // Do we need to draw the background?
            if (_paletteBack.GetBackDraw(State) == InheritBool.True)
            {
                // Render the background
                using var panelPath = new GraphicsPath();
                var rectF = ClientRectangleF;
                // Now workaround https://github.com/Krypton-Suite/Standard-Toolkit/issues/308
                rectF.Offset(-0.25f, -0.25f);
                // The path encloses the entire panel area
                panelPath.AddRectangle(rectF);

                // Apply RTL rendering adjustments if needed
                if (_rightToLeft == RightToLeft.Yes && _rightToLeftLayout)
                {
                    ApplyRTLRenderingAdjustments(context);
                }

                // Perform actual panel drawing
                _memento = context.Renderer.RenderStandardBack.DrawBack(context, ClientRectangle, panelPath, _paletteBack, VisualOrientation, State, _memento);
            }
        }
    }

    /// <summary>
    /// Apply RTL rendering adjustments.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    private void ApplyRTLRenderingAdjustments(RenderContext context)
    {
        // For RTL rendering, we might need to adjust the graphics context
        // This is a placeholder for more advanced RTL rendering logic
        if (context.Graphics != null)
        {
            // Apply RTL-specific rendering adjustments here if needed
            // For example, flipping the coordinate system for RTL
        }
    }
    #endregion
}