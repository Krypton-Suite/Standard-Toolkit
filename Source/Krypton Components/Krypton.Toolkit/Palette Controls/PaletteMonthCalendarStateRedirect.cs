namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for month calendar appearance states.
    /// </summary>
    public class PaletteMonthCalendarStateRedirect : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarStateRedirect class.
        /// </summary>
        public PaletteMonthCalendarStateRedirect()
            : this(null, null)
        {
        }

            /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarStateRedirect class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarStateRedirect(PaletteRedirect redirect,
                                                 NeedPaintHandler needPaint) =>
                Day = new PaletteTripleRedirect(redirect, 
                    PaletteBackStyle.ButtonCalendarDay, 
                    PaletteBorderStyle.ButtonCalendarDay, 
                    PaletteContentStyle.ButtonCalendarDay, 
                    needPaint);

            #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Day.IsDefault;

        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect) => Day.SetRedirector(redirect);

        #endregion

        #region Styles
        internal ButtonStyle DayStyle
        {
            set => Day.SetStyles(value);
        }
        #endregion

        #region Day
        /// <summary>
        /// Gets access to the day appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining day appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Day { get; }

        private bool ShouldSerializeDay() => !Day.IsDefault;

        #endregion
    }
}
