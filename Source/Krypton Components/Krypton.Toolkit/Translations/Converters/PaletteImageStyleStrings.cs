#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PaletteImageStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteImageStyleStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_PALETTE_IMAGE_STYLE_INHERIT = @"Inherit";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_STRETCH = @"Stretch";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TILE = @"Tile";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X = @"TileFlip - X";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_Y = @"TileFlip - Y";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X_Y = @"TileFlip - XY";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TOP_LEFT = @"Top - Left";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TOP_MIDDLE = @"Top - Middle";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_TOP_RIGHT = @"Top - Right";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_CENTER_LEFT = @"Center - Left";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_CENTER_MIDDLE = @"Center - Middle";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_CENTER_RIGHT = @"Center - Right";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_LEFT = @"Bottom - Left";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_MIDDLE = @"Bottom - Middle";
    private const string DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_RIGHT = @"Bottom - Right";

    #endregion

    #region Identity

    public PaletteImageStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Inherit.Equals(DEFAULT_PALETTE_IMAGE_STYLE_INHERIT) &&
                             Stretch.Equals(DEFAULT_PALETTE_IMAGE_STYLE_STRETCH) &&
                             Tile.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TILE) &&
                             TileFlipX.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X) &&
                             TileFlipY.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_Y) &&
                             TileFlipXY.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X_Y) &&
                             TopLeft.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TOP_LEFT) &&
                             TopMiddle.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TOP_MIDDLE) &&
                             TopRight.Equals(DEFAULT_PALETTE_IMAGE_STYLE_TOP_RIGHT) &&
                             CenterLeft.Equals(DEFAULT_PALETTE_IMAGE_STYLE_CENTER_LEFT) &&
                             CenterMiddle.Equals(DEFAULT_PALETTE_IMAGE_STYLE_CENTER_MIDDLE) &&
                             CenterRight.Equals(DEFAULT_PALETTE_IMAGE_STYLE_CENTER_RIGHT) &&
                             BottomLeft.Equals(DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_LEFT) &&
                             BottomMiddle.Equals(DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_MIDDLE) &&
                             BottomRight.Equals(DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_RIGHT);

    public void Reset()
    {
        Inherit = DEFAULT_PALETTE_IMAGE_STYLE_INHERIT;

        Stretch = DEFAULT_PALETTE_IMAGE_STYLE_STRETCH;

        Tile = DEFAULT_PALETTE_IMAGE_STYLE_TILE;

        TileFlipX = DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X;

        TileFlipY = DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_Y;

        TileFlipXY = DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X_Y;

        TopLeft = DEFAULT_PALETTE_IMAGE_STYLE_TOP_LEFT;

        TopMiddle = DEFAULT_PALETTE_IMAGE_STYLE_TOP_MIDDLE;

        TopRight = DEFAULT_PALETTE_IMAGE_STYLE_TOP_RIGHT;

        CenterLeft = DEFAULT_PALETTE_IMAGE_STYLE_CENTER_LEFT;

        CenterMiddle = DEFAULT_PALETTE_IMAGE_STYLE_CENTER_MIDDLE;

        CenterRight = DEFAULT_PALETTE_IMAGE_STYLE_CENTER_RIGHT;

        BottomLeft = DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_LEFT;

        BottomMiddle = DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_MIDDLE;

        BottomRight = DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_RIGHT;
    }

    /// <summary>Gets or sets the inherit palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The inherit palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_INHERIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Inherit { get; set; }

    /// <summary>Gets or sets the stretch palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The stretch palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_STRETCH)]
    public string Stretch { get; set; }

    /// <summary>Gets or sets the tile palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tile palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TILE)]
    public string Tile { get; set; }

    /// <summary>Gets or sets the tile flip X palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tile flip X palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X)]
    public string TileFlipX { get; set; }

    /// <summary>Gets or sets the tile flip Y palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tile flip Y palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_Y)]
    public string TileFlipY { get; set; }

    /// <summary>Gets or sets the tile flip X and Y palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tile flip X & Y palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X_Y)]
    public string TileFlipXY { get; set; }

    /// <summary>Gets or sets the top left palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The top left palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TOP_LEFT)]
    public string TopLeft { get; set; }

    /// <summary>Gets or sets the top middle palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The top middle palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TOP_MIDDLE)]
    public string TopMiddle { get; set; }

    /// <summary>Gets or sets the top right palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The top right palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_TOP_RIGHT)]
    public string TopRight { get; set; }

    /// <summary>Gets or sets the center left palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The center left palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_CENTER_LEFT)]
    public string CenterLeft { get; set; }

    /// <summary>Gets or sets the center middle palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The center middle palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_CENTER_MIDDLE)]
    public string CenterMiddle { get; set; }

    /// <summary>Gets or sets the center right palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The center right palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_CENTER_RIGHT)]
    public string CenterRight { get; set; }

    /// <summary>Gets or sets the bottom left palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bottom left palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_LEFT)]
    public string BottomLeft { get; set; }

    /// <summary>Gets or sets the bottom middle palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bottom middle palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_MIDDLE)]
    public string BottomMiddle { get; set; }

    /// <summary>Gets or sets the bottom right palette image style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bottom right palette image style.")]
    [DefaultValue(DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_RIGHT)]
    public string BottomRight { get; set; }

    #endregion
}