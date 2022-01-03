#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


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
