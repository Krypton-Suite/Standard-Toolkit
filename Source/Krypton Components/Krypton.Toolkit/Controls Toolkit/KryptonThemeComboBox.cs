#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
	/// <summary>Allows the user to change themes using a <see cref="KryptonComboBox"/>.</summary>
	/// <seealso cref="KryptonComboBox" />
	public class KryptonThemeComboBox : KryptonComboBox
	{
		#region Instance Fields

		private bool _reportSelectedThemeIndex;

		private int _selectedIndex;

		private readonly int? _defaultPaletteIndex = GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX;

		private PaletteMode _defaultPalette;

		private KryptonManager? _manager;

		#endregion

		#region Public

		/// <summary>Gets or sets a value indicating whether [report selected theme index].</summary>
		/// <value><c>true</c> if [report selected theme index]; otherwise, <c>false</c>.</value>
		public bool ReportSelectedThemeIndex
		{
			get => _reportSelectedThemeIndex;

			set => _reportSelectedThemeIndex = value;
		}

		/// <summary>Gets or sets the default palette mode.</summary>
		/// <value>The default palette mode.</value>
		[Category(@"Visuals")]
		[Description(@"The default palette mode.")]
		[DefaultValue(PaletteMode.Microsoft365Blue)]
		public PaletteMode DefaultPalette
		{
			get => _defaultPalette;

			set
			{
				_defaultPalette = value;

				UpdateDefaultPaletteIndex(value);
			}
		}

		/// <summary>
		/// Gets and sets the ThemeSelectedIndex.
		/// </summary>
		[Category(@"Visuals")]
		[Description(@"Theme Selected Index. (Default = `Office 365 - Blue`)")]
		[DefaultValue((int)PaletteMode.Microsoft365Blue)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ThemeSelectedIndex
		{
			get => _selectedIndex = _defaultPaletteIndex ?? 30;

			private set => _selectedIndex = SelectedIndex = value;
		}

		private void ResetThemeSelectedIndex() => _selectedIndex = _defaultPaletteIndex ?? 30;

		private bool ShouldSerializeThemeSelectedIndex() => _selectedIndex != _defaultPaletteIndex;

		/// <summary>
		/// Gets and sets the ThemeSelectedIndex.
		/// </summary>
		[Category(@"Visuals")]
		[Description(@"Custom Theme to use when `Custom` is selected")]
		[DefaultValue(null)]
		public KryptonCustomPaletteBase? KryptonCustomPalette { get; set; }

		[Category(@"Data")]
		[Description(@"")]
		[DefaultValue(null)]
		public KryptonManager? Manager
		{
			get => _manager;

			set => _manager = value;
		}

		#endregion

		#region Identity

		/// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
		public KryptonThemeComboBox()
		{
			_reportSelectedThemeIndex = false;

			_manager = new KryptonManager();

			DropDownStyle = ComboBoxStyle.DropDownList;
			foreach (var kvp in PaletteModeStrings.SupportedThemesMap)
			{
				Items.Add(kvp.Key);
			}
			Text = ThemeManager.ReturnPaletteModeAsString(PaletteMode.Microsoft365Blue);
			_selectedIndex = SelectedIndex = _defaultPaletteIndex ?? GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX;

			_defaultPalette = PaletteMode.Microsoft365Blue;

			Debug.Assert(_selectedIndex == _defaultPaletteIndex, $@"Microsoft365Blue needs to be at the index position of {_defaultPaletteIndex} for backward compatibility");
		}
		#endregion

		#region Implementation

		private void UpdateDefaultPaletteIndex(PaletteMode mode) => ThemeSelectedIndex = (int)mode + 1;

		/// <summary>Returns the palette mode.</summary>
		/// <returns>
		///   <br />
		/// </returns>
		public PaletteMode ReturnPaletteMode() => Manager!.GlobalPaletteMode;

		// TODO: Refresh the theme names if the values have been altered

		#endregion

		#region Protected Overrides

		/// <inheritdoc />
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			SelectedIndex = _selectedIndex;
		}

		/// <inheritdoc />
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			ThemeManager.ApplyTheme(Text!, Manager!);

			ThemeSelectedIndex = SelectedIndex;

			base.OnSelectedIndexChanged(e);
			if ((ThemeManager.GetThemeManagerMode(Text!) == PaletteMode.Custom)
				&& (KryptonCustomPalette != null)
			   )
			{
				Manager!.GlobalCustomPalette = KryptonCustomPalette;
			}

			if (_reportSelectedThemeIndex)
			{
				KryptonMessageBox.Show($@"The index for '{SelectedItem}' is {SelectedIndex}", @"Theme Index", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.SystemInformation);
			}
		}

		#endregion

		#region Removed Designer Visibility
		/// <summary>
		/// Gets and sets the text associated with the control.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[AllowNull]
		public override string? Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		/// <summary>Gets or sets the format specifier characters that indicate how a value is to be Displayed.</summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string FormatString
		{
			get => base.FormatString;

			set => base.FormatString = value;
		}

		/// <summary>
		/// Gets and sets the appearance and functionality of the KryptonComboBox.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ComboBoxStyle DropDownStyle
		{
			get => base.DropDownStyle;
			set => base.DropDownStyle = value;
		}

		/// <summary>
		/// Gets or sets the items in the KryptonComboBox.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ComboBox.ObjectCollection Items => base.Items;

		/// <summary>Gets or sets the draw mode of the combobox.</summary>
		/// <value>The draw mode of the combobox.</value>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DrawMode DrawMode
		{
			get => base.DrawMode;
			set => base.DrawMode = value;
		}

		/// <summary>
		/// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get => base.AutoCompleteCustomSource;
			set => base.AutoCompleteCustomSource = value;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int SelectedIndex { get => base.SelectedIndex; set => base.SelectedIndex = value; }

		#endregion
	}

}