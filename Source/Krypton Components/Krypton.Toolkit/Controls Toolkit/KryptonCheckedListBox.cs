#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a CheckedListBox with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCheckedListBox), "ToolboxBitmaps.KryptonCheckedListBox.bmp")]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(Items))]
[DefaultBindingProperty(nameof(SelectedValue))]
[Designer(typeof(KryptonCheckedListBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"Represents a checked list box control that allows single or multiple item selection.")]
public class KryptonCheckedListBox : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    /// <summary>
    /// Encapsulates the collection of indexes of checked items (including items in an indeterminate state) in a CheckedListBox.
    /// </summary>
    public class CheckedIndexCollection : IList
    {
        #region Instance Fields
        private readonly KryptonCheckedListBox _owner;
        private readonly InternalCheckedListBox _internalListBox;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CheckedIndexCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning control.</param>
        internal CheckedIndexCollection(KryptonCheckedListBox owner)
        {
            _owner = owner;
            _internalListBox = (InternalCheckedListBox)owner.ListBox;
        }
        #endregion

        #region Public
        /// <summary>
        /// Determines whether the collection contains the button spec.
        /// </summary>
        /// <param name="item">Object reference.</param>
        /// <returns>True if button spec found; otherwise false.</returns>
        public bool Contains(object? item) => IndexOf(item) != -1;

        /// <summary>
        /// Copies all the elements of the current collection to the specified Array.
        /// </summary>
        /// <param name="array">The Array that is the destination of the elements copied from the collection.</param>
        /// <param name="index">The index in array at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            var count = _owner.CheckedItems.Count;
            for (var i = 0; i < count; i++)
            {
                array.SetValue(this[i], i + index);
            }
        }

        /// <summary>
        /// Enumerate using non-generic interface.
        /// </summary>
        /// <returns>Enumerator instance.</returns>
        public IEnumerator GetEnumerator()
        {
            var dest = new int[Count];
            CopyTo(dest, 0);
            return dest.GetEnumerator();
        }

        /// <summary>
        /// Returns an index into the collection of checked indexes.
        /// </summary>
        /// <param name="index">The index of the checked item.</param>
        /// <returns>-1 if not found; otherwise index position.</returns>
        public int IndexOf(int index)
        {
            if ((index >= 0) && (index < _owner.Items.Count))
            {
                var entryObject = InnerArrayGetEntryObject(index, 0);
                return _owner.CheckedItems.IndexOfIdentifier(entryObject);
            }
            return -1;
        }

        /// <summary>
        /// Determines the index of the specified spec in the collection.
        /// </summary>
        /// <param name="item">Object reference.</param>
        /// <returns>-1 if not found; otherwise index position.</returns>
        public int IndexOf(object? item) => item is int i ? IndexOf(i) : -1;

        /// <summary>
        /// Gets the number of items in collection.
        /// </summary>
        public int Count => _owner.CheckedItems.Count;

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">Item index.</param>
        /// <returns>Item at specified index.</returns>
        public object? this[int index]
        {
            get
            {
                var entryObject = InnerArrayGetEntryObject(index, CheckedItemCollection._anyItemMask);
                return InnerArrayIndexOfIdentifier(entryObject, 0);
            }

            set => throw new NotSupportedException(@"Read Only Collection");
        }
        #endregion

        #region Private
        int IList.Add(object? value) => throw new NotSupportedException(@"Read Only Collection");

        void IList.Clear() => throw new NotSupportedException(@"Read Only Collection");

        void IList.Insert(int index, object? value) => throw new NotSupportedException(@"Read Only Collection");

        void IList.Remove(object? value) => throw new NotSupportedException(@"Read Only Collection");

        void IList.RemoveAt(int index) => throw new NotSupportedException(@"Read Only Collection");

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        bool IList.IsFixedSize => true;

        private object InnerArrayGetEntryObject(int index, int stateMask) => _internalListBox.InnerArrayGetEntryObject(index, stateMask);

        private int InnerArrayIndexOfIdentifier(object? identifier, int stateMask)
        {
#if NET8_0_OR_GREATER
// https://github.com/dotnet/winforms/commit/1f4a593a6de32e75ff0f5fa97d35191c1facbc93#diff-c4db2c84a2a605af84487ad4386f94c42193826e71b7cf8d297c610c034245f9
                return _internalListBox.InnerArrayIndexOf(identifier, stateMask);
#else
            return _internalListBox.InnerArrayIndexOfIdentifier(identifier, stateMask);
#endif
        }

        #endregion
    }

    /// <summary>
    /// Encapsulates the collection of checked items, including items in an indeterminate state, in a KryptonCheckedListBox control.
    /// </summary>
    public class CheckedItemCollection : IList
    {
        #region Static Fields
        internal static int _checkedItemMask;
        internal static int _indeterminateItemMask;
        internal static int _anyItemMask;
        #endregion

        #region Instance Fields

        private readonly InternalCheckedListBox _internalListBox;
        #endregion

        #region Identity
        static CheckedItemCollection()
        {
            _checkedItemMask = 0x10;
            _indeterminateItemMask = 0x20;
            _anyItemMask = _checkedItemMask | _indeterminateItemMask;
        }

        /// <summary>
        /// Initialize a new instance of the CheckedItemCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning control.</param>
        internal CheckedItemCollection(KryptonCheckedListBox owner) => _internalListBox = (InternalCheckedListBox)owner.ListBox;

        #endregion

        #region Public
        /// <summary>
        /// Determines whether the collection contains the button spec.
        /// </summary>
        /// <param name="item">Object reference.</param>
        /// <returns>True if button spec found; otherwise false.</returns>
        public bool Contains(object? item) => IndexOf(item) != -1;

        /// <summary>
        /// Copies all the elements of the current collection to the specified Array.
        /// </summary>
        /// <param name="array">The Array that is the destination of the elements copied from the collection.</param>
        /// <param name="index">The index in array at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            var count = InnerArrayGetCount(_anyItemMask);
            for (var i = 0; i < count; i++)
            {
                array.SetValue(InnerArrayGetItem(i, _anyItemMask), i + index);
            }
        }

        /// <summary>
        /// Enumerate using non-generic interface.
        /// </summary>
        /// <returns>Enumerator instance.</returns>
        public IEnumerator GetEnumerator() => InnerArrayGetEnumerator(_anyItemMask, true);

        /// <summary>
        /// Determines the index of the specified spec in the collection.
        /// </summary>
        /// <param name="item">Object reference.</param>
        /// <returns>-1 if not found; otherwise index position.</returns>
        public int IndexOf(object? item) => InnerArrayIndexOf(item, _anyItemMask);

        /// <summary>
        /// Determines the index of the specified spec in the collection.
        /// </summary>
        /// <param name="item">Object reference.</param>
        /// <returns>-1 if not found; otherwise index position.</returns>
        public int IndexOfIdentifier(object? item) => InnerArrayIndexOfIdentifier(item, _anyItemMask);

        /// <summary>
        /// Gets the number of items in collection.
        /// </summary>
        public int Count => InnerArrayGetCount(_anyItemMask);

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">Item index.</param>
        /// <returns>Item at specified index.</returns>
        public object? this[int index]
        {
            get => InnerArrayGetItem(index, _anyItemMask);
            set => throw new NotSupportedException(@"Read Only Collection");
        }
        #endregion

        #region Internal
        internal CheckState GetCheckedState(int index)
        {
            var state = InnerArrayGetState(index, _checkedItemMask);
            return InnerArrayGetState(index, _indeterminateItemMask)
                ? CheckState.Indeterminate
                : state ? CheckState.Checked : CheckState.Unchecked;
        }

        internal void SetCheckedState(int index, CheckState value)
        {
            bool isChecked;
            bool isIndeterminate;

            switch (value)
            {
                case CheckState.Checked:
                    isChecked = true;
                    isIndeterminate = false;
                    break;
                case CheckState.Indeterminate:
                    isChecked = false;
                    isIndeterminate = true;
                    break;
                default:
                    isChecked = false;
                    isIndeterminate = false;
                    break;
            }

            InnerArraySetState(index, _checkedItemMask, isChecked);
            InnerArraySetState(index, _indeterminateItemMask, isIndeterminate);
        }
        #endregion

        #region Private
        int IList.Add(object? value) => throw new NotSupportedException(@"Read Only Collection");

        void IList.Clear() => throw new NotSupportedException(@"Read Only Collection");

        void IList.Insert(int index, object? value) => throw new NotSupportedException(@"Read Only Collection");

        void IList.Remove(object? value) => throw new NotSupportedException(@"Read Only Collection");

        void IList.RemoveAt(int index) => throw new NotSupportedException(@"Read Only Collection");

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        bool IList.IsFixedSize => true;

        private int InnerArrayGetCount(int stateMask) => _internalListBox.InnerArrayGetCount(stateMask);

        private int InnerArrayIndexOf(object? item, int stateMask) => _internalListBox.InnerArrayIndexOf(item, stateMask);

        private int InnerArrayIndexOfIdentifier(object? identifier, int stateMask) => _internalListBox.InnerArrayIndexOfIdentifier(identifier, stateMask);

        private object InnerArrayGetItem(int index, int stateMask) => _internalListBox.InnerArrayGetItem(index, stateMask);

        private bool InnerArrayGetState(int index, int stateMask) => _internalListBox.InnerArrayGetState(index, stateMask);

        private void InnerArraySetState(int index, int stateMask, bool value) => _internalListBox.InnerArraySetState(index, stateMask, value);

        private IEnumerator InnerArrayGetEnumerator(int stateMask, bool anyBit) => _internalListBox.InnerArrayGetEnumerator(stateMask, anyBit);

        #endregion
    }

    /// <summary>
    /// Represents the collection of items in a CheckedListBox.
    /// </summary>
    public class ObjectCollection : ListBox.ObjectCollection
    {
        #region Instance Fields
        private readonly KryptonCheckedListBox _owner;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ObjectCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning control.</param>
        public ObjectCollection(KryptonCheckedListBox owner)
            : base(owner.ListBox) =>
            _owner = owner;

        #endregion

        #region Public
        /// <summary>
        /// Adds an item to the list of items for a CheckedListBox, specifying the object to add and whether it is checked.
        /// </summary>
        /// <param name="item">An object representing the item to add to the collection.</param>
        /// <param name="isChecked">true to check the item; otherwise, false</param>
        /// <returns>The index of the newly added item.</returns>
        public int Add(object? item, bool isChecked) => Add(item, isChecked ? CheckState.Checked : CheckState.Unchecked);

        /// <summary>
        /// Adds an item to the list of items for a CheckedListBox, specifying the object to add and the initial checked value.
        /// </summary>
        /// <param name="item">An object representing the item to add to the collection.</param>
        /// <param name="check">The initial CheckState for the checked portion of the item.</param>
        /// <returns>The index of the newly added item.</returns>
        public int Add(object? item, CheckState check)
        {
            var index = base.Add(item!);
            _owner.SetItemCheckState(index, check);
            return index;
        }
        #endregion
    }

    private sealed class InternalCheckedListBox : ListBox
    {
        #region Static Fields
        private static MethodInfo? _miGetCount;
        private static MethodInfo? _miIndexOf;
        private static MethodInfo? _miIndexOfIdentifier;
        private static MethodInfo? _miGetItem;
        private static MethodInfo? _miGetEntryObject;
        private static MethodInfo? _miGetState;
        private static MethodInfo? _miSetState;
        private static MethodInfo? _miGetEnumerator;
        // ReSharper disable InconsistentNaming
        private static readonly uint LBC_GETCHECKSTATE;
        private static readonly uint LBC_SETCHECKSTATE;
        // ReSharper restore InconsistentNaming
        #endregion

        #region Instance Fields
        private object? _innerArray;
        private readonly ViewManager? _viewManager;
        private readonly KryptonCheckedListBox _kryptonCheckedListBox;
        private readonly IntPtr _screenDC;
        private bool _mouseOver;
        private bool _killNextSelect;
        private int _lastSelected;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse enters the InternalListBox.
        /// </summary>
        public event EventHandler? TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the InternalListBox.
        /// </summary>
        public event EventHandler? TrackMouseLeave;
        #endregion

        #region Identity
        static InternalCheckedListBox()
        {
            LBC_GETCHECKSTATE = PI.RegisterWindowMessage(nameof(LBC_GETCHECKSTATE));
            LBC_SETCHECKSTATE = PI.RegisterWindowMessage(nameof(LBC_SETCHECKSTATE));
        }

        /// <summary>
        /// Initialize a new instance of the InternalCheckedListBox class.
        /// </summary>
        /// <param name="kryptonCheckedListBox">Reference to owning control.</param>
        public InternalCheckedListBox(KryptonCheckedListBox kryptonCheckedListBox)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            _kryptonCheckedListBox = kryptonCheckedListBox;
            MouseIndex = -1;
            _lastSelected = -1;

            // Create manager and view for drawing the background
            ViewDrawPanel = new ViewDrawPanel();
            _viewManager = new ViewManager(this, ViewDrawPanel);

            // Set required properties to act as an owner draw list box
            // ReSharper disable RedundantBaseQualifier
            base.Size = Size.Empty;
            base.BorderStyle = BorderStyle.None;
            base.IntegralHeight = false;
            base.MultiColumn = false;
            base.DrawMode = DrawMode.OwnerDrawVariable;
            // ReSharper restore RedundantBaseQualifier

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
        }

        /// <summary>
        /// Releases all resources used by the Control.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_screenDC != IntPtr.Zero)
            {
                PI.DeleteDC(_screenDC);
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Recreate the window handle.
        /// </summary>
        public void Recreate() => RecreateHandle();

        /// <summary>
        /// Gets access to the contained view draw panel instance.
        /// </summary>
        public ViewDrawPanel ViewDrawPanel { get; }

        /// <summary>
        /// Gets the item index the mouse is over.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MouseIndex { get; private set; }

        /// <summary>
        /// Gets and sets if the mouse is currently over the combo box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MouseOver
        {
            get => _mouseOver;

            set
            {
                // Only interested in changes
                if (_mouseOver != value)
                {
                    _mouseOver = value;

                    // Generate appropriate change event
                    if (_mouseOver)
                    {
                        OnTrackMouseEnter(EventArgs.Empty);
                    }
                    else
                    {
                        OnTrackMouseLeave(EventArgs.Empty);
                        MouseIndex = -1;
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the drawing mode of the checked list box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override DrawMode DrawMode
        {
            get => DrawMode.OwnerDrawVariable;
            set { }
        }

        /// <summary>
        /// Force the remeasure of items, so they are sized correctly.
        /// </summary>
        public void RefreshItemSizes()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DrawMode = DrawMode.OwnerDrawVariable;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Creates a new instance of the item collection.
        /// </summary>
        /// <returns>A ListBox.ObjectCollection that represents the new item collection.</returns>
        protected override ObjectCollection CreateItemCollection() => new ObjectCollection(this);

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs containing the event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar == ' ') && (SelectionMode != SelectionMode.None))
            {
                LbnSelChange();
            }

            if (_kryptonCheckedListBox.FormattingEnabled)
            {
                base.OnKeyPress(e);
            }
        }

        /// <summary>
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">A EventArgs containing the event data.</param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            _lastSelected = SelectedIndex;
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">A EventArgs containing the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            _killNextSelect = false;
            base.OnClick(e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Ask the panel to layout given our available size
            using var context = new ViewLayoutContext(_viewManager, this, _kryptonCheckedListBox,
                _kryptonCheckedListBox.Renderer);
            ViewDrawPanel.Layout(context);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == LBC_GETCHECKSTATE)
            {
                var wParam = (int)m.WParam.ToInt64();
                if ((wParam < 0) || (wParam >= Items.Count))
                {
                    m.Result = PI.InvalidIntPtr;
                }
                else
                {
                    m.Result = _kryptonCheckedListBox.GetItemChecked(wParam) ? ((IntPtr)1) : IntPtr.Zero;
                }
            }
            else if (m.Msg == LBC_SETCHECKSTATE)
            {
                var index = (int)m.WParam.ToInt64();
                var lParam = (int)m.LParam;
                if ((index < 0) || (index >= Items.Count) || ((lParam != 1) && (lParam != 0)))
                {
                    m.Result = IntPtr.Zero;
                }
                else
                {
                    _kryptonCheckedListBox.SetItemChecked(index, lParam == 1);
                    m.Result = (IntPtr)1;
                }
            }

            switch (m.Msg)
            {
                case PI.WM_.KEYDOWN:
                    WmKeyDown(ref m);
                    base.WndProc(ref m);
                    return;
                case PI.WM_.ERASEBKGND:
                    // Do not draw the background here, always do it in the paint
                    // instead to prevent flicker because of a two stage drawing process
                    break;
                case PI.WM_.PRINTCLIENT:
                case PI.WM_.PAINT:
                    WmPaint(ref m);
                    break;
                case PI.WM_.VSCROLL:
                case PI.WM_.HSCROLL:
                case PI.WM_.MOUSEWHEEL:
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSELEAVE:
                    // Mouse is not over the control
                    MouseOver = false;
                    _kryptonCheckedListBox.PerformNeedPaint(true);
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    // Mouse is over the control
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonCheckedListBox.PerformNeedPaint(true);
                        Invalidate();
                    }
                    else
                    {
                        // Find the item under the mouse
                        var mousePoint = new Point((int)m.LParam.ToInt64());
                        var mouseIndex = IndexFromPoint(mousePoint);

                        // If we have an actual item from the point
                        if ((mouseIndex >= 0) && (mouseIndex < Items.Count))
                        {
                            // Check that the mouse really is in the item rectangle
                            Rectangle indexRect = GetItemRectangle(mouseIndex);
                            if (!indexRect.Contains(mousePoint))
                            {
                                mouseIndex = -1;
                            }
                        }

                        // If item under mouse has changed, then need to reflect for tracking
                        if (MouseIndex != mouseIndex)
                        {
                            Invalidate();
                            MouseIndex = mouseIndex;
                        }
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="m">The Message the top-level window sent to the InternalCheckedListBox control.</param>
        protected override void WmReflectCommand(ref Message m)
        {
            switch (PI.HIWORD((int)m.WParam.ToInt64()))
            {
                case 1:
                    LbnSelChange();
                    base.WmReflectCommand(ref m);
                    return;

                case 2:
                    LbnSelChange();
                    base.WmReflectCommand(ref m);
                    return;
            }
            base.WmReflectCommand(ref m);
        }

        /// <summary>
        /// Raises the TrackMouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        private void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

        /// <summary>
        /// Raises the TrackMouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        private void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);

        #endregion

        #region Internal
        internal object? InnerArray
        {
            get
            {
                // First time around we need to use reflection to grab inner array
                if (_innerArray == null)
                {
                    PropertyInfo? pi = typeof(ObjectCollection).GetProperty(nameof(InnerArray),
                        BindingFlags.Instance |
                        BindingFlags.NonPublic |
                        BindingFlags.GetField);

                    _innerArray = pi?.GetValue(Items, Array.Empty<object>());
                }

                return _innerArray;
            }
        }

        internal int InnerArrayGetCount(int stateMask)
        {
            if (_miGetCount == null)
            {
                _miGetCount = InnerArray?.GetType().GetMethod(@"GetCount", [typeof(int)], null);
            }

            return (int)_miGetCount?.Invoke(InnerArray, [stateMask])!;
        }

        internal int InnerArrayIndexOf(object? item, int stateMask)
        {
            if (_miIndexOf == null)
            {
                _miIndexOf = InnerArray?.GetType().GetMethod(@"IndexOf", [typeof(object), typeof(int)], null);
            }

            return (int)_miIndexOf?.Invoke(InnerArray, [item, stateMask])!;
        }

        internal int InnerArrayIndexOfIdentifier(object? identifier, int stateMask)
        {
            if (_miIndexOfIdentifier == null)
            {
                _miIndexOfIdentifier = InnerArray?.GetType().GetMethod(@"IndexOfIdentifier", [typeof(object), typeof(int)
                ], null);
            }

            return (int)_miIndexOfIdentifier?.Invoke(InnerArray, [identifier, stateMask])!;
        }

        internal object InnerArrayGetItem(int index, int stateMask)
        {
            if (_miGetItem == null)
            {
                _miGetItem = InnerArray?.GetType().GetMethod(@"GetItem", [typeof(int), typeof(int)], null);
            }

            return _miGetItem?.Invoke(InnerArray, [index, stateMask])!;
        }

        internal object InnerArrayGetEntryObject(int index, int stateMask)
        {
            if (_miGetEntryObject == null)
            {
                _miGetEntryObject = InnerArray?.GetType().GetMethod(@"GetEntryObject", BindingFlags.NonPublic | BindingFlags.Instance);
            }

            return _miGetEntryObject?.Invoke(InnerArray, [index, stateMask])!;
        }

        internal bool InnerArrayGetState(int index, int stateMask)
        {
            if (_miGetState == null)
            {
                _miGetState = InnerArray?.GetType().GetMethod(@"GetState", [typeof(int), typeof(int)], null);
            }

            return (bool)_miGetState?.Invoke(InnerArray, [index, stateMask])!;
        }

        internal void InnerArraySetState(int index, int stateMask, bool value)
        {
            if (_miSetState == null)
            {
                _miSetState = InnerArray?.GetType().GetMethod(@"SetState", [typeof(int), typeof(int), typeof(bool)], null);
            }

            _miSetState?.Invoke(InnerArray, [index, stateMask, value]);
        }

        internal IEnumerator InnerArrayGetEnumerator(int stateMask, bool anyBit)
        {
            if (_miGetEnumerator == null)
            {
                _miGetEnumerator = InnerArray?.GetType().GetMethod(@"GetEnumerator", [typeof(int), typeof(bool)], null);
            }

            return (IEnumerator)_miGetEnumerator?.Invoke(InnerArray, [stateMask, anyBit])!;
        }
        #endregion

        #region Private
        private void WmPaint(ref Message m)
        {
            var ps = new PI.PAINTSTRUCT();

            // Do we need to BeginPaint or just take the given HDC?
            var hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            Rectangle realRect = CommonHelper.RealClientRectangle(Handle);

            // No point drawing when one of the dimensions is zero
            if (realRect is { Width: > 0, Height: > 0 })
            {
                var hBitmap = PI.CreateCompatibleBitmap(hdc, realRect.Width, realRect.Height);

                // If we managed to get a compatible bitmap
                if (hBitmap != IntPtr.Zero)
                {
                    // Must use the screen device context for the bitmap when drawing into the
                    // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                    // Select the new bitmap into the screen DC
                    IntPtr oldBitmap = PI.SelectObject(_screenDC, hBitmap);

                    try
                    {
                        // Easier to draw using a graphics instance than a DC!
                        using (Graphics g = Graphics.FromHdc(_screenDC))
                        {
                            // Ask the view element to layout in given space, needs this before a render call
                            using (var context = new ViewLayoutContext(this, _kryptonCheckedListBox.Renderer))
                            {
                                context.DisplayRectangle = realRect;
                                ViewDrawPanel.Layout(context);
                            }

                            using (var context = new RenderContext(this, _kryptonCheckedListBox, g,
                                       realRect, _kryptonCheckedListBox.Renderer))
                            {
                                ViewDrawPanel.Render(context);
                            }

                            // Replace given DC with the screen DC for base window proc drawing
                            var beforeDC = m.WParam;
                            m.WParam = _screenDC;
                            DefWndProc(ref m);
                            m.WParam = beforeDC;

                            if (Items.Count == 0)
                            {
                                using var context = new RenderContext(this, _kryptonCheckedListBox, g,
                                    realRect, _kryptonCheckedListBox.Renderer);
                                ViewDrawPanel.Render(context);
                            }
                        }

                        // Now blit from the bitmap from the screen to the real dc
                        PI.BitBlt(hdc, 0, 0, realRect.Width, realRect.Height, _screenDC, 0, 0, PI.SRCCOPY);

                        // When disabled with no items the above code does not draw the background! Strange but true, and
                        // so we need to draw the background instead directly, without using a bit blitting of bitmap
                        if (Items.Count == 0)
                        {
                            using Graphics g = Graphics.FromHdc(hdc);
                            using var context = new RenderContext(this, _kryptonCheckedListBox, g,
                                realRect, _kryptonCheckedListBox.Renderer);
                            ViewDrawPanel.Render(context);
                        }
                    }
                    finally
                    {
                        // Restore the original bitmap
                        PI.SelectObject(_screenDC, oldBitmap);

                        // Delete the temporary bitmap
                        PI.DeleteObject(hBitmap);
                    }
                }
            }

            // Do we need to match the original BeginPaint?
            if (m.WParam == IntPtr.Zero)
            {
                PI.EndPaint(Handle, ref ps);
            }
        }

        private void WmKeyDown(ref Message m)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch ((int)m.WParam.ToInt64())
#pragma warning restore IDE0066 // Convert switch statement to expression
            {
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                    _killNextSelect = true;
                    break;

                default:
                    _killNextSelect = false;
                    break;
            }
        }

        private void LbnSelChange()
        {
            var selectedIndex = SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex < Items.Count))
            {
                if (!_killNextSelect && ((selectedIndex == _lastSelected) || _kryptonCheckedListBox.CheckOnClick))
                {
                    CheckState checkedState = _kryptonCheckedListBox.GetItemCheckState(selectedIndex);
                    CheckState newCheckValue = (checkedState != CheckState.Unchecked) ? CheckState.Unchecked : CheckState.Checked;
                    var ice = new ItemCheckEventArgs(selectedIndex, newCheckValue, checkedState);
                    _kryptonCheckedListBox.SetItemCheckState(selectedIndex, ice.NewValue);
                }
                _lastSelected = selectedIndex;
                Invalidate();
            }
        }
        #endregion
    }

    #endregion

    #region Instance Fields

    private readonly CheckedListBox _checkedListBox;

    private readonly PaletteTripleOverride _overrideNormal;
    private readonly PaletteTripleOverride _overrideTracking;
    private readonly PaletteTripleOverride _overridePressed;
    private readonly PaletteTripleOverride _overrideCheckedNormal;
    private readonly PaletteTripleOverride _overrideCheckedTracking;
    private readonly PaletteTripleOverride _overrideCheckedPressed;
    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutDocker _layoutDocker;
    private readonly ViewLayoutCenter _layoutCenter;
    private readonly ViewDrawCheckBox _drawCheckBox;
    private readonly ViewLayoutFill _layoutFill;
    private readonly ViewDrawButton _drawButton;
    private readonly InternalCheckedListBox _listBox;
    private readonly FixedContentValue? _contentValues;
    private bool? _fixedActive;
    private ButtonStyle _style;
    private readonly IntPtr _screenDC;
    private int _lastSelectedIndex;
    private bool _mouseOver;
    private bool _alwaysActive;
    private bool _forcedLayout;
    private bool _trackingMouseEnter;
    private object? _dataSource;
    private string _displayMember;
    private string _valueMember;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the property of a control is bound to a data value.
    /// </summary>
    [Description(@"Occurs when the property of a control is bound to a data value.")]
    [Category(@"Property Changed")]
    public event EventHandler? Format;

    /// <summary>
    /// Occurs when the value of the FormatInfo property changes.
    /// </summary>
    [Description(@"Occurs when the value of the FormatInfo property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? FormatInfoChanged;

    /// <summary>
    /// Occurs when the value of the FormatString property changes.
    /// </summary>
    [Description(@"Occurs when the value of the FormatString property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? FormatStringChanged;

    /// <summary>
    /// Occurs when the value of the FormattingEnabled property changes.
    /// </summary>
    [Description(@"Occurs when the value of the FormattingEnabled property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? FormattingEnabledChanged;

    /// <summary>
    /// Occurs when the value of the SelectedValue property changes.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedValue property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? SelectedValueChanged;

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedIndex property changes.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Description(@"Indicates that an item is about to have its check state changed. The value is not updated until after the event occurs.")]
    [Category(@"Behavior")]
    public event ItemCheckEventHandler? ItemCheck;

    /// <summary>
    /// Occurs when the value of the BackColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackColorChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImage property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImageLayout property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the value of the ForeColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? ForeColorChanged;

    /// <summary>
    /// Occurs when the value of the MouseClick property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? PaddingChanged;

    /// <summary>
    /// Occurs when the value of the MouseClick property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event PaintEventHandler? Paint;

    /// <summary>
    /// Occurs when the value of the TextChanged property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? TextChanged;

    /// <summary>
    /// Occurs when the mouse enters the control.
    /// </summary>
    [Description(@"Raises the TrackMouseEnter event in the wrapped control.")]
    [Category(@"Mouse")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? TrackMouseEnter;

    /// <summary>
    /// Occurs when the mouse leaves the control.
    /// </summary>
    [Description(@"Raises the TrackMouseLeave event in the wrapped control.")]
    [Category(@"Mouse")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? TrackMouseLeave;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckedListBox class.
    /// </summary>
    public KryptonCheckedListBox()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // Cannot select this control, only the child CheckedListBox and does not generate a click event
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

        // Default fields
        _alwaysActive = true;
        _style = ButtonStyle.ListItem;
        _lastSelectedIndex = -1;
        _checkedListBox = new CheckedListBox();
        base.Padding = new Padding(1);

        // Create the palette storage
        Images = new CheckBoxImages(NeedPaintDelegate);
        var paletteCheckBoxImages = new PaletteRedirectCheckBox(Redirector, Images);
        StateCommon = new PaletteListStateRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, NeedPaintDelegate);
        OverrideFocus = new PaletteListItemTripleRedirect(Redirector, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, NeedPaintDelegate);
        StateDisabled = new PaletteListState(StateCommon, NeedPaintDelegate);
        StateActive = new PaletteDouble(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteListState(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StatePressed = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StateCheckedNormal = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StateCheckedTracking = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StateCheckedPressed = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);

        // Create the override handling classes
        _overrideNormal = new PaletteTripleOverride(OverrideFocus.Item, StateNormal.Item, PaletteState.FocusOverride);
        _overrideTracking = new PaletteTripleOverride(OverrideFocus.Item, StateTracking.Item, PaletteState.FocusOverride);
        _overridePressed = new PaletteTripleOverride(OverrideFocus.Item, StatePressed.Item, PaletteState.FocusOverride);
        _overrideCheckedNormal = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedNormal.Item, PaletteState.FocusOverride);
        _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedTracking.Item, PaletteState.FocusOverride);
        _overrideCheckedPressed = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedPressed.Item, PaletteState.FocusOverride);

        // Create the check box image drawer and place inside element so it is always centered
        _drawCheckBox = new ViewDrawCheckBox(paletteCheckBoxImages);
        _layoutCenter = new ViewLayoutCenter
        {
            _drawCheckBox
        };

        // Create the draw element for owner drawing individual items
        _contentValues = new FixedContentValue();
        _drawButton = new ViewDrawButton(StateDisabled.Item, _overrideNormal,
            _overrideTracking, _overridePressed,
            _overrideCheckedNormal, _overrideCheckedTracking,
            _overrideCheckedPressed,
            new PaletteMetricRedirect(Redirector),
            _contentValues, VisualOrientation.Top, false);

        // Place check box on the left and the label in the remainder
        _layoutDocker = new ViewLayoutDocker
        {
            { new ViewLayoutSeparator(1), ViewDockStyle.Left },
            { _layoutCenter, ViewDockStyle.Left },
            { new ViewLayoutSeparator(2), ViewDockStyle.Left },
            { _drawButton, ViewDockStyle.Fill }
        };

        // Create the internal list box used for containing content
        _listBox = new InternalCheckedListBox(this);
        _listBox.DrawItem += OnListBoxDrawItem;
        _listBox.MeasureItem += OnListBoxMeasureItem;
        _listBox.TrackMouseEnter += OnListBoxMouseChange;
        _listBox.TrackMouseLeave += OnListBoxMouseChange;
        _listBox.SelectedIndexChanged += OnListBoxSelectedIndexChanged;
        _listBox.SelectedValueChanged += OnListBoxSelectedValueChanged;
        _listBox.Format += OnListBoxFormat;
        _listBox.FormatInfoChanged += OnListBoxFormatInfoChanged;
        _listBox.FormatStringChanged += OnListBoxFormatStringChanged;
        _listBox.FormattingEnabledChanged += OnListBoxFormattingEnabledChanged;
        _listBox.GotFocus += OnListBoxGotFocus;
        _listBox.LostFocus += OnListBoxLostFocus;
        _listBox.KeyDown += OnListBoxKeyDown;
        _listBox.KeyUp += OnListBoxKeyUp;
        _listBox.KeyPress += OnListBoxKeyPress;
        _listBox.PreviewKeyDown += OnListBoxPreviewKeyDown;
        _listBox.Validating += OnListBoxValidating;
        _listBox.Validated += OnListBoxValidated;
        _listBox.Click += OnCheckedListClick;  // SKC: make sure that the default click is also routed.

        // Create extra collections for storing checked state and checked items
        CheckedItems = new CheckedItemCollection(this);
        CheckedIndices = new CheckedIndexCollection(this);

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_listBox)
        {
            DisplayPadding = new Padding(1)
        };

        // Create inner view for placing inside the drawing docker
        _drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Create view for the control border and background
        _drawDockerOuter = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
        {
            { _drawDockerInner, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDockerOuter);

        // We need to create and cache a device context compatible with the display
        _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

        // Add text box to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_listBox);

        HandleCreated += OnHandleCreated;
    }

    private void OnCheckedListClick(object? sender, EventArgs e) =>
        // ReSharper disable RedundantBaseQualifier
        base.OnClick(e);
    // ReSharper restore RedundantBaseQualifier

    /// <summary>
    /// Releases all resources used by the Control.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (_screenDC != IntPtr.Zero)
        {
            PI.DeleteDC(_screenDC);
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the contained CheckedListBox instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public ListBox ListBox => _listBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => ListBox;

    /// <summary>
    /// Gets or sets the text for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the font of the text Displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AmbientValue(null)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value!;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [DefaultValue(typeof(Padding), "1,1,1,1")]
    public new Padding Padding
    {
        get => base.Padding;

        set
        {
            base.Padding = value;
            _layoutFill.DisplayPadding = value;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets or sets the zero-based index of the currently selected item in a KryptonCheckedListBox.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => _listBox.SelectedIndex;
        set => _listBox.SelectedIndex = value;
    }

    /// <summary>
    /// Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.
    /// </summary>
    [Category(@"Data")]
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    public object? SelectedValue
    {
        get => _listBox.SelectedValue;
        set => _listBox.SelectedValue = value!;
    }

    /// <summary>
    /// Gets a collection that contains the zero-based indexes of all currently selected items in the KryptonCheckedListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListBox.SelectedIndexCollection SelectedIndices => _listBox.SelectedIndices;

    /// <summary>
    /// Gets or sets the currently selected item in the KryptonCheckedListBox.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get => _listBox.SelectedItem;
        set => _listBox.SelectedItem = value;
    }

    /// <summary>
    /// Gets a collection containing the currently selected items in the KryptonCheckedListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListBox.SelectedObjectCollection SelectedItems => _listBox.SelectedItems;

    /// <summary>
    /// Gets or sets the index of the first visible item in the KryptonCheckedListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TopIndex
    {
        get => _listBox.TopIndex;
        set => _listBox.TopIndex = value;
    }

    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Item style.")]
    public ButtonStyle ItemStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                StateCommon.Item.SetStyles(_style);
                OverrideFocus.Item.SetStyles(_style);
                _listBox.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetItemStyle() => ItemStyle = ButtonStyle.ListItem;

    private bool ShouldSerializeItemStyle() => ItemStyle != ButtonStyle.ListItem;

    /// <summary>
    /// Gets or sets the width by which the horizontal scroll bar of a KryptonCheckedListBox can scroll.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The width, in pixels, by which a list box can be scrolled horizontally. Only valid HorizontalScrollbar is true.")]
    [Localizable(true)]
    [DefaultValue(0)]
    public virtual int HorizontalExtent
    {
        get => _listBox.HorizontalExtent;
        set => _listBox.HorizontalExtent = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a horizontal scroll bar is Displayed in the control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the KryptonCheckedListBox will display a horizontal scrollbar for items beyond the right edge of the KryptonCheckedListBox.")]
    [Localizable(true)]
    [DefaultValue(false)]
    public virtual bool HorizontalScrollbar
    {
        get => _listBox.HorizontalScrollbar;
        set => _listBox.HorizontalScrollbar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the vertical scroll bar is shown at all times.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the list box should always have a scroll bar present, regardless of how many items are present.")]
    [Localizable(true)]
    [DefaultValue(false)]
    public virtual bool ScrollAlwaysVisible
    {
        get => _listBox.ScrollAlwaysVisible;
        set => _listBox.ScrollAlwaysVisible = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the check box should be toggled when an item is selected.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the check box should be toggled when an item is selected.")]
    [DefaultValue(false)]
    public bool CheckOnClick { get; set; }

    /// <summary>
    /// Gets or sets the selection mode of the KryptonCheckedListBox control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the checked list box is to be single-select or not selectable. (Multi## not supported)")]
    [DefaultValue(CheckedSelectionMode.One)]
    public virtual CheckedSelectionMode SelectionMode
    {
        get => _listBox.SelectionMode == System.Windows.Forms.SelectionMode.One
            ? CheckedSelectionMode.One
            : CheckedSelectionMode.None;

        set => _listBox.SelectionMode = (value == CheckedSelectionMode.One)
            ? System.Windows.Forms.SelectionMode.One
            : System.Windows.Forms.SelectionMode.None;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the items in the KryptonCheckedListBox are sorted alphabetically.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether the list is sorted.")]
    [DefaultValue(false)]
    public virtual bool Sorted
    {
        get => _listBox.Sorted;
        set => _listBox.Sorted = value;
    }

    /// <summary>
    /// Gets the items of the KryptonCheckedListBox.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the KryptonCheckedListBox.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Localizable(true)]
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual ListBox.ObjectCollection Items => _listBox.Items;

    /// <summary>
    /// Collection of checked items in this KryptonCheckedListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CheckedItemCollection CheckedItems { get; }

    /// <summary>
    /// Collection of checked indexes in this KryptonCheckedListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CheckedIndexCollection CheckedIndices { get; }

    /// <summary>
    /// Gets or sets the format specifier characters that indicate how a value is to be Displayed.
    /// </summary>
    [Description(@"The format specifier characters that indicate how a value is to be Displayed.")]
    [Editor(@"System.Windows.Forms.Design.FormatStringEditor", typeof(UITypeEditor))]
    [MergableProperty(false)]
    [DefaultValue(@"")]
    public string FormatString
    {
        get => _listBox.FormatString;
        set => _listBox.FormatString = value;
    }

    /// <summary>
    /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.
    /// </summary>
    [Description(@"If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.")]
    [DefaultValue(false)]
    public bool FormattingEnabled
    {
        get => _listBox.FormattingEnabled;
        set => _listBox.FormattingEnabled = value;
    }

    /// <summary>
    /// Gets and sets the background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Style used to draw the background.")]
    public PaletteBackStyle BackStyle
    {
        get => StateCommon.BackStyle;

        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                _listBox.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetBackStyle() => BackStyle = PaletteBackStyle.InputControlStandalone;

    private bool ShouldSerializeBackStyle() => BackStyle != PaletteBackStyle.InputControlStandalone;

    /// <summary>
    /// Gets and sets the border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Style used to draw the border.")]
    public PaletteBorderStyle BorderStyle
    {
        get => StateCommon.BorderStyle;

        set
        {
            if (StateCommon.BorderStyle != value)
            {
                StateCommon.BorderStyle = value;
                _listBox.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetBorderStyle() => BorderStyle = PaletteBorderStyle.InputControlStandalone;

    private bool ShouldSerializeBorderStyle() => BorderStyle != PaletteBorderStyle.InputControlStandalone;

    /// <summary>
    /// Gets access to the item appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to the checkbox image value overrides.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"CheckBox image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxImages Images { get; }

    private bool ShouldSerializeImages() => !Images.IsDefault;

    /// <summary>
    /// Gets access to the common appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListStateRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListState StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListState StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateActive { get; }

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the normal checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateCheckedPressed { get; }

    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

    /// <summary>
    /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if the control is always active or only when the mouse is over the control or has focus.")]
    [DefaultValue(true)]
    public bool AlwaysActive
    {
        get => _alwaysActive;

        set
        {
            if (_alwaysActive != value)
            {
                _alwaysActive = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Unselects all items in the KryptonCheckedListBox.
    /// </summary>
    public void ClearSelected() => _listBox.ClearSelected();

    /// <summary>
    /// Returns a value indicating whether the specified item is checked.
    /// </summary>
    /// <param name="index">The index of the item.</param>
    /// <returns>true if the item is checked; otherwise, false.</returns>
    public bool GetItemChecked(int index) => GetItemCheckState(index) != CheckState.Unchecked;

    /// <summary>
    /// Returns a value indicating the check state of the current item.
    /// </summary>
    /// <param name="index">The index of the item to get the checked value of.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <returns>One of the CheckState values.</returns>
    public CheckState GetItemCheckState(int index) =>
        // Check index actually exists
        (index < 0) || (index >= Items.Count)
            ? throw new ArgumentOutOfRangeException(nameof(index), @"index out of range")
            : CheckedItems.GetCheckedState(index);

    /// <summary>
    /// Sets CheckState for the item at the specified index to Checked.
    /// </summary>
    /// <param name="index">The index of the item to set the check state for.</param>
    /// <param name="value">true to set the item as checked; otherwise, false.</param>
    public void SetItemChecked(int index, bool value) => SetItemCheckState(index, value ? CheckState.Checked : CheckState.Unchecked);

    /// <summary>
    /// Sets the check state of the item at the specified index.
    /// </summary>
    /// <param name="index">The index of the item to set the state for.</param>
    /// <param name="value">One of the CheckState values.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetItemCheckState(int index, CheckState value)
    {
        // Check index actually exists
        if ((index < 0) || (index >= Items.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index), @"index out of range");
        }

        // Is the new state different from the current checked state?
        CheckState checkedState = CheckedItems.GetCheckedState(index);
        if (value != checkedState)
        {
            // Give developers a chance to see and alter the change
            var ice = new ItemCheckEventArgs(index, value, checkedState);
            OnItemCheck(ice);

            // If a change is still occurring
            if (ice.NewValue != checkedState)
            {
                CheckedItems.SetCheckedState(index, ice.NewValue);
                _listBox.Invalidate();
            }
        }
    }

    /// <summary>
    /// Finds the first item in the list box that starts with the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindString(string str) => _listBox.FindString(str);

    /// <summary>
    /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindString(string str, int startIndex) => _listBox.FindString(str, startIndex);

    /// <summary>
    /// Finds the first item in the list box that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindStringExact(string str) => _listBox.FindStringExact(str);

    /// <summary>
    /// Finds the first item after the specified index that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindStringExact(string str, int startIndex) => _listBox.FindStringExact(str, startIndex);

    /// <summary>
    /// Returns the height of an item in the KryptonCheckedListBox.
    /// </summary>
    /// <param name="index">The index of the item to return the height of.</param>
    /// <returns>The height, in pixels, of the item at the specified index.</returns>
    public int GetItemHeight(int index) => _listBox.GetItemHeight(index);

    /// <summary>
    /// Returns the bounding rectangle for an item in the KryptonCheckedListBox.
    /// </summary>
    /// <param name="index">The zero-based index of item whose bounding rectangle you want to return.</param>
    /// <returns>A Rectangle that represents the bounding rectangle for the specified item.</returns>
    public Rectangle GetItemRectangle(int index) => _listBox.GetItemRectangle(index);

    /// <summary>
    /// Returns a value indicating whether the specified item is selected.
    /// </summary>
    /// <param name="index">The zero-based index of the item that determines whether it is selected.</param>
    /// <returns>true if the specified item is currently selected in the KryptonCheckedListBox; otherwise, false.</returns>
    public bool GetSelected(int index) => _listBox.GetSelected(index);

    /// <summary>
    /// Returns the zero-based index of the item at the specified coordinates.
    /// </summary>
    /// <param name="p">A Point object containing the coordinates used to obtain the item index.</param>
    /// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
    public int IndexFromPoint(Point p) => _listBox.IndexFromPoint(p);

    /// <summary>
    /// Returns the zero-based index of the item at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the location to search.</param>
    /// <param name="y">The y-coordinate of the location to search.</param>
    /// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
    public int IndexFromPoint(int x, int y) => _listBox.IndexFromPoint(x, y);

    /// <summary>
    /// Selects or clears the selection for the specified item in a KryptonCheckedListBox.
    /// </summary>
    /// <param name="index">The zero-based index of the item in a KryptonCheckedListBox to select or clear the selection for.</param>
    /// <param name="value">true to select the specified item; otherwise, false.</param>
    public void SetSelected(int index, bool value) => _listBox.SetSelected(index, value);

    /// <summary>
    /// Returns the text representation of the specified item.
    /// </summary>
    /// <param name="item">The object from which to get the contents to display.</param>
    /// <returns>If the DisplayMember property is not specified,
    /// the value returned by GetItemText is the value of the item's ToString method.
    /// Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
    public string? GetItemText(object? item) => _listBox.GetItemText(item);

    /// <summary>
    /// Maintains performance while items are added to the ListBox one at a time by preventing the control from drawing until the EndUpdate method is called.
    /// </summary>
    public void BeginUpdate() => _listBox.BeginUpdate();

    /// <summary>
    /// Resumes painting the ListBox control after painting is suspended by the BeginUpdate method.
    /// </summary>
    public void EndUpdate() => _listBox.EndUpdate();

    /// <summary>
    /// Sets the fixed state of the control.
    /// </summary>
    /// <param name="active">Should the control be fixed as active.</param>
    public void SetFixedState(bool active) => _fixedActive = active;

    /// <summary>
    /// Gets a value indicating if the input control is active.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsActive => _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus || _mouseOver || (_listBox.MouseOver);

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => ListBox.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => ListBox.Select();

    /// <summary>Gets or sets the data source.</summary>
    /// <value>The data source.</value>
    [Category("Data")]
    [Description("Indicates the list that this control will use to get its items.")]
    [DefaultValue(null)]
    [AttributeProvider(typeof(IListSource))]
    public object? DataSource
    {
        get => _dataSource;
        set
        {
            if (_dataSource != value)
            {
                _dataSource = value;
                RefreshItems();
            }
        }
    }

    /// <summary>Gets or sets the display member.</summary>
    /// <value>The display member.</value>
    [Category("Data")]
    [Description("Indicates the property to display for the items in the control.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
    [DefaultValue("")]
    public string DisplayMember
    {
        get => _displayMember ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        set
        {
            if (_displayMember != value)
            {
                _displayMember = value;
                RefreshItems();
            }
        }
    }

    /// <summary>Gets or sets the value member.</summary>
    /// <value>The value member.</value>
    [Category("Data")]
    [Description("Indicates the property to use as the actual value of items in the control.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
    [DefaultValue("")]
    public string ValueMember
    {
        get => _valueMember ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        set => _valueMember = value;
    }

    /// <summary>
    /// Gets a list of value members (or raw items) for all currently checked items.
    /// This is useful for data binding scenarios where the control is bound to a data source
    /// and the selected values need to be retrieved based on the ValueMember property.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<object> CheckedItemList
    {
        get
        {
            List<object> results = new();

            foreach (var item in CheckedItems)
            {
                if (!string.IsNullOrWhiteSpace(ValueMember))
                {
                    var prop = TypeDescriptor.GetProperties(item)[ValueMember];
                    if (prop != null)
                    {
                        var val = prop.GetValue(item);
                        if (val != null)
                            results.Add(val);
                        continue;
                    }
                }

                results.Add(item);
            }

            return results;
        }
    }

    #endregion

    #region Protected
    /// <summary>
    /// Force the layout logic to size and position the controls.
    /// </summary>
    protected void ForceControlLayout()
    {
        if (!IsHandleCreated)
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = false;
        }
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedValueChanged(EventArgs e) => SelectedValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ItemCheck event.
    /// </summary>
    /// <param name="e">An ItemCheckEventArgs containing the event data.</param>
    protected virtual void OnItemCheck(ItemCheckEventArgs e) => ItemCheck?.Invoke(this, e);

    /// <summary>
    /// Raises the Format event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormat(ListControlConvertEventArgs e) => Format?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatInfoChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatInfoChanged(EventArgs e) => FormatInfoChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatStringChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatStringChanged(EventArgs e) => FormatStringChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the FormattingEnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormattingEnabledChanged(EventArgs e) => FormattingEnabledChanged?.Invoke(this, e);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonTextBox.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        _listBox.Recreate();
        _listBox.RefreshItemSizes();
        _listBox.Invalidate();

        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Processes a notification from palette of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        _listBox.RefreshItemSizes();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Change in enabled state requires a layout and repaint
        UpdateStateAndPalettes();
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the BackColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackColorChanged(EventArgs e) => BackColorChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageChanged(EventArgs e) => BackgroundImageChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageLayoutChanged(EventArgs e) => BackgroundImageLayoutChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ForeColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnForeColorChanged(EventArgs e) => ForeColorChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the PaddingChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaddingChanged(EventArgs e) => PaddingChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        ListBox.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        ListBox.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">An PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        Paint?.Invoke(this, e!);

        base.OnPaint(e);
    }

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An PaintEventArgs that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // Let base class do standard stuff
        base.OnHandleCreated(e);

        // Force the font to be set into the text box child control
        PerformNeedPaint(false);

        // We need a layout to occur before any painting
        InvokeLayout();
    }

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            _listBox.Invalidate();
        }
        else
        {
            ForceControlLayout();
        }

        // Update palette to reflect latest state
        UpdateStateAndPalettes();
        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsHandleCreated || _forcedLayout || (DesignMode))
        {
            Rectangle fillRect = _layoutFill.FillRect;
            _listBox.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
        }
    }

    /// <summary>
    /// Raises the MouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        _mouseOver = true;
        PerformNeedPaint(true);
        _listBox.Invalidate();
        base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        _mouseOver = false;
        PerformNeedPaint(true);
        _listBox.Invalidate();
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(120, 96);

    /// <summary>
    /// Raises the <see cref="E:BindingContextChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    protected override void OnBindingContextChanged(EventArgs e)
    {
        base.OnBindingContextChanged(e);

        RefreshItems();
    }

    #endregion

    #region Implementation
    private void UpdateStateAndPalettes()
    {
        if (!IsDisposed)
        {
            // Get the correct palette settings to use
            IPaletteDouble doubleState = GetDoubleState();
            _listBox.ViewDrawPanel.SetPalettes(doubleState.PaletteBack);
            _drawDockerOuter.SetPalettes(doubleState.PaletteBack, doubleState.PaletteBorder!);
            _drawDockerOuter.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state = Enabled ? (IsActive ? PaletteState.Tracking : PaletteState.Normal) : PaletteState.Disabled;

            _listBox.ViewDrawPanel.ElementState = state;
            _drawDockerOuter.ElementState = state;
        }
    }

    private IPaletteDouble GetDoubleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    private void OnListBoxDrawItem(object? sender, DrawItemEventArgs e)
    {
        // We cannot do anything with an invalid index
        if (e.Index < 0)
        {
            return;
        }

        // Update our content object with values from the list item
        UpdateContentFromItemIndex(e.Index);

        // By default, the button is in the normal state
        var buttonState = PaletteState.Normal;

        // Is this item disabled
        if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
            buttonState = PaletteState.Disabled;
        }
        else
        {
            // Is the mouse over the item about to be drawn
            var mouseOver = (e.Index >= 0) && (e.Index == _listBox.MouseIndex);

            // If selected then show as a checked item
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (SelectionMode == CheckedSelectionMode.None)
                {
                    _drawButton.Checked = false;
                }
                else
                {
                    _drawButton.Checked = true;
                    buttonState = mouseOver ? PaletteState.CheckedTracking : PaletteState.CheckedNormal;
                }
            }
            else
            {
                _drawButton.Checked = false;
                if (mouseOver && (SelectionMode != CheckedSelectionMode.None))
                {
                    buttonState = PaletteState.Tracking;
                }
            }

            // Do we need to show item as having the focus
            var hasFocus = ((e.State & DrawItemState.Focus) == DrawItemState.Focus) &&
                           ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect);

            _overrideNormal.Apply = hasFocus;
            _overrideTracking.Apply = hasFocus;
            _overridePressed.Apply = hasFocus;
            _overrideCheckedTracking.Apply = hasFocus;
            _overrideCheckedNormal.Apply = hasFocus;
            _overrideCheckedPressed.Apply = hasFocus;
        }

        // Update the view with the calculated state
        _drawButton.ElementState = buttonState;

        // Update check box to show correct checked image
        _drawCheckBox.CheckState = GetItemCheckState(e.Index);

        // Grab the raw device context for the graphics instance
        var hdc = e.Graphics.GetHdc();

        try
        {
            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            var hBitmap = PI.CreateCompatibleBitmap(hdc, e.Bounds.Right, e.Bounds.Bottom);

            // If we managed to get a compatible bitmap
            if (hBitmap != IntPtr.Zero)
            {
                // Must use the screen device context for the bitmap when drawing into the
                // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                IntPtr oldBitmap = PI.SelectObject(_screenDC, hBitmap);

                try
                {
                    // Easier to draw using a graphics instance than a DC!
                    using (Graphics g = Graphics.FromHdc(_screenDC))
                    {
                        // Ask the view element to layout in given space, needs this before a render call
                        using (var context = new ViewLayoutContext(this, Renderer))
                        {
                            context.DisplayRectangle = e.Bounds;
                            _listBox.ViewDrawPanel.Layout(context);
                            _layoutDocker.Layout(context);
                        }

                        // Ask the view element to actually draw
                        using (var context = new RenderContext(this, g, e.Bounds, Renderer))
                        {
                            _listBox.ViewDrawPanel.Render(context);
                            _layoutDocker.Render(context);
                        }

                        // Now blit from the bitmap from the screen to the real dc
                        PI.BitBlt(hdc, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, _screenDC, e.Bounds.X, e.Bounds.Y, PI.SRCCOPY);
                    }
                }
                finally
                {
                    // Restore the original bitmap
                    PI.SelectObject(_screenDC, oldBitmap);

                    // Delete the temporary bitmap
                    PI.DeleteObject(hBitmap);
                }
            }
        }
        finally
        {
            // Must reserve the GetHdc() call before
            e.Graphics.ReleaseHdc();
        }
    }

    private void OnListBoxMeasureItem(object? sender, MeasureItemEventArgs e)
    {
        UpdateContentFromItemIndex(e.Index);

        // Ask the view element to layout in given space, needs this before a render call
        using var context = new ViewLayoutContext(this, Renderer);
        Size size = _layoutDocker.GetPreferredSize(context);
        e.ItemWidth = size.Width;
        e.ItemHeight = size.Height;
    }

    private void UpdateContentFromItemIndex(int index)
    {

        // If the object exposes the rich interface then use is...
        if (Items[index] is IContentValues itemValues)
        {
            _contentValues!.ShortText = itemValues.GetShortText();
            _contentValues.LongText = itemValues.GetLongText();
            _contentValues.Image = itemValues.GetImage(PaletteState.Normal);
            _contentValues.ImageTransparentColor = itemValues.GetImageTransparentColor(PaletteState.Normal);
        }
        else
        {
            // Get the text string for the item
            _contentValues!.ShortText = _listBox.GetItemText(Items[index]);
            _contentValues.LongText = null;
            _contentValues.Image = null;
            _contentValues.ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        }
    }

    private void OnListBoxSelectedIndexChanged(object? sender, EventArgs e)
    {
        // Only interested in changes of selected index
        if (_lastSelectedIndex != _listBox.SelectedIndex)
        {
            _lastSelectedIndex = _listBox.SelectedIndex;
            UpdateStateAndPalettes();
            _listBox.Invalidate();
            OnSelectedIndexChanged(e);
        }
    }

    private void OnListBoxSelectedValueChanged(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listBox.Invalidate();
        OnSelectedValueChanged(e);
    }

    private void OnListBoxItemCheck(object? sender, ItemCheckEventArgs e) => OnItemCheck(e);

    private void OnListBoxFormat(object? sender, ListControlConvertEventArgs e) => OnFormat(e);

    private void OnListBoxFormatInfoChanged(object? sender, EventArgs e) => OnFormatInfoChanged(e);

    private void OnListBoxFormatStringChanged(object? sender, EventArgs e) => OnFormatStringChanged(e);

    private void OnListBoxFormattingEnabledChanged(object? sender, EventArgs e) => OnFormattingEnabledChanged(e);

    private void OnListBoxGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listBox.Invalidate();
        PerformNeedPaint(true);
        OnGotFocus(e);
    }

    private void OnListBoxLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listBox.Invalidate();
        PerformNeedPaint(true);
        OnLostFocus(e);
    }

    private void OnListBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnListBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnListBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnListBoxValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnListBoxValidating(object? sender, CancelEventArgs e) => OnValidating(e);

    private void OnListBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnListBoxMouseChange(object? sender, EventArgs e)
    {
        // Change in tracking state?
        if (_listBox.MouseOver != _trackingMouseEnter)
        {
            _trackingMouseEnter = _listBox.MouseOver;

            // Raise appropriate event
            if (_trackingMouseEnter)
            {
                OnTrackMouseEnter(EventArgs.Empty);
                OnMouseEnter(e);
            }
            else
            {
                OnTrackMouseLeave(EventArgs.Empty);
                OnMouseLeave(e);
            }
        }
    }

    private void RefreshItems()
    {
        if (!IsHandleCreated || _dataSource == null || string.IsNullOrWhiteSpace(_displayMember))
        {
            return;
        }

        try
        {
            CurrencyManager? cm = BindingContext![_dataSource] as CurrencyManager;
            if (cm == null)
            {
                return;
            }

            Items.Clear();

            PropertyDescriptor? descriptor = cm.GetItemProperties()?.Find(_displayMember, true);
            if (descriptor == null)
            {
                return;
            }

            for (int i = 0; i < cm.Count; i++)
            {
                object? dataItem = cm.List[i];

                if (dataItem is null)
                {
                    continue; // Skip null items to avoid issues
                }

                object displayValue = descriptor.GetValue(dataItem) ?? string.Empty;
                Items.Add(dataItem); // IMPORTANT: add the full object, not just the display string
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[KryptonCheckedListBox] Data binding failed: {ex.Message}");
        }
    }


    private void OnHandleCreated(object? sender, EventArgs e) => RefreshItems();

    /// <summary>Refreshes the bound items.</summary>
    /// <returns></returns>
    public void RefreshBoundItems() => RefreshItems();

    #endregion
}