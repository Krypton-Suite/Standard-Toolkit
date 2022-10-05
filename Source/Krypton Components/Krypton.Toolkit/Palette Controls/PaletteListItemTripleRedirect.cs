namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for a a list item triple.
    /// </summary>
    public class PaletteListItemTripleRedirect : Storage                                            
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteListItemTripleRedirect class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="backStyle">Initial background style.</param>
        /// <param name="borderStyle">Initial border style.</param>
        /// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteListItemTripleRedirect(PaletteRedirect redirect,
                                             PaletteBackStyle backStyle,
                                             PaletteBorderStyle borderStyle,
                                             PaletteContentStyle contentStyle,
                                             NeedPaintHandler needPaint)
        {
            Debug.Assert(redirect != null);
            Item = new PaletteTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Item.IsDefault;

        #endregion

        #region Item
        /// <summary>
        /// Gets the item appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Item { get; }

        private bool ShouldSerializeItem() => !Item.IsDefault;

        #endregion
    }
}
