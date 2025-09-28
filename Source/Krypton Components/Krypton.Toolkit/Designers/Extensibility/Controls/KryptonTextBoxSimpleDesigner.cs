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
/// Simplified designer for KryptonTextBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonTextBoxSimpleDesigner : ControlDesigner
{
    #region Public Overrides
    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            
            if (Component != null)
            {
                actionLists.Add(new KryptonTextBoxSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonTextBox optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonTextBoxSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonTextBox _textBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTextBoxSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonTextBoxSimpleActionList(IComponent component)
        : base(component)
    {
        _textBox = (KryptonTextBox)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the text box text.
    /// </summary>
    [Category("Appearance")]
    [Description("Text box text")]
    public string Text
    {
        get => _textBox.Text;
        set
        {
            if (_textBox.Text != value)
            {
                _textBox.Text = value;
                NotifyPropertyChanged("Text", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the text box cue hint text.
    /// </summary>
    [Category("Appearance")]
    [Description("Cue hint text")]
    public string CueHintText
    {
        get => _textBox.CueHint.CueHintText;
        set
        {
            if (_textBox.CueHint.CueHintText != value)
            {
                _textBox.CueHint.CueHintText = value;
                NotifyPropertyChanged("CueHintText", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the text box password character.
    /// </summary>
    [Category("Behavior")]
    [Description("Password character")]
    public char PasswordChar
    {
        get => _textBox.PasswordChar;
        set
        {
            if (_textBox.PasswordChar != value)
            {
                _textBox.PasswordChar = value;
                NotifyPropertyChanged("PasswordChar", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the text box multiline setting.
    /// </summary>
    [Category("Behavior")]
    [Description("Multiline")]
    public bool Multiline
    {
        get => _textBox.Multiline;
        set
        {
            if (_textBox.Multiline != value)
            {
                _textBox.Multiline = value;
                NotifyPropertyChanged("Multiline", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category("Visuals")]
    [Description("Palette applied to drawing")]
    public PaletteMode PaletteMode
    {
        get => _textBox.PaletteMode;
        set
        {
            if (_textBox.PaletteMode != value)
            {
                _textBox.PaletteMode = value;
                NotifyPropertyChanged("PaletteMode", value);
            }
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Notify that a property has changed.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    /// <param name="value">New value of the property.</param>
    private void NotifyPropertyChanged(string propertyName, object? value)
    {
        var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (changeService != null)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(_textBox)[propertyName];
            changeService.OnComponentChanged(_textBox, propertyDescriptor, null, value);
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
        var actions = new DesignerActionItemCollection();

        if (_textBox != null)
        {
            // Appearance
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Text box text"));
            actions.Add(new DesignerActionPropertyItem(nameof(CueHintText), "Cue Hint Text", "Appearance", "Cue hint text"));
            
            // Behavior
            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(PasswordChar), "Password Character", "Behavior", "Password character"));
            actions.Add(new DesignerActionPropertyItem(nameof(Multiline), "Multiline", "Behavior", "Multiline"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
