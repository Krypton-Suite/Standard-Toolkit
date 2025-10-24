#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Represents a tool strip Krypton progress bar control.</summary>
/// <seealso cref="Krypton.Toolkit.ToolStripControlHostFixed" />
[Category(@"code")]
[DefaultBindingProperty(@"Value")]
[DefaultProperty(@"Value")]
[Description(@"Represents a tool strip Krypton progress bar control.")]
[ToolboxBitmap(typeof(KryptonProgressBar))]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class KryptonProgressBarToolStripItem : ToolStripControlHostFixed
{

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonProgressBarToolStripItem" /> class.</summary>
    public KryptonProgressBarToolStripItem() : base(new KryptonProgressBar())
    {
        // Setup default size
        Size = new Size(100, 22);
    }

    #endregion

    #region Host Control

    /// <summary>Gets the krypton progress bar host.</summary>
    /// <value>The krypton progress bar host.</value>
    [RefreshProperties(RefreshProperties.All),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonProgressBar KryptonProgressBarHost => (Control as KryptonProgressBar)!;

    #endregion

    #region Public

    /// <summary>
    /// Gets access to the Progress Bar Label values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Progress Bar Label values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public LabelValues Values => KryptonProgressBarHost.Values;

    /// <summary>
    /// Gets access to the common ProgressBar appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common ProgressBar appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon => KryptonProgressBarHost.StateCommon;

    /// <summary>
    /// Gets access to the disabled ProgressBar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled ProgressBar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled => KryptonProgressBarHost.StateDisabled;

    /// <summary>
    /// Gets access to the normal ProgressBar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal ProgressBar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal => KryptonProgressBarHost.StateNormal;

    /// <summary>Gets or sets the manner in which progress should be indicated on the progress bar.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" /></returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a member of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> enumeration.</exception>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    [Description("Gets or sets the manner in which progress should be indicated on the progress bar.")]
    [DefaultValue(ProgressBarStyle.Continuous)]
    public ProgressBarStyle Style
    {
        get => KryptonProgressBarHost.Style;
        set => KryptonProgressBarHost.Style = value;
    }

    /// <summary>Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</summary>
    /// <returns>The time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The indicated time period is less than 0.</exception>
    [Category("Behavior")]
    [Description(
        "Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.")]
    [DefaultValue(100)]
    public int MarqueeAnimationSpeed
    {
        get => KryptonProgressBarHost.MarqueeAnimationSpeed;
        set => KryptonProgressBarHost.MarqueeAnimationSpeed = value;
    }

    /// <summary>Gets or sets the maximum value of the range of the control.</summary>
    /// <returns>The maximum value of the range. The default is 100.</returns>
    /// <exception cref="T:System.ArgumentException">The value specified is less than 0.</exception>
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the maximum value of the range of the control.")]
    [DefaultValue(100)]
    public int Maximum
    {
        get => KryptonProgressBarHost.Maximum;
        set => KryptonProgressBarHost.Maximum = value;
    }

    /// <summary>Gets or sets the minimum value of the range of the control.</summary>
    /// <returns>The minimum value of the range. The default is 0.</returns>
    /// <exception cref="T:System.ArgumentException">The value specified for the property is less than 0.</exception>
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the minimum value of the range of the control.")]
    [DefaultValue(0)]
    public int Minimum
    {
        get => KryptonProgressBarHost.Minimum;
        set => KryptonProgressBarHost.Minimum = value;
    }

    /// <summary>Gets or sets the amount by which a call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method increases the current position of the progress bar.</summary>
    /// <returns>The amount by which to increment the progress bar with each call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method. The default is 10.</returns>
    [Category("Behavior")]
    [Description(
        "Gets or sets the amount by which a call to the `PerformStep` method increases the current position of the progress bar.")]
    [DefaultValue(10)]
    public int Step
    {
        get => KryptonProgressBarHost.Step;
        set => KryptonProgressBarHost.Step = value;
    }

    /// <summary>Gets or sets the current position of the progress bar.</summary>
    /// <returns>The position within the range of the progress bar. The default is 0.</returns>
    /// <exception cref="T:System.ArgumentException">The value specified is greater than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Maximum" /> property.
    /// -or-
    /// The value specified is less than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Minimum" /> property.</exception>
    [Category("Behavior")]
    [Bindable(true)]
    [Description("Gets or sets the current position of the progress bar.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(0)]
    public int Value
    {
        get => KryptonProgressBarHost.Value;
        set => KryptonProgressBarHost.Value = value;
    }

    /// <inheritdoc/>
    [DefaultValue("")]
    [AllowNull]
    public override string Text
    {
        // KryptonProgress.Values.Text can be set to null
        // The getter will always return a string

        get => KryptonProgressBarHost.Text;
        set => KryptonProgressBarHost.Text = value;
    }

    /// <summary>Gets or sets a value indicating whether [use value as text].</summary>
    /// <value><c>true</c> if [use value as text]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals")]
    [Description(@"Use the progress value as text.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool UseValueAsText
    {
        get => KryptonProgressBarHost.UseValueAsText;

        set => KryptonProgressBarHost.UseValueAsText = value;
    }

    /// <inheritdoc />
    [DefaultValue(typeof(Size), @"100, 22")]
    public override Size Size { get => base.Size; set => base.Size = value; }

    #endregion

    #region Removed Designer

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

    #endregion
}