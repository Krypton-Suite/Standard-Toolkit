#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class CommandLinkSubscriptTextValue : NullContentValues
{
    #region Public

    /// <summary>Gets or sets the long text.</summary>
    /// <value>The long text.</value>
    public string LongText { get; set; }

    #endregion

    #region Implementation

    /// <summary>Gets the content long text.</summary>
    /// <returns>String value.</returns>
    public override string GetLongText() => LongText;

    #endregion
}