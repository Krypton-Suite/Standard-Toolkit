#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Designer-friendly list of caption controls for a <see cref="KryptonCaptionForm"/>.
/// </summary>
/// <remarks>
/// <para>
/// Drop this component on a <see cref="KryptonCaptionForm"/>, set <see cref="OwnerForm"/>, and add
/// <see cref="KryptonFormCaptionItem"/> entries to <see cref="Items"/>. Each item keeps its own
/// <see cref="FormCaptionTextBoxEdge"/>. At runtime the component calls
/// <see cref="KryptonCaptionForm.AddCaptionControl"/>.
/// </para>
/// <para>
/// Use this when several caption controls are needed. For a single text box,
/// <see cref="KryptonCaptionForm.CaptionTextBox"/> is enough and does not require this component.
/// </para>
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonForm))]
[DefaultProperty(nameof(Items))]
[DesignerCategory(@"code")]
[Description(@"Hosts a list of caption controls on a KryptonCaptionForm, each with its own edge.")]
public class KryptonFormCaptionItems : Component, ISupportInitialize
{
    #region Instance Fields

    private readonly KryptonFormCaptionItemCollection _items;
    private readonly List<Control> _applied = new List<Control>();
    private KryptonCaptionForm? _ownerForm;
    private bool _initializing;
    private bool _applyPending;
    private bool _applying;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItems"/> class.
    /// </summary>
    public KryptonFormCaptionItems() => _items = new KryptonFormCaptionItemCollection(this);

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItems"/> class.
    /// </summary>
    /// <param name="container">Container that owns this component.</param>
    public KryptonFormCaptionItems(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the caption form that receives the caption controls.
    /// </summary>
    /// <remarks>
    /// When null, the component tries the designer root component or a <see cref="KryptonCaptionForm"/>
    /// in the same container.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"KryptonCaptionForm that hosts the caption controls.")]
    [DefaultValue(null)]
    public KryptonCaptionForm? OwnerForm
    {
        get => _ownerForm;

        set
        {
            if (ReferenceEquals(_ownerForm, value))
            {
                return;
            }

            UnhookOwnerForm();
            _ownerForm = value;
            HookOwnerForm();
            RequestApply();
        }
    }

    /// <summary>
    /// Gets the caption items. Each entry has its own edge.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Caption controls and their edges.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonFormCaptionItemCollectionEditor), typeof(UITypeEditor))]
    public KryptonFormCaptionItemCollection Items => _items;

    /// <summary>
    /// Applies <see cref="Items"/> to <see cref="OwnerForm"/>.
    /// </summary>
    public void Apply()
    {
        if (_applying || _initializing || DesignMode)
        {
            _applyPending = true;
            return;
        }

        KryptonCaptionForm? form = ResolveOwnerForm();
        if (form == null || form.IsDisposed)
        {
            return;
        }

        _applying = true;
        try
        {
            var wanted = new List<Control>();
            foreach (KryptonFormCaptionItem item in _items)
            {
                if (item.Control is { IsDisposed: false } control && !(control is Form))
                {
                    wanted.Add(control);
                }
            }

            for (var i = _applied.Count - 1; i >= 0; i--)
            {
                Control previous = _applied[i];
                if (!ContainsControl(wanted, previous))
                {
                    form.RemoveCaptionControl(previous);
                    _applied.RemoveAt(i);
                }
            }

            foreach (KryptonFormCaptionItem item in _items)
            {
                if (item.Control is not { IsDisposed: false } control || control is Form)
                {
                    continue;
                }

                form.AddCaptionControl(control, item.Edge);
                if (!ContainsControl(_applied, control))
                {
                    _applied.Add(control);
                }
            }
        }
        finally
        {
            _applying = false;
            if (_applyPending)
            {
                _applyPending = false;
                Apply();
            }
        }
    }

    /// <summary>
    /// Signals the start of component initialization.
    /// </summary>
    public void BeginInit() => _initializing = true;

    /// <summary>
    /// Signals the end of component initialization and applies the items.
    /// </summary>
    public void EndInit()
    {
        _initializing = false;
        Apply();
    }

    #endregion

    #region Internal

    /// <summary>
    /// Called when the item collection changes.
    /// </summary>
    internal void NotifyCollectionChanged() => RequestApply();

    /// <summary>
    /// Called when one item's control or edge changes.
    /// </summary>
    /// <param name="item">Changed item.</param>
    internal void NotifyItemChanged(KryptonFormCaptionItem item)
    {
        if (_applying || _initializing || DesignMode)
        {
            RequestApply();
            return;
        }

        KryptonCaptionForm? form = ResolveOwnerForm();
        if (form == null || item.Control == null || item.Control.IsDisposed || item.Control is Form)
        {
            RequestApply();
            return;
        }

        form.AddCaptionControl(item.Control, item.Edge);
        if (!ContainsControl(_applied, item.Control))
        {
            _applied.Add(item.Control);
        }
    }

    #endregion

    #region Protected

    /// <summary>
    /// Releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> when called from Dispose.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnhookOwnerForm();
            ClearApplied();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Implementation

    private void RequestApply()
    {
        if (_initializing || DesignMode)
        {
            _applyPending = true;
            return;
        }

        Apply();
    }

    private KryptonCaptionForm? ResolveOwnerForm()
    {
        if (_ownerForm is { IsDisposed: false })
        {
            return _ownerForm;
        }

        if (Site?.Container != null)
        {
            foreach (IComponent component in Site.Container.Components)
            {
                if (component is KryptonCaptionForm form && !form.IsDisposed)
                {
                    return form;
                }
            }
        }

        return null;
    }

    private void HookOwnerForm()
    {
        if (_ownerForm == null)
        {
            return;
        }

        _ownerForm.HandleCreated += OnOwnerFormHandleCreated;
        _ownerForm.Disposed += OnOwnerFormDisposed;
    }

    private void UnhookOwnerForm()
    {
        if (_ownerForm == null)
        {
            return;
        }

        _ownerForm.HandleCreated -= OnOwnerFormHandleCreated;
        _ownerForm.Disposed -= OnOwnerFormDisposed;
    }

    private void OnOwnerFormHandleCreated(object? sender, EventArgs e) => Apply();

    private void OnOwnerFormDisposed(object? sender, EventArgs e)
    {
        UnhookOwnerForm();
        _ownerForm = null;
        _applied.Clear();
    }

    private void ClearApplied()
    {
        KryptonCaptionForm? form = ResolveOwnerForm();
        if (form != null && !form.IsDisposed)
        {
            foreach (Control control in _applied)
            {
                if (!control.IsDisposed)
                {
                    form.RemoveCaptionControl(control);
                }
            }
        }

        _applied.Clear();
    }

    private static bool ContainsControl(List<Control> list, Control control)
    {
        foreach (Control item in list)
        {
            if (ReferenceEquals(item, control))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}
