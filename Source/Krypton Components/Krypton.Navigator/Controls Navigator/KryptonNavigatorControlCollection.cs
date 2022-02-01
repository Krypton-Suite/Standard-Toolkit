#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Navigator
{
    /// <summary>
    /// Represents a collection of child controls for the navigator.
    /// </summary>
    public class KryptonNavigatorControlCollection : KryptonControlCollection
    {
        #region Instance Fields
        private readonly KryptonNavigator _owner;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonNavigatorControlCollection class.
        /// </summary>
        /// <param name="owner">Control containing this collection.</param>
        public KryptonNavigatorControlCollection(KryptonNavigator owner)
            : base(owner)
        {
            Debug.Assert(owner != null);

            // Remember the collection owner
            _owner = owner;
        }
        #endregion

        #region Public Overrides
        /// <summary>
        /// Adds the specified control to the control collection.
        /// </summary>
        /// <param name="value">The KryptonPage to add to the control collection.</param>
        public override void Add(Control value)
        {
            Debug.Assert(value != null);

            // Cast to correct type
            KryptonPage page = (KryptonPage)value;

            // We only allow KryptonPage controls to be added
            if (page == null)
            {
                throw new ArgumentException("Only KryptonPage controls can be added.", nameof(value));
            }

            // Let base class perform actual add
            base.Add(value);
        }
        #endregion
    }
}
