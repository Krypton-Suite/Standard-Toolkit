#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a context menu progress bar item.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonProgressBar))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Value))]
public class KryptonContextMenuProgressBar : KryptonContextMenuItemBase
{
    #region Instance Fields
    private int _minimumWidth;
    private readonly KryptonProgressBar _progressBar;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuProgressBar class.
    /// </summary>
    public KryptonContextMenuProgressBar()
    {
        _minimumWidth = 120;

        _progressBar = new KryptonProgressBar
        {
            Size = new Size(100, 22)
        };
    }

    /// <summary>
    /// Returns a description of the instance.
    /// </summary>
    /// <returns>String representation.</returns>
    public override string ToString() => $@"{Value} / {Maximum}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _progressBar.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public
    /// <summary>
    /// Returns the number of child menu items.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemChildCount => 0;

    /// <summary>
    /// Returns the indexed child menu item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override KryptonContextMenuItemBase? this[int index] => null;

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public override bool ProcessShortcut(Keys keyData) => false;

    /// <summary>
    /// Returns a view appropriate for this item based on the object it is inside.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="parent">Owning object reference.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
    public override ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn)
    {
        SetProvider(provider);
        return new ViewDrawMenuProgressBar(provider, this);
    }

    /// <summary>
    /// Gets or sets the lower bound of the range.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Lower bound of the range.")]
    [DefaultValue(0)]
    public int Minimum
    {
        get => _progressBar.Minimum;

        set
        {
            if (_progressBar.Minimum != value)
            {
                _progressBar.Minimum = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Minimum)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the upper bound of the range.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Upper bound of the range.")]
    [DefaultValue(100)]
    public int Maximum
    {
        get => _progressBar.Maximum;

        set
        {
            if (_progressBar.Maximum != value)
            {
                _progressBar.Maximum = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Maximum)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the current position.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Current position within the range.")]
    [DefaultValue(0)]
    public int Value
    {
        get => _progressBar.Value;

        set
        {
            if (_progressBar.Value != value)
            {
                _progressBar.Value = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the amount by which a call to <see cref="KryptonProgressBar.PerformStep"/> increments the position.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Step increment used by PerformStep.")]
    [DefaultValue(10)]
    public int Step
    {
        get => _progressBar.Step;

        set
        {
            if (_progressBar.Step != value)
            {
                _progressBar.Step = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Step)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the progress bar style.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Progress bar display style.")]
    [DefaultValue(ProgressBarStyle.Continuous)]
    public ProgressBarStyle Style
    {
        get => _progressBar.Style;

        set
        {
            if (_progressBar.Style != value)
            {
                _progressBar.Style = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Style)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the marquee animation speed in milliseconds.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Time period in milliseconds for marquee animation.")]
    [DefaultValue(100)]
    public int MarqueeAnimationSpeed
    {
        get => _progressBar.MarqueeAnimationSpeed;

        set
        {
            if (_progressBar.MarqueeAnimationSpeed != value)
            {
                _progressBar.MarqueeAnimationSpeed = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MarqueeAnimationSpeed)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the minimum display width of the progress bar.
    /// </summary>
    [KryptonPersist]
    [Category(@"Layout")]
    [Description(@"Minimum display width of the progress bar in pixels.")]
    [DefaultValue(120)]
    public int MinimumWidth
    {
        get => _minimumWidth;

        set
        {
            if (_minimumWidth != value)
            {
                _minimumWidth = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MinimumWidth)));
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating whether the progress bar is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the progress bar is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _progressBar.Enabled;

        set
        {
            if (_progressBar.Enabled != value)
            {
                _progressBar.Enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    #endregion

    #region Internal
    /// <summary>
    /// Gets the underlying KryptonProgressBar; used by the view to host the control directly.
    /// </summary>
    internal KryptonProgressBar ProgressBar => _progressBar;

    #endregion
}
