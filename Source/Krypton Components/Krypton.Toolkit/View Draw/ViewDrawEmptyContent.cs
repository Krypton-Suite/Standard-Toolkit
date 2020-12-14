// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Drawing;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// View element that draws empty content.
    /// </summary>
    public class ViewDrawEmptyContent : ViewDrawContent,
                                        IContentValues
    {
        #region Instance Fields
        private readonly IPaletteContent _paletteContentNormal;
        private readonly IPaletteContent _paletteContentDisabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawEmptyContent class.
        /// </summary>
        /// <param name="paletteContentDisabled">Palette source for the disabled content.</param>
        /// <param name="paletteContentNormal">Palette source for the normal content.</param>
        public ViewDrawEmptyContent(IPaletteContent paletteContentDisabled,
                                    IPaletteContent paletteContentNormal)
            : base(paletteContentNormal, null, VisualOrientation.Top)
        {
            Values = this;
            _paletteContentDisabled = paletteContentDisabled;
            _paletteContentNormal = paletteContentNormal;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        // Return the class name and instance identifier
        public override string ToString() => "ViewDrawEmptyContent:" + Id;
        #endregion

        #region Layout

        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // ReSharper disable RedundantBaseQualifier
            base.SetPalette(Enabled ? _paletteContentNormal : _paletteContentDisabled);

            return base.GetPreferredSize(context);
            // ReSharper restore RedundantBaseQualifier
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

            // ReSharper disable RedundantBaseQualifier
            base.SetPalette(Enabled ? _paletteContentNormal : _paletteContentDisabled);

            base.Layout(context);
            // ReSharper restore RedundantBaseQualifier
        }
        #endregion

        #region Paint

        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void RenderBefore(RenderContext context) 
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // ReSharper disable RedundantBaseQualifier
            base.SetPalette(Enabled ? _paletteContentNormal : _paletteContentDisabled);

            base.RenderBefore(context);
            // ReSharper restore RedundantBaseQualifier
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state) => null;

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText() => string.Empty;

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText() => string.Empty;
        #endregion
    }
}
