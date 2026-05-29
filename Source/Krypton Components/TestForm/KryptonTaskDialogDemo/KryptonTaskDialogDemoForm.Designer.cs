namespace TestForm
{
    partial class KryptonTaskDialogDemoForm
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
            this.propGridKtd = new System.Windows.Forms.PropertyGrid();
            this.btnShowDialog = new Krypton.Toolkit.KryptonButton();
            this.btnShowDialogOwner = new Krypton.Toolkit.KryptonButton();
            this.btnShow = new Krypton.Toolkit.KryptonButton();
            this.btnShowOwner = new Krypton.Toolkit.KryptonButton();
            this.btnModelessProgressDialog1 = new Krypton.Toolkit.KryptonButton();
            this.btnMessageBox1 = new Krypton.Toolkit.KryptonButton();
            this.btnMessageBox2 = new Krypton.Toolkit.KryptonButton();
            this.btnFreeWheeler2DataGridView = new Krypton.Toolkit.KryptonButton();
            this.btnReset = new Krypton.Toolkit.KryptonButton();
            this.btnFreeWheeler1CheckSet = new Krypton.Toolkit.KryptonButton();
            this.btnContent1 = new Krypton.Toolkit.KryptonButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // propGridKtd
            // 
            this.propGridKtd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propGridKtd.Location = new System.Drawing.Point(12, 12);
            this.propGridKtd.Name = "propGridKtd";
            this.propGridKtd.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propGridKtd.Size = new System.Drawing.Size(325, 660);
            this.propGridKtd.TabIndex = 0;
            // 
            // btnShowDialog
            // 
            this.btnShowDialog.Location = new System.Drawing.Point(355, 10);
            this.btnShowDialog.Name = "btnShowDialog";
            this.btnShowDialog.Size = new System.Drawing.Size(148, 35);
            this.btnShowDialog.TabIndex = 1;
            this.btnShowDialog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnShowDialog.Values.Text = "ShowDialog()";
            this.btnShowDialog.Click += new System.EventHandler(this.btnShowDialog_Click);
            // 
            // btnShowDialogOwner
            // 
            this.btnShowDialogOwner.Location = new System.Drawing.Point(355, 51);
            this.btnShowDialogOwner.Name = "btnShowDialogOwner";
            this.btnShowDialogOwner.Size = new System.Drawing.Size(148, 35);
            this.btnShowDialogOwner.TabIndex = 1;
            this.btnShowDialogOwner.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnShowDialogOwner.Values.Text = "ShowDialog( owner )";
            this.btnShowDialogOwner.Click += new System.EventHandler(this.btnShowDialogOwner_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(355, 92);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(148, 35);
            this.btnShow.TabIndex = 1;
            this.btnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnShow.Values.Text = "Show()";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnShowOwner
            // 
            this.btnShowOwner.Location = new System.Drawing.Point(355, 133);
            this.btnShowOwner.Name = "btnShowOwner";
            this.btnShowOwner.Size = new System.Drawing.Size(148, 35);
            this.btnShowOwner.TabIndex = 1;
            this.btnShowOwner.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnShowOwner.Values.Text = "Show( owner )";
            this.btnShowOwner.Click += new System.EventHandler(this.btnShowOwner_Click);
            // 
            // btnModelessProgressDialog1
            // 
            this.btnModelessProgressDialog1.Location = new System.Drawing.Point(523, 92);
            this.btnModelessProgressDialog1.Name = "btnModelessProgressDialog1";
            this.btnModelessProgressDialog1.Size = new System.Drawing.Size(225, 35);
            this.btnModelessProgressDialog1.TabIndex = 2;
            this.btnModelessProgressDialog1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModelessProgressDialog1.Values.Text = "Modeless Progress Dialog";
            this.btnModelessProgressDialog1.Click += new System.EventHandler(this.btnModelessProgressDialog1_Click);
            // 
            // btnMessageBox1
            // 
            this.btnMessageBox1.Location = new System.Drawing.Point(523, 10);
            this.btnMessageBox1.Name = "btnMessageBox1";
            this.btnMessageBox1.Size = new System.Drawing.Size(225, 35);
            this.btnMessageBox1.TabIndex = 4;
            this.btnMessageBox1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnMessageBox1.Values.Text = "MessageBox 1";
            this.btnMessageBox1.Click += new System.EventHandler(this.btnMessageBox1_Click);
            // 
            // btnMessageBox2
            // 
            this.btnMessageBox2.Location = new System.Drawing.Point(523, 51);
            this.btnMessageBox2.Name = "btnMessageBox2";
            this.btnMessageBox2.Size = new System.Drawing.Size(225, 35);
            this.btnMessageBox2.TabIndex = 4;
            this.btnMessageBox2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnMessageBox2.Values.Text = "MessageBox 2";
            this.btnMessageBox2.Click += new System.EventHandler(this.btnMessageBox2_Click);
            // 
            // btnFreeWheeler2DataGridView
            // 
            this.btnFreeWheeler2DataGridView.Location = new System.Drawing.Point(522, 133);
            this.btnFreeWheeler2DataGridView.Name = "btnFreeWheeler2DataGridView";
            this.btnFreeWheeler2DataGridView.Size = new System.Drawing.Size(225, 35);
            this.btnFreeWheeler2DataGridView.TabIndex = 4;
            this.btnFreeWheeler2DataGridView.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnFreeWheeler2DataGridView.Values.Text = "FreeWheeler 2 DataGridView";
            this.btnFreeWheeler2DataGridView.Click += new System.EventHandler(this.FreeWheeler2DataGridView_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(355, 174);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(148, 35);
            this.btnReset.TabIndex = 1;
            this.btnReset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnReset.Values.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnFreeWheeler1CheckSet
            // 
            this.btnFreeWheeler1CheckSet.Location = new System.Drawing.Point(523, 174);
            this.btnFreeWheeler1CheckSet.Name = "btnFreeWheeler1CheckSet";
            this.btnFreeWheeler1CheckSet.Size = new System.Drawing.Size(225, 35);
            this.btnFreeWheeler1CheckSet.TabIndex = 2;
            this.btnFreeWheeler1CheckSet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnFreeWheeler1CheckSet.Values.Text = "FreeWheeler 3 CheckSet";
            this.btnFreeWheeler1CheckSet.Click += new System.EventHandler(this.btnFreeWheeler1CheckSet_Click);
            // 
            // btnContent1
            // 
            this.btnContent1.Location = new System.Drawing.Point(523, 215);
            this.btnContent1.Name = "btnContent1";
            this.btnContent1.Size = new System.Drawing.Size(225, 35);
            this.btnContent1.TabIndex = 2;
            this.btnContent1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnContent1.Values.Text = "Content with an image";
            this.btnContent1.Click += new System.EventHandler(this.btnContent1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(453, 306);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(224, 170);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // KryptonTaskDialogDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 690);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnFreeWheeler2DataGridView);
            this.Controls.Add(this.btnMessageBox2);
            this.Controls.Add(this.btnMessageBox1);
            this.Controls.Add(this.btnContent1);
            this.Controls.Add(this.btnFreeWheeler1CheckSet);
            this.Controls.Add(this.btnModelessProgressDialog1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnShowOwner);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnShowDialogOwner);
            this.Controls.Add(this.btnShowDialog);
            this.Controls.Add(this.propGridKtd);
            this.Name = "KryptonTaskDialogDemoForm";
            this.Text = "KryptonTaskDialogDemoForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyGrid propGridKtd;
        private KryptonButton btnShowDialog;
        private KryptonButton btnShowDialogOwner;
        private KryptonButton btnShow;
        private KryptonButton btnShowOwner;
        private KryptonButton btnModelessProgressDialog1;
        private KryptonButton btnMessageBox1;
        private KryptonButton btnMessageBox2;
        private KryptonButton btnFreeWheeler2DataGridView;
        private KryptonButton btnReset;
        private KryptonButton btnFreeWheeler1CheckSet;
        private KryptonButton btnContent1;
        private PictureBox pictureBox1;
    }
}