#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Issue797IndividualCornerRoundingDemo
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Issue797IndividualCornerRoundingDemo));
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.klblMixedCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblUniformCaption = new Krypton.Toolkit.KryptonLabel();
            this.kbIndividualCorners = new Krypton.Toolkit.KryptonButton();
            this.kbUniformCorners = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblInfo.Location = new System.Drawing.Point(0, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.kwlblInfo.Size = new System.Drawing.Size(590, 77);
            this.kwlblInfo.Text = resources.GetString("kwlblInfo.Text");
            // 
            // kryptonPanelMain
            // 
            this.kryptonPanelMain.Controls.Add(this.tlpMain);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 77);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(10);
            this.kryptonPanelMain.Size = new System.Drawing.Size(789, 287);
            this.kryptonPanelMain.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.klblMixedCaption, 0, 0);
            this.tlpMain.Controls.Add(this.klblUniformCaption, 1, 0);
            this.tlpMain.Controls.Add(this.kbIndividualCorners, 0, 1);
            this.tlpMain.Controls.Add(this.kbUniformCorners, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(10, 10);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(769, 267);
            this.tlpMain.TabIndex = 0;
            // 
            // klblMixedCaption
            // 
            this.klblMixedCaption.AutoSize = false;
            this.klblMixedCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblMixedCaption.Location = new System.Drawing.Point(3, 3);
            this.klblMixedCaption.Name = "klblMixedCaption";
            this.klblMixedCaption.Size = new System.Drawing.Size(378, 21);
            this.klblMixedCaption.TabIndex = 0;
            this.klblMixedCaption.Values.Text = "TL/TR/BL = 5, BR = 0";
            // 
            // klblUniformCaption
            // 
            this.klblUniformCaption.AutoSize = false;
            this.klblUniformCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblUniformCaption.Location = new System.Drawing.Point(387, 3);
            this.klblUniformCaption.Name = "klblUniformCaption";
            this.klblUniformCaption.Size = new System.Drawing.Size(379, 21);
            this.klblUniformCaption.TabIndex = 1;
            this.klblUniformCaption.Values.Text = "Uniform Rounding = 5";
            // 
            // kbIndividualCorners
            // 
            this.kbIndividualCorners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbIndividualCorners.Location = new System.Drawing.Point(3, 30);
            this.kbIndividualCorners.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.kbIndividualCorners.Name = "kbIndividualCorners";
            this.kbIndividualCorners.Size = new System.Drawing.Size(376, 234);
            this.kbIndividualCorners.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kbIndividualCorners.StateCommon.Border.Rounding = 5F;
            this.kbIndividualCorners.StateCommon.Border.Width = 2;
            this.kbIndividualCorners.TabIndex = 2;
            this.kbIndividualCorners.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbIndividualCorners.Values.Text = "Mixed corners";
            // 
            // kbUniformCorners
            // 
            this.kbUniformCorners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbUniformCorners.Location = new System.Drawing.Point(389, 30);
            this.kbUniformCorners.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.kbUniformCorners.Name = "kbUniformCorners";
            this.kbUniformCorners.Size = new System.Drawing.Size(377, 234);
            this.kbUniformCorners.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kbUniformCorners.StateCommon.Border.Rounding = 5F;
            this.kbUniformCorners.StateCommon.Border.Width = 2;
            this.kbUniformCorners.TabIndex = 3;
            this.kbUniformCorners.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbUniformCorners.Values.Text = "Uniform corners";
            // 
            // Issue797IndividualCornerRoundingDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 364);
            this.Controls.Add(this.kryptonPanelMain);
            this.Controls.Add(this.kwlblInfo);
            this.MinimumSize = new System.Drawing.Size(551, 317);
            this.Name = "Issue797IndividualCornerRoundingDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issue #797 — Individual corner rounding";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private KryptonWrapLabel kwlblInfo;
        private KryptonPanel kryptonPanelMain;
        private TableLayoutPanel tlpMain;
        private KryptonLabel klblMixedCaption;
        private KryptonLabel klblUniformCaption;
        private KryptonButton kbIndividualCorners;
        private KryptonButton kbUniformCorners;
    }
}
