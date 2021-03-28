using System.Drawing;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Map quick access toolbar tooltip values to content values.
    /// </summary>
    internal class QATButtonToolTipToContent : IContentValues
    {
        #region Instance Fields
        private readonly IQuickAccessToolbarButton _qatButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the QATButtonToolTipToContent class.
        /// </summary>
        /// <param name="qatButton">Source quick access toolbar button.</param>
        public QATButtonToolTipToContent(IQuickAccessToolbarButton qatButton)
        {
            Debug.Assert(qatButton != null);
            _qatButton = qatButton;
        }
        #endregion

        #region HasContent
        /// <summary>
        /// Gets a value indicating if the mapping produces any content.
        /// </summary>
        public bool HasContent => (GetImage(PaletteState.Normal) != null) ||
                                  !string.IsNullOrEmpty(GetShortText()) ||
                                  !string.IsNullOrEmpty(GetLongText());

        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return _qatButton.GetToolTipImage();
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return _qatButton.GetToolTipImageTransparentColor();
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _qatButton.GetToolTipTitle();
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return _qatButton.GetToolTipBody();
        }
        #endregion
    }
}
