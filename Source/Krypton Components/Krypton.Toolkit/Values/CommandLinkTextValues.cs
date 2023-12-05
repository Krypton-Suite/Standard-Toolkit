#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class CommandLinkTextValues : CaptionValues, IContentValues
    {
        #region Static Fields

        private const string DEFAULT_HEADING = @"Krypton Command Link Button";

        private const string DEFAULT_DESCRIPTION = @"Krypton Command Link Button ""Note Text""";

        #endregion

        #region Instance Fields

        private Font? _descriptionFont;

        private Font? _headingFont;

        private PaletteRelativeAlign? _descriptionTextHAlignment;

        private PaletteRelativeAlign? _descriptionTextVAlignment;

        private PaletteRelativeAlign? _headingTextHAlignment;

        private PaletteRelativeAlign? _headingTextVAlignment;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="CommandLinkTextValues" /> class.</summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public CommandLinkTextValues(NeedPaintHandler needPaint) : base(needPaint)
        {
            _descriptionFont = null;

            _headingFont = null;

            _descriptionTextHAlignment = PaletteRelativeAlign.Near;

            _descriptionTextVAlignment = PaletteRelativeAlign.Far;

            _headingTextHAlignment = PaletteRelativeAlign.Near;

            _headingTextVAlignment = PaletteRelativeAlign.Center;
        }

        #endregion

        #region Protected

        /// <inheritdoc />
        protected override string GetDescriptionDefault() => DEFAULT_DESCRIPTION;

        /// <inheritdoc />
        protected override string GetHeadingDefault() => DEFAULT_HEADING;

        #endregion

        #region Implementation

        /// <inheritdoc />
        [DefaultValue(DEFAULT_DESCRIPTION)]
        public override string Description { get => base.Description; set => base.Description = value; }

        /// <summary>Resets the text.</summary>
        public void ResetText()
        {
            Heading = DEFAULT_HEADING;

            Description = DEFAULT_DESCRIPTION;

            DescriptionFont = _descriptionFont;

            HeadingFont = _headingFont;
        }

        [DefaultValue(null)]
        public Font? DescriptionFont
        {
            get => _descriptionFont;

            set
            {
                if (_descriptionFont != value)
                {
                    _descriptionFont = value;

                    PerformNeedPaint(true);
                }
            }
        }

        [DefaultValue(null)]
        public Font? HeadingFont
        {
            get => _headingFont;

            set
            {
                if (_headingFont != value)
                {
                    _headingFont = value;

                    PerformNeedPaint(true);
                }
            }
        }

        [DefaultValue(null)]
        public PaletteRelativeAlign? DescriptionTextHAlignment
        {
            get => _descriptionTextHAlignment;

            set
            {
                if (_descriptionTextHAlignment != value)
                {
                    _descriptionTextHAlignment = value;

                    PerformNeedPaint(true);
                }
            }
        }

        [DefaultValue(null)]
        public PaletteRelativeAlign? DescriptionTextVAlignment
        {
            get => _descriptionTextVAlignment;

            set
            {
                if (_descriptionTextVAlignment != value)
                {
                    _descriptionTextVAlignment = value;

                    PerformNeedPaint(true);
                }
            }
        }

        [DefaultValue(null)]
        public PaletteRelativeAlign? HeadingTextHAlignment
        {
            get => _headingTextHAlignment;

            set
            {
                if (_headingTextHAlignment != value)
                {
                    _headingTextHAlignment = value;

                    PerformNeedPaint(true);
                }
            }
        }

        [DefaultValue(null)]
        public PaletteRelativeAlign? HeadingTextVAlignment
        {
            get => _headingTextVAlignment;

            set
            {
                if (_headingTextVAlignment != value)
                {
                    _headingTextVAlignment = value;

                    PerformNeedPaint(true);
                }
            }
        }

        #endregion
    }
}