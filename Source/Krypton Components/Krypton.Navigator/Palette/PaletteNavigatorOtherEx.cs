namespace Krypton.Navigator
{
    /// <summary>
    /// Implement storage for other navigator appearance states.
    /// </summary>
    public class PaletteNavigatorOtherEx : PaletteNavigatorOther
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOtherEx class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorOtherEx(PaletteNavigatorRedirect redirect,
                                       NeedPaintHandler needPaint)
            : base(redirect, needPaint) =>
            // Create the palette storage
            Separator = new PaletteSeparatorPadding(redirect.Separator, redirect.Separator, needPaint);

        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => base.IsDefault && 
                                          Separator.IsDefault;

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritance parent.
        /// </summary>
        /// <param name="inheritNavigator">Source for inheriting.</param>
        public override void SetInherit(PaletteNavigator inheritNavigator)
        {
            Separator.SetInherit(inheritNavigator.Separator);
            base.SetInherit(inheritNavigator);
        }
        #endregion

        #region Separator
        /// <summary>
        /// Get access to the overrides for defining separator appearance.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPadding Separator { get; }

        private bool ShouldSerializeSeparator() => !Separator.IsDefault;

        #endregion
    }
}
