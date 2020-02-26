// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Temporary setup of the provided control in the context.
    /// </summary>
    public class CorrectContextControl : IDisposable
    {
        #region Instance Fields
        private readonly ViewLayoutContext _context;
        private readonly Control _startControl;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CorrectContextControl class.
        /// </summary>
        /// <param name="context">Context to update.</param>
        /// <param name="control">Actual parent control instance.</param>
        public CorrectContextControl(ViewLayoutContext context,
                                     Control control)
        {
            Debug.Assert(context != null);

            // Remmeber incoming context
            _context = context;

            // Remember staring setting
            _startControl = context.Control;

            // Update with correct control
            _context.Control = control;
        }

        /// <summary>
        /// Cleanup settings.
        /// </summary>
        public void Dispose()
        {
            // Put back the original setting
            _context.Control = _startControl;
        }
        #endregion
    }
}
