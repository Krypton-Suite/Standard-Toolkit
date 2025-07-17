#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for the group box caption values.
/// </summary>
public class CaptionValues : HeaderValuesBase
{
    #region Static Fields
    private const string DEFAULT_TEXT = "Caption";
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the CaptionValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="getDpiFactor"></param>
    public CaptionValues(NeedPaintHandler needPaint, GetDpiFactor getDpiFactor)
        : base(needPaint, getDpiFactor)
    {
    }
    #endregion

    #region Default Values
    /// <summary>
    /// Gets the default image value.
    /// </summary>
    /// <returns>Image reference.</returns>
    protected override Image? GetImageDefault() => null;

    /// <summary>
    /// Gets the default heading value.
    /// </summary>
    /// <returns>String reference.</returns>
    protected override string GetHeadingDefault() => DEFAULT_TEXT;

    /// <summary>
    /// Gets the default description value.
    /// </summary>
    /// <returns>String reference.</returns>
    protected override string GetDescriptionDefault() => GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion

    #region Description
    /// <summary>
    /// Gets and sets the header description text.
    /// </summary>
    [DefaultValue(@"")]
    public override string Description
    {
        get => base.Description;
        set => base.Description = value;
    }

    #endregion
}