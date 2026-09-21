#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Builds graphics paths with individually rounded rectangle corners.
/// </summary>
internal static class RoundedPathHelper
{
    /// <summary>
    /// Arc box diameters for each corner after clamping to the target rectangle.
    /// </summary>
    internal struct CornerArcLengths
    {
        public float TopLeft;
        public float TopRight;
        public float BottomRight;
        public float BottomLeft;

        public bool HasAnyRounding => TopLeft > 0f || TopRight > 0f || BottomRight > 0f || BottomLeft > 0f;

        public float MaxArc => Math.Max(Math.Max(TopLeft, TopRight), Math.Max(BottomLeft, BottomRight));

        public static CornerArcLengths FromCornerRounding(PaletteCornerRounding corners, RectangleF rect, int borderWidth)
        {
            float maxRadius = Math.Max(0f, Math.Min(rect.Width / 2f, rect.Height / 2f) - borderWidth);
            return new CornerArcLengths
            {
                TopLeft = ToArcDiameter(corners.TopLeft, maxRadius),
                TopRight = ToArcDiameter(corners.TopRight, maxRadius),
                BottomRight = ToArcDiameter(corners.BottomRight, maxRadius),
                BottomLeft = ToArcDiameter(corners.BottomLeft, maxRadius)
            };
        }

        private static float ToArcDiameter(float radius, float maxRadius)
        {
            if (radius < 0.1f)
            {
                if (radius > 0f)
                {
                    radius = 0.1f;
                }
                else
                {
                    return 0f;
                }
            }

            return Math.Max(0f, Math.Min(radius, maxRadius)) * 2f;
        }
    }

    /// <summary>
    /// Appends a closed rounded rectangle path using per-corner arc diameters.
    /// </summary>
    internal static void AppendRoundedRectangle(GraphicsPath path, RectangleF rectF, CornerArcLengths arcs, bool closeFigure)
    {
        if (!arcs.HasAnyRounding)
        {
            path.AddRectangle(rectF);
            if (closeFigure)
            {
                path.CloseFigure();
            }

            return;
        }

        float left = rectF.Left;
        float top = rectF.Top;
        float right = rectF.Right;
        float bottom = rectF.Bottom;
        float tl = arcs.TopLeft;
        float tr = arcs.TopRight;
        float br = arcs.BottomRight;
        float bl = arcs.BottomLeft;
        float tlR = tl * 0.5f;
        float trR = tr * 0.5f;
        float brR = br * 0.5f;
        float blR = bl * 0.5f;
        float trX = tr > 0f ? tr + 1f : 0f;
        float brX = br > 0f ? br + 1f : 0f;
        float blY = bl > 0f ? bl + 1f : 0f;
        float brY = br > 0f ? br + 1f : 0f;

        // Top-left corner
        if (tl > 0f)
        {
            path.AddArc(left, top, tl, tl, 180f, 90f);
        }
        else if (tr > 0f || br > 0f || bl > 0f)
        {
            path.AddLine(left, top, left, top);
        }

        // Top edge when the adjacent corner is square
        if (tl > 0f && tr == 0f)
        {
            path.AddLine(left + tlR, top, right, top);
        }
        else if (tl == 0f && tr > 0f)
        {
            path.AddLine(left, top, right - trX + trR, top);
        }
        else if (tl == 0f && tr == 0f && (br > 0f || bl > 0f))
        {
            path.AddLine(left, top, right, top);
        }

        // Top-right corner
        if (tr > 0f)
        {
            path.AddArc(right - trX, top, tr, tr, 270f, 90f);
        }
        else if (br > 0f || bl > 0f)
        {
            path.AddLine(right, top, right, top);
        }

        // Right edge when the adjacent corner is square
        if (tr > 0f && br == 0f)
        {
            path.AddLine(right, top + trR, right, bottom);
        }
        else if (tr == 0f && br > 0f)
        {
            path.AddLine(right, top, right, bottom - brY + brR);
        }
        else if (tr == 0f && br == 0f && bl > 0f)
        {
            path.AddLine(right, top, right, bottom);
        }

        // Bottom-right corner
        if (br > 0f)
        {
            path.AddArc(right - brX, bottom - brY, br, br, 0f, 90f);
        }
        else if (bl > 0f)
        {
            path.AddLine(right, bottom, right, bottom);
        }

        // Bottom edge when the adjacent corner is square
        if (br > 0f && bl == 0f)
        {
            path.AddLine(right - brX + brR, bottom, left, bottom);
        }
        else if (br == 0f && bl > 0f)
        {
            path.AddLine(right, bottom, left + blR, bottom);
        }
        else if (br == 0f && bl == 0f && tl > 0f)
        {
            path.AddLine(right, bottom, left, bottom);
        }

        // Bottom-left corner
        if (bl > 0f)
        {
            path.AddArc(left, bottom - blY, bl, bl, 90f, 90f);
        }
        else if (tl > 0f)
        {
            path.AddLine(left, bottom, left, bottom);
        }

        // Left edge when the adjacent corner is square
        if (bl > 0f && tl == 0f)
        {
            path.AddLine(left, bottom - blY + blR, left, top);
        }
        else if (bl == 0f && tl > 0f)
        {
            path.AddLine(left, bottom, left, top + tlR);
        }

        if (closeFigure)
        {
            path.CloseFigure();
        }
    }
}
