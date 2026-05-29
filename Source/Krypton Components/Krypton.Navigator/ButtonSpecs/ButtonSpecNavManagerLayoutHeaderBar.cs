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

namespace Krypton.Navigator;

/// <summary>
/// Manage a collection of button specs for use with a ViewLayoutDocker style bar.
/// </summary>
internal class ButtonSpecNavManagerLayoutHeaderBar : ButtonSpecManagerLayout
{
    #region Instance Fields
    private IPaletteContent? _paletteContent;
    private PaletteState _paletteState;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecNavManagerLayoutHeaderBar class.
    /// </summary>
    /// <param name="control">Control that owns the button manager.</param>
    /// <param name="redirector">Palette redirector.</param>
    /// <param name="variableSpecs">Variable set of button specifications.</param>
    /// <param name="fixedSpecs">Fixed set of button specifications.</param>
    /// <param name="viewDockers">Array of target view dockers.</param>
    /// <param name="viewMetrics">Array of target metric providers.</param>
    /// <param name="viewMetricIntOutside">Array of target metrics for outside spacer size.</param>
    /// <param name="viewMetricIntInside">Array of target metrics for inside spacer size.</param>
    /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
    /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="paletteContent">Palette source for color remapping.</param>
    /// <param name="paletteState">Palette state for color remapping.</param>
    public ButtonSpecNavManagerLayoutHeaderBar(Control control,
        PaletteRedirect redirector,
        ButtonSpecCollectionBase? variableSpecs,
        ButtonSpecCollectionBase? fixedSpecs,
        ViewLayoutDocker[] viewDockers,
        IPaletteMetric[] viewMetrics,
        PaletteMetricInt[] viewMetricIntOutside,
        PaletteMetricInt[] viewMetricIntInside,
        PaletteMetricPadding[] viewMetricPaddings,
        GetToolStripRenderer getRenderer,
        NeedPaintHandler needPaint,
        IPaletteContent? paletteContent,
        PaletteState paletteState)
        : base(control, redirector, variableSpecs, fixedSpecs,
            viewDockers, viewMetrics, viewMetricIntOutside,
            viewMetricIntInside, viewMetricPaddings, getRenderer,
            needPaint)
    {
        // Remember initial palette settings needed for color remapping
        _paletteContent = paletteContent;
        _paletteState = paletteState;
    }
    #endregion

    #region Public
    /// <summary>
    /// Update cached remapping values and update active views.
    /// </summary>
    /// <param name="paletteContent">Palette used to recover remapping colors.</param>
    /// <param name="paletteState">Palette state to use for remapping.</param>
    public void UpdateRemapping(IPaletteContent? paletteContent,
        PaletteState paletteState)
    {
        // Cache new values
        _paletteContent = paletteContent;
        _paletteState = paletteState;

        // Update each remapping instance in turn
        foreach (ButtonSpecView view in ButtonSpecViews)
        {
            // Cast the remapping palette to the correct type
            var remapPalette = (ButtonSpecRemapByContentCache)view.RemapPalette;
            remapPalette.SetPaletteContent(_paletteContent);
            remapPalette.SetPaletteState(_paletteState);
        }
    }

    /// <summary>
    /// Create a palette redirector for remapping button spec colors.
    /// </summary>
    /// <param name="redirector">Base palette class.</param>
    /// <param name="buttonSpec">ButtonSpec instance.</param>
    /// <returns>Palette redirector for the button spec instance.</returns>
    public override PaletteRedirect CreateButtonSpecRemap(PaletteRedirect redirector,
        ButtonSpec buttonSpec) =>
        new ButtonSpecRemapByContentCache(redirector, buttonSpec);

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets a value indicating if inside edge spacers are required.
    /// </summary>
    protected override bool UseInsideSpacers => true;

    /// <summary>
    /// Perform final steps now that the button spec has been created.
    /// </summary>
    /// <param name="buttonSpec">ButtonSpec instance.</param>
    /// <param name="buttonView">Associated ButtonSpecView instance.</param>
    /// <param name="viewDockerIndex">Index of view docker button is placed onto.</param>
    protected override void ButtonSpecCreated(ButtonSpec buttonSpec,
        ButtonSpecView? buttonView,
        int viewDockerIndex)
    {
        // Cast the remapping palette to the correct type
        var remapPalette = (ButtonSpecRemapByContentCache)buttonView?.RemapPalette!;

        // Update button with the foreground used for color mapping
        remapPalette.SetPaletteContent(_paletteContent);
        remapPalette.SetPaletteState(_paletteState);
    }
    #endregion
}