#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// A lightweight, borderless overlay that hosts a user-supplied <see cref="Control"/> as "Backstage" content.
/// </summary>
/// <remarks>
/// This form is owned by the top-level form that contains the ribbon and is positioned to cover the owner's
/// <see cref="Form.ClientRectangle"/> in screen coordinates.
///
/// The hosted content is temporarily re-parented into this form while the overlay is open and restored back to
/// its original parent when the overlay closes/disposes. The overlay does <b>not</b> dispose the hosted content.
/// </remarks>
internal sealed class VisualBackstageOverlayForm : KryptonForm
{
    #region Instance Fields

    // Top-level form that owns the ribbon; used as the overlay owner and for bounds tracking.
    private readonly Form _ownerForm;

    // Overlay mode determines coverage area
    private BackstageOverlayMode _overlayMode;

    // Reference to ribbon for below-ribbon mode calculations
    private KryptonRibbon? _ribbon;

    // Root container for palette integration and simple composition.
    private readonly KryptonPanel _root;

    // Header strip containing the back button (kept minimal; navigation is typically implemented by BackstageContent).
    private readonly KryptonPanel _header;

    // Back button triggers a request to close the backstage.
    private readonly KryptonButton _backButton;

    // Hosts the user content while backstage is visible.
    private readonly KryptonPanel _contentHost;

    // Current hosted content (re-parented into _contentHost while visible).
    private Control? _content;

    // Original parent and layout state so content can be restored on close/dispose.
    private Control? _originalParent;
    private int _originalIndex;
    private DockStyle _originalDock;
    private bool _originalVisible;
    #endregion

    #region Events
    /// <summary>
    /// Raised when the overlay wants to be closed (Back button or ESC).
    /// </summary>
    internal event EventHandler? BackRequested;
    #endregion

    #region Identity
    /// <summary>
    /// Create a new backstage overlay owned by the provided form.
    /// </summary>
    /// <param name="ownerForm">Top-level form that contains the ribbon.</param>
    /// <param name="overlayMode">Overlay mode that determines coverage area.</param>
    /// <param name="ribbon">Reference to the ribbon for below-ribbon mode calculations.</param>
    internal VisualBackstageOverlayForm(Form ownerForm, BackstageOverlayMode overlayMode = BackstageOverlayMode.FullClient, KryptonRibbon? ribbon = null)
    {
        _ownerForm = ownerForm ?? throw new ArgumentNullException(nameof(ownerForm));

        _overlayMode = overlayMode;

        _ribbon = ribbon;

        // Configure as a simple, borderless overlay window.
        //
        // Note: KryptonForm normally supports custom chrome (caption/border). For an overlay we explicitly
        // disable that behavior so the form behaves like a plain borderless surface.
        UseThemeFormChromeBorderWidth = false;
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        KeyPreview = true;

        // Root and children use an app-menu style so the backstage visually matches the existing app menu.
        _root = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.ControlRibbonAppMenu
        };

        _header = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = (int)(40 * (DeviceDpi / 96f)),
            PanelBackStyle = PaletteBackStyle.ControlRibbonAppMenu
        };

        _backButton = new KryptonButton
        {
            Dock = DockStyle.Left,
            Width = (int)(90 * (DeviceDpi / 96f)),
            Text = @"Back"
        };
        _backButton.Click += OnBackButtonClick;

        _contentHost = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.ControlRibbonAppMenu
        };

        // Compose visual tree.
        _header.Controls.Add(_backButton);
        _root.Controls.Add(_contentHost);
        _root.Controls.Add(_header);
        Controls.Add(_root);

        // Track owner bounds/visibility so the overlay behaves like part of the main window.
        AttachToOwner();
    }

    /// <summary>
    /// Clean up owner event hooks and restore any hosted content to its original parent.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DetachFromOwner();
            RestoreContent();

            _backButton.Click -= OnBackButtonClick;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Host the provided backstage content inside the overlay.
    /// </summary>
    /// <param name="content">
    /// Content to host. If the content already has a parent it will be temporarily removed and restored on close.
    /// </param>
    internal void SetContent(Control? content)
    {
        if (ReferenceEquals(_content, content))
        {
            return;
        }

        // Always restore previous content before switching.
        RestoreContent();
        _content = content;

        if (_content != null)
        {
            // Snapshot original parent/layout for restoration.
            _originalParent = _content.Parent;
            _originalDock = _content.Dock;
            _originalVisible = _content.Visible;
            _originalIndex = -1;

            if (_originalParent != null)
            {
                _originalIndex = _originalParent.Controls.GetChildIndex(_content, false);
                _originalParent.Controls.Remove(_content);
            }

            // While hosted we fill the client area and ensure it is visible.
            _content.Dock = DockStyle.Fill;
            _content.Visible = true;
            _contentHost.Controls.Add(_content);
        }
    }

    /// <summary>
    /// Update overlay bounds to match the owner's client rectangle and show/bring-to-front as needed.
    /// </summary>
    /// <remarks>
    /// If the owner is minimized the overlay is hidden until the owner is restored.
    /// </remarks>
    internal void UpdateOwnerBounds()
    {
        if (_ownerForm.WindowState == FormWindowState.Minimized)
        {
            if (Visible)
            {
                Hide();
            }

            return;
        }

        Rectangle screenBounds;

        if (_overlayMode == BackstageOverlayMode.BelowRibbon && _ribbon != null)
        {
            // Calculate bounds for below-ribbon mode
            screenBounds = CalculateBelowRibbonBounds();
        }
        else
        {
            // Full client area mode (default)
            screenBounds = _ownerForm.RectangleToScreen(_ownerForm.ClientRectangle);
        }

        Bounds = screenBounds;

        if (!Visible)
        {
            // Show as owned window and activate so keyboard (ESC) works as expected.
            Show(_ownerForm);
            Activate();
        }
        else
        {
            BringToFront();
        }
    }

    /// <summary>
    /// Calculates the screen bounds for below-ribbon overlay mode.
    /// </summary>
    private Rectangle CalculateBelowRibbonBounds()
    {
        if (_ribbon == null)
        {
            // Fallback to full client area
            return _ownerForm.RectangleToScreen(_ownerForm.ClientRectangle);
        }

        // Get ribbon's screen bounds
        Rectangle ribbonScreenBounds = _ribbon.RectangleToScreen(_ribbon.ClientRectangle);

        // Get owner form's client rectangle in screen coordinates
        Rectangle ownerClientScreen = _ownerForm.RectangleToScreen(_ownerForm.ClientRectangle);

        // Calculate area below ribbon
        int top = ribbonScreenBounds.Bottom;
        int left = ownerClientScreen.Left;
        int width = ownerClientScreen.Width;
        int height = ownerClientScreen.Bottom - top;

        // Ensure we don't go negative
        if (height < 0)
        {
            height = 0;
        }

        return new Rectangle(left, top, width, height);
    }

    #endregion

    #region Implementation
    /// <summary>
    /// Hook owner notifications so the overlay tracks move/size/close.
    /// </summary>
    private void AttachToOwner()
    {
        _ownerForm.LocationChanged += OnOwnerBoundsChanged;
        _ownerForm.SizeChanged += OnOwnerBoundsChanged;
        _ownerForm.ClientSizeChanged += OnOwnerBoundsChanged;
        _ownerForm.VisibleChanged += OnOwnerVisibleChanged;
        _ownerForm.FormClosed += OnOwnerClosed;
    }

    /// <summary>
    /// Unhook owner notifications to avoid leaks.
    /// </summary>
    private void DetachFromOwner()
    {
        _ownerForm.LocationChanged -= OnOwnerBoundsChanged;
        _ownerForm.SizeChanged -= OnOwnerBoundsChanged;
        _ownerForm.ClientSizeChanged -= OnOwnerBoundsChanged;
        _ownerForm.VisibleChanged -= OnOwnerVisibleChanged;
        _ownerForm.FormClosed -= OnOwnerClosed;
    }

    /// <summary>
    /// Restore hosted content to its original parent (if any) and original layout/visibility.
    /// </summary>
    private void RestoreContent()
    {
        if (_content != null)
        {
            _contentHost.Controls.Remove(_content);
            _content.Dock = _originalDock;
            _content.Visible = _originalVisible;

            if (_originalParent != null)
            {
                _originalParent.Controls.Add(_content);

                if (_originalIndex >= 0)
                {
                    _originalParent.Controls.SetChildIndex(_content, _originalIndex);
                }
            }

            _content = null;
            _originalParent = null;
            _originalIndex = -1;
        }
    }

    /// <summary>
    /// Back button requests close (actual close orchestration is owned by <see cref="KryptonRibbon"/>).
    /// </summary>
    private void OnBackButtonClick(object? sender, EventArgs e) => BackRequested?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Handle ESC as a request to close the overlay.
    /// </summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.Escape)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
            e.Handled = true;
        }
    }

    /// <summary>
    /// Keep overlay bounds aligned with the owner.
    /// </summary>
    private void OnOwnerBoundsChanged(object? sender, EventArgs e) => UpdateOwnerBounds();

    /// <summary>
    /// If the owner is hidden, close the overlay as it has no meaningful place to be shown.
    /// </summary>
    private void OnOwnerVisibleChanged(object? sender, EventArgs e)
    {
        if (!_ownerForm.Visible)
        {
            Close();
        }
    }

    /// <summary>
    /// Owner has closed; ensure the overlay closes as well.
    /// </summary>
    private void OnOwnerClosed(object? sender, FormClosedEventArgs e) => Close();
    #endregion
}
