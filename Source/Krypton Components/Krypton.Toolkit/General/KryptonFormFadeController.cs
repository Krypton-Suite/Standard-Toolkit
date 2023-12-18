#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>This deals with the fading in and out of <see cref="KryptonForm"/>. The developer must explicitly enable this, as it is turned off by default.</summary>
    internal class KryptonFormFadeController
    {
        #region Instance Fields

        private bool _fadingEnabled;

        private bool _shouldClose;

        private float _fadeIn;

        private float _fadeOut;

        private readonly VisualForm? _parentForm;

        private readonly VisualForm? _nextForm;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonFormFadeController" /> class.</summary>
        public KryptonFormFadeController()
        {
            _fadingEnabled = false;

            _shouldClose = true;

            _fadeIn = 0.0f;

            _fadeOut = 0.0f;

            _parentForm = null;

            _nextForm = null;
        }

        #endregion

        #region Implementation

        public static void FadeFormIn(VisualForm owner, int? fadeSpeed)
        {
            KryptonFormFadeController controller = new KryptonFormFadeController();

            for (controller._fadeIn = 0.0f; controller._fadeIn <= 1.1f; controller._fadeIn += 0.1f)
            {
                owner.Opacity = controller._fadeIn;

                owner.Refresh();

                int timer = fadeSpeed ?? 1000;

                Thread.Sleep(timer);
            }
        }

        public static async void FadeFormOut(VisualForm owner, VisualForm? nextWindow, int? fadeSpeed)
        {
            int timer = fadeSpeed ?? 1000;

            while (owner.Opacity > 0.0)
            {
                await Task.Delay(timer);

                owner.Opacity -= 0.05;
            }

            owner.Opacity = 0;

            nextWindow?.Show();
        }

        #endregion
    }
}