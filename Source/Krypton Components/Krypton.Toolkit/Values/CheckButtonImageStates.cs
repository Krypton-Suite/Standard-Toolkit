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

using System.Drawing;
using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for check button content value information.
    /// </summary>
    public class CheckButtonImageStates : ButtonImageStates
    {
        #region Instance Fields
        private Image _imageCheckedNormal;
        private Image _imageCheckedPressed;
        private Image _imageCheckedTracking;
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault &&
                                           (ImageCheckedNormal == null) &&
                                           (ImageCheckedPressed == null) &&
                                           (ImageCheckedTracking == null));

        #endregion

        #region ImageCheckedNormal
        /// <summary>
        /// Gets and sets the button image for checked normal state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for checked normal state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DefaultValue(null)]
        public override Image ImageCheckedNormal
        {
            get => _imageCheckedNormal;

            set
            {
                if (_imageCheckedNormal != value)
                {
                    _imageCheckedNormal = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageCheckedNormal()
        {
            return ImageCheckedNormal != null;
        }

        /// <summary>
        /// Resets the ImageCheckedNormal property to its default value.
        /// </summary>
        public void ResetImageCheckedNormal()
        {
            ImageCheckedNormal = null;
        }
        #endregion

        #region ImageCheckedPressed
        /// <summary>
        /// Gets and sets the button image for checked pressed state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for checked pressed state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DefaultValue(null)]
        public override Image ImageCheckedPressed
        {
            get => _imageCheckedPressed;

            set
            {
                if (_imageCheckedPressed != value)
                {
                    _imageCheckedPressed = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageCheckedPressed()
        {
            return ImageCheckedPressed != null;
        }

        /// <summary>
        /// Resets the ImageCheckedPressed property to its default value.
        /// </summary>
        public void ResetImageCheckedPressed()
        {
            ImageCheckedPressed = null;
        }
        #endregion

        #region ImageCheckedTracking
        /// <summary>
        /// Gets and sets the button image for checked tracking state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for checked tracking state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DefaultValue(null)]
        public override Image ImageCheckedTracking
        {
            get => _imageCheckedTracking;

            set
            {
                if (_imageCheckedTracking != value)
                {
                    _imageCheckedTracking = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageCheckedTracking()
        {
            return ImageCheckedTracking != null;
        }

        /// <summary>
        /// Resets the ImageCheckedTracking property to its default value.
        /// </summary>
        public void ResetImageCheckedTracking()
        {
            ImageCheckedTracking = null;
        }
        #endregion

        #region CopyFrom
        /// <summary>
        /// Value copy form the provided source to ourself.
        /// </summary>
        /// <param name="source">Source instance.</param>
        public void CopyFrom(CheckButtonImageStates source)
        {
            base.CopyFrom(source);
            ImageCheckedNormal = source.ImageCheckedNormal;
            ImageCheckedPressed = source.ImageCheckedPressed;
            ImageCheckedTracking = source.ImageCheckedTracking;
        }
        #endregion
    }
}
