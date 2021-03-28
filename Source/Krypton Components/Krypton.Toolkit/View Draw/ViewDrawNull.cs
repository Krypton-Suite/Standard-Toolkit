using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Draw a red rectangle in the location of the null element.
    /// </summary>
    public class ViewDrawNull : ViewLayoutNull
    {
        #region Instance Fields
        private readonly Color _fillColor;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawNull class.
        /// </summary>
        /// <param name="fillColor">Color to fill drawing area.</param>
        public ViewDrawNull(Color fillColor)
        {
            _fillColor = fillColor;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawNull:" + Id;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            using(SolidBrush fillBrush = new SolidBrush(_fillColor))
            {
                context.Graphics.FillRectangle(fillBrush, ClientRectangle);
            }
        }
        #endregion
    }
}
