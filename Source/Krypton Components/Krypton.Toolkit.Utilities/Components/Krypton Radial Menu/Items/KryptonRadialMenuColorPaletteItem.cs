#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Radial menu item that presents a colour palette ring when activated.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(ColorScheme))]
[DefaultEvent(nameof(SelectedColorChanged))]
public class KryptonRadialMenuColorPaletteItem : KryptonRadialMenuItemBase
{
    #region Instance Fields

    private string _text;
    private ColorScheme _colorScheme;
    private Color _selectedColor;
    private Color[] _colors;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when <see cref="SelectedColor"/> changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the SelectedColor property changes.")]
    public event EventHandler<ColorEventArgs>? SelectedColorChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuColorPaletteItem"/> class.
    /// </summary>
    public KryptonRadialMenuColorPaletteItem()
        : this(ColorScheme.Basic16)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuColorPaletteItem"/> class.
    /// </summary>
    /// <param name="scheme">Initial colour scheme.</param>
    public KryptonRadialMenuColorPaletteItem(ColorScheme scheme)
    {
        _text = @"Colors";
        _selectedColor = Color.Empty;
        _colorScheme = scheme;
        _colors = BuildSchemeColors(scheme);
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Text) ? "(Radial Color Palette)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the sector label text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the color palette sector.")]
    [DefaultValue(@"Colors")]
    [Localizable(true)]
    public string Text
    {
        get => _text;
        set
        {
            value ??= string.Empty;
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets or sets the colour scheme used to populate swatches.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colour scheme used for palette swatches.")]
    [DefaultValue(ColorScheme.Basic16)]
    public ColorScheme ColorScheme
    {
        get => _colorScheme;
        set
        {
            if (_colorScheme != value)
            {
                _colorScheme = value;
                _colors = BuildSchemeColors(value);
                OnPropertyChanged(nameof(ColorScheme));
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected colour.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Currently selected colour.")]
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
                SelectedColorChanged?.Invoke(this, new ColorEventArgs(value));
            }
        }
    }

    private bool ShouldSerializeSelectedColor() => !_selectedColor.IsEmpty;
    private void ResetSelectedColor() => SelectedColor = Color.Empty;

    /// <summary>
    /// Gets the colours shown in the editor ring.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color[] Colors => _colors;

    /// <summary>
    /// Gets or sets a custom colour list. Assigning replaces the scheme-generated colours.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Custom colour list. When set, replaces the scheme-generated colours.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color[]? CustomColors
    {
        get => _colors;
        set
        {
            _colors = value ?? Array.Empty<Color>();
            OnPropertyChanged(nameof(CustomColors));
        }
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool HasChildren => true;

    #endregion

    #region Implementation

    private static Color[] BuildSchemeColors(ColorScheme scheme)
    {
        // Flatten schemes that mirror KryptonContextMenuColorColumns defaults.
        switch (scheme)
        {
            case ColorScheme.None:
                return Array.Empty<Color>();
            case ColorScheme.Mono2:
                return [Color.White, Color.Black];
            case ColorScheme.Mono8:
                return
                [
                    Color.White,
                    Color.Silver,
                    Color.FromArgb(160, 160, 160),
                    Color.Gray,
                    Color.FromArgb(96, 96, 96),
                    Color.FromArgb(64, 64, 64),
                    Color.FromArgb(32, 32, 32),
                    Color.Black
                ];
            case ColorScheme.Basic16:
                return
                [
                    Color.White, Color.Black, Color.Silver, Color.Gray,
                    Color.Red, Color.Maroon, Color.Yellow, Color.Olive,
                    Color.Lime, Color.Green, Color.Cyan, Color.Teal,
                    Color.Blue, Color.Navy, Color.Fuchsia, Color.Purple
                ];
            default:
                return
                [
                    Color.White, Color.Black, Color.Silver, Color.Gray,
                    Color.Red, Color.Maroon, Color.Yellow, Color.Olive,
                    Color.Lime, Color.Green, Color.Cyan, Color.Teal,
                    Color.Blue, Color.Navy, Color.Fuchsia, Color.Purple,
                    Color.Orange, Color.FromArgb(255, 192, 128),
                    Color.FromArgb(192, 0, 0), Color.FromArgb(0, 0, 192)
                ];
        }
    }

    #endregion
}
