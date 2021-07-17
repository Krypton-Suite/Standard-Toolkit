// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
// by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide wrap label state storage.
    /// </summary>
    public class PaletteCueHintText : PaletteInputControlContentStates
    {
        #region Identity

        /// <summary>
        /// Initialize a new instance of the PaletteCueHintText class.
        /// </summary>
        public PaletteCueHintText(PaletteRedirect redirect,
            NeedPaintHandler needPaint)
            : base(new PaletteContentInheritRedirect(redirect, PaletteContentStyle.InputControlStandalone), needPaint)
        {
        }

        #endregion

        [Category("Visuals")]
        [Description("Set a watermark/prompt message for the user.")]
        [RefreshProperties(RefreshProperties.All)]
        public string CueHintText { get; set; }

        public override bool IsDefault =>
            base.IsDefault &&
            MissingFrameWorkAPIs.IsNullOrWhiteSpace(CueHintText);

        /// <summary>
        /// Gets the actual content draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public new InheritBool GetContentDraw(PaletteState state)
        {
            return MissingFrameWorkAPIs.IsNullOrWhiteSpace(CueHintText) ? InheritBool.True : InheritBool.False;
        }

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
        public new Color GetContentShortTextColor1(PaletteState state)
        {
            return Color1 != Color.Empty ? Color1 : ControlPaint.Light(Inherit.GetContentShortTextColor1(state));
        }

        internal void PerformPaint(VisualControlBase textBox, Graphics g, PI.RECT rect)
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
                _ => stringFormat.Alignment
            };

            // Use the correct prefix setting
            stringFormat.HotkeyPrefix = HotkeyPrefix.None;
            using Font font = GetContentShortTextNewFont(PaletteState.Normal);
            using SolidBrush foreBrush = new (GetContentShortTextColor1(PaletteState.Normal));
            var drawText = string.IsNullOrEmpty(CueHintText) ? textBox.Text : CueHintText;
            RectangleF layoutRectangle = new(rect.left, rect.top, rect.right - rect.left,
                rect.bottom - rect.top);
            var padding = GetContentPadding(PaletteState.Normal);
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                layoutRectangle.X += padding.Left;
                layoutRectangle.Y += padding.Top;
                layoutRectangle.Width += padding.Right;
                layoutRectangle.Height += padding.Bottom;
            }

            g.DrawString(drawText, font, foreBrush, layoutRectangle, stringFormat);


        }
    }
}
