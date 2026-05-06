#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
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
        CreateButton<AboutBoxTest>("AboutBox", "Try this About Box for a change");
        CreateButton<AccessibilityTest>("Accessibility Test (UIA Providers)", "Comprehensive demo and test for UIA Provider implementation (Issue #762). Tests all 10 controls with accessibility support, organized by category with detailed results.");
        CreateButton<ButtonsTest>("Buttons Test", "All the buttons you want to test.");
        CreateButton<Bug3381KryptonButtonRoundedTextCenteringDemo>("Bug 3381 KryptonButton Rounded Text Centering", "Demo for issue #3381: vertical and horizontal text centering inside heavily rounded KryptonButton (wide pill, Cyrillic, font metrics). Includes side-by-side stress, tall narrow capsule, low-rounding baseline, and live rounding / TextV / font / height controls.");
        CreateButton<CommandLinkButtons>("CommandLink Buttons", "No comment");
        CreateButton<ControlStylesForm>("Control Styles", string.Empty);
        CreateButton<DateTimeExample>("DateTime Example", string.Empty);
        CreateButton<DockingConfigSaveLoadTest>("Docking Config Save/Load Test", "Test SaveConfigToArray and LoadConfigFromArray");
        CreateButton<FormBorderTest>("FormBorder Test", string.Empty);
        CreateButton<HeaderExamples>("Header Examples", string.Empty);
        CreateButton<MenuToolBarStatusStripTest>("Menu/Tool/Status Strips", string.Empty);
        CreateButton<ProgressBarTest>("ProgressBar", "Checkout if progress has been made.");
        CreateButton<RibbonNavigatorWorkspaceTest>("Ribbon / Navigator / Workspace", string.Empty);
        CreateButton<FloatingWindowTest>("Floating Window Test", "Comprehensive test for floating window bug fix (Issue #2721)");
        CreateButton<SplashScreenExample>("Splash Screen", string.Empty);
        CreateButton<ThemeControlExamples>("Theme Controls", string.Empty);
        CreateButton<ToastNotificationTestChoice>("Toast", "For breakfast....?");
        CreateButton<WorkspaceTest>("WorkspaceTest", string.Empty);
        CreateButton<BlurExampleForm>("Blur Example", string.Empty);
        CreateButton<VisualControlsTest>("Visual Controls", string.Empty);
        CreateButton<BasicEmojiViewerForm>("EmojiViewer Basic", string.Empty);
        CreateButton<AdvancedEmojiViewerForm>("EmojiViewer Advanced", "Only hardcore devs can handle this one!");
        CreateButton<BreadCrumbTest>("BreadCrumb", "Follow the breadcrumbs and find the treasure...");
        CreateButton<CalendarTest>("Calendar", string.Empty);
        CreateButton<ComboBoxDateTimePickerConsistencyDemo>("ComboBox/DateTimePicker Consistency", "Comprehensive demonstration of KComboBox and KDateTimePicker consistency fix (Issue #1651). Shows drop-down buttons stretching to full height and centered text.");
        CreateButton<ControlsTest>("Controls Test", string.Empty);
        CreateButton<DataGridViewDemo>("KryptonDataGridView Demo", string.Empty);
        CreateButton<FadeFormTest>("FadeForm", string.Empty);
        CreateButton<GroupBoxTest>("GroupBox", string.Empty);
        CreateButton<InputBoxTest>("InputBox", string.Empty);
        CreateButton<MessageBoxTest>("MessageBox", string.Empty);
        CreateButton<Main>("Old Style Main: Fullscreen", string.Empty);
        CreateButton<PropertyGridTest>("PropertyGridTest", string.Empty);
        CreateButton<RibbonTest>("Ribbon", string.Empty);
        CreateButton<TextBoxEventTest>("TextBox", string.Empty);
        CreateButton<KryptonTextBoxValidatingTest>("TextBox Validating Test", "Tests fix for Validating event duplication bug #2801");
        CreateButton<RichTextBoxFormattingTest>("RichTextBox Formatting Test", "Tests fix for RichTextBox formatting preservation when palette changes (Issue #2832)");
        CreateButton<TreeViewExample>("TreeView", string.Empty);
        CreateButton<PanelForm>("Panel Form", string.Empty);
        CreateButton<PaletteViewerForm>("Palette Viewer", string.Empty);
        CreateButton<PoweredByButtonExample>("Powered By Button", string.Empty);
        CreateButton<KryptonTaskDialogDemoForm>("Krypton Task Dialog Demo", string.Empty);
        CreateButton<MdiWindow>("Krypton MDI Window", "KryptonForm MDI Container with both KForm and WForm children");
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

    private void CreateButton<TForm>(string heading, string description, Image? image = null) where TForm : Form
    {
        KryptonCommandLinkButton button = new();
        Type formType = typeof(TForm);
        
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
        if (Activator.CreateInstance(formType) is Form form)
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
