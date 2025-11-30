#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonTaskDialogElementCommandLinkButtons : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementRoundedCorners,
    IKryptonTaskDialogElementFlowDirection
{
    #region Fields
    internal Action? CollectionEditorClosed;
    internal bool CollectionEditorActive { get; set; }

    private readonly ObservableCollection<KryptonCommandLinkButton> _buttons;
    private readonly TableLayoutPanel _tlp;
    private readonly FlowLayoutPanel _flp;
    private readonly KryptonButton _btnFlowDirection;
    private bool _disposed;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor
    /// </summary>
    public KryptonTaskDialogElementCommandLinkButtons(KryptonTaskDialogDefaults taskDialogDefaults)
        : base(taskDialogDefaults)
    {
        _disposed = false;

        _buttons = new();
        _buttons.CollectionChanged += OnButtonsChanged;

        _btnFlowDirection = new();
        ShowFlowDirection = false;

        _flp = new();
        _tlp = new();

        SetupPanel();

        CollectionEditorClosed += OnCollectionEditorClosed;
        LayoutDirty = true;
    }
    #endregion

    private void OnCollectionEditorClosed() 
    {
        // Dummy array to get the correct constructor call that supports the Replace action
        List<string> l = [];
        NotifyCollectionChangedEventArgs e = new(NotifyCollectionChangedAction.Replace, l, l, 0);
        
        // The CollectionEditor has been closed, refresh the items
        OnButtonsChanged(this, e);
    }
    #region Public
    /// <summary>
    /// Krypton CommandLink Buttons collection.
    /// </summary>
    [Editor(typeof(KryptonTaskDialogElementCommandLinkButtons.ButtonsCollectionEditor), typeof(UITypeEditor))]
    public ObservableCollection<KryptonCommandLinkButton> Buttons => _buttons;

    /// <summary>
    /// Rounds the button corners.
    /// </summary>
    public bool RoundedCorners
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                SetRoundedCorners();
            }
        }
    }

    /// <inheritdoc/>
    public bool ShowFlowDirection 
    { 
        get => field;  
        
        set
        {
            if (field != value)
            {
                field = value;
                
                _btnFlowDirection.Visible = value;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    /// <inheritdoc/>
    public FlowDirection FlowDirection 
    { 
        get => _flp.FlowDirection;

        set
        {
            if (_flp.FlowDirection != value)
            {
                _flp.FlowDirection = value;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    #endregion

    #region Protected/Internal
    /// <inheritdoc/>
    protected override void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        base.OnPalettePaint(sender, e);

        // Flag dirty, and if visible call OnSizeChanged,
        // otherwise leave it deferred for a call from PerformLayout.
        LayoutDirty = true;
        if (Visible)
        {
            OnSizeChanged();
        }
    }

    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            // Flp needs a little help before it returns the correct height
            _flp.Refresh();
            Panel.Height = _tlp.Height + Defaults.PanelTop + Defaults.PanelBottom;

            base.OnSizeChanged(performLayout);
            LayoutDirty = false;
        }
    }

    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }
    #endregion

    #region Private
    private void SetRoundedCorners()
    {
        _btnFlowDirection.StateCommon.Border.Rounding = Defaults.GetCornerRouding(RoundedCorners);

        foreach ( KryptonCommandLinkButton button in _buttons)
        {
            SetButtonRoundedCorners(button);
        }
    }

    private void SetButtonRoundedCorners(KryptonCommandLinkButton button)
    {
        button.StateCommon.Border.Rounding = Defaults.GetCornerRouding(RoundedCorners);
    }

    private void SetupTableLayoutPanel()
    {
        _tlp.SetDoubleBuffered(true);
        _tlp.AutoSize = true;
        _tlp.Left = Defaults.PanelLeft;
        _tlp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tlp.MinimumSize = Defaults.TLP.StdMinSize;
        _tlp.MaximumSize = Defaults.TLP.StdMaxSize;
        _tlp.Padding = Defaults.NullPadding;
        _tlp.Margin = Defaults.NullMargin;
        _tlp.BackColor = Color.Transparent;

        _tlp.RowCount = 1;
        _tlp.ColumnCount = 2;

        _tlp.RowStyles.Clear();
        _tlp.ColumnStyles.Clear();
        _tlp.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
        _tlp.ColumnStyles.Add( new ColumnStyle( SizeType.AutoSize ) );
        _tlp.ColumnStyles.Add( new ColumnStyle( SizeType.AutoSize ) );
    }

    private void SetupFlowLayoutPanel()
    {
        // Note: do not dock the flow layout panel as that changes the behaviour of the flow direction.
        _flp.AutoSize = true;
        _flp.SetDoubleBuffered( true );
        _flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _flp.Top = 0;
        _flp.Left = 0;
        _flp.Margin = Defaults.NullMargin;
        _flp.Padding = new Padding( 0, Defaults.ComponentSpace, 0, 0 );
        _flp.Width = Panel.Width - Defaults.PanelLeft - Defaults.PanelRight - 20;
        _flp.MaximumSize = new Size( _flp.Width, 0 );
        _flp.BackColor = Color.Transparent;
    }

    private void SetupControls()
    {
        _btnFlowDirection.AutoSize = true;
        _btnFlowDirection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _btnFlowDirection.Size = Defaults.ButtonSize_24x75;
        _btnFlowDirection.MinimumSize = Defaults.ButtonSize_24x75;
        _btnFlowDirection.Margin = new Padding( 0, Defaults.ComponentSpace + 2, Defaults.ComponentSpace, 0 );
        _btnFlowDirection.Text = _flp.FlowDirection.ToString();
        _btnFlowDirection.Orientation = VisualOrientation.Left;
        _btnFlowDirection.ButtonOrientation = VisualOrientation.Left;
        _btnFlowDirection.Visible = false;
        _btnFlowDirection.Click += OnBtnFlowDirectionClick;
    }

    private void SetupPanel()
    {
        Panel.Width = Defaults.ClientWidth;

        SetupTableLayoutPanel();
        SetupFlowLayoutPanel();
        SetupControls();

        // Assemble the controls
        Panel.Controls.Add(_tlp);
        _tlp.Controls.Add(_btnFlowDirection, 0, 0);
        _tlp.Controls.Add(_flp, 1, 0);
    }

    private void OnBtnFlowDirectionClick(object? sender, EventArgs e)
    {
        _flp.FlowDirection = _flp.FlowDirection switch
        {
            FlowDirection.LeftToRight => FlowDirection.TopDown,
            FlowDirection.TopDown     => FlowDirection.RightToLeft,
            FlowDirection.RightToLeft => FlowDirection.BottomUp,
            FlowDirection.BottomUp    => FlowDirection.LeftToRight,
            _                         => FlowDirection.LeftToRight
        };

        _btnFlowDirection.Text = $"{_flp.FlowDirection}";
        LayoutDirty = true;
        OnSizeChanged();
    }

    private void OnButtonsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        /*
         *   The builtin winforms list editor only triggers Add and Reset actions.
         *   So, before adding new items it resets (clears) the list, leaving the references in the Controls container orphaned.
         *   Calling Flp.Controls.Clear() on the reset will clear those.
         *
         *   CollectionEditorActive suspends the editor's changes being pushed incrementally, when used on a propertygrid this leads to multiple form refreshes.
         *   The adapted CollectionEditor sets this flag to defer until the editor form is closed. On editor form close the event is fired to trigger the list update 
         *   and sync the list to the flow layout panel.
         */

        if (!CollectionEditorActive
            && e.Action is NotifyCollectionChangedAction.Add or NotifyCollectionChangedAction.Reset or NotifyCollectionChangedAction.Replace)
        {
            // Replace is used here to sync the list when the CollectionEditor is closed.
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                _flp.Controls.Clear();

                for (int i = 0; i < Buttons.Count; i++)
                {
                    _flp.Controls.Add(Buttons[i]);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems is not null)
            {
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    if (e.NewItems[i] is KryptonCommandLinkButton button)
                    {
                        _flp.Controls.Add(button);
                        SetButtonRoundedCorners(button);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _flp.Controls.Clear();
            }

            LayoutDirty = true;
            OnSizeChanged();
        }
    }
    #endregion

    #region IDispose
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            if (CollectionEditorClosed is not null)
            {
                CollectionEditorClosed -= OnCollectionEditorClosed;
            }

            _btnFlowDirection.Click -= OnBtnFlowDirectionClick;
            _buttons.CollectionChanged -= OnButtonsChanged;
            _buttons.Clear();
            _flp.Controls.Clear();

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
