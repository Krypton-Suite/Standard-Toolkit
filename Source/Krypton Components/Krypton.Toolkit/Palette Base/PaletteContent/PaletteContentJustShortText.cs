namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage but remove accesss to the non short text properties.
    /// </summary>
    public class PaletteContentJustShortText : PaletteContentJustText
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentJustShortText class.
        /// </summary>
        public PaletteContentJustShortText()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteContentJustShortText class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        public PaletteContentJustShortText(IPaletteContent inherit)
            : this(inherit, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteContentJustShortText class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteContentJustShortText(IPaletteContent inherit,
                                           NeedPaintHandler needPaint)
            : base(inherit, needPaint)
        {
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public override void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Draw = GetContentDraw(state);
            ShortText.Font = GetContentShortTextFont(state);
            ShortText.Hint = GetContentShortTextHint(state);
            ShortText.Prefix = GetContentShortTextPrefix(state);
            ShortText.Trim = GetContentShortTextTrim(state);
            ShortText.TextH = GetContentShortTextH(state);
            ShortText.TextV = GetContentShortTextV(state);
            ShortText.MultiLineH = GetContentShortTextMultiLineH(state);
            ShortText.MultiLine = GetContentShortTextMultiLine(state);
            ShortText.Color1 = GetContentShortTextColor1(state);
            ShortText.Color2 = GetContentShortTextColor2(state);
            ShortText.ColorStyle = GetContentShortTextColorStyle(state);
            ShortText.ColorAlign = GetContentShortTextColorAlign(state);
            ShortText.ColorAngle = GetContentShortTextColorAngle(state);
            ShortText.Image = GetContentShortTextImage(state);
            ShortText.ImageStyle = GetContentShortTextImageStyle(state);
            ShortText.ImageAlign = GetContentShortTextImageAlign(state);
            Padding = GetContentPadding(state);
        }
        #endregion

        #region LongText
        /// <summary>
        /// Gets access to the long text palette details.
        /// </summary>
        [KryptonPersist]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PaletteContentText LongText => base.LongText;

        #endregion

        #region AdjacentGap
        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        [KryptonPersist(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int AdjacentGap
        {
            get => base.AdjacentGap;
            set => base.AdjacentGap = value;
        }
        #endregion
    }
}
