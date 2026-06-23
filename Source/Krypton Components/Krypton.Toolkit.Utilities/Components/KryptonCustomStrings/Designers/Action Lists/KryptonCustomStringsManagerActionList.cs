#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Smart-tag action list for <see cref="KryptonCustomStringsManager"/>.
/// </summary>
internal class KryptonCustomStringsManagerActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonCustomStringsManager _manager;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCustomStringsManagerActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCustomStringsManagerActionList(KryptonCustomStringsManagerDesigner owner)
        : base(owner.Component)
    {
        _manager = (owner.Component as KryptonCustomStringsManager)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    /// <summary>
    /// Gets the custom string values exposed by the manager.
    /// </summary>
    public KryptonCustomStringValues CustomStrings => _manager.CustomStrings;

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_manager != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(
                new DesignerVerb(@"Reset Custom Strings", OnResetCustomStrings),
                @"Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));
            actions.Add(new DesignerActionPropertyItem(
                nameof(CustomStrings),
                @"Custom Strings",
                @"Data",
                @"Collection of custom string key/value entries that can be localised."));
        }

        return actions;
    }

    #endregion

    #region Implementation

    private void OnResetCustomStrings(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        DialogResult result = KryptonMessageBox.Show(
            @"This will reset all custom string values to their defaults. Do you want to continue?",
            @"Reset Custom Strings",
            KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            KryptonCustomStrings.ResetValues();
            _service?.OnComponentChanged(_manager, null, _manager.CustomStrings, _manager.CustomStrings);
        }
    }

    #endregion
}
