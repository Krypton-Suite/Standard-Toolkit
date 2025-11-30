#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonGroup control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonGroupExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonGroup? _group;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGroupExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonGroupExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _group = (KryptonGroup?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    public AutoSizeMode AutoSizeMode
    {
        get => _group?.AutoSizeMode ?? AutoSizeMode.GrowOnly;
        set => SetPropertyValue(_group!, nameof(AutoSizeMode), AutoSizeMode, value, v => _group!.AutoSizeMode = v);
    }

    /// <summary>
    /// Gets and sets whether the group auto sizes.
    /// </summary>
    public bool AutoSize
    {
        get => _group?.AutoSize ?? false;
        set => SetPropertyValue(_group!, nameof(AutoSize), AutoSize, value, v => _group!.AutoSize = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_group != null)
        {
            // Add the list of Group specific actions
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoSize), @"Auto Size", @"Layout", @"Automatically size the group"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoSizeMode), @"Auto Size Mode", @"Layout", @"Auto size mode"));
        }

        return actions;
    }
    #endregion
}
