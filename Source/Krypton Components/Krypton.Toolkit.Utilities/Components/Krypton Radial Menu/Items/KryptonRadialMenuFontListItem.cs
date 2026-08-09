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
/// Radial menu item that presents a scrollable font list ring when activated.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(SelectedFont))]
[DefaultEvent(nameof(SelectedFontChanged))]
public class KryptonRadialMenuFontListItem : KryptonRadialMenuItemBase
{
    #region Instance Fields

    private string _text;
    private Font? _selectedFont;
    private string[] _fontFamilies;
    private int _scrollOffset;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when <see cref="SelectedFont"/> changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the SelectedFont property changes.")]
    public event EventHandler? SelectedFontChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuFontListItem"/> class.
    /// </summary>
    public KryptonRadialMenuFontListItem()
    {
        _text = @"Font";
        _fontFamilies = BuildInstalledFamilies();
        _scrollOffset = 0;
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Text) ? "(Radial Font List)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the sector label text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the font list sector.")]
    [DefaultValue(@"Font")]
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
    /// Gets or sets the selected font.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Currently selected font.")]
    [DefaultValue(null)]
    public Font? SelectedFont
    {
        get => _selectedFont;
        set
        {
            if (!ReferenceEquals(_selectedFont, value))
            {
                _selectedFont = value;
                OnPropertyChanged(nameof(SelectedFont));
                SelectedFontChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets the font family names available in the editor ring.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] FontFamilies => _fontFamilies;

    /// <summary>
    /// Gets or sets a custom list of font family names.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Custom font family names. When set, replaces the installed-font list.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] CustomFontFamilies
    {
        get => _fontFamilies;
        set
        {
            _fontFamilies = value ?? Array.Empty<string>();
            _scrollOffset = 0;
            OnPropertyChanged(nameof(CustomFontFamilies));
        }
    }

    /// <summary>
    /// Gets or sets the scroll offset into the font list for the visible ring.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ScrollOffset
    {
        get => _scrollOffset;
        set
        {
            if (_fontFamilies.Length == 0)
            {
                _scrollOffset = 0;
                return;
            }

            var next = value % _fontFamilies.Length;
            if (next < 0)
            {
                next += _fontFamilies.Length;
            }

            if (_scrollOffset != next)
            {
                _scrollOffset = next;
                OnPropertyChanged(nameof(ScrollOffset));
            }
        }
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool HasChildren => true;

    /// <summary>
    /// Selects a font family by name.
    /// </summary>
    /// <param name="familyName">Font family name.</param>
    public void SelectFamily(string familyName)
    {
        if (string.IsNullOrEmpty(familyName))
        {
            return;
        }

        try
        {
            SelectedFont = new Font(familyName, 9f);
        }
        catch
        {
            // Ignore fonts that cannot be created on this system.
        }
    }

    #endregion

    #region Implementation

    private static string[] BuildInstalledFamilies()
    {
        try
        {
            return FontFamily.Families
                .Select(static f => f.Name)
                .OrderBy(static n => n, StringComparer.CurrentCultureIgnoreCase)
                .Take(64)
                .ToArray();
        }
        catch
        {
            return ["Arial", "Calibri", "Consolas", "Courier New", "Segoe UI", "Tahoma", "Times New Roman", "Verdana"];
        }
    }

    #endregion
}
