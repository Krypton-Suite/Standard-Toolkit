﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// KryptonHeaderGroup specific implementation of a button specification.
    /// </summary>
    public class ButtonSpecHeaderGroup : ButtonSpecAny
    {
        #region Instance Fields
        private HeaderLocation _location;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderGroupButtonSpec class.
        /// </summary>
        public ButtonSpecHeaderGroup()
        {
            _location = HeaderLocation.PrimaryHeader;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault &&
                                           (HeaderLocation == HeaderLocation.PrimaryHeader));

        #endregion

        #region HeaderLocation
        /// <summary>
        /// Gets and sets if the button header location.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Defines header location for the button.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(HeaderLocation), "PrimaryHeader")]
        public HeaderLocation HeaderLocation
        {
            get => _location;

            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnButtonSpecPropertyChanged(@"Location");
                }
            }
        }

        /// <summary>
        /// Resets the HeaderLocation property to its default value.
        /// </summary>
        public void ResetHeaderLocation()
        {
            HeaderLocation = HeaderLocation.PrimaryHeader;
        }
        #endregion

        #region CopyFrom
        /// <summary>
        /// Value copy form the provided source to our self.
        /// </summary>
        /// <param name="source">Source instance.</param>
        public void CopyFrom(ButtonSpecHeaderGroup source)
        {
            // Copy class specific values
            HeaderLocation = source.HeaderLocation;

            // Let base class copy the base values
            base.CopyFrom(source);
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button location value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button location.</returns>
        public override HeaderLocation GetLocation(IPalette palette)
        {
            return HeaderLocation;
        }
        #endregion
    }
}
