#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Implement storage for a knob common palette state.
/// </summary>
public class PaletteKnobRedirect : Storage
{
    #region Instance Fields
    private readonly PaletteDoubleRedirect _backRedirect;
    private readonly PaletteKnobTickColorRedirect _tickRedirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteKnobRedirect class.
    /// </summary>
    /// <param name="redirect">Inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteKnobRedirect([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect is not null);

        NeedPaint = needPaint;

        _backRedirect = new PaletteDoubleRedirect(redirect!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaint);
        Face = new PaletteElementColorRedirect(redirect!, PaletteElement.TrackBarTrack, NeedPaint);
        _tickRedirect = new PaletteKnobTickColorRedirect(redirect!, NeedPaint);
        Tick = _tickRedirect;
        Indicator = new PaletteElementColorRedirect(redirect!, PaletteElement.TrackBarPosition, NeedPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Back.IsDefault &&
                                      Face.IsDefault &&
                                      Tick.IsDefault &&
                                      Indicator.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect)
    {
        _backRedirect.SetRedirector(redirect);
        Face.SetRedirector(redirect);
        _tickRedirect.SetRedirector(redirect);
        Indicator.SetRedirector(redirect);
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
        Face.PopulateFromBase(state);
        Indicator.PopulateFromBase(state);
    }
    #endregion

    #region Face
    /// <summary>
    /// Gets access to the knob face appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining knob face appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorRedirect Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    #endregion

    #region Tick
    /// <summary>
    /// Gets access to the scale tick appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining scale tick and graduation label colour. Defaults to the palette control label text colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobTickColorRedirect Tick { get; }

    private bool ShouldSerializeTick() => !Tick.IsDefault;

    #endregion

    #region Indicator
    /// <summary>
    /// Gets access to the value indicator appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining value indicator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColorRedirect Indicator { get; }

    private bool ShouldSerializeIndicator() => !Indicator.IsDefault;

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
}
