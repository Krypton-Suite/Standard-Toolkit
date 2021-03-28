namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that InputControl values appear as neat text at design time.
    /// </summary>
    internal class InputControlStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InputControlStyleConverter clas.
        /// </summary>
        public InputControlStyleConverter()
            : base(typeof(InputControlStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(InputControlStyle.Standalone, "Standalone"),
            new Pair(InputControlStyle.Ribbon,     "Ribbon"),
            new Pair(InputControlStyle.Custom1,    "Custom1"),
            new Pair(InputControlStyle.Custom2,    "Custom2"),
            new Pair(InputControlStyle.Custom3,    "Custom3")
        };

        #endregion
    }
}
