#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion


namespace Krypton.Docking
{
    /// <summary>
    /// Tracks the currently active form without polling GetForegroundWindow.
    /// </summary>
    public static class ActiveFormTracker
    {
        #region Instance Fields

        private static Form? _activeForm;

        #endregion

        #region Public

        /// <summary>Gets the active form.</summary>
        /// <value>The active form.</value>
        public static Form? ActiveForm => _activeForm;

        #endregion

        #region Implementation

        public static void Attach(Form? form)
        {
            if (form != null)
            {
                form.Activated += (sender, args) => _activeForm = form;

                form.Deactivate += (sender, args) => { _activeForm ??= null; };
            }
        }

        /// <summary>Automates the attach all open forms.</summary>
        public static void AutoAttachAllOpenForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                Attach(form);
            }
        }

        /// <summary>Starts the automatic tracking. Call this in your 'Program.cs/vb' file.</summary>
        public static void StartAutomaticTracking() => Application.Idle += (_, _) => AutoAttachAllOpenForms();

        #endregion
    }
}