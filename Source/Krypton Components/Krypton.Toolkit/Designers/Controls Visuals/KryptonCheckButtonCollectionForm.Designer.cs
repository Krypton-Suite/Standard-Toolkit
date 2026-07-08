#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonCheckButtonCollectionForm
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
        checkedListBox = new KryptonCheckedListBox();
        buttonOK = new KryptonButton();
        buttonCancel = new KryptonButton();
        label = new KryptonLabel();
        contentPanel = new KryptonPanel();
        SuspendLayout();
        contentPanel.SuspendLayout();
        // 
        // contentPanel
        // 
        contentPanel.Controls.Add(checkedListBox);
        contentPanel.Controls.Add(label);
        contentPanel.Dock = DockStyle.Fill;
        contentPanel.Location = new Point(0, 0);
        contentPanel.Name = "contentPanel";
        contentPanel.Padding = new Padding(12, 9, 12, 9);
        contentPanel.PanelBackStyle = PaletteBackStyle.PanelClient;
        contentPanel.Size = new Size(305, 267);
        contentPanel.TabIndex = 0;
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
        // buttonOK
        // 
        buttonOK.DialogResult = DialogResult.OK;
        buttonOK.Name = "buttonOK";
        buttonOK.TabIndex = 2;
        buttonOK.Values.Text = "OK";
        buttonOK.Click += buttonOK_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.DialogResult = DialogResult.Cancel;
        buttonCancel.Name = "buttonCancel";
        buttonCancel.TabIndex = 3;
        buttonCancel.Values.Text = "Cancel";
        // 
        // KryptonCheckButtonCollectionForm
        // 
        AcceptButton = buttonOK;
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = buttonCancel;
        ClientSize = new Size(305, 317);
        ControlBox = false;
        Controls.Add(contentPanel);
        Controls.Add(KryptonDesignerEditorButtonBar.Create(this, buttonOK, buttonCancel));
        Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(250, 255);
        Name = "KryptonCheckButtonCollectionForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "CheckButtons Collection Editor";
        Load += KryptonCheckButtonCollectionForm_Load;
        contentPanel.ResumeLayout(false);
        contentPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private KryptonCheckedListBox checkedListBox;
    private KryptonButton buttonOK;
    private KryptonButton buttonCancel;
    private KryptonLabel label;
    private KryptonPanel contentPanel;
}
