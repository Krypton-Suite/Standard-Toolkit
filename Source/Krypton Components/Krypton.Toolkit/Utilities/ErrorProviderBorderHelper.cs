#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper class for managing control border colors based on error provider icon types.
/// </summary>
public static class ErrorProviderBorderHelper
{
    #region Constants

    /// <summary>
    /// Default error border color (red).
    /// </summary>
    public static readonly Color ErrorBorderColor = Color.FromArgb(220, 53, 69);

    /// <summary>
    /// Default warning border color (yellow/orange).
    /// </summary>
    public static readonly Color WarningBorderColor = Color.FromArgb(255, 193, 7);

    /// <summary>
    /// Default information border color (blue).
    /// </summary>
    public static readonly Color InformationBorderColor = Color.FromArgb(0, 123, 255);

    #endregion

    #region Icon Type Detection

    /// <summary>
    /// Determines the icon type from the provided icon.
    /// </summary>
    /// <param name="icon">The icon to analyze.</param>
    /// <returns>The detected icon type, or Error if unable to determine.</returns>
    public static ErrorProviderIconType GetIconType(Icon? icon)
    {
        if (icon == null)
        {
            return ErrorProviderIconType.Error;
        }

        // Check if it's a SystemIcons instance
        if (ReferenceEquals(icon, SystemIcons.Error) || ReferenceEquals(icon, SystemIcons.Hand))
        {
            return ErrorProviderIconType.Error;
        }

        if (ReferenceEquals(icon, SystemIcons.Warning) || ReferenceEquals(icon, SystemIcons.Exclamation))
        {
            return ErrorProviderIconType.Warning;
        }

        if (ReferenceEquals(icon, SystemIcons.Information) || ReferenceEquals(icon, SystemIcons.Asterisk))
        {
            return ErrorProviderIconType.Information;
        }

        // Default to error if unable to determine
        return ErrorProviderIconType.Error;
    }

    /// <summary>
    /// Determines the icon type from the error message text (optional heuristic).
    /// </summary>
    /// <param name="errorMessage">The error message text.</param>
    /// <returns>The detected icon type based on message content, or Error if unable to determine.</returns>
    public static ErrorProviderIconType GetIconTypeFromMessage(string? errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            return ErrorProviderIconType.Error;
        }

        var message = errorMessage?.ToLowerInvariant();

        // Check for warning keywords
        if (message != null && (message.Contains("warning") || message.Contains("caution") || message.Contains("attention")))
        {
            return ErrorProviderIconType.Warning;
        }

        // Check for information keywords
        if (message != null && (message.Contains("info") || message.Contains("information") || message.Contains("note") || message.Contains("tip")))
        {
            return ErrorProviderIconType.Information;
        }

        // Default to error
        return ErrorProviderIconType.Error;
    }

    #endregion

    #region Border Color Management

    /// <summary>
    /// Sets the border color on a Krypton control based on the icon type.
    /// </summary>
    /// <param name="control">The control to modify.</param>
    /// <param name="iconType">The icon type determining the border color.</param>
    /// <returns>True if the border color was successfully set; otherwise, false.</returns>
    public static bool SetBorderColor(Control? control, ErrorProviderIconType iconType)
    {
        if (control == null)
        {
            return false;
        }

        Color borderColor = iconType switch
        {
            ErrorProviderIconType.Error => ErrorBorderColor,
            ErrorProviderIconType.Warning => WarningBorderColor,
            ErrorProviderIconType.Information => InformationBorderColor,
            _ => ErrorBorderColor
        };

        return SetBorderColor(control, borderColor);
    }

    /// <summary>
    /// Sets the border color on a Krypton control.
    /// </summary>
    /// <param name="control">The control to modify.</param>
    /// <param name="borderColor">The border color to apply.</param>
    /// <returns>True if the border color was successfully set; otherwise, false.</returns>
    public static bool SetBorderColor(Control? control, Color borderColor)
    {
        if (control == null)
        {
            return false;
        }

        // Try to access StateCommon.Border.Color1 using reflection
        var stateCommonProperty = control.GetType().GetProperty("StateCommon");
        if (stateCommonProperty == null)
        {
            return false;
        }

        var stateCommon = stateCommonProperty.GetValue(control);
        if (stateCommon == null)
        {
            return false;
        }

        // Try to get Border property
        var borderProperty = stateCommon.GetType().GetProperty("Border");
        if (borderProperty == null)
        {
            return false;
        }

        var border = borderProperty.GetValue(stateCommon);
        if (border == null)
        {
            return false;
        }

        // Try to set Color1 property
        var color1Property = border.GetType().GetProperty("Color1");
        if (color1Property != null && color1Property.CanWrite)
        {
            color1Property.SetValue(border, borderColor);
            control.Invalidate();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Clears the custom border color by resetting it to the default (empty color).
    /// </summary>
    /// <param name="control">The control to reset.</param>
    /// <returns>True if the border color was successfully cleared; otherwise, false.</returns>
    public static bool ClearBorderColor(Control control)
    {
        return SetBorderColor(control, Color.Empty);
    }

    #endregion
}