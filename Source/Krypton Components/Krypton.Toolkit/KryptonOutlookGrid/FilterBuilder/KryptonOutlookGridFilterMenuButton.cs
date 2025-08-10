namespace Krypton.Toolkit;

#region Menu Button

internal class KryptonOutlookGridFilterMenuButton : KryptonDropButton
{

    #region Public Delegates And Events

    public event KryptonOutlookGridFilterSelectionChangedEventHandler? SelectionChanged;

    public event KryptonOutlookGridFilterSelectionChangingEventHandler? SelectionChanging;

    #endregion Public Delegates And Events

    #region Private Variables

    internal ContextMenuStrip Menu = new();
    protected int _SelectedIndex; // The selected index in the menu

    #endregion Private Variables

    #region Private Properties

    protected int SelectedIndex
    {
        get { return _SelectedIndex; }
        set
        {
            //' tests to see if it is already set
            if (_SelectedIndex == value)
            {
                return;
            }

            //' The index that it was previously set to
            int oldIndex = SelectedIndex;

            //' Raises an event that allows a user to cancel the selection changing event
            KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e = new(value, oldIndex);
            SelectionChanging?.Invoke(this, e);

            //' Checks if a handler of the SelectionChanging event cancel the selection
            if (!e.Cancel)
            {
                try
                {
                    bool selectionChanged = false;

                    //' Unchecks each item in the list except the one that was selected
                    foreach (ToolStripItem item in Menu.Items)
                    {
                        //' Only unchecks items that can be unchecked
                        if (item.GetType().Equals(typeof(System.Windows.Forms.ToolStripMenuItem)))
                        {
                            //' Checks whether the item is the newly selected one
                            if (!(Menu.Items.IndexOf(item) == value))
                            {
                                ((ToolStripMenuItem)item).Checked = false;
                            }
                            else
                            {
                                //' Checks if the selected one was already checked
                                if (!(((ToolStripMenuItem)item).Checked))
                                {
                                    selectionChanged = true;
                                    ((ToolStripMenuItem)item).Checked = true;
                                }
                                _SelectedIndex = value;
                                this.Text = ((ToolStripMenuItem)item).Text;
                            }
                        }
                    }

                    //' Raises the selectionChanged event if it actually did change
                    if (selectionChanged)
                    {
                        SelectionChanged?.Invoke(this, new KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs(value, oldIndex));
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }

    #endregion Private Properties

    #region Constructor

    public KryptonOutlookGridFilterMenuButton()
    {
        this.Splitter = false;
        this.ContextMenuStrip = Menu;
        //this.Click += Button1_Click;
        Menu.ItemClicked += Menu_ItemClicked;
    }

    #endregion Constructor

    #region Control Events

    private void Menu_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
    {
        SelectedIndex = Menu.Items.IndexOf(e.ClickedItem!);
    }

    #endregion Control Events

}

#endregion Menu Button

#region Filter ItemMenu Button

internal class KryptonOutlookGridFilterItemMenuButton : KryptonOutlookGridFilterMenuButton
{

    #region Enums

    /// <summary>
    ///  An enumeration of the items in the menu button
    /// </summary>
    /// <remarks></remarks>
    public enum Items : int
    {
        [Description("And")]
        AndItem = 0,
        [Description("Or")]
        OrItem = 1,
        [Description("Delete")]
        Delete = 3,
        [Description("Insert")]
        Insert = 4,
        [Description("Make Subgroup")]
        MakeSubgroup = 6,
        [Description("End")]
        EndItem = 8
    }

    #endregion Enums

    #region Public Properties

    /// <summary>
    ///   Sets which item is selected in the menu
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Items Item
    {
        get { return (Items)this.SelectedIndex; }
        set { this.SelectedIndex = (int)value; }
    }

    #endregion Public Properties

    #region Construtors

    public KryptonOutlookGridFilterItemMenuButton()
    {
        AddMenuItem();
        this.ContextMenuStrip = Menu;
        this.Item = Items.EndItem;
    }

    #endregion Construtors

    #region Private Methods

    private void AddMenuItem()
    {
        ToolStripMenuItem AndToolStripMenuItem = new();
        ToolStripMenuItem OrToolStripMenuItem = new();
        ToolStripSeparator ToolStripSeparator1 = new();
        ToolStripMenuItem DeleteToolStripMenuItem = new();
        ToolStripMenuItem InsertToolStripMenuItem = new();
        ToolStripSeparator ToolStripSeparator2 = new();
        ToolStripMenuItem EndToolStripMenuItem = new();
        ToolStripSeparator ToolStripSeparator3 = new();
        ToolStripMenuItem MakeSubgroupToolStripMenuItem = new();

        AndToolStripMenuItem.Name = "AndToolStripMenuItem";
        AndToolStripMenuItem.Text = "And";
        OrToolStripMenuItem.Name = "OrToolStripMenuItem";
        OrToolStripMenuItem.Text = "Or";

        ToolStripSeparator1.Name = "ToolStripSeparator1";

        DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
        DeleteToolStripMenuItem.Text = "Delete";
        InsertToolStripMenuItem.Name = "InsertToolStripMenuItem";
        InsertToolStripMenuItem.Text = "Insert";
        ToolStripSeparator2.Name = "ToolStripSeparator2";

        EndToolStripMenuItem.Checked = true;
        EndToolStripMenuItem.CheckState = CheckState.Checked;
        EndToolStripMenuItem.Name = "EndToolStripMenuItem";
        EndToolStripMenuItem.Text = "End";
        ToolStripSeparator3.Name = "ToolStripSeparator3";
        MakeSubgroupToolStripMenuItem.Name = "MakeSubgroupToolStripMenuItem";
        MakeSubgroupToolStripMenuItem.Text = "Make Subgroup";

        Menu.Items.Add(AndToolStripMenuItem);
        Menu.Items.Add(OrToolStripMenuItem);
        Menu.Items.Add(ToolStripSeparator1);
        Menu.Items.Add(DeleteToolStripMenuItem);
        Menu.Items.Add(InsertToolStripMenuItem);
        Menu.Items.Add(ToolStripSeparator2);
        Menu.Items.Add(MakeSubgroupToolStripMenuItem);
        Menu.Items.Add(ToolStripSeparator3);
        Menu.Items.Add(EndToolStripMenuItem);
    }

    #endregion Private Methods

}

#endregion Filter ItemMenu Button

#region Filter Item Group Menu Button

internal class KryptonOutlookGridFilterItemGroupMenuButton : KryptonOutlookGridFilterMenuButton
{

    #region Enums

    /// <summary>
    ///  An enumeration of the items in the menu button
    /// </summary>
    /// <remarks></remarks>
    public enum Items : int
    {
        [Description("And")]
        AndItem = 0,
        [Description("Or")]
        OrItem = 1,
        [Description("Delete")]
        Delete = 3,
        [Description("Insert")]
        Insert = 4,
        [Description("End")]
        EndItem = 6
    }

    #endregion Enums

    #region Public Properties

    /// <summary>
    ///   Sets which item is selected in the button's menu
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Items Item
    {
        get { return (Items)this.SelectedIndex; }
        set { this.SelectedIndex = (int)value; }
    }

    #endregion Public Properties

    #region Construtors

    public KryptonOutlookGridFilterItemGroupMenuButton()
    {
        AddMenuItem();
        Item = Items.EndItem;
    }

    #endregion Construtors

    #region Private Methods

    private void AddMenuItem()
    {
        ToolStripMenuItem AndToolStripMenuItem = new();
        ToolStripMenuItem OrToolStripMenuItem = new();
        ToolStripSeparator ToolStripSeparator1 = new();
        ToolStripMenuItem DeleteToolStripMenuItem = new();
        ToolStripMenuItem InsertToolStripMenuItem = new();
        ToolStripSeparator ToolStripSeparator2 = new();
        ToolStripMenuItem EndToolStripMenuItem = new();

        AndToolStripMenuItem.Name = "AndToolStripMenuItem";
        AndToolStripMenuItem.Text = "And";
        OrToolStripMenuItem.Name = "OrToolStripMenuItem";
        OrToolStripMenuItem.Text = "Or";

        ToolStripSeparator1.Name = "ToolStripSeparator1";

        DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
        DeleteToolStripMenuItem.Text = "Delete";
        InsertToolStripMenuItem.Name = "InsertToolStripMenuItem";
        InsertToolStripMenuItem.Text = "Insert";
        ToolStripSeparator2.Name = "ToolStripSeparator2";

        EndToolStripMenuItem.Checked = true;
        EndToolStripMenuItem.CheckState = CheckState.Checked;
        EndToolStripMenuItem.Name = "EndToolStripMenuItem";
        EndToolStripMenuItem.Text = "End";

        Menu.Items.Add(AndToolStripMenuItem);
        Menu.Items.Add(OrToolStripMenuItem);
        Menu.Items.Add(ToolStripSeparator1);
        Menu.Items.Add(DeleteToolStripMenuItem);
        Menu.Items.Add(InsertToolStripMenuItem);
        Menu.Items.Add(ToolStripSeparator2);
        Menu.Items.Add(EndToolStripMenuItem);
    }

    #endregion Private Methods

}

#endregion Filter Item Group Menu Button