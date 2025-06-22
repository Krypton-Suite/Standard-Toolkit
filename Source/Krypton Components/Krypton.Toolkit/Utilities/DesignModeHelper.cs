#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class DesignModeHelper
    {
        #region Instance FieldsAdd commentMore actions

        private static readonly bool _isInDesignMode;

        #endregion

        #region Public

        /// <summary>
        /// Gets a value indicating whether the application is running inside the Visual Studio designer.
        /// </summary>
        public static bool IsInDesignMode => _isInDesignMode;

        #endregion

        #region Identity

        /// <summary>
        /// Initializes the <see cref="DesignModeHelper"/> class.
        /// </summary>
        static DesignModeHelper()
        {
            _isInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime &&
                              Process.GetCurrentProcess().ProcessName == "devenv";
        }

        #endregion
    }
}