#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonFolderBrowserDialogActionList : DesignerActionList
{
    private readonly KryptonFolderBrowserDialog _dialog;
    private readonly IComponentChangeService? _service;

    public KryptonFolderBrowserDialogActionList(KryptonFolderBrowserDialogDesigner owner)
        : base(owner.Component)
    {
        _dialog = (owner.Component as KryptonFolderBrowserDialog)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    public string Title
    {
        get => _dialog.Title;
        set => SetProperty(_dialog.Title, value, () => _dialog.Title = value);
    }

    public Icon? Icon
    {
        get => _dialog.Icon;
        set => SetProperty(_dialog.Icon, value, () => _dialog.Icon = value);
    }

    public string SelectedPath
    {
        get => _dialog.SelectedPath;
        set => SetProperty(_dialog.SelectedPath, value, () => _dialog.SelectedPath = value);
    }

    public Environment.SpecialFolder RootFolder
    {
        get => _dialog.RootFolder;
        set => SetProperty(_dialog.RootFolder, value, () => _dialog.RootFolder = value);
    }

#if NET8_0_OR_GREATER
    public string InitialDirectory
    {
        get => _dialog.InitialDirectory;
        set => SetProperty(_dialog.InitialDirectory, value, () => _dialog.InitialDirectory = value);
    }
#endif

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_dialog != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Dialog"));
            actions.Add(new DesignerActionPropertyItem(nameof(Title), nameof(Title), @"Dialog", @"Dialog title."));
            actions.Add(new DesignerActionPropertyItem(nameof(Icon), nameof(Icon), @"Dialog", @"Dialog icon."));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectedPath), @"Selected Path", @"Dialog", @"Selected or initial folder path."));
#if NET8_0_OR_GREATER
            actions.Add(new DesignerActionPropertyItem(nameof(InitialDirectory), @"Initial Directory", @"Dialog", @"Initial directory shown by the dialog."));
#endif
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(RootFolder), nameof(RootFolder), @"Behavior", @"Root folder shown by the browser."));
        }

        return actions;
    }

    private void SetProperty<T>(T oldValue, T newValue, Action setValue)
    {
        if (!Equals(oldValue, newValue))
        {
            _service?.OnComponentChanged(_dialog, null, oldValue, newValue);
            setValue();
        }
    }
}
