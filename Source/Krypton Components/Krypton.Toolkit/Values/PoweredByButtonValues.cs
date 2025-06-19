#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PoweredByButtonValues : GlobalId, INotifyPropertyChanged
    {
        #region Identity

        /// <summary>Initializes a new instance of the <see cref="PoweredByButtonValues" /> class.</summary>
        public PoweredByButtonValues()
        {
            Reset();
        }

        #endregion

        #region Public

        /// <summary>
        /// Gets or sets a value indicating whether to show the change log button.
        /// </summary>
        [Category("Visuals")]
        [Description("Gets or sets a value indicating whether to show the change log button.")]
        [DefaultValue(false)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowChangeLogButton { get; set; }

        /// <summary>
        /// Gets or sets the type of the toolkit.
        /// </summary>
        [Category("Visuals")]
        [Description("Gets or sets the type of the toolkit.")]
        [DefaultValue(ToolkitSupportType.Stable)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ToolkitSupportType ToolkitSupportType { get; set; }

        #endregion

        #region Protected

        public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

        #endregion

        #region IsDefault

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefault => ShowChangeLogButton == false &&
                                 ToolkitSupportType == ToolkitSupportType.Stable;

        #endregion

        #region Implementation

        public void Reset()
        {
            ShowChangeLogButton = false;
            ToolkitSupportType = ToolkitSupportType.Stable;
        }

        #endregion

        #region Event

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanged Implementation

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>Sets the field.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}