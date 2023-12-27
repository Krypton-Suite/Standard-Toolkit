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
    /// <summary></summary>
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

        #region Instance Fields

        private bool? _useThreeColorIndicator;

        private bool? _useValueAsText;

        private Color? _colorOne;

        private Color? _colorTwo;

        private Color? _colorThree;

        private int? _minimum;

        private int? _maximum;

        private int? _step;

        #endregion

        #region Public

        public bool? UseThreeColors 
        {
            get => _useThreeColorIndicator; 
       
            set
            {
                if (_useThreeColorIndicator != value)
                {
                    _useThreeColorIndicator = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public bool? UseValueAsText 
        {
            get => _useValueAsText; 
       
            set
            {
                if (_useValueAsText != value)
                {
                    _useValueAsText = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public Color? ColorOne 
        {
            get => _colorOne; 
       
            set
            {
                if (_colorOne != value)
                {
                    _colorOne = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public Color? ColorTwo 
        {
            get => _colorTwo; 
       
            set
            {
                if (_colorTwo != value)
                {
                    _colorTwo = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public Color? ColorThree 
        {
            get => _colorThree; 
       
            set
            {
                if (_colorThree != value)
                {
                    _colorThree = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public int? Minimum 
        {
            get => _minimum; 
       
            set
            {
                if (_minimum != value)
                {
                    _minimum = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public int? Maximum 
        {
            get => _maximum; 
       
            set
            {
                if (_maximum != value)
                {
                    _maximum = value;

                    PerformNeedPaint(true);
                }
            }
        }

        public int? Step 
        {
            get => _step; 
       
            set
            {
                if (_step != value)
                {
                    _step = value;

                    PerformNeedPaint(true);
                }
            }
        }

        #endregion

        #region Identity

        public ProgressBarValues(NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;

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
