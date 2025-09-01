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
/// KryptonTaskDialog is a Dialog that resembles the functionality of the Winforms TaskDialog although implemented in a different fashion.<br/>
/// The dialog can be constructed through the individual elements and its properties.
/// </summary>
[ToolboxItem(false)]
public class KryptonTaskDialog : IDisposable
{
    #region Static defaults
    internal static class Defaults
    {
        // Initial Client height and width
        internal const int ClientWidth = 600;
        internal const int ClientHeight = 600;

        // Margins for all panels to align the controls to the edges of the panel
        internal const int PanelLeft = 10;
        internal const int PanelTop = 5;
        internal const int PanelBottom = 5;
        internal const int PanelRight = 10;

        // Default space between components
        internal const int ComponentSpace = 10;
    }
    #endregion

    #region Fields
    // The dialog form
    private KryptonTaskDialogKryptonForm _form;
    // TableLayoutPanel to arrange the elements
    private TableLayoutPanel _tableLayoutPanel;
    // List of all elements present in the KTD
    private List<KryptonTaskDialogElementBase> _elements;
    // Form ClientSize with initial values
    private Rectangle _clientRectangle;
    private bool _disposed;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor
    /// </summary>
    public KryptonTaskDialog()
    {
        _disposed = false;
        _elements = [];
        _form = new();
        _clientRectangle = new(0, 0, KryptonTaskDialog.Defaults.ClientWidth, KryptonTaskDialog.Defaults.ClientHeight);

        // Instantiate the Dialog form properties property
        // Before this _form most be instantiated.
        Dialog = new KryptonTaskDialogFormProperties(_form, _elements);

        // Instantiante all elements
        Heading = new KryptonTaskDialogElementHeading();
        Heading.VisibleChanged += UpdateFormSizing;
        
        Content = new KryptonTaskDialogElementContent();
        Content.VisibleChanged += UpdateFormSizing;
        Content.SizeChanged += UpdateFormSizing;

        Expander = new KryptonTaskDialogElementContent();
        Expander.VisibleChanged += UpdateFormSizing;
        Expander.SizeChanged += UpdateFormSizing;

        CommonButtons = new KryptonTaskDialogElementCommonButtons(_form);
        CommonButtons.VisibleChanged += UpdateFormSizing;

        FooterBar = new KryptonTaskDialogElementFooterBar(Expander);
        FooterBar.VisibleChanged += UpdateFormSizing;

        // Add all elements in order of display (top down) to the list
        _elements.Add(Heading);
        _elements.Add(Content);
        _elements.Add(Expander);
        _elements.Add(CommonButtons);
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
    /// <summary>
    /// Contains the properties for the dialog Heading<br/>
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementHeading Heading { get; }

    /// <summary>
    /// Contains the properties for the CommonButtons Elements panel, like OK, Cancel etc.
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementCommonButtons CommonButtons { get; }

    /// <summary>
    /// Contains the properties for the Contents panel.<br/>
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementContent Content { get; }

    /// <summary>
    /// Contains the properties for the Expander panel.<br/>
    /// The expander is user controllable through the Footer element by enabling the Expander button.<br/>
    /// There also custom text can be set for the expanded button and collapsed state.
    /// If you do not want to use the panel, set the Visible property to false.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementContent Expander { get; }

    /// <summary>
    /// Contains the properties for Footer element.<br/>
    /// The footer enables the use to control the Expander element.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementFooterBar FooterBar { get; }
    #endregion;

    #region Public methods
    /// <summary>
    /// Show the dialog modeless.<br/>
    /// The DialogResult can be obtained from this instance after the modeless dialog has been closed.
    /// </summary>
    /// <param name="owner">The parent window Handle that launched this dialog.</param>
    public void Show(IWin32Window? owner = null)
    {
        UpdateFormSizing();
        UpdateFormPosition(owner);

        if (owner is not null)
        {
            _form.Show(owner);
        }
        else
        {
            _form.Show();
        }
    }

    /// <summary>
    /// Show as a modal dialog.<br/>
    /// The caller will wait until the dialog has been dismissed.
    /// </summary>
    /// <param name="owner">The parent window Handle that launched this dialog.</param>
    /// <returns>The DialogResult</returns>
    public DialogResult ShowDialog(IWin32Window? owner = null)
    {
        UpdateFormSizing();
        UpdateFormPosition(owner);

        return owner is not null
            ? _form.ShowDialog(owner)
            : _form.ShowDialog();
    }

    /// <summary>
    /// Will close the dialog window.<br/>
    /// Can be use with a modeless dialog that is controlled and updated from code.
    /// </summary>
    public void Close()
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
            result.X = control.Left + (control.Width / 2) - (_form.Width / 2);
            result.Y = control.Top + (control.Height / 2) - (_form.Height / 2);
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
            _form.StartPosition = Dialog.Form.StartPosition;
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
                    // Since owner is null we will default to center screen
                    case FormStartPosition.CenterParent:
                        _form.StartPosition = FormStartPosition.Manual;
                        _form.Location = GetLocationCenterParent(owner);
                        break;

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
        return  _elements.Count > 0
            ? _elements.Sum(x => x.Visible ? x.Height : 0)
            : 0;
    }

    /// <summary>
    /// Update the form's client height so the elements precisely fit the form.
    /// </summary>
    private void UpdateFormSizing()
    {
        _form.ClientSize = new Size(_form.ClientSize.Width, GetVisibleElementsHeight());
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
        element.Panel.Width = _clientRectangle.Size.Width;
        element.Panel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
    }

    /// <summary>
    /// Setup the initial form
    /// </summary>
    private void SetupForm()
    {
        // Form behaviour
        _form.ClientSize = _clientRectangle.Size;
        _form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        _form.MaximizeBox = false;
        _form.ControlBox = true;

        SetupTableLayoutPanel();

        // Add the panel to the form
        // and dock it to the top so it can grow and shrink
        _form.Controls.Add(_tableLayoutPanel);
        _tableLayoutPanel.Dock = DockStyle.Top;

        // When the form is done add the elements
        SetupElements();

        // Handle form close
        _form.FormClosing += OnDialogFormClosing;
        _form.ResizeEnd += (s, e) => UpdateFormSizing();
    }

    private void OnDialogFormClosing(object? sender, FormClosingEventArgs e)
    {
        // Always hide the form while the user operates it.
        // Else let it close itself.
        if (_form.Visible && e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            _form.Hide();
        }
    }

    /// <summary>
    /// Setup the TableLayoutPanel
    /// </summary>
    private void SetupTableLayoutPanel()
    {
        _tableLayoutPanel = new();
        // keep the size small so auto size can do the rest
        _tableLayoutPanel.Size = new Size(100, 10);
        _tableLayoutPanel.AutoSize = true;
        _tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tableLayoutPanel.Location = new Point(10, 10);
        _tableLayoutPanel.RowCount = 0;
        _tableLayoutPanel.RowStyles.Clear();
        _tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tableLayoutPanel.BackColor = Color.Transparent;

        // 1 column at 100%
        _tableLayoutPanel.ColumnStyles.Clear();
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
    }
    #endregion

    #region IDispose
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            Content.SizeChanged -= UpdateFormSizing;

            _elements.ForEach(x => {
                x.VisibleChanged -= UpdateFormSizing;
                x.Dispose();
            });

            _form.FormClosing -= OnDialogFormClosing;
            _form.ResizeEnd -= (s, e) => UpdateFormSizing();
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
