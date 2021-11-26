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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for standard header storage.
    /// </summary>
    public class HeaderValues : HeaderValuesBase
    {
        #region Static Fields
        private const string _defaultHeading = "Heading";
        private const string _defaultDescription = "Description";
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public HeaderValues(NeedPaintHandler needPaint)
            : base(needPaint)
        {
        }
        #endregion

        #region Default Values
        /// <summary>
        /// Gets the default heading value.
        /// </summary>
        /// <returns>String reference.</returns>
        protected override string GetHeadingDefault() => _defaultHeading;

        /// <summary>
        /// Gets the default description value.
        /// </summary>
        /// <returns>String reference.</returns>
        protected override string GetDescriptionDefault() => _defaultDescription;

        #endregion
    }
}
