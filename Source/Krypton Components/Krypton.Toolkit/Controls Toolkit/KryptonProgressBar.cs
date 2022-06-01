namespace Krypton.Toolkit
{
    public partial class KryptonProgressBar : ProgressBar
    {
        #region Instance Fields

        private bool _useKrypton;

        #endregion

        #region Properties

        public bool UseKrypton { get => _useKrypton; set { _useKrypton = value; Invalidate(); } }

        #endregion

        #region Constructor

        public KryptonProgressBar()
        {
            _useKrypton = true;
        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_useKrypton)
            {
                if (ProgressBarRenderer.IsSupported)
                {
                    KryptonProfessionalRenderer kpr = null;

                    // Note: Is this correct?
                    Color color = kpr.KCT.StatusStripGradientEnd;

                    BackColor = color;
                }
            }

            base.OnPaint(e);
        }
        

        #endregion
    }
}