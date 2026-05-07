#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonSaveFileDialogActionList : DesignerActionList
{
    private readonly KryptonSaveFileDialog _dialog;
    private readonly IComponentChangeService? _service;

    public KryptonSaveFileDialogActionList(KryptonSaveFileDialogDesigner owner)
        : base(owner.Component)
    {
        _dialog = (owner.Component as KryptonSaveFileDialog)!;
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

    public string FileName
    {
        get => _dialog.FileName;
        set => SetProperty(_dialog.FileName, value, () => _dialog.FileName = value);
    }

    public string Filter
    {
        get => _dialog.Filter;
        set => SetProperty(_dialog.Filter, value, () => _dialog.Filter = value);
    }

    public string InitialDirectory
    {
        get => _dialog.InitialDirectory;
        set => SetProperty(_dialog.InitialDirectory, value, () => _dialog.InitialDirectory = value);
    }

    public bool CreatePrompt
    {
        get => _dialog.CreatePrompt;
        set => SetProperty(_dialog.CreatePrompt, value, () => _dialog.CreatePrompt = value);
    }

    public bool OverwritePrompt
    {
        get => _dialog.OverwritePrompt;
        set => SetProperty(_dialog.OverwritePrompt, value, () => _dialog.OverwritePrompt = value);
    }

    public bool ValidateNames
    {
        get => _dialog.ValidateNames;
        set => SetProperty(_dialog.ValidateNames, value, () => _dialog.ValidateNames = value);
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_dialog != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Dialog"));
            actions.Add(new DesignerActionPropertyItem(nameof(Title), nameof(Title), @"Dialog", @"Dialog title."));
            actions.Add(new DesignerActionPropertyItem(nameof(Icon), nameof(Icon), @"Dialog", @"Dialog icon."));
            actions.Add(new DesignerActionPropertyItem(nameof(FileName), nameof(FileName), @"Dialog", @"Selected file name."));
            actions.Add(new DesignerActionPropertyItem(nameof(Filter), nameof(Filter), @"Dialog", @"Filter options."));
            actions.Add(new DesignerActionPropertyItem(nameof(InitialDirectory), @"Initial Directory", @"Dialog", @"Initial directory shown by the dialog."));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(CreatePrompt), nameof(CreatePrompt), @"Behavior", @"Prompt before creating a non-existent file."));
            actions.Add(new DesignerActionPropertyItem(nameof(OverwritePrompt), nameof(OverwritePrompt), @"Behavior", @"Prompt before overwriting existing file."));
            actions.Add(new DesignerActionPropertyItem(nameof(ValidateNames), nameof(ValidateNames), @"Behavior", @"Allow only valid file names."));
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
