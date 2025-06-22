#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonThemeBrowserActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonListBox _themeListBox;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    public KryptonThemeBrowserActionList(KryptonThemeBrowserDesigner owner) : base(owner.Component)
    {
        _themeListBox = (owner.Component as KryptonListBox)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    public int SelectedIndex
    {
        get => _themeListBox.SelectedIndex;

        set
        {
            if (_themeListBox.SelectedIndex != value)
            {
                _service?.OnComponentChanged(_themeListBox, null, _themeListBox.SelectedIndex, value);

                _themeListBox.SelectedIndex = value;
            }
        }
    }

    #endregion

    #region Public Override

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actionItems = new DesignerActionItemCollection();

        if (_themeListBox != null)
        {
            actionItems.Add(new DesignerActionHeaderItem(@"Values"));
            actionItems.Add(new DesignerActionPropertyItem(nameof(SelectedIndex), @"Selected Index", @"Values", @"The selected index of the list box."));
        }

        return actionItems;
    }

    #endregion
}