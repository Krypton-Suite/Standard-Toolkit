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
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProgressBarValues : Storage
    {
        #region Static Variables

        private const bool DEFAULT_USE_THREE_COLOR_INDICATOR = false;

        private const bool DEFAULT_USE_VALUE_AS_TEXT = false;

        private readonly Color DEFAULT_COLOR_ONE = Color.Red;

        private readonly Color DEFAULT_COLOR_TWO = Color.Orange;

        private readonly Color DEFAULT_COLOR_THREE = Color.Green;

        private const int DEFAULT_MINIMUM = 0;

        private const int DEFAULT_MAXIMUM = 100;

        private const int DEFAULT_STEP = 10;

        #endregion

        #region Public

        public bool UseThreeColors { get; set; }

        public bool UseValueAsText { get; set; }

        public Color? ColorOne { get; set; }

        public Color? ColorTwo { get; set; }

        public Color? ColorThree { get; set; }

        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public int Step { get; set; }

        #endregion

        #region Identity

        public ProgressBarValues()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public override bool IsDefault => UseThreeColors.Equals(DEFAULT_USE_THREE_COLOR_INDICATOR) &&
                                          UseValueAsText.Equals(DEFAULT_USE_VALUE_AS_TEXT) &&
                                          ColorOne.Equals(DEFAULT_COLOR_ONE) &&
                                          ColorTwo.Equals(DEFAULT_COLOR_TWO) &&
                                          ColorThree.Equals(DEFAULT_COLOR_THREE) &&
                                          Minimum.Equals(DEFAULT_MINIMUM) &&
                                          Maximum.Equals(DEFAULT_MAXIMUM) &&
                                          Step.Equals(DEFAULT_STEP);

        #endregion

        #region Implementation

        public void Reset()
        {
            UseThreeColors = DEFAULT_USE_THREE_COLOR_INDICATOR;
            UseValueAsText = DEFAULT_USE_VALUE_AS_TEXT;
            ColorOne = DEFAULT_COLOR_ONE;
            ColorTwo = DEFAULT_COLOR_TWO;
            ColorThree = DEFAULT_COLOR_THREE;
            Minimum = DEFAULT_MINIMUM;
            Maximum = DEFAULT_MAXIMUM;
            Step = DEFAULT_STEP;
        }

        #endregion
    }
}
