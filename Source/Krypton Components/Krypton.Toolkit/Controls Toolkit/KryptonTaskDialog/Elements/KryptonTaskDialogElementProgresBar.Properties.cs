#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Used internally by the KryptonTaskDialogElementProgresBar element 
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class KryptonTaskDialogElementProgresBarProperties
{
    #region Fields
    KryptonTaskDialogElementProgresBar _progressBarElement;
    private KryptonProgressBar _progressBar;
    private Action<bool> _onSizeChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="progressBar">KryptonProgresBar instance.</param>
    /// <param name="onSizeChanged">KryptonTaskDialogElementProgresBar.OnSizeChanged callback delegate.</param>
    /// <param name="progressBarElement">Valid instance of the parent class</param>
    public KryptonTaskDialogElementProgresBarProperties(KryptonProgressBar progressBar, 
        Action<bool> onSizeChanged, KryptonTaskDialogElementProgresBar progressBarElement)
    {
        _progressBar = progressBar;
        _onSizeChanged = onSizeChanged;
        _progressBarElement = progressBarElement;
    }
    #endregion

    #region Public
    /// <summary>
    /// Maximum progress value.
    /// </summary>
    public int Maximum
    {
        get => _progressBar.Maximum;
        set => _progressBar.Maximum = value;
    }

    /// <summary>
    /// Minimum progress value.
    /// </summary>
    public int Minimum
    {
        get => _progressBar.Minimum;
        set => _progressBar.Minimum = value;
    }

    /// <summary>
    /// Current progress value.
    /// </summary>
    public int Value
    {
        get => _progressBar.Value;
        set => _progressBar.Value = value;
    }

    /// <summary>
    /// ProgressBar text.
    /// </summary>
    public string Text
    {
        get => _progressBar.Text;
        set => _progressBar.Text = value;
    }

    /// <summary>
    /// The ProgressBar.Value will be shown as text.
    /// </summary>
    public bool UseValueAsText
    {
        get => _progressBar.UseValueAsText;
        set => _progressBar.UseValueAsText = value;
    }

    /// <summary>
    /// ProgressBar progress display style.
    /// </summary>
    public ProgressBarStyle Style
    {
        get => _progressBar.Style;
        set => _progressBar.Style = value;
    }

    /// <summary>
    /// "Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.
    /// </summary>
    public int MarqueeAnimationSpeed
    {
        get => _progressBar.MarqueeAnimationSpeed;
        set => _progressBar.MarqueeAnimationSpeed = value;
    }

    /// <summary>
    /// Number of blocks when using Blocks style; 0 for automatic.
    /// </summary>
    public int BlockCount
    {
        get => _progressBar.BlockCount;
        set => _progressBar.BlockCount = value;
    }

    /// <summary>
    /// Width of the progressbar.
    /// </summary>
    public int Width
    {
        get => _progressBar.Width;
        set => _progressBar.Width = value;
    }

    /// <summary>
    /// Height of the progressbar.
    /// </summary>
    public int Height
    {
        get => _progressBar.Height;

        set
        {
            if (_progressBar.Height != value)
            {
                _progressBar.Height = value;

                // Flag dirty and request an update
                _progressBarElement.LayoutDirty = true;
                _onSizeChanged(false);
            }
        }
    }
    #endregion

    #region public override
    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public sealed override string ToString()
    {
        return string.Empty;
    }
    #endregion
}