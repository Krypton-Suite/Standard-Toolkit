namespace TestForm
{
    partial class WinformsForm
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinformsForm));
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.kryptonCommandLinkButton1 = new Krypton.Toolkit.KryptonCommandLinkButton();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(738, 56);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.SelectedObject = this.kryptonCommandLinkButton1;
            this.propertyGrid1.Size = new System.Drawing.Size(522, 806);
            this.propertyGrid1.TabIndex = 1;
            // 
            // kryptonCommandLinkButton1
            // 
            this.kryptonCommandLinkButton1.AutoSize = true;
            this.kryptonCommandLinkButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kryptonCommandLinkButton1.ButtonStyle = Krypton.Toolkit.ButtonStyle.ListItem;
            this.kryptonCommandLinkButton1.CommandLinkTextValues.Image = ((System.Drawing.Image)(resources.GetObject("kryptonCommandLinkButton1.CommandLinkTextValues.Image")));
            this.kryptonCommandLinkButton1.Location = new System.Drawing.Point(157, 208);
            this.kryptonCommandLinkButton1.Name = "kryptonCommandLinkButton1";
            this.kryptonCommandLinkButton1.OverrideFocus.Border.Draw = Krypton.Toolkit.InheritBool.True;
            this.kryptonCommandLinkButton1.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCommandLinkButton1.OverrideFocus.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kryptonCommandLinkButton1.Size = new System.Drawing.Size(248, 52);
            this.kryptonCommandLinkButton1.StateCommon.Content.LongText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCommandLinkButton1.StateCommon.Content.LongText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonCommandLinkButton1.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCommandLinkButton1.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCommandLinkButton1.TabIndex = 2;
            this.kryptonCommandLinkButton1.UACShieldIcon.UACShieldIconSize = Krypton.Toolkit.UACShieldIconSize.Medium;
            // 
            // WinformsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 890);
            this.Controls.Add(this.kryptonCommandLinkButton1);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "WinformsForm";
            this.Text = "WinformsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private PropertyGrid propertyGrid1;
        private KryptonCommandLinkButton kryptonCommandLinkButton1;
    }
}