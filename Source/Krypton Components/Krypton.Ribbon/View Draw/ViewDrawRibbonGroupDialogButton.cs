// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a dialog box launcher button for a group.
    /// </summary>
    internal class ViewDrawRibbonGroupDialogButton : ViewLeaf
    {
        #region Static Fields
        // Button is 8 for context image, 4 for context padding and 2 for border drawing
        private static readonly Size _viewSize = new Size(14, 14);
        // Inflate size to convert from view size to content size
        private static readonly Size _contentSize = new Size(-3, -3);
        #endregion

        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private readonly KryptonRibbonGroup _ribbonGroup;
        private IDisposable _mementoBack;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupDialogButton class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGroup">Reference to ribbon group this represents.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupDialogButton(KryptonRibbon ribbon,
                                               KryptonRibbonGroup ribbonGroup,
                                               NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGroup != null);

            // Remember incoming references
            _ribbon = ribbon;
            _ribbonGroup = ribbonGroup;

            // Attach a controller to this element for the pressing of the button
            DialogLauncherButtonController controller = new DialogLauncherButtonController(ribbon, this, needPaint);
            controller.Click += OnClick;
            MouseController = controller;
            SourceController = controller;
            KeyController = controller;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonGroupButton:" + Id;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementoBack != null)
                {
                    _mementoBack.Dispose();
                    _mementoBack = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region DialogButtonController
        /// <summary>
        /// Gets access to the controller used for the button.
        /// </summary>
        public DialogLauncherButtonController DialogButtonController => SourceController as DialogLauncherButtonController;

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            return _viewSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            IPaletteBack paletteBack = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBack;
            IPaletteBorder paletteBorder = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBorder;
            IPaletteRibbonGeneral paletteGeneral = _ribbon.StateCommon.RibbonGeneral;

            // Do we need to draw the background?
            if (paletteBack.GetBackDraw(State) == InheritBool.True)
            {
                // Get the border path which the background is clipped to drawing within
                using (GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, paletteBorder, VisualOrientation.Top, State))
                {
                    Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, State, VisualOrientation.Top);

                    // Apply the padding depending on the orientation
                    Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, ClientRectangle, borderPadding);

                    // Render the background inside the border path
                    _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                                                                                paletteBack, VisualOrientation.Top,
                                                                                State, _mementoBack);
                }
            }

            // Do we need to draw the border?
            if (paletteBorder.GetBorderDraw(State) == InheritBool.True)
            {
                context.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, paletteBorder, 
                    VisualOrientation.Top, State);
            }

            // Find the content area inside the button rectangle
            Rectangle contentRect = ClientRectangle;
            contentRect.Inflate(_contentSize);

            // Draw the dialog box launcher glyph in the center
            context.Renderer.RenderGlyph.DrawRibbonDialogBoxLauncher(_ribbon.RibbonShape, context, contentRect, paletteGeneral, State);
        }
        #endregion

        #region Implementation
        private void OnClick(object sender, MouseEventArgs e)
        {
            // We do not operate the dialog launcher at design time
            if (!_ribbon.InDesignMode)
            {
                // Fire the group defined event that indicates dialog box launcher button pressed
                _ribbonGroup.OnDialogBoxLauncherClick(EventArgs.Empty);
            }
        }
        #endregion
    }
}
