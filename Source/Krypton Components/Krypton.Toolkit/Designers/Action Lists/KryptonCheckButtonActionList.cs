using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    internal class KryptonCheckButtonActionList : KryptonButtonActionList
    {
        #region Instance Fields
        private readonly KryptonCheckButton _checkButton;
        private readonly IComponentChangeService _service;
        private string _action;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonCheckButtonActionList(KryptonCheckButtonDesigner owner)
            : base(owner)
        {
            // Remember the button instance
            _checkButton = owner.Component as KryptonCheckButton;

            // Assuming we were correctly passed an actual component...
            if (_checkButton != null)
            {
                // Get access to the actual Orientation property
                PropertyDescriptor checkedProp = TypeDescriptor.GetProperties(_checkButton)["Checked"];

                // If we succeeded in getting the property
                if (checkedProp != null)
                {
                    // Decide on the next action to take given the current setting
                    _action = (bool)checkedProp.GetValue(_checkButton) ? "Uncheck the button" : "Check the button";
                }
            }

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the checked state.
        /// </summary>
        public bool Checked
        {
            get => _checkButton.Checked;

            set
            {
                if (_checkButton.Checked != value)
                {
                    _service.OnComponentChanged(_checkButton, null, _checkButton.Checked, value);
                    _checkButton.Checked = value;
                }
            }
        }

        /// <summary>Gets or sets the context menu strip.</summary>
        /// <value>The context menu strip.</value>
        public ContextMenuStrip ContextMenuStrip
        {
            get => _checkButton.ContextMenuStrip;

            set
            {
                if (_checkButton.ContextMenuStrip != value)
                {
                    _service.OnComponentChanged(_checkButton, null, _checkButton.ContextMenuStrip, value);

                    _checkButton.ContextMenuStrip = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font ShortTextFont
        {
            get => _checkButton.StateCommon.Content.ShortText.Font;

            set
            {
                if (_checkButton.StateCommon.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_checkButton, null, _checkButton.StateCommon.Content.ShortText.Font, value);

                    _checkButton.StateCommon.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font LongTextFont
        {
            get => _checkButton.StateCommon.Content.LongText.Font;

            set
            {
                if (_checkButton.StateCommon.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_checkButton, null, _checkButton.StateCommon.Content.LongText.Font, value);

                    _checkButton.StateCommon.Content.LongText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(-1)]
        public int CornerRadius
        {
            get => _checkButton.StateCommon.Border.Rounding;

            set
            {
                if (_checkButton.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_checkButton, null, _checkButton.StateCommon.Border.Rounding, value);

                    _checkButton.StateCommon.Border.Rounding = value;
                }
            }
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

            // This can be null when deleting a control instance at design time
            if (_checkButton != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnCheckedClick), "Appearance"));
                actions.Add(new DesignerActionPropertyItem("ButtonStyle", "Style", "Appearance", "Button style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Button orientation"));
                actions.Add(new DesignerActionPropertyItem("ContextMenuStrip", "Context Menu Strip", "Appearance", "The context menu strip for the control."));
                actions.Add(new DesignerActionPropertyItem("ShortTextFont", "Short Text Font", "Appearance", "The short text font."));
                actions.Add(new DesignerActionPropertyItem("LongTextFont", "Long Text Font", "Appearance", "The long text font."));
                actions.Add(new DesignerActionPropertyItem("CornerRadius", "Corner Rounding Radius", "Appearance", "The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Button text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Button extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Button image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion

        #region Implementation
        private void OnCheckedClick(object sender, EventArgs e)
        {
            // Cast to the correct type

            // Double check the source is the expected type
            if (sender is DesignerVerb verb)
            {
                // Decide on the new orientation required
                bool isChecked = verb.Text.Equals("Uncheck the button");

                // Decide on the next action to take given the new setting
                _action = isChecked ? "Uncheck the button" : "Check the button";

                // Get access to the actual Orientation property
                PropertyDescriptor checkedProp = TypeDescriptor.GetProperties(_checkButton)["Checked"];

                // If we succeeded in getting the property
                // Update the actual property with the new value
                checkedProp?.SetValue(_checkButton, !isChecked);

                // Get the user interface service associated with actions

                // If we managed to get it then request it update to reflect new action setting
                if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
                {
                    service.Refresh(_checkButton);
                }
            }
        }
        #endregion
    }
}
