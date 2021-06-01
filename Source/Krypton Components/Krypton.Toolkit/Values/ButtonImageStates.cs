﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.ComponentModel;
using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for button content value information.
    /// </summary>
    public class ButtonImageStates : Storage
    {
        #region Instance Fields
        private Image _imageNormal;
        private Image _imageDisabled;
        private Image _imagePressed;
        private Image _imageTracking;
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (ImageNormal == null) &&
                                          (ImageDisabled == null) &&
                                          (ImagePressed == null) &&
                                          (ImageTracking == null);

        #endregion

        #region ImageNormal
        /// <summary>
        /// Gets and sets the button image for normal state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for normal state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public Image ImageNormal
        {
            get => _imageNormal;

            set
            {
                if (_imageNormal != value)
                {
                    _imageNormal = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageNormal()
        {
            return ImageNormal != null;
        }

        /// <summary>
        /// Resets the ImageNormal property to its default value.
        /// </summary>
        public void ResetImageNormal()
        {
            ImageNormal = null;
        }
        #endregion

        #region ImageDisabled
        /// <summary>
        /// Gets and sets the button image for disabled state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for disabled state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public Image ImageDisabled
        {
            get => _imageDisabled;

            set
            {
                if (_imageDisabled != value)
                {
                    _imageDisabled = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageDisabled()
        {
            return ImageDisabled != null;
        }

        /// <summary>
        /// Resets the ImageDisabled property to its default value.
        /// </summary>
        public void ResetImageDisabled()
        {
            ImageDisabled = null;
        }
        #endregion

        #region ImagePressed
        /// <summary>
        /// Gets and sets the button image for pressed state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for pressed state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public Image ImagePressed
        {
            get => _imagePressed;

            set
            {
                if (_imagePressed != value)
                {
                    _imagePressed = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImagePressed()
        {
            return ImagePressed != null;
        }

        /// <summary>
        /// Resets the ImagePressed property to its default value.
        /// </summary>
        public void ResetImagePressed()
        {
            ImagePressed = null;
        }
        #endregion

        #region ImageTracking
        /// <summary>
        /// Gets and sets the button image for tracking state.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image for tracking state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public Image ImageTracking
        {
            get => _imageTracking;

            set
            {
                if (_imageTracking != value)
                {
                    _imageTracking = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageTracking()
        {
            return ImageTracking != null;
        }

        /// <summary>
        /// Resets the ImageTracking property to its default value.
        /// </summary>
        public void ResetImageTracking()
        {
            ImageTracking = null;
        }
        #endregion

        #region ImageCheckedNormal
        /// <summary>
        /// Gets and sets the button image for checked normal state.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Image ImageCheckedNormal
        {
            get { return null; }
            set { }
        }

        #endregion

        #region ImageCheckedPressed
        /// <summary>
        /// Gets and sets the button image for checked pressed state.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Image ImageCheckedPressed
        {
            get { return null; }
            set { }
        }
        #endregion

        #region ImageCheckedTracking
        /// <summary>
        /// Gets and sets the button image for checked tracking state.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Image ImageCheckedTracking
        {
            get { return null; }
            set { }
        }
        #endregion

        #region CopyFrom
        /// <summary>
        /// Value copy form the provided source to ourself.
        /// </summary>
        /// <param name="source">Source instance.</param>
        public virtual void CopyFrom(ButtonImageStates source)
        {
            ImageDisabled = source.ImageDisabled;
            ImageNormal = source.ImageNormal;
            ImagePressed = source.ImagePressed;
            ImageTracking = source.ImageTracking;
        }
        #endregion
    }
}
