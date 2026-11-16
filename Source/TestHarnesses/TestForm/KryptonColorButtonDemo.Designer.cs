namespace TestForm
{
    partial class KryptonColorButtonDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new Krypton.Toolkit.KryptonPanel();
            this.lblDescription = new Krypton.Toolkit.KryptonLabel();
            this.grpMain = new Krypton.Toolkit.KryptonGroupBox();
            this.lblSelected = new Krypton.Toolkit.KryptonLabel();
            this.grpLivePreview = new Krypton.Toolkit.KryptonGroupBox();
            this.lblLivePreview = new Krypton.Toolkit.KryptonLabel();
            this.btnLivePreview = new Krypton.Toolkit.KryptonColorButton();
            this.btnCommittedOnly = new Krypton.Toolkit.KryptonColorButton();
            this.pnlLivePreview = new Krypton.Toolkit.KryptonPanel();
            this.grpNoMoreColors = new Krypton.Toolkit.KryptonGroupBox();
            this.btnNoMoreColors = new Krypton.Toolkit.KryptonColorButton();
            this.grpMaxCustom = new Krypton.Toolkit.KryptonGroupBox();
            this.btnMaxCustom = new Krypton.Toolkit.KryptonColorButton();
            this.grpCustomAndBuiltIn = new Krypton.Toolkit.KryptonGroupBox();
            this.btnCustomAndBuiltIn = new Krypton.Toolkit.KryptonColorButton();
            this.grpOnlyCustom = new Krypton.Toolkit.KryptonGroupBox();
            this.btnOnlyCustom = new Krypton.Toolkit.KryptonColorButton();
            this.grpDefault = new Krypton.Toolkit.KryptonGroupBox();
            this.btnDefault = new Krypton.Toolkit.KryptonColorButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).BeginInit();
            this.grpMain.Panel.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLivePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLivePreview.Panel)).BeginInit();
            this.grpLivePreview.Panel.SuspendLayout();
            this.grpLivePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLivePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNoMoreColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNoMoreColors.Panel)).BeginInit();
            this.grpNoMoreColors.Panel.SuspendLayout();
            this.grpNoMoreColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMaxCustom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMaxCustom.Panel)).BeginInit();
            this.grpMaxCustom.Panel.SuspendLayout();
            this.grpMaxCustom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCustomAndBuiltIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCustomAndBuiltIn.Panel)).BeginInit();
            this.grpCustomAndBuiltIn.Panel.SuspendLayout();
            this.grpCustomAndBuiltIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpOnlyCustom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOnlyCustom.Panel)).BeginInit();
            this.grpOnlyCustom.Panel.SuspendLayout();
            this.grpOnlyCustom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDefault.Panel)).BeginInit();
            this.grpDefault.Panel.SuspendLayout();
            this.grpDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblDescription);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(12);
            this.pnlHeader.Size = new System.Drawing.Size(904, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(15, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(899, 25);
            this.lblDescription.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Values.Text = "Custom colours (#776) and live preview (#4691). Hover Live preview: the bar and p" +
    "anel follow until you click.";
            // 
            // grpMain
            // 
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 70);
            // 
            // grpMain.Panel
            // 
            this.grpMain.Panel.AutoScroll = true;
            this.grpMain.Panel.Controls.Add(this.lblSelected);
            this.grpMain.Panel.Controls.Add(this.grpLivePreview);
            this.grpMain.Panel.Controls.Add(this.grpNoMoreColors);
            this.grpMain.Panel.Controls.Add(this.grpMaxCustom);
            this.grpMain.Panel.Controls.Add(this.grpCustomAndBuiltIn);
            this.grpMain.Panel.Controls.Add(this.grpOnlyCustom);
            this.grpMain.Panel.Controls.Add(this.grpDefault);
            this.grpMain.Size = new System.Drawing.Size(904, 496);
            this.grpMain.TabIndex = 1;
            this.grpMain.Values.Heading = "KryptonColorButton demos";
            // 
            // lblSelected
            // 
            this.lblSelected.Location = new System.Drawing.Point(15, 600);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(251, 25);
            this.lblSelected.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSelected.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblSelected.TabIndex = 5;
            this.lblSelected.Values.Text = "Selected: (pick a colour from any button)";
            // 
            // grpLivePreview
            // 
            this.grpLivePreview.Location = new System.Drawing.Point(15, 437);
            // 
            // grpLivePreview.Panel
            // 
            this.grpLivePreview.Panel.Controls.Add(this.lblLivePreview);
            this.grpLivePreview.Panel.Controls.Add(this.pnlLivePreview);
            this.grpLivePreview.Panel.Controls.Add(this.btnCommittedOnly);
            this.grpLivePreview.Panel.Controls.Add(this.btnLivePreview);
            this.grpLivePreview.Size = new System.Drawing.Size(875, 150);
            this.grpLivePreview.TabIndex = 5;
            this.grpLivePreview.Values.Heading = "6. Live preview (Issue #4691)";
            // 
            // lblLivePreview
            // 
            this.lblLivePreview.Location = new System.Drawing.Point(15, 52);
            this.lblLivePreview.Name = "lblLivePreview";
            this.lblLivePreview.Size = new System.Drawing.Size(820, 40);
            this.lblLivePreview.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblLivePreview.TabIndex = 3;
            this.lblLivePreview.Values.Text = "Live preview follows the hover and returns when the drop-down closes. Committed " +
    "only changes the bar after a click. The panel tracks the live-preview button.";
            // 
            // btnLivePreview
            // 
            this.btnLivePreview.Location = new System.Drawing.Point(15, 12);
            this.btnLivePreview.Name = "btnLivePreview";
            this.btnLivePreview.Size = new System.Drawing.Size(280, 28);
            this.btnLivePreview.TabIndex = 0;
            this.btnLivePreview.Values.Text = "Live preview";
            // 
            // btnCommittedOnly
            // 
            this.btnCommittedOnly.Location = new System.Drawing.Point(310, 12);
            this.btnCommittedOnly.Name = "btnCommittedOnly";
            this.btnCommittedOnly.Size = new System.Drawing.Size(280, 28);
            this.btnCommittedOnly.TabIndex = 1;
            this.btnCommittedOnly.Values.Text = "Committed only";
            // 
            // pnlLivePreview
            // 
            this.pnlLivePreview.Location = new System.Drawing.Point(610, 8);
            this.pnlLivePreview.Name = "pnlLivePreview";
            this.pnlLivePreview.Size = new System.Drawing.Size(72, 36);
            this.pnlLivePreview.TabIndex = 2;
            // 
            // grpNoMoreColors
            // 
            this.grpNoMoreColors.Location = new System.Drawing.Point(15, 351);
            // 
            // grpNoMoreColors.Panel
            // 
            this.grpNoMoreColors.Panel.Controls.Add(this.btnNoMoreColors);
            this.grpNoMoreColors.Size = new System.Drawing.Size(875, 80);
            this.grpNoMoreColors.TabIndex = 4;
            this.grpNoMoreColors.Values.Heading = "5. Only custom colours (No Color and More Colors hidden)";
            // 
            // btnNoMoreColors
            // 
            this.btnNoMoreColors.Location = new System.Drawing.Point(15, 15);
            this.btnNoMoreColors.Name = "btnNoMoreColors";
            this.btnNoMoreColors.Size = new System.Drawing.Size(380, 28);
            this.btnNoMoreColors.TabIndex = 0;
            this.btnNoMoreColors.Values.Text = "Only custom (no No Color / More Colors)";
            // 
            // grpMaxCustom
            // 
            this.grpMaxCustom.Location = new System.Drawing.Point(15, 265);
            // 
            // grpMaxCustom.Panel
            // 
            this.grpMaxCustom.Panel.Controls.Add(this.btnMaxCustom);
            this.grpMaxCustom.Size = new System.Drawing.Size(875, 80);
            this.grpMaxCustom.TabIndex = 3;
            this.grpMaxCustom.Values.Heading = "4. MaxCustomColors = 6 (list has 16 colours; only first 6 shown)";
            // 
            // btnMaxCustom
            // 
            this.btnMaxCustom.Location = new System.Drawing.Point(15, 15);
            this.btnMaxCustom.Name = "btnMaxCustom";
            this.btnMaxCustom.Size = new System.Drawing.Size(320, 28);
            this.btnMaxCustom.TabIndex = 0;
            this.btnMaxCustom.Values.Text = "MaxCustomColors = 6 (16 in list)";
            // 
            // grpCustomAndBuiltIn
            // 
            this.grpCustomAndBuiltIn.Location = new System.Drawing.Point(15, 179);
            // 
            // grpCustomAndBuiltIn.Panel
            // 
            this.grpCustomAndBuiltIn.Panel.Controls.Add(this.btnCustomAndBuiltIn);
            this.grpCustomAndBuiltIn.Size = new System.Drawing.Size(875, 80);
            this.grpCustomAndBuiltIn.TabIndex = 2;
            this.grpCustomAndBuiltIn.Values.Heading = "3. Custom colours + Theme + Standard + Recent";
            // 
            // btnCustomAndBuiltIn
            // 
            this.btnCustomAndBuiltIn.Location = new System.Drawing.Point(15, 15);
            this.btnCustomAndBuiltIn.Name = "btnCustomAndBuiltIn";
            this.btnCustomAndBuiltIn.Size = new System.Drawing.Size(400, 28);
            this.btnCustomAndBuiltIn.TabIndex = 0;
            this.btnCustomAndBuiltIn.Values.Text = "Custom + Theme + Standard + Recent";
            // 
            // grpOnlyCustom
            // 
            this.grpOnlyCustom.Location = new System.Drawing.Point(15, 101);
            // 
            // grpOnlyCustom.Panel
            // 
            this.grpOnlyCustom.Panel.Controls.Add(this.btnOnlyCustom);
            this.grpOnlyCustom.Size = new System.Drawing.Size(875, 80);
            this.grpOnlyCustom.TabIndex = 1;
            this.grpOnlyCustom.Values.Heading = "2. Only 10 custom colours (VisibleThemes/Standard/Recent = false)";
            // 
            // btnOnlyCustom
            // 
            this.btnOnlyCustom.Location = new System.Drawing.Point(15, 15);
            this.btnOnlyCustom.Name = "btnOnlyCustom";
            this.btnOnlyCustom.Size = new System.Drawing.Size(320, 28);
            this.btnOnlyCustom.TabIndex = 0;
            this.btnOnlyCustom.Values.Text = "Only 10 custom colours";
            // 
            // grpDefault
            // 
            this.grpDefault.Location = new System.Drawing.Point(15, 15);
            // 
            // grpDefault.Panel
            // 
            this.grpDefault.Panel.Controls.Add(this.btnDefault);
            this.grpDefault.Size = new System.Drawing.Size(875, 80);
            this.grpDefault.TabIndex = 0;
            this.grpDefault.Values.Heading = "1. Default (Theme + Standard + Recent)";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(15, 15);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(320, 28);
            this.btnDefault.TabIndex = 0;
            this.btnDefault.Values.Text = "Default (Theme + Standard + Recent)";
            // 
            // KryptonColorButtonDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 740);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "KryptonColorButtonDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonColorButton Custom Colours and Live Preview";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).EndInit();
            this.grpMain.Panel.ResumeLayout(false);
            this.grpMain.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).EndInit();
            this.grpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLivePreview.Panel)).EndInit();
            this.grpLivePreview.Panel.ResumeLayout(false);
            this.grpLivePreview.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLivePreview)).EndInit();
            this.grpLivePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLivePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNoMoreColors.Panel)).EndInit();
            this.grpNoMoreColors.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpNoMoreColors)).EndInit();
            this.grpNoMoreColors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpMaxCustom.Panel)).EndInit();
            this.grpMaxCustom.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpMaxCustom)).EndInit();
            this.grpMaxCustom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCustomAndBuiltIn.Panel)).EndInit();
            this.grpCustomAndBuiltIn.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCustomAndBuiltIn)).EndInit();
            this.grpCustomAndBuiltIn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpOnlyCustom.Panel)).EndInit();
            this.grpOnlyCustom.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpOnlyCustom)).EndInit();
            this.grpOnlyCustom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDefault.Panel)).EndInit();
            this.grpDefault.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDefault)).EndInit();
            this.grpDefault.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private Krypton.Toolkit.KryptonLabel lblDescription;
        private Krypton.Toolkit.KryptonGroupBox grpMain;
        private Krypton.Toolkit.KryptonGroupBox grpDefault;
        private Krypton.Toolkit.KryptonColorButton btnDefault;
        private Krypton.Toolkit.KryptonGroupBox grpOnlyCustom;
        private Krypton.Toolkit.KryptonColorButton btnOnlyCustom;
        private Krypton.Toolkit.KryptonGroupBox grpCustomAndBuiltIn;
        private Krypton.Toolkit.KryptonColorButton btnCustomAndBuiltIn;
        private Krypton.Toolkit.KryptonGroupBox grpMaxCustom;
        private Krypton.Toolkit.KryptonColorButton btnMaxCustom;
        private Krypton.Toolkit.KryptonGroupBox grpNoMoreColors;
        private Krypton.Toolkit.KryptonColorButton btnNoMoreColors;
        private Krypton.Toolkit.KryptonLabel lblSelected;
        private Krypton.Toolkit.KryptonGroupBox grpLivePreview;
        private Krypton.Toolkit.KryptonColorButton btnLivePreview;
        private Krypton.Toolkit.KryptonColorButton btnCommittedOnly;
        private Krypton.Toolkit.KryptonPanel pnlLivePreview;
        private Krypton.Toolkit.KryptonLabel lblLivePreview;
    }
}