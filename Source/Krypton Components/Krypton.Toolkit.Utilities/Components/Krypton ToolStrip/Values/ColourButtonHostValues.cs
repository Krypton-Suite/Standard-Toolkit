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
/// Expandable configuration for the <see cref="KryptonColorButtonToolStripMenuItem"/> tool strip host. Mirrors the
/// settings of the hosted <see cref="KryptonColorButton"/> (<see cref="KryptonColorButtonToolStripMenuItem.KryptonColourButtonControl"/>).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ColourButtonHostValues : Storage
{
    #region Instance Fields

    private readonly KryptonColorButtonToolStripMenuItem _owner;
    private Color _emptyBorderColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ColourButtonHostValues"/> class.
    /// </summary>
    /// <param name="owner">Owning colour button host.</param>
    public ColourButtonHostValues(KryptonColorButtonToolStripMenuItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        (_owner.KryptonColourButtonControl is null || _owner.KryptonColourButtonControl.SelectedColor == Color.Empty) &&
        (_owner.KryptonColourButtonControl is null || string.IsNullOrEmpty(_owner.KryptonColourButtonControl.Text)) &&
        (_owner.KryptonColourButtonControl is null || _owner.KryptonColourButtonControl.SelectedRect == new Rectangle(0, 12, 16, 4));

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the selected colour.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color SelectedColor
    {
        get => _owner.KryptonColourButtonControl!.SelectedColor;
        set
        {
            if (value != _owner.KryptonColourButtonControl!.SelectedColor)
            {
                _owner.KryptonColourButtonControl.SelectedColor = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the border colour used when no colour is selected.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "DarkGray")]
    public Color EmptyBorderColor
    {
        get => _owner.KryptonColourButtonControl!.EmptyBorderColor;
        set
        {
            if (value != _emptyBorderColor)
            {
                _owner.KryptonColourButtonControl!.EmptyBorderColor = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the button text.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue("")]
    public string Text
    {
        get => _owner.KryptonColourButtonControl!.Text;
        set => _owner.KryptonColourButtonControl!.Text = value;
    }

    /// <summary>
    /// Gets or sets the rectangle used to display the current colour selection swatch.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Rectangle), "0,12,16,4")]
    public Rectangle SelectedRect
    {
        get => _owner.KryptonColourButtonControl!.SelectedRect;
        set => _owner.KryptonColourButtonControl!.SelectedRect = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        if (_owner.KryptonColourButtonControl is { } button)
        {
            button.SelectedColor = Color.Empty;
            button.EmptyBorderColor = Color.DarkGray;
            button.Text = string.Empty;
            button.SelectedRect = new Rectangle(0, 12, 16, 4);
        }

        _emptyBorderColor = Color.Empty;
    }

    #endregion
}
