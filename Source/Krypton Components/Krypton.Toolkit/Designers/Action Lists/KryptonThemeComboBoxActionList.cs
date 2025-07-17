#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonThemeComboBoxActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonThemeComboBox _themeComboBox;
    private readonly IComponentChangeService? _service;

    #endregion

    public KryptonThemeComboBoxActionList(KryptonThemeComboBoxDesigner owner) : base(owner.Component)
    {
        _themeComboBox = (owner.Component as KryptonThemeComboBox)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
}