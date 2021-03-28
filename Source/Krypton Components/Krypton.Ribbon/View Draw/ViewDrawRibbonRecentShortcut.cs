using System;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
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
        public ViewDrawRibbonRecentShortcut(IPaletteContent paletteContent, 
                                            IContentValues values)
            : base(paletteContent, values, VisualOrientation.Top)
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonRecentShortcut:" + Id;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Only draw the shortcut text if there is some defined
            string shortcut = Values.GetShortText();
            if (!string.IsNullOrEmpty(shortcut))
            {
                // Only draw shortcut if the shortcut is not equal to the fixed string 'A'
                if (!shortcut.Equals("A"))
                {
                    base.RenderBefore(context);
                }
            }
        }
        #endregion
    }
}
