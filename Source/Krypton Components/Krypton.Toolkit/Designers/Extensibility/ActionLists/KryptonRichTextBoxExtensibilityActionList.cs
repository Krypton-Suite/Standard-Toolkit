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
/// Action list for KryptonRichTextBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonRichTextBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonRichTextBox _richTextBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRichTextBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRichTextBoxExtensibilityActionList(KryptonRichTextBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the richtextbox instance
        _richTextBox = (owner.Component as KryptonRichTextBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the richtextbox style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _richTextBox.InputControlStyle;
        set => SetPropertyValue(_richTextBox, nameof(InputControlStyle), _richTextBox.InputControlStyle, value, v => _richTextBox.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the richtextbox text.
    /// </summary>
    public string Text
    {
        get => _richTextBox.Text;
        set => SetPropertyValue(_richTextBox, nameof(Text), _richTextBox.Text, value, v => _richTextBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the maximum length.
    /// </summary>
    public int MaxLength
    {
        get => _richTextBox.MaxLength;
        set => SetPropertyValue(_richTextBox, nameof(MaxLength), _richTextBox.MaxLength, value, v => _richTextBox.MaxLength = v);
    }

    /// <summary>
    /// Gets and sets whether the richtextbox is multiline.
    /// </summary>
    public bool Multiline
    {
        get => _richTextBox.Multiline;
        set => SetPropertyValue(_richTextBox, nameof(Multiline), _richTextBox.Multiline, value, v => _richTextBox.Multiline = v);
    }

    /// <summary>
    /// Gets and sets whether the richtextbox is read-only.
    /// </summary>
    public bool ReadOnly
    {
        get => _richTextBox.ReadOnly;
        set => SetPropertyValue(_richTextBox, nameof(ReadOnly), _richTextBox.ReadOnly, value, v => _richTextBox.ReadOnly = v);
    }

    /// <summary>
    /// Gets and sets whether the richtextbox accepts tabs.
    /// </summary>
    public bool AcceptsTab
    {
        get => _richTextBox.AcceptsTab;
        set => SetPropertyValue(_richTextBox, nameof(AcceptsTab), _richTextBox.AcceptsTab, value, v => _richTextBox.AcceptsTab = v);
    }

    /// <summary>
    /// Gets and sets the word wrap mode.
    /// </summary>
    public bool WordWrap
    {
        get => _richTextBox.WordWrap;
        set => SetPropertyValue(_richTextBox, nameof(WordWrap), _richTextBox.WordWrap, value, v => _richTextBox.WordWrap = v);
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
            // Add the list of richtextbox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"Input control style"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Behavior", @"RichTextBox text"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaxLength), @"Max Length", @"Behavior", @"Maximum text length"));
            actions.Add(new DesignerActionPropertyItem(nameof(Multiline), nameof(Multiline), @"Behavior", @"Allow multiple lines"));
            actions.Add(new DesignerActionPropertyItem(nameof(ReadOnly), nameof(ReadOnly), @"Behavior", @"Read-only mode"));
            actions.Add(new DesignerActionPropertyItem(nameof(AcceptsTab), nameof(AcceptsTab), @"Behavior", @"Accept tab characters"));
            actions.Add(new DesignerActionPropertyItem(nameof(WordWrap), nameof(WordWrap), @"Behavior", @"Word wrap text"));
        }

        return actions;
    }
    #endregion
}
