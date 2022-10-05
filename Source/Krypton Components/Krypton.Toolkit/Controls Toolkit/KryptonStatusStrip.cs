namespace Krypton.Toolkit
{    
    [ToolboxBitmap(typeof(StatusStrip)), Description(@"A Krypton based status strip."), ToolboxItem(true)]
    public class KryptonStatusStrip : StatusStrip
    {
        #region Properties
        public ToolStripProgressBar[] ProgressBars { get; set; }
        #endregion

        #region Constructor
        public KryptonStatusStrip() =>
            // Use Krypton
            RenderMode = ToolStripRenderMode.ManagerRenderMode;

        #endregion

        #region Overrides
        protected override void OnRendererChanged(EventArgs e)
        {
            if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
            {
                if (ProgressBars != null)
                {
                    foreach (ToolStripProgressBar progressBar in ProgressBars)
                    {
                        progressBar.BackColor = kpr.KCT.StatusStripGradientEnd;
                    }
                }
            }

            base.OnRendererChanged(e);
        }
        #endregion
    }
}