#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PaletteButtonOrientationConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(nameof(ExpandableObjectConverter))]
public class PaletteButtonOrientationStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_PALETTE_BUTTON_ORIENTATION_AUTO = @"Auto";
    private const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_BOTTOM = @"Fixed Bottom";
    private const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_TOP = @"Fixed Top";
    private const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_LEFT = @"Fixed Left";
    private const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_RIGHT = @"Fixed Right";
    private const string DEFAULT_PALETTE_BUTTON_ORIENTATION_INHERIT = @"Inherit";

    #endregion

    #region Identity

    public PaletteButtonOrientationStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Auto.Equals(DEFAULT_PALETTE_BUTTON_ORIENTATION_AUTO) &&
                             FixedBottom.Equals(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_BOTTOM) &&
                             FixedTop.Equals(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_TOP) &&
                             FixedLeft.Equals(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_LEFT) &&
                             FixedRight.Equals(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_RIGHT) &&
                             Inherit.Equals(DEFAULT_PALETTE_BUTTON_ORIENTATION_INHERIT);

    public void Reset()
    {
        Auto = DEFAULT_PALETTE_BUTTON_ORIENTATION_AUTO;

        FixedBottom = DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_BOTTOM;

        FixedTop = DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_TOP;

        FixedRight = DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_RIGHT;

        FixedLeft = DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_LEFT;

        Inherit = DEFAULT_PALETTE_BUTTON_ORIENTATION_INHERIT;
    }

    /// <summary>Gets or sets the auto button orientation string string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The auto button orientation string.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_ORIENTATION_AUTO)]
    [RefreshProperties(RefreshProperties.All)]
    public string Auto { get; set; }

    /// <summary>Gets or sets the fixed bottom button orientation string string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The fixed bottom button orientation string.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_BOTTOM)]
    [RefreshProperties(RefreshProperties.All)]
    public string FixedBottom { get; set; }

    /// <summary>Gets or sets the fixed top button orientation string string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The fixed top button orientation string.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_TOP)]
    [RefreshProperties(RefreshProperties.All)]
    public string FixedTop { get; set; }

    /// <summary>Gets or sets the fixed left button orientation string string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The fixed left button orientation string.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_LEFT)]
    [RefreshProperties(RefreshProperties.All)]
    public string FixedLeft { get; set; }

    /// <summary>Gets or sets the fixed right button orientation string string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The fixed right orientation string.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_RIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string FixedRight { get; set; }

    /// <summary>Gets or sets the inherit button orientation string string.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The inherit orientation string.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_ORIENTATION_INHERIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Inherit { get; set; }

    #endregion
}