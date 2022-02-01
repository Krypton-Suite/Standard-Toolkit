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
    /// Positions the quick access toolbar extra button for the minibar in the caption.
    /// </summary>
    internal class ViewDrawRibbonQATExtraButtonMini : ViewDrawRibbonQATExtraButton
    {
        private readonly int MINI_BUTTON_HEIGHT; // = 22;
        private readonly int MINI_BUTTON_OFFSET; // = 24;
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonQATExtraButtonMini class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonQATExtraButtonMini(KryptonRibbon ribbon,
                                                NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            MINI_BUTTON_HEIGHT = (int)(22 * FactorDpiY);
            MINI_BUTTON_OFFSET = (int)(24 * FactorDpiX);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonQATExtraButtonMini:" + Id;

        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            Rectangle clientRect = context.DisplayRectangle;

            // For the minibar we have to position ourself at bottom of available area
            clientRect.Y = clientRect.Bottom - 1 - MINI_BUTTON_OFFSET;
            clientRect.Height = MINI_BUTTON_HEIGHT;

            // Use modified size to position base class and children
            context.DisplayRectangle = clientRect;

            // Let children be laid out inside border area
            base.Layout(context);

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
