#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
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

        private const VisualForm? DEFAULT_PARENT_FORM = null;

        const VisualForm? DEFAULT_NEXT_FORM = null;

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [fading enabled].</summary>
        /// <value><c>true</c> if [fading enabled]; otherwise, <c>false</c>.</value>
        [Category(@"Data")]
        [Description(@"Enables the fading effects. Default is 'false'.")]
        [DefaultValue(DEFAULT_FADING_ENABLED)]
        public bool FadingEnabled { get; set; }

        /// <summary>Gets or sets a value indicating whether [should close on fade out].</summary>
        /// <value><c>true</c> if [should close on fade out]; otherwise, <c>false</c>.</value>
        [Category(@"Data")]
        [Description(@"Should the form fade out on close. Default is 'true'.")]
        [DefaultValue(DEFAULT_SHOULD_CLOSE_ON_FADE_OUT)]
        public bool ShouldCloseOnFadeOut { get; set; }

        /// <summary>Gets or sets the fade speed.</summary>
        /// <value>The fade speed.</value>
        [Category(@"Data")]
        [Description(@"Controls the fading speed. Default is '0.5'.")]
        [DefaultValue(DEFAULT_FADE_SPEED)]
        public float FadeSpeed { get; set; }

        /// <summary>Gets or sets the duration of the fade.</summary>
        /// <value>The duration of the fade.</value>
        [Category(@"Data")]
        [Description(@"Controls the fading duration. Default is '50'.")]
        [DefaultValue(DEFAULT_FADE_DURATION)]
        public int FadeDuration { get; set; }

        /*/// <summary>Gets or sets the parent form.</summary>
        /// <value>The parent form.</value>
        [Category(@"Visuals")]
        [Description(@"The KryptonForm to apply fading effects to. Default is 'null'.")]
        [DefaultValue(DEFAULT_PARENT_FORM)]
        public VisualForm? ParentForm { get; set; }

        /// <summary>Gets or sets the next window.</summary>
        /// <value>The next window.</value>
        [Category(@"Visuals")]
        [Description(@"The child KryptonForm to apply fading effects to. Default is 'null'.")]
        [DefaultValue(DEFAULT_NEXT_FORM)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public VisualForm? NextWindow { get; set; }*/

        #endregion

        #region Identity

        public FadeValues()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public override bool IsDefault => FadingEnabled.Equals(DEFAULT_FADING_ENABLED) &&
                                          ShouldCloseOnFadeOut.Equals(DEFAULT_SHOULD_CLOSE_ON_FADE_OUT) &&
                                          FadeDuration.Equals(DEFAULT_FADE_DURATION) &&
                                          FadeSpeed.Equals(DEFAULT_FADE_SPEED) /*&&
                                          ParentForm!.Equals(DEFAULT_PARENT_FORM) &&
                                          NextWindow!.Equals(DEFAULT_NEXT_FORM)*/;

        #endregion

        #region Implementation

        public void Reset()
        {
            FadingEnabled = DEFAULT_FADING_ENABLED;

            ShouldCloseOnFadeOut = DEFAULT_SHOULD_CLOSE_ON_FADE_OUT;

            FadeDuration = DEFAULT_FADE_DURATION;

            FadeSpeed = DEFAULT_FADE_SPEED;

            //ParentForm = DEFAULT_PARENT_FORM;

            //NextWindow = DEFAULT_NEXT_FORM;
        }

        #endregion
    }
}
