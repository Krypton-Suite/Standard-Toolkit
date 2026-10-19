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
    partial class Bug4046DataGridViewScrollbarDemo
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
            this.klblDefaultCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblRoundedCaption = new Krypton.Toolkit.KryptonLabel();
            this.kdgvDefault = new Krypton.Toolkit.KryptonDataGridView();
            this.kdgvRounded = new Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvRounded)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(984, 661);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.klblDefaultCaption, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.klblRoundedCaption, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.kdgvDefault, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.kdgvRounded, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 637);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // kwlblInstructions
            //
            this.tableLayoutPanel1.SetColumnSpan(this.kwlblInstructions, 2);
            this.kwlblInstructions.AutoSize = false;
            this.kwlblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInstructions.Location = new System.Drawing.Point(3, 3);
            this.kwlblInstructions.Name = "kwlblInstructions";
            this.kwlblInstructions.Size = new System.Drawing.Size(954, 88);
            this.kwlblInstructions.Text =
                "Issue #4046: after the fix, the left grid (default border rounding) must not show a themed overlay scrollbar at runtime.\r\n" +
                "The right grid enables StateNormal.Border.Rounding = 8 with enough rows to require scrolling; its themed scrollbars should appear and scroll the grid.\r\n" +
                "Before the fix, the left grid could show a draggable Krypton scrollbar that did not move grid content.";
            this.kwlblInstructions.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // klblDefaultCaption
            //
            this.klblDefaultCaption.Location = new System.Drawing.Point(3, 97);
            this.klblDefaultCaption.Name = "klblDefaultCaption";
            this.klblDefaultCaption.Size = new System.Drawing.Size(474, 20);
            this.klblDefaultCaption.TabIndex = 1;
            this.klblDefaultCaption.Values.Text = "Default KryptonDataGridView (no corner rounding)";
            //
            // klblRoundedCaption
            //
            this.klblRoundedCaption.Location = new System.Drawing.Point(483, 97);
            this.klblRoundedCaption.Name = "klblRoundedCaption";
            this.klblRoundedCaption.Size = new System.Drawing.Size(474, 20);
            this.klblRoundedCaption.TabIndex = 2;
            this.klblRoundedCaption.Values.Text = "Rounded KryptonDataGridView (Border.Rounding = 8, scrollable content)";
            //
            // kdgvDefault
            //
            this.kdgvDefault.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kdgvDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdgvDefault.Location = new System.Drawing.Point(3, 124);
            this.kdgvDefault.Name = "kdgvDefault";
            this.kdgvDefault.Size = new System.Drawing.Size(474, 510);
            this.kdgvDefault.TabIndex = 3;
            //
            // kdgvRounded
            //
            this.kdgvRounded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kdgvRounded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdgvRounded.Location = new System.Drawing.Point(483, 124);
            this.kdgvRounded.Name = "kdgvRounded";
            this.kdgvRounded.Size = new System.Drawing.Size(474, 510);
            this.kdgvRounded.TabIndex = 4;
            //
            // Bug4046DataGridViewScrollbarDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.kryptonPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "Bug4046DataGridViewScrollbarDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bug 4046 - KryptonDataGridView Scrollbar";
            this.Load += new System.EventHandler(this.Bug4046DataGridViewScrollbarDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvRounded)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInstructions;
        private Krypton.Toolkit.KryptonLabel klblDefaultCaption;
        private Krypton.Toolkit.KryptonLabel klblRoundedCaption;
        private Krypton.Toolkit.KryptonDataGridView kdgvDefault;
        private Krypton.Toolkit.KryptonDataGridView kdgvRounded;
    }
}
