#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonDesignerSelectResourceForm
{
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _radioLocal = CreateRadio(@"&Local resource", ResourceSource.Local);
        _radioProject = CreateRadio(@"&Project resource file", ResourceSource.Project);
        _radioImport = CreateRadio(@"&Import", ResourceSource.Import);

        _resourceList = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _resourceList.SelectedIndexChanged += (_, _) => OnResourceSelected();

        _preview = new PictureBox
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = SystemColors.Window
        };

        _previewLabel = new KryptonLabel
        {
            AutoSize = true,
            Values = { Text = @"Preview:" }
        };

        _buttonImport = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Values = { Text = @"Import..." }
        };
        _buttonImport.Click += (_, _) => ImportImage();

        _buttonClear = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Values = { Text = @"Clear" }
        };
        _buttonClear.Click += (_, _) => ClearImage();

        _buttonOk = new KryptonButton
        {
            DialogResult = DialogResult.OK,
            Values = { Text = KryptonManager.Strings.GeneralStrings.OK }
        };

        _buttonCancel = new KryptonButton
        {
            DialogResult = DialogResult.Cancel,
            Values = { Text = KryptonManager.Strings.GeneralStrings.Cancel }
        };

        var sourceGroup = new KryptonGroupBox
        {
            Dock = DockStyle.Top,
            Height = KryptonDesignerEditorDpi.Scale(this, 110),
            Values = { Heading = @"Select resource source" }
        };

        var sourceLayout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(3),
            BackColor = Color.Transparent
        };
        sourceLayout.Controls.Add(_radioLocal);
        sourceLayout.Controls.Add(_radioProject);
        sourceLayout.Controls.Add(_radioImport);
        sourceGroup.Panel.Controls.Add(sourceLayout);

        var listGroup = new KryptonGroupBox
        {
            Dock = DockStyle.Fill,
            Values = { Heading = @"Resource" }
        };
        listGroup.Panel.Controls.Add(_resourceList);

        var previewPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        previewPanel.RowStyles.Add(new RowStyle());
        previewPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        previewPanel.Controls.Add(_previewLabel, 0, 0);
        previewPanel.Controls.Add(_preview, 0, 1);

        var actionButtons = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0, 6, 0, 0),
            BackColor = Color.Transparent
        };
        actionButtons.Controls.Add(_buttonImport);
        actionButtons.Controls.Add(_buttonClear);

        var body = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        body.RowStyles.Add(new RowStyle());
        body.Controls.Add(listGroup, 0, 0);
        body.Controls.Add(previewPanel, 1, 0);
        body.Controls.Add(actionButtons, 0, 1);
        body.SetColumnSpan(actionButtons, 2);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        content.RowStyles.Add(new RowStyle());
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        content.Controls.Add(sourceGroup, 0, 0);
        content.Controls.Add(body, 0, 1);

        Controls.Add(KryptonDesignerEditorContentPanel.Create(this, content));
        Controls.Add(KryptonDesignerEditorButtonBar.Create(this, _buttonOk, _buttonCancel));
        Name = nameof(KryptonDesignerSelectResourceForm);
    }

    #endregion

    private KryptonRadioButton _radioLocal;
    private KryptonRadioButton _radioProject;
    private KryptonRadioButton _radioImport;
    private KryptonListBox _resourceList;
    private PictureBox _preview;
    private KryptonLabel _previewLabel;
    private KryptonButton _buttonImport;
    private KryptonButton _buttonClear;
    private KryptonButton _buttonOk;
    private KryptonButton _buttonCancel;
}
