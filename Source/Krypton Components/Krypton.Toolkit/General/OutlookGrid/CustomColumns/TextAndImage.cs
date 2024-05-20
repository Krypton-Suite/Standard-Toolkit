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
    /// Class for TextAndImage object
    /// </summary>
    public class TextAndImage : IComparable<TextAndImage>
    {
        #region Public Fields

        /// <summary>
        /// The text
        /// </summary>
        public string Text;

        /// <summary>
        /// The image
        /// </summary>
        public Image? Image;

        #endregion

        #region Identity

        /// <summary>
        /// Constructor
        /// </summary>
        public TextAndImage()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="img">The image.</param>
        public TextAndImage(string text, Image? img)
        {
            Text = text;
            Image = img;
        }

        #endregion

        #region Public Overrides

        /// <summary>
        /// Overrides ToString
        /// </summary>
        /// <returns>String that represents TextAndImage</returns>
        public override string ToString() => Text;

        /// <summary>
        /// Overrides Equals
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>true if equal, false otherwise.</returns>
        public override bool Equals(object? obj) => Text.Equals(obj?.ToString());

        /// <summary>
        /// Overrides GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion

        #region Implementation

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(TextAndImage? other) => Text.CompareTo(other!.Text);

        #endregion
    }
}