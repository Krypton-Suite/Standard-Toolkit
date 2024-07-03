#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using Krypton.Toolkit;

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
            this.kbtnInputBox = new Krypton.Toolkit.KryptonButton();
            this.kbtnHeaderExamples = new Krypton.Toolkit.KryptonButton();
            this.kbtnDataGrid = new Krypton.Toolkit.KryptonButton();
            this.kbtnControlsTest = new Krypton.Toolkit.KryptonButton();
            this.kbtnThemeControls = new Krypton.Toolkit.KryptonButton();
            this.kbtnWorkspace = new Krypton.Toolkit.KryptonButton();
            this.kbtnCalendar = new Krypton.Toolkit.KryptonButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kbtnOutlookGrid = new Krypton.Toolkit.KryptonButton();
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
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kbtnAbout = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnAbout);
            this.kryptonPanel1.Controls.Add(this.kbtnInputBox);
            this.kryptonPanel1.Controls.Add(this.kbtnHeaderExamples);
            this.kryptonPanel1.Controls.Add(this.kbtnDataGrid);
            this.kryptonPanel1.Controls.Add(this.kbtnControlsTest);
            this.kryptonPanel1.Controls.Add(this.kbtnThemeControls);
            this.kryptonPanel1.Controls.Add(this.kbtnWorkspace);
            this.kryptonPanel1.Controls.Add(this.kbtnCalendar);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.kbtnOutlookGrid);
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
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(440, 452);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnInputBox
            // 
            this.kbtnInputBox.Location = new System.Drawing.Point(223, 195);
            this.kbtnInputBox.Name = "kbtnInputBox";
            this.kbtnInputBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnInputBox.TabIndex = 25;
            this.kbtnInputBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnInputBox.Values.Text = "Input Box";
            this.kbtnInputBox.Click += new System.EventHandler(this.kbtnInputBox_Click);
            // 
            // kbtnHeaderExamples
            // 
            this.kbtnHeaderExamples.Location = new System.Drawing.Point(13, 195);
            this.kbtnHeaderExamples.Name = "kbtnHeaderExamples";
            this.kbtnHeaderExamples.Size = new System.Drawing.Size(204, 25);
            this.kbtnHeaderExamples.TabIndex = 24;
            this.kbtnHeaderExamples.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnHeaderExamples.Values.Text = "Header Examples";
            this.kbtnHeaderExamples.Click += new System.EventHandler(this.kbtnHeaderExamples_Click);
            // 
            // kbtnDataGrid
            // 
            this.kbtnDataGrid.Location = new System.Drawing.Point(13, 133);
            this.kbtnDataGrid.Name = "kbtnDataGrid";
            this.kbtnDataGrid.Size = new System.Drawing.Size(204, 25);
            this.kbtnDataGrid.TabIndex = 23;
            this.kbtnDataGrid.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnDataGrid.Values.Text = "DataGrid";
            this.kbtnDataGrid.Click += new System.EventHandler(this.kbtnDataGrid_Click);
            // 
            // kbtnControlsTest
            // 
            this.kbtnControlsTest.Location = new System.Drawing.Point(223, 102);
            this.kbtnControlsTest.Name = "kbtnControlsTest";
            this.kbtnControlsTest.Size = new System.Drawing.Size(204, 25);
            this.kbtnControlsTest.TabIndex = 22;
            this.kbtnControlsTest.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnControlsTest.Values.Text = "Controls Test";
            this.kbtnControlsTest.Click += new System.EventHandler(this.kbtnControlsTest_Click);
            // 
            // kbtnThemeControls
            // 
            this.kbtnThemeControls.Location = new System.Drawing.Point(223, 319);
            this.kbtnThemeControls.Name = "kbtnThemeControls";
            this.kbtnThemeControls.Size = new System.Drawing.Size(204, 25);
            this.kbtnThemeControls.TabIndex = 21;
            this.kbtnThemeControls.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnThemeControls.Values.Text = "Theme Controls";
            this.kbtnThemeControls.Click += new System.EventHandler(this.kbtnThemeControls_Click);
            // 
            // kbtnWorkspace
            // 
            this.kbtnWorkspace.Location = new System.Drawing.Point(223, 381);
            this.kbtnWorkspace.Name = "kbtnWorkspace";
            this.kbtnWorkspace.Size = new System.Drawing.Size(204, 25);
            this.kbtnWorkspace.TabIndex = 20;
            this.kbtnWorkspace.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnWorkspace.Values.Text = "Workspace";
            this.kbtnWorkspace.Click += new System.EventHandler(this.kbtnWorkspace_Click);
            // 
            // kbtnCalendar
            // 
            this.kbtnCalendar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCalendar.Location = new System.Drawing.Point(223, 71);
            this.kbtnCalendar.Name = "kbtnCalendar";
            this.kbtnCalendar.Size = new System.Drawing.Size(204, 25);
            this.kbtnCalendar.TabIndex = 19;
            this.kbtnCalendar.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCalendar.Values.Text = "Calendar";
            this.kbtnCalendar.Click += new System.EventHandler(this.kbtnCalendar_Click);
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 417;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(10, 11);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(417, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 18;
            // 
            // kbtnOutlookGrid
            // 
            this.kbtnOutlookGrid.Location = new System.Drawing.Point(13, 257);
            this.kbtnOutlookGrid.Name = "kbtnOutlookGrid";
            this.kbtnOutlookGrid.Size = new System.Drawing.Size(204, 25);
            this.kbtnOutlookGrid.TabIndex = 17;
            this.kbtnOutlookGrid.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOutlookGrid.Values.Text = "Outlook Grid";
            this.kbtnOutlookGrid.Click += new System.EventHandler(this.kbtnOutlookGrid_Click);
            // 
            // kbtnTreeView
            // 
            this.kbtnTreeView.Location = new System.Drawing.Point(13, 381);
            this.kbtnTreeView.Name = "kbtnTreeView";
            this.kbtnTreeView.Size = new System.Drawing.Size(204, 25);
            this.kbtnTreeView.TabIndex = 16;
            this.kbtnTreeView.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTreeView.Values.Text = "TreeView";
            this.kbtnTreeView.Click += new System.EventHandler(this.kbtnTreeView_Click);
            // 
            // kbtnExit
            // 
            this.kbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnExit.Location = new System.Drawing.Point(13, 412);
            this.kbtnExit.Name = "kbtnExit";
            this.kbtnExit.Size = new System.Drawing.Size(204, 25);
            this.kbtnExit.TabIndex = 15;
            this.kbtnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExit.Values.Text = "Exit";
            this.kbtnExit.Click += new System.EventHandler(this.kbtnExit_Click);
            // 
            // kbtnFormBorder
            // 
            this.kbtnFormBorder.Location = new System.Drawing.Point(13, 164);
            this.kbtnFormBorder.Name = "kbtnFormBorder";
            this.kbtnFormBorder.Size = new System.Drawing.Size(204, 25);
            this.kbtnFormBorder.TabIndex = 14;
            this.kbtnFormBorder.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnFormBorder.Values.Text = "Form Border";
            this.kbtnFormBorder.Click += new System.EventHandler(this.kbtnFormBorder_Click);
            // 
            // kbtnToast
            // 
            this.kbtnToast.Location = new System.Drawing.Point(223, 350);
            this.kbtnToast.Name = "kbtnToast";
            this.kbtnToast.Size = new System.Drawing.Size(204, 25);
            this.kbtnToast.TabIndex = 13;
            this.kbtnToast.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToast.Values.Text = "Toast";
            this.kbtnToast.Click += new System.EventHandler(this.kbtnToast_Click);
            // 
            // kbtnTheme
            // 
            this.kbtnTheme.Location = new System.Drawing.Point(13, 350);
            this.kbtnTheme.Name = "kbtnTheme";
            this.kbtnTheme.Size = new System.Drawing.Size(204, 25);
            this.kbtnTheme.TabIndex = 12;
            this.kbtnTheme.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTheme.Values.Text = "Theme";
            this.kbtnTheme.Click += new System.EventHandler(this.kbtnTheme_Click);
            // 
            // kbtnTextBox
            // 
            this.kbtnTextBox.Location = new System.Drawing.Point(13, 319);
            this.kbtnTextBox.Name = "kbtnTextBox";
            this.kbtnTextBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnTextBox.TabIndex = 11;
            this.kbtnTextBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTextBox.Values.Text = "TextBox";
            this.kbtnTextBox.Click += new System.EventHandler(this.kbtnTextBox_Click);
            // 
            // kbtnRibbon
            // 
            this.kbtnRibbon.Location = new System.Drawing.Point(223, 288);
            this.kbtnRibbon.Name = "kbtnRibbon";
            this.kbtnRibbon.Size = new System.Drawing.Size(204, 25);
            this.kbtnRibbon.TabIndex = 10;
            this.kbtnRibbon.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRibbon.Values.Text = "Ribbon";
            this.kbtnRibbon.Click += new System.EventHandler(this.kbtnRibbon_Click);
            // 
            // kbtnProgressBar
            // 
            this.kbtnProgressBar.Location = new System.Drawing.Point(13, 288);
            this.kbtnProgressBar.Name = "kbtnProgressBar";
            this.kbtnProgressBar.Size = new System.Drawing.Size(204, 25);
            this.kbtnProgressBar.TabIndex = 9;
            this.kbtnProgressBar.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnProgressBar.Values.Text = "ProgressBar";
            this.kbtnProgressBar.Click += new System.EventHandler(this.kbtnProgressBar_Click);
            // 
            // kbtnButtons
            // 
            this.kbtnButtons.Location = new System.Drawing.Point(13, 71);
            this.kbtnButtons.Name = "kbtnButtons";
            this.kbtnButtons.Size = new System.Drawing.Size(204, 25);
            this.kbtnButtons.TabIndex = 8;
            this.kbtnButtons.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnButtons.Values.Text = "Buttons";
            this.kbtnButtons.Click += new System.EventHandler(this.kbtnButtons_Click);
            // 
            // kbtnAboutBox
            // 
            this.kbtnAboutBox.Location = new System.Drawing.Point(223, 257);
            this.kbtnAboutBox.Name = "kbtnAboutBox";
            this.kbtnAboutBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnAboutBox.TabIndex = 7;
            this.kbtnAboutBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAboutBox.Values.Text = "Old Style Main: \"Fullscreen\"";
            this.kbtnAboutBox.Click += new System.EventHandler(this.kbtnAboutBox_Click);
            // 
            // kbtnMessageBox
            // 
            this.kbtnMessageBox.Location = new System.Drawing.Point(223, 226);
            this.kbtnMessageBox.Name = "kbtnMessageBox";
            this.kbtnMessageBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnMessageBox.TabIndex = 6;
            this.kbtnMessageBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnMessageBox.Values.Text = "MessageBox";
            this.kbtnMessageBox.Click += new System.EventHandler(this.kbtnMessageBox_Click);
            // 
            // kbtnMenuToolStatusStrips
            // 
            this.kbtnMenuToolStatusStrips.Location = new System.Drawing.Point(13, 226);
            this.kbtnMenuToolStatusStrips.Name = "kbtnMenuToolStatusStrips";
            this.kbtnMenuToolStatusStrips.Size = new System.Drawing.Size(204, 25);
            this.kbtnMenuToolStatusStrips.TabIndex = 5;
            this.kbtnMenuToolStatusStrips.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnMenuToolStatusStrips.Values.Text = "Menu/Tool/Status Strips";
            this.kbtnMenuToolStatusStrips.Click += new System.EventHandler(this.kbtnMenuToolStatusStrips_Click);
            // 
            // kbtnGroupBox
            // 
            this.kbtnGroupBox.Location = new System.Drawing.Point(223, 164);
            this.kbtnGroupBox.Name = "kbtnGroupBox";
            this.kbtnGroupBox.Size = new System.Drawing.Size(204, 25);
            this.kbtnGroupBox.TabIndex = 4;
            this.kbtnGroupBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnGroupBox.Values.Text = "GroupBox";
            this.kbtnGroupBox.Click += new System.EventHandler(this.kbtnGroupBox_Click);
            // 
            // kbtnFadeForm
            // 
            this.kbtnFadeForm.Location = new System.Drawing.Point(223, 133);
            this.kbtnFadeForm.Name = "kbtnFadeForm";
            this.kbtnFadeForm.Size = new System.Drawing.Size(204, 25);
            this.kbtnFadeForm.TabIndex = 3;
            this.kbtnFadeForm.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnFadeForm.Values.Text = "Fade Form";
            this.kbtnFadeForm.Click += new System.EventHandler(this.kbtnFadeForm_Click);
            // 
            // kbtnCommandLinkButtons
            // 
            this.kbtnCommandLinkButtons.Location = new System.Drawing.Point(13, 102);
            this.kbtnCommandLinkButtons.Name = "kbtnCommandLinkButtons";
            this.kbtnCommandLinkButtons.Size = new System.Drawing.Size(204, 25);
            this.kbtnCommandLinkButtons.TabIndex = 2;
            this.kbtnCommandLinkButtons.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCommandLinkButtons.Values.Text = "CommandLink Buttons";
            this.kbtnCommandLinkButtons.Click += new System.EventHandler(this.kbtnCommandLinkButtons_Click);
            // 
            // kbtnBreadCrumb
            // 
            this.kbtnBreadCrumb.Location = new System.Drawing.Point(223, 40);
            this.kbtnBreadCrumb.Name = "kbtnBreadCrumb";
            this.kbtnBreadCrumb.Size = new System.Drawing.Size(204, 25);
            this.kbtnBreadCrumb.TabIndex = 1;
            this.kbtnBreadCrumb.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnBreadCrumb.Values.Text = "BreadCrumb";
            this.kbtnBreadCrumb.Click += new System.EventHandler(this.kbtnBreadCrumb_Click);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Copy = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Copy")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Cut = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Cut")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.New = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.New")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Open = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Open")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.PageSetup = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.PageSetup")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Paste = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Paste")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Print = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Print")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.PrintPreview = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.PrintPreview")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.QuickPrint = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.QuickPrint")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Redo = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Redo")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Save = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Save")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.SaveAll = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.SaveAll")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.SaveAs = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.SaveAs")));
            this.kryptonManager1.ToolkitImages.ToolbarImages.Undo = ((System.Drawing.Image)(resources.GetObject("kryptonManager1.ToolkitImages.ToolbarImages.Undo")));
            // 
            // kbtnAbout
            // 
            this.kbtnAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnAbout.Location = new System.Drawing.Point(12, 40);
            this.kbtnAbout.Name = "kbtnAbout";
            this.kbtnAbout.Size = new System.Drawing.Size(204, 25);
            this.kbtnAbout.TabIndex = 26;
            this.kbtnAbout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAbout.Values.Text = "About Box";
            this.kbtnAbout.Click += new System.EventHandler(this.kbtnAbout_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.kbtnExit;
            this.ClientSize = new System.Drawing.Size(440, 452);
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
        private Krypton.Toolkit.KryptonButton kbtnAboutBox;
        private Krypton.Toolkit.KryptonButton kbtnButtons;
        private Krypton.Toolkit.KryptonButton kbtnRibbon;
        private Krypton.Toolkit.KryptonButton kbtnProgressBar;
        private Krypton.Toolkit.KryptonButton kbtnTheme;
        private Krypton.Toolkit.KryptonButton kbtnTextBox;
        private Krypton.Toolkit.KryptonButton kbtnFormBorder;
        private Krypton.Toolkit.KryptonButton kbtnToast;
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private Krypton.Toolkit.KryptonButton kbtnTreeView;
        private Krypton.Toolkit.KryptonButton kbtnOutlookGrid;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private KryptonButton kbtnCalendar;
        private KryptonManager kryptonManager1;
        private KryptonButton kbtnWorkspace;
        private KryptonButton kbtnThemeControls;
        private KryptonButton kbtnControlsTest;
        private KryptonButton kbtnDataGrid;
        private KryptonButton kbtnInputBox;
        private KryptonButton kbtnHeaderExamples;
        private KryptonButton kbtnAbout;
    }
}