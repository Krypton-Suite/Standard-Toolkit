#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for form title bar button alignment values.
/// </summary>
/// <remarks>
/// Provides configuration for alignment of optional title bar buttons (New, Open, Save, Cut, Copy, Paste,
/// Undo, Redo, Page Setup, Print Preview, Print, Quick Print).
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FormTitleBarButtonAlignment
{
    #region Instance Fields

    private readonly Action? _onChanged;

    private PaletteRelativeEdgeAlign _newButtonAlignment;
    private PaletteRelativeEdgeAlign _openButtonAlignment;
    private PaletteRelativeEdgeAlign _saveButtonAlignment;
    private PaletteRelativeEdgeAlign _saveAsButtonAlignment;
    private PaletteRelativeEdgeAlign _saveAllButtonAlignment;
    private PaletteRelativeEdgeAlign _cutButtonAlignment;
    private PaletteRelativeEdgeAlign _copyButtonAlignment;
    private PaletteRelativeEdgeAlign _pasteButtonAlignment;
    private PaletteRelativeEdgeAlign _undoButtonAlignment;
    private PaletteRelativeEdgeAlign _redoButtonAlignment;
    private PaletteRelativeEdgeAlign _pageSetupButtonAlignment;
    private PaletteRelativeEdgeAlign _printPreviewButtonAlignment;
    private PaletteRelativeEdgeAlign _printButtonAlignment;
    private PaletteRelativeEdgeAlign _quickPrintButtonAlignment;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="FormTitleBarButtonAlignment"/> class.
    /// </summary>
    /// <param name="onChanged">Optional callback invoked when any alignment property changes.</param>
    public FormTitleBarButtonAlignment(Action? onChanged = null)
    {
        _onChanged = onChanged;
        _newButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _openButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _saveButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _saveAsButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _saveAllButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _cutButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _copyButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _pasteButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _undoButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _redoButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _pageSetupButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _printPreviewButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _printButtonAlignment = PaletteRelativeEdgeAlign.Far;
        _quickPrintButtonAlignment = PaletteRelativeEdgeAlign.Far;
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the alignment of the New button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the new button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign NewButtonAlignment { get => _newButtonAlignment; set { if (_newButtonAlignment != value) { _newButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Open button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the open button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign OpenButtonAlignment { get => _openButtonAlignment; set { if (_openButtonAlignment != value) { _openButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Save button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the save button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign SaveButtonAlignment { get => _saveButtonAlignment; set { if (_saveButtonAlignment != value) { _saveButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Save As button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the save as button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign SaveAsButtonAlignment { get => _saveAsButtonAlignment; set { if (_saveAsButtonAlignment != value) { _saveAsButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Save All button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the save all button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign SaveAllButtonAlignment { get => _saveAllButtonAlignment; set { if (_saveAllButtonAlignment != value) { _saveAllButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Cut button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the cut button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign CutButtonAlignment { get => _cutButtonAlignment; set { if (_cutButtonAlignment != value) { _cutButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Copy button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the copy button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign CopyButtonAlignment { get => _copyButtonAlignment; set { if (_copyButtonAlignment != value) { _copyButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Paste button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the paste button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign PasteButtonAlignment { get => _pasteButtonAlignment; set { if (_pasteButtonAlignment != value) { _pasteButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Undo button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the undo button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign UndoButtonAlignment { get => _undoButtonAlignment; set { if (_undoButtonAlignment != value) { _undoButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Redo button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the redo button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign RedoButtonAlignment { get => _redoButtonAlignment; set { if (_redoButtonAlignment != value) { _redoButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Page Setup button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the page setup button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign PageSetupButtonAlignment { get => _pageSetupButtonAlignment; set { if (_pageSetupButtonAlignment != value) { _pageSetupButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Print Preview button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the print preview button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign PrintPreviewButtonAlignment { get => _printPreviewButtonAlignment; set { if (_printPreviewButtonAlignment != value) { _printPreviewButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Print button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the print button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign PrintButtonAlignment { get => _printButtonAlignment; set { if (_printButtonAlignment != value) { _printButtonAlignment = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets the alignment of the Quick Print button in the title bar.</summary>
    [Category("Visuals")]
    [Description("Determines the alignment of the quick print button in the title bar.")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Far")]
    public PaletteRelativeEdgeAlign QuickPrintButtonAlignment { get => _quickPrintButtonAlignment; set { if (_quickPrintButtonAlignment != value) { _quickPrintButtonAlignment = value; _onChanged?.Invoke(); } } }

    #endregion

    #region Implementation

    /// <summary>
    /// Gets a value indicating whether all alignment settings are default (Far).
    /// </summary>
    [Browsable(false)]
    public bool IsDefault =>
        NewButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        OpenButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        SaveButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        SaveAsButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        SaveAllButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        CutButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        CopyButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        PasteButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        UndoButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        RedoButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        PageSetupButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        PrintPreviewButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        PrintButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far) &&
        QuickPrintButtonAlignment.Equals(PaletteRelativeEdgeAlign.Far);

    /// <summary>
    /// Resets all alignment properties to their default values (Far).
    /// </summary>
    public void Reset()
    {
        NewButtonAlignment = PaletteRelativeEdgeAlign.Far;
        OpenButtonAlignment = PaletteRelativeEdgeAlign.Far;
        SaveButtonAlignment = PaletteRelativeEdgeAlign.Far;
        SaveAsButtonAlignment = PaletteRelativeEdgeAlign.Far;
        SaveAllButtonAlignment = PaletteRelativeEdgeAlign.Far;
        CutButtonAlignment = PaletteRelativeEdgeAlign.Far;
        CopyButtonAlignment = PaletteRelativeEdgeAlign.Far;
        PasteButtonAlignment = PaletteRelativeEdgeAlign.Far;
        UndoButtonAlignment = PaletteRelativeEdgeAlign.Far;
        RedoButtonAlignment = PaletteRelativeEdgeAlign.Far;
        PageSetupButtonAlignment = PaletteRelativeEdgeAlign.Far;
        PrintPreviewButtonAlignment = PaletteRelativeEdgeAlign.Far;
        PrintButtonAlignment = PaletteRelativeEdgeAlign.Far;
        QuickPrintButtonAlignment = PaletteRelativeEdgeAlign.Far;
    }

    #endregion
}
