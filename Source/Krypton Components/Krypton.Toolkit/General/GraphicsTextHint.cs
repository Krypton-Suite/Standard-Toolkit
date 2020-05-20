// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Drawing;
using System.Drawing.Text;

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
        }
        #endregion
    }
}
