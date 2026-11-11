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
    partial class Bug3343RichTextBoxEditLossDemo
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
            this.klblRichCaption = new Krypton.Toolkit.KryptonLabel();
            this.krtbDemo = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblTextCaption = new Krypton.Toolkit.KryptonLabel();
            this.ktbxReference = new Krypton.Toolkit.KryptonTextBox();
            this.klblLive = new Krypton.Toolkit.KryptonLabel();
            this.klblAfterLeave = new Krypton.Toolkit.KryptonLabel();
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
            this.kryptonPanelMain.Size = new System.Drawing.Size(784, 561);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.klblRichCaption, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.krtbDemo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblTextCaption, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ktbxReference, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.klblLive, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.klblAfterLeave, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 537);
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
            this.kwlblInstructions.Size = new System.Drawing.Size(754, 72);
            this.kwlblInstructions.Text = "Issue #3343 — KryptonRichTextBox text lost when the mouse leaves the control.\r\n\r\nType a few characters (no need to press Tab). Move the mouse out of the rich text area. The text and \"TextLength\" below should stay the same. KryptonTextBox is included only as a reference control.";
            //
            // klblRichCaption
            //
            this.klblRichCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblRichCaption.Location = new System.Drawing.Point(3, 72);
            this.klblRichCaption.Name = "klblRichCaption";
            this.klblRichCaption.Size = new System.Drawing.Size(754, 24);
            this.klblRichCaption.TabIndex = 0;
            this.klblRichCaption.Values.Text = "KryptonRichTextBox";
            //
            // krtbDemo
            //
            this.krtbDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbDemo.Location = new System.Drawing.Point(3, 99);
            this.krtbDemo.Multiline = true;
            this.krtbDemo.Name = "krtbDemo";
            this.krtbDemo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.krtbDemo.Size = new System.Drawing.Size(754, 181);
            this.krtbDemo.TabIndex = 1;
            this.krtbDemo.Text = "";
            this.krtbDemo.TextChanged += new System.EventHandler(this.krtbDemo_TextChanged);
            this.krtbDemo.TrackMouseLeave += new System.EventHandler(this.krtbDemo_TrackMouseLeave);
            //
            // klblTextCaption
            //
            this.klblTextCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblTextCaption.Location = new System.Drawing.Point(3, 286);
            this.klblTextCaption.Name = "klblTextCaption";
            this.klblTextCaption.Size = new System.Drawing.Size(754, 24);
            this.klblTextCaption.TabIndex = 2;
            this.klblTextCaption.Values.Text = "KryptonTextBox (reference — should behave like standard text entry)";
            //
            // ktbxReference
            //
            this.ktbxReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbxReference.Location = new System.Drawing.Point(3, 313);
            this.ktbxReference.Multiline = true;
            this.ktbxReference.Name = "ktbxReference";
            this.ktbxReference.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktbxReference.Size = new System.Drawing.Size(754, 181);
            this.ktbxReference.TabIndex = 3;
            this.ktbxReference.TextChanged += new System.EventHandler(this.ktbxReference_TextChanged);
            //
            // klblLive
            //
            this.klblLive.AutoSize = false;
            this.klblLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblLive.Location = new System.Drawing.Point(3, 500);
            this.klblLive.Name = "klblLive";
            this.klblLive.Size = new System.Drawing.Size(754, 22);
            this.klblLive.StateCommon.ShortText.Color1 = System.Drawing.Color.DimGray;
            this.klblLive.TabIndex = 4;
            this.klblLive.Values.Text = "Live: (counts update as you type)";
            //
            // klblAfterLeave
            //
            this.klblAfterLeave.AutoSize = false;
            this.klblAfterLeave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblAfterLeave.Location = new System.Drawing.Point(3, 522);
            this.klblAfterLeave.Name = "klblAfterLeave";
            this.klblAfterLeave.Size = new System.Drawing.Size(754, 22);
            this.klblAfterLeave.StateCommon.ShortText.Color1 = System.Drawing.Color.DimGray;
            this.klblAfterLeave.TabIndex = 5;
            this.klblAfterLeave.Values.Text = "After TrackMouseLeave: (move the mouse out of the rich text box to update)";
            //
            // Bug3343RichTextBoxEditLossDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.kryptonPanelMain);
            this.MinimumSize = new System.Drawing.Size(520, 400);
            this.Name = "Bug3343RichTextBoxEditLossDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Issue #3343 — KryptonRichTextBox edit / mouse leave";
            this.Load += new System.EventHandler(this.Bug3343RichTextBoxEditLossDemo_Load);
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
        private Krypton.Toolkit.KryptonLabel klblRichCaption;
        private Krypton.Toolkit.KryptonRichTextBox krtbDemo;
        private Krypton.Toolkit.KryptonLabel klblTextCaption;
        private Krypton.Toolkit.KryptonTextBox ktbxReference;
        private Krypton.Toolkit.KryptonLabel klblLive;
        private Krypton.Toolkit.KryptonLabel klblAfterLeave;
    }
}
