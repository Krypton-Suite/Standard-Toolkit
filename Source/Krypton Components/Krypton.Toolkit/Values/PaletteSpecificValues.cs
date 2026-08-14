#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteSpecificValues : Storage
{
    #region Static Fields

    private const bool DEFAULT_USE_WINDOWS_CONTROL_BOX_LAYOUT = true;

    #endregion

    #region Instance Fields

    private readonly VisualForm _owner;

    private bool _useWindowsControlBoxLayout;

    #endregion

    #region Idenity

    public PaletteSpecificValues(VisualForm owner)
    {
        _owner = owner ?? ThrowHelper.ThrowArgumentNullException(owner);

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether [use windows control box layout].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [use windows control box layout]; otherwise, <c>false</c>.
    /// </value>
    [Description("Should the control box buttons be laid out in the same way as Windows does it.")]
    [Category("Visuals")]
    [DefaultValue(DEFAULT_USE_WINDOWS_CONTROL_BOX_LAYOUT)]
    public bool UseWindowsControlBoxLayout
    {
        get => _useWindowsControlBoxLayout;
        set
        {
            if (_useWindowsControlBoxLayout != value)
            {
                _useWindowsControlBoxLayout = value;
            }
        }
    }

    private bool ShouldSerializeUseWindowsControlBoxLayout() => !_useWindowsControlBoxLayout;

    public void ResetUseWindowsControlBoxLayout() => UseWindowsControlBoxLayout = DEFAULT_USE_WINDOWS_CONTROL_BOX_LAYOUT;

    #endregion

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => UseWindowsControlBoxLayout.Equals(DEFAULT_USE_WINDOWS_CONTROL_BOX_LAYOUT);

    #region Reset

    public void Reset()
    {
        ResetUseWindowsControlBoxLayout();
    }

    #endregion
}