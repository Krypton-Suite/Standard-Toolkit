#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Action list for KryptonWorkspaceSequence using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonWorkspaceSequenceExtensibilityActionList : KryptonWorkspaceExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonWorkspaceSequence _sequence;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceSequenceExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonWorkspaceSequenceExtensibilityActionList(KryptonWorkspaceSequenceExtensibilityDesigner owner)
        : base(owner)
    {
        _sequence = (owner.Component as KryptonWorkspaceSequence)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the orientation.
    /// </summary>
    [Category("Layout")]
    [Description("Orientation of the sequence.")]
    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
        get => _sequence.Orientation;
        set => SetPropertyValue(nameof(Orientation), value);
    }

    /// <summary>
    /// Gets or sets whether the sequence is visible.
    /// </summary>
    [Category("Behavior")]
    [Description("Whether the sequence is visible.")]
    [DefaultValue(true)]
    public bool Visible
    {
        get => _sequence.Visible;
        set => SetPropertyValue(nameof(Visible), value);
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();

        // Add the action items
        items.Add(new DesignerActionHeaderItem("Layout"));
        items.Add(new DesignerActionPropertyItem(nameof(Orientation), "Orientation", "Layout", "Orientation of the sequence."));
        items.Add(new DesignerActionHeaderItem("Behavior"));
        items.Add(new DesignerActionPropertyItem(nameof(Visible), "Visible", "Behavior", "Whether the sequence is visible."));

        return items;
    }
    #endregion
}
