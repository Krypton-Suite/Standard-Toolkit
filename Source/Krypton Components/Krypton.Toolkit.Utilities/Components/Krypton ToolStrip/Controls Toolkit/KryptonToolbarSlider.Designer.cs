#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

partial class KryptonToolbarSlider
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
        this.kplus = new KryptonSliderButton();
        this.kminus = new KryptonSliderButton();
        this.SuspendLayout();
        // 
        // kplus
        // 
        this.kplus.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.kplus.BackColor = System.Drawing.Color.Transparent;
        this.kplus.ButtonStyle = KryptonSliderButton.ButtonStyles.PlusButton;
        this.kplus.EventFireRate = 200;
        this.kplus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.kplus.Location = new System.Drawing.Point(124, 0);
        this.kplus.Name = "kplus";
        this.kplus.Orientation = Krypton.Toolkit.VisualOrientation.Top;
        this.kplus.SingleClick = false;
        this.kplus.TabIndex = 1;
        this.kplus.VisualLook = Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
        this.kplus.SliderButtonFire += new KryptonSliderButton.SliderButtonFireEventHandler(this.kplus_SliderButtonFire);
        // 
        // kminus
        // 
        this.kminus.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.kminus.BackColor = System.Drawing.Color.Transparent;
        this.kminus.ButtonStyle = KryptonSliderButton.ButtonStyles.MinusButton;
        this.kminus.EventFireRate = 200;
        this.kminus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.kminus.Location = new System.Drawing.Point(0, 0);
        this.kminus.Name = "kminus";
        this.kminus.Orientation = Krypton.Toolkit.VisualOrientation.Top;
        this.kminus.SingleClick = false;
        this.kminus.TabIndex = 0;
        this.kminus.VisualLook = Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
        this.kminus.SliderButtonFire += new KryptonSliderButton.SliderButtonFireEventHandler(this.kminus_SliderButtonFire);
        // 
        // KryptonToolbarSlider
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.BackColor = System.Drawing.Color.Transparent;
        this.Controls.Add(this.kplus);
        this.Controls.Add(this.kminus);
        this.DoubleBuffered = true;
        this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Name = "KryptonToolbarSlider";
        this.Size = new System.Drawing.Size(140, this.kplus.Height);
        this.kplus.Location = new System.Drawing.Point(this.Width - this.kplus.Width, 0);
        this.MouseLeave += new System.EventHandler(this.KryptonSliderButton_MouseLeave);
        this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KryptonSlider_MouseMove);
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KryptonSliderButton_MouseDown);
        this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KryptonSliderButton_MouseUp);
        this.MouseEnter += new System.EventHandler(this.KryptonSliderButton_MouseEnter);
        this.ResumeLayout(false);
    }

    internal KryptonSliderButton kminus;
    internal KryptonSliderButton kplus;
}
