#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
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
        //private bool _skipNextOpen;
        //private bool _showSplitOption;
        //private bool _useOSUACShieldIcon;
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

            _uacShieldIconSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_SIZE;

            //_useOSUACShieldIcon = false;

            _customUACShieldSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE;

            //_skipNextOpen = false;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the visual orientation of the control
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public virtual VisualOrientation Orientation
        {
            // Backward compatible fix.
            get => ButtonOrientation;
            set => ButtonOrientation = value;
        }

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
        //[Category(@"Visuals"), Description(@"UAC Shield Values"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public UACShieldValues UACShieldValues { get; }

        //private bool ShouldSerializeUACShieldValues() => !UACShieldValues.IsDefault;
        #endregion

        #region Protected Overrides

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Values.UseAsUACElevationButton)
            {
                var rawUACShield = SystemIcons.Shield.ToBitmap();

                // Resize rawUACShield down to 16 x 16 to make it fit
                // TODO: Use the same size rect as the dropDown, as that is scaled to fit !
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

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs? e)
        {
            if (Values.UseAsADialogButton)
            {
                Text = DialogResult switch
                {
                    DialogResult.Abort => KryptonManager.Strings.GeneralStrings.Abort,
                    DialogResult.Cancel => KryptonManager.Strings.GeneralStrings.Cancel,
                    DialogResult.OK => KryptonManager.Strings.GeneralStrings.OK,
                    DialogResult.Yes => KryptonManager.Strings.GeneralStrings.Yes,
                    DialogResult.No => KryptonManager.Strings.GeneralStrings.No,
                    DialogResult.Retry => KryptonManager.Strings.GeneralStrings.Retry,
                    DialogResult.Ignore => KryptonManager.Strings.GeneralStrings.Ignore,
                    _ => Text
                };
            }

            if (Values.UseAsUACElevationButton)
            {
                switch (Values.UACShieldIconSize)
                {
                    case UACShieldIconSize.ExtraSmall:
                        break;
                    case UACShieldIconSize.Small:
                        break;
                    case UACShieldIconSize.Medium:
                        break;
                    case UACShieldIconSize.Large:
                        break;
                    case UACShieldIconSize.ExtraLarge:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            base.OnPaint(e);
        }

        #endregion

    }
}
