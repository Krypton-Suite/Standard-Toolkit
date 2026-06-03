#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class StartScreen
{
    private void AddOptionalProjectButtons()
    {
        CreateButton<ButtonBadgeTest>("Badge Test", "Comprehensive badge functionality demonstration for KryptonButton and KryptonCheckButton.");
        CreateButton<ButtonTextTrackingExample>("Button Text Tracking", "Demonstrates alternate text color for tracking (hover) state on KryptonButton, KryptonCheckButton, KryptonColorButton and other controls (Issue #1326). Improves readability in dark themes.");
        CreateButton<KryptonColorButtonDemo>("KryptonColorButton Custom Colours", "Comprehensive demo of KryptonColorButton custom colours (Issue #776): CustomColors, MaxCustomColors, and visibility. Only 10 colours, or custom + theme + standard, or cap display count.");
        CreateButton<KryptonComboBoxUserControlDemo>("KryptonComboBoxUserControl", "Demo for Issue #3443: a ComboBox-style control whose drop-down hosts any UserControl. Shows tree-picker, grid-picker and a plain (non-contract) UserControl scenario.");
        CreateButton<KryptonTreeComboBoxDemo>("KryptonTreeComboBox", "Demo for Issue #3444: ComboBox-style control with a grouped tree drop-down (leaf/full path, breadcrumb, and parent-node selection).");
        CreateButton<KryptonCheckedListComboBoxDemo>("KryptonCheckedListComboBox", "Multi-select combo (#3445) with KryptonCheckedListBox drop-down: items + DataSource/DisplayMember/ValueMember demo and live summary.");
        CreateButton<BorderlessFormDemo>("Borderless Form Demo", "Demo for Issue #2922: Borderless KryptonForm without system title bar flicker on startup. Form should appear directly in borderless state.");
        CreateButton<Bug2914Test>("Bug 2914 Test", "Tests the fix for 2914.");
        CreateButton<Bug2984SeparatorTest>("Bug 2984 Separator Test", "Demo for Issue #2984: NullReferenceException in ViewDrawSeparator.RenderBefore. Exercises KryptonNavigator (Outlook), KryptonSplitContainer, and KryptonSeparator. Swap themes to verify no crash.");
        CreateButton<Bug3025KryptonLabelAutoSizeDemo>("Bug 3025 KryptonLabel AutoSize Demo", "Demo for Issue #3025: KryptonLabel with AutoSize now resizes to fit text when placed in the Designer (click-drag). Shows AutoSize on/off, LabelStyles, short/long text, and text + image.");
        CreateButton<Bug3342KryptonTextBoxResizeFlickerDemo>("Bug 3342 Multiline TextBox Flicker", "Demo for issue #3342: multiline KryptonTextBox text flicker while resizing. Includes manual resize steps and an automated stress-resize toggle.");
        CreateButton<Bug3382CueHintLinesDemo>("Bug 3382 CueHint line artifacts", "Demo for issue #3382: KryptonTextBox CueHint with TextH Near and mixed cue/content fonts — verify no stray top/left lines; cue remains vertically centered.");
        CreateButton<Bug3383KryptonButtonStateTrackingRoundingDemo>("Bug 3383 KryptonButton hover rounding vs OverrideFocus", "Demo for issue #3383: large StateCommon rounding with different StateTracking rounding and OverrideFocus rounding — Tab to focus, then hover (left repro vs right matched control). Corner fill and stroke should align after the palette merge fix.");
        CreateButton<Bug3451KryptonHeaderGroupPanelDemo>("Bug 3451 HeaderGroup panel parenting", "Demo for issue #3451: add child controls to KryptonHeaderGroup, KryptonGroup, and KryptonGroupBox via the internal Panel at design time. Open in the WinForms Designer and drag controls into each content area; verify no ReadOnly controls collection error.");
        CreateButton<KryptonToolTipTest>("KryptonToolTip", "Component wrapper (#3380): themed VisualPopupToolTip on arbitrary WinForms / Krypton controls via extender props or SetToolTip.");
        CreateButton<Bug3283ThemeComboBoxProgrammaticTest>("Bug 3283 ThemeComboBox programmatic", "Issue #3283: KryptonThemeComboBox must apply the global palette when SelectedIndex is set in code. Buttons cycle or jump the index; status lines show selection vs KryptonManager.CurrentGlobalPaletteMode. Optional: add a fresh combo with index set before its handle exists.");
        CreateButton<Bug2935MdiMultiMonitorDemo>("Bug 2935 MDI multi-monitor", "Demo for issue #2935: maximized MDI child form border drawn on the correct monitor. Move the MDI parent to a second monitor, open and maximize a child; the border should stay on the same monitor.");
        CreateButton<Bug3013TestForm>("Bug 3013 Test", "Tests the fix for 3013.");
        CreateButton<BugReportingDialogTest>("BugReportingTool", "Easily report bugs with this tool.");
        CreateButton<CodeEditorTest>("Code Editor", "Native code editor with syntax highlighting, line numbering, code folding, and auto-completion.");
        CreateButton<CountdownButtonTest>("Countdown Button", "Comprehensive demonstration of KryptonCountdownButton features with customizable duration, format, and enable-at-zero options.");
        CreateButton<KryptonCommandButtonSpecDemo>("KryptonCommand ButtonSpec", "Issue #1133: KryptonCommand.CommandType drives integrated toolbar and help ButtonSpecs with palette-aware images. Click toolbar and help buttons; change theme to verify refresh.");
        CreateButton<ControlStylesForm>("Control Styles", string.Empty);
        CreateButton<KryptonDateTimePickerMonthCalendarDemo>("DateTimePicker Month Calendar Background", "Comprehensive demo of KryptonDateTimePicker month calendar custom background (Issue #1827): CalendarBackColor, theme default, presets (dark/light), and pick-a-color to style the drop-down calendar.");
        CreateButton<DockingRedockDemo>("Docking Redock Demo", "Demo for Issue #2933: undock (Float) then redock; no floating window left behind.");
        CreateButton<FontAwesomeTest>("Font Awesome Test", string.Empty);
        CreateButton<FloatingToolbarsDemo>("Floating Toolbars Demo", "Comprehensive demonstration of KryptonFloatingToolbars features including drag-and-drop floating/docking, programmatic control, animation, window styles, docking preview indicators, custom themes, state persistence, and multi-monitor support.");
        CreateButton<FlowLayoutPanelTest>("FlowLayoutPanel", "Test KryptonFlowLayoutPanel with dynamic control layout and flow directions.");
        CreateButton<FileSystemWatcherTest>("FileSystemWatcher", "Monitor file system changes with Krypton integration.");
        CreateButton<ErrorProviderTest>("ErrorProvider", string.Empty);
        CreateButton<FileCheckSumDemo>("File checksum (Compute && Verify)", "Compute or verify file hashes (MD5, SHA-1, SHA-256, SHA-384, SHA-512, RIPEMD-160) using the Krypton checksum dialogs.");
        CreateButton<HelpProviderTest>("HelpProvider", "Test KryptonHelpProvider functionality");
        CreateButton<NotifyIconTest>("NotifyIcon", "Comprehensive demonstration of KryptonNotifyIcon with all events, balloon tips, and context menu support.");
        CreateButton<OAuth2Demo>("OAuth2 PKCE Demo", "Comprehensive OAuth2 with PKCE demo. Sign in with Azure AD, Google, or GitHub using embedded WebView2 or system browser. Configure client ID, redirect URI, and scopes.");
        CreateButton<QRCodeDemo>("QR Code (KryptonQRCode)", "Native QR code generation without external packages: live preview, ECC levels, module size, colors, quiet zone, Save PNG, clipboard, and static GenerateBitmap API.");
        CreateButton<ScrollBarTest>("ScrollBar", "Comprehensive demonstration of KryptonHScrollBar and KryptonVScrollBar controls with basic usage, scrolling content, synchronization, theming, programmatic control, and event logging.");
        CreateButton<ScrollbarManagerTest>("Scrollbar Manager", "Comprehensive demonstration of KryptonScrollbarManager with container mode, native wrapper mode, dynamic content, and integration examples.");
        CreateButton<RTLControlsTest>("RTL Compliance Tests", "Test the Krypton.Toolkit controls for compliance.");
        CreateButton<TaskbarOverlayIconTest>("Taskbar Overlay Icon Test", "Comprehensive demonstration of taskbar overlay icons on KryptonForm with configurable icons, descriptions, and interactive examples.");
        CreateButton<TaskbarThumbnailButtonsDemo>("Taskbar Thumbnail Buttons", "Demo of taskbar thumbnail toolbar buttons (Play, Pause, Next, Stop) in the taskbar preview. Hover the taskbar button to see them.");
        CreateButton<TaskbarProgressBarDemo>("Taskbar Progress Bar Demo", "Comprehensive demo of KryptonProgressBar taskbar synchronisation (Issue #2890). Covers enable/disable toggle, simulated download, manual slider, all ProgressBarStyles, all KryptonTaskbarProgressState overrides (Normal/Error/Paused/Indeterminate/NoProgress), and Min/Max range.");
        CreateButton<TooltipTimeoutTest>("Tooltip Extended/Infinite Timeout", "Comprehensive demo of extended and infinite tooltip timeout (Issue #3075). Krypton tooltips support AutoPopDelay > 5000ms and 0 (infinite) on all Windows versions.");
        CreateButton<TouchscreenHighDpiDemo>("Touchscreen + High DPI Demo", "Comprehensive demonstration of touchscreen support with per-monitor high DPI scaling (Issue #2844).");
        CreateButton<KryptonFormTitleBarDemo>("Title Bar Menu", "Demonstrates titlebar menu.");
        CreateButton<Bug3343RichTextBoxEditLossDemo>("Bug 3343 RichTextBox mouse leave", "Issue #3343: type in KryptonRichTextBox, move the mouse out without changing focus; text and TextLength must not reset. Includes KryptonTextBox for comparison.");
        CreateButton<RTLFormBorderTest>("RTL Layout Test", "Test for RTL compliance");
        CreateButton<WorkspaceTest>("WorkspaceTest", string.Empty);
        CreateButton<DropDownArrowsDemo>("Drop-Down Arrows Demo", "Comprehensive demonstration of drop-down arrows: smaller size and DPI awareness (Issue #2129). Shows KryptonButton, KryptonDropButton, KryptonComboBox, KryptonDateTimePicker, KryptonColorButton, and KryptonNumericUpDown. Move window between monitors to verify DPI scaling.");
        CreateButton<BindingNavigatorDemo>("KryptonBindingNavigator Demo", "Comprehensive example of KryptonBindingNavigator with data binding");
        CreateButton<JumpListTest>("Jump List Test", "Comprehensive demonstration of jump lists on KryptonForm with user tasks, custom categories, known categories, and interactive examples.");
        CreateButton<ProgressBarTriStateTest>("ProgressBar Tri-State", string.Empty);
        CreateButton<OverlayImageTest>("Overlay Image Test", "Comprehensive demonstration of overlay images on KryptonButton and KryptonLabel with configurable positions and scaling modes.");
        CreateButton<RibbonNotificationBarDemo>("Ribbon Notification Bar", "Comprehensive demonstration of the Krypton Ribbon Notification Bar feature with all customization options.");
        CreateButton<RibbonMergerDemo>("Ribbon Merger Demo", "Demonstrates UserControl hosting and ribbon merging for plugin architectures");
        CreateButton<RibbonDetachableTest>("Detachable Ribbons", "Demonstrates detachable ribbons feature - allows ribbon to be moved to a floating window (Issue #595)");
        CreateButton<TextSuggestionDemo>("TextSuggestion", string.Empty);
        CreateButton<TouchscreenSupportTest>("Touchscreen Support Test", "Comprehensive demonstration of touchscreen support with real-time scale factor adjustment.");
        CreateButton<ControlboxTouchscreenDemo>("Controlbox && Context Menu Touchscreen", "Demonstration of touchscreen support for controlbox buttons (minimize, maximize, close) and KryptonContextMenu items (Issue #2925).");
        CreateButton<TimerTest>("Timer", "Test KryptonTimer with interval configuration and event tracking.");
        CreateButton<TabbedMdiDemo>("Tabbed MDI Demo (Issue #1746)", "KryptonTabbedMdiManager: MDI child windows displayed as tab pages instead of overlapping windows.");
        CreateButton<RibbonMdiDemo>("Ribbon MDI Demo (Issue #2921)", "Comprehensive demo for Issue #2921: Ribbon + MDI. Verifies no double ribbon tabs when opening/closing maximized MDI children; close/minimize/maximize and QAT click areas aligned with visuals.");
        CreateButton<Bug3203QATLocationHiddenFormTest>("Ribbon QATLocation=Hidden does not hide QAT when FormBorderStyle=None (Issue #3203)", string.Empty);
        CreateButton<Bug3183SmallSquareRenderedNextToClose>("Small Square Rendered Next to Close Button (Issue #3183)", string.Empty);
    }
}
