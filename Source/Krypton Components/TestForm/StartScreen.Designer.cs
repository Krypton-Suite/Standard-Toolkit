namespace TestForm
{
    partial class StartScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnTreeView = new Krypton.Toolkit.KryptonButton();
            this.kbtnExit = new Krypton.Toolkit.KryptonButton();
            this.kbtnFormBorder = new Krypton.Toolkit.KryptonButton();
            this.kbtnToast = new Krypton.Toolkit.KryptonButton();
            this.kbtnTheme = new Krypton.Toolkit.KryptonButton();
            this.kbtnTextBox = new Krypton.Toolkit.KryptonButton();
            this.kbtnRibbon = new Krypton.Toolkit.KryptonButton();
            this.kbtnProgressBar = new Krypton.Toolkit.KryptonButton();
            this.kbtnButtons = new Krypton.Toolkit.KryptonButton();
            this.kbtnAboutBox = new Krypton.Toolkit.KryptonButton();
            this.kbtnMessageBox = new Krypton.Toolkit.KryptonButton();
            this.kbtnMenuToolStatusStrips = new Krypton.Toolkit.KryptonButton();
            this.kbtnGroupBox = new Krypton.Toolkit.KryptonButton();
            this.kbtnFadeForm = new Krypton.Toolkit.KryptonButton();
            this.kbtnCommandLinkButtons = new Krypton.Toolkit.KryptonButton();
            this.kbtnBreadCrumb = new Krypton.Toolkit.KryptonButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTreeView);
            this.kryptonPanel1.Controls.Add(this.kbtnExit);
            this.kryptonPanel1.Controls.Add(this.kbtnFormBorder);
            this.kryptonPanel1.Controls.Add(this.kbtnToast);
            this.kryptonPanel1.Controls.Add(this.kbtnTheme);
            this.kryptonPanel1.Controls.Add(this.kbtnTextBox);
            this.kryptonPanel1.Controls.Add(this.kbtnRibbon);
            this.kryptonPanel1.Controls.Add(this.kbtnProgressBar);
            this.kryptonPanel1.Controls.Add(this.kbtnButtons);
            this.kryptonPanel1.Controls.Add(this.kbtnAboutBox);
            this.kryptonPanel1.Controls.Add(this.kbtnMessageBox);
            this.kryptonPanel1.Controls.Add(this.kbtnMenuToolStatusStrips);
            this.kryptonPanel1.Controls.Add(this.kbtnGroupBox);
            this.kryptonPanel1.Controls.Add(this.kbtnFadeForm);
            this.kryptonPanel1.Controls.Add(this.kbtnCommandLinkButtons);
            this.kryptonPanel1.Controls.Add(this.kbtnBreadCrumb);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(451, 310);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnTreeView
            // 
            this.kbtnTreeView.Location = new System.Drawing.Point(13, 257);
            this.kbtnTreeView.Name = "kbtnTreeView";
            this.kbtnTreeView.Size = new System.Drawing.Size(204, 25);
            this.kbtnTreeView.TabIndex = 16;
            this.kbtnTreeView.Values.Text = "TreeView";
            this.kbtnTreeView.Click += new System.EventHandler(this.kbtnTreeView_Click);
            // 
            // kbtnExit
            // 
            this.kbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnExit.Location = new System.Drawing.Point(223, 257);
            this.kbtnExit.Name = "kbtnExit";
            this.kbtnExit.Size = new System.Drawing.Size(204, 25);
            this.kbtnExit.TabIndex = 15;
            this.kbtnExit.Values.Text = "Exit";
            this.kbtnExit.Click += new System.EventHandler(this.kbtnExit_Click);
            // 
            // kbtnFormBorder
            // 
            this.kbtnFormBorder.Location = new System.Drawing.Point(223, 102);
            this.kbtnFormBorder.Name = "kbtnFormBorder";
            this.kbtnFormBorder.Size = new System.Drawing.Size(204, 25);
            this.kbtnFormBorder.TabIndex = 14;
            this.kbtnFormBorder.Values.Text = "Form Border";
            this.kbtnFormBorder.Click += new System.EventHandler(this.kbtnFormBorder_Click);
            // 
            // kbtnToast
            // 
            this.kbtnToast.Location = new System.Drawing.Point(223, 226);
            this.kbtnToast.Name = "kbtnToast";
            this.kbtnToast.Size = new System.Drawing.Size(204, 25);
            this.kbtnToast.TabIndex = 13;
            this.kbtnToast.Values.Text = "Toast";
            this.kbtnToast.Click += new System.EventHandler(this.kbtnToast_Click);
            // 
            // kbtnTheme
            // 
            this.kbtnTheme.Location = new System.Drawing.Point(13, 226);
            this.kbtnTheme.Name = "kbtnTheme";
            this.kbtnTheme.Size = new System.Drawing.Size(204, 25);
            this.kbtnTheme.TabIndex = 12;
            this.kbtnTheme.Values.Text = "Theme";
            this.kbtnTheme.Click += new System.EventHandler(this.kbtnTheme_Click);
            // 
            // kbtnTextBox
            // 
            this.kbtnTextBox.Location = new System.Drawing.Point(223, 195);
            this.kbtnTextBox.Name = "kbtnTextBox";
            this.kbtnTextBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnTextBox.TabIndex = 11;
            this.kbtnTextBox.Values.Text = "TextBox";
            this.kbtnTextBox.Click += new System.EventHandler(this.kbtnTextBox_Click);
            // 
            // kbtnRibbon
            // 
            this.kbtnRibbon.Location = new System.Drawing.Point(13, 195);
            this.kbtnRibbon.Name = "kbtnRibbon";
            this.kbtnRibbon.Size = new System.Drawing.Size(204, 25);
            this.kbtnRibbon.TabIndex = 10;
            this.kbtnRibbon.Values.Text = "Ribbon";
            this.kbtnRibbon.Click += new System.EventHandler(this.kbtnRibbon_Click);
            // 
            // kbtnProgressBar
            // 
            this.kbtnProgressBar.Location = new System.Drawing.Point(223, 164);
            this.kbtnProgressBar.Name = "kbtnProgressBar";
            this.kbtnProgressBar.Size = new System.Drawing.Size(204, 25);
            this.kbtnProgressBar.TabIndex = 9;
            this.kbtnProgressBar.Values.Text = "ProgressBar";
            this.kbtnProgressBar.Click += new System.EventHandler(this.kbtnProgressBar_Click);
            // 
            // kbtnButtons
            // 
            this.kbtnButtons.Location = new System.Drawing.Point(13, 71);
            this.kbtnButtons.Name = "kbtnButtons";
            this.kbtnButtons.Size = new System.Drawing.Size(204, 25);
            this.kbtnButtons.TabIndex = 8;
            this.kbtnButtons.Values.Text = "Buttons";
            this.kbtnButtons.Click += new System.EventHandler(this.kbtnButtons_Click);
            // 
            // kbtnAboutBox
            // 
            this.kbtnAboutBox.Location = new System.Drawing.Point(13, 40);
            this.kbtnAboutBox.Name = "kbtnAboutBox";
            this.kbtnAboutBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnAboutBox.TabIndex = 7;
            this.kbtnAboutBox.Values.Text = "About Box";
            this.kbtnAboutBox.Click += new System.EventHandler(this.kbtnAboutBox_Click);
            // 
            // kbtnMessageBox
            // 
            this.kbtnMessageBox.Location = new System.Drawing.Point(13, 164);
            this.kbtnMessageBox.Name = "kbtnMessageBox";
            this.kbtnMessageBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnMessageBox.TabIndex = 6;
            this.kbtnMessageBox.Values.Text = "MessageBox";
            this.kbtnMessageBox.Click += new System.EventHandler(this.kbtnMessageBox_Click);
            // 
            // kbtnMenuToolStatusStrips
            // 
            this.kbtnMenuToolStatusStrips.Location = new System.Drawing.Point(223, 133);
            this.kbtnMenuToolStatusStrips.Name = "kbtnMenuToolStatusStrips";
            this.kbtnMenuToolStatusStrips.Size = new System.Drawing.Size(204, 25);
            this.kbtnMenuToolStatusStrips.TabIndex = 5;
            this.kbtnMenuToolStatusStrips.Values.Text = "Menu/Tool/Status Strips";
            this.kbtnMenuToolStatusStrips.Click += new System.EventHandler(this.kbtnMenuToolStatusStrips_Click);
            // 
            // kbtnGroupBox
            // 
            this.kbtnGroupBox.Location = new System.Drawing.Point(13, 133);
            this.kbtnGroupBox.Name = "kbtnGroupBox";
            this.kbtnGroupBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnGroupBox.TabIndex = 4;
            this.kbtnGroupBox.Values.Text = "GroupBox";
            this.kbtnGroupBox.Click += new System.EventHandler(this.kbtnGroupBox_Click);
            // 
            // kbtnFadeForm
            // 
            this.kbtnFadeForm.Location = new System.Drawing.Point(13, 102);
            this.kbtnFadeForm.Name = "kbtnFadeForm";
            this.kbtnFadeForm.Size = new System.Drawing.Size(204, 25);
            this.kbtnFadeForm.TabIndex = 3;
            this.kbtnFadeForm.Values.Text = "Fade Form";
            this.kbtnFadeForm.Click += new System.EventHandler(this.kbtnFadeForm_Click);
            // 
            // kbtnCommandLinkButtons
            // 
            this.kbtnCommandLinkButtons.Location = new System.Drawing.Point(223, 71);
            this.kbtnCommandLinkButtons.Name = "kbtnCommandLinkButtons";
            this.kbtnCommandLinkButtons.Size = new System.Drawing.Size(204, 25);
            this.kbtnCommandLinkButtons.TabIndex = 2;
            this.kbtnCommandLinkButtons.Values.Text = "CommandLink Buttons";
            this.kbtnCommandLinkButtons.Click += new System.EventHandler(this.kbtnCommandLinkButtons_Click);
            // 
            // kbtnBreadCrumb
            // 
            this.kbtnBreadCrumb.Location = new System.Drawing.Point(223, 40);
            this.kbtnBreadCrumb.Name = "kbtnBreadCrumb";
            this.kbtnBreadCrumb.Size = new System.Drawing.Size(204, 25);
            this.kbtnBreadCrumb.TabIndex = 1;
            this.kbtnBreadCrumb.Values.Text = "BreadCrumb";
            this.kbtnBreadCrumb.Click += new System.EventHandler(this.kbtnBreadCrumb_Click);
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 414;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(13, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.ReportSelectedThemeIndex = false;
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(414, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleOrange;
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnExit;
            this.ClientSize = new System.Drawing.Size(451, 310);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.Load += new System.EventHandler(this.StartScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnMessageBox;
        private Krypton.Toolkit.KryptonButton kbtnMenuToolStatusStrips;
        private Krypton.Toolkit.KryptonButton kbtnGroupBox;
        private Krypton.Toolkit.KryptonButton kbtnFadeForm;
        private Krypton.Toolkit.KryptonButton kbtnCommandLinkButtons;
        private Krypton.Toolkit.KryptonButton kbtnBreadCrumb;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonButton kbtnAboutBox;
        private Krypton.Toolkit.KryptonButton kbtnButtons;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonButton kbtnRibbon;
        private Krypton.Toolkit.KryptonButton kbtnProgressBar;
        private Krypton.Toolkit.KryptonButton kbtnTheme;
        private Krypton.Toolkit.KryptonButton kbtnTextBox;
        private Krypton.Toolkit.KryptonButton kbtnFormBorder;
        private Krypton.Toolkit.KryptonButton kbtnToast;
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private Krypton.Toolkit.KryptonButton kbtnTreeView;
    }
}