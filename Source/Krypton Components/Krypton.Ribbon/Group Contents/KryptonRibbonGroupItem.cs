#region BSD License
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
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Represents the base class for all ribbon group items.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public abstract class KryptonRibbonGroupItem : Component,
                                                   IRibbonGroupItem,
                                                   IBindableComponent
    {
        #region Instance Fields
        private object _tag;

        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupItem class.
        /// </summary>
        public KryptonRibbonGroupItem()
        {
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbon Ribbon { get; set; }

        /// <summary>
        /// Gets access to the owning ribbon tab.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbonTab RibbonTab { get; set; }

        /// <summary>
        /// Gets and sets the owning ribbon container instance.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbonGroupContainer RibbonContainer { get; set; }

        /// <summary>
        /// Gets the visible state of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract bool Visible { get; set; }

        /// <summary>
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract GroupItemSize ItemSizeMaximum { get; set; }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract GroupItemSize ItemSizeMinimum { get; set; }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract GroupItemSize ItemSizeCurrent { get; set; }

        /// <summary>
        /// Return the spacing gap between the provided previous item and this item.
        /// </summary>
        /// <param name="previousItem">Previous item.</param>
        /// <returns>Pixel gap between previous item and this item.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual int ItemGap(IRibbonGroupItem previousItem)
        {
            // If the previous item is a group button cluster then we want 3 pixels
            return previousItem is KryptonRibbonGroupCluster ? 3 : 1;

            // By default we just want a single pixel gap
        }

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract ViewBase CreateView(KryptonRibbon ribbon, NeedPaintHandler needPaint);

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
        /// Get a value indicating if all parent containers are visible.
        /// </summary>
        protected bool ChainVisible
        {
            get
            {
                KryptonRibbonGroupContainer parent = RibbonContainer;

                // Search up chain until we find the top
                while (parent != null)
                {
                    // If any parent is not visible, then abort
                    if (!parent.Visible)
                    {
                        return false;
                    }

                    // Move up a level
                    parent = parent.RibbonContainer;
                }

                return true;
            }
        }
        #endregion

        #region Internal
        internal abstract bool ProcessCmdKey(ref Message msg, Keys keyData);

        internal virtual Image InternalToolTipImage => null;

        internal virtual LabelStyle InternalToolTipStyle => LabelStyle.SuperTip;

        internal virtual Color InternalToolTipImageTransparentColor => Color.Empty;

        internal virtual string InternalToolTipTitle => string.Empty;

        internal virtual string InternalToolTipBody => string.Empty;

        #endregion

        #region IBindableComponent Members
        private BindingContext bindingContext;
        private ControlBindingsCollection dataBindings;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public BindingContext BindingContext
        {
            get => bindingContext ?? (bindingContext = new BindingContext());
            set => bindingContext = value;
        }

        /// <summary>
        ///     Retrieves the bindings for this control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(@"Data")]
        [Description(@"ControlBindings")]
        [RefreshProperties(RefreshProperties.All)]
        [ParenthesizePropertyName(true)]
        public ControlBindingsCollection DataBindings => dataBindings ?? (dataBindings = new ControlBindingsCollection(this));

        #endregion
    }
    }
