﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Encapsulates context for view layout operations.
    /// </summary>
    public class ViewLayoutContext : ViewContext
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewContext class.
        /// </summary>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="renderer">Rendering provider.</param>
        public ViewLayoutContext(Control control,
                                 IRenderer renderer)
            : this(null, control, control, null, renderer, control.Size)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewContext class.
        /// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used for aligning elements.</param>
        /// <param name="renderer">Rendering provider.</param>
        public ViewLayoutContext(ViewManager manager,
                                 Control control,
                                 Control alignControl,
                                 IRenderer renderer)
            : this(manager, control, alignControl, null, renderer, control.Size)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewContext class.
        /// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used for aligning elements.</param>
        /// <param name="renderer">Rendering provider.</param>
        /// <param name="displaySize">Display size.</param>
        public ViewLayoutContext(ViewManager manager,
                                 Control control,
                                 Control alignControl,
                                 IRenderer renderer,
                                 Size displaySize)
            : this(manager, control, alignControl, null, renderer, displaySize)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewContext class.
        /// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="form">Form associated with rendering.</param>
        /// <param name="formRect">Window rectangle for the Form.</param>
        /// <param name="renderer">Rendering provider.</param>
        public ViewLayoutContext(ViewManager manager,
                                 Form form,
                                 Rectangle formRect,
                                 IRenderer renderer)
            : base(manager, form, form, null, renderer) =>
            // The initial display rectangle is the provided size
            DisplayRectangle = new Rectangle(Point.Empty, formRect.Size);

        /// <summary>
        /// Initialize a new instance of the ViewContext class.
        /// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used for aligning elements.</param>
        /// <param name="graphics">Graphics instance for drawing.</param>
        /// <param name="renderer">Rendering provider.</param>
        /// <param name="displaySize">Display size.</param>
        public ViewLayoutContext(ViewManager manager,
                                 Control control,
                                 Control alignControl,
                                 Graphics graphics,
                                 IRenderer renderer,
                                 Size displaySize)
            : base(manager, control, alignControl, graphics, renderer) =>
            // The initial display rectangle is the provided size
            DisplayRectangle = new Rectangle(Point.Empty, displaySize);

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets and sets the available display area.
        /// </summary>
        public Rectangle DisplayRectangle { get; set; }

        public RectangleF DisplayRectangleF { get; set; }

        #endregion
    }
}
