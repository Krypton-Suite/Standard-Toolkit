using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for bread crumb appearance states.
    /// </summary>
    public class PaletteBreadCrumbState : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBreadCrumbState class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBreadCrumbState(PaletteBreadCrumbRedirect redirect,
                                      NeedPaintHandler needPaint) 
        {
            BreadCrumb = new PaletteTriple(redirect.BreadCrumb, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => BreadCrumb.IsDefault;

        #endregion

        #region BreadCrumb
        /// <summary>
        /// Gets access to the bread crumb appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining bread crumb appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple BreadCrumb { get; }

        private bool ShouldSerializeBreadCrumb()
        {
            return !BreadCrumb.IsDefault;
        }
        #endregion
    }
}
