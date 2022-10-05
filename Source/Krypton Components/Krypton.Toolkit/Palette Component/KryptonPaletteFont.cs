namespace Krypton.Toolkit
{
    /// <summary>Storage of user supplied font values, not used by Krypton.</summary>
    public class KryptonPaletteFont : Storage
    {
        #region Instance Fields

        private KryptonPaletteCommon _paletteCommon;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonPaletteFont" /> class.</summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteFont(PaletteRedirect redirector, NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;

            Debug.Assert(redirector != null);
        }

        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (CommonLongTextFont == new Font("Segoe UI", 9f)) && (CommonShortTextFont == new Font("Segoe UI", 9f));

        #endregion

        #region CommonLongTextFont
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"User supplied font value.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font CommonLongTextFont { get => null; set => _paletteCommon.StateCommon.Content.LongText.Font = value; }
        /// <summary>
        /// Resets the CommonLongTextFont property to its default value.
        /// </summary>
        public void ResetCommonLongTextFont() => CommonLongTextFont = new Font("Segoe UI", 9f);

        #endregion

        #region CommonShortTextFont
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"User supplied font value.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font CommonShortTextFont { get => null; set => _paletteCommon.StateCommon.Content.ShortText.Font = value; }

        /// <summary>
        /// Resets the CommonShortTextFont property to its default value.
        /// </summary>
        public void ResetCommonShortTextFont() => CommonShortTextFont = new Font("Segoe UI", 9f);

        #endregion
    }
}
