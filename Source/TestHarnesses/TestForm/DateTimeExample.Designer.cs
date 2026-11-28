namespace TestForm
{
    partial class DateTimeExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeExample));
            this.kryptonDateTimePicker1 = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonColorButton1 = new Krypton.Toolkit.KryptonColorButton();
            this.SuspendLayout();
            // 
            // kryptonDateTimePicker1
            // 
            this.kryptonDateTimePicker1.Location = new System.Drawing.Point(13, 13);
            this.kryptonDateTimePicker1.Name = "kryptonDateTimePicker1";
            this.kryptonDateTimePicker1.Size = new System.Drawing.Size(234, 21);
            this.kryptonDateTimePicker1.TabIndex = 0;
            // 
            // kryptonColorButton1
            // 
            this.kryptonColorButton1.Location = new System.Drawing.Point(13, 41);
            this.kryptonColorButton1.Name = "kryptonColorButton1";
            this.kryptonColorButton1.Size = new System.Drawing.Size(234, 25);
            this.kryptonColorButton1.TabIndex = 1;
            this.kryptonColorButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonColorButton1.Values.Image")));
            this.kryptonColorButton1.Values.Text = "kryptonColorButton1";
            this.kryptonColorButton1.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kryptonColorButton1_SelectedColorChanged);
            // 
            // DateTimeExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonColorButton1);
            this.Controls.Add(this.kryptonDateTimePicker1);
            this.Name = "DateTimeExample";
            this.Text = "DateTimeExample";
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonDateTimePicker kryptonDateTimePicker1;
        private KryptonColorButton kryptonColorButton1;
    }
}