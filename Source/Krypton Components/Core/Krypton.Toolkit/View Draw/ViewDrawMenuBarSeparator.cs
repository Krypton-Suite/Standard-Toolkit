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
/// Vertical separator drawn between top-level <see cref="KryptonMenuBar"/> items.
/// </summary>
internal sealed class ViewDrawMenuBarSeparator : ViewLeaf
{
    #region Static Fields

    private const int SeparatorWidth = 7;

    #endregion

    #region Instance Fields

    private readonly KryptonContextMenuSeparator _separator;
    private readonly PaletteBase? _palette;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawMenuBarSeparator"/> class.
    /// </summary>
    /// <param name="separator">Separator definition.</param>
    /// <param name="palette">Palette used to obtain separator colours.</param>
    public ViewDrawMenuBarSeparator(KryptonContextMenuSeparator separator, PaletteBase? palette)
    {
        _separator = separator;
        _palette = palette;
        Visible = separator.Visible;
    }

    /// <inheritdoc />
    public override string ToString() => "ViewDrawMenuBarSeparator";

    #endregion

    #region Public

    /// <summary>
    /// Gets the separator definition.
    /// </summary>
    public KryptonContextMenuSeparator Separator => _separator;

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        var height = context!.DisplayRectangle.Height;
        if (height <= 0)
        {
            height = 22;
        }

        return new Size(SeparatorWidth, height);
    }

    /// <inheritdoc />
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        ClientRectangle = context!.DisplayRectangle;
    }

    #endregion

    #region Paint

    /// <inheritdoc />
    public override void RenderBefore(RenderContext context)
    {
        Debug.Assert(context != null);

        var colorTable = _palette?.ColorTable;
        var lineColor = colorTable?.SeparatorDark ?? Color.FromArgb(160, 160, 160);
        var x = ClientRectangle.X + (ClientRectangle.Width / 2);
        var top = ClientRectangle.Y + 4;
        var bottom = ClientRectangle.Bottom - 4;
        if (bottom <= top)
        {
            return;
        }

        using var pen = new Pen(lineColor);
        context!.Graphics.DrawLine(pen, x, top, x, bottom);
    }

    #endregion
}
