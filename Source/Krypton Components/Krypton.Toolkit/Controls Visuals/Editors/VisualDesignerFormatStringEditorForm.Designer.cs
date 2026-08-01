#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualDesignerFormatStringEditorForm
{
    #region Windows Form Designer generated code

    private IContainer components = null!;

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();
        var formatTypeLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Format type:" } };
        _formatTypeCombo = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Top };
        _formatTypeCombo.Items.AddRange(
        [
            @"No formatting",
            @"Numeric",
            @"Decimal",
            @"Currency",
            @"Date",
            @"Time",
            @"Percent",
            @"Custom"
        ]);

        var formatStringLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Format string:" } };
        _formatStringTextBox = new KryptonTextBox { Dock = DockStyle.Top };
        _formatStringTextBox.TextChanged += (_, _) => UpdatePreview();

        var nullValueLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Null value:" } };
        _nullValueTextBox = new KryptonTextBox { Dock = DockStyle.Top };

        _previewLabel = new KryptonLabel
        {
            AutoSize = false,
            Height = 40,
            Values = { Text = @"Preview:" }
        };

        _layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 7,
            BackColor = Color.Transparent
        };
        _layout.RowStyles.Add(new RowStyle());
        _layout.RowStyles.Add(new RowStyle());
        _layout.RowStyles.Add(new RowStyle());
        _layout.RowStyles.Add(new RowStyle());
        _layout.RowStyles.Add(new RowStyle());
        _layout.RowStyles.Add(new RowStyle());
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        _layout.Controls.Add(formatTypeLabel, 0, 0);
        _layout.Controls.Add(_formatTypeCombo, 0, 1);
        _layout.Controls.Add(formatStringLabel, 0, 2);
        _layout.Controls.Add(_formatStringTextBox, 0, 3);
        _layout.Controls.Add(nullValueLabel, 0, 4);
        _layout.Controls.Add(_nullValueTextBox, 0, 5);
        _layout.Controls.Add(_previewLabel, 0, 6);

        kpnlContent = new KryptonPanel();
        kpnlButtonBar = new InternalDesignerEditorButtonBarPanel();
        ((ISupportInitialize)kpnlContent).BeginInit();
        kpnlContent.SuspendLayout();
        SuspendLayout();

        kpnlContent.Controls.Add(_layout);
        kpnlContent.Dock = DockStyle.Fill;
        kpnlContent.Name = "kpnlContent";
        kpnlContent.Padding = new Padding(9);
        kpnlContent.PanelBackStyle = PaletteBackStyle.PanelClient;

        kpnlButtonBar.Dock = DockStyle.Bottom;
        kpnlButtonBar.Name = "kpnlButtonBar";

        Controls.Add(kpnlContent);
        Controls.Add(kpnlButtonBar);
        Name = nameof(VisualDesignerFormatStringEditorForm);

        ((ISupportInitialize)kpnlContent).EndInit();
        kpnlContent.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private KryptonComboBox _formatTypeCombo;
    private KryptonTextBox _formatStringTextBox;
    private KryptonTextBox _nullValueTextBox;
    private KryptonLabel _previewLabel;
    private TableLayoutPanel _layout;
    private KryptonPanel kpnlContent;
    private InternalDesignerEditorButtonBarPanel kpnlButtonBar;
}
