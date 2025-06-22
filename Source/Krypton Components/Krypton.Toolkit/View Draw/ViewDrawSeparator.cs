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
/// View element that can draw a separator
/// </summary>
public class ViewDrawSeparator : ViewLeaf
{
    #region Instance Fields
    internal IPaletteDouble? _paletteDisabled;
    internal IPaletteDouble? _paletteNormal;
    internal IPaletteDouble? _paletteTracking;
    internal IPaletteDouble? _palettePressed;
    internal IPaletteMetric? _metricDisabled;
    internal IPaletteMetric? _metricNormal;
    internal IPaletteMetric? _metricTracking;
    internal IPaletteMetric? _metricPressed;
    internal IPaletteDouble? _palette;
    internal IPaletteMetric? _metric;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawSeparator class.
    /// </summary>
    /// <param name="paletteDisabled">Palette source for the disabled state.</param>
    /// <param name="paletteNormal">Palette source for the normal state.</param>
    /// <param name="paletteTracking">Palette source for the tracking state.</param>
    /// <param name="palettePressed">Palette source for the pressed state.</param>
    /// <param name="metricDisabled">Palette source for disabled metric values.</param>
    /// <param name="metricNormal">Palette source for normal metric values.</param>
    /// <param name="metricTracking">Palette source for tracking metric values.</param>
    /// <param name="metricPressed">Palette source for pressed metric values.</param>
    /// <param name="metricPadding">Metric used to get padding values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawSeparator([DisallowNull] IPaletteDouble paletteDisabled, [DisallowNull] IPaletteDouble paletteNormal,
        [DisallowNull] IPaletteDouble paletteTracking, [DisallowNull] IPaletteDouble palettePressed,
        [DisallowNull] IPaletteMetric metricDisabled, [DisallowNull] IPaletteMetric metricNormal,
        [DisallowNull] IPaletteMetric metricTracking, [DisallowNull] IPaletteMetric metricPressed,
        PaletteMetricPadding metricPadding,
        Orientation orientation)
    {
        Debug.Assert(paletteDisabled != null);
        Debug.Assert(paletteNormal != null);
        Debug.Assert(paletteTracking != null);
        Debug.Assert(palettePressed != null);
        Debug.Assert(metricDisabled != null);
        Debug.Assert(metricNormal != null);
        Debug.Assert(metricTracking != null);
        Debug.Assert(metricPressed != null);

        // Remember the source information
        _paletteDisabled = paletteDisabled;
        _paletteNormal = paletteNormal;
        _paletteTracking = paletteTracking;
        _palettePressed = palettePressed;
        _metricDisabled = metricDisabled;
        _metricNormal = metricNormal;
        _metricTracking = metricTracking;
        _metricPressed = metricPressed;
        MetricPadding = metricPadding;
        Orientation = orientation;

        // Default other state
        Length = 0;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawSeparator:{Id}";

    #endregion

    #region MetricPadding
    /// <summary>
    /// Gets and sets the metric used to calculate the padding.
    /// </summary>
    public PaletteMetricPadding MetricPadding { get; set; }

    #endregion

    #region Source
    /// <summary>
    /// Gets and sets the associated separator source.
    /// </summary>
    public ISeparatorSource Source { get; set; }

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public Orientation Orientation { get; set; }

    #endregion

    #region Length
    /// <summary>
    /// Gets and sets the length of the separator.
    /// </summary>
    public int Length { get; set; }

    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the source palettes for drawing.
    /// </summary>
    /// <param name="paletteDisabled">Palette source for the disabled state.</param>
    /// <param name="paletteNormal">Palette source for the normal state.</param>
    /// <param name="paletteTracking">Palette source for the tracking state.</param>
    /// <param name="palettePressed">Palette source for the pressed state.</param>
    /// <param name="metricDisabled">Palette source for disabled metric values.</param>
    /// <param name="metricNormal">Palette source for normal metric values.</param>
    /// <param name="metricTracking">Palette source for tracking metric values.</param>
    /// <param name="metricPressed">Palette source for pressed metric values.</param>
    public void SetPalettes([DisallowNull] IPaletteDouble paletteDisabled,
        [DisallowNull] IPaletteDouble paletteNormal,
        [DisallowNull] IPaletteDouble paletteTracking,
        [DisallowNull] IPaletteDouble palettePressed,
        [DisallowNull] IPaletteMetric metricDisabled,
        [DisallowNull] IPaletteMetric metricNormal,
        [DisallowNull] IPaletteMetric metricTracking,
        [DisallowNull] IPaletteMetric metricPressed)
    {
        Debug.Assert(paletteDisabled != null);
        Debug.Assert(paletteNormal != null);
        Debug.Assert(paletteTracking != null);
        Debug.Assert(palettePressed != null);
        Debug.Assert(metricDisabled != null);
        Debug.Assert(metricNormal != null);
        Debug.Assert(metricTracking != null);
        Debug.Assert(metricPressed != null);

        // Use newly provided palettes
        _paletteDisabled = paletteDisabled;
        _paletteNormal = paletteNormal;
        _paletteTracking = paletteTracking;
        _palettePressed = palettePressed;
        _metricDisabled = metricDisabled;
        _metricNormal = metricNormal;
        _metricTracking = metricTracking;
        _metricPressed = metricPressed;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return new Size(Length, Length);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        ClientRectangle = context!.DisplayRectangle;
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

        // Validate reference parameter
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Ensure we are using the correct palette
        CheckPaletteState();

        // Apply padding needed outside the border of the separator
        var rect = CommonHelper.ApplyPadding(Orientation, ClientRectangle,
            _metric!.GetMetricPadding(context.Control as KryptonForm, ElementState, MetricPadding));

        // Ask the renderer to perform drawing of the separator glyph
        context.Renderer.RenderGlyph.DrawSeparator(context, rect, _palette!.PaletteBack, _palette.PaletteBorder!,
            Orientation, State, (Source == null) || Source.SeparatorCanMove);
    }
    #endregion

    #region Implementation
    private void CheckPaletteState()
    {
        PaletteState state = IsFixed ? FixedState : State;

        // Set the current palette based on the element state
        switch (state)
        {
            case PaletteState.Disabled:
                _palette = _paletteDisabled;
                _metric = _metricDisabled;
                break;
            case PaletteState.Normal:
                _palette = _paletteNormal;
                _metric = _metricNormal;
                break;
            case PaletteState.Pressed:
                _palette = _palettePressed;
                _metric = _metricPressed;
                break;
            case PaletteState.Tracking:
                _palette = _paletteTracking;
                _metric = _metricTracking;
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}