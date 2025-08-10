#region Original License
/*
 *
 * Microsoft Public License (Ms-PL)
 *
 * This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
 *
 * 1. Definitions 
 *
 * The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
 *
 * A "contribution" is the original software, or any additions or changes to the software.
 *
 * A "contributor" is any person that distributes its contribution under this license.
 *
 * "Licensed patents" are a contributor's patent claims that read directly on its contribution.
 *
 * 2. Grant of Rights
 *
 * (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
 *
 * (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
 *
 * 3. Conditions and Limitations
 *
 * (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
 *
 * (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
 *
 * (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
 *
 * (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
 *
 * (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
 *
 */
#endregion

#region MIT License
/*
 * MIT License
 *
 * Copyright (c) 2017 - 2024 Krypton Suite
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */
#endregion

//#pragma warning disable 
// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit
{
    [DesignerCategory("code")]
    public partial class KryptonOutlookGridSearchToolBar : ToolStrip
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
            this.button_close = new System.Windows.Forms.ToolStripButton();
            this.label_search = new System.Windows.Forms.ToolStripLabel();
            this.comboBox_columns = new System.Windows.Forms.ToolStripComboBox();
            this.textBox_search = new System.Windows.Forms.ToolStripTextBox();
            this.button_frombegin = new System.Windows.Forms.ToolStripButton();
            this.button_casesensitive = new System.Windows.Forms.ToolStripButton();
            this.button_search = new System.Windows.Forms.ToolStripButton();
            this.button_Filter = new System.Windows.Forms.ToolStripButton();
            this.button_wholeword = new System.Windows.Forms.ToolStripButton();
            this.separator_search = new System.Windows.Forms.ToolStripSeparator();
            this.SuspendLayout();
            // 
            // button_close
            // 
            this.button_close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_close.Image = OutlookGridImageResources.SearchToolBar_ButtonCaseSensitive;
            this.button_close.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_close.Name = "button_close";
            this.button_close.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.button_close.Size = new System.Drawing.Size(23, 24);
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label_search
            // 
            this.label_search.Name = "label_search";
            this.label_search.Size = new System.Drawing.Size(45, 15);
            // 
            // comboBox_columns
            // 
            this.comboBox_columns.AutoSize = false;
            this.comboBox_columns.AutoToolTip = true;
            this.comboBox_columns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_columns.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.comboBox_columns.IntegralHeight = false;
            this.comboBox_columns.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.comboBox_columns.MaxDropDownItems = 12;
            this.comboBox_columns.Name = "comboBox_columns";
            this.comboBox_columns.Size = new System.Drawing.Size(150, 23);
            // 
            // textBox_search
            // 
            this.textBox_search.AutoSize = false;
            this.textBox_search.ForeColor = System.Drawing.Color.LightGray;
            this.textBox_search.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.textBox_search.Size = new System.Drawing.Size(100, 23);
            this.textBox_search.Enter += new System.EventHandler(this.textBox_search_Enter);
            this.textBox_search.Leave += new System.EventHandler(this.textBox_search_Leave);
            this.textBox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_KeyDown);
            this.textBox_search.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
            // 
            // button_frombegin
            // 
            this.button_frombegin.CheckOnClick = true;
            this.button_frombegin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_frombegin.Image = OutlookGridImageResources.SearchToolBar_ButtonFromBegin;
            this.button_frombegin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_frombegin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_frombegin.Name = "button_frombegin";
            this.button_frombegin.Size = new System.Drawing.Size(23, 20);
            // 
            // button_casesensitive
            // 
            this.button_casesensitive.CheckOnClick = true;
            this.button_casesensitive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_casesensitive.Image = OutlookGridImageResources.SearchToolBar_ButtonCaseSensitive;
            this.button_casesensitive.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_casesensitive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_casesensitive.Name = "button_casesensitive";
            this.button_casesensitive.Size = new System.Drawing.Size(23, 20);
            // 
            // button_search
            // 
            this.button_search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_search.Image = OutlookGridImageResources.SearchToolBar_ButtonSearch;
            this.button_search.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_search.Name = "button_search";
            this.button_search.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.button_search.Size = new System.Drawing.Size(23, 24);
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // button_search
            // 
            this.button_Filter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_Filter.Image = OutlookGridImageResources.SearchToolBar_Filter;
            this.button_Filter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_Filter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_Filter.Name = "button_Filter";
            this.button_Filter.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.button_Filter.Size = new System.Drawing.Size(23, 24);
            this.button_Filter.Click += new System.EventHandler(this.ButtonFilter_Click);
            // 
            // button_wholeword
            // 
            this.button_wholeword.CheckOnClick = true;
            this.button_wholeword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_wholeword.Image = OutlookGridImageResources.SearchToolBar_ButtonWholeWord;
            this.button_wholeword.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_wholeword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_wholeword.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.button_wholeword.Name = "button_wholeword";
            this.button_wholeword.Size = new System.Drawing.Size(23, 20);
            // 
            // separator_search
            // 
            this.separator_search.AutoSize = false;
            this.separator_search.Name = "separator_search";
            this.separator_search.Size = new System.Drawing.Size(10, 25);
            // 
            // AdvancedDataGridViewSearchToolBar
            // 
            this.AllowMerge = false;
            this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button_close,
            this.label_search,
            this.comboBox_columns,
            this.textBox_search,
            this.button_frombegin,
            this.button_wholeword,
            this.button_casesensitive,
            this.separator_search,
            this.button_search,
            this.button_Filter});
            this.MaximumSize = new System.Drawing.Size(0, 27);
            this.MinimumSize = new System.Drawing.Size(0, 27);
            this.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.Size = new System.Drawing.Size(0, 27);
            this.Resize += new System.EventHandler(this.ResizeMe);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripButton button_close;
        private ToolStripLabel label_search;
        private ToolStripComboBox comboBox_columns;
        private ToolStripTextBox textBox_search;
        private ToolStripButton button_frombegin;
        private ToolStripButton button_casesensitive;
        private ToolStripButton button_search;
        private ToolStripButton button_wholeword;
        private ToolStripSeparator separator_search;

        private ToolStripButton button_Filter;

        #endregion

        #region public events

        public event KryptonOutlookGridSearchToolBarSearchEventHandler? Search;
        public event EventHandler? OnFilter;

        #endregion


        #region class properties

        private PaletteBase? _palette;
        private PaletteRedirect _paletteRedirect;
        private PaletteBorderInheritRedirect _paletteBorder;
        private PaletteBorder _border;

        private DataGridViewColumnCollection? _columnsList;

        private const bool BUTTON_CLOSE_ENABLED = false;

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the border should be painted.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool PaintBorder { get; set; } = false;

        #endregion Public Properties

        #region translations

        /// <summary>
        /// Available translation keys
        /// </summary>
        public enum TranslationKey
        {
            ADGVSTBLabelSearch,
            ADGVSTBButtonFromBegin,
            ADGVSTBButtonCaseSensitiveToolTip,
            ADGVSTBButtonSearchToolTip,
            ADGVSTBButtonCloseToolTip,
            ADGVSTBButtonWholeWordToolTip,
            ADGVSTBComboBoxColumnsAll,
            ADGVSTBTextBoxSearchToolTip
        }

        /// <summary>
        /// Internationalization strings
        /// </summary>
        public static Dictionary<string, string> Translations = new Dictionary<string, string>()
        {
            { TranslationKey.ADGVSTBLabelSearch.ToString(), "Search:" },
            { TranslationKey.ADGVSTBButtonFromBegin.ToString(), "From Begin" },
            { TranslationKey.ADGVSTBButtonCaseSensitiveToolTip.ToString(), "Case Sensitivity" },
            { TranslationKey.ADGVSTBButtonSearchToolTip.ToString(), "Find Next" },
            { TranslationKey.ADGVSTBButtonCloseToolTip.ToString(), "Hide" },
            { TranslationKey.ADGVSTBButtonWholeWordToolTip.ToString(), "Search only Whole Word" },
            { TranslationKey.ADGVSTBComboBoxColumnsAll.ToString(), "(All Columns)" },
            { TranslationKey.ADGVSTBTextBoxSearchToolTip.ToString(), "Value for Search" }
        };

        /// <summary>
        /// Used to check if components translations has to be updated
        /// </summary>
        private Dictionary<string, string> _translationsRefreshComponentTranslationsCheck = new Dictionary<string, string>() { };

        #endregion


        #region constructor

        /// <summary>Initializes a new instance of the <see cref="KryptonOutlookGridSearchToolBar" /> class.</summary>
        public KryptonOutlookGridSearchToolBar()
        {
            //initialize components
            InitializeComponent();

            RefreshComponentTranslations();

            //set default values
            if (!BUTTON_CLOSE_ENABLED)
            {
                Items.RemoveAt(0);
            }

            comboBox_columns!.SelectedIndex = 0;

            // Cache the current global palette setting
            _palette = KryptonManager.CurrentGlobalPalette;

            // Hook into palette events
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            // (4) We want to be notified whenever the global palette changes
            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            // Create redirection object to the base palette
            _paletteRedirect = new PaletteRedirect(_palette);
            _paletteBorder = new PaletteBorderInheritRedirect(_paletteRedirect);
            // Create storage that maps onto the inherit instances
            _border = new PaletteBorder(_paletteBorder, null);

            // Use Krypton
            RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        #endregion


        #region translations methods

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
            /*IDictionary<string, string> ret = new Dictionary<string, string>();

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

            return ret;*/
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Update components translations
        /// </summary>
        private void RefreshComponentTranslations()
        {
            comboBox_columns.BeginUpdate();
            comboBox_columns.Items.Clear();
            comboBox_columns.Items.AddRange([Translations[TranslationKey.ADGVSTBComboBoxColumnsAll.ToString()]]);
            if (_columnsList != null)
            {
                foreach (DataGridViewColumn c in _columnsList)
                {
                    if (c.Visible)
                    {
                        comboBox_columns.Items.Add(c.HeaderText);
                    }
                }
            }

            comboBox_columns.SelectedIndex = 0;
            comboBox_columns.EndUpdate();
            button_close.ToolTipText = Translations[TranslationKey.ADGVSTBButtonCloseToolTip.ToString()];
            label_search.Text = Translations[TranslationKey.ADGVSTBLabelSearch.ToString()];
            textBox_search.ToolTipText = Translations[TranslationKey.ADGVSTBTextBoxSearchToolTip.ToString()];
            button_frombegin.ToolTipText = Translations[TranslationKey.ADGVSTBButtonFromBegin.ToString()];
            button_casesensitive.ToolTipText = Translations[TranslationKey.ADGVSTBButtonCaseSensitiveToolTip.ToString()];
            button_search.ToolTipText = Translations[TranslationKey.ADGVSTBButtonSearchToolTip.ToString()];
            button_wholeword.ToolTipText = Translations[TranslationKey.ADGVSTBButtonWholeWordToolTip.ToString()];
            textBox_search.Text = textBox_search.ToolTipText;
        }

        #endregion


        #region button events

        /// <summary>
        /// button Search Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_search_Click(object? sender, EventArgs e)
        {
            if (textBox_search.TextLength > 0 && textBox_search.Text != textBox_search.ToolTipText && Search != null)
            {
                DataGridViewColumn? c = null;
                if (comboBox_columns.SelectedIndex > 0 && _columnsList != null && _columnsList.GetColumnCount(DataGridViewElementStates.Visible) > 0)
                {
                    DataGridViewColumn?[] cols = _columnsList.Cast<DataGridViewColumn>().Where(col => col.Visible).ToArray();

                    if (cols.Length == comboBox_columns.Items.Count - 1)
                    {
                        if (cols[comboBox_columns.SelectedIndex - 1]!.HeaderText == comboBox_columns.SelectedItem!.ToString())
                        {
                            c = cols[comboBox_columns.SelectedIndex - 1];
                        }
                    }
                }

                KryptonOutlookGridSearchToolBarSearchEventArgs args = new KryptonOutlookGridSearchToolBarSearchEventArgs(
                    textBox_search.Text,
                    c,
                    button_casesensitive.Checked,
                    button_wholeword.Checked,
                    button_frombegin.Checked
                );
                Search(this, args);
            }
        }

        void ButtonFilter_Click(object? sender, EventArgs e)
        {
            OnFilter?.Invoke(this, EventArgs.Empty);
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


        #region textbox search events

        /// <summary>
        /// textBox Search TextChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_search_TextChanged(object? sender, EventArgs e)
        {
            button_search.Enabled = textBox_search.TextLength > 0 && textBox_search.Text != textBox_search.ToolTipText;
        }


        /// <summary>
        /// textBox Search Enter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_search_Enter(object? sender, EventArgs e)
        {
            if (textBox_search.Text == textBox_search.ToolTipText && textBox_search.ForeColor == Color.LightGray)
            {
                textBox_search.Text = "";
            }
            else
            {
                textBox_search.SelectAll();
            }

            textBox_search.ForeColor = SystemColors.WindowText;
        }

        /// <summary>
        /// textBox Search Leave event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_search_Leave(object? sender, EventArgs e)
        {
            if (textBox_search.Text.Trim() == "")
            {
                textBox_search.Text = textBox_search.ToolTipText;
                textBox_search.ForeColor = Color.LightGray;
            }
        }


        /// <summary>
        /// textBox Search KeyDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_search_KeyDown(object? sender, KeyEventArgs e)
        {
            if (textBox_search.TextLength > 0 && textBox_search.Text != textBox_search.ToolTipText && e.KeyData == Keys.Enter)
            {
                button_search_Click(button_search, EventArgs.Empty);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        #endregion


        #region public methods

        /// <summary>
        /// Set Columns to search in
        /// </summary>
        /// <param name="columns"></param>
        public void SetColumns(DataGridViewColumnCollection columns)
        {
            _columnsList = columns;
            comboBox_columns.BeginUpdate();
            comboBox_columns.Items.Clear();
            comboBox_columns.Items.AddRange([Translations[TranslationKey.ADGVSTBComboBoxColumnsAll.ToString()]]);
            if (_columnsList != null)
            {
                foreach (DataGridViewColumn c in _columnsList)
                {
                    if (c.Visible)
                    {
                        comboBox_columns.Items.Add(c.HeaderText);
                    }
                }
            }

            comboBox_columns.SelectedIndex = 0;
            comboBox_columns.EndUpdate();
        }

        #endregion


        #region resize events

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
            int oldW = comboBox_columns.Width + textBox_search.Width;
            foreach (ToolStripItem c in Items)
            {
                c.Overflow = ToolStripItemOverflow.Never;
                c.Visible = true;
            }

            int width = PreferredSize.Width - oldW + w1 + w2;
            if (Width < width)
            {
                label_search.Visible = false;
                GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                width = PreferredSize.Width - oldW + w1 + w2;

                if (Width < width)
                {
                    button_casesensitive.Overflow = ToolStripItemOverflow.Always;
                    GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                    width = PreferredSize.Width - oldW + w1 + w2;
                }

                if (Width < width)
                {
                    button_wholeword.Overflow = ToolStripItemOverflow.Always;
                    GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                    width = PreferredSize.Width - oldW + w1 + w2;
                }

                if (Width < width)
                {
                    button_frombegin.Overflow = ToolStripItemOverflow.Always;
                    separator_search.Visible = false;
                    GetResizeBoxSize(PreferredSize.Width - oldW + w1 + w2, ref w1, ref w2);
                    width = PreferredSize.Width - oldW + w1 + w2;
                }

                if (Width < width)
                {
                    comboBox_columns.Overflow = ToolStripItemOverflow.Always;
                    textBox_search.Overflow = ToolStripItemOverflow.Always;
                    w1 = 150;
                    w2 = Math.Max(Width - PreferredSize.Width - textBox_search.Margin.Left - textBox_search.Margin.Right, 75);
                    textBox_search.Overflow = ToolStripItemOverflow.Never;
                    width = PreferredSize.Width - textBox_search.Width + w2;
                }
                if (Width < width)
                {
                    button_search.Overflow = ToolStripItemOverflow.Always;
                    w2 = Math.Max(Width - PreferredSize.Width + textBox_search.Width, 75);
                    width = PreferredSize.Width - textBox_search.Width + w2;
                }
                if (Width < width)
                {
                    button_close.Overflow = ToolStripItemOverflow.Always;
                    textBox_search.Margin = new Padding(8, 2, 8, 2);
                    w2 = Math.Max(Width - PreferredSize.Width + textBox_search.Width, 75);
                    width = PreferredSize.Width - textBox_search.Width + w2;
                }

                if (Width < width)
                {
                    w2 = Math.Max(Width - PreferredSize.Width + textBox_search.Width, 20);
                    width = PreferredSize.Width - textBox_search.Width + w2;
                }
                if (width > Width)
                {
                    textBox_search.Overflow = ToolStripItemOverflow.Always;
                    textBox_search.Margin = new Padding(0, 2, 8, 2);
                    w2 = 150;
                }
            }
            else
            {
                GetResizeBoxSize(width, ref w1, ref w2);
            }

            if (comboBox_columns.Width != w1)
            {
                comboBox_columns.Width = w1;
            }

            if (textBox_search.Width != w2)
            {
                textBox_search.Width = w2;
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


        #region paint events

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
            if (PaintBorder)
            {
                if (_palette != null)
                {
                    // Get the renderer associated with this palette
                    IRenderer renderer = _palette.GetRenderer();

                    // Create the rendering context that is passed into all renderer calls
                    using (RenderContext renderContext = new(this, e.Graphics, e.ClipRectangle, renderer))
                    {
                        _paletteBorder.Style = PaletteBorderStyle.HeaderPrimary;
                        renderer.RenderStandardBorder.DrawBorder(renderContext, ClientRectangle, _border, VisualOrientation.Top, PaletteState.Normal);
                    }
                }
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Handles OnPalettePaint Event
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PaletteLayoutEventArgs that contains the event data.</param>
        private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Handles OnGlobalPaletteChanged event
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        private void OnGlobalPaletteChanged(object? sender, EventArgs e)
        {
            // (5) Unhook events from old palette
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
            }

            // (6) Cache the new IPalette that is the global palette
            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect.Target = _palette; //!!!!!!

            // (7) Hook into events for the new palette
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            // (8) Change of palette means we should repaint to show any changes
            Invalidate();
        }

        #endregion

    }
}