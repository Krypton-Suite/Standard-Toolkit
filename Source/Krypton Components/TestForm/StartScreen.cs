#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using System.Diagnostics.CodeAnalysis;

namespace TestForm;

public partial class StartScreen : KryptonForm
{
    private readonly List<KryptonCommandLinkButton> _buttons;
    private readonly IComparer<KryptonCommandLinkButton> _headingComparer;
    private readonly Timer _filterTimer;
    private int _panelWidth;
    private Size _sizeAtStartup;

    public StartScreen()
    {
        InitializeComponent();

        // Init & basic settings
        _buttons = [];
        _headingComparer = new ButtonHeadingComparer();
        _panelWidth = tlpMain.Width;
        _filterTimer = new();
        _sizeAtStartup = new Size(1360, 633);

        btnDockTopRight.Click += OnBtnDockTopRightClick;
        btnRestoreSize.Click += OnBtnRestoreSizeClick;

        SetupFilterBox();
        SetupTableLayoutPanel();
        AddButtons();
        SortButtons();
        AddButtonsToTlpMain();

        this.Size = _sizeAtStartup;
    }

    /// <summary>
    /// Buttons to be displayed in the list can be added / removed or altered here.
    /// </summary>
    private void AddButtons()
    {
        CreateButton("AboutBox", "Try this About Box for a change", typeof(AboutBoxTest));
        CreateButton("Buttons Test", "All the buttons you want to test.", typeof(ButtonsTest));
        CreateButton("CommandLink Buttons", "No comment", typeof(CommandLinkButtons));
        CreateButton("Control Styles", "", typeof(ControlStylesForm));
        CreateButton("DateTime Example", "", typeof(DateTimeExample));
        CreateButton("FormBorder Test", "", typeof(FormBorderTest));
        CreateButton("Header Examples", "", typeof(HeaderExamples));
        CreateButton("Menu/Tool/Status Strips", "", typeof(MenuToolBarStatusStripTest));
        CreateButton("Powered By Button", "", typeof(PoweredByButtonForm));
        CreateButton("ProgressBar", "", typeof(ProgressBarTest));

        CreateButton("CommandLink Buttons", "", typeof(CommandLinkButtons));
        CreateButton("CommandLink Buttons", "", typeof(CommandLinkButtons));
        CreateButton("CommandLink Buttons", "", typeof(CommandLinkButtons));
        CreateButton("CommandLink Buttons", "", typeof(CommandLinkButtons));

    }

    private void CreateButton(string heading, string description, Type formType, Image? image = null )
    {
        KryptonCommandLinkButton button = new();
        
        if (!typeof(Form).IsAssignableFrom(formType))
        {
            throw new InvalidCastException("Parameter formType is not of type Form or derived from Form.");
        }

        button.CommandLinkTextValues.Heading         = heading;
        button.CommandLinkTextValues.Description     = description;
        button.AutoSize                              = false;
        button.Size                                  = new Size(_panelWidth - 10, 60);
        button.Click                                 += (_, _) => OnCommandLinkTestButtonClick(formType);

        if (image is not null)
        {
            button.CommandLinkTextValues.UseDefaultImage = false;
            button.CommandLinkTextValues.Image = new Bitmap(image, 48, 48);
        }

        _buttons.Add(button);
    }

    private void SetupFilterBox()
    {
        tbFilter.Clear();

        FontFamily family = KryptonManager.CurrentGlobalPalette.GetContentShortTextFont(PaletteContentStyle.InputControlStandalone, PaletteState.Normal)!.FontFamily;
        tbFilter.StateCommon.Content.Font = new Font(family, 14F, FontStyle.Regular);
        tbFilter.TextChanged += OnFilterChanged;
        btnClearFilter.Click += (_, _) => tbFilter.Clear();

        _filterTimer.Interval = 200;
        _filterTimer.Tick += OnFilterChangedPerformFilter;
    }

    private void OnBtnRestoreSizeClick(object? sender, EventArgs e)
    {
        this.Size = _sizeAtStartup;
    }

    private void OnBtnDockTopRightClick(object? sender, EventArgs e)
    {
        this.Location = new Point(Screen.FromControl(this).Bounds.Width - this.Width, 0);
    }

    private void OnCommandLinkTestButtonClick(Type formType ) 
    {
        Form? form = Activator.CreateInstance(formType) as Form;
        form?.Show();
    }

    private void SortButtons()
    {
        _buttons.Sort(_headingComparer);
    }

    private void SetupTableLayoutPanel()
    {
        tlpMain.RowCount = 0;
        tlpMain.ColumnCount = 1;

        tlpMain.AutoSize     = false;
        tlpMain.MaximumSize  = new Size(_panelWidth, 0);
        tlpMain.MinimumSize  = new Size(_panelWidth, 464);
        tlpMain.BackColor    = Color.Transparent;
        tlpMain.Padding      = new Padding(0);
        tlpMain.Margin       = new Padding(0);
        tlpMain.AutoScroll   = true;

        tlpMain.RowStyles.Clear();
        tlpMain.ColumnStyles.Clear();
        tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
    }

    private void AddButtonsToTlpMain()
    {
        _buttons.ForEach(button => {
            tlpMain.RowCount += 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMain.Controls.Add(button, 0, tlpMain.RowCount - 1);
        });
    }

    private void OnFilterChanged(object? sender, EventArgs e)
    {
        _filterTimer.Stop();
        _filterTimer.Start();
    }

    private void OnFilterChangedPerformFilter(object? sender, EventArgs e)
    {
        _filterTimer.Stop();

        if (tbFilter.Text.Length > 0)
        {
            _buttons.ForEach(button => button.Visible = button.CommandLinkTextValues.Heading.IndexOf(tbFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        else
        {
            _buttons.ForEach(button => button.Visible = true);
        }
    }
    private class ButtonHeadingComparer : IComparer<KryptonCommandLinkButton>
    {
        /// <summary>
        /// Compares the command link buttons case insensitive by their Heading string.
        /// </summary>
        /// <param name="x">A valid reference to the first button.</param>
        /// <param name="y">A valid reference to the second button.</param>
        /// <returns>
        /// 0 : Heading x and y equal.<br/>
        /// 1 : Heading x is greater than y.<br/>
        /// -1: Heading y is greater than x.
        /// </returns>
        /// <exception cref="NullReferenceException">Is thrown when at least x or y is null.</exception>
        public int Compare(KryptonCommandLinkButton? x, KryptonCommandLinkButton? y)
        {
            return (x is not null && y is not null)
                ? x.CommandLinkTextValues.Heading.ToLower().CompareTo(y.CommandLinkTextValues.Heading.ToLower())
                : throw new NullReferenceException($"ButtonHeadingComparer: make sure that parameter x and y both are valid references to a KryptonCommandLinkButton instance.");
        }
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
        new ControlStylesForm().Show();
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