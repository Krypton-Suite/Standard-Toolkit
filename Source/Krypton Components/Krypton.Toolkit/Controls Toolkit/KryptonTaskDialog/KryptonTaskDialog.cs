#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// KryptonTaskDialog is a Dialog that resembles the functionality of the Winforms TaskDialog although implemented in a different fashion.<br/>
/// The dialog can be constructed through the individual elements and its properties.
/// </summary>
[ToolboxItem(false)]
public class KryptonTaskDialog : IDisposable
{
    #region Fields
    // The dialog form
    private KryptonTaskDialogKryptonForm _form;
    // TableLayoutPanel to arrange the elements
    private TableLayoutPanel _tableLayoutPanel;
    // List of all elements present in the KTD
    private List<KryptonTaskDialogElementBase> _elements;
    // Form ClientSize with initial values
    private Rectangle _clientRectangle;
    // Defaults
    private KryptonTaskDialogDefaults _taskDialogDefaults;
    // Are we disposed
    private bool _disposed;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor
    /// </summary>
    public KryptonTaskDialog(int dialogWidth = 0)
    {
        if (dialogWidth <= 0)
        {
            dialogWidth = 600;
        }
        _taskDialogDefaults = new(dialogWidth);

        _disposed = false;
        _elements = [];

        _form = new()
        {
            AutoScaleMode = AutoScaleMode.Font
        };

        // Initial form size. The width is fixed upon instantiation. The height given here is tentative.
        _clientRectangle = new(0, 0, _taskDialogDefaults.ClientWidth, _taskDialogDefaults.ClientHeight);

        // Instantiate the Dialog form properties
        Dialog = new KryptonTaskDialogFormProperties(_form, _elements, _taskDialogDefaults);

        // Default to center screen 
        Dialog.Form.StartPosition = FormStartPosition.CenterScreen;

        // Instantiante all elements
        Heading = new KryptonTaskDialogElementHeading(_taskDialogDefaults);
        Content            = new KryptonTaskDialogElementContent(_taskDialogDefaults);
        //ContentTest   = new KryptonTaskDialogElementContentTest((_taskDialogDefaults);
        Expander           = new KryptonTaskDialogElementContent(_taskDialogDefaults);
        RichTextBox        = new KryptonTaskDialogElementRichTextBox(_taskDialogDefaults);
        FreeWheeler1       = new KryptonTaskDialogElementFreeWheeler1(_taskDialogDefaults);
        FreeWheeler2       = new KryptonTaskDialogElementFreeWheeler2(_taskDialogDefaults);
        CommandLinkButtons = new KryptonTaskDialogElementCommandLinkButtons(_taskDialogDefaults);
        CheckBox           = new KryptonTaskDialogElementCheckBox(_taskDialogDefaults);
        HyperLink          = new KryptonTaskDialogElementHyperLink(_taskDialogDefaults);
        ComboBox           = new KryptonTaskDialogElementComboBox(_taskDialogDefaults);
        ProgresBar         = new KryptonTaskDialogElementProgresBar(_taskDialogDefaults);
        FooterBar          = new KryptonTaskDialogElementFooterBar(_taskDialogDefaults, Expander, _form);
        //ElementEmpty       = new KryptonTaskDialogElementEmpty(_taskDialogDefaults);


        // Add all elements in order of display (top down) to the list
        _elements.Add(Heading);
        _elements.Add(Content);
        //_elements.Add(ContentTest);
        _elements.Add(Expander);
        _elements.Add(RichTextBox);
        _elements.Add(FreeWheeler1);
        _elements.Add(FreeWheeler2);

        //_elements.Add(ElementEmpty);

        _elements.Add(CommandLinkButtons);
        _elements.Add(CheckBox);
        _elements.Add(ComboBox);
        _elements.Add(HyperLink);
        _elements.Add(ProgresBar);
        _elements.Add(FooterBar);

        // Initialise the dialog form & sets-up the elements
        SetupForm();
    }
    #endregion

    #region Public Dialog form properties
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogFormProperties Dialog { get; }
    #endregion

    #region Public Element properties
    ///// <summary>
    ///// Contains the properties for the Contents panel.<br/>
    ///// If you do not want to use the panel, set the Visible property to false.
    ///// </summary>
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    //public KryptonTaskDialogElementContentTest ContentTest { get; }

    //[TypeConverter(typeof(ExpandableObjectConverter))]
    //public KryptonTaskDialogElementEmpty ElementEmpty { get; }

    /// <summary>
    /// Contains the properties for the dialog Heading<br/>
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementHeading Heading { get; }

    /// <summary>
    /// Contains the properties for the Contents panel.<br/>
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementContent Content { get; }

    /// <summary>
    /// Contains the properties for the Expander element.<br/>
    /// The expander is user controllable through the Footer element by enabling the Expander button.<br/>
    /// There also custom text can be set for the expanded button and collapsed state.<br/>
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementContent Expander { get; }

    /// <summary>
    /// Contains the properties for the RichTextBox element.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementRichTextBox RichTextBox { get; }

    /// <summary>
    /// The FreeWheeler1 element exposes only a FlowLayoutPanel (and all it's properties) to which you can add and configure your own set of controls.<br/>
    /// Here you can host a choice of controls within KryptonTaskDialog.<br/>
    /// Note: Some controls do not work well with a FlowLayoutPanel, for those use KryptonTaskDialogElementFreeWheeler2 which exposes TableLayoutPanel.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementFreeWheeler1 FreeWheeler1 { get; }

    /// <summary>
    /// The FreeWheeler2 element exposes only a TableLayoutPanel (and all it's properties) to which you can add and configure your own set of controls.<br/>
    /// Here you can host a choice of controls within KryptonTaskDialog.<br/>
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementFreeWheeler2 FreeWheeler2 { get; }

    /// <summary>
    /// Contains the properties for the CommandLinkButtons element.<br/>
    /// This element enables you to add your own instances of KryptonCommandLinkButtons.<br/>
    /// If you want one of these buttons to close the dialog, then set it's DialogResult property.<br/>
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementCommandLinkButtons CommandLinkButtons { get; }

    /// <summary>
    /// Contains the properties for the FooterBar element.<br/>
    /// The footer enables the use to control the Expander element.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementFooterBar FooterBar { get; }

    /// <summary>
    /// Contains the properties for the CheckBox element.<br/>
    /// The footer enables the use to control the Expander element.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementCheckBox CheckBox { get; }

    /// <summary>
    /// Contains the properties for the ComboBox element.<br/>
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementComboBox ComboBox { get; }

    /// <summary>
    /// Contains the properties for the HyperLink element.<br/>
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementHyperLink HyperLink { get; }

    /// <summary>
    /// Contains the properties for the ProgressBar element.<br/>
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementProgresBar ProgresBar{ get; }

    #endregion;

    #region Public methods
    /// <summary>
    /// Use one call to hide all dialog elements from view
    /// </summary>
    public void HideAllElements()
    {
        _elements.ForEach(e => e.Visible = false);
    }

    /// <summary>
    /// Show the dialog modeless.<br/>
    /// The DialogResult can be obtained from this instance after the modeless dialog has been closed.
    /// </summary>
    /// <param name="owner">The parent window that launched this dialog.</param>
    public void Show(IWin32Window? owner = null)
    {
        if (!Dialog.Visible)
        {
            UpdateFormSizing();
            UpdateFormPosition(owner);
            ResetFormDialogResult();

            if (owner is not null)
            {
                _form.Show(owner);
            }
            else
            {
                _form.Show();
            }
        }
        else
        {
            KryptonMessageBox.Show(
                "The form is already visible and Show() cannot be called again.",
                "KryptonTaskDialog",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Exclamation);

            if (!_form.TopMost)
            {
                _form.BringToFront();
            }
        }
    }

    /// <summary>
    /// Show as a modal dialog.<br/>
    /// The caller will wait until the dialog has been dismissed.
    /// </summary>
    /// <param name="owner">The parent window that launched this dialog.</param>
    /// <returns>The DialogResult</returns>
    public DialogResult ShowDialog(IWin32Window? owner = null)
    {
        UpdateFormSizing();
        UpdateFormPosition(owner);
        ResetFormDialogResult();

        // The standard form's DialogResult property always returns Cancel when e.Cancel is set to true.<br/>
        // Before that happens the DialogResult is stored in DialogResultInternal.
        if (owner is not null)
        {
            _form.ShowDialog(owner);
        }
        else
        {
            _form.ShowDialog();
        }

        return Dialog.DialogResult;
    }

    /// <summary>
    /// Will close the dialog window.<br/>
    /// Can be use with a modeless dialog that is controlled and updated from code.
    /// </summary>
    public void CloseDialog()
    {
        if (_form.Visible)
        {
            _form.Hide();
        }
    }
    #endregion

    #region Private
    private Point GetLocationCenterPrimaryScreen()
    {
        return new Point(
            (Screen.PrimaryScreen!.WorkingArea.Width - _form.Width) / 2,
            (Screen.PrimaryScreen.WorkingArea.Height - _form.Height) / 2);
    }

    private Point GetLocationCenterScreen(IWin32Window owner)
    {
        Point result = new(0);

        if (owner is Control control)
        {
            Screen screen = Screen.FromControl(control);
            result.X = (screen.Bounds.Width - _form.Width) / 2;
            result.Y = (screen.Bounds.Height - _form.Height) / 2;
        }

        return result;
    }

    private Point GetLocationCenterParent(IWin32Window owner)
    {
        Point result = new(0);

        if (owner is Control control)
        {
            result.X = control.Left + (control.Width - _form.Width) / 2;
            result.Y = control.Top + (control.Height - _form.Height) / 2;
        }

        return result;
    }

    /// <summary>
    /// Since KryptonTaskDialog reuses it's form, auto positioning needs a hand.
    /// </summary>
    /// <param name="owner">The window that launched the dialog.</param>
    private void UpdateFormPosition(IWin32Window? owner)
    {
        if (Dialog.Form.StartPosition == FormStartPosition.Manual)
        {
            // 1. Change the startposition
            _form.StartPosition = Dialog.Form.StartPosition;
            // 2. Change the location to position the form
            _form.Location = Dialog.Form.Location;
        }
        else if (Screen.PrimaryScreen is not null)
        {
            if (owner is null)
            {
                switch (Dialog.Form.StartPosition)
                {
                    // Since owner is null we will default to center screen
                    case FormStartPosition.CenterParent:
                    case FormStartPosition.CenterScreen:
                        _form.StartPosition = FormStartPosition.Manual;
                        _form.Location = GetLocationCenterPrimaryScreen();
                        break;

                    // FormStartPosition.WindowsDefaultBounds can cause windows to resize the form.
                    // We dont want that and will default to WindowsDefaultLocation.
                    case FormStartPosition.WindowsDefaultLocation:
                    case FormStartPosition.WindowsDefaultBounds:
                        _form.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        break;
                }
            }
            else
            {
                switch (Dialog.Form.StartPosition)
                {
                    // Since owner is not null we will center on the parent
                    case FormStartPosition.CenterParent:
                        _form.StartPosition = FormStartPosition.Manual;
                        _form.Location = GetLocationCenterParent(owner);
                        break;

                    // Center on the display the owner form is on or most of it.
                    case FormStartPosition.CenterScreen:
                        _form.StartPosition = FormStartPosition.Manual;
                        _form.Location = GetLocationCenterScreen(owner);
                        break;

                    // FormStartPosition.WindowsDefaultBounds can cause windows to resize the form.
                    // We dont want that and will default to WindowsDefaultLocation.
                    case FormStartPosition.WindowsDefaultLocation:
                    case FormStartPosition.WindowsDefaultBounds:
                        _form.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Returns the total client height of all elements
    /// </summary>
    /// <returns>The total client height of all elements</returns>
    private int GetVisibleElementsHeight()
    {
        return _elements.Sum(element => 
        {
            // Force an update
            if (element.LayoutDirty)
            {
                element.PerformLayout();
            }

            // Height automatically returns zero when the element will not be visible in the dialog.
            return element.Height;
        });
    }

    /// <summary>
    /// Update the form's client height so the elements precisely fit the form.
    /// </summary>
    private void UpdateFormSizing()
    {
        // Border-Fixes: the filler offset enlarges the client height to compensate for the part
        // of the internal panel that slides under the border.
        _form.ClientSize = new Size(_taskDialogDefaults.ClientWidth, GetVisibleElementsHeight());
    }

    /// <summary>
    /// Processes the elements list.
    /// </summary>
    private void SetupElements()
    {
        _elements.ForEach(e => AddElement(e));
    }

    /// <summary>
    /// Adds each element to the TableLayoutPanel.
    /// </summary>
    /// <param name="element"></param>
    private void AddElement(KryptonTaskDialogElementBase element)
    {
        _tableLayoutPanel.RowCount += 1;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.Controls.Add(element.Panel, 0, _tableLayoutPanel.RowCount - 1);
        
        element.Panel.Top = 0;
        element.Panel.Left = 0;
        element.Panel.Width = _taskDialogDefaults.ClientWidth;
        element.Panel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

        // wire events
        element.VisibleChanged += UpdateFormSizing;

        // wire the SizeChanged event for those implementing it.
        if (element is IKryptonTaskDialogElementEventSizeChanged e)
        {
            e.SizeChanged += UpdateFormSizing;
        }
    }

    /// <summary>
    /// Setup the dialog form
    /// </summary>
    private void SetupForm()
    {
        // Form behaviour
        _form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        _form.Padding = _taskDialogDefaults.NullPadding;
        _form.MaximizeBox = false;
        _form.ControlBox = true;
        _form.SystemMenuValues.Enabled = false;

        SetupTableLayoutPanel();

        // When the form is done add the elements
        SetupElements();

        // Setup is finalized
        UpdateFormSizing();
    }

    private void ResetFormDialogResult()
    {
        _form.DialogResult = DialogResult.None;
        _form.DialogResultInternal = DialogResult.None;
    }

    private void SetupTableLayoutPanel()
    {
        _tableLayoutPanel = new();
        _tableLayoutPanel.SetDoubleBuffered(true);
        _tableLayoutPanel.AutoSize = true;
        _tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        
        _tableLayoutPanel.MinimumSize = new Size(_taskDialogDefaults.ClientWidth, 0);
        _tableLayoutPanel.MaximumSize = new Size(_taskDialogDefaults.ClientWidth, 0);

        _tableLayoutPanel.Padding = _taskDialogDefaults.NullPadding;
        _tableLayoutPanel.Margin = _taskDialogDefaults.NullPadding;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tableLayoutPanel.BackColor = Color.Transparent;

        // 1 column at 100%
        _tableLayoutPanel.ColumnStyles.Clear();
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        
        // Zero rows, those are added per element.
        _tableLayoutPanel.RowCount = 0;
        _tableLayoutPanel.RowStyles.Clear();

        // Add the panel to the form
        // and dock it to the top so it can grow and shrink
        _form.Controls.Add(_tableLayoutPanel);
        _tableLayoutPanel.Dock = DockStyle.Top;
    }
    #endregion

    #region IDispose
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _elements.ForEach(x => 
            {
                x.VisibleChanged -= UpdateFormSizing;

                // unwire the SizeChanged event for those implementing it.
                if (x is IKryptonTaskDialogElementEventSizeChanged e)
                {
                    e.SizeChanged -= UpdateFormSizing;
                }

                x.Dispose();
            });

            _form.Close();
            _form.Dispose();
            _form = null!;

            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
