#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[DefaultEvent("ValueChanged"), ToolboxBitmap(typeof(TrackBar)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.StatusStrip | ToolStripItemDesignerAvailability.ToolStrip)]
public partial class KryptonSlider : KryptonToolStripControlHostFixed
{
    #region Variables
    private readonly SliderHostValues _values;
    #endregion

    #region Events
    public event ValueChangedEventHandler ValueChanged;
    #endregion

    #region Delegates
    public delegate void ValueChangedEventHandler(KryptonToolbarSlider sender, KryptonToolbarSlider.SliderEventArgs eventArgs);
    #endregion

    #region Properties
    [Category("Control")]
    public KryptonToolbarSlider? Tracker => Control as KryptonToolbarSlider;

    /// <summary>
    /// Gets the expandable configuration values mirroring the hosted <see cref="Tracker"/> settings.
    /// </summary>
    [Category("Slider Values")]
    [Description("Value, click behavior, fire interval, range, and step settings for the hosted tracker.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SliderHostValues SliderValues => _values;

    private bool ShouldSerializeSliderValues() => !_values.IsDefault;

    private void ResetSliderValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SingleClick
    {
        get => _values.SingleClick;
        set => _values.SingleClick = value;
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int FireInterval
    {
        get => _values.FireInterval;
        set => _values.FireInterval = value;
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size TrackerSize
    {
        get => _values.TrackerSize;
        set => _values.TrackerSize = value;
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Value
    {
        get => _values.Value;
        set => _values.Value = value;
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Range
    {
        get => _values.Range;
        set => _values.Range = value;
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Steps
    {
        get => _values.Steps;
        set => _values.Steps = value;
    }

    #endregion

    #region Identity
    public KryptonSlider() : base(new KryptonToolbarSlider())
    {
        _values = new SliderHostValues(this);
    }
    #endregion

    #region Overrides
    protected override void OnSubscribeControlEvents(Control? control)
    {
        base.OnSubscribeControlEvents(control);

        ((Control as KryptonToolbarSlider)!).ValueChanged += OnValueChanged;

        //((Control as KryptonToolbarSlider)!).kminus.SliderButtonFire += kminus_SliderButtonFire;

        //((Control as KryptonToolbarSlider)!).kplus.SliderButtonFire += kplus_SliderButtonFire;
    }

    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        base.OnUnsubscribeControlEvents(control);

        ((Control as KryptonToolbarSlider)!).ValueChanged -= OnValueChanged;
        //(this.Control as KryptonToolbarSlider).kminus.SliderButtonFire -= kminus_SliderButtonFire;
        //(this.Control as KryptonToolbarSlider).kplus.SliderButtonFire -= kplus_SliderButtonFire;
    }
    #endregion

    #region Event Handlers
    private void OnValueChanged(KryptonToolbarSlider sender, KryptonToolbarSlider.SliderEventArgs e) => ValueChanged.Invoke(sender, e);

    private void kminus_SliderButtonFire(KryptonSliderButton sender, EventArgs e)
    {

    }

    private void kplus_SliderButtonFire(KryptonSliderButton sender, EventArgs e)
    {

    }
    #endregion
}