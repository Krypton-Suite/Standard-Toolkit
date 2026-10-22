#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Windows.Forms.Design;

namespace TestForm;

/// <summary>
/// Reproduces issue #4547: Krypton collection editor dialogs must not appear on the taskbar.
/// </summary>
public sealed class Bug4547CollectionEditorTaskbarDemo : KryptonForm
{
    private readonly KryptonWrapLabel _lblInfo;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonButton _button;

    public Bug4547CollectionEditorTaskbarDemo()
    {
        Text = @"Bug #4547 - Collection editor taskbar";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(760, 360);
        MinimumSize = new Size(640, 300);

        _lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 120,
            Text =
                @"How to test issue #4547:" + Environment.NewLine +
                @"1) Click Open collection editor." + Environment.NewLine +
                @"2) The ButtonSpec collection editor must not get a taskbar button." + Environment.NewLine +
                @"3) The status line must report ShowInTaskbar = False. The taskbar must not gain a button for the editor." + Environment.NewLine +
                @"The same flag covers the other designer collection editors and the palette collection editor."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 48,
            Text = @"Status: not opened yet."
        };

        var buttonHost = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 44,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        _button = new KryptonButton
        {
            Text = @"Open collection editor",
            AutoSize = true,
            Location = new Point(12, 8)
        };
        _button.Click += (_, _) => OpenCollectionEditor();
        buttonHost.Controls.Add(_button);

        Controls.Add(_lblStatus);
        Controls.Add(buttonHost);
        Controls.Add(_lblInfo);
    }

    /// <summary>
    /// Opens the ButtonSpec collection editor and records whether it would appear on the taskbar.
    /// </summary>
    public void OpenCollectionEditor()
    {
        var editor = new KryptonDesignerButtonSpecAnyCollectionEditor();
        var service = new EditorService(this);
        editor.EditValue(null, service, null);

        _lblStatus.Text = service.StatusText;
    }

    private sealed class EditorService : IWindowsFormsEditorService, IServiceProvider
    {
        private readonly Bug4547CollectionEditorTaskbarDemo _owner;

        public EditorService(Bug4547CollectionEditorTaskbarDemo owner) => _owner = owner;

        public string StatusText { get; private set; } = @"Status: editor did not open.";

        public void CloseDropDown()
        {
        }

        public void DropDownControl(Control? control)
        {
        }

        public DialogResult ShowDialog(Form dialog)
        {
            var pass = !dialog.ShowInTaskbar;
            StatusText = (pass ? @"PASS: " : @"FAIL: ") +
                         $@"ShowInTaskbar = {dialog.ShowInTaskbar}. The editor must not appear on the taskbar.";
            return dialog.ShowDialog(_owner);
        }

        public object? GetService(Type serviceType) =>
            serviceType == typeof(IWindowsFormsEditorService) ? this : null;
    }
}
