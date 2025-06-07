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
    public class KryptonPoweredByButton : KryptonButton
    {
        #region Instance Fields

        private ToolkitType _toolkitType;

        #endregion

        #region Public

        /// <summary>
        /// Gets or sets the type of the toolkit.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Gets or sets the type of the toolkit.")]
        [DefaultValue(typeof(ToolkitType), "Standard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolkitType ToolkitType
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

        public KryptonPoweredByButton()
        {
            _toolkitType = ToolkitType.Stable;

            Values.Text = @$"{KryptonManager.Strings.MiscellaneousStrings.PoweredByText} Krypton";

            Values.Image = GenericKryptonImageResources.Krypton_Stable_Button;

            Size = new Size(153, 25);
        }

        #endregion

        [AllowNull]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get; set; } =
            @$"{KryptonManager.Strings.MiscellaneousStrings.PoweredByText} Krypton";

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Click
        {
            add { base.Click += value; }
            remove { base.Click -= value; }
        }

        protected override void OnClick(EventArgs e)
        {
            KryptonToolkitInformation.ShowCore(_toolkitType);
        }

        protected override void OnPaint(PaintEventArgs? e)
        {
            base.OnPaint(e);

            switch (_toolkitType)
            {
                case ToolkitType.Canary:
                    Values.Image = GenericKryptonImageResources.Krypton_Canary_Button;
                    break;
                case ToolkitType.Nightly:
                    Values.Image = GenericKryptonImageResources.Krypton_Nightly_Button;
                    break;
                case ToolkitType.LongTermSupport:
                    //Values.Image = GenericKryptonImageResources.Krypton_LTS_Button;
                    break;
                case ToolkitType.Stable:
                default:
                    Values.Image = GenericKryptonImageResources.Krypton_Stable_Button;
                    break;
            }
        }
    }
}