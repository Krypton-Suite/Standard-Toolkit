using System;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Event argument data for a data grid view buttons spec.
    /// </summary>
    public class DataGridViewButtonSpecClickEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewButtonSpecClickEventArgs class.
        /// </summary>
        /// <param name="column">Reference to data grid view column.</param>
        /// <param name="cell">Reference to data grid view cell.</param>
        /// <param name="buttonSpec">Reference to button spec.</param>
        public DataGridViewButtonSpecClickEventArgs(DataGridViewColumn column,
            DataGridViewCell cell,
            ButtonSpecAny buttonSpec)
        {
            Column = column;
            Cell = cell;
            ButtonSpec = buttonSpec;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the column associated with the button spec.
        /// </summary>
        public DataGridViewColumn Column { get; }

        /// <summary>
        /// Gets a reference to the cell that generated the click event.
        /// </summary>
        public DataGridViewCell Cell { get; }

        /// <summary>
        /// Gets a reference to the button spec that is performing the click.
        /// </summary>
        public ButtonSpecAny ButtonSpec { get; }

        #endregion
    }
}