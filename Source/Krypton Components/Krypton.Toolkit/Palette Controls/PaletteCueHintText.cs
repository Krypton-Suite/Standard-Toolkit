// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
// by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Initialize a new instance of the PaletteCueHintText class.
    /// </summary>
    public class PaletteCueHintText : PaletteInputControlContentStates
    {
        #region Identity
        internal PaletteRelativeAlign _shortTextV;

        /// <summary>
        /// Initialize a new instance of the PaletteCueHintText class.
        /// </summary>
        public PaletteCueHintText(PaletteRedirect redirect,
            NeedPaintHandler needPaint)
            : base(new PaletteContentInheritRedirect(redirect, PaletteContentStyle.InputControlStandalone), needPaint)
        {
            _padding = Padding.Empty;
            _shortTextV = PaletteRelativeAlign.Center;
            _shortTextH = PaletteRelativeAlign.Near;
        }

        #endregion

        [Category("Visuals")]
        [Description("Set a watermark/prompt message for the user.")]
        [RefreshProperties(RefreshProperties.All)]
        public string CueHintText { get; set; }

        private bool ShouldSerializeCueHintText() => !string.IsNullOrWhiteSpace(CueHintText);

        /// <summary>
        /// Resets the Image property to its default value.
        /// </summary>
        private void ResetCueHintText() => CueHintText = string.Empty;


        public override bool IsDefault =>
             (Font == null) &&
             (Color1 == Color.Empty) &&
             Padding.Equals(Padding.Empty)      // <- This is not the same as the base
             && (TextH == PaletteRelativeAlign.Near) // <- This is not the same as the base
             && string.IsNullOrWhiteSpace(CueHintText)
             && (_shortTextV == PaletteRelativeAlign.Center);

        /// <summary>
        /// Gets the actual content draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public new InheritBool GetContentDraw(PaletteState state) => string.IsNullOrWhiteSpace(CueHintText) ? InheritBool.True : InheritBool.False;

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextNewFont(PaletteState state)
        {
            if (Font != null)
            {
                return new Font(Font, Font.Style);
            }
            var font = Inherit.GetContentShortTextFont(state);
            return new Font(font, FontStyle.Italic);
        }

        /// <summary>
        /// Gets the first color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public new Color GetContentShortTextColor1(PaletteState state) => Color1 != Color.Empty ? Color1 : ControlPaint.Light(Inherit.GetContentShortTextColor1(state));

        internal void PerformPaint(VisualControlBase textBox, Graphics g, PI.RECT rect, SolidBrush backBrush)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            // Define the string formatting requirements
            var stringFormat = new StringFormat
            {
                Trimming = StringTrimming.None,
                LineAlignment = StringAlignment.Near
            };
            stringFormat.Alignment = GetContentShortTextH(PaletteState.Normal) switch
            {
                PaletteRelativeAlign.Near => textBox.RightToLeft == RightToLeft.Yes
                    ? StringAlignment.Far
                    : StringAlignment.Near,
                PaletteRelativeAlign.Far => textBox.RightToLeft == RightToLeft.Yes
                    ? StringAlignment.Near
                    : StringAlignment.Far,
                PaletteRelativeAlign.Center => StringAlignment.Center,
                _ => StringAlignment.Near
            };
            // This is most applicable to the multi-line controls
            stringFormat.LineAlignment = GetContentShortTextV(PaletteState.Normal) switch
            {
                PaletteRelativeAlign.Near => StringAlignment.Near,
                //PaletteRelativeAlign.Center => StringAlignment.Center,
                PaletteRelativeAlign.Far => StringAlignment.Far,
                _ => StringAlignment.Center
            };

            // Use the correct prefix setting
            stringFormat.HotkeyPrefix = HotkeyPrefix.None;

            Rectangle layoutRectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);

            // Draw entire client area in the background color
            g.FillRectangle(backBrush, layoutRectangle);

            var padding = GetContentPadding(PaletteState.Normal);
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                layoutRectangle.X += padding.Left;
                layoutRectangle.Y += padding.Top;
                layoutRectangle.Width -= padding.Left + padding.Right;
                layoutRectangle.Height -= padding.Top + padding.Bottom;
            }


            using Font font = GetContentShortTextNewFont(PaletteState.Normal);
            using SolidBrush foreBrush = new(GetContentShortTextColor1(PaletteState.Normal));
            var drawText = string.IsNullOrEmpty(CueHintText) ? textBox.Text : CueHintText;
            g.DrawString(drawText, font, foreBrush, layoutRectangle, stringFormat);
        }

        #region TextV
        /// <summary>
        /// Gets and sets the horizontal Content text alignment for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Relative Vertical Content text alignment")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(PaletteRelativeAlign.Center)]
        public PaletteRelativeAlign TextV
        {
            get => _shortTextV;

            set
            {
                if (value != _shortTextV)
                {
                    _shortTextV = value;
                    PerformNeedPaint();
                }
            }
        }

        private bool ShouldSerializeTextV() => _shortTextV != PaletteRelativeAlign.Center;

        private void ResetTextV() => _shortTextV = PaletteRelativeAlign.Center;

        /// <summary>
        /// Gets and sets the horizontal Content text alignment for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Relative horizontal Content text alignment")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(PaletteRelativeAlign.Near)]
        public override PaletteRelativeAlign TextH
        {
            get => _shortTextH;

            set
            {
                if (value != _shortTextH)
                {
                    _shortTextH = value;
                    PerformNeedPaint();
                }
            }
        }
        private bool ShouldSerializeTextH() => _shortTextH != PaletteRelativeAlign.Near;

        private void ResetTextH() => _shortTextH = PaletteRelativeAlign.Near;

        /// <summary>
        /// Gets the actual content short text vertical alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextV(PaletteState state) => _shortTextV != PaletteRelativeAlign.Inherit ? _shortTextH : Inherit.GetContentShortTextV(state);

        #endregion

        #region Padding
        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding between the border and content drawing.")]
        [DefaultValue(typeof(Padding), "0")]
        public new Padding Padding
        {
            get => _padding;

            set
            {
                if (!value.Equals(_padding))
                {
                    _padding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializePadding() => !_padding.Equals(Padding.Empty);

        private void ResetPadding() => _padding = Padding.Empty;

        // Use the base class
        //protected virtual Padding GetContentPadding(PaletteState state) => !_padding.Equals(CommonHelper.InheritPadding) ? _padding : Inherit.GetContentPadding(state);

        #endregion
    }
}
