#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Specifies the color drawing style.
    /// </summary>
    public enum PaletteColorStyle
    {
        /// <summary>
        /// Specifies color should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies drawing as a series of dashes.
        /// </summary>
        Dashed,

        /// <summary>
        /// Specifies solid drawing instead of a gradient.
        /// </summary>
        Solid,

        /// <summary>
        /// Specifies solid block using the first color but with a line of second color one pixel inside.
        /// </summary>
        SolidInside,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on right edge.
        /// </summary>
        SolidRightLine,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on left edge.
        /// </summary>
        SolidLeftLine,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on top edge.
        /// </summary>
        SolidTopLine,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on bottom edge.
        /// </summary>
        SolidBottomLine,

        /// <summary>
        /// Specifies solid block using the first color and a rectangle of second color around all edges.
        /// </summary>
        SolidAllLine,

        /// <summary>
        /// Specifies a switch between the first and second colors at 25 percent of distance.
        /// </summary>
        Switch25,

        /// <summary>
        /// Specifies a switch between the first and second colors at 33 percent of distance.
        /// </summary>
        Switch33,

        /// <summary>
        /// Specifies a switch between the first and second colors at 50 percent of distance.
        /// </summary>
        Switch50,

        /// <summary>
        /// Specifies a switch between the first and second colors at 90 percent of distance.
        /// </summary>
        Switch90,

        /// <summary>
        /// Specifies a straight line gradient.
        /// </summary>
        Linear,

        /// <summary>
        /// Specifies the the first 25 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear25,

        /// <summary>
        /// Specifies the the first 33 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear33,

        /// <summary>
        /// Specifies the the first 40 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear40,

        /// <summary>
        /// Specifies the the first 50 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear50,

        /// <summary>
        /// Specifies a straight line gradient with shadow around the inner edge.
        /// </summary>
        LinearShadow,

        /// <summary>
        /// Specifies a rounded gradient by using a non-linear falloff.
        /// </summary>
        Rounded,

        /// <summary>
        /// Specifies a rounded look using a second variant blend of the two colors.
        /// </summary>
        Rounding2,

        /// <summary>
        /// Specifies a rounded look using a third variant blend of the two colors.
        /// </summary>
        Rounding3,

        /// <summary>
        /// Specifies a rounded look using a fourth variant blend of the two colors.
        /// </summary>
        Rounding4,

        /// <summary>
        /// Specifies a rounded look using a fifth variant blend of the two colors.
        /// </summary>
        Rounding5,

        /// <summary>
        /// Specifies a rounded gradient by using a non-linear falloff but with the top edge having light version of Color1.
        /// </summary>
        RoundedTopLight,

        /// <summary>
        /// Specifies a rounded gradient by using a non-linear falloff but with the top and left edges having a white border.
        /// </summary>
        RoundedTopLeftWhite,

        /// <summary>
        /// Specifies a sigma curve that peeks in the center.
        /// </summary>
        Sigma,

        /// <summary>
        /// Specifies a gradient effect in the first and second halfs of the area.
        /// </summary>
        HalfCut,

        /// <summary>
        /// Specifies first color fades into second color mostly within the first quarter of area.
        /// </summary>
        QuarterPhase,

        /// <summary>
        /// Specifies color transition similar to Microsoft OneNote.
        /// </summary>
        OneNote,

        /// <summary>
        /// Specifies a simple glass effect with three edges lighter.
        /// </summary>
        GlassThreeEdge,

        /// <summary>
        /// Specifies a simple glass effect.
        /// </summary>
        GlassSimpleFull,

        /// <summary>
        /// Specifies a full glass effect appropriate for a normal state.
        /// </summary>
        GlassNormalFull,

        /// <summary>
        /// Specifies a full glass effect appropriate for a tracking state.
        /// </summary>
        GlassTrackingFull,

        /// <summary>
        /// Specifies a full glass effect appropriate for a pressed state.
        /// </summary>
        GlassPressedFull,

        /// <summary>
        /// Specifies a full glass effect appropriate for a checked state.
        /// </summary>
        GlassCheckedFull,

        /// <summary>
        /// Specifies a full glass effect appropriate for a checked/tracking state.
        /// </summary>
        GlassCheckedTrackingFull,

        /// <summary>
        /// Specifies a stumpy glass effect appropriate for a normal state.
        /// </summary>
        GlassNormalStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropriate for a tracking state.
        /// </summary>
        GlassTrackingStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropriate for a pressed state.
        /// </summary>
        GlassPressedStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropriate for a checked state.
        /// </summary>
        GlassCheckedStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropriate for a checked/tracking state.
        /// </summary>
        GlassCheckedTrackingStump,

        /// <summary>
        /// Specifies a simple glass effect appropriate for a normal state.
        /// </summary>
        GlassNormalSimple,

        /// <summary>
        /// Specifies a simple glass effect appropriate for a tracking state.
        /// </summary>
        GlassTrackingSimple,

        /// <summary>
        /// Specifies a simple glass effect appropriate for a pressed state.
        /// </summary>
        GlassPressedSimple,

        /// <summary>
        /// Specifies a simple glass effect appropriate for a checked state.
        /// </summary>
        GlassCheckedSimple,

        /// <summary>
        /// Specifies a simple glass effect appropriate for a checked/tracking state.
        /// </summary>
        GlassCheckedTrackingSimple,

        /// <summary>
        /// Specifies a glass effect with fading from the center.
        /// </summary>
        GlassCenter,

        /// <summary>
        /// Specifies a glass effect with fading from the bottom.
        /// </summary>
        GlassBottom,

        /// <summary>
        /// Specifies a simple glass effect that fades away to nothing by end of the area.
        /// </summary>
        GlassFade,

        /// <summary>
        /// Specifies an expert style button with tracking effect.
        /// </summary>
        ExpertTracking,

        /// <summary>
        /// Specifies an expert style button with pressed effect.
        /// </summary>
        ExpertPressed,

        /// <summary>
        /// Specifies an expert style button that is checked.
        /// </summary>
        ExpertChecked,

        /// <summary>
        /// Specifies an expert style button that is checked with tracking effect.
        /// </summary>
        ExpertCheckedTracking,

        /// <summary>
        /// Specifies an expert style button that has a square inner area with highlighting.
        /// </summary>
        ExpertSquareHighlight,

        /// <summary>
        /// Specifies an expert style button that has a square inner area with highlighting variation 2.
        /// </summary>
        ExpertSquareHighlight2
    }
}