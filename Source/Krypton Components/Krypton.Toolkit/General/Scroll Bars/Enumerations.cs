namespace Krypton.Toolkit
{
    /// <summary>
    /// The scrollbar arrow button states.
    /// </summary>
    internal enum ScrollBarArrowButtonState
    {
        /// <summary>
        /// Indicates the up arrow is in normal state.
        /// </summary>
        UpNormal,

        /// <summary>
        /// Indicates the up arrow is in hot state.
        /// </summary>
        UpHot,

        /// <summary>
        /// Indicates the up arrow is in active state.
        /// </summary>
        UpActive,

        /// <summary>
        /// Indicates the up arrow is in pressed state.
        /// </summary>
        UpPressed,

        /// <summary>
        /// Indicates the up arrow is in disabled state.
        /// </summary>
        UpDisabled,

        /// <summary>
        /// Indicates the down arrow is in normal state.
        /// </summary>
        DownNormal,

        /// <summary>
        /// Indicates the down arrow is in hot state.
        /// </summary>
        DownHot,

        /// <summary>
        /// Indicates the down arrow is in active state.
        /// </summary>
        DownActive,

        /// <summary>
        /// Indicates the down arrow is in pressed state.
        /// </summary>
        DownPressed,

        /// <summary>
        /// Indicates the down arrow is in disabled state.
        /// </summary>
        DownDisabled
    }

    /// <summary>
    /// Enum for the scrollbar orientation.
    /// </summary>
    public enum ScrollBarOrientation
    {
        /// <summary>
        /// Indicates a horizontal scrollbar.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Indicates a vertical scrollbar.
        /// </summary>
        Vertical
    }

    /// <summary>
    /// The scrollbar states.
    /// </summary>
    internal enum ScrollBarState
    {
        /// <summary>
        /// Indicates a normal scrollbar state.
        /// </summary>
        Normal,

        /// <summary>
        /// Indicates a hot scrollbar state.
        /// </summary>
        Hot,

        /// <summary>
        /// Indicates an active scrollbar state.
        /// </summary>
        Active,

        /// <summary>
        /// Indicates a pressed scrollbar state.
        /// </summary>
        Pressed,

        /// <summary>
        /// Indicates a disabled scrollbar state.
        /// </summary>
        Disabled
    }
}