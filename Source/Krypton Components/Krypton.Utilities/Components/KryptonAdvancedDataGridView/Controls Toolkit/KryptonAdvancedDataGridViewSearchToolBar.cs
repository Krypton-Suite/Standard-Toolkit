#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

// Control specific using statements

#if NETFRAMEWORK
using System.Web.Script.Serialization;
#else
using System.Text.Json;
#endif

namespace Krypton.Utilities;

[DesignerCategory("code")]
public partial class KryptonAdvancedDataGridViewSearchToolBar : ToolStrip
{
    #region Design Code

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer? components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
        this.tslblSearch = new System.Windows.Forms.ToolStripLabel();
        this.tscmbColumns = new System.Windows.Forms.ToolStripComboBox();
        this.tstxtSearch = new System.Windows.Forms.ToolStripTextBox();
        this.tsbtnFromBegin = new System.Windows.Forms.ToolStripButton();
        this.tsbtnCaseSensitive = new System.Windows.Forms.ToolStripButton();
        this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
        this.tsbtnWholeWord = new System.Windows.Forms.ToolStripButton();
        this.tssepSearch = new System.Windows.Forms.ToolStripSeparator();
        this.SuspendLayout();
        // 
        // button_close
        // 
        this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnClose.Image = global::Krypton.Utilities.Properties.Resources.SearchToolBar_ButtonCaseSensitive;
        this.tsbtnClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnClose.Name = "tsbtnClose";
        this.tsbtnClose.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
        this.tsbtnClose.Size = new System.Drawing.Size(23, 24);
        this.tsbtnClose.Click += new System.EventHandler(this.button_close_Click);
        // 
        // label_search
        // 
        this.tslblSearch.Name = "tslblSearch";
        this.tslblSearch.Size = new System.Drawing.Size(45, 15);

        // 
        // comboBox_columns
        // 
        this.tscmbColumns.AutoSize = false;
        this.tscmbColumns.AutoToolTip = true;
        this.tscmbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.tscmbColumns.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
        this.tscmbColumns.IntegralHeight = false;
        this.tscmbColumns.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
        this.tscmbColumns.MaxDropDownItems = 12;
        this.tscmbColumns.Name = "tscmbColumns";
        this.tscmbColumns.Size = new System.Drawing.Size(150, 23);
        // 
        // textBox_search
        // 
        this.tstxtSearch.AutoSize = false;
        this.tstxtSearch.ForeColor = System.Drawing.Color.LightGray;
        this.tstxtSearch.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
        this.tstxtSearch.Name = "tstxtSearch";
        this.tstxtSearch.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
        this.tstxtSearch.Size = new System.Drawing.Size(100, 23);
        this.tstxtSearch.Enter += new System.EventHandler(this.textBox_search_Enter);
        this.tstxtSearch.Leave += new System.EventHandler(this.textBox_search_Leave);
        this.tstxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_KeyDown);
        this.tstxtSearch.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
        // 
        // button_frombegin
        // 
        this.tsbtnFromBegin.CheckOnClick = true;
        this.tsbtnFromBegin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnFromBegin.Image = global::Krypton.Utilities.Properties.Resources.SearchToolBar_ButtonFromBegin;
        this.tsbtnFromBegin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        this.tsbtnFromBegin.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnFromBegin.Name = "tsbtnFromBegin";
        this.tsbtnFromBegin.Size = new System.Drawing.Size(23, 20);
        // 
        // button_casesensitive
        // 
        this.tsbtnCaseSensitive.CheckOnClick = true;
        this.tsbtnCaseSensitive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnCaseSensitive.Image = global::Krypton.Utilities.Properties.Resources.SearchToolBar_ButtonCaseSensitive;
        this.tsbtnCaseSensitive.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        this.tsbtnCaseSensitive.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnCaseSensitive.Name = "tsbtnCaseSensitive";
        this.tsbtnCaseSensitive.Size = new System.Drawing.Size(23, 20);
        // 
        // button_search
        // 
        this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnSearch.Image = global::Krypton.Utilities.Properties.Resources.SearchToolBar_ButtonSearch;
        this.tsbtnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnSearch.Name = "tsbtnSearch";
        this.tsbtnSearch.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
        this.tsbtnSearch.Size = new System.Drawing.Size(23, 24);
        this.tsbtnSearch.Click += new System.EventHandler(this.button_search_Click);
        // 
        // button_wholeword
        // 
        this.tsbtnWholeWord.CheckOnClick = true;
        this.tsbtnWholeWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnWholeWord.Image = global::Krypton.Utilities.Properties.Resources.SearchToolBar_ButtonWholeWord;
        this.tsbtnWholeWord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        this.tsbtnWholeWord.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnWholeWord.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
        this.tsbtnWholeWord.Name = "tsbtnWholeWord";
        this.tsbtnWholeWord.Size = new System.Drawing.Size(23, 20);
        // 
        // separator_search
        // 
        this.tssepSearch.AutoSize = false;
        this.tssepSearch.Name = "tssepSearch";
        this.tssepSearch.Size = new System.Drawing.Size(10, 25);
        // 
        // AdvancedDataGridViewSearchToolBar
        // 
        this.AllowMerge = false;
        this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClose,
            this.tslblSearch,
            this.tscmbColumns,
            this.tstxtSearch,
            this.tsbtnFromBegin,
            this.tsbtnWholeWord,
            this.tsbtnCaseSensitive,
            this.tssepSearch,
            this.tsbtnSearch});
        this.MaximumSize = new System.Drawing.Size(0, 27);
        this.MinimumSize = new System.Drawing.Size(0, 27);
        this.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
        this.Size = new System.Drawing.Size(0, 27);
        this.Resize += new System.EventHandler(this.ResizeMe);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private ToolStripButton tsbtnClose;
    private ToolStripLabel tslblSearch;
    private ToolStripComboBox tscmbColumns;
    private ToolStripTextBox tstxtSearch;
    private ToolStripButton tsbtnFromBegin;
    private ToolStripButton tsbtnCaseSensitive;
    private ToolStripButton tsbtnSearch;
    private ToolStripButton tsbtnWholeWord;
    private ToolStripSeparator tssepSearch;

    #endregion

    #region public events

    public event AdvancedDataGridViewSearchToolBarSearchEventHandler? Search;

    #endregion


    #region class properties

    private DataGridViewColumnCollection? _columnsList;

    private const bool BUTTON_CLOSE_ENABLED = false;

    #endregion


    #region translations

    /// <summary>
    /// Internationalization strings
    /// </summary>
    public static Dictionary<string, string> Translations = new Dictionary<string, string>()
    {
        { nameof(TranslationKey.ADGVSTBLabelSearch), "Search:" },
        { nameof(TranslationKey.ADGVSTBButtonFromBegin), "From Begin" },
        { nameof(TranslationKey.ADGVSTBButtonCaseSensitiveToolTip), "Case Sensitivity" },
        { nameof(TranslationKey.ADGVSTBButtonSearchToolTip), "Find Next" },
        { nameof(TranslationKey.ADGVSTBButtonCloseToolTip), "Hide" },
        { nameof(TranslationKey.ADGVSTBButtonWholeWordToolTip), "Search only Whole Word" },
        { nameof(TranslationKey.ADGVSTBComboBoxColumnsAll), "(All Columns)" },
        { nameof(TranslationKey.ADGVSTBTextBoxSearchToolTip), "Value for Search" }
    };

    /// <summary>
    /// Used to check if components translations has to be updated
    /// </summary>
    private Dictionary<string, string> _translationsRefreshComponentTranslationsCheck = new Dictionary<string, string>() { };

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonAdvancedDataGridViewSearchToolBar" /> class.</summary>
    public KryptonAdvancedDataGridViewSearchToolBar()
    {
        //initialize components
        InitializeComponent();

        RefreshComponentTranslations();

        //set default values
        if (!BUTTON_CLOSE_ENABLED)
        {
            Items.RemoveAt(0);
        }

        tscmbColumns!.SelectedIndex = 0;

        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;
    }

    #endregion

    #region Translations Methods

    /// <summary>
    /// Set translation dictionary
    /// </summary>
    /// <param name="translations"></param>
    public static void SetTranslations(IDictionary<string, string>? translations)
    {
        //set localization strings
        if (translations != null)
        {
            foreach (KeyValuePair<string, string> translation in translations)
            {
                if (Translations.ContainsKey(translation.Key))
                {
                    Translations[translation.Key] = translation.Value;
                }
            }
        }
    }

    /// <summary>
    /// Get translation dictionary
    /// </summary>
    /// <returns></returns>
    public static IDictionary<string, string> GetTranslations()
    {
        return Translations;
    }

    /// <summary>
    /// Load translations from file
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static IDictionary<string, string> LoadTranslationsFromFile(string filename)
    {
        IDictionary<string, string> ret = new Dictionary<string, string>();

        if (!String.IsNullOrEmpty(filename))
        {
            //deserialize the file
            try
            {
                string jsonText = File.ReadAllText(filename);
#if NETFRAMEWORK
                Dictionary<string, string> translations =
                    new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonText);
#else
                Dictionary<string, string>? translations =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);
#endif
                foreach (KeyValuePair<string, string> translation in translations!)
                {
                    if (!ret.ContainsKey(translation.Key) && Translations.ContainsKey(translation.Key))
                    {
                        ret.Add(translation.Key, translation.Value);
                    }
                }
            }
            catch
            {
                // Nothing to do
            }
        }

        //add default translations if not in files
        foreach (KeyValuePair<string, string> translation in GetTranslations())
        {
            if (!ret.ContainsKey(translation.Key))
            {
                ret.Add(translation.Key, translation.Value);
            }
        }

        return ret;
    }

    /// <summary>
    /// Update components translations
    /// </summary>
    private void RefreshComponentTranslations()
    {
        tscmbColumns.BeginUpdate();
        tscmbColumns.Items.Clear();
        tscmbColumns.Items.AddRange([Translations[nameof(TranslationKey.ADGVSTBComboBoxColumnsAll)]]);
        if (_columnsList != null)
        {
            foreach (DataGridViewColumn c in _columnsList)
            {
                if (c.Visible)
                {
                    tscmbColumns.Items.Add(c.HeaderText);
                }
            }
        }

        tscmbColumns.SelectedIndex = 0;
        tscmbColumns.EndUpdate();
        tsbtnClose.ToolTipText = Translations[nameof(TranslationKey.ADGVSTBButtonCloseToolTip)];
        tslblSearch.Text = Translations[nameof(TranslationKey.ADGVSTBLabelSearch)];
        tstxtSearch.ToolTipText = Translations[nameof(TranslationKey.ADGVSTBTextBoxSearchToolTip)];
        tsbtnFromBegin.ToolTipText = Translations[nameof(TranslationKey.ADGVSTBButtonFromBegin)];
        tsbtnCaseSensitive.ToolTipText = Translations[nameof(TranslationKey.ADGVSTBButtonCaseSensitiveToolTip)];
        tsbtnSearch.ToolTipText = Translations[nameof(TranslationKey.ADGVSTBButtonSearchToolTip)];
        tsbtnWholeWord.ToolTipText = Translations[nameof(TranslationKey.ADGVSTBButtonWholeWordToolTip)];
        tstxtSearch.Text = tstxtSearch.ToolTipText;
    }

    #endregion

    #region Button Events

    /// <summary>
    /// button Search Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void button_search_Click(object? sender, EventArgs e)
    {
        if (tstxtSearch.TextLength > 0 && tstxtSearch.Text != tstxtSearch.ToolTipText && Search != null)
        {
            DataGridViewColumn? c = null;
            if (tscmbColumns.SelectedIndex > 0 && _columnsList != null && _columnsList.GetColumnCount(DataGridViewElementStates.Visible) > 0)
            {
                DataGridViewColumn?[] cols = _columnsList.Cast<DataGridViewColumn>().Where(col => col.Visible).ToArray<DataGridViewColumn>();

                if (cols.Length == tscmbColumns.Items.Count - 1)
                {
                    if (cols[tscmbColumns.SelectedIndex - 1]!.HeaderText == tscmbColumns.SelectedItem?.ToString())
                    {
                        c = cols[tscmbColumns.SelectedIndex - 1];
                    }
                }
            }

            AdvancedDataGridViewSearchToolBarSearchEventArgs args = new AdvancedDataGridViewSearchToolBarSearchEventArgs(
                tstxtSearch.Text,
                c,
                tsbtnCaseSensitive.Checked,
                tsbtnWholeWord.Checked,
                tsbtnFromBegin.Checked
            );
            Search(this, args);
        }
    }

    /// <summary>
    /// button Close Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void button_close_Click(object? sender, EventArgs e)
    {
        Hide();
    }

    #endregion
    
    #region Textbox Search Events

    /// <summary>
    /// textBox Search TextChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void textBox_search_TextChanged(object? sender, EventArgs e)
    {
        tsbtnSearch.Enabled = tstxtSearch.TextLength > 0 && tstxtSearch.Text != tstxtSearch.ToolTipText;
    }


    /// <summary>
    /// textBox Search Enter event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void textBox_search_Enter(object? sender, EventArgs e)
    {
        if (tstxtSearch.Text == tstxtSearch.ToolTipText && tstxtSearch.ForeColor == Color.LightGray)
        {
            tstxtSearch.Text = "";
        }
        else
        {
            tstxtSearch.SelectAll();
        }

        tstxtSearch.ForeColor = SystemColors.WindowText;
    }

    /// <summary>
    /// textBox Search Leave event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void textBox_search_Leave(object? sender, EventArgs e)
    {
        if (tstxtSearch.Text.Trim() == "")
        {
            tstxtSearch.Text = tstxtSearch.ToolTipText;
            tstxtSearch.ForeColor = Color.LightGray;
        }
    }


    /// <summary>
    /// textBox Search KeyDown event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void textBox_search_KeyDown(object? sender, KeyEventArgs e)
    {
        if (tstxtSearch.TextLength > 0 && tstxtSearch.Text != tstxtSearch.ToolTipText && e.KeyData == Keys.Enter)
        {
            button_search_Click(tsbtnSearch, EventArgs.Empty);
            e.SuppressKeyPress = true;
            e.Handled = true;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Set Columns to search in
    /// </summary>
    /// <param name="columns"></param>
    public void SetColumns(DataGridViewColumnCollection columns)
    {
        _columnsList = columns;
        tscmbColumns.BeginUpdate();
        tscmbColumns.Items.Clear();
        tscmbColumns.Items.AddRange([Translations[nameof(TranslationKey.ADGVSTBComboBoxColumnsAll)]]);
        if (_columnsList != null)
        {
            foreach (DataGridViewColumn c in _columnsList)
            {
                if (c.Visible)
                {
                    tscmbColumns.Items.Add(c.HeaderText);
                }
            }
        }

        tscmbColumns.SelectedIndex = 0;
        tscmbColumns.EndUpdate();
    }

    #endregion

    #region Resize Events

    /// <summary>
    /// Resize event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResizeMe(object? sender, EventArgs e)
    {
        SuspendLayout();
        int w1 = 150;
        int w2 = 150;
        int oldW = tscmbColumns.Width + tstxtSearch.Width;
        foreach (ToolStripItem c in Items)
        {
            c.Overflow = ToolStripItemOverflow.Never;
            c.Visible = true;
        }

        int width = PreferredSize.Width - oldW + w1 + w2;
        if (Width < width)
        {
            tslblSearch.Visible = false;
            GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
            width = PreferredSize.Width - oldW + w1 + w2;

            if (Width < width)
            {
                tsbtnCaseSensitive.Overflow = ToolStripItemOverflow.Always;
                GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                width = PreferredSize.Width - oldW + w1 + w2;
            }

            if (Width < width)
            {
                tsbtnWholeWord.Overflow = ToolStripItemOverflow.Always;
                GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                width = PreferredSize.Width - oldW + w1 + w2;
            }

            if (Width < width)
            {
                tsbtnFromBegin.Overflow = ToolStripItemOverflow.Always;
                tssepSearch.Visible = false;
                GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                width = PreferredSize.Width - oldW + w1 + w2;
            }

            if (Width < width)
            {
                tscmbColumns.Overflow = ToolStripItemOverflow.Always;
                tstxtSearch.Overflow = ToolStripItemOverflow.Always;
                w1 = 150;
                w2 = Math.Max(Width - PreferredSize.Width - tstxtSearch.Margin.Left - tstxtSearch.Margin.Right, 75);
                tstxtSearch.Overflow = ToolStripItemOverflow.Never;
                width = PreferredSize.Width - tstxtSearch.Width + w2;
            }
            if (Width < width)
            {
                tsbtnSearch.Overflow = ToolStripItemOverflow.Always;
                w2 = Math.Max(Width - PreferredSize.Width + tstxtSearch.Width, 75);
                width = PreferredSize.Width - tstxtSearch.Width + w2;
            }
            if (Width < width)
            {
                tsbtnClose.Overflow = ToolStripItemOverflow.Always;
                tstxtSearch.Margin = new Padding(8, 2, 8, 2);
                w2 = Math.Max(Width - PreferredSize.Width + tstxtSearch.Width, 75);
                width = PreferredSize.Width - tstxtSearch.Width + w2;
            }

            if (Width < width)
            {
                w2 = Math.Max(Width - PreferredSize.Width + tstxtSearch.Width, 20);
                width = PreferredSize.Width - tstxtSearch.Width + w2;
            }
            if (width > Width)
            {
                tstxtSearch.Overflow = ToolStripItemOverflow.Always;
                tstxtSearch.Margin = new Padding(0, 2, 8, 2);
                w2 = 150;
            }
        }
        else
        {
            GetResizeBoxSize(width, ref w1, ref w2);
        }

        if (tscmbColumns.Width != w1)
        {
            tscmbColumns.Width = w1;
        }

        if (tstxtSearch.Width != w2)
        {
            tstxtSearch.Width = w2;
        }

        ResumeLayout();
    }

    /// <summary>
    /// Get a Resize Size for a box
    /// </summary>
    /// <param name="width"></param>
    /// <param name="w1"></param>
    /// <param name="w2"></param>
    private void GetResizeBoxSize(int width, ref int w1, ref int w2)
    {
        int dif = (int)Math.Round((width - Width) / 2.0, 0, MidpointRounding.AwayFromZero);

        int oldW1 = w1;
        int oldW2 = w2;
        if (Width < width)
        {
            w1 = Math.Max(w1 - dif, 75);
            w2 = Math.Max(w2 - dif, 75);
        }
        else
        {
            w1 = Math.Min(w1 - dif, 150);
            w2 += Width - width + oldW1 - w1;
        }
    }

    #endregion

    #region Paint Events

    /// <summary>
    /// On Paint event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPaint(PaintEventArgs e)
    {
        //check if translations are changed and update components
        if (!(_translationsRefreshComponentTranslationsCheck == Translations || (_translationsRefreshComponentTranslationsCheck.Count == Translations.Count && !_translationsRefreshComponentTranslationsCheck.Except(Translations).Any())))
        {
            _translationsRefreshComponentTranslationsCheck = Translations;
            RefreshComponentTranslations();
        }

        base.OnPaint(e);
    }

    #endregion
}
