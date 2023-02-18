﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Apply a requested text rendering hint to a graphics instance.
    /// </summary>
    public class GraphicsTextHint : GlobalId,
                                    IDisposable
    {
        #region Instance Fields
        private readonly Graphics? _graphics;
        private readonly TextRenderingHint _textHint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GraphicsSmooth class.
        /// </summary>
        /// <param name="graphics">Graphics context.</param>
        /// <param name="textHint">Temporary text rendering hint to apply.</param>
        public GraphicsTextHint(Graphics? graphics, TextRenderingHint textHint)
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
