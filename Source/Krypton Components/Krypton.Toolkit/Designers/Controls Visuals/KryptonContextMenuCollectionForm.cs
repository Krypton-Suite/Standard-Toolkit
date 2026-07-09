#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonContextMenuCollectionEditor
{
    #region Classes
    /// <summary>
    /// Form used for editing the KryptonContextMenuCollection.
    /// </summary>
    protected partial class KryptonContextMenuCollectionForm : KryptonDesignerCollectionForm
    {
        #region Types
        /// <summary>
        /// Simple class to reduce the length of declarations!
        /// </summary>
        protected class DictItemBase : Dictionary<KryptonContextMenuItemBase, KryptonContextMenuItemBase>;

        /// <summary>
        /// Tree node that is attached to a context menu item.
        /// </summary>
        protected class MenuTreeNode : TreeNode
        {
            #region Identity
            /// <summary>
            /// Initialize a new instance of the MenuTreeNode class.
            /// </summary>
            /// <param name="item">Menu item to represent.</param>
            public MenuTreeNode([DisallowNull] KryptonContextMenuItemBase item)
            {
                Debug.Assert(item != null);
                Item = item!;
                PropertyObject = item!;

                // Setup the initial starting image and description strings
                ImageIndex = ImageIndexFromItem();
                SelectedImageIndex = ImageIndex;
                Text = Item.ToString();

                // Hook into property changes
                Item.PropertyChanged += OnPropertyChanged;
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets access to the associated item.
            /// </summary>
            public KryptonContextMenuItemBase Item { get; }

            /// <summary>
            /// Gets access to object wrapper for use in the property grid.
            /// </summary>
            public object PropertyObject { get; }

            #endregion

            #region Implementation
            private int ImageIndexFromItem()
            {
                return Item switch
                {
                    KryptonContextMenuColorColumns _ => 0,
                    KryptonContextMenuHeading _ => 1,
                    KryptonContextMenuItem _ => 2,
                    KryptonContextMenuItems _ => 3,
                    KryptonContextMenuSeparator _ => 4,
                    KryptonContextMenuRadioButton _ => 5,
                    KryptonContextMenuCheckBox _ => 6,
                    KryptonContextMenuCheckButton _ => 7,
                    KryptonContextMenuLinkLabel _ => 8,
                    //imageList.Images.SetKeyName(9, "delete2.png");
                    //imageList.Images.SetKeyName(10, "arrow_up_blue.png");
                    //imageList.Images.SetKeyName(11, "arrow_down_blue.png");
                    KryptonContextMenuImageSelect _ => 12,
                    KryptonContextMenuMonthCalendar _ => 13,
                    KryptonContextMenuComboBox _ => 14,
                    KryptonContextMenuTextBox _ => 15,
                    KryptonContextMenuProgressBar _ => 16,
                    _ => 2
                };
            }

            private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
                // Update with correct string for new state
                Text = Item.ToString();
            #endregion
        }

        /// <summary>
        /// Site that allows the property grid to discover Visual Studio services.
        /// </summary>
        protected class PropertyGridSite : ISite, IServiceProvider
        {
            #region Instance Fields

            private readonly IServiceProvider? _serviceProvider;
            private bool _inGetService;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the PropertyGridSite.
            /// </summary>
            /// <param name="serviceProvider">Reference to service container.</param>
            /// <param name="component">Reference to component.</param>
            public PropertyGridSite(IServiceProvider serviceProvider,
                IComponent component)
            {
                _serviceProvider = serviceProvider;
                Component = component;
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets the service object of the specified type. 
            /// </summary>
            /// <param name="t">An object that specifies the type of service object to get. </param>
            /// <returns>A service object of type serviceType; or null reference if there is no service object of type serviceType.</returns>
            public object? GetService(Type t)
            {
                if (!_inGetService && (_serviceProvider != null))
                {
                    try
                    {
                        _inGetService = true;
                        return _serviceProvider.GetService(t);
                    }
                    finally
                    {
                        _inGetService = false;
                    }
                }

                return null;
            }

            /// <summary>
            /// Gets the component associated with the ISite when implemented by a class.
            /// </summary>
            public IComponent Component { get; }

            /// <summary>
            /// Gets the IContainer associated with the ISite when implemented by a class.
            /// </summary>
            public IContainer? Container => null;

            /// <summary>
            /// Determines whether the component is in design mode when implemented by a class.
            /// </summary>
            public bool DesignMode => false;

            /// <summary>
            /// Gets or sets the name of the component associated with the ISite when implemented by a class.
            /// </summary>
            public string? Name
            {
                get => null;
                set { }
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private DictItemBase _beforeItems;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuCollectionForm class.
        /// </summary>
        public KryptonContextMenuCollectionForm(KryptonDesignerCollectionEditor editor)
            : base(editor)
        {
            InitializeComponent();
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Provides an opportunity to perform processing when a collection value has changed.
        /// </summary>
        protected override void OnEditValueChanged()
        {
            if (EditValue is null || Items is null)
            {
                return;
            }

            // Cache a lookup of all items before changes are made
            _beforeItems = CreateItemsDictionary(Items);

            // Need to link the property browser to a site otherwise Image properties cannot be
            // edited because it cannot navigate to the owning project for its resources
            _propertyGrid1.Site = new PropertyGridSite(Context!, _propertyGrid1);
            ApplyOwnerPalette();

            // Add all the top level clones
            _treeView.Nodes.Clear();
            foreach (KryptonContextMenuItemBase item in Items)
            {
                AddMenuTreeNode(item, null);
            }

            // Expand to show all entries
            _treeView.ExpandAll();

            // Select the first node
            if (_treeView.Nodes.Count > 0)
            {
                _treeView.SelectedNode = _treeView.Nodes[0];
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }
        #endregion

        #region Implementation
        private static void ConfigureImageListButton(KryptonButton button, ImageList imageList, int imageIndex,
            string text, EventHandler click)
        {
            button.ButtonStyle = ButtonStyle.ListItem;
            button.Values.Image = imageList.Images[imageIndex];
            button.Values.Text = text;
            button.Click += click;
        }

        private void ApplyOwnerPalette() => ApplyOwnerPaletteFromContext();

        private void buttonCancel_Click(object? sender, EventArgs e)
        {
            // Inform designer of changes in component items
            SynchronizeCollections(_beforeItems, _beforeItems, Context!);

            // Notify container that the value has been changed
            Context!.OnComponentChanged();

            _treeView.Nodes.Clear();
        }

        private void buttonOK_Click(object? sender, EventArgs e)
        {
            // Create an array with all the root items
            var rootItems = new object[_treeView.Nodes.Count];
            for (var i = 0; i < rootItems.Length; i++)
            {
                rootItems[i] = ((MenuTreeNode)_treeView.Nodes[i]).Item;
            }

            // Cache a lookup of all items after changes are made
            DictItemBase afterItems = CreateItemsDictionary(rootItems);

            // Update collection with new set of items
            Items = rootItems;

            // Clear down contents of tree as this form can be reused
            _treeView.Nodes.Clear();

            // Inform designer of changes in component items
            SynchronizeCollections(_beforeItems, afterItems, Context!);

            // Notify container that the value has been changed
            Context!.OnComponentChanged();
        }

        private void buttonMoveUp_Click(object? sender, EventArgs e)
        {
            // We should have a selected node!
            if (_treeView.SelectedNode is MenuTreeNode node)
            {
                var treeNode = (MenuTreeNode)node;

                // If at the root level then move up in the root items collection
                if (node.Parent is null)
                {
                    var index = _treeView.Nodes.IndexOf(node);
                    _treeView.Nodes.Remove(node);
                    _treeView.Nodes.Insert(index - 1, node);
                }
                else
                {
                    var index = node.Parent.Nodes.IndexOf(node);
                    TreeNode parentNode = node.Parent;
                    var treeParentNode = (MenuTreeNode)parentNode;

                    switch (treeParentNode?.Item)
                    {
                        case KryptonContextMenuItems items1:
                            items1.Items.Remove(treeNode.Item);
                            items1.Items.Insert(index - 1, treeNode.Item);
                            break;

                        case KryptonContextMenuItem items:
                            items.Items.Remove(treeNode.Item);
                            items.Items.Insert(index - 1, treeNode.Item);
                            break;
                    }

                    parentNode.Nodes.Remove(node);
                    parentNode.Nodes.Insert(index - 1, node);
                }

                _treeView.SelectedNode = node;
                _treeView.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonMoveDown_Click(object? sender, EventArgs e)
        {
            // We should have a selected node!
            if (_treeView.SelectedNode is MenuTreeNode node)
            {
                var treeNode = (MenuTreeNode)node;

                // If at the root level then move down in the root items collection
                if (node.Parent == null)
                {
                    var index = _treeView.Nodes.IndexOf(node);
                    _treeView.Nodes.Remove(node);
                    _treeView.Nodes.Insert(index + 1, node);
                }
                else
                {
                    var index = node.Parent.Nodes.IndexOf(node);
                    TreeNode parentNode = node.Parent;
                    var treeParentNode = parentNode as MenuTreeNode;

                    switch (treeParentNode?.Item)
                    {
                        case KryptonContextMenuItems items1:
                            items1.Items.Remove(treeNode.Item);
                            items1.Items.Insert(index + 1, treeNode.Item);
                            break;

                        case KryptonContextMenuItem items:
                            items.Items.Remove(treeNode.Item);
                            items.Items.Insert(index + 1, treeNode.Item);
                            break;
                    }

                    parentNode.Nodes.Remove(node);
                    parentNode.Nodes.Insert(index + 1, node);
                }

                _treeView.SelectedNode = node;
                _treeView.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonAddItem_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItem)));

        private void buttonAddItems_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItems)));

        private void buttonAddHeading_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuHeading)));

        private void buttonAddMonthCalendar_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuMonthCalendar)));

        private void buttonAddSeparator_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuSeparator)));

        private void buttonAddCheckBox_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckBox)));

        private void buttonAddCheckButton_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckButton)));

        private void buttonAddRadioButton_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuRadioButton)));

        private void buttonAddLinkLabel_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuLinkLabel)));

        private void buttonAddColorColumns_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuColorColumns)));

        private void buttonAddImageSelect_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuImageSelect)));

        private void buttonAddComboBox_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuComboBox)));

        private void buttonAddProgressBar_Click(object? sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuProgressBar)));

        private void buttonDelete_Click(object? sender, EventArgs e)
        {
            // We should have a selected node!
            if (_treeView.SelectedNode is MenuTreeNode node)
            {
                var treeNode = (MenuTreeNode)node;

                // If at root level then remove from root, otherwise from the parent collection
                if (node.Parent == null)
                {
                    _treeView.Nodes.Remove(node);
                }
                else
                {
                    TreeNode parentNode = node.Parent;
                    var treeParentNode = parentNode as MenuTreeNode;

                    switch (treeParentNode?.Item)
                    {
                        case KryptonContextMenuItems items1:
                            items1.Items.Remove(treeNode.Item);
                            break;

                        case KryptonContextMenuItem items:
                            items.Items.Remove(treeNode.Item);
                            break;
                    }

                    node.Parent.Nodes.Remove(node);
                }

                _treeView.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void SelectionChanged(object? sender, TreeViewEventArgs e)
        {
            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void UpdatePropertyGrid()
        {
            if (_treeView.SelectedNode is MenuTreeNode node)
            {
                var propertyObject = ((MenuTreeNode)node)?.PropertyObject!;
                _propertyGrid1.SelectedObject = propertyObject;
            }
        }

        private void AddMenuTreeNode(KryptonContextMenuItemBase item, MenuTreeNode? parent)
        {
            // Create a node to match the item
            var node = new MenuTreeNode(item);

            // Add to either root or parent node
            if (parent != null)
            {
                parent.Nodes.Add(node);
            }
            else
            {
                _treeView.Nodes.Add(node);
            }

            // Check for items that can contain collections of children
            switch (item)
            {
                case KryptonContextMenuItems itemsCollection1:
                    foreach (KryptonContextMenuItemBase child in itemsCollection1.Items)
                    {
                        AddMenuTreeNode(child, node);
                    }
                    break;

                case KryptonContextMenuItem itemsCollection:
                    foreach (KryptonContextMenuItemBase child in itemsCollection.Items)
                    {
                        AddMenuTreeNode(child, node);
                    }
                    break;
            }
        }

        private void AddNewItem(KryptonContextMenuItemBase item)
        {
            TreeNode? selectedNode = _treeView.SelectedNode;
            TreeNode newNode = new MenuTreeNode(item);

            // If there is no selection then append to root
            if (selectedNode == null)
            {
                _treeView.Nodes.Add(newNode);
            }
            else
            {
                // If current selection is at the root
                if (selectedNode.Parent == null)
                {
                    // If adding a menu item to an items
                    if (item is KryptonContextMenuItem)
                    {
                        var treeSelectedNode = selectedNode as MenuTreeNode;
                        var items = treeSelectedNode?.Item as KryptonContextMenuItems;
                        items!.Items.Add(item);
                        selectedNode.Nodes.Add(newNode);
                    }
                    else
                    {
                        var index = _treeView.Nodes.IndexOf(selectedNode);
                        _treeView.Nodes.Insert(index + 1, newNode);
                    }
                }
                else
                {
                    var index = selectedNode.Parent.Nodes.IndexOf(selectedNode);
                    TreeNode parentNode = selectedNode.Parent;
                    var treeParentNode = parentNode as MenuTreeNode;

                    switch (treeParentNode?.Item)
                    {
                        case KryptonContextMenuItems items1:
                            if (ValidInItemCollection(item))
                            {
                                items1.Items.Insert(index + 1, item);
                                selectedNode.Parent.Nodes.Insert(index + 1, newNode);
                            }
                            else
                            {
                                var treeSelectedNode = (MenuTreeNode)selectedNode;
                                Debug.Assert(treeSelectedNode.Item is KryptonContextMenuItem);
                                var items = (KryptonContextMenuItem)treeSelectedNode.Item;
                                items!.Items.Add(item);
                                selectedNode.Nodes.Add(newNode);
                            }
                            break;

                        case KryptonContextMenuItem items2:
                            if (ValidInCollection(item))
                            {
                                items2.Items.Insert(index + 1, item);
                                selectedNode.Parent.Nodes.Insert(index + 1, newNode);
                            }
                            else
                            {
                                var treeSelectedNode = (MenuTreeNode)selectedNode;
                                Debug.Assert(treeSelectedNode.Item is KryptonContextMenuItems);
                                var items = treeSelectedNode.Item as KryptonContextMenuItems;
                                items!.Items.Add(item);
                                selectedNode.Nodes.Add(newNode);
                            }
                            break;
                    }
                }
            }

            // Select the newly added node
            _treeView.SelectedNode = newNode;
            _treeView.Focus();

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            KryptonContextMenuItemBase? item = null;
            KryptonContextMenuItemBase? parent = null;
            var parentNodeCount = _treeView.Nodes.Count;
            var nodeIndex = -1;

            // ReSharper disable once UsePatternMatching
            MenuTreeNode? node = _treeView.SelectedNode as MenuTreeNode;
            if (node != null)
            {
                item = node.Item;
                nodeIndex = _treeView.Nodes.IndexOf(node);
                if (node.Parent != null)
                {
                    parentNodeCount = node.Parent.Nodes.Count;
                    nodeIndex = node.Parent.Nodes.IndexOf(node);
                    node = node.Parent as MenuTreeNode;
                    if (node != null)
                    {
                        parent = node.Item;
                    }
                }
            }

            _buttonMoveUp.Enabled = (item != null) && (nodeIndex > 0);
            _buttonMoveDown.Enabled = (item != null) && (nodeIndex < (parentNodeCount - 1));
            _buttonAddItem.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuItem));
            _buttonAddItems.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuItems));
            _buttonAddSeparator.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuSeparator));
            _buttonAddHeading.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuHeading));
            _buttonAddMonthCalendar.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuMonthCalendar));
            _buttonAddCheckBox.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuCheckBox));
            _buttonAddCheckButton.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuCheckButton));
            _buttonAddRadioButton.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuRadioButton));
            _buttonAddLinkLabel.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuLinkLabel));
            _buttonAddColorColumns.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuColorColumns));
            _buttonAddImageSelect.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuImageSelect));
            _buttonDelete.Enabled = item != null;
        }

        private bool AllowAddItem(KryptonContextMenuItemBase? item,
            KryptonContextMenuItemBase? parent,
            Type addType)
        {
            // Special case: you can use add button on an Items collection so it adds an item inside it
            if ((item is KryptonContextMenuItems) && addType.Equals(typeof(KryptonContextMenuItem)))
            {
                return true;
            }

            if (ItemInsideCollection(item, parent))
            {
                var temp = new KryptonContextMenuCollection();
                return temp.RestrictTypes.Any(t => t.Equals(addType));
            }

            var temp1 = new KryptonContextMenuItemCollection();
            if (temp1.RestrictTypes.Any(t => t.Equals(addType)))
            {
                return true;
            }

            if (item is KryptonContextMenuItem)
            {
                var temp2 = new KryptonContextMenuCollection();
                return temp2.RestrictTypes.Any(t => t.Equals(addType));
            }

            return false;
        }

        private bool ValidInCollection(KryptonContextMenuItemBase item)
        {
            Type addType = item.GetType();
            var temp = new KryptonContextMenuCollection();
            return temp.RestrictTypes.Any(t => t.Equals(addType));
        }

        private bool ValidInItemCollection(KryptonContextMenuItemBase item)
        {
            Type addType = item.GetType();
            var temp = new KryptonContextMenuItemCollection();
            return temp.RestrictTypes.Any(t => t.Equals(addType));
        }

        private bool ItemInsideCollection(KryptonContextMenuItemBase? item,
            KryptonContextMenuItemBase? parent) =>
            // If it has no parent then it must be inside a collection
            // If inside an items then not inside a collection
            parent is not KryptonContextMenuItems;

        private DictItemBase CreateItemsDictionary(object[] items)
        {
            var dictItems = new DictItemBase();

            foreach (KryptonContextMenuItemBase item in items.Cast<KryptonContextMenuItemBase>())
            {
                AddItemsToDictionary(dictItems, item);
            }

            return dictItems;
        }

        private void AddItemsToDictionary(DictItemBase dictItems, KryptonContextMenuItemBase baseItem)
        {
            // Add item to the dictionary
            dictItems.Add(baseItem, baseItem);

            switch (baseItem)
            {
                // Add children of an items collection
                case KryptonContextMenuItems items:
                    foreach (KryptonContextMenuItemBase childItem in items.Items)
                    {
                        AddItemsToDictionary(dictItems, childItem);
                    }
                    break;

                // Add children of an item
                case KryptonContextMenuItem item:
                    foreach (KryptonContextMenuItemBase childItem in item.Items)
                    {
                        AddItemsToDictionary(dictItems, childItem);
                    }
                    break;
            }
        }

        private void SynchronizeCollections(DictItemBase before,
            DictItemBase after,
            ITypeDescriptorContext context)
        {
            // Add all new components (in the 'after' but not the 'before'
            foreach (KryptonContextMenuItemBase item in after.Values.Where(item => !before.ContainsKey(item)))
            {
                context.Container?.Add(item);
            }

            // Delete all old components (in the 'before' but not the 'after'
            foreach (KryptonContextMenuItemBase item in before.Values.Where(item => !after.ContainsKey(item)))
            {
                DestroyInstance(item);

                context.Container?.Remove(item);
            }

            if (GetService(typeof(IComponentChangeService)) is IComponentChangeService changeService)
            {
                // Mark components as changed when not added or removed
                foreach (KryptonContextMenuItemBase item in after.Values.Where(before.ContainsKey))
                {
                    changeService.OnComponentChanging(item, null);
                    changeService.OnComponentChanged(item, null, null, null);
                }
            }
        }
        #endregion

    }
    #endregion
}