namespace Krypton.Toolkit
{
    /// <summary>
    /// Base class for the palette TMS storage classes to derive from.
    /// </summary>
    public abstract class KryptonPaletteTMSBase : Storage
    {
        #region Instance Fields

        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTBase class.
        /// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSBase(KryptonInternalKCT internalKCT,
                                       NeedPaintHandler needPaint)
        {
            Debug.Assert(internalKCT != null);

            InternalKCT = internalKCT;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the internal class used to inherit values.
        /// </summary>
        internal KryptonInternalKCT InternalKCT { get; }

        #endregion
    }
}
