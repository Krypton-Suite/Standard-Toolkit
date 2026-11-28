namespace TestForm
{
    partial class AccessibilityTest
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
            this._mainPanel = new Krypton.Toolkit.KryptonPanel();
            this._splitContainer = new Krypton.Toolkit.KryptonSplitContainer();
            this._resultsPanel = new Krypton.Toolkit.KryptonPanel();
            this._txtResults = new Krypton.Toolkit.KryptonRichTextBox();
            this.resultsHeader = new Krypton.Toolkit.KryptonPanel();
            this._btnExportResults = new Krypton.Toolkit.KryptonButton();
            this._btnClearResults = new Krypton.Toolkit.KryptonButton();
            this._btnRunTests = new Krypton.Toolkit.KryptonButton();
            this._lblStatus = new Krypton.Toolkit.KryptonLabel();
            this.lblResults = new Krypton.Toolkit.KryptonLabel();
            this.headerPanel = new Krypton.Toolkit.KryptonPanel();
            this._lblFramework = new Krypton.Toolkit.KryptonLabel();
            this.lblDescription = new Krypton.Toolkit.KryptonLabel();
            this.lblTitle = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this._mainPanel)).BeginInit();
            this._mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel1)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel2)).BeginInit();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._resultsPanel)).BeginInit();
            this._resultsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsHeader)).BeginInit();
            this.resultsHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._splitContainer);
            this._mainPanel.Controls.Add(this.headerPanel);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Padding = new System.Windows.Forms.Padding(10);
            this._mainPanel.Size = new System.Drawing.Size(1200, 800);
            this._mainPanel.TabIndex = 0;
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(10, 90);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._txtResults);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._resultsPanel);
            this._splitContainer.Size = new System.Drawing.Size(1180, 700);
            this._splitContainer.SplitterDistance = 500;
            this._splitContainer.TabIndex = 1;
            // 
            // _resultsPanel
            // 
            this._resultsPanel.Controls.Add(this._txtResults);
            this._resultsPanel.Controls.Add(this.resultsHeader);
            this._resultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultsPanel.Location = new System.Drawing.Point(0, 0);
            this._resultsPanel.Name = "_resultsPanel";
            this._resultsPanel.Padding = new System.Windows.Forms.Padding(5);
            this._resultsPanel.Size = new System.Drawing.Size(1180, 196);
            this._resultsPanel.TabIndex = 0;
            // 
            // _txtResults
            // 
            this._txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._txtResults.Location = new System.Drawing.Point(5, 55);
            this._txtResults.Margin = new System.Windows.Forms.Padding(5);
            this._txtResults.Name = "_txtResults";
            this._txtResults.ReadOnly = true;
            this._txtResults.Size = new System.Drawing.Size(1170, 136);
            this._txtResults.TabIndex = 1;
            this._txtResults.Text = "";
            // 
            // resultsHeader
            // 
            this.resultsHeader.Controls.Add(this._btnExportResults);
            this.resultsHeader.Controls.Add(this._btnClearResults);
            this.resultsHeader.Controls.Add(this._btnRunTests);
            this.resultsHeader.Controls.Add(this._lblStatus);
            this.resultsHeader.Controls.Add(this.lblResults);
            this.resultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.resultsHeader.Location = new System.Drawing.Point(5, 5);
            this.resultsHeader.Name = "resultsHeader";
            this.resultsHeader.Padding = new System.Windows.Forms.Padding(5);
            this.resultsHeader.Size = new System.Drawing.Size(1170, 50);
            this.resultsHeader.TabIndex = 0;
            // 
            // _btnExportResults
            // 
            this._btnExportResults.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnExportResults.Location = new System.Drawing.Point(1010, 5);
            this._btnExportResults.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._btnExportResults.Name = "_btnExportResults";
            this._btnExportResults.Size = new System.Drawing.Size(80, 40);
            this._btnExportResults.TabIndex = 4;
            this._btnExportResults.Values.Text = "Export";
            this._btnExportResults.Click += new System.EventHandler(this.BtnExportResults_Click);
            // 
            // _btnClearResults
            // 
            this._btnClearResults.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnClearResults.Location = new System.Drawing.Point(930, 5);
            this._btnClearResults.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._btnClearResults.Name = "_btnClearResults";
            this._btnClearResults.Size = new System.Drawing.Size(80, 40);
            this._btnClearResults.TabIndex = 3;
            this._btnClearResults.Values.Text = "Clear";
            this._btnClearResults.Click += new System.EventHandler(this.BtnClearResults_Click);
            // 
            // _btnRunTests
            // 
            this._btnRunTests.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnRunTests.Location = new System.Drawing.Point(830, 5);
            this._btnRunTests.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._btnRunTests.Name = "_btnRunTests";
            this._btnRunTests.Size = new System.Drawing.Size(100, 40);
            this._btnRunTests.TabIndex = 2;
            this._btnRunTests.Values.Text = "Run Tests";
            this._btnRunTests.Click += new System.EventHandler(this.BtnRunTests_Click);
            // 
            // _lblStatus
            // 
            this._lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this._lblStatus.Location = new System.Drawing.Point(160, 5);
            this._lblStatus.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(200, 40);
            this._lblStatus.TabIndex = 1;
            this._lblStatus.Values.Text = "Ready";
            // 
            // lblResults
            // 
            this.lblResults.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResults.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblResults.Location = new System.Drawing.Point(5, 5);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(155, 40);
            this.lblResults.TabIndex = 0;
            this.lblResults.Values.Text = "Test Results:";
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this._lblFramework);
            this.headerPanel.Controls.Add(this.lblDescription);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(10, 10);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(10);
            this.headerPanel.Size = new System.Drawing.Size(1180, 80);
            this.headerPanel.TabIndex = 0;
            // 
            // _lblFramework
            // 
            this._lblFramework.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblFramework.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this._lblFramework.Location = new System.Drawing.Point(10, 70);
            this._lblFramework.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._lblFramework.Name = "_lblFramework";
            this._lblFramework.Size = new System.Drawing.Size(1160, 20);
            this._lblFramework.TabIndex = 2;
            this._lblFramework.Values.Text = "";
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Location = new System.Drawing.Point(10, 30);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(1160, 40);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Values.Text = "Comprehensive demonstration and testing of UIA Provider support for Krypton controls that wrap standard Windows Forms controls.";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1160, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Values.Text = "UIA Provider Accessibility Implementation Demo";
            // 
            // AccessibilityTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this._mainPanel);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "AccessibilityTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIA Provider Accessibility Demo - Issue #762";
            ((System.ComponentModel.ISupportInitialize)(this._mainPanel)).EndInit();
            this._mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel1)).EndInit();
            this._splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel2)).EndInit();
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._resultsPanel)).EndInit();
            this._resultsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsHeader)).EndInit();
            this.resultsHeader.ResumeLayout(false);
            this.resultsHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel _mainPanel;
        private Krypton.Toolkit.KryptonSplitContainer _splitContainer;
        private Krypton.Toolkit.KryptonPanel _resultsPanel;
        private Krypton.Toolkit.KryptonRichTextBox _txtResults;
        private Krypton.Toolkit.KryptonPanel resultsHeader;
        private Krypton.Toolkit.KryptonButton _btnExportResults;
        private Krypton.Toolkit.KryptonButton _btnClearResults;
        private Krypton.Toolkit.KryptonButton _btnRunTests;
        private Krypton.Toolkit.KryptonLabel _lblStatus;
        private Krypton.Toolkit.KryptonLabel lblResults;
        private Krypton.Toolkit.KryptonPanel headerPanel;
        private Krypton.Toolkit.KryptonLabel _lblFramework;
        private Krypton.Toolkit.KryptonLabel lblDescription;
        private Krypton.Toolkit.KryptonLabel lblTitle;
    }
}