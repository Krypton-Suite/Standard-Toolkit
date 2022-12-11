#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

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

        public KryptonProgressBar() => _useKrypton = true;

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