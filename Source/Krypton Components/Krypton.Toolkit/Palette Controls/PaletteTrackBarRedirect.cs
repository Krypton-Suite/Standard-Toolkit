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
/// Implement storage for a track bar state.
/// </summary>
public class PaletteTrackBarRedirect : Storage
{
    #region Instance Fields
    private readonly PaletteDoubleRedirect _backRedirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteTrackBarRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTrackBarRedirect([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(redirect is not null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        _backRedirect = new PaletteDoubleRedirect(redirect!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaint);
        Tick = new PaletteElementColorRedirect(redirect!, PaletteElement.TrackBarTick, NeedPaint);
        Track = new PaletteElementColorRedirect(redirect!, PaletteElement.TrackBarTrack, NeedPaint);
        Position = new PaletteElementColorRedirect(redirect!, PaletteElement.TrackBarPosition, NeedPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Back.IsDefault &&
                                      Tick.IsDefault &&
                                      Track.IsDefault &&
                                      Position.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect)
    {
        _backRedirect.SetRedirector(redirect);
        Tick.SetRedirector(redirect);
        Track.SetRedirector(redirect);
        Position.SetRedirector(redirect);
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        _backRedirect.PopulateFromBase(state);
        Tick.PopulateFromBase(state);
        Track.PopulateFromBase(state);
        Position.PopulateFromBase(state);
    }
    #endregion

    #region Tick
    /// <summary>
    /// Gets access to the tick appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tick appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorRedirect Tick { get; }

    private bool ShouldSerializeTick() => !Tick.IsDefault;

    #endregion

    #region Track
    /// <summary>
    /// Gets access to the track appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining track appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorRedirect Track { get; }

    private bool ShouldSerializeTrack() => !Track.IsDefault;

    #endregion

    #region Position
    /// <summary>
    /// Gets access to the position marker appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining position marker appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorRedirect Position { get; }

    private bool ShouldSerializePosition() => !Position.IsDefault;

    #endregion

    #region Internal
    /// <summary>
    /// Gets access to the background appearance.
    /// </summary>
    internal PaletteBack Back => _backRedirect.Back;

    internal PaletteBackStyle BackStyle
    {
        get => _backRedirect.BackStyle;
        set => _backRedirect.BackStyle = value;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Handle a change event from palette source.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="needLayout">True if a layout is also needed.</param>
    protected void OnNeedPaint(object? sender, bool needLayout) =>
        // Pass request from child to our own handler
        PerformNeedPaint(needLayout);

    #endregion
}