namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for back, border and tree node triple.
    /// </summary>
    public class PaletteTreeStateRedirect : PaletteDoubleRedirect
                                            
    {
        #region Instance Fields
        private readonly PaletteRedirect _redirect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTreeStateRedirect class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="back">Storage for back values.</param>
        /// <param name="backInherit">inheritance for back values.</param>
        /// <param name="border">Storage for border values.</param>
        /// <param name="borderInherit">inheritance for border values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTreeStateRedirect(PaletteRedirect redirect,
                                        PaletteBack back,
                                        PaletteBackInheritRedirect backInherit,
                                        PaletteBorder border,
                                        PaletteBorderInheritRedirect borderInherit,
                                        NeedPaintHandler needPaint)
            : base(redirect, back, backInherit, border, borderInherit, needPaint)
        {
            Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;

            // Create the item redirector
            Node = new PaletteTripleRedirect(redirect,
                                                      PaletteBackStyle.ButtonListItem,
                                                      PaletteBorderStyle.ButtonListItem,
                                                      PaletteContentStyle.ButtonListItem,
                                                      needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => base.IsDefault && Node.IsDefault;

        #endregion

        #region Node
        /// <summary>
        /// Gets the node appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining node appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Node { get; }

        private bool ShouldSerializeNode() => !Node.IsDefault;

        #endregion
    }
}
