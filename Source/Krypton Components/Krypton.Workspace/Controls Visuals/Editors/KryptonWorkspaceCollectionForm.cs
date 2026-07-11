#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

using System.Linq;

namespace Krypton.Workspace;

internal partial class KryptonWorkspaceCollectionForm : VisualDesignerCollectionForm
{
        #region Types
        /// <summary>
        /// Simple class to reduce the length of declaractions!
        /// </summary>
        private class DictItemBase : Dictionary<Component, Component> { }

        /// <summary>
        /// Act as proxy for krypton page item to control the exposed properties to the property grid.
        /// </summary>
        private class PageProxy
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
            public PaletteNavigatorRedirect? StateCommon => _item.StateCommon;

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
            public Bitmap? ImageSmall
            {
                get => _item.ImageSmall;
                set => _item.ImageSmall = value;
            }

            /// <summary>
            /// Gets and sets the medium image for the page.
            /// </summary>
            [Category(@"Appearance")]
            [DefaultValue(null)]
            public Bitmap? ImageMedium
            {
                get => _item.ImageMedium;
                set => _item.ImageMedium = value;
            }

            /// <summary>
            /// Gets and sets the large image for the page.
            /// </summary>
            [Category(@"Appearance")]
            [DefaultValue(null)]
            public Bitmap? ImageLarge
            {
                get => _item.ImageLarge;
                set => _item.ImageLarge = value;
            }

            /// <summary>
            /// Gets and sets the page tooltip image.
            /// </summary>
            [Category(@"Appearance")]
            [DefaultValue(null)]
            public Bitmap? ToolTipImage
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
            // ToDo V120 LTS: Migrate designer editor to KryptonDesignerMultilineStringEditor (replaces System.ComponentModel.Design.MultilineStringEditor).
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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
            // ToDo V120 LTS: Migrate designer editor to KryptonDesignerMultilineStringEditor (replaces System.ComponentModel.Design.MultilineStringEditor).
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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
            [DefaultValue(typeof(LabelStyle), nameof(ToolTip))]
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
            public KryptonContextMenu? KryptonContextMenu
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
            public object? Tag
            {
                get => _item.Tag;
                set => _item.Tag = value;
            }
            #endregion
        }

        /// <summary>
        /// Act as proxy for workspace cell item to control the exposed properties to the property grid.
        /// </summary>
        private class CellProxy
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
            public PaletteNavigatorRedirect? StateCommon => _item.StateCommon;

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
            public KryptonPage? SelectedPage
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
            public KryptonContextMenu? KryptonContextMenu
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
            public object? Tag
            {
                get => _item.Tag;
                set => _item.Tag = value;
            }
            #endregion
        }

        /// <summary>
        /// Act as proxy for workspace sequence item to control the exposed properties to the property grid.
        /// </summary>
        private class SequenceProxy
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
        private class MenuTreeNode : TreeNode
        {
            #region Static Fields
            private static int _id = 1;
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
                    Text = $"Page ({PageItem?.Text})";
                }

                CellItem = item as KryptonWorkspaceCell;
                if (CellItem != null)
                {
                    CellItem.PropertyChanged += OnCellPropertyChanged;
                    Text = $"Cell ({CellItem?.StarSize})";
                }

                SequenceItem = item as KryptonWorkspaceSequence;
                if (SequenceItem != null)
                {
                    SequenceItem.PropertyChanged += OnSequencePropertyChanged;
                    Text = $"{SequenceItem?.Orientation} ({SequenceItem?.StarSize})";
                }
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets access to the associated workspace cell item.
            /// </summary>
            public Component? Item => PageItem ?? (CellItem ?? SequenceItem as Component);

            /// <summary>
            /// Gets access to the associated workspace cell item.
            /// </summary>
            public KryptonPage? PageItem { get; }

            /// <summary>
            /// Gets access to the associated workspace cell item.
            /// </summary>
            public KryptonWorkspaceCell? CellItem { get; }

            /// <summary>
            /// Gets access to the associated workspace sequence item.
            /// </summary>
            public KryptonWorkspaceSequence? SequenceItem { get; }

            /// <summary>
            /// Gets the instance identifier.
            /// </summary>
            public int InstanceId { get; }

            #endregion

            #region Implementation
            private void OnPageTextChanged(object? sender, EventArgs e) => Text = $"Page ({PageItem?.Text})";

            private void OnCellPropertyChanged(object? sender, PropertyChangedEventArgs e) => Text = $"Cell ({CellItem?.StarSize})";

            private void OnSequencePropertyChanged(object? sender, PropertyChangedEventArgs e) => Text = $"{SequenceItem?.Orientation} ({SequenceItem?.StarSize})";
            #endregion
        }

        /// <summary>
        /// Site that allows the property grid to discover Visual Studio services.
        /// </summary>
        private class PropertyGridSite : ISite, IServiceProvider
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
    private readonly KryptonWorkspaceCollectionEditor? _editor;
    private DictItemBase _beforeItems = null!;
    private List<Component> _sessionStartRootOrder = null!;
    private Dictionary<KryptonWorkspaceSequence, List<Component>> _sessionStartSequenceChildren = null!;
    private Dictionary<KryptonWorkspaceCell, List<KryptonPage>> _sessionStartCellPages = null!;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonWorkspaceCollectionForm"/> class for the WinForms designer.
    /// </summary>
    public KryptonWorkspaceCollectionForm()
        : base()
    {
        InitializeComponent();
        ConfigureDesignerChrome();
    }

    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceCollectionForm class.
    /// </summary>
    public KryptonWorkspaceCollectionForm(KryptonWorkspaceCollectionEditor editor)
        : base(editor)
    {
        _editor = editor;
        InitializeComponent();
        ConfigureDesignerChrome();
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

            // Need to link the property browser to a site otherwise Image properties cannot be
            // edited because it cannot navigate to the owning project for its resources
            _propertyGrid.Site = new PropertyGridSite(Context!, _propertyGrid);
            ApplyWorkspacePalette();

            // Cache a lookup of all items before changes are made
            _beforeItems = CreateItemsDictionary(Items);
            CaptureSessionSnapshot();

            // Add all the top level clones
            _treeView.Nodes.Clear();
            foreach (Component item in Items)
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
    private void ConfigureDesignerChrome()
    {
        InternalDesignerEditorFormChrome.Apply(this, kpnlContent, kpnlButtonBar);
        kpnlButtonBar.OkButton.Values.Text = KryptonManager.Strings.GeneralStrings.OK;
        kpnlButtonBar.CancelButton.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        kpnlButtonBar.OkButton.Click += buttonOK_Click;
        kpnlButtonBar.CancelButton.Click += buttonCancel_Click;
    }

    private static void ConfigureToolbarButton(KryptonButton button, Image image, string text, EventHandler click)
        {
            button.ButtonStyle = ButtonStyle.ListItem;
            button.Values.Image = image;
            button.Values.Text = text;
            button.Click += click;
        }

    private void ApplyWorkspacePalette()
    {
        if (_editor is null)
        {
            return;
        }

        var workspace = _editor.Workspace;
            var mode = workspace.PaletteMode;
            var custom = workspace.Palette as KryptonCustomPaletteBase;
            if (mode == PaletteMode.Global)
            {
                mode = KryptonManager.CurrentGlobalPaletteMode;
                custom = KryptonManager.CurrentGlobalPalette as KryptonCustomPaletteBase;
            }

            KryptonDesignerEditorTheme.ApplyToForm(this, mode, custom);
        }

    private void OnVisibleChanged(object? sender, EventArgs e)
    {
        if (_editor is null)
        {
            return;
        }

        if (Visible)
            {
                _editor.Workspace.SuspendWorkspaceLayout();
            }
            else
            {
                _editor.Workspace.ResumeWorkspaceLayout();
            }
        }

        private void buttonOK_Click(object? sender, EventArgs e)
        {
            // Create an array with all the root items
            object[] rootItems = new object[_treeView.Nodes.Count];
            for (var i = 0; i < rootItems.Length; i++)
            {
                rootItems[i] = ((MenuTreeNode)_treeView.Nodes[i]).Item!;
            }

            // Cache a lookup of all items after changes are made
            DictItemBase afterItems = CreateItemsDictionary(rootItems);

            // Update collection with new set of items
            Items = rootItems;

            // Write items back to the collection instance for the designer EditValue return path
            CommitDesignerItems();

            // Inform designer of changes in component items
            SynchronizeCollections(_beforeItems, afterItems, Context!);

            // Notify container that the value has been changed
            Context!.OnComponentChanged();

            // Clear down contents of tree as this form can be reused
            _treeView.Nodes.Clear();
        }

        private void buttonCancel_Click(object? sender, EventArgs e)
        {
            RevertSessionChanges();
            _treeView.Nodes.Clear();
        }

        private void RevertSessionChanges()
        {
            DiscardAddedDesignerItems();
            RestoreSessionHierarchy();
        }

        private void CaptureSessionSnapshot()
        {
            _sessionStartRootOrder = Items!.Cast<Component>().ToList();
            _sessionStartSequenceChildren = new Dictionary<KryptonWorkspaceSequence, List<Component>>();
            _sessionStartCellPages = new Dictionary<KryptonWorkspaceCell, List<KryptonPage>>();

            foreach (Component item in _beforeItems.Keys)
            {
                switch (item)
                {
                    case KryptonWorkspaceSequence sequence:
                        _sessionStartSequenceChildren[sequence] = new List<Component>(sequence.Children!.Cast<Component>());
                        break;
                    case KryptonWorkspaceCell cell:
                        _sessionStartCellPages[cell] = new List<KryptonPage>(cell.Pages.Cast<KryptonPage>());
                        break;
                }
            }
        }

        private void RestoreSessionHierarchy()
        {
            foreach (KeyValuePair<KryptonWorkspaceCell, List<KryptonPage>> entry in _sessionStartCellPages)
            {
                entry.Key.Pages.Clear();
                foreach (KryptonPage page in entry.Value)
                {
                    entry.Key.Pages.Add(page);
                }
            }

            foreach (KeyValuePair<KryptonWorkspaceSequence, List<Component>> entry in _sessionStartSequenceChildren)
            {
                entry.Key.Children!.Clear();
                foreach (Component child in entry.Value)
                {
                    entry.Key.Children!.Add(child);
                }
            }

            Items = _sessionStartRootOrder.ToArray();
            CommitDesignerItems();
        }

        private void DiscardAddedDesignerItems()
        {
            var rootItems = new object[_treeView.Nodes.Count];
            for (var i = 0; i < rootItems.Length; i++)
            {
                rootItems[i] = ((MenuTreeNode)_treeView.Nodes[i]).Item!;
            }

            DictItemBase currentItems = CreateItemsDictionary(rootItems);

            foreach (Component item in currentItems.Values.Where(item => !_beforeItems.ContainsKey(item)))
            {
                DestroyInstance(item);
                Context?.Container?.Remove(item);
            }
        }

        private void buttonMoveUp_Click(object? sender, EventArgs e)
        {
            // If we have a selected node
            var node = _treeView.SelectedNode as MenuTreeNode;
            if (node != null)
            {
                NodeToType(node, out var isNodePage, out var isNodeCell, out var isNodeSequence);

                // Find the previous node compatible as target for the selected node
                if (PreviousNode(node) is MenuTreeNode previousNode)
                {
                    NodeToType(previousNode, out var isPreviousPage, out var isPreviousCell, out var isPreviousSequence);

                    // If moving a page...
                    if (isNodePage)
                    {
                        // Remove page from parent cell
                        var parentNode = node.Parent as MenuTreeNode ?? throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull(nameof(node.Parent)));

                        parentNode.CellItem!.Pages.Remove(node.PageItem!);
                        parentNode.Nodes.Remove(node);

                        // If the previous node is also a page
                        if (isPreviousPage)
                        {
                            // Add page to the parent cell of target page
                            var previousParent = previousNode.Parent as MenuTreeNode;
                            var pageIndex = previousParent!.CellItem!.Pages.IndexOf(previousNode.PageItem!);

                            // If the current and previous nodes are inside different cells
                            if (previousParent != parentNode)
                            {
                                // If the page is the last one in the collection then we need to insert afterwards
                                if (pageIndex == (previousParent.CellItem.Pages.Count - 1))
                                {
                                    pageIndex++;
                                }
                            }

                            previousParent.CellItem.Pages.Insert(pageIndex, node.PageItem!);
                            previousParent.Nodes.Insert(pageIndex, node);
                        }
                        else if (isPreviousCell)
                        {
                            // Add page as last item of target cell
                            parentNode = previousNode;
                            parentNode.CellItem!.Pages.Insert(parentNode.CellItem.Pages.Count, node.PageItem!);
                            parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                        }
                    }
                    else if (isNodeCell)
                    {
                        // Is the current node contained inside the next node
                        var contained = ContainsNode(previousNode, node);

                        // Remove cell from parent collection
                        var parentNode = node.Parent as MenuTreeNode;
                        TreeNodeCollection parentCollection = (node.Parent == null ? _treeView.Nodes : node.Parent.Nodes);
                        parentNode?.SequenceItem!.Children!.Remove(node.CellItem!);
                        parentCollection.Remove(node);

                        // If the previous node is also a cell
                        if (isPreviousCell || contained)
                        {
                            // Add cell to the parent sequence of target cell
                            var previousParent = previousNode.Parent as MenuTreeNode;
                            parentCollection = (previousNode.Parent == null ? _treeView.Nodes : previousNode.Parent.Nodes);
                            var pageIndex = parentCollection.IndexOf(previousNode);

                            // If the current and previous nodes are inside different cells
                            if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                            {
                                // If the page is the last one in the collection then we need to insert afterwards
                                if (pageIndex == (previousParent.SequenceItem!.Children!.Count - 1))
                                {
                                    pageIndex++;
                                }
                            }

                            previousParent?.SequenceItem!.Children!.Insert(pageIndex, node.CellItem);
                            parentCollection.Insert(pageIndex, node);
                        }
                        else if (isPreviousSequence)
                        {
                            // Add cell as last item of target sequence
                            parentNode = previousNode;
                            parentNode.SequenceItem!.Children!.Insert(parentNode.SequenceItem.Children.Count, node.CellItem);
                            parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                        }
                    }
                    else if (isNodeSequence)
                    {
                        // Is the current node contained inside the next node
                        var contained = ContainsNode(previousNode, node);

                        // Remove sequence from parent collection
                        var parentNode = node.Parent as MenuTreeNode;
                        TreeNodeCollection parentCollection = (node.Parent == null ? _treeView.Nodes : node.Parent.Nodes);
                        parentNode?.SequenceItem!.Children!.Remove(node.SequenceItem!);
                        parentCollection.Remove(node);

                        // If the previous node is also a sequence
                        if (isPreviousCell || contained)
                        {
                            // Add sequence to the parent sequence of target cell
                            var previousParent = previousNode.Parent as MenuTreeNode;
                            parentCollection = (previousNode.Parent == null ? _treeView.Nodes : previousNode.Parent.Nodes);
                            var pageIndex = parentCollection.IndexOf(previousNode);

                            // If the current and previous nodes are inside different cells
                            if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                            {
                                // If the page is the last one in the collection then we need to insert afterwards
                                if (pageIndex == (previousParent.SequenceItem!.Children!.Count - 1))
                                {
                                    pageIndex++;
                                }
                            }

                            previousParent?.SequenceItem!.Children!.Insert(pageIndex, node.SequenceItem);
                            parentCollection.Insert(pageIndex, node);
                        }
                        else if (isPreviousSequence)
                        {
                            // Add sequence as last item of target sequence
                            parentNode = previousNode;
                            parentNode.SequenceItem!.Children!.Insert(parentNode.SequenceItem.Children.Count, node.SequenceItem);
                            parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                        }
                    }
                }
            }

            // Ensure the target node is still selected
            _treeView.SelectedNode = node;

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonMoveDown_Click(object? sender, EventArgs e)
        {
            // If we have a selected node
            var node = _treeView.SelectedNode as MenuTreeNode;
            if (node != null)
            {
                NodeToType(node, out var isNodePage, out var isNodeCell, out var isNodeSequence);

                // Find the next node compatible as target for the selected node
                if (NextNode(node) is MenuTreeNode nextNode)
                {
                    NodeToType(nextNode, out var isNextPage, out var isNextCell, out var isNextSequence);

                    // If moving a page...
                    if (isNodePage)
                    {
                        // Remove page from parent cell
                        var parentNode = node.Parent as MenuTreeNode;
                        parentNode!.CellItem!.Pages.Remove(node.PageItem!);
                        parentNode.Nodes.Remove(node);

                        // If the next node is also a page
                        if (isNextPage)
                        {
                            // Add page to the parent cell of target page
                            var previousParent = nextNode.Parent as MenuTreeNode;
                            var pageIndex = previousParent!.CellItem!.Pages.IndexOf(nextNode.PageItem!);
                            previousParent.CellItem.Pages.Insert(pageIndex + 1, node.PageItem!);
                            previousParent.Nodes.Insert(pageIndex + 1, node);
                        }
                        else if (isNextCell)
                        {
                            // Add page as first item of target cell
                            parentNode = nextNode;
                            parentNode.CellItem!.Pages.Insert(0, node.PageItem!);
                            parentNode.Nodes.Insert(0, node);
                        }
                    }
                    else if (isNodeCell)
                    {
                        // Is the current node contained inside the next node
                        var contained = ContainsNode(nextNode, node);

                        // Remove cell from parent collection
                        var parentNode = node.Parent as MenuTreeNode;
                        TreeNodeCollection parentCollection = (node.Parent == null ? _treeView.Nodes : node.Parent.Nodes);
                        parentNode?.SequenceItem!.Children!.Remove(node.CellItem!);
                        parentCollection.Remove(node);

                        // If the next node is also a cell
                        if (isNextCell || contained)
                        {
                            // Add cell to the parent sequence of target cell
                            var previousParent = nextNode.Parent as MenuTreeNode;
                            parentCollection = (nextNode.Parent == null ? _treeView.Nodes : nextNode.Parent.Nodes);
                            var pageIndex = parentCollection.IndexOf(nextNode);
                            previousParent?.SequenceItem!.Children!.Insert(pageIndex + 1, node.CellItem);
                            parentCollection.Insert(pageIndex + 1, node);
                        }
                        else if (isNextSequence)
                        {
                            // Add cell as first item of target sequence
                            parentNode = nextNode;
                            parentNode.SequenceItem!.Children!.Insert(0, node.CellItem);
                            parentNode.Nodes.Insert(0, node);
                        }
                    }
                    else if (isNodeSequence)
                    {
                        // Is the current node contained inside the next node
                        var contained = ContainsNode(nextNode, node);

                        // Remove sequence from parent collection
                        var parentNode = node.Parent as MenuTreeNode;
                        TreeNodeCollection parentCollection = (node.Parent == null ? _treeView.Nodes : node.Parent.Nodes);
                        parentNode?.SequenceItem!.Children!.Remove(node.SequenceItem!);
                        parentCollection.Remove(node);

                        // If the next node is a cell
                        if (isNextCell || contained)
                        {
                            // Add sequence to the parent sequence of target cell
                            var previousParent = nextNode.Parent as MenuTreeNode;
                            parentCollection = (nextNode.Parent == null ? _treeView.Nodes : nextNode.Parent.Nodes);
                            var pageIndex = parentCollection.IndexOf(nextNode);
                            previousParent?.SequenceItem!.Children!.Insert(pageIndex + 1, node.SequenceItem);
                            parentCollection.Insert(pageIndex + 1, node);
                        }
                        else if (isNextSequence)
                        {
                            // Add sequence as first item of target sequence
                            parentNode = nextNode;
                            parentNode.SequenceItem!.Children!.Insert(0, node.SequenceItem);
                            parentNode.Nodes.Insert(0, node);
                        }
                    }
                }
            }

            // Ensure the target node is still selected
            _treeView.SelectedNode = node;

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonAddPage_Click(object? sender, EventArgs e)
        {
            // Create new page and menu node for the page
            var page = (KryptonPage)CreateInstance(typeof(KryptonPage));
            TreeNode newNode = new MenuTreeNode(page);

            var selectedNode = (MenuTreeNode)_treeView.SelectedNode!;
                
            if (selectedNode.CellItem != null)
            {
                // Selected node is a cell, so append page to end of cells page collection
                selectedNode.CellItem.Pages.Add(page);
                selectedNode.Nodes.Add(newNode);
            }
            else if (selectedNode.PageItem != null)
            {
                // Selected node is a page, so insert after this page
                var selectedParentNode = (MenuTreeNode)selectedNode.Parent!;
                var selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                selectedParentNode.CellItem!.Pages.Insert(selectedIndex + 1, page);
                selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
            }

            // Selected the newly added node
            _treeView.SelectedNode = newNode;
            _treeView.Focus();

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonAddCell_Click(object? sender, EventArgs e)
        {
            // Create new cell and menu node for the cell and two nodes for the child pages
            var cell = (KryptonWorkspaceCell)CreateInstance(typeof(KryptonWorkspaceCell));
            TreeNode newNode = new MenuTreeNode(cell);

            // Add each page inside the new cell as a child of the new node
            foreach (KryptonPage page in cell.Pages)
            {
                newNode.Nodes.Add(new MenuTreeNode(page));
            }
            newNode.Expand();

            var selectedNode = _treeView.SelectedNode as MenuTreeNode;

            if (selectedNode is null)
            {
                // Nothing is selected, so add to the root
                _treeView.Nodes.Add(newNode);
            }
            else if (selectedNode.SequenceItem != null)
            {
                // Selected node is a sequence, so append cell to end of sequence collection
                selectedNode.SequenceItem.Children!.Add(cell);
                selectedNode.Nodes.Add(newNode);
            }
            else if (selectedNode.CellItem is not null)
            {
                if (selectedNode.Parent is null)
                {
                    // Selected node is cell in root, so insert after it in the root
                    _treeView.Nodes.Insert(_treeView.Nodes.IndexOf(selectedNode) + 1, newNode);
                }
                else
                {
                    // Selected node is a cell, so insert after this cell
                    var selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                    var selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                    selectedParentNode.SequenceItem!.Children!.Insert(selectedIndex + 1, cell);
                    selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                }
            }

            // Selected the newly added node
            _treeView.SelectedNode = newNode;
            _treeView.Focus();

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonAddSequence_Click(object? sender, EventArgs e)
        {
            // Create new sequence and menu node for the sequence
            var sequence = CreateInstance(typeof(KryptonWorkspaceSequence)) as KryptonWorkspaceSequence ?? throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull("sequence"));
            TreeNode newNode = new MenuTreeNode(sequence);

            var selectedNode = _treeView.SelectedNode as MenuTreeNode;
            if (selectedNode == null)
            {
                // Nothing is selected, so add to the root
                _treeView.Nodes.Add(newNode);
            }
            else if (selectedNode.CellItem != null)
            {
                if (selectedNode.Parent == null)
                {
                    // Selected node is cell in root, so insert after it in the root
                    _treeView.Nodes.Insert(_treeView.Nodes.IndexOf(selectedNode) + 1, newNode);
                }
                else
                {
                    // Selected node is a cell, so insert after this cell
                    var selectedParentNode = selectedNode.Parent as MenuTreeNode ?? throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull(nameof(selectedNode.Parent)));
                    var selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                    selectedParentNode.SequenceItem!.Children!.Insert(selectedIndex + 1, sequence);
                    selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                }
            }
            else if (selectedNode.SequenceItem != null)
            {
                // Selected node is a sequence, so append sequence to end of child collection
                selectedNode.SequenceItem.Children!.Add(sequence);
                selectedNode.Nodes.Add(newNode);
            }

            // Selected the newly added node
            _treeView.SelectedNode = newNode;
            _treeView.Focus();

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void buttonDelete_Click(object? sender, EventArgs e)
        {
            if (_treeView.SelectedNode is not null)
            {
                var treeNode = (MenuTreeNode)_treeView.SelectedNode!;

                if (treeNode.Parent is null)
                {
                    // Remove from the root collection
                    _treeView.Nodes.Remove(treeNode);
                }
                else
                {
                    // Remove from parent node collection
                    var parentNode = (MenuTreeNode)treeNode.Parent;
                    treeNode.Parent.Nodes.Remove(treeNode);

                    // Remove item from parent container
                    if (parentNode.CellItem != null)
                    {
                        parentNode.CellItem.Pages.Remove(treeNode.Item!);
                    }
                    else
                    {
                        parentNode.SequenceItem?.Children!.Remove(treeNode.Item!);
                    }
                }

                _treeView.Focus();
            }

            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void treeView_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            UpdateButtons();
            UpdatePropertyGrid();
        }

        private void NodeToType(TreeNode node, out bool isPage, out bool isCell, out bool isSequence) => NodeToType((MenuTreeNode)node, out isPage, out isCell, out isSequence);

        private static void NodeToType(MenuTreeNode? node, out bool isPage, out bool isCell, out bool isSequence)
        {
            isPage = node?.PageItem != null;
            isCell = node?.CellItem != null;
            isSequence = node?.SequenceItem != null;
        }

        private bool ContainsNode(TreeNode node, TreeNode find)
        {
            return node.Nodes.Contains(find) || node.Nodes.Cast<TreeNode>().Any(child => ContainsNode(child, find));
        }

        private TreeNode? NextNode(TreeNode? currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }

            var found = false;
            NodeToType(currentNode, out var isPage, out var isCell, out var isSequence);
            TreeNode? returnNode = currentNode;

            do
            {
                // Find the previous node
                returnNode = RecursiveFind(_treeView.Nodes, returnNode, ref found, isPage, isCell, isSequence, true);

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

        private TreeNode? PreviousNode(TreeNode? currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }

            var found = false;
            NodeToType(currentNode, out var isPage, out var isCell, out var isSequence);
            TreeNode? returnNode = currentNode;

            do
            {
                // Find the previous node
                returnNode = RecursiveFind(_treeView.Nodes, returnNode, ref found, isPage, isCell, isSequence, false);

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

        private TreeNode? RecursiveFind(TreeNodeCollection nodes,
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
                    TreeNode? findNode = RecursiveFind(node.Nodes, target, ref found, findPage, findCell, findSequence, forward);

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
            out IWorkspaceItem? before)
        {
            // Workspace item after the separator (to the right or below)
            after = separator.WorkspaceItem;

            // Workspace item before the separator (to the left or above)
            var beforeSequence = after.WorkspaceParent as KryptonWorkspaceSequence ?? throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull(nameof(after.WorkspaceParent)));

            // Previous items might be invisible and so search till we find the visible one we expect
            before = null;
            for (var i = beforeSequence.Children!.IndexOf(after) - 1; i >= 0; i--)
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
            var node = _treeView.SelectedNode as MenuTreeNode;
            var isNone = (node == null);
            var isPage = node?.PageItem != null;
            var isCell = node?.CellItem != null;
            var isSequence = node?.SequenceItem != null;

            _buttonMoveUp.Enabled = !isNone && (PreviousNode(node) != null);
            _buttonMoveDown.Enabled = !isNone && (NextNode(node) != null);
            _buttonAddPage.Enabled = isPage || isCell;
            _buttonAddCell.Enabled = isNone || isCell || isSequence;
            _buttonAddSequence.Enabled = isNone || isCell || isSequence;
            _buttonDelete.Enabled = (node != null);
        }

        private void UpdatePropertyGrid()
        {
            TreeNode? node = _treeView.SelectedNode;

            if (node is null)
            {
                _propertyGrid.SelectedObject = null;
            }
            else
            {
                var menuNode = (MenuTreeNode)node;

                if (menuNode.PageItem != null)
                {
                    _propertyGrid.SelectedObject = new PageProxy(menuNode.PageItem);
                }
                else if (menuNode.CellItem != null)
                {
                    _propertyGrid.SelectedObject = new CellProxy(menuNode.CellItem);
                }
                else
                {
                    _propertyGrid.SelectedObject = new SequenceProxy(menuNode.SequenceItem!);
                }
            }
        }

        private DictItemBase CreateItemsDictionary(object?[] items)
        {
            var dictItems = new DictItemBase();

            foreach (Component? item in items)
            {
                AddItemsToDictionary(dictItems, item);
            }

            return dictItems;
        }

        private void AddItemsToDictionary(DictItemBase dictItems, Component? baseItem)
        {
            // Add item to the dictionary
            if (dictItems is not null && baseItem is not null)
            {
                dictItems.Add(baseItem, baseItem);

                switch (baseItem)
                {
                    // Add pages from a cell
                    case KryptonWorkspaceCell cell:
                        foreach (Component? item in cell.Pages)
                        {
                            AddItemsToDictionary(dictItems, item);
                        }

                        break;
                    // Add children from a sequence
                    case KryptonWorkspaceSequence sequence:
                        foreach (Component? item in sequence.Children!)
                        {
                            AddItemsToDictionary(dictItems, item);
                        }

                        break;
                }
            }
        }

        private void AddMenuTreeNode(Component item, MenuTreeNode? parent)
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

            switch (item)
            {
                // Add pages from a cell
                case KryptonWorkspaceCell cell:
                    foreach (Component page in cell.Pages)
                    {
                        AddMenuTreeNode(page, node);
                    }

                    break;
                // Add children from a sequence
                case KryptonWorkspaceSequence sequence:
                    foreach (Component child in sequence.Children!)
                    {
                        AddMenuTreeNode(child, node);
                    }

                    break;
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

            if (GetService(typeof(IComponentChangeService)) is IComponentChangeService changeService)
            {
                // Mark components as changed when not added or removed
                foreach (Component item in after.Values.Where(before.ContainsKey))
                {
                    changeService.OnComponentChanging(item, null);
                    changeService.OnComponentChanged(item, null, null, null);
                }
            }
        }
        #endregion
    }
