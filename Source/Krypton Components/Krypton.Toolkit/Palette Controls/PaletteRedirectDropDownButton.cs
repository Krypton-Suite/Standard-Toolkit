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


namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirects requests for drop down button images from the DropDownButtonImages instance.
    /// </summary>
    public class PaletteRedirectDropDownButton : PaletteRedirect
    {
        #region Instance Fields
        private readonly DropDownButtonImages _images;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDropDownButton class.
        /// </summary>
        /// <param name="images">Reference to source of drop down button images.</param>
        public PaletteRedirectDropDownButton(DropDownButtonImages images)
            : this(null, images)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDropDownButton class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="images">Reference to source of drop down button images.</param>
        public PaletteRedirectDropDownButton(IPalette target,
                                             DropDownButtonImages images)
            : base(target)
        {
            Debug.Assert(images != null);

            // Remember incoming target
            _images = images;
        }
        #endregion

        #region Images
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image GetDropDownButtonImage(PaletteState state)
        {
            // Grab state specific image
            Image retImage = state switch
            {
                PaletteState.Disabled => _images.Disabled,
                PaletteState.Normal => _images.Normal,
                PaletteState.Tracking => _images.Tracking,
                PaletteState.Pressed => _images.Pressed,
                _ => null
            };

            // Not found, then get the common image
            if (retImage == null)
            {
                retImage = _images.Common;
            }

            // Not found, then inherit from target
            return retImage ?? Target.GetDropDownButtonImage(state);
        }
        #endregion
    }
}
