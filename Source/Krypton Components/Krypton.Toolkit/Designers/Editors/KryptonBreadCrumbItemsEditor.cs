#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonBreadCrumbItemsEditor : CollectionEditor
{
    #region Classes
    /// <summary>
    /// Form used for editing the KryptonBreadCrumbItems.
    /// </summary>
    protected partial class KryptonBreadCrumbItemsForm : CollectionForm
    {
        #region Types
        /// <summary>
        /// Simple class to reduce the length of declarations!
        /// </summary>
        protected class DictItemBase : Dictionary<KryptonBreadCrumbItem, KryptonBreadCrumbItem>;

        /// <summary>
        /// Act as proxy for a crumb item to control the exposed properties to the property grid.
        /// </summary>
        protected class CrumbProxy
        {
            #region Instance Fields
            private readonly KryptonBreadCrumbItem _item;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the KryptonBreadCrumbItem class.
            /// </summary>
            /// <param name="item">Item to act as proxy for.</param>
            public CrumbProxy(KryptonBreadCrumbItem item) => _item = item;

            #endregion

            #region ShortText
            /// <summary>
            /// Gets and sets the short text.
            /// </summary>
            [Category(@"Appearance")]
            public string ShortText
            {
                get => _item.ShortText;
                set => _item.ShortText = value;
            }
            #endregion

            #region LongText
            /// <summary>
            /// Gets and sets the long text.
            /// </summary>
            [Category(@"Appearance")]
            public string LongText
            {
                get => _item.LongText;
                set => _item.LongText = value;
            }
            #endregion

            #region Image
            /// <summary>
            /// Gets and sets the image.
            /// </summary>
            [Category(@"Appearance")]
            [DefaultValue(null)]
            public Image? Image
            {
                get => _item.Image;
                set => _item.Image = value;
            }
            #endregion

            #region ImageTransparentColor
            /// <summary>
            /// Gets and sets the image transparent color.
            /// </summary>
            [Category(@"Appearance")]
            [DefaultValue(typeof(Color), "Empty")]
            public Color ImageTransparentColor
            {
                get => _item.ImageTransparentColor;
                set => _item.ImageTransparentColor = value;
            }
            #endregion

            #region Tag
            /// <summary>
            /// Gets and sets user-defined data associated with the object.
            /// </summary>
            [Category(@"Data")]
            [TypeConverter(typeof(StringConverter))]
            [DefaultValue(null)]
            public object? Tag
            {
                get => _item.Tag;
                set => _item.Tag = value;
            }
            #endregion
        }

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
            public MenuTreeNode(KryptonBreadCrumbItem item)
            {
                Item = item;
                PropertyObject = item;

                Text = Item.ToString();

                // Hook into property changes
                Item.PropertyChanged += OnPropertyChanged;
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets access to the associated item.
            /// </summary>
            public KryptonBreadCrumbItem Item { get; }

            /// <summary>
            /// Gets access to object wrapper for use in the property grid.
            /// </summary>
            public object PropertyObject { get; }

            #endregion

            #region Implementation
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
            /// <param name="servicePovider">Reference to service container.</param>
            /// <param name="component">Reference to component.</param>
            public PropertyGridSite(IServiceProvider servicePovider,
                IComponent component)
            {
                _serviceProvider = servicePovider;
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
            public IContainer Container 
                //nul forgiving added since the interface defines as non-nullable
                => null!;

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
        private readonly KryptonBreadCrumbItemsEditor _editor;
        private DictItemBase _beforeItems;
        private readonly Button buttonOK;
        private readonly Button buttonCancel;
        private readonly TreeView treeView1;
        private readonly Button buttonMoveUp;
        private readonly Button buttonMoveDown;
        private readonly Button buttonAddItem;
        private readonly Button buttonDelete;
        private readonly PropertyGrid propertyGrid1;
        private readonly Label label1;
        private readonly Label label2;
        private readonly Button buttonAddChild;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbItemsForm class.
        /// </summary>
        public KryptonBreadCrumbItemsForm(KryptonBreadCrumbItemsEditor editor)
            : base(editor)
        {
            _editor = editor;

            buttonOK = new Button();
            buttonCancel = new Button();
            treeView1 = new TreeView();
            buttonMoveUp = new Button();
            buttonMoveDown = new Button();
            buttonAddItem = new Button();
            buttonDelete = new Button();
            propertyGrid1 = new PropertyGrid();
            label1 = new Label();
            label2 = new Label();
            buttonAddChild = new Button();
            SuspendLayout();
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(547, 382);
            buttonOK.Name = nameof(buttonOK);
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 9;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(12, 382);
            buttonCancel.Name = nameof(buttonCancel);
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 8;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView1.Location = new Point(12, 32);
            treeView1.Name = nameof(treeView1);
            treeView1.Size = new Size(254, 339);
            treeView1.TabIndex = 1;
            treeView1.HideSelection = false;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // buttonMoveUp
            // 
            buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMoveUp.Image = BlueArrowResources.arrow_up_blue;
            buttonMoveUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonMoveUp.Location = new Point(272, 32);
            buttonMoveUp.Name = nameof(buttonMoveUp);
            buttonMoveUp.Size = new Size(95, 28);
            buttonMoveUp.TabIndex = 2;
            buttonMoveUp.Text = "Move Up";
            buttonMoveUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonMoveUp.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMoveUp.UseVisualStyleBackColor = true;
            buttonMoveUp.Click += buttonMoveUp_Click;
            // 
            // buttonMoveDown
            // 
            buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMoveDown.Image = BlueArrowResources.arrow_down_blue;
            buttonMoveDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonMoveDown.Location = new Point(272, 66);
            buttonMoveDown.Name = nameof(buttonMoveDown);
            buttonMoveDown.Size = new Size(95, 28);
            buttonMoveDown.TabIndex = 3;
            buttonMoveDown.Text = "Move Down";
            buttonMoveDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonMoveDown.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMoveDown.UseVisualStyleBackColor = true;
            buttonMoveDown.Click += buttonMoveDown_Click;
            // 
            // buttonAddItem
            // 
            buttonAddItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddItem.Image = GenericImageResources.add;
            buttonAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddItem.Location = new Point(272, 112);
            buttonAddItem.Name = nameof(buttonAddItem);
            buttonAddItem.Size = new Size(95, 28);
            buttonAddItem.TabIndex = 4;
            buttonAddItem.Text = "Add Sibling";
            buttonAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddItem.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAddItem.UseVisualStyleBackColor = true;
            buttonAddItem.Click += buttonAddSibling_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDelete.Image = GenericImageResources.delete;
            buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonDelete.Location = new Point(272, 190);
            buttonDelete.Name = nameof(buttonDelete);
            buttonDelete.Size = new Size(95, 28);
            buttonDelete.TabIndex = 6;
            buttonDelete.Text = "Delete Item";
            buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            propertyGrid1.HelpVisible = false;
            propertyGrid1.Location = new Point(373, 32);
            propertyGrid1.Name = nameof(propertyGrid1);
            propertyGrid1.Size = new Size(249, 339);
            propertyGrid1.TabIndex = 7;
            propertyGrid1.ToolbarVisible = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(370, 13);
            label1.Name = nameof(label1);
            label1.Size = new Size(81, 13);
            label1.TabStop = false;
            label1.Text = "Item Properties";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 13);
            label2.Name = nameof(label2);
            label2.Size = new Size(142, 13);
            label2.TabStop = false;
            label2.Text = "BreadCrumbItems Collection";
            // 
            // buttonAddChild
            // 
            buttonAddChild.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddChild.Image = GenericImageResources.add;
            buttonAddChild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddChild.Location = new Point(272, 146);
            buttonAddChild.Name = nameof(buttonAddChild);
            buttonAddChild.Size = new Size(95, 28);
            buttonAddChild.TabIndex = 5;
            buttonAddChild.Text = "Add Child";
            buttonAddChild.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddChild.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAddChild.UseVisualStyleBackColor = true;
            buttonAddChild.Click += buttonAddChild_Click;
            // 
            // KryptonBreadCrumbCollectionForm
            // 
            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 414);
            ControlBox = false;
            Controls.Add(buttonAddChild);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(propertyGrid1);
            Controls.Add(buttonDelete);
            Controls.Add(buttonAddItem);
            Controls.Add(buttonMoveDown);
            Controls.Add(buttonMoveUp);
            Controls.Add(treeView1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Font = new Font(@"Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //MinimumSize = new Size(501, 296);
            MinimumSize = new Size(634, 414);
            Name = "KryptonBreadCrumbCollectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BreadCrumbItem Collection Editor";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Provides an opportunity to perform processing when a collection value has changed.
        /// </summary>
        protected override void OnEditValueChanged()
        {
            if (EditValue != null)
            {
                // Cache a lookup of all items before changes are made
                _beforeItems = CreateItemsDictionary(Items);

                // Need to link the property browser to a site otherwise Image properties cannot be
                // edited because it cannot navigate to the owning project for its resources
                propertyGrid1.Site = new PropertyGridSite(Context!, propertyGrid1);

                // Add all the top level clones
                treeView1.Nodes.Clear();
                foreach (KryptonBreadCrumbItem item in Items)
                {
                    AddMenuTreeNode(item, null);
                }

                // Expand to show all entries
                treeView1.ExpandAll();

                // Select the first node
                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0];
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }
        }
        #endregion

        #region Implementation
        private void buttonCancel_Click(object? sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.Close();
        }

        private void buttonOK_Click(object? sender, EventArgs e)
        {
            // Create an array with all the root items
            var rootItems = new object[treeView1.Nodes.Count];
            for (var i = 0; i < rootItems.Length; i++)
            {
                rootItems[i] = ((MenuTreeNode)treeView1.Nodes[i]).Item;
            }

            // Cache a lookup of all items after changes are made
            DictItemBase afterItems = CreateItemsDictionary(rootItems);

            // Update collection with new set of items
            Items = rootItems;

            // Clear down contents of tree as this form can be reused
            treeView1.Nodes.Clear();

            // Inform designer of changes in component items
            SynchronizeCollections(_beforeItems, afterItems, Context!);

            // Notify container that the value has been changed
            Context!.OnComponentChanged();
        }

        private bool ContainsNode(TreeNode node, TreeNode find)
        {
            if (node.Nodes.Contains(find))
            {
                return true;
            }
            else
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (ContainsNode(child, find))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private TreeNode? NextNode(TreeNode? currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }

            var found = false;
            return RecursiveFind(treeView1.Nodes, currentNode, ref found, true);
        }

        private TreeNode? PreviousNode(TreeNode? currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }

            var found = false;
            return RecursiveFind(treeView1.Nodes, currentNode, ref found, false);
        }

        private TreeNode? RecursiveFind(TreeNodeCollection nodes,
            TreeNode target,
            ref bool found,
            bool forward)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                TreeNode node = nodes[forward ? i : nodes.Count - 1 - i];

                // Searching forward we check the node before any child collection
                if (forward)
                {
                    if (!found)
                    {
                        found |= node == target;
                    }
                    else
                    {
                        return node;
                    }
                }

                // Do not recurse into the children if looking forwards and at the target
                if (!(found && forward))
                {
                    // Searching the child collection of nodes
                    TreeNode? findNode = RecursiveFind(node.Nodes, target, ref found, forward);

                    // If we found a node to return then return it now
                    if (findNode != null)
                    {
                        return findNode;
                    }
                    else if (found && (target != node))
                    {
                        return node;
                    }

                    // Searching backwards we check the child collection after checking the node
                    if (!forward)
                    {
                        if (!found)
                        {
                            found |= node == target;
                        }
                        else
                        {
                            return node;
                        }
                    }
                }
            }

            return null;
        }

        private void buttonMoveUp_Click(object? sender, EventArgs e)
        {
            // If we have a selected node
            MenuTreeNode node = (MenuTreeNode)treeView1.SelectedNode!;
            if (node != null)
            {
                // Find the previous node using the currently selected node
                if (PreviousNode(node) is MenuTreeNode previousNode)
                {
                    // Is the current node contained inside the next node
                    var contained = ContainsNode(previousNode, node);

                    // Remove cell from parent collection
                    MenuTreeNode parentNode = (MenuTreeNode)node.Parent!;
                    TreeNodeCollection parentCollection = node.Parent == null ? treeView1.Nodes : node.Parent.Nodes;
                    parentNode?.Item.Items.Remove(node.Item);
                    parentCollection.Remove(node);

                    if (contained)
                    {
                        // Add cell to the parent of target node
                        var previousParent = previousNode.Parent as MenuTreeNode;
                        parentCollection = previousNode.Parent == null ? treeView1.Nodes : previousNode.Parent.Nodes;
                        var pageIndex = parentCollection.IndexOf(previousNode);

                        // If the current and previous nodes are inside the same common node
                        if (!contained
                            && (previousParent != null)
                            && (previousParent != parentNode)
                           )
                        {
                            // If the page is the last one in the collection then we need to insert afterwards
                            if (pageIndex == (previousParent.Nodes.Count - 1))
                            {
                                pageIndex++;
                            }
                        }

                        previousParent?.Item.Items.Insert(pageIndex, node.Item);
                        parentCollection.Insert(pageIndex, node);
                    }
                    else
                    {
                        parentNode = previousNode;
                        parentNode.Item.Items.Insert(parentNode.Nodes.Count, node.Item);
                        parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                    }
                }
            }

            // Ensure the target node is still selected
            treeView1.SelectedNode = node;

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonMoveDown_Click(object? sender, EventArgs e)
        {
            // If we have a selected node
            var node = treeView1.SelectedNode as MenuTreeNode;
            if (node != null)
            {
                // Find the next node using the currently selected node
                if (NextNode(node) is MenuTreeNode nextNode)
                {
                    // Is the current node contained inside the next node
                    var contained = ContainsNode(nextNode, node);

                    // Remove cell from parent collection
                    var parentNode = node.Parent as MenuTreeNode;
                    TreeNodeCollection parentCollection = node.Parent == null ? treeView1.Nodes : node.Parent.Nodes;
                    parentNode?.Item.Items.Remove(node.Item);
                    parentCollection.Remove(node);

                    if (contained)
                    {
                        // Add cell to the parent sequence of target cell
                        MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent!;
                        parentCollection = nextNode.Parent == null ? treeView1.Nodes : nextNode.Parent.Nodes;
                        var pageIndex = parentCollection.IndexOf(nextNode);
                        previousParent?.Item.Items.Insert(pageIndex + 1, node.Item);
                        parentCollection.Insert(pageIndex + 1, node);
                    }
                    else
                    {
                        parentNode = nextNode;
                        parentNode.Item.Items.Insert(0, node.Item);
                        parentNode.Nodes.Insert(0, node);
                    }
                }
            }

            // Ensure the target node is still selected
            treeView1.SelectedNode = node;

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonAddSibling_Click(object? sender, EventArgs e)
        {
            KryptonBreadCrumbItem item = (KryptonBreadCrumbItem)CreateInstance(typeof(KryptonBreadCrumbItem));
            TreeNode newNode = new MenuTreeNode(item);
            TreeNode selectedNode = treeView1.SelectedNode!;

            // If there is no selection then append to root
            if (selectedNode == null)
            {
                treeView1.Nodes.Add(newNode);
            }
            else
            {
                // If current selection is at the root
                TreeNode parentNode = selectedNode.Parent!;
                if (parentNode == null)
                {
                    treeView1.Nodes.Insert(treeView1.Nodes.IndexOf(selectedNode) + 1, newNode);
                }
                else
                {
                    var parentMenu = (MenuTreeNode)parentNode;
                    parentMenu.Item.Items.Insert(parentNode.Nodes.IndexOf(selectedNode) + 1, item);
                    parentNode.Nodes.Insert(parentNode.Nodes.IndexOf(selectedNode) + 1, newNode);
                }
            }

            // Select the newly added node
            if (newNode != null)
            {
                treeView1.SelectedNode = newNode;
                treeView1.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonAddChild_Click(object? sender, EventArgs e)
        {
            var item = (KryptonBreadCrumbItem)CreateInstance(typeof(KryptonBreadCrumbItem));
            TreeNode newNode = new MenuTreeNode(item);
            TreeNode selectedNode = treeView1.SelectedNode!;

            // If there is no selection then append to root
            if (selectedNode == null)
            {
                treeView1.Nodes.Add(newNode);
            }
            else
            {
                var selectedMenu = (MenuTreeNode)selectedNode;
                selectedMenu.Item.Items.Add(item);
                selectedNode.Nodes.Add(newNode);
            }

            // Select the newly added node
            if (newNode != null)
            {
                treeView1.SelectedNode = newNode;
                treeView1.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonDelete_Click(object? sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode!;

            // We should have a selected node!
            if (node != null)
            {
                MenuTreeNode treeNode = (MenuTreeNode)node;

                // If at root level then remove from root, otherwise from the parent collection
                if (node.Parent == null)
                {
                    treeView1.Nodes.Remove(node);
                }
                else
                {
                    TreeNode parentNode = node.Parent;
                    MenuTreeNode treeParentNode = (MenuTreeNode)parentNode;
                    treeParentNode.Item.Items.Remove(treeNode.Item);
                    node.Parent.Nodes.Remove(node);
                }

                treeView1.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void treeView1_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void UpdateButtons()
        {
            var node = treeView1.SelectedNode as MenuTreeNode;
            buttonMoveUp.Enabled = (node != null) && (PreviousNode(node) != null);
            buttonMoveDown.Enabled = (node != null) && (NextNode(node) != null);
            buttonDelete.Enabled = node != null;
        }

        private void UpdatePropertyGrid()
        {
            TreeNode node = treeView1.SelectedNode!;
            propertyGrid1.SelectedObject = node == null ? null : new CrumbProxy((KryptonBreadCrumbItem)((MenuTreeNode)node).PropertyObject);
        }

        private DictItemBase CreateItemsDictionary(object[] items)
        {
            var dictItems = new DictItemBase();

            foreach (KryptonBreadCrumbItem item in items.Cast<KryptonBreadCrumbItem>())
            {
                AddItemsToDictionary(dictItems, item);
            }

            return dictItems;
        }

        private void AddItemsToDictionary(DictItemBase dictItems, KryptonBreadCrumbItem baseItem)
        {
            // Add item to the dictionary
            dictItems.Add(baseItem, baseItem);

            // Add children of an items collection
            foreach (KryptonBreadCrumbItem item in baseItem.Items)
            {
                AddItemsToDictionary(dictItems, item);
            }
        }

        private void AddMenuTreeNode(KryptonBreadCrumbItem item, MenuTreeNode? parent)
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
                treeView1.Nodes.Add(node);
            }

            // Add children of an items collection
            foreach (KryptonBreadCrumbItem child in item.Items)
            {
                AddMenuTreeNode(child, node);
            }
        }

        private void SynchronizeCollections(DictItemBase before,
            DictItemBase after,
            ITypeDescriptorContext context)
        {
            // Add all new components (in the 'after' but not the 'before'
            foreach (KryptonBreadCrumbItem item in after.Values)
            {
                if (!before.ContainsKey(item))
                {
                    context.Container?.Add(item as IComponent);
                }
            }

            // Delete all old components (in the 'before' but not the 'after'
            foreach (KryptonBreadCrumbItem item in before.Values)
            {
                if (!after.ContainsKey(item))
                {
                    DestroyInstance(item);

                    context.Container?.Remove(item as IComponent);
                }
            }

            if (GetService(typeof(IComponentChangeService)) is IComponentChangeService changeService)
            {
                // Mark components as changed when not added or removed
                foreach (KryptonBreadCrumbItem item in after.Values)
                {
                    if (before.ContainsKey(item))
                    {
                        changeService.OnComponentChanging(item, null);
                        changeService.OnComponentChanged(item, null, null, null);
                    }
                }
            }
        }
        #endregion
    }
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonBreadCrumbItemsEditor class.
    /// </summary>
    public KryptonBreadCrumbItemsEditor()
        : base(typeof(KryptonBreadCrumbItem.BreadCrumbItems))
    {
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates a new form to display and edit the current collection.
    /// </summary>
    /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
    protected override CollectionForm CreateCollectionForm() => new KryptonBreadCrumbItemsForm(this);

    #endregion
}