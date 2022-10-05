namespace Krypton.Toolkit
{
    /// <summary>
    /// What will be Displayed in the designer
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public class ToolTipValues : HeaderValues
    {
        private LabelStyle _toolTipStyle;

        /// <summary>
        /// </summary>
        /// <param name="needPaint"></param>
        public ToolTipValues(NeedPaintHandler needPaint)
            : base(needPaint)
        {
            ResetToolTipStyle();
            ToolTipPosition = new PopupPositionValues();
        }

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
        }

        /// <summary>
        /// Make sure default values are         
        /// Gets and sets the EnableToolTips
        /// </summary>
        [DefaultValue(false)]
        public bool EnableToolTips { get; set; }

        private bool ShouldSerializeEnableToolTips() => EnableToolTips;

        /// <inheritdoc />
        protected override Image GetImageDefault() => null;

        /// <summary>
        /// 
        /// </summary>
        public void ResetEnableToolTips()
        {
            EnableToolTips = false;
        }

        #region ToolTipShadow
        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        [Category(@"ToolTip")]
        [Description(@"Button tooltip Shadow.")]
        [DefaultValue(true)]
        public bool ToolTipShadow { get; set; } = true; // Backward compatible -> "Material Design" suggests this to be false

        private bool ShouldSerializeToolTipShadow() => !ToolTipShadow;

        private void ResetToolTipShadow()
        {
            ToolTipShadow = true;
        }
        #endregion

        /// <summary>
        /// Gets and sets the EnableToolTips
        /// </summary>
        [Description(@"The orientation of the ToolTip control when it opens, and specifies how the ToolTip control behaves when it overlaps screen boundaries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PopupPositionValues ToolTipPosition { get; set; }

        private bool ShouldSerializeToolTipPosition() => !ToolTipPosition.IsDefault;

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetToolTipPosition() => ToolTipPosition.Reset();

        #region ToolTipStyle

        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        [Description(@"Button tooltip label style.")]
        [DefaultValue(typeof(LabelStyle), "SuperTip")]
        public LabelStyle ToolTipStyle
        {
            get => _toolTipStyle;
            set => _toolTipStyle = value;
        }

        private bool ShouldSerializeToolTipStyle() => ToolTipStyle != LabelStyle.SuperTip;

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetToolTipStyle()
        {
            ToolTipStyle = LabelStyle.SuperTip;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => !ShouldSerializeEnableToolTips()
                                           && !ShouldSerializeToolTipStyle()
                                           && !ShouldSerializeToolTipPosition()
                                           && base.IsDefault
            ;


        #endregion

    }
}
