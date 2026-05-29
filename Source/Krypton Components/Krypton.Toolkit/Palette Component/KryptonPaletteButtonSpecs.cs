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
/// Overrides for defining button specifications.
/// </summary>
public class KryptonPaletteButtonSpecs : Storage
{
    #region Events
    /// <summary>
    /// Occurs when a button spec change occurs.
    /// </summary>
    public event EventHandler? ButtonSpecChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteButtonSpecs class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    public KryptonPaletteButtonSpecs([DisallowNull] PaletteRedirect redirector)
    {
        Debug.Assert(redirector! != null);

        // Create exposed button specifications
        Common = new KryptonPaletteButtonSpecTyped(redirector!);
        Generic = new KryptonPaletteButtonSpecTyped(redirector!);
        Close = new KryptonPaletteButtonSpecTyped(redirector!);
        Context = new KryptonPaletteButtonSpecTyped(redirector!);
        Next = new KryptonPaletteButtonSpecTyped(redirector!);
        Previous = new KryptonPaletteButtonSpecTyped(redirector!);
        ArrowLeft = new KryptonPaletteButtonSpecTyped(redirector!);
        ArrowRight = new KryptonPaletteButtonSpecTyped(redirector!);
        ArrowUp = new KryptonPaletteButtonSpecTyped(redirector!);
        ArrowDown = new KryptonPaletteButtonSpecTyped(redirector!);
        DropDown = new KryptonPaletteButtonSpecTyped(redirector!);
        PinVertical = new KryptonPaletteButtonSpecTyped(redirector!);
        PinHorizontal = new KryptonPaletteButtonSpecTyped(redirector!);
        FormClose = new KryptonPaletteButtonSpecTyped(redirector!);
        FormMax = new KryptonPaletteButtonSpecTyped(redirector!);
        FormMin = new KryptonPaletteButtonSpecTyped(redirector!);
        FormRestore = new KryptonPaletteButtonSpecTyped(redirector!);
        FormHelp = new KryptonPaletteButtonSpecTyped(redirector!);
        PendantClose = new KryptonPaletteButtonSpecTyped(redirector!);
        PendantMin = new KryptonPaletteButtonSpecTyped(redirector!);
        PendantRestore = new KryptonPaletteButtonSpecTyped(redirector!);
        WorkspaceMaximize = new KryptonPaletteButtonSpecTyped(redirector!);
        WorkspaceRestore = new KryptonPaletteButtonSpecTyped(redirector!);
        RibbonMinimize = new KryptonPaletteButtonSpecTyped(redirector!);
        RibbonExpand = new KryptonPaletteButtonSpecTyped(redirector!);

        // Create redirector for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectButtonSpec(redirector!, Common);

        // Inform the button spec to use the new redirector
        Generic.SetRedirector(redirectCommon);
        Close.SetRedirector(redirectCommon);
        Context.SetRedirector(redirectCommon);
        Next.SetRedirector(redirectCommon);
        Previous.SetRedirector(redirectCommon);
        ArrowLeft.SetRedirector(redirectCommon);
        ArrowRight.SetRedirector(redirectCommon);
        ArrowUp.SetRedirector(redirectCommon);
        ArrowDown.SetRedirector(redirectCommon);
        DropDown.SetRedirector(redirectCommon);
        PinVertical.SetRedirector(redirectCommon);
        PinHorizontal.SetRedirector(redirectCommon);
        FormClose.SetRedirector(redirectCommon);
        FormMax.SetRedirector(redirectCommon);
        FormMin.SetRedirector(redirectCommon);
        FormRestore.SetRedirector(redirectCommon);
        FormHelp.SetRedirector(redirectCommon);
        PendantClose.SetRedirector(redirectCommon);
        PendantMin.SetRedirector(redirectCommon);
        PendantRestore.SetRedirector(redirectCommon);
        WorkspaceMaximize.SetRedirector(redirectCommon);
        WorkspaceRestore.SetRedirector(redirectCommon);
        RibbonMinimize.SetRedirector(redirectCommon);
        RibbonExpand.SetRedirector(redirectCommon);

        // Hook into the storage change events
        Common.ButtonSpecChanged += OnButtonSpecChanged;
        Generic.ButtonSpecChanged += OnButtonSpecChanged;
        Close.ButtonSpecChanged += OnButtonSpecChanged;
        Context.ButtonSpecChanged += OnButtonSpecChanged;
        Next.ButtonSpecChanged += OnButtonSpecChanged;
        Previous.ButtonSpecChanged += OnButtonSpecChanged;
        ArrowLeft.ButtonSpecChanged += OnButtonSpecChanged;
        ArrowRight.ButtonSpecChanged += OnButtonSpecChanged;
        ArrowUp.ButtonSpecChanged += OnButtonSpecChanged;
        ArrowDown.ButtonSpecChanged += OnButtonSpecChanged;
        DropDown.ButtonSpecChanged += OnButtonSpecChanged;
        PinVertical.ButtonSpecChanged += OnButtonSpecChanged;
        PinHorizontal.ButtonSpecChanged += OnButtonSpecChanged;
        FormClose.ButtonSpecChanged += OnButtonSpecChanged;
        FormMax.ButtonSpecChanged += OnButtonSpecChanged;
        FormMin.ButtonSpecChanged += OnButtonSpecChanged;
        FormRestore.ButtonSpecChanged += OnButtonSpecChanged;
        FormHelp.ButtonSpecChanged += OnButtonSpecChanged;
        PendantClose.ButtonSpecChanged += OnButtonSpecChanged;
        PendantMin.ButtonSpecChanged += OnButtonSpecChanged;
        PendantRestore.ButtonSpecChanged += OnButtonSpecChanged;
        WorkspaceMaximize.ButtonSpecChanged += OnButtonSpecChanged;
        WorkspaceRestore.ButtonSpecChanged += OnButtonSpecChanged;
        RibbonMinimize.ButtonSpecChanged += OnButtonSpecChanged;
        RibbonExpand.ButtonSpecChanged += OnButtonSpecChanged;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Common.IsDefault &&
                                      Generic.IsDefault &&
                                      Close.IsDefault &&
                                      Context.IsDefault &&
                                      Next.IsDefault &&
                                      Previous.IsDefault &&
                                      ArrowLeft.IsDefault &&
                                      ArrowRight.IsDefault &&
                                      ArrowUp.IsDefault &&
                                      ArrowDown.IsDefault &&
                                      DropDown.IsDefault &&
                                      PinVertical.IsDefault &&
                                      PinHorizontal.IsDefault &&
                                      FormClose.IsDefault &&
                                      FormMax.IsDefault &&
                                      FormMin.IsDefault &&
                                      FormRestore.IsDefault &&
                                      FormHelp.IsDefault &&
                                      PendantClose.IsDefault &&
                                      PendantMin.IsDefault &&
                                      PendantRestore.IsDefault &&
                                      WorkspaceMaximize.IsDefault &&
                                      WorkspaceRestore.IsDefault &&
                                      RibbonMinimize.IsDefault &&
                                      RibbonExpand.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        Generic.PopulateFromBase(PaletteButtonSpecStyle.Generic);
        Close.PopulateFromBase(PaletteButtonSpecStyle.Close);
        Context.PopulateFromBase(PaletteButtonSpecStyle.Context);
        Next.PopulateFromBase(PaletteButtonSpecStyle.Next);
        Previous.PopulateFromBase(PaletteButtonSpecStyle.Previous);
        ArrowLeft.PopulateFromBase(PaletteButtonSpecStyle.ArrowLeft);
        ArrowRight.PopulateFromBase(PaletteButtonSpecStyle.ArrowRight);
        ArrowUp.PopulateFromBase(PaletteButtonSpecStyle.ArrowUp);
        ArrowDown.PopulateFromBase(PaletteButtonSpecStyle.ArrowDown);
        DropDown.PopulateFromBase(PaletteButtonSpecStyle.DropDown);
        PinVertical.PopulateFromBase(PaletteButtonSpecStyle.PinVertical);
        PinHorizontal.PopulateFromBase(PaletteButtonSpecStyle.PinHorizontal);
        FormClose.PopulateFromBase(PaletteButtonSpecStyle.FormClose);
        FormMax.PopulateFromBase(PaletteButtonSpecStyle.FormMax);
        FormMin.PopulateFromBase(PaletteButtonSpecStyle.FormMin);
        FormRestore.PopulateFromBase(PaletteButtonSpecStyle.FormRestore);
        FormHelp.PopulateFromBase(PaletteButtonSpecStyle.FormRestore);
        PendantClose.PopulateFromBase(PaletteButtonSpecStyle.PendantClose);
        PendantRestore.PopulateFromBase(PaletteButtonSpecStyle.PendantRestore);
        PendantMin.PopulateFromBase(PaletteButtonSpecStyle.PendantMin);
        PendantRestore.PopulateFromBase(PaletteButtonSpecStyle.PendantRestore);
        WorkspaceMaximize.PopulateFromBase(PaletteButtonSpecStyle.WorkspaceMaximize);
        WorkspaceRestore.PopulateFromBase(PaletteButtonSpecStyle.WorkspaceRestore);
        RibbonMinimize.PopulateFromBase(PaletteButtonSpecStyle.RibbonMinimize);
        RibbonExpand.PopulateFromBase(PaletteButtonSpecStyle.RibbonExpand);
    }
    #endregion

    #region Common
    /// <summary>
    /// Gets access to the common button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped Common { get; }

    private bool ShouldSerializeCommon() => !Common.IsDefault;

    #endregion

    #region Generic
    /// <summary>
    /// Gets access to the generic button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining generic button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped Generic { get; }

    private bool ShouldSerializeGeneric() => !Generic.IsDefault;

    #endregion

    #region Close
    /// <summary>
    /// Gets access to the close button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining close button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped Close { get; }

    private bool ShouldSerializeClose() => !Close.IsDefault;

    #endregion

    #region Context
    /// <summary>
    /// Gets access to the context button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped Context { get; }

    private bool ShouldSerializeContext() => !Context.IsDefault;

    #endregion

    #region Next
    /// <summary>
    /// Gets access to the next button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining next button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped Next { get; }

    private bool ShouldSerializeNext() => !Next.IsDefault;

    #endregion

    #region Previous
    /// <summary>
    /// Gets access to the previous button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining previous button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped Previous { get; }

    private bool ShouldSerializePrevious() => !Previous.IsDefault;

    #endregion

    #region ArrowLeft
    /// <summary>
    /// Gets access to the left arrow button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining left arrow button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped ArrowLeft { get; }

    private bool ShouldSerializeArrowLeft() => !ArrowLeft.IsDefault;

    #endregion

    #region ArrowRight
    /// <summary>
    /// Gets access to the right arrow button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining right arrow button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped ArrowRight { get; }

    private bool ShouldSerializeArrowRight() => !ArrowRight.IsDefault;

    #endregion

    #region ArrowUp
    /// <summary>
    /// Gets access to the right up button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining up arrow button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped ArrowUp { get; }

    private bool ShouldSerializeArrowUp() => !ArrowUp.IsDefault;

    #endregion

    #region ArrowDown
    /// <summary>
    /// Gets access to the right up button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining up arrow button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped ArrowDown { get; }

    private bool ShouldSerializeArrowDown() => !ArrowDown.IsDefault;

    #endregion

    #region DropDown
    /// <summary>
    /// Gets access to the drop-down button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining drop-down button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped DropDown { get; }

    private bool ShouldSerializeDropDown() => !DropDown.IsDefault;

    #endregion

    #region PinVertical
    /// <summary>
    /// Gets access to the pin vertical button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pin vertical button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped PinVertical { get; }

    private bool ShouldSerializePinVertical() => !PinVertical.IsDefault;

    #endregion

    #region PinHorizontal
    /// <summary>
    /// Gets access to the pin horizontal button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pin horizontal button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped PinHorizontal { get; }

    private bool ShouldSerializePinHorizontal() => !PinHorizontal.IsDefault;

    #endregion

    #region FormClose
    /// <summary>
    /// Gets access to the form close button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining form close button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped FormClose { get; }

    private bool ShouldSerializeFormClose() => !FormClose.IsDefault;

    #endregion

    #region FormMin
    /// <summary>
    /// Gets access to the form minimize button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining form minimize button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped FormMin { get; }

    private bool ShouldSerializeFormMin() => !FormMin.IsDefault;

    #endregion

    #region FormMax
    /// <summary>
    /// Gets access to the form maximize button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining form maximize button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped FormMax { get; }

    private bool ShouldSerializeFormMax() => !FormMax.IsDefault;

    #endregion

    #region FormRestore
    /// <summary>
    /// Gets access to the form restore button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining form restore button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped FormRestore { get; }

    private bool ShouldSerializeFormRestore() => !FormRestore.IsDefault;

    #endregion

    #region FormHelp
    /// <summary>
    /// Gets access to the form "Help" button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining form 'Help' button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped FormHelp { get; }

    private bool ShouldSerializeFormHelp() => !FormHelp.IsDefault;

    #endregion

    #region PendantClose
    /// <summary>
    /// Gets access to the pendant close button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pendant close button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped PendantClose { get; }

    private bool ShouldSerializePendantClose() => !PendantClose.IsDefault;

    #endregion

    #region PendantMin
    /// <summary>
    /// Gets access to the pendant minimize button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pendant minimize button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped PendantMin { get; }

    private bool ShouldSerializePendantMin() => !PendantMin.IsDefault;

    #endregion

    #region PendantRestore
    /// <summary>
    /// Gets access to the pendant restore button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pendant restore button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped PendantRestore { get; }

    private bool ShouldSerializePendantRestore() => !PendantRestore.IsDefault;

    #endregion

    #region WorkspaceMaximize
    /// <summary>
    /// Gets access to the workspace maximize button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining workspace maximize button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped WorkspaceMaximize { get; }

    private bool ShouldSerializeWorkspaceMaximize() => !WorkspaceMaximize.IsDefault;

    #endregion

    #region WorkspaceRestore
    /// <summary>
    /// Gets access to the workspace restore button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining workspace restore button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped WorkspaceRestore { get; }

    private bool ShouldSerializeWorkspaceRestore() => !WorkspaceRestore.IsDefault;

    #endregion

    #region RibbonMinimize
    /// <summary>
    /// Gets access to the ribbon minimize button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon minimize button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped RibbonMinimize { get; }

    private bool ShouldSerializeRibbonMinimize() => !RibbonMinimize.IsDefault;

    #endregion

    #region RibbonExpand
    /// <summary>
    /// Gets access to the ribbon expand button specification.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon expand button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteButtonSpecTyped RibbonExpand { get; }

    private bool ShouldSerializeRibbonExpand() => !RibbonExpand.IsDefault;

    #endregion

    #region OnButtonSpecChanged
    /// <summary>
    /// Raises the ButtonSpecChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnButtonSpecChanged(object? sender, EventArgs e) => ButtonSpecChanged?.Invoke(this, e);

    #endregion
}