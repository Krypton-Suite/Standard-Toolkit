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
    protected class KryptonContextMenuCollectionForm : KryptonDesignerCollectionForm
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
        private ImageList _imageList;
        private KryptonButton _buttonOk;
        private KryptonButton _buttonCancel;
        private KryptonTreeView _treeView;
        private KryptonLabel _label1;
        private KryptonLabel _label2;
        private KryptonButton _buttonDelete;
        private KryptonButton _buttonMoveUp;
        private KryptonButton _buttonMoveDown;
        private KryptonButton _buttonAddCheckBox;
        private KryptonButton _buttonAddCheckButton;
        private KryptonButton _buttonAddRadioButton;
        private KryptonButton _buttonAddLinkLabel;
        private KryptonButton _buttonAddSeparator;
        private KryptonButton _buttonAddItem;
        private KryptonButton _buttonAddItems;
        private KryptonButton _buttonAddHeading;
        private KryptonButton _buttonAddMonthCalendar;
        private KryptonButton _buttonAddColorColumns;
        private KryptonButton _buttonAddImageSelect;
        private KryptonButton _buttonAddComboBox;
        private KryptonButton _buttonAddProgressBar;
        private KryptonPropertyGrid _propertyGrid1;
        private IContainer components;
        private TableLayoutPanel _tableLayoutPanel1;
        private KryptonPanel _panel1;
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
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new Container();
            _buttonOk = new KryptonButton();
            _buttonCancel = new KryptonButton();
            _treeView = new KryptonTreeView();
            _imageList = new ImageList(components);
            _label1 = new KryptonLabel();
            _buttonDelete = new KryptonButton();
            _buttonMoveUp = new KryptonButton();
            _buttonMoveDown = new KryptonButton();
            _buttonAddCheckBox = new KryptonButton();
            _buttonAddCheckButton = new KryptonButton();
            _buttonAddRadioButton = new KryptonButton();
            _buttonAddLinkLabel = new KryptonButton();
            _buttonAddSeparator = new KryptonButton();
            _buttonAddItem = new KryptonButton();
            _buttonAddItems = new KryptonButton();
            _buttonAddHeading = new KryptonButton();
            _buttonAddMonthCalendar = new KryptonButton();
            _propertyGrid1 = new KryptonPropertyGrid();
            _label2 = new KryptonLabel();
            _buttonAddColorColumns = new KryptonButton();
            _buttonAddImageSelect = new KryptonButton();
            _buttonAddComboBox = new KryptonButton();
            _buttonAddProgressBar = new KryptonButton();
            _tableLayoutPanel1 = new TableLayoutPanel();
            _panel1 = new KryptonPanel();
            _tableLayoutPanel1.SuspendLayout();
            _panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonOK
            // 
            _buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonOk.DialogResult = DialogResult.OK;
            _buttonOk.Location = new Point(729, 675);
            _buttonOk.Name = nameof(_buttonOk);
            _buttonOk.Size = new Size(75, 33);
            _buttonOk.TabIndex = 16;
            _buttonOk.Values.Text = @"OK";
            _buttonOk.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            _buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _buttonCancel.DialogResult = DialogResult.Cancel;
            _buttonCancel.Location = new Point(10, 675);
            _buttonCancel.Name = nameof(_buttonCancel);
            _buttonCancel.Size = new Size(90, 33);
            _buttonCancel.TabIndex = 17;
            _buttonCancel.Values.Text = @"Cancel";
            _buttonCancel.Click += buttonCancel_Click;
            // 
            // treeView
            // 
            _treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _treeView.HideSelection = false;
            _treeView.ImageIndex = 0;
            _treeView.ImageList = _imageList;
            _treeView.Location = new Point(16, 33);
            _treeView.Name = nameof(_treeView);
            _treeView.SelectedImageIndex = 0;
            _treeView.Size = new Size(320, 615);
            _treeView.TabIndex = 0;
            _treeView.AfterSelect += SelectionChanged;
            // 
            // imageList
            // 
            _imageList.TransparentColor = Color.Magenta;
            _imageList.Images.AddRange([
                GenericKryptonImageResources.KryptonContextMenuColorColumns,
                GenericKryptonImageResources.KryptonContextMenuHeading,
                GenericKryptonImageResources.KryptonContextMenuItem,
                GenericKryptonImageResources.KryptonContextMenuItems,
                GenericKryptonImageResources.KryptonContextMenuSeparator,
                GenericKryptonImageResources.KryptonRadioButton,
                GenericKryptonImageResources.KryptonCheckBox,
                GenericKryptonImageResources.KryptonCheckButton,
                GenericKryptonImageResources.KryptonLinkLabel,
                GenericImageResources.delete,
                BlueArrowResources.arrow_up_blue,
                BlueArrowResources.arrow_down_blue,
                GenericKryptonImageResources.KryptonContextMenuImageSelect,
                GenericKryptonImageResources.KryptonMonthCalendar,
                GenericKryptonImageResources.KryptonComboBox,
                GenericKryptonImageResources.KryptonTextBox,
                GenericKryptonImageResources.KryptonNumericUpDown
            ]);

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
            _imageList.Images.SetKeyName(9, "delete.png");
            _imageList.Images.SetKeyName(10, "arrow_up_blue.png");
            _imageList.Images.SetKeyName(11, "arrow_down_blue.png");
            _imageList.Images.SetKeyName(12, "KryptonContextMenuImageSelect.bmp");
            _imageList.Images.SetKeyName(13, "KryptonContextMenuMonthCalendar.bmp");
            _imageList.Images.SetKeyName(14, "KryptonComboBox.bmp");
            _imageList.Images.SetKeyName(15, "KryptonTextBox.bmp");
            _imageList.Images.SetKeyName(16, "KryptonNumericUpDown.bmp");
            // 
            // label1
            // 
            _label1.AutoSize = true;
            _label1.Location = new Point(3, 0);
            _label1.Name = nameof(_label1);
            _label1.Size = new Size(120, 21);
            _label1.TabIndex = 7;
            _label1.Text = @"Item Hierarchy";
            // 
            // buttonDelete
            // 
            ConfigureImageListButton(_buttonDelete, _imageList, 9, @"Delete", buttonDelete_Click);
            _buttonDelete.Anchor = AnchorStyles.Top;
            _buttonDelete.Location = new Point(316, 688);
            _buttonDelete.Name = nameof(_buttonDelete);
            _buttonDelete.Size = new Size(184, 32);
            _buttonDelete.TabIndex = 14;
            // 
            // buttonMoveUp
            // 
            ConfigureImageListButton(_buttonMoveUp, _imageList, 10, @"Move Up", buttonMoveUp_Click);
            _buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonMoveUp.Location = new Point(21, 29);
            _buttonMoveUp.Name = nameof(_buttonMoveUp);
            _buttonMoveUp.Size = new Size(184, 32);
            _buttonMoveUp.TabIndex = 1;
            // 
            // buttonMoveDown
            // 
            ConfigureImageListButton(_buttonMoveDown, _imageList, 11, @"Move Down", buttonMoveDown_Click);
            _buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonMoveDown.Location = new Point(21, 70);
            _buttonMoveDown.Name = nameof(_buttonMoveDown);
            _buttonMoveDown.Size = new Size(184, 32);
            _buttonMoveDown.TabIndex = 2;
            // 
            // buttonAddCheckBox
            // 
            ConfigureImageListButton(_buttonAddCheckBox, _imageList, 6, @"Add CheckBox", buttonAddCheckBox_Click);
            _buttonAddCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddCheckBox.Location = new Point(21, 275);
            _buttonAddCheckBox.Name = nameof(_buttonAddCheckBox);
            _buttonAddCheckBox.Size = new Size(184, 32);
            _buttonAddCheckBox.TabIndex = 7;
            // 
            // buttonAddCheckButton
            // 
            ConfigureImageListButton(_buttonAddCheckButton, _imageList, 7, @"Add CheckButton", buttonAddCheckButton_Click);
            _buttonAddCheckButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddCheckButton.Location = new Point(21, 316);
            _buttonAddCheckButton.Name = nameof(_buttonAddCheckButton);
            _buttonAddCheckButton.Size = new Size(184, 32);
            _buttonAddCheckButton.TabIndex = 8;
            // 
            // buttonAddRadioButton
            // 
            ConfigureImageListButton(_buttonAddRadioButton, _imageList, 5, @"Add RadioButton", buttonAddRadioButton_Click);
            _buttonAddRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddRadioButton.Location = new Point(21, 357);
            _buttonAddRadioButton.Name = nameof(_buttonAddRadioButton);
            _buttonAddRadioButton.Size = new Size(184, 32);
            _buttonAddRadioButton.TabIndex = 9;
            // 
            // buttonAddLinkLabel
            // 
            ConfigureImageListButton(_buttonAddLinkLabel, _imageList, 8, @"Add LinkLabel", buttonAddLinkLabel_Click);
            _buttonAddLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddLinkLabel.Location = new Point(21, 398);
            _buttonAddLinkLabel.Name = nameof(_buttonAddLinkLabel);
            _buttonAddLinkLabel.Size = new Size(184, 32);
            _buttonAddLinkLabel.TabIndex = 10;
            // 
            // buttonAddSeparator
            // 
            ConfigureImageListButton(_buttonAddSeparator, _imageList, 4, @"Add Separator", buttonAddSeparator_Click);
            _buttonAddSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddSeparator.Location = new Point(21, 234);
            _buttonAddSeparator.Name = nameof(_buttonAddSeparator);
            _buttonAddSeparator.Size = new Size(184, 32);
            _buttonAddSeparator.TabIndex = 6;
            // 
            // buttonAddItem
            // 
            ConfigureImageListButton(_buttonAddItem, _imageList, 2, @"Add Item", buttonAddItem_Click);
            _buttonAddItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddItem.Location = new Point(21, 111);
            _buttonAddItem.Name = nameof(_buttonAddItem);
            _buttonAddItem.Size = new Size(184, 32);
            _buttonAddItem.TabIndex = 3;
            // 
            // buttonAddItems
            // 
            ConfigureImageListButton(_buttonAddItems, _imageList, 3, @"Add Items", buttonAddItems_Click);
            _buttonAddItems.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddItems.Location = new Point(21, 152);
            _buttonAddItems.Name = nameof(_buttonAddItems);
            _buttonAddItems.Size = new Size(184, 32);
            _buttonAddItems.TabIndex = 4;
            // 
            // buttonAddHeading
            // 
            ConfigureImageListButton(_buttonAddHeading, _imageList, 1, @"Add Heading", buttonAddHeading_Click);
            _buttonAddHeading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddHeading.Location = new Point(21, 193);
            _buttonAddHeading.Name = nameof(_buttonAddHeading);
            _buttonAddHeading.Size = new Size(184, 32);
            _buttonAddHeading.TabIndex = 5;
            // 
            // buttonAddMonthCalendar
            // 
            ConfigureImageListButton(_buttonAddMonthCalendar, _imageList, 13, @"Add Month Calendar", buttonAddMonthCalendar_Click);
            _buttonAddMonthCalendar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddMonthCalendar.Location = new Point(21, 521);
            _buttonAddMonthCalendar.Name = nameof(_buttonAddMonthCalendar);
            _buttonAddMonthCalendar.Size = new Size(184, 32);
            _buttonAddMonthCalendar.TabIndex = 13;
            // 
            // propertyGrid1
            // 
            _propertyGrid1.Dock = DockStyle.Fill;
            _propertyGrid1.HelpVisible = true;
            _propertyGrid1.Location = new Point(524, 24);
            _propertyGrid1.Name = "_propertyGrid1";
            _propertyGrid1.Size = new Size(289, 658);
            _propertyGrid1.TabIndex = 15;
            _propertyGrid1.ToolbarVisible = true;
            // 
            // label2
            // 
            _label2.AutoSize = true;
            _label2.Location = new Point(524, 0);
            _label2.Name = nameof(_label2);
            _label2.Size = new Size(125, 21);
            _label2.TabIndex = 16;
            _label2.Text = @"Item Properties";
            // 
            // buttonAddColorColumns
            // 
            ConfigureImageListButton(_buttonAddColorColumns, _imageList, 0, @"Add ColorColumns", buttonAddColorColumns_Click);
            _buttonAddColorColumns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddColorColumns.Location = new Point(21, 439);
            _buttonAddColorColumns.Name = nameof(_buttonAddColorColumns);
            _buttonAddColorColumns.Size = new Size(184, 32);
            _buttonAddColorColumns.TabIndex = 11;
            // 
            // buttonAddImageSelect
            // 
            ConfigureImageListButton(_buttonAddImageSelect, _imageList, 12, @"Add ImageSelect", buttonAddImageSelect_Click);
            _buttonAddImageSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddImageSelect.Location = new Point(21, 480);
            _buttonAddImageSelect.Name = nameof(_buttonAddImageSelect);
            _buttonAddImageSelect.Size = new Size(184, 32);
            _buttonAddImageSelect.TabIndex = 12;
            // 
            // buttonAddComboBox
            // 
            ConfigureImageListButton(_buttonAddComboBox, _imageList, 14, @"Add ComboBox", buttonAddComboBox_Click);
            _buttonAddComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddComboBox.Location = new Point(21, 562);
            _buttonAddComboBox.Name = nameof(_buttonAddComboBox);
            _buttonAddComboBox.Size = new Size(184, 32);
            _buttonAddComboBox.TabIndex = 14;
            // 
            // buttonAddProgressBar
            // 
            ConfigureImageListButton(_buttonAddProgressBar, _imageList, 16, @"Add ProgressBar", buttonAddProgressBar_Click);
            _buttonAddProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddProgressBar.Location = new Point(21, 603);
            _buttonAddProgressBar.Name = nameof(_buttonAddProgressBar);
            _buttonAddProgressBar.Size = new Size(184, 32);
            _buttonAddProgressBar.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            _tableLayoutPanel1.ColumnCount = 3;
            _tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 226F));
            _tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tableLayoutPanel1.Controls.Add(_label1, 0, 0);
            _tableLayoutPanel1.Controls.Add(_buttonOk, 2, 2);
            _tableLayoutPanel1.Controls.Add(_buttonCancel, 0, 2);
            _tableLayoutPanel1.Controls.Add(_label2, 2, 0);
            _tableLayoutPanel1.Controls.Add(_treeView, 0, 1);
            _tableLayoutPanel1.Controls.Add(_propertyGrid1, 2, 1);
            _tableLayoutPanel1.Controls.Add(_panel1, 1, 1);
            _tableLayoutPanel1.Controls.Add(_buttonDelete, 1, 2);
            _tableLayoutPanel1.Dock = DockStyle.Fill;
            _tableLayoutPanel1.Location = new Point(0, 0);
            _tableLayoutPanel1.Name = "_tableLayoutPanel1";
            _tableLayoutPanel1.RowCount = 3;
            _tableLayoutPanel1.RowStyles.Add(new RowStyle());
            _tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _tableLayoutPanel1.RowStyles.Add(new RowStyle());
            _tableLayoutPanel1.Size = new Size(816, 724);
            _tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            _panel1.Controls.Add(_buttonMoveUp);
            _panel1.Controls.Add(_buttonAddProgressBar);
            _panel1.Controls.Add(_buttonAddComboBox);
            _panel1.Controls.Add(_buttonAddMonthCalendar);
            _panel1.Controls.Add(_buttonAddImageSelect);
            _panel1.Controls.Add(_buttonAddColorColumns);
            _panel1.Controls.Add(_buttonMoveDown);
            _panel1.Controls.Add(_buttonAddItem);
            _panel1.Controls.Add(_buttonAddHeading);
            _panel1.Controls.Add(_buttonAddLinkLabel);
            _panel1.Controls.Add(_buttonAddSeparator);
            _panel1.Controls.Add(_buttonAddRadioButton);
            _panel1.Controls.Add(_buttonAddItems);
            _panel1.Controls.Add(_buttonAddCheckButton);
            _panel1.Controls.Add(_buttonAddCheckBox);
            _panel1.Dock = DockStyle.Fill;
            _panel1.Location = new Point(298, 24);
            _panel1.Name = "_panel1";
            _panel1.Size = new Size(220, 658);
            _panel1.TabIndex = 17;
            // 
            // KryptonContextMenuCollectionForm
            // 
            AcceptButton = _buttonOk;
            CancelButton = _buttonCancel;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(816, 724);
            Controls.Add(_tableLayoutPanel1);
            Font = new Font(@"Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MinimumSize = new Size(733, 593);
            Name = nameof(KryptonContextMenuCollectionForm);
            Text = @"KryptonContextMenu Items Editor";
            _tableLayoutPanel1.ResumeLayout(false);
            _tableLayoutPanel1.PerformLayout();
            _panel1.ResumeLayout(false);
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

        private void ApplyOwnerPalette()
        {
            if (Context?.Instance is KryptonContextMenu contextMenu)
            {
                ApplyOwnerPalette(contextMenu.PaletteMode, contextMenu.LocalCustomPalette);
            }
        }

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