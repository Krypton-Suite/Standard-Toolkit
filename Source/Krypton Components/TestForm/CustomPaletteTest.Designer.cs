namespace TestForm
{
    partial class CustomPaletteTest
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
            this.components = new System.ComponentModel.Container();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleOrange;
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonCustomPaletteBase1.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kryptonCustomPaletteBase1.ThemeName = null;
            this.kryptonCustomPaletteBase1.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 121;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(239, 217);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.ReportSelectedThemeIndex = false;
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(121, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            // 
            // CustomPaletteTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 466);
            this.Controls.Add(this.kryptonThemeComboBox1);
            this.Name = "CustomPaletteTest";
            this.Text = "CustomPaletteTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
    }
}