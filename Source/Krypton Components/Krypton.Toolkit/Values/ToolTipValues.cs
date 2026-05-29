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
    /// Make sure default values are         
    /// Gets and sets the EnableToolTips
    /// </summary>
    [DefaultValue(false)]
    public bool EnableToolTips { get; set; }

    private bool ShouldSerializeEnableToolTips() => EnableToolTips;

    private void ResetEnableToolTips() => EnableToolTips = false;
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
    [Description("Hover interval (in millisecs) before a tooltip is shown\n[Currently ONLY designer values used]")]
    [DefaultValue(5000)]
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
    [Description(@"Interval (in millisecs) before a tooltip is closed. Use 0 for infinite.\n[Currently ONLY designer values used]")]
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
                                      && base.IsDefault
    ;
    #endregion

}