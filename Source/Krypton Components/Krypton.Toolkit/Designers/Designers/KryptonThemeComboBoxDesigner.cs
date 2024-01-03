#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonThemeComboBoxDesigner : ControlDesigner
    {
        #region Instance Fields

        private bool _lastHitTest;
        private KryptonThemeComboBox? _themeComboBox;
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private ISelectionService _selectionService;
        
        #endregion
    }
}
