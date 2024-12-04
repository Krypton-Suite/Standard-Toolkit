#region BSD License
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

// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Combines button functionality with the styling features of the Krypton Toolkit.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
    [DefaultEvent(nameof(Click))]
    [DefaultProperty(nameof(Text))]
    [DesignerCategory(@"code")]
    [Description(@"Raises an event when the user clicks it.")]
    [Designer(typeof(KryptonButtonDesigner))]
    public class KryptonButton : KryptonDropButton
    {
        #region Instance Fields
        private bool _useAsDialogButton;
        private bool _useAsUACElevationButton;
        private bool _skipNextOpen;
        //private bool _useOSUACShieldIcon;
        private float _cornerRoundingRadius;
        private Size _customUACShieldSize;
        private UACShieldIconSize _uacShieldIconSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonButton class.
        /// </summary>
        public KryptonButton()
        {
            // Create the view button instance
            _drawButton.DropDown = false;
            _drawButton.Splitter = false;

            // Create a button controller to handle button style behaviour
            _buttonController.BecomesFixed = false;

            _useAsDialogButton = false;

            _useAsUACElevationButton = false;

            _uacShieldIconSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_SIZE;

            //_useOSUACShieldIcon = false;

            _customUACShieldSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE;

            // Set `CornerRoundingRadius' to 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE' (-1)
            CornerRoundingRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;

            _skipNextOpen = false;
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the corner rounding radius.</summary>
        /// <value>The corner rounding radius.</value>
        [Category(@"Visuals")]
        [Description(@"Gets or sets the corner rounding radius.")]
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float CornerRoundingRadius
        {
            get => _cornerRoundingRadius;

            set => SetCornerRoundingRadius(value);
        }

        /// <summary>
        /// Gets and sets the visual orientation of the control.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual VisualOrientation Orientation
        {
            // Backward compatible fix.
            get => ButtonOrientation;

            set => ButtonOrientation = value;
        }

        [DefaultValue(false),
         Description(@"If set to true, the text will pair up with the equivalent KryptonManager's dialog button text result. (Note: You'll lose any previous text)")]
        public bool UseAsADialogButton
        {
            get => _useAsDialogButton;
            set => _useAsDialogButton = value;
        }

        [DefaultValue(false),
         Description(@"Transforms the button into a UAC elevated button.")]
        public bool UseAsUACElevationButton
        {
            get => _useAsUACElevationButton;
            set
            {
                _useAsUACElevationButton = value;

                switch (_uacShieldIconSize)
                {
                    //if (_customUACShieldSize.Height > 0 && _customUACShieldSize.Width > 0)
                    //{
                    //    ShowUACShield(value, UACShieldIconSize.Custom, _customUACShieldSize.Width, _customUACShieldSize.Height);
                    //}
                    //else if (_uacShieldIconSize != UACShieldIconSize.Custom)
                    //{
                    //    ShowUACShield(value, _uacShieldIconSize);
                    //}
                    case UACShieldIconSize.ExtraSmall:
                        ShowUACShield(value, UACShieldIconSize.ExtraSmall);
                        break;
                    case UACShieldIconSize.Small:
                        ShowUACShield(value, UACShieldIconSize.Small);
                        break;
                    case UACShieldIconSize.Medium:
                        ShowUACShield(value, UACShieldIconSize.Medium);
                        break;
                    case UACShieldIconSize.Large:
                        ShowUACShield(value, UACShieldIconSize.Large);
                        break;
                    case UACShieldIconSize.ExtraLarge:
                        ShowUACShield(value, UACShieldIconSize.ExtraLarge);
                        break;
                    default:
                        ShowUACShield(value, UACShieldIconSize.ExtraSmall);
                        break;
                }
            }
        }

        /*
        [DefaultValue(false), Description(@"Use the operating system UAC shield icon image.")]
        public bool UseOSUACShieldIcon { get => _useOSUACShieldIcon; set { _useOSUACShieldIcon = value; UpdateOSUACShieldIcon(); } }
        
        [DefaultValue(null), Description(@"")]
        public Size CustomUACShieldSize { get => _customUACShieldSize; set { _customUACShieldSize = value; ShowUACShield(_useAsUACElevationButton, UACShieldIconSize.Custom, value.Width, value.Height); } }
        */

        [DefaultValue(UACShieldIconSize.ExtraSmall), Description(@"")]
        public UACShieldIconSize UACShieldIconSize { get => _uacShieldIconSize; set { _uacShieldIconSize = value; ShowUACShield(_useAsUACElevationButton, value); } }

        //[Category(@"Visuals"), Description(@"UAC Shield Values"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public UACShieldValues UACShieldValues { get; }

        //private bool ShouldSerializeUACShieldValues() => !UACShieldValues.IsDefault;
        /// <summary>Gets or sets a value indicating whether [show split option].</summary>
        /// <value><c>true</c> if [show split option]; otherwise, <c>false</c>.</value>

        [Category(@"Visuals")]
        [DefaultValue(false)]
        [Description(@"Displays the split/dropdown option.")]
        public bool ShowSplitOption
        {
            // Backward compatible fix.
            get => base.Splitter;
            set 
            {
                _drawButton.DropDown = value;
                base.Splitter = value; 
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        public new bool Splitter
        {
            get => base.Splitter;
            set => base.Splitter = value;
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (_useAsUACElevationButton)
            {
                var rawUACShield = SystemIcons.Shield.ToBitmap();

                // Resize rawUACShield down to 16 x 16 to make it fit
                var resizedUACShield = new Bitmap(rawUACShield, new Size(16, 16));

                if (Values.Image == null)
                {
                    Values.Image = resizedUACShield;
                }
                else if (Values.Image != null)
                {
                    // TODO: If Values.Image is set, and then image becomes null, to then display the UAC icon
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_useAsDialogButton)
            {
                Text = DialogResult switch
                {
                    DialogResult.Abort => KryptonLanguageManager.GeneralToolkitStrings.Abort,
                    DialogResult.Cancel => KryptonLanguageManager.GeneralToolkitStrings.Cancel,
                    DialogResult.OK => KryptonLanguageManager.GeneralToolkitStrings.OK,
                    DialogResult.Yes => KryptonLanguageManager.GeneralToolkitStrings.Yes,
                    DialogResult.No => KryptonLanguageManager.GeneralToolkitStrings.No,
                    DialogResult.Retry => KryptonLanguageManager.GeneralToolkitStrings.Retry,
                    DialogResult.Ignore => KryptonLanguageManager.GeneralToolkitStrings.Ignore,
                    _ => Text
                };
            }

            base.OnPaint(e);
        }

        #endregion

        #region Implementation

        private void SetCornerRoundingRadius(float? radius)
        {
            _cornerRoundingRadius = radius ?? GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;

            StateCommon.Border.Rounding = _cornerRoundingRadius;
        }

        #region UAC Stuff

        /// <summary>Shows the uac shield.</summary>
        /// <param name="showUACShield">if set to <c>true</c> [show uac shield].</param>
        /// <param name="shieldIconSize">Size of the shield icon.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private void ShowUACShield(bool showUACShield, UACShieldIconSize? shieldIconSize = null, int? width = null, int? height = null)
        {
            if (showUACShield)
            {
                int h = height ?? 16, w = width ?? 16;

                Image shield = SystemIcons.Shield.ToBitmap();

                switch (shieldIconSize)
                {
                    //case UACShieldIconSize.Custom:
                    //    Values.Image = GraphicsExtensions.ScaleImage(shield, w, h);
                    //    break;
                    case UACShieldIconSize.ExtraSmall:
                        Values.Image = GraphicsExtensions.ScaleImage(shield, 16, 16);
                        break;
                    case UACShieldIconSize.Small:
                        Values.Image = GraphicsExtensions.ScaleImage(shield, 32, 32);
                        break;
                    case UACShieldIconSize.Medium:
                        Values.Image = GraphicsExtensions.ScaleImage(shield, 64, 64);
                        break;
                    case UACShieldIconSize.Large:
                        Values.Image = GraphicsExtensions.ScaleImage(shield, 128, 128);
                        break;
                    case UACShieldIconSize.ExtraLarge:
                        Values.Image = GraphicsExtensions.ScaleImage(shield, 256, 256);
                        break;
                    case null:
                        Values.Image = GraphicsExtensions.ScaleImage(shield, 16, 16);
                        break;
                }

                Invalidate();
            }
            else
            {
                Values.Image = null;
            }
        }

        /// <summary>Updates the UAC shield icon.</summary>
        /// <param name="iconSize">Size of the icon.</param>
        /// <param name="customSize">Size of the custom.</param>
        private void UpdateOSUACShieldIcon(UACShieldIconSize? iconSize = null, Size? customSize = null)
        {
            //if (OSUtilities.IsWindowsEleven)
            //{
            //    Image windowsElevenUacShieldImage = UACShieldIconResources.UACShieldWindows11;

            //    if (iconSize == UACShieldIconSize.Custom)
            //    {
            //        UpdateShieldSize(UACShieldIconSize.Custom, customSize, windowsElevenUacShieldImage);
            //    }
            //    else
            //    {
            //        UpdateShieldSize(iconSize, null, windowsElevenUacShieldImage);
            //    }
            //}
            //else if (OSUtilities.IsWindowsTen)
            //{
            //    Image windowsTenUacShieldImage = UACShieldIconResources.UACShieldWindows10;

            //    if (iconSize == UACShieldIconSize.Custom)
            //    {
            //        UpdateShieldSize(UACShieldIconSize.Custom, customSize, windowsTenUacShieldImage);
            //    }
            //    else
            //    {
            //        UpdateShieldSize(iconSize, null, windowsTenUacShieldImage);
            //    }
            //}
            //else if (OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight || OSUtilities.IsWindowsSeven)
            //{
            //    Image windowsEightUacShieldImage = UACShieldIconResources.UACShieldWindows7881;

            //    if (iconSize == UACShieldIconSize.Custom)
            //    {
            //        UpdateShieldSize(UACShieldIconSize.Custom, customSize, windowsEightUacShieldImage);
            //    }
            //    else
            //    {
            //        UpdateShieldSize(iconSize, null, windowsEightUacShieldImage);
            //    }
            //}
        }

        #endregion

        #endregion
    }
}
