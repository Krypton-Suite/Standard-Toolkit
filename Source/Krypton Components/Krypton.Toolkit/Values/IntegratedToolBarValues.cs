#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit
{
    [DesignerCategory(@"code")]
    [ToolboxItem(false)]
    public class IntegratedToolBarValues : Storage
    {
        #region Static Fields

        private const bool DEFAULT_SHOW_NEW_BUTTON = true;
        private const bool DEFAULT_SHOW_OPEN_BUTTON = true;
        private const bool DEFAULT_SHOW_SAVE_BUTTON = true;
        private const bool DEFAULT_SHOW_SAVE_ALL_BUTTON = true;
        private const bool DEFAULT_SHOW_SAVE_AS_BUTTON = true;
        private const bool DEFAULT_SHOW_CUT_BUTTON = true;
        private const bool DEFAULT_SHOW_COPY_BUTTON = true;
        private const bool DEFAULT_SHOW_PASTE_BUTTON = true;
        private const bool DEFAULT_SHOW_UNDO_BUTTON = true;
        private const bool DEFAULT_SHOW_REDO_BUTTON = true;
        private const bool DEFAULT_SHOW_PAGE_SETUP_BUTTON = true;
        private const bool DEFAULT_SHOW_PRINT_PREVIEW_BUTTON = true;
        private const bool DEFAULT_SHOW_PRINT_BUTTON = true;
        private const bool DEFAULT_SHOW_QUICK_PRINT_BUTTON = true;

        #endregion

        #region Instance Fields

        private bool _allowFormIntegration;

        internal ButtonSpecAny[] _integratedToolBarButtons;

        private PaletteButtonOrientation _integratedToolBarButtonOrientation;

        private PaletteRelativeEdgeAlign _integratedToolBarButtonAlignment;

        private KryptonIntegratedToolBarManager _toolbarManager = new KryptonIntegratedToolBarManager();

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [allow form integration].</summary>
        /// <value><c>true</c> if [allow form integration]; otherwise, <c>false</c>.</value>
        /// <exception cref="ArgumentNullException">@"The 'ParentForm' property cannot be null.</exception>
        [Category(@"Visuals"), DefaultValue(false), Description(@"Add/remove the integrated tool bar buttons to the parent form. (Note: Existing buttonspecs will not be affected.)")]
        public bool AllowFormIntegration
        {
            get => _allowFormIntegration;

            set
            {
                _allowFormIntegration = value;

                if (_toolbarManager._parentForm != null)
                {
                    if (value)
                    {
                        _toolbarManager.AttachIntegratedToolBarToParent(_toolbarManager._parentForm);
                    }
                    else
                    {
                        _toolbarManager.DetachIntegratedToolBarFromParent(_toolbarManager._parentForm);
                    }
                }
                else
                {
                    throw new ArgumentNullException($@"The 'ParentForm' property cannot be null.");
                }
            }
        }

        /// <summary>Gets the integrated tool bar buttons.</summary>
        /// <value>The integrated tool bar buttons.</value>
        [Category(@"Visuals"), DefaultValue(null), Description(@"Contains all the integrated tool bar buttons.")]
        public ButtonSpecAny[] IntegratedToolBarButtons => _integratedToolBarButtons;

        /// <summary>Gets or sets the integrated tool bar button orientation.</summary>
        /// <value>The integrated tool bar button orientation.</value>
        [Category(@"Visuals"), DefaultValue(typeof(PaletteButtonOrientation), @"PaletteButtonOrientation.FixedTop"), Description(@"Gets or sets the integrated tool bar button orientation.")]
        public PaletteButtonOrientation IntegratedToolBarButtonOrientation { get => _integratedToolBarButtonOrientation; set { _integratedToolBarButtonOrientation = value; _toolbarManager.UpdateButtonOrientation(value); } }

        /// <summary>Gets or sets the integrated tool bar button alignment.</summary>
        /// <value>The integrated tool bar button alignment.</value>
        [Category(@"Visuals"), DefaultValue(typeof(PaletteRelativeEdgeAlign), @"PaletteRelativeEdgeAlign.Far"), Description(@"Gets or sets the integrated tool bar button alignment.")]
        public PaletteRelativeEdgeAlign IntegratedToolBarButtonAlignment { get => _integratedToolBarButtonAlignment; set { _integratedToolBarButtonAlignment = value; _toolbarManager.UpdateButtonAlignment(value); } }

        #endregion

        #region Identity

        public IntegratedToolBarValues()
        {
            Reset();
        }

        #endregion
        public override bool IsDefault { get; }

        #region Implementation

        public void Reset()
        {
            _allowFormIntegration = false;

            _integratedToolBarButtonOrientation = PaletteButtonOrientation.FixedTop;

            _integratedToolBarButtonAlignment = PaletteRelativeEdgeAlign.Far;
        }

        #endregion
    }
}