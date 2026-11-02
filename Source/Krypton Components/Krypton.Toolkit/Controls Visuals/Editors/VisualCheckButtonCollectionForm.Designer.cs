#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualCheckButtonCollectionForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null!;

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
        components = new Container();
        checkedListBox = new KryptonCheckedListBox();
        label = new KryptonLabel();
        kpnlContent = new KryptonPanel();
        kpnlButtonBar = new InternalDesignerEditorButtonBarPanel();
        ((ISupportInitialize)kpnlContent).BeginInit();
        kpnlContent.SuspendLayout();
        SuspendLayout();
        // 
        // kpnlContent
        // 
        kpnlContent.Controls.Add(checkedListBox);
        kpnlContent.Controls.Add(label);
        kpnlContent.Dock = DockStyle.Fill;
        kpnlContent.Location = new Point(0, 0);
        kpnlContent.Name = "kpnlContent";
        kpnlContent.Padding = new Padding(12, 9, 12, 9);
        kpnlContent.PanelBackStyle = PaletteBackStyle.PanelClient;
        kpnlContent.Size = new Size(305, 267);
        kpnlContent.TabIndex = 0;
        // 
        // checkedListBox
        // 
        checkedListBox.Dock = DockStyle.Fill;
        checkedListBox.CheckOnClick = true;
        checkedListBox.Location = new Point(12, 29);
        checkedListBox.Name = "checkedListBox";
        checkedListBox.Size = new Size(281, 229);
        checkedListBox.TabIndex = 1;
        // 
        // label
        // 
        label.AutoSize = true;
        label.Dock = DockStyle.Top;
        label.Location = new Point(12, 9);
        label.Name = "label";
        label.Size = new Size(214, 20);
        label.TabIndex = 0;
        label.Values.Text = "Select the check buttons to group together";
        // 
        // kpnlButtonBar
        // 
        kpnlButtonBar.Dock = DockStyle.Bottom;
        kpnlButtonBar.Name = "kpnlButtonBar";
        // 
        // KryptonCheckButtonCollectionForm
        // 
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(305, 317);
        ControlBox = false;
        Controls.Add(kpnlContent);
        Controls.Add(kpnlButtonBar);
        Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(250, 255);
        Name = "KryptonCheckButtonCollectionForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "CheckButtons Collection Editor";
        Load += KryptonCheckButtonCollectionForm_Load;
        ((ISupportInitialize)kpnlContent).EndInit();
        kpnlContent.ResumeLayout(false);
        kpnlContent.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private KryptonCheckedListBox checkedListBox;
    private KryptonLabel label;
    private KryptonPanel kpnlContent;
    private InternalDesignerEditorButtonBarPanel kpnlButtonBar;
}
