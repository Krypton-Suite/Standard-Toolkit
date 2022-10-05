namespace Krypton.Toolkit
{
    /// <summary>
    /// Implementation for the fixed navigator buttons.
    /// </summary>
    [TypeConverter(typeof(ButtonSpecFormFixedConverter))]
    public abstract class ButtonSpecFormFixed : ButtonSpec
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecFormFixed class.
        /// </summary>
        /// <param name="form">Reference to owning krypton form.</param>
        /// <param name="fixedStyle">Fixed style to use.</param>
        protected ButtonSpecFormFixed(KryptonForm form,
                                   PaletteButtonSpecStyle fixedStyle)
        {
            Debug.Assert(form != null);

            // Remember back reference to owning navigator.
            KryptonForm = form;

            // Fix the type
            ProtectedType = fixedStyle;
        }      
        #endregion   

        #region AllowComponent
        /// <summary>
        /// Can a component be associated with the view.
        /// </summary>
        public override bool AllowComponent => false;

        #endregion

        #region KryptonForm
        /// <summary>
        /// Gets access to the owning krypton form.
        /// </summary>
        protected KryptonForm KryptonForm { get; }

        #endregion

        #region ButtonSpecType
        /// <summary>
        /// Gets and sets the actual type of the button.
        /// </summary>
        public virtual PaletteButtonSpecStyle ButtonSpecType
        {
            get => ProtectedType;
            set => ProtectedType = value;
        }
        #endregion
    }
}
