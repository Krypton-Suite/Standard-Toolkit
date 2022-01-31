#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Toolkit
{
    public partial class KryptonContextMenuCollectionEditor
    {
        #region Classes
        /// <summary>
        /// Form used for editing the KryptonContextMenuCollection.
        /// </summary>
        protected class KryptonContextMenuCollectionForm : CollectionForm
        {
            #region Types
            /// <summary>
            /// Simple class to reduce the length of declarations!
            /// </summary>
            protected class DictItemBase : Dictionary<KryptonContextMenuItemBase, KryptonContextMenuItemBase> { }

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
                        case KryptonContextMenuColorColumns _:
                            return 0;
                        case KryptonContextMenuHeading _:
                            return 1;
                        case KryptonContextMenuItem _:
                            return 2;
                        case KryptonContextMenuItems _:
                            return 3;
                        case KryptonContextMenuSeparator _:
                            return 4;
                        case KryptonContextMenuRadioButton _:
                            return 5;
                        case KryptonContextMenuCheckBox _:
                            return 6;
                        case KryptonContextMenuCheckButton _:
                            return 7;
                        case KryptonContextMenuLinkLabel _:
                            return 8;
                        //imageList.Images.SetKeyName(9, "delete2.png");
                        //imageList.Images.SetKeyName(10, "arrow_up_blue.png");
                        //imageList.Images.SetKeyName(11, "arrow_down_blue.png");
                        case KryptonContextMenuImageSelect _:
                            return 12;
                        case KryptonContextMenuMonthCalendar _:
                            return 13;
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
                    get => null;
                    set { }
                }
                #endregion
            }
            #endregion

            #region Instance Fields
            private DictItemBase _beforeItems;
            private readonly KryptonContextMenuCollectionEditor _editor;
            private Button _buttonOk;
            private TreeView _treeView;
            private Label _label1;
            private Label _label2;
            private ImageList _imageList;
            private Button _buttonDelete;
            private Button _buttonMoveUp;
            private Button _buttonMoveDown;
            private Button _buttonAddCheckBox;
            private Button _buttonAddCheckButton;
            private Button _buttonAddRadioButton;
            private Button _buttonAddLinkLabel;
            private Button _buttonAddSeparator;
            private Button _buttonAddItem;
            private Button _buttonAddItems;
            private Button _buttonAddHeading;
            private Button _buttonAddMonthCalendar;
            private Button _buttonAddColorColumns;
            private Button _buttonAddImageSelect;
            private Button _buttonAddComboBox;
            private PropertyGrid _propertyGrid1;
            private IContainer components;
            #endregion

            #region Identity

            //public KryptonContextMenuCollectionForm()
            //{
            //    InitializeComponent();
            //}
            /// <summary>
            /// Initialize a new instance of the KryptonContextMenuCollectionForm class.
            /// </summary>
            public KryptonContextMenuCollectionForm(KryptonContextMenuCollectionEditor editor)
            : base(editor)
            {
                _editor = editor;
                InitializeComponent();
            }


            private void InitializeComponent()
            {
                components = new Container();
                _buttonOk = new Button();
                _treeView = new TreeView();
                _imageList = new ImageList(components);
                _label1 = new Label();
                _buttonDelete = new Button();
                _buttonMoveUp = new Button();
                _buttonMoveDown = new Button();
                _buttonAddCheckBox = new Button();
                _buttonAddCheckButton = new Button();
                _buttonAddRadioButton = new Button();
                _buttonAddLinkLabel = new Button();
                _buttonAddSeparator = new Button();
                _buttonAddItem = new Button();
                _buttonAddItems = new Button();
                _buttonAddHeading = new Button();
                _buttonAddMonthCalendar = new Button();
                _propertyGrid1 = new PropertyGrid();
                _label2 = new Label();
                _buttonAddColorColumns = new Button();
                _buttonAddImageSelect = new Button();
                _buttonAddComboBox = new Button();
                SuspendLayout();
                // 
                // buttonOK
                // 
                _buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                _buttonOk.DialogResult = DialogResult.OK;
                _buttonOk.Location = new Point(729, 675);
                _buttonOk.Name = "_buttonOk";
                _buttonOk.Size = new Size(75, 33);
                _buttonOk.TabIndex = 16;
                _buttonOk.Text = @"OK";
                _buttonOk.UseVisualStyleBackColor = true;
                _buttonOk.Click += buttonOK_Click;
                // 
                // treeView
                // 
                _treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                   | AnchorStyles.Left
                                  | AnchorStyles.Right;
                _treeView.HideSelection = false;
                _treeView.ImageIndex = 0;
                _treeView.ImageList = _imageList;
                _treeView.Location = new Point(16, 33);
                _treeView.Name = "_treeView";
                _treeView.SelectedImageIndex = 0;
                _treeView.Size = new Size(320, 615);
                _treeView.TabIndex = 0;
                _treeView.AfterSelect += SelectionChanged;
                // 
                // imageList
                // 
                _imageList.TransparentColor = Color.Magenta;
                _imageList.Images.AddRange(new Image[]{
                    KryptonGenericResources.KryptonContextMenuColorColumns,
                    KryptonGenericResources.KryptonContextMenuHeading,
                    KryptonGenericResources.KryptonContextMenuItem,
                    KryptonGenericResources.KryptonContextMenuItems,
                    KryptonGenericResources.KryptonContextMenuSeparator,
                    KryptonGenericResources.KryptonRadioButton,
                    KryptonGenericResources.KryptonCheckBox,
                    KryptonGenericResources.KryptonCheckButton,
                    KryptonGenericResources.KryptonLinkLabel,
                    GenericImageResources.delete2,
                    BlueArrowResources.arrow_up_blue,
                    BlueArrowResources.arrow_down_blue,
                    KryptonGenericResources.KryptonContextMenuImageSelect,
                    KryptonGenericResources.KryptonMonthCalendar,
                    KryptonGenericResources.KryptonComboBox
                });

                // TODO: Do these need updating?
                _imageList.Images.SetKeyName(0, "KryptonContextMenuColorColumns.bmp");
                _imageList.Images.SetKeyName(1, "KryptonContextMenuHeading.bmp");
                _imageList.Images.SetKeyName(2, "KryptonContextMenuItem.bmp");
                _imageList.Images.SetKeyName(3, "KryptonContextMenuItems.bmp");
                _imageList.Images.SetKeyName(4, "KryptonContextMenuSeparator.bmp");
                _imageList.Images.SetKeyName(5, "KryptonRadioButton.bmp");
                _imageList.Images.SetKeyName(6, "KryptonCheckBox.bmp");
                _imageList.Images.SetKeyName(7, "KryptonCheckButton.bmp");
                _imageList.Images.SetKeyName(8, "KryptonLinkLabel.bmp");
                _imageList.Images.SetKeyName(9, "delete2.png");
                _imageList.Images.SetKeyName(10, "arrow_up_blue.png");
                _imageList.Images.SetKeyName(11, "arrow_down_blue.png");
                _imageList.Images.SetKeyName(12, "KryptonContextMenuImageSelect.bmp");
                _imageList.Images.SetKeyName(13, "KryptonContextMenuMonthCalendar.bmp");
                _imageList.Images.SetKeyName(14, "KryptonComboBox.bmp");
                // 
                // label1
                // 
                _label1.AutoSize = true;
                _label1.Location = new Point(13, 11);
                _label1.Name = "_label1";
                _label1.Size = new Size(120, 21);
                _label1.TabIndex = 7;
                _label1.Text = @"Item Hierarchy";
                // 
                // buttonDelete
                // 
                _buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonDelete.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonDelete.ImageIndex = 9;
                _buttonDelete.ImageList = _imageList;
                _buttonDelete.Location = new Point(341, 603);
                _buttonDelete.Name = "_buttonDelete";
                _buttonDelete.Size = new Size(184, 32);
                _buttonDelete.TabIndex = 14;
                _buttonDelete.Text = @"Delete";
                _buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonDelete.UseVisualStyleBackColor = true;
                _buttonDelete.Click += buttonDelete_Click;
                // 
                // buttonMoveUp
                // 
                _buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonMoveUp.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonMoveUp.ImageIndex = 10;
                _buttonMoveUp.ImageList = _imageList;
                _buttonMoveUp.Location = new Point(341, 29);
                _buttonMoveUp.Name = "_buttonMoveUp";
                _buttonMoveUp.Size = new Size(184, 32);
                _buttonMoveUp.TabIndex = 1;
                _buttonMoveUp.Text = @"Move Up";
                _buttonMoveUp.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonMoveUp.UseVisualStyleBackColor = true;
                _buttonMoveUp.Click += buttonMoveUp_Click;
                // 
                // buttonMoveDown
                // 
                _buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonMoveDown.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonMoveDown.ImageIndex = 11;
                _buttonMoveDown.ImageList = _imageList;
                _buttonMoveDown.Location = new Point(341, 70);
                _buttonMoveDown.Name = "_buttonMoveDown";
                _buttonMoveDown.Size = new Size(184, 32);
                _buttonMoveDown.TabIndex = 2;
                _buttonMoveDown.Text = @"Move Down";
                _buttonMoveDown.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonMoveDown.UseVisualStyleBackColor = true;
                _buttonMoveDown.Click += buttonMoveDown_Click;
                // 
                // buttonAddCheckBox
                // 
                _buttonAddCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddCheckBox.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddCheckBox.ImageIndex = 6;
                _buttonAddCheckBox.ImageList = _imageList;
                _buttonAddCheckBox.Location = new Point(341, 275);
                _buttonAddCheckBox.Name = "_buttonAddCheckBox";
                _buttonAddCheckBox.Size = new Size(184, 32);
                _buttonAddCheckBox.TabIndex = 7;
                _buttonAddCheckBox.Text = @"Add CheckBox";
                _buttonAddCheckBox.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddCheckBox.UseVisualStyleBackColor = true;
                _buttonAddCheckBox.Click += buttonAddCheckBox_Click;
                // 
                // buttonAddCheckButton
                // 
                _buttonAddCheckButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddCheckButton.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddCheckButton.ImageIndex = 7;
                _buttonAddCheckButton.ImageList = _imageList;
                _buttonAddCheckButton.Location = new Point(341, 316);
                _buttonAddCheckButton.Name = "_buttonAddCheckButton";
                _buttonAddCheckButton.Size = new Size(184, 32);
                _buttonAddCheckButton.TabIndex = 8;
                _buttonAddCheckButton.Text = @"Add CheckButton";
                _buttonAddCheckButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddCheckButton.UseVisualStyleBackColor = true;
                _buttonAddCheckButton.Click += buttonAddCheckButton_Click;
                // 
                // buttonAddRadioButton
                // 
                _buttonAddRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddRadioButton.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddRadioButton.ImageIndex = 5;
                _buttonAddRadioButton.ImageList = _imageList;
                _buttonAddRadioButton.Location = new Point(341, 357);
                _buttonAddRadioButton.Name = "_buttonAddRadioButton";
                _buttonAddRadioButton.Size = new Size(184, 32);
                _buttonAddRadioButton.TabIndex = 9;
                _buttonAddRadioButton.Text = @"Add RadioButton";
                _buttonAddRadioButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddRadioButton.UseVisualStyleBackColor = true;
                _buttonAddRadioButton.Click += buttonAddRadioButton_Click;
                // 
                // buttonAddLinkLabel
                // 
                _buttonAddLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddLinkLabel.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddLinkLabel.ImageIndex = 8;
                _buttonAddLinkLabel.ImageList = _imageList;
                _buttonAddLinkLabel.Location = new Point(341, 398);
                _buttonAddLinkLabel.Name = "_buttonAddLinkLabel";
                _buttonAddLinkLabel.Size = new Size(184, 32);
                _buttonAddLinkLabel.TabIndex = 10;
                _buttonAddLinkLabel.Text = @"Add LinkLabel";
                _buttonAddLinkLabel.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddLinkLabel.UseVisualStyleBackColor = true;
                _buttonAddLinkLabel.Click += buttonAddLinkLabel_Click;
                // 
                // buttonAddSeparator
                // 
                _buttonAddSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddSeparator.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddSeparator.ImageIndex = 4;
                _buttonAddSeparator.ImageList = _imageList;
                _buttonAddSeparator.Location = new Point(341, 234);
                _buttonAddSeparator.Name = "_buttonAddSeparator";
                _buttonAddSeparator.Size = new Size(184, 32);
                _buttonAddSeparator.TabIndex = 6;
                _buttonAddSeparator.Text = @"Add Separator";
                _buttonAddSeparator.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddSeparator.UseVisualStyleBackColor = true;
                _buttonAddSeparator.Click += buttonAddSeparator_Click;
                // 
                // buttonAddItem
                // 
                _buttonAddItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddItem.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddItem.ImageIndex = 2;
                _buttonAddItem.ImageList = _imageList;
                _buttonAddItem.Location = new Point(341, 111);
                _buttonAddItem.Name = "_buttonAddItem";
                _buttonAddItem.Size = new Size(184, 32);
                _buttonAddItem.TabIndex = 3;
                _buttonAddItem.Text = @"Add Item";
                _buttonAddItem.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddItem.UseVisualStyleBackColor = true;
                _buttonAddItem.Click += buttonAddItem_Click;
                // 
                // buttonAddItems
                // 
                _buttonAddItems.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddItems.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddItems.ImageIndex = 3;
                _buttonAddItems.ImageList = _imageList;
                _buttonAddItems.Location = new Point(341, 152);
                _buttonAddItems.Name = "_buttonAddItems";
                _buttonAddItems.Size = new Size(184, 32);
                _buttonAddItems.TabIndex = 4;
                _buttonAddItems.Text = @"Add Items";
                _buttonAddItems.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddItems.UseVisualStyleBackColor = true;
                _buttonAddItems.Click += buttonAddItems_Click;
                // 
                // buttonAddHeading
                // 
                _buttonAddHeading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddHeading.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddHeading.ImageIndex = 1;
                _buttonAddHeading.ImageList = _imageList;
                _buttonAddHeading.Location = new Point(341, 193);
                _buttonAddHeading.Name = "_buttonAddHeading";
                _buttonAddHeading.Size = new Size(184, 32);
                _buttonAddHeading.TabIndex = 5;
                _buttonAddHeading.Text = @"Add Heading";
                _buttonAddHeading.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddHeading.UseVisualStyleBackColor = true;
                _buttonAddHeading.Click += buttonAddHeading_Click;
                // 
                // buttonAddMonthCalendar
                // 
                _buttonAddMonthCalendar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddMonthCalendar.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddMonthCalendar.ImageIndex = 13;
                _buttonAddMonthCalendar.ImageList = _imageList;
                _buttonAddMonthCalendar.Location = new Point(341, 521);
                _buttonAddMonthCalendar.Name = "_buttonAddMonthCalendar";
                _buttonAddMonthCalendar.Size = new Size(184, 32);
                _buttonAddMonthCalendar.TabIndex = 13;
                _buttonAddMonthCalendar.Text = @"Add Month Calendar";
                _buttonAddMonthCalendar.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddMonthCalendar.UseVisualStyleBackColor = true;
                _buttonAddMonthCalendar.Click += buttonAddMonthCalendar_Click;
                // 
                // propertyGrid1
                // 
                _propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                   | AnchorStyles.Right;
                _propertyGrid1.HelpVisible = false;
                _propertyGrid1.Location = new Point(538, 33);
                _propertyGrid1.Name = "_propertyGrid1";
                _propertyGrid1.Size = new Size(266, 615);
                _propertyGrid1.TabIndex = 15;
                _propertyGrid1.ToolbarVisible = false;
                // 
                // label2
                // 
                _label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _label2.AutoSize = true;
                _label2.Location = new Point(535, 11);
                _label2.Name = "_label2";
                _label2.Size = new Size(125, 21);
                _label2.TabIndex = 16;
                _label2.Text = @"Item Properties";
                // 
                // buttonAddColorColumns
                // 
                _buttonAddColorColumns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddColorColumns.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddColorColumns.ImageIndex = 0;
                _buttonAddColorColumns.ImageList = _imageList;
                _buttonAddColorColumns.Location = new Point(341, 439);
                _buttonAddColorColumns.Name = "_buttonAddColorColumns";
                _buttonAddColorColumns.Size = new Size(184, 32);
                _buttonAddColorColumns.TabIndex = 11;
                _buttonAddColorColumns.Text = @"Add ColorColumns";
                _buttonAddColorColumns.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddColorColumns.UseVisualStyleBackColor = true;
                _buttonAddColorColumns.Click += buttonAddColorColumns_Click;
                // 
                // buttonAddImageSelect
                // 
                _buttonAddImageSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddImageSelect.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddImageSelect.ImageIndex = 12;
                _buttonAddImageSelect.ImageList = _imageList;
                _buttonAddImageSelect.Location = new Point(341, 480);
                _buttonAddImageSelect.Name = "_buttonAddImageSelect";
                _buttonAddImageSelect.Size = new Size(184, 32);
                _buttonAddImageSelect.TabIndex = 12;
                _buttonAddImageSelect.Text = @"Add ImageSelect";
                _buttonAddImageSelect.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddImageSelect.UseVisualStyleBackColor = true;
                _buttonAddImageSelect.Click += buttonAddImageSelect_Click;
                // 
                // buttonAddComboBox
                // 
                _buttonAddComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _buttonAddComboBox.ImageAlign = ContentAlignment.MiddleLeft;
                _buttonAddComboBox.ImageIndex = 14;
                _buttonAddComboBox.ImageList = _imageList;
                _buttonAddComboBox.Location = new Point(341, 562);
                _buttonAddComboBox.Name = "_buttonAddComboBox";
                _buttonAddComboBox.Size = new Size(184, 32);
                _buttonAddComboBox.TabIndex = 14;
                _buttonAddComboBox.Text = @"Add ComboBox";
                _buttonAddComboBox.TextImageRelation = TextImageRelation.ImageBeforeText;
                _buttonAddComboBox.UseVisualStyleBackColor = true;
                // Note Remove this when fully implemented
                _buttonAddComboBox.Enabled = false;
                _buttonAddComboBox.Click += buttonAddComboBox_Click;
                // 
                // KryptonContextMenuCollectionForm
                // 
                AcceptButton = _buttonOk;
                AutoScaleMode = AutoScaleMode.None;
                ClientSize = new Size(816, 724);
                ControlBox = false;
                Controls.Add(_buttonAddColorColumns);
                Controls.Add(_buttonAddImageSelect);
                Controls.Add(_label2);
                Controls.Add(_propertyGrid1);
                Controls.Add(_buttonAddMonthCalendar);
                Controls.Add(_buttonAddHeading);
                Controls.Add(_buttonAddItems);
                Controls.Add(_buttonAddItem);
                Controls.Add(_buttonAddSeparator);
                Controls.Add(_buttonAddLinkLabel);
                Controls.Add(_buttonAddRadioButton);
                Controls.Add(_buttonAddCheckButton);
                Controls.Add(_buttonAddCheckBox);
                Controls.Add(_buttonMoveDown);
                Controls.Add(_buttonMoveUp);
                Controls.Add(_buttonAddComboBox);
                Controls.Add(_buttonDelete);
                Controls.Add(_label1);
                Controls.Add(_treeView);
                Controls.Add(_buttonOk);
                Font = new Font(@"Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
                MinimumSize = new Size(733, 593);
                Name = "KryptonContextMenuCollectionForm";
                StartPosition = FormStartPosition.CenterScreen;
                Text = @"KryptonContextMenu Items Editor";
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
                    _propertyGrid1.Site = new PropertyGridSite(Context, _propertyGrid1);

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
            }
            #endregion

            #region Implementation
            private void KryptonContextMenuEditorForm_Load(object sender, EventArgs e)
            {
                // Set allowed categories into the property grid filter
                _propertyGrid1.BrowsableAttributes = new AttributeCollection(new KryptonPersistAttribute());
            }

            private void buttonOK_Click(object sender, EventArgs e)
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
                SynchronizeCollections(_beforeItems, afterItems, Context);

                // Notify container that the value has been changed
                Context.OnComponentChanged();
            }

            private void buttonMoveUp_Click(object sender, EventArgs e)
            {
                TreeNode node = _treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at the root level then move up in the root items collection
                    if (node.Parent == null)
                    {
                        var index = _treeView.Nodes.IndexOf(node);
                        _treeView.Nodes.Remove(node);
                        _treeView.Nodes.Insert(index - 1, node);
                    }
                    else
                    {
                        var index = node.Parent.Nodes.IndexOf(node);
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

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

            private void buttonMoveDown_Click(object sender, EventArgs e)
            {
                TreeNode node = _treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

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
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

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

            private void buttonAddItem_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItem)));

            private void buttonAddItems_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItems)));

            private void buttonAddHeading_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuHeading)));

            private void buttonAddMonthCalendar_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuMonthCalendar)));

            private void buttonAddSeparator_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuSeparator)));

            private void buttonAddCheckBox_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckBox)));

            private void buttonAddCheckButton_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckButton)));

            private void buttonAddRadioButton_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuRadioButton)));

            private void buttonAddLinkLabel_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuLinkLabel)));

            private void buttonAddColorColumns_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuColorColumns)));

            private void buttonAddImageSelect_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuImageSelect)));
            
            private void buttonAddComboBox_Click(object sender, EventArgs e) => AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuComboBox)));

            private void buttonDelete_Click(object sender, EventArgs e)
            {
                TreeNode node = _treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at root level then remove from root, otherwise from the parent collection
                    if (node.Parent == null)
                    {
                        _treeView.Nodes.Remove(node);
                    }
                    else
                    {
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

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

            private void SelectionChanged(object sender, TreeViewEventArgs e)
            {
                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void UpdatePropertyGrid()
            {
                TreeNode node = _treeView.SelectedNode;
                _propertyGrid1.SelectedObject = ((MenuTreeNode)node)?.PropertyObject;
            }

            private void AddMenuTreeNode(KryptonContextMenuItemBase item, MenuTreeNode parent)
            {
                // Create a node to match the item
                MenuTreeNode node = new(item);

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
                TreeNode selectedNode = _treeView.SelectedNode;
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
                            MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                            KryptonContextMenuItems items = treeSelectedNode?.Item as KryptonContextMenuItems;
                            items.Items.Add(item);
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
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

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
                                    MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                                    Debug.Assert(treeSelectedNode?.Item is KryptonContextMenuItem);
                                    KryptonContextMenuItem items = treeSelectedNode?.Item as KryptonContextMenuItem;
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
                                    Debug.Assert(treeSelectedNode?.Item is KryptonContextMenuItems);
                                    KryptonContextMenuItems items = treeSelectedNode?.Item as KryptonContextMenuItems;
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
                    _treeView.SelectedNode = newNode;
                    _treeView.Focus();
                }

                UpdateButtons();
            }

            private void UpdateButtons()
            {
                KryptonContextMenuItemBase item = null;
                KryptonContextMenuItemBase parent = null;
                var parentNodeCount = _treeView.Nodes.Count;
                var nodeIndex = -1;

                if (_treeView.SelectedNode is MenuTreeNode node)
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
                    KryptonContextMenuCollection temp = new();
                    return temp.RestrictTypes.Any(t => t.Equals(addType));
                }

                KryptonContextMenuItemCollection temp1 = new();
                if (temp1.RestrictTypes.Any(t => t.Equals(addType)))
                {
                    return true;
                }

                if (item is KryptonContextMenuItem)
                {
                    KryptonContextMenuCollection temp2 = new();
                    return temp2.RestrictTypes.Any(t => t.Equals(addType));
                }

                return false;
            }

            private bool ValidInCollection(KryptonContextMenuItemBase item)
            {
                Type addType = item.GetType();
                KryptonContextMenuCollection temp = new();
                return temp.RestrictTypes.Any(t => t.Equals(addType));
            }

            private bool ValidInItemCollection(KryptonContextMenuItemBase item)
            {
                Type addType = item.GetType();
                KryptonContextMenuItemCollection temp = new();
                return temp.RestrictTypes.Any(t => t.Equals(addType));
            }

            private bool ItemInsideCollection(KryptonContextMenuItemBase item,
                                              KryptonContextMenuItemBase parent) =>
                // If it has no parent the it must be inside a collection
                // If inside an items then not inside a collection
                parent is not KryptonContextMenuItems;

            private DictItemBase CreateItemsDictionary(object[] items)
            {
                DictItemBase dictItems = new();

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

                IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (changeService != null)
                {
                    // Mark components as changed when not added or removed
                    foreach (KryptonContextMenuItemBase item in after.Values.Where(item => before.ContainsKey(item)))
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
}
