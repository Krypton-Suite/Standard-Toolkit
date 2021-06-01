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

using System;
using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    public class BlurValues : Storage
    {
        #region statics
        private bool _enableBlur;
        private byte _radius;
        private const byte _radiusDefault = 4;
        private double _opacity;
        private const double _opacityDefault = 50.0;
        private bool _blurWhenFocusLost;
        #endregion

        #region Events
#pragma warning disable 1591
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler EnableBlurChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler RadiusChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler OpacityChanged;
#pragma warning restore 1591
        #endregion

        #region Identity
        /// <summary>
        /// 
        /// </summary>
        public BlurValues()
        {
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            ResetEnableBlur();
            ResetRadius();
            ResetOpacity();
            ResetBlurWhenFocusLost();
        }
        #endregion Identity

        /// <summary>
        /// </summary>
        [Description("Blur this when not Active")]
        [DefaultValue(false)]
        public bool EnableBlur
        {
            get => _enableBlur;
            set
            {
                if (_enableBlur != value)
                {
                    _enableBlur = value;
                    EnableBlurChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeEnableBlur()
        {
            return EnableBlur;
        }

        /// <summary>
        /// </summary>
        public void ResetEnableBlur()
        {
            EnableBlur = false;
        }


        /// <summary>
        /// </summary>
        [Description("Gausian pixel radius used to blur each pixel")]
        [DefaultValue(_radiusDefault)]
        public Byte Radius
        {
            get => _radius;
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    RadiusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeRadius()
        {
            return _radius != _radiusDefault;
        }

        /// <summary>
        /// </summary>
        public void ResetRadius()
        {
            _radius = _radiusDefault;
        }

        /// <summary>
        /// </summary>
        [Description("Opacity Percentage to be applied to the blur over source form. Tuning this allows for background updates to show through.")]
        [DefaultValue(_opacityDefault)]
        public double Opacity
        {
            get => _opacity;
            set
            {
                if (Math.Abs(_opacity - value) > 0.001
                    && 0 <= value
                    && value <= 100
                )
                {
                    _opacity = value;
                    OpacityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeOpacity()
        {
            return Math.Abs(_opacity - _opacityDefault) > 0.001;
        }

        /// <summary>
        /// </summary>
        public void ResetOpacity()
        {
            _opacity = _opacityDefault;
        }

        /// <summary>
        /// </summary>
        [Description("Blur this when not Focused")]
        [DefaultValue(false)]
        public bool BlurWhenFocusLost
        {
            get => _blurWhenFocusLost;
            set
            {
                if (_blurWhenFocusLost != value)
                {
                    _blurWhenFocusLost = value;
                }
            }
        }

        private bool ShouldSerializeBlurWhenFocusLost()
        {
            return BlurWhenFocusLost;
        }

        /// <summary>
        /// </summary>
        public void ResetBlurWhenFocusLost()
        {
            BlurWhenFocusLost = false;
        }

        #region Default Values
        /// <summary>
        /// 
        /// </summary>
        public override bool IsDefault => (!ShouldSerializeEnableBlur()
                                            && !ShouldSerializeRadius()
                                            && !ShouldSerializeOpacity()
                                            && !ShouldSerializeBlurWhenFocusLost()
                                            );

        #endregion Default Values
    }
}
