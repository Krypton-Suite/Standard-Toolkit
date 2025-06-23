#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class UserAccountControlShieldValues : NullContentValues
{
    #region Instance Fields

    private bool _useAsUACShieldButton;

    private bool _useOSStyleImage;

    private UACShieldIconSize _iconSize;

    private Size _customImageSize;

    #endregion

    #region Identity

    public UserAccountControlShieldValues()
    {
        _useAsUACShieldButton = false;

        _useOSStyleImage = true;

        _iconSize = UACShieldIconSize.Small;
    }

    #endregion

    #region Public

    public bool UseAsUACShieldButton { get => _useAsUACShieldButton; set => _useAsUACShieldButton = value; }

    public bool UseOSStyleImage { get => _useOSStyleImage; set => _useOSStyleImage = value; }

    public UACShieldIconSize ShieldIconSize { get => _iconSize; set => _iconSize = value; }

    public Size CustomImageSize { get => _customImageSize; set => _customImageSize = value; }

    #endregion
}