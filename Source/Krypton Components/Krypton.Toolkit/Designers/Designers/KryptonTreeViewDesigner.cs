﻿namespace Krypton.Toolkit
{
    internal class KryptonTreeViewDesigner : ControlDesigner
    {
        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            // Let base class do standard stuff
            base.Initialize(component);

            Debug.Assert(component != null);

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;
        }

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                DesignerActionListCollection actionLists = new()
                {

                    // Add the label specific list
                    new KryptonTreeViewActionList(this)
                };

                return actionLists;
            }
        }
        #endregion
    }

    internal class NoneExcludedImageIndexConverter : ImageIndexConverter
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NoneExcludedImageIndexConverter class.
        /// </summary>
        public NoneExcludedImageIndexConverter()
        {
        }
        #endregion

        // Properties
        /// <summary>
        /// Indicates if the the None value should be included in standard values for selection.
        /// </summary>
        protected override bool IncludeNoneAsStandardValue => false;
    }
}
