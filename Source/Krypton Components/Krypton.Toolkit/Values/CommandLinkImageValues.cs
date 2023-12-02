#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class CommandLinkImageValues : Storage, IContentValues
    {
        #region Static Fields

        private static readonly Image DEFAULT_IMAGE = MessageBoxImageResources.GenericQuestion;

        #endregion

        #region Instance Fields

        private Color _transparencyKey;

        private Image _image;

        #endregion

        #region Public

        //public bool ShowUACShield { get; set; }

        /// <summary>Gets and sets the heading image transparent color.</summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Image transparent color.")]
        [RefreshProperties(RefreshProperties.All)]
        [KryptonDefaultColor()]
        public Color ImageTransparentColor
        {
            get => _transparencyKey;

            set
            {
                if (_transparencyKey != value)
                {
                    _transparencyKey = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != Color.Empty;

        /// <summary>Resets the ImageTransparentColor property to its default value.</summary>
        public void ResetImageTransparentColor() => ImageTransparentColor = Color.Empty;

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("The image.")]
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

        private bool ShouldSerializeImage() => Image != DEFAULT_IMAGE;

        public void ResetImage() => Image = DEFAULT_IMAGE;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="CommandLinkImageValues" /> class.</summary>
        /// <param name="needPaint">The need paint.</param>
        public CommandLinkImageValues(NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;

            ResetImage();

            ResetImageTransparentColor();
        }

        #endregion

        #region IsDefault

        /// <inheritdoc />
        [Browsable(false)]
        public override bool IsDefault => ((Image == DEFAULT_IMAGE) &&
                                           (ImageTransparentColor == Color.Empty));

        #endregion

        #region Implementation

        /// <inheritdoc />
        public Image? GetImage(PaletteState state) => Image;

        /// <inheritdoc />
        public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

        /// <inheritdoc />
        public string GetShortText() => string.Empty;

        /// <inheritdoc />
        public string GetLongText() => string.Empty;

        #endregion
    }
}