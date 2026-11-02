#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Horizontal top-level menu bar item. Paints like a low-profile button using the
/// owning bar palettes and the <see cref="KryptonContextMenuItem"/> text/image.
/// </summary>
internal sealed class ViewDrawMenuBarItem : ViewDrawButton
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawMenuBarItem"/> class.
    /// </summary>
    /// <param name="item">Top-level menu item definition.</param>
    /// <param name="paletteDisabled">Disabled palette.</param>
    /// <param name="paletteNormal">Normal palette.</param>
    /// <param name="paletteTracking">Tracking palette.</param>
    /// <param name="palettePressed">Pressed palette.</param>
    /// <param name="paletteMetric">Metric palette.</param>
    /// <param name="useMnemonic">Whether to underline and match mnemonics.</param>
    public ViewDrawMenuBarItem(KryptonContextMenuItem item,
        IPaletteTriple paletteDisabled,
        IPaletteTriple paletteNormal,
        IPaletteTriple paletteTracking,
        IPaletteTriple palettePressed,
        IPaletteMetric? paletteMetric,
        bool useMnemonic)
        : base(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
            paletteMetric, new MenuBarItemContentValues(item), VisualOrientation.Top, useMnemonic)
    {
        Item = item;
        Enabled = item.Enabled;
        Visible = item.Visible;
        UseMnemonic = useMnemonic;
        TestForFocusCues = false;
        DropDown = false;
        Splitter = false;
    }

    /// <inheritdoc />
    public override string ToString() => $"ViewDrawMenuBarItem:{Item.Text}";

    #endregion

    #region Public

    /// <summary>
    /// Gets the menu item this view represents.
    /// </summary>
    public KryptonContextMenuItem Item { get; }

    /// <summary>
    /// Gets the button controller assigned by the owning menu bar, if any.
    /// </summary>
    public MenuBarItemController? MenuBarController => MouseController as MenuBarItemController;

    #endregion

    #region Nested Classes

    private sealed class MenuBarItemContentValues : IContentValues
    {
        private readonly KryptonContextMenuItem _item;

        public MenuBarItemContentValues(KryptonContextMenuItem item) => _item = item;

        public string GetShortText() => _item.Text;

        public string GetLongText() => string.Empty;

        public Image? GetImage(PaletteState state) => _item.Image;

        public Color GetImageTransparentColor(PaletteState state) => _item.ImageTransparentColor;

        public Image? GetOverlayImage(PaletteState state) => null;

        public Color GetOverlayImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

        public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

        public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

        public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

        public Size GetOverlayImageFixedSize(PaletteState state) => Size.Empty;
    }

    #endregion
}
