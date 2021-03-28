using System.Drawing;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirects requests for context menu images from the ContextMenuImages instance.
    /// </summary>
    public class PaletteRedirectContextMenu : PaletteRedirect
    {
        #region Instance Fields
        private readonly ContextMenuImages _images;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectContextMenu class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="images">Reference to source of context menu images.</param>
        public PaletteRedirectContextMenu(IPalette target,
                                          ContextMenuImages images)
            : base(target)
        {
            Debug.Assert(images != null);

            // Remember incoming target
            _images = images;
        }
        #endregion

        #region Images
        /// <summary>
        /// Gets a checked image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuCheckedImage()
        {
            Image retImage = _images.Checked ?? Target.GetContextMenuCheckedImage();

            // Not found, then inherit from target

            return retImage;
        }

        /// <summary>
        /// Gets a indeterminate image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuIndeterminateImage()
        {
            Image retImage = _images.Indeterminate ?? Target.GetContextMenuIndeterminateImage();

            // Not found, then inherit from target

            return retImage;
        }

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuSubMenuImage()
        {
            Image retImage = _images.SubMenu ?? Target.GetContextMenuSubMenuImage();

            // Not found, then inherit from target

            return retImage;
        }
        #endregion
    }
}
