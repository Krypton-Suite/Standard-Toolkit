#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ToggleSwitchValues : GlobalId
    {
        #region Instance Fields

        //private bool _useGradient;

        //private float _gradientStartIntensity;
        //private float _gradientEndIntensity;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="ToggleSwitchValues" /> class.</summary>
        public ToggleSwitchValues()
        {
            Reset();
        }

        /// <inheritdoc />
        public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

        #endregion

        #region IsDefault

        /// <summary>Gets a value indicating whether this instance is default.</summary>
        /// <value>
        ///   <c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsDefault => AnimateToggle.Equals(true) &&
                                 EnableKnobGradient.Equals(false) &&
                                 GradientStartIntensity.Equals(0.8f) &&
                                 GradientEndIntensity.Equals(0.6f) &&
                                 ShowText.Equals(true) &&
                                 GradientDirection.Equals(LinearGradientMode.ForwardDiagonal) &&
                                 OffColor.Equals(Color.Red) &&
                                 OnColor.Equals(Color.Green) && 
                                 CornerRadius.Equals(10);

        #endregion

        #region Public

        public void Reset()
        {
            AnimateToggle = true;

            EnableKnobGradient = false;

            GradientStartIntensity = 0.8f;

            GradientEndIntensity = 0.6f;

            GradientDirection = LinearGradientMode.ForwardDiagonal;

            ShowText = true;

            OffColor = Color.Red;

            OnColor = Color.Green;

            CornerRadius = 10;
        }

        /// <summary>Gets or sets a value indicating whether [animate toggle].</summary>
        /// <value><c>true</c> if [animate toggle]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Description("Use animation when toggling.")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AnimateToggle { get; set; }

        /// <summary>Gets or sets a value indicating whether [enable knob gradient].</summary>
        /// <value><c>true</c> if [enable knob gradient]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Description("Indicates whether the knob should have a gradient effect.")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EnableKnobGradient { get; set; }

        [Category("Appearance")]
        [Description("Specifies the color of the knob.")]
        [DefaultValue(typeof(Color), "Color.Red")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color OffColor { get; set; }

        [Category("Appearance")]
        [Description("Specifies the color of the knob.")]
        [DefaultValue(typeof(Color), "Color.Green")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color OnColor { get; set; }

        /// <summary>Gets or sets the gradient start intensity.</summary>
        /// <value>The gradient start intensity.</value>
        [Category("Appearance")]
        [Description("Specifies the gradient intensity for the knob.")]
        [DefaultValue(0.8f)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float GradientStartIntensity { get; set; }

        /// <summary>Gets or sets the gradient end intensity.</summary>
        /// <value>The gradient end intensity.</value>
        [Category("Appearance")]
        [Description("Specifies the gradient intensity for the knob.")]
        [DefaultValue(0.6f)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float GradientEndIntensity { get; set; }

        /// <summary>Gets or sets a value indicating whether [show text].</summary>
        /// <value>
        ///   <c>true</c> if [show text]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Description("Specifies whether the text should be displayed.")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowText { get; set; }

        /// <summary>Gets or sets the gradient direction.</summary>
        /// <value>The gradient direction.</value>
        [Category("Appearance")]
        [Description("Specifies the direction of the gradient.")]
        [DefaultValue(LinearGradientMode.ForwardDiagonal)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LinearGradientMode GradientDirection { get; set; }

        [Category("Appearance")]
        [Description("Specifies the color of the knob.")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseThemeColors { get; set; }

        [Category("Visuals")]
        [Description("Defines the corner radius of the toggle switch.")]
        [DefaultValue(10)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; }

        #endregion
    }
}
