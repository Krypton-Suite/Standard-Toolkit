#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege & Ahmed Abdelhameed et al. 2024 - 2025. All rights reserved.
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
    /// Parameters for Bar formatting
    /// </summary>
    /// <seealso cref="IKryptonOutlookGridFormatParams" />
    public class KryptonOutlookGridBarParams : IKryptonOutlookGridFormatParams
    {
        #region Public Fields

        /// <summary>
        /// The bar color
        /// </summary>
        public Color BarColor;

        /// <summary>
        /// The gradient fill
        /// </summary>
        public bool GradientFill;

        /// <summary>
        /// The proportion value
        /// </summary>
        public double ProportionValue;

        #endregion

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="KryptonOutlookGridBarParams"/> class.
        /// </summary>
        /// <param name="barColor">Color of the bar.</param>
        /// <param name="gradientFill">if set to <c>true</c> [gradient fill].</param>
        public KryptonOutlookGridBarParams(Color barColor, bool gradientFill)
        {
            BarColor = barColor;
            GradientFill = gradientFill;
        }

        #endregion

        #region Implementation

        /// <summary>Creates an object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() => MemberwiseClone();

        /// <summary>
        /// Persists the parameters.
        /// </summary>
        /// <param name="writer">The XML writer.</param>
        void IKryptonOutlookGridFormatParams.Persist(XmlWriter writer)
        {
            writer.WriteElementString("BarColor", BarColor.ToArgb().ToString());
            writer.WriteElementString("GradientFill", CommonHelper.BoolToString(GradientFill));
        }

        #endregion
    }
}