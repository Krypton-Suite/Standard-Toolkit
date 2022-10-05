namespace Krypton.Toolkit
{
    internal class PlacementModeConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PlacementMode.Absolute, "Placement Mode - Absolute"),
            new(PlacementMode.AbsolutePoint, "Placement Mode - Absolute Point"),
            new(PlacementMode.Bottom, "Placement Mode - Bottom"),
            new(PlacementMode.Center, "Placement Mode - Center"),
            new(PlacementMode.Left, "Placement Mode - Left"),
            new(PlacementMode.Mouse, "Placement Mode - Mouse"),
            new(PlacementMode.MousePoint, "Placement Mode - Mouse Point"),
            new(PlacementMode.Relative, "Placement Mode - Relative"),
            new(PlacementMode.RelativePoint, "Placement Mode - Relative Point"),
            new(PlacementMode.Right, "Placement Mode - Right"),
            new(PlacementMode.Top, "Placement Mode - Top")
        };

        #endregion

        #region Identity

        public PlacementModeConverter() : base(typeof(PlacementMode))
        {
        }

        #endregion

        #region Protected

        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
