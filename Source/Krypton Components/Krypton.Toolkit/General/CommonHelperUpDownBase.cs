#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper class for the NumericUpDown and DomainUpDown controls
/// </summary>
internal static class CommonHelperUpDownBase
{
    /// <summary>
    /// Returns the width of the contained UpDown spin button control in a Numeric- or DomainUpDown
    /// </summary>
    /// <param name="controls">The DomainUpDown- or NumericUpDown.Controls collection.</param>
    /// <returns>The width of the UpDownButtons control or 0 if not found.</returns>
    internal static int GetUpDownButtonWidth(DomainUpDown.ControlCollection controls)
    {
        int result = 0;

        // In the inner updown controls collection find the UpDownButtons control and record it's width
        foreach (var c in controls)
        {
            if (c is Control control)
            {
                if (c.GetType().Name.Equals("UpDownButtons"))
                {
                    result = control.Width;
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Returns the total width for all buttonspecs in the collection
    /// </summary>
    /// <param name="buttonSpecsCollection">ButtonSpecs collection.</param>
    /// <returns>The total width of buttons in the collection.</returns>
    internal static int GetButtonSpecsWidth(ButtonSpecCollection<ButtonSpecAny> buttonSpecsCollection)
    {
        int result = 0;

        foreach (var bs in buttonSpecsCollection)
        {
            result += (int)Math.Ceiling(bs.GetView().ClientRectangleF.Width);
        }

        return result;
    }

    /// <summary>
    /// Selects the width based on MinimumSize- and MaximumSize.Width
    /// </summary>
    /// <param name="newWidth">The new width</param>
    /// <param name="minimumWidth">Maximum width possible.</param>
    /// <param name="maximumWidth">Minimum width required.</param>
    /// <returns>The width fot the control after bounds have been checked.</returns>
    internal static int GetAutoSizeWidth(int newWidth, int minimumWidth, int maximumWidth)
    {
        int result = newWidth;

        if (newWidth > 0)
        {
            if (maximumWidth > 0 && newWidth >= maximumWidth)
            {
                result = maximumWidth;
            }
            else if (minimumWidth > 0 && newWidth <= minimumWidth)
            {
                result = minimumWidth;
            }
        }

        return result;
    }
}