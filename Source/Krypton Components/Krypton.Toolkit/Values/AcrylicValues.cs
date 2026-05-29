#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class AcrylicValues : GlobalId
{
    #region Static Fields

    private readonly Color _acrylicColor = Color.Transparent;

    private const bool DEFAULT_ACRYLIC_ENABLED = false;

    #endregion

    #region Identity

    public AcrylicValues()
    {
        Reset();
    }

    #endregion

    #region Public

    public bool EnableAcrylic { get; set; }

    public Color AcrylicColor { get; set; }

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => EnableAcrylic.Equals(DEFAULT_ACRYLIC_ENABLED) &&
                             AcrylicColor.Equals(_acrylicColor);

    #endregion

    #region Implementation

    public void Reset()
    {
        EnableAcrylic = DEFAULT_ACRYLIC_ENABLED;

        AcrylicColor = _acrylicColor;
    }

    #endregion
}