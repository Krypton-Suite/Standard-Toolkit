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
    partial class Bug3902ScrollbarGapDemo
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
            this.klblTextCaption = new Krypton.Toolkit.KryptonLabel();
            this.ktbxDemo = new Krypton.Toolkit.KryptonTextBox();
            this.klblRichCaption = new Krypton.Toolkit.KryptonLabel();
            this.krtbDemo = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblListCaption = new Krypton.Toolkit.KryptonLabel();
            this.klstDemo = new Krypton.Toolkit.KryptonListBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(884, 661);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.klblTextCaption, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ktbxDemo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblRichCaption, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.krtbDemo, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.klblListCaption, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.klstDemo, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(860, 637);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // kwlblInstructions
            //
            this.kwlblInstructions.AutoSize = false;
            this.kwlblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInstructions.Location = new System.Drawing.Point(3, 3);
            this.kwlblInstructions.Name = "kwlblInstructions";
            this.kwlblInstructions.Size = new System.Drawing.Size(854, 72);
            this.kwlblInstructions.Text =
                "Issue #3902: inspect the right and bottom edges where Krypton scrollbars meet the control border.\r\n" +
                "Expected: no white 1–2px strip between the scrollbar and the themed border on KryptonTextBox, KryptonRichTextBox, and KryptonListBox.\r\n" +
                "Try at 100% and 150% display scaling. Swap themes from the TestForm menu to confirm across palettes.";
            this.kwlblInstructions.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // klblTextCaption
            //
            this.klblTextCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblTextCaption.Location = new System.Drawing.Point(3, 81);
            this.klblTextCaption.Name = "klblTextCaption";
            this.klblTextCaption.Size = new System.Drawing.Size(854, 18);
            this.klblTextCaption.TabIndex = 1;
            this.klblTextCaption.Values.Text = "KryptonTextBox (UseKryptonScrollbars = true)";
            //
            // ktbxDemo
            //
            this.ktbxDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbxDemo.Location = new System.Drawing.Point(3, 105);
            this.ktbxDemo.Multiline = true;
            this.ktbxDemo.Name = "ktbxDemo";
            this.ktbxDemo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ktbxDemo.Size = new System.Drawing.Size(854, 150);
            this.ktbxDemo.TabIndex = 2;
            this.ktbxDemo.UseKryptonScrollbars = true;
            //
            // klblRichCaption
            //
            this.klblRichCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblRichCaption.Location = new System.Drawing.Point(3, 261);
            this.klblRichCaption.Name = "klblRichCaption";
            this.klblRichCaption.Size = new System.Drawing.Size(854, 18);
            this.klblRichCaption.TabIndex = 3;
            this.klblRichCaption.Values.Text = "KryptonRichTextBox (UseKryptonScrollbars = true)";
            //
            // krtbDemo
            //
            this.krtbDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbDemo.Location = new System.Drawing.Point(3, 285);
            this.krtbDemo.Name = "krtbDemo";
            this.krtbDemo.Size = new System.Drawing.Size(854, 150);
            this.krtbDemo.TabIndex = 4;
            this.krtbDemo.UseKryptonScrollbars = true;
            //
            // klblListCaption
            //
            this.klblListCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblListCaption.Location = new System.Drawing.Point(3, 441);
            this.klblListCaption.Name = "klblListCaption";
            this.klblListCaption.Size = new System.Drawing.Size(854, 18);
            this.klblListCaption.TabIndex = 5;
            this.klblListCaption.Values.Text = "KryptonListBox (UseKryptonScrollbars = true)";
            //
            // klstDemo
            //
            this.klstDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klstDemo.Location = new System.Drawing.Point(3, 465);
            this.klstDemo.Name = "klstDemo";
            this.klstDemo.Size = new System.Drawing.Size(854, 169);
            this.klstDemo.TabIndex = 6;
            this.klstDemo.UseKryptonScrollbars = true;
            //
            // Bug3902ScrollbarGapDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.kryptonPanelMain);
            this.Name = "Bug3902ScrollbarGapDemo";
            this.Text = "Bug 3902 Scrollbar Gap Demo";
            this.Load += new System.EventHandler(this.Bug3902ScrollbarGapDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInstructions;
        private Krypton.Toolkit.KryptonLabel klblTextCaption;
        private Krypton.Toolkit.KryptonTextBox ktbxDemo;
        private Krypton.Toolkit.KryptonLabel klblRichCaption;
        private Krypton.Toolkit.KryptonRichTextBox krtbDemo;
        private Krypton.Toolkit.KryptonLabel klblListCaption;
        private Krypton.Toolkit.KryptonListBox klstDemo;
    }
}
