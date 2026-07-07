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
        SuspendLayout();
        // 
        // checkedListBox
        // 
        checkedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        checkedListBox.CheckOnClick = true;
        checkedListBox.Location = new Point(12, 30);
        checkedListBox.Name = "checkedListBox";
        checkedListBox.Size = new Size(281, 246);
        checkedListBox.TabIndex = 1;
        // 
        // buttonOK
        // 
        buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonOK.DialogResult = DialogResult.OK;
        buttonOK.Location = new Point(136, 282);
        buttonOK.Name = "buttonOK";
        buttonOK.Size = new Size(90, 25);
        buttonOK.TabIndex = 2;
        buttonOK.Values.Text = "OK";
        buttonOK.Click += buttonOK_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.DialogResult = DialogResult.Cancel;
        buttonCancel.Location = new Point(218, 282);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new Size(90, 25);
        buttonCancel.TabIndex = 3;
        buttonCancel.Values.Text = "Cancel";
        // 
        // label
        // 
        label.AutoSize = true;
        label.Location = new Point(9, 9);
        label.Name = "label";
        label.Size = new Size(214, 20);
        label.TabIndex = 0;
        label.Values.Text = "Select the check buttons to group together";
        // 
        // KryptonCheckButtonCollectionForm
        // 
        AcceptButton = buttonOK;
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = buttonCancel;
        ClientSize = new Size(305, 317);
        ControlBox = false;
        Controls.Add(label);
        Controls.Add(buttonCancel);
        Controls.Add(buttonOK);
        Controls.Add(checkedListBox);
        Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(250, 205);
        Name = "KryptonCheckButtonCollectionForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "CheckButtons Collection Editor";
        Load += KryptonCheckButtonCollectionForm_Load;
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

    private KryptonCheckedListBox checkedListBox;
    private KryptonButton buttonOK;
    private KryptonButton buttonCancel;
    private KryptonLabel label;
}
