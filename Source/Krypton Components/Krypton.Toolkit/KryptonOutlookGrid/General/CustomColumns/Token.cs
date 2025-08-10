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
    /// Token object
    /// </summary>
    public class Token : IComparable<Token>
    {
        #region Identity

        /// <summary>
        /// Default constructor
        /// </summary>
        public Token()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text of the token</param>
        /// <param name="bg">Background color</param>
        /// <param name="fg">Foreground text color</param>
        public Token(string text, Color bg, Color fg)
        {
            Text = text;

            BackColor = bg;

            ForeColor = fg;
        }

        #endregion

        #region Public

        /// <summary>
        /// Text of the token
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Background color
        /// </summary>
        public Color BackColor { get; set; }
        /// <summary>
        /// Foreground text color
        /// </summary>
        public Color ForeColor { get; set; }

        #endregion

        #region Implementation

        /// <summary>
        /// Compare a Token to another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Token? other) => Text.CompareTo(other!.Text);

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
    }
}