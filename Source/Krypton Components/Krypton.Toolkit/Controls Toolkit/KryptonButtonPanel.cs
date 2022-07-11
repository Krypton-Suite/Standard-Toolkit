#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [EditorBrowsable(EditorBrowsableState.Never), ToolboxItem(false)]
    public class KryptonButtonPanel : UserControl
    {
        private KryptonPanel kpnlContainer;
        private KryptonBorderEdge kbeTopDivider;
        private void InitializeComponent()
        {
            this.kpnlContainer = new Krypton.Toolkit.KryptonPanel();
            this.kbeTopDivider = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContainer)).BeginInit();
            this.kpnlContainer.SuspendLayout();
            this.SuspendLayout();
            //
            // kpnlContainer
            //
            this.kpnlContainer.Controls.Add(this.kbeTopDivider);
            this.kpnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContainer.Location = new System.Drawing.Point(0, 0);
            this.kpnlContainer.Name = "kpnlContainer";
            this.kpnlContainer.Size = new System.Drawing.Size(280, 50);
            this.kpnlContainer.TabIndex = 0;
            //
            // kbeTopDivider
            //
            this.kbeTopDivider.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kbeTopDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.kbeTopDivider.Location = new System.Drawing.Point(0, 0);
            this.kbeTopDivider.Name = "kbeTopDivider";
            this.kbeTopDivider.Size = new System.Drawing.Size(280, 1);
            this.kbeTopDivider.Text = "kbeTopDivider";
            // 
            // KryptonButtonPanel
            // 
            this.Controls.Add(kpnlContainer);
            this.Name = "KryptonButtonPanel";
            this.Size = new System.Drawing.Size(247, 50);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContainer)).EndInit();
            this.kpnlContainer.ResumeLayout(false);
            this.kpnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}