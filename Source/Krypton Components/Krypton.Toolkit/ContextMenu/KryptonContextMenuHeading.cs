﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide a context menu heading.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuHeading), "ToolboxBitmaps.KryptonContextMenuHeading.bmp")]
    [DesignerCategory(@"code")]
    [DesignTimeVisible(false)]
    [DefaultProperty(nameof(Text))]
    public class KryptonContextMenuHeading : KryptonContextMenuItemBase
    {
        #region Instance Fields
        private string _text;
        private string? _extraText;
        private Image? _image;
        private Color _imageTransparentColor;
        private readonly PaletteRedirectTriple? _redirectHeading;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuHeading class.
        /// </summary>
        public KryptonContextMenuHeading()
            : this(@"Heading")
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuHeading class.
        /// </summary>
        /// <param name="initialText">Initial text for display.</param>
        public KryptonContextMenuHeading(string initialText)
        {
            // Default fields
            _text = initialText;
            _extraText = string.Empty;
            _image = null;
            _imageTransparentColor = Color.Empty;

            // Create the redirector that can get values from the krypton context menu
            _redirectHeading = new PaletteRedirectTriple();

            // Create the header storage for overriding specific values
            StateNormal = new PaletteTripleRedirect(_redirectHeading, 
                                                     PaletteBackStyle.ContextMenuHeading,
                                                     PaletteBorderStyle.ContextMenuHeading,
                                                     PaletteContentStyle.ContextMenuHeading);
        }

        /// <summary>
        /// Returns a description of the instance.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => Text;

        #endregion

        #region Public
        /// <summary>
        /// Returns the number of child menu items.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ItemChildCount => 0;

        /// <summary>
        /// Returns the indexed child menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override KryptonContextMenuItemBase? this[int index] => null;

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shortcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public override bool ProcessShortcut(Keys keyData) => false;

        /// <summary>
        /// Returns a view appropriate for this item based on the object it is inside.
        /// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="parent">Owning object reference.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
        public override ViewBase GenerateView(IContextMenuProvider provider,
                                              object parent,
                                              ViewLayoutStack columns,
                                              bool standardStyle,
                                              bool imageColumn) =>
            new ViewDrawMenuHeading(this, provider.ProviderStateCommon.Heading);

        /// <summary>
        /// Gets and sets the heading menu item text.
        /// </summary>
        [KryptonPersist]
        [Category(@"Appearance")]
        [Description(@"Heading menu item text.")]
        [Editor(@"System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
        [Localizable(true)]
        [DefaultValue(@"Heading")]
        public string Text
        {
            get => _text;

            set 
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Text)));
                }
            }
        }

        /// <summary>
        /// Gets and sets the heading menu item extra text.
        /// </summary>
        [KryptonPersist]
        [Category(@"Appearance")]
        [Description(@"Heading menu item extra text.")]
        [Editor(@"System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
        [Localizable(true)]
        [DefaultValue(null)]
        [AllowNull]
        public string ExtraText
        {
            get => _extraText ?? string.Empty;

            set 
            {
                if (_extraText != value)
                {
                    _extraText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ExtraText)));
                }
            }
        }

        /// <summary>
        /// Gets and sets the heading menu item image.
        /// </summary>
        [KryptonPersist]
        [Category(@"Appearance")]
        [Description(@"Heading menu item image.")]
        [Localizable(true)]
        [DefaultValue(null)]
        public Image? Image
        {
            get => _image;

            set 
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Image)));
                }
            }
        }

        /// <summary>
        /// Gets and sets the heading image color to make transparent.
        /// </summary>
        [KryptonPersist]
        [Category(@"Appearance")]
        [Description(@"Heading image color to make transparent.")]
        [Localizable(true)]
        public Color ImageTransparentColor
        {
            get => _imageTransparentColor;

            set 
            {
                if (_imageTransparentColor != value)
                {
                    _imageTransparentColor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageTransparentColor)));
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor() => (_imageTransparentColor == null) || !_imageTransparentColor.Equals(Color.Empty);

        /// <summary>
        /// Gets access to the header instance specific appearance values.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining header instance specific appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

        #endregion

        #region Internal
        internal void SetPaletteRedirect(PaletteTripleRedirect redirector) => _redirectHeading.SetRedirectStates(redirector, redirector);

        #endregion
    }
}
