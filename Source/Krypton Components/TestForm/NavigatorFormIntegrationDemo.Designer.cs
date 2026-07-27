#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Navigator.Utilities;

namespace TestForm;

partial class NavigatorFormIntegrationDemo
{
    private System.ComponentModel.IContainer components = null!;

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
        components = new Container();
        var page1 = new KryptonPage();
        var page2 = new KryptonPage();
        var page3 = new KryptonPage();
        var label1 = new KryptonLabel();
        var label2 = new KryptonLabel();
        var label3 = new KryptonLabel();
        kryptonPanel1 = new KryptonPanel();
        klblInstructions = new KryptonLabel();
        klblStatus = new KryptonLabel();
        kryptonPanel2 = new KryptonPanel();
        chkEnabled = new KryptonCheckBox();
        chkSyncTitle = new KryptonCheckBox();
        chkTearOutEnabled = new KryptonCheckBox();
        chkCloseEmptySourceWindow = new KryptonCheckBox();
        chkShowNewTabButton = new KryptonCheckBox();
        cmbMode = new KryptonComboBox();
        klblMode = new KryptonLabel();
        btnAddPage = new KryptonButton();
        kryptonNavigator1 = new KryptonNavigator();
        kryptonNavigatorFormIntegrator1 = new KryptonNavigatorFormIntegrator(components);
        ((ISupportInitialize)kryptonPanel1).BeginInit();
        kryptonPanel1.SuspendLayout();
        ((ISupportInitialize)kryptonPanel2).BeginInit();
        kryptonPanel2.SuspendLayout();
        ((ISupportInitialize)cmbMode).BeginInit();
        ((ISupportInitialize)kryptonNavigator1).BeginInit();
        ((ISupportInitialize)page1).BeginInit();
        page1.SuspendLayout();
        ((ISupportInitialize)page2).BeginInit();
        page2.SuspendLayout();
        ((ISupportInitialize)page3).BeginInit();
        page3.SuspendLayout();
        SuspendLayout();
        //
        // page1
        //
        page1.AutoHiddenSlideSize = new Size(200, 200);
        page1.Controls.Add(label1);
        page1.Flags = 65534;
        page1.LastVisibleSet = true;
        page1.MinimumSize = new Size(50, 50);
        page1.Name = "page1";
        page1.Size = new Size(776, 353);
        page1.Text = "Home";
        page1.TextTitle = "Home";
        page1.UniqueName = "HomePage925";
        //
        // label1
        //
        label1.Dock = DockStyle.Fill;
        label1.Location = new Point(0, 0);
        label1.Name = "label1";
        label1.Size = new Size(776, 353);
        label1.TabIndex = 0;
        label1.Values.Text = "Home page\r\n\r\nCaptionIntegrated injects tabs into the form title bar (form keeps min/max/close).\r\nClientChrome hides the form control box and shows form buttons on the navigator tab bar.\r\nCaptionAdjacent keeps a normal caption and optional title sync.";
        //
        // page2
        //
        page2.AutoHiddenSlideSize = new Size(200, 200);
        page2.Controls.Add(label2);
        page2.Flags = 65534;
        page2.LastVisibleSet = true;
        page2.MinimumSize = new Size(50, 50);
        page2.Name = "page2";
        page2.Size = new Size(776, 353);
        page2.Text = "Reports";
        page2.TextTitle = "Reports";
        page2.UniqueName = "ReportsPage925";
        //
        // label2
        //
        label2.Dock = DockStyle.Fill;
        label2.Location = new Point(0, 0);
        label2.Name = "label2";
        label2.Size = new Size(776, 353);
        label2.TabIndex = 0;
        label2.Values.Text = "Reports page — switch tabs and watch Form.Text update when Sync title is checked.";
        //
        // page3
        //
        page3.AutoHiddenSlideSize = new Size(200, 200);
        page3.Controls.Add(label3);
        page3.Flags = 65534;
        page3.LastVisibleSet = true;
        page3.MinimumSize = new Size(50, 50);
        page3.Name = "page3";
        page3.Size = new Size(776, 353);
        page3.Text = "Settings";
        page3.TextTitle = "Settings";
        page3.UniqueName = "SettingsPage925";
        //
        // label3
        //
        label3.Dock = DockStyle.Fill;
        label3.Location = new Point(0, 0);
        label3.Name = "label3";
        label3.Size = new Size(776, 353);
        label3.TabIndex = 0;
        label3.Values.Text = "Settings page — try theme changes from the TestForm host if available.";
        //
        // kryptonPanel1
        //
        kryptonPanel1.Controls.Add(klblStatus);
        kryptonPanel1.Controls.Add(klblInstructions);
        kryptonPanel1.Dock = DockStyle.Bottom;
        kryptonPanel1.Location = new Point(0, 451);
        kryptonPanel1.Name = "kryptonPanel1";
        kryptonPanel1.Padding = new Padding(8);
        kryptonPanel1.Size = new Size(784, 72);
        kryptonPanel1.TabIndex = 2;
        //
        // klblInstructions
        //
        klblInstructions.Dock = DockStyle.Top;
        klblInstructions.Location = new Point(8, 8);
        klblInstructions.Name = "klblInstructions";
        klblInstructions.Size = new Size(768, 20);
        klblInstructions.TabIndex = 0;
        klblInstructions.Values.Text = "Issue #925: CaptionIntegrated puts tabs in the form title bar. ClientChrome moves min/max/close onto the navigator. CaptionAdjacent keeps a normal caption. Use the optional '+' next to the last tab (or Add page / right-click New tab) to create tabs. Drag tabs between windows or out to tear out when enabled.";
        //
        // klblStatus
        //
        klblStatus.Dock = DockStyle.Fill;
        klblStatus.Location = new Point(8, 28);
        klblStatus.Name = "klblStatus";
        klblStatus.Size = new Size(768, 36);
        klblStatus.TabIndex = 1;
        klblStatus.Values.Text = "Status";
        //
        // kryptonPanel2
        //
        kryptonPanel2.Controls.Add(btnAddPage);
        kryptonPanel2.Controls.Add(chkSyncTitle);
        kryptonPanel2.Controls.Add(chkEnabled);
        kryptonPanel2.Controls.Add(chkTearOutEnabled);
        kryptonPanel2.Controls.Add(chkCloseEmptySourceWindow);
        kryptonPanel2.Controls.Add(chkShowNewTabButton);
        kryptonPanel2.Controls.Add(cmbMode);
        kryptonPanel2.Controls.Add(klblMode);
        kryptonPanel2.Dock = DockStyle.Top;
        kryptonPanel2.Location = new Point(0, 0);
        kryptonPanel2.Name = "kryptonPanel2";
        kryptonPanel2.Padding = new Padding(8);
        kryptonPanel2.Size = new Size(784, 68);
        kryptonPanel2.TabIndex = 0;
        //
        // klblMode
        //
        klblMode.Location = new Point(11, 14);
        klblMode.Name = "klblMode";
        klblMode.Size = new Size(39, 20);
        klblMode.TabIndex = 0;
        klblMode.Values.Text = "Mode";
        //
        // cmbMode
        //
        cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMode.IntegralHeight = false;
        cmbMode.Location = new Point(56, 12);
        cmbMode.Name = "cmbMode";
        cmbMode.Size = new Size(180, 21);
        cmbMode.TabIndex = 1;
        cmbMode.Items.AddRange(new object[] { "CaptionIntegrated", "ClientChrome", "CaptionAdjacent" });
        cmbMode.SelectedIndexChanged += CmbMode_SelectedIndexChanged;
        //
        // chkEnabled
        //
        chkEnabled.Checked = true;
        chkEnabled.CheckState = CheckState.Checked;
        chkEnabled.Location = new Point(250, 14);
        chkEnabled.Name = "chkEnabled";
        chkEnabled.Size = new Size(73, 20);
        chkEnabled.TabIndex = 2;
        chkEnabled.Values.Text = "Enabled";
        chkEnabled.CheckedChanged += ChkEnabled_CheckedChanged;
        //
        // chkSyncTitle
        //
        chkSyncTitle.Checked = false;
        chkSyncTitle.CheckState = CheckState.Unchecked;
        chkSyncTitle.Location = new Point(340, 14);
        chkSyncTitle.Name = "chkSyncTitle";
        chkSyncTitle.Size = new Size(84, 20);
        chkSyncTitle.TabIndex = 3;
        chkSyncTitle.Values.Text = "Sync title";
        chkSyncTitle.CheckedChanged += ChkSyncTitle_CheckedChanged;
        //
        // btnAddPage
        //
        btnAddPage.Location = new Point(700, 30);
        btnAddPage.Name = "btnAddPage";
        btnAddPage.Size = new Size(100, 28);
        btnAddPage.TabIndex = 4;
        btnAddPage.Values.Text = "Add page";
        btnAddPage.Click += BtnAddPage_Click;
        //
        // chkTearOutEnabled
        //
        chkTearOutEnabled.Checked = true;
        chkTearOutEnabled.CheckState = CheckState.Checked;
        chkTearOutEnabled.Location = new Point(250, 36);
        chkTearOutEnabled.Name = "chkTearOutEnabled";
        chkTearOutEnabled.Size = new Size(120, 20);
        chkTearOutEnabled.TabIndex = 5;
        chkTearOutEnabled.Values.Text = "Tear out";
        chkTearOutEnabled.CheckedChanged += ChkTearOutEnabled_CheckedChanged;
        //
        // chkCloseEmptySourceWindow
        //
        chkCloseEmptySourceWindow.Checked = true;
        chkCloseEmptySourceWindow.CheckState = CheckState.Checked;
        chkCloseEmptySourceWindow.Location = new Point(380, 36);
        chkCloseEmptySourceWindow.Name = "chkCloseEmptySourceWindow";
        chkCloseEmptySourceWindow.Size = new Size(150, 20);
        chkCloseEmptySourceWindow.TabIndex = 6;
        chkCloseEmptySourceWindow.Values.Text = "Close empty window";
        chkCloseEmptySourceWindow.CheckedChanged += ChkCloseEmptySourceWindow_CheckedChanged;
        //
        // chkShowNewTabButton
        //
        chkShowNewTabButton.Checked = true;
        chkShowNewTabButton.CheckState = CheckState.Checked;
        chkShowNewTabButton.Location = new Point(540, 36);
        chkShowNewTabButton.Name = "chkShowNewTabButton";
        chkShowNewTabButton.Size = new Size(140, 20);
        chkShowNewTabButton.TabIndex = 7;
        chkShowNewTabButton.Values.Text = "New-tab '+' button";
        chkShowNewTabButton.CheckedChanged += ChkShowNewTabButton_CheckedChanged;
        //
        // kryptonNavigator1
        //
        kryptonNavigator1.Button.CloseButtonDisplay = ButtonDisplay.Hide;
        kryptonNavigator1.Dock = DockStyle.Fill;
        kryptonNavigator1.Location = new Point(0, 68);
        kryptonNavigator1.Name = "kryptonNavigator1";
        kryptonNavigator1.NavigatorMode = NavigatorMode.BarTabOnly;
        kryptonNavigator1.Pages.AddRange(new[] { page1, page2, page3 });
        kryptonNavigator1.SelectedIndex = 0;
        kryptonNavigator1.Size = new Size(784, 383);
        kryptonNavigator1.TabIndex = 1;
        kryptonNavigator1.Text = "kryptonNavigator1";
        //
        // kryptonNavigatorFormIntegrator1
        //
        kryptonNavigatorFormIntegrator1.Mode = NavigatorFormIntegrationMode.CaptionIntegrated;
        kryptonNavigatorFormIntegrator1.SyncFormTitle = false;
        //
        // NavigatorFormIntegrationDemo
        //
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 523);
        Controls.Add(kryptonNavigator1);
        Controls.Add(kryptonPanel2);
        Controls.Add(kryptonPanel1);
        MinimumSize = new Size(640, 400);
        Name = "NavigatorFormIntegrationDemo";
        Text = "Navigator Form Integration (#925)";
        ((ISupportInitialize)kryptonPanel1).EndInit();
        kryptonPanel1.ResumeLayout(false);
        kryptonPanel1.PerformLayout();
        ((ISupportInitialize)kryptonPanel2).EndInit();
        kryptonPanel2.ResumeLayout(false);
        kryptonPanel2.PerformLayout();
        ((ISupportInitialize)cmbMode).EndInit();
        ((ISupportInitialize)page1).EndInit();
        page1.ResumeLayout(false);
        page1.PerformLayout();
        ((ISupportInitialize)page2).EndInit();
        page2.ResumeLayout(false);
        page2.PerformLayout();
        ((ISupportInitialize)page3).EndInit();
        page3.ResumeLayout(false);
        page3.PerformLayout();
        ((ISupportInitialize)kryptonNavigator1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private KryptonPanel kryptonPanel1;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel klblInstructions;
    private KryptonLabel klblStatus;
    private KryptonLabel klblMode;
    private KryptonComboBox cmbMode;
    private KryptonCheckBox chkEnabled;
    private KryptonCheckBox chkSyncTitle;
    private KryptonCheckBox chkTearOutEnabled;
    private KryptonCheckBox chkCloseEmptySourceWindow;
    private KryptonCheckBox chkShowNewTabButton;
    private KryptonButton btnAddPage;
    private KryptonNavigator kryptonNavigator1;
    private KryptonNavigatorFormIntegrator kryptonNavigatorFormIntegrator1;
}
