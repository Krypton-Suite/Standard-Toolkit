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
/// Action list for KryptonTextBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonTextBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonTextBox _textBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTextBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTextBoxExtensibilityActionList(KryptonTextBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the textbox instance
        _textBox = (owner.Component as KryptonTextBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the textbox style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _textBox.InputControlStyle;
        set => SetPropertyValue(_textBox, nameof(InputControlStyle), _textBox.InputControlStyle, value, v => _textBox.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the textbox text.
    /// </summary>
    public string Text
    {
        get => _textBox.Text;
        set => SetPropertyValue(_textBox, nameof(Text), _textBox.Text, value, v => _textBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the maximum length.
    /// </summary>
    public int MaxLength
    {
        get => _textBox.MaxLength;
        set => SetPropertyValue(_textBox, nameof(MaxLength), _textBox.MaxLength, value, v => _textBox.MaxLength = v);
    }

    /// <summary>
    /// Gets and sets whether the textbox is multiline.
    /// </summary>
    public bool Multiline
    {
        get => _textBox.Multiline;
        set => SetPropertyValue(_textBox, nameof(Multiline), _textBox.Multiline, value, v => _textBox.Multiline = v);
    }

    /// <summary>
    /// Gets and sets whether the textbox is password protected.
    /// </summary>
    public bool UseSystemPasswordChar
    {
        get => _textBox.UseSystemPasswordChar;
        set => SetPropertyValue(_textBox, nameof(UseSystemPasswordChar), _textBox.UseSystemPasswordChar, value, v => _textBox.UseSystemPasswordChar = v);
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
        if (_textBox != null)
        {
            // Add the list of textbox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"Input control style"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Behavior", @"Textbox text"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaxLength), @"Max Length", @"Behavior", @"Maximum text length"));
            actions.Add(new DesignerActionPropertyItem(nameof(Multiline), @"Multiline", @"Behavior", @"Allow multiple lines"));
            actions.Add(new DesignerActionPropertyItem(nameof(UseSystemPasswordChar), @"Use System Password Char", @"Behavior", @"Use system password character"));
        }

        return actions;
    }
    #endregion
}
