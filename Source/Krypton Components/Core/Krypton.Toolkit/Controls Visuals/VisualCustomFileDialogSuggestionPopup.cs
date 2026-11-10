#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a palette-aware suggestion popup for a text box in the custom file dialog.
/// </summary>
internal sealed class VisualCustomFileDialogSuggestionPopup : VisualPopup
{
    private readonly KryptonListBox _listBox;
    private readonly Action<string> _acceptSuggestion;
    private readonly int _maximumVisibleItems;
    private bool _tracking;

    public VisualCustomFileDialogSuggestionPopup(Action<string> acceptSuggestion, int maximumVisibleItems = 8)
        : base(new ViewManager(), KryptonManager.CurrentGlobalPalette.GetRenderer(), false)
    {
        _acceptSuggestion = acceptSuggestion;
        _maximumVisibleItems = maximumVisibleItems;
        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _listBox.DoubleClick += OnListBoxDoubleClick;

        var layoutFill = new ViewLayoutFill(_listBox);
        var layoutDocker = new ViewLayoutDocker
        {
            { layoutFill, ViewDockStyle.Fill }
        };

        ViewManager!.Control = this;
        ViewManager.AlignControl = this;
        ViewManager.Root = layoutDocker;
        Controls.Add(_listBox);
    }

    public string? SelectedSuggestion => _listBox.SelectedItem as string;

    // Control.Visible is true before the popup window is ever shown, so track the shown state explicitly.
    public bool IsPopupVisible => _tracking && !IsDisposed;

    /// <summary>
    /// Keeps keyboard input with the text box being completed. Without this the popup manager
    /// redirects every key to this popup, so typing and Enter never reach the text box.
    /// </summary>
    public override bool KeyboardInert => true;

    public void ShowSuggestions(KryptonTextBox textBox, IReadOnlyList<string> suggestions)
    {
        _listBox.BeginUpdate();
        try
        {
            _listBox.Items.Clear();
            foreach (var suggestion in suggestions)
            {
                _listBox.Items.Add(suggestion);
            }

            if (_listBox.Items.Count > 0)
            {
                _listBox.SelectedIndex = 0;
            }
        }
        finally
        {
            _listBox.EndUpdate();
        }

        if (_listBox.Items.Count == 0)
        {
            ClosePopup();
            return;
        }

        var itemHeight = _listBox.GetItemHeight(0);
        var visibleItems = Math.Min(_listBox.Items.Count, _maximumVisibleItems);
        var minWidth = ScaleLogicalWidth(textBox, 280);
        var popupSize = new Size(Math.Max(textBox.Width, minWidth), (visibleItems * itemHeight) + 4);
        var location = textBox.PointToScreen(new Point(0, textBox.Height));
        var screen = Screen.FromControl(textBox).WorkingArea;

        if (location.Y + popupSize.Height > screen.Bottom)
        {
            location.Y = textBox.PointToScreen(Point.Empty).Y - popupSize.Height;
        }

        if (location.X + popupSize.Width > screen.Right)
        {
            location.X = screen.Right - popupSize.Width;
        }

        location.X = Math.Max(screen.Left, location.X);
        location.Y = Math.Max(screen.Top, location.Y);

        var screenRect = new Rectangle(location, popupSize);
        if (IsPopupVisible)
        {
            // Already tracked; resize in place. Calling Show again would stack this popup on itself.
            SetBounds(screenRect.X, screenRect.Y, screenRect.Width, screenRect.Height);
            return;
        }

        _tracking = true;
        Show(screenRect);
    }

    public void SelectNext() => SelectRelative(1);

    public void SelectPrevious() => SelectRelative(-1);

    public void AcceptSelected()
    {
        if (SelectedSuggestion is string suggestion)
        {
            _acceptSuggestion(suggestion);
        }
    }

    public void ClosePopup()
    {
        if (!_tracking)
        {
            return;
        }

        // Only end tracking for a popup the manager actually owns; otherwise EndPopupTracking
        // would unwind and dispose unrelated popups while searching for this one.
        _tracking = false;
        if (!IsDisposed)
        {
            VisualPopupManager.Singleton.EndPopupTracking(this);
        }
    }

    protected override void Dispose(bool disposing)
    {
        _tracking = false;
        base.Dispose(disposing);
    }

    private void SelectRelative(int offset)
    {
        if (_listBox.Items.Count == 0)
        {
            return;
        }

        var index = _listBox.SelectedIndex + offset;
        if (index < 0)
        {
            index = _listBox.Items.Count - 1;
        }
        else if (index >= _listBox.Items.Count)
        {
            index = 0;
        }

        _listBox.SelectedIndex = index;
        _listBox.TopIndex = Math.Max(0, index - _maximumVisibleItems + 1);
    }

    private void OnListBoxDoubleClick(object? sender, EventArgs e)
    {
        if (SelectedSuggestion is not string suggestion)
        {
            return;
        }

        // Accepting closes (and disposes) this popup, so post it rather than tearing
        // down the list box while it is still dispatching this click.
        var context = SynchronizationContext.Current;
        if (context != null)
        {
            context.Post(_ => _acceptSuggestion(suggestion), null);
        }
        else
        {
            _acceptSuggestion(suggestion);
        }
    }

    private static int ScaleLogicalWidth(Control control, int logicalPixels)
    {
        float factor;
        try
        {
            factor = control.IsHandleCreated
                ? KryptonManager.GetDpiFactor(control.Handle)
                : KryptonManager.GetDpiFactor();
        }
        catch
        {
            factor = 1f;
        }

        return (int)Math.Round(logicalPixels * Math.Max(factor, 0.1f));
    }
}
