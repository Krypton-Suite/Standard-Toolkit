#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #3891: multiple context menu items can share one <see cref="KryptonCommand"/>.
/// </summary>
public partial class KryptonContextMenuSharedCommandDemo : KryptonForm
{
    private readonly KryptonCommand _sharedCommand;
    private readonly KryptonContextMenu _contextMenu;

    public KryptonContextMenuSharedCommandDemo()
    {
        InitializeComponent();

        _sharedCommand = new KryptonCommand();
        _sharedCommand.Execute += OnSharedCommandExecute;

        _contextMenu = BuildContextMenu();
        kbtnShowMenu.KryptonContextMenu = _contextMenu;
    }

    private KryptonContextMenu BuildContextMenu()
    {
        var menu = new KryptonContextMenu();
        var items = new KryptonContextMenuItems();
        menu.Items.Add(items);

        items.Items.Add(new KryptonContextMenuHeading("Standard items"));
        items.Items.Add(new KryptonContextMenuItem("Cut")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.Cut
        });
        items.Items.Add(new KryptonContextMenuItem("Copy")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.Copy
        });
        items.Items.Add(new KryptonContextMenuItem("Paste")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.Paste
        });

        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuHeading("Other command-backed items"));
        items.Items.Add(new KryptonContextMenuLinkLabel("Open help topic")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.Help
        });
        items.Items.Add(new KryptonContextMenuCheckButton("Toggle preview")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.TogglePreview
        });
        items.Items.Add(new KryptonContextMenuCheckBox("Enable logging")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.EnableLogging
        });
        items.Items.Add(new KryptonContextMenuRadioButton("Layout: Compact")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.LayoutCompact
        });
        items.Items.Add(new KryptonContextMenuRadioButton("Layout: Comfortable")
        {
            KryptonCommand = _sharedCommand,
            CommandParameter = MenuAction.LayoutComfortable
        });

        return menu;
    }

    private void OnSharedCommandExecute(object? sender, EventArgs e)
    {
        var sourceType = sender?.GetType().Name ?? "<unknown>";
        var parameter = e is KryptonCommandExecuteEventArgs executeArgs
            ? executeArgs.Parameter
            : KryptonCommandContext.GetCommandParameter(sender);

        klblStatus.Values.Text = $"Executed via {sourceType}; parameter = {parameter}";
    }

    private enum MenuAction
    {
        Cut,
        Copy,
        Paste,
        Help,
        TogglePreview,
        EnableLogging,
        LayoutCompact,
        LayoutComfortable
    }
}
