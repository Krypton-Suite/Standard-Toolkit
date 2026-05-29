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
/// Colors associated with menus and tool strip.
/// </summary>
public class KryptonPaletteTMS : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCT class.
    /// </summary>
    /// <param name="palette">Associated palette instance.</param>
    /// <param name="baseKCT">Initial base KCT to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteTMS(PaletteBase palette,
        [DisallowNull] KryptonColorTable baseKCT,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(baseKCT != null);

        // Create actual KCT for storage
        InternalKCT = new KryptonInternalKCT(baseKCT!, palette);

        // Create the set of sub objects that expose the palette properties
        Button = new KryptonPaletteTMSButton(InternalKCT, needPaint);
        Grip = new KryptonPaletteTMSGrip(InternalKCT, needPaint);
        Menu = new KryptonPaletteTMSMenu(InternalKCT, needPaint);
        MenuStrip = new KryptonPaletteTMSMenuStrip(InternalKCT, needPaint);
        Rafting = new KryptonPaletteTMSRafting(InternalKCT, needPaint);
        Separator = new KryptonPaletteTMSSeparator(InternalKCT, needPaint);
        StatusStrip = new KryptonPaletteTMSStatusStrip(InternalKCT, needPaint);
        ToolStrip = new KryptonPaletteTMSToolStrip(InternalKCT, needPaint);
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => InternalKCT.IsDefault &&
                                      Button.IsDefault &&
                                      Grip.IsDefault &&
                                      Menu.IsDefault &&
                                      Rafting.IsDefault &&
                                      MenuStrip.IsDefault &&
                                      Separator.IsDefault &&
                                      StatusStrip.IsDefault &&
                                      ToolStrip.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        Button.PopulateFromBase();
        Grip.PopulateFromBase();
        Menu.PopulateFromBase();
        Rafting.PopulateFromBase();
        MenuStrip.PopulateFromBase();
        Separator.PopulateFromBase();
        StatusStrip.PopulateFromBase();
        ToolStrip.PopulateFromBase();
        UseRoundedEdges = InternalKCT.UseRoundedEdges;
    }
    #endregion

    #region Button
    /// <summary>
    /// Get access to the button colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"Button specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSButton Button { get; }

    private bool ShouldSerializeButton() => !Button.IsDefault;

    #endregion

    #region Grip
    /// <summary>
    /// Get access to the grip colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"Grip specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSGrip Grip { get; }

    private bool ShouldSerializeGrip() => !Grip.IsDefault;

    #endregion

    #region Menu
    /// <summary>
    /// Get access to the menu colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"Menu specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSMenu Menu { get; }

    private bool ShouldSerializeMenu() => !Menu.IsDefault;

    #endregion

    #region Rafting
    /// <summary>
    /// Get access to the rafting colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"Rafting specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSRafting Rafting { get; }

    private bool ShouldSerializeRafting() => !Rafting.IsDefault;

    #endregion

    #region MenuStrip
    /// <summary>
    /// Get access to the menu strip colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"MenuStrip specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSMenuStrip MenuStrip { get; }

    private bool ShouldSerializeMenuStrip() => !MenuStrip.IsDefault;

    #endregion

    #region Separator
    /// <summary>
    /// Get access to the separator colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"Separator specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSSeparator Separator { get; }

    private bool ShouldSerializeSeparator() => !Separator.IsDefault;

    #endregion

    #region StatusStrip
    /// <summary>
    /// Get access to the status strip colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"StatusStrip specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSStatusStrip StatusStrip { get; }

    private bool ShouldSerializeStatusStrip() => !StatusStrip.IsDefault;

    #endregion

    #region ToolStrip
    /// <summary>
    /// Get access to the tool strip colors.
    /// </summary>
    [KryptonPersist]
    [Category(@"ToolMenuStatus")]
    [Description(@"ToolStrip specific colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteTMSToolStrip ToolStrip { get; }

    private bool ShouldSerializeToolStrip() => !ToolStrip.IsDefault;

    #endregion

    #region UseRoundedEdges
    /// <summary>
    /// Gets and sets the use of rounded or square edges when rendering.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Should rendering use rounded or square edges.")]
    [DefaultValue(InheritBool.Inherit)]
    public InheritBool UseRoundedEdges
    {
        get => InternalKCT.InternalUseRoundedEdges;

        set
        {
            InternalKCT.InternalUseRoundedEdges = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the UseRoundedEdges property to its default value.
    /// </summary>
    public void ResetUseRoundedEdges() => UseRoundedEdges = InheritBool.Inherit;
    #endregion

    #region Internal
    public KryptonColorTable BaseKCT
    {
        get => InternalKCT.BaseKCT;
        set => InternalKCT.BaseKCT = value;
    }

    public KryptonInternalKCT InternalKCT { get; }

    #endregion
}