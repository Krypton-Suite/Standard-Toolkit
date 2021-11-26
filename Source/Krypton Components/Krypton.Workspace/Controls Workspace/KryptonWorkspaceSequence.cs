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
    /// <summary>
    /// Represents a sequence of workspace items.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonWorkspaceSequence), "ToolboxBitmaps.KryptonWorkspaceSequence.bmp")]
    [TypeConverter(typeof(KryptonWorkspaceSequenceConverter))]
    [Designer("Krypton.Workspace.KryptonWorkspaceSequenceDesigner, Krypton.Workspace")]
    [DesignTimeVisible(false)]
    [DesignerCategory("code")]
    [DefaultProperty("Children")]
    public class KryptonWorkspaceSequence : Component,
                                            IWorkspaceItem
    {
        #region Instance Fields

        private Orientation _orientation;
        private bool _setVisible;

        #endregion

        #region Events
        /// <summary>
        /// Occurs after a change has occured to the workspace.
        /// </summary>
        [Browsable(false)]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the user clicks the maximize/restore button.
        /// </summary>
        [Browsable(false)]
        public event EventHandler MaximizeRestoreClicked;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonWorkspaceSequence class.
        /// </summary>
        public KryptonWorkspaceSequence()
            : this(Orientation.Horizontal)
        {
        }

        /// <summary>
        /// Initialise a new instance of the KryptonWorkspaceSequence class.
        /// </summary>
        /// <param name="orientation">Initial orientation of the children.</param>
        public KryptonWorkspaceSequence(Orientation orientation)
        {
            _orientation = orientation;

            // Create the child collection for holding items
            Children = new KryptonWorkspaceCollection(this);
            Children.PropertyChanged += OnChildrenPropertyChanged;
            Children.MaximizeRestoreClicked += OnChildrenMaximizeRestoreClicked;

            // Default properties
            _setVisible = true;
            WorkspaceStarSize = new StarSize();
            WorkspaceActualSize = Size.Empty;
            UniqueName = CommonHelper.UniqueString;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Children != null)
                {
                    for (var i = Children.Count - 1; i >= 0; i--)
                    {
                        Children[i].Dispose();
                    }

                    Children.PropertyChanged -= OnChildrenPropertyChanged;
                    Children.Clear();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() => Orientation + " (" + Children.Count.ToString() + " Children)";

        #endregion

        #region Public
        /// <summary>
        /// Gets access to the collection of child workspace items.
        /// </summary>
        [Category("Workspace")]
        [Description("Collection of child workspace items.")]
        [MergableProperty(false)]
        [Editor(@"Krypton.Workspace.KryptonWorkspaceCollectionEditor, Krypton.Workspace", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonWorkspaceCollection Children { get; }

        /// <summary>
        /// Gets and sets the orientation for laying out the child entries.
        /// </summary>
        [Category("Workspace")]
        [Description("Orientation to layout the child entries.")]
        [DefaultValue(typeof(Orientation), "Horizontal")]
        public Orientation Orientation
        {
            get => _orientation;

            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    OnPropertyChanged("Orientation");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the sequence is displayed.
        /// </summary>
        [Category("Visuals")]
        [Description("Should the sequence be visible.")]
        [DefaultValue(true)]
        public bool Visible
        {
            get => _setVisible;

            set
            {
                if (_setVisible != value)
                {
                    _setVisible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        /// <summary>
        /// Conceals the control from the user.
        /// </summary>
        public void Hide() => Visible = false;

        /// <summary>
        /// Displays the control to the user.
        /// </summary>
        public void Show() => Visible = true;

        /// <summary>
        /// Star notation the describes the sizing of the workspace item.
        /// </summary>
        [Category("Workspace")]
        [Description("Star notation for specifying the size of the item.")]
        [DefaultValue("50*,50*")]
        public string StarSize
        {
            get => WorkspaceStarSize.Value;

            set
            {
                WorkspaceStarSize.Value = value;
                OnPropertyChanged("StarSize");
            }
        }

        /// <summary>
        /// Gets and sets the unique name of the workspace sequence.
        /// </summary>
        [Category("Appearance")]
        [Description("The unique name of the workspace sequence.")]
        public string UniqueName
        {
            [DebuggerStepThrough]
            get;
            [DebuggerStepThrough]
            set;
        }

        /// <summary>
        /// Resets the UniqueName property to its default value.
        /// </summary>
        public void ResetUniqueName()
        {
            UniqueName = CommonHelper.UniqueString;
        }

        /// <summary>
        /// Perform any compacting actions allowed by the flags.
        /// </summary>
        /// <param name="flags">Set of compacting actions allowed.</param>
        public void Compact(CompactFlags flags)
        {
            if (!DesignMode)
            {
                // Compact each child
                foreach (Component component in Children)
                {
                    if (component is IWorkspaceItem item)
                    {
                        item.Compact(flags);
                    }
                }

                CompactRemoveEmptyCells(flags);
                CompactRemoveEmptySequences(flags);
                CompactPromoteLeafs(flags);
            }
        }

        /// <summary>
        /// Gets access to the parent workspace item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IWorkspaceItem WorkspaceParent { get; internal set; }

        /// <summary>
        /// Current pixel size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspaceActualSize { get; internal set; }

        /// <summary>
        /// Current preferred size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspacePreferredSize
        {
            get
            {
                Size totalSize = Size.Empty;

                // Search all the children to find the largest preferred size in each direction
                foreach (Component component in Children)
                {
                    // Get can only grab values from the IWorkspaceItem interface
                    if ((component is IWorkspaceItem item) && item.WorkspaceVisible)
                    {
                        StarSize itemStar = item.WorkspaceStarSize;
                        var preferredWidth = (!itemStar.StarWidth.UsingStar && (itemStar.StarWidth.FixedSize < 0));
                        var preferredHeight = (!itemStar.StarHeight.UsingStar && (itemStar.StarHeight.FixedSize < 0));

                        // Does the item specify a preferred size in either direction?
                        if (preferredWidth || preferredHeight)
                        {
                            Size itemPreferred = item.WorkspacePreferredSize;

                            // Track the largest values found
                            if (preferredWidth)
                            {
                                totalSize.Width = Math.Max(totalSize.Width, itemPreferred.Width);
                            }
                            if (preferredHeight)
                            {
                                totalSize.Height = Math.Max(totalSize.Height, itemPreferred.Height);
                            }
                        }
                    }
                }

                return totalSize;
            }
        }

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
        public Size WorkspaceMinSize
        {
            get
            {
                Size minSize = Size.Empty;

                // Search all the children to find the largest minimum/fixed size
                foreach (Component component in Children)
                {
                    // Get can only grab values from the IWorkspaceItem interface
                    if ((component is IWorkspaceItem item) && item.WorkspaceVisible)
                    {
                        // Sequence minimum is the largest min value of the children
                        Size itemMin = item.WorkspaceMinSize;
                        minSize.Width = Math.Max(minSize.Width, itemMin.Width);
                        minSize.Height = Math.Max(minSize.Height, itemMin.Height);

                        // Sequence minimum should be enough to show fixed size items
                        StarSize itemStar = item.WorkspaceStarSize;
                        if (!itemStar.StarWidth.UsingStar)
                        {
                            minSize.Width = Math.Max(minSize.Width, itemStar.StarWidth.FixedSize);
                        }

                        if (!itemStar.StarHeight.UsingStar)
                        {
                            minSize.Height = Math.Max(minSize.Height, itemStar.StarHeight.FixedSize);
                        }
                    }
                }

                return minSize;
            }
        }

        /// <summary>
        /// Get the defined maximum size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspaceMaxSize
        {
            get
            {
                Size maxSize = new(int.MaxValue, int.MaxValue);

                // Search all children for the smallest defined maximum
                foreach (Component component in Children)
                {
                    // Get can only grab values from the IWorkspaceItem interface
                    if ((component is IWorkspaceItem item) && item.WorkspaceVisible)
                    {
                        // Sequence maximum is the smallest min value of the children
                        Size itemMax = item.WorkspaceMaxSize;
                        if (itemMax.Width > 0)
                        {
                            maxSize.Width = Math.Min(maxSize.Width, itemMax.Width);
                        }

                        if (itemMax.Height > 0)
                        {
                            maxSize.Height = Math.Min(maxSize.Height, itemMax.Height);
                        }
                    }
                }

                // Ensure that the maximum is big enough to allow any fixed item to show
                // (have to do this after the above loop as the fixed calculation should
                //  override any values found in the above loop. doing it above would have
                //  allowed subsequence items to override the fixed setting of previous items)
                foreach (Component component in Children)
                {
                    // Get can only grab values from the IWorkspaceItem interface
                    if ((component is IWorkspaceItem item) && item.WorkspaceVisible)
                    {
                        // Sequence maximum should be enough to show fixed size items
                        StarSize itemStar = item.WorkspaceStarSize;
                        if (!itemStar.StarWidth.UsingStar)
                        {
                            maxSize.Width = maxSize.Width < int.MaxValue
                                ? Math.Max(maxSize.Width, itemStar.StarWidth.FixedSize)
                                : itemStar.StarWidth.FixedSize;
                        }

                        if (!itemStar.StarHeight.UsingStar)
                        {
                            if (maxSize.Height < int.MaxValue)
                            {
                                maxSize.Height = Math.Max(maxSize.Height, itemStar.StarHeight.FixedSize);
                            }
                            else
                            {
                                maxSize.Width = itemStar.StarWidth.FixedSize;
                            }
                        }
                    }
                }

                // If no maximum is encountered then use a zero value and not the int.MaxValue
                return new Size(maxSize.Width == int.MaxValue ? 0 : maxSize.Width,
                                maxSize.Height == int.MaxValue ? 0 : maxSize.Height);
            }
        }

        /// <summary>
        /// Gets and sets if the user can a separator to resize this workspace cell.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool WorkspaceAllowResizing
        {
            get
            {
                // If any child says no resizing then we cannot be resized
                foreach (Component component in Children)
                {
                    if ((component is IWorkspaceItem item) && item.WorkspaceVisible && !item.WorkspaceAllowResizing)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Should the item be displayed in the workspace.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool WorkspaceVisible
        {
            get
            {
                // Only if we are visible do we need to check the children
                if (Visible)
                {
                    // If we have any visible children then we are visible
                    foreach (Component component in Children)
                    {
                        if ((component is IWorkspaceItem item) && item.WorkspaceVisible)
                        {
                            return true;
                        }
                    }
                }

                // If we set invisible or all the children are invisible then don't show
                return false;
            }
        }

        /// <summary>
        /// Should the item be disposed when it is removed from the workspace.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool DisposeOnRemove => false;

        /// <summary>
        /// Request this sequence save its information about children.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace instance.</param>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        public void SaveToXml(KryptonWorkspace workspace, XmlWriter xmlWriter)
        {
            // Output standard values appropriate for all Sequence instances
            xmlWriter.WriteStartElement("WS");
            workspace.WriteSequenceElement(xmlWriter, this);

            // Persist each child sequence/cell in turn
            foreach (object child in Children)
            {
                if (child is KryptonWorkspaceSequence sequence)
                {
                    sequence.SaveToXml(workspace, xmlWriter);
                }

                if (child is KryptonWorkspaceCell cell)
                {
                    cell.SaveToXml(workspace, xmlWriter);
                }
            }

            // Terminate the workspace element        
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Request this sequence load and recreate children.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace instance.</param>
        /// <param name="xmlReader">Xml reader for loading information.</param>
        /// <param name="existingPages">Dictionary on existing pages before load.</param>
        public void LoadFromXml(KryptonWorkspace workspace,
                                XmlReader xmlReader,
                                UniqueNameToPage existingPages)
        {
            // Load the sequence details
            workspace.ReadSequenceElement(xmlReader, this);

            // If the sequence contains nothing then exit immediately
            if (!xmlReader.IsEmptyElement)
            {
                do
                {
                    // Read the next Element
                    if (!xmlReader.Read())
                    {
                        throw new ArgumentException("An element was expected but could not be read in.");
                    }

                    // Is this the end of the sequence
                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }

                    // Is it another sequence?
                    switch (xmlReader.Name)
                    {
                        case "WS":
                            KryptonWorkspaceSequence sequence = new();
                            sequence.LoadFromXml(workspace, xmlReader, existingPages);
                            Children.Add(sequence);
                            break;
                        case "WC":
                            KryptonWorkspaceCell cell = new();
                            cell.LoadFromXml(workspace, xmlReader, existingPages);
                            Children.Add(cell);
                            break;
                        default:
                            throw new ArgumentException("Unknown element was encountered.");
                    }
                }
                while (true);
            }
        }

        /// <summary>
        /// Internal method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public KryptonWorkspace WorkspaceControl { get; set; }

        /// <summary>
        /// Output debug information about the workspace hierarchy.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DebugOutput(int indent)
        {
            Console.WriteLine("{0}Sequence Count:{1} Visible:{2} Orientation:{3}", new string(' ', indent * 2), Children.Count, Visible, Orientation.ToString());

            foreach (object child in Children)
            {
                if (child is KryptonWorkspaceSequence sequence)
                {
                    sequence.DebugOutput(indent + 1);
                }

                if (child is KryptonWorkspaceCell cell)
                {
                    cell.DebugOutput(indent + 1);
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Handle a change in the child collection of items.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments associated with the event.</param>
        protected void OnChildrenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        /// <summary>
        /// Handle a maximize/restore request from a child item.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments associated with the event.</param>
        protected void OnChildrenMaximizeRestoreClicked(object sender, EventArgs e)
        {
            MaximizeRestoreClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="property">Name of property that has changed.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        #endregion

        #region Implementation
        private void CompactRemoveEmptyCells(CompactFlags flags)
        {
            if ((flags & CompactFlags.RemoveEmptyCells) == CompactFlags.RemoveEmptyCells)
            {
                // Search for child cell items
                for (var i = Children.Count - 1; i >= 0; i--)
                {
                    // If a cell and that cell does not have any pages
                    if ((Children[i] is KryptonWorkspaceCell cell) && (cell.Pages.Count == 0))
                    {
                        Children.RemoveAt(i);

                        // If the cell is not inside a controls collection then we need to dispose of it here
                        // because the layout code will never try and dispose on remove from controls collection
                        // as it is not inside the controls collection
                        if ((cell.Parent == null) && (cell.DisposeOnRemove))
                        {
                            cell.Dispose();
                        }
                    }
                }
            }
        }

        private void CompactRemoveEmptySequences(CompactFlags flags)
        {
            if ((flags & CompactFlags.RemoveEmptySequences) == CompactFlags.RemoveEmptySequences)
            {
                // Search for child sequence items
                for (var i = Children.Count - 1; i >= 0; i--)
                {
                    // If a sequence and that sequence does not have any children
                    if ((Children[i] is KryptonWorkspaceSequence sequence) && (sequence.Children.Count == 0))
                    {
                        Children.RemoveAt(i);
                    }
                }
            }
        }

        private void CompactPromoteLeafs(CompactFlags flags)
        {
            if ((flags & CompactFlags.PromoteLeafs) == CompactFlags.PromoteLeafs)
            {
                // Search for child sequence items
                for (var i = Children.Count - 1; i >= 0; i--)
                {
                    // If a sequence and that sequence has just a single child
                    if ((Children[i] is KryptonWorkspaceSequence sequence) && (sequence.Children.Count == 1))
                    {
                        // Extract the leaf
                        Component leaf = sequence.Children[0];
                        sequence.Children.RemoveAt(0);

                        // Use the sequence size in the promoted child so the display remains constant
                        if (leaf is KryptonWorkspaceCell cell)
                        {
                            cell.StarSize = sequence.StarSize;
                        }

                        if (leaf is KryptonWorkspaceSequence workspaceSequence)
                        {
                            workspaceSequence.StarSize = sequence.StarSize;
                        }

                        // Replace the sequence with its leaf child
                        Children.RemoveAt(i);
                        Children.Insert(i, leaf);
                    }
                }
            }
        }
        #endregion
    }
}
