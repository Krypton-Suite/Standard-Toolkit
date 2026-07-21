#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Feature4008RichTextBoxJustifyDemo
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
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.klblKryptonCaption = new Krypton.Toolkit.KryptonLabel();
            this.flowKryptonAlign = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnLeft = new Krypton.Toolkit.KryptonButton();
            this.kbtnCenter = new Krypton.Toolkit.KryptonButton();
            this.kbtnRight = new Krypton.Toolkit.KryptonButton();
            this.kbtnJustify = new Krypton.Toolkit.KryptonButton();
            this.kbtnSelectAll = new Krypton.Toolkit.KryptonButton();
            this.krtbDemo = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblNativeCaption = new Krypton.Toolkit.KryptonLabel();
            this.flowNativeAlign = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnNativeLeft = new Krypton.Toolkit.KryptonButton();
            this.kbtnNativeCenter = new Krypton.Toolkit.KryptonButton();
            this.kbtnNativeRight = new Krypton.Toolkit.KryptonButton();
            this.rtbNative = new System.Windows.Forms.RichTextBox();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowKryptonAlign.SuspendLayout();
            this.flowNativeAlign.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(900, 620);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.klblKryptonCaption, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowKryptonAlign, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.krtbDemo, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.klblNativeCaption, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowNativeAlign, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.rtbNative, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.klblStatus, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(876, 596);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // kwlblInstructions
            //
            this.kwlblInstructions.AutoSize = false;
            this.kwlblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kwlblInstructions.Location = new System.Drawing.Point(3, 0);
            this.kwlblInstructions.Name = "kwlblInstructions";
            this.kwlblInstructions.Size = new System.Drawing.Size(870, 72);
            this.kwlblInstructions.Text = "Issue #4008 — KryptonRichTextBox.SelectionParagraphAlignment (Left / Center / Right / Justify).\r\n\r\nUse the Krypton buttons to apply paragraph alignment. Justify should fill soft spaces to both margins on full lines. The native RichTextBox below only has SelectionAlignment (no Justify). Narrow the window to see wrapping differences more clearly.";
            //
            // klblKryptonCaption
            //
            this.klblKryptonCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblKryptonCaption.Location = new System.Drawing.Point(3, 72);
            this.klblKryptonCaption.Name = "klblKryptonCaption";
            this.klblKryptonCaption.Size = new System.Drawing.Size(870, 24);
            this.klblKryptonCaption.TabIndex = 0;
            this.klblKryptonCaption.Values.Text = "KryptonRichTextBox — SelectionParagraphAlignment";
            //
            // flowKryptonAlign
            //
            this.flowKryptonAlign.Controls.Add(this.kbtnLeft);
            this.flowKryptonAlign.Controls.Add(this.kbtnCenter);
            this.flowKryptonAlign.Controls.Add(this.kbtnRight);
            this.flowKryptonAlign.Controls.Add(this.kbtnJustify);
            this.flowKryptonAlign.Controls.Add(this.kbtnSelectAll);
            this.flowKryptonAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowKryptonAlign.Location = new System.Drawing.Point(3, 99);
            this.flowKryptonAlign.Name = "flowKryptonAlign";
            this.flowKryptonAlign.Size = new System.Drawing.Size(870, 34);
            this.flowKryptonAlign.TabIndex = 1;
            //
            // kbtnLeft
            //
            this.kbtnLeft.Location = new System.Drawing.Point(3, 3);
            this.kbtnLeft.Name = "kbtnLeft";
            this.kbtnLeft.Size = new System.Drawing.Size(90, 28);
            this.kbtnLeft.TabIndex = 0;
            this.kbtnLeft.Values.Text = "Left";
            this.kbtnLeft.Click += new System.EventHandler(this.kbtnLeft_Click);
            //
            // kbtnCenter
            //
            this.kbtnCenter.Location = new System.Drawing.Point(99, 3);
            this.kbtnCenter.Name = "kbtnCenter";
            this.kbtnCenter.Size = new System.Drawing.Size(90, 28);
            this.kbtnCenter.TabIndex = 1;
            this.kbtnCenter.Values.Text = "Center";
            this.kbtnCenter.Click += new System.EventHandler(this.kbtnCenter_Click);
            //
            // kbtnRight
            //
            this.kbtnRight.Location = new System.Drawing.Point(195, 3);
            this.kbtnRight.Name = "kbtnRight";
            this.kbtnRight.Size = new System.Drawing.Size(90, 28);
            this.kbtnRight.TabIndex = 2;
            this.kbtnRight.Values.Text = "Right";
            this.kbtnRight.Click += new System.EventHandler(this.kbtnRight_Click);
            //
            // kbtnJustify
            //
            this.kbtnJustify.Location = new System.Drawing.Point(291, 3);
            this.kbtnJustify.Name = "kbtnJustify";
            this.kbtnJustify.Size = new System.Drawing.Size(90, 28);
            this.kbtnJustify.TabIndex = 3;
            this.kbtnJustify.Values.Text = "Justify";
            this.kbtnJustify.Click += new System.EventHandler(this.kbtnJustify_Click);
            //
            // kbtnSelectAll
            //
            this.kbtnSelectAll.Location = new System.Drawing.Point(387, 3);
            this.kbtnSelectAll.Name = "kbtnSelectAll";
            this.kbtnSelectAll.Size = new System.Drawing.Size(100, 28);
            this.kbtnSelectAll.TabIndex = 4;
            this.kbtnSelectAll.Values.Text = "Select All";
            this.kbtnSelectAll.Click += new System.EventHandler(this.kbtnSelectAll_Click);
            //
            // krtbDemo
            //
            this.krtbDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbDemo.Location = new System.Drawing.Point(3, 139);
            this.krtbDemo.Multiline = true;
            this.krtbDemo.Name = "krtbDemo";
            this.krtbDemo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.krtbDemo.Size = new System.Drawing.Size(870, 160);
            this.krtbDemo.TabIndex = 2;
            this.krtbDemo.Text = "";
            this.krtbDemo.SelectionChanged += new System.EventHandler(this.krtbDemo_SelectionChanged);
            //
            // klblNativeCaption
            //
            this.klblNativeCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNativeCaption.Location = new System.Drawing.Point(3, 302);
            this.klblNativeCaption.Name = "klblNativeCaption";
            this.klblNativeCaption.Size = new System.Drawing.Size(870, 24);
            this.klblNativeCaption.TabIndex = 3;
            this.klblNativeCaption.Values.Text = "Native WinForms RichTextBox — SelectionAlignment only (no Justify)";
            //
            // flowNativeAlign
            //
            this.flowNativeAlign.Controls.Add(this.kbtnNativeLeft);
            this.flowNativeAlign.Controls.Add(this.kbtnNativeCenter);
            this.flowNativeAlign.Controls.Add(this.kbtnNativeRight);
            this.flowNativeAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNativeAlign.Location = new System.Drawing.Point(3, 329);
            this.flowNativeAlign.Name = "flowNativeAlign";
            this.flowNativeAlign.Size = new System.Drawing.Size(870, 34);
            this.flowNativeAlign.TabIndex = 4;
            //
            // kbtnNativeLeft
            //
            this.kbtnNativeLeft.Location = new System.Drawing.Point(3, 3);
            this.kbtnNativeLeft.Name = "kbtnNativeLeft";
            this.kbtnNativeLeft.Size = new System.Drawing.Size(90, 28);
            this.kbtnNativeLeft.TabIndex = 0;
            this.kbtnNativeLeft.Values.Text = "Left";
            this.kbtnNativeLeft.Click += new System.EventHandler(this.kbtnNativeLeft_Click);
            //
            // kbtnNativeCenter
            //
            this.kbtnNativeCenter.Location = new System.Drawing.Point(99, 3);
            this.kbtnNativeCenter.Name = "kbtnNativeCenter";
            this.kbtnNativeCenter.Size = new System.Drawing.Size(90, 28);
            this.kbtnNativeCenter.TabIndex = 1;
            this.kbtnNativeCenter.Values.Text = "Center";
            this.kbtnNativeCenter.Click += new System.EventHandler(this.kbtnNativeCenter_Click);
            //
            // kbtnNativeRight
            //
            this.kbtnNativeRight.Location = new System.Drawing.Point(195, 3);
            this.kbtnNativeRight.Name = "kbtnNativeRight";
            this.kbtnNativeRight.Size = new System.Drawing.Size(90, 28);
            this.kbtnNativeRight.TabIndex = 2;
            this.kbtnNativeRight.Values.Text = "Right";
            this.kbtnNativeRight.Click += new System.EventHandler(this.kbtnNativeRight_Click);
            //
            // rtbNative
            //
            this.rtbNative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbNative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNative.Location = new System.Drawing.Point(3, 369);
            this.rtbNative.Multiline = true;
            this.rtbNative.Name = "rtbNative";
            this.rtbNative.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbNative.Size = new System.Drawing.Size(870, 160);
            this.rtbNative.TabIndex = 5;
            this.rtbNative.Text = "";
            this.rtbNative.SelectionChanged += new System.EventHandler(this.rtbNative_SelectionChanged);
            //
            // klblStatus
            //
            this.klblStatus.AutoSize = false;
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 535);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(870, 40);
            this.klblStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.DimGray;
            this.klblStatus.TabIndex = 6;
            this.klblStatus.Values.Text = "Status";
            //
            // Feature4008RichTextBoxJustifyDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.kryptonPanelMain);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Feature4008RichTextBoxJustifyDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Issue #4008 — RichTextBox paragraph justify";
            this.Load += new System.EventHandler(this.Feature4008RichTextBoxJustifyDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowKryptonAlign.ResumeLayout(false);
            this.flowNativeAlign.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInstructions;
        private Krypton.Toolkit.KryptonLabel klblKryptonCaption;
        private System.Windows.Forms.FlowLayoutPanel flowKryptonAlign;
        private Krypton.Toolkit.KryptonButton kbtnLeft;
        private Krypton.Toolkit.KryptonButton kbtnCenter;
        private Krypton.Toolkit.KryptonButton kbtnRight;
        private Krypton.Toolkit.KryptonButton kbtnJustify;
        private Krypton.Toolkit.KryptonButton kbtnSelectAll;
        private Krypton.Toolkit.KryptonRichTextBox krtbDemo;
        private Krypton.Toolkit.KryptonLabel klblNativeCaption;
        private System.Windows.Forms.FlowLayoutPanel flowNativeAlign;
        private Krypton.Toolkit.KryptonButton kbtnNativeLeft;
        private Krypton.Toolkit.KryptonButton kbtnNativeCenter;
        private Krypton.Toolkit.KryptonButton kbtnNativeRight;
        private System.Windows.Forms.RichTextBox rtbNative;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}
