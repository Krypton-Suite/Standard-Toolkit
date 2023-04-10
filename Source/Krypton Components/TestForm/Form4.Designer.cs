namespace TestForm
{
    partial class Form4
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
            this.kryptonNavigator1 = new Krypton.Navigator.KryptonNavigator();
            this.kryptonCommand1 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonComboBox1 = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.ControlKryptonFormFeatures = false;
            this.kryptonNavigator1.Location = new System.Drawing.Point(634, 41);
            this.kryptonNavigator1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.kryptonNavigator1.Owner = null;
            this.kryptonNavigator1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonNavigator1.Size = new System.Drawing.Size(250, 150);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonCommand1
            // 
            this.kryptonCommand1.Text = "kryptonCommand1";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(65, 66);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonComboBox1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonCheckBox1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(278, 255);
            this.kryptonGroupBox1.TabIndex = 1;
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Location = new System.Drawing.Point(46, 23);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(152, 24);
            this.kryptonCheckBox1.TabIndex = 0;
            this.kryptonCheckBox1.Values.Text = "kryptonCheckBox1";
            // 
            // kryptonComboBox1
            // 
            this.kryptonComboBox1.DropDownWidth = 121;
            this.kryptonComboBox1.IntegralHeight = false;
            this.kryptonComboBox1.Location = new System.Drawing.Point(28, 99);
            this.kryptonComboBox1.Name = "kryptonComboBox1";
            this.kryptonComboBox1.Size = new System.Drawing.Size(121, 25);
            this.kryptonComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonComboBox1.TabIndex = 1;
            this.kryptonComboBox1.Text = "kryptonComboBox1";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.kryptonNavigator1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private Krypton.Navigator.KryptonPage kryptonPage2;
        private Krypton.Toolkit.KryptonCommand kryptonCommand1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonComboBox kryptonComboBox1;
        private Krypton.Toolkit.KryptonCheckBox kryptonCheckBox1;
    }
}