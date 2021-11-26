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
    /// Storage for label content value information.
    /// </summary>
    public class LabelValues : Storage,
                               IContentValues
    {
        #region Static Fields
        private const string _defaultText = "Label";
        private static readonly string _defaultExtraText = string.Empty;
        #endregion

        #region Instance Fields
        private Image _image;
        private Color _transparent;
        private string _text;
        private string _extraText;
        #endregion

        #region Events
        /// <summary>
        /// Occures when the value of the Text property changes.
        /// </summary>
        public event EventHandler TextChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the LabelValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public LabelValues(NeedPaintHandler needPaint)
        {
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Set initial values
            _image = null;
            _transparent = Color.Empty;
            _text = _defaultText;
            _extraText = _defaultExtraText;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (Image == null) &&
                                           (ImageTransparentColor == Color.Empty) &&
                                           (Text == _defaultText) &&
                                           (ExtraText == _defaultExtraText);

        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the label image.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Label image.")]
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

        private bool ShouldSerializeImage() => Image != null;

        /// <summary>
        /// Resets the Image property to its default value.
        /// </summary>
        public void ResetImage()
        {
            Image = null;
        }

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state) => Image;

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
        public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

        #endregion

        #region Text
        /// <summary>
        /// Gets and sets the label text.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Label text.")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
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

        private bool ShouldSerializeText() => Text != _defaultText;

        /// <summary>
        /// Resets the Text property to its default value.
        /// </summary>
        public void ResetText()
        {
            Text = _defaultText;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        public string GetShortText() => Text;

        #endregion

        #region ExtraText
        /// <summary>
        /// Gets and sets the label extra text.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Label extra text.")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
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

        private bool ShouldSerializeExtraText() => ExtraText != _defaultExtraText;

        /// <summary>
        /// Resets the Description property to its default value.
        /// </summary>
        public void ResetExtraText()
        {
            // TODO: What is the intention of this
            ExtraText = ExtraText;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        public string GetLongText() => ExtraText;

        #endregion
    }
}
