namespace Krypton.Toolkit
{
    /// <summary>
    /// Apply a requested text rendering hint to a graphics instance.
    /// </summary>
    public class GraphicsTextHint : GlobalId,
                                    IDisposable
    {
        #region Instance Fields
        private readonly Graphics _graphics;
        private readonly TextRenderingHint _textHint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GraphicsSmooth class.
        /// </summary>
        /// <param name="graphics">Graphics context.</param>
        /// <param name="textHint">Temporary text rendering hint to apply.</param>
        public GraphicsTextHint(Graphics graphics, TextRenderingHint textHint)
        {
            // Cache graphics instance
            _graphics = graphics;

            // Remember current text hint
            _textHint = _graphics.TextRenderingHint;

            // Apply new text hint
            _graphics.TextRenderingHint = textHint;
        }

        /// <summary>
        /// Reverse the text hint change.
        /// </summary>
        public void Dispose()
        {
            if (_graphics != null)
            {
                try
                {
                    // Put back to the original text hint
                    _graphics.TextRenderingHint = _textHint;
                }
                catch { }
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
