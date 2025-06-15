#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// A button that displays the Krypton Toolkit branding and provides information about the toolkit version.
    /// </summary>
    /// <seealso cref="Krypton.Toolkit.KryptonButton" />
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
    [DesignerCategory(@"code")]
    [Description(@"A button that displays the Krypton Toolkit branding and provides information about the toolkit version.")]
    [Designer(typeof(KryptonButtonDesigner))]
    public class KryptonPoweredByButton : KryptonButton
    {
        #region Instance Fields

        private ToolkitSupportType _toolkitType;

        #endregion

        #region Public

        /// <summary>
        /// Gets or sets the type of the toolkit.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Gets or sets the type of the toolkit.")]
        [DefaultValue(typeof(ToolkitSupportType), "Stable")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolkitSupportType ToolkitSupportType
        {
            get => _toolkitType;
            set
            {
                _toolkitType = value;

                Invalidate();
            }
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonPoweredByButton" /> class.</summary>
        public KryptonPoweredByButton()
        {
            _toolkitType = ToolkitSupportType.Stable;

            Values.Text = @$"{KryptonManager.Strings.MiscellaneousStrings.PoweredByText} Krypton";

            Values.Image = ButtonImageResources.Krypton_Stable_Button;

            Size = new Size(153, 25);
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        [AllowNull]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get; set; } =
            @$"{KryptonManager.Strings.MiscellaneousStrings.PoweredByText} Krypton";

        /// <inheritdoc />
        protected override void OnClick(EventArgs e)
        {
            new VisualToolkitBinaryInformationForm(_toolkitType).ShowDialog();

            base.OnClick(e);
        }

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs? e)
        {
            base.OnPaint(e);

            switch (_toolkitType)
            {
                case ToolkitSupportType.Canary:
                    Values.Image = ButtonImageResources.Krypton_Canary_Button;
                    break;
                case ToolkitSupportType.Nightly:
                    Values.Image = ButtonImageResources.Krypton_Nightly_Button;
                    break;
                case ToolkitSupportType.LongTermSupport:
                    Values.Image = ButtonImageResources.Krypton_Long_Term_Stable_Button;
                    break;
                case ToolkitSupportType.Stable:
                default:
                    Values.Image = ButtonImageResources.Krypton_Stable_Button;
                    break;
            }
        }

        #endregion

        #region Event

        /// <summary>Occurs when the control is clicked.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Click
        {
            add { base.Click += value; }
            remove { base.Click -= value; }
        }

        #endregion
    }
}