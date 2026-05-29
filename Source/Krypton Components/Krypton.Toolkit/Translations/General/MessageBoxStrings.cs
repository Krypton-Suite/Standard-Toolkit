#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes a general set of strings that are used within the Krypton MessageBox, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MessageBoxStrings : GlobalId
{
    #region Static Values

    private const string DEFAULT_COLLAPSE = @"&Collapse ▲";
    private const string DEFAULT_EXPAND = @"E&xpand ▼";
    private const string DEFAULT_MORE_DETAILS = @"&More Details...";
    private const string DEFAULT_LESS_DETAILS = @"L&ess Details...";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="MessageBoxStrings" /> class.</summary>
    public MessageBoxStrings()
    {
        Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => Collapse.Equals(DEFAULT_COLLAPSE) && 
                             Expand.Equals(DEFAULT_EXPAND) && 
                             MoreDetails.Equals(DEFAULT_MORE_DETAILS) &&
                             LessDetails.Equals(DEFAULT_LESS_DETAILS);

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the collapse text on the expand button.
    /// </summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"Collapse text on the expand button.")]
    [DefaultValue(DEFAULT_COLLAPSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Collapse { get; set; }

    /// <summary>
    /// Gets and sets the expand text on the expand button.
    /// </summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"Expand text on the expand button.")]
    [DefaultValue(DEFAULT_EXPAND)]
    [RefreshProperties(RefreshProperties.All)]
    public string Expand { get; set; }

    /// <summary>Gets or sets the more details string used in expandable footers.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"More details string used in expandable footers.")]
    public string MoreDetails { get; set; }

    /// <summary>Gets or sets the less details string used in expandable footers.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Less details string used in expandable footers.")]
    public string LessDetails { get; set; }

    #endregion

    #region Implementation

    public void Reset()
    {
        Collapse = DEFAULT_COLLAPSE;
        Expand = DEFAULT_EXPAND;
        MoreDetails = DEFAULT_MORE_DETAILS;
        LessDetails = DEFAULT_LESS_DETAILS;
    }

    #endregion
}