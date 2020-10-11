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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Attribute that marks properties for persistence inside the Krypton palette.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class KryptonPersistAttribute : Attribute
    {
        // Instance fields
        private bool _navigate;
        private bool _populate;

        /// <summary>
        /// Initialize a new instance of the KryptonPersistAttribute class.
        /// </summary>
        public KryptonPersistAttribute()
            : this(true, true)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonPersistAttribute class.
        /// </summary>
        /// <param name="navigate">Should persistence navigate down the property.</param>
        public KryptonPersistAttribute(bool navigate)
            : this(navigate, true)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonPersistAttribute class.
        /// </summary>
        /// <param name="navigate">Should persistence navigate down the property.</param>
        /// <param name="populate">Should property be reset as part of a populate operation.</param>
        public KryptonPersistAttribute(bool navigate, bool populate)
        {
            _navigate = navigate;
            _populate = populate;
        }

        /// <summary>
        /// Gets and sets a value indicating if the persistence should navigate the property.
        /// </summary>
        public bool Navigate
        {
            get => _navigate;
            set => _navigate = value;
        }

        /// <summary>
        /// Gets and sets a value indicating if the property should be reset as part of a populate operation.
        /// </summary>
        public bool Populate
        {
            get => _populate;
            set => _populate = value;
        }
    }
}
