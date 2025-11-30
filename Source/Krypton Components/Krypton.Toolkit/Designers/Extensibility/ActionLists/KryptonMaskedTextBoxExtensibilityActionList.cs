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
/// Action list for KryptonMaskedTextBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonMaskedTextBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonMaskedTextBox _maskedTextBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonMaskedTextBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonMaskedTextBoxExtensibilityActionList(KryptonMaskedTextBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the maskedtextbox instance
        _maskedTextBox = (owner.Component as KryptonMaskedTextBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the maskedtextbox style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _maskedTextBox.InputControlStyle;
        set => SetPropertyValue(_maskedTextBox, nameof(InputControlStyle), _maskedTextBox.InputControlStyle, value, v => _maskedTextBox.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the maskedtextbox text.
    /// </summary>
    public string Text
    {
        get => _maskedTextBox.Text;
        set => SetPropertyValue(_maskedTextBox, nameof(Text), _maskedTextBox.Text, value, v => _maskedTextBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the mask.
    /// </summary>
    public string Mask
    {
        get => _maskedTextBox.Mask;
        set => SetPropertyValue(_maskedTextBox, nameof(Mask), _maskedTextBox.Mask, value, v => _maskedTextBox.Mask = v);
    }

    /// <summary>
    /// Gets and sets the maximum length.
    /// </summary>
    public int MaxLength
    {
        get => _maskedTextBox.MaxLength;
        set => SetPropertyValue(_maskedTextBox, nameof(MaxLength), _maskedTextBox.MaxLength, value, v => _maskedTextBox.MaxLength = v);
    }

    /// <summary>
    /// Gets and sets the prompt character.
    /// </summary>
    public char PromptChar
    {
        get => _maskedTextBox.PromptChar;
        set => SetPropertyValue(_maskedTextBox, nameof(PromptChar), _maskedTextBox.PromptChar, value, v => _maskedTextBox.PromptChar = v);
    }

    /// <summary>
    /// Gets and sets whether to hide prompt on leave.
    /// </summary>
    public bool HidePromptOnLeave
    {
        get => _maskedTextBox.HidePromptOnLeave;
        set => SetPropertyValue(_maskedTextBox, nameof(HidePromptOnLeave), _maskedTextBox.HidePromptOnLeave, value, v => _maskedTextBox.HidePromptOnLeave = v);
    }

    /// <summary>
    /// Gets and sets whether to use system password character.
    /// </summary>
    public bool UseSystemPasswordChar
    {
        get => _maskedTextBox.UseSystemPasswordChar;
        set => SetPropertyValue(_maskedTextBox, nameof(UseSystemPasswordChar), _maskedTextBox.UseSystemPasswordChar, value, v => _maskedTextBox.UseSystemPasswordChar = v);
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
        if (_maskedTextBox != null)
        {
            // Add the list of maskedtextbox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"Input control style"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Behavior", @"MaskedTextBox text"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaxLength), @"Max Length", @"Behavior", @"Maximum text length"));
            actions.Add(new DesignerActionPropertyItem(nameof(UseSystemPasswordChar), @"Use System Password Char", @"Behavior", @"Use system password character"));
            actions.Add(new DesignerActionHeaderItem(@"Mask"));
            actions.Add(new DesignerActionPropertyItem(nameof(Mask), nameof(Mask), @"Mask", @"Input mask"));
            actions.Add(new DesignerActionPropertyItem(nameof(PromptChar), nameof(PromptChar), @"Mask", @"Prompt character"));
            actions.Add(new DesignerActionPropertyItem(nameof(HidePromptOnLeave), @"Hide Prompt On Leave", @"Mask", @"Hide prompt when control loses focus"));
        }

        return actions;
    }
    #endregion
}
