#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the <see cref="KryptonMenuBar"/> designer.
/// </summary>
internal class KryptonMenuBarActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonMenuBar _menuBar;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMenuBarActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonMenuBarActionList(KryptonMenuBarDesigner owner)
        : base(owner.Component)
    {
        _menuBar = (owner.Component as KryptonMenuBar)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_menuBar != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(
                new DesignerVerb(@"Insert Standard Items", OnInsertStandardItems),
                @"Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));
            actions.Add(new DesignerActionPropertyItem(nameof(Items), @"Items", @"Data",
                @"Top-level menu items displayed on the bar."));
        }

        return actions;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the top-level item collection.
    /// </summary>
    public KryptonMenuBarItemCollection Items => _menuBar.Items;

    #endregion

    #region Implementation

    private void OnInsertStandardItems(object? sender, EventArgs e) =>
        KryptonMenuBarDesigner.InsertStandardItems(
            _menuBar,
            GetService(typeof(IDesignerHost)) as IDesignerHost,
            _service);

    #endregion
}
