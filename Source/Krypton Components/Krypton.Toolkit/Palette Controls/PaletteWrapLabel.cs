﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide wrap label state storage.
    /// </summary>
    public class PaletteWrapLabel : Storage
    {
        #region Instance Fields
        private Font _font;
        private Color _textColor;
        private PaletteTextHint _hint;
        private readonly KryptonWrapLabel _wrapLabel;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteWrapLabel class.
        /// </summary>
        /// <param name="wrapLabel">Reference to owning control.</param>
        public PaletteWrapLabel(KryptonWrapLabel wrapLabel)
        {
            _wrapLabel = wrapLabel;
            _font = null;
            _textColor = Color.Empty;
            _hint = PaletteTextHint.Inherit;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (_font == null) &&
                                          (_textColor == Color.Empty) &&
                                          (_hint == PaletteTextHint.Inherit);

        #endregion

        #region Font
        /// <summary>
        /// Gets the font for label text.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Font for drawing the label text.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font Font
        {
            get => _font;

            set
            {
                _font = value;
                _wrapLabel.PerformLayout();
                _wrapLabel.Invalidate();
            }
        }
        #endregion

        #region TextColor
        /// <summary>
        /// Gets and sets the color for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Color for the text.")]
        [KryptonDefaultColor()]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color TextColor
        {
            get => _textColor;

            set
            {
                _textColor = value;
                _wrapLabel.Invalidate();
            }
        }
        #endregion

        #region Hint
        /// <summary>
        /// Gets the text rendering hint for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Text rendering hint for the content text.")]
        [DefaultValue(typeof(PaletteTextHint), "Inherit")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual PaletteTextHint Hint
        {
            get => _hint;

            set
            {
                _hint = value;
                _wrapLabel.Invalidate();
            }
        }
        #endregion
    }
}
