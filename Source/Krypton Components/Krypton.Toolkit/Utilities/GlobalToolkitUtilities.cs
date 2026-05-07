#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary></summary>
public class GlobalToolkitUtilities
{
    #region Identity

    public GlobalToolkitUtilities()
    {

    }

    #endregion

    #region Implementation

    /// <summary>Launches a chosen process.</summary>
    /// <param name="processName">Name of the process.</param>
    public static void LaunchProcess(string processName)
    {
        try
        {
            Process.Start(processName);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Launches a chosen process.</summary>
    /// <param name="processName">Name of the process.</param>
    /// <param name="arguments">The arguments  to pass through.</param>
    public static void LaunchProcess(string processName, string arguments)
    {
        try
        {
            Process.Start(processName, arguments);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Launches a chosen process.</summary>
    /// <param name="startInfo">The <see cref="ProcessStartInfo"/> object in which to start.</param>
    public static Process? LaunchProcess(ProcessStartInfo startInfo)
    {
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }

        return null;
    }

    /// <summary>Gets the file list.</summary>
    /// <param name="directory">The directory.</param>
    /// <param name="fileType">Type of the file.</param>
    /// <param name="searchOption">The search option.</param>
    /// <returns></returns>
    public static List<string>? GetFileList(string directory, string? fileType, SearchOption searchOption = SearchOption.AllDirectories)
    {
        try
        {
            List<string>? fileList = new List<string>();

            if (string.IsNullOrEmpty(fileType))
            {
                fileList = Directory.GetFiles(directory, "*", searchOption).ToList();
            }
            else
            {
                fileList = Directory.GetFiles(directory, fileType, searchOption).ToList();
            }

            return fileList;
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }

        return null;
    }

    #endregion
}