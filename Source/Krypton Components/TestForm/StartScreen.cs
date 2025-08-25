#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class StartScreen : KryptonForm
{
    public StartScreen()
    {
        InitializeComponent();
    }

    private void StartScreen_Load(object sender, EventArgs e)
    {
        // Dev note: visualize whether lines are button borders
        // Keep this in for later test cases, it can be quite handy!
        // Use the real About button (kbtnAbout) shown at the top-left
        /*
        kbtnAbout.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
        kbtnAbout.StateCommon.Border.Width = 2;
        kbtnAbout.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.None;
        kbtnAbout.StateCommon.Border.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.StateCommon.Border.Color1 = Color.Lime;
        kbtnAbout.StateCommon.Border.Color2 = Color.Lime;

        kbtnAbout.StateNormal.Border.DrawBorders = PaletteDrawBorders.All;
        kbtnAbout.StateNormal.Border.Width = 2;
        kbtnAbout.StateNormal.Border.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.StateNormal.Border.Color1 = Color.Lime;
        kbtnAbout.StateNormal.Border.Color2 = Color.Lime;

        kbtnAbout.StateDisabled.Border.DrawBorders = PaletteDrawBorders.All;
        kbtnAbout.StateDisabled.Border.Width = 2;
        kbtnAbout.StateDisabled.Border.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.StateDisabled.Border.Color1 = Color.Lime;
        kbtnAbout.StateDisabled.Border.Color2 = Color.Lime;

        kbtnAbout.StateTracking.Border.DrawBorders = PaletteDrawBorders.All;
        kbtnAbout.StateTracking.Border.Width = 2;
        kbtnAbout.StateTracking.Border.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.StateTracking.Border.Color1 = Color.Lime;
        kbtnAbout.StateTracking.Border.Color2 = Color.Lime;

        kbtnAbout.StatePressed.Border.DrawBorders = PaletteDrawBorders.All;
        kbtnAbout.StatePressed.Border.Width = 2;
        kbtnAbout.StatePressed.Border.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.StatePressed.Border.Color1 = Color.Lime;
        kbtnAbout.StatePressed.Border.Color2 = Color.Lime;

        kbtnAbout.OverrideDefault.Border.DrawBorders = PaletteDrawBorders.All;
        kbtnAbout.OverrideDefault.Border.Width = 2;
        kbtnAbout.OverrideDefault.Border.GraphicsHint = PaletteGraphicsHint.None;
        kbtnAbout.OverrideDefault.Border.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.OverrideDefault.Border.Color1 = Color.Lime;
        kbtnAbout.OverrideDefault.Border.Color2 = Color.Lime;

        // Also color the button background to detect any 1px interior gaps vs parent background
        kbtnAbout.StateCommon.Back.Draw = InheritBool.True;
        kbtnAbout.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.StateCommon.Back.Color1 = Color.FromArgb(200, 0, 0); // red fill
        kbtnAbout.StateCommon.Back.Color2 = Color.FromArgb(200, 0, 0);
        kbtnAbout.OverrideDefault.Back.ColorStyle = PaletteColorStyle.Solid;
        kbtnAbout.OverrideDefault.Back.Color1 = Color.FromArgb(200, 0, 0);
        kbtnAbout.OverrideDefault.Back.Color2 = Color.FromArgb(200, 0, 0);
        */
    }

    private void kbtnAboutBox_Click(object sender, EventArgs e)
    {
        var main = new Main();
        main.Show();
    }

    private void kbtnBreadCrumb_Click(object sender, EventArgs e)
    {
        var breadCrumb = new BreadCrumbTest();

        breadCrumb.Show();
    }

    private void kbtnButtons_Click(object sender, EventArgs e)
    {
        var buttons = new ButtonsTest();

        buttons.Show();
    }

    private void kbtnCommandLinkButtons_Click(object sender, EventArgs e)
    {
        var commandLinkButtons = new CommandLinkButtons();

        commandLinkButtons.Show();
    }

    private void kbtnFadeForm_Click(object sender, EventArgs e)
    {
        var fadeForm = new FadeFormTest();

        fadeForm.Show();
    }

    private void kbtnFormBorder_Click(object sender, EventArgs e)
    {
        var formBorder = new FormBorderTest();

        formBorder.Show();
    }

    private void kbtnGroupBox_Click(object sender, EventArgs e)
    {
        var groupBox = new GroupBoxTest();

        groupBox.Show();
    }

    private void kbtnMenuToolStatusStrips_Click(object sender, EventArgs e)
    {
        var menuToolBarStatusStrip = new MenuToolBarStatusStripTest();

        menuToolBarStatusStrip.Show();
    }

    private void kbtnMessageBox_Click(object sender, EventArgs e)
    {
        var messageBox = new MessageBoxTest();

        messageBox.Show();
    }

    private void kbtnProgressBar_Click(object sender, EventArgs e)
    {
        var progressBar = new ProgressBarTest();

        progressBar.Show();
    }

    private void kbtnRibbon_Click(object sender, EventArgs e)
    {
        var ribbon = new RibbonTest();

        ribbon.Show();
    }

    private void kbtnTextBox_Click(object sender, EventArgs e)
    {
        var textBoxEvent = new TextBoxEventTest();

        textBoxEvent.Show();
    }

    private void kbtnTheme_Click(object sender, EventArgs e)
    {
    }

    private void kbtnToast_Click(object sender, EventArgs e)
    {
        var toastNotification = new ToastNotificationTestChoice();

        toastNotification.Show();
    }

    private void kbtnExit_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;

        Close();
    }

    private void kbtnTreeView_Click(object sender, EventArgs e)
    {
        var treeViewExample = new TreeViewExample();

        treeViewExample.Show();
    }

    private void kbtnCalendar_Click(object sender, EventArgs e)
    {
        var calendarTest = new CalendarTest();

        calendarTest.Show();
    }

    private void kbtnWorkspace_Click(object sender, EventArgs e)
    {
        var workspaceTest = new WorkspaceTest();

        workspaceTest.Show();
    }

    private void kbtnThemeControls_Click(object sender, EventArgs e)
    {
        var themeControls = new ThemeControlExamples();

        themeControls.Show();
    }

    private void kbtnControlsTest_Click(object sender, EventArgs e)
    {
        var controlsTest = new ControlsTest();

        controlsTest.Show();
    }

    private void kbtnHeaderExamples_Click(object sender, EventArgs e)
    {
        var headerExamples = new HeaderExamples();

        headerExamples.Show();
    }

    private void kbtnInputBox_Click(object sender, EventArgs e)
    {
        var inputBox = new InputBoxTest();

        inputBox.Show();
    }

    private void kbtnAbout_Click(object sender, EventArgs e)
    {
        var aboutBox = new AboutBoxTest();

        aboutBox.Show();
    }

    private void kbtnDataGrid_Click(object sender, EventArgs e)
    {
        var form = new DataGridViewDemo();

        form.Show();
    }

    private void btnColourTestimonials_Click(object sender, EventArgs e)
    {
        new PanelForm().Show();
    }

    private void kbtnRibbonNavigatorWorkspace_Click(object sender, EventArgs e)
    {
        new RibbonNavigatorWorkspaceTest().Show();
    }

    private void kbtnPropertyGrid_Click(object sender, EventArgs e)
    {
        new PropertyGridTest().Show(this);
    }

    private void kbtnDateTime_Click(object sender, EventArgs e)
    {
        new DateTimeExample().Show();
    }

    private void kbtnControlStyles_Click(object sender, EventArgs e)
    {
        new ControlStyles().Show();
    }

    private void kbtnSplashScreen_Click(object sender, EventArgs e)
    {
        new SplashScreenExample().Show();
    }

    private void kbtnPoweredByButton_Click(object sender, EventArgs e)
    {
        new PoweredByButtonForm().Show();
    }

    private void kbtnBlurredForm_Click(object sender, EventArgs e)
    {
        new BlurExampleForm().Show();
    }

    private void kbtnPaletteViewer_Click(object sender, EventArgs e)
    {
        var viewer = new PaletteViewerForm();
        viewer.AttachKryptonManager(kryptonManager1);
        viewer.Show();
    }

    private void kbtnVisualControls_Click(object sender, EventArgs e)
    {
        var vcontrols = new VisualControlsTest();
        vcontrols.Show(this);
    }

    private void kbtnBasicEmojiViewer_Click(object sender, EventArgs e)
    {
        new BasicEmojiViewerForm().Show();
    }

    private void kbtnAdvancedEmojiViewer_Click(object sender, EventArgs e)
    {
        new AdvancedEmojiViewerForm().Show();
    }
}