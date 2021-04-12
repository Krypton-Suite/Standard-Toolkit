#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Override the contained child to present a fixed size.
    /// </summary>
    public class ViewDecoratorFixedSize : ViewDecorator
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewBase class.
        /// </summary>
        public ViewDecoratorFixedSize(ViewBase child, Size fixedSize)
            : base(child)
        {
            FixedSize = fixedSize;

        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDecoratorFixedSize:" + Id;
        }
        #endregion

        #region FixedSize
        /// <summary>
        /// Gets and sets the fixed size for laying out the contained child element.
        /// </summary>
        public Size FixedSize { get; set; }

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Always provide the requested fixed size
            return FixedSize;
        }
        #endregion
    }
}
