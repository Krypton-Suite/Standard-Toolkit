#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class SystemMenuStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_CLOSE = @"C&lose";
    private const string DEFAULT_MINIMIZE = @"M&inimize";
    private const string DEFAULT_MAXIMIZE = @"&Maximize";
    private const string DEFAULT_RESTORE = @"Re&store";
    private const string DEFAULT_MOVE = @"Mo&ve";
    private const string DEFAULT_SIZE = @"Siz&e";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="SystemMenuStrings" /> class.</summary>
    public SystemMenuStrings()
    {
        ResetValues();
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault =>
        Close.Equals(DEFAULT_CLOSE) &&
        Minimize.Equals(DEFAULT_MINIMIZE) &&
        Maximize.Equals(DEFAULT_MAXIMIZE) &&
        Restore.Equals(DEFAULT_RESTORE) &&
        Move.Equals(DEFAULT_MOVE) &&
        Size.Equals(DEFAULT_SIZE);

    #endregion

    #region Properties

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text for the Close menu item.")]
    [DefaultValue(DEFAULT_CLOSE)]
    public string Close { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text for the Minimize menu item.")]
    [DefaultValue(DEFAULT_MINIMIZE)]
    public string Minimize { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text for the Maximize menu item.")]
    [DefaultValue(DEFAULT_MAXIMIZE)]
    public string Maximize { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text for the Restore menu item.")]
    [DefaultValue(DEFAULT_RESTORE)]
    public string Restore { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text for the Move menu item.")]
    [DefaultValue(DEFAULT_MOVE)]
    public string Move { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text for the Size menu item.")]
    [DefaultValue(DEFAULT_SIZE)]
    public string Size { get; set; }

    #endregion

    #region Reset Values

    public void ResetValues()
    {
        Close = DEFAULT_CLOSE;
        Minimize = DEFAULT_MINIMIZE;
        Maximize = DEFAULT_MAXIMIZE;
        Restore = DEFAULT_RESTORE;
        Move = DEFAULT_MOVE;
        Size = DEFAULT_SIZE;
    }

    #endregion
}