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

internal class KryptonRichTextBoxActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonRichTextBox _richTextBox;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRichTextBoxActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRichTextBoxActionList(KryptonRichTextBoxDesigner owner)
        : base(owner.Component)
    {
        // Remember the text box instance
        _richTextBox = (owner.Component as KryptonRichTextBox)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _richTextBox.KryptonContextMenu;

        set
        {
            if (_richTextBox.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_richTextBox, null, _richTextBox.KryptonContextMenu, value);

                _richTextBox.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _richTextBox.PaletteMode;

        set
        {
            if (_richTextBox.PaletteMode != value)
            {
                _service?.OnComponentChanged(_richTextBox, null, _richTextBox.PaletteMode, value);
                _richTextBox.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _richTextBox.InputControlStyle;

        set
        {
            if (_richTextBox.InputControlStyle != value)
            {
                _service?.OnComponentChanged(_richTextBox, null, _richTextBox.InputControlStyle, value);
                _richTextBox.InputControlStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the Multiline mode.
    /// </summary>
    public bool Multiline
    {
        get => _richTextBox.Multiline;

        set
        {
            if (_richTextBox.Multiline != value)
            {
                _service?.OnComponentChanged(_richTextBox, null, _richTextBox.Multiline, value);
                _richTextBox.Multiline = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the WordWrap mode.
    /// </summary>
    public bool WordWrap
    {
        get => _richTextBox.WordWrap;

        set
        {
            if (_richTextBox.WordWrap != value)
            {
                _service?.OnComponentChanged(_richTextBox, null, _richTextBox.WordWrap, value);
                _richTextBox.WordWrap = value;
            }
        }
    }

    // <summary>Gets or sets the rich text box font.</summary>
    /// <value>The rich text box font.</value>
    public Font Font
    {
        get => _richTextBox.StateCommon.Content.Font!;

        set
        {
            if (_richTextBox.StateCommon.Content.Font != value)
            {
                _service?.OnComponentChanged(_richTextBox, null, _richTextBox.StateCommon.Content.Font, value);

                _richTextBox.StateCommon.Content.Font = value;
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
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_richTextBox != null)
        {
            // Add the list of rich text box specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"TextBox display style."));
            actions.Add(new DesignerActionPropertyItem(nameof(Font), nameof(Font), nameof(Appearance), @"Modifies the font of the control."));
            actions.Add(new DesignerActionHeaderItem(nameof(TextBox)));
            actions.Add(new DesignerActionPropertyItem(nameof(Multiline), nameof(Multiline), nameof(TextBox), @"Should text span multiple lines."));
            actions.Add(new DesignerActionPropertyItem(nameof(WordWrap), nameof(WordWrap), nameof(TextBox), @"Should words be wrapped over multiple lines."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing."));
        }

        return actions;
    }
    #endregion
}