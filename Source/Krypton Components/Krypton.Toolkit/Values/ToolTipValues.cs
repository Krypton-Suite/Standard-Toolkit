#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// What will be Displayed in the designer
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToolTipValues : HeaderValues
{
    #region Instance Fields

    private int _showIntervalDelay = 500;
    private int _closeIntervalDelay = 5000;
    private LabelStyle _toolTipStyle = LabelStyle.SuperTip;
    private Control? _hostedContent;

    #endregion

    #region Events

    /// <summary>Raised when <see cref="ShowIntervalDelay"/> changes.</summary>
    public event EventHandler? ShowIntervalDelayChanged;

    /// <summary>Raised when <see cref="CloseIntervalDelay"/> changes.</summary>
    public event EventHandler? CloseIntervalDelayChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the ToolTipValues class with the specified paint notification handler and DPI factor provider.
    /// </summary>
    /// <param name="needPaint">A delegate used to notify when a repaint is required. Can be null if paint notifications are not needed.</param>
    /// <param name="getDpiFactor">A delegate that provides the current DPI scaling factor for rendering.</param>
    public ToolTipValues(NeedPaintHandler? needPaint, GetDpiFactor getDpiFactor)
        : base(needPaint, getDpiFactor)
    {
        ResetToolTipStyle();
        ToolTipPosition = new PopupPositionValues();
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override Image? GetImageDefault() => null;

    #endregion

    #region Public

    #region EnableToolTips
    /// <summary>
    /// Gets and sets the EnableToolTips
    /// </summary>
    [DefaultValue(false)]
    public bool EnableToolTips { get; set; }

    private bool _defaultEnableToolTips;

    private bool ShouldSerializeEnableToolTips() => EnableToolTips != _defaultEnableToolTips;

    private void ResetEnableToolTips() => EnableToolTips = _defaultEnableToolTips;

    /// <summary>
    /// Treats <paramref name="value"/> as the unset designer default for <see cref="EnableToolTips"/>.
    /// </summary>
    internal void SetDefaultEnableToolTips(bool value)
    {
        _defaultEnableToolTips = value;
        EnableToolTips = value;
    }
    #endregion

    #region ToolTipShadow
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Button tooltip Shadow.")]
    [DefaultValue(true)]
    public bool ToolTipShadow { get; set; } = true; // Backward compatible -> "Material Design" suggests this to be false

    private bool ShouldSerializeToolTipShadow() => !ToolTipShadow;

    private void ResetToolTipShadow() => ToolTipShadow = true;

    #endregion

    #region ToolTipPosition
    /// <summary>
    /// Gets and sets the EnableToolTips
    /// </summary>
    [Description(@"The orientation of the ToolTip control when it opens, and specifies how the ToolTip control behaves when it overlaps screen boundaries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PopupPositionValues ToolTipPosition { get; set; }

    private bool ShouldSerializeToolTipPosition() => !ToolTipPosition.IsDefault;

    private void ResetToolTipPosition() => ToolTipPosition.Reset();
    #endregion
        
    #region ToolTipStyle

    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Description(@"Button tooltip label style.")]
    [DefaultValue(LabelStyle.SuperTip)]
    public LabelStyle ToolTipStyle
    {
        get => _toolTipStyle;
        set => _toolTipStyle = value;
    }

    private bool ShouldSerializeToolTipStyle() => ToolTipStyle != LabelStyle.SuperTip;

    private void ResetToolTipStyle() => ToolTipStyle = LabelStyle.SuperTip;
    #endregion

    #region ShowIntervalDelay
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Hover interval (in millisecs) before a tooltip is shown\n[Currently ONLY designer values used]")]
    [DefaultValue(500)]
    public int ShowIntervalDelay
    {
        get => _showIntervalDelay;
        set
        {
            // Cannot have an interval less than 1ms
            if (value < 0)
            {
                value = 1;
            }

            if (_showIntervalDelay != value)
            {
                _showIntervalDelay = value;

                ShowIntervalDelayChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeShowIntervalDelay() => _showIntervalDelay != 500;

    private void ResetShowIntervalDelay() => ShowIntervalDelay = 500;
    #endregion

    #region CloseIntervalDelay

    /// <summary>
    /// Gets and sets the interval (in milliseconds) before a tooltip is closed.
    /// Use 0 for infinite display (tooltip stays until the pointer leaves the control).
    /// </summary>
    [Category(@"ToolTip")]
    [Description("Interval (in millisecs) before a tooltip is closed. Use 0 for infinite.\n[Currently ONLY designer values used]")]
    [DefaultValue(5000)]
    public int CloseIntervalDelay
    {
        get => _closeIntervalDelay;
        set
        {
            // 0 = infinite; negative values are clamped to 0
            if (value < 0)
            {
                value = 0;
            }

            if (_closeIntervalDelay != value)
            {
                _closeIntervalDelay = value;

                CloseIntervalDelayChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeCloseIntervalDelay() => _closeIntervalDelay != 5000;

    private void ResetCloseIntervalDelay() => CloseIntervalDelay = 5000;

    #endregion

    #region HostedContent
    /// <summary>
    /// Optional control shown inside the tooltip chrome (interactive). Not designer-serialized.
    /// Cannot be a <see cref="Form"/>. The tooltip unparents the control while shown and does not dispose it.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    public Control? HostedContent
    {
        get => _hostedContent;
        set
        {
            if (value is Form)
            {
                ThrowHelper.ThrowArgumentException(@"A Form cannot be hosted inside a tooltip.", nameof(value));
            }

            _hostedContent = value;
        }
    }

    private bool ShouldSerializeHostedContent() => HostedContent != null;

    private void ResetHostedContent() => HostedContent = null;
    #endregion

    #region EnableInteractiveKeyboard
    /// <summary>
    /// When true, an interactive hosted tooltip receives keyboard input (Escape dismisses). Default is false so hover does not steal typing.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, hosted-control tooltips receive keyboard input (Escape dismisses).")]
    [DefaultValue(false)]
    public bool EnableInteractiveKeyboard { get; set; }

    private bool ShouldSerializeEnableInteractiveKeyboard() => EnableInteractiveKeyboard;

    private void ResetEnableInteractiveKeyboard() => EnableInteractiveKeyboard = false;
    #endregion

    #region UseCloseTimerForInteractive
    /// <summary>
    /// When true, <see cref="CloseIntervalDelay"/> also applies to hosted-control tooltips. Default is false (stay until leave or click-away).
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, CloseIntervalDelay applies to interactive hosted-control tooltips.")]
    [DefaultValue(false)]
    public bool UseCloseTimerForInteractive { get; set; }

    private bool ShouldSerializeUseCloseTimerForInteractive() => UseCloseTimerForInteractive;

    private void ResetUseCloseTimerForInteractive() => UseCloseTimerForInteractive = false;
    #endregion

    #region DismissInteractiveOnTargetMouseDown
    /// <summary>
    /// When true, mouse-down on the hover target dismisses an interactive tooltip. Default is false.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, clicking the hover target dismisses an interactive tooltip.")]
    [DefaultValue(false)]
    public bool DismissInteractiveOnTargetMouseDown { get; set; }

    private bool ShouldSerializeDismissInteractiveOnTargetMouseDown() => DismissInteractiveOnTargetMouseDown;

    private void ResetDismissInteractiveOnTargetMouseDown() => DismissInteractiveOnTargetMouseDown = false;
    #endregion

    #endregion

    #region IsDefault
    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        ResetEnableToolTips();
        ResetToolTipStyle();
        ResetToolTipPosition();
        ResetImage();
        ResetImageTransparentColor();
        ResetHeading();
        ResetDescription();
        ResetShowIntervalDelay();
        ResetCloseIntervalDelay();
        ResetHostedContent();
        ResetEnableInteractiveKeyboard();
        ResetUseCloseTimerForInteractive();
        ResetDismissInteractiveOnTargetMouseDown();
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeEnableToolTips()
                                      && !ShouldSerializeToolTipStyle()
                                      && !ShouldSerializeToolTipPosition()
                                      && !ShouldSerializeShowIntervalDelay()
                                      && !ShouldSerializeCloseIntervalDelay()
                                      && !ShouldSerializeHostedContent()
                                      && !ShouldSerializeEnableInteractiveKeyboard()
                                      && !ShouldSerializeUseCloseTimerForInteractive()
                                      && !ShouldSerializeDismissInteractiveOnTargetMouseDown()
                                      && base.IsDefault
    ;
    #endregion

}