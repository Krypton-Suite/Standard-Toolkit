namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that ButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class ButtonStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(ButtonStyle.Standalone, "Standalone"),
            new(ButtonStyle.Alternate, "Alternate"),
            new(ButtonStyle.LowProfile, "Low Profile"),
            new(ButtonStyle.ButtonSpec, "ButtonSpec"),
            new(ButtonStyle.BreadCrumb, "BreadCrumb"),
            new(ButtonStyle.CalendarDay, "Calendar Day"),
            new(ButtonStyle.Cluster, "Cluster"),
            new(ButtonStyle.Gallery, "Gallery"),
            new(ButtonStyle.NavigatorStack, "Navigator Stack"),
            new(ButtonStyle.NavigatorOverflow, "Navigator Overflow"),
            new(ButtonStyle.NavigatorMini, "Navigator Mini"),
            new(ButtonStyle.InputControl, "Input Control"),
            new(ButtonStyle.ListItem, "List Item"),
            new(ButtonStyle.Form, "Form"),
            new(ButtonStyle.FormClose, "Form Close"),
            new(ButtonStyle.Command, "Command"),
            new(ButtonStyle.Custom1, "Custom1"),
            new(ButtonStyle.Custom2, "Custom2"),
            new(ButtonStyle.Custom3, "Custom3")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonStyleConverter class.
        /// </summary>
        public ButtonStyleConverter()
            : base(typeof(ButtonStyle))
        {
        }
        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
