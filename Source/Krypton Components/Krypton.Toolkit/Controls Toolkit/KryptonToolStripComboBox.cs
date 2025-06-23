#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxBitmap(typeof(KryptonComboBox))]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
//[DefaultEvent(nameof(SelectedIndexChanged))]
//[DefaultProperty(nameof(Text))]
public class KryptonToolStripComboBox : ToolStripControlHostFixed
{
    #region Instance Fields



    #endregion

    #region Host Control

    /// <summary>Gets access to the krypton ComboBox control.</summary>
    /// <value>The krypton ComboBox control.</value>
    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description(@"Access to the hosted KryptonComboBox.")]
    public KryptonComboBox? KryptonComboBoxControl => Control as KryptonComboBox;

    #endregion

    #region Public

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor { get; set; }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor { get; set; }

#if NET8_0_OR_GREATER
        /// <inheritdoc />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image? BackgroundImage { get; set; }
#else
    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image BackgroundImage { get; set; }
#endif

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonToolStripComboBox" /> class.</summary>
    public KryptonToolStripComboBox() : base(new KryptonComboBox())
    {
        AutoSize = false;
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnSubscribeControlEvents(Control? control)
    {
        base.OnSubscribeControlEvents(control);
    }

    /// <inheritdoc />
    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        base.OnUnsubscribeControlEvents(control);
    }

    #endregion
}