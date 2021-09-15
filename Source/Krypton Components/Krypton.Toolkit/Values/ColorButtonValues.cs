#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for color button content value information.
    /// </summary>
    public class ColorButtonValues : Storage,
                                     IContentValues
    {
        #region Static Fields
        private const string _defaultText = "Color";
        private static readonly string _defaultExtraText = string.Empty;
        private static readonly Image _defaultImage = Resources.ButtonColorImageSmall;
        #endregion

        #region Instance Fields
        private Image _image;
        private Image _sourceImage;
        private Image _compositeImage;
        private Color _transparent;
        private string _text;
        private string _extraText;
        private Color _selectedColor;
        private Color _emptyBorderColor;
        private Rectangle _selectedRect;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the Text property changes.
        /// </summary>
        public event EventHandler TextChanged;
        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ColorButtonValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ColorButtonValues(NeedPaintHandler needPaint)
        {
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Set initial values
            _image = _defaultImage;
            _transparent = Color.Empty;
            _text = _defaultText;
            _extraText = _defaultExtraText;
            ImageStates = CreateImageStates();
            ImageStates.NeedPaint = needPaint;
            _emptyBorderColor = Color.Gray;
            _selectedColor = Color.Red;
            _selectedRect = new Rectangle(0, 12, 16, 4);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (ImageStates.IsDefault &&
                                           (Image == _defaultImage) &&
                                           (ImageTransparentColor == Color.Empty) &&
                                           (Text == _defaultText) &&
                                           (ExtraText == _defaultExtraText));

        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image.")]
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

        private bool ShouldSerializeImage()
        {
            return Image != _defaultImage;
        }

        /// <summary>
        /// Resets the Image property to its default value.
        /// </summary>
        public void ResetImage()
        {
            Image = _defaultImage;
        }
        #endregion

        #region ImageTransparentColor
        /// <summary>
        /// Gets and sets the label image transparent color.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Label image transparent color.")]
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

        private bool ShouldSerializeImageTransparentColor()
        {
            return ImageTransparentColor != Color.Empty;
        }

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
        public Color GetImageTransparentColor(PaletteState state)
        {
            return ImageTransparentColor;
        }
        #endregion

        #region ImageStates
        /// <summary>
        /// Gets access to the state specific images for the button.
        /// </summary>
        [Category("Visuals")]
        [Description("State specific images for the button.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonImageStates ImageStates { get; }

        private bool ShouldSerializeImageStates()
        {
            return !ImageStates.IsDefault;
        }
        #endregion

        #region Text
        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button text.")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Text
        {
            get => _text;

            set
            {
                if (_text != value)
                {
                    _text = value;
                    PerformNeedPaint(true);
                    TextChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeText()
        {
            return Text != _defaultText;
        }

        /// <summary>
        /// Resets the Text property to its default value.
        /// </summary>
        public void ResetText()
        {
            Text = _defaultText;
        }
        #endregion

        #region ExtraText
        /// <summary>
        /// Gets and sets the button extra text.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button extra text.")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ExtraText
        {
            get => _extraText;

            set
            {
                if (_extraText != value)
                {
                    _extraText = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeExtraText()
        {
            return ExtraText != _defaultExtraText;
        }

        /// <summary>
        /// Resets the Description property to its default value.
        /// </summary>
        public void ResetExtraText()
        {
            ExtraText = _defaultExtraText;
        }
        #endregion

        #region SelectedColor
        /// <summary>
        /// Gets and sets the selected color for the composite image.
        /// </summary>
        internal Color SelectedColor
        {
            get => _selectedColor;

            set 
            { 
                _selectedColor = value;
                _compositeImage = null;
            }
        }
        #endregion

        #region EmptyBorderColor
        /// <summary>
        /// Gets and sets the empty border color for the composite image.
        /// </summary>
        internal Color EmptyBorderColor
        {
            get => _emptyBorderColor;

            set 
            { 
                _emptyBorderColor = value;
                _compositeImage = null;
            }
        }
        #endregion

        #region SelectedRect
        /// <summary>
        /// Gets and sets the selected rectangle for the composite image.
        /// </summary>
        internal Rectangle SelectedRect
        {
            get => _selectedRect;

            set 
            { 
                _selectedRect = value;
                _compositeImage = null;
            }
        }
        #endregion

        #region CreateImageStates
        /// <summary>
        /// Create the storage for the image states.
        /// </summary>
        /// <returns>Storage object.</returns>
        protected virtual ButtonImageStates CreateImageStates()
        {
            return new ButtonImageStates();
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public virtual Image GetImage(PaletteState state)
        {
            Image image = null;

            // Try and find a state specific image
            switch (state)
            {
                case PaletteState.Disabled:
                    image = ImageStates.ImageDisabled;
                    break;
                case PaletteState.Normal:
                    image = ImageStates.ImageNormal;
                    break;
                case PaletteState.Pressed:
                    image = ImageStates.ImagePressed;
                    break;
                case PaletteState.Tracking:
                    image = ImageStates.ImageTracking;
                    break;
            }

            // If there is no image then use the generic image
            if (image == null)
            {
                image = Image;
            }

            // Do we need to create another composite image?
            if ((_sourceImage != image) || (_compositeImage == null))
            {
                // Remember image used to create the composite image
                _sourceImage = image;

                if (image == null)
                {
                    _compositeImage = null;
                }
                else
                {
                    // Create a copy of the source image
                    Bitmap copyBitmap = new(image);

                    // Paint over the image with a color indicator
                    using (Graphics g = Graphics.FromImage(copyBitmap))
                    {
                        // If the color is not defined, i.e. it is empty then...
                        if (_selectedColor.Equals(Color.Empty))
                        {
                            // Indicate the absense of a color by drawing a border around 
                            // the selected color area, thus indicating the area inside the
                            // block is blank/empty.
                            using (Pen borderPen = new(_emptyBorderColor))
                            {
                                g.DrawRectangle(borderPen, new Rectangle(_selectedRect.X,
                                                                         _selectedRect.Y,
                                                                         _selectedRect.Width - 1,
                                                                         _selectedRect.Height - 1));
                            }
                        }
                        else
                        {
                            // We have a valid selected color so draw a solid block of color
                            using (SolidBrush colorBrush = new(_selectedColor))
                            {
                                g.FillRectangle(colorBrush, _selectedRect);
                            }
                        }
                    }

                    // Cache it for future use
                    _compositeImage = copyBitmap;
                }
            }

            return _compositeImage;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        public virtual string GetShortText()
        {
            return Text;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        public virtual string GetLongText()
        {
            return ExtraText;
        }
        #endregion
    }
}
