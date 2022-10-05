namespace Krypton.Toolkit
{
    /// <summary>
    /// Contains a global identifier that is unique among objects.
    /// </summary>
    public class GlobalId
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GlobalId class.
        /// </summary>
        [DebuggerStepThrough]
        public GlobalId() =>
            // Assign the next global identifier
            Id = CommonHelper.NextId;

        #endregion

        #region Id
        /// <summary>
        /// Gets the unique identifier of the object.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Id { get; }

        #endregion
    }
}
