﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// View element that draws nothing and will center all children within itself.
    /// </summary>
    internal class ViewLayoutRibbonCenter : ViewComposite
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonCenterButton class.
        /// </summary>
        public ViewLayoutRibbonCenter()
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            $"ViewLayoutRibbonCenter:{Id}";

        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout([DisallowNull] ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Layout each child centered within this space
            foreach (ViewBase child in this)
            {
                // Only layout visible children
                if (child.Visible)
                {
                    // Ask child for it's own preferred size
                    Size childPreferred = child.GetPreferredSize(context);

                    // Make sure the child is never bigger than the available space
                    if (childPreferred.Width > ClientWidth)
                    {
                        childPreferred.Width = ClientWidth;
                    }
                    if (childPreferred.Height > ClientHeight)
                    {
                        childPreferred.Height = ClientHeight;
                    }

                    // Find vertical and horizontal offsets for centering
                    var xOffset = (ClientWidth - childPreferred.Width) / 2;
                    var yOffset = (ClientHeight - childPreferred.Height) / 2;

                    // Create the rectangle that centers the child in our space
                    context.DisplayRectangle = new Rectangle(ClientRectangle.X + xOffset,
                                                             ClientRectangle.Y + yOffset,
                                                             childPreferred.Width,
                                                             childPreferred.Height);

                    // Finally ask the child to layout
                    child.Layout(context);
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
