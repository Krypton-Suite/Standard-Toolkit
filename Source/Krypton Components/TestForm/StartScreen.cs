#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using System.Reflection;
using System.Windows.Forms;

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
        SetupExitButton();
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
        CreateButton("Control Styles", string.Empty, typeof(ControlStylesForm));
        CreateButton("DateTime Example", string.Empty, typeof(DateTimeExample));
        CreateButton("FormBorder Test", string.Empty, typeof(FormBorderTest));
        CreateButton("Header Examples", string.Empty, typeof(HeaderExamples));
        CreateButton("Menu/Tool/Status Strips", string.Empty, typeof(MenuToolBarStatusStripTest));
        CreateButton("ProgressBar", "Checkout if progress has been made.", typeof(ProgressBarTest));
        CreateButton("Ribbon / Navigator / Workspace", string.Empty, typeof(RibbonNavigatorWorkspaceTest));
        CreateButton("Splash Screen", string.Empty, typeof(SplashScreenExample));
        CreateButton("Theme Controls", string.Empty, typeof(ThemeControlExamples));
        CreateButton("Toast", "For breakfast....?", typeof(ToastNotificationTestChoice));
        CreateButton("WorkspaceTest", string.Empty, typeof(WorkspaceTest));
        CreateButton("Blur Example", string.Empty, typeof(BlurExampleForm));
        CreateButton("Visual Controls", string.Empty, typeof(VisualControlsTest));
        CreateButton("EmojiViewer Basic", string.Empty, typeof(BasicEmojiViewerForm));
        CreateButton("EmojiViewer Advanced", "Only hardcore devs can handle this one!", typeof(AdvancedEmojiViewerForm));
        CreateButton("BreadCrumb", "Follow the breadcrumbs and find the treasure...", typeof(BreadCrumbTest));
        CreateButton("Calendar", string.Empty, typeof(CalendarTest));
        CreateButton("Controls Test", string.Empty, typeof(ControlsTest));
        CreateButton("KryptonDataGridView Demo", string.Empty, typeof(DataGridViewDemo));
        CreateButton("FadeForm", string.Empty, typeof(FadeFormTest));
        CreateButton("GroupBox", string.Empty, typeof(GroupBoxTest));
        CreateButton("InputBox", string.Empty, typeof(InputBoxTest));
        CreateButton("MessageBox", string.Empty, typeof(MessageBoxTest));
        CreateButton("Old Style Main: Fullscreen", string.Empty, typeof(Main));
        CreateButton("PropertyGridTest", string.Empty, typeof(PropertyGridTest));
        CreateButton("Ribbon", string.Empty, typeof(RibbonTest));
        CreateButton("TextBox", string.Empty, typeof(TextBoxEventTest));
        CreateButton("TreeView", string.Empty, typeof(TreeViewExample));
        CreateButton("Panel Form", string.Empty, typeof(PanelForm));
        CreateButton("Palette Viewer", string.Empty, typeof(PaletteViewerForm));
        CreateButton("Powered By Button", string.Empty, typeof(PoweredByButtonExample));
        CreateButton("Krypton Task Dialog Demo", string.Empty, typeof(KryptonTaskDialogDemoForm));
    }

    private void OnFormClosed(object? sender, FormClosedEventArgs e)
    {
        _registryAccess.LastFilterString = tbFilter.Text;
        _registryAccess.DockTopRight = IsFormDockedTopRight();
        _registryAccess.FormSize = this.Size;
    }

    private bool IsFormDockedTopRight()
    {
        return this.Top == 0 
            && this.Left == Screen.FromControl(this).Bounds.Width - this.Size.Width;
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

    private void SetupExitButton()
    {
        FontFamily family = KryptonManager.CurrentGlobalPalette.GetContentShortTextFont(PaletteContentStyle.InputControlStandalone, PaletteState.Normal)!.FontFamily;
        kbtnExit.StateCommon.Content.ShortText.Font = new Font(family, 14F, FontStyle.Regular);
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
        SetTableLayoutPanelDoubleBuffered(true);
        tlpMain.RowCount = 0;
        tlpMain.ColumnCount = 1;

        tlpMain.AutoSize     = false;
        tlpMain.BackColor    = Color.Transparent;
        tlpMain.Padding      = new Padding(0);
        tlpMain.Margin       = new Padding(0);
        tlpMain.AutoScroll   = true;

        tlpMain.RowStyles.Clear();
        tlpMain.ColumnStyles.Clear();
        tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
    }
    
    private void SetTableLayoutPanelDoubleBuffered(bool enableDoubleBuffering)
    {
        PropertyInfo? propertyInfo = typeof(TableLayoutPanel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        if (propertyInfo is not null)
        {
            propertyInfo.SetValue(tlpMain, enableDoubleBuffering);
        }
        else
        {
            throw new NullReferenceException(nameof(propertyInfo));
        }
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
        /// 0 : Heading x and y are equal.<br/>
        /// 1 : Heading x is greater than y.<br/>
        /// -1: Heading y is greater than x.
        /// </returns>
        /// <exception cref="NullReferenceException">Is thrown when at least x or y is null.</exception>
        public int Compare(KryptonCommandLinkButton? x, KryptonCommandLinkButton? y)
        {
            if (x is not null && y is not null)
            {
                string headingX = x.CommandLinkTextValues.Heading.ToLower(CultureInfo.InvariantCulture);
                string headingY = y.CommandLinkTextValues.Heading.ToLower(CultureInfo.InvariantCulture);

                return headingX.CompareTo(headingY);
            }
            else
            {
                throw new NullReferenceException($"ButtonHeadingComparer: make sure that parameter x and y both are valid references to a KryptonCommandLinkButton instance.");
            }
        }
    }
}