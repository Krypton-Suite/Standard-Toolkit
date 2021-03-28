using System;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details about the context menu that has been closed up on a KryptonDateTimePicker.
    /// </summary>
    public class DateTimePickerCloseArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DateTimePickerCloseArgs class.
        /// </summary>
        /// <param name="kcm">KryptonContextMenu that can be examined.</param>
        public DateTimePickerCloseArgs(KryptonContextMenu kcm)
        {
            KryptonContextMenu = kcm;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the KryptonContextMenu instance.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
