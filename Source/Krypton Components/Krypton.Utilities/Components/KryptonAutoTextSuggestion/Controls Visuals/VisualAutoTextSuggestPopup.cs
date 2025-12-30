#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Popup control for displaying text suggestions.
/// </summary>
internal class VisualAutoTextSuggestPopup : VisualPopup
{
    #region Instance Fields

    private readonly KryptonListBox _listBox;
    private readonly KryptonAutoTextSuggestion _provider;

    #endregion

    #region Identity
    
    /// <summary>
    /// Initializes a new instance of the VisualAutoTextSuggestPopup class.
    /// </summary>
    /// <param name="provider">The suggestion provider.</param>
    /// <param name="renderer">The renderer.</param>
    public VisualAutoTextSuggestPopup(KryptonAutoTextSuggestion provider, IRenderer? renderer)
        : base(new ViewManager(), renderer, true)
    {
        _provider = provider;
        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        _listBox.DoubleClick += ListBox_DoubleClick;

        // Create a view layout that fills the popup with the list box
        var layoutFill = new ViewLayoutFill(_listBox);
        var layoutDocker = new ViewLayoutDocker
        {
            { layoutFill, ViewDockStyle.Fill }
        };

        // Set the Control property on ViewManager before setting Root
        ViewManager!.Control = this;
        ViewManager!.AlignControl = this;

        // Set the root view for the ViewManager
        ViewManager!.Root = layoutDocker;

        // Add the control to the popup
        Controls.Add(_listBox);
        Size = new Size(200, 150);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the list box control.
    /// </summary>
    public KryptonListBox ListBox => _listBox;

    /// <summary>
    /// Updates the suggestions displayed in the popup.
    /// </summary>
    /// <param name="suggestions">The list of suggestions.</param>
    public void UpdateSuggestions(List<KryptonAutoTextSuggestItem> suggestions)
    {
        _listBox.Items.Clear();
        foreach (var item in suggestions)
        {
            _listBox.Items.Add(item);
        }

        if (_listBox.Items.Count > 0)
        {
            _listBox.SelectedIndex = 0;
        }

        AdjustSize();
    }

    /// <summary>
    /// Selects the next suggestion.
    /// </summary>
    public void SelectNext()
    {
        if (_listBox.Items.Count == 0)
        {
            return;
        }

        int nextIndex = _listBox.SelectedIndex + 1;
        if (nextIndex >= _listBox.Items.Count)
        {
            nextIndex = 0;
        }

        _listBox.SelectedIndex = nextIndex;
        EnsureVisible();
    }

    /// <summary>
    /// Selects the previous suggestion.
    /// </summary>
    public void SelectPrevious()
    {
        if (_listBox.Items.Count == 0)
        {
            return;
        }

        int prevIndex = _listBox.SelectedIndex - 1;
        if (prevIndex < 0)
        {
            prevIndex = _listBox.Items.Count - 1;
        }

        _listBox.SelectedIndex = prevIndex;
        EnsureVisible();
    }

    /// <summary>
    /// Gets the currently selected suggestion item.
    /// </summary>
    /// <returns>The selected item, or null if none selected.</returns>
    public KryptonAutoTextSuggestItem? GetSelectedItem()
    {
        if (_listBox.SelectedIndex >= 0 && _listBox.SelectedIndex < _listBox.Items.Count)
        {
            return _listBox.Items[_listBox.SelectedIndex] as KryptonAutoTextSuggestItem;
        }

        return null;
    }

    /// <summary>
    /// Shows the popup relative to the specified control.
    /// </summary>
    /// <param name="parentControl">The parent control.</param>
    public void ShowPopup(Control? parentControl)
    {
        if (parentControl == null || !parentControl.IsHandleCreated)
        {
            return;
        }

        Point location = parentControl.PointToScreen(new Point(0, parentControl.Height));
        Rectangle screenRect = new Rectangle(location, Size);

        // Adjust to stay on screen
        Screen screen = Screen.FromControl(parentControl);
        if (screenRect.Right > screen.WorkingArea.Right)
        {
            screenRect.X = screen.WorkingArea.Right - screenRect.Width;
        }

        if (screenRect.Bottom > screen.WorkingArea.Bottom)
        {
            screenRect.Y = parentControl.PointToScreen(Point.Empty).Y - screenRect.Height;
        }

        if (screenRect.X < screen.WorkingArea.Left)
        {
            screenRect.X = screen.WorkingArea.Left;
        }

        if (screenRect.Y < screen.WorkingArea.Top)
        {
            screenRect.Y = screen.WorkingArea.Top;
        }

        Show(screenRect);
    }

    /// <summary>
    /// Closes the popup.
    /// </summary>
    public void Close()
    {
        if (IsHandleCreated && !IsDisposed)
        {
            VisualPopupManager.Singleton.EndPopupTracking(this);
        }
    }

    #endregion

    #region Protected Overrides

    protected override void WndProc(ref Message m)
    {
        // Forward keyboard messages to the attached control if it has focus
        // This allows typing to continue in the text control while the popup is visible
        Control? attachedControl = _provider.AttachedControl;
        if (attachedControl != null && attachedControl.IsHandleCreated && attachedControl.Focused)
        {
            // Check if this is a keyboard message
            if (m.Msg is PI.WM_.KEYDOWN or PI.WM_.KEYUP or PI.WM_.CHAR or PI.WM_.SYSKEYDOWN or PI.WM_.SYSKEYUP or PI.WM_.SYSCHAR or PI.WM_.DEADCHAR or PI.WM_.SYSDEADCHAR)
            {
                // Forward the message to the attached control using SendMessage
                PI.SendMessage(attachedControl.Handle, m.Msg, m.WParam, m.LParam);
                return;
            }
        }

        base.WndProc(ref m);
    }

    #endregion

    #region Implementation

    private void AdjustSize()
    {
        int itemCount = _listBox.Items.Count;
        int maxVisibleItems = _provider.MaxVisibleItems;

        // Get item height - use GetItemHeight if items exist, otherwise use font height + padding
        int itemHeight = itemCount > 0
            ? _listBox.GetItemHeight(0)
            : Font.Height + 4;

        int height = Math.Min(itemCount * itemHeight + 4, maxVisibleItems * itemHeight + 4);
        height = Math.Max(height, itemHeight + 4);

        Size = new Size(_provider.PopupWidth, height);
    }

    private void EnsureVisible()
    {
        if (_listBox.SelectedIndex >= 0)
        {
            _listBox.TopIndex = Math.Max(0, _listBox.SelectedIndex - _provider.MaxVisibleItems + 1);
        }
    }

    private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Could raise hover event here if needed
    }

    private void ListBox_DoubleClick(object? sender, EventArgs e)
    {
        var item = GetSelectedItem();
        if (item != null)
        {
            _provider.ApplySuggestion(item);
        }
    }

    #endregion
}
