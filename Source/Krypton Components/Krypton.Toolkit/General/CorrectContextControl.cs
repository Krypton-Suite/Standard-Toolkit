﻿namespace Krypton.Toolkit
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
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
