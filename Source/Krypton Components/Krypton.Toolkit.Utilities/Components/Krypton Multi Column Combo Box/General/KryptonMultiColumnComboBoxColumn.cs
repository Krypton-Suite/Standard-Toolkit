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
/// Describes a text column shown in the drop-down of a <see cref="KryptonMultiColumnComboBox"/>.
/// Mapped onto a <see cref="DataGridViewTextBoxColumn"/> when the drop-down is built.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonMultiColumnComboBoxColumn
{
    #region Static Fields

    private const int DefaultWidth = 100;

    #endregion

    #region Instance Fields

    private string _name = string.Empty;
    private string _headerText = string.Empty;
    private string _dataPropertyName = string.Empty;
    private int _width = DefaultWidth;
    private bool _visible = true;
    private DataGridViewContentAlignment _alignment = DataGridViewContentAlignment.NotSet;
    private string _format = string.Empty;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMultiColumnComboBoxColumn"/> class.
    /// </summary>
    public KryptonMultiColumnComboBoxColumn()
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMultiColumnComboBoxColumn"/> class.
    /// </summary>
    /// <param name="dataPropertyName">Bound property or column name.</param>
    /// <param name="headerText">Header caption. Empty uses <paramref name="dataPropertyName"/>.</param>
    /// <param name="width">Column width in pixels when auto-size mode is <see cref="DataGridViewAutoSizeColumnsMode.None"/>.</param>
    public KryptonMultiColumnComboBoxColumn(string dataPropertyName, string headerText, int width)
    {
        _dataPropertyName = dataPropertyName ?? string.Empty;
        _headerText = headerText ?? string.Empty;
        _name = _dataPropertyName;
        _width = Math.Max(1, width);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the column name used as the <see cref="DataGridViewColumn.Name"/>.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Name of the drop-down column.")]
    [DefaultValue("")]
    public string Name
    {
        get => _name;
        set
        {
            string normalized = value ?? string.Empty;
            if (_name == normalized)
            {
                return;
            }

            _name = normalized;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the header caption.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Header text shown in the drop-down.")]
    [DefaultValue("")]
    public string HeaderText
    {
        get => _headerText;
        set
        {
            string normalized = value ?? string.Empty;
            if (_headerText == normalized)
            {
                return;
            }

            _headerText = normalized;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the property or data-column name bound to this column.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Property or data-column name bound to this column.")]
    [DefaultValue("")]
    public string DataPropertyName
    {
        get => _dataPropertyName;
        set
        {
            string normalized = value ?? string.Empty;
            if (_dataPropertyName == normalized)
            {
                return;
            }

            _dataPropertyName = normalized;
            if (string.IsNullOrEmpty(_name))
            {
                _name = normalized;
            }

            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the column width in pixels.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Column width in pixels when the grid is not filling remaining space.")]
    [DefaultValue(DefaultWidth)]
    public int Width
    {
        get => _width;
        set
        {
            int normalized = Math.Max(1, value);
            if (_width == normalized)
            {
                return;
            }

            _width = normalized;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Whether the column is visible in the drop-down.")]
    [DefaultValue(true)]
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
            {
                return;
            }

            _visible = value;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the cell content alignment.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Alignment of cell content in this column.")]
    [DefaultValue(DataGridViewContentAlignment.NotSet)]
    public DataGridViewContentAlignment Alignment
    {
        get => _alignment;
        set
        {
            if (_alignment == value)
            {
                return;
            }

            _alignment = value;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the format string applied to cell values (for example <c>C2</c> or <c>d</c>).
    /// </summary>
    [Category(@"Data")]
    [Description(@"Format string applied to cell values.")]
    [DefaultValue("")]
    public string Format
    {
        get => _format;
        set
        {
            string normalized = value ?? string.Empty;
            if (_format == normalized)
            {
                return;
            }

            _format = normalized;
            NotifyOwner();
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Gets or sets the owning combo so property edits can rebuild the drop-down columns.
    /// </summary>
    internal KryptonMultiColumnComboBox? Owner { get; set; }

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override string ToString()
    {
        if (!string.IsNullOrEmpty(_headerText))
        {
            return _headerText;
        }

        if (!string.IsNullOrEmpty(_dataPropertyName))
        {
            return _dataPropertyName;
        }

        return string.IsNullOrEmpty(_name) ? nameof(KryptonMultiColumnComboBoxColumn) : _name;
    }

    #endregion

    #region Implementation

    private void NotifyOwner() => Owner?.NotifyColumnChanged();

    #endregion
}
