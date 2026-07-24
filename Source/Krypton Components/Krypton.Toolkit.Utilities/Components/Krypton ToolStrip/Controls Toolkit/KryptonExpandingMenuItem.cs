#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides expansion in menus, similar to Word and Exel 2003. The parent 
/// menu item must be an ExpandingMenuItem and any children that are selectively
/// shown must be ExpandingMenuItems (or derived classes). 
/// </summary>
/// <remarks>
/// NOTE: Recently used menu items are not yet recorded.
/// <para/>
/// The SpecialRenderer is responsible for actual painting
/// <para/>
/// The rules for expansion are:
/// <list>
/// <item>
/// To keep an item visible at all times, set IsStandardItem to true
/// </item>
/// <item>
/// If an item should be invisible for the user, set AlwaysHidden
/// NOT visible because visible is required to manage the menu. 
/// </item>
/// <item>
/// If a menu would display three or less items, the first three items are displayed
/// </item>
/// <item>
/// Duplicate separators are removed. 
/// <list>
/// <item>
/// This removes concern about role changes producing duplicate separators, 
/// they are removed here.
/// </item>
/// <item>
/// Not visible separators can not be included, because logic here will 
/// make them visible. 
/// </item>
/// <item>
/// Separators that appear at the start are hidden
/// </item>
/// <item>
/// Separators that appear last are ignored
/// </item>
/// </list>
/// </item>
/// </list>
/// </remarks>
[ToolboxBitmap(typeof(ToolStripDropDown)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
public class KryptonExpandingMenuItem : ToolStripMenuItem
{
    #region Instance Fields
    private readonly ExpandingMenuItemValues _values;

    private bool _inExpandedClick;

    private KryptonExpandingMenuItem _menuItemReference;

    private Timer _timer;
    #endregion

    #region Constants
    private const string EXPANDABLE_MENU_ITEM = "ExpandableMenuItem";

    private const int MINIMUM_ITEM_COUNT = 1;
    private const int MILLISECOND_DELAY = 2000;
    #endregion

    #region Properties
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Timer Timer
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => _timer;

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            if (_timer != null)
            {
                _timer.Tick -= Timer_Tick;
            }

            _timer = value;

            if (_timer != null)
            {
                _timer.Tick += Timer_Tick;
            }
        }
    }

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Standard item and always-hidden settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ExpandingMenuItemValues ExpandingValues => _values;

    private bool ShouldSerializeExpandingValues() => !_values.IsDefault;

    private void ResetExpandingValues() => _values.Reset();

    /// <summary>
    /// Gets or sets whether this item is a standard menu item. Standard menu
    /// items are always displayed, regardless of the expand status
    /// </summary>
    /// <value>True if the menu item is a standard item, otherwise false</value>
    /// <returns>True if the menu item is a standard item, otherwise false</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsStandardItem { get => _values.IsStandardItem; set => _values.IsStandardItem = value; }

    /// <summary>
    /// Gets or sets whether this menu item is always hidden for this user.
    /// </summary>
    /// <value>True if this user should not see this menu item, otherwise false</value>
    /// <returns>True if this user should not see this menu item, otherwise false</returns>
    /// <remarks>
    /// This is necessary because Visible is used to display menu items.
    /// We can't track it because during OnDropDown it's always false.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AlwaysHidden { get => _values.AlwaysHidden; set => _values.AlwaysHidden = value; }
    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonExpandingMenuItem"/> class.
    /// </summary>
    public KryptonExpandingMenuItem()
    {
        _values = new ExpandingMenuItemValues(this);

        _menuItemReference = this;

        Timer = new Timer();

        Timer.Interval = MILLISECOND_DELAY;
    }
    #endregion

    #region Event Handlers
    private void ExpandItem_Click(object? sender, EventArgs e) => ExpandItems();

    private void Timer_Tick(object? sender, EventArgs e) => ExpandItems();
    #endregion

    #region Overrides
    protected override void OnDropDownShow(EventArgs e)
    {
        base.OnDropDownShow(e);

        if (!DesignMode & !_inExpandedClick)
        {
            DropDown.SuspendLayout();

            bool hasExpandable = false;

            int itemCount = 1;

            foreach (KryptonExpandingMenuItem expandingMenuItem in DropDownItems.OfType<KryptonExpandingMenuItem>())
            {
                itemCount += 1;

                if (expandingMenuItem is { IsStandardItem: false })
                {
                    hasExpandable = true;

                    expandingMenuItem.Visible = false;

                    itemCount -= 1;
                }
            }

            if (itemCount < MINIMUM_ITEM_COUNT)
            {
                hasExpandable = !DisplayMinimumItems();
            }

            RemoveAdjacentSeparators();

            if (hasExpandable)
            {
                AddExpandMenuItem();
            }

            Timer.Enabled = true;

            DropDown.ResumeLayout();
        }
    }

    protected override void OnDropDownClosed(EventArgs e)
    {
        base.OnDropDownClosed(e);

        ResetMenu();
    }
    #endregion

    #region Methods
    private void ExpandItems()
    {
        DropDown.SuspendLayout();
        foreach (KryptonExpandingMenuItem menuItem in DropDownItems.OfType<KryptonExpandingMenuItem>())
        {
            if (menuItem.Visible == false & !menuItem.AlwaysHidden)
            {
                menuItem.Visible = true;
            }
        }

        foreach (ToolStripSeparator separator in DropDownItems.OfType<ToolStripSeparator>())
        {
            separator.Visible = true;
        }

        _inExpandedClick = true;
        ShowDropDown();
        _inExpandedClick = false;
        ResetMenu();
        DropDown.ResumeLayout();
    }

    private bool DisplayMinimumItems()
    {
        int cnt = 0;
        foreach (ToolStripItem item in DropDownItems)
        {
            if (item is ToolStripSeparator)
            {
                continue;
            }

            cnt += 1;
            if (cnt > MINIMUM_ITEM_COUNT)
            {
                return false;
            }
            // If its not an ExpandingMenuItem, we didn't hide it
            KryptonExpandingMenuItem? menuItem = item as KryptonExpandingMenuItem;
            if (menuItem is { AlwaysHidden: false })
            {
                menuItem.Visible = true;
            }
        }

        return true;
    }

    private void RemoveAdjacentSeparators()
    {
        ToolStripSeparator? lastSeparator = null;
        bool lastVisibleIsSeparator = false;
        foreach (ToolStripItem item in DropDownItems)
        {
            ToolStripSeparator? separator = item as ToolStripSeparator;
            if (separator != null)
            {
                if (lastVisibleIsSeparator)
                {
                    // This item is a separator and the last visible item is a
                    // separator - meaning adajcent separators
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    lastSeparator = separator;
                }

                lastVisibleIsSeparator = true;
            }
            // support anything that might be in the dropdown here
            else if (item.Visible)
            {
                lastSeparator = null;
                lastVisibleIsSeparator = false;
            }
        }

        if (lastSeparator != null)
        {
            lastSeparator.Visible = false;
        }
    }

    private void AddExpandMenuItem()
    {
        if (DropDownItems[EXPANDABLE_MENU_ITEM] is null)
        {
            var expandedItem = new KryptonExpandedItem();
            expandedItem.Click += ExpandItem_Click;
            DropDownItems.Add(expandedItem);
        }
    }

    private void ResetMenu()
    {
        if (DropDownItems[EXPANDABLE_MENU_ITEM] is object)
        {
            DropDownItems.RemoveByKey(EXPANDABLE_MENU_ITEM);
        }

        Timer.Enabled = false;
    }
    #endregion
}
