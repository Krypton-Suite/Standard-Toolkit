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
/// Krypton-themed Column and Row Styles editor for a <see cref="TableLayoutPanel"/>.
/// </summary>
/// <remarks>
/// The layout follows the stock Column and Row Styles dialog: a member grid
/// (Member, Size Type, Value) and a Size Type group with Absolute, Percent, and AutoSize.
/// Changes are applied to the live panel. Cancel writes back the styles, counts, and cell
/// positions captured when the dialog opened.
/// </remarks>
internal sealed class VisualTableLayoutStyleEditorForm : KryptonForm
{
    #region Instance Fields

    private const string RowSpanHelpUrl = @"https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutpanel.setrowspan";
    private const string ColumnSpanHelpUrl = @"https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutpanel.setcolumnspan";
    private const string AnchorHelpUrl = @"https://learn.microsoft.com/dotnet/api/system.windows.forms.control.anchor";

    private readonly TableLayoutPanel _panel;
    private readonly List<KryptonTableLayoutStyleEdit.StyleValue> _originalColumns;
    private readonly List<KryptonTableLayoutStyleEdit.StyleValue> _originalRows;
    private readonly KryptonTableLayoutStyleEdit.CellValue[] _originalCells;
    private readonly KryptonComboBox _showCombo;
    private readonly KryptonListView _members;
    private readonly KryptonButton _addButton;
    private readonly KryptonButton _insertButton;
    private readonly KryptonButton _deleteButton;
    private readonly KryptonRadioButton _absoluteRadio;
    private readonly KryptonRadioButton _percentRadio;
    private readonly KryptonRadioButton _autoSizeRadio;
    private readonly KryptonNumericUpDown _absoluteValue;
    private readonly KryptonNumericUpDown _percentValue;
    private bool _editingColumns;
    private bool _suspend;
    private bool _restored;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualTableLayoutStyleEditorForm"/> class.
    /// </summary>
    /// <param name="panel">Panel whose column and row styles are edited.</param>
    /// <param name="editColumns"><see langword="true"/> to open on columns; <see langword="false"/> to open on rows.</param>
    internal VisualTableLayoutStyleEditorForm(TableLayoutPanel panel, bool editColumns)
    {
        SetInheritedControlOverride();
        _panel = panel ?? throw new ArgumentNullException(nameof(panel));
        _editingColumns = editColumns;
        _originalColumns = KryptonTableLayoutStyleEdit.Read(panel, columns: true);
        _originalRows = KryptonTableLayoutStyleEdit.Read(panel, columns: false);
        _originalCells = KryptonTableLayoutStyleEdit.ReadCells(panel);

        Text = @"Column and Row Styles";
        MaximizeBox = false;
        MinimizeBox = false;
        ShowIcon = false;
        ShowInTaskbar = SharedStaticVariables.SHOW_COLLECTION_EDITORS_IN_TASKBAR;
        StartPosition = FormStartPosition.CenterScreen;
        AutoScaleMode = AutoScaleMode.Font;
        AutoScaleDimensions = new SizeF(6F, 13F);
        ClientSize = new Size(860, 640);
        MinimumSize = SizeFromClientSize(ClientSize);
        Font = new Font(@"Segoe UI", 9F);

        _showCombo = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            Margin = new Padding(8, 4, 0, 0)
        };
        _showCombo.Items.AddRange(new object[] { @"Columns", @"Rows" });
        _members = new KryptonListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            HideSelection = false,
            MultiSelect = false,
            HeaderStyle = ColumnHeaderStyle.Nonclickable
        };
        _members.Columns.Add(@"Member", 140);
        _members.Columns.Add(@"Size Type", 90);
        _members.Columns.Add(@"Value", -2);
        _addButton = CreateButton(@"Add");
        _deleteButton = CreateButton(@"Delete");
        _insertButton = CreateButton(@"Insert");
        _absoluteRadio = CreateRadio(@"Absolute");
        _percentRadio = CreateRadio(@"Percent");
        _autoSizeRadio = CreateRadio(@"AutoSize");
        _absoluteValue = CreateSizeBox(decimalPlaces: 0);
        _percentValue = CreateSizeBox(decimalPlaces: 2);
        _absoluteValue.Value = (decimal)KryptonTableLayoutStyleEdit.NewStyleSize;
        _percentValue.Value = 100;

        Control content = BuildContent();
        KryptonPanel host = InternalDesignerEditorFormChrome.CreateContentHost(content);
        var buttonBar = new InternalDesignerEditorButtonBarPanel { Dock = DockStyle.Bottom };
        Controls.Add(host);
        Controls.Add(buttonBar);
        InternalDesignerEditorFormChrome.Apply(this, host, buttonBar);
        buttonBar.OkButton.Values.Text = KryptonManager.Strings.GeneralStrings.OK;
        buttonBar.CancelButton.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        KryptonDialogLayout.EnableResizable(this);

        _showCombo.SelectedIndex = editColumns ? 0 : 1;
        _showCombo.SelectedIndexChanged += OnShowChanged;
        _members.SelectedIndexChanged += (_, _) => LoadSelectedStyle();
        _addButton.Click += (_, _) => AddStyle();
        _deleteButton.Click += (_, _) => DeleteStyle();
        _insertButton.Click += (_, _) => InsertStyle();
        _absoluteRadio.CheckedChanged += (_, _) => ApplySelectedStyle();
        _percentRadio.CheckedChanged += (_, _) => ApplySelectedStyle();
        _autoSizeRadio.CheckedChanged += (_, _) => ApplySelectedStyle();
        _absoluteValue.ValueChanged += (_, _) => ApplySelectedStyle();
        _percentValue.ValueChanged += (_, _) => ApplySelectedStyle();

        RefreshMembers(0);
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        // Font autoscaling is set up front. Configure() would switch to DPI mode and collapse the form.
        KryptonDesignerEditorDpi.ApplyOnShown(this);
    }

    /// <inheritdoc />
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK)
        {
            RestoreOriginal();
        }

        base.OnFormClosing(e);
    }

    #endregion

    #region Implementation

    private Control BuildContent()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            Padding = new Padding(12),
            BackColor = Color.Transparent
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));

        var showHost = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.Transparent
        };
        showHost.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        showHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        showHost.Controls.Add(new KryptonLabel
        {
            Text = @"Show:",
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 6, 0, 0)
        }, 0, 0);
        showHost.Controls.Add(_showCombo, 1, 0);
        root.Controls.Add(showHost, 0, 0);
        root.Controls.Add(_members, 0, 1);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0, 10, 0, 0),
            BackColor = Color.Transparent
        };
        buttons.Controls.Add(_addButton);
        buttons.Controls.Add(_deleteButton);
        buttons.Controls.Add(_insertButton);
        root.Controls.Add(buttons, 0, 2);
        root.Controls.Add(BuildSizeTypeSide(), 1, 1);
        return root;
    }

    private Control BuildSizeTypeSide()
    {
        var side = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Margin = new Padding(16, 0, 0, 0),
            BackColor = Color.Transparent
        };
        side.RowStyles.Add(new RowStyle(SizeType.Absolute, 196F));
        side.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var group = new KryptonGroupBox { Dock = DockStyle.Fill, Text = @"Size Type" };
        group.Panel.Padding = new Padding(12, 8, 12, 8);
        group.Panel.Controls.Add(BuildSizeTypeOptions());
        side.Controls.Add(group, 0, 0);
        side.Controls.Add(BuildHelp(), 0, 1);
        return side;
    }

    private Control BuildSizeTypeOptions()
    {
        var options = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 3,
            BackColor = Color.Transparent
        };
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 104F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64F));
        options.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        options.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        options.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        options.Controls.Add(_absoluteRadio, 0, 0);
        options.Controls.Add(_absoluteValue, 1, 0);
        options.Controls.Add(CreateUnitLabel(@"pixels"), 2, 0);
        options.Controls.Add(_percentRadio, 0, 1);
        options.Controls.Add(_percentValue, 1, 1);
        options.Controls.Add(CreateUnitLabel(@"%"), 2, 1);
        options.Controls.Add(_autoSizeRadio, 0, 2);
        options.SetColumnSpan(_autoSizeRadio, 3);
        return options;
    }

    private Control BuildHelp()
    {
        var help = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(0, 16, 0, 0),
            BackColor = Color.Transparent
        };
        help.Controls.Add(HelpBlock(
            @"Column and row spanning:",
            @"If you want a control to span multiple rows or columns, set the RowSpan and ColumnSpan properties on the control.",
            (@"RowSpan", RowSpanHelpUrl),
            (@"ColumnSpan", ColumnSpanHelpUrl)));
        help.Controls.Add(HelpBlock(
            @"Alignment and stretching:",
            @"If you want to align a control within a cell, or if you want a control to stretch within a cell, use the control's Anchor property.",
            (@"Anchor", AnchorHelpUrl)));
        return help;
    }

    private Control HelpBlock(string heading, string body, params (string Text, string Url)[] links)
    {
        var block = new TableLayoutPanel
        {
            Width = 370,
            AutoSize = true,
            ColumnCount = 2,
            RowCount = 3,
            Margin = new Padding(0, 0, 0, 10),
            BackColor = Color.Transparent
        };
        block.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
        block.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        block.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        block.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        block.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var icon = new KryptonPictureBox
        {
            Size = new Size(16, 16),
            SizeMode = PictureBoxSizeMode.CenterImage,
            Image = SystemIcons.Information.ToBitmap(),
            Margin = new Padding(0, 2, 0, 0)
        };
        var title = new KryptonWrapLabel
        {
            AutoSize = true,
            Text = heading,
            Font = new Font(Font, FontStyle.Bold)
        };
        var text = new KryptonWrapLabel
        {
            AutoSize = true,
            MaximumSize = new Size(340, 0),
            Text = body
        };
        var linkRow = new FlowLayoutPanel
        {
            AutoSize = true,
            WrapContents = false,
            FlowDirection = FlowDirection.LeftToRight,
            Margin = new Padding(0),
            BackColor = Color.Transparent
        };
        foreach ((string linkText, string url) in links)
        {
            linkRow.Controls.Add(CreateDocLink(linkText, url));
        }

        block.Controls.Add(icon, 0, 0);
        block.Controls.Add(title, 1, 0);
        block.Controls.Add(text, 1, 1);
        block.Controls.Add(linkRow, 1, 2);
        return block;
    }

    private static KryptonLinkLabel CreateDocLink(string text, string url)
    {
        var link = new KryptonLinkLabel
        {
            Text = text,
            AutoSize = true,
            LinkBehavior = KryptonLinkBehavior.HoverUnderline,
            Margin = new Padding(0, 0, 12, 0)
        };
        link.LinkClicked += (_, _) => OpenHelp(url);
        return link;
    }

    private static void OpenHelp(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Win32Exception)
        {
            // The browser could not be started. The dialog stays open.
        }
    }

    private static KryptonButton CreateButton(string text) =>
        new KryptonButton
        {
            AutoSize = false,
            Size = new Size(75, 25),
            Margin = new Padding(0, 0, 6, 0),
            Values = { Text = text }
        };

    private static KryptonRadioButton CreateRadio(string text) =>
        new KryptonRadioButton
        {
            Text = text,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 8, 8, 0)
        };

    private static KryptonNumericUpDown CreateSizeBox(int decimalPlaces) =>
        new KryptonNumericUpDown
        {
            Width = 96,
            Anchor = AnchorStyles.Left,
            DecimalPlaces = decimalPlaces,
            Minimum = 0,
            Maximum = 100000,
            Increment = 1,
            Margin = new Padding(0, 6, 8, 0)
        };

    private static KryptonLabel CreateUnitLabel(string text) =>
        new KryptonLabel
        {
            Text = text,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 8, 0, 0)
        };

    private int SelectedMemberIndex =>
        _members.SelectedIndices.Count == 0 ? -1 : _members.SelectedIndices[0];

    private SizeType SelectedSizeType =>
        _percentRadio.Checked ? SizeType.Percent :
        _autoSizeRadio.Checked ? SizeType.AutoSize :
        SizeType.Absolute;

    private void OnShowChanged(object? sender, EventArgs e)
    {
        _editingColumns = _showCombo.SelectedIndex == 0;
        RefreshMembers(0);
    }

    private void RefreshMembers(int selectIndex)
    {
        _suspend = true;
        try
        {
            _members.Items.Clear();
            List<KryptonTableLayoutStyleEdit.StyleValue> styles = KryptonTableLayoutStyleEdit.Read(_panel, _editingColumns);
            string prefix = _editingColumns ? @"Column" : @"Row";
            for (var index = 0; index < styles.Count; index++)
            {
                var item = new ListViewItem(prefix + (index + 1).ToString(CultureInfo.CurrentCulture));
                item.SubItems.Add(SizeTypeText(styles[index].SizeType));
                item.SubItems.Add(FormatValue(styles[index]));
                _members.Items.Add(item);
            }

            if (styles.Count == 0)
            {
                return;
            }

            int selected = Math.Min(Math.Max(selectIndex, 0), styles.Count - 1);
            _members.Items[selected].Selected = true;
            _members.Items[selected].Focused = true;
        }
        finally
        {
            _suspend = false;
        }

        LoadSelectedStyle();
    }

    private void LoadSelectedStyle()
    {
        if (_suspend)
        {
            return;
        }

        int index = SelectedMemberIndex;
        bool hasSelection = index >= 0;
        _deleteButton.Enabled = hasSelection;
        _absoluteRadio.Enabled = hasSelection;
        _percentRadio.Enabled = hasSelection;
        _autoSizeRadio.Enabled = hasSelection;
        if (!hasSelection)
        {
            _absoluteValue.Enabled = false;
            _percentValue.Enabled = false;
            return;
        }

        List<KryptonTableLayoutStyleEdit.StyleValue> styles = KryptonTableLayoutStyleEdit.Read(_panel, _editingColumns);
        KryptonTableLayoutStyleEdit.StyleValue style = styles[index];
        _suspend = true;
        try
        {
            if (style.SizeType == SizeType.Percent)
            {
                _percentValue.Value = Clamp(_percentValue, style.Size);
                _percentRadio.Checked = true;
            }
            else if (style.SizeType == SizeType.AutoSize)
            {
                _autoSizeRadio.Checked = true;
            }
            else
            {
                _absoluteValue.Value = Clamp(_absoluteValue, style.Size);
                _absoluteRadio.Checked = true;
            }

            _absoluteValue.Enabled = style.SizeType == SizeType.Absolute;
            _percentValue.Enabled = style.SizeType == SizeType.Percent;
        }
        finally
        {
            _suspend = false;
        }
    }

    private void ApplySelectedStyle()
    {
        int index = SelectedMemberIndex;
        if (_suspend || index < 0)
        {
            return;
        }

        SizeType sizeType = SelectedSizeType;
        _absoluteValue.Enabled = sizeType == SizeType.Absolute;
        _percentValue.Enabled = sizeType == SizeType.Percent;
        float size = sizeType == SizeType.Percent ? (float)_percentValue.Value : (float)_absoluteValue.Value;
        if (sizeType == SizeType.AutoSize)
        {
            size = 0f;
        }

        var style = new KryptonTableLayoutStyleEdit.StyleValue(sizeType, size);
        KryptonTableLayoutStyleEdit.Update(_panel, _editingColumns, index, style.SizeType, style.Size);
        ListViewItem item = _members.Items[index];
        item.SubItems[1].Text = SizeTypeText(sizeType);
        item.SubItems[2].Text = FormatValue(style);
    }

    private void AddStyle()
    {
        int index = _editingColumns ? _panel.ColumnCount : _panel.RowCount;
        KryptonTableLayoutStyleEdit.Insert(_panel, _editingColumns, index);
        RefreshMembers(index);
    }

    private void InsertStyle()
    {
        int index = SelectedMemberIndex < 0 ? 0 : SelectedMemberIndex;
        KryptonTableLayoutStyleEdit.Insert(_panel, _editingColumns, index);
        RefreshMembers(index);
    }

    private void DeleteStyle()
    {
        int index = SelectedMemberIndex;
        if (index < 0)
        {
            return;
        }

        KryptonTableLayoutStyleEdit.RemoveAt(_panel, _editingColumns, index);
        RefreshMembers(index);
    }

    private void RestoreOriginal()
    {
        if (_restored)
        {
            return;
        }

        _restored = true;
        KryptonTableLayoutStyleEdit.Restore(_panel, _originalColumns, _originalRows, _originalCells);
    }

    private static decimal Clamp(KryptonNumericUpDown box, float size)
    {
        decimal value = (decimal)size;
        if (value < box.Minimum)
        {
            return box.Minimum;
        }

        return value > box.Maximum ? box.Maximum : value;
    }

    private static string SizeTypeText(SizeType sizeType)
    {
        switch (sizeType)
        {
            case SizeType.Percent:
                return @"Percent";
            case SizeType.AutoSize:
                return @"AutoSize";
            default:
                return @"Absolute";
        }
    }

    private static string FormatValue(KryptonTableLayoutStyleEdit.StyleValue style)
    {
        switch (style.SizeType)
        {
            case SizeType.Percent:
                return style.Size.ToString(@"0.00", CultureInfo.CurrentCulture) + @"%";
            case SizeType.AutoSize:
                return string.Empty;
            default:
                return style.Size.ToString(@"0.##", CultureInfo.CurrentCulture);
        }
    }

    #endregion
}
