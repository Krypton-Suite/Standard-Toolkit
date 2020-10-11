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
