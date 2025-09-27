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
/// Action list for the KryptonSplitterPanel control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonSplitterPanelExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonSplitterPanel? _splitterPanel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitterPanelExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonSplitterPanelExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _splitterPanel = (KryptonSplitterPanel?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the splitter panel minimum size.
    /// </summary>
    public Size MinimumSize
    {
        get => _splitterPanel?.MinimumSize ?? Size.Empty;
        set => SetPropertyValue(_splitterPanel!, nameof(MinimumSize), MinimumSize, value, v => _splitterPanel!.MinimumSize = v);
    }

    /// <summary>
    /// Gets and sets the splitter panel maximum size.
    /// </summary>
    public Size MaximumSize
    {
        get => _splitterPanel?.MaximumSize ?? Size.Empty;
        set => SetPropertyValue(_splitterPanel!, nameof(MaximumSize), MaximumSize, value, v => _splitterPanel!.MaximumSize = v);
    }

    /// <summary>
    /// Gets and sets whether the splitter panel is visible.
    /// </summary>
    public bool Visible
    {
        get => _splitterPanel?.Visible ?? true;
        set => SetPropertyValue(_splitterPanel!, nameof(Visible), Visible, value, v => _splitterPanel!.Visible = v);
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
        if (_splitterPanel != null)
        {
            // Add the list of SplitterPanel specific actions
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(MinimumSize), @"Minimum Size", @"Layout", @"Minimum panel size"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaximumSize), @"Maximum Size", @"Layout", @"Maximum panel size"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Panel visible"));
        }

        return actions;
    }
    #endregion
}
