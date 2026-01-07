#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws the notification bar below the ribbon groups.
/// </summary>
internal class ViewDrawRibbonNotificationBar : ViewComposite
{
    #region Type Definitions
    /// <summary>
    /// Palette wrapper that forces left text alignment.
    /// </summary>
    private class LeftAlignedContentPalette : IPaletteContent
    {
        private readonly IPaletteContent _basePalette;

        public LeftAlignedContentPalette(IPaletteContent basePalette) => _basePalette = basePalette;

        public PaletteRelativeAlign GetContentShortTextH(PaletteState state) => PaletteRelativeAlign.Near;
        public PaletteRelativeAlign GetContentLongTextH(PaletteState state) => PaletteRelativeAlign.Near;

        // Delegate all other methods to base palette
        public InheritBool GetContentDraw(PaletteState state) => _basePalette.GetContentDraw(state);
        public InheritBool GetContentDrawFocus(PaletteState state) => _basePalette.GetContentDrawFocus(state);
        public Font? GetContentShortTextFont(PaletteState state) => _basePalette.GetContentShortTextFont(state);
        public Font? GetContentShortTextNewFont(PaletteState state) => _basePalette.GetContentShortTextNewFont(state);
        public PaletteTextHint GetContentShortTextHint(PaletteState state) => _basePalette.GetContentShortTextHint(state);
        public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => _basePalette.GetContentShortTextPrefix(state);
        public InheritBool GetContentShortTextMultiLine(PaletteState state) => _basePalette.GetContentShortTextMultiLine(state);
        public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) => _basePalette.GetContentShortTextMultiLineH(state);
        public PaletteTextTrim GetContentShortTextTrim(PaletteState state) => _basePalette.GetContentShortTextTrim(state);
        public PaletteRelativeAlign GetContentShortTextV(PaletteState state) => _basePalette.GetContentShortTextV(state);
        public Color GetContentShortTextColor1(PaletteState state) => _basePalette.GetContentShortTextColor1(state);
        public Color GetContentShortTextColor2(PaletteState state) => _basePalette.GetContentShortTextColor2(state);
        public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) => _basePalette.GetContentShortTextColorStyle(state);
        public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) => _basePalette.GetContentShortTextColorAlign(state);
        public float GetContentShortTextColorAngle(PaletteState state) => _basePalette.GetContentShortTextColorAngle(state);
        public Image? GetContentShortTextImage(PaletteState state) => _basePalette.GetContentShortTextImage(state);
        public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) => _basePalette.GetContentShortTextImageStyle(state);
        public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) => _basePalette.GetContentShortTextImageAlign(state);
        public Font? GetContentLongTextFont(PaletteState state) => _basePalette.GetContentLongTextFont(state);
        public Font? GetContentLongTextNewFont(PaletteState state) => _basePalette.GetContentLongTextNewFont(state);
        public PaletteTextHint GetContentLongTextHint(PaletteState state) => _basePalette.GetContentLongTextHint(state);
        public InheritBool GetContentLongTextMultiLine(PaletteState state) => _basePalette.GetContentLongTextMultiLine(state);
        public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) => _basePalette.GetContentLongTextMultiLineH(state);
        public PaletteTextTrim GetContentLongTextTrim(PaletteState state) => _basePalette.GetContentLongTextTrim(state);
        public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) => _basePalette.GetContentLongTextPrefix(state);
        public PaletteRelativeAlign GetContentLongTextV(PaletteState state) => _basePalette.GetContentLongTextV(state);
        public Color GetContentLongTextColor1(PaletteState state) => _basePalette.GetContentLongTextColor1(state);
        public Color GetContentLongTextColor2(PaletteState state) => _basePalette.GetContentLongTextColor2(state);
        public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) => _basePalette.GetContentLongTextColorStyle(state);
        public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) => _basePalette.GetContentLongTextColorAlign(state);
        public float GetContentLongTextColorAngle(PaletteState state) => _basePalette.GetContentLongTextColorAngle(state);
        public Image? GetContentLongTextImage(PaletteState state) => _basePalette.GetContentLongTextImage(state);
        public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) => _basePalette.GetContentLongTextImageStyle(state);
        public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) => _basePalette.GetContentLongTextImageAlign(state);
        public PaletteRelativeAlign GetContentImageH(PaletteState state) => _basePalette.GetContentImageH(state);
        public PaletteRelativeAlign GetContentImageV(PaletteState state) => _basePalette.GetContentImageV(state);
        public PaletteImageEffect GetContentImageEffect(PaletteState state) => _basePalette.GetContentImageEffect(state);
        public Color GetContentImageColorMap(PaletteState state) => _basePalette.GetContentImageColorMap(state);
        public Color GetContentImageColorTo(PaletteState state) => _basePalette.GetContentImageColorTo(state);
        public Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => _basePalette.GetBorderContentPadding(owningForm, state);
        public int GetContentAdjacentGap(PaletteState state) => _basePalette.GetContentAdjacentGap(state);
        public PaletteContentStyle GetContentStyle() => _basePalette.GetContentStyle();
    }
    /// <summary>
    /// Represents a button view with its controller and index.
    /// </summary>
    private class ButtonViewInfo
    {
        public ViewDrawButton ButtonView { get; set; } = null!;
        public ButtonController Controller { get; set; } = null!;
        public int Index { get; set; }
        public bool IsCloseButton { get; set; }
    }

    /// <summary>
    /// Content provider for action buttons.
    /// </summary>
    private class ActionButtonContent : IContentValues
    {
        private readonly string _text;

        public ActionButtonContent(string text)
        {
            _text = text;
        }

        public bool HasContent => !string.IsNullOrEmpty(_text);

        public Image? GetImage(PaletteState state) => null;

        public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        public string GetShortText() => _text;

        public string GetLongText() => string.Empty; // Only use short text to prevent duplication
    }

    /// <summary>
    /// Content provider for close button (X symbol).
    /// </summary>
    private class CloseButtonContent : IContentValues
    {
        public bool HasContent => true;

        public Image? GetImage(PaletteState state) => null;

        public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        public string GetShortText() => "×";

        public string GetLongText() => string.Empty; // Only use short text to prevent duplication
    }
    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly NeedPaintHandler _needPaint;
    private readonly int _iconSize;
    private readonly int _closeButtonSize;
    private readonly int _buttonSpacing;
    private readonly int _horizontalPadding;
    private KryptonRibbonNotificationBarData? _notificationData;
    private readonly List<ButtonViewInfo> _buttonViews;
    private IDisposable? _mementoBack;
    private ViewLayoutDocker? _layoutDocker;
    private ViewDrawContent? _iconContent;
    private ViewDrawContent? _textContent;
    private ViewLayoutStack? _buttonStack;
    private ViewLayoutDocker? _buttonContainer;
    private ViewLayoutCenter? _buttonCenter;
    private PaletteTripleRedirect? _buttonPalette;
    private PaletteMetricRedirect? _buttonMetric;
    private bool _layoutNeedsUpdate;
    private bool _isUpdatingLayout;
    private readonly int _buttonVerticalPadding;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when an action button or close button is clicked.
    /// </summary>
    public event EventHandler<RibbonNotificationBarEventArgs>? ButtonClick;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonNotificationBar class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
    public ViewDrawRibbonNotificationBar([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] NeedPaintHandler needPaintDelegate)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(needPaintDelegate != null);

        _ribbon = ribbon!;
        _needPaint = needPaintDelegate!;
        _iconSize = (int)(24 * FactorDpiX);
        _closeButtonSize = (int)(20 * FactorDpiX);
        _buttonSpacing = (int)(8 * FactorDpiX);
        _buttonVerticalPadding = (int)(4 * FactorDpiY);
        _horizontalPadding = (int)(12 * FactorDpiX);
        _buttonViews = new List<ButtonViewInfo>();

        // Create button palettes
        var redirector = _ribbon.GetRedirector();
        _buttonPalette = new PaletteTripleRedirect(redirector,
            PaletteBackStyle.ButtonStandalone,
            PaletteBorderStyle.ButtonStandalone,
            PaletteContentStyle.ButtonStandalone,
            needPaintDelegate);
        _buttonMetric = new PaletteMetricRedirect(redirector);

        // Create layout structure
        _layoutDocker = new ViewLayoutDocker();
        _buttonStack = new ViewLayoutStack(true); // true = horizontal layout
        _layoutNeedsUpdate = true;

        Add(_layoutDocker);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        $@"ViewDrawRibbonNotificationBar:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementoBack != null)
            {
                _mementoBack.Dispose();
                _mementoBack = null;
            }

            if (_notificationData != null)
            {
                _notificationData.PropertyChanged -= OnNotificationDataPropertyChanged;
                _notificationData = null;
            }

            // Clean up button views
            foreach (var buttonInfo in _buttonViews)
            {
                buttonInfo.Controller.Click -= OnButtonClick;
            }
            _buttonViews.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the notification bar data.
    /// </summary>
    public KryptonRibbonNotificationBarData? NotificationData
    {
        get => _notificationData;
        set
        {
            if (_notificationData != value)
            {
                if (_notificationData != null)
                {
                    _notificationData.PropertyChanged -= OnNotificationDataPropertyChanged;
                }

                _notificationData = value;

                if (_notificationData != null)
                {
                    _notificationData.PropertyChanged += OnNotificationDataPropertyChanged;
                }

                _layoutNeedsUpdate = true;
                UpdateLayout();
                _needPaint(this, new NeedLayoutEventArgs(false));
            }
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the element.
    /// </summary>
    public override bool Visible
    {
        get => _ribbon.Visible && base.Visible && (_notificationData?.Visible ?? false);
        set => base.Visible = value;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        if (_notificationData == null || !_notificationData.Visible)
        {
            return Size.Empty;
        }

        // Calculate preferred height
        int preferredHeight = _notificationData.Height > 0
            ? _notificationData.Height
            : Math.Max((int)(40 * FactorDpiY), CalculateContentHeight());

        // Width fills available space
        return new Size(context.DisplayRectangle.Width, preferredHeight);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        if (_notificationData == null || !_notificationData.Visible || _layoutDocker == null)
        {
            ClientRectangle = Rectangle.Empty;
            return;
        }

        Rectangle clientRect = context!.DisplayRectangle;
        ClientRectangle = clientRect;

        // Update layout structure only if needed (when data changed)
        if (_layoutNeedsUpdate && _layoutDocker != null)
        {
            UpdateLayout();
            _layoutNeedsUpdate = false;
        }

        // Layout the docker
        if (_layoutDocker != null)
        {
            context.DisplayRectangle = ClientRectangle;
            _layoutDocker.Layout(context);
            context.DisplayRectangle = ClientRectangle;
        }
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (_notificationData == null || !_notificationData.Visible)
        {
            return;
        }

        // Draw background
        DrawBackground(context);

        // Draw border
        DrawBorder(context);
    }
    #endregion

    #region Implementation
    private void UpdateLayout()
    {
        if (_layoutDocker == null || _notificationData == null || _buttonPalette == null || _buttonMetric == null)
        {
            return;
        }

        // Prevent re-entrancy
        if (_isUpdatingLayout)
        {
            return;
        }

        _isUpdatingLayout = true;
        try
        {
            // Dispose old views completely before clearing
        if (_iconContent != null)
        {
            if (_iconContent.Parent != null)
            {
                _iconContent.Parent = null;
            }
            _iconContent.Dispose();
            _iconContent = null;
        }
        if (_textContent != null)
        {
            if (_textContent.Parent != null)
            {
                _textContent.Parent = null;
            }
            _textContent.Dispose();
            _textContent = null;
        }

        // Clear existing children
        _layoutDocker.Clear();

        // Add left padding
        _layoutDocker.Add(new ViewLayoutSeparator(_horizontalPadding), ViewDockStyle.Left);

        // Create fresh icon view if needed
        if (_notificationData.ShowIcon && _notificationData.Icon != null)
        {
            var iconPalette = new RibbonToContent(_ribbon.StateCommon.RibbonGeneral);
            var iconProvider = new NotificationIconContent(_notificationData);
            _iconContent = new ViewDrawContent(iconPalette, iconProvider, VisualOrientation.Top);
            // Verify not already in docker (shouldn't happen after Clear, but defensive check)
            if (!_layoutDocker.Contains(_iconContent))
            {
                _layoutDocker.Add(_iconContent, ViewDockStyle.Left);
            }
            // Add spacing after icon
            _layoutDocker.Add(new ViewLayoutSeparator(_buttonSpacing), ViewDockStyle.Left);
        }

        // Create fresh text view with left-aligned palette
        var baseTextPalette = new RibbonToContent(_ribbon.StateCommon.RibbonGeneral);
        var leftAlignedTextPalette = new LeftAlignedContentPalette(baseTextPalette);
        var textProvider = new NotificationTextContent(_notificationData);
        _textContent = new ViewDrawContent(leftAlignedTextPalette, textProvider, VisualOrientation.Top);
        // Verify not already in docker (shouldn't happen after Clear, but defensive check)
        if (!_layoutDocker.Contains(_textContent))
        {
            _layoutDocker.Add(_textContent, ViewDockStyle.Fill);
        }

        // Remove button container from docker if it exists
        if (_buttonContainer != null && _buttonContainer.Parent != null)
        {
            _buttonContainer.Parent = null;
        }
        // Remove button center from container if it exists
        if (_buttonCenter != null && _buttonCenter.Parent != null)
        {
            _buttonCenter.Parent = null;
        }
        // Remove button stack from center if it exists
        if (_buttonStack != null && _buttonStack.Parent != null)
        {
            _buttonStack.Parent = null;
        }

        // Dispose old button views
        foreach (var buttonInfo in _buttonViews)
        {
            buttonInfo.Controller.Click -= OnButtonClick;
            if (buttonInfo.ButtonView.Parent != null)
            {
                buttonInfo.ButtonView.Parent = null;
            }
            buttonInfo.ButtonView.Dispose();
        }
        _buttonViews.Clear();

        // Create button container with vertical padding
        if (_buttonContainer == null)
        {
            _buttonContainer = new ViewLayoutDocker();
        }
        _buttonContainer.Clear();

        // Create button stack for action buttons and close button (horizontal layout)
        if (_buttonStack == null)
        {
            _buttonStack = new ViewLayoutStack(true); // true = horizontal layout
        }
        _buttonStack.Clear();

        // Create action buttons first (in correct order, left to right)
        if (_notificationData.ShowActionButtons && _notificationData.ActionButtonTexts != null)
        {
            for (int i = 0; i < _notificationData.ActionButtonTexts.Length; i++)
            {
                string buttonText = _notificationData.ActionButtonTexts[i];
                var actionButton = CreateButton(new ActionButtonContent(buttonText), false, i);
                
                // Add spacing before button (except first one)
                if (i > 0)
                {
                    _buttonStack.Add(new ViewLayoutSeparator(_buttonSpacing));
                }
                
                _buttonStack.Add(actionButton.ButtonView);
                _buttonViews.Add(actionButton);
            }
        }

        // Create close button last (rightmost)
        if (_notificationData.ShowCloseButton)
        {
            // Add spacing before close button if there are action buttons
            if (_notificationData.ShowActionButtons && _notificationData.ActionButtonTexts != null && _notificationData.ActionButtonTexts.Length > 0)
            {
                _buttonStack.Add(new ViewLayoutSeparator(_buttonSpacing));
            }
            
            var closeButton = CreateButton(new CloseButtonContent(), true, -1);
            _buttonStack.Add(closeButton.ButtonView);
            _buttonViews.Add(closeButton);
        }

        // Add button stack to container with vertical padding if it has buttons
        if (_buttonStack.Count > 0)
        {
            // Create center wrapper to prevent horizontal expansion
            if (_buttonCenter == null)
            {
                _buttonCenter = new ViewLayoutCenter();
            }
            _buttonCenter.Clear();
            _buttonCenter.Add(_buttonStack);

            // Add vertical padding separators
            _buttonContainer.Add(new ViewLayoutSeparator(_buttonVerticalPadding), ViewDockStyle.Top);
            // Use center wrapper instead of Fill to prevent horizontal expansion
            _buttonContainer.Add(_buttonCenter, ViewDockStyle.Fill);
            _buttonContainer.Add(new ViewLayoutSeparator(_buttonVerticalPadding), ViewDockStyle.Bottom);

            // Add button container to main layout if not already added
            if (_buttonContainer.Parent == null)
            {
                _layoutDocker.Add(_buttonContainer, ViewDockStyle.Right);
                // Add right padding after buttons
                _layoutDocker.Add(new ViewLayoutSeparator(_horizontalPadding), ViewDockStyle.Right);
            }
        }
        else if (_buttonContainer != null && _buttonContainer.Parent != null)
        {
            // Remove button container if no buttons
            _buttonContainer.Parent = null;
        }
        }
        finally
        {
            _isUpdatingLayout = false;
        }
    }

    private ButtonViewInfo CreateButton(IContentValues content, bool isCloseButton, int index)
    {
        // Create the button view
        var buttonView = new ViewDrawButton(
            _buttonPalette!,
            _buttonPalette!,
            _buttonPalette!,
            _buttonPalette!,
            _buttonMetric!,
            content,
            VisualOrientation.Top,
            false)
        {
            TestForFocusCues = false
        };

        // Create button controller
        var controller = new ButtonController(buttonView, _needPaint)
        {
            BecomesFixed = false
        };

        // Wire up click event
        controller.Click += OnButtonClick;

        // Assign controllers to button view
        buttonView.MouseController = controller;
        buttonView.KeyController = controller;
        buttonView.SourceController = controller;

        return new ButtonViewInfo
        {
            ButtonView = buttonView,
            Controller = controller,
            Index = index,
            IsCloseButton = isCloseButton
        };
    }

    private int CalculateContentHeight()
    {
        if (_notificationData == null)
        {
            return 0;
        }

        int height = _notificationData.Padding.Vertical;
        int textHeight = (int)(16 * FactorDpiY); // Default text height
        height += Math.Max(textHeight, _iconSize);
        return height;
    }

    private void DrawBackground(RenderContext context)
    {
        if (_notificationData == null)
        {
            return;
        }

        Color backColor = GetBackColor();
        using var brush = new SolidBrush(backColor);
        context.Graphics.FillRectangle(brush, ClientRectangle);
    }

    private void DrawBorder(RenderContext context)
    {
        if (_notificationData == null)
        {
            return;
        }

        Color borderColor = GetBorderColor();
        using var pen = new Pen(borderColor, 1);
        context.Graphics.DrawLine(pen, ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Right, ClientRectangle.Top);
    }

    private Color GetBackColor()
    {
        if (_notificationData == null)
        {
            return Color.White;
        }

        return _notificationData.Type switch
        {
            RibbonNotificationBarType.Information => Color.FromArgb(217, 236, 255),
            RibbonNotificationBarType.Warning => Color.FromArgb(255, 242, 204),
            RibbonNotificationBarType.Error => Color.FromArgb(255, 204, 204),
            RibbonNotificationBarType.Success => Color.FromArgb(204, 255, 204),
            RibbonNotificationBarType.Custom => _notificationData.CustomBackColor,
            _ => Color.White
        };
    }

    private Color GetForeColor()
    {
        if (_notificationData == null)
        {
            return Color.Black;
        }

        return _notificationData.Type == RibbonNotificationBarType.Custom
            ? _notificationData.CustomForeColor
            : Color.Black;
    }

    private Color GetBorderColor()
    {
        if (_notificationData == null)
        {
            return Color.FromArgb(200, 200, 200);
        }

        return _notificationData.Type switch
        {
            RibbonNotificationBarType.Information => Color.FromArgb(91, 155, 213),
            RibbonNotificationBarType.Warning => Color.FromArgb(255, 192, 0),
            RibbonNotificationBarType.Error => Color.FromArgb(192, 0, 0),
            RibbonNotificationBarType.Success => Color.FromArgb(0, 192, 0),
            RibbonNotificationBarType.Custom => _notificationData.CustomBorderColor,
            _ => Color.FromArgb(200, 200, 200)
        };
    }

    private void OnNotificationDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        _layoutNeedsUpdate = true;
        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    private void OnButtonClick(object? sender, MouseEventArgs e)
    {
        if (sender is ButtonController controller)
        {
            var buttonInfo = _buttonViews.FirstOrDefault(bv => bv.Controller == controller);
            if (buttonInfo != null)
            {
                ButtonClick?.Invoke(this, new RibbonNotificationBarEventArgs(buttonInfo.IsCloseButton ? -1 : buttonInfo.Index));
            }
        }
    }
    #endregion

    #region Content Providers
    /// <summary>
    /// Provides icon content for the notification bar.
    /// </summary>
    private class NotificationIconContent : IContentValues
    {
        private readonly KryptonRibbonNotificationBarData _data;

        public NotificationIconContent(KryptonRibbonNotificationBarData data)
        {
            _data = data;
        }

        public bool HasContent => _data.ShowIcon && _data.Icon != null;

        public Image? GetImage(PaletteState state) => _data.Icon;

        public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        public string GetShortText() => string.Empty;

        public string GetLongText() => string.Empty;
    }

    /// <summary>
    /// Provides text content for the notification bar.
    /// </summary>
    private class NotificationTextContent : IContentValues
    {
        private readonly KryptonRibbonNotificationBarData _data;

        public NotificationTextContent(KryptonRibbonNotificationBarData data)
        {
            _data = data;
        }

        public bool HasContent => !string.IsNullOrEmpty(_data.Text) || !string.IsNullOrEmpty(_data.Title);

        public Image? GetImage(PaletteState state) => null;

        public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        public string GetShortText()
        {
            if (!string.IsNullOrEmpty(_data.Title))
            {
                return _data.Title + " " + (_data.Text ?? string.Empty);
            }
            return _data.Text ?? string.Empty;
        }

        public string GetLongText() => string.Empty; // Only use short text to prevent duplication
    }
    #endregion
}
