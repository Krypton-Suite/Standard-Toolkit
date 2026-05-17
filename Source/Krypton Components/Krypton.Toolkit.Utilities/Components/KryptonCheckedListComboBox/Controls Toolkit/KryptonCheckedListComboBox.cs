#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides a ComboBox-style control whose drop-down hosts a <see cref="KryptonCheckedListBox"/> for
/// multi-select with check boxes. Built on <see cref="KryptonComboBoxUserControl"/>.
/// </summary>
/// <remarks>
/// The editor shows a summary of checked items (comma-separated by default). The drop-down stays
/// open while the user checks or unchecks items; press Enter (when <see cref="CloseDropDownOnEnter"/>
/// is <see langword="true"/>) or click outside to close.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCheckedListBox), "ToolboxBitmaps.KryptonCheckedListBox.bmp")]
[DefaultEvent(nameof(ItemCheck))]
[DefaultProperty(nameof(Items))]
[Designer(typeof(KryptonCheckedListComboBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"A ComboBox-style control whose drop-down hosts a checked list for multi-select.")]
public class KryptonCheckedListComboBox : KryptonComboBoxUserControl
{
    #region Static Fields

    private const int DefaultDropDownWidth = 260;
    private const int DefaultDropDownHeight = 200;
    private const string DefaultValueSeparator = @", ";

    #endregion

    #region Instance Fields

    private readonly KryptonCheckedListComboBoxDropDown _dropDown;
    private string _valueSeparator = DefaultValueSeparator;
    private string _emptySelectionText = string.Empty;
    private bool _closeDropDownOnEnter = true;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the checked items summary in the editor is updated.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the checked items summary in the editor is updated.")]
    public event EventHandler? CheckedItemsChanged;

    /// <summary>
    /// Occurs when the checked state of an item is about to change. Forwards the inner
    /// <see cref="KryptonCheckedListBox.ItemCheck"/> event.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the checked state of an item is about to change.")]
    public event ItemCheckEventHandler? ItemCheck;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCheckedListComboBox"/> class.
    /// </summary>
    public KryptonCheckedListComboBox()
    {
        ReadOnlyEditor = true;
        DropDownResizable = true;
        DropDownWidth = DefaultDropDownWidth;
        DropDownHeight = DefaultDropDownHeight;

        _dropDown = new KryptonCheckedListComboBoxDropDown(this);
        _dropDown.ItemCheck += OnInnerListItemCheck;
        base.DropContent = _dropDown;

        ValueCommitted += OnDropDownValueCommitted;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ValueCommitted -= OnDropDownValueCommitted;
            _dropDown.ItemCheck -= OnInnerListItemCheck;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the collection of items in the drop-down list.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of items in the drop-down list.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    public ListBox.ObjectCollection Items => _dropDown.Items;

    /// <summary>
    /// Gets the collection of checked items in the drop-down, including indeterminate items.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonCheckedListBox.CheckedItemCollection CheckedItems => _dropDown.CheckedItems;

    /// <summary>
    /// Gets a collection of indexes of currently checked items.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonCheckedListBox.CheckedIndexCollection CheckedIndices => _dropDown.CheckedIndices;

    /// <summary>
    /// Gets the hosted <see cref="KryptonCheckedListBox"/>. Do not reparent this control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonCheckedListBox CheckedListBox => _dropDown;

    /// <summary>
    /// Gets or sets the separator placed between checked item texts in the editor summary.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Separator placed between checked item texts in the editor summary.")]
    [DefaultValue(DefaultValueSeparator)]
    public string ValueSeparator
    {
        get => _valueSeparator;
        set => _valueSeparator = value ?? DefaultValueSeparator;
    }

    /// <summary>
    /// Gets or sets the editor text when no items are checked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Editor text when no items are checked.")]
    [DefaultValue("")]
    public string EmptySelectionText
    {
        get => _emptySelectionText;
        set => _emptySelectionText = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets a value indicating whether pressing Enter in the drop-down closes the popup.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, pressing Enter in the drop-down closes the popup.")]
    [DefaultValue(true)]
    public bool CloseDropDownOnEnter
    {
        get => _closeDropDownOnEnter;
        set => _closeDropDownOnEnter = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether an item toggles its checked state when clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether an item toggles its checked state when clicked.")]
    [DefaultValue(true)]
    public bool CheckOnClick
    {
        get => _dropDown.CheckOnClick;
        set => _dropDown.CheckOnClick = value;
    }

    /// <summary>
    /// Hidden. The checked list drop-down is fixed for this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control? DropContent
    {
        get => base.DropContent;
        set => base.DropContent = value;
    }

    /// <summary>
    /// Hidden. Filter-as-you-type is not supported for checked list combo boxes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public new bool AutoOpenOnType
    {
        get => base.AutoOpenOnType;
        set => base.AutoOpenOnType = value;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns whether the item at the specified index is checked.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <returns><see langword="true"/> if the item is checked.</returns>
    public bool GetItemChecked(int index) => _dropDown.GetItemChecked(index);

    /// <summary>
    /// Sets whether the item at the specified index is checked.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <param name="value">The checked state to use.</param>
    public void SetItemChecked(int index, bool value) => _dropDown.SetItemChecked(index, value);

    /// <summary>
    /// Returns the check state of the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <returns>The check state of the item.</returns>
    public CheckState GetItemCheckState(int index) => _dropDown.GetItemCheckState(index);

    /// <summary>
    /// Sets the check state of the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <param name="value">The check state to use.</param>
    public void SetItemCheckState(int index, CheckState value) => _dropDown.SetItemCheckState(index, value);

    /// <summary>
    /// Unchecks all items in the list.
    /// </summary>
    public void ClearChecked() => _dropDown.ClearChecked();

    /// <summary>
    /// Builds an array of the currently checked item values.
    /// </summary>
    /// <returns>An array of checked item objects.</returns>
    public object[] GetCheckedValues() => BuildCheckedValue();

    /// <summary>
    /// Formats the currently checked items for display in the editor.
    /// </summary>
    /// <returns>Comma-separated (or custom-separated) summary text.</returns>
    public string FormatCheckedItemsDisplay()
    {
        var parts = new List<string>();
        foreach (object? item in CheckedItems)
        {
            if (item != null)
            {
                parts.Add(item.ToString() ?? string.Empty);
            }
        }

        return parts.Count == 0 ? _emptySelectionText : string.Join(_valueSeparator, parts);
    }

    /// <summary>
    /// Refreshes the editor summary from the current checked items.
    /// </summary>
    public void RefreshCheckedSummary()
    {
        if (IsDroppedDown)
        {
            _dropDown.PublishSelection(keepDropDownOpen: true);
            return;
        }

        Text = FormatCheckedItemsDisplay();
        OnCheckedItemsChanged(EventArgs.Empty);
    }

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="CheckedItemsChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnCheckedItemsChanged(EventArgs e) => CheckedItemsChanged?.Invoke(this, e);

    #endregion

    #region Internal

    /// <summary>
    /// Builds the value object committed to <see cref="KryptonComboBoxUserControl.SelectedValue"/>.
    /// </summary>
    internal object[] BuildCheckedValue()
    {
        var values = new List<object>();
        foreach (object? item in CheckedItems)
        {
            if (item != null)
            {
                values.Add(item);
            }
        }

        return values.ToArray();
    }

    #endregion

    #region Implementation

    private void OnDropDownValueCommitted(object? sender, KryptonDropDownCommitEventArgs e) =>
        OnCheckedItemsChanged(EventArgs.Empty);

    private void OnInnerListItemCheck(object? sender, ItemCheckEventArgs e) => ItemCheck?.Invoke(this, e);

    #endregion
}
