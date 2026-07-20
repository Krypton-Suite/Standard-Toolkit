#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class InternalDesignerEditorButtonBarPanel
{
    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        kbEdge = new KryptonBorderEdge();
        kcmbTheme = new KryptonThemeComboBox();
        flpExtraButtons = new FlowLayoutPanel();
        kbtnOk = new KryptonButton();
        kbtnCancel = new KryptonButton();
        ((ISupportInitialize)kcmbTheme).BeginInit();
        SuspendLayout();
        //
        // kbEdge
        //
        kbEdge.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        kbEdge.Dock = DockStyle.Top;
        kbEdge.Name = "kbEdge";
        //
        // kcmbTheme
        //
        kcmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
        kcmbTheme.Name = "kcmbDesignerEditorTheme";
        kcmbTheme.TabIndex = 0;
        //
        // flpExtraButtons
        //
        flpExtraButtons.AutoSize = true;
        flpExtraButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flpExtraButtons.BackColor = Color.Transparent;
        flpExtraButtons.FlowDirection = FlowDirection.RightToLeft;
        flpExtraButtons.Name = "flpExtraButtons";
        flpExtraButtons.WrapContents = false;
        //
        // kbtnOk
        //
        kbtnOk.DialogResult = DialogResult.OK;
        kbtnOk.Name = "kbtnOk";
        kbtnOk.Size = new Size(112, 28);
        kbtnOk.TabIndex = 1;
        kbtnOk.Values.Text = "OK";
        //
        // kbtnCancel
        //
        kbtnCancel.DialogResult = DialogResult.Cancel;
        kbtnCancel.Name = "kbtnCancel";
        kbtnCancel.Size = new Size(112, 28);
        kbtnCancel.TabIndex = 2;
        kbtnCancel.Values.Text = "Cancel";
        //
        // KryptonDesignerEditorButtonBarPanel
        //
        Controls.Add(kbtnCancel);
        Controls.Add(kbtnOk);
        Controls.Add(flpExtraButtons);
        Controls.Add(kcmbTheme);
        Controls.Add(kbEdge);
        Name = "KryptonDesignerEditorButtonBarPanel";
        Size = new Size(584, 52);
        ((ISupportInitialize)kcmbTheme).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private KryptonBorderEdge kbEdge;
    private KryptonThemeComboBox kcmbTheme;
    private FlowLayoutPanel flpExtraButtons;
    private KryptonButton kbtnOk;
    private KryptonButton kbtnCancel;
}
