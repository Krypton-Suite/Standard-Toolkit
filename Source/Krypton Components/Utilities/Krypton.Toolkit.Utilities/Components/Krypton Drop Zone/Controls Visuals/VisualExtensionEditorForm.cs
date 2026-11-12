#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class VisualExtensionEditorForm : KryptonForm
{
    private static readonly KeyValuePair<string, string>[] Presets =
    {
            new KeyValuePair<string, string>("Preset_Images", ".png .jpg .jpeg .gif .bmp .tif .tiff .webp"),
            new KeyValuePair<string, string>("Preset_Documents", ".pdf .doc .docx .odt .rtf .txt"),
            new KeyValuePair<string, string>("Preset_Office", ".xls .xlsx .csv .ppt .pptx"),
            new KeyValuePair<string, string>("Preset_Archives", ".zip .7z .rar .tar .gz"),
            new KeyValuePair<string, string>("Preset_Media", ".mp3 .wav .flac .mp4 .mov .avi .mkv"),
        };

    private readonly KryptonTextBox _text = new KryptonTextBox
    {
        Multiline = true,
        Dock = DockStyle.Fill,
        ScrollBars = ScrollBars.Vertical,
        AcceptsReturn = true,
        Font = new Font(FontFamily.GenericMonospace, 9f)
    };

    public string Result => string.Join(", ", ExtensionParser.Parse(_text.Text));

    public VisualExtensionEditorForm(string initial)
    {
        Text = DropZoneStrings.Get("Dlg_Extensions");
        Size = new Size(440, 380);
        StartPosition = FormStartPosition.CenterParent;
        MinimizeBox = MaximizeBox = ShowInTaskbar = false;
        _text.Text = string.Join(Environment.NewLine, ExtensionParser.Parse(initial));

        var presets = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(4) };
        foreach (KeyValuePair<string, string> preset in Presets)
        {
            string exts = preset.Value;
            var b = new Button { Text = DropZoneStrings.Get(preset.Key), AutoSize = true };
            b.Click += (s, e) => _text.Text = string.Join(Environment.NewLine, ExtensionParser.Parse(_text.Text + " " + exts));
            presets.Controls.Add(b);
        }
        var clear = new Button { Text = DropZoneStrings.Get("Preset_Clear"), AutoSize = true };
        clear.Click += (s, e) => _text.Clear();
        presets.Controls.Add(clear);

        var hint = new Label { Text = DropZoneStrings.Get("Dlg_ExtensionsHint"), Dock = DockStyle.Top, Height = 22, Padding = new Padding(4, 4, 0, 0) };
        var ok = new Button { Text = "OK", DialogResult = DialogResult.OK };
        var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };
        var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(6) };
        bottom.Controls.Add(cancel);
        bottom.Controls.Add(ok);
        AcceptButton = ok;
        CancelButton = cancel;

        Controls.Add(_text);      // fill first, then edge-docked controls
        Controls.Add(presets);
        Controls.Add(hint);
        Controls.Add(bottom);
    }
}