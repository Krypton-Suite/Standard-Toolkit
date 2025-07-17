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

/// <summary>
/// Provides an identifiable area for containing other controls.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultEvent(nameof(Paint))]
[DefaultProperty("PanelStyle")]
[Designer(typeof(KryptonPanelDesigner))]
[DesignerCategory(@"code")]
[Description(@"Enables you to group collections of controls.")]
[Docking(DockingBehavior.Ask)]
public class KryptonPanel : VisualPanel
{
    #region Instance Fields

    private readonly PaletteDoubleRedirect _stateCommon;
    private readonly PaletteDouble? _stateDisabled;
    private readonly PaletteDouble? _stateNormal;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPanel class.
    /// </summary>
    public KryptonPanel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(Redirector!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
        _stateDisabled = new PaletteDouble(_stateCommon, NeedPaintDelegate);
        _stateNormal = new PaletteDouble(_stateCommon, NeedPaintDelegate);

        Construct();
    }

    /// <summary>
    /// Initialize a new instance of the KryptonPanel class.
    /// </summary>
    /// <param name="stateCommon">Common appearance state to inherit from.</param>
    /// <param name="stateDisabled">Disabled appearance state.</param>
    /// <param name="stateNormal">Normal appearance state.</param>
    public KryptonPanel(PaletteDoubleRedirect stateCommon,
        PaletteDouble stateDisabled,
        PaletteDouble stateNormal)
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(stateCommon is not null);
        Debug.Assert(stateDisabled is not null);
        Debug.Assert(stateNormal is not null);

        // Remember the palette storage
        _stateCommon = stateCommon!;
        _stateDisabled = stateDisabled;
        _stateNormal = stateNormal;

        Construct();
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the panel style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Panel style.")]
    public PaletteBackStyle PanelBackStyle
    {
        get => _stateCommon.BackStyle;

        set
        {
            if (_stateCommon.BackStyle != value)
            {
                _stateCommon.BackStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePanelBackStyle() => PanelBackStyle != PaletteBackStyle.PanelClient;

    private void ResetPanelBackStyle() => PanelBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets access to the common panel appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common panel appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon.Back;

    private bool ShouldSerializeStateCommon() => !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled!.Back;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled!.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal!.Back;

    private bool ShouldSerializeStateNormal() => !_stateNormal!.Back.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) =>
        // Request fixed state from the view
        ViewDrawPanel.FixedState = state;
    #endregion

    #region Protected
    /// <summary>
    /// Gets access to the view element used to draw the KryptonPanel.
    /// </summary>
    protected ViewDrawPanel ViewDrawPanel { get; private set; }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        ViewDrawPanel.SetPalettes(Enabled ? _stateNormal!.Back : _stateDisabled!.Back);

        // Update with latest enabled state
        ViewDrawPanel.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }
    #endregion

    #region Implementation
    private void Construct()
    {
        // Our view contains just a simple canvas that covers entire client area
        ViewDrawPanel = new ViewDrawPanel(_stateNormal!.Back);

        // Create the view manager instance
        ViewManager = new ViewManager(this, ViewDrawPanel);
    }
    #endregion
}