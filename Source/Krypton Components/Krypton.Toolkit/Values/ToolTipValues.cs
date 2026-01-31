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
    private int _showIntervalDelay = 500;
    private int _closeIntervalDelay = 5000;
    private LabelStyle _toolTipStyle = LabelStyle.SuperTip;

    /// <summary>
    /// </summary>
    /// <param name="needPaint"></param>
    /// <param name="getDpiFactor"></param>
    public ToolTipValues(NeedPaintHandler? needPaint, GetDpiFactor getDpiFactor)
        : base(needPaint, getDpiFactor)
    {
        ResetToolTipStyle();
        ToolTipPosition = new PopupPositionValues();
    }

    /// <inheritdoc />
    protected override Image? GetImageDefault() => null;

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

            _showIntervalDelay = value;
            // TODO: Raise an event to cause the tooltipMgr to update !
        }
    }

    private bool ShouldSerializeShowIntervalDelay() => _showIntervalDelay != 500;

    private void ResetShowIntervalDelay() => ShowIntervalDelay = 500;
    #endregion

    #region CloseIntervalDelay
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Interval (in millisecs) before a tooltip is closed\n[Currently ONLY designer values used]")]
    [DefaultValue(5000)]
    public int CloseIntervalDelay
    {
        get => _closeIntervalDelay;
        set
        {
            // Cannot have an interval less than 1ms
            if (value < 0)
            {
                value = 1;
            }

            _closeIntervalDelay = value;
            // TODO: Raise an event to cause the tooltipMgr to update !
        }
    }

    private bool ShouldSerializeCloseIntervalDelay() => _closeIntervalDelay != 5000;

    private void ResetCloseIntervalDelay() => CloseIntervalDelay = 5000;
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