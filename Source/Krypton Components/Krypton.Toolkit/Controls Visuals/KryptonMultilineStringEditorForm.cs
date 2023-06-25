#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2022 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public partial class KryptonMultilineStringEditorForm : KryptonForm
    {
        #region Instance Fields

        private bool _useRichTextBox;

        private string _headerText;

        private string[]? _contents;

        private StringCollection? _collection;

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether to use a <see cref="KryptonRichTextBox"/> in place of a multiline <see cref="KryptonTextBox"/>.</summary>
        /// <value><c>true</c> if [use rich text box]; otherwise, <c>false</c>.</value>
        [Category(@"Visuals"), DefaultValue(false), Description(@"Use a KryptonRichTextBox in place of a multiline KryptonTextBox.")]
        public bool UseRichTextBox { get => _useRichTextBox; set { _useRichTextBox = value; Invalidate(); } }

        /// <summary>Gets or sets the header text.</summary>
        /// <value>The header text.</value>
        [Category(@"Visuals"), DefaultValue(@"Enter the strings in the collection (one per line):"), Description(@"The text of the header label.")]
        public string HeaderText { get => _headerText; set { _headerText = value; Invalidate(); } }

        /// <summary>Gets the contents of the text field.</summary>
        /// <value>The contents of the text field.</value>
        [Category(@"Data"),
         DefaultValue(null),
         Description(@"The contents of the text field.")]
        public string[]? Contents
        {
            get => _contents;
            private set => _contents = value;
        }

        #endregion

        #region Identity

        public KryptonMultilineStringEditorForm()
        {
            InitializeComponent();

            InitialSetup();

            SetupControlsText();
        }

        public KryptonMultilineStringEditorForm(string[]? contents, StringCollection? collection = null, bool? useRichTextBox = true, string? headerText = @"Enter the strings in the collection (one per line):", string windowTitle = @"String Collection Editor")
        {
            InitializeComponent();

            SetupVariables(contents, collection, useRichTextBox, headerText, windowTitle);

            SetupControlsText();

            UpdateInput(contents, collection);
        }

        #endregion

        #region Implementation

        private void SetupControlsText()
        {
            kbtnCancel.Text = KryptonLanguageManager.GeneralToolkitStrings.Cancel;

            kbtnOk.Text = KryptonLanguageManager.GeneralToolkitStrings.OK;

            kcRichTextBoxCopy.Text = KryptonLanguageManager.GeneralToolkitStrings.Copy;

            kcRichTextBoxCut.Text = KryptonLanguageManager.GeneralToolkitStrings.Cut;

            kcRichTextBoxPaste.Text = KryptonLanguageManager.GeneralToolkitStrings.Paste;

            kcRichTextBoxSelectAll.Text = KryptonLanguageManager.GeneralToolkitStrings.SelectAll;

            kcTextBoxCopy.Text = KryptonLanguageManager.GeneralToolkitStrings.Copy;

            kcTextBoxCut.Text = KryptonLanguageManager.GeneralToolkitStrings.Cut;

            kcTextBoxPaste.Text = KryptonLanguageManager.GeneralToolkitStrings.Paste;

            kcTextBoxSelectAll.Text = KryptonLanguageManager.GeneralToolkitStrings.SelectAll;
        }

        private void SetupVariables(string[]? contents, StringCollection? collection, bool? useRichTextBox, string? headerText, string? windowTitle)
        {
            _contents = contents;

            _collection = collection;

            _useRichTextBox = useRichTextBox ?? true;

            _headerText = headerText ?? @"Enter the strings in the collection (one per line):";

            klblHeader.Text = _headerText;

            Text = windowTitle ?? @"String Collection Editor";
        }

        private void InitialSetup()
        {
            // Set default values

            _useRichTextBox = false;

            _headerText = @"Enter the strings in the collection (one per line):";

            _contents = null;

            _collection = null;
        }

        private void SetupIputCanvas()
        {
            if (_useRichTextBox)
            {
                ktxtStringCollection.Visible = false;

                krtbContents.Visible = true;
            }
            else
            {
                ktxtStringCollection.Visible = true;

                krtbContents.Visible = false;
            }
        }

        private void UpdateInput(string[]? contents, StringCollection? collection)
        {
            if (_useRichTextBox)
            {
                if (contents != null)
                {
                    krtbContents.Lines = contents;
                }
                else if (collection != null)
                {
                    string[] tmp = new string[collection.Count];

                    collection.CopyTo(tmp, 0);

                    krtbContents.Lines = tmp;
                }
            }
            else
            {
                if (contents != null)
                {
                    ktxtStringCollection.Lines = contents;
                }
                else if (collection != null)
                {
                    // Setup temporary string array
                    string[] tmpStrings = new string[collection.Count];

                    // Copy the contents of 'collection' into string array
                    collection.CopyTo(tmpStrings, 0);

                    ktxtStringCollection.Lines = tmpStrings;
                }
            }
        }

        private void kbtnOk_Click(object sender, EventArgs e)
        {
            if (_useRichTextBox)
            {
                foreach (string line in krtbContents.Lines)
                {
                    // TODO: This is not right.. It will only have the last line it !
                    Contents = new string[]
                    {
                        line
                    };
                }
            }
            else
            {
                foreach (string line in ktxtStringCollection.Lines)
                {
                    // TODO: This is not right.. It will only have the last line it !
                    Contents = new string[]
                    {
                        line
                    };
                }
            }
        }

        private void kcRichTextBoxCut_Execute(object sender, EventArgs e) => krtbContents.Cut();

        private void kcRichTextBoxCopy_Execute(object sender, EventArgs e) => krtbContents.Copy();

        private void kcRichTextBoxPaste_Execute(object sender, EventArgs e) => krtbContents.Paste();

        private void kcRichTextBoxSelectAll_Execute(object sender, EventArgs e) => krtbContents.SelectAll();

        private void kcTextBoxCut_Execute(object sender, EventArgs e) => ktxtStringCollection.Cut();

        private void kcTextBoxCopy_Execute(object sender, EventArgs e) => ktxtStringCollection.Copy();

        private void kcTextBoxPaste_Execute(object sender, EventArgs e) => ktxtStringCollection.Paste();

        private void kcTextBoxSelectAll_Execute(object sender, EventArgs e) => ktxtStringCollection.SelectAll();

        internal static string[]? InternalShow(IWin32Window? owner, string[] input, bool? useRichTextBox, string? headerText, string windowTitle)
        {
            string[]? collection;

            IWin32Window? showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            using KryptonMultilineStringEditorForm kmse = new(input, null, useRichTextBox, headerText, windowTitle);

            kmse.StartPosition = showOwner == null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

            collection = kmse._useRichTextBox ? kmse.krtbContents.Lines : kmse.ktxtStringCollection.Lines;

            return kmse.ShowDialog(showOwner) == DialogResult.OK ? collection : null;
        }

        internal static StringCollection? InternalShowStringCollection(IWin32Window? owner, StringCollection input, bool? useRichTextBox, string? headerText, string windowTitle)
        {
            StringCollection? collection;

            IWin32Window showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            using KryptonMultilineStringEditorForm kmse = new(null, input, useRichTextBox, headerText, windowTitle);

            kmse.StartPosition = showOwner == null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

            if (kmse._useRichTextBox)
            {
                collection = new();

                string[] tmp = kmse.krtbContents.Lines;

                collection.AddRange(tmp);
            }
            else
            {
                collection = new();

                string[] tmp = kmse.ktxtStringCollection.Lines;

                collection.AddRange(tmp);
            }

            return kmse.ShowDialog(showOwner) == DialogResult.OK ? collection : null;
        }

        #endregion
    }
}
