#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Compact combo box on a <see cref="KryptonMiniToolbar"/>, typically used for font family or size.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(SelectedIndexChanged))]
public class KryptonMiniToolbarComboBox : KryptonMiniToolbarItemBase
{
    #region Instance Fields

    private int _width;
    private ComboBoxStyle _dropDownStyle;
    private readonly List<object> _items;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the selected index changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selected combo box item changes.")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the combo box text changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the combo box text changes.")]
    public event EventHandler? TextChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarComboBox"/> class.
    /// </summary>
    public KryptonMiniToolbarComboBox()
    {
        _width = 108;
        _dropDownStyle = ComboBoxStyle.DropDownList;
        _items = [];
        SelectedIndex = -1;
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Text) ? "(ComboBox)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets the combo box items. Edit in the designer (one string per line) or add at runtime.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the combo box.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonDesignerStringCollectionEditor), typeof(UITypeEditor))]
    [Localizable(true)]
    [MergableProperty(false)]
    public List<object> Items => _items;

    private bool ShouldSerializeItems() => _items.Count > 0;

    /// <summary>
    /// Clears designer-serialized combo box items.
    /// </summary>
    public void ResetItems() => _items.Clear();

    /// <summary>
    /// Gets or sets the display width of the combo box.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Display width of the combo box in pixels.")]
    [DefaultValue(108)]
    public int Width
    {
        get => _width;
        set
        {
            value = Math.Max(40, value);
            if (_width != value)
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
    }

    /// <summary>
    /// Gets or sets the combo box drop-down style.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Controls whether the combo box is a drop-down list or editable.")]
    [DefaultValue(ComboBoxStyle.DropDownList)]
    public ComboBoxStyle DropDownStyle
    {
        get => _dropDownStyle;
        set
        {
            if (_dropDownStyle != value)
            {
                _dropDownStyle = value;
                OnPropertyChanged(nameof(DropDownStyle));
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected index.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex { get; set; }

    /// <summary>
    /// Gets or sets the selected item.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem { get; set; }

    /// <summary>
    /// Raises <see cref="SelectedIndexChanged"/>.
    /// </summary>
    internal void RaiseSelectedIndexChanged() => SelectedIndexChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Raises <see cref="TextChanged"/>.
    /// </summary>
    internal void RaiseTextChanged() => TextChanged?.Invoke(this, EventArgs.Empty);

    #endregion
}
