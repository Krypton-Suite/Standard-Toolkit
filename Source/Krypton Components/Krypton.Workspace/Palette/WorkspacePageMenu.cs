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

namespace Krypton.Workspace;

/// <summary>
/// Storage for workspace context menu for pages.
/// </summary>
public class WorkspaceMenus : Storage
{
    #region Static Fields

    private const string DEFAULT_TEXT_CLOSE = "&Close";
    private const string DEFAULT_TEXT_CLOSE_ALL_BUT_THIS = "Close &All But This";
    private const string DEFAULT_TEXT_MOVE_NEXT = "Move &Next";
    private const string DEFAULT_TEXT_MOVE_PREVIOUS = "Move &Previous";
    private const string DEFAULT_TEXT_SPLIT_VERTICAL = "Split &Vertical";
    private const string DEFAULT_TEXT_SPLIT_HORIZONTAL = "Split &Horizontal";
    private const string DEFAULT_TEXT_REBALANCE = "&Rebalance";
    private const string DEFAULT_TEXT_MAXIMIZE = "&Maximize";
    private const string DEFAULT_TEXT_RESTORE = "Res&tore";
    private const Keys DEFAULT_SHORTCUT_CLOSE = Keys.Control | Keys.Shift | Keys.C;
    private const Keys DEFAULT_SHORTCUT_CLOSE_ALL_BUT_THIS = Keys.Control | Keys.Shift | Keys.A;
    private const Keys DEFAULT_SHORTCUT_MOVE_NEXT = Keys.Control | Keys.Shift | Keys.N;
    private const Keys DEFAULT_SHORTCUT_MOVE_PREVIOUS = Keys.Control | Keys.Shift | Keys.P;
    private const Keys DEFAULT_SHORTCUT_SPLIT_VERTICAL = Keys.Control | Keys.Shift | Keys.V;
    private const Keys DEFAULT_SHORTCUT_SPLIT_HORIZONTAL = Keys.Control | Keys.Shift | Keys.H;
    private const Keys DEFAULT_SHORTCUT_REBALANCE = Keys.Control | Keys.Shift | Keys.R;
    private const Keys DEFAULT_SHORTCUT_MAXIMIZE_RESTORE = Keys.Control | Keys.Shift | Keys.M;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the WorkspaceMenus class.
    /// </summary>
    public WorkspaceMenus(KryptonWorkspace workspace)
    {
        // Default values
        TextClose = DEFAULT_TEXT_CLOSE;
        TextCloseAllButThis = DEFAULT_TEXT_CLOSE_ALL_BUT_THIS;
        TextMoveNext = DEFAULT_TEXT_MOVE_NEXT;
        TextMovePrevious = DEFAULT_TEXT_MOVE_PREVIOUS;
        TextSplitVertical = DEFAULT_TEXT_SPLIT_VERTICAL;
        TextSplitHorizontal = DEFAULT_TEXT_SPLIT_HORIZONTAL;
        TextRebalance = DEFAULT_TEXT_REBALANCE;
        TextMaximize = DEFAULT_TEXT_MAXIMIZE;
        TextRestore = DEFAULT_TEXT_RESTORE;
        ShortcutClose = DEFAULT_SHORTCUT_CLOSE;
        ShortcutCloseAllButThis = DEFAULT_SHORTCUT_CLOSE_ALL_BUT_THIS;
        ShortcutMoveNext = DEFAULT_SHORTCUT_MOVE_NEXT;
        ShortcutMovePrevious = DEFAULT_SHORTCUT_MOVE_PREVIOUS;
        ShortcutSplitVertical = DEFAULT_SHORTCUT_SPLIT_VERTICAL;
        ShortcutSplitHorizontal = DEFAULT_SHORTCUT_SPLIT_HORIZONTAL;
        ShortcutRebalance = DEFAULT_SHORTCUT_REBALANCE;
        ShortcutMaximizeRestore = DEFAULT_SHORTCUT_MAXIMIZE_RESTORE;
        ShowContextMenu = true;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (TextClose.Equals(DEFAULT_TEXT_CLOSE) &&
                                       TextCloseAllButThis.Equals(DEFAULT_TEXT_CLOSE_ALL_BUT_THIS) &&
                                       TextMoveNext.Equals(DEFAULT_TEXT_MOVE_NEXT) &&
                                       TextMovePrevious.Equals(DEFAULT_TEXT_MOVE_PREVIOUS) &&
                                       TextSplitVertical.Equals(DEFAULT_TEXT_SPLIT_VERTICAL) &&
                                       TextSplitHorizontal.Equals(DEFAULT_TEXT_SPLIT_HORIZONTAL) &&
                                       TextRebalance.Equals(DEFAULT_TEXT_REBALANCE) &&
                                       TextMaximize.Equals(DEFAULT_TEXT_MAXIMIZE) &&
                                       TextRestore.Equals(DEFAULT_TEXT_RESTORE) &&
                                       ShortcutClose.Equals(DEFAULT_SHORTCUT_CLOSE) &&
                                       ShortcutCloseAllButThis.Equals(DEFAULT_SHORTCUT_CLOSE_ALL_BUT_THIS) &&
                                       ShortcutMoveNext.Equals(DEFAULT_SHORTCUT_MOVE_NEXT) &&
                                       ShortcutMovePrevious.Equals(DEFAULT_SHORTCUT_MOVE_PREVIOUS) &&
                                       ShortcutSplitVertical.Equals(DEFAULT_SHORTCUT_SPLIT_VERTICAL) &&
                                       ShortcutSplitHorizontal.Equals(DEFAULT_SHORTCUT_SPLIT_HORIZONTAL) &&
                                       ShortcutRebalance.Equals(DEFAULT_SHORTCUT_REBALANCE) &&
                                       ShortcutMaximizeRestore.Equals(DEFAULT_SHORTCUT_MAXIMIZE_RESTORE) &&
                                       ShowContextMenu);

    #endregion

    #region TextClose
    /// <summary>
    /// Gets and sets the text to use for the close context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the close context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("&Close")]
    [Localizable(true)]
    public string TextClose { get; set; }

    /// <summary>
    /// Resets the TextClose property to its default value.
    /// </summary>
    public void ResetTextClose() => TextClose = DEFAULT_TEXT_CLOSE;
    #endregion

    #region TextCloseAllButThis
    /// <summary>
    /// Gets and sets the text to use for the 'close all but this' context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the 'close all but this' context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Close &All But This")]
    [Localizable(true)]
    public string TextCloseAllButThis { get; set; }

    /// <summary>
    /// Resets the TextCloseAllButThis property to its default value.
    /// </summary>
    public void ResetTextCloseAllButThis() => TextCloseAllButThis = DEFAULT_TEXT_CLOSE_ALL_BUT_THIS;
    #endregion

    #region TextMoveNext
    /// <summary>
    /// Gets and sets the text to use for the move next context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the move next context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Move &Next")]
    [Localizable(true)]
    public string TextMoveNext { get; set; }

    /// <summary>
    /// Resets the TextMoveNext property to its default value.
    /// </summary>
    public void ResetTextMoveNext() => TextMoveNext = DEFAULT_TEXT_MOVE_NEXT;
    #endregion

    #region TextMovePrevious
    /// <summary>
    /// Gets and sets the text to use for the move previous context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the move previous context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Move &Previous")]
    [Localizable(true)]
    public string TextMovePrevious { get; set; }

    /// <summary>
    /// Resets the TextMovePrevious property to its default value.
    /// </summary>
    public void ResetTextMovePrevious() => TextMovePrevious = DEFAULT_TEXT_MOVE_PREVIOUS;
    #endregion

    #region TextSplitVertical
    /// <summary>
    /// Gets and sets the text to use for the split vertical context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the split vertical context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Split &Vertical")]
    [Localizable(true)]
    public string TextSplitVertical { get; set; }

    /// <summary>
    /// Resets the TextSplitVertical property to its default value.
    /// </summary>
    public void ResetTextSplitVertical() => TextSplitVertical = DEFAULT_TEXT_SPLIT_VERTICAL;
    #endregion

    #region TextSplitHorizontal
    /// <summary>
    /// Gets and sets the text to use for the split horizontal context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the split horizontal context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Split &Horizontal")]
    [Localizable(true)]
    public string TextSplitHorizontal { get; set; }

    /// <summary>
    /// Resets the TextSplitHorizontal property to its default value.
    /// </summary>
    public void ResetTextSplitHorizontal() => TextSplitHorizontal = DEFAULT_TEXT_SPLIT_HORIZONTAL;
    #endregion

    #region TextRebalance
    /// <summary>
    /// Gets and sets the text to use for the rebalance context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the rebalance context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("&Rebalance")]
    [Localizable(true)]
    public string TextRebalance { get; set; }

    /// <summary>
    /// Resets the TextRebalance property to its default value.
    /// </summary>
    public void ResetTextRebalance() => TextRebalance = DEFAULT_TEXT_REBALANCE;
    #endregion

    #region TextMaximize
    /// <summary>
    /// Gets and sets the text to use for the maximize context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the maximize context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("&Maximize")]
    [Localizable(true)]
    public string TextMaximize { get; set; }

    /// <summary>
    /// Resets the TextMaximize property to its default value.
    /// </summary>
    public void ResetTextMaximize() => TextMaximize = DEFAULT_TEXT_MAXIMIZE;
    #endregion

    #region TextRestore
    /// <summary>
    /// Gets and sets the text to use for the restore context menu item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use for the restore context menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Res&tore")]
    [Localizable(true)]
    public string TextRestore { get; set; }

    /// <summary>
    /// Resets the TextRestore property to its default value.
    /// </summary>
    public void ResetTextRestore() => TextRestore = DEFAULT_TEXT_RESTORE;
    #endregion

    #region ShortcutClose
    /// <summary>
    /// Gets and sets the shortcut for closing the current page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for closing the current page.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutClose { get; set; }

    /// <summary>
    /// Decide if the shortcut for closing the current page.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutClose() => !ShortcutClose.Equals(DEFAULT_SHORTCUT_CLOSE);

    /// <summary>
    /// Resets the ShortcutClose property to its default value.
    /// </summary>
    public void ResetShortcutClose() => ShortcutClose = DEFAULT_SHORTCUT_CLOSE;
    #endregion

    #region ShortcutCloseAllButThis
    /// <summary>
    /// Gets and sets the shortcut for 'close all but this' page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for 'close all but this' page.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutCloseAllButThis { get; set; }

    /// <summary>
    /// Decide if the shortcut for 'close all but this' page.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutCloseAllButThis() => !ShortcutCloseAllButThis.Equals(DEFAULT_SHORTCUT_CLOSE_ALL_BUT_THIS);

    /// <summary>
    /// Resets the ShortcutCloseAllButThis property to its default value.
    /// </summary>
    public void ResetShortcutCloseAllButThis() => ShortcutCloseAllButThis = DEFAULT_SHORTCUT_CLOSE_ALL_BUT_THIS;
    #endregion

    #region ShortcutMoveNext
    /// <summary>
    /// Gets and sets the shortcut for moving the current page to the next cell.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for moving the current page to the next cell.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutMoveNext { get; set; }

    /// <summary>
    /// Decide if the shortcut for moving the current page to the next cell.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutMoveNext() => !ShortcutMoveNext.Equals(DEFAULT_SHORTCUT_MOVE_NEXT);

    /// <summary>
    /// Resets the ShortcutMoveNext property to its default value.
    /// </summary>
    public void ResetShortcutMoveNext() => ShortcutMoveNext = DEFAULT_SHORTCUT_MOVE_NEXT;
    #endregion

    #region ShortcutMovePrevious
    /// <summary>
    /// Gets and sets the shortcut for moving the current page to the previous cell.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for moving the current page to the previous cell.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutMovePrevious { get; set; }

    /// <summary>
    /// Decide if the shortcut for moving the current page to the previous cell.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutMovePrevious() => !ShortcutMovePrevious.Equals(DEFAULT_SHORTCUT_MOVE_PREVIOUS);

    /// <summary>
    /// Resets the ShortcutMovePrevious property to its default value.
    /// </summary>
    public void ResetShortcutMovePrevious() => ShortcutMovePrevious = DEFAULT_SHORTCUT_MOVE_PREVIOUS;
    #endregion

    #region ShortcutSplitVertical
    /// <summary>
    /// Gets and sets the shortcut for splitting the current page into a vertical aligned page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for splitting the current page into a vertical aligned page.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutSplitVertical { get; set; }

    /// <summary>
    /// Decide if the shortcut for splitting the current page into a vertical aligned page.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutSplitVertical() => !ShortcutSplitVertical.Equals(DEFAULT_SHORTCUT_SPLIT_VERTICAL);

    /// <summary>
    /// Resets the ShortcutSplitVertical property to its default value.
    /// </summary>
    public void ResetShortcutSplitVertical() => ShortcutSplitVertical = DEFAULT_SHORTCUT_SPLIT_VERTICAL;
    #endregion

    #region ShortcutSplitHorizontal
    /// <summary>
    /// Gets and sets the shortcut for splitting the current page into a horizontal aligned page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for splitting the current page into a horizontal aligned page.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutSplitHorizontal { get; set; }

    /// <summary>
    /// Decide if the shortcut for splitting the current page into a horizontal aligned page.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutSplitHorizontal() => !ShortcutSplitHorizontal.Equals(DEFAULT_SHORTCUT_SPLIT_HORIZONTAL);

    /// <summary>
    /// Resets the ShortcutSplitHorizontal property to its default value.
    /// </summary>
    public void ResetShortcutSplitHorizontal() => ShortcutSplitHorizontal = DEFAULT_SHORTCUT_SPLIT_HORIZONTAL;
    #endregion

    #region ShortcutRebalance
    /// <summary>
    /// Gets and sets the shortcut for rebalancing the layout.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for rebalancing the layout.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutRebalance { get; set; }

    /// <summary>
    /// Decide if the shortcut for rebalancing the layout.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutRebalance() => !ShortcutRebalance.Equals(DEFAULT_SHORTCUT_REBALANCE);

    /// <summary>
    /// Resets the ShortcutRebalance property to its default value.
    /// </summary>
    public void ResetShortcutRebalance() => ShortcutRebalance = DEFAULT_SHORTCUT_REBALANCE;
    #endregion

    #region ShortcutMaximizeRestore
    /// <summary>
    /// Gets and sets the shortcut for maximizing/restoring the layout.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shortcut for maximizing/restoring the layout.")]
    [RefreshProperties(RefreshProperties.All)]
    public Keys ShortcutMaximizeRestore { get; set; }

    /// <summary>
    /// Decide if the shortcut for maximizing/restoring the layout.
    /// </summary>
    /// <returns>True if value should be serialized.</returns>
    protected bool ShouldSerializeShortcutMaximizeRestore() => !ShortcutMaximizeRestore.Equals(DEFAULT_SHORTCUT_MAXIMIZE_RESTORE);

    /// <summary>
    /// Resets the ShortcutMaximizeRestore property to its default value.
    /// </summary>
    public void ResetShortcutMaximizeRestore() => ShortcutMaximizeRestore = DEFAULT_SHORTCUT_MAXIMIZE_RESTORE;
    #endregion

    #region ShowContextMenu
    /// <summary>
    /// Gets and sets if a workspace context menu is shown on tab right clicking.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if a workspace context menu is added on tab right clicking.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public bool ShowContextMenu { get; set; }

    #endregion
}