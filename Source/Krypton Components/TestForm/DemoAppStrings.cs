#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Sample strongly-typed custom string set for issue #3757.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class DemoAppStrings : GlobalId, IKryptonCustomStringSet
{
    #region Static Fields

    private const string DEFAULT_SAVE_DRAFT = "S&ave Draft";
    private const string DEFAULT_DISCARD_CHANGES = "&Discard Changes";

    #endregion

    #region Identity

    public DemoAppStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault =>
        SaveDraft.Equals(DEFAULT_SAVE_DRAFT) &&
        DiscardChanges.Equals(DEFAULT_DISCARD_CHANGES);

    public void Reset()
    {
        SaveDraft = DEFAULT_SAVE_DRAFT;
        DiscardChanges = DEFAULT_DISCARD_CHANGES;
    }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Save draft string used for custom situations.")]
    [DefaultValue(DEFAULT_SAVE_DRAFT)]
    public string SaveDraft { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Discard changes string used for custom situations.")]
    [DefaultValue(DEFAULT_DISCARD_CHANGES)]
    public string DiscardChanges { get; set; }

    #endregion
}
