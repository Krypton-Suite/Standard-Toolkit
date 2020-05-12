using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
    [ToolboxBitmap(typeof(ToolStrip)), Description("A standard toolbar equipped with the Krypton theme."), ToolboxItem(true)]
    public class KryptonToolBar : ToolStrip
    {
        #region Constructor
        public KryptonToolBar()
        {
            // Use Krypton
            RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }
        #endregion
    }
}