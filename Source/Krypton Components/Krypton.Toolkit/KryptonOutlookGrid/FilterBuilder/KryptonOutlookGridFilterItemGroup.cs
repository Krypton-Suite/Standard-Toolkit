namespace Krypton.Toolkit
{
    internal partial class KryptonOutlookGridFilterItemGroup : UserControl, IKryptonOutlookGridFilterItem
    {

        #region Public Delegates And Events

        /// <summary>
        /// Occurs when an AND/OR operation is selected for a group.
        /// </summary>
        public event KryptonOutlookGridFilterGroupSelectedAndOrEventHandler? GroupSelectedAndOr;

        /// <summary>
        /// Occurs when the selection of a group has ended.
        /// </summary>
        public event KryptonOutlookGridFilterGroupSelectedEndEventHandler? GroupSelectedEnd;

        /// <summary>
        /// Occurs when a group is about to be deleted.
        /// </summary>
        public event KryptonOutlookGridFilterGroupSelectedDeleteEventHandler? GroupSelectedDelete;

        /// <summary>
        /// Occurs when a new item is about to be inserted into a group.
        /// </summary>
        public event KryptonOutlookGridFilterGroupSelectedInsertEventHandler? GroupSelectedInsert;

        /// <summary>
        /// Occurs when the filter criteria have changed.
        /// </summary>
        public event KryptonOutlookGridFilterFilterChangedEventHandler? FilterChanged;

        #endregion Public Delegates And Events

        #region Private Variables

        private List<KryptonOutlookGridFilterSourceColumn> _columns = null!;
        private bool _PrimaryGroup;

        #endregion Private Variables

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get { return GroupBox1.Text; }
            set { GroupBox1.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonOutlookGridFilterItemMenuButton.Items SelectedMenuItem
        {
            get { return (KryptonOutlookGridFilterItemMenuButton.Items)FilterGroupMenu.Item; }
            set { FilterGroupMenu.Item = (KryptonOutlookGridFilterItemGroupMenuButton.Items)value; }
        }

        public List<KryptonOutlookGridFilterField> FilterData => FillData();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonOutlookGridFilterField FieldValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Filter
        {
            get
            {
                string filterString = "";
                string lastConjunction = "";
                int count = 0;

                //' joins each item in the collection
                foreach (IKryptonOutlookGridFilterItem filterItem in FilterItems.Controls)
                {
                    string tempFilter = filterItem.Filter;
                    if (tempFilter != null && tempFilter.Length > 0)
                    {
                        filterString += lastConjunction + " " + tempFilter + " ";
                        lastConjunction = filterItem.Conjunction;
                        count += 1;
                    }
                }

                if (count >= 2)
                {
                    return "(" + filterString.Trim() + ")";
                }
                else
                {
                    return filterString.Trim();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ReadableFilter => $"({Title})";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Conjunction
        {
            get
            {
                if (this.SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.AndItem)
                {
                    return "AND";
                }
                else if (this.SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.OrItem)
                {
                    return "OR";
                }
                else
                {
                    return "";
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<KryptonOutlookGridFilterSourceColumn> Columns
        {
            get { return this._columns; }
            set { this._columns = value; }
        }

        /// <summary>
        ///  Indicates whether the group is the primary group.  Some things are rendered slightly
        ///  differently if it is the primary group
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool PrimaryGroup
        {
            get { return _PrimaryGroup; }
            set
            {
                _PrimaryGroup = value;
                FilterGroupMenu.Visible = !value;
                this.Title = "Main Group";
            }
        }

        #endregion Properties

        #region Constructors

        public KryptonOutlookGridFilterItemGroup()
        {
            InitializeComponent();

            FilterGroupMenu.SelectionChanged += FilterMenu_SelectionChanged;
            FilterGroupMenu.SelectionChanging += FilterMenu_SelectionChanging;
            SelectedMenuItem = KryptonOutlookGridFilterItemMenuButton.Items.EndItem;
            FilterGroupMenu.Item = KryptonOutlookGridFilterItemGroupMenuButton.Items.EndItem;
            // Sets the readable filter of the group
            SetReadableFilter();
        }

        public KryptonOutlookGridFilterItemGroup(List<KryptonOutlookGridFilterSourceColumn> columns) : this()
        {
            Columns = columns;
        }

        #endregion Constructors

        #region Control Events

        private void FilterItem_SelectedAndOr(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e)
        {
            if (FilterItems.Controls.IndexOf((Control)sender) == FilterItems.Controls.Count - 1)
            {
                this.AddFilterItem(new KryptonOutlookGridFilterItem(_columns));
            }
            SetReadableFilter();
        }

        private void FilterItem_SelectedDelete(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e)
        {
            //' Checks to make sure there is more than one control left
            if (FilterItems.Controls.Count > 1)
            {
                if (FilterItems.Controls.IndexOf((Control)sender) == FilterItems.Controls.Count - 1)
                {
                    //' Removes the control
                    FilterItems.Controls.Remove((Control)sender);

                    //' Sets the menu item of the now last control to 'end'
                    if (sender.GetType().Equals(typeof(KryptonOutlookGridFilterItem)))
                    {
                        ((IKryptonOutlookGridFilterItem)FilterItems.Controls[FilterItems.Controls.Count - 1]).SelectedMenuItem = KryptonOutlookGridFilterItemMenuButton.Items.EndItem;
                    }
                    else
                    {
                        ((IKryptonOutlookGridFilterItem)FilterItems.Controls[FilterItems.Controls.Count - 1]).SelectedMenuItem = (KryptonOutlookGridFilterItemMenuButton.Items)KryptonOutlookGridFilterItemGroupMenuButton.Items.EndItem;
                    }
                }
                else
                {
                    FilterItems.Controls.Remove((Control)sender);
                }

                SetReadableFilter();
            }
            else
            {
                if (sender.GetType().Equals(typeof(KryptonOutlookGridFilterItem)))
                {
                    ((IKryptonOutlookGridFilterItem)sender).SelectedMenuItem = KryptonOutlookGridFilterItemMenuButton.Items.EndItem;
                }
                else
                {
                    ((IKryptonOutlookGridFilterItem)sender).SelectedMenuItem = (KryptonOutlookGridFilterItemMenuButton.Items)KryptonOutlookGridFilterItemGroupMenuButton.Items.EndItem;
                }
            }
        }

        private void FilterItem_SelectedInsert(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e)
        {
            int oldIndex = FilterItems.Controls.IndexOf((Control)sender);

            //' Creates a new Item to insert, and sets its menuItem to 'and'
            /*FilterItem newItem = new(this.Columns)
            {
                SelectedMenuItem = FilterItemMenuButton.Items.AndItem
            };*/

            KryptonOutlookGridFilterItem newItem = new(_columns);
            newItem.SelectedMenuItem = KryptonOutlookGridFilterItemMenuButton.Items.AndItem;
            //' Adds the new item to the list of items in the correct location
            FilterItems.SuspendLayout();
            AddFilterItem(newItem);
            FilterItems.Controls.SetChildIndex(newItem, oldIndex);
            FilterItems.ResumeLayout();
        }

        private void FilterItem_SelectedEnd(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e)
        {
            RemoveControlsAfter((Control)sender);
        }

        private void FilterItem_SelectedMakeSubgroup(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e)
        {
            FilterItems.SuspendLayout();

            //' converts the filterItem to a filterItemGroup
            KryptonOutlookGridFilterItemGroup newGroup = ConvertToSubgroup((KryptonOutlookGridFilterItem)sender);
            newGroup.FilterChanged = FilterChanged;
            //' resets all of the group titles
            SetGroupTitles();

            //' sets the readable filter to reflect the changes to the group titles
            SetReadableFilter();
            newGroup.SetReadableFilter();

            FilterItems.ResumeLayout();
        }

        private void FilterItem_FilterChanged(object sender, EventArgs e)
        {
            //' updates the readable filter string
            SetReadableFilter();
            FilterChanged?.Invoke(sender, e);
        }

        private void FilterMenu_SelectionChanged(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e)
        {
            switch (e.NewSelectedIndex)
            {
                case (int)KryptonOutlookGridFilterItemGroupMenuButton.Items.AndItem:
                case (int)KryptonOutlookGridFilterItemMenuButton.Items.OrItem:
                    GroupSelectedAndOr?.Invoke(this, e);
                    break;

                case (int)KryptonOutlookGridFilterItemGroupMenuButton.Items.EndItem:
                    GroupSelectedEnd?.Invoke(this, e);
                    break;
            }
        }

        private void FilterMenu_SelectionChanging(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e)
        {
            switch (e.NewSelectedIndex)
            {
                case (int)KryptonOutlookGridFilterItemGroupMenuButton.Items.Delete:
                    e.Cancel = true;
                    GroupSelectedDelete?.Invoke(this, e);
                    break;

                case (int)KryptonOutlookGridFilterItemGroupMenuButton.Items.Insert:
                    e.Cancel = true;
                    GroupSelectedInsert?.Invoke(this, e);
                    break;
            }
        }

        #endregion Control Events

        #region Public Methods

        public List<KryptonOutlookGridFilterField> FillData()
        {
            List<KryptonOutlookGridFilterField> FilterData = [];
            foreach (IKryptonOutlookGridFilterItem filterItem in FilterItems.Controls)
            {
                if (filterItem is KryptonOutlookGridFilterItemGroup group)
                {
                    var data = group.FillData();
                    FilterData.AddRange(data);
                }
                else
                {
                    KryptonOutlookGridFilterField field = filterItem.FieldValue;
                    if (field != null)
                    {
                        field.GroupInfo = this.Title.Replace("Group ", "").Replace("Main Group", "").Trim();
                        field.GroupConjunction = this.FilterGroupMenu.Item.GetDescription();
                        field.GroupConjunctionItem = (int)this.FilterGroupMenu.Item;
                        FilterData.Add(field);
                    }
                }
            }
            return FilterData;
        }

        public void SetGroupEndOperator(List<KryptonOutlookGridFilterField> fields)
        {
            //Set end of group menu button
            foreach (IKryptonOutlookGridFilterItem filterItem in FilterItems.Controls)
            {
                if (filterItem is KryptonOutlookGridFilterItemGroup group)
                {
                    string grpText = group.Title.Replace("Group ", "");
                    var field = fields.Where(f => f.GroupInfo == grpText).FirstOrDefault();
                    if (field != null)
                    {
                        group.FilterGroupMenu.Item = (KryptonOutlookGridFilterItemGroupMenuButton.Items)field.GroupConjunctionItem;
                        group.FilterGroupMenu.Text = group.FilterGroupMenu.Item.GetDescription();
                    }
                    group.SetGroupEndOperator(fields);
                }
            }
        }

        public bool AddFilterItem(KryptonOutlookGridFilterItem item)
        {
            item.SelectedAndOr += FilterItem_SelectedAndOr;
            item.SelectedEnd += FilterItem_SelectedEnd;
            item.SelectedDelete += FilterItem_SelectedDelete;
            item.SelectedInsert += FilterItem_SelectedInsert;
            item.SelectedMakeSubgroup += FilterItem_SelectedMakeSubgroup;
            item.FilterChanged += FilterItem_FilterChanged;
            FilterItems.SuspendLayout();
            FilterItems.Controls.Add(item);
            FilterItems.Controls.SetChildIndex(item, FilterItems.Controls.Count - 1);

            SetReadableFilter();
            FilterItems.ResumeLayout();

            return true;
        }

        public KryptonOutlookGridFilterItemGroup MakeSubGroup(KryptonOutlookGridFilterItem item)
        {
            FilterItems.SuspendLayout();

            //' converts the filterItem to a filterItemGroup
            KryptonOutlookGridFilterItemGroup newGroup = ConvertToSubgroup(item, false);
            newGroup.FilterChanged += FilterChanged;
            //' resets all of the group titles
            SetGroupTitles();

            //' sets the readable filter to reflect the changes to the group titles
            SetReadableFilter();
            newGroup.SetReadableFilter();

            FilterItems.ResumeLayout();
            return newGroup;
        }

        public KryptonOutlookGridFilterItemGroup ConvertToSubgroup(KryptonOutlookGridFilterItem item, bool changeSelectedMenuItem = true)
        {
            //FilterItemGroup newGroup = new(this.Columns); //' A new group of the controls currently in the group
            KryptonOutlookGridFilterItemGroup newGroup = new(Columns); //' A new group of the controls currently in the group
            int itemIndex = FilterItems.Controls.IndexOf(item); //' Where to add the new group

            SuspendLayout();

            //' Remove handlers of the filter item from this group
            item.SelectedAndOr -= FilterItem_SelectedAndOr;
            item.SelectedDelete -= FilterItem_SelectedDelete;
            item.SelectedEnd -= FilterItem_SelectedEnd;
            item.SelectedInsert -= FilterItem_SelectedInsert;
            item.SelectedMakeSubgroup -= FilterItem_SelectedMakeSubgroup;
            item.FilterChanged -= FilterItem_FilterChanged;

            if (changeSelectedMenuItem)
            {
                //' Sets the menu selection for the new group to whatever the item had selected
                newGroup.SelectedMenuItem = item.SelectedMenuItem;
                //' Sets the menu button of the item
                item.SelectedMenuItem = KryptonOutlookGridFilterItemMenuButton.Items.EndItem;
            }

            //' Add the item to the new group
            newGroup.AddFilterItem(item);
            //item.Group = newGroup;

            //' Add handlers to the new group
            newGroup.GroupSelectedAndOr += FilterItem_SelectedAndOr;
            newGroup.GroupSelectedDelete += FilterItem_SelectedDelete;
            newGroup.GroupSelectedEnd += FilterItem_SelectedEnd;
            newGroup.GroupSelectedInsert += FilterItem_SelectedInsert;
            //xxx newGroup.GroupSelectedMakeSubGroup += FilterItem_SelectedMakeSubgroup;

            //' Adds two new subgroups - one containing all the old filter items, and one for the new subgroup
            FilterItems.Controls.Add(newGroup);
            FilterItems.Controls.SetChildIndex(newGroup, itemIndex);

            ResumeLayout();

            return newGroup;
        }

        public void SetReadableFilter()
        {
            string readableFilter = "";
            string currentFilter;
            string lastConjunction = "";

            foreach (IKryptonOutlookGridFilterItem item in FilterItems.Controls)
            {
                currentFilter = item.ReadableFilter;
                if (currentFilter.Length > 0)
                {
                    readableFilter += " " + lastConjunction + " " + currentFilter;
                    lastConjunction = item.Conjunction;
                }
            }

            if (readableFilter.Length > 0)
            {
                GroupFilter.Text = readableFilter.Trim();
            }
            else
            {
                GroupFilter.Text = "The filter for " + Title + " will go here";
            }
        }

        public void ClearFilterItems()
        {
            FilterItems.SuspendLayout();
            FilterItems.Controls.Clear();

            AddFilterItem(new KryptonOutlookGridFilterItem(_columns));

            FilterItems.ResumeLayout();
        }

        #endregion Public Methods

        #region Private Methods

        private void RemoveControlsAfter(Control control)
        {
            //' A test that fixes something to do with event bubbling (I think)
            if (FilterItems.Controls.Contains(control))
            {
                SuspendLayout();
                //INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of FilterItems.Controls.IndexOf(control) for every iteration:
                int tempVar = FilterItems.Controls.IndexOf(control);
                for (int i = FilterItems.Controls.Count - 1; i > tempVar; i--)
                {
                    FilterItems.Controls.RemoveAt(i);
                }
                ResumeLayout();
            }
        }

        private void SetGroupTitles()
        {
            KryptonOutlookGridFilterItemGroup group;
            int count = 1;

            foreach (object item in FilterItems.Controls)
            {
                if (item.GetType().Equals(typeof(KryptonOutlookGridFilterItemGroup)))
                {
                    group = (KryptonOutlookGridFilterItemGroup)item;

                    //' The primary group will use different naming conventions
                    if (!PrimaryGroup)
                    {
                        group.Title = this.Title + "." + count;
                    }
                    else
                    {
                        group.Title = "Group " + count;
                    }

                    count += 1;
                }
            }
        }

        #endregion Private Methods

    }
}