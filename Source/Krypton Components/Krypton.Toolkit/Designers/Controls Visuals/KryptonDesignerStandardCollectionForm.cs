#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Standard Krypton-themed collection editor dialog with members list and property grid.
/// </summary>
internal partial class KryptonDesignerStandardCollectionForm : KryptonDesignerCollectionForm
{
    #region Instance Fields
    private readonly KryptonDesignerStandardCollectionEditor _standardEditor;
    private object[]? _workingItems;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerStandardCollectionForm"/> class.
    /// </summary>
    /// <param name="editor">Owning collection editor.</param>
    public KryptonDesignerStandardCollectionForm(KryptonDesignerStandardCollectionEditor editor)
        : base(editor)
    {
        _standardEditor = editor;
        InitializeComponent();

        Text = $"{CollectionItemTypeName} Collection Editor";
        ApplyButtonSizes();
        _listBox.SelectedIndexChanged += (_, _) => UpdatePropertyGrid();
        AcceptButton = _buttonOk;
        CancelButton = _buttonCancel;
        ControlBox = false;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(640, 420));
        MinimumSize = KryptonDesignerEditorDpi.Scale(this, new Size(520, 320));
    }
    #endregion

    #region Implementation
    private string CollectionItemTypeName => _standardEditor.DesignerCollectionItemType.Name;

    private void ApplyButtonSizes()
    {
        var buttonSize = KryptonDesignerEditorDpi.Scale(this, new Size(112, 28));
        foreach (var button in new[] { _buttonAdd, _buttonRemove, _buttonOk, _buttonCancel })
        {
            button.AutoSize = false;
            button.Size = buttonSize;
            button.MinimumSize = buttonSize;
        }
    }

    /// <inheritdoc />
    protected override void OnEditValueChanged()
    {
        if (EditValue is null || Items is null)
        {
            return;
        }

        _workingItems = (object[])Items.Clone();
        RefreshList();
        _propertyGrid.Site = new KryptonDesignerPropertyGridSite(Context, _propertyGrid);
        ApplyOwnerPaletteFromContext();
        UpdateButtons();
        UpdatePropertyGrid();
        _listBox.Focus();
    }

    private void RefreshList()
    {
        _listBox.Items.Clear();
        if (_workingItems is null)
        {
            return;
        }

        foreach (var item in _workingItems)
        {
            _listBox.Items.Add(new CollectionListItem(item, _standardEditor.GetDesignerDisplayText(item)));
        }

        if (_listBox.Items.Count > 0 && _listBox.SelectedIndex < 0)
        {
            _listBox.SelectedIndex = 0;
        }
    }

    private void UpdatePropertyGrid()
    {
        if (_listBox.SelectedItem is CollectionListItem entry)
        {
            _propertiesLabel.Values.Text = string.Format(
                CultureInfo.CurrentCulture,
                @"{0} properties",
                _standardEditor.GetDesignerDisplayText(entry.Item));
            _propertyGrid.SelectedObject = entry.Item;
        }
        else
        {
            _propertiesLabel.Values.Text = @"Properties:";
            _propertyGrid.SelectedObject = null;
        }
    }

    private void UpdateButtons()
    {
        var hasSelection = _listBox.SelectedItem is not null;
        _buttonRemove.Enabled = hasSelection;
        _buttonAdd.Enabled = _standardEditor.GetDesignerNewItemTypes().Length > 0;
    }

    private void OnAddClick(object? sender, EventArgs e)
    {
        var itemTypes = _standardEditor.GetDesignerNewItemTypes();
        if (itemTypes.Length == 0)
        {
            return;
        }

        var itemType = itemTypes.Length == 1 ? itemTypes[0] : PromptNewItemType(itemTypes);
        if (itemType is null)
        {
            return;
        }

        var item = CreateInstance(itemType);
        _workingItems = AppendItem(_workingItems, item);
        Items = _workingItems;
        RefreshList();
        _listBox.SelectedIndex = _listBox.Items.Count - 1;
        UpdateButtons();
        UpdatePropertyGrid();
    }

    private void OnRemoveClick(object? sender, EventArgs e)
    {
        if (_listBox.SelectedItem is not CollectionListItem entry || _workingItems is null)
        {
            return;
        }

        _standardEditor.OnDesignerItemRemoving(entry.Item);
        _workingItems = RemoveItem(_workingItems, entry.Item);
        DestroyInstance(entry.Item);
        Items = _workingItems;
        RefreshList();
        UpdateButtons();
        UpdatePropertyGrid();
    }

    private void OnOkClick(object? sender, EventArgs e)
    {
        Items = _workingItems;
        CommitDesignerItems();
        Context?.OnComponentChanged();
    }

    private Type? PromptNewItemType(IReadOnlyList<Type> itemTypes)
    {
        using var form = new KryptonForm
        {
            ControlBox = false,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            Text = @"Select item type",
            ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(320, 280))
        };
        KryptonDesignerEditorTheme.ApplyFromContext(form, Context);

        var listBox = new KryptonListBox { Dock = DockStyle.Fill };
        foreach (var itemType in itemTypes)
        {
            listBox.Items.Add(itemType);
        }

        if (listBox.Items.Count > 0)
        {
            listBox.SelectedIndex = 0;
        }

        var okButton = new KryptonButton
        {
            DialogResult = DialogResult.OK,
            Values = { Text = KryptonManager.Strings.GeneralStrings.OK }
        };
        form.Controls.Add(listBox);
        form.Controls.Add(KryptonDesignerEditorButtonBar.Create(form, okButton));
        form.AcceptButton = okButton;

        return form.ShowDialog(this) == DialogResult.OK && listBox.SelectedItem is Type selected
            ? selected
            : null;
    }

    private static object[] AppendItem(object[]? items, object item)
    {
        if (items is null || items.Length == 0)
        {
            return [item];
        }

        var result = new object[items.Length + 1];
        Array.Copy(items, result, items.Length);
        result[items.Length] = item;
        return result;
    }

    private static object[] RemoveItem(object[] items, object item)
    {
        var index = Array.IndexOf(items, item);
        if (index < 0)
        {
            return items;
        }

        var result = new object[items.Length - 1];
        if (index > 0)
        {
            Array.Copy(items, 0, result, 0, index);
        }

        if (index < items.Length - 1)
        {
            Array.Copy(items, index + 1, result, index, items.Length - index - 1);
        }

        return result;
    }

    private sealed class CollectionListItem
    {
        public CollectionListItem(object item, string text)
        {
            Item = item;
            Text = text;
        }

        public object Item { get; }

        public string Text { get; }

        public override string ToString() => Text;
    }
    #endregion
}
