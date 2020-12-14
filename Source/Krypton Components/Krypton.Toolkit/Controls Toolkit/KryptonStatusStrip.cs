using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(StatusStrip)), Description("A Krypton based status strip."), ToolboxItem(true)]
    public class KryptonStatusStrip : StatusStrip
    {
        #region Variables
        private ToolStripProgressBar[] _progressBars;

        private Color[] _progressbarColours;
        #endregion

        #region Properties
        public ToolStripProgressBar[] ProgressBars { get => _progressBars; set => _progressBars = value; }
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
			try
            {
				if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
				{
					foreach (ToolStripProgressBar progressBar in ProgressBars)
					{
						progressBar.BackColor = kpr.KCT.StatusStripGradientEnd;
					}
				}
			}
			catch (Exception exc)
			{
			}
			 
			 base.OnRendererChanged(e);
        }
        #endregion
    }
}