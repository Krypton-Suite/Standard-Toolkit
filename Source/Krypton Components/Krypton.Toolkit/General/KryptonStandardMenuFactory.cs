#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared File / Edit / Tools / Help menu trees used by
/// <see cref="KryptonMenuBar.InsertStandardItems"/> and
/// <see cref="KryptonFormTitleBar.InsertStandardItems"/>.
/// Text comes from <see cref="KryptonManager.Strings"/>.
/// </summary>
public static class KryptonStandardMenuFactory
{
    #region Public

    /// <summary>
    /// Creates the standard File items group (New, Open, Save, Print, Exit).
    /// </summary>
    /// <returns>A <see cref="KryptonContextMenuItems"/> collection.</returns>
    public static KryptonContextMenuItems CreateFileItems()
    {
        var tb = KryptonManager.Strings.ToolBarStrings;
        var fb = KryptonManager.Strings.TitleBarStrings;
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(tb.NewMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.OpenMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.SaveMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.SaveAsMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.SaveAllMenuItem));
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuItem(tb.PrintMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.PrintPreviewMenuItem));
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuItem(fb.Exit));
        return items;
    }

    /// <summary>
    /// Creates the standard Edit items group (Undo, Redo, Cut, Copy, Paste, Select All).
    /// </summary>
    /// <returns>A <see cref="KryptonContextMenuItems"/> collection.</returns>
    public static KryptonContextMenuItems CreateEditItems()
    {
        var tb = KryptonManager.Strings.ToolBarStrings;
        var fb = KryptonManager.Strings.TitleBarStrings;
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(tb.UndoMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.RedoMenuItem));
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuItem(tb.CutMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.CopyMenuItem));
        items.Items.Add(new KryptonContextMenuItem(tb.PasteMenuItem));
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuItem(fb.SelectAll));
        return items;
    }

    /// <summary>
    /// Creates the standard Tools items group (Customize, Options).
    /// </summary>
    /// <returns>A <see cref="KryptonContextMenuItems"/> collection.</returns>
    public static KryptonContextMenuItems CreateToolsItems()
    {
        var fb = KryptonManager.Strings.TitleBarStrings;
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(fb.Customize));
        items.Items.Add(new KryptonContextMenuItem(fb.Options));
        return items;
    }

    /// <summary>
    /// Creates the standard Help items group (Contents, Index, About).
    /// </summary>
    /// <returns>A <see cref="KryptonContextMenuItems"/> collection.</returns>
    public static KryptonContextMenuItems CreateHelpItems()
    {
        var fb = KryptonManager.Strings.TitleBarStrings;
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(fb.Contents));
        items.Items.Add(new KryptonContextMenuItem(fb.Index));
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuItem(fb.About));
        return items;
    }

    /// <summary>
    /// Creates a <see cref="KryptonContextMenu"/> containing the standard File items.
    /// </summary>
    /// <returns>A context menu ready to assign to a drop-down host.</returns>
    public static KryptonContextMenu CreateFileContextMenu() => Wrap(CreateFileItems());

    /// <summary>
    /// Creates a <see cref="KryptonContextMenu"/> containing the standard Edit items.
    /// </summary>
    /// <returns>A context menu ready to assign to a drop-down host.</returns>
    public static KryptonContextMenu CreateEditContextMenu() => Wrap(CreateEditItems());

    /// <summary>
    /// Creates a <see cref="KryptonContextMenu"/> containing the standard Tools items.
    /// </summary>
    /// <returns>A context menu ready to assign to a drop-down host.</returns>
    public static KryptonContextMenu CreateToolsContextMenu() => Wrap(CreateToolsItems());

    /// <summary>
    /// Creates a <see cref="KryptonContextMenu"/> containing the standard Help items.
    /// </summary>
    /// <returns>A context menu ready to assign to a drop-down host.</returns>
    public static KryptonContextMenu CreateHelpContextMenu() => Wrap(CreateHelpItems());

    /// <summary>
    /// Creates the four standard top-level menu bar items (File, Edit, Tools, Help)
    /// with nested drop-down collections.
    /// </summary>
    /// <returns>Top-level items suitable for <see cref="KryptonMenuBar.Items"/>.</returns>
    public static KryptonContextMenuItem[] CreateStandardMenuBarItems()
    {
        var fb = KryptonManager.Strings.TitleBarStrings;
        return
        [
            CreateTopLevel(fb.File, CreateFileItems()),
            CreateTopLevel(fb.Edit, CreateEditItems()),
            CreateTopLevel(fb.Tools, CreateToolsItems()),
            CreateTopLevel(fb.Help, CreateHelpItems())
        ];
    }

    #endregion

    #region Implementation

    private static KryptonContextMenu Wrap(KryptonContextMenuItems items)
    {
        var menu = new KryptonContextMenu();
        menu.Items.Add(items);
        return menu;
    }

    private static KryptonContextMenuItem CreateTopLevel(string text, KryptonContextMenuItems items)
    {
        var item = new KryptonContextMenuItem(text);
        item.Items.Add(items);
        return item;
    }

    #endregion
}
