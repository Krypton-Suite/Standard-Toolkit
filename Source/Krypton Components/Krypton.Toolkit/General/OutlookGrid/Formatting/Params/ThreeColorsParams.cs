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
    /// Three scale color class parameters
    /// </summary>
    /// <seealso cref="IFormatParams" />
    public class ThreeColorsParams : IFormatParams
    {
        #region Public Fields

        /// <summary>
        /// The minimum color
        /// </summary>
        public Color MinimumColor;

        /// <summary>
        /// The medium color
        /// </summary>
        public Color MediumColor;

        /// <summary>
        /// The maximum color
        /// </summary>
        public Color MaximumColor;

        /// <summary>
        /// The color associated to the value
        /// </summary>
        public Color ValueColor;

        #endregion

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreeColorsParams"/> class.
        /// </summary>
        /// <param name="minColor">The minimum color.</param>
        /// <param name="mediumColor">Color of the medium.</param>
        /// <param name="maxColor">The maximum color.</param>
        public ThreeColorsParams(Color minColor, Color mediumColor, Color maxColor)
        {
            MinimumColor = minColor;
            MediumColor = mediumColor;
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
        void IFormatParams.Persist(XmlWriter writer)
        {
            writer.WriteElementString("MinimumColor", MinimumColor.ToArgb().ToString());
            writer.WriteElementString("MediumColor", MediumColor.ToArgb().ToString());
            writer.WriteElementString("MaximumColor", MaximumColor.ToArgb().ToString());
        }

        #endregion
    }
}