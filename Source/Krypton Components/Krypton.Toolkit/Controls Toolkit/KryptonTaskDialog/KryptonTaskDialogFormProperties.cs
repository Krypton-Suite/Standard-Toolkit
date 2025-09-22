#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides centralised access to the KryptonTaksDialog from properties.
/// </summary>
public class KryptonTaskDialogFormProperties
{
    #region Fields
    private KryptonTaskDialogKryptonForm _form;
    private List<KryptonTaskDialogElementBase> _elements;
    #endregion

    #region Types
    public class FormInstance
    {
        #region Fields
        KryptonTaskDialogKryptonForm _form;
        #endregion

        #region Identity
        public FormInstance(KryptonTaskDialogKryptonForm form)
        {
            _form = form;
        }
        #endregion

        #region Public
        /// <summary>
        /// The coordinates of the upper left corner of the dialog relative to the upper left corner of the screen.<br/>
        /// Set StartPosition to Manual and use the Location property to display the dialog at any custom screen position.<br/>
        /// - Note: First set the StartPosition to manual, then set the location.
        /// </summary>
        public Point Location 
        {
            get
            {
                // When the form is visible the form's location is used.
                // When the form is hidden the field value is used so the user
                // can change the position before the form is shown.
                return _form.Visible
                    ? _form.Location
                    : field;
            }

            set
            {
                // When the form is hidden only the position is recorded here.
                // And used to position the window before the form is shown.
                field = value;

                if (_form.Visible)
                {
                    // When the form is visible the window is repositioned directly.
                    // The field value is saved for when the user wants to change the position when the form is hidden.
                    _form.Location = value;
                }
            }
        }

        /// <summary>
        /// Determines the position of the dialog to appear.<br/>
        /// Set StartPosition to Manual and use the Location property to display the dialog at any custom screen position.
        /// - Note: First set the StartPosition to manual, then set the location.
        /// </summary>
        public FormStartPosition StartPosition 
        {
            get;
            set; 
        }

        /// <summary>
        /// <b>(Experimental and might be removed)</b><br/>
        /// Intercepts all mouse and keystrokes to the form.<br/>
        /// Testing if this can be used to display a modeless window that is updated as a 
        /// progres or status dialog to the user while some process is running.
        /// </summary>
        public bool InertForm 
        {
            get => _form.InertForm;
            set => _form.InertForm = value;
        }

        /// <summary>
        /// Disables close by ALT+F4.<br/>
        /// To force the user to respond to the dialog through the buttons set CloseBox to false when using IgnoreAltF4.
        /// </summary>
        public bool IgnoreAltF4 
        {
            get => _form.IgnoreAltF4;
            set => _form.IgnoreAltF4 = value;
        }

        /// <summary>
        /// Toggles the systemmenu button.
        /// </summary>
        public bool ControlBox 
        {
            get => _form.ControlBox;
            set => _form.ControlBox = value;
        }

        /// <summary>
        /// Toggles the close button and system menu option.
        /// </summary>
        public bool CloseBox 
        {
            get => _form.CloseBox;
            set => _form.CloseBox = value;
        }

        /// <summary>
        /// Toggles the minimize button and system menu option.
        /// </summary>
        public bool MinimizeBox 
        {
            get => _form.MinimizeBox;
            set => _form.MinimizeBox = value;
        }

        /// <summary>
        /// Toggles the maximize button and system menu option.
        /// </summary>
        public bool MaximizeBox 
        {
            get => _form.MaximizeBox;
            set => _form.MaximizeBox = value;
        }

        /// <summary>
        /// Display form title, left (near), center or right (far).
        /// </summary>
        public PaletteRelativeAlign FormTitleAlign 
        {
            get => _form.FormTitleAlign;
            set => _form.FormTitleAlign = value;
        }

        /// <summary>
        /// Shows a Taskbar Button when the Dialog is shown.<br/>
        /// Note: <br/>
        /// - Make sure to set the caption, otherwise the taskbar button will only show and icon.<br/>
        /// - Note: ShowInTaskBar only works when used with Show() or Show(owner).<br/>
        /// - ShowDialog is a modal dialog and will not display a taskbar button, even when this is set to true.<br/>
        /// </summary>
        public bool ShowInTaskBar 
        {
            get => _form.ShowInTaskbar;
            set => _form.ShowInTaskbar = value;
        }

        /// <summary>
        /// The title-bar text for the dialog.
        /// </summary>
        public string Text 
        {
            get => _form.Text;
            set => _form.Text = value;
        }

        /// <summary>
        /// Wether the dialog should de displayed as a TopMost window.
        /// </summary>
        public bool TopMost 
        {
            get => _form.TopMost;
            set => _form.TopMost = value;
        }

        /// <summary>
        /// Rounds the window corners
        /// </summary>
        public bool RoundedCorners 
        {
            get => field;

            set
            {
                if (field != value && _form.StateCommon is not null)
                {
                    field = value;
                    _form.StateCommon.Border.Rounding = value ? 10 : -1;
                }
            }
        }

        /// <summary>
        /// KryptonTaskDialog icon.
        /// </summary>
        public Icon? Icon 
        {
            get => _form.Icon;
            set => _form.Icon = value;
        }
        #endregion

        #region Public override
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns>String.Empty</returns>
        public override string ToString()
        {
            return string.Empty;
        }

        public AutoScaleMode AutoScaleMode
        {
            get => _form.AutoScaleMode;
            set => _form.AutoScaleMode = value;
        }
        #endregion
    }

    public class GlobalInstance
    {
        #region Fields
        private List<KryptonTaskDialogElementBase> _elements;
        #endregion

        #region Identity
        public GlobalInstance(List<KryptonTaskDialogElementBase> elements)
        {
            _elements = elements;
        }
        #endregion

        #region Public
        /// <summary>
        /// Sets the BackColor1 property for all elements in the dialog
        /// </summary>
        public Color BackColor1 
        {
            get => field;

            set
            {
                field = value;
                _elements.ForEach(e => e.BackColor1 = value);
            }
        }

        /// <summary>
        /// Sets the BackColor2 property for all elements in the dialog
        /// </summary>
        public Color BackColor2 
        {
            get => field;

            set
            {
                field = value;
                _elements.ForEach(e => e.BackColor2 = value);
            }
        }

        /// <summary>
        /// Sets the ForeColor property for all elements in the dialog that have this property.
        /// </summary>
        public Color ForeColor 
        {
            get => field;

            set
            {
                field = value;
                _elements.ForEach(e => {
                    if (e is IKryptonTaskDialogElementForeColor element)
                    {
                        element.ForeColor = value;
                    }
                });
            }
        }
        #endregion

        #region Public override
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns>String.Empty</returns>
        public override string ToString()
        {
            return string.Empty;
        }
        #endregion
    }
    #endregion

    #region Identity
    public KryptonTaskDialogFormProperties(KryptonTaskDialogKryptonForm kryptonForm, List<KryptonTaskDialogElementBase> elements)
    {
        _form = kryptonForm;
        _elements = elements;
        
        Form = new(_form);
        Globals = new(_elements);

        Form.ShowInTaskBar = true;
        Form.Text = "Krypton Task Dialog";
    }
    #endregion

    /// <summary>
    /// TODO Will be removed in the final version
    /// </summary>
    public KryptonTaskDialogKryptonForm KryptonForm => _form;

    #region Public properties
    /// <summary>
    /// Dialog form properties
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public FormInstance Form { get; }

    /// <summary>
    /// Apply settings to all elements at once.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public GlobalInstance Globals { get; }

    /// <summary>
    /// Indicates if the dialog window is visible.
    /// </summary>
    public bool Visible => _form.Visible;

    /// <summary>
    /// The last dialog result after the window was closed.<br/>
    /// Note: When showing the dialog modeless using Show(...) the result is updated after the window has been closed.
    /// </summary>
    public DialogResult DialogResult 
    {
        get => _form.DialogResult;
    }

    /// <summary>
    /// TODO: Will be removed in the final version
    /// </summary>
    public FormBorderStyle FormBorderStyle 
    {
        get => _form.FormBorderStyle;
        set => _form.FormBorderStyle = value;
    }
    #endregion

    #region Public override
    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public override string ToString()
    {
        return string.Empty;
    }
    #endregion
}