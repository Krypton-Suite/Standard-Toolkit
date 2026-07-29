#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable configuration for <see cref="KryptonMRUMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MruMenuItemValues : Storage
{
    #region Constants

    private const string DEFAULT_TEXT = @"Mo&st Recently Used...";

    #endregion

    #region Instance Fields

    private readonly KryptonMRUMenuItem _owner;
    private string _defaultText = DEFAULT_TEXT;
    private Control? _outputControl;
    private string? _applicationName;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MruMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning MRU menu item.</param>
    public MruMenuItemValues(KryptonMRUMenuItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _defaultText == DEFAULT_TEXT &&
        _outputControl is null &&
        _applicationName is null;

    #endregion

    #region Public

    /// <summary>Gets or sets the default caption text.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_TEXT)]
    [Description(@"Gets or sets the default caption text.")]
    public string DefaultText
    {
        get => _defaultText;
        set
        {
            _defaultText = value;
            _owner.Text = value;
        }
    }

    /// <summary>Gets or sets the control that receives file contents when an MRU entry is opened.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"Gets or sets the control that receives file contents when an MRU entry is opened.")]
    public Control? OutputControl
    {
        get => _outputControl;
        set => _outputControl = value;
    }

    /// <summary>Gets or sets the application name used for the registry MRU key.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"Gets or sets the application name used for the registry MRU key.")]
    public string? ApplicationName
    {
        get => _applicationName;
        set
        {
            _applicationName = value;
            _owner.EnsureManager();
        }
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _defaultText = DEFAULT_TEXT;
        _owner.Text = DEFAULT_TEXT;
        _outputControl = null;
        _applicationName = null;
    }

    #endregion
}
