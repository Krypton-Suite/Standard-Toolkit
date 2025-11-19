#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Allows the user to change themes using a <see cref="KryptonComboBox"/>.</summary>
/// <seealso cref="KryptonComboBox" />
[Designer(typeof(KryptonStubDesigner))]
public class KryptonThemeComboBox : KryptonComboBox, IKryptonThemeSelectorBase
{
    #region Instance Fields

    /// <summary> When we change the palette, Krypton Manager will notify us that there was a change. Since we are changing it that notification can be skipped.</summary>
    private bool _isLocalUpdate = false;
    /// <summary> Suppress code execution in the SelectedIndexChanged event handler, when a theme change via the KManager has been performed.</summary>
    private bool _isExternalUpdate = false;
    /// <summary> Backing var for the DefaultPalette property.</summary>
    private PaletteMode _defaultPalette = PaletteMode.Global;
    /// <summary> Local Krypton Manager instance.</summary>
    private readonly KryptonManager _manager;
    /// <summary> User defined palette.</summary>
    private KryptonCustomPaletteBase? _kryptonCustomPalette = null;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
    public KryptonThemeComboBox()
    {
        _manager = new KryptonManager();
        DropDownStyle = ComboBoxStyle.DropDownList;

        Items.Clear();
        Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());

        // Sets the intial palette from either global or DefaultPalette property
        SelectedIndex = CommonHelperThemeSelectors.GetInitialSelectedIndex(DefaultPalette, _manager, Items);

    }
    #endregion

    #region Public

    /// <inheritdoc/>
    [Category(@"Visuals")]
    [Description(@"The custom assigned palette mode.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Obsolete("Deprecated and will be removed in V110. Set a global custom palette through 'ThemeManager.ApplyTheme(...)'.")]
    public KryptonCustomPaletteBase? KryptonCustomPalette
    {
        get => _kryptonCustomPalette;
        set => _kryptonCustomPalette = value;
    }

    private void ResetKryptonCustomPalette() => _kryptonCustomPalette = null;
    private bool ShouldSerializeKryptonCustomPalette() => _kryptonCustomPalette is not null;

    /// <inheritdoc/>
    [Category(@"Visuals")]
    [Description(@"The default palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public PaletteMode DefaultPalette
    {
        get => _defaultPalette;
        set => SelectedIndex = CommonHelperThemeSelectors.DefaultPaletteSetter(ref _defaultPalette, value, Items, SelectedIndex);
    }

    private void ResetDefaultPalette() => DefaultPalette = PaletteMode.Global;
    private bool ShouldSerializeDefaultPalette() => _defaultPalette != PaletteMode.Global;

    #endregion

    #region Implementation

    /// <summary>
    /// Routine that will be executed when the control is fully instantiated.
    /// </summary>
    /// <param name="e">EventArgs param. Not used in this implementation.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // React to theme changes from outside this control.
        KryptonManager.GlobalPaletteChanged += KryptonManagerGlobalPaletteChanged;
        base.OnHandleCreated(e);
    }
    /// <summary>
    /// This method will run when the KryptonManager.GlobalPaletteChanged event is fired.<br/>
    /// It will synchronize the SelectedIndex with the newly assigned Global Palette.
    /// </summary>
    /// <param name="sender">Object that intiated the call.</param>
    /// <param name="e">Eventargs object data (not used).</param>
    private void KryptonManagerGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_isLocalUpdate)
        {
            return;
        }

        var mode = KryptonManager.CurrentGlobalPaletteMode;
        if (mode == PaletteMode.Global)
        {
            return;
        }

        int idx = CommonHelperThemeSelectors.GetPaletteIndex(Items, mode);
        if (idx == SelectedIndex)
        {
            return;
        }

        void Commit()
        {
            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }
            _isExternalUpdate = true;
            try
            {
                SelectedIndex = idx;
            }
            finally
            {
                _isExternalUpdate = false;
            }
        }

        if (ThemeChangeCoordinator.InProgress && !IsDisposed && IsHandleCreated)
        {
            BeginInvoke((System.Windows.Forms.MethodInvoker)Commit);
        }
        else
        {
            // If the handle is not yet created (or disposed), update immediately to avoid InvalidOperationException
            Commit();
        }
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        // Disable redraw immediately to reduce flicker; defer the heavy theme swap until after WM_COMMAND unwinds
        if (IsHandleCreated)
        {
            PI.SendMessage(Handle, PI.SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        if (!IsHandleCreated)
        {
            base.OnSelectedIndexChanged(e);
            return;
        }

        BeginInvoke((System.Windows.Forms.MethodInvoker)(() =>
        {
            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }
            // Mark this form as the initiator so redraw is not disabled for it during the change
            ThemeChangeCoordinator.Begin(FindForm());
            try
            {
                if (!CommonHelperThemeSelectors.OnSelectedIndexChanged(ref _isLocalUpdate, _isExternalUpdate, ref _defaultPalette, Text, _manager, _kryptonCustomPalette))
                {
                    // theme change failed; resync index to current global palette
                    SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(Items, _manager.GlobalPaletteMode);
                }

                base.OnSelectedIndexChanged(e);
            }
            finally
            {
                ThemeChangeCoordinator.End();
            }

            if (IsHandleCreated)
            {
                // Re-enable redraw and perform a single composited repaint to reduce flicker
                PI.SendMessage(Handle, PI.SETREDRAW, (IntPtr)1, IntPtr.Zero);
                var form = FindForm();
                if (form is { IsHandleCreated: true })
                {
                    // Force full subtree invalidation and a one-pass update including children and frame
                    form.Invalidate(true);
                    PI.RedrawWindow(form.Handle, IntPtr.Zero, IntPtr.Zero,
                        PI.RDW_INVALIDATE | PI.RDW_ALLCHILDREN | PI.RDW_UPDATENOW | PI.RDW_FRAME);
                }
                else
                {
                    Invalidate(true);
                    Update();
                }
            }
        }));
    }

    #endregion

    #region Removed Designer Visibility

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        //nullable operator removed
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>Gets or sets the format specifier characters that indicate how a value is to be Displayed.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string FormatString
    {
        get => base.FormatString;
        set => base.FormatString = value;
    }

    /// <summary>
    /// Gets and sets the appearance and functionality of the KryptonComboBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ComboBoxStyle DropDownStyle
    {
        get => base.DropDownStyle;
        set => base.DropDownStyle = value;
    }

    /// <summary>
    /// Gets or sets the items in the KryptonComboBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ComboBox.ObjectCollection Items => base.Items;

    /// <summary>Gets or sets the draw mode of the combobox.</summary>
    /// <value>The draw mode of the combobox.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DrawMode DrawMode
    {
        get => base.DrawMode;
        set => base.DrawMode = value;
    }

    /// <summary>
    /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get => base.AutoCompleteCustomSource;
        set => base.AutoCompleteCustomSource = value;
    }

    /// <summary>Gets or sets the text completion behavior of the combobox.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new AutoCompleteMode AutoCompleteMode
    {
        get => base.AutoCompleteMode;
        set => base.AutoCompleteMode = value;
    }

    /// <summary>Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new AutoCompleteSource AutoCompleteSource
    {
        get => base.AutoCompleteSource;
        set => base.AutoCompleteSource = value;
    }

    /// <summary>Gets and sets the selected index.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int SelectedIndex
    {
        get => base.SelectedIndex;
        set => base.SelectedIndex = value;
    }

    #endregion
}