namespace Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonWorkspace to work within the docking edge of a control.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockspace : KryptonSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockspace class.
        /// </summary>
        /// <remarks>
        /// If Min Size not set in the Embedded control, then will default to 150, 50
        /// </remarks>
        public KryptonDockspace()
            : base(@"Docked") =>
            // Define a sensible default minimum size
            base.MinimumSize = new Size(150, 150);

        /// <summary>
        /// Gets a string representation of the class.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $@"KryptonDockspace {Dock}";

        #endregion
    }
}
