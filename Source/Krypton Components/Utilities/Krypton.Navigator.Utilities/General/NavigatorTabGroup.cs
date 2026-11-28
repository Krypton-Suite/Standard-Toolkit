#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Catalog entry for a browser-style caption tab group (Chrome/Edge-like).
/// </summary>
/// <remarks>
/// Page membership is stored on <see cref="KryptonPage.TabGroupId"/>; this type holds
/// display metadata (title, color, collapsed) for a given id.
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class NavigatorTabGroup
{
    private string _id = string.Empty;
    private string _title = string.Empty;
    private Color _color = Color.DodgerBlue;
    private bool _collapsed;

    /// <summary>
    /// Occurs when a group property that affects chrome has changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigatorTabGroup"/> class.
    /// </summary>
    public NavigatorTabGroup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigatorTabGroup"/> class.
    /// </summary>
    /// <param name="id">Stable group identifier referenced by <see cref="KryptonPage.TabGroupId"/>.</param>
    /// <param name="title">Display title for the group header chip.</param>
    /// <param name="color">Accent color for the group header and tab accents.</param>
    public NavigatorTabGroup(string id, string title, Color color)
    {
        _id = id ?? string.Empty;
        _title = title ?? string.Empty;
        _color = color.IsEmpty ? Color.DodgerBlue : color;
    }

    /// <summary>
    /// Gets or sets the stable group identifier.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Stable id referenced by KryptonPage.TabGroupId.")]
    [DefaultValue("")]
    public string Id
    {
        get => _id;
        set
        {
            value ??= string.Empty;
            if (_id == value)
            {
                return;
            }

            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    /// <summary>
    /// Gets or sets the display title shown on the group header chip.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Title shown on the caption group header.")]
    [DefaultValue("")]
    public string Title
    {
        get => _title;
        set
        {
            value ??= string.Empty;
            if (_title == value)
            {
                return;
            }

            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    /// <summary>
    /// Gets or sets the accent color for the group.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Accent color for the group header and member tab accents.")]
    [DefaultValue(typeof(Color), "DodgerBlue")]
    public Color Color
    {
        get => _color;
        set
        {
            if (_color == value)
            {
                return;
            }

            _color = value.IsEmpty ? Color.DodgerBlue : value;
            OnPropertyChanged(nameof(Color));
        }
    }

    /// <summary>
    /// Gets or sets whether member tabs are hidden (header-only).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, member tabs are hidden and only the group header is shown.")]
    [DefaultValue(false)]
    public bool Collapsed
    {
        get => _collapsed;
        set
        {
            if (_collapsed == value)
            {
                return;
            }

            _collapsed = value;
            OnPropertyChanged(nameof(Collapsed));
        }
    }

    /// <inheritdoc />
    public override string ToString() =>
        string.IsNullOrEmpty(Title) ? (string.IsNullOrEmpty(Id) ? nameof(NavigatorTabGroup) : Id) : Title;

    /// <summary>
    /// Creates a shallow copy of this group definition.
    /// </summary>
    public NavigatorTabGroup Clone() =>
        new NavigatorTabGroup(Id, Title, Color) { Collapsed = Collapsed };

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
