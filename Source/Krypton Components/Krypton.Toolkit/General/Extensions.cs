#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class Extensions
{
    /// <summary>
    /// Returns if the control is in desigmode.
    /// </summary>
    /// <param name="control">The control instance to operate on.</param>
    /// <returns></returns>
    internal static bool InDesignMode(this Control control)
    {
        return control.Site?.DesignMode ?? false;
    }

    /// <summary>
    /// Returns if the component is in desigmode.
    /// </summary>
    /// <param name="component">The component instance to operate on.</param>
    /// <returns></returns>
    internal static bool InDesignMode(this Component component)
    {
        return component.Site?.DesignMode ?? false;
    }
}

