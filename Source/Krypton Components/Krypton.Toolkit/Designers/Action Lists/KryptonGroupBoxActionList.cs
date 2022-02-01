#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    internal class KryptonGroupBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonGroupBox _groupBox;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonGroupBoxDesigner class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonGroupBoxActionList(KryptonGroupBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the control instance
            _groupBox = owner.Component as KryptonGroupBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the group background style.
        /// </summary>
        public PaletteBackStyle GroupBackStyle
        {
            get => _groupBox.GroupBackStyle;

            set
            {
                if (_groupBox.GroupBackStyle != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.GroupBackStyle, value);
                    _groupBox.GroupBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the group border style.
        /// </summary>
        public PaletteBorderStyle GroupBorderStyle
        {
            get => _groupBox.GroupBorderStyle;

            set
            {
                if (_groupBox.GroupBorderStyle != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.GroupBorderStyle, value);
                    _groupBox.GroupBorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the group box label style.
        /// </summary>
        public LabelStyle CaptionStyle
        {
            get => _groupBox.CaptionStyle;

            set
            {
                if (_groupBox.CaptionStyle != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.CaptionStyle, value);
                    _groupBox.CaptionStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the caption edge.
        /// </summary>
        public VisualOrientation CaptionEdge
        {
            get => _groupBox.CaptionEdge;

            set
            {
                if (_groupBox.CaptionEdge != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.CaptionEdge, value);
                    _groupBox.CaptionEdge = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the caption overlap.
        /// </summary>
        public double CaptionOverlap
        {
            get => _groupBox.CaptionOverlap;

            set
            {
                if (_groupBox.CaptionOverlap != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.CaptionOverlap, value);
                    _groupBox.CaptionOverlap = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _groupBox.PaletteMode;

            set
            {
                if (_groupBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.PaletteMode, value);
                    _groupBox.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description
        {
            get => _groupBox.Values.Description;

            set
            {
                if (_groupBox.Values.Description != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.Values.Description, value);

                    _groupBox.Values.Description = value;
                }
            }
        }

        /// <summary>Gets or sets the heading.</summary>
        /// <value>The heading.</value>
        public string Heading
        {
            get => _groupBox.Values.Heading;

            set
            {
                if (_groupBox.Values.Heading != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.Values.Heading, value);

                    _groupBox.Values.Heading = value;
                }
            }
        }

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        public Image Image
        {
            get => _groupBox.Values.Image;

            set
            {
                if (_groupBox.Values.Image != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.Values.Image, value);

                    _groupBox.Values.Image = value;
                }
            }
        }

        /// <summary>Gets or sets the long text font.</summary>
        /// <value>The long text font.</value>
        public Font LongTextFont
        {
            get => _groupBox.StateCommon.Content.LongText.Font;

            set
            {
                if (_groupBox.StateCommon.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.StateCommon.Content.LongText.Font, value);

                    _groupBox.StateCommon.Content.LongText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the short text font.</summary>
        /// <value>The short text font.</value>
        public Font ShortTextFont
        {
            get => _groupBox.StateCommon.Content.ShortText.Font;

            set
            {
                if (_groupBox.StateCommon.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.StateCommon.Content.ShortText.Font, value);

                    _groupBox.StateCommon.Content.ShortText.Font = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new();

            // This can be null when deleting a control instance at design time
            if (_groupBox != null)
            {
                // Add the list of panel specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"GroupBackStyle", @"Back style", @"Appearance", @"Background style"));
                actions.Add(new DesignerActionPropertyItem(@"GroupBorderStyle", @"Border style", @"Appearance", @"Border style"));
                actions.Add(new DesignerActionPropertyItem(@"CaptionStyle", @"Caption style", @"Appearance", @"Caption style"));
                actions.Add(new DesignerActionPropertyItem(@"CaptionEdge", @"Caption edge", @"Appearance", @"Caption edge"));
                actions.Add(new DesignerActionPropertyItem(@"CaptionOverlap", @"Caption overlap", @"Appearance", @"Caption overlap"));
                actions.Add(new DesignerActionPropertyItem(@"ShortTextFont", @"Short Text Font", @"Appearance", @"The short text font."));
                actions.Add(new DesignerActionPropertyItem(@"LongTextFont", @"Long Text Font", @"Appearance", @"The long text font."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
                actions.Add(new DesignerActionHeaderItem(@"Values"));
                actions.Add(new DesignerActionPropertyItem(@"Description", @"Description", @"Values", @"The header description text."));
                actions.Add(new DesignerActionPropertyItem(@"Heading", @"Heading", @"Values", @"The heading text."));
                actions.Add(new DesignerActionPropertyItem(@"Image", @"Image", @"Values", @"The heading image."));
            }

            return actions;
        }
        #endregion
    }
}
