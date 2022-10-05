namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for GroupBox states.
    /// </summary>
    public class PaletteGroupBox : PaletteDouble
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteGroupBox class.
        /// </summary>
        /// <param name="inherit">Source for inheriting palette defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteGroupBox(PaletteGroupBoxRedirect inherit,
                               NeedPaintHandler needPaint)
            : base(inherit, needPaint) =>
            Content = new PaletteContent(inherit.PaletteContent, needPaint);

        #endregion

        #region Content
        /// <summary>
        /// Gets access to the content palette details.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining content appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent Content { get; }

        private bool ShouldSerializeContent() => !Content.IsDefault;

        /// <summary>
        /// Gets the content palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPaletteContent PaletteContent => Content;

        #endregion
    }
}
