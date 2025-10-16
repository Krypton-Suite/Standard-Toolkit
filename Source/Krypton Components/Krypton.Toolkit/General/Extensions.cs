#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class Extensions
{
    #region Component.InDesignMode
    /// <summary>
    /// Returns if the component is in desigmode.
    /// </summary>
    /// <param name="component">The component instance to operate on.</param>
    /// <returns></returns>
    internal static bool InDesignMode(this Component component)
    {
        return component.Site?.DesignMode ?? false;
    }
    #endregion

    #region Control.DoubleBuffered
    /// <summary>
    /// Enable or disable double buffering on the given control.<br/>
    /// Note: Some classes derived from Control expose their own DoubleBuffered property.
    /// </summary>
    /// <param name="control">The instance to operate on.</param>
    /// <param name="enableDoubleBuffering">Enable or disable double buffering.</param>
    /// <exception cref="NullReferenceException">When the property was not found.</exception>
    internal static void SetDoubleBuffered(this Control control, bool enableDoubleBuffering)
    {
        PropertyInfo? propertyInfo = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        if (propertyInfo is not null)
        {
            propertyInfo.SetValue(control, enableDoubleBuffering);
        }
        else
        {
            throw new NullReferenceException(nameof(propertyInfo));
        }
    }

    /// <summary>
    /// Return the state of the control's DoubleBuffered property.<br/>
    /// Note: Some classes derived from Control expose their own DoubleBuffered property.
    /// </summary>
    /// <param name="control">The instance to operate on.</param>
    /// <returns>The current state.</returns>
    /// <exception cref="NullReferenceException">When the property was not found.</exception>
    internal static bool GetDoubleBuffered(this Control control)
    {
        PropertyInfo? propertyInfo = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        if (propertyInfo is not null)
        {
            return propertyInfo.GetValue(control) is bool result
                ? result
                : false;
        }
        else
        {
            throw new NullReferenceException(nameof(propertyInfo));
        }
    }
    #endregion
}

