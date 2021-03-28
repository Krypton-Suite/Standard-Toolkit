using System.ComponentModel;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for a tree node triple.
    /// </summary>
    public class PaletteTreeNodeTripleRedirect : Storage                                            
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTreeNodeTripleRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="backStyle">Initial background style.</param>
        /// <param name="borderStyle">Initial border style.</param>
        /// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTreeNodeTripleRedirect(PaletteRedirect redirect,
                                             PaletteBackStyle backStyle,
                                             PaletteBorderStyle borderStyle,
                                             PaletteContentStyle contentStyle,
                                             NeedPaintHandler needPaint)
        {
            Debug.Assert(redirect != null);
            Node = new PaletteTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Node.IsDefault;

        #endregion

        #region Node
        /// <summary>
        /// Gets the node appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining node appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Node { get; }

        private bool ShouldSerializeItem()
        {
            return !Node.IsDefault;
        }
        #endregion
    }
}
