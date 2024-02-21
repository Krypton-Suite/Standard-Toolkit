#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Two scale color class parameters
    /// </summary>
    /// <seealso cref="IFormatParams" />
    public class TwoColorsParams : IFormatParams
    {
        #region Public Fields

        /// <summary>
        /// Minimum color
        /// </summary>
        public Color MinimumColor;
        /// <summary>
        /// Maximum color
        /// </summary>
        public Color MaximumColor;
        /// <summary>
        /// Color associated to the value between min and max color
        /// </summary>
        public Color ValueColor;

        #endregion

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoColorsParams"/> class.
        /// </summary>
        /// <param name="minColor">The minimum color.</param>
        /// <param name="maxColor">The maximum color.</param>
        public TwoColorsParams(Color minColor, Color maxColor)
        {
            MinimumColor = minColor;
            MaximumColor = maxColor;
        }

        #endregion

        #region Implementation

        /// <summary>Creates an object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Persists the parameters.
        /// </summary>
        /// <param name="writer">The XML writer.</param>
        public void Persist(XmlWriter writer)
        {
            writer.WriteElementString("MinimumColor", MinimumColor.ToArgb().ToString());
            writer.WriteElementString("MaximumColor", MaximumColor.ToArgb().ToString());
        }

        #endregion
    }
}