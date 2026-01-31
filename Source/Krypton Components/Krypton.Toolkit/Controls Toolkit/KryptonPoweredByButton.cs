#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A button that displays the Krypton Toolkit branding and provides information about the toolkit version.
/// </summary>
/// <seealso cref="Krypton.Toolkit.KryptonButton" />
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DesignerCategory(@"code")]
[Description(@"A button that displays the Krypton Toolkit branding and provides information about the toolkit version.")]
[Designer(typeof(KryptonButtonDesigner))]
public class KryptonPoweredByButton : KryptonButton
{
    #region Instance Fields

    private PoweredByButtonValues? _poweredByButtonValues;

    #endregion

    #region Public

    /// <summary>Gets or sets the button values.</summary>
    /// <value>The button values.</value>
    [Category(@"Visuals")]
    [Description(@"Gets or sets the values for the Powered By button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
    public PoweredByButtonValues ButtonValues
    {
        get => _poweredByButtonValues ??= new PoweredByButtonValues(this);

        set
        {
            if (_poweredByButtonValues != value)
            {
                if (_poweredByButtonValues != null)
                {
                    _poweredByButtonValues.PropertyChanged -= OnPropertyChanged;
                }

                _poweredByButtonValues = value ?? new PoweredByButtonValues(this);

                _poweredByButtonValues.PropertyChanged += OnPropertyChanged;

                Invalidate();
            }
        }
    }

    private bool ShouldSerializeButtonValues() => !ButtonValues.IsDefault;

    public void ResetButtonValues() => ButtonValues.Reset();

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonPoweredByButton" /> class.</summary>
    public KryptonPoweredByButton()
    {
        Values.Text = @$"{KryptonManager.Strings.MiscellaneousStrings.PoweredByText} Krypton";

        Values.Image = ButtonImageResources.Krypton_Stable_Button;

        Size = new Size(153, 25);
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    [AllowNull]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text { get; set; } =
        @$"{KryptonManager.Strings.MiscellaneousStrings.PoweredByText} Krypton";

    /// <inheritdoc />
    protected override void OnClick(EventArgs e)
    {
        new VisualToolkitBinaryInformationForm(ButtonValues.ToolkitSupportType, ButtonValues.ShowChangeLogButton, ButtonValues.ShowReadmeButton).ShowDialog();

        base.OnClick(e);
    }

    #endregion

    #region Event

    /// <summary>Occurs when the control is clicked.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler Click
    {
        add { base.Click += value; }
        remove { base.Click -= value; }
    }

    #endregion

    #region Implementation

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => Invalidate();

    #endregion
}