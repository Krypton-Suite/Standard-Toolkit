using System;
using System.Drawing;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// View element that positions the gallery buttons.
    /// </summary>
    internal class ViewLayoutRibbonGalleryButtons : ViewComposite
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGalleryButtons class.
        /// </summary>
        public ViewLayoutRibbonGalleryButtons()
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutRibbonGalleryButtons:" + Id;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size preferredSize = Size.Empty;

            // Height is the total height of all children, but width is just the widest found
            foreach (ViewBase child in this)
            {
                // Ask child for it's own preferred size
                Size childPreferred = child.GetPreferredSize(context);

                // Always add on the height of the child
                preferredSize.Height += childPreferred.Width;

                // Find the widest of the children
                preferredSize.Width = Math.Max(preferredSize.Width, childPreferred.Width);
            }

            return preferredSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Layout children from top to bottom with equal height and the total width
            int yOffset = 0;
            int childHeight = (ClientHeight / Count) + 1;
            foreach (ViewBase child in this)
            {
                // If this is the last child in collection...
                if (child == this[Count - 1])
                {
                    //...then give it all the remainder space
                    childHeight = ClientHeight - yOffset;
                }

                // Position the child
                context.DisplayRectangle = new Rectangle(ClientLocation.X, yOffset, ClientWidth, childHeight);
                child.Layout(context);

                // Move down to next position
                yOffset += (childHeight - 1);
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
