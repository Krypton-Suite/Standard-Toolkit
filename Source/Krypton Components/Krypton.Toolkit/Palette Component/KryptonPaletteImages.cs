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
/// Storage for palette image settings.
/// </summary>
public class KryptonPaletteImages : Storage
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonPaletteImages class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteImages([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the different image sets
        CheckBox = new KryptonPaletteImagesCheckBox(redirector, needPaint);
        ContextMenu = new KryptonPaletteImagesContextMenu(redirector, needPaint);
        GalleryButtons = new KryptonPaletteImagesGalleryButtons(redirector, needPaint);
        RadioButton = new KryptonPaletteImagesRadioButton(redirector, needPaint);
        TreeView = new KryptonPaletteImagesTreeView(redirector, needPaint);
        IntegratedToolBar = new KryptonPaletteImagesIntegratedToolBar(redirector, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => CheckBox.IsDefault &&
                                      ContextMenu.IsDefault &&
                                      GalleryButtons.IsDefault &&
                                      RadioButton.IsDefault &&
                                      TreeView.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        CheckBox.PopulateFromBase();
        ContextMenu.PopulateFromBase();
        GalleryButtons.PopulateFromBase();
        RadioButton.PopulateFromBase();
        TreeView.PopulateFromBase();
    }
    #endregion

    #region CheckBox
    /// <summary>
    /// Gets access to the check box set of images.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining check box images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesCheckBox CheckBox { get; }

    private bool ShouldSerializeCheckBox() => !CheckBox.IsDefault;

    #endregion

    #region ContextMenu
    /// <summary>
    /// Gets access to the context menu set of images.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context menu images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesContextMenu ContextMenu { get; }

    private bool ShouldSerializeContextMenu() => !ContextMenu.IsDefault;

    #endregion

    #region CheckBox
    /// <summary>
    /// Gets access to the gallery button images.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining gallery button images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesGalleryButtons GalleryButtons { get; }

    private bool ShouldSerializeGalleryButtons() => !GalleryButtons.IsDefault;

    #endregion

    #region RadioButton
    /// <summary>
    /// Gets access to the radio button set of images.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining radio button images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesRadioButton RadioButton { get; }

    private bool ShouldSerializeRadioButton() => !RadioButton.IsDefault;

    #endregion

    #region TreeView
    /// <summary>
    /// Gets access to the tree view set of images.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tree view images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesTreeView TreeView { get; }

    private bool ShouldSerializeTreeView() => !TreeView.IsDefault;

    #endregion

    #region IntegratedToolBar

    /// <summary>Gets access the integrated tool bar set of images.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining integrated toolbar images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesIntegratedToolBar IntegratedToolBar { get; }

    private bool ShouldSerializeIntegratedToolBar() => !IntegratedToolBar.IsDefault;

    #endregion
}