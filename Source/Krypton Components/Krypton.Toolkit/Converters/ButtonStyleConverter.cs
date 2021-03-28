namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that ButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class ButtonStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(ButtonStyle.Standalone,           "Standalone"),
            new Pair(ButtonStyle.Alternate,            "Alternate"),
            new Pair(ButtonStyle.LowProfile,           "Low Profile"),
            new Pair(ButtonStyle.ButtonSpec,           "ButtonSpec"),
            new Pair(ButtonStyle.BreadCrumb,           "BreadCrumb"),
            new Pair(ButtonStyle.CalendarDay,          "Calendar Day"),
            new Pair(ButtonStyle.Cluster,              "Cluster"),
            new Pair(ButtonStyle.Gallery,              "Gallery"),
            new Pair(ButtonStyle.NavigatorStack,       "Navigator Stack"),
            new Pair(ButtonStyle.NavigatorOverflow,    "Navigator Overflow"),
            new Pair(ButtonStyle.NavigatorMini,        "Navigator Mini"),
            new Pair(ButtonStyle.InputControl,         "Input Control"),
            new Pair(ButtonStyle.ListItem,             "List Item"),
            new Pair(ButtonStyle.Form,                 "Form"),
            new Pair(ButtonStyle.FormClose,            "Form Close"),
            new Pair(ButtonStyle.Command,              "Command"),
            new Pair(ButtonStyle.Custom1,              "Custom1"),
            new Pair(ButtonStyle.Custom2,              "Custom2"),
            new Pair(ButtonStyle.Custom3,              "Custom3") };

        #endregion
    }
}
