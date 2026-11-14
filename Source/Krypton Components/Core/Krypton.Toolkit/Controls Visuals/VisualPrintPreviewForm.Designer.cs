namespace Krypton.Toolkit
{
    partial class VisualPrintPreviewForm
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._toolbarPanel = new Krypton.Toolkit.KryptonPanel();
            this._btnClose = new Krypton.Toolkit.KryptonButton();
            this._lblPageInfo = new Krypton.Toolkit.KryptonLabel();
            this._btnSixPages = new Krypton.Toolkit.KryptonButton();
            this._btnFourPages = new Krypton.Toolkit.KryptonButton();
            this._btnThreePages = new Krypton.Toolkit.KryptonButton();
            this._btnTwoPages = new Krypton.Toolkit.KryptonButton();
            this._btnOnePage = new Krypton.Toolkit.KryptonButton();
            this._zoomTrackBar = new Krypton.Toolkit.KryptonTrackBar();
            this._btnZoomOut = new Krypton.Toolkit.KryptonButton();
            this._btnZoomIn = new Krypton.Toolkit.KryptonButton();
            this._btnPrint = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this._previewControl = new Krypton.Toolkit.KryptonPrintPreviewControl();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.klblZoomFactor = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this._toolbarPanel)).BeginInit();
            this._toolbarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolbarPanel
            // 
            this._toolbarPanel.Controls.Add(this._lblPageInfo);
            this._toolbarPanel.Controls.Add(this._btnSixPages);
            this._toolbarPanel.Controls.Add(this._btnFourPages);
            this._toolbarPanel.Controls.Add(this._btnThreePages);
            this._toolbarPanel.Controls.Add(this._btnTwoPages);
            this._toolbarPanel.Controls.Add(this._btnOnePage);
            this._toolbarPanel.Controls.Add(this._btnZoomOut);
            this._toolbarPanel.Controls.Add(this._btnZoomIn);
            this._toolbarPanel.Controls.Add(this._btnPrint);
            this._toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._toolbarPanel.Location = new System.Drawing.Point(0, 0);
            this._toolbarPanel.Name = "_toolbarPanel";
            this._toolbarPanel.Size = new System.Drawing.Size(800, 40);
            this._toolbarPanel.TabIndex = 0;
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnClose.Location = new System.Drawing.Point(713, 6);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(75, 25);
            this._btnClose.TabIndex = 9;
            this._btnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnClose.Values.Text = "Close";
            this._btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // _lblPageInfo
            // 
            this._lblPageInfo.Location = new System.Drawing.Point(656, 11);
            this._lblPageInfo.Name = "_lblPageInfo";
            this._lblPageInfo.Size = new System.Drawing.Size(100, 20);
            this._lblPageInfo.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this._lblPageInfo.TabIndex = 8;
            this._lblPageInfo.Values.Text = "Page 1 of 1";
            // 
            // _btnSixPages
            // 
            this._btnSixPages.Location = new System.Drawing.Point(575, 8);
            this._btnSixPages.Name = "_btnSixPages";
            this._btnSixPages.Size = new System.Drawing.Size(75, 25);
            this._btnSixPages.TabIndex = 7;
            this._btnSixPages.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnSixPages.Values.Text = "Six Pages";
            this._btnSixPages.Click += new System.EventHandler(this.BtnSixPages_Click);
            // 
            // _btnFourPages
            // 
            this._btnFourPages.Location = new System.Drawing.Point(494, 8);
            this._btnFourPages.Name = "_btnFourPages";
            this._btnFourPages.Size = new System.Drawing.Size(75, 25);
            this._btnFourPages.TabIndex = 6;
            this._btnFourPages.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnFourPages.Values.Text = "Four Pages";
            this._btnFourPages.Click += new System.EventHandler(this.BtnFourPages_Click);
            // 
            // _btnThreePages
            // 
            this._btnThreePages.Location = new System.Drawing.Point(413, 8);
            this._btnThreePages.Name = "_btnThreePages";
            this._btnThreePages.Size = new System.Drawing.Size(75, 25);
            this._btnThreePages.TabIndex = 5;
            this._btnThreePages.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnThreePages.Values.Text = "Three Pages";
            this._btnThreePages.Click += new System.EventHandler(this.BtnThreePages_Click);
            // 
            // _btnTwoPages
            // 
            this._btnTwoPages.Location = new System.Drawing.Point(332, 8);
            this._btnTwoPages.Name = "_btnTwoPages";
            this._btnTwoPages.Size = new System.Drawing.Size(75, 25);
            this._btnTwoPages.TabIndex = 4;
            this._btnTwoPages.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnTwoPages.Values.Text = "Two Pages";
            this._btnTwoPages.Click += new System.EventHandler(this.BtnTwoPages_Click);
            // 
            // _btnOnePage
            // 
            this._btnOnePage.Location = new System.Drawing.Point(251, 8);
            this._btnOnePage.Name = "_btnOnePage";
            this._btnOnePage.Size = new System.Drawing.Size(75, 25);
            this._btnOnePage.TabIndex = 3;
            this._btnOnePage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnOnePage.Values.Text = "One Page";
            this._btnOnePage.Click += new System.EventHandler(this.BtnOnePage_Click);
            // 
            // _zoomTrackBar
            // 
            this._zoomTrackBar.BackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this._zoomTrackBar.Location = new System.Drawing.Point(12, 6);
            this._zoomTrackBar.Maximum = 500;
            this._zoomTrackBar.Minimum = 25;
            this._zoomTrackBar.Name = "_zoomTrackBar";
            this._zoomTrackBar.Size = new System.Drawing.Size(247, 27);
            this._zoomTrackBar.TabIndex = 10;
            this._zoomTrackBar.Value = 100;
            this._zoomTrackBar.ValueChanged += new System.EventHandler(this.ZoomTrackBar_ValueChanged);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.Location = new System.Drawing.Point(170, 8);
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.Size = new System.Drawing.Size(75, 25);
            this._btnZoomOut.TabIndex = 2;
            this._btnZoomOut.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnZoomOut.Values.Text = "Zoom Out";
            this._btnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.Location = new System.Drawing.Point(89, 8);
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.Size = new System.Drawing.Size(75, 25);
            this._btnZoomIn.TabIndex = 1;
            this._btnZoomIn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnZoomIn.Values.Text = "Zoom In";
            this._btnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // _btnPrint
            // 
            this._btnPrint.Location = new System.Drawing.Point(8, 8);
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.Size = new System.Drawing.Size(75, 25);
            this._btnPrint.TabIndex = 0;
            this._btnPrint.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._btnPrint.Values.Text = "Print...";
            this._btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblZoomFactor);
            this.kryptonPanel1.Controls.Add(this._btnClose);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Controls.Add(this._zoomTrackBar);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 560);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 40);
            this.kryptonPanel1.TabIndex = 3;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this._previewControl);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 40);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(800, 520);
            this.kryptonPanel2.TabIndex = 4;
            // 
            // _previewControl
            // 
            this._previewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewControl.Location = new System.Drawing.Point(0, 0);
            this._previewControl.Name = "_previewControl";
            this._previewControl.Size = new System.Drawing.Size(800, 520);
            this._previewControl.TabIndex = 2;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(800, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // klblZoomFactor
            // 
            this.klblZoomFactor.Location = new System.Drawing.Point(266, 6);
            this.klblZoomFactor.Name = "klblZoomFactor";
            this.klblZoomFactor.Size = new System.Drawing.Size(90, 25);
            this.klblZoomFactor.TabIndex = 11;
            this.klblZoomFactor.Values.Text = "kryptonLabel1";
            // 
            // VisualPrintPreviewForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this._toolbarPanel);
            this.Name = "VisualPrintPreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Preview";
            ((System.ComponentModel.ISupportInitialize)(this._toolbarPanel)).EndInit();
            this._toolbarPanel.ResumeLayout(false);
            this._toolbarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel _toolbarPanel;
        private KryptonButton _btnPrint;
        private KryptonButton _btnZoomIn;
        private KryptonButton _btnZoomOut;
        private KryptonTrackBar _zoomTrackBar;
        private KryptonButton _btnOnePage;
        private KryptonButton _btnTwoPages;
        private KryptonButton _btnThreePages;
        private KryptonButton _btnFourPages;
        private KryptonButton _btnSixPages;
        private KryptonButton _btnClose;
        private KryptonLabel _lblPageInfo;
        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private KryptonPrintPreviewControl _previewControl;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonLabel klblZoomFactor;
    }
}
