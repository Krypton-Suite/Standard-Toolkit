#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Exposes hosted ButtonSpec instances as UIA-visible controls without changing their rendered button views.
/// </summary>
internal sealed class ButtonSpecAccessibilityProxyManager : IDisposable
{
    #region Classes
    private sealed class ButtonSpecAccessibilityProxy : Control
    {
        #region Classes
        private sealed class ButtonSpecAccessibilityProxyAccessibleObject : Control.ControlAccessibleObject
        {
            #region Instance Fields
            private readonly ButtonSpecAccessibilityProxy _owner;
            #endregion

            #region Identity
            public ButtonSpecAccessibilityProxyAccessibleObject(ButtonSpecAccessibilityProxy owner)
                : base(owner)
            {
                _owner = owner;
            }
            #endregion

            #region Public Overrides
            public override string? Name => _owner.AccessibleName;

            public override string? Description => _owner.AccessibleDescription;

            public override AccessibleRole Role => AccessibleRole.PushButton;

            public override AccessibleStates State
            {
                get
                {
                    AccessibleStates state = AccessibleStates.Focusable;

                    if (!_owner.Visible)
                    {
                        state |= AccessibleStates.Invisible;
                    }

                    if (!_owner.Enabled)
                    {
                        state |= AccessibleStates.Unavailable;
                    }

                    return state;
                }
            }

            public override string DefaultAction => @"Press";

            public override void DoDefaultAction()
            {
                if (_owner.Enabled && _owner.Visible)
                {
                    _owner.ButtonSpec.PerformClick();
                }
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private readonly ButtonSpec _buttonSpec;
        #endregion

        #region Identity
        public ButtonSpecAccessibilityProxy(ButtonSpec buttonSpec)
        {
            _buttonSpec = buttonSpec;

            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            TabStop = false;
            AccessibleRole = AccessibleRole.PushButton;

            UpdateAccessibility();
        }
        #endregion

        #region Public
        public ButtonSpec ButtonSpec => _buttonSpec;

        public void UpdateAccessibility()
        {
            AccessibleName = GetAccessibleName();
            AccessibleDescription = !string.IsNullOrEmpty(_buttonSpec.ToolTipBody)
                ? _buttonSpec.ToolTipBody
                : null;
        }
        #endregion

        #region Protected
        protected override void OnClick(EventArgs e)
        {
            _buttonSpec.PerformClick(e);
            base.OnClick(e);
        }

        protected override AccessibleObject CreateAccessibilityInstance() => new ButtonSpecAccessibilityProxyAccessibleObject(this);

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }
        #endregion

        #region Implementation
        private string GetAccessibleName()
        {
            if (!string.IsNullOrEmpty(_buttonSpec.ToolTipTitle))
            {
                return _buttonSpec.ToolTipTitle;
            }

            if (!string.IsNullOrEmpty(_buttonSpec.Text))
            {
                return _buttonSpec.Text;
            }

            return _buttonSpec is ButtonSpecAny buttonSpecAny
                ? buttonSpecAny.Type.ToString()
                : _buttonSpec.GetType().Name;
        }
        #endregion
    }
    #endregion

    #region Instance Fields
    private readonly Control _owner;
    private readonly ButtonSpecCollectionBase _buttonSpecs;
    private readonly Func<ButtonSpecManagerBase?> _getButtonSpecManager;
    private readonly Dictionary<ButtonSpec, ButtonSpecAccessibilityProxy> _proxies = new Dictionary<ButtonSpec, ButtonSpecAccessibilityProxy>();
    private bool _disposed;
    #endregion

    #region Identity
    public ButtonSpecAccessibilityProxyManager(Control owner, ButtonSpecCollectionBase buttonSpecs, Func<ButtonSpecManagerBase?> getButtonSpecManager)
    {
        _owner = owner;
        _buttonSpecs = buttonSpecs;
        _getButtonSpecManager = getButtonSpecManager;

        _buttonSpecs.Inserted += OnButtonSpecInserted;
        _buttonSpecs.Removed += OnButtonSpecRemoved;
    }
    #endregion

    #region Public
    public void Sync()
    {
        if (_disposed)
        {
            return;
        }

        ButtonSpecManagerBase? buttonSpecManager = _getButtonSpecManager();

        if (buttonSpecManager == null)
        {
            return;
        }

        ButtonSpec[] buttonSpecs = _buttonSpecs.Enumerate().Cast<ButtonSpec>().ToArray();

        foreach (ButtonSpec buttonSpec in _proxies.Keys.Except(buttonSpecs).ToArray())
        {
            RemoveProxy(buttonSpec);
        }

        foreach (ButtonSpec buttonSpec in buttonSpecs)
        {
            if (!_proxies.TryGetValue(buttonSpec, out ButtonSpecAccessibilityProxy? proxy))
            {
                proxy = new ButtonSpecAccessibilityProxy(buttonSpec);
                _proxies.Add(buttonSpec, proxy);
                AddProxy(proxy);
                proxy.BringToFront();
            }

            Rectangle rectangle = buttonSpecManager.GetButtonRectangle(buttonSpec);
            proxy.Bounds = rectangle == Rectangle.Empty
                ? Rectangle.Empty
                : new Rectangle(rectangle.Location, new Size(1, 1));
            proxy.Visible = rectangle != Rectangle.Empty && buttonSpec.GetViewEnabled();
            proxy.Enabled = _owner.Enabled && buttonSpec.GetViewEnabled();
            proxy.UpdateAccessibility();
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _buttonSpecs.Inserted -= OnButtonSpecInserted;
        _buttonSpecs.Removed -= OnButtonSpecRemoved;

        foreach (ButtonSpec buttonSpec in _proxies.Keys.ToArray())
        {
            RemoveProxy(buttonSpec);
        }

        _proxies.Clear();
    }
    #endregion

    #region Implementation
    private void OnButtonSpecInserted(object? sender, ButtonSpecEventArgs e) => Sync();

    private void OnButtonSpecRemoved(object? sender, ButtonSpecEventArgs e) => Sync();

    private void RemoveProxy(ButtonSpec buttonSpec)
    {
        if (_proxies.TryGetValue(buttonSpec, out ButtonSpecAccessibilityProxy? proxy))
        {
            _proxies.Remove(buttonSpec);
            RemoveProxyControl(proxy);
            proxy.Dispose();
        }
    }

    private void AddProxy(ButtonSpecAccessibilityProxy proxy)
    {
        if (_owner.Controls is KryptonReadOnlyControls controls)
        {
            controls.AddInternal(proxy);
        }
        else
        {
            _owner.Controls.Add(proxy);
        }
    }

    private void RemoveProxyControl(ButtonSpecAccessibilityProxy proxy)
    {
        if (_owner.Controls is KryptonReadOnlyControls controls)
        {
            controls.RemoveInternal(proxy);
        }
        else
        {
            _owner.Controls.Remove(proxy);
        }
    }
    #endregion
}