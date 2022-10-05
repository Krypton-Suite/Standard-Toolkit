namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(ToolStrip)), Description(@"A standard tool strip equipped with the Krypton theme."), ToolboxItem(true)]
    public class KryptonToolStrip : ToolStrip
    {
        #region Constructor
        public KryptonToolStrip() =>
            // Use Krypton
            RenderMode = ToolStripRenderMode.ManagerRenderMode;

        #endregion
    }
}