#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public partial class VisualCustomFilterForm : KryptonForm
{
    #region Instance Fields

    private readonly FilterType _filterType;
    private Control? _valControl1;
    private Control? _valControl2;

    private readonly bool _filterDateAndTimeEnabled;

    private string? _filterString;

    private string? _filterStringDescription;

    #endregion

    #region Public

    /// <summary>
    /// Get the Filter string
    /// </summary>
    public string? FilterString => _filterString;

    /// <summary>
    /// Get the Filter string description
    /// </summary>
    public string? FilterStringDescription => _filterStringDescription;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="VisualCustomFilterForm"/> class.
    /// </summary>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="filterDateAndTimeEnabled">if set to <c>true</c> [filter date and time enabled].</param>
    public VisualCustomFilterForm(Type dataType, bool filterDateAndTimeEnabled)
    {
        InitializeComponent();

        //set component translations
        Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewFormTitle)];
        label_columnName.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLabelColumnNameText)];
        label_and.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLabelAnd)];
        button_ok.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewButtonOk)];
        button_cancel.Text = KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewButtonCancel)];

        _filterType = dataType switch
        {
            _ when dataType == typeof(DateTime) => FilterType.DateTime,
            _ when dataType == typeof(TimeSpan) => FilterType.TimeSpan,
            _ when dataType == typeof(int) || dataType == typeof(long) || dataType == typeof(short) ||
                 dataType == typeof(uint) || dataType == typeof(ulong) || dataType == typeof(ushort) ||
                 dataType == typeof(byte) || dataType == typeof(sbyte) => FilterType.Integer,
            _ when dataType == typeof(float) || dataType == typeof(double) || dataType == typeof(decimal) => FilterType.Float,
            _ when dataType == typeof(string) => FilterType.String,
            _ => FilterType.Unknown
        };

        _filterDateAndTimeEnabled = filterDateAndTimeEnabled;

        switch (_filterType)
        {
            case FilterType.DateTime:
                _valControl1 = new DateTimePicker();
                _valControl2 = new DateTimePicker();
                if (_filterDateAndTimeEnabled)
                {
                    DateTimeFormatInfo dt = Thread.CurrentThread.CurrentCulture.DateTimeFormat;

                    (_valControl1 as DateTimePicker)!.CustomFormat = $@"{dt.ShortDatePattern} HH:mm";
                    (_valControl2 as DateTimePicker)!.CustomFormat = $@"{dt.ShortDatePattern} HH:mm";
                    (_valControl1 as DateTimePicker)!.Format = DateTimePickerFormat.Custom;
                    (_valControl2 as DateTimePicker)!.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    (_valControl1 as DateTimePicker)!.Format = DateTimePickerFormat.Short;
                    (_valControl2 as DateTimePicker)!.Format = DateTimePickerFormat.Short;
                }

                comboBox_filterType.Items.AddRange([
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEquals)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEarlierThan)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEarlierThanOrEqualTo)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLaterThan)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLaterThanOrEqualTo)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewBetween)]
                ]);
                break;

            case FilterType.TimeSpan:
                _valControl1 = new TextBox();
                _valControl2 = new TextBox();
                comboBox_filterType.Items.AddRange([
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewContains)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotContain)]
                ]);
                break;

            case FilterType.Integer:
            case FilterType.Float:
                _valControl1 = new TextBox();
                _valControl2 = new TextBox();
                _valControl1.TextChanged += valControl_TextChanged;
                _valControl2.TextChanged += valControl_TextChanged;
                comboBox_filterType.Items.AddRange([
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEquals)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewGreaterThan)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewGreaterThanOrEqualTo)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLessThan)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLessThanOrEqualTo)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewBetween)]
                ]);
                _valControl1.Tag = true;
                _valControl2.Tag = true;
                button_ok.Enabled = false;
                break;

            default:
                _valControl1 = new TextBox();
                _valControl2 = new TextBox();
                comboBox_filterType.Items.AddRange([
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEquals)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewBeginsWith)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotBeginWith)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEndsWith)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEndWith)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewContains)],
                    KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotContain)]
                ]);
                break;
        }
        comboBox_filterType.SelectedIndex = 0;

        _valControl1.Name = "valControl1";
        _valControl1.Location = new(20, 66);
        _valControl1.Size = new(166, 20);
        _valControl1.Width = comboBox_filterType.Width - 20;
        _valControl1.TabIndex = 4;
        _valControl1.Visible = true;
        _valControl1.KeyDown += valControl_KeyDown;

        _valControl2.Name = "valControl2";
        _valControl2.Location = new(20, 108);
        _valControl2.Size = new(166, 20);
        _valControl2.Width = comboBox_filterType.Width - 20;
        _valControl2.TabIndex = 5;
        _valControl2.Visible = false;
        _valControl2.VisibleChanged += valControl2_VisibleChanged;
        _valControl2.KeyDown += valControl_KeyDown;

        Controls.Add(_valControl1);
        Controls.Add(_valControl2);

        ep.SetIconAlignment(_valControl1, KryptonErrorIconAlignment.MiddleRight);
        ep.SetIconPadding(_valControl1, -18);
        ep.SetIconAlignment(_valControl2, KryptonErrorIconAlignment.MiddleRight);
        ep.SetIconPadding(_valControl2, -18);
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Build a Filter string
    /// </summary>
    /// <param name="filterType"></param>
    /// <param name="filterDateAndTimeEnabled"></param>
    /// <param name="filterTypeConditionText"></param>
    /// <param name="control1"></param>
    /// <param name="control2"></param>
    /// <returns></returns>
    private string? BuildCustomFilter(FilterType filterType, bool filterDateAndTimeEnabled, string filterTypeConditionText, Control control1, Control control2)
    {
        string? filterString;

        string? column = @"[{0}] ";

        if (filterType == FilterType.Unknown)
        {
            column = $"Convert([{{0}}], 'System.String') ";
        }

        filterString = column;

        switch (filterType)
        {
            case FilterType.DateTime:
                DateTime dt = ((DateTimePicker)control1).Value;
                dt = new(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);

                if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEquals)])
                {
                    filterString =
                        $"Convert([{{0}}], 'System.String') LIKE '%{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}%'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEarlierThan)])
                {
                    filterString +=
                        $"< '{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEarlierThanOrEqualTo)])
                {
                    filterString +=
                        $"<= '{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLaterThan)])
                {
                    filterString +=
                        $"> '{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLaterThanOrEqualTo)])
                {
                    filterString +=
                        $">= '{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewBetween)])
                {
                    DateTime dt1 = ((DateTimePicker)control2).Value;
                    dt1 = new(dt1.Year, dt1.Month, dt1.Day, dt1.Hour, dt1.Minute, 0);
                    filterString +=
                        $">= '{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}'";
                    filterString +=
                        $" AND {column}<= '{Convert.ToString(filterDateAndTimeEnabled ? dt1 : dt1.Date, CultureInfo.CurrentCulture)}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual)])
                {
                    filterString =
                        $"Convert([{{0}}], 'System.String') NOT LIKE '%{Convert.ToString(filterDateAndTimeEnabled ? dt : dt.Date, CultureInfo.CurrentCulture)}%'";
                }

                break;

            case FilterType.TimeSpan:
                try
                {
                    TimeSpan ts = TimeSpan.Parse(control1.Text);

                    if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewContains)])
                    {
                        filterString =
                            $"(Convert([{{0}}], 'System.String') LIKE '%P{(ts.Days > 0 ? $"{ts.Days}D" : "")}{(ts.TotalHours > 0 ? "T" : "")}{(ts.Hours > 0 ? $"{ts.Hours}H" : "")}{(ts.Minutes > 0 ? $"{ts.Minutes}M" : "")}{(ts.Seconds > 0 ? $"{ts.Seconds}S" : "")}%')";
                    }
                    else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotContain)])
                    {
                        filterString =
                            $"(Convert([{{0}}], 'System.String') NOT LIKE '%P{(ts.Days > 0 ? $"{ts.Days}D" : "")}{(ts.TotalHours > 0 ? "T" : "")}{(ts.Hours > 0 ? $"{ts.Hours}H" : "")}{(ts.Minutes > 0 ? $"{ts.Minutes}M" : "")}{(ts.Seconds > 0 ? $"{ts.Seconds}S" : "")}%')";
                    }
                }
                catch (FormatException)
                {
                    filterString = null;
                }
                break;

            case FilterType.Integer:
            case FilterType.Float:

                string num = control1.Text;

                if (filterType == FilterType.Float)
                {
                    num = num.Replace(",", ".");
                }

                if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEquals)])
                {
                    filterString += $"= {num}";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewBetween)])
                {
                    filterString +=
                        $">= {num} AND {column}<= {(filterType == FilterType.Float ? control2.Text.Replace(",", ".") : control2.Text)}";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual)])
                {
                    filterString += $"<> {num}";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewGreaterThan)])
                {
                    filterString += $"> {num}";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewGreaterThanOrEqualTo)])
                {
                    filterString += $">= {num}";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLessThan)])
                {
                    filterString += $"< {num}";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewLessThanOrEqualTo)])
                {
                    filterString += $"<= {num}";
                }

                break;

            default:
                string txt = FormatFilterString(control1.Text);
                if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEquals)])
                {
                    filterString += $"LIKE '{txt}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual)])
                {
                    filterString += $"NOT LIKE '{txt}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewBeginsWith)])
                {
                    filterString += $"LIKE '{txt}%'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewEndsWith)])
                {
                    filterString += $"LIKE '%{txt}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotBeginWith)])
                {
                    filterString += $"NOT LIKE '{txt}%'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEndWith)])
                {
                    filterString += $"NOT LIKE '%{txt}'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewContains)])
                {
                    filterString += $"LIKE '%{txt}%'";
                }
                else if (filterTypeConditionText == KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotContain)])
                {
                    filterString += $"NOT LIKE '%{txt}%'";
                }

                break;
        }

        return filterString;
    }

    /// <summary>
    /// Format a text Filter string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private string FormatFilterString(string text)
    {
        string result = "";
        string s;
        string[] replace = ["%", "[", "]", "*", "\"", "\\"];

        for (int i = 0; i < text.Length; i++)
        {
            s = text[i].ToString();
            if (replace.Contains(s))
            {
                result += $"[{s}]";
            }
            else
            {
                result += s;
            }
        }

        return result.Replace("'", "''");
    }

    private void button_cancel_Click(object sender, EventArgs e)
    {
        _filterStringDescription = null;
        _filterString = null;
        DialogResult = DialogResult.Cancel;
    }

    private void button_ok_Click(object sender, EventArgs e)
    {
        if (_valControl1 != null && _valControl2 != null && ((_valControl1.Visible && _valControl1.Tag != null && (bool)_valControl1.Tag) ||
                                                             (_valControl2.Visible && _valControl2.Tag != null && (bool)_valControl2.Tag)))
        {
            button_ok.Enabled = false;
            return;
        }

        if (_valControl1 != null)
        {
            if (_valControl2 != null)
            {
                string? filterString = BuildCustomFilter(_filterType, _filterDateAndTimeEnabled, comboBox_filterType.Text, _valControl1, _valControl2);

                if (!String.IsNullOrEmpty(filterString))
                {
                    _filterString = filterString;
                    _filterStringDescription = string.Format(KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewFilterStringDescription)], comboBox_filterType.Text, _valControl1.Text);
                    if (_valControl2.Visible)
                    {
                        _filterStringDescription += $" {label_and.Text} \"{_valControl2.Text}\"";
                    }

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    _filterString = null;
                    _filterStringDescription = null;
                    DialogResult = DialogResult.Cancel;
                }
            }
        }

        Close();
    }

    private void comboBox_filterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_valControl2 != null)
        {
            _valControl2.Visible = comboBox_filterType.Text ==
                                   KryptonAdvancedDataGridView.Translations[
                                       nameof(TranslationKey.KryptonAdvancedDataGridViewBetween)];
            if (_valControl1 != null)
            {
                button_ok.Enabled =
                    !(_valControl1.Visible && _valControl1.Tag != null && (bool)_valControl1.Tag) ||
                    (_valControl2.Visible && _valControl2.Tag != null && (bool)_valControl2.Tag);
            }
        }
    }

    /// <summary>
    /// Changed control2 visibility
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void valControl2_VisibleChanged(object? sender, EventArgs e)
    {
        if (_valControl2 != null)
        {
            label_and.Visible = _valControl2.Visible;
        }
    }

    /// <summary>
    /// Changed a control Text
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void valControl_TextChanged(object? sender, EventArgs e)
    {
        bool hasErrors = false;
        switch (_filterType)
        {
            case FilterType.Integer:
                long val;
                hasErrors = !long.TryParse((sender as TextBox)!.Text, out val);
                break;

            case FilterType.Float:
                double val1;
                hasErrors = !double.TryParse((sender as TextBox)!.Text, out val1);
                break;
        }

        (sender as Control)!.Tag = hasErrors || (sender as TextBox)!.Text.Length == 0;

        if (hasErrors && (sender as TextBox)!.Text.Length > 0)
        {
            ep.SetError((sender as Control)!, KryptonAdvancedDataGridView.Translations[nameof(TranslationKey.KryptonAdvancedDataGridViewInvalidValue)]);
        }
        else
        {
            ep.SetError((sender as Control)!, "");
        }

        if (_valControl1 != null && _valControl2 != null)
        {
            button_ok.Enabled = !(_valControl1.Visible && _valControl1.Tag != null && (bool)_valControl1.Tag) ||
                                (_valControl2.Visible && _valControl2.Tag != null && (bool)_valControl2.Tag);
        }
    }

    /// <summary>
    /// KeyDown on a control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void valControl_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.Enter)
        {
            if (sender == _valControl1)
            {
                if (_valControl2 is { Visible: true })
                {
                    _valControl2.Focus();
                }
                else
                {
                    button_ok_Click(button_ok, EventArgs.Empty);
                }
            }
            else
            {
                button_ok_Click(button_ok, EventArgs.Empty);
            }

            e.SuppressKeyPress = false;
            e.Handled = true;
        }
    }

    #endregion
}