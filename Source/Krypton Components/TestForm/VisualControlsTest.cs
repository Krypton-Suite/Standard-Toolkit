#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using System.Reflection;
using System.Threading;

namespace TestForm;

public partial class VisualControlsTest : KryptonForm
{
    public VisualControlsTest()
    {
        InitializeComponent();
    }

    private void ShowFormByName(string typeName, params object[]? args)
    {
        var asm = typeof(KryptonForm).Assembly; // Krypton.Toolkit assembly
        var type = asm.GetType($"Krypton.Toolkit.{typeName}");
        if (type == null)
        {
            _ = MessageBox.Show($"Type {typeName} not found.");
            return;
        }

        try
        {
            var instance = Activator.CreateInstance(type, args ?? Array.Empty<object>());
            if (instance is Form frm)
            {
                // Only add dummy content when base form directly
                if (typeName == "VisualToastNotificationBaseForm")
                {
                    var label = new KryptonLabel
                    {
                        Text = "Demo Toast Notification Base Form",
                        Dock = DockStyle.Fill
                    };
                    frm.Controls.Add(label);
                    frm.ClientSize = new System.Drawing.Size(350, 100);
                }

                frm.ShowDialog(this);
            }
            else if (instance != null)
            {
                // try ShowEditor for MultilineStringEditor1
                var mi = type.GetMethod("ShowEditor", BindingFlags.Instance | BindingFlags.Public);
                mi?.Invoke(instance, null);
            }
            // Special handling for MultilineStringEditorForm to ensure content area visible
            if (typeName.StartsWith("VisualMultilineStringEditorForm"))
            {
                type.GetMethod("SetupInputCanvas", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.Invoke(instance, null);
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show($"Could not create instance of {typeName}: {ex.Message}");
        }
    }

    private void kbtnVisualInformationBoxForm_Click(object sender, EventArgs e) =>
        ShowFormByName("VisualInformationBoxForm");

    private void kbtnVisualMultilineStringEditorForm_Click(object sender, EventArgs e) =>
        ShowFormByName("VisualMultilineStringEditorForm", new string[] { "Alpha", "Beta", "Gamma" }, null!, true, "Demo header", "Multiline Editor Demo");

    private void kbtnVisualSplashScreen_Click(object sender, EventArgs e)
    {
        var data = new KryptonSplashScreenData
        {
            ShowApplicationName = true,
            ShowVersion = true,
            ShowProgressBar = false
        };

        ShowFormByName("VisualSplashScreenForm", data);
    }

    private void kbtnVisualTaskDialogForm_Click(object sender, EventArgs e)
    {
        var td = new KryptonTaskDialog
        {
            WindowTitle = "Demo TaskDialogForm",
            MainInstruction = "Hello from TaskDialogForm",
            Content = "This is a demo of VisualTaskDialogForm.",
            Icon = KryptonMessageBoxIcon.Information
        };

        ShowFormByName("VisualTaskDialogForm", td);
    }

    private void kbtnVisualTaskDialog_Click(object sender, EventArgs e)
    {
        var td = new KryptonTaskDialog
        {
            WindowTitle = "Demo VisualTaskDialog",
            MainInstruction = "Hello from VisualTaskDialog",
            Content = "This is a demo of VisualTaskDialog window.",
            Icon = KryptonMessageBoxIcon.Information
        };

        ShowFormByName("VisualTaskDialog", td);
    }

    private void kbtnVisualThemeBrowser_Click(object sender, EventArgs e)
    {
        var data = new KryptonThemeBrowserData { WindowTitle = "Theme Browser", StartIndex = 1 };
        KryptonThemeBrowser.Show(data);
    }

    private void kbtnVisualThemeBrowserRtlAware_Click(object sender, EventArgs e)
    {
        var data = new KryptonThemeBrowserData { WindowTitle = "Theme Browser RTL", StartIndex = 1 };
        ShowFormByName("VisualThemeBrowserFormRtlAware", data);
    }

    private void kbtnVisualToastNotification_Click(object sender, EventArgs e)
    {
        var data = new KryptonBasicToastNotificationData
        {
            NotificationTitle = "Demo Toast",
            NotificationContent = "This is a basic toast notification.",
            NotificationIcon = KryptonToastNotificationIcon.Information,
            ShowCloseBox = true,
            CountDownSeconds = 0
        };

        ShowFormByName("VisualToastNotificationBasicForm", data);
    }

    private void kbtnModalWaitDialog_Click(object sender, EventArgs e)
    {
        using var waitDlg = new ModalWaitDialog(true, 0, 100);

        for (int i = 0; i <= 100; i++)
        {
            waitDlg.UpdateProgressBarValue(i);
            waitDlg.UpdateDialog();
            Thread.Sleep(40);
        }

        waitDlg.Close();
    }
}