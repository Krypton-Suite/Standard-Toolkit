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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class DataGridViewColumnSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewColumnSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public DataGridViewColumnSpecCollection(object owner)
            : base(owner)
        {
        }
        #endregion
    }
}
