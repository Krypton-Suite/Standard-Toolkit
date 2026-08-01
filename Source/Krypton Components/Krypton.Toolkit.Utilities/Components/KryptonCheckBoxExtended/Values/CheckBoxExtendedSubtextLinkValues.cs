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
/// Storage for linked subtext settings on <see cref="KryptonCheckBoxExtended"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CheckBoxExtendedSubtextLinkValues : Storage
{
    #region Instance Fields

    private KryptonCheckBoxExtended? _owner;
    private LinkArea _linkArea;
    private Color _linkColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="CheckBoxExtendedSubtextLinkValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public CheckBoxExtendedSubtextLinkValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        _linkArea = new LinkArea(0, 0);
        _linkColor = GlobalStaticVariables.EMPTY_COLOR;
    }

    #endregion

    #region Public

    /// <inheritdoc />
    public override bool IsDefault => _linkArea.Length == 0 && _linkColor.IsEmpty;

    /// <summary>
    /// Gets or sets the portion of the subtext that behaves as a link.
    /// </summary>
    [Category(@"Link")]
    [Description(@"The portion of the subtext that behaves as a link.")]
    [DefaultValue(typeof(LinkArea), "0, 0")]
    public LinkArea LinkArea
    {
        get => _linkArea;

        set
        {
            if (_linkArea.Start != value.Start || _linkArea.Length != value.Length)
            {
                _linkArea = value;
                _owner?.OnSubtextLinkValuesChanged();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the <see cref="LinkArea"/> property to its default value.
    /// </summary>
    public void ResetLinkArea() => LinkArea = new LinkArea(0, 0);

    private bool ShouldSerializeLinkArea() => LinkArea.Length != 0;

    /// <summary>
    /// Gets or sets the color used to render linked subtext.
    /// </summary>
    [Category(@"Link")]
    [Description(@"The color used to render linked subtext.")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color LinkColor
    {
        get => _linkColor;

        set
        {
            if (_linkColor != value)
            {
                _linkColor = value;
                _owner?.OnSubtextLinkValuesChanged();
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Resets the <see cref="LinkColor"/> property to its default value.
    /// </summary>
    public void ResetLinkColor() => LinkColor = GlobalStaticVariables.EMPTY_COLOR;

    private bool ShouldSerializeLinkColor() => !LinkColor.IsEmpty;

    #endregion

    #region Implementation

    internal void SetOwner(KryptonCheckBoxExtended owner) => _owner = owner;

    /// <summary>
    /// Returns a string representation of the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this instance.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";

    #endregion
}
