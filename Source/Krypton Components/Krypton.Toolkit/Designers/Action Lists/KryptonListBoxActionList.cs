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

internal class KryptonListBoxActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonListBox _listBox;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonListBoxActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonListBoxActionList(KryptonListBoxDesigner owner)
        : base(owner.Component)
    {
        // Remember the list box instance
        _listBox = (owner.Component as KryptonListBox)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the syle used for list items.
    /// </summary>
    public ButtonStyle ItemStyle
    {
        get => _listBox.ItemStyle;

        set
        {
            if (_listBox.ItemStyle != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.ItemStyle, value);
                _listBox.ItemStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the background drawing style.
    /// </summary>
    public PaletteBackStyle BackStyle
    {
        get => _listBox.BackStyle;

        set
        {
            if (_listBox.BackStyle != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.BackStyle, value);
                _listBox.BackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the border drawing style.
    /// </summary>
    public PaletteBorderStyle BorderStyle
    {
        get => _listBox.BorderStyle;

        set
        {
            if (_listBox.BorderStyle != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.BorderStyle, value);
                _listBox.BorderStyle = value;
            }
        }
    }

    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _listBox.KryptonContextMenu;

        set
        {
            if (_listBox.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.KryptonContextMenu, value);

                _listBox.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the selection mode.
    /// </summary>
    public SelectionMode SelectionMode
    {
        get => _listBox.SelectionMode;

        set
        {
            if (_listBox.SelectionMode != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.SelectionMode, value);
                _listBox.SelectionMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the selection mode.
    /// </summary>
    public bool Sorted
    {
        get => _listBox.Sorted;

        set
        {
            if (_listBox.Sorted != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.Sorted, value);
                _listBox.Sorted = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _listBox.PaletteMode;

        set
        {
            if (_listBox.PaletteMode != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.PaletteMode, value);
                _listBox.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonShortTextFont
    {
        get => _listBox.StateCommon.Item.Content.ShortText.Font!;

        set
        {
            if (_listBox.StateCommon.Item.Content.ShortText.Font != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.StateCommon.Item.Content.ShortText.Font, value);

                _listBox.StateCommon.Item.Content.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonLongTextFont
    {
        get => _listBox.StateCommon.Item.Content.LongText.Font!;

        set
        {
            if (_listBox.StateCommon.Item.Content.LongText.Font != value)
            {
                _service?.OnComponentChanged(_listBox, null, _listBox.StateCommon.Item.Content.LongText.Font, value);

                _listBox.StateCommon.Item.Content.LongText.Font = value;
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
            // Add the list of list box specific actions
            new DesignerActionHeaderItem(nameof(Appearance)),
            new DesignerActionPropertyItem(nameof(BackStyle), @"Back Style", nameof(Appearance),
                @"Style used to draw background."),
            new DesignerActionPropertyItem(nameof(BorderStyle), @"Border Style", nameof(Appearance), @"Style used to draw the border."),
            new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."),
            new DesignerActionPropertyItem(nameof(ItemStyle), @"Item Style", nameof(Appearance), @"How to display list items."),
            new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."),
            new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."),
            new DesignerActionHeaderItem(nameof(Behavior)),
            new DesignerActionPropertyItem(nameof(SelectionMode), @"Selection Mode", nameof(Behavior), @"Determines the selection mode."),
            new DesignerActionPropertyItem(nameof(Sorted), nameof(Sorted), nameof(Behavior), @"Should items be sorted according to string."),
            new DesignerActionHeaderItem(@"Visuals"),
            new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing")
        };

        return actions;
    }
    #endregion
}