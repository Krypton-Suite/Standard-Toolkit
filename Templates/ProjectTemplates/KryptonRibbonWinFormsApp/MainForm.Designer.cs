namespace $safeprojectname$;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer? components = null;
    private Krypton.Ribbon.KryptonRibbon kryptonRibbon;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        kryptonRibbon = new Krypton.Ribbon.KryptonRibbon();
        SuspendLayout();
        //
        // kryptonRibbon
        //
        kryptonRibbon.InDesignHelperMode = true;
        kryptonRibbon.Location = new System.Drawing.Point(0, 0);
        kryptonRibbon.MinimizedMode = true;
        kryptonRibbon.Name = "kryptonRibbon";
        kryptonRibbon.RibbonAppButton.AppButtonVisible = false;
        kryptonRibbon.Size = new System.Drawing.Size(1000, 115);
        kryptonRibbon.TabIndex = 0;
        //
        // MainForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1000, 700);
        Controls.Add(kryptonRibbon);
        Name = "MainForm";
        Text = "$safeprojectname$";
        ResumeLayout(false);
        PerformLayout();
    }
}
