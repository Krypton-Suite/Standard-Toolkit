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
    partial class Feature882NavigatorTaskbarThumbnailsDemo
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.kchkEnabled = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAllowClose = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnAddPage = new Krypton.Toolkit.KryptonButton();
            this.kbtnToggleWizardExclude = new Krypton.Toolkit.KryptonButton();
            this.kryptonNavigator = new Krypton.Navigator.KryptonNavigator();
            this.pageAlpha = new Krypton.Navigator.KryptonPage();
            this.klblAlpha = new Krypton.Toolkit.KryptonLabel();
            this.pageBeta = new Krypton.Navigator.KryptonPage();
            this.klblBeta = new Krypton.Toolkit.KryptonLabel();
            this.pageGamma = new Krypton.Navigator.KryptonPage();
            this.klblGamma = new Krypton.Toolkit.KryptonLabel();
            this.pageWizardStep = new Krypton.Navigator.KryptonPage();
            this.klblWizard = new Krypton.Toolkit.KryptonLabel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.taskbarThumbnails = new Krypton.Navigator.Utilities.KryptonNavigatorTaskbarThumbnails(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator)).BeginInit();
            this.kryptonNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageAlpha)).BeginInit();
            this.pageAlpha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageBeta)).BeginInit();
            this.pageBeta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageGamma)).BeginInit();
            this.pageGamma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageWizardStep)).BeginInit();
            this.pageWizardStep.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(900, 560);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowToolbar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonNavigator, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblStatus, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(876, 536);
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
            this.kwlblInstructions.Text = "Issue #882 — KryptonNavigatorTaskbarThumbnails (Krypton.Navigator.Utilities).\r\n\r\n1. Leave \"Enable taskbar thumbnails\" checked.\r\n2. Hover this form's taskbar button — you should see one thumbnail per document page (Alpha/Beta/Gamma), not the wizard step (flag cleared).\r\n3. Click a thumbnail to select that page. Optional: close from the thumbnail X. Toggle the wizard flag to include/exclude it.";
            this.kwlblInstructions.TabIndex = 0;
            //
            // flowToolbar
            //
            this.flowToolbar.Controls.Add(this.kchkEnabled);
            this.flowToolbar.Controls.Add(this.kchkAllowClose);
            this.flowToolbar.Controls.Add(this.kbtnAddPage);
            this.flowToolbar.Controls.Add(this.kbtnToggleWizardExclude);
            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowToolbar.Location = new System.Drawing.Point(3, 75);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Size = new System.Drawing.Size(870, 34);
            this.flowToolbar.TabIndex = 1;
            //
            // kchkEnabled
            //
            this.kchkEnabled.Checked = true;
            this.kchkEnabled.Location = new System.Drawing.Point(3, 3);
            this.kchkEnabled.Name = "kchkEnabled";
            this.kchkEnabled.Size = new System.Drawing.Size(176, 20);
            this.kchkEnabled.TabIndex = 0;
            this.kchkEnabled.Values.Text = "Enable taskbar thumbnails";
            this.kchkEnabled.CheckedChanged += new System.EventHandler(this.kchkEnabled_CheckedChanged);
            //
            // kchkAllowClose
            //
            this.kchkAllowClose.Checked = true;
            this.kchkAllowClose.Location = new System.Drawing.Point(185, 3);
            this.kchkAllowClose.Name = "kchkAllowClose";
            this.kchkAllowClose.Size = new System.Drawing.Size(168, 20);
            this.kchkAllowClose.TabIndex = 1;
            this.kchkAllowClose.Values.Text = "Allow close from thumbnail";
            this.kchkAllowClose.CheckedChanged += new System.EventHandler(this.kchkAllowClose_CheckedChanged);
            //
            // kbtnAddPage
            //
            this.kbtnAddPage.Location = new System.Drawing.Point(359, 3);
            this.kbtnAddPage.Name = "kbtnAddPage";
            this.kbtnAddPage.Size = new System.Drawing.Size(100, 25);
            this.kbtnAddPage.TabIndex = 2;
            this.kbtnAddPage.Values.Text = "Add page";
            this.kbtnAddPage.Click += new System.EventHandler(this.kbtnAddPage_Click);
            //
            // kbtnToggleWizardExclude
            //
            this.kbtnToggleWizardExclude.Location = new System.Drawing.Point(465, 3);
            this.kbtnToggleWizardExclude.Name = "kbtnToggleWizardExclude";
            this.kbtnToggleWizardExclude.Size = new System.Drawing.Size(200, 25);
            this.kbtnToggleWizardExclude.TabIndex = 3;
            this.kbtnToggleWizardExclude.Values.Text = "Toggle wizard thumbnail flag";
            this.kbtnToggleWizardExclude.Click += new System.EventHandler(this.kbtnToggleWizardExclude_Click);
            //
            // kryptonNavigator
            //
            this.kryptonNavigator.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.RemovePageAndDispose;
            this.kryptonNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator.Location = new System.Drawing.Point(3, 115);
            this.kryptonNavigator.Name = "kryptonNavigator";
            this.kryptonNavigator.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.pageAlpha,
            this.pageBeta,
            this.pageGamma,
            this.pageWizardStep});
            this.kryptonNavigator.SelectedIndex = 0;
            this.kryptonNavigator.Size = new System.Drawing.Size(870, 390);
            this.kryptonNavigator.TabIndex = 2;
            this.kryptonNavigator.SelectedPageChanged += new System.EventHandler(this.kryptonNavigator_SelectedPageChanged);
            //
            // pageAlpha
            //
            this.pageAlpha.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.pageAlpha.Controls.Add(this.klblAlpha);
            this.pageAlpha.Flags = 65534;
            this.pageAlpha.LastVisibleSet = true;
            this.pageAlpha.MinimumSize = new System.Drawing.Size(50, 50);
            this.pageAlpha.Name = "pageAlpha";
            this.pageAlpha.Size = new System.Drawing.Size(868, 359);
            this.pageAlpha.Text = "Document Alpha";
            this.pageAlpha.TextDescription = "Document Alpha";
            this.pageAlpha.TextTitle = "Document Alpha";
            this.pageAlpha.UniqueName = "Feature882-Alpha";
            //
            // klblAlpha
            //
            this.klblAlpha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblAlpha.Location = new System.Drawing.Point(0, 0);
            this.klblAlpha.Name = "klblAlpha";
            this.klblAlpha.Size = new System.Drawing.Size(868, 359);
            this.klblAlpha.TabIndex = 0;
            this.klblAlpha.Values.Text = "Document Alpha content — should appear as its own taskbar thumbnail when the feature is enabled.";
            //
            // pageBeta
            //
            this.pageBeta.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.pageBeta.Controls.Add(this.klblBeta);
            this.pageBeta.Flags = 65534;
            this.pageBeta.LastVisibleSet = true;
            this.pageBeta.MinimumSize = new System.Drawing.Size(50, 50);
            this.pageBeta.Name = "pageBeta";
            this.pageBeta.Size = new System.Drawing.Size(868, 359);
            this.pageBeta.Text = "Document Beta";
            this.pageBeta.TextDescription = "Document Beta";
            this.pageBeta.TextTitle = "Document Beta";
            this.pageBeta.UniqueName = "Feature882-Beta";
            //
            // klblBeta
            //
            this.klblBeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblBeta.Location = new System.Drawing.Point(0, 0);
            this.klblBeta.Name = "klblBeta";
            this.klblBeta.Size = new System.Drawing.Size(868, 359);
            this.klblBeta.TabIndex = 0;
            this.klblBeta.Values.Text = "Document Beta content — switch here via the taskbar thumbnail flyout.";
            //
            // pageGamma
            //
            this.pageGamma.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.pageGamma.Controls.Add(this.klblGamma);
            this.pageGamma.Flags = 65534;
            this.pageGamma.LastVisibleSet = true;
            this.pageGamma.MinimumSize = new System.Drawing.Size(50, 50);
            this.pageGamma.Name = "pageGamma";
            this.pageGamma.Size = new System.Drawing.Size(868, 359);
            this.pageGamma.Text = "Document Gamma";
            this.pageGamma.TextDescription = "Document Gamma";
            this.pageGamma.TextTitle = "Document Gamma";
            this.pageGamma.UniqueName = "Feature882-Gamma";
            //
            // klblGamma
            //
            this.klblGamma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblGamma.Location = new System.Drawing.Point(0, 0);
            this.klblGamma.Name = "klblGamma";
            this.klblGamma.Size = new System.Drawing.Size(868, 359);
            this.klblGamma.TabIndex = 0;
            this.klblGamma.Values.Text = "Document Gamma content — add more pages with Add page to grow the flyout.";
            //
            // pageWizardStep
            //
            this.pageWizardStep.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.pageWizardStep.Controls.Add(this.klblWizard);
            this.pageWizardStep.Flags = 63486;
            this.pageWizardStep.LastVisibleSet = true;
            this.pageWizardStep.MinimumSize = new System.Drawing.Size(50, 50);
            this.pageWizardStep.Name = "pageWizardStep";
            this.pageWizardStep.Size = new System.Drawing.Size(868, 359);
            this.pageWizardStep.Text = "Wizard Step (excluded)";
            this.pageWizardStep.TextDescription = "Wizard step excluded from thumbnails";
            this.pageWizardStep.TextTitle = "Wizard Step";
            this.pageWizardStep.UniqueName = "Feature882-Wizard";
            //
            // klblWizard
            //
            this.klblWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblWizard.Location = new System.Drawing.Point(0, 0);
            this.klblWizard.Name = "klblWizard";
            this.klblWizard.Size = new System.Drawing.Size(868, 359);
            this.klblWizard.TabIndex = 0;
            this.klblWizard.Values.Text = "Wizard-style page with AllowTaskbarThumbnail cleared (Flags without 0x0800). It should not appear in the taskbar flyout until you toggle the flag.";
            //
            // klblStatus
            //
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 508);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(870, 25);
            this.klblStatus.TabIndex = 3;
            this.klblStatus.Values.Text = "Status";
            //
            // Feature882NavigatorTaskbarThumbnailsDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.kryptonPanelMain);
            this.Name = "Feature882NavigatorTaskbarThumbnailsDemo";
            this.Text = "Feature 882 — Navigator Taskbar Thumbnails";
            this.Load += new System.EventHandler(this.Feature882NavigatorTaskbarThumbnailsDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowToolbar.ResumeLayout(false);
            this.flowToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator)).EndInit();
            this.kryptonNavigator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pageAlpha)).EndInit();
            this.pageAlpha.ResumeLayout(false);
            this.pageAlpha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageBeta)).EndInit();
            this.pageBeta.ResumeLayout(false);
            this.pageBeta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageGamma)).EndInit();
            this.pageGamma.ResumeLayout(false);
            this.pageGamma.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageWizardStep)).EndInit();
            this.pageWizardStep.ResumeLayout(false);
            this.pageWizardStep.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInstructions;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private Krypton.Toolkit.KryptonCheckBox kchkEnabled;
        private Krypton.Toolkit.KryptonCheckBox kchkAllowClose;
        private Krypton.Toolkit.KryptonButton kbtnAddPage;
        private Krypton.Toolkit.KryptonButton kbtnToggleWizardExclude;
        private Krypton.Navigator.KryptonNavigator kryptonNavigator;
        private Krypton.Navigator.KryptonPage pageAlpha;
        private Krypton.Toolkit.KryptonLabel klblAlpha;
        private Krypton.Navigator.KryptonPage pageBeta;
        private Krypton.Toolkit.KryptonLabel klblBeta;
        private Krypton.Navigator.KryptonPage pageGamma;
        private Krypton.Toolkit.KryptonLabel klblGamma;
        private Krypton.Navigator.KryptonPage pageWizardStep;
        private Krypton.Toolkit.KryptonLabel klblWizard;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Navigator.Utilities.KryptonNavigatorTaskbarThumbnails taskbarThumbnails;
    }
}
