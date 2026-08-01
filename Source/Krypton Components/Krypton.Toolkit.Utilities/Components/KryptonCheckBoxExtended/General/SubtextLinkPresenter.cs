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
/// Presents word-wrapped subtext with link styling and hit testing for <see cref="KryptonCheckBoxExtended"/>.
/// </summary>
internal sealed class SubtextLinkPresenter : KryptonLinkWrapLabel
{
    #region Events

    /// <summary>
    /// Occurs when the user clicks the subtext outside of a link region.
    /// </summary>
    public event EventHandler? NonLinkClick;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="SubtextLinkPresenter"/> class.
    /// </summary>
    public SubtextLinkPresenter()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        AutoSize = false;
        TabStop = false;
        BackColor = Color.Transparent;
        LinkBehavior = LinkBehavior.HoverUnderline;
    }

    #endregion

    #region Implementation

    private int GetCharIndexFromPoint(Point localPoint)
    {
        MethodInfo? method = typeof(LinkLabel).GetMethod(
            "PointToCharIndex",
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new[] { typeof(Point) },
            null);

        if (method is null)
        {
            return -1;
        }

        return (int)method.Invoke(this, new object[] { localPoint })!;
    }

    private bool IsLinkCharIndex(int charIndex) =>
        charIndex >= LinkArea.Start && charIndex < LinkArea.Start + LinkArea.Length;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating whether the specified point is within a link region.
    /// </summary>
    /// <param name="parentClientPoint">Point in the parent control's client coordinates.</param>
    /// <returns>True if the point is over a link; otherwise false.</returns>
    public bool ContainsLinkPoint(Point parentClientPoint)
    {
        if (!Visible || LinkArea.Length <= 0)
        {
            return false;
        }

        Point localPoint = new Point(parentClientPoint.X - Left, parentClientPoint.Y - Top);
        int charIndex = GetCharIndexFromPoint(localPoint);
        return charIndex >= 0 && IsLinkCharIndex(charIndex);
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            int charIndex = GetCharIndexFromPoint(new Point(e.X, e.Y));
            if (!IsLinkCharIndex(charIndex))
            {
                NonLinkClick?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        base.OnMouseUp(e);
    }

    #endregion
}
