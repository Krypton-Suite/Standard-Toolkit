using System;

namespace Krypton.Toolkit
{
    internal class Utility
    {
        #region Constructor
        public Utility()
        {

        }
        #endregion

        #region Methods
        public static bool IsSevenOrHigher()
        {
            Version osVersion = Environment.OSVersion.Version;

            if (osVersion.Major >= 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}