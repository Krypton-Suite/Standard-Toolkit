#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonTreeViewActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonTreeView _treeView;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTreeViewActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTreeViewActionList(KryptonTreeViewDesigner owner)
        : base(owner.Component)
    {
        // Remember the tree view instance
        _treeView = (owner.Component as KryptonTreeView)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _treeView.KryptonContextMenu;

        set
        {
            if (_treeView.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.KryptonContextMenu, value);

                _treeView.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the syle used for tree items.
    /// </summary>
    public ButtonStyle ItemStyle
    {
        get => _treeView.ItemStyle;

        set
        {
            if (_treeView.ItemStyle != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.ItemStyle, value);
                _treeView.ItemStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the background drawing style.
    /// </summary>
    public PaletteBackStyle BackStyle
    {
        get => _treeView.BackStyle;

        set
        {
            if (_treeView.BackStyle != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.BackStyle, value);
                _treeView.BackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the border drawing style.
    /// </summary>
    public PaletteBorderStyle BorderStyle
    {
        get => _treeView.BorderStyle;

        set
        {
            if (_treeView.BorderStyle != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.BorderStyle, value);
                _treeView.BorderStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the selection mode.
    /// </summary>
    public bool Sorted
    {
        get => _treeView.Sorted;

        set
        {
            if (_treeView.Sorted != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.Sorted, value);
                _treeView.Sorted = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _treeView.PaletteMode;

        set
        {
            if (_treeView.PaletteMode != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.PaletteMode, value);
                _treeView.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonShortTextFont
    {
        get => _treeView.StateCommon.Node.Content.ShortText.Font!;

        set
        {
            if (_treeView.StateCommon.Node.Content.ShortText.Font != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.StateCommon.Node.Content.ShortText.Font, value);

                _treeView.StateCommon.Node.Content.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonLongTextFont
    {
        get => _treeView.StateCommon.Node.Content.LongText.Font!;

        set
        {
            if (_treeView.StateCommon.Node.Content.LongText.Font != value)
            {
                _service?.OnComponentChanged(_treeView, null, _treeView.StateCommon.Node.Content.LongText.Font, value);

                _treeView.StateCommon.Node.Content.LongText.Font = value;
            }
        }
    }

    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection
        {
            // This can be null when deleting a control instance at design time
            // Add the list of tree view specific actions
            new DesignerActionHeaderItem(nameof(Appearance)),
            new DesignerActionPropertyItem(nameof(BackStyle), @"Back Style", nameof(Appearance),
                @"Style used to draw background."),
            new DesignerActionPropertyItem(nameof(BorderStyle), @"Border Style", nameof(Appearance), @"Style used to draw the border."),
            new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."),
            new DesignerActionPropertyItem(nameof(ItemStyle), @"Item Style", nameof(Appearance), @"How to display tree items."),
            new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."),
            new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."),
            new DesignerActionHeaderItem(nameof(Behavior)),
            new DesignerActionPropertyItem(nameof(Sorted), nameof(Sorted), nameof(Behavior), @"Should items be sorted according to string."),
            new DesignerActionHeaderItem(@"Visuals"),
            new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing")
        };

        return actions;
    }
    #endregion
}