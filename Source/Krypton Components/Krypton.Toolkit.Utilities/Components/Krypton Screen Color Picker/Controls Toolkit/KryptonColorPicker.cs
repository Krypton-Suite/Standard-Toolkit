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
/// Designer-sited screen colour picker. Drop from the toolbox onto a form, set flyout and format
/// properties, then call <see cref="ShowDialog()"/> or <see cref="TryPick(out Color)"/> to sample.
/// </summary>
/// <remarks>
/// The static <see cref="KryptonScreenColorPicker"/> helper remains available for code-first use.
/// This component stores per-instance flyout, magnifier, zoom, and format settings.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonColorDialog), "ToolboxBitmaps.KryptonColorDialog.bmp")]
[DesignerCategory(@"code")]
[DefaultEvent(nameof(ColorChanged))]
[DefaultProperty(nameof(Color))]
[Description(@"PowerToys-style screen colour picker. Call ShowDialog to sample a colour from the desktop.")]
public class KryptonColorPicker : Component
{
    private Color _color = Color.Empty;
    private KryptonScreenColorPickerFlyoutStyle _flyoutStyle = KryptonScreenColorPickerFlyoutStyle.Krypton;
    private int _magnifierSize = KryptonScreenColorPicker.DefaultMagnifierSize;
    private int _zoom = KryptonScreenColorPicker.DefaultZoom;
    private KryptonScreenColorPickerColorFormat _visibleColorFormats = ScreenColorPickerColorFormatter.DefaultFormats;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonColorPicker"/> class.
    /// </summary>
    public KryptonColorPicker()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonColorPicker"/> class.
    /// </summary>
    /// <param name="container">The container that owns this component.</param>
    public KryptonColorPicker(IContainer container)
        : this()
    {
        ThrowHelper.ThrowIfNull(container);
        container.Add(this);
    }

    /// <summary>
    /// Gets the localisable overlay, flyout, and format strings.
    /// Shared by all screen colour pickers in the application.
    /// </summary>
    [Category(@"Localization")]
    [Description(@"Localisable overlay, flyout, and format strings. Shared by all screen colour pickers.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    [MergableProperty(false)]
    public KryptonScreenColorPickerStrings Strings => KryptonScreenColorPicker.Strings;

    /// <summary>
    /// Occurs when <see cref="Color"/> changes, including after a successful pick.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the sampled colour changes.")]
    public event EventHandler? ColorChanged;

    /// <summary>
    /// Last colour sampled by <see cref="ShowDialog()"/>, or <see cref="Color.Empty"/> when none.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Last colour sampled from the screen.")]
    public Color Color
    {
        get => _color;
        set
        {
            if (_color == value)
            {
                return;
            }

            _color = value;
            OnColorChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Flyout chrome used when picking.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(KryptonScreenColorPickerFlyoutStyle.Krypton)]
    [Description(@"Classic painted flyout or themed Krypton flyout.")]
    public KryptonScreenColorPickerFlyoutStyle FlyoutStyle
    {
        get => _flyoutStyle;
        set => _flyoutStyle = value;
    }

    /// <summary>
    /// Odd number of source pixels shown in the magnifier (7–21).
    /// Updated to the last size used when a pick session ends.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(11)]
    [Description(@"Odd number of source pixels shown in the magnifier (7–21).")]
    public int MagnifierSize
    {
        get => _magnifierSize;
        set => _magnifierSize = KryptonScreenColorPicker.ClampMagnifierSize(value);
    }

    /// <summary>
    /// Pixel zoom used when a pick starts (6–24).
    /// Updated to the last zoom used when a pick session ends.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(12)]
    [Description(@"Pixel zoom used when a pick starts (6–24).")]
    public int Zoom
    {
        get => _zoom;
        set => _zoom = KryptonScreenColorPicker.ClampZoom(value);
    }

    /// <summary>
    /// Colour formats shown on the magnifier flyout.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Colour formats shown on the magnifier flyout.")]
    public KryptonScreenColorPickerColorFormat VisibleColorFormats
    {
        get => _visibleColorFormats;
        set => _visibleColorFormats = ScreenColorPickerColorFormatter.Normalize(value);
    }

    /// <summary>
    /// Captures a colour from the screen using this component's settings.
    /// </summary>
    /// <returns><see cref="DialogResult.OK"/> when a colour was picked; otherwise <see cref="DialogResult.Cancel"/>.</returns>
    public DialogResult ShowDialog() => ShowDialog(null);

    /// <summary>
    /// Captures a colour from the screen using this component's settings.
    /// Makes <paramref name="owner"/> fully transparent while picking when it is a visible form.
    /// </summary>
    /// <param name="owner">Owner window. May be null.</param>
    /// <returns><see cref="DialogResult.OK"/> when a colour was picked; otherwise <see cref="DialogResult.Cancel"/>.</returns>
    public DialogResult ShowDialog(IWin32Window? owner) =>
        TryPick(owner, out _) ? DialogResult.OK : DialogResult.Cancel;

    /// <summary>
    /// Captures a colour from the screen using this component's settings.
    /// </summary>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public bool TryPick(out Color color) => TryPick(null, out color);

    /// <summary>
    /// Captures a colour from the screen using this component's settings.
    /// Makes <paramref name="owner"/> fully transparent while picking when it is a visible form.
    /// </summary>
    /// <param name="owner">Owner window. May be null.</param>
    /// <param name="color">The sampled colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a colour was picked.</returns>
    public bool TryPick(IWin32Window? owner, out Color color)
    {
        bool picked = KryptonScreenColorPicker.TryPick(owner, FlyoutStyle, MagnifierSize, Zoom, VisibleColorFormats,
            out color);
        MagnifierSize = KryptonScreenColorPicker.DefaultMagnifierSize;
        Zoom = KryptonScreenColorPicker.DefaultZoom;
        if (picked)
        {
            Color = color;
        }

        return picked;
    }

    /// <summary>
    /// Populates <paramref name="list"/> with every colour format and checks those in
    /// <see cref="VisibleColorFormats"/>. Check changes update this component's formats.
    /// </summary>
    /// <param name="list">Checked list used as a format picker. Cannot be null.</param>
    public void BindColorFormatList(KryptonCheckedListBox list) =>
        ScreenColorPickerColorFormatter.BindCheckedList(list, VisibleColorFormats, ColorFormatList_ItemCheck);

    /// <summary>
    /// Raises the <see cref="ColorChanged"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnColorChanged(EventArgs e) => ColorChanged?.Invoke(this, e);

    private bool ShouldSerializeColor() => !_color.IsEmpty;

    private void ResetColor() => Color = Color.Empty;

    private bool ShouldSerializeStrings() => !Strings.IsDefault;

    private void ResetStrings() => Strings.Reset();

    private bool ShouldSerializeVisibleColorFormats() =>
        _visibleColorFormats != ScreenColorPickerColorFormatter.DefaultFormats;

    private void ResetVisibleColorFormats() =>
        VisibleColorFormats = ScreenColorPickerColorFormatter.DefaultFormats;

    private void ColorFormatList_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        if (!(sender is KryptonCheckedListBox list))
        {
            return;
        }

        if (!ScreenColorPickerColorFormatter.TryReadCheckedFlags(list, e, out KryptonScreenColorPickerColorFormat flags))
        {
            e.NewValue = CheckState.Checked;
            return;
        }

        VisibleColorFormats = flags;
    }
}
