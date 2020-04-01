using System;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    public class KryptonStatusStrip : StatusStrip
    {
        #region Variables
        private ToolStripProgressBar[] _progressBars;
        #endregion

        #region Properties
        public ToolStripProgressBar[] ProgressBars { get => _progressBars; set => _progressBars = value; }
        #endregion

        #region Constructor
        public KryptonStatusStrip()
        {

        }
        #endregion

        #region Overrides
        protected override void OnRendererChanged(EventArgs e)
        {
            if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
            {
                foreach (ToolStripProgressBar progressBar in ProgressBars)
                {
                    progressBar.BackColor = kpr.KCT.StatusStripGradientEnd;
                }
            }

            base.OnRendererChanged(e);
        }
        #endregion
    }
}