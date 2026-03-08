#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Converts traditional MDI (Multiple Document Interface) into a tabbed interface.
/// Drop this component on an MDI parent form; a tab bar appears below the menu/toolbar, and each MDI child gets a tab
/// with icon, caption, and close button. Child windows retain their native title bars and controls inside the MDI area.
/// </summary>
/// <remarks>
/// <para>
/// Add <see cref="KryptonTabbedMdiManager"/> to your MDI parent form. It automatically sets <see cref="Form.IsMdiContainer"/>,
/// adds a horizontal tab bar above the MDI client area, and creates a tab for each MDI child. Children remain as native
/// MDI windows (with title bars) inside the client; the tab bar provides quick switching and closing.
/// </para>
/// <para>
/// Child forms must be shown with <c>MdiParent = parentForm</c>. Each child appears both as a tab and as a normal
/// MDI window. Use <see cref="PageAdding"/> to customize the tab (e.g. set a thumbnail image) before it is added.
/// </para>
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonNavigator))]
[Designer(typeof(KryptonTabbedMdiManagerDesigner))]
[DefaultProperty(nameof(ParentForm))]
[Description(@"Converts MDI child windows into a tabbed interface.")]
public class KryptonTabbedMdiManager : Component
{
    #region Static Fields

    private static readonly FieldInfo? _mdiClientField = GetMdiClientField();

    private static FieldInfo? GetMdiClientField()
    {
        const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
        return typeof(Form).GetField("mdiclient", FLAGS)
            ?? typeof(Form).GetField("MdiClient", FLAGS);
    }

    #endregion

    #region Instance Fields

    private const int _defaultTabBarHeight = 34;

    private Form? _parentForm;
    private KryptonNavigator? _navigator;
    private Panel? _tabBarPanel;
    private Control? _mdiClient;
    private int _tabBarHeight = _defaultTabBarHeight;
    private readonly Dictionary<Form, KryptonPage> _formToPage;
    private readonly Dictionary<KryptonPage, Form> _pageToForm;
    private bool _initialized;
    private bool _disposed;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when an MDI child form is about to be added as a tab page.
    /// </summary>
    [Category(@"Tabbed MDI")]
    [Description(@"Occurs when an MDI child form is about to be added as a tab page.")]
    public event EventHandler<FormEventArgs>? PageAdding;

    /// <summary>
    /// Occurs when an MDI child form has been added as a tab page.
    /// </summary>
    [Category(@"Tabbed MDI")]
    [Description(@"Occurs when an MDI child form has been added as a tab page.")]
    public event EventHandler<FormEventArgs>? PageAdded;

    /// <summary>
    /// Occurs when a tab page is about to be removed (form closing).
    /// </summary>
    [Category(@"Tabbed MDI")]
    [Description(@"Occurs when a tab page is about to be removed.")]
    public event EventHandler<FormEventArgs>? PageRemoving;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonTabbedMdiManager"/> class.
    /// </summary>
    public KryptonTabbedMdiManager()
    {
        _formToPage = new Dictionary<Form, KryptonPage>();
        _pageToForm = new Dictionary<KryptonPage, Form>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonTabbedMdiManager"/> class with the specified container.
    /// </summary>
    /// <param name="container">The container for the component.</param>
    public KryptonTabbedMdiManager(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            UnwireParentForm();
            if (_navigator != null)
            {
                _navigator.CloseAction -= OnNavigatorCloseAction;
                _navigator.SelectedPageChanged -= OnNavigatorSelectedPageChanged;
                _navigator.Dispose();
                _navigator = null;
            }
            _tabBarPanel?.Dispose();
            _tabBarPanel = null;
            _mdiClient = null;
            _formToPage.Clear();
            _pageToForm.Clear();
        }

        _disposed = true;
        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the MDI parent form. When null, the component attempts to resolve the parent from its design-time container.
    /// </summary>
    [Category(@"Tabbed MDI")]
    [Description(@"The MDI parent form. Leave null to use the form the component is dropped on.")]
    [DefaultValue(null)]
    public Form? ParentForm
    {
        get => _parentForm ?? ResolveParentForm();
        set
        {
            if (_parentForm == value)
            {
                return;
            }

            UnwireParentForm();
            _parentForm = value;
            _initialized = false;
            WireParentForm();
        }
    }

    /// <summary>
    /// Gets the <see cref="KryptonNavigator"/> used to display tab pages.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonNavigator? Navigator => _navigator;

    /// <summary>
    /// Gets the collection of tab pages (one per MDI child).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPageCollection? Pages => _navigator?.Pages;

    /// <summary>
    /// Gets or sets the height in pixels of the tab bar above the MDI client area.
    /// </summary>
    [Category(@"Tabbed MDI")]
    [Description(@"The height in pixels of the tab bar above the MDI client area.")]
    [DefaultValue(_defaultTabBarHeight)]
    public int TabBarHeight
    {
        get => _tabBarHeight;
        set
        {
            var height = Math.Max(21, value);
            if (_tabBarHeight == height)
            {
                return;
            }

            _tabBarHeight = height;
            if (_tabBarPanel != null)
            {
                _tabBarPanel.Height = height;
                _tabBarPanel.MinimumSize = new Size(0, height);
            }
        }
    }

    #endregion

    #region Private

    private Form? ResolveParentForm()
    {
        if (Site?.Container is Form form)
        {
            return form;
        }

        return null;
    }

    private void WireParentForm()
    {
        var form = ParentForm;
        if (form == null || DesignMode)
        {
            return;
        }

        form.Load += OnParentFormLoad;
        form.Shown += OnParentFormShown;
        if (form.IsHandleCreated)
        {
            TryInitialize(form);
        }
    }

    private void UnwireParentForm()
    {
        if (_navigator != null)
        {
            _navigator.CloseAction -= OnNavigatorCloseAction;
            _navigator.SelectedPageChanged -= OnNavigatorSelectedPageChanged;
        }

        var form = _parentForm ?? ResolveParentForm();
        if (form != null)
        {
            form.Load -= OnParentFormLoad;
            form.Shown -= OnParentFormShown;
            form.MdiChildActivate -= OnMdiChildActivate;

            foreach (var kvp in _formToPage.ToList())
            {
                UnwireChildForm(kvp.Key);
            }
        }
    }

    private void OnParentFormLoad(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            TryInitialize(form);
        }
    }

    private void OnParentFormShown(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            TryInitialize(form);
        }
    }

    private void TryInitialize(Form form)
    {
        if (_initialized)
        {
            return;
        }

        form.IsMdiContainer = true;

        _mdiClient = GetMdiClient(form);
        if (_mdiClient == null)
        {
            return;
        }

        form.Load -= OnParentFormLoad;
        form.Shown -= OnParentFormShown;

        var mdiParent = _mdiClient.Parent;
        if (mdiParent == null)
        {
            return;
        }

        _tabBarPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = _tabBarHeight,
            MinimumSize = new Size(0, _tabBarHeight)
        };

        _navigator = new KryptonNavigator
        {
            Dock = DockStyle.Fill,
            NavigatorMode = NavigatorMode.BarTabOnly,
            Button =
            {
                CloseButtonDisplay = ButtonDisplay.ShowEnabled,
                CloseButtonAction = CloseButtonAction.RemovePage
            }
        };

        _tabBarPanel.Controls.Add(_navigator);
        mdiParent.Controls.Add(_tabBarPanel);
        _tabBarPanel.BringToFront();

        _navigator.SelectedPageChanged += OnNavigatorSelectedPageChanged;
        _navigator.CloseAction += OnNavigatorCloseAction;

        form.MdiChildActivate += OnMdiChildActivate;

        _initialized = true;

        foreach (Form child in form.MdiChildren)
        {
            AddFormAsPage(child);
        }
    }

    private static Control? GetMdiClient(Form form)
    {
        // Try reflection first (field name varies: "mdiclient" in .NET Framework, "MdiClient" in .NET 5+)
        if (_mdiClientField?.GetValue(form) is Control mdiClient)
        {
            return mdiClient;
        }

        // Fallback: search control hierarchy for MdiClient (KryptonForm and other wrappers may nest it)
        return FindMdiClientRecursive(form);
    }

    private static Control? FindMdiClientRecursive(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            if (control.GetType().Name == "MdiClient")
            {
                return control;
            }

            var found = FindMdiClientRecursive(control);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    private void OnMdiChildActivate(object? sender, EventArgs e)
    {
        var form = ParentForm;
        if (form == null || _navigator == null)
        {
            return;
        }

        var activeChild = form.ActiveMdiChild;
        if (activeChild != null && !_formToPage.ContainsKey(activeChild))
        {
            AddFormAsPage(activeChild);
        }
        else if (activeChild != null && _formToPage.TryGetValue(activeChild, out var page))
        {
            _navigator.SelectedPage = page;
        }
    }

    private void AddFormAsPage(Form childForm)
    {
        if (_navigator == null || _formToPage.ContainsKey(childForm))
        {
            return;
        }

        var args = new FormEventArgs(childForm);
        PageAdding?.Invoke(this, args);
        if (args.Cancel)
        {
            return;
        }

        var page = new KryptonPage
        {
            Text = childForm.Text,
            TextTitle = childForm.Text,
            UniqueName = Guid.NewGuid().ToString()
        };

        if (childForm.Icon != null)
        {
            try
            {
                page.ImageSmall = childForm.Icon.ToBitmap();
            }
            catch
            {
                // Icon conversion can fail; ignore
            }
        }

        childForm.TextChanged += OnChildFormTextChanged;
        childForm.FormClosed += OnChildFormClosed;

        _formToPage[childForm] = page;
        _pageToForm[page] = childForm;

        _navigator.Pages.Add(page);
        _navigator.SelectedPage = page;

        PageAdded?.Invoke(this, new FormEventArgs(childForm));
    }

    private void OnNavigatorCloseAction(object? sender, CloseActionEventArgs e)
    {
        if (e.Item != null && _pageToForm.TryGetValue(e.Item, out var childForm) && !childForm.IsDisposed)
        {
            e.Action = CloseButtonAction.None;
            childForm.Close();
        }
    }

    private void UnwireChildForm(Form childForm)
    {
        childForm.TextChanged -= OnChildFormTextChanged;
        childForm.FormClosed -= OnChildFormClosed;

        if (_formToPage.TryGetValue(childForm, out var page))
        {
            _formToPage.Remove(childForm);
            _pageToForm.Remove(page);

            if (_navigator != null && _navigator.Pages.Contains(page))
            {
                PageRemoving?.Invoke(this, new FormEventArgs(childForm));
                _navigator.Pages.Remove(page);
            }
        }
    }

    private void OnChildFormTextChanged(object? sender, EventArgs e)
    {
        if (sender is Form form && _formToPage.TryGetValue(form, out var page))
        {
            page.Text = form.Text;
            page.TextTitle = form.Text;
        }
    }

    private void OnChildFormClosed(object? sender, FormClosedEventArgs e)
    {
        if (sender is Form form)
        {
            UnwireChildForm(form);
        }
    }

    private void OnNavigatorSelectedPageChanged(object? sender, EventArgs e)
    {
        if (_navigator?.SelectedPage != null
            && _pageToForm.TryGetValue(_navigator.SelectedPage, out var form)
            && !form.IsDisposed)
        {
            form.Select();
            form.Focus();
        }
    }

    #endregion
}

