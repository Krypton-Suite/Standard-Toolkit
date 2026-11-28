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
    partial class Bug3383KryptonButtonStateTrackingRoundingDemo
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
            kwlblInfo = new KryptonWrapLabel();
            kryptonPanelMain = new KryptonPanel();
            tlpMain = new TableLayoutPanel();
            klblReproCaption = new KryptonLabel();
            klblControlCaption = new KryptonLabel();
            kbIssuePattern = new KryptonButton();
            kbMatchedRounding = new KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanelMain).BeginInit();
            kryptonPanelMain.SuspendLayout();
            tlpMain.SuspendLayout();
            SuspendLayout();
            //
            // kwlblInfo
            //
            kwlblInfo.Dock = DockStyle.Top;
            kwlblInfo.Padding = new Padding(12, 12, 12, 8);
            kwlblInfo.Text =
                "GitHub issue #3383: With OverrideFocus.Border.Rounding set (e.g. pill) plus a different StateTracking.Border.Rounding, focus override merge previously forced the OverrideFocus rounding when the button had keyboard focus — white corner artifacts appeared on hover.\r\n\r\n"
                + "Steps — left button:\r\n"
                + "\u2022 Tab onto the green button so it receives focus (tracking palette merge active).\r\n"
                + "\u2022 Slowly move the pointer over it: fill and stroke should share the StateTracking rounding (no inner light gaps).\r\n\r\n"
                + "Compare with the right control where StateTracking matches StateCommon rounding.\r\n\r\n"
                + "The outer edge line is the palette border (dark green here; the report used black). It is not a rendering defect.";
            //
            // kryptonPanelMain
            //
            kryptonPanelMain.Controls.Add(tlpMain);
            kryptonPanelMain.Dock = DockStyle.Fill;
            kryptonPanelMain.Location = new Point(0, 0);
            kryptonPanelMain.Name = @"kryptonPanelMain";
            kryptonPanelMain.Padding = new Padding(12);
            kryptonPanelMain.Size = new Size(920, 360);
            kryptonPanelMain.TabIndex = 0;
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.Controls.Add(klblReproCaption, 0, 0);
            tlpMain.Controls.Add(klblControlCaption, 1, 0);
            tlpMain.Controls.Add(kbIssuePattern, 0, 1);
            tlpMain.Controls.Add(kbMatchedRounding, 1, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(12, 12);
            tlpMain.Name = @"tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(896, 336);
            tlpMain.TabIndex = 0;
            //
            // klblReproCaption
            //
            klblReproCaption.AutoSize = false;
            klblReproCaption.Dock = DockStyle.Fill;
            klblReproCaption.Location = new Point(3, 0);
            klblReproCaption.Name = @"klblReproCaption";
            klblReproCaption.Size = new Size(442, 40);
            klblReproCaption.TabIndex = 0;
            klblReproCaption.Text = @"Left — issue pattern: StateCommon R=50, StateTracking R=10 + thick border, OverrideFocus R=50";
            //
            // klblControlCaption
            //
            klblControlCaption.AutoSize = false;
            klblControlCaption.Dock = DockStyle.Fill;
            klblControlCaption.Location = new Point(451, 0);
            klblControlCaption.Name = @"klblControlCaption";
            klblControlCaption.Size = new Size(442, 40);
            klblControlCaption.TabIndex = 1;
            klblControlCaption.Text = @"Right — control: StateTracking rounding matches StateCommon (no geometry mismatch)";
            //
            // kbIssuePattern
            //
            kbIssuePattern.Cursor = Cursors.Hand;
            kbIssuePattern.Dock = DockStyle.Fill;
            kbIssuePattern.Location = new Point(3, 43);
            kbIssuePattern.Name = @"kbIssuePattern";
            kbIssuePattern.OverrideDefault.Back.Color1 = Color.YellowGreen;
            kbIssuePattern.OverrideDefault.Back.Color2 = Color.YellowGreen;
            kbIssuePattern.OverrideDefault.Border.Color1 = Color.YellowGreen;
            kbIssuePattern.OverrideDefault.Border.DrawBorders = PaletteDrawBorders.All;
            kbIssuePattern.OverrideFocus.Border.Color1 = DemoOutline;
            kbIssuePattern.OverrideFocus.Border.Color2 = DemoOutline;
            kbIssuePattern.OverrideFocus.Border.DrawBorders = PaletteDrawBorders.All;
            kbIssuePattern.OverrideFocus.Border.Width = 1;
            kbIssuePattern.OverrideFocus.Border.Rounding = 50F;
            kbIssuePattern.StateCommon.Back.Color1 = Color.FromArgb(0, 192, 0);
            kbIssuePattern.StateCommon.Back.Color2 = Color.FromArgb(0, 192, 0);
            kbIssuePattern.StateCommon.Border.Color1 = DemoOutline;
            kbIssuePattern.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            kbIssuePattern.StateCommon.Border.Rounding = 50F;
            kbIssuePattern.StateCommon.Border.Width = 1;
            kbIssuePattern.StateCommon.Content.ShortText.Color1 = Color.White;
            kbIssuePattern.StateCommon.Content.ShortText.Font = new Font(@"Century Gothic", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            kbIssuePattern.StateCommon.Content.ShortText.Hint = PaletteTextHint.AntiAlias;
            kbIssuePattern.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Center;
            kbIssuePattern.StateCommon.Content.ShortText.TextV = PaletteRelativeAlign.Center;
            kbIssuePattern.StateTracking.Back.Color1 = Color.OliveDrab;
            kbIssuePattern.StateTracking.Back.Color2 = Color.OliveDrab;
            kbIssuePattern.StateTracking.Border.Color1 = DemoOutline;
            kbIssuePattern.StateTracking.Border.Color2 = DemoOutline;
            kbIssuePattern.StateTracking.Border.Rounding = 10F;
            kbIssuePattern.StateTracking.Border.Width = 3;
            kbIssuePattern.TabIndex = 2;
            kbIssuePattern.Values.Text = @"Focus + hover (#3383)";
            kbIssuePattern.Values.DropDownArrowColor = Color.Empty;
            //
            // kbMatchedRounding
            //
            kbMatchedRounding.Cursor = Cursors.Hand;
            kbMatchedRounding.Dock = DockStyle.Fill;
            kbMatchedRounding.Location = new Point(451, 43);
            kbMatchedRounding.Name = @"kbMatchedRounding";
            kbMatchedRounding.OverrideDefault.Back.Color1 = Color.YellowGreen;
            kbMatchedRounding.OverrideDefault.Back.Color2 = Color.YellowGreen;
            kbMatchedRounding.OverrideDefault.Border.Color1 = Color.YellowGreen;
            kbMatchedRounding.OverrideDefault.Border.DrawBorders = PaletteDrawBorders.All;
            kbMatchedRounding.OverrideFocus.Border.Color1 = DemoOutline;
            kbMatchedRounding.OverrideFocus.Border.Color2 = DemoOutline;
            kbMatchedRounding.OverrideFocus.Border.DrawBorders = PaletteDrawBorders.All;
            kbMatchedRounding.OverrideFocus.Border.Width = 1;
            kbMatchedRounding.OverrideFocus.Border.Rounding = 50F;
            kbMatchedRounding.StateCommon.Back.Color1 = Color.FromArgb(0, 132, 0);
            kbMatchedRounding.StateCommon.Back.Color2 = Color.FromArgb(0, 132, 0);
            kbMatchedRounding.StateCommon.Border.Color1 = DemoOutline;
            kbMatchedRounding.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            kbMatchedRounding.StateCommon.Border.Rounding = 50F;
            kbMatchedRounding.StateCommon.Border.Width = 1;
            kbMatchedRounding.StateCommon.Content.ShortText.Color1 = Color.White;
            kbMatchedRounding.StateCommon.Content.ShortText.Font = new Font(@"Century Gothic", 28F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            kbMatchedRounding.StateCommon.Content.ShortText.Hint = PaletteTextHint.AntiAlias;
            kbMatchedRounding.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Center;
            kbMatchedRounding.StateCommon.Content.ShortText.TextV = PaletteRelativeAlign.Center;
            kbMatchedRounding.StateTracking.Back.Color1 = Color.DarkSeaGreen;
            kbMatchedRounding.StateTracking.Back.Color2 = Color.DarkSeaGreen;
            kbMatchedRounding.StateTracking.Border.Color1 = DemoOutline;
            kbMatchedRounding.StateTracking.Border.Color2 = DemoOutline;
            kbMatchedRounding.StateTracking.Border.Rounding = 50F;
            kbMatchedRounding.StateTracking.Border.Width = 1;
            kbMatchedRounding.TabIndex = 3;
            kbMatchedRounding.Values.Text = @"Matched rounding";
            kbMatchedRounding.Values.DropDownArrowColor = Color.Empty;
            //
            // Bug3383KryptonButtonStateTrackingRoundingDemo
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 480);
            Controls.Add(kryptonPanelMain);
            Controls.Add(kwlblInfo);
            MinimumSize = new Size(640, 380);
            Name = @"Bug3383KryptonButtonStateTrackingRoundingDemo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"Bug #3383 - KryptonButton StateTracking rounding (focus merge)";
            ((System.ComponentModel.ISupportInitialize)kryptonPanelMain).EndInit();
            kryptonPanelMain.ResumeLayout(false);
            kryptonPanelMain.PerformLayout();
            tlpMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private KryptonWrapLabel kwlblInfo;
        private KryptonPanel kryptonPanelMain;
        private TableLayoutPanel tlpMain;
        private KryptonLabel klblReproCaption;
        private KryptonLabel klblControlCaption;
        private KryptonButton kbIssuePattern;
        private KryptonButton kbMatchedRounding;
    }
}
