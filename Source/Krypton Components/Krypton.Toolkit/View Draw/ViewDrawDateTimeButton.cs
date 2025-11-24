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
/// View element that can draw a date time picker button.
/// </summary>
public class ViewDrawDateTimeButton : ViewDrawButton
{
    #region Enums
    /// <summary>
    /// Specific the possible glyphs the button can draw.
    /// </summary>
    public enum DrawDateTimeGlyph
    {
        /// <summary>
        /// Specifies the drop-down button glyph.
        /// </summary>
        DropDownButton,

        /// <summary>
        /// Specifies the up button glyph.
        /// </summary>
        UpButton,

        /// <summary>
        /// Specifies the down button glyph.
        /// </summary>
        DownButton
    }
    #endregion

    #region Instance Fields
    private readonly DrawDateTimeGlyph _glyph;
    private readonly KryptonDateTimePicker _dateTimePicker;
    private readonly ButtonController _controller;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the button is clicked.
    /// </summary>
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the mouse is used to left select the target.
    /// </summary>
    public event MouseEventHandler? MouseSelect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawButton class.
    /// </summary>
    /// <param name="dateTimePicker">Owning control.</param>
    /// <param name="paletteState">Palette source for states.</param>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="buttonValues">Source for content values.</param>
    /// <param name="glyph">Glyph to be drawn.</param>
    /// <param name="needPaintHandler">Delegate for requests repainting.</param>
    /// <param name="repeat">Should button repeat.</param>
    public ViewDrawDateTimeButton(KryptonDateTimePicker dateTimePicker,
        IPaletteTriple paletteState,
        IPaletteMetric paletteMetric,
        IContentValues? buttonValues,
        DrawDateTimeGlyph glyph,
        NeedPaintHandler needPaintHandler,
        bool repeat)        
        : base(paletteState, paletteState, paletteState, paletteState, 
            paletteMetric, buttonValues, VisualOrientation.Top, false)
    {
        _dateTimePicker = dateTimePicker;
        _glyph = glyph;

        // Assign a controller to handle visual interaction
        _controller = new ButtonController(this, needPaintHandler)
        {
            BecomesFixed = !repeat
        };
        _controller.Click += OnButtonClick;
        _controller.MouseSelect += OnButtonMouseSelect;
        _controller.Repeat = repeat;
        _controller.ClickOnDown = true;
        MouseController = _controller;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawDateTimeButton:{Id}";

    #endregion

    #region RemoveFixed
    /// <summary>
    /// Remove the fixed appearance of the button.
    /// </summary>
    public void RemoveFixed() => _controller.RemoveFixed();

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) =>
        // We want to be as wide as drop-down buttons on standard controls
        new Size(SystemInformation.VerticalScrollBarWidth - 2, 0);

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

        // Layout the button drawing elements using a reduced size
        Rectangle beforeRect = context.DisplayRectangle;
        context.DisplayRectangle = _glyph switch
        {
            DrawDateTimeGlyph.DropDownButton => beforeRect with { Y = beforeRect.Y + 1, Height = beforeRect.Height - 2 },
            DrawDateTimeGlyph.UpButton => beforeRect with { Y = beforeRect.Y + 1, Height = beforeRect.Height - 1 },
            DrawDateTimeGlyph.DownButton => beforeRect with { Height = beforeRect.Height - 1 },
            _ => context.DisplayRectangle
        };

        base.Layout(context);

        // Restore the original size and use that as the actual client rectane
        context.DisplayRectangle = beforeRect;
        ClientRectangle = beforeRect;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        switch (_glyph)
        {
            case DrawDateTimeGlyph.DropDownButton:
                context.Renderer.RenderGlyph.DrawInputControlDropDownGlyph(context, ClientRectangle, CurrentPalette.PaletteContent!, State);
                break;
            case DrawDateTimeGlyph.UpButton:
                context.Renderer.RenderGlyph.DrawInputControlNumericUpGlyph(context, ClientRectangle, CurrentPalette.PaletteContent!, State);
                break;
            case DrawDateTimeGlyph.DownButton:
                context.Renderer.RenderGlyph.DrawInputControlNumericDownGlyph(context, ClientRectangle, CurrentPalette.PaletteContent!, State);
                break;
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Check that the palette and state are correct.
    /// </summary>
    /// <param name="context">Reference to the view context.</param>
    protected override void CheckPaletteState(ViewContext context)
    {
        PaletteState state = ElementState;

        // If the drop-down calendar is showing then always draw button as pressed
        if (_dateTimePicker.IsDropped)
        {
            state = PaletteState.Pressed;
        }
        else
        {
            // If the button is in a normal state (not being tracked or pressed)
            if (ElementState is PaletteState.Normal or PaletteState.CheckedNormal)
            {
                // If the control is active then use the checked normal appearance, otherwise not active and so use the normal appearance
                if (_dateTimePicker.IsActive || _dateTimePicker is { IsFixedActive: true, InputControlStyle: InputControlStyle.Standalone })
                {
                    state = PaletteState.CheckedNormal;
                }
                else
                {
                    state = PaletteState.Normal;
                }
            }
        }

        ElementState = state;
        foreach (ViewBase child in this)
        {
            child.ElementState = state;
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments assocaited with the event.</param>
    protected void OnButtonClick(object? sender, MouseEventArgs e) => Click?.Invoke(this, e);

    /// <summary>
    /// Raises the MouseSelect event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments assocaited with the event.</param>
    protected void OnButtonMouseSelect(object? sender, MouseEventArgs e) => MouseSelect?.Invoke(this, e);

    #endregion
}