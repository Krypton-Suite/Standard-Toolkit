#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Utilities;

internal class KryptonCodeEditorActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonCodeEditor _codeEditor;
    private readonly IComponentChangeService? _service;
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonCodeEditorActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCodeEditorActionList(KryptonCodeEditorDesigner owner)
        : base(owner.Component)
    {
        // Remember the code editor instance
        _codeEditor = (owner.Component as KryptonCodeEditor)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    
    #endregion

    #region Public
    
    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _codeEditor.KryptonContextMenu;

        set
        {
            if (_codeEditor.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.KryptonContextMenu, value);

                _codeEditor.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _codeEditor.PaletteMode;

        set
        {
            if (_codeEditor.PaletteMode != value)
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.PaletteMode, value);
                _codeEditor.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the programming language.
    /// </summary>
    public Language Language
    {
        get => _codeEditor.Language;

        set
        {
            if (_codeEditor.Language != value)
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.Language, value);
                _codeEditor.Language = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets whether line numbers are shown.
    /// </summary>
    public bool ShowLineNumbers
    {
        get => _codeEditor.ShowLineNumbers;

        set
        {
            if (_codeEditor.ShowLineNumbers != value)
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.ShowLineNumbers, value);
                _codeEditor.ShowLineNumbers = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets whether code folding is enabled.
    /// </summary>
    public bool EnableCodeFolding
    {
        get => _codeEditor.EnableCodeFolding;

        set
        {
            if (_codeEditor.EnableCodeFolding != value)
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.EnableCodeFolding, value);
                _codeEditor.EnableCodeFolding = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets whether auto-completion is enabled.
    /// </summary>
    public bool AutoCompleteEnabled
    {
        get => _codeEditor.AutoCompleteEnabled;

        set
        {
            if (_codeEditor.AutoCompleteEnabled != value)
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.AutoCompleteEnabled, value);
                _codeEditor.AutoCompleteEnabled = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the editor font.
    /// </summary>
    public Font EditorFont
    {
        get => _codeEditor.EditorFont;

        set
        {
            if (!Equals(_codeEditor.EditorFont, value))
            {
                _service?.OnComponentChanged(_codeEditor, null, _codeEditor.EditorFont, value);
                _codeEditor.EditorFont = value;
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
        if (_codeEditor is not null)
        {
            // Add the list of code editor specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionPropertyItem(nameof(EditorFont), nameof(EditorFont), nameof(Appearance), @"The font used for editing code."));
            actions.Add(new DesignerActionHeaderItem(@"Code Editor"));
            actions.Add(new DesignerActionPropertyItem(nameof(Language), nameof(Language), @"Code Editor", @"The programming language for syntax highlighting."));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowLineNumbers), @"Show Line Numbers", @"Code Editor", @"Indicates whether line numbers are displayed."));
            actions.Add(new DesignerActionPropertyItem(nameof(EnableCodeFolding), @"Enable Code Folding", @"Code Editor", @"Indicates whether code folding is enabled."));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoCompleteEnabled), @"Auto-Complete Enabled", @"Code Editor", @"Indicates whether auto-completion is enabled."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing."));
        }

        return actions;
    }
    
    #endregion
}
