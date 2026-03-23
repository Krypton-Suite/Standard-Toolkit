#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="ButtonStyleConverter"/> integrated toolbar strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class IntegratedToolBarStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEW = @"New";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_OPEN = @"Open";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE = @"Save";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_AS = @"Save As";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_ALL = @"Save All";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CUT = @"Cut";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_COPY = @"Copy";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PASTE = @"Paste";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_UNDO = @"Undo";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_REDO = @"Redo";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PAGE_SETUP = @"Page Setup";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT_PREVIEW = @"Print Preview";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT = @"Print";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_QUICK_PRINT = @"Quick Print";

    // Menu item text — includes & accelerator characters for keyboard navigation.
    // These are separate from the tooltip-only strings above so tooltips remain unaffected.
    private const string DEFAULT_MENU_ITEM_NEW = @"&New"; // Accelerator key - 'N'
    private const string DEFAULT_MENU_ITEM_OPEN = @"&Open"; // Accelerator key - 'O'
    private const string DEFAULT_MENU_ITEM_SAVE = @"&Save"; // Accelerator key - 'S'
    private const string DEFAULT_MENU_ITEM_SAVE_AS = @"Save &As"; // Accelerator key - 'A'
    private const string DEFAULT_MENU_ITEM_SAVE_ALL = @"Save A&ll"; // Accelerator key - 'l'
    private const string DEFAULT_MENU_ITEM_CUT = @"Cu&t"; // Accelerator key - 't'
    private const string DEFAULT_MENU_ITEM_COPY = @"&Copy"; // Accelerator key - 'C'
    private const string DEFAULT_MENU_ITEM_PASTE = @"&Paste"; // Accelerator key - 'P'
    private const string DEFAULT_MENU_ITEM_UNDO = @"&Undo"; // Accelerator key - 'U'
    private const string DEFAULT_MENU_ITEM_REDO = @"&Redo"; // Accelerator key - 'R'
    private const string DEFAULT_MENU_ITEM_PRINT_PREVIEW = @"Print Pre&view"; // Accelerator key - 'v'
    private const string DEFAULT_MENU_ITEM_PRINT = @"&Print"; // Accelerator key - 'P'

    #endregion

    #region Identity

    public IntegratedToolBarStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => New.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEW) &&
                             Open.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_OPEN) &&
                             Save.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE) &&
                             SaveAll.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_ALL) &&
                             SaveAs.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_AS) &&
                             Cut.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CUT) &&
                             Copy.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_COPY) &&
                             Paste.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PASTE) &&
                             Undo.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_UNDO) &&
                             Redo.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_REDO) &&
                             PageSetup.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PAGE_SETUP) &&
                             PrintPreview.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT_PREVIEW) &&
                             Print.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT) &&
                             QuickPrint.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_QUICK_PRINT) &&
                             NewMenuItem.Equals(DEFAULT_MENU_ITEM_NEW) &&
                             OpenMenuItem.Equals(DEFAULT_MENU_ITEM_OPEN) &&
                             SaveMenuItem.Equals(DEFAULT_MENU_ITEM_SAVE) &&
                             SaveAsMenuItem.Equals(DEFAULT_MENU_ITEM_SAVE_AS) &&
                             SaveAllMenuItem.Equals(DEFAULT_MENU_ITEM_SAVE_ALL) &&
                             CutMenuItem.Equals(DEFAULT_MENU_ITEM_CUT) &&
                             CopyMenuItem.Equals(DEFAULT_MENU_ITEM_COPY) &&
                             PasteMenuItem.Equals(DEFAULT_MENU_ITEM_PASTE) &&
                             UndoMenuItem.Equals(DEFAULT_MENU_ITEM_UNDO) &&
                             RedoMenuItem.Equals(DEFAULT_MENU_ITEM_REDO) &&
                             PrintPreviewMenuItem.Equals(DEFAULT_MENU_ITEM_PRINT_PREVIEW) &&
                             PrintMenuItem.Equals(DEFAULT_MENU_ITEM_PRINT);

    public void Reset()
    {
        New = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEW;

        Open = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_OPEN;

        Save = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE;

        SaveAll = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_ALL;

        SaveAs = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_AS;

        Cut = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CUT;

        Copy = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_COPY;

        Paste = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PASTE;

        Undo = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_UNDO;

        Redo = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_REDO;

        PageSetup = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PAGE_SETUP;

        PrintPreview = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT_PREVIEW;

        Print = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT;

        QuickPrint = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_QUICK_PRINT;

        NewMenuItem = DEFAULT_MENU_ITEM_NEW;

        OpenMenuItem = DEFAULT_MENU_ITEM_OPEN;

        SaveMenuItem = DEFAULT_MENU_ITEM_SAVE;

        SaveAsMenuItem = DEFAULT_MENU_ITEM_SAVE_AS;

        SaveAllMenuItem = DEFAULT_MENU_ITEM_SAVE_ALL;

        CutMenuItem = DEFAULT_MENU_ITEM_CUT;

        CopyMenuItem = DEFAULT_MENU_ITEM_COPY;

        PasteMenuItem = DEFAULT_MENU_ITEM_PASTE;

        UndoMenuItem = DEFAULT_MENU_ITEM_UNDO;

        RedoMenuItem = DEFAULT_MENU_ITEM_REDO;

        PrintPreviewMenuItem = DEFAULT_MENU_ITEM_PRINT_PREVIEW;

        PrintMenuItem = DEFAULT_MENU_ITEM_PRINT;
    }

    /// <summary>Gets or sets the new integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The new integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEW)]
    [RefreshProperties(RefreshProperties.All)]
    public string New { get; set; }

    /// <summary>Gets or sets the open integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The open integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_OPEN)]
    [RefreshProperties(RefreshProperties.All)]
    public string Open { get; set; }

    /// <summary>Gets or sets the save integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The save integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Save { get; set; }

    /// <summary>Gets or sets the save as integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The save as integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_AS)]
    [RefreshProperties(RefreshProperties.All)]
    public string SaveAs { get; set; }

    /// <summary>Gets or sets the save all integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The save all integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_ALL)]
    [RefreshProperties(RefreshProperties.All)]
    public string SaveAll { get; set; }

    /// <summary>Gets or sets the cut integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The cut integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CUT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cut { get; set; }

    /// <summary>Gets or sets the copy integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The copy integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_COPY)]
    [RefreshProperties(RefreshProperties.All)]
    public string Copy { get; set; }

    /// <summary>Gets or sets the paste integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The paste integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PASTE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Paste { get; set; }

    /// <summary>Gets or sets the undo integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The undo integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_UNDO)]
    [RefreshProperties(RefreshProperties.All)]
    public string Undo { get; set; }

    /// <summary>Gets or sets the redo integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The redo integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_REDO)]
    [RefreshProperties(RefreshProperties.All)]
    public string Redo { get; set; }

    /// <summary>Gets or sets the page setup integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The page setup integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PAGE_SETUP)]
    [RefreshProperties(RefreshProperties.All)]
    public string PageSetup { get; set; }

    /// <summary>Gets or sets the print preview integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The print preview integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT_PREVIEW)]
    [RefreshProperties(RefreshProperties.All)]
    public string PrintPreview { get; set; }

    /// <summary>Gets or sets the print integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The print integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Print { get; set; }

    /// <summary>Gets or sets the quick print integrated toolbar button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The quick print integrated toolbar button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_QUICK_PRINT)]
    [RefreshProperties(RefreshProperties.All)]
    public string QuickPrint { get; set; }

    /// <summary>Gets or sets the New context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The New context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_NEW)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string NewMenuItem { get; set; }

    /// <summary>Gets or sets the Open context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Open context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_OPEN)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string OpenMenuItem { get; set; }

    /// <summary>Gets or sets the Save context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Save context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_SAVE)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string SaveMenuItem { get; set; }

    /// <summary>Gets or sets the Save As context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Save As context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_SAVE_AS)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string SaveAsMenuItem { get; set; }

    /// <summary>Gets or sets the Save All context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Save All context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_SAVE_ALL)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string SaveAllMenuItem { get; set; }

    /// <summary>Gets or sets the Cut context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Cut context menu item text shown in the title bar Edit menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_CUT)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string CutMenuItem { get; set; }

    /// <summary>Gets or sets the Copy context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Copy context menu item text shown in the title bar Edit menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_COPY)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string CopyMenuItem { get; set; }

    /// <summary>Gets or sets the Paste context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Paste context menu item text shown in the title bar Edit menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_PASTE)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string PasteMenuItem { get; set; }

    /// <summary>Gets or sets the Undo context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Undo context menu item text shown in the title bar Edit menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_UNDO)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string UndoMenuItem { get; set; }

    /// <summary>Gets or sets the Redo context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Redo context menu item text shown in the title bar Edit menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_REDO)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string RedoMenuItem { get; set; }

    /// <summary>Gets or sets the Print Preview context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Print Preview context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_PRINT_PREVIEW)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string PrintPreviewMenuItem { get; set; }

    /// <summary>Gets or sets the Print context menu item text (includes &amp; accelerator).</summary>
    [Category(@"Visuals")]
    [Description(@"The Print context menu item text shown in the title bar File menu. Supports & accelerator characters.")]
    [DefaultValue(DEFAULT_MENU_ITEM_PRINT)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public string PrintMenuItem { get; set; }

    #endregion
}