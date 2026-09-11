#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;

namespace TestForm;

partial class ToolkitRtlGalleryDemo
{
    private IContainer components = null!;
    private KryptonPanel kpanelTop;
    private KryptonLabel klblInstructions;
    private KryptonCheckBox kchkDualRtl;
    private KryptonNavigator kryptonNavigator;

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
        kpanelTop = new KryptonPanel();
        klblInstructions = new KryptonLabel();
        kchkDualRtl = new KryptonCheckBox();
        kryptonNavigator = new KryptonNavigator();
        ((ISupportInitialize)kpanelTop).BeginInit();
        kpanelTop.SuspendLayout();
        ((ISupportInitialize)kryptonNavigator).BeginInit();
        SuspendLayout();
        //
        // kpanelTop
        //
        kpanelTop.Controls.Add(kchkDualRtl);
        kpanelTop.Controls.Add(klblInstructions);
        kpanelTop.Dock = DockStyle.Top;
        kpanelTop.Location = new Point(0, 0);
        kpanelTop.Name = "kpanelTop";
        kpanelTop.Padding = new Padding(12, 8, 12, 8);
        kpanelTop.Size = new Size(1100, 88);
        kpanelTop.TabIndex = 0;
        //
        // klblInstructions
        //
        klblInstructions.Location = new Point(12, 8);
        klblInstructions.Name = "klblInstructions";
        klblInstructions.Size = new Size(1076, 44);
        klblInstructions.TabIndex = 0;
        klblInstructions.Values.Text =
            "Issue #2379 — every visual Krypton.Toolkit control with the two-flag RTL contract. " +
            "Check Dual RTL (RightToLeft.Yes + RightToLeftLayout) to mirror docks, spin chrome, lists, and captions. " +
            "Uncheck to compare LTR. Tray components (Manager, Timer, dialogs as components) are not hosted here.";
        //
        // kchkDualRtl
        //
        kchkDualRtl.Location = new Point(12, 56);
        kchkDualRtl.Name = "kchkDualRtl";
        kchkDualRtl.Size = new Size(280, 24);
        kchkDualRtl.TabIndex = 1;
        kchkDualRtl.Values.Text = "Dual RTL (RightToLeft + RightToLeftLayout)";
        //
        // kryptonNavigator
        //
        kryptonNavigator.Dock = DockStyle.Fill;
        kryptonNavigator.Location = new Point(0, 88);
        kryptonNavigator.Name = "kryptonNavigator";
        kryptonNavigator.NavigatorMode = NavigatorMode.BarTabGroup;
        kryptonNavigator.Size = new Size(1100, 632);
        kryptonNavigator.TabIndex = 1;
        kryptonNavigator.Text = "kryptonNavigator";
        //
        // ToolkitRtlGalleryDemo
        //
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 720);
        Controls.Add(kryptonNavigator);
        Controls.Add(kpanelTop);
        MinimumSize = new Size(800, 500);
        Name = "ToolkitRtlGalleryDemo";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Toolkit RTL Gallery (#2379)";
        ((ISupportInitialize)kpanelTop).EndInit();
        kpanelTop.ResumeLayout(false);
        kpanelTop.PerformLayout();
        ((ISupportInitialize)kryptonNavigator).EndInit();
        ResumeLayout(false);
    }

    #endregion
}
