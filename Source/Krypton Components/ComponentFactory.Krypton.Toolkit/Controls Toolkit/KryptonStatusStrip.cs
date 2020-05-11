using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
    [ToolboxBitmap(typeof(StatusStrip)), Description("A status strip with the Krypton theme."), ToolboxItem(true)]
    public class KryptonStatusStrip : StatusStrip
    {
        #region Variables
        private ToolStripProgressBar[] _progressBars;
        #endregion

        #region Properties
        //public ToolStripProgressBar[] ProgressBars { get => _progressBars; set => _progressBars = value; }
        #endregion

        #region Constructor
        public KryptonStatusStrip()
        {
            // Use Krypton
            RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }
        #endregion

        #region Overrides
        protected override void OnRendererChanged(EventArgs e)
        {
            // TODO: Needs looking at
            /*
              if (ToolStripManager.Renderer is KryptonProfessionalRenderer renderer)
              {
                  foreach (ToolStripProgressBar progressBar in ProgressBars)
                  {
                      progressBar.BackColor = renderer.KCT.StatusStripGradientEnd;
                  }
              }
              */

            base.OnRendererChanged(e);
        }
        #endregion
    }
}