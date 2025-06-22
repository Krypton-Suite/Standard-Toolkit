#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion



namespace Krypton.Toolkit;

public class GeneralToolkitUtilities
{
    #region Implementation

    /// <summary>Gets the size of the current screen.</summary>
    /// <returns></returns>
    public static Point GetCurrentScreenSize()
    {
        var screenWidth = Screen.PrimaryScreen!.Bounds.Width;

        var screenHeight = Screen.PrimaryScreen!.Bounds.Height;

        return new Point(screenWidth, screenHeight);
    }

    /// <summary>Adjusts the form dimensions.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="width">The width.</param>
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
            KryptonExceptionHandler.CaptureException(e);
        }
    }

    internal static void Start(string inputLocation, string arguments)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo(inputLocation, arguments)
            {
                UseShellExecute = true
            };
            Process.Start(processStartInfo);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);
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
            KryptonExceptionHandler.CaptureException(e);
        }
    }

    #endregion
}