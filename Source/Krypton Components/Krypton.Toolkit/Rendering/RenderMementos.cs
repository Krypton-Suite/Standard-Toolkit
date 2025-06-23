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

#region MementoDisposable
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoDisposable : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Dispose of resources.
    /// </summary>
    ~MementoDisposable()
    {
        // If not already disposed manually, do it now
        if (!_disposed)
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// Dispose of resources.
    /// </summary>
    public void Dispose()
    {
        // Only need to dispose of resources once
        if (!_disposed)
        {
            Dispose(true);
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose of resources.
    /// </summary>
    /// <param name="disposing"></param>
    public virtual void Dispose(bool disposing) => _disposed = true;
}
#endregion

#region MementoDouble
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoDouble : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    internal IDisposable? First;
    /// <summary>For internal use only.</summary>
    internal IDisposable? Second;

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (First != null)
        {
            First.Dispose();
            First = null;
        }

        if (Second != null)
        {
            Second.Dispose();
            Second = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoTriple
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoTriple : MementoDouble
{
    /// <summary>For internal use only.</summary>
    public IDisposable? Third;

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (Third != null)
        {
            Third.Dispose();
            Third = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRectOneColor
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRectOneColor : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public Rectangle Rect;
    /// <summary>For internal use only.</summary>
    public Color C1;

    /// <summary>For internal use only.</summary>
    public MementoRectOneColor(Rectangle r, Color color1)
    {
        Rect = r;
        C1 = color1;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r, Color color1)
    {
        var ret = Rect.Equals(r) && C1.Equals(color1);

        Rect = r;
        C1 = color1;

        return ret;
    }
}
#endregion

#region MementoRectTwoColor
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRectTwoColor : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public Rectangle Rect;
    /// <summary>For internal use only.</summary>
    public Color C1, C2;

    /// <summary>For internal use only.</summary>
    public MementoRectTwoColor(Rectangle r, Color color1, Color color2)
    {
        Rect = r;
        C1 = color1;
        C2 = color2;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r, Color color1, Color color2)
    {
        var ret = Rect.Equals(r) &&
                  C1.Equals(color1) &&
                  C2.Equals(color2);

        Rect = r;
        C1 = color1;
        C2 = color2;

        return ret;
    }
}
#endregion

#region MementoRectThreeColor
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRectThreeColor : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public Color C3;

    /// <summary>For internal use only.</summary>
    public MementoRectThreeColor(Rectangle r,
        Color color1, Color color2,
        Color color3)
        : base(r, color1, color2) =>
        C3 = color3;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3)
    {
        var ret = base.UseCachedValues(r, color1, color2) &&
                  C3.Equals(color3);

        C3 = color3;

        return ret;
    }
}
#endregion

#region MementoRectFourColor
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRectFourColor : MementoRectThreeColor
{
    /// <summary>For internal use only.</summary>
    public Color C4;

    /// <summary>For internal use only.</summary>
    public MementoRectFourColor(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4)
        : base(r, color1, color2, color3) =>
        C4 = color4;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3) &&
                  C4.Equals(color4);

        C4 = color4;

        return ret;
    }
}
#endregion

#region MementoRectFiveColor
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRectFiveColor : MementoRectFourColor
{
    /// <summary>For internal use only.</summary>
    public Color C5;

    /// <summary>For internal use only.</summary>
    public MementoRectFiveColor(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5)
        : base(r, color1, color2, color3, color4) =>
        C5 = color5;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3, color4) &&
                  C5.Equals(color5);

        C5 = color5;

        return ret;
    }
}
#endregion

#region MementoRibbonLinear
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonLinear : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? LinearBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonLinear(Rectangle r,
        Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (LinearBrush != null)
        {
            LinearBrush.Dispose();
            LinearBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonLinearBorder
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonLinearBorder : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? LinearBrush;
    /// <summary>For internal use only.</summary>
    public Pen? LinearPen;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BorderPath;

    /// <summary>For internal use only.</summary>
    public MementoRibbonLinearBorder(Rectangle r,
        Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        LinearBrush?.Dispose();
        BorderPath?.Dispose();
        LinearPen?.Dispose();

        LinearBrush = null;
        BorderPath = null;
        LinearPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonAppButtonInner
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonAppButtonInner : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public SolidBrush? OutsideBrush;
    /// <summary>For internal use only.</summary>
    public SolidBrush? InsideBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonAppButtonInner(Rectangle r,
        Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        OutsideBrush?.Dispose();
        OutsideBrush = null;
        InsideBrush?.Dispose();
        InsideBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonAppButtonOuter
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonAppButtonOuter : MementoRectThreeColor
{
    /// <summary>For internal use only.</summary>
    public SolidBrush? WholeBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BackPath;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BottomDarkGradient;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopLightenGradient;

    /// <summary>For internal use only.</summary>
    public MementoRibbonAppButtonOuter(Rectangle r,
        Color color1, Color color2,
        Color color3)
        : base(r, color1, color2, color3)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        WholeBrush?.Dispose();
        WholeBrush = null;
        BackPath?.Dispose();
        BackPath = null;
        BottomDarkGradient?.Dispose();
        BottomDarkGradient = null;
        TopLightenGradient?.Dispose();
        TopLightenGradient = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonAppTab
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonAppTab : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BorderPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BorderFillPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsideFillPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? HighlightPath;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? HighlightBrush;
    /// <summary>For internal use only.</summary>
    public Rectangle HighlightRect;
    /// <summary>For internal use only.</summary>
    public Pen? BorderPen;
    /// <summary>For internal use only.</summary>
    public Brush? BorderBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InsideFillBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonAppTab(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public void GeneratePaths(Rectangle rect, PaletteState state)
    {
        // Create the border path
        BorderPath = new GraphicsPath();
        BorderPath.AddLine(rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1.75f);
        BorderPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
        BorderPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
        BorderPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
        BorderPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 2);

        // Create border path for filling
        BorderFillPath = new GraphicsPath();
        BorderFillPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1.75f);
        BorderFillPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
        BorderFillPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
        BorderFillPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
        BorderFillPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 1);

        // Path for the highlight at bottom center
        HighlightRect = new Rectangle(rect.Left - (rect.Width / 8), rect.Top + (rect.Height / 2) - 2, rect.Width + (rect.Width / 5), rect.Height + 4);
        HighlightPath = new GraphicsPath();
        HighlightPath.AddEllipse(HighlightRect);
        HighlightBrush = new PathGradientBrush(HighlightPath)
        {
            CenterPoint = new PointF(HighlightRect.Left + (HighlightRect.Width / 2), HighlightRect.Top + (HighlightRect.Height / 2)),
            SurroundColors = [Color.Transparent]
        };

        // Reduce rectangle to the inside fill area
        rect.X += 2;
        rect.Y += 2;
        rect.Width -= 3;
        rect.Height -= 2;

        // Create inside path for filling
        InsideFillPath = new GraphicsPath();
        InsideFillPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1f);
        InsideFillPath.AddLine(rect.Left, rect.Top + 1f, rect.Left + 1, rect.Top);
        InsideFillPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
        InsideFillPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
        InsideFillPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 1);
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BorderPath?.Dispose();
        BorderFillPath?.Dispose();
        InsideFillPath?.Dispose();
        BorderPen?.Dispose();
        BorderBrush?.Dispose();
        InsideFillBrush?.Dispose();

        BorderPath = null;
        BorderFillPath = null;
        InsideFillPath = null;
        BorderPen = null;
        BorderBrush = null;
        InsideFillBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonAppTab2013
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonAppTab2013 : MementoRectOneColor
{
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? HighlightBrush;
    /// <summary>For internal use only.</summary>
    public Rectangle HighlightRect;
    /// <summary>For internal use only.</summary>
    public SolidBrush? InsideFillBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonAppTab2013(Rectangle r, Color color1)
        : base(r, color1)
    {
    }

    /// <summary>For internal use only.</summary>
    public void GeneratePaths(Rectangle rect, PaletteState state) =>
        //// Create the border path
        //borderPath = new GraphicsPath();
        //borderPath.AddLine(rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1.75f);
        ////borderPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
        //borderPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
        ////borderPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
        //borderPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 2);

        //// Create border path for filling
        //borderFillPath = new GraphicsPath();
        //borderFillPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1.75f);
        ////borderFillPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
        //borderFillPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
        ////borderFillPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
        //borderFillPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 1);

        //// Path for the highlight at bottom center
        //highlightRect = new Rectangle(rect.Left - (rect.Width / 8), rect.Top + (rect.Height / 2) - 2, rect.Width + (rect.Width / 5), rect.Height + 4);
        //highlightPath = new GraphicsPath();
        //highlightPath.AddEllipse(highlightRect);
        //highlightBrush = new PathGradientBrush(highlightPath);
        //highlightBrush.CenterPoint = new PointF(highlightRect.Left + (highlightRect.Width / 2), highlightRect.Top + (highlightRect.Height / 2));
        //highlightBrush.SurroundColors = new Color[] { Color.Transparent };

        // Reduce rectangle to the inside fill area
        rect.X -= 1;//rect.Y += 2;//rect.Width -= 3;//rect.Height -= 2;// Create inside path for filling//insideFillPath = new GraphicsPath();//insideFillPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1f);//insideFillPath.AddLine(rect.Left, rect.Top + 1f, rect.Left + 1, rect.Top);//insideFillPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);//insideFillPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);//insideFillPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 1);

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing) =>
        //if (borderPath != null)
        //{
        //    borderPath.Dispose();
        //    borderFillPath.Dispose();
        //    insideFillPath.Dispose();
        //    borderPen.Dispose();
        //    borderBrush.Dispose();
        //    insideFillBrush.Dispose();
        //    borderPath = null;
        //    borderFillPath = null;
        //    insideFillPath = null;
        //    borderPen = null;
        //    borderBrush = null;
        //    insideFillBrush = null;
        //}
        base.Dispose(disposing);
}
#endregion

#region MementoRibbonGroupGradientOne
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupGradientOne : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? Brush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupGradientOne(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        Brush?.Dispose();
        Brush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupGradientTwo
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupGradientTwo : MementoRectFourColor
{
    /// <summary>For internal use only.</summary>
    public Rectangle TopRect, BottomRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BottomBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupGradientTwo(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4)
        : base(r, color1, color2, color3, color4)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        TopBrush?.Dispose();
        BottomBrush?.Dispose();

        TopBrush = null;
        BottomBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupCollapsedBorder
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupCollapsedBorder : MementoRectFourColor
{
    /// <summary>For internal use only.</summary>
    public GraphicsPath? SolidPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePath;
    /// <summary>For internal use only.</summary>
    public Pen? SolidPen;
    /// <summary>For internal use only.</summary>
    public Pen? InsidePen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupCollapsedBorder(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4)
        : base(r, color1, color2, color3, color4)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        SolidPath?.Dispose();
        InsidePath?.Dispose();
        SolidPen?.Dispose();
        InsidePen?.Dispose();

        SolidPath = null;
        InsidePath = null;
        SolidPen = null;
        InsidePen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupCollapsedFrameBorder
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupCollapsedFrameBorder : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public GraphicsPath? SolidPath;
    /// <summary>For internal use only.</summary>
    public SolidBrush? TitleBrush;
    /// <summary>For internal use only.</summary>
    public Pen? SolidPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupCollapsedFrameBorder(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        SolidPath?.Dispose();
        TitleBrush?.Dispose();
        SolidPen?.Dispose();

        SolidPath = null;
        TitleBrush = null;
        SolidPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupNormalBorderSep
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupNormal : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TotalBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InnerBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TrackSepBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TrackFillBrush;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? TrackHighlightBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? PressedFillBrush;
    /// <summary>For internal use only.</summary>
    public Pen? InnerPen;
    /// <summary>For internal use only.</summary>
    public Pen? TrackSepPen;
    /// <summary>For internal use only.</summary>
    public Pen? TrackBottomPen;

    private bool _tracking;
    private bool _dark;


    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupNormal(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        bool tracking, bool dark)
        : base(r, color1, color2, color3, color4, color5)
    {
        _tracking = tracking;
        _dark = dark;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        bool tracking, bool dark)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                  (_tracking == tracking) &&
                  (_dark == dark);

        _tracking = tracking;
        _dark = dark;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (TotalBrush != null)
        {
            TotalBrush.Dispose();
            InnerBrush?.Dispose();
            TrackSepBrush?.Dispose();
            TrackFillBrush?.Dispose();
            PressedFillBrush?.Dispose();
            TrackHighlightBrush?.Dispose();
            InnerPen?.Dispose();
            TrackSepPen?.Dispose();
            TrackBottomPen?.Dispose();

            TotalBrush = null;
            InnerBrush = null;
            TrackSepBrush = null;
            TrackFillBrush = null;
            PressedFillBrush = null;
            TrackHighlightBrush = null;
            InnerPen = null;
            TrackSepPen = null;
            TrackBottomPen = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupNormalBorder
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupNormalBorder : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public Rectangle BackRect;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? SolidPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? LightPath;
    /// <summary>For internal use only.</summary>
    public Pen? SolidPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupNormalBorder(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        SolidPath?.Dispose();
        InsidePath?.Dispose();
        OutsidePath?.Dispose();
        LightPath?.Dispose();
        SolidPen?.Dispose();

        SolidPath = null;
        InsidePath = null;
        OutsidePath = null;
        LightPath = null;
        SolidPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupNormalBorderSep
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupNormalBorderSep : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TotalBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InnerBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TrackSepBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TrackFillBrush;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? TrackHighlightBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? PressedFillBrush;
    /// <summary>For internal use only.</summary>
    public Pen? InnerPen;
    /// <summary>For internal use only.</summary>
    public Pen? TrackSepPen;
    /// <summary>For internal use only.</summary>
    public Pen? TrackBottomPen;

    private bool _tracking;
    private bool _dark;


    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupNormalBorderSep(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        bool tracking, bool dark)
        : base(r, color1, color2, color3, color4, color5)
    {
        _tracking = tracking;
        _dark = dark;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        bool tracking, bool dark)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                  (_tracking == tracking) &&
                  (_dark == dark);

        _tracking = tracking;
        _dark = dark;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        TotalBrush?.Dispose();
        InnerBrush?.Dispose();
        TrackSepBrush?.Dispose();
        TrackFillBrush?.Dispose();
        PressedFillBrush?.Dispose();
        TrackHighlightBrush?.Dispose();
        InnerPen?.Dispose();
        TrackSepPen?.Dispose();
        TrackBottomPen?.Dispose();

        TotalBrush = null;
        InnerBrush = null;
        TrackSepBrush = null;
        TrackFillBrush = null;
        PressedFillBrush = null;
        TrackHighlightBrush = null;
        InnerPen = null;
        TrackSepPen = null;
        TrackBottomPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupNormalTitle
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupNormalTitle : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public GraphicsPath? TitlePath;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TitleBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupNormalTitle(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        TitlePath?.Dispose();
        TitleBrush?.Dispose();

        TitlePath = null;
        TitleBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupAreaBorder
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupAreaBorder : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePathN;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePathL;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? ShadowPath;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? FillBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? FillTopBrush;
    /// <summary>For internal use only.</summary>
    public Pen? ShadowPenN;
    /// <summary>For internal use only.</summary>
    public Pen? ShadowPenL;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? InsidePen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupAreaBorder(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5)
        : base(r, color1, color2, color3, color4, color5)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        OutsidePath?.Dispose();
        InsidePathN?.Dispose();
        InsidePathL?.Dispose();
        ShadowPath?.Dispose();
        FillBrush?.Dispose();
        FillTopBrush?.Dispose();
        ShadowPenN?.Dispose();
        ShadowPenL?.Dispose();
        OutsidePen?.Dispose();
        InsidePen?.Dispose();

        OutsidePath = null;
        InsidePathN = null;
        InsidePathL = null;
        ShadowPath = null;
        FillBrush = null;
        FillTopBrush = null;
        ShadowPenN = null;
        ShadowPenL = null;
        OutsidePen = null;
        InsidePen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupAreaBorder3
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupAreaBorder3 : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public Rectangle BorderRect;
    /// <summary>For internal use only.</summary>
    public Point[] BorderPoints;
    /// <summary>For internal use only.</summary>
    public Rectangle BackRect1;
    /// <summary>For internal use only.</summary>
    public Rectangle BackRect2;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BackBrush1;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BackBrush2;
    /// <summary>For internal use only.</summary>
    public SolidBrush? BackBrush3;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? GradientBorderBrush;
    /// <summary>For internal use only.</summary>
    public Pen? GradientBorderPen;
    /// <summary>For internal use only.</summary>
    public Pen? SolidBorderPen;
    /// <summary>For internal use only.</summary>
    public Pen? ShadowPen1;
    /// <summary>For internal use only.</summary>
    public Pen? ShadowPen2;
    /// <summary>For internal use only.</summary>
    public Pen? ShadowPen3;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupAreaBorder3(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5)
        : base(r, color1, color2, color3, color4, color5)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BackBrush1?.Dispose();
        BackBrush2?.Dispose();
        BackBrush3?.Dispose();
        GradientBorderBrush?.Dispose();
        GradientBorderPen?.Dispose();
        SolidBorderPen?.Dispose();
        ShadowPen1?.Dispose();
        ShadowPen2?.Dispose();
        ShadowPen3?.Dispose();

        BackBrush1 = null;
        BackBrush2 = null;
        BackBrush3 = null;
        GradientBorderBrush = null;
        GradientBorderPen = null;
        SolidBorderPen = null;
        ShadowPen1 = null;
        ShadowPen2 = null;
        ShadowPen3 = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonGroupAreaBorderContext
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonGroupAreaBorderContext : MementoRectThreeColor
{
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? ShadowPath;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? FillBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? FillTopBrush;
    /// <summary>For internal use only.</summary>
    public Pen? ShadowPen;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? InsidePen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonGroupAreaBorderContext(Rectangle r,
        Color color1, Color color2,
        Color color3)
        : base(r, color1, color2, color3)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        OutsidePath?.Dispose();
        InsidePath?.Dispose();
        ShadowPath?.Dispose();
        FillBrush?.Dispose();
        FillTopBrush?.Dispose();
        ShadowPen?.Dispose();
        OutsidePen?.Dispose();
        InsidePen?.Dispose();

        OutsidePath = null;
        InsidePath = null;
        ShadowPath = null;
        FillBrush = null;
        FillTopBrush = null;
        ShadowPen = null;
        OutsidePen = null;
        InsidePen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabTracking2007
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabTracking2007 : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public Rectangle Half1Rect;
    /// <summary>For internal use only.</summary>
    public Rectangle Half2Rect;
    /// <summary>For internal use only.</summary>
    public RectangleF Half2RectF;
    /// <summary>For internal use only.</summary>
    public RectangleF EllipseRect;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? TopPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? EllipsePath;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? Half1LeftBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? Half1RightBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? Half1LightBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? OutsideBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InsideBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopBrush;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? EllipseBrush;
    /// <summary>For internal use only.</summary>
    public SolidBrush? Half2Brush;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? TopPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabTracking2007(Rectangle r,
        Color color1, Color color2,
        VisualOrientation orient)
        : base(r, color1, color2) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        VisualOrientation orient)
    {
        var ret = Rect.Equals(r) &&
                  C1.Equals(color1) &&
                  C2.Equals(color2) &&
                  (orient == Orientation);

        Rect = r;
        C1 = color1;
        C2 = color2;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        OutsidePath?.Dispose();
        TopPath?.Dispose();
        EllipsePath?.Dispose();
        Half1LeftBrush?.Dispose();
        Half1RightBrush?.Dispose();
        Half1LightBrush?.Dispose();
        OutsideBrush?.Dispose();
        InsideBrush?.Dispose();
        TopBrush?.Dispose();
        Half2Brush?.Dispose();
        OutsidePen?.Dispose();
        TopPen?.Dispose();

        OutsidePath = null;
        TopPath = null;
        EllipsePath = null;
        Half1LeftBrush = null;
        Half1RightBrush = null;
        Half1LightBrush = null;
        OutsideBrush = null;
        InsideBrush = null;
        TopBrush = null;
        Half2Brush = null;
        OutsidePen = null;
        TopPen = null;

        if (EllipseBrush != null)
        {
            EllipseBrush.Dispose();
            EllipseBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabTracking2010
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabTracking2010 : MementoRectFourColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BorderPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePath;
    /// <summary>For internal use only.</summary>
    public SolidBrush? OutsideBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InsideBrush;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabTracking2010(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        VisualOrientation orient)
        : base(r, color1, color2, color3, color4) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        VisualOrientation orient)
    {
        var ret = Rect.Equals(r) &&
                  C1.Equals(color1) &&
                  C2.Equals(color2) &&
                  C3.Equals(color1) &&
                  C4.Equals(color2) &&
                  (orient == Orientation);

        Rect = r;
        C1 = color1;
        C2 = color2;
        C3 = color3;
        C4 = color4;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BorderPath?.Dispose();
        OutsidePath?.Dispose();
        InsidePath?.Dispose();
        OutsideBrush?.Dispose();
        InsideBrush?.Dispose();
        OutsidePen?.Dispose();

        BorderPath = null;
        OutsidePath = null;
        InsidePath = null;
        OutsideBrush = null;
        InsideBrush = null;
        OutsidePen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabSelected2007
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabSelected2007 : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public Rectangle CenterRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? CenterBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public SolidBrush? InsideBrush;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? MiddlePen;
    /// <summary>For internal use only.</summary>
    public Pen? InsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? CenterPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabSelected2007(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        VisualOrientation orient)
        : base(r, color1, color2, color3, color4, color5) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        VisualOrientation orient)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                  (orient == Orientation);

        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        CenterBrush?.Dispose();
        OutsidePath?.Dispose();
        InsideBrush?.Dispose();
        OutsidePen?.Dispose();
        MiddlePen?.Dispose();
        InsidePen?.Dispose();
        CenterPen?.Dispose();

        CenterBrush = null;
        OutsidePath = null;
        InsideBrush = null;
        OutsidePen = null;
        MiddlePen = null;
        InsidePen = null;
        CenterPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabSelected2010
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabSelected2010 : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? CenterBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? CenterPen;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InsideBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? InsidePath;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabSelected2010(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        VisualOrientation orient)
        : base(r, color1, color2, color3, color4, color5) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        VisualOrientation orient)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                  (orient == Orientation);

        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        CenterBrush?.Dispose();
        OutsidePath?.Dispose();
        OutsidePen?.Dispose();
        CenterPen?.Dispose();
        InsideBrush?.Dispose();
        InsidePath?.Dispose();

        CenterBrush = null;
        OutsidePath = null;
        OutsidePen = null;
        CenterPen = null;
        InsideBrush = null;
        InsidePath = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabContextSelected
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabContextSelected : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public Rectangle InteriorRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InsideBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? L1, L2, L3;
    /// <summary>For internal use only.</summary>
    public Pen? LeftPen;
    /// <summary>For internal use only.</summary>
    public Pen? RightPen;
    /// <summary>For internal use only.</summary>
    public Pen? BottomInnerPen;
    /// <summary>For internal use only.</summary>
    public Pen? BottomOuterPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabContextSelected(Rectangle r,
        Color color1, Color color2,
        VisualOrientation orient)
        : base(r, color1, color2) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        VisualOrientation orient)
    {
        var ret = base.UseCachedValues(r, color1, color2) &&
                  (orient == Orientation);

        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        OutsidePath?.Dispose();
        InsideBrush?.Dispose();
        OutsidePen?.Dispose();
        L1?.Dispose();
        L2?.Dispose();
        L3?.Dispose();
        LeftPen?.Dispose();
        RightPen?.Dispose();
        BottomInnerPen?.Dispose();
        BottomOuterPen?.Dispose();

        OutsidePath = null;
        InsideBrush = null;
        OutsidePen = null;
        L1 = null;
        L2 = null;
        L3 = null;
        LeftPen = null;
        RightPen = null;
        BottomInnerPen = null;
        BottomOuterPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabHighlight
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabHighlight : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopBorderBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BorderVertBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? OutsideVertBrush;
    /// <summary>For internal use only.</summary>
    public MementoRibbonTabSelected2007? SelectedMemento;
    /// <summary>For internal use only.</summary>
    public Pen? InnerVertPen;
    /// <summary>For internal use only.</summary>
    public Pen? InnerHorzPen;
    /// <summary>For internal use only.</summary>
    public Pen? BorderHorzPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabHighlight(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        VisualOrientation orient)
        : base(r, color1, color2, color3, color4, color5) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5,
        VisualOrientation orient)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                  (orient == Orientation);

        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        SelectedMemento?.Dispose();
        SelectedMemento = null;

        TopBorderBrush?.Dispose();
        BorderVertBrush?.Dispose();
        OutsideVertBrush?.Dispose();
        InnerVertPen?.Dispose();
        InnerHorzPen?.Dispose();
        BorderHorzPen?.Dispose();

        TopBorderBrush = null;
        BorderVertBrush = null;
        OutsideVertBrush = null;
        InnerVertPen = null;
        InnerHorzPen = null;
        BorderHorzPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabGlowing
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabGlowing : MementoRectThreeColor
{
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public RectangleF FullRect;
    /// <summary>For internal use only.</summary>
    public RectangleF EllipseRect;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? OutsidePath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? TopPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? EllipsePath;
    /// <summary>For internal use only.</summary>
    public SolidBrush? InsideBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopBrush;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? EllipseBrush;
    /// <summary>For internal use only.</summary>
    public Pen? InsidePen;
    /// <summary>For internal use only.</summary>
    public Pen? OutsidePen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabGlowing(Rectangle r,
        Color color1, Color color2,
        Color color3,
        VisualOrientation orient)
        : base(r, color1, color2, color3) =>
        Orientation = orient;

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color color1, Color color2,
        Color color3,
        VisualOrientation orient)
    {
        var ret = base.UseCachedValues(r, color1, color2, color3) &&
                  (orient == Orientation);

        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        OutsidePath?.Dispose();
        TopPath?.Dispose();
        EllipsePath?.Dispose();
        InsideBrush?.Dispose();
        TopBrush?.Dispose();
        InsidePen?.Dispose();
        OutsidePen?.Dispose();

        OutsidePath = null;
        TopPath = null;
        EllipsePath = null;
        InsideBrush = null;
        TopBrush = null;
        InsidePen = null;
        OutsidePen = null;

        if (EllipseBrush != null)
        {
            EllipseBrush.Dispose();
            EllipseBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabContext
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabContext : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public Rectangle FillRect;
    /// <summary>For internal use only.</summary>
    public Pen? BorderPen;
    /// <summary>For internal use only.</summary>
    public Pen? UnderlinePen;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? FillBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabContext(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BorderPen?.Dispose();
        FillBrush?.Dispose();
        UnderlinePen?.Dispose();

        BorderPen = null;
        FillBrush = null;
        UnderlinePen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabContextOffice
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabContextOffice : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public Rectangle FillRect;
    /// <summary>For internal use only.</summary>
    public Pen? BorderPen;
    /// <summary>For internal use only.</summary>
    public Pen? UnderlinePen;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? FillBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabContextOffice(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BorderPen?.Dispose();
        FillBrush?.Dispose();
        UnderlinePen?.Dispose();

        BorderPen = null;
        FillBrush = null;
        UnderlinePen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonTabContextOffice2010
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonTabContextOffice2010 : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public Pen? BorderInnerPen;
    /// <summary>For internal use only.</summary>
    public Pen? BorderOuterPen;
    /// <summary>For internal use only.</summary>
    public SolidBrush? TopBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BottomBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonTabContextOffice2010(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BorderInnerPen?.Dispose();
        BorderOuterPen?.Dispose();
        TopBrush?.Dispose();
        BottomBrush?.Dispose();

        BorderInnerPen = null;
        BorderOuterPen = null;
        TopBrush = null;
        BottomBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonQATMinibar
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonQATMinibar : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public Pen? LightPen;
    /// <summary>For internal use only.</summary>
    public Pen? BorderPen;
    /// <summary>For internal use only.</summary>
    public Pen? WhitenPen;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BorderPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? TopRight1;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? BottomLeft1;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InnerBrush;

    /// <summary>For internal use only.</summary>
    public MementoRibbonQATMinibar(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5)
        : base(r, color1, color2, color3, color4, color5)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        LightPen?.Dispose();
        BorderPen?.Dispose();
        WhitenPen?.Dispose();
        BorderPath?.Dispose();
        TopRight1?.Dispose();
        BottomLeft1?.Dispose();
        InnerBrush?.Dispose();

        LightPen = null;
        BorderPen = null;
        WhitenPen = null;
        BorderPath = null;
        TopRight1 = null;
        BottomLeft1 = null;
        InnerBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonQATFullbarRound
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonQATFullbarRound : MementoRectThreeColor
{
    /// <summary>For internal use only.</summary>
    public Rectangle InnerRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InnerBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? DarkPath;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? LightPath1;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? LightPath2;
    /// <summary>For internal use only.</summary>
    public Pen? DarkPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonQATFullbarRound(Rectangle r,
        Color color1, Color color2,
        Color color3)
        : base(r, color1, color2, color3)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        InnerBrush?.Dispose();
        DarkPath?.Dispose();
        LightPath1?.Dispose();
        LightPath2?.Dispose();
        DarkPen?.Dispose();

        InnerBrush = null;
        DarkPath = null;
        LightPath1 = null;
        LightPath2 = null;
        DarkPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonQATFullbarSquare
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonQATFullbarSquare : MementoRectThreeColor
{
    /// <summary>For internal use only.</summary>
    public Pen? LightPen;
    /// <summary>For internal use only.</summary>
    public SolidBrush? MediumBrush;
    /// <summary>For internal use only.</summary>
    public Pen? DarkPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonQATFullbarSquare(Rectangle r,
        Color color1, Color color2,
        Color color3)
        : base(r, color1, color2, color3)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        LightPen?.Dispose();
        MediumBrush?.Dispose();
        DarkPen?.Dispose();

        LightPen = null;
        MediumBrush = null;
        DarkPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonQATOverflow
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonQATOverflow : MementoRectTwoColor
{
    /// <summary>For internal use only.</summary>
    public SolidBrush? BackBrush;
    /// <summary>For internal use only.</summary>
    public Pen? BorderPen;

    /// <summary>For internal use only.</summary>
    public MementoRibbonQATOverflow(Rectangle r, Color color1, Color color2)
        : base(r, color1, color2)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BackBrush?.Dispose();
        BorderPen?.Dispose();

        BackBrush = null;
        BorderPen = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoRibbonAppButton
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoRibbonAppButton : MementoRectFiveColor
{
    /// <summary>For internal use only.</summary>
    public RectangleF BorderShadow1;
    /// <summary>For internal use only.</summary>
    public RectangleF BorderShadow2;
    /// <summary>For internal use only.</summary>
    public RectangleF BorderMain1;
    /// <summary>For internal use only.</summary>
    public RectangleF BorderMain2;
    /// <summary>For internal use only.</summary>
    public RectangleF BorderMain3;
    /// <summary>For internal use only.</summary>
    public RectangleF BorderMain4;
    /// <summary>For internal use only.</summary>
    public RectangleF RectLower;
    /// <summary>For internal use only.</summary>
    public RectangleF RectBottomGlow;
    /// <summary>For internal use only.</summary>
    public RectangleF RectUpperGlow;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BrushUpper1;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BrushLower;

    /// <summary>For internal use only.</summary>
    public MementoRibbonAppButton(Rectangle r,
        Color color1, Color color2,
        Color color3, Color color4,
        Color color5)
        : base(r, color1, color2, color3, color4, color5)
    {
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        BrushUpper1?.Dispose();
        BrushUpper1 = null;

        BrushLower?.Dispose();
        BrushLower = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackSolid
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackSolid : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public SolidBrush? SolidBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackSolid(RectangleF dR, Color c1)
    {
        DrawRect = dR;
        Color1 = c1;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR, Color c1)
    {
        var ret = DrawRect.Equals(dR) && Color1.Equals(c1);

        DrawRect = dR;
        Color1 = c1;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        SolidBrush?.Dispose();
        SolidBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackLinear
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackLinear : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public bool Sigma;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? EntireBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackLinear(RectangleF dR,
        bool sig,
        Color c1,
        Color c2,
        VisualOrientation orient)
    {
        DrawRect = dR;
        Sigma = sig;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        bool sig,
        Color c1,
        Color c2,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) &&
                  (Sigma == sig) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2) &&
                  (Orientation == orient);

        DrawRect = dR;
        Sigma = sig;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        EntireBrush?.Dispose();
        EntireBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackLinearRadial
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackLinearRadial : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public Color Color3;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public RectangleF EllipseRect;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? Path;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? BottomBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackLinearRadial(RectangleF dR,
        Color c2,
        Color c3,
        VisualOrientation orient)
    {
        DrawRect = dR;
        Color2 = c2;
        Color3 = c3;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        Color c2,
        Color c3,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color2.Equals(c2) &&
                  Color3.Equals(c3) &&
                  (Orientation == orient);

        DrawRect = dR;
        Color2 = c2;
        Color3 = c3;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (Path != null)
        {
            Path.Dispose();
            BottomBrush?.Dispose();

            Path = null;
            BottomBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackGlassBasic
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackGlassBasic : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public Color GlassColor1;
    /// <summary>For internal use only.</summary>
    public Color GlassColor2;
    /// <summary>For internal use only.</summary>
    public float FactorX;
    /// <summary>For internal use only.</summary>
    public float FactorY;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public float GlassPercent;
    /// <summary>For internal use only.</summary>
    public RectangleF GlassRect;
    /// <summary>For internal use only.</summary>
    public SolidBrush? TotalBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? GlassBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackGlassBasic(RectangleF dR,
        Color c1, Color c2,
        Color gC1, Color gC2,
        float fX, float fY,
        VisualOrientation orient,
        float gP)
    {
        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        GlassColor1 = gC1;
        GlassColor2 = gC2;
        FactorX = fX;
        FactorY = fY;
        Orientation = orient;
        GlassPercent = gP;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        Color c1, Color c2,
        Color gC1, Color gC2,
        float fX, float fY,
        VisualOrientation orient,
        float gP)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color1.Equals(c1) && Color2.Equals(c2) &&
                  GlassColor1.Equals(gC1) && GlassColor2.Equals(gC2) &&
                  (FactorX == fX) && (FactorY == fY) &&
                  (Orientation == orient) &&
                  (GlassPercent == gP);

        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        GlassColor1 = gC1;
        GlassColor2 = gC2;
        FactorX = fX;
        FactorY = fY;
        Orientation = orient;
        GlassPercent = gP;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (TotalBrush != null)
        {
            TotalBrush.Dispose();
            GlassBrush?.Dispose();

            TotalBrush = null;
            GlassBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackGlassLinear
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackGlassLinear : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public RectangleF OuterRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public Color GlassColor1;
    /// <summary>For internal use only.</summary>
    public Color GlassColor2;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public float GlassPercent;
    /// <summary>For internal use only.</summary>
    public RectangleF GlassRect;
    /// <summary>For internal use only.</summary>
    public RectangleF MainRect;
    /// <summary>For internal use only.</summary>
    public SolidBrush? TotalBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? BottomBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackGlassLinear(RectangleF dR, RectangleF oR,
        Color c1, Color c2,
        Color gC1, Color gC2,
        VisualOrientation orient,
        float gP)
    {
        DrawRect = dR;
        OuterRect = oR;
        Color1 = c1;
        Color2 = c2;
        GlassColor1 = gC1;
        GlassColor2 = gC2;
        Orientation = orient;
        GlassPercent = gP;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR, RectangleF oR,
        Color c1, Color c2,
        Color gC1, Color gC2,
        VisualOrientation orient,
        float gP)
    {
        var ret = DrawRect.Equals(dR) &&
                  OuterRect.Equals(oR) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2) &&
                  GlassColor1.Equals(gC1) &&
                  GlassColor2.Equals(gC2) &&
                  (Orientation == orient) &&
                  (GlassPercent == gP);

        DrawRect = dR;
        OuterRect = oR;
        Color1 = c1;
        Color2 = c2;
        GlassColor1 = gC1;
        GlassColor2 = gC2;
        Orientation = orient;
        GlassPercent = gP;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (TotalBrush != null)
        {
            TotalBrush.Dispose();
            TotalBrush = null;

            if (TopBrush != null)
            {
                TopBrush.Dispose();
                BottomBrush?.Dispose();

                TopBrush = null;
                BottomBrush = null;
            }
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackGlassCenter
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackGlassCenter : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? Path;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? BottomBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackGlassCenter(RectangleF dR, Color c2)
    {
        DrawRect = dR;
        Color2 = c2;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR, Color c2)
    {
        var ret = DrawRect.Equals(dR) && Color2.Equals(c2);

        DrawRect = dR;
        Color2 = c2;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (Path != null)
        {
            Path.Dispose();
            BottomBrush?.Dispose();

            Path = null;
            BottomBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackGlassRadial
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackGlassRadial : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public float FactorX;
    /// <summary>For internal use only.</summary>
    public float FactorY;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public RectangleF MainRect;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? Path;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? BottomBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackGlassRadial(RectangleF dR,
        Color c1, Color c2,
        float fX, float fY,
        VisualOrientation orient)
    {
        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        FactorX = fX;
        FactorY = fY;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        Color c1, Color c2,
        float fX, float fY,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2) &&
                  (FactorX == fX) &&
                  (FactorY == fY) &&
                  (Orientation == orient);

        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        FactorX = fX;
        FactorY = fY;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        Path?.Dispose();
        Path = null;

        BottomBrush?.Dispose();
        BottomBrush = null;

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackGlassFade
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackGlassFade : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public RectangleF OuterRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public Color GlassColor1;
    /// <summary>For internal use only.</summary>
    public Color GlassColor2;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public RectangleF GlassRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? MainBrush;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? TopBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackGlassFade(RectangleF dR, RectangleF oR,
        Color c1, Color c2,
        Color gC1, Color gC2,
        VisualOrientation orient)
    {
        DrawRect = dR;
        OuterRect = oR;
        Color1 = c1;
        Color2 = c2;
        GlassColor1 = gC1;
        GlassColor2 = gC2;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR, RectangleF oR,
        Color c1, Color c2,
        Color gC1, Color gC2,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) && OuterRect.Equals(oR) &&
                  Color1.Equals(c1) && Color2.Equals(c2) &&
                  GlassColor1.Equals(gC1) && GlassColor2.Equals(gC2) &&
                  (Orientation == orient);

        DrawRect = dR;
        OuterRect = oR;
        Color1 = c1;
        Color2 = c2;
        GlassColor1 = gC1;
        GlassColor2 = gC2;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (TopBrush != null)
        {
            TopBrush.Dispose();
            MainBrush?.Dispose();

            TopBrush = null;
            MainBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackGlassThreeEdge
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackGlassThreeEdge : MementoDouble
{
    /// <summary>For internal use only.</summary>
    public Rectangle Rect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public Color ColorA1L;
    /// <summary>For internal use only.</summary>
    public Color ColorA2L;
    /// <summary>For internal use only.</summary>
    public Color ColorA2Ll;
    /// <summary>For internal use only.</summary>
    public Color ColorB2Ll;
    /// <summary>For internal use only.</summary>
    public Rectangle RectB;

    /// <summary>For internal use only.</summary>
    public MementoBackGlassThreeEdge(Rectangle r,
        Color c1,
        Color c2,
        VisualOrientation orient)
    {
        Rect = r;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(Rectangle r,
        Color c1,
        Color c2,
        VisualOrientation orient)
    {
        var ret = Rect.Equals(r) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2) &&
                  (Orientation == orient);

        Rect = r;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;

        return ret;
    }
}
#endregion

#region MementoBackDarkEdge
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackDarkEdge : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public int Thickness;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public RectangleF EntireRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? EntireBrush;

    /// <summary>For internal use only.</summary>
    public MementoBackDarkEdge(RectangleF dR,
        Color c1,
        int thick,
        VisualOrientation orient)
    {
        DrawRect = dR;
        Color1 = c1;
        Thickness = thick;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        Color c1,
        int thick,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color1.Equals(c1) &&
                  (Thickness == thick) &&
                  (Orientation == orient);

        DrawRect = dR;
        Color1 = c1;
        Thickness = thick;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (EntireBrush != null)
        {
            EntireBrush.Dispose();
            EntireBrush = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackExpertChecked
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackExpertChecked : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? EntireBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? EllipsePath;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? InsideLighten;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? ClipPath;

    /// <summary>For internal use only.</summary>
    public MementoBackExpertChecked(RectangleF dR,
        Color c1, Color c2,
        VisualOrientation orient)
    {
        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        Color c1, Color c2,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2) &&
                  (Orientation == orient);

        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (EntireBrush != null)
        {
            EntireBrush.Dispose();
            EllipsePath?.Dispose();
            InsideLighten?.Dispose();
            ClipPath?.Dispose();

            EntireBrush = null;
            EllipsePath = null;
            InsideLighten = null;
            ClipPath = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackExpertShadow
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackExpertShadow : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? Path1;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? Path2;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? Path3;
    /// <summary>For internal use only.</summary>
    public SolidBrush? Brush1;
    /// <summary>For internal use only.</summary>
    public SolidBrush? Brush2;
    /// <summary>For internal use only.</summary>
    public SolidBrush? Brush3;

    /// <summary>For internal use only.</summary>
    public MementoBackExpertShadow(RectangleF dR, Color c1, Color c2)
    {
        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR, Color c1, Color c2)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2);

        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (Path1 != null)
        {
            Path1.Dispose();
            Path2?.Dispose();
            Path3?.Dispose();
            Brush1?.Dispose();
            Brush2?.Dispose();
            Brush3?.Dispose();

            Path1 = null;
            Path2 = null;
            Path3 = null;
            Brush1 = null;
            Brush2 = null;
            Brush3 = null;
        }

        base.Dispose(disposing);
    }
}
#endregion

#region MementoBackExpertChecked
/// <summary>
/// Memento used to cache drawing details.
/// </summary>
public class MementoBackExpertSquareHighlight : MementoDisposable
{
    /// <summary>For internal use only.</summary>
    public RectangleF DrawRect;
    /// <summary>For internal use only.</summary>
    public Color Color1;
    /// <summary>For internal use only.</summary>
    public Color Color2;
    /// <summary>For internal use only.</summary>
    public VisualOrientation Orientation;
    /// <summary>For internal use only.</summary>
    public SolidBrush? BackBrush;
    /// <summary>For internal use only.</summary>
    public Rectangle InnerRect;
    /// <summary>For internal use only.</summary>
    public LinearGradientBrush? InnerBrush;
    /// <summary>For internal use only.</summary>
    public GraphicsPath? EllipsePath;
    /// <summary>For internal use only.</summary>
    public PathGradientBrush? InsideLighten;

    /// <summary>For internal use only.</summary>
    public MementoBackExpertSquareHighlight(RectangleF dR,
        Color c1, Color c2,
        VisualOrientation orient)
    {
        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;
    }

    /// <summary>For internal use only.</summary>
    public bool UseCachedValues(RectangleF dR,
        Color c1, Color c2,
        VisualOrientation orient)
    {
        var ret = DrawRect.Equals(dR) &&
                  Color1.Equals(c1) &&
                  Color2.Equals(c2) &&
                  (Orientation == orient);

        DrawRect = dR;
        Color1 = c1;
        Color2 = c2;
        Orientation = orient;

        return ret;
    }

    /// <summary>For internal use only.</summary>
    public override void Dispose(bool disposing)
    {
        if (BackBrush != null)
        {
            BackBrush.Dispose();
            InnerBrush?.Dispose();
            EllipsePath?.Dispose();
            InsideLighten?.Dispose();

            BackBrush = null;
            InnerBrush = null;
            EllipsePath = null;
            InsideLighten = null;
        }

        base.Dispose(disposing);
    }
}
#endregion