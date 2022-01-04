#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Ribbon
{
    /// <summary>
    /// Allocate a spacer for the right side of a window that prevents layout over the min/max/close buttons.
    /// </summary>
    internal class ViewDrawRibbonCompoRightBorder : ViewLeaf
    {
        #region Static Fields

        private const int SPACING_GAP = 10;

        #endregion

        #region Instance Fields

        private int _width;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonCompoRightBorder class.
        /// </summary>
        public ViewDrawRibbonCompoRightBorder()
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewDrawRibbonCompoRightBorder:" + Id;

        #endregion

        #region CompOwnerForm
        /// <summary>
        /// Gets and sets the owner form to use when compositing.
        /// </summary>
        public VisualForm CompOwnerForm { get; set; }

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size preferredSize = Size.Empty;

            // We need an owning form to perform calculations
            if (CompOwnerForm != null)
            {
                // We only have size if custom chrome is being used with composition
                if (CompOwnerForm.ApplyCustomChrome && CompOwnerForm.ApplyComposition)
                {
                    try
                    {
                        // Create structure that will be populated by call to WM_GETTITLEBARINFOEX
                        PI.TITLEBARINFOEX tbi = new();
                        tbi.cbSize = (uint) Marshal.SizeOf(tbi);

                        // Ask the window for the title bar information
                        PI.SendMessage(CompOwnerForm.Handle, PI.WM_.GETTITLEBARINFOEX, IntPtr.Zero, ref tbi);

                        // Find width of the button rectangle
                        var closeWidth = tbi.rcCloseButton.right - tbi.rcCloseButton.left;
                        var helpWidth = tbi.rcHelpButton.right - tbi.rcHelpButton.left;
                        var minWidth = tbi.rcMinimizeButton.right - tbi.rcMinimizeButton.left;
                        var maxWidth = tbi.rcMaximizeButton.right - tbi.rcMaximizeButton.left;

                        var clientWidth = CompOwnerForm.ClientSize.Width;
                        var clientScreenRight = CompOwnerForm.RectangleToScreen(CompOwnerForm.ClientRectangle).Right;
                        var leftMost = clientScreenRight;

                        // Find the left most button edge (start with right side of client area)
                        if ((closeWidth > 0) && (closeWidth < clientWidth))
                        {
                            leftMost = Math.Min(leftMost, tbi.rcCloseButton.left);
                        }

                        if ((helpWidth > 0) && (helpWidth < clientWidth))
                        {
                            leftMost = Math.Min(leftMost, tbi.rcHelpButton.left);
                        }

                        if ((minWidth > 0) && (minWidth < clientWidth))
                        {
                            leftMost = Math.Min(leftMost, tbi.rcMinimizeButton.left);
                        }

                        if ((maxWidth > 0) && (maxWidth < clientWidth))
                        {
                            leftMost = Math.Min(leftMost, tbi.rcMaximizeButton.left);
                        }

                        // Our width is the distance between the left most button edge and the right
                        // side of the client area (this space the buttons are taking up). Plus a small
                        // extra gap between the first button and the caption elements to its left.
                        _width = clientScreenRight - leftMost + SPACING_GAP;

                        preferredSize.Width = _width;
                    }
                    catch(ObjectDisposedException)
                    {
                        // Asking for the WM_GETTITLEBARINFOEX can cause exception if the form level
                        // Icon has already been disposed. This happens in rare circumstances.
                    }
                }
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

            // Start with all the provided space
            ClientRectangle = context.DisplayRectangle;
        }
        #endregion
    }
}
