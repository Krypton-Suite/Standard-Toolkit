﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public class ShadowValues : Storage
    {
        #region statics
        private static readonly Point _defaultOffset = new(5,5);
        private double _blurDistance;
        private bool _enableShadows;
        private Point _offset;
        private byte _extraWidth;
        private Color _colour;
        private double _opacity;
        #endregion

        #region Events
#pragma warning disable 1591
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler EnableShadowsChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler MarginsChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler BlurDistanceChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<ColorEventArgs> ColourChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler OpacityChanged;
#pragma warning restore 1591
        #endregion

        #region Identity
        /// <summary>
        /// 
        /// </summary>
        public ShadowValues()
        {
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            ResetEnableShadows();
            ResetOffset();
            ResetExtraWidth();
            ResetBlurDistance();
            ResetColour();
            ResetOpacity();
        }
        #endregion Identity

        /// <summary>
        /// </summary>
        [Description(@"Use this enahanced shadow feature")]
        [DefaultValue(false)]
        public bool EnableShadows
        {
            get => _enableShadows;
            set
            {
                if (_enableShadows != value)
                {
                    _enableShadows = value;
                    EnableShadowsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeEnableShadows() => EnableShadows;

        /// <summary>
        /// </summary>
        public void ResetEnableShadows()
        {
            EnableShadows = false;
        }


        /// <summary>
        /// </summary>
        [Description(@"Relative location of the top-left of the shadow, to the form. +ve means shadow out the bottom right")]
        public Point Offset
        {
            get => _offset;
            set
            {
                if (_offset != value)
                {
                    _offset = value;
                    MarginsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeOffset() => _offset != _defaultOffset;

        /// <summary>
        /// </summary>
        public void ResetOffset()
        {
            _offset = _defaultOffset;
        }

        /// <summary>
        /// </summary>
        [Description(@"Extra width to be applied to all edges.")]
        public byte ExtraWidth
        {
            get => _extraWidth;
            set
            {
                if (_extraWidth != value)
                {
                    _extraWidth = value;
                    MarginsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeExtraWidth() => _extraWidth != 5;

        /// <summary>
        /// </summary>
        public void ResetExtraWidth()
        {
            _extraWidth = 5;
        }



        /// <summary>
        /// </summary>
        [Description(@"% of 'Extra Width' to start blur +ve")]
        [DefaultValue(50.0)]
        public double BlurDistance
        {
            get => _blurDistance;
            set
            {
                if (Math.Abs(_blurDistance - value) > 0.001
                    && value is >= 0 and <= 100
                    )
                {
                    _blurDistance = value;
                    BlurDistanceChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeBlurDistance() => Math.Abs(BlurDistance - 50) > 0.001;

        /// <summary>
        /// </summary>
        public void ResetBlurDistance()
        {
            BlurDistance = 50;
        }


        /// <summary>
        /// </summary>
        [Description(@"What Colour will be used for the Blur Solid")]
        public Color Colour
        {
            get => _colour;
            set
            {
                if (_colour != value)
                {
                    _colour = value;
                    ColourChanged?.Invoke(this, new ColorEventArgs(_colour));
                }
            }
        }

        private bool ShouldSerializeColour() => Colour != SystemColors.ActiveBorder;

        /// <summary>
        /// Resets the PlacementMode property to its default value.
        /// </summary>
        public void ResetColour()
        {
            Colour = SystemColors.ActiveBorder;
        }

        /// <summary>
        /// </summary>
        [Description(@"Opacity Percentage")]
        [DefaultValue(95.0)]
        public double Opacity
        {
            get => _opacity;
            set
            {
                if (Math.Abs(_opacity - value) > 0.001
                    && value is >= 0 and <= 100
                )
                {
                    _opacity = value;
                    OpacityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeOpacity() => Math.Abs(_opacity - 95) > 0.001;

        /// <summary>
        /// </summary>
        public void ResetOpacity()
        {
            _opacity = 95;
        }


        #region Default Values
        /// <summary>
        /// 
        /// </summary>
        public override bool IsDefault => !ShouldSerializeEnableShadows()
                                            && !ShouldSerializeOffset()
                                            && !ShouldSerializeExtraWidth()
                                            && !ShouldSerializeBlurDistance()
                                            && !ShouldSerializeColour()
                                            && !ShouldSerializeOpacity()
                                            ;

        #endregion Default Values
    }
}
