#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Service class that provides themed system menu functionality for forms.
    /// This can be composed into any form that needs themed system menu support.
    /// </summary>
    public class KryptonThemedSystemMenuService
    {
        #region Instance Fields
        private readonly Form _form;
        private readonly KryptonThemedSystemMenu _themedSystemMenu;
        private bool _useThemedSystemMenu = true;
        private bool _showOnLeftClick = true;
        private bool _showOnRightClick = true;
        private bool _showOnAltSpace = true;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonThemedSystemMenuService class.
        /// </summary>
        /// <param name="form">The form to attach the themed system menu to.</param>
        public KryptonThemedSystemMenuService(Form form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
            _themedSystemMenu = new KryptonThemedSystemMenu(form);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets and sets a value indicating if the themed system menu is enabled.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Enables or disables the themed system menu that replaces the native Windows system menu.")]
        [DefaultValue(true)]
        public bool UseThemedSystemMenu
        {
            get => _useThemedSystemMenu && _themedSystemMenu.Enabled;
            set
            {
                _useThemedSystemMenu = value;
                _themedSystemMenu.Enabled = value;
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if left-click on title bar shows the themed system menu.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Determines if left-click on title bar shows the themed system menu.")]
        [DefaultValue(true)]
        public bool ShowThemedSystemMenuOnLeftClick
        {
            get => _showOnLeftClick;
            set => _showOnLeftClick = value;
        }

        /// <summary>
        /// Gets and sets a value indicating if right-click on title bar shows the themed system menu.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Determines if right-click on title bar shows the themed system menu.")]
        [DefaultValue(true)]
        public bool ShowThemedSystemMenuOnRightClick
        {
            get => _showOnRightClick;
            set => _showOnRightClick = value;
        }

        /// <summary>
        /// Gets and sets a value indicating if Alt+Space shows the themed system menu.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Determines if Alt+Space shows the themed system menu.")]
        [DefaultValue(true)]
        public bool ShowThemedSystemMenuOnAltSpace
        {
            get => _showOnAltSpace;
            set => _showOnAltSpace = value;
        }

        /// <summary>
        /// Gets access to the themed system menu for advanced customization.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonThemedSystemMenu ThemedSystemMenu => _themedSystemMenu;
        #endregion

        #region Public Methods
        /// <summary>
        /// Handles keyboard shortcuts for system menu actions.
        /// </summary>
        /// <param name="keyData">The key combination pressed.</param>
        /// <returns>True if the shortcut was handled; otherwise false.</returns>
        public bool HandleKeyboardShortcut(Keys keyData)
        {
            if (!UseThemedSystemMenu) return false;

            // Handle Alt+Space to show the themed system menu
            if (ShowThemedSystemMenuOnAltSpace && keyData == (Keys.Alt | Keys.Space))
            {
                _themedSystemMenu.ShowAtFormTopLeft();
                return true;
            }

            // Handle Alt+F4 for close (integrate with themed system menu if enabled)
            if (keyData == (Keys.Alt | Keys.F4))
            {
                return _themedSystemMenu.HandleKeyboardShortcut(keyData);
            }

            return false;
        }

        /// <summary>
        /// Handles right-click in non-client area (title bar).
        /// </summary>
        /// <param name="screenPoint">The screen coordinates of the click.</param>
        /// <param name="isInTitleBarArea">Whether the click is in the title bar area.</param>
        /// <param name="isOnControlButtons">Whether the click is on control buttons.</param>
        /// <returns>True if the event was handled; otherwise false.</returns>
        public bool HandleRightClick(Point screenPoint, bool isInTitleBarArea, bool isOnControlButtons)
        {
            if (!UseThemedSystemMenu || !ShowThemedSystemMenuOnRightClick) return false;

            // Check if the click is in the title bar area (but not on buttons)
            if (isInTitleBarArea && !isOnControlButtons)
            {
                _themedSystemMenu.Show(screenPoint);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Handles left-click in non-client area (title bar).
        /// </summary>
        /// <param name="screenPoint">The screen coordinates of the click.</param>
        /// <param name="isInTitleBarArea">Whether the click is in the title bar area.</param>
        /// <param name="isOnControlButtons">Whether the click is on control buttons.</param>
        /// <returns>True if the event was handled; otherwise false.</returns>
        public bool HandleLeftClick(Point screenPoint, bool isInTitleBarArea, bool isOnControlButtons)
        {
            if (!UseThemedSystemMenu || !ShowThemedSystemMenuOnLeftClick) return false;

            // Only show the menu if clicking in the title bar area (but not on buttons)
            if (isInTitleBarArea && !isOnControlButtons)
            {
                _themedSystemMenu.Show(screenPoint);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Refreshes the themed system menu.
        /// </summary>
        public void Refresh()
        {
            if (UseThemedSystemMenu)
            {
                _themedSystemMenu.Refresh();
            }
        }

        /// <summary>
        /// Disposes of the service and its resources.
        /// </summary>
        public void Dispose()
        {
            _themedSystemMenu?.Dispose();
        }
        #endregion
    }
}
