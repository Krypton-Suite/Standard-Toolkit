#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

[EditorBrowsable(EditorBrowsableState.Never), ToolboxItem(false)]
[DesignerCategory("code")]
[Description("Enables you to group collections of controls.")]
public class KryptonButtonPanel : UserControl
{
    private KryptonPanel kpnlContainer;
    private KryptonBorderEdge kbeTopDivider;
    private void InitializeComponent()
    {
        kpnlContainer = new KryptonPanel();
        kbeTopDivider = new KryptonBorderEdge();
        ((ISupportInitialize)(kpnlContainer)).BeginInit();
        kpnlContainer.SuspendLayout();
        SuspendLayout();
        //
        // kpnlContainer
        //
        kpnlContainer.Controls.Add(kbeTopDivider);
        kpnlContainer.Dock = DockStyle.Fill;
        kpnlContainer.Location = new Point(0, 0);
        kpnlContainer.Name = "kpnlContainer";
        kpnlContainer.Size = new Size(280, 50);
        kpnlContainer.TabIndex = 0;
        //
        // kbeTopDivider
        //
        kbeTopDivider.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        kbeTopDivider.Dock = DockStyle.Top;
        kbeTopDivider.Location = new Point(0, 0);
        kbeTopDivider.Name = "kbeTopDivider";
        kbeTopDivider.Size = new Size(280, 1);
        kbeTopDivider.Text = "kbeTopDivider";
        // 
        // KryptonButtonPanel
        // 
        Controls.Add(kpnlContainer);
        Name = "KryptonButtonPanel";
        Size = new Size(247, 50);
        ((ISupportInitialize)(kpnlContainer)).EndInit();
        kpnlContainer.ResumeLayout(false);
        kpnlContainer.PerformLayout();
        ResumeLayout(false);

    }
}