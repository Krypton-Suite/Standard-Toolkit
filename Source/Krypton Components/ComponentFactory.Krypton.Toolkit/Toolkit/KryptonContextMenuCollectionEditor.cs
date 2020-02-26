// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2020, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Designer for a collection of context menu items.
    /// </summary>
    public class KryptonContextMenuCollectionEditor : CollectionEditor
    {
        #region Classes
        /// <summary>
        /// Form used for editing the KryptonContextMenuCollection.
        /// </summary>
        protected partial class KryptonContextMenuCollectionForm : CollectionForm
        {
            #region Types
            /// <summary>
            /// Simple class to reduce the length of declarations!
            /// </summary>
            protected class DictItemBase : Dictionary<KryptonContextMenuItemBase, KryptonContextMenuItemBase> { };

            /// <summary>
            /// Tree node that is attached to a context menu item.
            /// </summary>
            protected class MenuTreeNode : TreeNode
            {
                #region Instance Fields

                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the MenuTreeNode class.
                /// </summary>
                /// <param name="item">Menu item to represent.</param>
                public MenuTreeNode(KryptonContextMenuItemBase item)
                {
                    Debug.Assert(item != null);
                    Item = item;
                    PropertyObject = item;

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
                    switch (Item)
                    {
                        case KryptonContextMenuCheckBox _:
                            return 6;
                        case KryptonContextMenuCheckButton _:
                            return 7;
                        case KryptonContextMenuColorColumns _:
                            return 0;
                        case KryptonContextMenuHeading _:
                            return 1;
                        case KryptonContextMenuItem _:
                            return 2;
                        case KryptonContextMenuItems _:
                            return 3;
                        case KryptonContextMenuLinkLabel _:
                            return 8;
                        case KryptonContextMenuRadioButton _:
                            return 5;
                        case KryptonContextMenuSeparator _:
                            return 4;
                        case KryptonContextMenuImageSelect _:
                            return 13;
                        case KryptonContextMenuMonthCalendar _:
                            return 14;
                    }

                    Debug.Assert(false);
                    return -1;
                }

                private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    // Update with correct string for new state
                    Text = Item.ToString();
                }
                #endregion
            }

            /// <summary>
            /// Site that allows the property grid to discover Visual Studio services.
            /// </summary>
            protected class PropertyGridSite : ISite, IServiceProvider
            {
                #region Instance Fields

                private readonly IServiceProvider _serviceProvider;
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
                public object GetService(Type t)
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
                public IContainer Container => null;

                /// <summary>
                /// Determines whether the component is in design mode when implemented by a class.
                /// </summary>
                public bool DesignMode => false;

                /// <summary>
                /// Gets or sets the name of the component associated with the ISite when implemented by a class.
                /// </summary>
                public string Name
                {
                    get { return null; }
                    set { }
                }
                #endregion
            }
            #endregion

            #region Instance Fields
            private DictItemBase _beforeItems;
            private KryptonContextMenuCollectionEditor _editor;
            private readonly Button buttonOK;
            private readonly TreeView treeView;
            private readonly Label label1;
            private readonly Label label2;
            private readonly ImageList imageList;
            private readonly Button buttonDelete;
            private readonly Button buttonMoveUp;
            private readonly Button buttonMoveDown;
            private readonly Button buttonAddCheckBox;
            private readonly Button buttonAddCheckButton;
            private readonly Button buttonAddRadioButton;
            private readonly Button buttonAddLinkLabel;
            private readonly Button buttonAddSeparator;
            private readonly Button buttonAddItem;
            private readonly Button buttonAddItems;
            private readonly Button buttonAddHeading;
            private readonly Button buttonAddMonthCalendar;
            private readonly Button buttonAddColorColumns;
            private readonly Button buttonAddImageSelect;
            private readonly PropertyGrid propertyGrid1;
            private readonly IContainer components = null;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the KryptonContextMenuCollectionForm class.
            /// </summary>
            public KryptonContextMenuCollectionForm(KryptonContextMenuCollectionEditor editor)
                : base(editor)
            {
                _editor = editor;

                components = new Container();
                buttonOK = new Button();
                treeView = new TreeView();
                imageList = new ImageList(components);
                label1 = new Label();
                buttonDelete = new Button();
                buttonMoveUp = new Button();
                buttonMoveDown = new Button();
                buttonAddCheckBox = new Button();
                buttonAddCheckButton = new Button();
                buttonAddRadioButton = new Button();
                buttonAddLinkLabel = new Button();
                buttonAddSeparator = new Button();
                buttonAddItem = new Button();
                buttonAddItems = new Button();
                buttonAddHeading = new Button();
                buttonAddMonthCalendar = new Button();
                propertyGrid1 = new PropertyGrid();
                label2 = new Label();
                buttonAddColorColumns = new Button();
                buttonAddImageSelect = new Button();
                SuspendLayout();
                // 
                // buttonOK
                // 
                buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonOK.DialogResult = DialogResult.OK;
                buttonOK.Location = new Point(630, 504);
                buttonOK.Name = "buttonOK";
                buttonOK.Size = new Size(75, 23);
                buttonOK.TabIndex = 16;
                buttonOK.Text = "OK";
                buttonOK.UseVisualStyleBackColor = true;
                buttonOK.Click += buttonOK_Click;
                // 
                // treeView
                // 
                treeView.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                                   | AnchorStyles.Left)
                                  | AnchorStyles.Right;
                treeView.HideSelection = false;
                treeView.ImageIndex = 0;
                treeView.ImageList = imageList;
                treeView.Location = new Point(16, 29);
                treeView.Name = "treeView";
                treeView.SelectedImageIndex = 0;
                treeView.Size = new Size(251, 466);
                treeView.TabIndex = 0;
                treeView.AfterSelect += SelectionChanged;
                // 
                // imageList
                // 
                imageList.TransparentColor = Color.Magenta;
                imageList.Images.AddRange(new Image[]{
                    Properties.Resources.KryptonContextMenuColorColumns,
                    Properties.Resources.KryptonContextMenuHeading,
                    Properties.Resources.KryptonContextMenuItem,
                    Properties.Resources.KryptonContextMenuItems,
                    Properties.Resources.KryptonContextMenuSeparator,
                    Properties.Resources.KryptonRadioButton,
                    Properties.Resources.KryptonCheckBox,
                    Properties.Resources.KryptonCheckButton,
                    Properties.Resources.KryptonLinkLabel,
                    Properties.Resources.delete2,
                    Properties.Resources.arrow_up_blue,
                    Properties.Resources.arrow_down_blue,
                    Properties.Resources.KryptonContextMenuColorColumns,
                    Properties.Resources.KryptonContextMenuImageSelect,
                    Properties.Resources.KryptonMonthCalendar});
                imageList.Images.SetKeyName(0, "KryptonContextMenuColorColumns.bmp");
                imageList.Images.SetKeyName(1, "KryptonContextMenuHeading.bmp");
                imageList.Images.SetKeyName(2, "KryptonContextMenuItem.bmp");
                imageList.Images.SetKeyName(3, "KryptonContextMenuItems.bmp");
                imageList.Images.SetKeyName(4, "KryptonContextMenuSeparator.bmp");
                imageList.Images.SetKeyName(5, "KryptonRadioButton.bmp");
                imageList.Images.SetKeyName(6, "KryptonCheckBox.bmp");
                imageList.Images.SetKeyName(7, "KryptonCheckButton.bmp");
                imageList.Images.SetKeyName(8, "KryptonLinkLabel.bmp");
                imageList.Images.SetKeyName(9, "delete2.png");
                imageList.Images.SetKeyName(10, "arrow_up_blue.png");
                imageList.Images.SetKeyName(11, "arrow_down_blue.png");
                imageList.Images.SetKeyName(12, "KryptonContextMenuColorColumns.bmp");
                imageList.Images.SetKeyName(13, "KryptonContextMenuImageSelect.bmp");
                // 
                // label1
                // 
                label1.AutoSize = true;
                label1.Location = new Point(13, 11);
                label1.Name = "label1";
                label1.Size = new Size(75, 13);
                label1.TabIndex = 7;
                label1.Text = "Item Hierarchy";
                // 
                // buttonDelete
                // 
                buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonDelete.ImageAlign = ContentAlignment.MiddleLeft;
                buttonDelete.ImageIndex = 9;
                buttonDelete.ImageList = imageList;
                buttonDelete.Location = new Point(282, 467);
                buttonDelete.Name = "buttonDelete";
                buttonDelete.Size = new Size(144, 28);
                buttonDelete.TabIndex = 14;
                buttonDelete.Text = "Delete";
                buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonDelete.UseVisualStyleBackColor = true;
                buttonDelete.Click += buttonDelete_Click;
                // 
                // buttonMoveUp
                // 
                buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonMoveUp.ImageAlign = ContentAlignment.MiddleLeft;
                buttonMoveUp.ImageIndex = 10;
                buttonMoveUp.ImageList = imageList;
                buttonMoveUp.Location = new Point(282, 29);
                buttonMoveUp.Name = "buttonMoveUp";
                buttonMoveUp.Size = new Size(144, 28);
                buttonMoveUp.TabIndex = 1;
                buttonMoveUp.Text = "Move Up";
                buttonMoveUp.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonMoveUp.UseVisualStyleBackColor = true;
                buttonMoveUp.Click += buttonMoveUp_Click;
                // 
                // buttonMoveDown
                // 
                buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonMoveDown.ImageAlign = ContentAlignment.MiddleLeft;
                buttonMoveDown.ImageIndex = 11;
                buttonMoveDown.ImageList = imageList;
                buttonMoveDown.Location = new Point(282, 60);
                buttonMoveDown.Name = "buttonMoveDown";
                buttonMoveDown.Size = new Size(144, 28);
                buttonMoveDown.TabIndex = 2;
                buttonMoveDown.Text = "Move Down";
                buttonMoveDown.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonMoveDown.UseVisualStyleBackColor = true;
                buttonMoveDown.Click += buttonMoveDown_Click;
                // 
                // buttonAddCheckBox
                // 
                buttonAddCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddCheckBox.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddCheckBox.ImageIndex = 6;
                buttonAddCheckBox.ImageList = imageList;
                buttonAddCheckBox.Location = new Point(282, 231);
                buttonAddCheckBox.Name = "buttonAddCheckBox";
                buttonAddCheckBox.Size = new Size(144, 28);
                buttonAddCheckBox.TabIndex = 7;
                buttonAddCheckBox.Text = "Add CheckBox";
                buttonAddCheckBox.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddCheckBox.UseVisualStyleBackColor = true;
                buttonAddCheckBox.Click += buttonAddCheckBox_Click;
                // 
                // buttonAddCheckButton
                // 
                buttonAddCheckButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddCheckButton.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddCheckButton.ImageIndex = 7;
                buttonAddCheckButton.ImageList = imageList;
                buttonAddCheckButton.Location = new Point(282, 263);
                buttonAddCheckButton.Name = "buttonAddCheckButton";
                buttonAddCheckButton.Size = new Size(144, 28);
                buttonAddCheckButton.TabIndex = 8;
                buttonAddCheckButton.Text = "Add CheckButton";
                buttonAddCheckButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddCheckButton.UseVisualStyleBackColor = true;
                buttonAddCheckButton.Click += buttonAddCheckButton_Click;
                // 
                // buttonAddRadioButton
                // 
                buttonAddRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddRadioButton.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddRadioButton.ImageIndex = 5;
                buttonAddRadioButton.ImageList = imageList;
                buttonAddRadioButton.Location = new Point(282, 295);
                buttonAddRadioButton.Name = "buttonAddRadioButton";
                buttonAddRadioButton.Size = new Size(144, 28);
                buttonAddRadioButton.TabIndex = 9;
                buttonAddRadioButton.Text = "Add RadioButton";
                buttonAddRadioButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddRadioButton.UseVisualStyleBackColor = true;
                buttonAddRadioButton.Click += buttonAddRadioButton_Click;
                // 
                // buttonAddLinkLabel
                // 
                buttonAddLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddLinkLabel.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddLinkLabel.ImageIndex = 8;
                buttonAddLinkLabel.ImageList = imageList;
                buttonAddLinkLabel.Location = new Point(282, 327);
                buttonAddLinkLabel.Name = "buttonAddLinkLabel";
                buttonAddLinkLabel.Size = new Size(144, 28);
                buttonAddLinkLabel.TabIndex = 10;
                buttonAddLinkLabel.Text = "Add LinkLabel";
                buttonAddLinkLabel.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddLinkLabel.UseVisualStyleBackColor = true;
                buttonAddLinkLabel.Click += buttonAddLinkLabel_Click;
                // 
                // buttonAddSeparator
                // 
                buttonAddSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddSeparator.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddSeparator.ImageIndex = 4;
                buttonAddSeparator.ImageList = imageList;
                buttonAddSeparator.Location = new Point(282, 199);
                buttonAddSeparator.Name = "buttonAddSeparator";
                buttonAddSeparator.Size = new Size(144, 28);
                buttonAddSeparator.TabIndex = 6;
                buttonAddSeparator.Text = "Add Separator";
                buttonAddSeparator.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddSeparator.UseVisualStyleBackColor = true;
                buttonAddSeparator.Click += buttonAddSeparator_Click;
                // 
                // buttonAddItem
                // 
                buttonAddItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddItem.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddItem.ImageIndex = 2;
                buttonAddItem.ImageList = imageList;
                buttonAddItem.Location = new Point(282, 103);
                buttonAddItem.Name = "buttonAddItem";
                buttonAddItem.Size = new Size(144, 28);
                buttonAddItem.TabIndex = 3;
                buttonAddItem.Text = "Add Item";
                buttonAddItem.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddItem.UseVisualStyleBackColor = true;
                buttonAddItem.Click += buttonAddItem_Click;
                // 
                // buttonAddItems
                // 
                buttonAddItems.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddItems.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddItems.ImageIndex = 3;
                buttonAddItems.ImageList = imageList;
                buttonAddItems.Location = new Point(282, 135);
                buttonAddItems.Name = "buttonAddItems";
                buttonAddItems.Size = new Size(144, 28);
                buttonAddItems.TabIndex = 4;
                buttonAddItems.Text = "Add Items";
                buttonAddItems.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddItems.UseVisualStyleBackColor = true;
                buttonAddItems.Click += buttonAddItems_Click;
                // 
                // buttonAddHeading
                // 
                buttonAddHeading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddHeading.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddHeading.ImageIndex = 1;
                buttonAddHeading.ImageList = imageList;
                buttonAddHeading.Location = new Point(282, 167);
                buttonAddHeading.Name = "buttonAddHeading";
                buttonAddHeading.Size = new Size(144, 28);
                buttonAddHeading.TabIndex = 5;
                buttonAddHeading.Text = "Add Heading";
                buttonAddHeading.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddHeading.UseVisualStyleBackColor = true;
                buttonAddHeading.Click += buttonAddHeading_Click;
                // 
                // buttonAddMonthCalendar
                // 
                buttonAddMonthCalendar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddMonthCalendar.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddMonthCalendar.ImageIndex = 14;
                buttonAddMonthCalendar.ImageList = imageList;
                buttonAddMonthCalendar.Location = new Point(282, 423);
                buttonAddMonthCalendar.Name = "buttonAddMonthCalendar";
                buttonAddMonthCalendar.Size = new Size(144, 28);
                buttonAddMonthCalendar.TabIndex = 13;
                buttonAddMonthCalendar.Text = "Add Month Calendar";
                buttonAddMonthCalendar.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddMonthCalendar.UseVisualStyleBackColor = true;
                buttonAddMonthCalendar.Click += buttonAddMonthCalendar_Click;
                // 
                // propertyGrid1
                // 
                propertyGrid1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom)
                                       | AnchorStyles.Right;
                propertyGrid1.HelpVisible = false;
                propertyGrid1.Location = new Point(439, 29);
                propertyGrid1.Name = "propertyGrid1";
                propertyGrid1.Size = new Size(266, 466);
                propertyGrid1.TabIndex = 15;
                propertyGrid1.ToolbarVisible = false;
                // 
                // label2
                // 
                label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                label2.AutoSize = true;
                label2.Location = new Point(436, 11);
                label2.Name = "label2";
                label2.Size = new Size(77, 13);
                label2.TabIndex = 16;
                label2.Text = "Item Properties";
                // 
                // buttonAddColorColumns
                // 
                buttonAddColorColumns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddColorColumns.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddColorColumns.ImageIndex = 12;
                buttonAddColorColumns.ImageList = imageList;
                buttonAddColorColumns.Location = new Point(282, 359);
                buttonAddColorColumns.Name = "buttonAddColorColumns";
                buttonAddColorColumns.Size = new Size(144, 28);
                buttonAddColorColumns.TabIndex = 11;
                buttonAddColorColumns.Text = "Add ColorColumns";
                buttonAddColorColumns.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddColorColumns.UseVisualStyleBackColor = true;
                buttonAddColorColumns.Click += buttonAddColorColumns_Click;
                // 
                // buttonAddImageSelect
                // 
                buttonAddImageSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddImageSelect.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddImageSelect.ImageIndex = 13;
                buttonAddImageSelect.ImageList = imageList;
                buttonAddImageSelect.Location = new Point(282, 391);
                buttonAddImageSelect.Name = "buttonAddImageSelect";
                buttonAddImageSelect.Size = new Size(144, 28);
                buttonAddImageSelect.TabIndex = 12;
                buttonAddImageSelect.Text = "Add ImageSelect";
                buttonAddImageSelect.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddImageSelect.UseVisualStyleBackColor = true;
                buttonAddImageSelect.Click += buttonAddImageSelect_Click;
                // 
                // KryptonContextMenuEditorForm
                // 
                AcceptButton = buttonOK;
                AutoScaleMode = AutoScaleMode.None;
                ClientSize = new Size(717, 557);
                ControlBox = false;
                Controls.Add(buttonAddColorColumns);
                Controls.Add(buttonAddImageSelect);
                Controls.Add(label2);
                Controls.Add(propertyGrid1);
                Controls.Add(buttonAddMonthCalendar);
                Controls.Add(buttonAddHeading);
                Controls.Add(buttonAddItems);
                Controls.Add(buttonAddItem);
                Controls.Add(buttonAddSeparator);
                Controls.Add(buttonAddLinkLabel);
                Controls.Add(buttonAddRadioButton);
                Controls.Add(buttonAddCheckButton);
                Controls.Add(buttonAddCheckBox);
                Controls.Add(buttonMoveDown);
                Controls.Add(buttonMoveUp);
                Controls.Add(buttonDelete);
                Controls.Add(label1);
                Controls.Add(treeView);
                Controls.Add(buttonOK);
                MinimumSize = new Size(733, 593);
                Name = "KryptonContextMenuEditorForm";
                StartPosition = FormStartPosition.CenterScreen;
                Text = "KryptonContextMenu Items Editor";
                Load += KryptonContextMenuEditorForm_Load;
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
                    propertyGrid1.Site = new PropertyGridSite(Context, propertyGrid1);

                    // Add all the top level clones
                    treeView.Nodes.Clear();
                    foreach (KryptonContextMenuItemBase item in Items)
                    {
                        AddMenuTreeNode(item, null);
                    }

                    // Expand to show all entries
                    treeView.ExpandAll();

                    // Select the first node
                    if (treeView.Nodes.Count > 0)
                    {
                        treeView.SelectedNode = treeView.Nodes[0];
                    }

                    UpdateButtons();
                    UpdatePropertyGrid();
                }
            }
            #endregion

            #region Implementation
            private void KryptonContextMenuEditorForm_Load(object sender, EventArgs e)
            {
                // Set allowed categories into the property grid filter
                propertyGrid1.BrowsableAttributes = new AttributeCollection(new KryptonPersistAttribute());
            }

            private void buttonOK_Click(object sender, EventArgs e)
            {
                // Create an array with all the root items
                object[] rootItems = new object[treeView.Nodes.Count];
                for (int i = 0; i < rootItems.Length; i++)
                {
                    rootItems[i] = ((MenuTreeNode)treeView.Nodes[i]).Item;
                }

                // Cache a lookup of all items after changes are made
                DictItemBase afterItems = CreateItemsDictionary(rootItems);

                // Update collection with new set of items
                Items = rootItems;

                // Clear down contents of tree as this form can be reused
                treeView.Nodes.Clear();

                // Inform designer of changes in component items
                SynchronizeCollections(_beforeItems, afterItems, Context);

                // Notify container that the value has been changed
                Context.OnComponentChanged();
            }

            private void buttonMoveUp_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at the root level then move up in the root items collection
                    if (node.Parent == null)
                    {
                        int index = treeView.Nodes.IndexOf(node);
                        treeView.Nodes.Remove(node);
                        treeView.Nodes.Insert(index - 1, node);
                    }
                    else
                    {
                        int index = node.Parent.Nodes.IndexOf(node);
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        switch (treeParentNode.Item)
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

                    treeView.SelectedNode = node;
                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonMoveDown_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at the root level then move down in the root items collection
                    if (node.Parent == null)
                    {
                        int index = treeView.Nodes.IndexOf(node);
                        treeView.Nodes.Remove(node);
                        treeView.Nodes.Insert(index + 1, node);
                    }
                    else
                    {
                        int index = node.Parent.Nodes.IndexOf(node);
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        switch (treeParentNode.Item)
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

                    treeView.SelectedNode = node;
                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddItem_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItem)));
            }

            private void buttonAddItems_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItems)));
            }

            private void buttonAddHeading_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuHeading)));
            }

            private void buttonAddMonthCalendar_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuMonthCalendar)));
            }

            private void buttonAddSeparator_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuSeparator)));
            }

            private void buttonAddCheckBox_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckBox)));
            }

            private void buttonAddCheckButton_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckButton)));
            }

            private void buttonAddRadioButton_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuRadioButton)));
            }

            private void buttonAddLinkLabel_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuLinkLabel)));
            }

            private void buttonAddColorColumns_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuColorColumns)));
            }

            private void buttonAddImageSelect_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuImageSelect)));
            }

            private void buttonDelete_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at root level then remove from root, otherwise from the parent collection
                    if (node.Parent == null)
                    {
                        treeView.Nodes.Remove(node);
                    }
                    else
                    {
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        switch (treeParentNode.Item)
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

                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void SelectionChanged(object sender, TreeViewEventArgs e)
            {
                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void UpdatePropertyGrid()
            {
                TreeNode node = treeView.SelectedNode;
                propertyGrid1.SelectedObject = ((MenuTreeNode) node)?.PropertyObject;
            }

            private void AddMenuTreeNode(KryptonContextMenuItemBase item, MenuTreeNode parent)
            {
                // Create a node to match the item
                MenuTreeNode node = new MenuTreeNode(item);

                // Add to either root or parent node
                if (parent != null)
                {
                    parent.Nodes.Add(node);
                }
                else
                {
                    treeView.Nodes.Add(node);
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
                TreeNode selectedNode = treeView.SelectedNode;
                TreeNode newNode = new MenuTreeNode(item);

                // If there is no selection then append to root
                if (selectedNode == null)
                {
                    treeView.Nodes.Add(newNode);
                }
                else
                {
                    // If current selection is at the root
                    if (selectedNode.Parent == null)
                    {
                        // If adding a menu item to an items
                        if (item is KryptonContextMenuItem)
                        {
                            MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                            KryptonContextMenuItems items = treeSelectedNode.Item as KryptonContextMenuItems;
                            items.Items.Add(item);
                            selectedNode.Nodes.Add(newNode);
                        }
                        else
                        {
                            int index = treeView.Nodes.IndexOf(selectedNode);
                            treeView.Nodes.Insert(index + 1, newNode);
                        }
                    }
                    else
                    {
                        int index = selectedNode.Parent.Nodes.IndexOf(selectedNode);
                        TreeNode parentNode = selectedNode.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        switch (treeParentNode.Item)
                        {
                            case KryptonContextMenuItems items1:
                                if (ValidInItemCollection(item))
                                {
                                    items1.Items.Insert(index + 1, item);
                                    selectedNode.Parent.Nodes.Insert(index + 1, newNode);
                                }
                                else
                                {
                                    MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                                    Debug.Assert(treeSelectedNode.Item is KryptonContextMenuItem);
                                    KryptonContextMenuItem items = treeSelectedNode.Item as KryptonContextMenuItem;
                                    items.Items.Add(item);
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
                                    MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                                    Debug.Assert(treeSelectedNode.Item is KryptonContextMenuItems);
                                    KryptonContextMenuItems items = treeSelectedNode.Item as KryptonContextMenuItems;
                                    items.Items.Add(item);
                                    selectedNode.Nodes.Add(newNode);
                                }
                                break;
                        }
                    }
                }

                // Select the newly added node
                if (newNode != null)
                {
                    treeView.SelectedNode = newNode;
                    treeView.Focus();
                }

                UpdateButtons();
            }

            private void UpdateButtons()
            {
                KryptonContextMenuItemBase item = null;
                KryptonContextMenuItemBase parent = null;
                int parentNodeCount = treeView.Nodes.Count;
                int nodeIndex = -1;

                if (treeView.SelectedNode is MenuTreeNode node)
                {
                    item = node.Item;
                    nodeIndex = treeView.Nodes.IndexOf(node);
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

                buttonMoveUp.Enabled = ((item != null) && (nodeIndex > 0));
                buttonMoveDown.Enabled = ((item != null) && (nodeIndex < (parentNodeCount - 1)));
                buttonAddItem.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuItem));
                buttonAddItems.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuItems));
                buttonAddSeparator.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuSeparator));
                buttonAddHeading.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuHeading));
                buttonAddMonthCalendar.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuMonthCalendar));
                buttonAddCheckBox.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuCheckBox));
                buttonAddCheckButton.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuCheckButton));
                buttonAddRadioButton.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuRadioButton));
                buttonAddLinkLabel.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuLinkLabel));
                buttonAddColorColumns.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuColorColumns));
                buttonAddImageSelect.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuImageSelect));
                buttonDelete.Enabled = (item != null);
            }

            private bool AllowAddItem(KryptonContextMenuItemBase item,
                                      KryptonContextMenuItemBase parent,
                                      Type addType)
            {
                // Special case the you can use add button on an Items collection so it adds an item inside it
                if ((item is KryptonContextMenuItems) && addType.Equals(typeof(KryptonContextMenuItem)))
                {
                    return true;
                }

                if (ItemInsideCollection(item, parent))
                {
                    KryptonContextMenuCollection temp = new KryptonContextMenuCollection();
                    foreach (Type t in temp.RestrictTypes)
                    {
                        if (t.Equals(addType))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    KryptonContextMenuItemCollection temp1 = new KryptonContextMenuItemCollection();
                    foreach (Type t in temp1.RestrictTypes)
                    {
                        if (t.Equals(addType))
                        {
                            return true;
                        }
                    }

                    if (item is KryptonContextMenuItem)
                    {
                        KryptonContextMenuCollection temp2 = new KryptonContextMenuCollection();
                        foreach (Type t in temp2.RestrictTypes)
                        {
                            if (t.Equals(addType))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            private bool ValidInCollection(KryptonContextMenuItemBase item)
            {
                Type addType = item.GetType();
                KryptonContextMenuCollection temp = new KryptonContextMenuCollection();
                foreach (Type t in temp.RestrictTypes)
                {
                    if (t.Equals(addType))
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool ValidInItemCollection(KryptonContextMenuItemBase item)
            {
                Type addType = item.GetType();
                KryptonContextMenuItemCollection temp = new KryptonContextMenuItemCollection();
                foreach (Type t in temp.RestrictTypes)
                {
                    if (t.Equals(addType))
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool ItemInsideCollection(KryptonContextMenuItemBase item,
                                              KryptonContextMenuItemBase parent)
            {
                // If it has no parent the it must be inside a collection
                // If inside an items then not inside a collection
                return !(parent is KryptonContextMenuItems);
            }

            private DictItemBase CreateItemsDictionary(object[] items)
            {
                DictItemBase dictItems = new DictItemBase();

                foreach (KryptonContextMenuItemBase item in items)
                {
                    AddItemsToDictionary(dictItems, item);
                }

                return dictItems;
            }

            private void AddItemsToDictionary(DictItemBase dictItems, KryptonContextMenuItemBase baseItem)
            {
                // Add item to the dictionary
                dictItems.Add(baseItem, baseItem);

                // Add children of an items collection
                if (baseItem is KryptonContextMenuItems items)
                {
                    foreach (KryptonContextMenuItemBase childItem in items.Items)
                    {
                        AddItemsToDictionary(dictItems, childItem);
                    }
                }

                // Add children of an item
                if (baseItem is KryptonContextMenuItem item)
                {
                    foreach (KryptonContextMenuItemBase childItem in item.Items)
                    {
                        AddItemsToDictionary(dictItems, childItem);
                    }
                }
            }

            private void SynchronizeCollections(DictItemBase before,
                                                DictItemBase after,
                                                ITypeDescriptorContext context)
            {
                // Add all new components (in the 'after' but not the 'before'
                foreach (KryptonContextMenuItemBase item in after.Values)
                {
                    if (!before.ContainsKey(item))
                    {
                        context.Container?.Add(item as IComponent);
                    }
                }

                // Delete all old components (in the 'before' but not the 'after'
                foreach (KryptonContextMenuItemBase item in before.Values)
                {
                    if (!after.ContainsKey(item))
                    {
                        DestroyInstance(item);

                        context.Container?.Remove(item as IComponent);
                    }
                }

                IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (changeService != null)
                {
                    // Mark components as changed when not added or removed
                    foreach (KryptonContextMenuItemBase item in after.Values)
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
        /// Initialize a new instance of the KryptonContextMenuCollectionEditor class.
        /// </summary>
        public KryptonContextMenuCollectionEditor()
            : base(typeof(KryptonContextMenuCollection))
        {
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm()
        {
            return new KryptonContextMenuCollectionForm(this);
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(KryptonContextMenuItems),
                                typeof(KryptonContextMenuSeparator),
                                typeof(KryptonContextMenuHeading),
                                typeof(KryptonContextMenuLinkLabel),
                                typeof(KryptonContextMenuCheckBox),
                                typeof(KryptonContextMenuCheckButton),
                                typeof(KryptonContextMenuRadioButton),
                                typeof(KryptonContextMenuColorColumns),
                                typeof(KryptonContextMenuMonthCalendar),
                                typeof(KryptonContextMenuImageSelect)};
        }
        #endregion
    }
}
