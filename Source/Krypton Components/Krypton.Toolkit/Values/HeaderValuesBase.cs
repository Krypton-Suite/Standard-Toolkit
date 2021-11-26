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
    /// Storage for header content value information.
    /// </summary>
    public abstract class HeaderValuesBase : Storage,
                                             IContentValues
    {
        #region Static Fields
        private static readonly Image _defaultImage = GenericImageResources.KryptonLogoGeneric;
        #endregion

        #region Instance Fields
        private Image _image;
        private Color _transparent;
        private string _heading;
        private string _description;
        #endregion

        #region Events
        /// <summary>
        /// Occures when the value of the Text property changes.
        /// </summary>
        public event EventHandler TextChanged;
        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderValuesBase class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        protected HeaderValuesBase(NeedPaintHandler needPaint)
        {
            // Store the provided paint notification delegate
            NeedPaint = needPaint;
            
            // Set initial values to the default
            _image = GetImageDefault();
            _transparent = Color.Empty;
            _heading = GetHeadingDefault();
            _description = GetDescriptionDefault();
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (Image == GetImageDefault()) &&
                                           (ImageTransparentColor == Color.Empty) &&
                                           (Heading == GetHeadingDefault()) &&
                                           (Description == GetDescriptionDefault());

        #endregion

        #region Default Values
        /// <summary>
        /// Gets the default image value.
        /// </summary>
        /// <returns>Image reference.</returns>
        protected virtual Image GetImageDefault() => _defaultImage;

        /// <summary>
        /// Gets the default heading value.
        /// </summary>
        /// <returns>String reference.</returns>
        protected abstract string GetHeadingDefault();

        /// <summary>
        /// Gets the default description value.
        /// </summary>
        /// <returns>String reference.</returns>
        protected abstract string GetDescriptionDefault();
        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the heading image.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Heading image.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image Image
        {
            get => _image;

            set
            {
                if (_image != value)
                {
                    _image = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImage() => Image != GetImageDefault();

        /// <summary>
        /// Resets the Image property to its default value.
        /// </summary>
        public void ResetImage()
        {
            Image = GetImageDefault();
        }

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public virtual Image GetImage(PaletteState state) => Image;

        #endregion

        #region ImageTransparentColor
        /// <summary>
        /// Gets and sets the heading image transparent color.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Heading image transparent color.")]
        [RefreshProperties(RefreshProperties.All)]
        [KryptonDefaultColor()]
        public Color ImageTransparentColor
        {
            get => _transparent;

            set
            {
                if (_transparent != value)
                {
                    _transparent = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != Color.Empty;

        /// <summary>
        /// Resets the ImageTransparentColor property to its default value.
        /// </summary>
        public void ResetImageTransparentColor()
        {
            ImageTransparentColor = Color.Empty;
        }

        /// <summary>
        /// Gets the content image transparent color.
        /// </summary>
        /// <param name="state">The state for which the image color is needed.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

        #endregion
        
        #region Heading
        /// <summary>
        /// Gets and sets the heading text.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Heading text.")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
        public virtual string Heading
        {
            get => _heading;

            set
            {
                if (_heading != value)
                {
                    _heading = value;
                    PerformNeedPaint(true);
                    TextChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeHeading() => Heading != GetHeadingDefault();

        /// <summary>
        /// Resets the Heading property to its default value.
        /// </summary>
        public void ResetHeading()
        {
            Heading = GetHeadingDefault();
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        public virtual string GetShortText() => Heading;

        #endregion

        #region Description
        /// <summary>
        /// Gets and sets the header description text.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Header description text.")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
        public virtual string Description
        {
            get => _description;

            set
            {
                if (_description != value)
                {
                    _description = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeDescription() => Description != GetDescriptionDefault();

        /// <summary>
        /// Resets the Description property to its default value.
        /// </summary>
        public void ResetDescription()
        {
            Description = GetDescriptionDefault();
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        public virtual string GetLongText() => Description;

        #endregion
    }
}
