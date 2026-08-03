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
/// Clickable/draggable group header chip shown before a contiguous run of grouped caption tabs.
/// </summary>
internal sealed class ViewDrawTabGroupHeader : ViewDrawButton
{
    private readonly NavigatorTabGroup _group;
    private readonly NavigatorTabGroupAppearance _appearance;
    private readonly Action<NavigatorTabGroup> _toggleCollapsed;
    private readonly Action<NavigatorTabGroup>? _activateGroup;
    private readonly Action<NavigatorTabGroup, DragStartEventCancelArgs>? _dragStart;
    private readonly Action<PointEventArgs>? _dragMove;
    private readonly Action<PointEventArgs>? _dragEnd;
    private readonly Action? _dragQuit;
    private readonly Func<NavigatorTabGroup, int>? _memberCount;

    public ViewDrawTabGroupHeader(
        KryptonNavigator navigator,
        NavigatorTabGroup group,
        NavigatorTabGroupAppearance appearance,
        NeedPaintHandler needPaint,
        Action<NavigatorTabGroup> toggleCollapsed,
        Action<NavigatorTabGroup>? activateGroup,
        Func<NavigatorTabGroup, int>? memberCount = null,
        Action<NavigatorTabGroup, DragStartEventCancelArgs>? dragStart = null,
        Action<PointEventArgs>? dragMove = null,
        Action<PointEventArgs>? dragEnd = null,
        Action? dragQuit = null)
        : base(
            navigator.StateDisabled.MiniButton,
            navigator.StateNormal.MiniButton,
            navigator.StateTracking.MiniButton,
            navigator.StatePressed.MiniButton,
            navigator.StateNormal.MiniButton,
            navigator.StateNormal.MiniButton,
            navigator.StateNormal.MiniButton,
            null,
            new FixedContentValue(ResolveTitle(group, memberCount), string.Empty, null, Color.Empty),
            VisualOrientation.Top,
            false)
    {
        _group = group ?? throw new ArgumentNullException(nameof(group));
        _appearance = appearance ?? throw new ArgumentNullException(nameof(appearance));
        _toggleCollapsed = toggleCollapsed ?? throw new ArgumentNullException(nameof(toggleCollapsed));
        _activateGroup = activateGroup;
        _memberCount = memberCount;
        _dragStart = dragStart;
        _dragMove = dragMove;
        _dragEnd = dragEnd;
        _dragQuit = dragQuit;

        var controller = new ButtonController(this, needPaint)
        {
            AllowDragging = dragStart != null
        };
        controller.Click += (_, _) => OnHeaderClick();
        if (dragStart != null)
        {
            controller.DragStart += (_, e) => _dragStart?.Invoke(_group, e);
            controller.DragMove += (_, e) => _dragMove?.Invoke(e);
            controller.DragEnd += (_, e) => _dragEnd?.Invoke(e);
            controller.DragQuit += (_, _) => _dragQuit?.Invoke();
        }

        MouseController = controller;
        KeyController = controller;
        SourceController = controller;
    }

    public NavigatorTabGroup Group => _group;

    public override void Render(RenderContext context)
    {
        base.Render(context);

        if (_group.Color.IsEmpty || ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
        {
            return;
        }

        Color groupColor = _group.Color;

        // Soft wash of the group color across the whole chip so each group reads as a
        // distinct colored cluster without hiding the themed button base or its text.
        // Collapsed headers stand alone (no member tabs follow), so tint them a little
        // more strongly by default to keep the color identity.
        int washAlpha = _group.Collapsed
            ? _appearance.CollapsedHeaderWashAlpha
            : _appearance.HeaderWashAlpha;
        if (washAlpha > 0)
        {
            using (var wash = new SolidBrush(Color.FromArgb(washAlpha, groupColor)))
            {
                context.Graphics.FillRectangle(wash, ClientRectangle);
            }
        }

        // Solid bottom accent bar using the group color (does not replace palette theming).
        if (_appearance.ShowHeaderAccent && _appearance.HeaderAccentHeight > 0)
        {
            int height = Math.Min(_appearance.HeaderAccentHeight, ClientRectangle.Height);
            var accent = new Rectangle(ClientRectangle.X, ClientRectangle.Bottom - height,
                ClientRectangle.Width, height);
            using (var brush = new SolidBrush(groupColor))
            {
                context.Graphics.FillRectangle(brush, accent);
            }
        }
    }

    private void OnHeaderClick()
    {
        if (_group.Collapsed)
        {
            _activateGroup?.Invoke(_group);
        }

        _toggleCollapsed(_group);
    }

    private static string ResolveTitle(NavigatorTabGroup group, Func<NavigatorTabGroup, int>? memberCount)
    {
        string title = !string.IsNullOrEmpty(group.Title)
            ? group.Title
            : (string.IsNullOrEmpty(group.Id)
                ? KryptonManager.Strings.NavigatorIntegrationStrings.DefaultGroupTitle
                : group.Id);

        if (group.Collapsed && memberCount != null)
        {
            int count = memberCount(group);
            if (count > 0)
            {
                return $"{title} ({count})";
            }
        }

        return title;
    }
}
