#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class KryptonTaskDialogExtensions
{
    /// <summary>
    /// A control object that is set to be visible and part of a form/control reports Visible as false when the form is not visible.<br/>
    /// Internally Control maintains the correct state or DesiredVisibility.
    /// </summary>
    /// <param name="control">The control this extension applies to.</param>
    /// <returns>If the control's real visibility state.</returns>
    internal static bool GetDesiredVisibility(this Control control)
    {
        bool result = false;

        MethodInfo? mi = typeof(Control).GetMethod("GetState", BindingFlags.Instance | BindingFlags.NonPublic);
        if ( mi is not null)
        {
            result = ((bool?)mi.Invoke(control, BindingFlags.Instance | BindingFlags.NonPublic, null, [2], CultureInfo.InvariantCulture)) ?? false;
        }

        return result;
    }
}