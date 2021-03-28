using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Extends the ViewComposite by laying out children to all fill the total area.
    /// </summary>
    public class ViewLayoutPile : ViewComposite
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutStack class.
        /// </summary>
        public ViewLayoutPile()
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutPile:" + Id;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Ensure all children are layed out in our total space
            base.Layout(context);
        }
        #endregion
    }
}
