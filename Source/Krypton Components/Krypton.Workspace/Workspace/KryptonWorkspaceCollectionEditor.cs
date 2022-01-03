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


namespace Krypton.Workspace
{
    internal class KryptonWorkspaceCollectionEditor : CollectionEditor
    {
        #region Classes
        /// <summary>
        /// Form used for editing the KryptonWorkspaceCollection.
        /// </summary>
        protected partial class KryptonWorkspaceCollectionForm : CollectionForm
        {
            #region Types
            /// <summary>
            /// Simple class to reduce the length of declaractions!
            /// </summary>
            protected class DictItemBase : Dictionary<Component, Component> { }

            /// <summary>
            /// Act as proxy for krypton page item to control the exposed properties to the property grid.
            /// </summary>
            protected class PageProxy
            {
                #region Instance Fields
                private readonly KryptonPage _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the PageProxy class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public PageProxy(KryptonPage item) => _item = item;

                #endregion

                #region Public
                /// <summary>
                /// Gets access to the common page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorRedirect StateCommon => _item.StateCommon;

                /// <summary>
                /// Gets access to the disabled page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigator StateDisabled => _item.StateDisabled;

                /// <summary>
                /// Gets access to the normal page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigator StateNormal => _item.StateNormal;

                /// <summary>
                /// Gets access to the tracking page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOtherEx StateTracking => _item.StateTracking;

                /// <summary>
                /// Gets access to the pressed page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOtherEx StatePressed => _item.StatePressed;

                /// <summary>
                /// Gets access to the selected page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOther StateSelected => _item.StateSelected;

                /// <summary>
                /// Gets access to the focus page appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOtherRedirect OverrideFocus => _item.OverrideFocus;

                /// <summary>
                /// Gets and sets the page text.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue("Page")]
                public string Text
                {
                    get => _item.Text;
                    set => _item.Text = value;
                }

                /// <summary>
                /// Gets and sets the title text for the page.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue("Page Title")]
                public string TextTitle
                {
                    get => _item.TextTitle;
                    set => _item.TextTitle = value;
                }

                /// <summary>
                /// Gets and sets the description text for the page.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue("Page Description")]
                public string TextDescription
                {
                    get => _item.TextDescription;
                    set => _item.TextDescription = value;
                }

                /// <summary>
                /// Gets and sets the small image for the page.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue(null)]
                public Bitmap ImageSmall
                {
                    get => _item.ImageSmall;
                    set => _item.ImageSmall = value;
                }

                /// <summary>
                /// Gets and sets the medium image for the page.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue(null)]
                public Bitmap ImageMedium
                {
                    get => _item.ImageMedium;
                    set => _item.ImageMedium = value;
                }

                /// <summary>
                /// Gets and sets the large image for the page.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue(null)]
                public Bitmap ImageLarge
                {
                    get => _item.ImageLarge;
                    set => _item.ImageLarge = value;
                }

                /// <summary>
                /// Gets and sets the page tooltip image.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue(null)]
                public Bitmap ToolTipImage
                {
                    get => _item.ToolTipImage;
                    set => _item.ToolTipImage = value;
                }

                /// <summary>
                /// Gets and sets the tooltip image transparent color.
                /// </summary>
                [Category(@"Appearance")]
                [Description(@"Page tooltip image transparent color.")]
                public Color ToolTipImageTransparentColor
                {
                    get => _item.ToolTipImageTransparentColor;
                    set => _item.ToolTipImageTransparentColor = value;
                }

                /// <summary>
                /// Gets and sets the page tooltip title text.
                /// </summary>
                [Category(@"Appearance")]
                [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
                [DefaultValue("")]
                public string ToolTipTitle
                {
                    get => _item.ToolTipTitle;
                    set => _item.ToolTipTitle = value;
                }

                /// <summary>
                /// Gets and sets the page tooltip body text.
                /// </summary>
                [Category(@"Appearance")]
                [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
                [DefaultValue("")]
                public string ToolTipBody
                {
                    get => _item.ToolTipBody;
                    set => _item.ToolTipBody = value;
                }

                /// <summary>
                /// Gets and sets the tooltip label style.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue(typeof(LabelStyle), "ToolTip")]
                public LabelStyle ToolTipStyle
                {
                    get => _item.ToolTipStyle;
                    set => _item.ToolTipStyle = value;
                }

                /// <summary>
                /// Gets and sets the unique name of the page.
                /// </summary>
                [Category(@"Appearance")]
                public string UniqueName
                {
                    get => _item.UniqueName;
                    set => _item.UniqueName = value;
                }

                /// <summary>
                /// Gets and sets if the page should be shown.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(true)]
                public bool Visible
                {
                    get => _item.LastVisibleSet;
                    set => _item.LastVisibleSet = value;
                }

                /// <summary>
                /// Gets and sets if the page should be enabled.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(true)]
                public bool Enabled
                {
                    get => _item.Enabled;
                    set => _item.Enabled = value;
                }

                /// <summary>
                /// Gets and sets the KryptonContextMenu to show when right clicked.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(null)]
                public KryptonContextMenu KryptonContextMenu
                {
                    get => _item.KryptonContextMenu;
                    set => _item.KryptonContextMenu = value;
                }

                /// <summary>
                /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
                /// </summary>
                [Category(@"Layout")]
                [DefaultValue(typeof(Size), "50,50")]
                public Size MinimumSize
                {
                    get => _item.MinimumSize;
                    set => _item.MinimumSize = value;
                }

                /// <summary>
                /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
                /// </summary>
                [Category(@"Layout")]
                [DefaultValue(typeof(Size), "0,0")]
                public Size MaximumSize
                {
                    get => _item.MaximumSize;
                    set => _item.MaximumSize = value;
                }

                /// <summary>
                /// Gets or sets the page padding.
                /// </summary>
                [Category(@"Layout")]
                [DefaultValue(typeof(Padding), "0,0,0,0")]
                public Padding Padding
                {
                    get => _item.Padding;
                    set => _item.Padding = value;
                }

                /// <summary>
                /// Gets and sets user-defined data associated with the object.
                /// </summary>
                [Category(@"Data")]
                [TypeConverter(typeof(StringConverter))]
                [DefaultValue(null)]
                public object Tag
                {
                    get => _item.Tag;
                    set => _item.Tag = value;
                }
                #endregion
            }

            /// <summary>
            /// Act as proxy for workspace cell item to control the exposed properties to the property grid.
            /// </summary>
            protected class CellProxy
            {
                #region Instance Fields
                private readonly KryptonWorkspaceCell _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the CellProxy class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public CellProxy(KryptonWorkspaceCell item) => _item = item;

                #endregion

                #region Public
                /// <summary>
                /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
                /// </summary>
                [Category(@"Layout")]
                [DefaultValue(typeof(Size), "0,0")]
                public Size MinimumSize
                {
                    get => _item.MinimumSize;
                    set => _item.MinimumSize = value;
                }

                /// <summary>
                /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
                /// </summary>
                [Category(@"Layout")]
                [DefaultValue(typeof(Size), "0,0")]
                public Size MaximumSize
                {
                    get => _item.MaximumSize;
                    set => _item.MaximumSize = value;
                }

                /// <summary>
                /// Gets and sets if the user can a separator to resize this workspace cell.
                /// </summary>
                [Category(@"Visuals")]
                [DefaultValue(true)]
                public bool AllowResizing
                {
                    get => _item.AllowResizing;
                    set => _item.AllowResizing = value;
                }

                /// <summary>
                /// Star notation the describes the sizing of the workspace item.
                /// </summary>
                [Category(@"Workspace")]
                [DefaultValue("50*,50*")]
                public string StarSize
                {
                    get => _item.StarSize;
                    set => _item.StarSize = value;
                }

                /// <summary>
                /// Should the item be disposed when it is removed from the workspace.
                /// </summary>
                [Category(@"Workspace")]
                [DefaultValue(true)]
                public bool DisposeOnRemove
                {
                    get => _item.DisposeOnRemove;
                    set => _item.DisposeOnRemove = value;
                }

                /// <summary>
                /// Gets access to the bar specific settings.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorBar Bar => _item.Bar;

                /// <summary>
                /// Gets access to the stack specific settings.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorStack Stack => _item.Stack;

                /// <summary>
                /// Gets access to the outlook mode specific settings.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorOutlook Outlook => _item.Outlook;

                /// <summary>
                /// Gets access to button specifications and fixed button logic.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorButton Button => _item.Button;

                /// <summary>
                /// Gets access to the group specific settings.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorGroup Group => _item.Group;

                /// <summary>
                /// Gets access to the header specific settings.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorHeader Header => _item.Header;

                /// <summary>
                /// Gets access to the panels specific settings.
                /// </summary>
                [Category(@"Visuals (Modes)")]
                public NavigatorPanel Panel => _item.Panel;

                /// <summary>
                /// Gets access to the popup page specific settings.
                /// </summary>
                [Category(@"Visuals")]
                public NavigatorPopupPages PopupPages => _item.PopupPages;

                /// <summary>
                /// Gets access to the tooltip specific settings.
                /// </summary>
                [Category(@"Visuals")]
                public NavigatorToolTips ToolTips => _item.ToolTips;

                /// <summary>
                /// Gets access to the common navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorRedirect StateCommon => _item.StateCommon;

                /// <summary>
                /// Gets access to the disabled navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigator StateDisabled => _item.StateDisabled;

                /// <summary>
                /// Gets access to the normal navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigator StateNormal => _item.StateNormal;

                /// <summary>
                /// Gets access to the tracking navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOtherEx StateTracking => _item.StateTracking;

                /// <summary>
                /// Gets access to the pressed navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOtherEx StatePressed => _item.StatePressed;

                /// <summary>
                /// Gets access to the selected navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOther StateSelected => _item.StateSelected;

                /// <summary>
                /// Gets access to the focus navigator appearance entries.
                /// </summary>
                [Category(@"Visuals")]
                public PaletteNavigatorOtherRedirect OverrideFocus => _item.OverrideFocus;

                /// <summary>
                /// Gets and sets the display mode.
                /// </summary>
                [Category(@"Visuals")]
                [DefaultValue(typeof(NavigatorMode), "BarTabGroup")]
                public NavigatorMode NavigatorMode
                {
                    get => _item.NavigatorMode;
                    set => _item.NavigatorMode = value;
                }

                /// <summary>
                /// Gets and sets the page background style.
                /// </summary>
                [Category(@"Visuals")]
                [DefaultValue(typeof(PaletteBackStyle), "ControlClient")]
                public PaletteBackStyle PageBackStyle
                {
                    get => _item.PageBackStyle;
                    set => _item.PageBackStyle = value;
                }

                /// <summary>
                /// Gets or sets the default setting for allowing the page dragging from of the navigator.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(true)]
                public bool AllowPageDrag
                {
                    get => _item.AllowPageDrag;
                    set => _item.AllowPageDrag = value;
                }

                /// <summary>
                /// Gets or sets if the tab headers are allowed to take the focus.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(false)]
                public bool AllowTabFocus
                {
                    get => _item.AllowTabFocus;
                    set => _item.AllowTabFocus = value;
                }

                /// <summary>
                /// Gets and sets if the cell should be shown.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(true)]
                public bool Visible
                {
                    get => _item.LastVisibleSet;
                    set => _item.LastVisibleSet = value;
                }

                /// <summary>
                /// Gets and sets if the cell should be enabled.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(true)]
                public bool Enabled
                {
                    get => _item.Enabled;
                    set => _item.Enabled = value;
                }

                /// <summary>
                /// Gets and sets if the cell selected page.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(true)]
                public KryptonPage SelectedPage
                {
                    get => _item.SelectedPage;

                    set 
                    { 
                        // Check that the target cell allows selected tabs
                        if (_item.AllowTabSelect)
                        {
                            _item.SelectedPage = value;
                        }
                    }
                }

                /// <summary>
                /// Gets and sets the KryptonContextMenu to show when right clicked.
                /// </summary>
                [Category(@"Behavior")]
                [DefaultValue(null)]
                public KryptonContextMenu KryptonContextMenu
                {
                    get => _item.KryptonContextMenu;
                    set => _item.KryptonContextMenu = value;
                }

                /// <summary>
                /// Gets or sets a value indicating whether mnemonics select pages and button specs.
                /// </summary>
                [Category(@"Appearance")]
                [DefaultValue(true)]
                public bool UseMnemonic
                {
                    get => _item.UseMnemonic;
                    set => _item.UseMnemonic = value;
                }

                /// <summary>
                /// Gets and sets user-defined data associated with the object.
                /// </summary>
                [Category(@"Data")]
                [TypeConverter(typeof(StringConverter))]
                [DefaultValue(null)]
                public object Tag
                {
                    get => _item.Tag;
                    set => _item.Tag = value;
                }
                #endregion
            }

            /// <summary>
            /// Act as proxy for workspace sequence item to control the exposed properties to the property grid.
            /// </summary>
            protected class SequenceProxy
            {
                #region Instance Fields
                private readonly KryptonWorkspaceSequence _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the SequenceProxy class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public SequenceProxy(KryptonWorkspaceSequence item) => _item = item;

                #endregion

                #region Public
                /// <summary>
                /// Gets or sets a value indicating whether the sequence is Displayed.
                /// </summary>
                [Category(@"Visuals")]
                [DefaultValue(true)]
                public bool Visible
                {
                    get => _item.Visible;
                    set => _item.Visible = value;
                }

                /// <summary>
                /// Gets and sets the orientation for laying out the child entries.
                /// </summary>
                [Category(@"Workspace")]
                [DefaultValue(typeof(Orientation), "Horizontal")]
                public Orientation Orientation
                {
                    get => _item.Orientation;
                    set => _item.Orientation = value;
                }

                /// <summary>
                /// Star notation the describes the sizing of the workspace item.
                /// </summary>
                [Category(@"Workspace")]
                [DefaultValue("50*,50*")]
                public string StarSize
                {
                    get => _item.StarSize;
                    set => _item.StarSize = value;
                }
                #endregion
            }

            /// <summary>
            /// Tree node that is attached to a context menu item.
            /// </summary>
            protected class MenuTreeNode : TreeNode
            {
                #region Static Fields
                private static int _id = 1;
                #endregion

                #region Instance Fields

                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the MenuTreeNode class.
                /// </summary>
                /// <param name="item">Item to represent.</param>
                public MenuTreeNode(Component item)
                {
                    InstanceId = _id++;

                    PageItem = item as KryptonPage;
                    if (PageItem != null)
                    {
                        PageItem.TextChanged += OnPageTextChanged;
                        Text = "Page (" + PageItem.Text.ToString() + ")";
                    }

                    CellItem = item as KryptonWorkspaceCell;
                    if (CellItem != null)
                    {
                        CellItem.PropertyChanged += OnCellPropertyChanged;
                        Text = "Cell (" + CellItem.StarSize.ToString() + ")";
                    }

                    SequenceItem = item as KryptonWorkspaceSequence;
                    if (SequenceItem != null)
                    {
                        SequenceItem.PropertyChanged += OnSequencePropertyChanged;
                        Text = SequenceItem.Orientation + " (" + SequenceItem.StarSize.ToString() + ")";
                    }
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets access to the associated workspace cell item.
                /// </summary>
                public Component Item => PageItem ?? (CellItem ?? (Component)SequenceItem);

                /// <summary>
                /// Gets access to the associated workspace cell item.
                /// </summary>
                public KryptonPage PageItem { get; }

                /// <summary>
                /// Gets access to the associated workspace cell item.
                /// </summary>
                public KryptonWorkspaceCell CellItem { get; }

                /// <summary>
                /// Gets access to the associated workspace sequence item.
                /// </summary>
                public KryptonWorkspaceSequence SequenceItem { get; }

                /// <summary>
                /// Gets the instance identifier.
                /// </summary>
                public int InstanceId { get; }

                #endregion

                #region Implementation
                private void OnPageTextChanged(object sender, EventArgs e)
                {
                    Text = "Page (" + PageItem.Text.ToString() + ")";
                }

                private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    Text = "Cell (" + CellItem.StarSize.ToString() + ")";
                }

                private void OnSequencePropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    Text = SequenceItem.Orientation + " (" + SequenceItem.StarSize.ToString() + ")";
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
            private readonly KryptonWorkspaceCollectionEditor _editor;
            private DictItemBase _beforeItems;
            private readonly TreeView treeView;
            private readonly PropertyGrid propertyGrid;
            private readonly Button buttonMoveUp;
            private readonly Button buttonMoveDown;
            private readonly Button buttonAddPage;
            private readonly Button buttonAddCell;
            private readonly Button buttonAddSequence;
            private readonly Button buttonOK;
            private readonly Button buttonDelete;
            private readonly Label labelItemProperties;
            private readonly Label labelWorkspaceCollection;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the KryptonWorkspaceCollectionForm class.
            /// </summary>
            public KryptonWorkspaceCollectionForm(KryptonWorkspaceCollectionEditor editor)
                : base(editor)
            {
                _editor = editor;

                buttonOK = new Button();
                treeView = new TreeView();
                buttonMoveUp = new Button();
                buttonMoveDown = new Button();
                buttonAddPage = new Button();
                buttonAddCell = new Button();
                buttonAddSequence = new Button();
                buttonDelete = new Button();
                propertyGrid = new PropertyGrid();
                labelItemProperties = new Label();
                labelWorkspaceCollection = new Label();
                SuspendLayout();
                // 
                // buttonOK
                // 
                buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonOK.DialogResult = DialogResult.OK;
                buttonOK.Location = new Point(547, 382);
                buttonOK.Name = "buttonOK";
                buttonOK.Size = new Size(75, 23);
                buttonOK.TabIndex = 8;
                buttonOK.Text = "OK";
                buttonOK.UseVisualStyleBackColor = true;
                buttonOK.Click += buttonOK_Click;
                // 
                // treeView
                // 
                treeView.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                                   | AnchorStyles.Left)
                                  | AnchorStyles.Right;
                treeView.Location = new Point(12, 32);
                treeView.Name = "treeView";
                treeView.Size = new Size(251, 339);
                treeView.TabIndex = 1;
                treeView.HideSelection = false;
                treeView.AfterSelect += treeView_AfterSelect;
                // 
                // buttonMoveUp
                // 
                buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonMoveUp.Image = Properties.Resources.arrow_up_blue;
                buttonMoveUp.ImageAlign = ContentAlignment.MiddleLeft;
                buttonMoveUp.Location = new Point(272, 32);
                buttonMoveUp.Name = "buttonMoveUp";
                buttonMoveUp.Size = new Size(95, 28);
                buttonMoveUp.TabIndex = 2;
                buttonMoveUp.Text = " Move Up";
                buttonMoveUp.TextAlign = ContentAlignment.MiddleLeft;
                buttonMoveUp.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonMoveUp.UseVisualStyleBackColor = true;
                buttonMoveUp.Click += buttonMoveUp_Click;
                // 
                // buttonMoveDown
                // 
                buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonMoveDown.Image = Properties.Resources.arrow_down_blue;
                buttonMoveDown.ImageAlign = ContentAlignment.MiddleLeft;
                buttonMoveDown.Location = new Point(272, 66);
                buttonMoveDown.Name = "buttonMoveDown";
                buttonMoveDown.Size = new Size(95, 28);
                buttonMoveDown.TabIndex = 3;
                buttonMoveDown.Text = " Move Down";
                buttonMoveDown.TextAlign = ContentAlignment.MiddleLeft;
                buttonMoveDown.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonMoveDown.UseVisualStyleBackColor = true;
                buttonMoveDown.Click += buttonMoveDown_Click;
                // 
                // buttonDelete
                // 
                buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonDelete.Image = Properties.Resources.delete2;
                buttonDelete.ImageAlign = ContentAlignment.MiddleLeft;
                buttonDelete.Location = new Point(272, 234);
                buttonDelete.Name = "buttonDelete";
                buttonDelete.Size = new Size(95, 28);
                buttonDelete.TabIndex = 5;
                buttonDelete.Text = " Delete Item";
                buttonDelete.TextAlign = ContentAlignment.MiddleLeft;
                buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonDelete.UseVisualStyleBackColor = true;
                buttonDelete.Click += buttonDelete_Click;
                // 
                // propertyGrid
                // 
                propertyGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom)
                                      | AnchorStyles.Right;
                propertyGrid.HelpVisible = false;
                propertyGrid.Location = new Point(376, 32);
                propertyGrid.Name = "propertyGrid";
                propertyGrid.Size = new Size(246, 339);
                propertyGrid.TabIndex = 7;
                propertyGrid.ToolbarVisible = false;
                // 
                // labelItemProperties
                // 
                labelItemProperties.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                labelItemProperties.AutoSize = true;
                labelItemProperties.Location = new Point(370, 13);
                labelItemProperties.Name = "labelItemProperties";
                labelItemProperties.Size = new Size(81, 13);
                labelItemProperties.TabIndex = 6;
                labelItemProperties.Text = "Item Properties";
                // 
                // labelWorkspaceCollection
                // 
                labelWorkspaceCollection.AutoSize = true;
                labelWorkspaceCollection.Location = new Point(12, 13);
                labelWorkspaceCollection.Name = "labelWorkspaceCollection";
                labelWorkspaceCollection.Size = new Size(142, 13);
                labelWorkspaceCollection.TabIndex = 0;
                labelWorkspaceCollection.Text = "Workspace Collection";
                // 
                // buttonAddPage
                // 
                buttonAddPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddPage.Image = Properties.Resources.add;
                buttonAddPage.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddPage.Location = new Point(272, 114);
                buttonAddPage.Name = "buttonAddPage";
                buttonAddPage.Size = new Size(95, 28);
                buttonAddPage.TabIndex = 4;
                buttonAddPage.Text = " Page";
                buttonAddPage.TextAlign = ContentAlignment.MiddleLeft;
                buttonAddPage.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddPage.UseVisualStyleBackColor = true;
                buttonAddPage.Click += buttonAddPage_Click;
                // 
                // buttonAddCell
                // 
                buttonAddCell.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddCell.Image = Properties.Resources.add;
                buttonAddCell.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddCell.Location = new Point(272, 148);
                buttonAddCell.Name = "buttonAddCell";
                buttonAddCell.Size = new Size(95, 28);
                buttonAddCell.TabIndex = 9;
                buttonAddCell.Text = " Cell";
                buttonAddCell.TextAlign = ContentAlignment.MiddleLeft;
                buttonAddCell.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddCell.UseVisualStyleBackColor = true;
                buttonAddCell.Click += buttonAddCell_Click;
                // 
                // buttonAddSequence
                // 
                buttonAddSequence.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                buttonAddSequence.Image = Properties.Resources.add;
                buttonAddSequence.ImageAlign = ContentAlignment.MiddleLeft;
                buttonAddSequence.Location = new Point(272, 182);
                buttonAddSequence.Name = "buttonAddSequence";
                buttonAddSequence.Size = new Size(95, 28);
                buttonAddSequence.TabIndex = 9;
                buttonAddSequence.Text = " Sequence";
                buttonAddSequence.TextAlign = ContentAlignment.MiddleLeft;
                buttonAddSequence.TextImageRelation = TextImageRelation.ImageBeforeText;
                buttonAddSequence.UseVisualStyleBackColor = true;
                buttonAddSequence.Click += buttonAddSequence_Click;

                AcceptButton = buttonOK;
                AutoScaleDimensions = new SizeF(6F, 13F);
                AutoScaleMode = AutoScaleMode.Font;
                ClientSize = new Size(634, 414);
                ControlBox = false;
                Controls.Add(treeView);
                Controls.Add(buttonMoveUp);
                Controls.Add(buttonMoveDown);
                Controls.Add(buttonAddPage);
                Controls.Add(buttonAddCell);
                Controls.Add(buttonAddSequence);
                Controls.Add(propertyGrid);
                Controls.Add(buttonDelete);
                Controls.Add(buttonOK);
                Controls.Add(labelWorkspaceCollection);
                Controls.Add(labelItemProperties);
                VisibleChanged += OnVisibleChanged;
                Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                MinimumSize = new Size(501, 344);
                Name = "KryptonWorkspaceCollectionForm";
                StartPosition = FormStartPosition.CenterScreen;
                Text = "Workspace Collection Editor";
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
                    // Need to link the property browser to a site otherwise Image properties cannot be
                    // edited because it cannot navigate to the owning project for its resources
                    propertyGrid.Site = new PropertyGridSite(Context, propertyGrid);

                    // Cache a lookup of all items before changes are made
                    _beforeItems = CreateItemsDictionary(Items);

                    // Add all the top level clones
                    treeView.Nodes.Clear();
                    foreach (Component item in Items)
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
            private void OnVisibleChanged(object sender, EventArgs e)
            {
                if (Visible)
                {
                    _editor.Workspace.SuspendWorkspaceLayout();
                }
                else
                {
                    _editor.Workspace.ResumeWorkspaceLayout();
                }
            }

            private void buttonOK_Click(object sender, EventArgs e)
            {
                // Create an array with all the root items
                var rootItems = new object[treeView.Nodes.Count];
                for (var i = 0; i < rootItems.Length; i++)
                {
                    rootItems[i] = ((MenuTreeNode)treeView.Nodes[i]).Item;
                }

                // Cache a lookup of all items after changes are made
                DictItemBase afterItems = CreateItemsDictionary(rootItems);

                // Update collection with new set of items
                Items = rootItems;

                // Inform designer of changes in component items
                SynchronizeCollections(_beforeItems, afterItems, Context);

                // Notify container that the value has been changed
                Context.OnComponentChanged();

                // Clear down contents of tree as this form can be reused
                treeView.Nodes.Clear();
            }

            private void buttonMoveUp_Click(object sender, EventArgs e)
            {
                // If we have a selected node
                MenuTreeNode node = (MenuTreeNode)treeView.SelectedNode;
                if (node != null)
                {
                    NodeToType(node, out var isNodePage, out var isNodeCell, out var isNodeSequence);

                    // Find the previous node compatible as target for the selected node
                    MenuTreeNode previousNode = (MenuTreeNode)PreviousNode(node);
                    if (previousNode != null)
                    {
                        NodeToType(previousNode, out var isPreviousPage, out var isPreviousCell, out var isPreviousSequence);

                        // If moving a page...
                        if (isNodePage)
                        {
                            // Remove page from parent cell
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            parentNode.CellItem.Pages.Remove(node.PageItem);
                            parentNode.Nodes.Remove(node);

                            // If the previous node is also a page
                            if (isPreviousPage)
                            {
                                // Add page to the parent cell of target page
                                MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                                var pageIndex = previousParent.CellItem.Pages.IndexOf(previousNode.PageItem);

                                // If the current and previous nodes are inside different cells
                                if (previousParent != parentNode)
                                {
                                    // If the page is the last one in the collection then we need to insert afterwards
                                    if (pageIndex == (previousParent.CellItem.Pages.Count - 1))
                                    {
                                        pageIndex++;
                                    }
                                }

                                previousParent.CellItem.Pages.Insert(pageIndex, node.PageItem);
                                previousParent.Nodes.Insert(pageIndex, node);
                            }
                            else if (isPreviousCell)
                            {
                                // Add page as last item of target cell
                                parentNode = previousNode;
                                parentNode.CellItem.Pages.Insert(parentNode.CellItem.Pages.Count, node.PageItem);
                                parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                            }
                        }
                        else if (isNodeCell)
                        {
                            // Is the current node contained inside the next node
                            var contained = ContainsNode(previousNode, node);

                            // Remove cell from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            parentNode?.SequenceItem.Children.Remove(node.CellItem);
                            parentCollection.Remove(node);

                            // If the previous node is also a cell
                            if (isPreviousCell || contained)
                            {
                                // Add cell to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                                parentCollection = (previousNode.Parent == null ? treeView.Nodes : previousNode.Parent.Nodes);
                                var pageIndex = parentCollection.IndexOf(previousNode);

                                // If the current and previous nodes are inside different cells
                                if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                                {
                                    // If the page is the last one in the collection then we need to insert afterwards
                                    if (pageIndex == (previousParent.SequenceItem.Children.Count - 1))
                                    {
                                        pageIndex++;
                                    }
                                }

                                previousParent?.SequenceItem.Children.Insert(pageIndex, node.CellItem);
                                parentCollection.Insert(pageIndex, node);
                            }
                            else if (isPreviousSequence)
                            {
                                // Add cell as last item of target sequence
                                parentNode = previousNode;
                                parentNode.SequenceItem.Children.Insert(parentNode.SequenceItem.Children.Count, node.CellItem);
                                parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                            }
                        }
                        else if (isNodeSequence)
                        {
                            // Is the current node contained inside the next node
                            var contained = ContainsNode(previousNode, node);

                            // Remove sequence from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            parentNode?.SequenceItem.Children.Remove(node.SequenceItem);
                            parentCollection.Remove(node);

                            // If the previous node is also a sequence
                            if (isPreviousCell || contained)
                            {
                                // Add sequence to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                                parentCollection = (previousNode.Parent == null ? treeView.Nodes : previousNode.Parent.Nodes);
                                var pageIndex = parentCollection.IndexOf(previousNode);

                                // If the current and previous nodes are inside different cells
                                if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                                {
                                    // If the page is the last one in the collection then we need to insert afterwards
                                    if (pageIndex == (previousParent.SequenceItem.Children.Count - 1))
                                    {
                                        pageIndex++;
                                    }
                                }

                                previousParent?.SequenceItem.Children.Insert(pageIndex, node.SequenceItem);
                                parentCollection.Insert(pageIndex, node);
                            }
                            else if (isPreviousSequence)
                            {
                                // Add sequence as last item of target sequence
                                parentNode = previousNode;
                                parentNode.SequenceItem.Children.Insert(parentNode.SequenceItem.Children.Count, node.SequenceItem);
                                parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                            }
                        }
                    }
                }

                // Ensure the target node is still selected
                treeView.SelectedNode = node;

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonMoveDown_Click(object sender, EventArgs e)
            {
                // If we have a selected node
                MenuTreeNode node = (MenuTreeNode)treeView.SelectedNode;
                if (node != null)
                {
                    NodeToType(node, out var isNodePage, out var isNodeCell, out var isNodeSequence);

                    // Find the next node compatible as target for the selected node
                    MenuTreeNode nextNode = (MenuTreeNode)NextNode(node);
                    if (nextNode != null)
                    {
                        NodeToType(nextNode, out var isNextPage, out var isNextCell, out var isNextSequence);

                        // If moving a page...
                        if (isNodePage)
                        {
                            // Remove page from parent cell
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            parentNode.CellItem.Pages.Remove(node.PageItem);
                            parentNode.Nodes.Remove(node);

                            // If the next node is also a page
                            if (isNextPage)
                            {
                                // Add page to the parent cell of target page
                                MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                                var pageIndex = previousParent.CellItem.Pages.IndexOf(nextNode.PageItem);
                                previousParent.CellItem.Pages.Insert(pageIndex + 1, node.PageItem);
                                previousParent.Nodes.Insert(pageIndex + 1, node);
                            }
                            else if (isNextCell)
                            {
                                // Add page as first item of target cell
                                parentNode = nextNode;
                                parentNode.CellItem.Pages.Insert(0, node.PageItem);
                                parentNode.Nodes.Insert(0, node);
                            }
                        }
                        else if (isNodeCell)
                        {
                            // Is the current node contained inside the next node
                            var contained = ContainsNode(nextNode, node);

                            // Remove cell from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            parentNode?.SequenceItem.Children.Remove(node.CellItem);
                            parentCollection.Remove(node);

                            // If the next node is also a cell
                            if (isNextCell || contained)
                            {
                                // Add cell to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                                parentCollection = (nextNode.Parent == null ? treeView.Nodes : nextNode.Parent.Nodes);
                                var pageIndex = parentCollection.IndexOf(nextNode);
                                previousParent?.SequenceItem.Children.Insert(pageIndex + 1, node.CellItem);
                                parentCollection.Insert(pageIndex + 1, node);
                            }
                            else if (isNextSequence)
                            {
                                // Add cell as first item of target sequence
                                parentNode = nextNode;
                                parentNode.SequenceItem.Children.Insert(0, node.CellItem);
                                parentNode.Nodes.Insert(0, node);
                            }
                        }
                        else if (isNodeSequence)
                        {
                            // Is the current node contained inside the next node
                            var contained = ContainsNode(nextNode, node);

                            // Remove sequence from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            parentNode?.SequenceItem.Children.Remove(node.SequenceItem);
                            parentCollection.Remove(node);

                            // If the next node is a cell
                            if (isNextCell || contained)
                            {
                                // Add sequence to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                                parentCollection = (nextNode.Parent == null ? treeView.Nodes : nextNode.Parent.Nodes);
                                var pageIndex = parentCollection.IndexOf(nextNode);
                                previousParent?.SequenceItem.Children.Insert(pageIndex + 1, node.SequenceItem);
                                parentCollection.Insert(pageIndex + 1, node);
                            }
                            else if (isNextSequence)
                            {
                                // Add sequence as first item of target sequence
                                parentNode = nextNode;
                                parentNode.SequenceItem.Children.Insert(0, node.SequenceItem);
                                parentNode.Nodes.Insert(0, node);
                            }
                        }
                    }
                }

                // Ensure the target node is still selected
                treeView.SelectedNode = node;

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddPage_Click(object sender, EventArgs e)
            {
                // Create new page and menu node for the page
                KryptonPage page = (KryptonPage)CreateInstance(typeof(KryptonPage));
                TreeNode newNode = new MenuTreeNode(page);

                MenuTreeNode selectedNode = (MenuTreeNode)treeView.SelectedNode;
                if (selectedNode.CellItem != null)
                {
                    // Selected node is a cell, so append page to end of cells page collection
                    selectedNode.CellItem.Pages.Add(page);
                    selectedNode.Nodes.Add(newNode);
                }
                else if (selectedNode.PageItem != null)
                {
                    // Selected node is a page, so insert after this page
                    MenuTreeNode selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                    var selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                    selectedParentNode.CellItem.Pages.Insert(selectedIndex + 1, page);
                    selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                }

                // Selected the newly added node
                treeView.SelectedNode = newNode;
                treeView.Focus();

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddCell_Click(object sender, EventArgs e)
            {
                // Create new cell and menu node for the cell and two nodes for the child pages
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)CreateInstance(typeof(KryptonWorkspaceCell));
                TreeNode newNode = new MenuTreeNode(cell);

                // Add each page inside the new cell as a child of the new node
                foreach (KryptonPage page in cell.Pages)
                {
                    newNode.Nodes.Add(new MenuTreeNode(page));
                }
                newNode.Expand();

                MenuTreeNode selectedNode = (MenuTreeNode)treeView.SelectedNode;
                if (selectedNode == null)
                {
                    // Nothing is selected, so add to the root
                    treeView.Nodes.Add(newNode);
                }
                else if (selectedNode.SequenceItem != null)
                {
                    // Selected node is a sequence, so append cell to end of sequence collection
                    selectedNode.SequenceItem.Children.Add(cell);
                    selectedNode.Nodes.Add(newNode);
                }
                else if (selectedNode.CellItem != null)
                {
                    if (selectedNode.Parent == null)
                    {
                        // Selected node is cell in root, so insert after it in the root
                        treeView.Nodes.Insert(treeView.Nodes.IndexOf(selectedNode) + 1, newNode);
                    }
                    else
                    {
                        // Selected node is a cell, so insert after this cell
                        MenuTreeNode selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                        var selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                        selectedParentNode.SequenceItem.Children.Insert(selectedIndex + 1, cell);
                        selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                    }
                }

                // Selected the newly added node
                treeView.SelectedNode = newNode;
                treeView.Focus();

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddSequence_Click(object sender, EventArgs e)
            {
                // Create new sequence and menu node for the sequence
                KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)CreateInstance(typeof(KryptonWorkspaceSequence));
                TreeNode newNode = new MenuTreeNode(sequence);

                MenuTreeNode selectedNode = (MenuTreeNode)treeView.SelectedNode;
                if (selectedNode == null)
                {
                    // Nothing is selected, so add to the root
                    treeView.Nodes.Add(newNode);
                }
                else if (selectedNode.CellItem != null)
                {
                    if (selectedNode.Parent == null)
                    {
                        // Selected node is cell in root, so insert after it in the root
                        treeView.Nodes.Insert(treeView.Nodes.IndexOf(selectedNode) + 1, newNode);
                    }
                    else
                    {
                        // Selected node is a cell, so insert after this cell
                        MenuTreeNode selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                        var selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                        selectedParentNode.SequenceItem.Children.Insert(selectedIndex + 1, sequence);
                        selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                    }
                }
                else if (selectedNode.SequenceItem != null)
                {
                    // Selected node is a sequence, so append sequence to end of child collection
                    selectedNode.SequenceItem.Children.Add(sequence);
                    selectedNode.Nodes.Add(newNode);
                }

                // Selected the newly added node
                treeView.SelectedNode = newNode;
                treeView.Focus();

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonDelete_Click(object sender, EventArgs e)
            {
                if (treeView.SelectedNode != null)
                {
                    MenuTreeNode treeNode = (MenuTreeNode)treeView.SelectedNode;

                    if (treeNode.Parent == null)
                    {
                        // Remove from the root collection
                        treeView.Nodes.Remove(treeNode);
                    }
                    else
                    {
                        // Remove from parent node collection
                        MenuTreeNode parentNode = (MenuTreeNode)treeNode.Parent;
                        treeNode.Parent.Nodes.Remove(treeNode);

                        // Remove item from parent container
                        if (parentNode.CellItem != null)
                        {
                            parentNode.CellItem.Pages.Remove(treeNode.Item);
                        }
                        else
                        {
                            parentNode.SequenceItem?.Children.Remove(treeNode.Item);
                        }
                    }

                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
            {
                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void NodeToType(TreeNode node, out bool isPage, out bool isCell, out bool isSequence)
            {
                NodeToType((MenuTreeNode)node, out isPage, out isCell, out isSequence);
            }

            private static void NodeToType(MenuTreeNode node, out bool isPage, out bool isCell, out bool isSequence)
            {
                isPage = node?.PageItem != null;
                isCell = node?.CellItem != null;
                isSequence = node?.SequenceItem != null;
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

            private TreeNode NextNode(TreeNode currentNode)
            {
                if (currentNode == null)
                {
                    return null;
                }

                var found = false;
                NodeToType(currentNode, out var isPage, out var isCell, out var isSequence);
                TreeNode returnNode = currentNode;

                do
                {
                    // Find the previous node
                    returnNode = RecursiveFind(treeView.Nodes, returnNode, ref found, isPage, isCell, isSequence, true);

                    // If we actually found a next node
                    if (returnNode != null)
                    {
                        // Cannot move a sequence inside itself
                        if (isSequence && ContainsNode(currentNode, returnNode))
                        {
                            found = false;
                            continue;
                        }
                    }

                    // Found no reason not the accept the found node
                    break;
                
                } while (returnNode != null);

                return returnNode;
            }

            private TreeNode PreviousNode(TreeNode currentNode)
            {
                if (currentNode == null)
                {
                    return null;
                }

                var found = false;
                NodeToType(currentNode, out var isPage, out var isCell, out var isSequence);
                TreeNode returnNode = currentNode;

                do
                {
                    // Find the previous node
                    returnNode = RecursiveFind(treeView.Nodes, returnNode, ref found, isPage, isCell, isSequence, false);

                    // If we actually found a previous node
                    if (returnNode != null)
                    {
                        // If searching from a page that is the first page in the owning cell and the previous
                        // node is actually the owning cell of the page then we need to continue with searching
                        if (isPage && (currentNode.Index == 0) && (returnNode == currentNode.Parent))
                        {
                            found = false;
                            continue;
                        }
                    }

                    // Found no reason not the accept the found node
                    break;
                
                } while (returnNode != null);

                return returnNode;
            }

            private TreeNode RecursiveFind(TreeNodeCollection nodes,
                                           TreeNode target,
                                           ref bool found,
                                           bool findPage,
                                           bool findCell, 
                                           bool findSequence,
                                           bool forward)
            {
                for (var i = 0; i < nodes.Count; i++)
                {
                    TreeNode node = nodes[forward ? i : nodes.Count - 1 - i];
                    NodeToType(node, out var isPage, out var isCell, out var isSequence);

                    // Searching forward we check the node before any child collection
                    if (forward)
                    {
                        if (!found)
                        {
                            found |= (node == target);
                        }
                        else if ((isPage && findPage) || (isCell && (findPage || findCell)) || (isSequence && (findCell || findSequence)))
                        {
                            return node;
                        }
                    }

                    // Do not recurse into the children if looking forwards and at the target sequence
                    if (!(isSequence && findSequence && found && forward))
                    {
                        // Searching the child collection of nodes
                        TreeNode findNode = RecursiveFind(node.Nodes, target, ref found, findPage, findCell, findSequence, forward);

                        // If we found a node to return then return it now
                        if (findNode != null)
                        {
                            return findNode;
                        }
                        else if (found && (target != node))
                        {
                            if ((findCell && (isCell || isSequence)) ||
                                (findSequence && (isCell || isSequence)))
                            {
                                return node;
                            }
                        }

                        // Searching backwards we check the child collection after checking the node
                        if (!forward)
                        {
                            if (!found)
                            {
                                found |= (node == target);
                            }
                            else if ((isPage && findPage) || (isCell && (findPage || findCell)) || (isSequence && (findCell || findSequence)))
                            {
                                return node;
                            }
                        }
                    }
                }

                return null;
            }

            private static void SeparatorToItems(ViewDrawWorkspaceSeparator separator,
                                          out IWorkspaceItem after,
                                          out IWorkspaceItem before)
            {
                // Workspace item after the separator (to the right or below)
                after = separator.WorkspaceItem;

                // Workspace item before the separator (to the left or above)
                KryptonWorkspaceSequence beforeSequence = (KryptonWorkspaceSequence)after.WorkspaceParent;

                // Previous items might be invisible and so search till we find the visible one we expect
                before = null;
                for (var i = beforeSequence.Children.IndexOf(after) - 1; i >= 0; i--)
                {
                    if ((beforeSequence.Children[i] is IWorkspaceItem { WorkspaceVisible: true } item))
                    {
                        before = item;
                        break;
                    }
                }
            }

            private void UpdateButtons()
            {
                MenuTreeNode node = (MenuTreeNode)treeView.SelectedNode;
                var isNone = (node == null);
                var isPage = node?.PageItem != null;
                var isCell = node?.CellItem != null;
                var isSequence = node?.SequenceItem != null;

                buttonMoveUp.Enabled = !isNone && (PreviousNode(node) != null);
                buttonMoveDown.Enabled = !isNone && (NextNode(node) != null);
                buttonAddPage.Enabled = isPage || isCell;
                buttonAddCell.Enabled = isNone || isCell || isSequence;
                buttonAddSequence.Enabled = isNone || isCell || isSequence;
                buttonDelete.Enabled = (node != null);
            }

            private void UpdatePropertyGrid()
            {
                TreeNode node = treeView.SelectedNode;
                if (node == null)
                {
                    propertyGrid.SelectedObject = null;
                }
                else
                {
                    MenuTreeNode menuNode = (MenuTreeNode)node;

                    if (menuNode.PageItem != null)
                    {
                        propertyGrid.SelectedObject = new PageProxy(menuNode.PageItem);
                    }
                    else if (menuNode.CellItem != null)
                    {
                        propertyGrid.SelectedObject = new CellProxy(menuNode.CellItem);
                    }
                    else
                    {
                        propertyGrid.SelectedObject = new SequenceProxy(menuNode.SequenceItem);
                    }
                }
            }

            private DictItemBase CreateItemsDictionary(object[] items)
            {
                DictItemBase dictItems = new();

                foreach (Component item in items)
                {
                    AddItemsToDictionary(dictItems, item);
                }

                return dictItems;
            }

            private void AddItemsToDictionary(DictItemBase dictItems, Component baseItem)
            {
                // Add item to the dictionary
                dictItems.Add(baseItem, baseItem);

                // Add pages from a cell
                if (baseItem is KryptonWorkspaceCell cell)
                {
                    foreach (Component item in cell.Pages)
                    {
                        AddItemsToDictionary(dictItems, item);
                    }
                }

                // Add children from a sequence
                if (baseItem is KryptonWorkspaceSequence sequence)
                {
                    foreach (Component item in sequence.Children)
                    {
                        AddItemsToDictionary(dictItems, item);
                    }
                }
            }

            private void AddMenuTreeNode(Component item, MenuTreeNode parent)
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
                    treeView.Nodes.Add(node);
                }

                // Add pages from a cell
                if (item is KryptonWorkspaceCell cell)
                {
                    foreach (Component page in cell.Pages)
                    {
                        AddMenuTreeNode(page, node);
                    }
                }

                // Add children from a sequence
                if (item is KryptonWorkspaceSequence sequence)
                {
                    foreach (Component child in sequence.Children)
                    {
                        AddMenuTreeNode(child, node);
                    }
                }
            }

            private void SynchronizeCollections(DictItemBase before,
                                                DictItemBase after,
                                                ITypeDescriptorContext context)
            {
                // Add all new components (in the 'after' but not the 'before'
                foreach (Component item in after.Values)
                {
                    if (!before.ContainsKey(item))
                    {
                        context.Container?.Add(item);
                    }
                }

                // Delete all old components (in the 'before' but not the 'after'
                foreach (Component item in before.Values)
                {
                    if (!after.ContainsKey(item))
                    {
                        DestroyInstance(item);

                        context.Container?.Remove(item);
                    }
                }

                IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (changeService != null)
                {
                    // Mark components as changed when not added or removed
                    foreach (Component item in after.Values)
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
        /// Initialize a new instance of the KryptonWorkspaceCollectionEditor class.
        /// </summary>
        public KryptonWorkspaceCollectionEditor()
            : base(typeof(KryptonWorkspaceCollection))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the owning workspace instance.
        /// </summary>
        public KryptonWorkspace Workspace
        {
            get
            {
                KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)Context.Instance;
                return sequence.WorkspaceControl;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm() => new KryptonWorkspaceCollectionForm(this);

        #endregion
    }
}
