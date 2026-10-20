#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for issue #4584: bordered <see cref="KryptonForm"/> dialogs must not flash the
/// Windows 11 system title bar when opened with <see cref="Form.ShowDialog()"/>.
/// </summary>
public sealed class Bug4584KryptonFormShowDialogFlickerDemo : KryptonForm
{
	private readonly KryptonWrapLabel _lblInfo;
	private readonly KryptonWrapLabel _lblStatus;

	public Bug4584KryptonFormShowDialogFlickerDemo()
	{
		Text = @"Bug #4584 - ShowDialog title-bar flicker";
		StartPosition = FormStartPosition.CenterScreen;
		Size = new Size(720, 340);
		MinimumSize = new Size(640, 300);

		_lblInfo = new KryptonWrapLabel
		{
			Dock = DockStyle.Top,
			AutoSize = false,
			Height = 140,
			Padding = new Padding(12, 10, 12, 4),
			Text =
				@"How to test issue #4584:" + Environment.NewLine +
				@"1) Click Open Sizable dialog or Open FixedDialog." + Environment.NewLine +
				@"2) Watch the first frame as the dialog appears — there must be no glimpse of the" +
				@" Windows 11 system title bar before Krypton chrome." + Environment.NewLine +
				@"3) Repeat several times and switch themes; the flash must stay gone." + Environment.NewLine +
				@"(Related: #2922 covered FormBorderStyle.None / MDI; this is the bordered ShowDialog path.)"
		};

		_lblStatus = new KryptonWrapLabel
		{
			Dock = DockStyle.Bottom,
			AutoSize = false,
			Height = 40,
			Padding = new Padding(12, 4, 12, 8),
			Text = @"Status: not opened yet."
		};

		var buttonHost = new KryptonPanel
		{
			Dock = DockStyle.Top,
			Height = 48,
			PanelBackStyle = PaletteBackStyle.PanelClient,
			Padding = new Padding(12, 8, 12, 8)
		};

		var flow = new FlowLayoutPanel
		{
			Dock = DockStyle.Fill,
			WrapContents = false,
			AutoSize = false
		};

		var btnSizable = new KryptonButton
		{
			Text = @"Open Sizable dialog",
			AutoSize = true,
			MinimumSize = new Size(160, 28)
		};
		btnSizable.Click += (_, _) => OpenSampleDialog(FormBorderStyle.Sizable);

		var btnFixed = new KryptonButton
		{
			Text = @"Open FixedDialog",
			AutoSize = true,
			MinimumSize = new Size(160, 28)
		};
		btnFixed.Click += (_, _) => OpenSampleDialog(FormBorderStyle.FixedDialog);

		var cmbTheme = new KryptonThemeComboBox { Width = 240 };
		flow.Controls.Add(btnSizable);
		flow.Controls.Add(btnFixed);
		flow.Controls.Add(cmbTheme);
		buttonHost.Controls.Add(flow);

		Controls.Add(_lblStatus);
		Controls.Add(buttonHost);
		Controls.Add(_lblInfo);
	}

	/// <summary>
	/// Opens a sample bordered dialog used to visually check for a system-caption flash.
	/// </summary>
	/// <param name="borderStyle">Border style for the sample dialog.</param>
	public void OpenSampleDialog(FormBorderStyle borderStyle)
	{
		using var dialog = new SampleDialogForm(borderStyle);
		_lblStatus.Text = $@"Status: showing {borderStyle} dialog…";
		dialog.ShowDialog(this);
		_lblStatus.Text = $@"Status: last dialog was {borderStyle} (closed with {dialog.DialogResult}).";
	}

	/// <summary>
	/// Small bordered <see cref="KryptonForm"/> opened via <see cref="Form.ShowDialog"/>.
	/// </summary>
	private sealed class SampleDialogForm : KryptonForm
	{
		public SampleDialogForm(FormBorderStyle borderStyle)
		{
			Text = $@"Sample {borderStyle} dialog (#4584)";
			FormBorderStyle = borderStyle;
			StartPosition = FormStartPosition.CenterParent;
			Size = new Size(420, 220);
			MinimizeBox = borderStyle == FormBorderStyle.Sizable;
			MaximizeBox = borderStyle == FormBorderStyle.Sizable;
			ShowInTaskbar = false;

			var lbl = new KryptonWrapLabel
			{
				Dock = DockStyle.Fill,
				Text =
					@"If you saw the Windows 11 title bar for a frame before this chrome," +
					@" the #4584 flash is still present. Close returns to the host."
			};

			var btnClose = new KryptonButton
			{
				Text = @"Close",
				DialogResult = DialogResult.OK,
				Dock = DockStyle.Bottom,
				Height = 36
			};

			Controls.Add(lbl);
			Controls.Add(btnClose);
			AcceptButton = btnClose;
		}
	}
}
