#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    #endregion

    #region Identity
    public KryptonRibbonAccessibleObject(KryptonRibbon owner)
        : base(owner)
    {
        _ribbon = owner;
    }
    #endregion

    #region Public Overrides
    public override string? Name => !string.IsNullOrEmpty(_ribbon.AccessibleName)
        ? _ribbon.AccessibleName
        : base.Name ?? _ribbon.Name;

    public override AccessibleRole Role => _ribbon.AccessibleRole != AccessibleRole.Default
        ? _ribbon.AccessibleRole
        : AccessibleRole.ToolBar;

    public override AccessibleStates State
    {
        get
        {
            AccessibleStates state = AccessibleStates.None;

            if (!_ribbon.Visible)
            {
                state |= AccessibleStates.Invisible;
            }

            if (!_ribbon.Enabled)
            {
                state |= AccessibleStates.Unavailable;
            }

            return state;
        }
    }

    public override Rectangle Bounds => _ribbon.RectangleToScreen(_ribbon.ClientRectangle);

    public override int GetChildCount() => GetChildren().Count;

    public override AccessibleObject? GetChild(int index)
    {
        List<AccessibleObject> children = GetChildren();

        return index >= 0 && index < children.Count
            ? children[index]
            : null;
    }

    public override AccessibleObject? HitTest(int x, int y)
    {
        foreach (AccessibleObject child in GetChildren())
        {
            if (child.Bounds.Contains(x, y))
            {
                AccessibleObject? nested = child.HitTest(x, y);

                return nested ?? child;
            }
        }

        return Bounds.Contains(x, y) ? this : null;
    }
    #endregion

    #region Implementation
    private List<AccessibleObject> GetChildren()
    {
        var children = new List<AccessibleObject>();

        foreach (IQuickAccessToolbarButton qatButton in _ribbon.QATButtons)
        {
            if (qatButton.GetVisible())
            {
                children.Add(new RibbonQATButtonAccessibleObject(this, _ribbon, qatButton));
            }
        }

        foreach (KryptonRibbonTab tab in _ribbon.RibbonTabs)
        {
            if (tab.Visible)
            {
                children.Add(new RibbonTabAccessibleObject(this, _ribbon, tab));
            }
        }

        if (_ribbon.SelectedTab != null)
        {
            foreach (KryptonRibbonGroup group in _ribbon.SelectedTab.Groups)
            {
                if (group.Visible)
                {
                    children.Add(new RibbonGroupAccessibleObject(this, _ribbon, group));
                }
            }
        }

        return children;
    }

    private static string? SiteName(Component? component) => component?.Site?.Name;

    private static string? FirstNonEmpty(params string?[] values)
    {
        foreach (string? value in values)
        {
            string? normalized = Normalize(value);

            if (!string.IsNullOrEmpty(normalized))
            {
                return normalized;
            }
        }

        return null;
    }

    private static string? Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value!.Trim();
    }

    private static string? CombineText(string? line1, string? line2)
    {
        string? first = Normalize(line1);
        string? second = Normalize(line2);

        if (!string.IsNullOrEmpty(first) && !string.IsNullOrEmpty(second))
        {
            return $@"{first} {second}";
        }

        return first ?? second;
    }

    private static string? ToolTipName(ToolTipValues? toolTipValues) => toolTipValues == null
        ? null
        : FirstNonEmpty(toolTipValues.Heading, toolTipValues.Description);

    private static string? ToolTipDescription(ToolTipValues? toolTipValues) => toolTipValues == null
        ? null
        : FirstNonEmpty(toolTipValues.Description, toolTipValues.Heading);

    private static string? ButtonName(KryptonRibbonGroupButton button) =>
        FirstNonEmpty(
            CombineText(button.KryptonCommand?.TextLine1, button.KryptonCommand?.TextLine2),
            button.KryptonCommand?.Text,
            CombineText(button.TextLine1, button.TextLine2),
            ToolTipName(button.ToolTipValues),
            button.KeyTip,
            SiteName(button),
            button.GetType().Name);

    private static string? CheckBoxName(KryptonRibbonGroupCheckBox checkBox) =>
        FirstNonEmpty(
            CombineText(checkBox.KryptonCommand?.TextLine1, checkBox.KryptonCommand?.TextLine2),
            checkBox.KryptonCommand?.Text,
            CombineText(checkBox.TextLine1, checkBox.TextLine2),
            ToolTipName(checkBox.ToolTipValues),
            checkBox.KeyTip,
            SiteName(checkBox),
            checkBox.GetType().Name);

    private static string? GroupName(KryptonRibbonGroup group) =>
        FirstNonEmpty(
            CombineText(group.TextLine1, group.TextLine2),
            group.KeyTipGroup,
            SiteName(group),
            group.GetType().Name);

    private static string? TabName(KryptonRibbonTab tab) =>
        FirstNonEmpty(tab.Text, tab.KeyTip, SiteName(tab), tab.GetType().Name);

    private static string? QATName(IQuickAccessToolbarButton qatButton) =>
        FirstNonEmpty(
            qatButton.GetText(),
            qatButton.GetToolTipTitle(),
            qatButton.GetToolTipBody(),
            SiteName(qatButton as Component),
            qatButton.GetType().Name);

    private static string? CustomControlName(KryptonRibbonGroupCustomControl customControl)
    {
        Control? control = customControl.CustomControl;

        return FirstNonEmpty(
            control?.AccessibleName,
            control?.Text,
            control?.Name,
            ToolTipName(customControl.ToolTipValues),
            customControl.KeyTip,
            SiteName(customControl),
            customControl.GetType().Name);
    }

    private static string? ItemName(KryptonRibbonGroupItem item) => item switch
    {
        KryptonRibbonGroupButton button => ButtonName(button),
        KryptonRibbonGroupCheckBox checkBox => CheckBoxName(checkBox),
        KryptonRibbonGroupCustomControl customControl => CustomControlName(customControl),
        _ => FirstNonEmpty(ToolTipName(item.ToolTipValues), SiteName(item), item.GetType().Name)
    };

    private static string? ItemDescription(KryptonRibbonGroupItem item) => ToolTipDescription(item.ToolTipValues);

    private static Rectangle ViewBounds(KryptonRibbon ribbon, ViewBase? view)
    {
        return view != null && view.ClientRectangle != Rectangle.Empty
            ? ribbon.RectangleToScreen(view.ClientRectangle)
            : Rectangle.Empty;
    }

    private static Rectangle BoundsOrOwner(KryptonRibbon ribbon, ViewBase? view)
    {
        Rectangle bounds = ViewBounds(ribbon, view);

        return !bounds.IsEmpty ? bounds : ribbon.RectangleToScreen(ribbon.ClientRectangle);
    }

    private static ViewBase? FindQATView(KryptonRibbon ribbon, IQuickAccessToolbarButton qatButton)
    {
        ViewBase? root = ribbon.ViewRibbonManager?.Root;

        return root == null ? null : FindQATView(root, qatButton);
    }

    private static ViewBase? FindQATView(ViewBase view, IQuickAccessToolbarButton qatButton)
    {
        if (view is ViewDrawRibbonQATButton qatView && ReferenceEquals(qatView.QATButton, qatButton))
        {
            return qatView;
        }

        foreach (ViewBase? child in view)
        {
            if (child == null)
            {
                continue;
            }

            ViewBase? found = FindQATView(child, qatButton);

            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    private static ViewBase? ItemView(KryptonRibbonGroupItem item) => item switch
    {
        KryptonRibbonGroupButton button => button.ButtonView,
        KryptonRibbonGroupCheckBox checkBox => checkBox.CheckBoxView,
        KryptonRibbonGroupCustomControl customControl => customControl.CustomControlView,
        _ => null
    };

    private static bool ItemEnabled(KryptonRibbonGroupItem item) => item switch
    {
        KryptonRibbonGroupButton button => button.KryptonCommand?.Enabled ?? button.Enabled,
        KryptonRibbonGroupCheckBox checkBox => checkBox.KryptonCommand?.Enabled ?? checkBox.Enabled,
        KryptonRibbonGroupCustomControl customControl => customControl.Enabled && (customControl.CustomControl?.Enabled ?? true),
        _ => true
    };

    private static CheckState ItemCheckState(KryptonRibbonGroupItem item) => item switch
    {
        KryptonRibbonGroupButton { ButtonType: GroupButtonType.Check } button =>
            (button.KryptonCommand?.Checked ?? button.Checked) ? CheckState.Checked : CheckState.Unchecked,
        KryptonRibbonGroupCheckBox checkBox => checkBox.KryptonCommand?.CheckState ?? checkBox.CheckState,
        _ => CheckState.Unchecked
    };

    private static IEnumerable<KryptonRibbonGroupItem> ChildItems(KryptonRibbonGroup group)
    {
        foreach (KryptonRibbonGroupContainer container in group.Items)
        {
            foreach (KryptonRibbonGroupItem item in ChildItems(container))
            {
                yield return item;
            }
        }
    }

    private static IEnumerable<KryptonRibbonGroupItem> ChildItems(KryptonRibbonGroupContainer container)
    {
        foreach (Component component in container.GetChildComponents())
        {
            if (component is KryptonRibbonGroupContainer childContainer)
            {
                foreach (KryptonRibbonGroupItem item in ChildItems(childContainer))
                {
                    yield return item;
                }
            }
            else if (component is KryptonRibbonGroupItem item)
            {
                yield return item;
            }
        }
    }

    private abstract class RibbonChildAccessibleObject : AccessibleObject
    {
        private readonly AccessibleObject _parent;

        protected RibbonChildAccessibleObject(AccessibleObject parent, KryptonRibbon ribbon)
        {
            _parent = parent;
            Ribbon = ribbon;
        }

        protected KryptonRibbon Ribbon { get; }

        public override AccessibleObject? Parent => _parent;

        public override int GetChildCount() => 0;

        public override AccessibleObject? GetChild(int index) => null;

        public override AccessibleObject? HitTest(int x, int y) => Bounds.Contains(x, y) ? this : null;
    }

    private sealed class RibbonTabAccessibleObject : RibbonChildAccessibleObject
    {
        private readonly KryptonRibbonTab _tab;

        public RibbonTabAccessibleObject(AccessibleObject parent, KryptonRibbon ribbon, KryptonRibbonTab tab)
            : base(parent, ribbon)
        {
            _tab = tab;
        }

        public override string? Name => TabName(_tab);

        public override string DefaultAction => @"Select";

        public override AccessibleRole Role => AccessibleRole.PageTab;

        public override AccessibleStates State
        {
            get
            {
                AccessibleStates state = AccessibleStates.Focusable | AccessibleStates.Selectable;

                if (!_tab.Visible)
                {
                    state |= AccessibleStates.Invisible;
                }

                if (Ribbon.SelectedTab == _tab)
                {
                    state |= AccessibleStates.Selected;
                }

                if (!Ribbon.Enabled)
                {
                    state |= AccessibleStates.Unavailable;
                }

                return state;
            }
        }

        public override Rectangle Bounds => BoundsOrOwner(Ribbon, _tab.TabView);

        public override void DoDefaultAction() => Select(AccessibleSelection.TakeSelection);

        public override void Select(AccessibleSelection flags)
        {
            if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
            {
                Ribbon.SelectedTab = _tab;
            }
        }
    }

    private sealed class RibbonGroupAccessibleObject : RibbonChildAccessibleObject
    {
        private readonly KryptonRibbonGroup _group;

        public RibbonGroupAccessibleObject(AccessibleObject parent, KryptonRibbon ribbon, KryptonRibbonGroup group)
            : base(parent, ribbon)
        {
            _group = group;
        }

        public override string? Name => GroupName(_group);

        public override AccessibleRole Role => AccessibleRole.Grouping;

        public override AccessibleStates State
        {
            get
            {
                AccessibleStates state = AccessibleStates.None;

                if (!_group.Visible || Ribbon.SelectedTab != _group.RibbonTab)
                {
                    state |= AccessibleStates.Invisible;
                }

                if (!Ribbon.Enabled)
                {
                    state |= AccessibleStates.Unavailable;
                }

                return state;
            }
        }

        public override Rectangle Bounds => BoundsOrOwner(Ribbon, _group.GroupView);

        public override int GetChildCount() => GetChildren().Count;

        public override AccessibleObject? GetChild(int index)
        {
            List<AccessibleObject> children = GetChildren();

            return index >= 0 && index < children.Count
                ? children[index]
                : null;
        }

        public override AccessibleObject? HitTest(int x, int y)
        {
            foreach (AccessibleObject child in GetChildren())
            {
                if (child.Bounds.Contains(x, y))
                {
                    AccessibleObject? nested = child.HitTest(x, y);

                    return nested ?? child;
                }
            }

            return Bounds.Contains(x, y) ? this : null;
        }

        private List<AccessibleObject> GetChildren()
        {
            var children = new List<AccessibleObject>();

            foreach (KryptonRibbonGroupItem item in ChildItems(_group))
            {
                if (item.Visible)
                {
                    children.Add(new RibbonGroupItemAccessibleObject(this, Ribbon, item));
                }
            }

            return children;
        }
    }

    private sealed class RibbonGroupItemAccessibleObject : RibbonChildAccessibleObject
    {
        private readonly KryptonRibbonGroupItem _item;

        public RibbonGroupItemAccessibleObject(AccessibleObject parent, KryptonRibbon ribbon, KryptonRibbonGroupItem item)
            : base(parent, ribbon)
        {
            _item = item;
        }

        public override string? Name => ItemName(_item);

        public override string? Description => ItemDescription(_item);

        public override string DefaultAction => _item is KryptonRibbonGroupCustomControl ? @"Focus" : @"Press";

        public override AccessibleRole Role => _item switch
        {
            KryptonRibbonGroupCheckBox _ => AccessibleRole.CheckButton,
            KryptonRibbonGroupButton button => button.ButtonType == GroupButtonType.Check ? AccessibleRole.CheckButton : AccessibleRole.PushButton,
            KryptonRibbonGroupCustomControl customControl => customControl.CustomControl?.AccessibilityObject.Role ?? AccessibleRole.Client,
            _ => AccessibleRole.PushButton
        };

        public override AccessibleStates State
        {
            get
            {
                AccessibleStates state = AccessibleStates.Focusable;

                if (!_item.Visible || _item.RibbonTab?.Ribbon?.SelectedTab != _item.RibbonTab)
                {
                    state |= AccessibleStates.Invisible;
                }

                if (!Ribbon.Enabled || !ItemEnabled(_item))
                {
                    state |= AccessibleStates.Unavailable;
                }

                state |= ItemCheckState(_item) switch
                {
                    CheckState.Checked => AccessibleStates.Checked,
                    CheckState.Indeterminate => AccessibleStates.Indeterminate,
                    _ => 0
                };

                return state;
            }
        }

        public override Rectangle Bounds
        {
            get
            {
                if (_item is KryptonRibbonGroupCustomControl customControl
                    && customControl.CustomControl != null
                    && customControl.CustomControl.Visible)
                {
                    return customControl.CustomControl.RectangleToScreen(customControl.CustomControl.ClientRectangle);
                }

                return BoundsOrOwner(Ribbon, ItemView(_item));
            }
        }

        public override void DoDefaultAction()
        {
            if (!ItemEnabled(_item))
            {
                return;
            }

            if (_item is KryptonRibbonGroupButton button)
            {
                if (button.ButtonType is GroupButtonType.DropDown or GroupButtonType.Split)
                {
                    button.PerformDropDown();
                }
                else
                {
                    button.PerformClick();
                }
            }
            else if (_item is KryptonRibbonGroupCheckBox checkBox)
            {
                checkBox.PerformClick();
            }
            else if (_item is KryptonRibbonGroupCustomControl customControl
                && customControl.CustomControl?.CanFocus == true)
            {
                customControl.CustomControl.Focus();
            }
        }
    }

    private sealed class RibbonQATButtonAccessibleObject : RibbonChildAccessibleObject
    {
        private readonly IQuickAccessToolbarButton _qatButton;

        public RibbonQATButtonAccessibleObject(AccessibleObject parent, KryptonRibbon ribbon, IQuickAccessToolbarButton qatButton)
            : base(parent, ribbon)
        {
            _qatButton = qatButton;
        }

        public override string? Name => QATName(_qatButton);

        public override string? Description => FirstNonEmpty(_qatButton.GetToolTipBody(), _qatButton.GetToolTipTitle());

        public override string DefaultAction => @"Press";

        public override AccessibleRole Role => AccessibleRole.PushButton;

        public override AccessibleStates State
        {
            get
            {
                AccessibleStates state = AccessibleStates.Focusable;

                if (!_qatButton.GetVisible())
                {
                    state |= AccessibleStates.Invisible;
                }

                if (!Ribbon.Enabled || !_qatButton.GetEnabled())
                {
                    state |= AccessibleStates.Unavailable;
                }

                return state;
            }
        }

        public override Rectangle Bounds => BoundsOrOwner(Ribbon, FindQATView(Ribbon, _qatButton));

        public override void DoDefaultAction()
        {
            if (Ribbon.Enabled && _qatButton.GetEnabled() && _qatButton.GetVisible())
            {
                _qatButton.PerformClick();
            }
        }
    }
    #endregion
}
