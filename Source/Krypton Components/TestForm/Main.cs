#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

using System.Diagnostics;
using System.Reflection;

namespace TestForm;

public partial class Main : KryptonForm
{
    public Main()
    {
        InitializeComponent();
    }

    private void AddEvent(string message) => kryptonListBox1.Items.Add(message);

    private void textBox1_Validated(object sender, EventArgs e) => AddEvent(nameof(textBox1_Validated));

    private void kryptonTextBox1_Validated(object sender, EventArgs e) => AddEvent(nameof(kryptonTextBox1_Validated));

    private void kryptonTextBox1_DoubleClick(object sender, EventArgs e) => AddEvent(nameof(kryptonTextBox1_DoubleClick));

    private void kryptonTextBox1_MouseDoubleClick(object sender, MouseEventArgs e) => AddEvent(nameof(kryptonTextBox1_MouseDoubleClick));

    private void kryptonTextBox1_MouseClick(object sender, MouseEventArgs e) => AddEvent(nameof(kryptonTextBox1_MouseClick));

    private void kryptonTextBox1_Click(object sender, EventArgs e) => AddEvent(nameof(kryptonTextBox1_Click));

    private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e) => AddEvent(nameof(textBox1_MouseDoubleClick));

    private void textBox1_MouseClick(object sender, MouseEventArgs e) => AddEvent(nameof(textBox1_MouseClick));

    private void textBox1_DoubleClick(object sender, EventArgs e) => AddEvent(nameof(textBox1_DoubleClick));

    private void textBox1_Click(object sender, EventArgs e) => AddEvent(nameof(textBox1_Click));

    private void textBox1_Validating(object sender, CancelEventArgs e) => AddEvent(nameof(textBox1_Validating));

    private void kryptonTextBox1_Validating(object sender, CancelEventArgs e) => AddEvent(nameof(kryptonTextBox1_Validating));

    private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => AddEvent(nameof(textBox1_PreviewKeyDown));

    private void kryptonTextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => AddEvent(nameof(kryptonTextBox1_PreviewKeyDown));

    private void kryptonTextBox1_KeyDown(object sender, KeyEventArgs e) => AddEvent(nameof(kryptonTextBox1_KeyDown));

    private void kryptonTextBox1_KeyPress(object sender, KeyPressEventArgs e) => AddEvent(nameof(kryptonTextBox1_KeyPress));

    private void kryptonTextBox1_KeyUp(object sender, KeyEventArgs e) => AddEvent(nameof(kryptonTextBox1_KeyUp));

    private void textBox1_KeyDown(object sender, KeyEventArgs e) => AddEvent(nameof(textBox1_KeyDown));

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e) => AddEvent(nameof(textBox1_KeyPress));

    private void textBox1_KeyUp(object sender, KeyEventArgs e) => AddEvent(nameof(textBox1_KeyUp));

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        var form2 = new BreadCrumbTest();

        form2.ShowDialog();
    }

    private void kryptonButton2_Click(object sender, EventArgs e)
    {
        var form3 = new RibbonTest();

        form3.ShowDialog();
    }

    private void kbtnTestMessagebox_Click(object sender, EventArgs e)
    {
        KryptonMessageBox.Show(this, @"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information, showCloseButton: kryptonCheckBox1.Checked);

        KryptonMessageBox.Show(this, @"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information, KryptonMessageBoxDefaultButton.Button1,
            options: MessageBoxOptions.RtlReading, 
            showCloseButton: kryptonCheckBox1.Checked);
    }

    private void kcmdMessageboxTest_Execute(object sender, EventArgs e)
    {
        try
        {
            Process.Start(@"C:\\Windows\\Notepad.exe");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }
    }

    private void kbtnIntegratedToolbar_Click(object sender, EventArgs e)
    {
            
    }

    private void kryptonButton3_Click(object sender, EventArgs e)
    {
        var data = new KryptonThemeBrowserData()
        {
            ShowImportButton = true,
            ShowSilentOption = true,
            StartIndex = GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX,
            StartPosition = FormStartPosition.CenterScreen,
            WindowTitle = KryptonManager.Strings.MiscellaneousThemeStrings.ThemeBrowserWindowTitle
        };

        KryptonThemeBrowser.Show(data);
    }

    private void kchkUseProgressValueAsText_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.UseValueAsText = kchkUseProgressValueAsText.Checked;
    }

    private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.Value = ktrkProgressValues.Value;
    }

    private void kbtnVisualStudio2010Theme_Click(object sender, EventArgs e)
    {
    }

    private void kryptonButton4_Click(object sender, EventArgs e)
    {
        new GroupBoxTest().Show();
    }

    private void kbtnExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    //private void kryptonButton6_Click(object sender, EventArgs e)
    //{
    //    kryptonCustomPaletteBase1.Import();
    //}

    //private void kryptonButton7_Click(object sender, EventArgs e)
    //{
    //    kryptonCustomPaletteBase1.Export();
    //}

    private void kryptonButton8_Click(object sender, EventArgs e)
    {
        var aboutBoxData = new KryptonAboutBoxData()
        {
            ApplicationName = @"TestForm",
            CurrentAssembly = Assembly.GetExecutingAssembly(),
            HeaderImage = null,
            MainImage = null,
            ShowToolkitInformation = true,
        };

        var aboutToolkitData = new KryptonAboutToolkitData();

        KryptonAboutBox.Show(aboutBoxData, aboutToolkitData);
    }

    private void kcbtnNone_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.None);

    private void kcbtnFixedSingle_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.FixedSingle);

    private void kcbtnFixed3D_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.Fixed3D);

    private void kcbtnFixedDialog_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.FixedDialog);

    private void kcbtnSizable_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.Sizable);

    private void kcbtnFixedToolWindow_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.FixedToolWindow);

    private void kcbtnSizableToolWindow_Click(object sender, EventArgs e) => UpdateBorderStyle(FormBorderStyle.SizableToolWindow);

    private void UpdateBorderStyle(FormBorderStyle borderStyle)
    {
        switch (borderStyle)
        {
            case FormBorderStyle.None:
                FormBorderStyle = FormBorderStyle.None;

                kcbtnNone.Checked = true;

                kcbtnFixed3D.Checked = false;

                kcbtnFixedDialog.Checked = false;

                kcbtnFixedSingle.Checked = false;

                kcbtnFixedToolWindow.Checked = false;

                kcbtnSizable.Checked = false;

                kcbtnSizableToolWindow.Checked = false;
                break;

            case FormBorderStyle.FixedSingle:
                FormBorderStyle = FormBorderStyle.FixedSingle;

                kcbtnNone.Checked = false;

                kcbtnFixed3D.Checked = false;

                kcbtnFixedDialog.Checked = false;

                kcbtnFixedSingle.Checked = true;

                kcbtnFixedToolWindow.Checked = false;

                kcbtnSizable.Checked = false;

                kcbtnSizableToolWindow.Checked = false;
                break;
            case FormBorderStyle.Fixed3D:
                FormBorderStyle = FormBorderStyle.Fixed3D;

                kcbtnNone.Checked = false;

                kcbtnFixed3D.Checked = true;

                kcbtnFixedDialog.Checked = false;

                kcbtnFixedSingle.Checked = false;

                kcbtnFixedToolWindow.Checked = false;

                kcbtnSizable.Checked = false;

                kcbtnSizableToolWindow.Checked = false;
                break;
            case FormBorderStyle.FixedDialog:
                FormBorderStyle = FormBorderStyle.FixedDialog;

                kcbtnNone.Checked = false;

                kcbtnFixed3D.Checked = false;

                kcbtnFixedDialog.Checked = true;

                kcbtnFixedSingle.Checked = false;

                kcbtnFixedToolWindow.Checked = false;

                kcbtnSizable.Checked = false;

                kcbtnSizableToolWindow.Checked = false;
                break;
            case FormBorderStyle.Sizable:
                FormBorderStyle = FormBorderStyle.Sizable;

                kcbtnNone.Checked = false;

                kcbtnFixed3D.Checked = false;

                kcbtnFixedDialog.Checked = false;

                kcbtnFixedSingle.Checked = false;

                kcbtnFixedToolWindow.Checked = false;

                kcbtnSizable.Checked = true;

                kcbtnSizableToolWindow.Checked = false;
                break;
            case FormBorderStyle.FixedToolWindow:
                FormBorderStyle = FormBorderStyle.FixedToolWindow;

                kcbtnNone.Checked = false;

                kcbtnFixed3D.Checked = false;

                kcbtnFixedDialog.Checked = false;

                kcbtnFixedSingle.Checked = false;

                kcbtnFixedToolWindow.Checked = true;

                kcbtnSizable.Checked = false;

                kcbtnSizableToolWindow.Checked = false;
                break;
            case FormBorderStyle.SizableToolWindow:
                FormBorderStyle = FormBorderStyle.SizableToolWindow;

                kcbtnNone.Checked = false;

                kcbtnFixed3D.Checked = false;

                kcbtnFixedDialog.Checked = false;

                kcbtnFixedSingle.Checked = false;

                kcbtnFixedToolWindow.Checked = false;

                kcbtnSizable.Checked = false;

                kcbtnSizableToolWindow.Checked = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(borderStyle), borderStyle, null);
        }
    }

    private void kryptonButton5_Click(object sender, EventArgs e)
    {
        kryptonTaskDialog1.ShowDialog();
    }

    private void kryptonButton9_Click(object sender, EventArgs e)
    {
        var commandLinks = new CommandLinkButtons();

        commandLinks.ShowDialog();
    }

    private void kryptonColorButton1_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonButton1.Values.DropDownArrowColor = e.Color;
    }

    private void kryptonButton10_Click(object sender, EventArgs e)
    {
        var toastNotification = new ToastNotificationTestChoice();

        toastNotification.Show();
    }

    private void kryptonButton11_Click(object sender, EventArgs e)
    {
        KryptonMessageBox.Show(string.Empty, @"Test with no Text", showCloseButton: kryptonCheckBox1.Checked);
    }

    private void kryptonButton12_Click(object sender, EventArgs e)
    {
        var fadeForm = new FadeFormTest();

        fadeForm.ShowDialog();
    }

    private void kcmdOpenImage_Execute(object sender, EventArgs e)
    {

    }

    private void kbtnDialogs_Click(object sender, EventArgs e)
    {
        var kde = new KryptonDialogExamples();

        kde.ShowDialog();
    }

    private void kryptonButton13_Click(object sender, EventArgs e)
    {
        new CheckBoxStyleExamples().Show();
    }

    private void kryptonButton14_Click(object sender, EventArgs e)
    {
        KryptonMessageBox.Show("question?", "title", KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Warning, KryptonMessageBoxDefaultButton.Button2);
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
            
    }

    private void kryptonButton15_Click(object sender, EventArgs e)
    {
        new PanelForm().ShowDialog(this);
    }

    private void kbtnExceptionDialog_Click(object sender, EventArgs e)
    {
        try
        {
            throw new ArgumentOutOfRangeException();
        }
        catch (Exception ex)
        {
            KryptonExceptionDialog.Show(ex, true, true);
        }
    }

    private void kryptonButton16_Click(object sender, EventArgs e)
    {
        new ToggleSwitchTest().ShowDialog();
    }

    private void kryptonButton17_Click(object sender, EventArgs e)
    {
        new CheckedListBoxDemo().ShowDialog();
    }
}