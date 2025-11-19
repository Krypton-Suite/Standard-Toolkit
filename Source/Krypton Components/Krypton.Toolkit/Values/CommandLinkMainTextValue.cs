#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class CommandLinkMainTextValue : NullContentValues
{
    #region Public

    /// <summary>Gets or sets the short text.</summary>
    /// <value>The short text.</value>
    public string ShortText { get; set; }

    #endregion

    #region Implementation

    /// <summary>Gets the content short text.</summary>
    /// <returns>String value.</returns>
    public override string GetShortText() => ShortText;

    #endregion
}