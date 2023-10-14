namespace Krypton.Toolkit
{
    partial class KryptonToolkitPoweredByControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.klwlblDetails = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.kwlblVersionInformation = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlblCurrentTheme = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktcmbCurrentTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ktcmbCurrentTheme)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.tableLayoutPanel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(659, 249);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Powered by Krypton Toolkit";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbxLogo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.klwlblDetails, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblVersionInformation, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.kwlblCurrentTheme, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ktcmbCurrentTheme, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(655, 225);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Location = new System.Drawing.Point(8, 4);
            this.pbxLogo.Margin = new System.Windows.Forms.Padding(8, 4, 4, 4);
            this.pbxLogo.Name = "pbxLogo";
            this.tableLayoutPanel1.SetRowSpan(this.pbxLogo, 4);
            this.pbxLogo.Size = new System.Drawing.Size(64, 64);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxLogo.TabIndex = 0;
            this.pbxLogo.TabStop = false;
            // 
            // klwlblDetails
            // 
            this.klwlblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblDetails.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblDetails.LinkArea = new System.Windows.Forms.LinkArea(134, 144);
            this.klwlblDetails.Location = new System.Drawing.Point(79, 0);
            this.klwlblDetails.Name = "klwlblDetails";
            this.klwlblDetails.Size = new System.Drawing.Size(573, 168);
            this.klwlblDetails.Text = "Some of the components used in this application are part of the Krypton Standard " +
    "Toolkit. \r\n\r\nLicense: BSD-3-Clause\r\n\r\nTo learn more, click here.";
            this.klwlblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.klwlblDetails.UseCompatibleTextRendering = true;
            // 
            // kwlblVersionInformation
            // 
            this.kwlblVersionInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblVersionInformation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblVersionInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblVersionInformation.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblVersionInformation.Location = new System.Drawing.Point(79, 168);
            this.kwlblVersionInformation.Name = "kwlblVersionInformation";
            this.kwlblVersionInformation.Size = new System.Drawing.Size(573, 15);
            this.kwlblVersionInformation.Text = "kryptonWrapLabel1";
            this.kwlblVersionInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kwlblCurrentTheme
            // 
            this.kwlblCurrentTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblCurrentTheme.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.kwlblCurrentTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblCurrentTheme.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblCurrentTheme.Location = new System.Drawing.Point(79, 183);
            this.kwlblCurrentTheme.Name = "kwlblCurrentTheme";
            this.kwlblCurrentTheme.Size = new System.Drawing.Size(573, 15);
            this.kwlblCurrentTheme.Text = "Current Theme:";
            // 
            // ktcmbCurrentTheme
            // 
            this.ktcmbCurrentTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktcmbCurrentTheme.DropDownWidth = 573;
            this.ktcmbCurrentTheme.IntegralHeight = false;
            this.ktcmbCurrentTheme.Location = new System.Drawing.Point(79, 201);
            this.ktcmbCurrentTheme.Name = "ktcmbCurrentTheme";
            this.ktcmbCurrentTheme.Size = new System.Drawing.Size(573, 21);
            this.ktcmbCurrentTheme.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ktcmbCurrentTheme.TabIndex = 4;
            // 
            // KryptonToolkitPoweredByControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.kryptonGroupBox1);
            this.Name = "KryptonToolkitPoweredByControl";
            this.Size = new System.Drawing.Size(659, 249);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ktcmbCurrentTheme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonGroupBox kryptonGroupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pbxLogo;
        private KryptonLinkWrapLabel klwlblDetails;
        private KryptonWrapLabel kwlblVersionInformation;
        private KryptonWrapLabel kwlblCurrentTheme;
        private KryptonThemeComboBox ktcmbCurrentTheme;
    }
}
