namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for month calendar appearance states.
    /// </summary>
    public class PaletteMonthCalendarState : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarState class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect)
            : this(redirect, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarState class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect,
                                         NeedPaintHandler needPaint) =>
            Day = new PaletteTriple(redirect.Day, needPaint);

        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Day.IsDefault;

        #endregion

        #region Day
        /// <summary>
        /// Gets access to the day appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining day appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Day { get; }

        private bool ShouldSerializeDay() => !Day.IsDefault;

        #endregion
    }
}
