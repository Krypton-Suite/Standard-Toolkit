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
/// Storage for form title bar button visibility values.
/// </summary>
/// <remarks>
/// Provides configuration for optional title bar buttons (New, Open, Save, Cut, Copy, Paste,
/// Undo, Redo, Page Setup, Print Preview, Print, Quick Print).
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FormTitleBarButtonVisibility
{
    #region Instance Fields

    private readonly Action? _onChanged;

    private bool _showNewButton;
    private bool _showOpenButton;
    private bool _showSaveButton;
    private bool _showSaveAsButton;
    private bool _showSaveAllButton;
    private bool _showCutButton;
    private bool _showCopyButton;
    private bool _showPasteButton;
    private bool _showUndoButton;
    private bool _showRedoButton;
    private bool _showPageSetupButton;
    private bool _showPrintPreviewButton;
    private bool _showPrintButton;
    private bool _showQuickPrintButton;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="FormTitleBarButtonVisibility"/> class.
    /// </summary>
    /// <param name="onChanged">Optional callback invoked when any visibility property changes.</param>
    public FormTitleBarButtonVisibility(Action? onChanged = null) => _onChanged = onChanged;

    #endregion

    #region Public

    /// <summary>Gets or sets whether the New button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the new button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowNewButton { get => _showNewButton; set { if (_showNewButton != value) { _showNewButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Open button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the open button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowOpenButton { get => _showOpenButton; set { if (_showOpenButton != value) { _showOpenButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Save button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the save button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowSaveButton { get => _showSaveButton; set { if (_showSaveButton != value) { _showSaveButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Save As button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the save as button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowSaveAsButton { get => _showSaveAsButton; set { if (_showSaveAsButton != value) { _showSaveAsButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Save All button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the save all button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowSaveAllButton { get => _showSaveAllButton; set { if (_showSaveAllButton != value) { _showSaveAllButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Cut button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the cut button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowCutButton { get => _showCutButton; set { if (_showCutButton != value) { _showCutButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Copy button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the copy button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowCopyButton { get => _showCopyButton; set { if (_showCopyButton != value) { _showCopyButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Paste button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the paste button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowPasteButton { get => _showPasteButton; set { if (_showPasteButton != value) { _showPasteButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Undo button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the undo button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowUndoButton { get => _showUndoButton; set { if (_showUndoButton != value) { _showUndoButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Redo button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the redo button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowRedoButton { get => _showRedoButton; set { if (_showRedoButton != value) { _showRedoButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Page Setup button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the page setup button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowPageSetupButton { get => _showPageSetupButton; set { if (_showPageSetupButton != value) { _showPageSetupButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Print Preview button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the print preview button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowPrintPreviewButton { get => _showPrintPreviewButton; set { if (_showPrintPreviewButton != value) { _showPrintPreviewButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Print button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the print button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowPrintButton { get => _showPrintButton; set { if (_showPrintButton != value) { _showPrintButton = value; _onChanged?.Invoke(); } } }

    /// <summary>Gets or sets whether the Quick Print button is shown in the title bar.</summary>
    [Category("Visuals")]
    [Description("Should the quick print button be shown in the title bar.")]
    [DefaultValue(false)]
    public bool ShowQuickPrintButton { get => _showQuickPrintButton; set { if (_showQuickPrintButton != value) { _showQuickPrintButton = value; _onChanged?.Invoke(); } } }

    #endregion

    #region Implementation

    /// <summary>
    /// Gets a value indicating whether all visibility settings are default (all buttons hidden).
    /// </summary>
    [Browsable(false)]
    public bool IsDefault =>
        ShowCutButton.Equals(false) &&
        ShowCopyButton.Equals(false) &&
        ShowPasteButton.Equals(false) &&
        ShowUndoButton.Equals(false) &&
        ShowRedoButton.Equals(false) &&
        ShowPageSetupButton.Equals(false) &&
        ShowPrintPreviewButton.Equals(false) &&
        ShowPrintButton.Equals(false) &&
        ShowQuickPrintButton.Equals(false) &&
        ShowNewButton.Equals(false) &&
        ShowOpenButton.Equals(false) &&
        ShowSaveButton.Equals(false) &&
        ShowSaveAsButton.Equals(false) &&
        ShowSaveAllButton.Equals(false);

    /// <summary>
    /// Resets all visibility properties to their default values (all buttons hidden).
    /// </summary>
    public void Reset()
    {
        ShowCutButton = false;
        ShowCopyButton = false;
        ShowPasteButton = false;
        ShowUndoButton = false;
        ShowRedoButton = false;
        ShowPageSetupButton = false;
        ShowPrintPreviewButton = false;
        ShowPrintButton = false;
        ShowQuickPrintButton = false;
        ShowNewButton = false;
        ShowOpenButton = false;
        ShowSaveButton = false;
        ShowSaveAsButton = false;
        ShowSaveAllButton = false;
    }

    #endregion
}
