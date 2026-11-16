#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class GeneralLibraryUtilities
{
    /// <summary>Gets the size of the primary screen.</summary>
    public static Point GetCurrentScreenSize()
    {
        var bounds = Screen.PrimaryScreen!.Bounds;

        return new Point(bounds.Width, bounds.Height);
    }

    /// <summary>Adjusts the form dimensions.</summary>
    /// <param name="owner">The form to resize.</param>
    /// <param name="width">The new width.</param>
    /// <param name="height">The height.</param>
    internal static void AdjustFormDimensions(KryptonForm owner, int width, int height) => owner.Size = new Size(width, height);

    internal static void Start(string inputLocation)
    {
        try
        {
            Process.Start(inputLocation);
        }
        catch (Exception e)
        {
            Krypton.Toolkit.KryptonExceptionHandler.CaptureException(e);
        }
    }

    internal static void Start(string inputLocation, string arguments)
    {
        try
        {
            Process.Start(new ProcessStartInfo(inputLocation, arguments)
            {
                UseShellExecute = true
            });
        }
        catch (Exception e)
        {
            Krypton.Toolkit.KryptonExceptionHandler.CaptureException(e);
        }
    }

    internal static void Start(ProcessStartInfo processStartInfo)
    {
        try
        {
            Process.Start(processStartInfo);
        }
        catch (Exception e)
        {
            Krypton.Toolkit.KryptonExceptionHandler.CaptureException(e);
        }
    }
}
