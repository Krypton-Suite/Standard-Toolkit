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

namespace Krypton.Navigator;

/// <summary>
/// Implementation for the fixed navigator buttons.
/// </summary>
[TypeConverter(typeof(ButtonSpecNavFixedConverter))]
public abstract class ButtonSpecNavFixed : ButtonSpec
{
    #region Instance Fields

    private HeaderLocation _location;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecNavFixed class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="fixedStyle">Fixed style to use.</param>
    protected ButtonSpecNavFixed([DisallowNull] KryptonNavigator navigator,
        PaletteButtonSpecStyle fixedStyle)
    {
        Debug.Assert(navigator != null);

        // Remember back reference to owning navigator.
        Navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Fix the type
        ProtectedType = fixedStyle;

        // Default other properties
        _location = HeaderLocation.PrimaryHeader;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (base.IsDefault &&
                                       (HeaderLocation == HeaderLocation.PrimaryHeader));

    #endregion

    #region AllowComponent
    /// <summary>
    /// Gets a value indicating if the component is allowed to be selected at design time.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AllowComponent => false;

    #endregion

    #region HeaderLocation
    /// <summary>
    /// Gets and sets if the button header location.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Defines header location for the button.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(HeaderLocation), "PrimaryHeader")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public HeaderLocation HeaderLocation
    {
        get => _location;

        set
        {
            if (_location != value)
            {
                _location = value;
                OnButtonSpecPropertyChanged(@"Location");
            }
        }
    }

    /// <summary>
    /// Resets the HeaderLocation property to its default value.
    /// </summary>
    public void ResetHeaderLocation() => HeaderLocation = HeaderLocation.PrimaryHeader;
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button location value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button location.</returns>
    public override HeaderLocation GetLocation(PaletteBase? palette) =>
        // Ask the view builder to recover the correct location
        Navigator.ViewBuilder!.GetFixedButtonLocation(this);

    #endregion

    #region Navigator
    /// <summary>
    /// Gets access to the owning navigator control.
    /// </summary>
    protected KryptonNavigator Navigator { get; }

    #endregion
}