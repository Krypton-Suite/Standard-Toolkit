#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonFormTitleBarActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonFormTitleBar _titleBar;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormTitleBarActionList"/> class.
    /// </summary>
    public KryptonFormTitleBarActionList(KryptonFormTitleBarDesigner owner)
        : base(owner.Component)
    {
        _titleBar = (owner.Component as KryptonFormTitleBar)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public Override

    /// <inheritdoc/>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_titleBar != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(
                new DesignerVerb(@"Insert Standard Items", OnInsertStandardItems),
                @"Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonSpecs), @"Button Specs", @"Visuals",
                @"Collection of button specifications displayed in the title bar."));
        }

        return actions;
    }

    #endregion

    #region Implementation

    /// <summary>Gets the button-spec collection.</summary>
    public KryptonFormTitleBar.FormTitleBarButtonSpecCollection ButtonSpecs => _titleBar.ButtonSpecs;

    private void OnInsertStandardItems(object? sender, EventArgs e) =>
        KryptonFormTitleBarDesigner.InsertStandardItems(
            _titleBar,
            GetService(typeof(IDesignerHost)) as IDesignerHost,
            _service);

    #endregion
}
