namespace Krypton.Toolkit
{
    public partial class KryptonStringCollectionEditor : KryptonForm
    {
        #region Design Code

        private InternalKryptonStringCollectionEditor iksceEditor;
        private KryptonCommand kcHelpIconProvider;
        private ButtonSpecAny bsHelpIcon;

        private void InitializeComponent()
        {
            this.iksceEditor = new Krypton.Toolkit.InternalKryptonStringCollectionEditor();
            this.bsHelpIcon = new Krypton.Toolkit.ButtonSpecAny();
            this.kcHelpIconProvider = new Krypton.Toolkit.KryptonCommand();
            this.SuspendLayout();
            // 
            // iksceEditor
            // 
            this.iksceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iksceEditor.Location = new System.Drawing.Point(0, 0);
            this.iksceEditor.Name = "iksceEditor";
            this.iksceEditor.Owner = this;
            this.iksceEditor.Size = new System.Drawing.Size(586, 504);
            this.iksceEditor.TabIndex = 0;
            this.iksceEditor.UseRichTextBox = true;
            // 
            // bsHelpIcon
            // 
            this.bsHelpIcon.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.bsHelpIcon.KryptonCommand = this.kcHelpIconProvider;
            this.bsHelpIcon.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormHelp;
            this.bsHelpIcon.UniqueName = "1dd64653256748d5a5d96bd1e0e66a80";
            // 
            // KryptonStringCollectionEditor
            // 
            this.ButtonSpecs.AddRange(new Krypton.Toolkit.ButtonSpecAny[] {
            this.bsHelpIcon});
            this.ClientSize = new System.Drawing.Size(586, 504);
            this.Controls.Add(this.iksceEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonStringCollectionEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "String Collection Editor";
            this.ResumeLayout(false);

        }

        #endregion

        #region Instance Fields

        private bool _useTextBox;

        private bool _useRichTextBox;

        private string _headerText;

        private string _okButtonText;

        private string _cancelButtonText;

        private string[] _contents;

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether to use a multiline <see cref="KryptonTextBox"/> in place of a <see cref="KryptonRichTextBox"/>.</summary>
        /// <value><c>true</c> if [use text box]; otherwise, <c>false</c>.</value>
        [Category(@"Visuals"), DefaultValue(true), Description(@"Use a multiline KryptonTextBox in place of a KryptonRichTextBox.")]
        public bool UseTextBox { get => _useTextBox; set { _useTextBox = value; Invalidate(); } }

        /// <summary>Gets or sets a value indicating whether to use a <see cref="KryptonRichTextBox"/> in place of a multiline <see cref="KryptonTextBox"/>.</summary>
        /// <value><c>true</c> if [use rich text box]; otherwise, <c>false</c>.</value>
        [Category(@"Visuals"), DefaultValue(false), Description(@"Use a KryptonRichTextBox in place of a multiline KryptonTextBox.")]
        public bool UseRichTextBox { get => _useRichTextBox; set { _useRichTextBox = value; Invalidate(); } }

        /// <summary>Gets or sets the header text.</summary>
        /// <value>The header text.</value>
        [Category(@"Visuals"), DefaultValue(@"Enter the strings in the collection (one per line):"), Description(@"The text of the header label.")]
        public string HeaderText { get => _headerText; set { _headerText = value; Invalidate(); } }

        /// <summary>Gets or sets the ok button text.</summary>
        /// <value>The ok button text.</value>
        [Category(@"Visuals"), DefaultValue(@"O&K"), Description(@"The OK button text.")]
        public string OkButtonText { get => _okButtonText; set { _okButtonText = value; Invalidate(); } }

        /// <summary>Gets or sets the cancel button text.</summary>
        /// <value>The cancel button text.</value>
        [Category(@"Visuals"), DefaultValue(@"C&ancel"), Description(@"The cancel button text.")]
        public string CancelButtonText { get => _cancelButtonText; set { _cancelButtonText = value; Invalidate(); } }

        /// <summary>Gets the contents of the text field.</summary>
        /// <value>The contents of the text field.</value>
        [Category(@"Data"), DefaultValue(null), Description(@"The contents of the text field.")]
        public string[] Contents { get => _contents; private set => _contents = value; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonStringCollectionEditor" /> class.</summary>
        public KryptonStringCollectionEditor()
        {
            InitializeComponent();

            AcceptButton = iksceEditor.OkButton;

            CancelButton = iksceEditor.CancelButton;

            // Feed information through
            iksceEditor.UseRichTextBox = _useRichTextBox;

            iksceEditor.UseTextBox = _useTextBox;

            iksceEditor.CancelButtonText = _cancelButtonText;

            iksceEditor.OkButtonText = _okButtonText;

            iksceEditor.HeaderText = _headerText;
        }

        #endregion
    }
}