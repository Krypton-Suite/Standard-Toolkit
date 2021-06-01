﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.Drawing;
using System.ComponentModel;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Represents a single context definition.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonContext), "ToolboxBitmaps.KryptonRibbonContext.bmp")]
    [DefaultProperty("ContextName")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonRibbonContext : Component
    {
        #region Instance Fields
        private string _contextName;
        private string _contextTitle;
        private Color _contextColor;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonContext class.
        /// </summary>
        public KryptonRibbonContext()
        {
            // Default fields
            _contextName = "Context";
            _contextTitle = "Context Tools";
            _contextColor = Color.Red;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the unique name of the context.
        /// </summary>
        [Category("Appearance")]
        [Description("Unique name of the context.")]
        [DefaultValue("Context")]
        public string ContextName
        {
            get => _contextName;

            set 
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                {
                    value = "Context";
                }

                if (value != _contextName)
                {
                    _contextName = value;
                    OnPropertyChanged("ContextName");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display title for associated contextual tabs.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Display title for associated contextual tabs.")]
        [DefaultValue("Context")]
        public string ContextTitle
        {
            get => _contextTitle;

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                {
                    value = "Context Tools";
                }

                if (value != _contextTitle)
                {
                    _contextTitle = value;
                    OnPropertyChanged("ContextTitle");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display color for associated contextual tabs.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Display color for associated contextual tabs.")]
        [DefaultValue(typeof(Color), "Red")]
        public Color ContextColor
        {
            get => _contextColor;

            set
            {
                // We never allow a null or transparent color
                if ((value == null) || (value == Color.Transparent))
                {
                    value = Color.Red;
                }

                if (value != _contextColor)
                {
                    _contextColor = value;
                    OnPropertyChanged("ContextColor");
                }
            }
        }

        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [Category("Data")]
        [Description("User-defined data associated with the object.")]
        [TypeConverter(typeof(StringConverter))]
        [Bindable(true)]
        public object Tag
        {
            get => _tag;

            set
            {
                if (value != _tag)
                {
                    _tag = value;
                    OnPropertyChanged("Tag");
                }
            }
        }

        private bool ShouldSerializeTag()
        {
            return (Tag != null);
        }

        private void ResetTag()
        {
            Tag = null;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
