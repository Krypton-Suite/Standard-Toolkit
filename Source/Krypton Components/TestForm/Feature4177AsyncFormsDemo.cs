#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Threading.Tasks;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for Issue #4177: WinForms async form / dialog APIs wrapped by Krypton (.NET 9+).
/// </summary>
public sealed class Feature4177AsyncFormsDemo : KryptonForm
{
    private readonly KryptonWrapLabel _status;

    public Feature4177AsyncFormsDemo()
    {
        Text = @"4177 — Async Form / Dialog Methods";
        Size = new Size(780, 640);
        StartPosition = FormStartPosition.CenterScreen;

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 108,
            Padding = new Padding(12),
            Text =
                "Issue #4177: await Krypton dialog helpers that wrap Form.ShowAsync / ShowDialogAsync (.NET 9+; stable on .NET 10).\r\n" +
                "Prefer await on the UI thread (default ConfigureAwait). For Extended overloads that collide, use KryptonMessageBoxExtendedData.\r\n" +
                "Chrome smoke: ShowDialogAsync with owner, then dispose while awaiting ShowAsync on a modeless peer."
        };

        _status = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 48,
            Padding = new Padding(12),
            Text = @"Ready."
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            WrapContents = true,
            AutoScroll = true
        };

#if NET9_0_OR_GREATER
        buttons.Controls.Add(CreateButton("MessageBox.ShowAsync", async (_, _) =>
        {
            DialogResult result = await KryptonMessageBox.ShowAsync(this,
                "Async KryptonMessageBox.ShowAsync completed.",
                "4177",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Information);
            SetStatus($"MessageBox result: {result}");
        }));

        buttons.Controls.Add(CreateButton("TaskDialog.ShowDialogAsync", async (_, _) =>
        {
            using var dialog = new KryptonTaskDialog();
            dialog.Heading.Text = "Async Task Dialog";
            dialog.Heading.Visible = true;
            dialog.Content.Text = "Await ShowDialogAsync — UI stays responsive until you dismiss.";
            dialog.Content.Visible = true;
            dialog.FooterBar.CommonButtons.Buttons = KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
            dialog.FooterBar.Visible = true;
            DialogResult result = await dialog.ShowDialogAsync(this);
            SetStatus($"TaskDialog result: {result}");
        }));

        buttons.Controls.Add(CreateButton("KryptonForm chrome smoke", async (_, _) =>
        {
            using var modal = new KryptonForm
            {
                Text = @"Modal ShowDialogAsync",
                Size = new Size(360, 200),
                StartPosition = FormStartPosition.CenterParent
            };
            var label = new KryptonLabel
            {
                Text = @"Owner modal via ShowDialogAsync. Close with OK.",
                Dock = DockStyle.Fill,
                Padding = new Padding(16)
            };
            var ok = new KryptonButton { Text = @"OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
            modal.Controls.Add(label);
            modal.Controls.Add(ok);
            modal.AcceptButton = ok;
            DialogResult modalResult = await modal.ShowDialogAsync(this);

            var modeless = new KryptonForm
            {
                Text = @"Modeless ShowAsync (auto-close)",
                Size = new Size(320, 160),
                StartPosition = FormStartPosition.CenterScreen
            };
            modeless.Controls.Add(new KryptonLabel
            {
                Text = @"Disposed while awaiting ShowAsync.",
                Dock = DockStyle.Fill,
                Padding = new Padding(16)
            });
            Task showTask = modeless.ShowAsync(this);
            await Task.Delay(400);
            modeless.Close();
            modeless.Dispose();
            await showTask;
            SetStatus($"Chrome smoke: modal={modalResult}, ShowAsync completed after dispose.");
        }));

        buttons.Controls.Add(CreateButton("InputBox.ShowAsync", async (_, _) =>
        {
            string value = await KryptonInputBox.ShowAsync(new KryptonInputBoxData
            {
                Owner = this,
                Caption = "4177 Input",
                Prompt = "Enter a value (async):",
                DefaultResponse = "Hello"
            });
            SetStatus(string.IsNullOrEmpty(value) ? "InputBox cancelled." : $"InputBox value: {value}");
        }));

        buttons.Controls.Add(CreateButton("MessageBoxExtended.ShowAsync", async (_, _) =>
        {
            DialogResult result = await KryptonMessageBoxExtended.ShowAsync(new KryptonMessageBoxExtendedData
            {
                Owner = this,
                MessageText = "Async extended message box (data overload avoids ambiguity).",
                Caption = "4177 Extended",
                Buttons = ExtendedMessageBoxButtons.OKCancel,
                Icon = ExtendedKryptonMessageBoxIcon.Information
            });
            SetStatus($"MessageBoxExtended result: {result}");
        }));

        buttons.Controls.Add(CreateButton("FoldableDialog.ShowAsync", async (_, _) =>
        {
            DialogResult result = await KryptonFoldableDialog.ShowAsync(this,
                "Async foldable dialog",
                "Details are collapsible.",
                "Expanded details content for issue #4177.",
                "4177 Foldable",
                KryptonMessageBoxButtons.OKCancel,
                ExtendedKryptonMessageBoxIcon.Information);
            SetStatus($"FoldableDialog result: {result}");
        }));

        buttons.Controls.Add(CreateButton("AboutBox.ShowAsync", async (_, _) =>
        {
            DialogResult result = await KryptonAboutBox.ShowAsync(new KryptonAboutBoxData
            {
                ApplicationName = "Async Forms Demo",
                CurrentAssembly = typeof(Feature4177AsyncFormsDemo).Assembly,
                ShowToolkitInformation = false
            });
            SetStatus($"AboutBox result: {result}");
        }));

        buttons.Controls.Add(CreateButton("ThemeBrowser.ShowAsync", async (_, _) =>
        {
            await KryptonThemeBrowser.ShowAsync(new KryptonThemeBrowserData
            {
                ShowImportButton = false,
                ShowSilentOption = false
            });
            SetStatus("ThemeBrowser closed.");
        }));

        buttons.Controls.Add(CreateButton("StringCollection.ShowAsync", async (_, _) =>
        {
            string[] lines = await KryptonStringCollectionEditor.ShowAsync(this,
                new[] { "Alpha", "Beta" },
                true,
                "Edit lines (async):",
                "4177 String Collection");
            SetStatus(lines == null ? "StringCollection cancelled." : $"StringCollection count: {lines.Length}");
        }));

        buttons.Controls.Add(CreateButton("ExceptionDialog.ShowAsync", async (_, _) =>
        {
            await Krypton.Toolkit.Utilities.KryptonExceptionDialog.ShowAsync(
                new InvalidOperationException("Demo exception for ShowAsync."),
                null,
                true,
                false);
            SetStatus("ExceptionDialog closed.");
        }));

        buttons.Controls.Add(CreateButton("BinaryInformation.ShowAsync", async (_, _) =>
        {
            await KryptonPoweredByButton.ShowBinaryInformationAsync(ToolkitSupportType.Stable, true, true);
            SetStatus("Toolkit binary information closed.");
        }));

        buttons.Controls.Add(CreateButton("ComputeChecksum.ShowAsync", async (_, _) =>
        {
            DialogResult result = await KryptonComputeFileCheckSum.ShowAsync(this);
            SetStatus($"ComputeChecksum result: {result}");
        }));

        buttons.Controls.Add(CreateButton("MultilineStringEditor.ShowAsync", async (_, _) =>
        {
            DialogResult result = await KryptonMultilineStringEditor.ShowAsync(
                new[] { "Line 1", "Line 2" },
                true,
                "Async multiline editor",
                "4177");
            SetStatus($"MultilineStringEditor result: {result}");
        }));

        buttons.Controls.Add(CreateButton("Shell OpenFile.ShowDialogAsync", async (_, _) =>
        {
            using var ofd = new KryptonOpenFileDialog
            {
                Title = "4177 Shell async (prefer Custom for non-blocking await)",
                ProviderMode = KryptonDialogProviderMode.Custom
            };
            DialogResult result = await ofd.ShowDialogAsync(this);
            SetStatus($"OpenFileDialog result: {result}");
        }));

        buttons.Controls.Add(CreateButton("Toast basic Show*Async", async (_, _) =>
        {
            bool doNotShow = await KryptonToast.ShowBasicNotificationWithBooleanReturnValueAsync(
                new KryptonBasicToastData
                {
                    ToastHost = this,
                    NotificationTitle = "4177 Toast",
                    NotificationContent = "Modal basic toast with boolean return (async).",
                    ShowDoNotShowAgainOption = true,
                    CountDownSeconds = 60
                });
            SetStatus($"Toast do-not-show-again: {doNotShow}");
        }));

        buttons.Controls.Add(CreateButton("Toast user-input ShowNotificationAsync", async (_, _) =>
        {
            object value = await KryptonToast.ShowNotificationAsync(new KryptonUserInputToastData
            {
                ToastHost = this,
                NotificationTitle = "4177 User Input",
                NotificationContent = "Enter text (async):",
                NotificationInputAreaType = KryptonToastInputAreaType.TextBox,
                CountDownSeconds = 60
            });
            SetStatus($"User-input toast value: {value}");
        }));

        buttons.Controls.Add(CreateButton("ColorDialog.ShowDialogAsync", async (_, _) =>
        {
            using var dialog = new KryptonColorDialog();
            DialogResult result = await dialog.ShowDialogAsync(this);
            SetStatus($"ColorDialog result: {result}, color: {dialog.Color}");
        }));

        buttons.Controls.Add(CreateButton("FontDialog.ShowDialogAsync", async (_, _) =>
        {
            using var dialog = new KryptonFontDialog();
            DialogResult result = await dialog.ShowDialogAsync(this);
            SetStatus($"FontDialog result: {result}");
        }));
#else
        buttons.Controls.Add(new KryptonWrapLabel
        {
            AutoSize = true,
            MaximumSize = new Size(640, 0),
            Text = "This demo requires a net9.0-windows (or newer) build. Rebuild TestForm targeting net9+ / net10+ to exercise ShowAsync APIs."
        });
#endif

        Controls.Add(buttons);
        Controls.Add(_status);
        Controls.Add(instructions);
    }

#if NET9_0_OR_GREATER
    private static KryptonButton CreateButton(string text, Func<object?, EventArgs, Task> onClick)
    {
        var button = new KryptonButton
        {
            Text = text,
            Size = new Size(250, 40),
            Margin = new Padding(4)
        };
        button.Click += async (s, e) =>
        {
            try
            {
                await onClick(s, e);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, "4177 Async Demo", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        };
        return button;
    }

    private void SetStatus(string text) => _status.Text = text;
#endif
}
