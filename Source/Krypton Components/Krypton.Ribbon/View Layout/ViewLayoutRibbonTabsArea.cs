#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Ribbon area that contains the tabs and pendant.
/// </summary>
internal class ViewLayoutRibbonTabsArea : ViewLayoutDocker
{
    #region Type Definitions
    /// <summary>
    /// Collection for managing ButtonSpec fixed instances.
    /// </summary>
    public class RibbonButtonSpecFixedCollection : ButtonSpecCollection<ButtonSpec>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonButtonSpecFixedCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public RibbonButtonSpecFixedCollection(KryptonRibbon owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Static Fields
    private static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);
    private const int BUTTON_TAB_GAP_2007 = 5;
    private const int BUTTON_TAB_GAP_2010 = 0;  //TODO dpi 12 ? 
    private const int FAR_TAB_GAP = 1;
    private const int SCROLL_SPEED = 12;

    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private ViewLayoutRibbonScrollPort _tabsViewport;
    private ViewLayoutSeparator _layoutAppButtonSep;
    private ViewLayoutRibbonSeparator _leftSeparator;
    private ViewLayoutRibbonSeparator _rightSeparator;
    private readonly ViewDrawRibbonCaptionArea _captionArea;
    private readonly ViewLayoutRibbonContextTitles _layoutContexts;
    private readonly AppButtonController? _appButtonController;
    private readonly AppTabController? _appTabController;
    private VisualPopupToolTip? _visualPopupToolTip;
    private VisualPopupAppMenu? _appMenu;
    private DateTime _lastAppButtonClick;

    // Fixed button specifications
    private RibbonButtonSpecFixedCollection _buttonSpecsFixed;
    private ButtonSpecMdiChildClose _buttonSpecClose;
    private ButtonSpecMdiChildRestore _buttonSpecRestore;
    private ButtonSpecMdiChildMin _buttonSpecMin;
    private ButtonSpecMinimizeRibbon _buttonSpecMinimize;
    private ButtonSpecExpandRibbon _buttonSpecExpand;

    // Monitoring the containing form and mdi status
    private Timer _invalidateTimer;
    private Form? _formContainer;
    private Form? _activeMdiChild;
    private int _paintCount;
    private bool _setVisible;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the background needs painting.
    /// </summary>
    public event PaintEventHandler? PaintBackground;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonTabsArea class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="redirect">Reference to redirector for palette settings.</param>
    /// <param name="captionArea">Reference to the caption area.</param>
    /// <param name="layoutContexts">Reference to layout of the context area.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
    public ViewLayoutRibbonTabsArea([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] PaletteRedirect redirect,
        [DisallowNull] ViewDrawRibbonCaptionArea captionArea,
        [DisallowNull] ViewLayoutRibbonContextTitles layoutContexts,
        [DisallowNull] NeedPaintHandler needPaintDelegate)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(redirect != null);
        Debug.Assert(layoutContexts != null);
        Debug.Assert(captionArea != null);
        Debug.Assert(needPaintDelegate != null);

        // Remember incoming references
        _ribbon = ribbon!;
        _captionArea = captionArea!;
        _appButtonController = _captionArea.AppButtonController;
        _appTabController = _captionArea.AppTabController;
        _layoutContexts = layoutContexts!;
        NeedPaintDelegate = needPaintDelegate!;

        // Default other state
        _setVisible = true;
        _lastAppButtonClick = DateTime.MinValue;

        CreateController();
        CreateButtonSpecs();
        CreateViewElements(redirect!);
        SetupParentMonitoring();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Remove any showing tooltip
            OnCancelToolTip(this, EventArgs.Empty);

            // Unhook from the parent control
            _ribbon.ParentChanged += OnRibbonParentChanged;

            // Unhook from watching any top level window
            if (_formContainer != null)
            {
                _formContainer.Deactivate -= OnRibbonFormDeactivate;
                _formContainer.Activated -= OnRibbonFormActivated;
                _formContainer.SizeChanged -= OnRibbonFormSizeChanged;
                _formContainer.MdiChildActivate -= OnRibbonMdiChildActivate;
                _formContainer = null;
            }

            // Unhook from watching any mdi child window
            if (_activeMdiChild != null)
            {
                _activeMdiChild.SizeChanged -= OnRibbonMdiChildSizeChanged;
                _activeMdiChild = null;
            }

            // Destruct the button manager resources
            if (ButtonSpecManager != null)
            {
                ButtonSpecManager.Destruct();
                ButtonSpecManager = null;
            }
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonTabsArea:{Id}";

    #endregion

    #region HookToolTipHandling
    /// <summary>
    /// Perform steps to generate a tooltip event when mouse is over the application button.
    /// </summary>
    public void HookToolTipHandling()
    {
        LayoutAppButton.MouseController = new ToolTipController(_ribbon.TabsArea!.ButtonSpecManager?.ToolTipManager!, LayoutAppButton, _appButtonController);
        LayoutAppTab.MouseController = new ToolTipController(_ribbon.TabsArea?.ButtonSpecManager!.ToolTipManager!, LayoutAppTab, _appTabController);
    }
    #endregion

    #region CheckRibbonSize
    /// <summary>
    /// Check if the ribbon should be visible or hidden at its current size.
    /// </summary>
    public void CheckRibbonSize()
    {
        // Just in case we have already been disposed
        if (!_ribbon.IsDisposed)
        {
            // Find the top most form we are inside
            Form? topForm = _ribbon.FindForm();

            // Just in case we have not been created as yet
            if (topForm != null)
            {
                // Ignore the minimized state, as that would always cause the ribbon to be hidden
                // when then causes it to be reshown when the window is restored. This creates a 
                // flicker after the restore so prevent it from disappearing in the first case.
                if (!CommonHelper.IsFormMinimized(topForm))
                {
                    var show = (topForm.ClientSize.Width >= _ribbon.HideRibbonSize.Width) &&
                               (topForm.ClientSize.Height >= _ribbon.HideRibbonSize.Height);

                    // How we handle visibility differs in OS
                    if (Environment.OSVersion.Version.Major >= 6)
                    {
                        if (_ribbon.MainPanel.Visible != show)
                        {
                            _ribbon.MainPanel.Visible = show;
                            _captionArea.PreventIntegration = !show;
                        }
                    }
                    else
                    {
                        if ((_ribbon.Visible != show) ||
                            (_setVisible != show))
                        {
                            _ribbon.Visible = show;
                            _setVisible = show;

                            // If using custom chrome
                            if (_captionArea.UsingCustomChrome)
                            {
                                _paintCount = _captionArea.KryptonForm!.PaintCount;
                                _invalidateTimer.Start();
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region AppButtonVisibleChanged
    /// <summary>
    /// A change in the app button visibility needs to be processed.
    /// </summary>
    public void AppButtonVisibleChanged()
    {
        // Update visible state of the app button/tab to reflect current state
        LayoutAppButton.Visible = _ribbon.RibbonFileAppButton.AppButtonVisible && (_ribbon.RibbonShape == PaletteRibbonShape.Office2007);
        LayoutAppTab.Visible = _ribbon.RibbonFileAppButton.AppButtonVisible && (_ribbon.RibbonShape != PaletteRibbonShape.Office2007);
        _leftSeparator.SeparatorSize = (_ribbon.RibbonShape == PaletteRibbonShape.Office2007) ? new Size(BUTTON_TAB_GAP_2007, BUTTON_TAB_GAP_2007) : new Size(BUTTON_TAB_GAP_2010, BUTTON_TAB_GAP_2010);

        // If no app button then need separator to stop first tab being to close to the left edge
        _layoutAppButtonSep.Visible = !LayoutAppButton.Visible;
        ViewDrawRibbonCaptionArea? viewDrawRibbonCaptionArea = _ribbon.CaptionArea;
        if (viewDrawRibbonCaptionArea?.KryptonForm != null)
        {
            if (viewDrawRibbonCaptionArea.KryptonForm.CloseBox != _ribbon.RibbonFileAppButton.FormCloseBoxVisible)
            {
                viewDrawRibbonCaptionArea.KryptonForm.CloseBox = _ribbon.RibbonFileAppButton.FormCloseBoxVisible;
                viewDrawRibbonCaptionArea.PerformFormChromeCheck();
            }
        }
    }
    #endregion

    #region LayoutTabs
    /// <summary>
    /// Gets access to the view layout used for the individual ribbon tabs.
    /// </summary>
    public ViewLayoutRibbonTabs LayoutTabs { get; private set; }

    #endregion

    #region LayoutAppButton
    /// <summary>
    /// Gets access to the view layout used for the application button.
    /// </summary>
    public ViewLayoutRibbonAppButton LayoutAppButton { get; private set; }

    #endregion

    #region LayoutAppTab
    /// <summary>
    /// Gets access to the view layout used for the application tab.
    /// </summary>
    public ViewLayoutRibbonAppTab LayoutAppTab { get; private set; }

    #endregion

    #region TabsContainerControl
    /// <summary>
    /// Gets access to the control that contains the tabs.
    /// </summary>
    public ViewLayoutControl TabsContainerControl => _tabsViewport.ViewLayoutControl;

    #endregion

    #region GetAppButtonKeyTip
    public KeyTipInfo GetAppButtonKeyTip()
    {
        // Get the screen location of the bottom half of the button
        Rectangle buttonRect = _ribbon.RectangleToScreen(LayoutAppButton.ClientRectangle);

        // Alter the rect to account for it not being exactly half the button height
        buttonRect.Y -= 5;
        buttonRect.X += 2;

        // The keytip should be centered on the top center of the bottom half
        var screenPt = new Point(buttonRect.Left + (buttonRect.Width / 2), buttonRect.Top);

        // Return key tip details
        return new KeyTipInfo(true, KryptonManager.Strings.RibbonStrings.AppButtonKeyTip, screenPt,
            LayoutAppButton.ClientRectangle, _appButtonController);

    }
    #endregion

    #region GetAppTabKeyTip
    public KeyTipInfo GetAppTabKeyTip()
    {
        // Get the screen location of the bottom half of the button
        Rectangle buttonRect = _ribbon.RectangleToScreen(LayoutAppTab.ClientRectangle);

        // The keytip should be centered on the top center of the bottom half
        var screenPt = new Point(buttonRect.Left + (buttonRect.Width / 2), buttonRect.Bottom + 2);

        // Return key tip details
        return new KeyTipInfo(true, KryptonManager.Strings.RibbonStrings.AppButtonKeyTip, screenPt,
            LayoutAppTab.ClientRectangle, _appTabController);

    }
    #endregion

    #region GetGroupKeyTips
    /// <summary>
    /// Gets the array of group level key tips.
    /// </summary>
    /// <returns>Array of KeyTipInfo; otherwise null.</returns>
    public KeyTipInfo[] GetTabKeyTips()
    {
        var keyTips = new KeyTipInfoList();

        // Grab the list of key tips for all tab headers
        keyTips.AddRange(LayoutTabs.GetTabKeyTips());

        // Remove all those that do not intercept the scroll port the tabs are inside
        var scrollRect = new Rectangle(Point.Empty, _tabsViewport.ClientSize);
        foreach (KeyTipInfo ti in keyTips.Where(ti => !scrollRect.Contains(ti.ClientRect)))
        {
            ti.Visible = false;
        }

        return keyTips.ToArray();
    }
    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Let base class perform standard layout
        base.Layout(context);

        // Ask the context titles to layout again to take into account any
        // change in the positions and sizes of the actual tabs that it mimics
        Rectangle temp = context.DisplayRectangle;
        context.DisplayRectangle = _layoutContexts.ClientRectangle;
        _layoutContexts.Layout(context);
        context.DisplayRectangle = temp;

        // If using custom chrome 
        if (_captionArea is { UsingCustomChrome: true })
        {
            _paintCount = _captionArea.KryptonForm!.PaintCount;
            _invalidateTimer.Start();
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Test if there has been a double click of the app button.
    /// </summary>
    /// <returns>True if a double click was detected and pressed.</returns>
    public void TestForAppButtonDoubleClick()
    {
        // Office 2010 does not close on a double click
        if (_ribbon.IgnoreDoubleClickClose
            || _ribbon.RibbonShape == PaletteRibbonShape.Office2010
           )
        {
            return;
        }

        DateTime nowClick = DateTime.Now;
        TimeSpan elapsed = nowClick - _lastAppButtonClick;
        _lastAppButtonClick = nowClick;

        // If this is the second click within the double click time...
        if (elapsed.TotalMilliseconds < SystemInformation.DoubleClickTime)
        {
            // Close down the associated application window
            Form? ownerForm = _ribbon.FindForm();
            ownerForm?.Close();
        }
    }

    /// <summary>
    /// Gets access to the tool tip manager.
    /// </summary>
    public ToolTipManager ToolTipManager { get; private set; }

    /// <summary>
    /// Gets the button specification manager.
    /// </summary>
    public ButtonSpecManagerLayoutRibbon? ButtonSpecManager { get; private set; }

    /// <summary>
    /// Recreate the button specifications.
    /// </summary>
    public void RecreateButtons() => ButtonSpecManager?.RecreateButtons();

    #endregion

    #region Protected
    /// <summary>
    /// Gets access to the need paint delegate.
    /// </summary>
    protected NeedPaintHandler NeedPaintDelegate
    {
        [DebuggerStepThrough]
        get;
    }

    /// <summary>
    /// Fires a request to have painting/layout performed.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected void PerformNeedPaint(bool needLayout) => NeedPaintDelegate(this, new NeedLayoutEventArgs(needLayout));

    #endregion

    #region Implementation
    private void CreateController()
    {
        // Use a controller to initiate context menu when using right mouse click
        var controller = new RibbonTabsController(_ribbon);
        controller.ContextClick += OnContextClicked;
        MouseController = controller;
    }

    private void CreateButtonSpecs()
    {
        // Create fixed button specifications
        _buttonSpecsFixed = new RibbonButtonSpecFixedCollection(_ribbon);
        _buttonSpecClose = new ButtonSpecMdiChildClose(_ribbon);
        _buttonSpecRestore = new ButtonSpecMdiChildRestore(_ribbon);
        _buttonSpecMin = new ButtonSpecMdiChildMin(_ribbon);
        _buttonSpecMinimize = new ButtonSpecMinimizeRibbon(_ribbon);
        _buttonSpecExpand = new ButtonSpecExpandRibbon(_ribbon);
        _buttonSpecsFixed.AddRange(new ButtonSpec[] { _buttonSpecMinimize, _buttonSpecExpand, _buttonSpecMin, _buttonSpecRestore, _buttonSpecClose });
    }

    private void CreateViewElements(PaletteRedirect redirect)
    {
        // Layout for individual tabs inside the header
        LayoutTabs = new ViewLayoutRibbonTabs(_ribbon, NeedPaintDelegate);

        // Put inside a viewport so scrollers are used when tabs cannot be shrunk to fill space
        _tabsViewport = new ViewLayoutRibbonScrollPort(_ribbon, System.Windows.Forms.Orientation.Horizontal, LayoutTabs, true, SCROLL_SPEED, NeedPaintDelegate)
        {
            TransparentBackground = true
        };
        _tabsViewport.PaintBackground += OnTabsPaintBackground;
        LayoutTabs.ParentControl = _tabsViewport.ViewLayoutControl.ChildControl;
        LayoutTabs.NeedPaintDelegate = _tabsViewport.ViewControlPaintDelegate;

        // We use a layout docker as a child to prevent buttons going to the left of the app button
        var tabsDocker = new ViewLayoutDocker
        {

            // Place the tabs viewport as the fill inside ourself, the button specs will be placed 
            // to the left and right of this fill element automatically by the button manager below
            { _tabsViewport, ViewDockStyle.Fill }
        };

        // We need to draw the bottom half of the application button or a full app tab
        LayoutAppButton = new ViewLayoutRibbonAppButton(_ribbon, true);
        LayoutAppTab = new ViewLayoutRibbonAppTab(_ribbon);

        // Connect up the application button controller to the app button element
        if (_appButtonController != null)
        {
            _appButtonController.Target3 = LayoutAppButton.AppButton;
            _appButtonController.Click += OnAppButtonClicked;
            _appButtonController.MouseReleased += OnAppButtonReleased;
            LayoutAppButton.MouseController = _appButtonController;
            LayoutAppButton.SourceController = _appButtonController;
            LayoutAppButton.KeyController = _appButtonController;
        }

        if (_appTabController != null)
        {
            _appTabController.Target1 = LayoutAppTab.AppTab;
            _appTabController.Click += OnAppButtonClicked;
            _appTabController.MouseReleased += OnAppButtonReleased;
            LayoutAppTab.MouseController = _appTabController;
            LayoutAppTab.SourceController = _appTabController;
            LayoutAppTab.KeyController = _appTabController;
        }

        // When the app button is not visible we need separator instead before start of first tab
        _layoutAppButtonSep = new ViewLayoutSeparator(5, 0)
        {
            Visible = false
        };

        // Used separators around the tabs and the edge elements
        _rightSeparator = new ViewLayoutRibbonSeparator(FAR_TAB_GAP, true);
        _leftSeparator = new ViewLayoutRibbonSeparator(BUTTON_TAB_GAP_2007, true);

        // Place application button on left  and tabs as the filler (with some separators for neatness)
        Add(_rightSeparator, ViewDockStyle.Left);
        Add(_leftSeparator, ViewDockStyle.Left);
        Add(LayoutAppButton, ViewDockStyle.Left);
        Add(_layoutAppButtonSep, ViewDockStyle.Left);
        Add(LayoutAppTab, ViewDockStyle.Left);
        Add(tabsDocker, ViewDockStyle.Fill);

        // Create button specification collection manager
        PaletteRedirect aeroOverrideText = new PaletteRedirectRibbonAeroOverride(_ribbon, redirect);
        ButtonSpecManager = new ButtonSpecManagerLayoutRibbon(_ribbon, aeroOverrideText, _ribbon.ButtonSpecs, _buttonSpecsFixed,
            [tabsDocker],
            [_ribbon.StateCommon],
            [PaletteMetricInt.HeaderButtonEdgeInsetPrimary],
            [PaletteMetricPadding.RibbonButtonPadding],
            _ribbon.CreateToolStripRenderer,
            NeedPaintDelegate);

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(new ToolTipValues(null, GetDpiFactor)); // use default, as each button "could" have different values ??!!??
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
        ButtonSpecManager.ToolTipManager = ToolTipManager;
    }

    private float GetDpiFactor() =>
        (_visualPopupToolTip != null)
            ? _visualPopupToolTip.DeviceDpi / 96F
            : 1F;

    private void SetupParentMonitoring()
    {
        // We have to know when the parent of the ribbon changes, so we can then hook
        // into monitoring the mdi active child status of the top level form, this is
        // required to get the pendant buttons to operate as needed.
        _ribbon.ParentChanged += OnRibbonParentChanged;

        _invalidateTimer = new Timer
        {
            Interval = 1
        };
        _invalidateTimer.Tick += OnRedrawTick;
    }

    private void OnRibbonParentChanged(object? sender, EventArgs e)
    {
        // Unhook from watching any top level window
        if (_formContainer != null)
        {
            _formContainer.Deactivate -= OnRibbonFormDeactivate;
            _formContainer.Activated -= OnRibbonFormActivated;
            _formContainer.SizeChanged -= OnRibbonFormSizeChanged;
            _formContainer.MdiChildActivate -= OnRibbonMdiChildActivate;
        }

        // Find the new top level form (which might be a mdi container)
        _formContainer = _ribbon.FindForm();

        // Monitor changes in active mdi child
        if (_formContainer != null)
        {
            _formContainer.MdiChildActivate += OnRibbonMdiChildActivate;
            _formContainer.SizeChanged += OnRibbonFormSizeChanged;
            _formContainer.Activated += OnRibbonFormActivated;
            _formContainer.Deactivate += OnRibbonFormDeactivate;
            _ribbon.UpdateBackStyle();
        }
    }

    private void OnRibbonFormActivated(object? sender, EventArgs e)
    {
        _ribbon.ViewRibbonManager?.Active();
        _ribbon.UpdateBackStyle();
    }

    private void OnRibbonFormDeactivate(object? sender, EventArgs e)
    {
        _ribbon.ViewRibbonManager?.Inactive();
        _ribbon.UpdateBackStyle();
    }

    private void OnRibbonFormSizeChanged(object? sender, EventArgs e) => CheckRibbonSize();

    private void OnRibbonMdiChildActivate(object? sender, EventArgs e)
    {
        // Cast to correct type
        var topForm = sender as Form;

        // Unhook from watching any previous mdi child
        if (_activeMdiChild != null)
        {
            _activeMdiChild.SizeChanged -= OnRibbonMdiChildSizeChanged;
        }

        _activeMdiChild = topForm?.ActiveMdiChild;

        // Start watching any new mdi child
        if (_activeMdiChild != null)
        {
            _activeMdiChild.SizeChanged += OnRibbonMdiChildSizeChanged;
        }

        // Update the pendant buttons with reference to new child
        _buttonSpecClose.MdiChild = _activeMdiChild;
        _buttonSpecRestore.MdiChild = _activeMdiChild;
        _buttonSpecMin.MdiChild = _activeMdiChild;
        ButtonSpecManager?.RecreateButtons();
        PerformNeedPaint(true);

        // We never want the mdi child window to have a system menu, we provide the 
        // pendant buttons as part of the ribbon and so replace the need for it.
        PI.SetMenu(new HandleRef(_ribbon, topForm!.Handle), NullHandleRef);

        if (_activeMdiChild != null)
        {
            var windowStyle = PI.GetWindowLong(_activeMdiChild.Handle, PI.GWL_.STYLE);
            windowStyle |= PI.WS_.SYSMENU;
            PI.SetWindowLong(_activeMdiChild.Handle, PI.GWL_.STYLE, windowStyle);
        }
    }

    private void OnRibbonMdiChildSizeChanged(object? sender, EventArgs e)
    {
        // Update pendant buttons to reflect new child state
        ButtonSpecManager?.RecreateButtons();
        PerformNeedPaint(true);
    }

    private void OnRedrawTick(object? sender, EventArgs e)
    {
        _invalidateTimer.Stop();

        if (_captionArea is { KryptonForm: not null, UsingCustomChrome: true })
        {
            if (_captionArea.KryptonForm.PaintCount == _paintCount)
            {
                _captionArea.RedrawCustomChrome(true);
            }
        }
    }

    private void OnAppButtonReleased(object? sender, EventArgs e)
    {
        // We do not operate the application button at design time
        if (!_ribbon.InDesignMode)
        {
            TestForAppButtonDoubleClick();
        }
    }

    private void OnAppButtonClicked(object? sender, EventArgs e)
    {
        // We do not operate the application button at design time
        if (_ribbon.InDesignMode)
        {
            OnAppMenuDisposed(this, EventArgs.Empty);
        }
        else
        {
            // Give event handler a change to cancel the open request
            var cea = new CancelEventArgs();
            _ribbon.OnAppButtonMenuOpening(cea);

            if (cea.Cancel)
            {
                OnAppMenuDisposed(this, EventArgs.Empty);
            }
            else
            {
                // Remove any minimized popup window from display
                if (_ribbon.RealMinimizedMode)
                {
                    _ribbon.KillMinimizedPopup();
                }

                // Give popups a change to cleanup
                Application.DoEvents();

                if (_ribbon is { InDesignMode: false, IsDisposed: false })
                {
                    Rectangle appRectTop;
                    Rectangle appRectBottom;
                    Rectangle appRectShow;

                    if (_ribbon.RibbonShape == PaletteRibbonShape.Office2007)
                    {
                        // Find screen location of the application button lower half
                        Rectangle appButtonRect = _ribbon.RectangleToScreen(LayoutAppButton.AppButton.ClientRectangle);
                        var localHalf = (int)(21 * FactorDpiY);
                        appRectBottom = appButtonRect with { Y = appButtonRect.Y + localHalf, Height = appButtonRect.Height - localHalf };
                        appRectTop = appRectBottom with { Y = appRectBottom.Y - localHalf, Height = localHalf };
                        appRectShow = appRectBottom;
                    }
                    else
                    {
                        // Find screen location of the application tab lower half
                        Rectangle appButtonRect = _ribbon.RectangleToScreen(LayoutAppTab.AppTab.ClientRectangle);
                        appRectBottom = Rectangle.Empty;
                        appRectTop = appButtonRect;
                        appRectShow = appButtonRect with { Y = appButtonRect.Bottom - 1, Height = 0 };
                    }

                    // Create the actual control used to show the context menu
                    _appMenu = new VisualPopupAppMenu(_ribbon,
                        _ribbon.LocalCustomPalette, _ribbon.PaletteMode,
                        _ribbon.GetRedirector(),
                        appRectTop, appRectBottom,
                        _appButtonController!.Keyboard);

                    // Need to know when the visual control is removed
                    _appMenu.Disposed += OnAppMenuDisposed;

                    // Adjust the screen rect of the app button/tab, so we show half-way down the button
                    appRectShow.X -= 3;
                    appRectShow.Height = 0;

                    // Request the menu be shown immediately
                    _appMenu.Show(appRectShow);

                    // Indicate the context menu is fully constructed and displayed
                    _ribbon.OnAppButtonMenuOpened(EventArgs.Empty);
                }
            }
        }
    }

    private void OnAppMenuDisposed(object? sender, EventArgs e)
    {
        // We always kill keyboard mode when the app button menu is removed
        _ribbon.KillKeyboardMode();

        // Remove the fixed 'pressed' state from the application button
        _appButtonController?.RemoveFixed();

        _appTabController?.RemoveFixed();

        // Should still be caching a reference to actual display control
        if (_appMenu != null)
        {
            // Unhook from control, so it can be garbage collected
            _appMenu.Disposed -= OnAppMenuDisposed;

            // Discover the reason for the menu close
            var closeReason = ToolStripDropDownCloseReason.AppFocusChange;
            if (_appMenu.CloseReason.HasValue)
            {
                closeReason = _appMenu.CloseReason.Value;
            }

            // No longer need to cache reference
            _appMenu = null;

            // Notify event handlers the context menu has been closed and why it closed
            _ribbon.OnAppButtonMenuClosed(new ToolStripDropDownClosedEventArgs(closeReason));
        }
    }

    private void OnContextClicked(object? sender, MouseEventArgs e)
    {
        if (!_ribbon.InDesignMode)
        {
            _ribbon.DisplayRibbonContextMenu(e);
        }
    }

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!_ribbon.IsDisposed)
        {
            // Do not show tooltips when the form we are in does not have focus
            Form? topForm = _ribbon.FindForm();
            if (topForm is { ContainsFocus: false })
            {
                return;
            }

            // Never show tooltips are design time
            if (!_ribbon.InDesignMode)
            {
                IContentValues? sourceContent = null;
                var toolTipStyle = LabelStyle.SuperTip;
                var shadow = true;
                var screenRect = new Rectangle(e.ControlMousePosition, new Size(1, 1));

                // If the target is the application button
                switch (e.Target)
                {
                    case ViewLayoutRibbonAppButton:
                    case ViewLayoutRibbonAppTab:
                    {
                        // Create a content that recovers values from the ribbon for the app button/tab
                        var appButtonContent = new AppButtonToolTipToContent(_ribbon);

                        // Is there actually anything to show for the tooltip
                        if (appButtonContent.HasContent)
                        {
                            sourceContent = appButtonContent;

                            // Grab the style from the app button settings
                            toolTipStyle = _ribbon.RibbonFileAppButton.AppButtonToolTipStyle;
                            shadow = _ribbon.RibbonFileAppButton.ToolTipShadow;

                            // Display below the mouse cursor
                            screenRect.Height += SystemInformation.CursorSize.Height / 3 * 2;
                        }
                    }
                        break;
                    case ViewDrawRibbonQATButton viewElement1:
                    {
                        // If the target is a QAT button
                        // Cast to correct type

                        // Create a content that recovers values from a IQuickAccessToolbarButton
                        var qatButtonContent = new QATButtonToolTipToContent(viewElement1.QATButton);

                        // Is there actually anything to show for the tooltip
                        if (qatButtonContent.HasContent)
                        {
                            sourceContent = qatButtonContent;

                            // Grab the style from the QAT button settings
                            toolTipStyle = viewElement1.QATButton.GetToolTipStyle();
                            shadow = viewElement1.QATButton.GetToolTipShadow();

                            // Display below the mouse cursor
                            screenRect.Height += SystemInformation.CursorSize.Height / 3 * 2;
                        }
                    }
                        break;
                    default:
                    {
                        // Find the button spec associated with the tooltip request
                        ButtonSpec? buttonSpec = ButtonSpecManager?.ButtonSpecFromView(e.Target);

                        // If the tooltip is for a button spec
                        if (buttonSpec != null)
                        {
                            // Are we allowed to show page related tooltips
                            if (_ribbon.AllowButtonSpecToolTips)
                            {
                                // Create a helper object to provide tooltip values
                                var buttonSpecMapping = new ButtonSpecToContent(_ribbon.GetRedirector(), buttonSpec);

                                // Is there actually anything to show for the tooltip
                                if (buttonSpecMapping.HasContent)
                                {
                                    sourceContent = buttonSpecMapping;

                                    // Grab the style from the button spec settings
                                    toolTipStyle = buttonSpec.ToolTipStyle;
                                    shadow = buttonSpec.ToolTipShadow;

                                    // Display below the mouse cursor
                                    screenRect.Height += SystemInformation.CursorSize.Height / 3 * 2;
                                }
                            }
                        }
                        else
                        {
                            // Cast to correct type
                            if (e.Target.Parent is { Component: IRibbonGroupItem { ToolTipValues.EnableToolTips: true } groupItem })
                                // Is there actually anything to show for the tooltip
                            {
                                sourceContent = groupItem.ToolTipValues;

                                // Grab the style from the group radio button settings
                                toolTipStyle = groupItem.ToolTipValues.ToolTipStyle;
                                shadow = groupItem.ToolTipValues.ToolTipShadow;

                                // Display below the bottom of the ribbon control
                                Rectangle ribbonScreenRect = _ribbon.ToolTipScreenRectangle;
                                screenRect.Y = ribbonScreenRect.Y;
                                screenRect.Height = ribbonScreenRect.Height;
                                screenRect.X = ribbonScreenRect.X + e.Target.Parent.ClientLocation.X;
                                screenRect.Width = e.Target.Parent.ClientWidth;
                            }
                        }

                    }
                        break;
                }

                if (sourceContent != null)
                {
                    // Remove any currently showing tooltip
                    _visualPopupToolTip?.Dispose();

                    // Create the actual tooltip popup object
                    _visualPopupToolTip = new VisualPopupToolTip(_ribbon.GetRedirector(),
                        sourceContent,
                        _ribbon.Renderer,
                        PaletteBackStyle.ControlToolTip,
                        PaletteBorderStyle.ControlToolTip,
                        CommonHelper.ContentStyleFromLabelStyle(toolTipStyle),
                        shadow);

                    _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

                    // The popup tooltip control always adds on a border above/below, so we negate that here.
                    screenRect.Height -= 20;
                    _visualPopupToolTip.ShowRelativeTo(e.Target, screenRect.Location);
                }
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e) =>
        // Remove any currently showing tooltip
        _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        if (sender is VisualPopupToolTip popupToolTip && popupToolTip is not null)
        {
            popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

            // Not showing a popup page anymore
            _visualPopupToolTip = null;
        }
    }

    private void OnTabsPaintBackground(object? sender, PaintEventArgs e) => PaintBackground?.Invoke(sender, e);

    #endregion
}