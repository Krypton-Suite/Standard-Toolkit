using System.ComponentModel.Design;
using System.Drawing;

namespace Krypton.Toolkit
{
    internal class KryptonContextMenuActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonContextMenu _contextMenu;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonContextMenuActionList(KryptonContextMenuDesigner owner)
            : base(owner.Component)
        {
            // Remember the context menu instance
            _contextMenu = owner.Component as KryptonContextMenu;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a component instance at design time
            if (_contextMenu != null)
            {
                // Add the list of panel specific actions
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _contextMenu.PaletteMode;

            set
            {
                if (_contextMenu.PaletteMode != value)
                {
                    _service.OnComponentChanged(_contextMenu, null, _contextMenu.PaletteMode, value);
                    _contextMenu.PaletteMode = value;
                }
            }
        }
        #endregion
    }
}
