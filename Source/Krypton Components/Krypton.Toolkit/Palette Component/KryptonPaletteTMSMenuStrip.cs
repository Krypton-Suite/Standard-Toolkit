﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for menu strip entries of the professional color table.
    /// </summary>
    public class KryptonPaletteTMSMenuStrip : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTMenuStrip class.
        /// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSMenuStrip(KryptonInternalKCT internalKCT,
                                            NeedPaintHandler needPaint)
            : base(internalKCT, needPaint)
        {
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (InternalKCT.InternalMenuStripText == Color.Empty) &&
                                          (InternalKCT.InternalMenuStripFont == null) &&
                                          (InternalKCT.InternalMenuStripGradientBegin == Color.Empty) &&
                                          (InternalKCT.InternalMenuStripGradientEnd == Color.Empty);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            MenuStripText = InternalKCT.MenuStripText;
            MenuStripFont = InternalKCT.MenuStripFont;
            MenuStripGradientBegin = InternalKCT.MenuStripGradientBegin;
            MenuStripGradientEnd = InternalKCT.MenuStripGradientEnd;
        }
        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets and sets the color to draw text on the menu strip.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"ToolMenuStatus")]
        [Description(@"Color to draw text on the MenuStrip.")]
        [KryptonDefaultColor()]
        public Color MenuStripText
        {
            get => InternalKCT.InternalMenuStripText;

            set
            {
                InternalKCT.InternalMenuStripText = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuStripText property to its default value.
        /// </summary>
        public void ResetMenuStripText()
        {
            MenuStripText = Color.Empty;
        }
        #endregion

        #region MenuStripFont
        /// <summary>
        /// Gets and sets the font to draw text on the menu strip.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"ToolMenuStatus")]
        [Description(@"Font to draw text on the MenuStrip.")]
        [DefaultValue(null)]
        public Font MenuStripFont
        {
            get => InternalKCT.InternalMenuStripFont;

            set
            {
                InternalKCT.InternalMenuStripFont = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuStripFont property to its default value.
        /// </summary>
        public void ResetMenuStripFont()
        {
            MenuStripFont = null;
        }
        #endregion

        #region MenuStripGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the MenuStrip..
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"ToolMenuStatus")]
        [Description(@"Starting color of the gradient used in the MenuStrip.")]
        [KryptonDefaultColor()]
        public Color MenuStripGradientBegin
        {
            get => InternalKCT.InternalMenuStripGradientBegin;

            set 
            { 
                InternalKCT.InternalMenuStripGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuStripGradientBegin property to its default value.
        /// </summary>
        public void ResetMenuStripGradientBegin()
        {
            MenuStripGradientBegin = Color.Empty;
        }
        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the MenuStrip..
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"ToolMenuStatus")]
        [Description(@"Ending color of the gradient used in the MenuStrip.")]
        [KryptonDefaultColor()]
        public Color MenuStripGradientEnd
        {
            get => InternalKCT.InternalMenuStripGradientEnd;

            set 
            { 
                InternalKCT.InternalMenuStripGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuStripGradientEnd property to its default value.
        /// </summary>
        public void ResetMenuStripGradientEnd()
        {
            MenuStripGradientEnd = Color.Empty;
        }
        #endregion
    }
}
