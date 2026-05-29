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

using System.Linq;

namespace Krypton.Workspace;

/// <summary>
/// Represents an individual workspace cell.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonWorkspaceCell), "ToolboxBitmaps.KryptonWorkspaceCell.bmp")]
[Designer(typeof(KryptonWorkspaceCellDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Pages))]
public class KryptonWorkspaceCell : KryptonNavigator,
    IWorkspaceItem
{
    #region Instance Fields

    private IWorkspaceItem? _parent;
    private bool _disposeOnRemove;
    private bool _events;
    //seb
    private bool _allowDroppingPages;
    //end seb
    #endregion

    #region Events
    /// <summary>
    /// Occurs after a change has occurred to the collection.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the user clicks the maximize/restore button.
    /// </summary>
    public event EventHandler? MaximizeRestoreClicked;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonWorkspaceCell class.
    /// </summary>
    public KryptonWorkspaceCell()
        : this(@"50*,50*")
    {
    }

    /// <summary>
    /// Initialise a new instance of the KryptonWorkspaceCell class.
    /// </summary>
    /// <param name="starSize">Initial star sizing value.</param>
    public KryptonWorkspaceCell(string starSize)
    {
        // Change Navigator defaults to workspace requirements
        AllowPageDrag = true;
        AllowTabFocus = false;

        // Initialize internal fields
        _disposeOnRemove = true;
        WorkspaceVisible = true;
        WorkspaceStarSize = new StarSize(starSize);
        WorkspaceAllowResizing = true;
        UniqueName = CommonHelper.UniqueString;
        //seb
        _allowDroppingPages = true;
        //end seb

        // We need to know when the set of pages has changed
        Pages.Cleared += OnPagesChanged;
        Pages.Removed += OnPagesChanged;
        Pages.Inserted += OnPagesChanged;
        _events = true;

        // Add a button spec used to handle maximize/restore functionality
        MaximizeRestoreButton = new ButtonSpecNavigator
        {
            Type = PaletteButtonSpecStyle.WorkspaceMaximize
        };
        MaximizeRestoreButton.Click += OnMaximizeRestoreButtonClicked;
        Button.ButtonSpecs!.Add(MaximizeRestoreButton);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        try
        {
            // Must unhook to prevent memory leak
            _events = false;
            Pages.Cleared -= OnPagesChanged;
            Pages.Removed -= OnPagesChanged;
            Pages.Inserted -= OnPagesChanged;

            // Must remove from parent workspace manually because the control collection is readonly
            if (Parent != null)
            {
                var controls = (KryptonReadOnlyControls)Parent.Controls;
                controls.RemoveInternal(this);
            }

            base.Dispose(disposing);
        }
        catch 
        { 
            //
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the docking value of the cell.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
        get => DockStyle.None;

        set
        {
            // The cell must never have dock defined because that would interfere with 
            // layout that using the control Bounds to define its runtime location
        }
    }

    /// <summary>
    /// Gets and sets the control text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public new string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the coordinates of the upper-left corner of the workspace item relative to the upper-left corner of its KryptonWorkspace. 
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Point Location
    {
        get => base.Location;
        set => base.Location = value;
    }

    /// <summary>
    /// Gets or sets the height and width of the workspace item.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Size Size
    {
        get => base.Size;
        set => base.Size = value;
    }

    /// <summary>
    /// Gets or sets the tab order of the workspace item within its KryptonWorkspace.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int TabIndex
    {
        get => base.TabIndex;
        set => base.TabIndex = value;
    }

    /// <summary>
    /// Perform any compacting actions allowed by the flags.
    /// </summary>
    /// <param name="flags">Set of compacting actions allowed.</param>
    public void Compact(CompactFlags flags)
    {
        if (!DesignMode)
        {
        }
    }

    /// <summary>
    /// Should the item be Displayed in the workspace.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public bool WorkspaceVisible { get; private set; }

    /// <summary>
    /// Gets and sets if the user can a separator to resize this workspace cell.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public bool WorkspaceAllowResizing { get; private set; }

    /// <summary>
    /// Current pixel size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public Size WorkspaceActualSize => Size;

    /// <summary>
    /// Current preferred size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public Size WorkspacePreferredSize => IsDisposed ? Size.Empty : GetPreferredSize(WorkspaceMinSize);

    /// <summary>
    /// Get the required size in star notation.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public StarSize WorkspaceStarSize { get; }

    /// <summary>
    /// Get the defined minimum size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Size WorkspaceMinSize => MinimumSize;

    /// <summary>
    /// Get the defined maximum size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Size WorkspaceMaxSize => MaximumSize;

    /// <summary>
    /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override Size MinimumSize
    {
        get => base.MinimumSize;

        set
        {
            if (!base.MinimumSize.Equals(value))
            {
                base.MinimumSize = value;
                OnPropertyChanged(nameof(MinimumSize));
            }
        }
    }

    /// <summary>
    /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override Size MaximumSize
    {
        get => base.MaximumSize;

        set
        {
            if (!base.MaximumSize.Equals(value))
            {
                base.MaximumSize = value;
                OnPropertyChanged(nameof(MaximumSize));
            }
        }
    }

    /// <summary>
    /// Gets access to the parent workspace item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWorkspaceItem? WorkspaceParent
    {
        get => _parent;

        internal set
        {
            if (_parent != value)
            {
                _parent = value;
                if (_parent != null)
                {
                    AttachGlobalEvents();
                }
                else
                {
                    UnattachGlobalEvents();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets if the user can a separator to resize this workspace cell.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if the user can a separator to resize this workspace cell.")]
    [DefaultValue(true)]
    public bool AllowResizing
    {
        get => WorkspaceAllowResizing;

        set
        {
            if (WorkspaceAllowResizing != value)
            {
                WorkspaceAllowResizing = value;
                OnPropertyChanged(nameof(AllowResizing));
            }
        }
    }

    //seb
    /// <summary>
    /// Determines if the user can can drop pages in this workspace cell.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if the user can can drop pages in this workspace cell.")]
    [DefaultValue(true)]
    public bool AllowDroppingPages
    {
        get => _allowDroppingPages;

        set
        {
            _allowDroppingPages = value;
            OnPropertyChanged(nameof(AllowDroppingPages));
        }
    }
    //end seb

    /// <summary>
    /// Star notation the describes the sizing of the workspace item.
    /// </summary>
    [Category(@"Workspace")]
    [Description(@"Star notation for specifying the size of the item.")]
    [DefaultValue("50*,50*")]
    public string StarSize
    {
        get => WorkspaceStarSize.Value;

        set
        {
            WorkspaceStarSize.Value = value;
            OnPropertyChanged(nameof(StarSize));
        }
    }

    /// <summary>
    /// Should the item be disposed when it is removed from the workspace.
    /// </summary>
    [Category(@"Workspace")]
    [Description(@"Should the KryptonNavigator be Disposed when removed from KryptonWorkspace.")]
    [DefaultValue(true)]
    public virtual bool DisposeOnRemove
    {
        get => _disposeOnRemove;
        set => _disposeOnRemove = value;
    }

    /// <summary>
    /// Gets and sets the unique name of the workspace cell.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The unique name of the workspace cell.")]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public string UniqueName
    {
        [DebuggerStepThrough]
        get;
        [DebuggerStepThrough]
        set;
    }


    /// <summary>
    /// Gets access to the maximize/restore button spec.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecNavigator MaximizeRestoreButton { get; }

    /// <summary>
    /// Request this cell save its information.
    /// </summary>
    /// <param name="workspace">Reference to owning workspace instance..</param>
    /// <param name="xmlWriter">Xml writer to save information into.</param>
    public void SaveToXml(KryptonWorkspace workspace, XmlWriter xmlWriter)
    {
        // Output cell values but not the actual customization of appearance
        xmlWriter.WriteStartElement(@"WC");
        workspace.WriteCellElement(xmlWriter, this);

        // Persist each child page in turn
        foreach (KryptonPage page in Pages.Where(static page => page.AreFlagsSet(KryptonPageFlags.AllowConfigSave)))
        {
            xmlWriter.WriteStartElement(@"KP");
            workspace.WritePageElement(xmlWriter, page);

            // Give event handlers a chance to save custom data with the page
            xmlWriter.WriteStartElement(@"CPD");
            workspace.OnPageSaving(new PageSavingEventArgs(workspace, page, xmlWriter));
            xmlWriter.WriteEndElement();

            // Terminate the page element        
            xmlWriter.WriteEndElement();
        }

        // Terminate the cell element        
        xmlWriter.WriteEndElement();
    }

    /// <summary>
    /// Request this cell load and update state.
    /// </summary>
    /// <param name="workspace">Reference to owning workspace instance.</param>
    /// <param name="xmlReader">Xml reader for loading information.</param>
    /// <param name="existingPages">Dictionary on existing pages before load.</param>
    public void LoadFromXml(KryptonWorkspace workspace,
        XmlReader xmlReader,
        UniqueNameToPage existingPages)
    {
        // Load the cell details and return the unique name of the selected page for the cell
        var selectedPageUniqueName = workspace.ReadCellElement(xmlReader, this);
        KryptonPage? selectedPage = null;

        // If the cell contains nothing then exit immediately
        if (!xmlReader.IsEmptyElement)
        {
            do
            {
                // Read the next Element
                if (!xmlReader.Read())
                {
                    throw new ArgumentException(@"An element was expected but could not be read in.");
                }

                // Is this the end of the cell
                if (xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }

                if (xmlReader.Name == @"KP")
                {
                    // Load the page details and optionally recreate the page
                    var uniqueName = XmlHelper.XmlAttributeToText(xmlReader, @"UN");
                    KryptonPage? page = workspace.ReadPageElement(xmlReader, uniqueName, existingPages);

                    if (xmlReader.Name != @"CPD")
                    {
                        throw new ArgumentException(@"Expected 'CPD' element was not found");
                    }

                    var finished = xmlReader.IsEmptyElement;

                    // Generate event so custom data can be loaded and/or the page to be added can be modified
                    var plea = new PageLoadingEventArgs(workspace, page, xmlReader);
                    workspace.OnPageLoading(plea);
                    page = plea.Page;

                    // Read everything until we get the end of custom data marker
                    while (!finished)
                    {
                        // Check it has the expected name
                        if (xmlReader.NodeType == XmlNodeType.EndElement)
                        {
                            finished = (xmlReader.Name == @"CPD");
                        }

                        if (!finished)
                        {
                            if (!xmlReader.Read())
                            {
                                throw new ArgumentException(@"An element was expected but could not be read in.");
                            }
                        }
                    }

                    // Read past the end of page element                    
                    if (!xmlReader.Read())
                    {
                        throw new ArgumentException(@"An element was expected but could not be read in.");
                    }

                    // Check it has the expected name
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        throw new ArgumentException(@"End of 'KP' element expected but missing.");
                    }

                    // PageLoading event might have nulled the page value to prevent it being added
                    if (page != null)
                    {
                        // Remember the page that should become selected
                        if (!string.IsNullOrEmpty(page.UniqueName) && (page.UniqueName == selectedPageUniqueName))
                        {
                            // Can only select a visible page
                            if (page.LastVisibleSet)
                            {
                                selectedPage = page;
                            }
                        }

                        Pages.Add(page);
                    }
                }
                else
                {
                    throw new ArgumentException(@"Unknown element was encountered.");
                }
            }
            while (true);
        }

        // Did we find a matching page that should become selected?
        // (and we are allowed to have selected tabs)
        if ((selectedPage != null) && AllowTabSelect)
        {
            SelectedPage = selectedPage;
        }
    }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool LastVisibleSet
    {
        get => WorkspaceVisible;
        set => WorkspaceVisible = value;
    }

    /// <summary>
    /// Output debug information about the workspace hierarchy.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void DebugOutput(int indent)
    {
        Console.WriteLine(@"{0}Cell Count:{1} Visible:{2}", new string(' ', indent++ * 2), Pages.Count, LastVisibleSet);

        var prefix = new string(' ', indent * 2);
        foreach (KryptonPage page in Pages)
        {
            Console.WriteLine(@"{0}Page Text:{1} Visible:{2} Type:{3}", prefix, page.Text, page.LastVisibleSet, page.GetType().Name);
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Should the OnInitialized call perform layout.
    /// </summary>
    protected override bool LayoutOnInitialized => false;

    /// <summary>
    /// Sets the control to the specified visible state. 
    /// </summary>
    /// <param name="value">true to make the control visible; otherwise, false.</param>
    protected override void SetVisibleCore(bool value)
    {
        if (WorkspaceVisible != value)
        {
            WorkspaceVisible = value;
            OnPropertyChanged(nameof(Visible));
        }

        base.SetVisibleCore(value);
    }

    /// <summary>
    /// Gets the child panel used for displaying actual pages.
    /// </summary>
    protected internal KryptonGroupPanel? CellChildPanel => ChildPanel;

    /// <summary>
    /// Called by the designer to hit test a point.
    /// </summary>
    /// <param name="pt">Point to be tested.</param>
    /// <returns>True if a hit otherwise false.</returns>
    protected internal bool CellDesignerGetHitTest(Point pt) => DesignerGetHitTest(pt);

    /// <summary>
    /// Called by the designer to get the component associated with the point.
    /// </summary>
    /// <param name="pt">Point to be tested.</param>
    /// <returns>Component associated with point or null.</returns>
    protected internal Component? CellDesignerComponentFromPoint(Point pt) => DesignerComponentFromPoint(pt);

    /// <summary>
    /// Called by the designer to indicate that the mouse has left the control.
    /// </summary>
    protected internal void CellDesignerMouseLeave() =>
        // ReSharper disable RedundantBaseQualifier
        base.DesignerMouseLeave();// ReSharper restore RedundantBaseQualifier

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="property">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string property) => OnPropertyChanged(new PropertyChangedEventArgs(property));

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    #endregion

    #region Implementation
    private void OnPagesChanged(object? sender, EventArgs e)
    {
        // Need to raise property changed so that the owning workspace will layout as 
        // a change in pages might cause compacting to perform extra actions.
        if (_events)
        {
            OnPropertyChanged(nameof(Pages));
        }
    }

    private void OnMaximizeRestoreButtonClicked(object? sender, EventArgs e) => MaximizeRestoreClicked?.Invoke(this, EventArgs.Empty);
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Size GetMinSize()
    {
        var sizeBefore = Size;
        var childSizeBefore = ChildPanel!.Size;
        var minSize = GetMinSize(Controls);
        ChildPanel.MinimumSize = minSize;

        // Now determine the size of the Secondary header and bottoms buttons for this theme
        switch (Header.HeaderPositionBar)
        {
            case VisualOrientation.Top:
            case VisualOrientation.Bottom:
                minSize.Height += (sizeBefore.Height - childSizeBefore.Height) + 10 /*Padding.Vertical*/;
                break;
            case VisualOrientation.Left:
            case VisualOrientation.Right:
                minSize.Width += (sizeBefore.Width - childSizeBefore.Width) + 10 /*Padding.Horizontal*/;
                break;
        }

        MinimumSize = minSize;
        return minSize;
    }

    private static Size GetMinSize(ControlCollection controls)
    {
        var minWidth = int.MinValue;
        var minHeight = int.MinValue;
        foreach (var kryptonPage in controls.OfType<Control>())
        {
            var childMinSize = GetMinSize(kryptonPage.Controls);
            if (minWidth < childMinSize.Width)
            {
                minWidth = childMinSize.Width;
            }
            if (minHeight < childMinSize.Height)
            {
                minHeight = childMinSize.Height;
            }
            if (minWidth < kryptonPage.MinimumSize.Width)
            {
                minWidth = kryptonPage.MinimumSize.Width;
            }
            if (minHeight < kryptonPage.MinimumSize.Height)
            {
                minHeight = kryptonPage.MinimumSize.Height;
            }
        }

        return new Size(minWidth, minHeight);
    }

}

/// <summary>
/// Manages a list of KryptonWorkspaceCell instances.
/// </summary>
public class KryptonWorkspaceCellList : List<KryptonWorkspaceCell> { }