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
/// Base class for Krypton-themed standard list/property-grid collection editors.
/// </summary>
public abstract class KryptonDesignerStandardCollectionEditor : KryptonDesignerCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerStandardCollectionEditor"/> class.
    /// </summary>
    /// <param name="collectionType">Collection item type.</param>
    protected KryptonDesignerStandardCollectionEditor(Type collectionType)
        : base(collectionType)
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override KryptonDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        new KryptonDesignerStandardCollectionForm(this);

    /// <summary>
    /// Allows specialized editors to handle item removal before the item is destroyed.
    /// </summary>
    /// <param name="item">Item being removed.</param>
    internal virtual void OnDesignerItemRemoving(object? item)
    {
    }
    #endregion
}

/// <summary>
/// Standard Krypton-themed collection editor dialog with members list and property grid.
/// </summary>
internal sealed class KryptonDesignerStandardCollectionForm : KryptonDesignerCollectionForm
{
    #region Instance Fields
    private readonly KryptonDesignerStandardCollectionEditor _standardEditor;
    private readonly KryptonListBox _listBox;
    private readonly KryptonPropertyGrid _propertyGrid;
    private readonly KryptonButton _buttonAdd;
    private readonly KryptonButton _buttonRemove;
    private readonly KryptonButton _buttonOk;
    private readonly KryptonButton _buttonCancel;
    private readonly KryptonLabel _membersLabel;
    private readonly KryptonLabel _propertiesLabel;
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
        Text = $"{CollectionItemTypeName} Collection Editor";

        _membersLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Members:" } };
        _propertiesLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Properties:" } };
        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _propertyGrid = new KryptonPropertyGrid { Dock = DockStyle.Fill };
        _buttonAdd = CreateButton(@"Add", OnAddClick);
        _buttonRemove = CreateButton(@"Remove", OnRemoveClick);
        _buttonOk = CreateButton(KryptonManager.Strings.GeneralStrings.OK, OnOkClick);
        _buttonOk.DialogResult = DialogResult.OK;
        _buttonCancel = CreateButton(KryptonManager.Strings.GeneralStrings.Cancel, null);
        _buttonCancel.DialogResult = DialogResult.Cancel;

        InitializeLayout();

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

    private static KryptonButton CreateButton(string text, EventHandler? click)
    {
        var button = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Values = { Text = text }
        };

        if (click is not null)
        {
            button.Click += click;
        }

        return button;
    }

    private void InitializeLayout()
    {
        var membersPanel = new KryptonPanel { Dock = DockStyle.Fill };
        membersPanel.Controls.Add(_listBox);
        membersPanel.Controls.Add(_membersLabel);
        _membersLabel.Dock = DockStyle.Top;

        var propertiesPanel = new KryptonPanel { Dock = DockStyle.Fill };
        propertiesPanel.Controls.Add(_propertyGrid);
        propertiesPanel.Controls.Add(_propertiesLabel);
        _propertiesLabel.Dock = DockStyle.Top;

        var buttonPanel = new TableLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 2,
            Dock = DockStyle.Bottom
        };
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        buttonPanel.Controls.Add(_buttonAdd, 0, 0);
        buttonPanel.Controls.Add(_buttonRemove, 1, 0);

        var content = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill
        };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        content.RowStyles.Add(new RowStyle());
        content.Controls.Add(membersPanel, 0, 0);
        content.Controls.Add(propertiesPanel, 1, 0);
        content.Controls.Add(buttonPanel, 0, 1);

        var buttonBar = KryptonDesignerEditorButtonBar.Create(this, _buttonOk, _buttonCancel);
        Controls.Add(KryptonDesignerEditorContentPanel.Create(this, content));
        Controls.Add(buttonBar);
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
        ApplyOwnerPaletteFromContext();

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

/// <summary>
/// Minimal site wrapper so the property grid can access designer services.
/// </summary>
internal sealed class KryptonDesignerPropertyGridSite : ISite, IServiceProvider
{
    #region Instance Fields
    private readonly ITypeDescriptorContext? _context;
    private readonly IComponent _component;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerPropertyGridSite"/> class.
    /// </summary>
    /// <param name="context">Designer context.</param>
    /// <param name="component">Component shown in the property grid.</param>
    public KryptonDesignerPropertyGridSite(ITypeDescriptorContext? context, IComponent component)
    {
        _context = context;
        _component = component;
    }
    #endregion

    #region ISite
    /// <inheritdoc />
    public IComponent Component => _component;

    /// <inheritdoc />
    public IContainer? Container => null;

    /// <inheritdoc />
    public bool DesignMode => true;

    /// <inheritdoc />
    public string? Name { get; set; }
    #endregion

    #region IServiceProvider
    /// <inheritdoc />
    public object? GetService(Type serviceType) => _context?.GetService(serviceType);
    #endregion
}
