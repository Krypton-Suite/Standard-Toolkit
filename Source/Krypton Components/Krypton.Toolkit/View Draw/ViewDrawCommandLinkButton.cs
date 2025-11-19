#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// View element that can draw a CommandLinkButton.
/// </summary>
public class ViewDrawCommandLinkButton : ViewComposite
{
    #region Instance Fields

    private IPaletteTriple _paletteDisabled;
    private IPaletteTriple _paletteNormal;
    private IPaletteTriple _paletteTracking;
    private IPaletteTriple _palettePressed;
    private IPaletteTriple _paletteCheckedNormal;
    private IPaletteTriple _paletteCheckedTracking;
    private IPaletteTriple _paletteCheckedPressed;
    private readonly ViewDrawCanvas _drawCanvas;
    private readonly ViewDrawContent _drawContent;
    private readonly ViewDrawContent _drawImageContent;
    private readonly ViewLayoutCenter _drawImage;
    private bool _forcePaletteUpdate;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawCommandLinkButton"/> class.
    /// </summary>
    /// <param name="paletteDisabled">Palette source for the disabled state.</param>
    /// <param name="paletteNormal">Palette source for the normal state.</param>
    /// <param name="paletteTracking">Palette source for the tracking state.</param>
    /// <param name="palettePressed">Palette source for the pressed state.</param>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="imageValues"></param>
    /// <param name="commandLinkTextValues"></param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="useMnemonic">Use mnemonics.</param>
    public ViewDrawCommandLinkButton(IPaletteTriple paletteDisabled,
        IPaletteTriple paletteNormal,
        IPaletteTriple paletteTracking,
        IPaletteTriple palettePressed,
        IPaletteMetric paletteMetric,
        CommandLinkImageValues imageValues,
        CommandLinkTextValues commandLinkTextValues,
        VisualOrientation orientation,
        bool useMnemonic)
        : this(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
            paletteNormal, paletteTracking, palettePressed, paletteMetric,
            imageValues, commandLinkTextValues, orientation, useMnemonic)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawButton class.
    /// </summary>
    /// <param name="paletteDisabled">Palette source for the disabled state.</param>
    /// <param name="paletteNormal">Palette source for the normal state.</param>
    /// <param name="paletteTracking">Palette source for the tracking state.</param>
    /// <param name="palettePressed">Palette source for the pressed state.</param>
    /// <param name="paletteCheckedNormal">Palette source for the normal checked state.</param>
    /// <param name="paletteCheckedTracking">Palette source for the tracking checked state.</param>
    /// <param name="paletteCheckedPressed">Palette source for the pressed checked state.</param>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="imageValues"></param>
    /// <param name="commandLinkTextValues"></param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="useMnemonic">Use mnemonics.</param>
    public ViewDrawCommandLinkButton(IPaletteTriple paletteDisabled,
        IPaletteTriple paletteNormal,
        IPaletteTriple paletteTracking,
        IPaletteTriple palettePressed,
        IPaletteTriple paletteCheckedNormal,
        IPaletteTriple paletteCheckedTracking,
        IPaletteTriple paletteCheckedPressed,
        IPaletteMetric paletteMetric,
        CommandLinkImageValues imageValues,
        CommandLinkTextValues commandLinkTextValues,
        VisualOrientation orientation,
        bool useMnemonic)
    {
        // Remember the source information
        _paletteDisabled = paletteDisabled;
        _paletteNormal = paletteNormal;
        _paletteTracking = paletteTracking;
        _palettePressed = palettePressed;
        _paletteCheckedNormal = paletteCheckedNormal;
        _paletteCheckedTracking = paletteCheckedTracking;
        _paletteCheckedPressed = paletteCheckedPressed;
        CurrentPalette = _paletteNormal;

        // Default to not being checked
        Checked = false;
        AllowUncheck = true;

        // Create the drop-down view
        _drawImageContent = new ViewDrawContent(_paletteNormal.PaletteContent, imageValues, orientation);
        _drawImage = new ViewLayoutCenter(paletteMetric, PaletteMetricPadding.BarPaddingOnly,
            orientation, _drawImageContent);

        // Our view contains background and border with content inside
        _drawContent = new ViewDrawContent(_paletteNormal.PaletteContent, commandLinkTextValues, orientation)
        {
            // Pass the mnemonic default to the content view
            UseMnemonic = useMnemonic
        };

        // Use a docker layout to organize the contents of the canvas
        LayoutDocker = new ViewLayoutDocker
        {
            { _drawContent, ViewDockStyle.Left },
            { _drawImage, ViewDockStyle.Left }
        };
        LayoutDocker.Tag = this;


        _drawCanvas = new ViewDrawCanvas(_paletteNormal.PaletteBack, _paletteNormal.PaletteBorder!, paletteMetric,
            PaletteMetricPadding.BarPaddingTabs, orientation)
        {
            // Place the content inside the canvas
            LayoutDocker
        };

        // Place the canvas inside ourself
        Add(_drawCanvas);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString()
    {
        // Return the class name and instance identifier
        return $"ViewDrawButton:{Id}";
    }
    #endregion

    #region LayoutDocker
    /// <summary>
    /// Gets access to the contained layout docker.
    /// </summary>
    public ViewLayoutDocker LayoutDocker { get; }

    #endregion

    #region CurrentPalette
    /// <summary>
    /// Gets access to the currently selected palette.
    /// </summary>
    public IPaletteTriple CurrentPalette { get; private set; }

    #endregion

    #region ButtonValues
    /// <summary>
    /// Gets and sets the source for button values.
    /// </summary>
    public IContentValues? ButtonValues
    {
        get => _drawContent.Values;
        set => _drawContent.Values = value;
    }
    #endregion

    #region DrawTabBorder
    /// <summary>
    /// Gets and sets if the border should be drawn as a tab border.
    /// </summary>
    public bool DrawTabBorder
    {
        get => _drawCanvas.DrawTabBorder;
        set => _drawCanvas.DrawTabBorder = value;
    }
    #endregion

    #region TabBorderStyle
    /// <summary>
    /// Gets and sets the tab border style of the button.
    /// </summary>
    public TabBorderStyle TabBorderStyle
    {
        get => _drawCanvas.TabBorderStyle;
        set => _drawCanvas.TabBorderStyle = value;
    }
    #endregion

    #region Enabled
    /// <summary>
    /// Gets and sets the enabled state of the element.
    /// </summary>
    public override bool Enabled
    {
        get => base.Enabled;

        set
        {
            base.Enabled = value;

            if (Enabled && (ElementState == PaletteState.Disabled))
            {
                ElementState = Checked ? PaletteState.CheckedNormal : PaletteState.Normal;
            }

            // Pass on the new state to the child elements
            _drawCanvas.Enabled = value;
            _drawContent.Enabled = value;
            _drawImageContent.Enabled = value;
        }
    }
    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public virtual VisualOrientation Orientation
    {
        get => _drawCanvas.Orientation;
        set => SetOrientation(value, value);
    }

    /// <summary>
    /// Set the orientation of the two button components.
    /// </summary>
    /// <param name="borderBackOrient">Orientation of the button border and background..</param>
    /// <param name="contentOrient">Orientation of the button contents.</param>
    public void SetOrientation(VisualOrientation borderBackOrient,
        VisualOrientation contentOrient)
    {
        _drawCanvas.Orientation = borderBackOrient;
        _drawContent.Orientation = contentOrient;
    }
    #endregion

    #region UseMnemonic
    /// <summary>
    /// Gets and sets usage of mnemonics.
    /// </summary>
    public bool UseMnemonic
    {
        get => _drawContent.UseMnemonic;
        set => _drawContent.UseMnemonic = value;
    }
    #endregion

    #region Checked
    /// <summary>
    /// Gets and sets the checked state.
    /// </summary>
    public bool Checked { get; set; }

    #endregion

    #region AllowUncheck
    /// <summary>
    /// Gets and sets the allow uncheck state.
    /// </summary>
    public bool AllowUncheck { get; set; }

    #endregion

    #region DrawButtonComposition
    /// <summary>
    /// Gets and sets the composition usage of the button.
    /// </summary>
    public bool DrawButtonComposition
    {
        get => _drawCanvas.DrawCanvasOnComposition;
        set => _drawCanvas.DrawCanvasOnComposition = value;
    }
    #endregion

    #region TestForFocusCues
    /// <summary>
    /// Gets and sets the use of focus cues for deciding if focus rects are allowed.
    /// </summary>
    public bool TestForFocusCues
    {
        get => _drawContent.TestForFocusCues;
        set => _drawContent.TestForFocusCues = value;
    }
    #endregion

    #region Palettes
    /// <summary>
    /// Update the source palettes for non-checked drawing.
    /// </summary>
    /// <param name="paletteDisabled">Palette source for the disabled state.</param>
    /// <param name="paletteNormal">Palette source for the normal state.</param>
    /// <param name="paletteTracking">Palette source for the tracking state.</param>
    /// <param name="palettePressed">Palette source for the pressed state.</param>
    public void SetPalettes(IPaletteTriple paletteDisabled,
        IPaletteTriple paletteNormal,
        IPaletteTriple paletteTracking,
        IPaletteTriple palettePressed)
    {
        Debug.Assert(paletteDisabled != null);
        Debug.Assert(paletteNormal != null);
        Debug.Assert(paletteTracking != null);
        Debug.Assert(palettePressed != null);

        // Remember the new palette settings
        _paletteDisabled = paletteDisabled!;
        _paletteNormal = paletteNormal!;
        _paletteTracking = paletteTracking!;
        _palettePressed = palettePressed!;

        // Must force update of palettes to use latest ones provided
        _forcePaletteUpdate = true;
    }

    /// <summary>
    /// Update the source palettes for checked state drawing.
    /// </summary>
    /// <param name="paletteCheckedNormal">Palette source for the normal checked state.</param>
    /// <param name="paletteCheckedTracking">Palette source for the tracking checked state.</param>
    /// <param name="paletteCheckedPressed">Palette source for the pressed checked state.</param>
    public void SetCheckedPalettes(IPaletteTriple paletteCheckedNormal,
        IPaletteTriple paletteCheckedTracking,
        IPaletteTriple paletteCheckedPressed)
    {
        Debug.Assert(paletteCheckedNormal != null);
        Debug.Assert(paletteCheckedTracking != null);
        Debug.Assert(paletteCheckedPressed != null);

        // Remember the new palette settings
        _paletteCheckedNormal = paletteCheckedNormal!;
        _paletteCheckedTracking = paletteCheckedTracking!;
        _paletteCheckedPressed = paletteCheckedPressed!;

        // Must force update of palettes to use latest ones provided
        _forcePaletteUpdate = true;
    }
    #endregion

    #region Eval
    /// <summary>
    /// Evaluate the need for drawing transparent areas.
    /// </summary>
    /// <param name="context">Evaluation context.</param>
    /// <returns>True if transparent areas exist; otherwise false.</returns>
    public override bool EvalTransparentPaint(ViewContext context)
    {
        Debug.Assert(context != null);

        // Ensure that child elements have correct palette state
        CheckPaletteState(context!);

        // Ask the renderer to evaluate the given palette
        return _drawCanvas.EvalTransparentPaint(context!);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        Debug.Assert(_drawCanvas != null);

        // Ensure that child elements have correct palette state
        CheckPaletteState(context!);

        // Delegate work to the child canvas
        return _drawCanvas!.GetPreferredSize(context!);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout(ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Ensure that child elements have correct palette state
        CheckPaletteState(context);

        // Let base class perform usual processing
        base.Layout(context);


    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render(RenderContext context)
    {
        Debug.Assert(context != null);

        // Ensure that child elements have correct palette state
        CheckPaletteState(context!);

        // Let base class perform standard rendering
        base.Render(context!);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Check that the palette and state are correct.
    /// </summary>
    /// <param name="context">Reference to the view context.</param>
    protected virtual void CheckPaletteState(ViewContext context)
    {
        // Default to using this element calculated state
        PaletteState buttonState = State;

        // If the actual control is not enabled, force to disabled state
        if (!IsFixed && !context.Control!.Enabled)
        {
            buttonState = PaletteState.Disabled;
        }

        // Apply the checked state if not fixed
        if (!IsFixed && Checked)
        {
            // Is the checked button allowed to become unchecked
            if (AllowUncheck)
            {
                // Show feedback on tracking and pressed
                switch (buttonState)
                {
                    case PaletteState.Normal:
                        buttonState = PaletteState.CheckedNormal;
                        break;
                    case PaletteState.Tracking:
                        buttonState = PaletteState.CheckedTracking;
                        break;
                    case PaletteState.Pressed:
                        buttonState = PaletteState.CheckedPressed;
                        break;
                }
            }
            else
            {
                // Always use the normal state as user cannot uncheck the button
                buttonState = PaletteState.CheckedNormal;
            }
        }

        // If the child elements are not in correct state
        if (_forcePaletteUpdate || (_drawCanvas.ElementState != buttonState))
        {
            // No longer need to force the palettes to be updated
            _forcePaletteUpdate = false;

            // Switch the child elements over to correct state
            _drawCanvas.ElementState = buttonState;
            _drawContent.ElementState = buttonState;
            _drawImageContent.ElementState = buttonState;

            // Push the correct palettes into them
            switch (buttonState)
            {
                case PaletteState.Disabled:
                    CurrentPalette = _paletteDisabled;
                    break;
                case PaletteState.Normal:
                    CurrentPalette = _paletteNormal;
                    break;
                case PaletteState.CheckedNormal:
                    CurrentPalette = _paletteCheckedNormal;
                    break;
                case PaletteState.Pressed:
                    CurrentPalette = _palettePressed;
                    break;
                case PaletteState.CheckedPressed:
                    CurrentPalette = _paletteCheckedPressed;
                    break;
                case PaletteState.Tracking:
                    CurrentPalette = _paletteTracking;
                    break;
                case PaletteState.CheckedTracking:
                    CurrentPalette = _paletteCheckedTracking;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(buttonState.ToString());
                    break;
            }

            // Update with the correct palettes
            _drawCanvas.SetPalettes(CurrentPalette.PaletteBack, CurrentPalette.PaletteBorder!);
            _drawContent.SetPalette(CurrentPalette.PaletteContent!);
            //_drawImageContent.SetPalette(CurrentPalette.PaletteContent);
        }
    }
    #endregion
}