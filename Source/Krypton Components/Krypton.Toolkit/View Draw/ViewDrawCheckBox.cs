﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Draws a check box using the provided renderer.
    /// </summary>
    public class ViewDrawCheckBox : ViewLeaf
    {
        #region Instance Fields
        private readonly IPalette _palette;
        private bool _tracking;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawCheckBox class.
        /// </summary>
        /// <param name="palette">Palette for source of drawing values.</param>
        public ViewDrawCheckBox(IPalette palette)
        {
            Debug.Assert(palette != null);
            _palette = palette;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewDrawCheckBox:" + Id;

        #endregion

        #region CheckState
        /// <summary>
        /// Gets and sets the check state of the check box.
        /// </summary>
        public CheckState CheckState { get; set; }

        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the tracking state of the check box.
        /// </summary>
        public bool Tracking
        {
            get => ForcedTracking || _tracking;
            set => _tracking = value;
        }
        #endregion

        #region ForcedTracking
        /// <summary>
        /// Gets and sets the forced tracking state of the checkbox.
        /// </summary>
        public bool ForcedTracking { get; set; }

        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the pressed state of the check box.
        /// </summary>
        public bool Pressed { get; set; }

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Ask the renderer for the required size of the check box
            return context.Renderer.RenderGlyph.GetCheckBoxPreferredSize(context, _palette, 
                                                                         Enabled, CheckState, 
                                                                         Tracking, Pressed);
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        /// <exception cref="ArgumentNullException"></exception>
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
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) =>
            context.Renderer.RenderGlyph.DrawCheckBox(context, ClientRectangle, 
                _palette, Enabled, 
                CheckState, Tracking, 
                Pressed);

        #endregion
    }
}
