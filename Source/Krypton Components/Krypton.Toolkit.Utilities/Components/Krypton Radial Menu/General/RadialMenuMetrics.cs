#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Logical (96-DPI) constants and a scaled snapshot used for radial menu layout, paint, and hit-testing.
/// </summary>
/// <remarks>
/// Build with <see cref="From"/> so radii auto-fit the available client/working area and all secondary
/// paint metrics derive from the primary logical values × <see cref="LayoutScale"/>.
/// </remarks>
internal readonly struct RadialMenuMetrics
{
    #region Logical constants

    public const int DefaultMenuRadius = 140;
    public const int DefaultInnerRadius = 42;
    public const float DefaultOuterRingThickness = 10f;
    public const float DefaultScale = 1f;
    public const int DefaultItemImageSize = 24;
    public const float DefaultShadowOpacity = 0.18f;
    public const int DefaultShadowBlur = 14;
    public const int DefaultShadowOffset = 4;
    public const float DefaultStartAngle = -90f;
    public const float DefaultHitPadding = 4f;
    public const int DefaultAnimationDurationMs = 220;
    public const string DefaultHubText = @"+";
    public const string DefaultSubMenuGlyph = @"›";

    public const int MinMenuRadius = 60;
    public const int MinInnerRadius = 16;
    public const int MinInnerOuterGap = 24;
    public const int MinInnerWhenAutoAdjust = 20;
    public const int InnerAutoAdjustOuterGap = 20;
    public const int DefaultInnerRadiusDivisor = 3;
    public const float MaxOuterRingThickness = 16f;
    public const float MinScale = 0.5f;
    public const float MaxScale = 3f;
    public const int MinItemImageSize = 8;
    public const int MaxItemImageSize = 64;
    public const int MaxShadowBlur = 48;
    public const int MaxShadowOffset = 32;
    public const int MaxVisibleItemsCap = 64;
    public const float MaxHitPadding = 24f;
    public const int MaxAnimationDurationMs = 2000;
    public const float MinLayoutScale = 0.25f;

    /// <summary>Extra client pixels beyond <c>2 × radius</c> for the circular surface.</summary>
    public const int ClientDiameterPadding = 8;

    /// <summary>Extra soft padding around the shadow halo beyond blur + hole inset.</summary>
    public const int ShadowPadExtra = 2;

    /// <summary>Inset punched in the shadow so Magenta fringe sits under the opaque popup.</summary>
    public const int ShadowHoleInset = 3;

    /// <summary>Logical drag threshold (scaled at use).</summary>
    public const int MoveDragThresholdLogical = 8;

    /// <summary>Animation timer interval targeting ~60 fps.</summary>
    public const int AnimationFrameIntervalMs = 16;

    /// <summary>Visible editor page size (font list / calendar day ring).</summary>
    public const int EditorPageSize = 8;

    public const float SectorBorderNormalLogical = 1f;
    public const float SectorBorderTrackingLogical = 2.5f;
    public const float CenterBorderLogical = 2f;
    public const float SectorCaptionFontPtLogical = 8f;
    public const float CheckedGlyphFontPtLogical = 10f;
    public const float BackChevronFontPtLogical = 14f;
    public const float HubCloseFontPtLogical = 10f;
    public const float CenterCaptionShortFontPtLogical = 9.5f;
    public const float CenterCaptionLongFontPtLogical = 8f;
    public const float EditorLabelFontPtLogical = 9f;
    public const float SliderValueFontPtLogical = 11f;
    public const float MinFontPt = 6f;
    public const int CenterCaptionLongThreshold = 12;
    public const float HubTextFontFraction = 0.45f;
    public const float MinHubRadiusLogical = 12f;

    public const float RingArcGapMaxDeg = 2.5f;
    public const float RingArcGapMinDeg = 0.8f;
    public const float RingArcGapFrac = 0.04f;
    public const float MinRingArcSweepDeg = 0.5f;

    public const float SubMenuGlyphFontBaseLogical = 12f;
    public const float SubMenuGlyphFontMinLogical = 14f;
    public const float SubMenuGlyphFontMaxLogical = 22f;
    public const float SubMenuGlyphFontRingFactor = 1.25f;
    public const float SubMenuGlyphMinRingLogical = 4f;

    #endregion

    #region Scaled snapshot

    /// <summary>Combined DPI × <see cref="KryptonRadialMenuValues.Scale"/> factor.</summary>
    public float LayoutScale { get; }

    /// <summary>Effective outer radius after scale and viewport clamp (device pixels).</summary>
    public int MenuRadius { get; }

    /// <summary>Effective inner radius after scale and proportional clamp (device pixels).</summary>
    public int InnerRadius { get; }

    /// <summary>Preferred outer radius before viewport clamp (device pixels).</summary>
    public int PreferredMenuRadius { get; }

    /// <summary>Preferred inner radius before viewport clamp (device pixels).</summary>
    public int PreferredInnerRadius { get; }

    public float OuterRingThickness { get; }
    public int ItemImageSize { get; }
    public float HitPadding { get; }
    public int ShadowBlur { get; }
    public int ShadowOffset { get; }
    public int MoveDragThreshold { get; }

    public float SectorBorderNormal { get; }
    public float SectorBorderTracking { get; }
    public float CenterBorder { get; }
    public float ImageTextSpacing { get; }
    public float SectorTextHalfWidth { get; }
    public float SectorTextHeight { get; }
    public float SectorCaptionFontPt { get; }
    public float CheckedGlyphFontPt { get; }
    public float BackChevronFontPt { get; }
    public float HubCloseFontPt { get; }
    public float CenterCaptionShortFontPt { get; }
    public float CenterCaptionLongFontPt { get; }
    public float EditorLabelFontPt { get; }
    public float SliderValueFontPt { get; }
    public float CenterGlyphSize { get; }
    public float MinSectorBodyThickness { get; }
    public float CheckedDotDiameter { get; }
    public float CheckedGlyphOffsetX { get; }
    public float CheckedGlyphOffsetY { get; }
    public float CheckedDotOffsetX { get; }
    public float CheckedDotOffsetY { get; }
    public float SectorTextOffsetStacked { get; }
    public float SectorTextOffsetPlain { get; }
    public float SliderTrackInset { get; }

    #endregion

    private RadialMenuMetrics(
        float layoutScale,
        int menuRadius,
        int innerRadius,
        int preferredMenuRadius,
        int preferredInnerRadius,
        float outerRingThickness,
        int itemImageSize,
        float hitPadding,
        int shadowBlur,
        int shadowOffset,
        int moveDragThreshold)
    {
        LayoutScale = layoutScale;
        MenuRadius = menuRadius;
        InnerRadius = innerRadius;
        PreferredMenuRadius = preferredMenuRadius;
        PreferredInnerRadius = preferredInnerRadius;
        OuterRingThickness = outerRingThickness;
        ItemImageSize = itemImageSize;
        HitPadding = hitPadding;
        ShadowBlur = shadowBlur;
        ShadowOffset = shadowOffset;
        MoveDragThreshold = moveDragThreshold;

        SectorBorderNormal = SectorBorderNormalLogical * layoutScale;
        SectorBorderTracking = SectorBorderTrackingLogical * layoutScale;
        CenterBorder = CenterBorderLogical * layoutScale;
        ImageTextSpacing = Math.Max(MinItemImageSize * layoutScale * 0.5f, itemImageSize * 0.5f);
        SectorTextHalfWidth = itemImageSize * 1.5f;
        SectorTextHeight = itemImageSize * (4f / 3f);
        SectorCaptionFontPt = Math.Max(MinFontPt, SectorCaptionFontPtLogical * layoutScale);
        CheckedGlyphFontPt = Math.Max(MinFontPt, CheckedGlyphFontPtLogical * layoutScale);
        BackChevronFontPt = Math.Max(MinFontPt, BackChevronFontPtLogical * layoutScale);
        HubCloseFontPt = Math.Max(MinFontPt, HubCloseFontPtLogical * layoutScale);
        CenterCaptionShortFontPt = Math.Max(MinFontPt, CenterCaptionShortFontPtLogical * layoutScale);
        CenterCaptionLongFontPt = Math.Max(MinFontPt, CenterCaptionLongFontPtLogical * layoutScale);
        EditorLabelFontPt = Math.Max(MinFontPt, EditorLabelFontPtLogical * layoutScale);
        SliderValueFontPt = Math.Max(MinFontPt, SliderValueFontPtLogical * layoutScale);
        CenterGlyphSize = Math.Min(itemImageSize, innerRadius);
        MinSectorBodyThickness = Math.Max(MinInnerOuterGap * layoutScale * 0.33f, OuterRingThickness);
        CheckedDotDiameter = itemImageSize * (5.5f / 24f);
        CheckedGlyphOffsetX = itemImageSize * (22f / 24f);
        CheckedGlyphOffsetY = itemImageSize * (18f / 24f);
        CheckedDotOffsetX = itemImageSize;
        CheckedDotOffsetY = itemImageSize * (16f / 24f);
        SectorTextOffsetStacked = itemImageSize * (14f / 24f);
        SectorTextOffsetPlain = itemImageSize * 0.5f;
        SliderTrackInset = Math.Max(OuterRingThickness, ClientDiameterPadding * layoutScale);
    }

    /// <summary>
    /// Builds scaled metrics from values, device DPI factor, and the available client/working-area size.
    /// </summary>
    /// <param name="values">Logical appearance values.</param>
    /// <param name="dpiScale">Device DPI / 96.</param>
    /// <param name="availableClient">Available size to fit the disc into (client or working area).</param>
    /// <returns>Scaled metrics with viewport clamp applied.</returns>
    public static RadialMenuMetrics From(KryptonRadialMenuValues values, float dpiScale, Size availableClient)
    {
        if (dpiScale < MinLayoutScale)
        {
            dpiScale = 1f;
        }

        var layoutScale = Math.Max(MinLayoutScale, dpiScale * values.Scale);
        var preferredOuter = Math.Max(MinMenuRadius, (int)Math.Round(values.MenuRadius * layoutScale));
        var preferredInner = Math.Max(MinInnerRadius, (int)Math.Round(values.InnerRadius * layoutScale));
        preferredInner = Math.Min(preferredInner, Math.Max(MinInnerRadius, preferredOuter - ScaleGap(layoutScale)));

        var outer = preferredOuter;
        var inner = preferredInner;
        var available = Math.Min(availableClient.Width, availableClient.Height);
        if (available > 0)
        {
            var maxDiameter = Math.Max(DiameterFromRadius(MinMenuRadius), available);
            var maxOuter = Math.Max(MinMenuRadius, (maxDiameter - ClientDiameterPadding) / 2);
            if (outer > maxOuter)
            {
                var ratio = preferredOuter > 0 ? preferredInner / (float)preferredOuter : 0.3f;
                outer = maxOuter;
                inner = Math.Max(MinInnerRadius, (int)Math.Round(outer * ratio));
                inner = Math.Min(inner, Math.Max(MinInnerRadius, outer - ScaleGap(layoutScale)));
            }
        }

        var ring = Math.Max(0f, Math.Min(MaxOuterRingThickness, values.OuterRingThickness)) * layoutScale;
        var imageSize = Math.Max(1, (int)Math.Round(Math.Max(MinItemImageSize, Math.Min(MaxItemImageSize, values.ItemImageSize)) * layoutScale));
        var hitPadding = Math.Max(0f, Math.Min(MaxHitPadding, values.HitPadding)) * layoutScale;
        var shadowBlur = Math.Max(0, (int)Math.Round(Math.Max(0, Math.Min(MaxShadowBlur, values.ShadowBlur)) * layoutScale));
        var shadowOffset = (int)Math.Round(Math.Max(0, Math.Min(MaxShadowOffset, values.ShadowOffset)) * layoutScale);
        var drag = Math.Max(1, (int)Math.Round(MoveDragThresholdLogical * layoutScale));

        return new RadialMenuMetrics(
            layoutScale,
            outer,
            inner,
            preferredOuter,
            preferredInner,
            ring,
            imageSize,
            hitPadding,
            shadowBlur,
            shadowOffset,
            drag);
    }

    /// <summary>
    /// Client diameter for a circular surface of the given radius.
    /// </summary>
    public static int DiameterFromRadius(int radius) => (Math.Max(0, radius) * 2) + ClientDiameterPadding;

    /// <summary>
    /// Minimum control size derived from <see cref="MinMenuRadius"/>.
    /// </summary>
    public static Size MinControlSize => new(DiameterFromRadius(MinMenuRadius), DiameterFromRadius(MinMenuRadius));

    /// <summary>
    /// Shadow window padding for the given blur.
    /// </summary>
    public int ShadowPadding => ShadowBlur + ShadowHoleInset + ShadowPadExtra;

    /// <summary>
    /// Sector body outer radius so fills stop inside the outer-ring stroke.
    /// </summary>
    public float SectorBodyOuterRadius(float outer, float inner) =>
        Math.Max(inner + MinSectorBodyThickness, outer - OuterRingThickness);

    /// <summary>
    /// Font size for the outer-ring submenu chevron from the scaled ring thickness.
    /// </summary>
    public float SubMenuGlyphFontPt =>
        Math.Max(
            SubMenuGlyphFontMinLogical * LayoutScale,
            Math.Min(
                SubMenuGlyphFontMaxLogical * LayoutScale,
                (SubMenuGlyphFontBaseLogical * LayoutScale) + (Math.Max(SubMenuGlyphMinRingLogical * LayoutScale, OuterRingThickness) * SubMenuGlyphFontRingFactor)));

    /// <summary>
    /// Radial inset for the submenu glyph when the ring stroke is hidden.
    /// </summary>
    public float SubMenuGlyphInsetWhenRingHidden =>
        Math.Max(OuterRingThickness, DefaultOuterRingThickness * LayoutScale);

    /// <summary>
    /// Content label rectangle centred on a sector content point.
    /// </summary>
    public RectangleF SectorContentTextRect(PointF content, float textY) =>
        new(content.X - SectorTextHalfWidth, textY, SectorTextHalfWidth * 2f, SectorTextHeight);

    /// <summary>
    /// Compact editor label rectangle (font names, text options, calendar days).
    /// </summary>
    public RectangleF EditorLabelRect(PointF content, float widthFactor = 1f) =>
        new(
            content.X - (SectorTextHalfWidth * widthFactor),
            content.Y - (SectorTextHeight * 0.3f),
            SectorTextHalfWidth * 2f * widthFactor,
            SectorTextHeight * 0.6f);

    private static int ScaleGap(float layoutScale) =>
        Math.Max(MinInnerOuterGap, (int)Math.Round(MinInnerOuterGap * layoutScale));
}
