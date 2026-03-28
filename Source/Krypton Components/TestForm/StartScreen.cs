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

using Krypton.Utilities;

namespace TestForm;

public partial class StartScreen : KryptonForm
{
    private readonly List<KryptonCommandLinkButton> _buttons;
    private readonly IComparer<KryptonCommandLinkButton> _headingComparer;
    private readonly Timer _filterTimer;
    private readonly int _panelWidth;
    private readonly Size _sizeAtStartup;
    private readonly RegistryAccess _registryAccess;
    private bool _dockTopRight;

    public StartScreen()
    {
        InitializeComponent();

        // Init & basic settings
        _registryAccess = new RegistryAccess();
        _dockTopRight = false;
        _buttons = [];
        _headingComparer = new ButtonHeadingComparer();
        _panelWidth = tlpMain.Width;
        _filterTimer = new Timer();
        _sizeAtStartup = new Size(902, 633);
        
        this.Size = _sizeAtStartup;
        this.FormClosing += OnFormClosing;

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
        CreateButton<ButtonBadgeTest>("Badge Test", "Comprehensive badge functionality demonstration for KryptonButton and KryptonCheckButton.");
        CreateButton<ButtonTextTrackingExample>("Button Text Tracking", "Demonstrates alternate text color for tracking (hover) state on KryptonButton, KryptonCheckButton, KryptonColorButton and other controls (Issue #1326). Improves readability in dark themes.");
        CreateButton<ButtonsTest>("Buttons Test", "All the buttons you want to test.");
        CreateButton<KryptonColorButtonDemo>("KryptonColorButton Custom Colours", "Comprehensive demo of KryptonColorButton custom colours (Issue #776): CustomColors, MaxCustomColors, and visibility. Only 10 colours, or custom + theme + standard, or cap display count.");
        CreateButton<BorderlessFormDemo>("Borderless Form Demo", "Demo for Issue #2922: Borderless KryptonForm without system title bar flicker on startup. Form should appear directly in borderless state.");
        CreateButton<Bug2914Test>("Bug 2914 Test", "Tests the fix for 2914.");
        CreateButton<Bug2984SeparatorTest>("Bug 2984 Separator Test", "Demo for Issue #2984: NullReferenceException in ViewDrawSeparator.RenderBefore. Exercises KryptonNavigator (Outlook), KryptonSplitContainer, and KryptonSeparator. Swap themes to verify no crash.");
        CreateButton<Bug3025KryptonLabelAutoSizeDemo>("Bug 3025 KryptonLabel AutoSize Demo", "Demo for Issue #3025: KryptonLabel with AutoSize now resizes to fit text when placed in the Designer (click-drag). Shows AutoSize on/off, LabelStyles, short/long text, and text + image.");
        CreateButton<Bug2935MdiMultiMonitorDemo>("Bug 2935 MDI multi-monitor", "Demo for issue #2935: maximized MDI child form border drawn on the correct monitor. Move the MDI parent to a second monitor, open and maximize a child; the border should stay on the same monitor.");
        CreateButton<Bug3013TestForm>("Bug 3013 Test", "Tests the fix for 3013.");
        CreateButton<BugReportingDialogTest>("BugReportingTool", "Easily report bugs with this tool.");
        CreateButton<CodeEditorTest>("Code Editor", "Native code editor with syntax highlighting, line numbering, code folding, and auto-completion.");
        CreateButton<CountdownButtonTest>("Countdown Button", "Comprehensive demonstration of KryptonCountdownButton features with customizable duration, format, and enable-at-zero options.");
        CreateButton<CommandLinkButtons>("CommandLink Buttons", "No comment");
        CreateButton<ControlStylesForm>("Control Styles", string.Empty);
        CreateButton<KryptonDateTimePickerMonthCalendarDemo>("DateTimePicker Month Calendar Background", "Comprehensive demo of KryptonDateTimePicker month calendar custom background (Issue #1827): CalendarBackColor, theme default, presets (dark/light), and pick-a-color to style the drop-down calendar.");
        CreateButton<DateTimeExample>("DateTime Example", string.Empty);
        CreateButton<DockingConfigSaveLoadTest>("Docking Config Save/Load Test", "Test SaveConfigToArray and LoadConfigFromArray");
        CreateButton<DockingRedockDemo>("Docking Redock Demo", "Demo for Issue #2933: undock (Float) then redock; no floating window left behind.");
        CreateButton<FontAwesomeTest>("Font Awesome Test", string.Empty);
        CreateButton<FloatingWindowTest>("Floating Window Test", "Comprehensive test for floating window bug fix (Issue #2721)");
        CreateButton<FloatingToolbarsDemo>("Floating Toolbars Demo", "Comprehensive demonstration of KryptonFloatingToolbars features including drag-and-drop floating/docking, programmatic control, animation, window styles, docking preview indicators, custom themes, state persistence, and multi-monitor support.");
        CreateButton<FlowLayoutPanelTest>("FlowLayoutPanel", "Test KryptonFlowLayoutPanel with dynamic control layout and flow directions.");
        CreateButton<FileSystemWatcherTest>("FileSystemWatcher", "Monitor file system changes with Krypton integration.");
        CreateButton<ErrorProviderTest>("ErrorProvider", string.Empty);
        CreateButton<FileCheckSumDemo>("File checksum (Compute && Verify)", "Compute or verify file hashes (MD5, SHA-1, SHA-256, SHA-384, SHA-512, RIPEMD-160) using the Krypton checksum dialogs.");
        CreateButton<FormBorderTest>("FormBorder Test", string.Empty);
        CreateButton<HeaderExamples>("Header Examples", string.Empty);
        CreateButton<HelpProviderTest>("HelpProvider", "Test KryptonHelpProvider functionality");
        CreateButton<MenuToolBarStatusStripTest>("Menu/Tool/Status Strips", string.Empty);
        CreateButton<NotifyIconTest>("NotifyIcon", "Comprehensive demonstration of KryptonNotifyIcon with all events, balloon tips, and context menu support.");
        CreateButton<OAuth2Demo>("OAuth2 PKCE Demo", "Comprehensive OAuth2 with PKCE demo. Sign in with Azure AD, Google, or GitHub using embedded WebView2 or system browser. Configure client ID, redirect URI, and scopes.");
        CreateButton<ProgressBarTest>("ProgressBar", "Checkout if progress has been made.");
        CreateButton<ScrollBarTest>("ScrollBar", "Comprehensive demonstration of KryptonHScrollBar and KryptonVScrollBar controls with basic usage, scrolling content, synchronization, theming, programmatic control, and event logging.");
        CreateButton<ScrollbarManagerTest>("Scrollbar Manager", "Comprehensive demonstration of KryptonScrollbarManager with container mode, native wrapper mode, dynamic content, and integration examples.");
        CreateButton<RibbonNavigatorWorkspaceTest>("Ribbon / Navigator / Workspace", string.Empty);
        CreateButton<RTLControlsTest>("RTL Compliance Tests", "Test the Krypton.Toolkit controls for compliance.");
        CreateButton<SplashScreenExample>("Splash Screen", string.Empty);
        CreateButton<TaskbarOverlayIconTest>("Taskbar Overlay Icon Test", "Comprehensive demonstration of taskbar overlay icons on KryptonForm with configurable icons, descriptions, and interactive examples.");
        CreateButton<TaskbarThumbnailButtonsDemo>("Taskbar Thumbnail Buttons", "Demo of taskbar thumbnail toolbar buttons (Play, Pause, Next, Stop) in the taskbar preview. Hover the taskbar button to see them.");
        CreateButton<TaskbarProgressBarDemo>("Taskbar Progress Bar Demo", "Comprehensive demo of KryptonProgressBar taskbar synchronisation (Issue #2890). Covers enable/disable toggle, simulated download, manual slider, all ProgressBarStyles, all KryptonTaskbarProgressState overrides (Normal/Error/Paused/Indeterminate/NoProgress), and Min/Max range.");
        CreateButton<ThemeControlExamples>("Theme Controls", string.Empty);
        CreateButton<TooltipTimeoutTest>("Tooltip Extended/Infinite Timeout", "Comprehensive demo of extended and infinite tooltip timeout (Issue #3075). Krypton tooltips support AutoPopDelay > 5000ms and 0 (infinite) on all Windows versions.");
        CreateButton<KryptonTextBoxValidatingTest>("TextBox Validating Test", "Tests fix for Validating event duplication bug #2801");
        CreateButton<TouchscreenHighDpiDemo>("Touchscreen + High DPI Demo", "Comprehensive demonstration of touchscreen support with per-monitor high DPI scaling (Issue #2844).");
        CreateButton<KryptonFormTitleBarDemo>("Title Bar Menu", "Demonstrates titlebar menu.");
        CreateButton<RichTextBoxFormattingTest>("RichTextBox Formatting Test", "Tests fix for RichTextBox formatting preservation when palette changes (Issue #2832)");
        CreateButton<RTLFormBorderTest>("RTL Layout Test", "Test for RTL compliance");
        CreateButton<ToastNotificationTestChoice>("Toast", "For breakfast....?");
        CreateButton<WorkspaceTest>("WorkspaceTest", string.Empty);
        CreateButton<BlurExampleForm>("Blur Example", string.Empty);
        CreateButton<VisualControlsTest>("Visual Controls", string.Empty);
        CreateButton<BasicEmojiViewerForm>("EmojiViewer Basic", string.Empty);
        CreateButton<AdvancedEmojiViewerForm>("EmojiViewer Advanced", "Only hardcore devs can handle this one!");
        CreateButton<BreadCrumbTest>("BreadCrumb", "Follow the breadcrumbs and find the treasure...");
        CreateButton<CalendarTest>("Calendar", string.Empty);
        CreateButton<ComboBoxDateTimePickerConsistencyDemo>("ComboBox/DateTimePicker Consistency", "Comprehensive demonstration of KComboBox and KDateTimePicker consistency fix (Issue #1651). Shows drop-down buttons stretching to full height and centered text.");
        CreateButton<DropDownArrowsDemo>("Drop-Down Arrows Demo", "Comprehensive demonstration of drop-down arrows: smaller size and DPI awareness (Issue #2129). Shows KryptonButton, KryptonDropButton, KryptonComboBox, KryptonDateTimePicker, KryptonColorButton, and KryptonNumericUpDown. Move window between monitors to verify DPI scaling.");
        CreateButton<ControlsTest>("Controls Test", string.Empty);
        CreateButton<DataGridViewDemo>("KryptonDataGridView Demo", string.Empty);
        CreateButton<BindingNavigatorDemo>("KryptonBindingNavigator Demo", "Comprehensive example of KryptonBindingNavigator with data binding");
        CreateButton<KryptonDialogExamples>("Krypton Dialog tests", "Tests the various types of dialogs.");
        CreateButton<FadeFormTest>("FadeForm", string.Empty);
        CreateButton<GroupBoxTest>("GroupBox", string.Empty);
        CreateButton<InputBoxTest>("InputBox", string.Empty);
        CreateButton<JumpListTest>("Jump List Test", "Comprehensive demonstration of jump lists on KryptonForm with user tasks, custom categories, known categories, and interactive examples.");
        CreateButton<MessageBoxTest>("MessageBox", string.Empty);
        CreateButton<Main>("Old Style Main: Fullscreen", string.Empty);
        CreateButton<ProgressBarTriStateTest>("ProgressBar Tri-State", string.Empty);
        CreateButton<OverlayImageTest>("Overlay Image Test", "Comprehensive demonstration of overlay images on KryptonButton and KryptonLabel with configurable positions and scaling modes.");
        CreateButton<PropertyGridTest>("PropertyGridTest", string.Empty);
        CreateButton<RibbonTest>("Ribbon", string.Empty);
        CreateButton<RibbonNotificationBarDemo>("Ribbon Notification Bar", "Comprehensive demonstration of the Krypton Ribbon Notification Bar feature with all customization options.");
        CreateButton<RibbonMergerDemo>("Ribbon Merger Demo", "Demonstrates UserControl hosting and ribbon merging for plugin architectures");
        CreateButton<RibbonDetachableTest>("Detachable Ribbons", "Demonstrates detachable ribbons feature - allows ribbon to be moved to a floating window (Issue #595)");
        CreateButton<TextBoxEventTest>("TextBox", string.Empty);
        CreateButton<TextSuggestionDemo>("TextSuggestion", string.Empty);
        CreateButton<TreeViewExample>("TreeView", string.Empty);
        CreateButton<TouchscreenSupportTest>("Touchscreen Support Test", "Comprehensive demonstration of touchscreen support with real-time scale factor adjustment.");
        CreateButton<ControlboxTouchscreenDemo>("Controlbox && Context Menu Touchscreen", "Demonstration of touchscreen support for controlbox buttons (minimize, maximize, close) and KryptonContextMenu items (Issue #2925).");
        CreateButton<TimerTest>("Timer", "Test KryptonTimer with interval configuration and event tracking.");
        CreateButton<PanelForm>("Panel Form", string.Empty);
        CreateButton<PaletteViewerForm>("Palette Viewer", string.Empty);
        CreateButton<PoweredByButtonExample>("Powered By Button", string.Empty);
        CreateButton<KryptonTaskDialogDemoForm>("Krypton Task Dialog Demo", string.Empty);
        CreateButton<MdiWindow>("Krypton MDI Window", "KryptonForm MDI Container with both KForm and WForm children");
        CreateButton<TabbedMdiDemo>("Tabbed MDI Demo (Issue #1746)", "KryptonTabbedMdiManager: MDI child windows displayed as tab pages instead of overlapping windows.");
        CreateButton<RibbonMdiDemo>("Ribbon MDI Demo (Issue #2921)", "Comprehensive demo for Issue #2921: Ribbon + MDI. Verifies no double ribbon tabs when opening/closing maximized MDI children; close/minimize/maximize and QAT click areas aligned with visuals.");
        CreateButton<Bug3203QATLocationHiddenFormTest>("Ribbon QATLocation=Hidden does not hide QAT when FormBorderStyle=None (Issue #3203)", string.Empty);
	}

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        SaveSettings();
    }

    private bool IsFormDockedTopRight()
    {
        return this.Top == 0 
            && this.Left == Screen.FromControl(this).Bounds.Width - this.Size.Width;
    }

    private void SaveSettings()
    {
        _registryAccess.LastFilterString = tbFilter.Text;
        _registryAccess.DockTopRight = IsFormDockedTopRight();
        _registryAccess.FormSize = this.Size;
    }

    private void RestoreSettings()
    {
        RestoreFormSize();
        RestoreLastFilter();
        RestoreFormLocation();
    }

    private void RestoreFormLocation()
    {
        _dockTopRight = _registryAccess.DockTopRight;
        if (_dockTopRight)
        {
            OnBtnDockTopRightClick(null, EventArgs.Empty);
        }
    }

    private void RestoreLastFilter()
    {
        string lastFilter = _registryAccess.LastFilterString;
        if (lastFilter.Length > 0)
        {
            tbFilter.Text = lastFilter;
        }
    }

    private void RestoreFormSize()
    {
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

        button.CommandLinkTextValues.Heading = heading;
        button.CommandLinkTextValues.Description = description;
        button.AutoSize = false;
        button.Size = new Size(_panelWidth - 10, 60);
        button.Click += (_, _) => OnCommandLinkTestButtonClick(formType);

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

                return string.Compare(headingX, headingY, StringComparison.Ordinal);
            }
            else
            {
                throw new NullReferenceException($"ButtonHeadingComparer: make sure that parameter x and y both are valid references to a KryptonCommandLinkButton instance.");
            }
        }
    }
}