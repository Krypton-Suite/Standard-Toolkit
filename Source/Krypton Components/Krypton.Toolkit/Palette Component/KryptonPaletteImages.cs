using System.ComponentModel;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette image settings.
    /// </summary>
    public class KryptonPaletteImages : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteImages class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteImages(PaletteRedirect redirector,
                                      NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the different image sets
            CheckBox = new KryptonPaletteImagesCheckBox(redirector, needPaint);
            ContextMenu = new KryptonPaletteImagesContextMenu(redirector, needPaint);
            DropDownButton = new KryptonPaletteImagesDropDownButton(redirector, needPaint);
            GalleryButtons = new KryptonPaletteImagesGalleryButtons(redirector, needPaint);
            RadioButton = new KryptonPaletteImagesRadioButton(redirector, needPaint);
            TreeView = new KryptonPaletteImagesTreeView(redirector, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault => CheckBox.IsDefault &&
                                          ContextMenu.IsDefault &&
                                          DropDownButton.IsDefault &&
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
            DropDownButton.PopulateFromBase();
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
        [Category("Visuals")]
        [Description("Overrides for defining check box images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesCheckBox CheckBox { get; }

        private bool ShouldSerializeCheckBox()
        {
            return !CheckBox.IsDefault;
        }
        #endregion

        #region ContextMenu
        /// <summary>
        /// Gets access to the context menu set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context menu images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesContextMenu ContextMenu { get; }

        private bool ShouldSerializeContextMenu()
        {
            return !ContextMenu.IsDefault;
        }
        #endregion

        #region DropDownButton
        /// <summary>
        /// Gets access to the drop down button set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining drop down button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesDropDownButton DropDownButton { get; }

        private bool ShouldSerializeDropDownButton()
        {
            return !DropDownButton.IsDefault;
        }
        #endregion

        #region CheckBox
        /// <summary>
        /// Gets access to the gallery button images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining gallery button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesGalleryButtons GalleryButtons { get; }

        private bool ShouldSerializeGalleryButtons()
        {
            return !GalleryButtons.IsDefault;
        }
        #endregion

        #region RadioButton
        /// <summary>
        /// Gets access to the radio button set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining radio button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesRadioButton RadioButton { get; }

        private bool ShouldSerializeRadioButton()
        {
            return !RadioButton.IsDefault;
        }
        #endregion

        #region TreeView
        /// <summary>
        /// Gets access to the tree view set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tree view images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesTreeView TreeView { get; }

        private bool ShouldSerializeTreeView()
        {
            return !TreeView.IsDefault;
        }
        #endregion
    }
}
