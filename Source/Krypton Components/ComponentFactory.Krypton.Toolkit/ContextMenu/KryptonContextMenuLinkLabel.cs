// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Provide a context menu link label.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuLinkLabel), "ToolboxBitmaps.KryptonLinkLabel.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    public class KryptonContextMenuLinkLabel : KryptonContextMenuItemBase
    {
        #region Instance Fields
        private bool _autoClose;
        private string _text;
        private string _extraText;
        private Image _image;
        private Color _imageTransparentColor;
        private readonly PaletteContentInheritRedirect _stateNormalRedirect;
        private readonly PaletteContentInheritRedirect _stateVisitedRedirect;
        private readonly PaletteContentInheritRedirect _stateNotVisitedRedirect;
        private readonly PaletteContentInheritRedirect _statePressedRedirect;
        private readonly PaletteContentInheritRedirect _stateFocusRedirect;
        private readonly PaletteContentInheritOverride _overrideVisited;
        private readonly PaletteContentInheritOverride _overrideNotVisited;
        private readonly PaletteContentInheritOverride _overridePressed;
        private KryptonCommand _command;
        private LabelStyle _style;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the link label item is clicked.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the link label item is clicked.")]
        public event EventHandler Click;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuLinkLabel class.
        /// </summary>
        public KryptonContextMenuLinkLabel()
            : this("LinkLabel")
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuLinkLabel class.
        /// </summary>
        /// <param name="initialText">Initial text for display.</param>
        public KryptonContextMenuLinkLabel(string initialText)
        {
            // Default fields
            _text = initialText;
            _extraText = string.Empty;
            _image = null;
            _imageTransparentColor = Color.Empty;
            _style = LabelStyle.NormalControl;
            _autoClose = true;
            
            // Create the redirectors
            _stateNormalRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalControl);
            _stateVisitedRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalControl);
            _stateNotVisitedRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalControl);
            _statePressedRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalControl);
            _stateFocusRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalControl);

            // Create the states
            StateNormal = new PaletteContent(_stateNormalRedirect);
            OverrideVisited = new PaletteContent(_stateVisitedRedirect);
            OverrideNotVisited = new PaletteContent(_stateNotVisitedRedirect);
            OverrideFocus = new PaletteContent(_stateFocusRedirect);
            OverridePressed = new PaletteContent(_statePressedRedirect);

            // Override the normal state to implement the underling logic
            LinkBehaviorNormal = new LinkLabelBehaviorInherit(StateNormal, KryptonLinkBehavior.AlwaysUnderline);

            // Create the override handling classes
            _overrideVisited = new PaletteContentInheritOverride(OverrideVisited, LinkBehaviorNormal, PaletteState.LinkVisitedOverride, false);
            _overrideNotVisited = new PaletteContentInheritOverride(OverrideNotVisited, _overrideVisited, PaletteState.LinkNotVisitedOverride, true);
            OverrideFocusNotVisited = new PaletteContentInheritOverride(OverrideFocus, _overrideNotVisited, PaletteState.FocusOverride, false);
            _overridePressed = new PaletteContentInheritOverride(OverridePressed, LinkBehaviorNormal, PaletteState.LinkPressedOverride, true);
            OverridePressedFocus = new PaletteContentInheritOverride(OverrideFocus, _overridePressed, PaletteState.FocusOverride, false);
        }

        /// <summary>
        /// Returns a description of the instance.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return Text;
        }
        #endregion

        #region Public
        /// <summary>
        /// Returns the number of child menu items.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ItemChildCount => 0;

        /// <summary>
        /// Returns the indexed child menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override KryptonContextMenuItemBase this[int index] => null;

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shorcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public override bool ProcessShortcut(Keys keyData)
        {
            return false;
        }

        /// <summary>
        /// Returns a view appropriate for this item based on the object it is inside.
        /// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="parent">Owning object reference.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        /// <returns>ViewBase that is the root of the view hierachy being added.</returns>
        public override ViewBase GenerateView(IContextMenuProvider provider,
                                              object parent,
                                              ViewLayoutStack columns,
                                              bool standardStyle,
                                              bool imageColumn)
        {
            return new ViewDrawMenuLinkLabel(provider, this);
        }

        /// <summary>
        /// Gets and sets the link label style.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Link label style.")]
        [DefaultValue(typeof(LabelStyle), "NormalControl")]
        public LabelStyle LabelStyle
        {
            get => _style;

            set
            {
                if (_style != value)
                {
                    _style = value;
                    SetLinkLabelStyle(_style);
                    OnPropertyChanged(new PropertyChangedEventArgs("LabelStyle"));
                }
            }
        }

        /// <summary>
        /// Gets and sets a value that determines the underline behavior of the link label.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Determines the underline behavior of the link label.")]
        [DefaultValue(typeof(KryptonLinkBehavior), "Always Underline")]
        public KryptonLinkBehavior LinkBehavior
        {
            get => LinkBehaviorNormal.LinkBehavior;

            set 
            {
                if (LinkBehaviorNormal.LinkBehavior != value)
                {
                    LinkBehaviorNormal.LinkBehavior = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LinkBehavior"));
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the label has been visited.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Indicates if the hyperlink has been visited already.")]
        [DefaultValue(false)]
        public bool LinkVisited
        {
            get => _overrideVisited.Apply;
            set
            {
                if (_overrideVisited.Apply != value)
                {
                    _overrideVisited.Apply = value;
                    _overrideNotVisited.Apply = !value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LinkVisited"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if clicking the link label automatically closes the context menu.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates if clicking the link label automatically closes the context menu.")]
        [DefaultValue(true)]
        public bool AutoClose
        {
            get => _autoClose;

            set 
            {
                if (_autoClose != value)
                {
                    _autoClose = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("AutoClose"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the link label text.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Main link label text.")]
        [DefaultValue("LinkLabel")]
        [Localizable(true)]
        public string Text
        {
            get => _text;

            set 
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the link label extra text.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Link label extra text.")]
        [DefaultValue(null)]
        [Localizable(true)]
        public string ExtraText
        {
            get => _extraText;

            set 
            {
                if (_extraText != value)
                {
                    _extraText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ExtraText"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the link label image.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Link label image.")]
        [DefaultValue(null)]
        [Localizable(true)]
        public Image Image
        {
            get => _image;

            set 
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Image"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the link label image color to make transparent.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Link label image color to make transparent.")]
        [Localizable(true)]
        public Color ImageTransparentColor
        {
            get => _imageTransparentColor;

            set 
            {
                if (_imageTransparentColor != value)
                {
                    _imageTransparentColor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageTransparentColor"));
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor()
        {
            return (_imageTransparentColor == null) || !_imageTransparentColor.Equals(Color.Empty);
        }

        /// <summary>
        /// Gets access to the link label normal instance specific appearance values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining link label normal instance specific appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateNormal { get; }

        private bool ShouldSerializeStateNormal()
        {
            return !StateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed link label appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed link label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverridePressed { get; }

        private bool ShouldSerializeOverridePressed()
        {
            return !OverridePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the link label appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining link label appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideFocus { get; }

        private bool ShouldSerializeOverrideFocus()
        {
            return !OverrideFocus.IsDefault;
        }

        /// <summary>
        /// Gets access to normal state modifications when link label has been visited.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for modifying normal state when link label has been visited.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideVisited { get; }

        private bool ShouldSerializeOverrideVisited()
        {
            return !OverrideVisited.IsDefault;
        }

        /// <summary>
        /// Gets access to normal state modifications when link label has not been visited.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for modifying normal state when link label has not been visited.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideNotVisited { get; }

        private bool ShouldSerializeOverrideNotVisited()
        {
            return !OverrideNotVisited.IsDefault;
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Command associated with the menu check box.")]
        [DefaultValue(null)]
        public virtual KryptonCommand KryptonCommand
        {
            get => _command;

            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KryptonCommand"));
                }
            }
        }

        /// <summary>
        /// Generates a Click event for the component.
        /// </summary>
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);

            // If we have an attached command then execute it
            KryptonCommand?.PerformExecute();
        }
        #endregion

        #region Internal
        internal LinkLabelBehaviorInherit LinkBehaviorNormal { get; }

        internal PaletteContentInheritOverride OverrideFocusNotVisited { get; }

        internal PaletteContentInheritOverride OverridePressedFocus { get; }

        internal void SetPaletteRedirect(PaletteRedirect redirector)
        {
            _stateNormalRedirect.SetRedirector(redirector);
            _stateVisitedRedirect.SetRedirector(redirector);
            _stateNotVisitedRedirect.SetRedirector(redirector);
            _statePressedRedirect.SetRedirector(redirector);
            _stateFocusRedirect.SetRedirector(redirector);
        }
        #endregion

        #region Private
        private void SetLinkLabelStyle(LabelStyle style)
        {
            PaletteContentStyle contentStyle = CommonHelper.ContentStyleFromLabelStyle(style);
            _stateNormalRedirect.Style = contentStyle;
            _stateVisitedRedirect.Style = contentStyle;
            _stateNotVisitedRedirect.Style = contentStyle;
            _statePressedRedirect.Style = contentStyle; 
            _stateFocusRedirect.Style = contentStyle;
        }
        #endregion
    }
}
