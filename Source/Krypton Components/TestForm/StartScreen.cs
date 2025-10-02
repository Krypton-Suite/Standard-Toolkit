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
    private readonly List<KryptonCommandLinkButton> _buttons;
    private readonly IComparer<KryptonCommandLinkButton> _headingComparer;
    private readonly Timer _filterTimer;
    private int _panelWidth;
    private Size _sizeAtStartup;
    private RegistryAccess _registryAccess;
    private bool _dockTopRight;

    public StartScreen()
    {
        InitializeComponent();

        // Init & basic settings
        _registryAccess = new();
        _dockTopRight = false;
        _buttons = [];
        _headingComparer = new ButtonHeadingComparer();
        _panelWidth = tlpMain.Width;
        _filterTimer = new();
        _sizeAtStartup = new Size(902, 633);
        this.Size = _sizeAtStartup;
        this.FormClosed += OnFormClosed;

        btnDockTopRight.Click += OnBtnDockTopRightClick;
        btnRestoreSize.Click += OnBtnRestoreSizeClick;

        SetupFilterBox();
        SetupTableLayoutPanel();
        AddButtons();
        SortButtons();
        AddButtonsToTlpMain();
        RestoreSettings();
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
        CreateButton("Ribbon / Navigator / Workspace", "", typeof(RibbonNavigatorWorkspaceTest));
        CreateButton("Splash Screen", "", typeof(SplashScreenExample));
        CreateButton("Theme Controls", "", typeof(ThemeControlExamples));
        CreateButton("Toast", "For breakfast....?", typeof(ToastNotificationTestChoice));
        CreateButton("WorkspaceTest", "", typeof(WorkspaceTest));
        CreateButton("Blur Example", "", typeof(BlurExampleForm));
        CreateButton("Visual Controls", "", typeof(VisualControlsTest));
        CreateButton("EmojiViewer Basic", "", typeof(BasicEmojiViewerForm));
        CreateButton("EmojiViewer Advanced", "Only hardcore devs can handle this one!", typeof(AdvancedEmojiViewerForm));
        CreateButton("BreadCrumb", "Follow the breadcrumbs and find the treasure...", typeof(BreadCrumbTest));
        CreateButton("Calendar", "", typeof(CalendarTest));
        CreateButton("Controls Test", "", typeof(ControlsTest));
        CreateButton("KryptonDataGridView Demo", "", typeof(DataGridViewDemo));
        CreateButton("FadeForm", "", typeof(FadeFormTest));
        CreateButton("GroupBox", "", typeof(GroupBoxTest));
        CreateButton("InputBox", "", typeof(InputBoxTest));
        CreateButton("MessageBox", "", typeof(MessageBoxTest));
        CreateButton("Old Style Main: Fullscreen", "", typeof(Main));
        CreateButton("PropertyGridTest", "", typeof(PropertyGridTest));
        CreateButton("Ribbon", "", typeof(RibbonTest));
        CreateButton("TextBox", "", typeof(TextBoxEventTest));
        CreateButton("TreeView", "", typeof(TreeViewExample));
        CreateButton("Panel Form", "", typeof(PanelForm));
        CreateButton("Palette Viewer", "", typeof(PaletteViewerForm));
    }

    private void OnFormClosed(object? sender, FormClosedEventArgs e)
    {
        _registryAccess.LastFilterString = tbFilter.Text;
        _registryAccess.DockTopRight = FormIsDockedTopRight();
        _registryAccess.FormSize = this.Size;
    }

    private bool FormIsDockedTopRight()
    {
        return this.Top == 0 && this.Left == (Screen.FromControl(this).Bounds.Width - this.Size.Width);
    }

    private void RestoreSettings()
    {
        _dockTopRight = _registryAccess.DockTopRight;
        if (_dockTopRight)
        {
            OnBtnDockTopRightClick(null, EventArgs.Empty);
        }

        string lastFilter = _registryAccess.LastFilterString;
        if (lastFilter.Length > 0)
        {
            tbFilter.Text = lastFilter;
        }

        Size size = _registryAccess.FormSize;
        if (size.Width > 0 && size.Height > 0)
        {
            this.Size = _registryAccess.FormSize;
        }
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
        _dockTopRight = true;
        this.Location = new Point(Screen.FromControl(this).Bounds.Width - this.Width, 0);
    }

    private void OnCommandLinkTestButtonClick(Type formType)
    {
        // This one needs a special handling
        if (formType == typeof(PaletteViewerForm)
            && Activator.CreateInstance(formType) is PaletteViewerForm paletteViewerForm)
        {
            paletteViewerForm.AttachKryptonManager(kryptonManager1);
            paletteViewerForm.Show();
        }
        else if (Activator.CreateInstance(formType) is Form form)
        {
            form.Show();
        }
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

        if (tlpMain.Controls.Count > 0)
        {
            tlpMain.ScrollControlIntoView(tlpMain.Controls[0]);
        }
    }

    private void kbtnExit_Click(object? sender, EventArgs e)
    {
        this.Close();
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

}