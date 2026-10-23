#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(Button)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All), DefaultEvent(@"SelectedColorChanged"), DefaultProperty(@"SelectedColor")]
public class KryptonColorButtonToolStripMenuItem : KryptonToolStripControlHostFixed
{
    #region Instance Fields

    private readonly ColourButtonHostValues _values;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the SelectedColor property changes value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the SelectedColor property changes value.")]
    public event EventHandler<ColorEventArgs>? SelectedColorChanged;

    /// <summary>
    /// Occurs when the user is tracking over a color.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when user is tracking over a color.")]
    public event EventHandler<ColorEventArgs>? TrackingColor;

    /// <summary>
    /// Occurs when the user selects the more colors option.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when user selects the more colors option.")]
    public event CancelEventHandler? MoreColors;

    #endregion

    #region Host Control

    /// <summary>
    /// Gets the KryptonColorButton control.
    /// </summary>
    /// <value>The KryptonColorButton control.</value>
    [RefreshProperties(RefreshProperties.All),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonColorButton? KryptonColourButtonControl => Control as KryptonColorButton;

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable configuration values mirroring the hosted <see cref="KryptonColourButtonControl"/> settings.
    /// </summary>
    [Category("Appearance")]
    [Description("Selected colour, empty border colour, text, and selection rectangle settings for the hosted button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ColourButtonHostValues ColourValues => _values;

    private bool ShouldSerializeColourValues() => !_values.IsDefault;

    private void ResetColourValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectedColor
    {
        get => _values.SelectedColor;
        set => _values.SelectedColor = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color EmptyBorderColor
    {
        get => _values.EmptyBorderColor;
        set => _values.EmptyBorderColor = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string Text { get => _values.Text; set => _values.Text = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle SelectedRect { get => _values.SelectedRect; set => _values.SelectedRect = value; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonColorButtonToolStripMenuItem"/> class.
    /// </summary>
    public KryptonColorButtonToolStripMenuItem()
        : base(new KryptonColorButton())
    {
        _values = new ColourButtonHostValues(this);

        AutoSize = false;

        SelectedColor = KryptonColourButtonControl!.SelectedColor;

        EmptyBorderColor = KryptonColourButtonControl.EmptyBorderColor;
    }

    #endregion

    #region Protected Virtual

    protected virtual void OnSelectedColorChanged(ColorEventArgs e) => SelectedColorChanged?.Invoke(this, e);

    protected virtual void OnTrackingColor(ColorEventArgs e) => TrackingColor?.Invoke(this, e);

    protected virtual void OnMoreColors(CancelEventArgs e) => MoreColors?.Invoke(this, e);

    #endregion

    #region Implementation

    /// <summary>
    /// Retrieves the size of a rectangular area into which a control can be fitted.
    /// </summary>
    /// <param name="constrainingSize">The custom-sized area for a control.</param>
    /// <returns>
    /// An ordered pair of type <see cref="T:System.Drawing.Size"></see> representing the width and height of a rectangle.
    /// </returns>
    public override Size GetPreferredSize(Size constrainingSize) =>
        KryptonColourButtonControl!.GetPreferredSize(constrainingSize);

    /// <summary>
    /// Subscribes events from the hosted control.
    /// </summary>
    /// <param name="control">The control from which to subscribe events.</param>
    protected override void OnSubscribeControlEvents(Control? control)
    {
        base.OnSubscribeControlEvents(control);

        if (control is KryptonColorButton button)
        {
            button.SelectedColorChanged += Button_SelectedColorChanged;
            button.TrackingColor += Button_TrackingColor;
            button.MoreColors += Button_MoreColors;
        }
    }

    /// <summary>
    /// Unsubscribes events from the hosted control.
    /// </summary>
    /// <param name="control">The control from which to unsubscribe events.</param>
    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        if (control is KryptonColorButton button)
        {
            button.SelectedColorChanged -= Button_SelectedColorChanged;
            button.TrackingColor -= Button_TrackingColor;
            button.MoreColors -= Button_MoreColors;
        }

        base.OnUnsubscribeControlEvents(control);
    }

    private void Button_SelectedColorChanged(object? sender, ColorEventArgs e) => OnSelectedColorChanged(e);

    private void Button_TrackingColor(object? sender, ColorEventArgs e) => OnTrackingColor(e);

    private void Button_MoreColors(object? sender, CancelEventArgs e) => OnMoreColors(e);

    #endregion
}