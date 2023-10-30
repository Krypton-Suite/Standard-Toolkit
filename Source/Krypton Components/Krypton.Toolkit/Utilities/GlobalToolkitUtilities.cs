using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public class GlobalToolkitUtilities
    {
        #region Identity

        public GlobalToolkitUtilities()
        {

        }

        #endregion

        #region Implementation

        public static void LaunchProcess(string processName)
        {
            try
            {
                Process.Start(processName);
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
            }
        }

        public static void LaunchProcess(string processName, string arguments)
        {
            try
            {
                Process.Start(processName, arguments);
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
            }
        }

        #endregion
    }
}