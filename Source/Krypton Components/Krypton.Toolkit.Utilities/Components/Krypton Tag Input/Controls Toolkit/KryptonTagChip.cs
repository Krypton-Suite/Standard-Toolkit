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
/// Compact themed chip used by <see cref="KryptonTagInputControl"/> to display one tag.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
internal sealed class KryptonTagChip : KryptonHeader
{
    #region Instance Fields

    private readonly ButtonSpecAny _closeSpec;
    private readonly KryptonTagInputControl _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagChip"/> class.
    /// </summary>
    /// <param name="owner">Owning tag input control.</param>
    /// <param name="tag">Tag text shown on the chip.</param>
    public KryptonTagChip(KryptonTagInputControl owner, string tag)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

        HeaderStyle = HeaderStyle.Secondary;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Margin = new Padding(2);
        Tag = tag;
        Values.Heading = tag;
        Values.Description = string.Empty;
        Values.Image = null;

        _closeSpec = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            ToolTipTitle = @"Remove"
        };
        _closeSpec.Click += OnCloseClick;
        ButtonSpecs.Add(_closeSpec);

        AccessibleName = tag;
        AccessibleRole = AccessibleRole.PushButton;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the tag text this chip represents.
    /// </summary>
    public string TagText => Tag as string ?? Values.Heading;

    /// <summary>
    /// Applies rounding, optional category colour, and close-button visibility.
    /// </summary>
    /// <param name="categoryColor">Override fill colour, or empty to inherit the header palette.</param>
    /// <param name="rounding">Corner rounding for the chip border.</param>
    /// <param name="showRemove">Whether the close button is visible.</param>
    /// <param name="enabled">Whether the close button can be clicked.</param>
    public void ApplyAppearance(Color categoryColor, float rounding, bool showRemove, bool enabled)
    {
        StateCommon.Border.Rounding = rounding;
        StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;

        if (!categoryColor.IsEmpty)
        {
            StateCommon.Back.Color1 = categoryColor;
            StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            StateCommon.Content.ShortText.Color1 = IsDark(categoryColor) ? Color.White : Color.Black;
        }
        else
        {
            StateCommon.Back.Color1 = Color.Empty;
            StateCommon.Back.ColorStyle = PaletteColorStyle.Inherit;
            StateCommon.Content.ShortText.Color1 = Color.Empty;
        }

        _closeSpec.Visible = showRemove;
        _closeSpec.Enabled = enabled ? ButtonEnabled.True : ButtonEnabled.False;
    }

    #endregion

    #region Implementation

    private void OnCloseClick(object? sender, EventArgs e)
    {
        if (_closeSpec.Enabled == ButtonEnabled.True)
        {
            _owner.RemoveTag(TagText);
        }
    }

    private static bool IsDark(Color color) =>
        ((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114)) < 128.0;

    #endregion
}
