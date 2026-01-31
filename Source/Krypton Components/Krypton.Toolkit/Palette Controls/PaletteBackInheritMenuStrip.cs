#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides palette-backed inheritance for MenuStrip background values by adapting the current global palette ColorTable.
/// </summary>
public sealed class PaletteBackInheritMenuStrip : PaletteBackInherit
{
    private PaletteBase? _palette;

    public PaletteBackInheritMenuStrip()
    {
    }

    public PaletteBackInheritMenuStrip(PaletteBase? palette)
    {
        _palette = palette;
    }

    public void SetPalette(PaletteBase? palette)
    {
        _palette = palette;
    }

    private KryptonColorTable? CurrentColorTable
    {
        get
        {
            PaletteBase? palette = _palette ?? KryptonManager.CurrentGlobalPalette;
            return palette?.ColorTable;
        }
    }

    public override InheritBool GetBackDraw(PaletteState state) => InheritBool.True;

    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) => PaletteGraphicsHint.Inherit;

    public override Color GetBackColor1(PaletteState state)
    {
        var ct = CurrentColorTable;
        if (ct is not null && ct.MenuStripGradientBegin != GlobalStaticValues.EMPTY_COLOR)
        {
            return ct.MenuStripGradientBegin;
        }
        return GlobalStaticValues.EMPTY_COLOR;
    }

    public override Color GetBackColor2(PaletteState state)
    {
        var ct = CurrentColorTable;
        if (ct is not null && ct.MenuStripGradientEnd != GlobalStaticValues.EMPTY_COLOR)
        {
            return ct.MenuStripGradientEnd;
        }
        return GlobalStaticValues.EMPTY_COLOR;
    }

    public override PaletteColorStyle GetBackColorStyle(PaletteState state) => PaletteColorStyle.Inherit;

    public override PaletteRectangleAlign GetBackColorAlign(PaletteState state) => PaletteRectangleAlign.Inherit;

    public override float GetBackColorAngle(PaletteState state) => -1f;

    public override Image? GetBackImage(PaletteState state) => null;

    public override PaletteImageStyle GetBackImageStyle(PaletteState state) => PaletteImageStyle.Inherit;

    public override PaletteRectangleAlign GetBackImageAlign(PaletteState state) => PaletteRectangleAlign.Inherit;
}

