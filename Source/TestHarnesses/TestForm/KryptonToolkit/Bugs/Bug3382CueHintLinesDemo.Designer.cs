#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3382CueHintLinesDemo
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
        /// Required method for Designer support — do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bug3382CueHintLinesDemo));
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.klblIssueStyleHeader = new Krypton.Toolkit.KryptonLabel();
            this.ktxtIssueStyle = new Krypton.Toolkit.KryptonTextBox();
            this.klblMatchedFontsHeader = new Krypton.Toolkit.KryptonLabel();
            this.ktxtMatchedFonts = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblInfo.Location = new System.Drawing.Point(0, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Padding = new System.Windows.Forms.Padding(12, 12, 12, 8);
            this.kwlblInfo.Size = new System.Drawing.Size(1783, 35);
            this.kwlblInfo.Text = resources.GetString("kwlblInfo.Text");
            // 
            // kryptonPanelMain
            // 
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanelMain);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 35);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(720, 385);
            this.kryptonPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.Controls.Add(this.klblIssueStyleHeader, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ktxtIssueStyle, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.klblMatchedFontsHeader, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.ktxtMatchedFonts, 0, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(696, 361);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // klblIssueStyleHeader
            // 
            this.klblIssueStyleHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblIssueStyleHeader.Location = new System.Drawing.Point(3, 3);
            this.klblIssueStyleHeader.Name = "klblIssueStyleHeader";
            this.klblIssueStyleHeader.Size = new System.Drawing.Size(690, 20);
            this.klblIssueStyleHeader.TabIndex = 0;
            this.klblIssueStyleHeader.Values.Text = "TextH Near, Verdana 9 content, smaller Segoe UI cue (issue-style)";
            // 
            // ktxtIssueStyle
            // 
            this.ktxtIssueStyle.CueHint.Color1 = System.Drawing.Color.DarkGray;
            this.ktxtIssueStyle.CueHint.CueHintText = "Объект не выбран";
            this.ktxtIssueStyle.CueHint.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ktxtIssueStyle.CueHint.Padding = new System.Windows.Forms.Padding(0);
            this.ktxtIssueStyle.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ktxtIssueStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtIssueStyle.Location = new System.Drawing.Point(3, 29);
            this.ktxtIssueStyle.Name = "ktxtIssueStyle";
            this.ktxtIssueStyle.Size = new System.Drawing.Size(690, 20);
            this.ktxtIssueStyle.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.ktxtIssueStyle.StateCommon.Content.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ktxtIssueStyle.StateDisabled.Back.Color1 = System.Drawing.Color.White;
            this.ktxtIssueStyle.TabIndex = 1;
            this.ktxtIssueStyle.WordWrap = false;
            // 
            // klblMatchedFontsHeader
            // 
            this.klblMatchedFontsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblMatchedFontsHeader.Location = new System.Drawing.Point(3, 57);
            this.klblMatchedFontsHeader.Name = "klblMatchedFontsHeader";
            this.klblMatchedFontsHeader.Size = new System.Drawing.Size(690, 20);
            this.klblMatchedFontsHeader.TabIndex = 2;
            this.klblMatchedFontsHeader.Values.Text = "Same layout, cue font aligned to Verdana 9 (milder stress)";
            // 
            // ktxtMatchedFonts
            // 
            this.ktxtMatchedFonts.CueHint.Color1 = System.Drawing.Color.DarkGray;
            this.ktxtMatchedFonts.CueHint.CueHintText = "Объект не выбран";
            this.ktxtMatchedFonts.CueHint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ktxtMatchedFonts.CueHint.Padding = new System.Windows.Forms.Padding(0);
            this.ktxtMatchedFonts.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ktxtMatchedFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtMatchedFonts.Location = new System.Drawing.Point(3, 83);
            this.ktxtMatchedFonts.Name = "ktxtMatchedFonts";
            this.ktxtMatchedFonts.Size = new System.Drawing.Size(690, 20);
            this.ktxtMatchedFonts.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.ktxtMatchedFonts.StateCommon.Content.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ktxtMatchedFonts.StateDisabled.Back.Color1 = System.Drawing.Color.White;
            this.ktxtMatchedFonts.TabIndex = 3;
            this.ktxtMatchedFonts.WordWrap = false;
            // 
            // Bug3382CueHintLinesDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 420);
            this.Controls.Add(this.kryptonPanelMain);
            this.Controls.Add(this.kwlblInfo);
            this.MinimumSize = new System.Drawing.Size(560, 320);
            this.Name = "Bug3382CueHintLinesDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bug #3382 - CueHint KryptonTextBox line artifacts";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private Krypton.Toolkit.KryptonLabel klblIssueStyleHeader;
        private Krypton.Toolkit.KryptonTextBox ktxtIssueStyle;
        private Krypton.Toolkit.KryptonLabel klblMatchedFontsHeader;
        private Krypton.Toolkit.KryptonTextBox ktxtMatchedFonts;
    }
}
