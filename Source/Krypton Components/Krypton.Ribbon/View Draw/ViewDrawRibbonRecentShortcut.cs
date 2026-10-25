#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draw the shortcut associated with a recent document entry in an application menu.
/// </summary>
internal class ViewDrawRibbonRecentShortcut : ViewDrawContent
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonRecentShortcut class.
    /// </summary>
    /// <param name="paletteContent">Palette source for the content.</param>
    /// <param name="values">Reference to actual content values.</param>
    public ViewDrawRibbonRecentShortcut(IPaletteContent? paletteContent, 
        IContentValues values)
        : base(paletteContent, values, VisualOrientation.Top)
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonRecentShortcut:{Id}";

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context) 
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Only draw the shortcut text if there is some defined
        var shortcut = Values?.GetShortText();
        if (!string.IsNullOrEmpty(shortcut))
        {
            // Only draw shortcut if the shortcut is not equal to the fixed string 'A'
            if (!shortcut!.Equals("A"))
            {
                base.RenderBefore(context);
            }
        }
    }
    #endregion
}