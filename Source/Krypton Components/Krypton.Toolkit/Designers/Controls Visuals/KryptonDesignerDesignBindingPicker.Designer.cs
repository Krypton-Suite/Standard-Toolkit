#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonDesignerDesignBindingPicker
{
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _helpLabel = new KryptonLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 36,
            Values = { Text = @"Select a binding." }
        };

        _treeView = new KryptonTreeView
        {
            Dock = DockStyle.Fill,
            HideSelection = false,
            FullRowSelect = true
        };
        _treeView.AfterSelect += (_, e) => UpdateHelp(e.Node);

        _layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layout.RowStyles.Add(new RowStyle());
        _layout.Controls.Add(_treeView, 0, 0);
        _layout.Controls.Add(_helpLabel, 0, 1);

        BorderStyle = BorderStyle.FixedSingle;
        MinimumSize = new Size(250, 250);
        Size = new Size(320, 320);
        Controls.Add(_layout);
        Name = nameof(KryptonDesignerDesignBindingPicker);
    }

    #endregion

    private KryptonTreeView _treeView;
    private KryptonLabel _helpLabel;
    private TableLayoutPanel _layout;
}
