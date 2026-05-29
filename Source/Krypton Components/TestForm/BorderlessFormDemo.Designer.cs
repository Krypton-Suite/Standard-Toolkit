namespace TestForm
{
    partial class BorderlessFormDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BorderlessFormDemo));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.klblDescription = new Krypton.Toolkit.KryptonLabel();
            this.kbtnOpenAnother = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblDescription);
            this.kryptonPanel1.Controls.Add(this.kbtnOpenAnother);
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.kryptonPanel1.Size = new System.Drawing.Size(884, 200);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // klblDescription
            // 
            this.klblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblDescription.Location = new System.Drawing.Point(23, 23);
            this.klblDescription.Name = "klblDescription";
            this.klblDescription.Size = new System.Drawing.Size(809, 80);
            this.klblDescription.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblDescription.TabIndex = 0;
            this.klblDescription.Values.Text = resources.GetString("klblDescription.Values.Text");
            // 
            // kbtnOpenAnother
            // 
            this.kbtnOpenAnother.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOpenAnother.Location = new System.Drawing.Point(667, 155);
            this.kbtnOpenAnother.Name = "kbtnOpenAnother";
            this.kbtnOpenAnother.Size = new System.Drawing.Size(100, 25);
            this.kbtnOpenAnother.TabIndex = 1;
            this.kbtnOpenAnother.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOpenAnother.Values.Text = "Open Another";
            this.kbtnOpenAnother.Click += new System.EventHandler(this.kbtnOpenAnother_Click);
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnClose.Location = new System.Drawing.Point(773, 155);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(90, 25);
            this.kbtnClose.TabIndex = 2;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // BorderlessFormDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnClose;
            this.ClientSize = new System.Drawing.Size(884, 200);
            this.CloseBox = false;
            this.ControlBox = false;
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BorderlessFormDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Borderless Form Demo (Issue #2922)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

            }

            #endregion

            private Krypton.Toolkit.KryptonPanel kryptonPanel1;
            private Krypton.Toolkit.KryptonLabel klblDescription;
            private Krypton.Toolkit.KryptonButton kbtnOpenAnother;
            private Krypton.Toolkit.KryptonButton kbtnClose;
        }
}