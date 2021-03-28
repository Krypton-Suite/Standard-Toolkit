using System.ComponentModel;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Base class for krypton specific control collections.
    /// </summary>
    public class KryptonControlCollection : Control.ControlCollection
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonControlCollection class.
        /// </summary>
        /// <param name="owner">Owning control.</param>
        public KryptonControlCollection(Control owner)
            : base(owner)
        {
        }
        #endregion

        #region AddInternal
        /// <summary>
        /// Add a control to the collection overriding the normal checks.
        /// </summary>
        /// <param name="control">Control to be added.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddInternal(Control control)
        {
            // ReSharper disable RedundantBaseQualifier
            // Do not remove base, as the KryptonReadOnlyControls is a mess !
            base.Add(control);
            // ReSharper restore RedundantBaseQualifier
        }
        #endregion

        #region RemoveInternal
        /// <summary>
        /// Add a control to the collection overriding the normal checks.
        /// </summary>
        /// <param name="control">Control to be added.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveInternal(Control control)
        {
            // ReSharper disable RedundantBaseQualifier
            // Do not remove base, as the KryptonReadOnlyControls is a mess !
            base.Remove(control);
            // ReSharper restore RedundantBaseQualifier
        }
        #endregion

        #region ClearInternal
        /// <summary>
        /// Clear out all the entries in the collection.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ClearInternal()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                RemoveInternal(this[i]);
            }
        }
        #endregion
    }
}
