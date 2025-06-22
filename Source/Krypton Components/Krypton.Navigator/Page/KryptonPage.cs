#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Navigator;

/// <summary>
/// Page class used inside visual containers.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonPage), "ToolboxBitmaps.KryptonPage.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[Designer(typeof(KryptonPageDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public class KryptonPage : VisualPanel
{
    #region Instance Fields
    private readonly ViewDrawPanel _drawPanel;
    private readonly PaletteRedirectDoubleMetric _redirectNavigator;
    private readonly PaletteRedirectDoubleMetric _redirectNavigatorHeaderGroup;
    private readonly PaletteRedirectTripleMetric _redirectNavigatorHeaderPrimary;
    private readonly PaletteRedirectTripleMetric _redirectNavigatorHeaderSecondary;
    private readonly PaletteRedirectTripleMetric _redirectNavigatorHeaderBar;
    private readonly PaletteRedirectTripleMetric _redirectNavigatorHeaderOverflow;
    private readonly PaletteRedirectTriple _redirectNavigatorCheckButton;
    private readonly PaletteRedirectTriple _redirectNavigatorOverflowButton;
    private readonly PaletteRedirectTriple _redirectNavigatorMiniButton;
    private readonly PaletteRedirectTriple _redirectNavigatorTab;
    private readonly PaletteRedirectRibbonTabContent _redirectNavigatorRibbonTab;
    private readonly PaletteRedirectMetric _redirectNavigatorBar;
    private readonly PaletteRedirectDouble _redirectNavigatorPage;
    private readonly PaletteRedirectDoubleMetric _redirectNavigatorSeparator;
    private readonly PaletteNavigatorRedirect? _stateCommon;
    private readonly PaletteNavigator _stateDisabled;
    private readonly PaletteNavigator _stateNormal;
    private readonly NeedPaintHandler? _needDisabledPaint;
    private readonly NeedPaintHandler? _needNormalPaint;
    private BoolFlags31 _flags;
    private string? _textTitle;
    private string? _textDescription;
    private string _toolTipTitle;
    private string _toolTipBody;
    private string _uniqueName;
    private Bitmap? _imageSmall;
    private Bitmap? _imageMedium;
    private Bitmap? _imageLarge;
    private Bitmap? _toolTipImage;
    private Color _toolTipImageTransparentColor;
    private bool _setVisible;
    private LabelStyle _toolTipStyle;
    private Size _autoHiddenSlideSize;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the control is loaded.
    /// </summary>
    [Category(@"Page")]
    [Description(@"Occurs when the control is loaded.")]
    public event EventHandler? Load;

    /// <summary>
    /// Occurs when an appearance specific page property has changed.
    /// </summary>
    [Category(@"Page")]
    [Description(@"Occurs when an appearance specific page property has changed.")]
#pragma warning disable CA1070 // Do not declare event fields as virtual
    public virtual event PropertyChangedEventHandler? AppearancePropertyChanged;
#pragma warning restore CA1070 // Do not declare event fields as virtual

    /// <summary>
    /// Occurs when the flags have changed.
    /// </summary>
    [Category(@"Page")]
    [Description(@"Occurs when the flags have changed.")]
    public event KryptonPageFlagsEventHandler? FlagsChanged;

    /// <summary>
    /// Occurs when the AutoHiddenSlideSize property has changed.
    /// </summary>
    [Category(@"Page")]
    [Description(@"Occurs when the auto hidden slide size have changed.")]
    public event EventHandler? AutoHiddenSlideSizeChanged;

    /// <summary>
    /// Occurs when the value of the Dock property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? DockChanged;

    /// <summary>
    /// Occurs when the value of the Location property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? LocationChanged;

    /// <summary>
    /// Occurs when the value of the TabIndex property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? TabIndexChanged;

    /// <summary>
    /// Occurs when the value of the TabStop property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? TabStopChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPage class.
    /// </summary>
    public KryptonPage()
        : this(@"Page", null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonPage class.
    /// </summary>
    /// <param name="text">Initial text.</param>
    public KryptonPage(string text)
        : this(text, null, null)
    {
    }
        
    /// <summary>
    /// Initialize a new instance of the KryptonPage class.
    /// </summary>
    /// <param name="text">Initial text.</param>
    /// <param name="uniqueName">Initial unique name.</param>
    public KryptonPage(string text, string? uniqueName)
        : this(text, null, uniqueName ?? string.Empty)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonPage class.
    /// </summary>
    /// <param name="text">Initial text.</param>
    /// <param name="imageSmall">Initial small image.</param>
    /// <param name="uniqueName">Initial unique name.</param>
    /// <remarks>
    /// If Min Size not set in the Embedded control, then will default to 150, 50
    /// </remarks>
    public KryptonPage(string text, Bitmap? imageSmall, string? uniqueName)
        : this(text, imageSmall, uniqueName, new Size(150, 50))
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonPage class.
    /// </summary>
    /// <param name="text">Initial text.</param>
    /// <param name="imageSmall">Initial small image.</param>
    /// <param name="uniqueName">Initial unique name.</param>
    /// <param name="minSize">Min Size of dragged docked control, if not set by Embedded</param>
    public KryptonPage(string text, Bitmap? imageSmall, string? uniqueName, Size minSize)
    {
        // Default properties
        Text = text;
        MinimumSize = minSize;
        _textTitle = @"Page Title";
        _textDescription = @"Page Description";
        _toolTipTitle = @"Page ToolTip";
        _toolTipBody = string.Empty;
        _toolTipImage = null;
        _toolTipStyle = LabelStyle.ToolTip;
        _toolTipImageTransparentColor = Color.Empty;
        _imageSmall = imageSmall;
        _setVisible = true;
        _autoHiddenSlideSize = new Size(200, 200);
        _uniqueName = string.IsNullOrEmpty(uniqueName) ? CommonHelper.UniqueString : uniqueName ?? string.Empty;
        _flags.Flags = (int)KryptonPageFlags.All;
        _flags.ClearFlags((int)KryptonPageFlags.PageInOverflowBarForOutlookMode);

        // Create delegates
        _needDisabledPaint = OnNeedDisabledPaint!;
        _needNormalPaint = OnNeedNormalPaint!;

        if (Redirector is null)
        {
            throw new ArgumentNullException(nameof(Redirector));
        }

        // Create redirector for inheriting from owning navigator
        _redirectNavigator = new PaletteRedirectDoubleMetric(Redirector);
        _redirectNavigatorPage = new PaletteRedirectDouble(Redirector);
        _redirectNavigatorHeaderGroup = new PaletteRedirectDoubleMetric(Redirector);
        _redirectNavigatorHeaderPrimary = new PaletteRedirectTripleMetric(Redirector);
        _redirectNavigatorHeaderSecondary = new PaletteRedirectTripleMetric(Redirector);
        _redirectNavigatorHeaderBar = new PaletteRedirectTripleMetric(Redirector);
        _redirectNavigatorHeaderOverflow = new PaletteRedirectTripleMetric(Redirector);
        _redirectNavigatorCheckButton = new PaletteRedirectTriple(Redirector);
        _redirectNavigatorOverflowButton = new PaletteRedirectTriple(Redirector);
        _redirectNavigatorMiniButton = new PaletteRedirectTriple(Redirector);
        _redirectNavigatorBar = new PaletteRedirectMetric(Redirector);
        _redirectNavigatorSeparator = new PaletteRedirectDoubleMetric(Redirector);
        _redirectNavigatorTab = new PaletteRedirectTriple(Redirector);
        _redirectNavigatorRibbonTab = new PaletteRedirectRibbonTabContent(Redirector);

        // Create the palette storage
        _stateCommon = new PaletteNavigatorRedirect(null,
            _redirectNavigator,
            _redirectNavigatorPage,
            _redirectNavigatorHeaderGroup,
            _redirectNavigatorHeaderPrimary,
            _redirectNavigatorHeaderSecondary,
            _redirectNavigatorHeaderBar,
            _redirectNavigatorHeaderOverflow,
            _redirectNavigatorCheckButton,
            _redirectNavigatorOverflowButton,
            _redirectNavigatorMiniButton,
            _redirectNavigatorBar,
            new PaletteRedirectBorder(Redirector),
            _redirectNavigatorSeparator,
            _redirectNavigatorTab,
            _redirectNavigatorRibbonTab,
            new PaletteRedirectRibbonGeneral(Redirector),
            NeedPaintDelegate);

        _stateDisabled = new PaletteNavigator(_stateCommon, _needDisabledPaint);
        _stateNormal = new PaletteNavigator(_stateCommon, _needNormalPaint);
        StateTracking = new PaletteNavigatorOtherEx(_stateCommon, _needNormalPaint);
        StatePressed = new PaletteNavigatorOtherEx(_stateCommon, _needNormalPaint);
        StateSelected = new PaletteNavigatorOther(_stateCommon, _needNormalPaint);

        OverrideFocus = new PaletteNavigatorOtherRedirect(_redirectNavigatorCheckButton,
            _redirectNavigatorOverflowButton,
            _redirectNavigatorMiniButton,
            _redirectNavigatorTab,
            _redirectNavigatorRibbonTab, _needNormalPaint);

        // Our view contains just a simple canvas that covers entire client area
        _drawPanel = new ViewDrawPanel(_stateNormal.Page);

        // Create page specific button spec storage
        ButtonSpecs = new PageButtonSpecCollection(this);

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawPanel);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"KryptonPage {Text}";

    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteMode PaletteMode
    {
        [DebuggerStepThrough]
        get => base.PaletteMode;
        set => throw new OperationCanceledException("Cannot change PaletteMode property");
    }

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBase? Palette
    {
        [DebuggerStepThrough]
        get => base.Palette;
        set => throw new OperationCanceledException("Cannot change PaletteMode property");
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PageButtonSpecCollection? ButtonSpecs { get; }

    /// <summary>
    /// Gets access to the common page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigatorRedirect? StateCommon
    {
        [DebuggerStepThrough]
        get => _stateCommon;
    }

    private bool ShouldSerializeStateCommon() => !StateCommon!.IsDefault;

    /// <summary>
    /// Gets access to the disabled page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigator StateDisabled
    {
        [DebuggerStepThrough]
        get => _stateDisabled;
    }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigator StateNormal
    {
        [DebuggerStepThrough]
        get => _stateNormal;
    }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigatorOtherEx StateTracking
    {
        [DebuggerStepThrough]
        get;
    }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigatorOtherEx StatePressed
    {
        [DebuggerStepThrough]
        get;
    }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the selected page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining selected page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigatorOther StateSelected
    {
        [DebuggerStepThrough]
        get;
    }

    private bool ShouldSerializeStateSelected() => !StateSelected.IsDefault;

    /// <summary>
    /// Gets access to the focus page appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining focus page appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavigatorOtherRedirect OverrideFocus
    {
        [DebuggerStepThrough]
        get;
    }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets and sets the page text.
    /// </summary>
    [Bindable(true)]
    [Browsable(true)]
    [Category(@"Appearance")]
    [Description(@"The page text.")]
    [DefaultValue("Page")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [AllowNull]
    public override string Text
    {
        [DebuggerStepThrough]
        get => base.Text;

        set
        {
            if (base.Text != value)
            {
                base.Text = value;
                OnAppearancePropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets and sets the title text for the page.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"The title text for the page.")]
    [DefaultValue("Page Title")]
    [AllowNull]
    public virtual string TextTitle
    {
        [DebuggerStepThrough]
        get => _textTitle ?? string.Empty;

        set
        {
            if (_textTitle != value)
            {
                _textTitle = value;
                OnAppearancePropertyChanged(nameof(TextTitle));
            }
        }
    }

    private void ResetTextTitle() => TextTitle = null;

    /// <summary>
    /// Gets and sets the description text for the page.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"The description text for the page.")]
    [DefaultValue("Page Description")]
    [AllowNull]
    public virtual string TextDescription
    {
        [DebuggerStepThrough]
        get => _textDescription ?? string.Empty;

        set
        {
            if (_textDescription != value)
            {
                _textDescription = value;
                OnAppearancePropertyChanged(nameof(TextDescription));
            }
        }
    }

    private void ResetTextDescription() => TextDescription = null;

    /// <summary>
    /// Gets and sets the small image for the page.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The small image that represents the page.")]
    [Localizable(true)]
    [DefaultValue(null)]
    public virtual Bitmap? ImageSmall
    {
        [DebuggerStepThrough]
        get => _imageSmall;

        set
        {
            if (_imageSmall != value)
            {
                _imageSmall = value;
                OnAppearancePropertyChanged(nameof(ImageSmall));
            }
        }
    }

    private void ResetImageSmall() => ImageSmall = null;

    /// <summary>
    /// Gets and sets the medium image for the page.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The medium image that represents the page.")]
    [Localizable(true)]
    [DefaultValue(null)]
    public virtual Bitmap? ImageMedium
    {
        [DebuggerStepThrough]
        get => _imageMedium;

        set
        {
            if (_imageMedium != value)
            {
                _imageMedium = value;
                OnAppearancePropertyChanged(nameof(ImageMedium));
            }
        }
    }

    private void ResetImageMedium() => ImageMedium = null;

    /// <summary>
    /// Gets and sets the large image for the page.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The large image that represents the page.")]
    [Localizable(true)]
    [DefaultValue(null)]
    public virtual Bitmap? ImageLarge
    {
        [DebuggerStepThrough]
        get => _imageLarge;

        set
        {
            if (_imageLarge != value)
            {
                _imageLarge = value;
                OnAppearancePropertyChanged(nameof(ImageLarge));
            }
        }
    }

    private void ResetImageLarge() => ImageLarge = null;

    /// <summary>
    /// Gets and sets the page tooltip image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Page tooltip image.")]
    [DefaultValue(null)]
    public virtual Bitmap? ToolTipImage
    {
        get => _toolTipImage;

        set
        {
            if (_toolTipImage != value)
            {
                _toolTipImage = value;
                OnAppearancePropertyChanged(nameof(ToolTipImage));
            }
        }
    }

    private bool ShouldSerializeToolTipImage() => ToolTipImage != null;
    private void ResetToolTipImage() => ToolTipImage = null;

    /// <summary>
    /// Gets and sets the tooltip image transparent color.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Page tooltip image transparent color.")]
    [KryptonDefaultColor]
    public virtual Color ToolTipImageTransparentColor
    {
        get => _toolTipImageTransparentColor;

        set
        {
            if (_toolTipImageTransparentColor != value)
            {
                _toolTipImageTransparentColor = value;
                OnAppearancePropertyChanged(nameof(ToolTipImageTransparentColor));
            }
        }
    }

    private bool ShouldSerializeToolTipImageTransparentColor() => ToolTipImageTransparentColor != Color.Empty;
    private void ResetToolTipImageTransparentColor() => ToolTipImageTransparentColor = Color.Empty;

    /// <summary>
    /// Gets and sets the page tooltip title text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Page tooltip title text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    public virtual string ToolTipTitle
    {
        get => _toolTipTitle;

        set
        {
            if (_toolTipTitle != value)
            {
                _toolTipTitle = value;
                OnAppearancePropertyChanged(nameof(ToolTipTitle));
            }
        }
    }

    private bool ShouldSerializeToolTipTitle() => ToolTipTitle != string.Empty;
    private void ResetToolTipTitle() => ToolTipTitle = string.Empty;

    /// <summary>
    /// Gets and sets the page tooltip body text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Page tooltip body text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    public virtual string ToolTipBody
    {
        get => _toolTipBody;

        set
        {
            if (_toolTipBody != value)
            {
                _toolTipBody = value;
                OnAppearancePropertyChanged(nameof(ToolTipBody));
            }
        }
    }

    private bool ShouldSerializeToolTipBody() => ToolTipBody != string.Empty;
    private void ResetToolTipBody() => ToolTipBody = string.Empty;

    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Page tooltip label style.")]
    //[DefaultValue(typeof(LabelStyle), "ToolTip")]
    public virtual LabelStyle ToolTipStyle
    {
        get => _toolTipStyle;

        set
        {
            if (_toolTipStyle != value)
            {
                _toolTipStyle = value;
                OnAppearancePropertyChanged(nameof(ToolTipStyle));
            }
        }
    }

    private bool ShouldSerializeToolTipStyle() => ToolTipStyle != LabelStyle.ToolTip;
    private void ResetToolTipStyle() => ToolTipStyle = LabelStyle.ToolTip;

    #region ToolTipShadow
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Button tooltip Shadow.")]
    [DefaultValue(true)]
    public bool ToolTipShadow { get; set; } = true; // Backward compatible -> "Material Design" suggests this to be false

    private bool ShouldSerializeToolTipShadow() => !ToolTipShadow;
    private void ResetToolTipShadow() => ToolTipShadow = true;
    #endregion

    /// <summary>
    /// Gets and sets the unique name of the page.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The unique name of the page.")]
    [DisallowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public virtual string UniqueName
    {
        [DebuggerStepThrough]
        get => _uniqueName;

        [DebuggerStepThrough]
        set => _uniqueName = value;
    }

    private void ResetUniqueName() => UniqueName = CommonHelper.UniqueString;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) =>
        // Request fixed state from the view
        _drawPanel.FixedState = state;

    /// <summary>
    /// Gets and sets the preferred size for the page when inside an auto hidden slide panel.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category(@"Appearance")]
    [Description(@"When used within a KryptonDockingSpace,\nGive a hint on the Minimum Initial size needed")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual Size AutoHiddenSlideSize
    {
        get => _autoHiddenSlideSize;

        set
        {
            if (_autoHiddenSlideSize != value)
            {
                _autoHiddenSlideSize = value;
                OnAutoHiddenSlideSizeChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Define the state to use when inheriting state values.
    /// </summary>
    /// <param name="alignControl">Control to use when aligning rectangles.</param>
    /// <param name="common">State palette for inheriting common values.</param>
    /// <param name="disabled">State palette for inheriting disabled values.</param>
    /// <param name="normal">State palette for inheriting normal values.</param>
    /// <param name="tracking">State palette for inheriting tracking values.</param>
    /// <param name="pressed">State palette for inheriting pressed values.</param>
    /// <param name="selected">State palette for inheriting selected values.</param>
    /// <param name="focus">State palette for inheriting focus values.</param>
    public virtual void SetInherit(Control alignControl,
        PaletteNavigatorRedirect? common,
        PaletteNavigator disabled,
        PaletteNavigator normal,
        PaletteNavigatorOtherEx tracking,
        PaletteNavigatorOtherEx pressed,
        PaletteNavigatorOther selected,
        PaletteNavigatorOtherRedirect focus)
    {
        if (ViewManager != null)
        {
            ViewManager.AlignControl = alignControl;
        }

        // Setup the redirection states
        _redirectNavigator?.SetRedirectStates(disabled, disabled, normal, normal);

        _redirectNavigatorPage?.SetRedirectStates(disabled.PalettePage, normal.PalettePage);

        _redirectNavigatorHeaderGroup?.SetRedirectStates(disabled.HeaderGroup, disabled.HeaderGroup,
            normal.HeaderGroup, normal.HeaderGroup);

        _redirectNavigatorHeaderPrimary?.SetRedirectStates(disabled.HeaderGroup.HeaderPrimary,
            disabled.HeaderGroup.HeaderPrimary, normal.HeaderGroup.HeaderPrimary,
            normal.HeaderGroup.HeaderPrimary);

        _redirectNavigatorHeaderSecondary?.SetRedirectStates(disabled.HeaderGroup.HeaderSecondary,
            disabled.HeaderGroup.HeaderSecondary, normal.HeaderGroup.HeaderSecondary,
            normal.HeaderGroup.HeaderSecondary);

        _redirectNavigatorHeaderBar?.SetRedirectStates(disabled.HeaderGroup.HeaderBar,
            disabled.HeaderGroup.HeaderBar, normal.HeaderGroup.HeaderBar, normal.HeaderGroup.HeaderBar);

        _redirectNavigatorHeaderOverflow?.SetRedirectStates(disabled.HeaderGroup.HeaderOverflow,
            disabled.HeaderGroup.HeaderOverflow, normal.HeaderGroup.HeaderOverflow,
            normal.HeaderGroup.HeaderOverflow);

        _redirectNavigatorCheckButton?.SetRedirectStates(disabled.CheckButton, normal.CheckButton,
            pressed.CheckButton, tracking.CheckButton, selected.CheckButton, selected.CheckButton,
            selected.CheckButton, focus.CheckButton, null!);

        _redirectNavigatorOverflowButton?.SetRedirectStates(disabled.OverflowButton, normal.OverflowButton,
            pressed.OverflowButton, tracking.OverflowButton, selected.OverflowButton, selected.OverflowButton,
            selected.OverflowButton, focus.OverflowButton, null!);

        _redirectNavigatorMiniButton?.SetRedirectStates(disabled.MiniButton, normal.MiniButton,
            pressed.MiniButton, tracking.MiniButton, selected.MiniButton, selected.MiniButton,
            selected.MiniButton, focus.MiniButton, null!);

        _redirectNavigatorBar?.SetRedirectStates(common!.Bar, common.Bar);

        _redirectNavigatorSeparator?.SetRedirectStates(disabled.Separator, disabled.Separator, normal.Separator,
            normal.Separator, pressed.Separator, pressed.Separator, tracking.Separator, tracking.Separator);

        _redirectNavigatorTab?.SetRedirectStates(disabled.Tab, normal.Tab, pressed.Tab, tracking.Tab,
            selected.Tab, selected.Tab, selected.Tab, focus.Tab, null!);

        _redirectNavigatorRibbonTab?.SetRedirectStates(disabled.RibbonTab, normal.RibbonTab, pressed.RibbonTab,
            tracking.RibbonTab, selected.RibbonTab, focus.RibbonTab);

        if (_stateCommon is not null && Redirector is not null) 
        {
            _stateCommon.RedirectBorderEdge = new PaletteRedirectBorderEdge(Redirector, disabled.BorderEdge, normal.BorderEdge);
            _stateCommon.RedirectRibbonGeneral = new PaletteRedirectRibbonGeneral(Redirector);
        }
    }

    /// <summary>
    /// Reset the state palettes so they no longer inherit from external source.
    /// </summary>
    /// <param name="alignControl">Only if inherited values are still the same as when the aligned control was set are they reset.</param>
    public virtual void ResetInherit(Control alignControl)
    {
        if (ViewManager != null && alignControl == ViewManager.AlignControl)
        {
            ViewManager.AlignControl = this;

            // Clear down the redirection states
            _redirectNavigator?.ResetRedirectStates();

            _redirectNavigatorPage?.ResetRedirectStates();

            _redirectNavigatorHeaderGroup?.ResetRedirectStates();

            _redirectNavigatorHeaderPrimary?.ResetRedirectStates();

            _redirectNavigatorHeaderSecondary?.ResetRedirectStates();

            _redirectNavigatorHeaderBar?.ResetRedirectStates();

            _redirectNavigatorHeaderOverflow?.ResetRedirectStates();

            _redirectNavigatorCheckButton?.ResetRedirectStates();

            _redirectNavigatorOverflowButton?.ResetRedirectStates();

            _redirectNavigatorMiniButton?.ResetRedirectStates();

            _redirectNavigatorBar?.ResetRedirectStates();

            _redirectNavigatorSeparator?.ResetRedirectStates();

            _redirectNavigatorTab?.ResetRedirectStates();

            _redirectNavigatorRibbonTab?.ResetRedirectStates();

            if (_stateCommon is not null && Redirector is not null)
            {
                _stateCommon.RedirectBorderEdge = new PaletteRedirectBorder(Redirector);
                _stateCommon.RedirectRibbonGeneral = new PaletteRedirectRibbonGeneral(Redirector);
            }
        }
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets which edges of the control are anchored to the edges of its container.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AnchorStyles Anchor
    {
        get => base.Anchor;
        set => base.Anchor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets or sets the size of the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new Size Size
    {
        get => base.Size;
        set => base.Size = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set => base.AutoSizeMode = value;
    }

    /// <summary>
    /// Gets or sets which edge of the parent container a control is docked to.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
        get => base.Dock;
        set => base.Dock = value;
    }

    /// <summary>
    /// Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Point Location
    {
        get => base.Location;
        set => base.Location = value;
    }

    /// <summary>
    /// Gets or sets the tab order of the control within its container.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int TabIndex
    {
        get => base.TabIndex;
        set => base.TabIndex = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool TabStop
    {
        get => base.TabStop;
        set => base.TabStop = value;
    }

    /// <summary>
    /// Gets the string that matches the mapping request.
    /// </summary>
    /// <param name="mapping">Text mapping.</param>
    /// <returns>Matching string.</returns>
    public virtual string GetTextMapping(MapKryptonPageText mapping)
    {
        var ret = string.Empty;

        // Recover the first preference value
        switch (mapping)
        {
            case MapKryptonPageText.Text:
            case MapKryptonPageText.TextTitle:
            case MapKryptonPageText.TextTitleDescription:
            case MapKryptonPageText.TextDescription:
                ret = Text;
                break;
            case MapKryptonPageText.Title:
            case MapKryptonPageText.TitleDescription:
            case MapKryptonPageText.TitleText:
                ret = TextTitle;
                break;
            case MapKryptonPageText.Description:
            case MapKryptonPageText.DescriptionText:
            case MapKryptonPageText.DescriptionTitle:
            case MapKryptonPageText.DescriptionTitleText:
                ret = TextDescription;
                break;
            case MapKryptonPageText.ToolTipTitle:
                ret = ToolTipTitle;
                break;
            case MapKryptonPageText.ToolTipBody:
                ret = ToolTipBody;
                break;
        }

        // If nothing found then...
        if (string.IsNullOrEmpty(ret))
        {
            // Recover the second preference value
            switch (mapping)
            {
                case MapKryptonPageText.TitleText:
                case MapKryptonPageText.DescriptionText:
                    ret = Text;
                    break;
                case MapKryptonPageText.TextTitle:
                case MapKryptonPageText.TextTitleDescription:
                case MapKryptonPageText.DescriptionTitle:
                case MapKryptonPageText.DescriptionTitleText:
                    ret = TextTitle;
                    break;
                case MapKryptonPageText.TextDescription:
                case MapKryptonPageText.TitleDescription:
                    ret = TextDescription;
                    break;
            }
        }

        // If nothing found then...
        if (string.IsNullOrEmpty(ret))
        {
            // Recover the third preference value
            switch (mapping)
            {
                case MapKryptonPageText.DescriptionTitleText:
                    ret = Text;
                    break;
                case MapKryptonPageText.TextTitleDescription:
                    ret = TextDescription;
                    break;
            }
        }

        // We do not want to return a null
        return ret;
    }

    /// <summary>
    /// Gets the image that matches the mapping request.
    /// </summary>
    /// <param name="mapping">Image mapping.</param>
    /// <returns>Image reference.</returns>
    public virtual Image? GetImageMapping(MapKryptonPageImage mapping)
    {
        Image? ret = null;

        // Recover the first preference value
        switch (mapping)
        {
            case MapKryptonPageImage.Small:
            case MapKryptonPageImage.SmallMedium:
            case MapKryptonPageImage.SmallMediumLarge:
                ret = ImageSmall;
                break;
            case MapKryptonPageImage.Medium:
            case MapKryptonPageImage.MediumLarge:
            case MapKryptonPageImage.MediumSmall:
                ret = ImageMedium;
                break;
            case MapKryptonPageImage.Large:
            case MapKryptonPageImage.LargeMedium:
            case MapKryptonPageImage.LargeMediumSmall:
                ret = ImageLarge;
                break;
            case MapKryptonPageImage.ToolTip:
                ret = ToolTipImage;
                break;
        }

        // If nothing found then...
        if (ret == null)
        {
            // Recover the second preference value
            switch (mapping)
            {
                case MapKryptonPageImage.MediumSmall:
                    ret = ImageSmall;
                    break;
                case MapKryptonPageImage.SmallMedium:
                case MapKryptonPageImage.SmallMediumLarge:
                case MapKryptonPageImage.LargeMedium:
                case MapKryptonPageImage.LargeMediumSmall:
                    ret = ImageMedium;
                    break;
                case MapKryptonPageImage.MediumLarge:
                    ret = ImageLarge;
                    break;
            }
        }

        // If nothing found then...
        if (ret == null)
        {
            // Recover the third preference value
            switch (mapping)
            {
                case MapKryptonPageImage.LargeMediumSmall:
                    ret = ImageSmall;
                    break;
                case MapKryptonPageImage.SmallMediumLarge:
                    ret = ImageLarge;
                    break;
            }
        }

        return ret;
    }

    /// <summary>
    /// Gets the Krypton control that is acting as the parent.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Control KryptonParentContainer
    {
        [DebuggerStepThrough]
        get => ViewManager?.AlignControl!;
    }

    /// <summary>
    /// Gets and sets the set of page flags.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(0)]
    public virtual int Flags
    {
        get => _flags.Flags;

        set
        {
            if (_flags.Flags != value)
            {
                var changed = _flags.Flags ^ value;
                _flags.Flags = value;
                OnFlagsChanged((KryptonPageFlags)changed);
            }
        }
    }

    /// <summary>
    /// Set all the provided flags to true.
    /// </summary>
    /// <param name="flags">Flags to set.</param>
    public virtual void SetFlags(KryptonPageFlags flags)
    {
        var changed = _flags.SetFlags((int)flags);

        if (changed != 0)
        {
            OnFlagsChanged((KryptonPageFlags)changed);
        }
    }

    /// <summary>
    /// Sets all the provided flags to false.
    /// </summary>
    /// <param name="flags">Flags to set.</param>
    public virtual void ClearFlags(KryptonPageFlags flags)
    {
        var changed = _flags.ClearFlags((int)flags);

        if (changed != 0)
        {
            OnFlagsChanged((KryptonPageFlags)changed);
        }
    }

    /// <summary>
    /// Are all the provided flags set to true.
    /// </summary>
    /// <param name="flags">Flags to test.</param>
    /// <returns>True if all provided flags are defined as true; otherwise false.</returns>
    public virtual bool AreFlagsSet(KryptonPageFlags flags) => _flags.AreFlagsSet((int)flags);

    /// <summary>
    /// Gets the last value set to the Visible property.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual bool LastVisibleSet
    {
        get => _setVisible;

        set
        {
            if (value != _setVisible)
            {
                _setVisible = value;

                // Must generate event manually because if we are set to false and the parent
                // chain is also false then an event will not be generated automatically.
                OnVisibleChanged(EventArgs.Empty);
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// The OnCreateControl method is called when the control is first created.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        OnLoad(EventArgs.Empty);
    }

    /// <summary>
    /// Sets the control to the specified visible state. 
    /// </summary>
    /// <param name="value">true to make the control visible; otherwise, false.</param>
    protected override void SetVisibleCore(bool value)
    {
        LastVisibleSet = value;
        base.SetVisibleCore(value);
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        // We need to snoop the need to show a context menu
        if (m.Msg == PI.WM_.CONTEXTMENU)
        {
            // Only interested in overriding the behavior when we have a krypton context menu...
            if (KryptonContextMenu != null)
            {
                // Extract the screen mouse position (if might not actually be provided)
                var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                // If keyboard activated, the menu position is centered
                mousePt = ((int)m.LParam) == -1 ? new Point(Width / 2, Height / 2) : PointToClient(mousePt);

                // If the mouse position is within our client area
                if (ClientRectangle.Contains(mousePt))
                {
                    if (!DesignMode)
                    {
                        // Show the context menu
                        KryptonContextMenu.Show(this, PointToScreen(mousePt));

                        // We eat the message!
                        return;
                    }
                }
            }
        }

        base.WndProc(ref m);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        if (_stateNormal != null && _stateDisabled != null)
        {
            _drawPanel.SetPalettes(Enabled ? _stateNormal.Page : _stateDisabled.Page);
        }

        // Update state of view panel to reflect page state
        _drawPanel.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }


    /// <summary>
    /// Raises the DockChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnDockChanged(EventArgs e) => DockChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the LocationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnLocationChanged(EventArgs e) => LocationChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TabIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnTabIndexChanged(EventArgs e) => TabIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TabStopChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnTabStopChanged(EventArgs e) =>
        // https://github.com/Krypton-Suite/Standard-Toolkit/issues/1023#issuecomment-1588810368
        TabStopChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the AppearancePropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the appearance property that has changed.</param>
    protected virtual void OnAppearancePropertyChanged(string propertyName) => AppearancePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Raises the FlagsChanged event.
    /// </summary>
    /// <param name="changed">Set of flags that have changed.</param>
    protected virtual void OnFlagsChanged(KryptonPageFlags changed) => FlagsChanged?.Invoke(this, new KryptonPageFlagsEventArgs(changed));

    /// <summary>
    /// Raises the AutoHiddenSlideSizeChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnAutoHiddenSlideSizeChanged(EventArgs e) => AutoHiddenSlideSizeChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Load event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLoad(EventArgs e) => Load?.Invoke(this, e);

    /// <summary>
    /// Processes the need for a repaint for the disabled palette values.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected virtual void OnNeedDisabledPaint(object sender, NeedLayoutEventArgs e)
    {
        if (!Enabled)
        {
            OnAppearancePropertyChanged(nameof(Palette));
            OnNeedPaint(this, e);
        }
    }

    /// <summary>
    /// Processes the need for a repaint for the enabled palette values.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected virtual void OnNeedNormalPaint(object sender, NeedLayoutEventArgs e)
    {
        if (Enabled)
        {
            OnAppearancePropertyChanged(nameof(Palette));
            OnNeedPaint(this, e);
        }
    }
    #endregion

    #region Implementation
    private void OnKryptonContextMenuDisposed(object sender, EventArgs e) =>
        // When the current krypton context menu is disposed, we should remove 
        // it to prevent it being used again, as that would just throw an exception 
        // because it has been disposed.
        KryptonContextMenu = null;
    #endregion
}