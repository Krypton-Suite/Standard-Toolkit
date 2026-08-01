#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes a general set of strings that are used within the Krypton Foldable Dialog, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonFoldableDialogStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_COLLAPSE_TEXT = @"&Show Details";
    private const string DEFAULT_EXPAND_TEXT = @"H&ide Details";

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFoldableDialogStrings"/> class.
    /// </summary>
    public KryptonFoldableDialogStrings()
    {
        Reset();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => CollapseText == DEFAULT_COLLAPSE_TEXT && ExpandText == DEFAULT_EXPAND_TEXT;

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the collapse text on the collapse button.
    /// </summary>
    [Category(@"Values")]
    [Browsable(true)]
    [Description(@"The text to display on the collapse button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [DefaultValue(DEFAULT_COLLAPSE_TEXT)]
    public string CollapseText { get; set; }

    /// <summary>
    /// Gets and sets the expand text on the expand button.
    /// </summary>
    [Category(@"Values")]
    [Browsable(true)]
    [Description(@"The text to display on the expand button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [DefaultValue(DEFAULT_EXPAND_TEXT)]
    public string ExpandText { get; set; }

    #endregion

    #region Reset

    public void Reset()
    {
        CollapseText = DEFAULT_COLLAPSE_TEXT;
        ExpandText = DEFAULT_EXPAND_TEXT;
    }

    #endregion
}