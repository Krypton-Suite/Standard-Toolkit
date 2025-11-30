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
/// Action list for the KryptonLinkLabel control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonLinkLabelExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonLinkLabel? _linkLabel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLinkLabelExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonLinkLabelExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _linkLabel = (KryptonLinkLabel?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the link behavior.
    /// </summary>
    public KryptonLinkBehavior LinkBehavior
    {
        get => _linkLabel?.LinkBehavior ?? KryptonLinkBehavior.AlwaysUnderline;
        set => SetPropertyValue(_linkLabel!, nameof(LinkBehavior), LinkBehavior, value, v => _linkLabel!.LinkBehavior = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether the link should be visited.
    /// </summary>
    public bool LinkVisited
    {
        get => _linkLabel?.LinkVisited ?? false;
        set => SetPropertyValue(_linkLabel!, nameof(LinkVisited), LinkVisited, value, v => _linkLabel!.LinkVisited = v);
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
        if (_linkLabel != null)
        {
            // Add the list of LinkLabel specific actions
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(LinkBehavior), @"Link Behavior", @"Behavior", @"Link behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(LinkVisited), @"Link Visited", @"Behavior", @"Link visited state"));
        }

        return actions;
    }
    #endregion
}
