#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Implements a timer that raises an event at user-defined intervals with Krypton integration.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(Timer))]
[DefaultEvent(nameof(Tick))]
[DefaultProperty(nameof(Interval))]
[DesignerCategory(@"code")]
[Description(@"Implements a timer that raises an event at user-defined intervals.")]
public class KryptonTimer : Component
{
    #region Instance Fields
    
    private Timer? _internalTimer;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    
    #endregion

    #region Events
    
    /// <summary>
    /// Occurs when the specified timer interval has elapsed and the timer is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the specified timer interval has elapsed and the timer is enabled.")]
    public event EventHandler? Tick;

    /// <summary>
    /// Raises the Tick event.
    /// </summary>
    protected virtual void OnTick(EventArgs e) => Tick?.Invoke(this, e);

    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimer"/>> class.
    /// </summary>
    public KryptonTimer()
    {
        _internalTimer = new Timer();
        _internalTimer.Tick += OnTimerTick;

        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimer"/>> class with a container.
    /// </summary>
    /// <param name="container">The IContainer that will contain this timer.</param>
    public KryptonTimer(IContainer container)
        : this()
    {
        container.Add(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            if (_internalTimer != null)
            {
                _internalTimer.Tick -= OnTimerTick;
                _internalTimer.Dispose();
                _internalTimer = null;
            }
        }

        base.Dispose(disposing);
    }
    
    #endregion

    #region Public
    
    /// <summary>
    /// Gets or sets a value indicating whether the timer is running.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the timer is running.")]
    [DefaultValue(false)]
    public bool Enabled
    {
        get => _internalTimer?.Enabled ?? false;
        set => _internalTimer?.Enabled = value;
    }

    /// <summary>
    /// Gets or sets the time, in milliseconds, between timer ticks.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The time, in milliseconds, between timer ticks.")]
    [DefaultValue(100)]
    public int Interval
    {
        get => _internalTimer?.Interval ?? 100;
        set => _internalTimer?.Interval = value;
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;
        set
        {
            if (_paletteMode != value)
            {
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;
        set
        {
            if (_palette != value)
            {
                _palette = value;

                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    _paletteMode = PaletteMode.Custom;
                }
            }
        }
    }

    /// <summary>
    /// Gets access to the underlying Timer component.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Timer? Timer => _internalTimer;

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void Start() => _internalTimer?.Start();

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void Stop() => _internalTimer?.Stop();
    
    #endregion

    #region Implementation

    private void OnTimerTick(object? sender, EventArgs e)
    {
        OnTick(e);
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
        }
    }
    
    #endregion
}
