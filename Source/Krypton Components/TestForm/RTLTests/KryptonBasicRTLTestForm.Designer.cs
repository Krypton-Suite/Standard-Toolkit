namespace TestForm;

partial class KryptonBasicRTLTestForm
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
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 13);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.kryptonPropertyGrid1.SelectedObject = this;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(519, 634);
            this.kryptonPropertyGrid1.TabIndex = 0;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(560, 124);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 1;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "WinForms Test";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // KryptonBasicRTLTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 652);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.kryptonPropertyGrid1);
            this.Name = "KryptonBasicRTLTestForm";
            this.Text = "KryptonBasicRTLTestForm";
            this.ResumeLayout(false);

    }

    #endregion

    private KryptonPropertyGrid kryptonPropertyGrid1;
    private KryptonButton kryptonButton1;
}