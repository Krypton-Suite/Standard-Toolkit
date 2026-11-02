#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public class ValueChangedEventArgs : EventArgs
{
    #region Instance Fields

    private int _progressValue;

    private Color _progressColor;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the color of the progress.
    /// </summary>
    /// <value>
    /// The color of the progress.
    /// </value>
    [Description("Gets or sets the color of the progress.")]
    [DefaultValue(typeof(Color), "Green")]
    public Color ProgressColor { get => _progressColor; set => _progressColor = value; }

    /// <summary>
    /// Gets or sets the progress value.
    /// </summary>
    /// <value>
    /// The progress value.
    /// </value>
    [Description("Gets or sets the progress value.")]
    [DefaultValue(0)]
    public int ProgressValue { get => _progressValue; set => _progressValue = value; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="progressValue">The progress value.</param>
    public ValueChangedEventArgs(int progressValue) => ProgressValue = progressValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="progressColor">Color of the progress.</param>
    /// <param name="progressValue">The progress value.</param>
    public ValueChangedEventArgs(Color progressColor, int progressValue)
    {
        ProgressColor = progressColor;

        ProgressValue = progressValue;
    }

    #endregion
}