#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug2984SeparatorTest
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
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonNavigator1 = new Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new Krypton.Navigator.KryptonPage();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPage2 = new Krypton.Navigator.KryptonPage();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.kryptonPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.kryptonSplitContainer1);
            this.kryptonPanelMain.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(684, 441);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Global;
            this.kryptonThemeComboBox1.DropDownWidth = 200;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(12, 12);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(200, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            //
            // kryptonSplitContainer1
            //
            this.kryptonSplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(12, 44);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            this.kryptonSplitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
            //
            // kryptonSplitContainer1.Panel1
            //
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonNavigator1);
            //
            // kryptonSplitContainer1.Panel2
            //
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.kryptonPanel1);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(660, 373);
            this.kryptonSplitContainer1.SplitterDistance = 220;
            this.kryptonSplitContainer1.TabIndex = 1;
            //
            // kryptonNavigator1
            //
            this.kryptonNavigator1.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator1.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.RemovePageAndDispose;
            this.kryptonNavigator1.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.ControlKryptonFormFeatures = false;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Header.HeaderPositionBar = Krypton.Toolkit.VisualOrientation.Top;
            this.kryptonNavigator1.Header.HeaderPositionPrimary = Krypton.Toolkit.VisualOrientation.Top;
            this.kryptonNavigator1.Header.HeaderPositionSecondary = Krypton.Toolkit.VisualOrientation.Bottom;
            this.kryptonNavigator1.Header.HeaderStyleBar = Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonNavigator1.Header.HeaderStylePrimary = Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonNavigator1.Header.HeaderStyleSecondary = Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonNavigator1.Header.HeaderValuesPrimary.MapDescription = Krypton.Navigator.MapKryptonPageText.None;
            this.kryptonNavigator1.Header.HeaderValuesPrimary.MapHeading = Krypton.Navigator.MapKryptonPageText.TitleText;
            this.kryptonNavigator1.Header.HeaderValuesPrimary.MapImage = Krypton.Navigator.MapKryptonPageImage.None;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.NavigatorMode = Krypton.Navigator.NavigatorMode.OutlookFull;
            this.kryptonNavigator1.Owner = null;
            this.kryptonNavigator1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonNavigator1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
                this.kryptonPage1,
                this.kryptonPage2});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(220, 389);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            //
            // kryptonPage1
            //
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.kryptonLabel1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(150, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Padding = new System.Windows.Forms.Padding(8);
            this.kryptonPage1.Size = new System.Drawing.Size(218, 367);
            this.kryptonPage1.Text = "Page 1";
            this.kryptonPage1.TextDescription = "First page";
            this.kryptonPage1.TextTitle = "Page 1";
            this.kryptonPage1.ToolTipTitle = "Page 1";
            this.kryptonPage1.UniqueName = "page1";
            //
            // kryptonLabel1
            //
            this.kryptonLabel1.Location = new System.Drawing.Point(11, 11);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(196, 60);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Outlook Navigator (has separator between stack and content).\r\n\r\nSwitch themes to test #2984 fix.";
            //
            // kryptonPage2
            //
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.kryptonLabel2);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(150, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Padding = new System.Windows.Forms.Padding(8);
            this.kryptonPage2.Size = new System.Drawing.Size(218, 367);
            this.kryptonPage2.Text = "Page 2";
            this.kryptonPage2.TextDescription = "Second page";
            this.kryptonPage2.TextTitle = "Page 2";
            this.kryptonPage2.ToolTipTitle = "Page 2";
            this.kryptonPage2.UniqueName = "page2";
            //
            // kryptonLabel2
            //
            this.kryptonLabel2.Location = new System.Drawing.Point(11, 11);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(196, 40);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "SplitContainer has a separator.\r\nKryptonSeparator below.";
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.kryptonSeparator1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(460, 389);
            this.kryptonPanel1.TabIndex = 0;
            //
            // kryptonSeparator1
            //
            this.kryptonSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonSeparator1.Location = new System.Drawing.Point(15, 80);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.kryptonSeparator1.Size = new System.Drawing.Size(430, 5);
            this.kryptonSeparator1.TabIndex = 0;
            //
            // kryptonLabel3
            //
            this.kryptonLabel3.Location = new System.Drawing.Point(15, 15);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(430, 60);
            this.kryptonLabel3.TabIndex = 1;
            this.kryptonLabel3.Values.Text = "Issue #2984: NullReferenceException in ViewDrawSeparator.RenderBefore.\r\n\r\nThis demo shows KryptonNavigator (Outlook), KryptonSplitContainer, and KryptonSeparator. Change theme above to verify no crash during paint.";
            //
            // Bug2984SeparatorTest
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 441);
            this.Controls.Add(this.kryptonPanelMain);
            this.Name = "Bug2984SeparatorTest";
            this.Text = "Bug 2984 - ViewDrawSeparator NullReferenceException Demo";
            this.Controls.SetChildIndex(this.kryptonPanelMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.kryptonPage2.ResumeLayout(false);
            this.kryptonPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Navigator.KryptonPage kryptonPage2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
    }
}
