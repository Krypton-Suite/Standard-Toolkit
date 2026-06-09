#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draw element for a context menu scroll overflow button.
/// </summary>
internal class ViewDrawMenuScrollButton : ViewDrawCanvas
{
    #region Instance Fields
    private readonly IContextMenuProvider _provider;
    private readonly PaletteContent _contentPalette;
    private readonly ViewDrawContent _drawContent;
    private readonly bool _scrollUp;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuScrollButton class.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="scrollUp">True for scroll up; otherwise scroll down.</param>
    public ViewDrawMenuScrollButton(IContextMenuProvider provider, bool scrollUp)
        : base(provider.ProviderStateNormal.ItemHighlight.Back,
            provider.ProviderStateNormal.ItemHighlight.Border,
            provider.ProviderStateNormal.ItemHighlight,
            PaletteMetricPadding.ContextMenuItemHighlight,
            VisualOrientation.Top)
    {
        _provider = provider;
        _scrollUp = scrollUp;

        var text = KryptonManager.Strings.ContextMenuStrings.GetOverflowScrollText(scrollUp,
            provider.ProviderOverflowScrollUseArrows);

        _contentPalette = new PaletteContent(provider.ProviderStateNormal.ItemTextStandard);
        _contentPalette.ShortText.TextH = PaletteRelativeAlign.Center;

        _drawContent = new ViewDrawContent(_contentPalette,
            new FixedContentValue(text, string.Empty, null, GlobalStaticValues.EMPTY_COLOR),
            VisualOrientation.Top);

        var docker = new ViewLayoutDocker();
        docker.Add(_drawContent, ViewDockStyle.Fill);
        Add(docker);

        var controller = new MenuScrollButtonController(provider.ProviderViewManager, this,
            provider.ProviderNeedPaintDelegate);
        KeyController = controller;
        MouseController = controller;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        $"ViewDrawMenuScrollButton:{Id}";

    #endregion

    #region ScrollUp
    /// <summary>
    /// Gets a value indicating if this is the scroll up button.
    /// </summary>
    public bool ScrollUp => _scrollUp;

    #endregion

    #region OverflowColumn
    /// <summary>
    /// Gets and sets the overflow column that owns this scroll button.
    /// </summary>
    public ViewLayoutContextMenuOverflowColumn? OverflowColumn { get; set; }

    #endregion

    #region ItemEnabled
    /// <summary>
    /// Gets the enabled state of the scroll button.
    /// </summary>
    public bool ItemEnabled => _provider.ProviderEnabled;

    #endregion

    #region HighlightState
    /// <summary>
    /// Update view to reflect the highlighted state.
    /// </summary>
    public void HighlightState()
    {
        if (ItemEnabled)
        {
            ElementState = PaletteState.Tracking;
            SetPalettes(_provider.ProviderStateHighlight.ItemHighlight.Back,
                _provider.ProviderStateHighlight.ItemHighlight.Border);
        }
        else
        {
            ElementState = PaletteState.Disabled;
            SetPalettes(_provider.ProviderStateDisabled.ItemHighlight.Back,
                _provider.ProviderStateDisabled.ItemHighlight.Border);
            _contentPalette.SetInherit(_provider.ProviderStateDisabled.ItemTextStandard);
        }
    }

    /// <summary>
    /// Update view to reflect the normal state.
    /// </summary>
    public void NormalState()
    {
        ElementState = PaletteState.Normal;
        SetPalettes(_provider.ProviderStateNormal.ItemHighlight.Back,
            _provider.ProviderStateNormal.ItemHighlight.Border);
        _contentPalette.SetInherit(_provider.ProviderStateNormal.ItemTextStandard);
    }

    #endregion
}
