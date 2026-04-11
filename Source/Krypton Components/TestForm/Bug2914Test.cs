using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm;

public partial class Bug2914Test : KryptonForm
{
    public Bug2914Test()
    {
        InitializeComponent();
    }

	private void Bug2914Test_Load(object sender, EventArgs e)
	{

	}

	// Event handler for kryptonButton1 click
	// Opens the Bug2914Test form as a modal dialog with no title bar text
	// and without the system control buttons (minimize, maximize, close).
	private void kryptonButton1_Click(object sender, EventArgs e)
	{
		// Create a new instance of the Bug2914Test form
		Bug2914Test frm = new Bug2914Test();

		// Center the form on the screen when it is shown
		frm.StartPosition = FormStartPosition.Manual;

		// Explicitly set the location (redundant when using CenterScreen,
		// but kept for consistency or testing purposes)
		frm.Location = new Point(0, 0);

		// Remove the window title text
		frm.Text = string.Empty;

		// Allow the form to be resized
		frm.FormBorderStyle = FormBorderStyle.Sizable;

		// Disable the control box (Close, Minimize, Maximize buttons)
		frm.ControlBox = false;

		// Show the form as a modal dialog
		frm.ShowDialog();
	}

	// Event handler for kryptonButton2 click
	// Closes the current form.
	private void kryptonButton2_Click(object sender, EventArgs e)
	{
		// Close the current window
		this.Close();
	}

	// Event handler for kryptonButton3 click
	// Opens the Bug2914Test form as a modal dialog with a visible title.
	private void kryptonButton3_Click(object sender, EventArgs e)
	{
		// Create a new instance of the Bug2914Test form
		Bug2914Test frm = new Bug2914Test();

		// Center the form on the screen
		frm.StartPosition = FormStartPosition.CenterScreen;

		// Set the initial position of the form
		frm.Location = new Point(0, 0);

		// Set the window title text
		frm.Text = "HOLA";

		// Allow the form to be resized
		frm.FormBorderStyle = FormBorderStyle.Sizable;

		// Show the form as a modal dialog
		frm.ShowDialog();
	}
}