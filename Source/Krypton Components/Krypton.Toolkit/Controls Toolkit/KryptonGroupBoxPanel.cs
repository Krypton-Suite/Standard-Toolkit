namespace Krypton.Toolkit
{
    /// <summary>
    /// Special panel used in the KryptonGroup and KryptonHeaderGroup controls.
    /// </summary>
    public class KryptonGroupBoxPanel : KryptonGroupPanel
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonGroupPanel class.
        /// </summary>
        /// <param name="alignControl">Container control for alignment.</param>
        /// <param name="stateCommon">Common appearance state to inherit from.</param>
        /// <param name="stateDisabled">Disabled appearance state.</param>
        /// <param name="stateNormal">Normal appearance state.</param>
        /// <param name="layoutHandler">Callback delegate for layout processing.</param>
        public KryptonGroupBoxPanel(Control alignControl,
            PaletteDoubleRedirect stateCommon,
            PaletteDouble stateDisabled,
            PaletteDouble stateNormal,
            NeedPaintHandler layoutHandler)
            : base(alignControl, stateCommon, stateDisabled, stateNormal, layoutHandler)
        {
        }
        #endregion

        #region Public
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KryptonContextMenu KryptonContextMenu
        {
            get => base.KryptonContextMenu;
            set { /* Ignore request */ }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get => base.ContextMenuStrip;
            set { /* Ignore request */ }
            }

        #endregion public

    }
}
