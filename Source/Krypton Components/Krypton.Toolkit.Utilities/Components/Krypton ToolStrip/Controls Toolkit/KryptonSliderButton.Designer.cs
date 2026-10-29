#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

partial class KryptonSliderButton
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.FireTimer = new System.Windows.Forms.Timer(this.components);
        this.SuspendLayout();
        // 
        // FireTimer
        // 
        this.FireTimer.Interval = 1000;
        this.FireTimer.Tick += new System.EventHandler(this.FireTimer_Tick);
        // 
        // KryptonSliderButton
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.DoubleBuffered = true;
        this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        this.Name = "KryptonSliderButton";
        this.Size = new System.Drawing.Size(16, 16);
        this.MouseLeave += new System.EventHandler(this.KryptonSliderButton_MouseLeave);
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KryptonSliderButton_MouseDown);
        this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KryptonSliderButton_MouseUp);
        this.MouseEnter += new System.EventHandler(this.KryptonSliderButton_MouseEnter);
        this.ResumeLayout(false);
    }

    internal System.Windows.Forms.Timer FireTimer;
}
