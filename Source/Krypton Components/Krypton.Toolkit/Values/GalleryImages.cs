// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for gallery button images.
    /// </summary>
    public class GalleryImages : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GalleryImages class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public GalleryImages(NeedPaintHandler needPaint) 
        {
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create the storage
            Up = new GalleryButtonImages(needPaint);
            Down = new GalleryButtonImages(needPaint);
            DropDown = new GalleryButtonImages(needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Up.IsDefault &&
                                          Down.IsDefault &&
                                          DropDown.IsDefault;

        #endregion

        #region Up
        /// <summary>
        /// Gallery up button images.
        /// </summary>
        [KryptonPersist(true)]
        [Category("Visuals")]
        [Description("Gallery up button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GalleryButtonImages Up { get; }

        #endregion

        #region Down
        /// <summary>
        /// Gallery down button images.
        /// </summary>
        [KryptonPersist(true)]
        [Category("Visuals")]
        [Description("Gallery down button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GalleryButtonImages Down { get; }

        #endregion

        #region DropDown
        /// <summary>
        /// Gallery drop down button images.
        /// </summary>
        [KryptonPersist(true)]
        [Category("Visuals")]
        [Description("Gallery drop down button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GalleryButtonImages DropDown { get; }

        #endregion
    }
}
