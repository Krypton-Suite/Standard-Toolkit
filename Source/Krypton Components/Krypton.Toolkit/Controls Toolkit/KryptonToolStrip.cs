using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(ToolStrip)), Description("A standard tool strip equipped with the Krypton theme."), ToolboxItem(false)]
    public class KryptonToolStrip : ToolStrip
    {
        #region Constructor
        public KryptonToolStrip()
        {
            // Use Krypton
            RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }
        #endregion
    }
}