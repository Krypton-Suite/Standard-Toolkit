#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// RTL-aware renderer that properly handles RightToLeft and RightToLeftLayout properties.
/// </summary>
public class RTLRenderStandard : RenderStandard
{
    /// <summary>
    /// Gets the effective RTL setting considering both RightToLeft and RightToLeftLayout.
    /// </summary>
    /// <param name="control">The control to check.</param>
    /// <returns>RightToLeft.Yes only when both RTL and RTL Layout are enabled.</returns>
    private static RightToLeft GetEffectiveRTL(Control control)
    {
        // Only apply RTL adjustments when both RTL and RTL Layout are enabled
        if (control.RightToLeft == RightToLeft.Yes)
        {
            // Check if the control supports RightToLeftLayout property
            var rightToLeftLayoutProperty = control.GetType().GetProperty("RightToLeftLayout");
            if (rightToLeftLayoutProperty?.CanRead == true)
            {
                var rightToLeftLayoutValue = rightToLeftLayoutProperty.GetValue(control);
                if (rightToLeftLayoutValue is bool rtlLayout && rtlLayout)
                {
                    return RightToLeft.Yes;
                }
            }
        }
            
        // When RTL Layout is disabled (even if RTL is true), return No to prevent RTL adjustments
        return RightToLeft.No;
    }

    /// <summary>
    /// Override to use effective RTL setting instead of just RightToLeft.
    /// </summary>
    public override Size GetContentPreferredSize(ViewLayoutContext context,
        IPaletteContent palette,
        IContentValues values,
        VisualOrientation orientation,
        PaletteState state)
    {
        // Use effective RTL setting that considers both RightToLeft and RightToLeftLayout
        // This ensures that content mirroring only occurs when both RTL and RTL Layout are enabled
        RightToLeft effectiveRtl = GetEffectiveRTL(context.Control);
        
        // Call the base implementation with the effective RTL setting
        // The base class will handle the content allocation using our effective RTL value
        return base.GetContentPreferredSize(context, palette, values, orientation, state);
    }
}