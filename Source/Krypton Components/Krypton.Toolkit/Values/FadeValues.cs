#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Controls the values for <see cref="KryptonFormFadeController"/>.</summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public class FadeValues : Storage
    {
        #region Static Values

        private const bool DEFAULT_FADING_ENABLED = false;

        private const bool DEFAULT_SHOULD_CLOSE_ON_FADE_OUT = true;

        private const float DEFAULT_FADE_SPEED = 0.5f;

        private const int DEFAULT_FADE_DURATION = 50;

        private const VisualForm? DEFAULT_OWNER_FORM = null;

        private static FadeSpeedChoice DEFAULT_FADE_SPEED_CHOICE = FadeSpeedChoice.Normal;

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [fading enabled].</summary>
        /// <value><c>true</c> if [fading enabled]; otherwise, <c>false</c>.</value>
        [Category(@"Data")]
        [Description(@"Enables the fading effects. Default is 'false'.")]
        [DefaultValue(DEFAULT_FADING_ENABLED)]
        public bool FadingEnabled { get; set; }

        private bool ShouldSerializeFadingEnabled() => !FadingEnabled.Equals(DEFAULT_FADING_ENABLED);

        public void ResetFadingEnabled() => FadingEnabled = DEFAULT_FADING_ENABLED;

        /// <summary>Gets or sets a value indicating whether [should close on fade out].</summary>
        /// <value><c>true</c> if [should close on fade out]; otherwise, <c>false</c>.</value>
        [Category(@"Data")]
        [Description(@"Should the form fade out on close. Default is 'true'.")]
        [DefaultValue(DEFAULT_SHOULD_CLOSE_ON_FADE_OUT)]
        public bool ShouldCloseOnFadeOut { get; set; }

        private bool ShouldSerializeShouldCloseOnFadeOut() => !ShouldCloseOnFadeOut.Equals(DEFAULT_SHOULD_CLOSE_ON_FADE_OUT);

        public void ResetShouldCloseOnFadeOut() => ShouldCloseOnFadeOut = DEFAULT_SHOULD_CLOSE_ON_FADE_OUT;

        /// <summary>Gets or sets the fade speed.</summary>
        /// <value>The fade speed.</value>
        [Category(@"Data")]
        [Description(@"Controls the fading speed. Default is '0.5'. (Use this if you are using .NET FrameWork 4.x)")]
        [DefaultValue(DEFAULT_FADE_SPEED)]
        public float FadeSpeed { get; set; }

        private bool ShouldSerializeFadeSpeed() => !FadeSpeed.Equals(DEFAULT_FADE_SPEED);

        private void ResetFadeSpeed() => FadeSpeed = DEFAULT_FADE_SPEED;

        /// <summary>Gets or sets the duration of the fade.</summary>
        /// <value>The duration of the fade.</value>
        [Category(@"Data")]
        [Description(@"Controls the fading duration. Default is '50'. (Use this if you are using .NET)")]
        [DefaultValue(DEFAULT_FADE_DURATION)]
        public int FadeDuration { get; set; }

        private bool ShouldSerializeFadeDuration() => !FadeDuration.Equals(DEFAULT_FADE_DURATION);

        private void ResetFadeDuration() => FadeDuration = DEFAULT_FADE_DURATION;

        /// <summary>Gets or sets the fade speed choice.</summary>
        /// <value>The fade speed choice.</value>
        [Category(@"Data")]
        [Description(@"Controls the fading speed. Default is 'Normal'.")]
        [DefaultValue(typeof(FadeSpeedChoice), @"Normal")]
        public FadeSpeedChoice FadeSpeedChoice { get; set; }

        private bool ShouldSerializeFadeSpeedChoice() => !FadeSpeedChoice.Equals(DEFAULT_FADE_SPEED_CHOICE);

        private void ResetFadeSpeedChoice() => FadeSpeedChoice = DEFAULT_FADE_SPEED_CHOICE;

        [Category(@"Data")]
        [Browsable(false)]
        public VisualForm? Owner { get; set; }

        #endregion

        #region Identity

        public FadeValues()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsDefault => FadingEnabled.Equals(DEFAULT_FADING_ENABLED) &&
                                          ShouldCloseOnFadeOut.Equals(DEFAULT_SHOULD_CLOSE_ON_FADE_OUT) &&
                                          FadeDuration.Equals(DEFAULT_FADE_DURATION) &&
                                          FadeSpeed.Equals(DEFAULT_FADE_SPEED) &&
                                          FadeSpeedChoice.Equals(DEFAULT_FADE_SPEED_CHOICE) /*&&
                                          Owner.Equals(null)*/;

        #endregion

        #region Implementation

        internal void Reset()
        {
            FadingEnabled = DEFAULT_FADING_ENABLED;

            ShouldCloseOnFadeOut = DEFAULT_SHOULD_CLOSE_ON_FADE_OUT;

            FadeDuration = DEFAULT_FADE_DURATION;

            FadeSpeed = DEFAULT_FADE_SPEED;

            FadeSpeedChoice = DEFAULT_FADE_SPEED_CHOICE;

            Owner = DEFAULT_OWNER_FORM;
        }

        #endregion
    }
}
