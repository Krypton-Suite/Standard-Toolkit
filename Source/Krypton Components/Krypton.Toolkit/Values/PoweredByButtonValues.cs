#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class PoweredByButtonValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private ToolkitSupportType _supportType;

    private readonly KryptonPoweredByButton _poweredByButton;

    #endregion

    #region Identity


    /// <summary>Initializes a new instance of the <see cref="PoweredByButtonValues" /> class.</summary>
    /// <param name="poweredByButton">The powered by button.</param>
    public PoweredByButtonValues(KryptonPoweredByButton poweredByButton)
    {
        _poweredByButton = poweredByButton;

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether to show the change log button.
    /// </summary>
    [Category("Visuals")]
    [Description("Gets or sets a value indicating whether to show the change log button.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowChangeLogButton { get; set; }

    /// <summary>Gets or sets a value indicating whether [show readme button].</summary>
    /// <value>
    ///   <c>true</c> if [show readme button]; otherwise, <c>false</c>.</value>
    [Category("Visuals")]
    [Description("Gets or sets a value indicating whether to show the readme button.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowReadmeButton { get; set; }

    /// <summary>
    /// Gets or sets the type of the toolkit.
    /// </summary>
    [Category("Visuals")]
    [Description("Gets or sets the type of the toolkit.")]
    [DefaultValue(ToolkitSupportType.Stable)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ToolkitSupportType ToolkitSupportType
    {
        get => _supportType;

        set
        {
            _supportType = value;

            SetIcon(value);
        }
    }

    #endregion

    #region Protected

    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion

    #region IsDefault

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => ShowChangeLogButton == false &&
                             ShowReadmeButton == false &&
                             ToolkitSupportType == ToolkitSupportType.Stable;

    #endregion

    #region Implementation

    public void Reset()
    {
        ShowChangeLogButton = false;

        ShowReadmeButton = false;

        ToolkitSupportType = ToolkitSupportType.Stable;
    }

    private void SetIcon(ToolkitSupportType toolkitSupportType)
    {
        switch (toolkitSupportType)
        {
            case ToolkitSupportType.Canary:
                _poweredByButton.Values.Image = ButtonImageResources.Krypton_Canary_Button;
                break;
            case ToolkitSupportType.Nightly:
                _poweredByButton.Values.Image = ButtonImageResources.Krypton_Nightly_Button;
                break;
            case ToolkitSupportType.LongTermSupport:
                _poweredByButton.Values.Image = ButtonImageResources.Krypton_Long_Term_Stable_Button;
                break;
            case ToolkitSupportType.Stable:
            default:
                _poweredByButton.Values.Image = ButtonImageResources.Krypton_Stable_Button;
                break;
        }
    }

    #endregion

    #region Event

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region INotifyPropertyChanged Implementation

    /// <summary>Called when [property changed].</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>Sets the field.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}