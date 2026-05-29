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

/// <summary>
/// Enforce mutual exclusive for a group of KryptonCheckButton controls.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCheckSet), "ToolboxBitmaps.KryptonCheckSet.bmp")]
[DefaultEvent(nameof(CheckedButtonChanged))]
[DefaultProperty(nameof(CheckButtons))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonCheckSetDesigner))]
[Description(@"Provide exclusive checked logic for a set of KryptonCheckButton controls.")]
public class KryptonCheckSet : Component,
    ISupportInitialize
{
    #region Type Definitions
    /// <summary>
    /// Manages a collection of KryptonCheckButton references.
    /// </summary>
    public class KryptonCheckButtonCollection : CollectionBase
    {
        #region Instance Fields
        private readonly KryptonCheckSet _owner;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckButtonCollection class.
        /// </summary>
        /// <param name="owner">Owning component</param>
        public KryptonCheckButtonCollection([DisallowNull] KryptonCheckSet owner)
        {
            Debug.Assert(owner != null);
            _owner = owner!;
        }
        #endregion

        #region Public

        /// <summary>
        /// Adds the specifies KryptonCheckButton to the collection.
        /// </summary>
        /// <param name="checkButton">The KryptonCheckButton object to add to the collection.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>The index of the new entry.</returns>
        public int Add([DisallowNull] KryptonCheckButton checkButton)
        {
            Debug.Assert(checkButton != null);

            if (checkButton == null)
            {
                throw new ArgumentNullException(nameof(checkButton));
            }

            if (Contains(checkButton))
            {
                throw new ArgumentException(@"Reference already exists in the collection", nameof(checkButton));
            }

            // ReSharper disable RedundantBaseQualifier
            base.List.Add(checkButton);

            return base.List.Count - 1;
            // ReSharper restore RedundantBaseQualifier
        }

        /// <summary>
        /// Determines whether a KryptonCheckButton is in the collection.
        /// </summary>
        /// <param name="checkButton">The KryptonCheckButton to locate in the collection.</param>
        /// <returns>True if found in collection; otherwise false.</returns>
        public bool Contains(KryptonCheckButton? checkButton) =>
            // ReSharper disable RedundantBaseQualifier
            base.List.Contains(checkButton);
        // ReSharper restore RedundantBaseQualifier

        /// <summary>
        /// Returns the index of the KryptonCheckButton reference.
        /// </summary>
        /// <param name="checkButton">The KryptonCheckButton to locate.</param>
        /// <returns>Index of reference; otherwise -1.</returns>
        public int IndexOf(KryptonCheckButton? checkButton) =>
            // ReSharper disable RedundantBaseQualifier
            base.List.IndexOf(checkButton);
        // ReSharper restore RedundantBaseQualifier

        /// <summary>
        /// Inserts a KryptonCheckButton reference into the collection at the specified location.
        /// </summary>
        /// <param name="index">Index of position to insert.</param>
        /// <param name="checkButton">The KryptonCheckButton reference to insert.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void Insert(int index, [DisallowNull] KryptonCheckButton checkButton)
        {
            Debug.Assert(checkButton != null);

            if (checkButton == null)
            {
                throw new ArgumentNullException(nameof(checkButton));
            }

            if ((index < 0) || (index > Count))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (Contains(checkButton))
            {
                throw new ArgumentException(@"Reference already in collection", nameof(checkButton));
            }

            // ReSharper disable RedundantBaseQualifier
            base.List.Insert(index, checkButton);
            // ReSharper restore RedundantBaseQualifier
        }

        /// <summary>
        /// Removes a KryptonCheckButton from the collection.
        /// </summary>
        /// <param name="checkButton">The KryptonCheckButton to remove.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void Remove([DisallowNull] KryptonCheckButton checkButton)
        {
            Debug.Assert(checkButton != null);

            if (checkButton == null)
            {
                throw new ArgumentNullException(nameof(checkButton));
            }

            if (!Contains(checkButton))
            {
                throw new ArgumentException(@"No matching reference to remove", nameof(checkButton));
            }

            // ReSharper disable RedundantBaseQualifier
            base.List.Remove(checkButton);
            // ReSharper restore RedundantBaseQualifier
        }

        /// <summary>
        /// Gets the KryptonCheckButton at the specified index.
        /// </summary>
        /// <param name="index">Index of entry to return.</param>
        /// <returns>Reference of KryptonCheckButton instance.</returns>
        public KryptonCheckButton? this[int index]
        {
            get
            {
                if ((index < 0) || (index > Count))
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                // ReSharper disable RedundantBaseQualifier
                return base.List[index] as KryptonCheckButton;
                // ReSharper restore RedundantBaseQualifier
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Occurs when the collection is about to be cleared.
        /// </summary>
        protected override void OnClear()
        {
            // ReSharper disable RedundantBaseQualifier
            foreach (KryptonCheckButton checkButton in base.List)
                // ReSharper restore RedundantBaseQualifier
            {
                _owner.CheckButtonRemoved(checkButton);
            }

            base.OnClear();
        }

        /// <summary>
        /// Occurs when a new entry has been added to the collection.
        /// </summary>
        /// <param name="index">Index of new entry.</param>
        /// <param name="value">Value at the new index.</param>
        protected override void OnInsertComplete(int index, object? value)
        {
            _owner.CheckButtonAdded(value as KryptonCheckButton);
            base.OnInsertComplete(index, value);
        }

        /// <summary>
        /// Occurs when an entry has been removed from the collection.
        /// </summary>
        /// <param name="index">Index of the removed entry.</param>
        /// <param name="value">Value at the removed entry.</param>
        protected override void OnRemoveComplete(int index, object? value)
        {
            _owner.CheckButtonRemoved(value as KryptonCheckButton);
            base.OnRemoveComplete(index, value);
        }

        /// <summary>
        /// Occurs when a index has a value replaced.
        /// </summary>
        /// <param name="index">Index of the change in value.</param>
        /// <param name="oldValue">Value being replaced.</param>
        /// <param name="newValue">Value to be used.</param>
        protected override void OnSetComplete(int index, object? oldValue, object? newValue)
        {
            _owner.CheckButtonRemoved(oldValue as KryptonCheckButton);
            _owner.CheckButtonAdded(newValue as KryptonCheckButton);
            base.OnSetComplete(index, oldValue, newValue);
        }
        #endregion
    }
    #endregion

    #region Instance Fields
    private bool _initializing;
    private bool _checkedChanged;
    private bool _ignoreEvents;
    private KryptonCheckButton? _checkedButton;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the CheckedButton property has changed.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs whenever the CheckedButton property has changed.")]
    public event EventHandler? CheckedButtonChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckSet class.
    /// </summary>
    public KryptonCheckSet() => CheckButtons = new KryptonCheckButtonCollection(this);

    /// <summary>
    /// Initialize a new instance of the KryptonCheckSet class.
    /// </summary>
    /// <param name="container">Container that owns the component.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public KryptonCheckSet([DisallowNull] IContainer container)
        : this()
    {
        Debug.Assert(container != null);

        // Validate reference parameter
        if (container == null)
        {
            throw new ArgumentNullException(nameof(container));
        }

        container.Add(this);
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Signals the object that initialization is starting.
    /// </summary>
    public void BeginInit()
    {
        _checkedChanged = false;
        _initializing = true;
    }

    /// <summary>
    /// Signals the object that initialization is complete.
    /// </summary>
    public void EndInit()
    {
        _initializing = false;

        if (_checkedChanged)
        {
            OnCheckedButtonChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the checked button is allowed to be unchecked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Is the current checked button allowed to be unchecked.")]
    [DefaultValue(false)]
    public bool AllowUncheck { get; set; }

    /// <summary>
    /// Gets and sets the currently checked button in the set.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Determine which of the associated buttons is checked.")]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof(KryptonCheckedButtonConverter))]
    [DefaultValue(null)]
    public KryptonCheckButton? CheckedButton
    {
        get => _checkedButton;

        set
        {
            if (_checkedButton != value)
            {
                // Check the new target is associated with us already
                if ((value != null) && !CheckButtons.Contains(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        @"Provided value is not a KryptonCheckButton associated with this set.");
                }

                // Prevent processing events caused by ourself
                _ignoreEvents = true;

                if (_checkedButton != null)
                {
                    _checkedButton.Checked = false;
                }

                _checkedButton = value;

                if (_checkedButton != null)
                {
                    _checkedButton.Checked = true;
                }

                _ignoreEvents = false;

                // Generate event to show the value has changed
                OnCheckedButtonChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets the index of the checked button.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Determine the index of the checked button.")]
    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(-1)]
    public int CheckedIndex
    {
        get => CheckedButton == null ? -1 : CheckButtons.IndexOf(CheckedButton);

        set
        {
            // Check for a value outside of limits
            if ((value < -1) || (value >= CheckButtons.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            // Special case the value of -1 as requesting nothing checked
            CheckedButton = value == -1 ? null : CheckButtons[value];
        }
    }

    /// <summary>
    /// Gets access to the collection of KryptonCheckButton references.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine which of the associated buttons is checked.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonCheckButtonCollectionEditor), typeof(UITypeEditor))]
    [RefreshProperties(RefreshProperties.All)]
    public KryptonCheckButtonCollection CheckButtons { get; }

    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the CheckedButtonChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnCheckedButtonChanged(EventArgs e)
    {
        if (!_initializing)
        {
            CheckedButtonChanged?.Invoke(this, e);
        }
        else
        {
            _checkedChanged = true;
        }
    }
    #endregion

    #region Implementation
    private void CheckButtonAdded(KryptonCheckButton? checkButton)
    {
        if (checkButton == null)
        {
            return;
        }

        // If the incoming button is already checked
        if (checkButton.Checked)
        {
            // If we already have a button checked
            if (_checkedButton != null)
            {
                // Then uncheck the incoming button
                checkButton.Checked = false;
            }
            else
            {
                // Remember this as the currently checked instance
                _checkedButton = checkButton;

                // Generate event to show the value has changed
                OnCheckedButtonChanged(EventArgs.Empty);
            }
        }

        // Need to monitor and control the change of checked state
        checkButton.CheckedChanging += OnCheckedChanging;
        checkButton.CheckedChanged += OnCheckedChanged;
    }

    private void CheckButtonRemoved(KryptonCheckButton? checkButton)
    {
        // Unhook from monitoring events
        if (checkButton != null)
        {
            checkButton.CheckedChanging -= OnCheckedChanging;
            checkButton.CheckedChanged -= OnCheckedChanged;
        }

        // If the removed button is the currently checked one
        if (_checkedButton == checkButton)
        {
            // Then we no longer have a currently checked button
            _checkedButton = null;

            // Generate event to show the value has changed
            OnCheckedButtonChanged(EventArgs.Empty);
        }
    }

    private void OnCheckedChanging(object? sender, CancelEventArgs e)
    {
        // Are we allowed to process the event?
        if (!_ignoreEvents)
        {
            // Cast to the correct type
            var checkedButton = sender as KryptonCheckButton ?? throw new ArgumentNullException(nameof(sender));

            // Prevent the checked button becoming unchecked unless AllowUncheck is defined
            e.Cancel = checkedButton.Checked && !AllowUncheck;
        }
    }

    private void OnCheckedChanged(object? sender, EventArgs e)
    {
        // Are we allowed to process the event?
        if (!_ignoreEvents)
        {
            // Cast to the correct type
            var checkedButton = sender as KryptonCheckButton ?? throw new ArgumentNullException(nameof(sender));

            if (checkedButton.Checked)
            {
                // Uncheck the currently checked button
                if (_checkedButton != null)
                {
                    // Prevent processing events caused by ourself
                    _ignoreEvents = true;
                    _checkedButton.Checked = false;
                    _ignoreEvents = false;
                }

                // Remember the newly checked button
                _checkedButton = checkedButton;
            }
            else
            {
                // No check button is checked anymore
                _checkedButton = null;
            }

            // Generate event to show the value has changed
            OnCheckedButtonChanged(EventArgs.Empty);
        }
    }
    #endregion
}