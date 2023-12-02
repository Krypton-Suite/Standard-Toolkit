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
    public class CommandLinkTextValues : CaptionValues
    {
        #region Static Fields

        private const string DEFAULT_HEADING = @"Krypton Command Link Button";

        private const string DEFAULT_DESCRIPTION = @"Krypton Command Link Button ""Note Text""";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="CommandLinkTextValues" /> class.</summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public CommandLinkTextValues(NeedPaintHandler needPaint) : base(needPaint)
        {
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
        }

        #endregion
    }
}