# <img src="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Krypton.png?raw=true"> Standard Toolkit - ChangeLog

====

## 2025-11-xx - Build 2511 (V10 - alpha) - November 2025
* Implemented [#2377](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2377), Adds the `KryptonDataGridViewRatingColumn`.
* Implemented [#2386](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2386), Fix exceptions in `KryptonTextBox`, `KryptonMaskedTextBox` when disabled
* Implemented [#2384](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2384), Fix TrackBar colors usage in palettes.
* Implemented [#2368](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2368), Fix exception in `PaletteRedirectGrids.GetInheritBack` due to missing `BoldedOverride`.
* Implemented [#2365](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2365), Fix exception in `KryptonCustomPaletteBase.GetPaletteBackGridHeaderColumn` for `BoldedOverride` state.
* Implemented [#2349](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2349), Fix `KryptonListBox` shifting on visible item selection.
* Implemented [#2354](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2354), `KryptonDataGridView.DoubleBuffered` property added.
* Implemented [#2220](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2220), Enables limited support on multiple Krypton Controls for unicode surrogates.
* Implemented [#2339](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2339), Add a emoji parser for future features
* Implemented [#2338](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2338), Update specific pre-processor directives
* Resolved [#2341](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2341), Fix exception in `RenderStandard.ContentFontForButtonForm` during teardown
* Implemented [#2328](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2328), Set the baseline support to .NET Framework 4.7.2
	- **Note:** This is a breaking change, as the minimum supported version of .NET Framework has been raised from 4.6.2 to 4.7.2.
* Resolved [#2329](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2329), `AccurateText.StringFormatToFlags()` performs incorrect conversion to TextFormatFlags.
* Resolved [#2324](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2324), Update `PlatformInvoke.cs` imports (see #2316)
* Resolved [#2318](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2318), `KryptonForm` does not handle the `ControlRemoved` and `ControlAdded` event correctly.
* Resolved [#2319](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2319), Enhance `KryptonContextMenuItem` and `KryptonCommand` functionality
* Resolved [#2178](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2178), Fix `KryptonPropertyGrid` enabling logic for `Reset` menu-item
* Resolved [#2312](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2312), (fix) correct GDI resource handling of components' WmPaint
* Resolved [#2309](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2309), `KryptonDataGridViewImageColumn` causes lagging in grid refresh when a new row is auto added.
* Resolved [#2294](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2294), Added ThemeGen assembly and kptheme CLI app.
* Resolved [#2296](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2296), Fix `Visual Controls' components from showing blank content.
* Resolved [#2299](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2299), Fix memory leak in PaletteBase
* Resolved [#2235](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2235), `OSUtilities` Adds OsVersionInfo to the properties.
* Resolved [#2264](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2264), Implementation of style: `PaletteBackStyle.Control`, `PaletteContentStyle.LabelAlternateControl` and `PaletteContentStyle.LabelAlternatePanel` in all themes
* Resolved [#2200](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2200), `KryptonAutoHiddenSlidePanel.PreFilterMessage` will now use the ActiveFormTracker.
* Implemented [#2251](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2251), Implements the `ActiveFormTracker` static class and integrates the tracker into the `KryptonForm`.
* Resolved [#2260](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2260), `KryptonBreadCrumb` Items designer lacks the cancel button.
* Implemented [#1263](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1263), Is it possible to create custom pre-processor directives
* Implemented [#2231](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2231), Use `filescoped` namespaces
* Resolved [#2049](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2049), BlurValues do not work as expected
* Implemented [#1190](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1190), Enables Windows 11 snap layouts.
* Resolved [#2235](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2235), `OSUtilities` corrects Windows 10 & 11 detection.
* Resolved [#2095](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2095), `KryptonRibbon` `StateNormal` & `StateCommon` do not write changes to `RibbonFileAppTab` to the designer source.
* Implemented [#1009](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1009), Powered by Krypton Toolkit button
* Resolved [#2101](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2101), `KryptonContextMenu` items editor doesn't have a cancel button.
* Resolved [#2213](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2213), `KryptonToolStrip` & `KryptonStatusBar` controls text unreadable on Microsoft 365 White theme.
* Resolved [#2209](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2209), `KryptonDropButton` does process shortcutkey
* Resolved [#2180](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2180), `KryptonTextBox` does not store the TabStop property in the designer source when needed.
* Resolved [#2166](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2166), Form with Krypton Ribbon, when maximized, cuts off the right, left and bottom edges.
* Resolved [#2112](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2112), MdiContainer and KForm
* Implemented [#2164](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2164), `KryptonDataGridView.ColumnCount` when set, now converts basic columns to `KryptonDataGridViewTextBoxColumns` when Autogenation is enabled.
* Implemented [#119](https://github.com/Krypton-Suite/Standard-Toolkit/issues/119), `KryptonCheckedListBox` with `DataSource`, `DisplayMember` and `ValueMember` property
* Implemented [#2134](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2134), Implement a way of getting a way to show keyboard shortcuts
* Resolved [#868](https://github.com/Krypton-Suite/Standard-Toolkit/issues/868), `KryptonForm` Does not route `WM_HELP` request
* Resolved [#2164](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2164), Adjusted `.Width` and adds `.HeaderCell.Alignment`, `.DefaultCellStyle.Alignment`, `.Visible`, `.AutoSizeMode` and `.DefaultCellStyle.Format` in `ReplaceDefaultColumsWithKryptonColumns`
* Resolved [#2138](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2138), NuGet License type is not being detected in projects that use `PackageLicenseExpression`
* Resolved [#2165](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2165), `KryptonPropertyGrid` lacks the `PropertyValueChanged` event handler
* Implemented [#2145](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2145), Support the new `slnx` format
* Resolved [#2035](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2035), Removes the designer attributes from all `KrpytonDataGridView` Columns. Using the default WinForms Column Designers
* Implemented [#2026](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2026), Adds the `KryptonDataGridViewImageColumn` to the `KrpytonDataGridView` column collection
* Implemented [#2026](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2026), Adds the `KryptonDataGridViewProgressColumn` to the `KrpytonDataGridView` column collection
* Resolved [#1832](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1832), `KryptonComboBox` now will always vertically center the inner ComboBox. The `IntegralHeight` property now is true by default.
* Implemented [#1116](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1116), Toggle switch/button
* Implemented [#667](https://github.com/Krypton-Suite/Standard-Toolkit/issues/667), Adds the AutoSize property and functionality to `KryptonNumericUpDown` and `KryptonDomainUpDown`
* Obsolete [#2022](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2022), Remove obsolete method `ThemeManager.SetTheme()`. Use `ThemeManager.ApplyTheme(...)` instead.
* Implemented [#1623](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1623), Settings custom themes only available through ThemeManager from V110 onward. Adds Obsolete warnings so the move can be prepared from V100.
* Resolved [#2053](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2053), `KryptonRichTextBox` Designer Issues
* Resolved [#2047](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2047), `KryptonForm` based MDI child windows do not respond to LayoutMDI calls
* Resolved [#2037](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2037), `KryptonDataGridView` Several bug fixes and improvements to the `KryptonDataGridView` and its components have been made. See this ticket for a complete overview.
* Reverted [#2027](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2027), Reverting `KryptonOutlookGrid` back to Extended Toolkit for another inspection.
* Resolved [#2023](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2023), `KryptonDataGridView` IconSpecs do not get a repaint when changed at run-time.
* Resolved [#2018](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2018), Corrects `KryptonDataGridView` palette switching difficulties.
* Resolved [#561](https://github.com/Krypton-Suite/Standard-Toolkit/issues/561), MenuItem images are not scaled with dpi Awareness
* Resolved [#2010](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2010), `KryptonDataGridView` columns with editing controls do not react to local palette changes.
* Resolved [#2009](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2009), `KryptonCombBox` does not display the `CueHint`
* Resolved [#1287](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1287), `KryptonToast` has "Many" Issues
	- Text areas now have padding
	- Content area has been replaced with a `KryptonRichTextBox`
* Resolved [#1784](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1784), `KryptonDataGridView` Auto generation of columns is not serialized correctly.
* Resolved [#1977](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1977), When a control is set to anchor to the bottom, the control can be stretched beyond the form bottom.
* Resolved [#1964](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1964), `KryptonTreeView` Node crosses are not Dpi Scaled
* Implemented [#1968](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1968), Open up 'ExceptionHandler' for public use
	- To invoke, use `KryptonExceptionHandler`
* Resolved [#560](https://github.com/Krypton-Suite/Standard-Toolkit/issues/560), CheckBox Images are not scaled with dpi Awareness
* Resolved [#565](https://github.com/Krypton-Suite/Standard-Toolkit/issues/565), GroupBox icons are not scaled for dpi awareness
* Resolved [#559](https://github.com/Krypton-Suite/Standard-Toolkit/issues/559), Header group icon is not scaled for dpi awareness
* Resolved [#1946](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1946), ButtonSpecs do not scale anymore!
* Resolved [#1940](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1940), In V95 KContextMenuItem no longer stores the Text value in the designer !!
* Resolved [#1938](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1938), `KryptonTextBox` CueHint Text
* Implemented [#1928](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1928), A proper exception dialog
	- To invoke, use `KryptonExceptionDialog`
* Implemented [#519](https://github.com/Krypton-Suite/Standard-Toolkit/issues/519), `PaletteDataGridViewAll` does not expose the border value of the `private readonly PaletteDouble _background`
* Resolved [#240](https://github.com/Krypton-Suite/Standard-Toolkit/issues/240), **[Breaking Change]** `KryptonRichTextBox` Why is it not possible to have the `ButtonSpecs` aligned to the top of a control
	- `ButtonSpecs` have been removed from the `KryptonRichTextBox`
	- Use another layout to align in the designers
* Resolved [#1905](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1905), `Sparkle` Themes have an issue with the Background
* Resolved [#980](https://github.com/Krypton-Suite/Standard-Toolkit/issues/980), `KryptonDockableNavigator` with pages without `AllowConfigSave` flag are incorrectly saved
* Resolved [#1909](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1909), `KryptonDataGridViewComboBoxCell` displays an empty drop-down list on the first new row.
* Resolved [#1910](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1910), `Workspace Persistence` -> "Save to array" Causes an exception in `Toolkit.XmlHelper.Image.Save`
* Implemented [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117), Is it possible to have the KForm back colour as the KPanel colour
* Resolved [#1900](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1900), Remove Obsolete `KryptonMessageBoxDep` from V100 code base
* Resolved [#1211](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1211), Button 'drop down' arrows should use palette text colour
* Resolved [#1212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1212), **[Breaking Change]** `KColorButton` 'drop-down' arrow should be drawn
	- Create Scaled Drop Glyph and use for colour button and comboDrops
	- Remove the `PaletteRedirectDropDownButton`
	- Remove `KryptonPaletteImagesDropDownButton`
	- **Breaking Change**: Remove `DropDownButtonImages` from designers
* Resolved [#1887](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1887), If `KryptonPropertyGrid` has focus on groupName and loses mouse over, then it is all filled in black
* Resolved [#1878](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1878), `KryptonListView` is missing key events
* Resolved [#1877](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1843), HeaderGroups are 'clipped' after upgrade to 90.24.11.317
* Resolved [#1783](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1843), KForm borders incorrect
* Resolved [#1843](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1843), `ButtonSpec` position is off due to an incorrect padding when style is set to "List Item".
* Resolved [#1842](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1842), `KryptonTextBox` height collapses when the MultiLine property is enabled.
* Implemented [#1184](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1184), A proper `SplashScreen` item
* Implemented [#1236](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1236), Backport `StockIconId` feature
* Support for .NET 10
* Version bump `90.xx.xx.xx` -> `100.xx.xx.xx`

=======

# 2025-06-23 - Build 2506 (Version 95 - Patch 7) - June 2025
* Resolved [#2166](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2166), Form with Krypton Ribbon, when maximized, cuts off the right, left and bottom edges.

====

# 2025-04-21 - Build 2504 (Version 95 - Patch 6) - April 2025
* Resolved [#2112](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2112), MdiContainer and KForm
* Implemented [#2164](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2164), `KryptonDataGridView.ColumnCount` when set, now converts basic columns to `KryptonDataGridViewTextBoxColumns` when Autogeneration is enabled.
* Resolved [#868](https://github.com/Krypton-Suite/Standard-Toolkit/issues/868), `KryptonForm` Does not route `WM_HELP` request
* Resolved [#2138](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2138), NuGet License type is not being detected in projects that use `PackageLicenseExpression`
* Resolved [#2165](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2165), `KryptonPropertyGrid` lacks the `PropertyValueChanged` event handler
* Resolved [#2137](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2137), NuGet version `90.25.2.55` should be `95.25.xx.xxx`
* Resolved [#2035](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2035), Removes the designer attributes from all `KrpytonDataGridView` Columns. Using the default WinForms Column Designers
* Resolved [#2164](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2164), Adjusted `.Width` and adds `.HeaderCell.Alignment`, `.DefaultCellStyle.Alignment`, `.Visible`, `.AutoSizeMode` and `.DefaultCellStyle.Format` in `ReplaceDefaultColumsWithKryptonColumns`.

=======

# 2025-02-25 - Build 2502 (Version 95 - Patch 5) - February 2025
* Implemented [#2125](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2125), Adding NotificationIcon Size in ToastNotification
* Resolved [#1832](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1832), `KryptonComboBox` now will always vertically center the inner ComboBox. The `IntegralHeight` property now defaults to true.
* Resolved [#2108](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2108), `KryptonPropertyGrid` needs to support 'resetting' of values
* Resolved [#2047](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2047), `KryptonForm` based MDI child windows do not respond to LayoutMDI calls
* Resolved [#2037](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2037) `KryptonDataGridView` Several bug fixes and improvements to the `KryptonDataGridView` and its components have been made. See this ticket for a complete overview.
* Resolved [#2023](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2023), `KryptonDataGridView` IconSpecs do not get a repaint when changed at run-time.
* Resolved [#561](https://github.com/Krypton-Suite/Standard-Toolkit/issues/561), MenuItem images are not scaled with dpi Awareness
* Resolved [#2009](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2009), `KryptonCombBox` does not display the `CueHint`
* Resolved [#1784](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1784), `KryptonDataGridView` Auto generation of columns is not serialized correctly.
* Resolved [#1964](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1964), `KryptonTreeView` Node crosses are not Dpi Scaled
* Resolved [#560](https://github.com/Krypton-Suite/Standard-Toolkit/issues/560), CheckBox Images are not scaled with dpi Awareness
* Resolved [#565](https://github.com/Krypton-Suite/Standard-Toolkit/issues/565), GroupBox icons are not scaled for dpi awareness
* Resolved [#559](https://github.com/Krypton-Suite/Standard-Toolkit/issues/559), Header group icon is not scaled for dpi awareness
* Resolved [#1946](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1946), ButtonSpecs do not scale anymore!
* Resolved [#1940](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1940), In V95 KContextMenuItem no longer stores the Text value in the designer !!
* Resolved [#1938](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1938), `KryptonTextBox` CueHint Text
* Implemented [#519](https://github.com/Krypton-Suite/Standard-Toolkit/issues/519), `PaletteDataGridViewAll` does not expose the border value of the `private readonly PaletteDouble _background`
* Resolved [#1905](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1905), `Sparkle` Themes have an issue with the Background
* Resolved [#980](https://github.com/Krypton-Suite/Standard-Toolkit/issues/980), `KryptonDockableNavigator` with pages without `AllowConfigSave` flag are incorrectly saved
* Resolved [#1909](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1909), `KryptonDataGridViewComboBoxCell` displays an empty drop-down list on the first new row.
* Resolved [#1910](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1910), `Workspace Persistence` -> "Save to array" Causes an exception in `Toolkit.XmlHelper.Image.Save`
* Resolved [#1900](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1211), **Deprecate** -Remove `KryptonMessageBoxDep` from V100 code base
* Resolved [#1211](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1211), Button 'drop down' arrows should use palette text colour
* Resolved [#1212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1212), **[Breaking Change]** `KColorButton` 'drop-down' arrow should be drawn
	- Create Scaled Drop Glyph and use for colour button and comboDrops
	- Remove the `PaletteRedirectDropDownButton`
	- Remove `KryptonPaletteImagesDropDownButton`
	- **Breaking Change**: Remove `DropDownButtonImages` from designers
* Resolved [#1887](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1887), If `KryptonPropertyGrid` has focus on groupName and loses mouse over, then it is all filled in black
* Resolved [#1878](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1878), `KryptonListView` is missing key events
* Resolved [#1843](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1843), `ButtonSpec` position is off due to an incorrect padding when style is set to "List Item".
* Resolved [#1865](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1865), Outlook Mockup Error (thanks to [AngeloCresta](https://github.com/AngeloCresta))
* Resolved [#1862](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1862), `VisualPanel.PaintTransparentBackground()` throws a null reference exception
* Resolved [#1399](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1399), Hard coded colour setting removed from the `KryptonRibbonTab`.
* Resolved [#1837](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1837), `KryptonMessageBoxDefaultButton.Button2` doesn't work
* Resolved [#1877](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1843), HeaderGroups are 'clipped' after upgrade to 90.24.11.317
* Resolved [#1783](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1843), KForm borders incorrect
* Resolved [#1807](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1807), `ChromeBorderWidth` and Padding
* Resolved [#1842](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1842), `KryptonTextBox` height collapses when MultiLine is enabled.
* Resolved [#1241](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1241), `KryptonDataGridViewComboBoxColumn` ignores `ValueMember` in data binding.


=======

## 2024-11-12 - Build 2411 (V90) - November 2024
* Resolved [#1787](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1787), Office 2007 & 2010 Silver Dark Mode themes ribbon button tracking colors adjusted.
* Resolved [#1800](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1800), `KryptonDataGridViewComboBoxEditingControl.EditingControlFormattedValue` property is differently implemented.
* Resolved [#66](https://github.com/Krypton-Suite/Standard-Toolkit/issues/66), Cannot Add Ribbon-Buttons-Container (KryptonRibbonGroupTripple) when using .NETCore onwards [Returns error due to abstract class]
* Resolved [#1757](https://github.com/Krypton-Suite/Standard-Toolkit/issues1757), KForm has a thin magenta border after the fix of #1749
* Implemented [#1765](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1765), Colours for `KryptonRibbon` contexts need sorting out
* Resolved [#1715](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1715), Not Implemented Exception thrown for `GetRibbonBackColorStyle` `PaletteOffice2010Base.cs`
* Resolved [#1299](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1299), Ribbon context colours not implemented
* Resolved [#1749](https://github.com/Krypton-Suite/Standard-Toolkit/issues1749), Rounded Form borders have "Triangles" in corners.
* Resolved [#1692](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1692), Resolves `KryptonMessageBoxes` incompatibility between message text and dark themes.
* Implemented [#1734](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1734), Add `ForceDesignerDPIUnaware` option
* Resolved [#1729](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1729), `KryptonCustomPaletteBase` does not implement ##Tracking states
* Resolved [#1693](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1693), `KryptonCustomPaletteBase` Illegal characters in path
* Resolved [#1552](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1552), `KryptonMessageBox` throws an error when using custom theme `Asphalt_v19.xml`.
* Resolved [#1708](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1708). `KryptonButton` crashes program on invalid type cast.
* Resolved [#1706](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1706), Restore: `KryptonComboBox` (On Form) does not respect designers `DropDownWidth` setting
* Resolved [#1704](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1704), Remove properties from `KryptonRichtTextBox`
* Implemented [#1700](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1700), Adds a method to `CommonHelper` which normalizes line breaks within a string, `CommonHelper.NormalizeLineBreaks`.
* Resolved [#1685](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1685), Theme Selectors `KryptonManagerGlobalPaletteChanged` event sometimes gets fired while the control is not fully initialized.
* Resolved [#1689](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1689), MessageBox text is "Hard to read" when using "MS 365 dark theme"
* Resolved [#1672](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1672), `KryptonContextMenuItemBase`: does not have a "Text" access AP
* Resolved [#1686](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1686), `TestForm`: MessageBox "No Close button" is not respected anymore
* Resolved [#1683](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1683), After #1657 `TestForm` forms still have Toolkit Image strings in the designer files
* Resolved [#1657](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1657), What is/does "GenericToolkitImages" supposed to do
* Resolved [#1661](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1661), Office 2007 Dark Blue theme: Form Text is hard to read when app loses focus
* Implemented [#1650](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1650), EditorConfig null masking needs to be "Unhidden"
* Resolved [#822](https://github.com/Krypton-Suite/Standard-Toolkit/issues/822), Unable to make closed auto hidden docked page visible after config reloading (fix courtesy of [dyurshevich](https://github.com/dyurshevich))
* Resolved [#1646](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1646), `KryptonRibbonGroupThemeComboBox` does not react to index changes anymore.
* Resolved [#1633](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1633), `KryptonRibbon` - Clicking the Mini QAT Menu Button causes an exception.
* Resolved [#1624](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1624), Theme Selector controls default to Professional System theme when set to `PaletteMode.Global`. Instead those shoud default to `ThemeManager.DefaultGlobalPalette`.
* Resolved [#1628](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1628), Some themes do not render the "ToolStrip" Correctly
* Implemented [#632](https://github.com/Krypton-Suite/Standard-Toolkit/issues/632), **[Breaking Change]** `KryptonPropertyGrid` should have a customisable back colour.
* Resolved [#1564](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1564), Disabled Button Text in Ribbons is not visible in some themes
* Resolved [#1607](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1607), "MS365 - Black" theme is unreadable
* Resolved [#1581](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1581), **Enhancement** KListview has background problems for disabled view on each "Item" [now with added List and Details Views]
* Resolved/Implemented [#1597](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1597), Use `KryptonUseRTLLayout` to prevent LTR/RTL issues
* Resolved/Implemented [#1601](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1601), Rollback `KryptonPaletteCustomBase` ability to use a single schema
* Resolved [#1593](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1593), KInputBox is stuck in RTL mode
* RollBack [#1584](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1584), Disable the FadeValues property in VisualForm and move the FadeValues class to Extended. It was a V90 feature but is up for further development in V100.
* Resolved [#1573](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1573), KCheckedListbox & KListBox do not respect 'disabled' back colours
* Resolved [#1522](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1522), Declare `ThemeManager.SetTheme()` Obsolete from V100
* Resolved [#371](https://github.com/Krypton-Suite/Standard-Toolkit/issues/371), Office 365 Black theme ribbon needs better colours for disabled etc.
* Resolved [#1522](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1522), Declare `ThemeManager.SetTheme()` Obsolete from V100
* Resolved [#1092](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1092), `KryptonManager.GlobalPaletteMode` property is not updated when a custom theme is assigned.
* Resolved [#1561](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1561), KryptonRibbonGroup Controls remain enabled at runtime when set to disabled in the designer.
* Resolved [#1536](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1536), Build script does not follow same behaviour when 'rebuilding'
* Resolved [#1381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1508), Update `ButtonSpecAny` `ShowDrop` property description.
* Resolved [#1381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1502), Docking Persistence broken since build `##.23.10.303`
* Resolved [#1522](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1522), **[Breaking Change]** Check `ThemeManager` & `KryptonManager` for the use of hard coded theme indexes. See issue for full details.
* Resolved [#239](https://github.com/Krypton-Suite/Standard-Toolkit/issues/239), Toolstrip combo boxes do not have the theme background applied
* Implemented [#1507](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1507), **[Breaking Change]** `KryptonThemeComboBox`, `KryptonThemeListBox` & `KryptonRibbonGroupThemeComboBox`:
	- All controls had their code base updated to one standard.
	- The assignment of themes via an index has been removed from all.
	- The previous has been replaced by assignment per PaletteMode identifier.
	- All controls do now react to theme changes propagated via the KryptonManager. The control will then synchronize the selected item in the list with the newly activated theme.
	- Form designer files or your code using a theme selector control might hold references to these properties: `KryptonManager`, `ReportSelectedThemeIndex`, `ThemeSelectedIndex` & `SynchronizeDropDownWidth`. These can, safely, be removed.
	- The DefaultPalette property is now stored in the designer file and if set, the selected palette wil be loaded when the selector control is instantiated.
	- The DefaultPalette property can also be used to switch palettes from code.
* Resolved [#1502](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1502), Fixes some problems creating workspaces introduced through warnings removal.
* Resolved [#1497](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1497), When pressing ALT to show the Ribbon KeyTips a null reference exception is thrown.
* Resolved [#1462](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1462), TestForm app: KCombobox from main.cs causes a crash
* Resolved [#1414](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1414), `SetDate` API is missing from `KryptonMonthCalendar`
* Resolved [#1138](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1138), BinaryFormatter is deprecated due to possible security risks and will be removed with .NET 9.
* Resolved [#1490](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1490), **[Regression]** Clean get of alpha branches results in multiple build errors
* Resolved [#1489](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1489), **[Regression]** KMessageBox (and "Deprecated") using Error Icon plays the wrong sound
* Resolved [#1461](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1461), Remove designer visibility of MouseDoubleClick and DoubleClick Events for the KryptonComboxBox
* Resolved [#1478](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1478), Wrongly assigned designers to `KryptonListview` and `KryptonProgressBar` corrected. DesignerActionLists code updated.
* Resolved [#1475](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1475), Build Scripts will run when no suitable environment is detected. Add 'BinLog' option to `build-*.cmd`
* Implemented [#1435](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1435), **[Breaking Change]** Take KMB back to the WinForm override (Remove Checkbox etc)
* Implemented [#1432](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1432), Copy `KryptonMessageBox` to `KryptonMessageBoxDep`
* Resolved [#1424](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1424), **[Breaking Change]** `KryptonMessageBox` does not obey tab characters like `MessageBox`
* Resolved [#1381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1381), **[Regression]** Docking Persistence broken since build ##.23.10.303
* Resolved [#1356](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1356), **[Breaking Change]** AppButton colours don't change while switching themes
	- See https://github.com/Krypton-Suite/Standard-Toolkit/issues/1356#issuecomment-2039412890
	- `RibbonAppButton` has become `RibbonFileAppButton`
	- Addition `RibbonFileAppTab` to hold the tab text (Defaults to `File`)
	- Colours for the `FileAppTab` have been moved into the `StateCommon` area
* Resolved [#1301](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1301), **[Regression]** When Maximised - intergrated KryptonRibbon has titlebar issues
* Resolved [#1383](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1383), Closing last Page in undocked page group prevents addition of further Pages via `KryptonDockingManager.AddToWorkspace` (fix courtesy of [stizler](https://github.com/stigzler))
* Resolved [#1336](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1336), **[Regression]** KryptonForm has "Black Line" under Titlebar when maximised
* Resolved [#1370](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1370), **[Regression]** KryptonForm background colour
* **[Breaking Change]:** The `RibbonStrings` options, usually located in `KryptonRibbon` has been moved to `KryptonManager` -> `ToolkitStrings` -> `RibbonStrings`
* Resolved [#1363](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1363), Incorrect usage of storage objects
* Resolved [#1362](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1362), Using today's Alpha and todays alpha Demos: cannot open Outlook Mail Clone Form in the designer
* Tested [#1188](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1188), Duplicate window titles when window maximized
* Resolved [#1362](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1362), **[Regression]** Using todays Alpha and todays alpha Demos: cannot open Outlook Mail Clone Form in the designer
* Tested [#1188](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1188), **[Regression]** Duplicate window titles when window maximized
* Resolved [#1361](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1361), Opening an existing (Or creating a new) RibbonBar creates incorrect designer code for new `ToolBarImages` object(s)
* Implemented [#1355](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1355), Ability to merge `KryptonRibbon`s
	- **Note:** This feature is activated via the `KryptonRibbonMerger` API
* Resolved issue whereby `CustomFormatMinimumColorButtonText` was assigned `null`, therefore flagging `KryptonOutlookGridStrings` as 'modified'
* Resolved [#1351](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1351), **[Regression]** KryptonFolderBrowserDialog display and runtime errors
* Implemented [#1343](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1343), Extend palette to accept `AppButton` colours
* Resolved [#1337](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1337), ViewManager is visible in the designer as a readonly field, when it should be invisible!
* Resolved [#1244](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1244), Should `IsDefault` set to be `internal`
* Implemented [#1329](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1329), Adjust Ribbon colours for tab row
* Resolved [#1322](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1322), Exception at design time When Assigning CustomPalette to PropertyGrid / TreeGrid
* Resolved [#1340](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1340), `KryptonPropertyGrid` Category header text colours
* Resolved [#1331](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1331), Fix white menu text in White themes (2010, 2013, 365); fixes to `KryptonPropertyGrid` and `KryptonThemeComboBox` with regard to theme switching
* Resolved [#1313](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1313), White background in tabs area
* Implement [#1309](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1309), Is it time to bring over `KryptonOutlookGrid`
* Resolved [#1316](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1316), `KryptonCustomPaletteBase` Import fails if XML contains images (fix courtesy of [tobitege](https://github.com/tobitege))
* Resolved [#876](https://github.com/Krypton-Suite/Standard-Toolkit/issues/876), **[Regression]** `Office 365 - Black` does not display text correctly
* Resolved [#1308](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1308), `RibbonAppButton.cs` - **FormCloseBoxVisible**: null reference exception
* Resolved [#1266](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1266), **[Regression]** **[Breaking Change]** Since V 5.400, the QAT button is supposed to perform the close, therefore the Close Form button should not be visible
* Resolved [#313](https://github.com/Krypton-Suite/Standard-Toolkit/issues/313), **[Regression]** `KryptonMessagebox` is not RTL compliant
* Resolved [#1269](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1269), **[Breaking Change]** Remove AllowFormIntegrate to give consistent experience on all supported OS's
* Resolved [#1268](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1268), **[Breaking Change]** Many Krypton Controls have a CornerRoundingRadius that overrides the State#### Node Rounding values. Please remove!
* Resolved [#1245](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1245), Visual Studio do not open Form after Nuget-Package-Update
* Resolved [#1243](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1243), Krypton Navigator - Outlook Full Nav Mode
* Resolved / Implemented [#215](https://github.com/Krypton-Suite/Standard-Toolkit/issues/215), **[Breaking Change]** `KryptonTreeView` Multi Node Select
* Resolved [#1249](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1249), Ribbon Form Bars are no longer Drawn with the theme colouring
* Resolved [#1255](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1255), **[Breaking Change]** Why does `CornerRoundingRadius` override the KRyptonForm StateCommon.Border.Rounding value
* Resolved [#1252](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1252),Using V80, Setting a "Fat" Form border leads to poor layout
* Implemented [#327](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1220), (Name) and other Standard-Properties in KryptonContextMenu Items Editor
* Resolved [#1247](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1247),`VisualMessageBoxForm` Throws Exception when run from Example Code
* Implemented [#1220](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1220), Is it time to bring over `KryptonToast`s
	- [#1237](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1237), Core 'Toast' UI
	- [#1238](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1238), New `KryptonToastManager`
	- [#1239](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1239), Toasts should behave like a `KryptonMessageBox`
	- [#1240](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1240), New fade in/out ability for `KryptonForm`s
		- **Note:** The developer must explicitly enable this feature, as it is turned off by default
	- [#1281](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1281), Implement User Input Types
	- [#1291](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1291), Make `KryptonToastNotification` RTL Aware
	- [#1292](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1292), `KryptonToastNotification`: Add support for border colouring
* Resolved [#238](https://github.com/Krypton-Suite/Standard-Toolkit/issues/238), Dark / light Mode themes do not modify the calendar control background
* Implemented [#139](https://github.com/Krypton-Suite/Standard-Toolkit/issues/139), Themes (via KryptonManager design option) should have option to respect Current Metrics for Form Border widths
* Implemented [#124](https://github.com/Krypton-Suite/Standard-Toolkit/issues/124), **[Breaking Change]** When setting AllowFormChrome = false, then the Form Bar should still be Theme rendered
* Implemented [#1224](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1224), **[Breaking Change]** Move `GlobalPaletteMode` into `GlobalPalette` and rename
* Implemented [#1223](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1223), Move `UseKryptonFileDialogs` to a better designer location
* Implemented [#1222](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1222), Remove `CustomPalette` (Should be part of the palette definition)
* Implemented [#1204](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1204), Build on `KryptonCommandLinkButtons`
	- [#1218](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1218), Default 'arrow' images, depending on OS version
	- [#1217](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1217), Add support for text alignment
	- [#1216](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1216), Add support for fonts
* Resolved [#996](https://github.com/Krypton-Suite/Standard-Toolkit/issues/996), DataGridView ComboBox Adding list over and over
* Resolved [#1207](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1207), Microsoft 365 - Black (Dark Mode) Drop button is not visible
* Resolved [#1206](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1206), **[Breaking Change]** Remove the Font Size (as it is already covered by the actual font !)
* Resolved [#1197](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1197), `KryptonTaskDialog` Footer Images
* Resolved [#1189](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1189), The Context and Next/Previous buttons of the `KryptonDockableNavigator` cannot be used
* Implemented [#1187](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1187), Bring over the `KryptonCommandLinkButtons`
* Resolved [#1176](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1176), KryptonProgressBar: small values escape drawing area
* Resolved [#1169](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1169), Button Spec Krypton Context Menu (Canary)
* Implemented [#1166](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1166), Use a struct to contain `KryptonMessageBox` data
* Implemented [#1161](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1161), A proper about box
* Resolved [#1091](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1091), Krypton File Dialogs Missing Buttons
* Implemented [#1009](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1009), Powered by Krypton Toolkit button
	- Use `KryptonAboutToolkit.Show();` to invoke
* New `KryptonLanguageManager` is now integrated into `KryptonManager` as `ToolkitStrings`
* Removed support for .NET 6 and 7, in accordance with their official release cadences
* Support for .NET 9
* Version bump `85.xx.xx.xx` -> `90.xx.xx.xx`

=======

# 2025-02-25 - Build 2502 (Patch 5) - February 2025
* Resolved [#2108](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2108), `KryptonPropertyGrid` needs to support 'resetting' of values
* Resolved [#561](https://github.com/Krypton-Suite/Standard-Toolkit/issues/561), MenuItem images are not scaled with dpi Awareness
* Resolved [#1964](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1964), `KryptonTreeView` Node crosses are not Dpi Scaled
* Resolved [#560](https://github.com/Krypton-Suite/Standard-Toolkit/issues/560), CheckBox Images are not scaled with dpi Awareness
* Resolved [#565](https://github.com/Krypton-Suite/Standard-Toolkit/issues/565), GroupBox icons are not scaled for dpi awareness
* Resolved [#559](https://github.com/Krypton-Suite/Standard-Toolkit/issues/559), Header group icon is not scaled for dpi awareness
* Resolved [#1946](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1946), ButtonSpecs do not scale anymore!
* Resolved [#1905](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1905), `Sparkle` Themes have an issue with the Background
* Resolved [#980](https://github.com/Krypton-Suite/Standard-Toolkit/issues/980), `KryptonDockableNavigator` with pages without `AllowConfigSave` flag are incorrectly saved
* Resolved [#1910](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1910), `Workspace Persistence` -> "Save to array" Causes an exception in `Toolkit.XmlHelper.Image.Save`
* Resolved [#1211](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1211), Button 'drop down' arrows should use palette text colour
* Resolved [#1212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1212), **[Breaking Change]** `KColorButton` 'drop-down' arrow should be drawn
	- Create Scaled Drop Glyph and use for colour button and comboDrops
	- Remove the `PaletteRedirectDropDownButton`
	- Remove `KryptonPaletteImagesDropDownButton`
	- **Breaking Change**: Remove `DropDownButtonImages` from designers
* Resolved [#1842](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1842), `KryptonTextBox` height collapses when MultiLine is enabled.
* Resolved [#1399](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1399), Hard coded colour setting removed from the `KryptonRibbonTab`.

=======

# 2024-11-14 - Build 2411 (Patch 4) - November 2024
* Resolved [#1837](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1837), `KryptonMessageBoxDefaultButton.Button2` doesn't work
* Resolved [#1820](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1820), When KryptonDataGridView.AutoGenerate is set WinForms columns are used. See the issue for full text.

=======

# 2024-10-14 - Build 2410 (Patch 3) - October 2024
* Implemented [#1813](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1813), LTS Configuration
* Resolved [#1800](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1800) `KryptonDataGridViewComboBoxEditingControl.EditingControlFormattedValue` property is differently implemented.
* Implemented [#1792](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1792), Enable 'SourceLink' for NuGet packages
* Resolved [#66](https://github.com/Krypton-Suite/Standard-Toolkit/issues/66), Cannot Add Ribbon-Buttons-Container (KryptonRibbonGroupTripple) when using .netcore onwards [Returns error due to abstract class]
* Resolved [#297](https://github.com/Krypton-Suite/Standard-Toolkit/issues/297), Office 2k7 colour usages are wrong
* Resolved [#1772](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1772), `KryptonDataGridViewComboBoxCell` properties, `AutoCompleteMode` and `AutoCompleteSource` have incorrect default values.

=======

## 2024-08-26 - Build 2408 (Patch 2) - August 2024
* Resolved [#1697](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1697), `KryptonComboBox` change in DropDownStyle cripples the control while the control is disabled when reenabled again.
* Resolved [#1755](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1755), Ribbon `GalleryButtonController` timer component causes an exception on mouse movements.
* Resolved [#1548](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1548), KComboBox DropDown arrow is illegible in certain themes
* Resolved [#1659](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1659), Solves `KryptonMessageBox` selected text issue, usage of diverse line breaks and sizing issues.
* Resolved [#1675](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1675), Catastrophic failure wherever `KryptonGroupPanel` is used.
* Resolved [#1677](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1677), `KryptonComboBox` cuts of text on high DPI.

=======

## 2024-07-22 - Build 2407 (Version 85 - Patch 1) - July 2024
* Resolved [#1373](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1373), `KT.CommonHelper.CheckContextMenuForShortcut()` handles direct type casts differently from .NET 8.0 onward. Solution courtesy of @Tape-Worm
* Resolved [#1583](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1583), `KryptonThemeComboBox` and `KrpytonThemeListBox` have the wrong designer assigned. Adds the `KryptonStubDesigner` internal class.
* Resolved [#1614](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1614), `KryptonMessageBox` throws an exception after Esc key is pressed.
* Resolved [#1613](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1613), `KryptonMessageBox` text is not centered vertically.
* Resolved [#1599](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1599), `KryptonMessageBox` cuts off the last line.
* Resolved [#1600](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1600), `KryptonMessageBox` stays on top of other windows.
* Resolved [#1580](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1580), Changing to certain modes in `KryptonNavigator` can cause a System.NullReferenceException

=======

## 2024-06-24 - Build 2406 - June 2024
* Resolved [#1561](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1561), KryptonRibbonGroup Controls remain enabled at runtime when set to disabled in the designer.
* Resolved [#1302](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1302), **[Breaking Change]** Font being used by "Professional" theme is pants !
* Resolved [#1528](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1528), Tracking colours need reviewing
* Resolved [#982](https://github.com/Krypton-Suite/Standard-Toolkit/issues/982), Double click on the Form1 file in the Krypton toolkit test project results in a designer error
* Resolved [#1455](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1455), **[Regression]** KryptonComboBox text is clipped; as height is incorrect.
* Resolved [#1381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1502), Docking Persistence broken since build `##.23.10.303`
* Resolved [#1508](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1508), **[Breaking Change]** ButtonSpec does not open assigned context menu when clicked.
	- Added property `ShowDrop`, which displays a drop-down arrow on the button.
	- When a `KryptonContextMenu` is connected the menu is shown when the button is clicked.
	- When a WinForms `ContextMenuStrip` is connected the menu is shown when the button is clicked.
	- When both type of the above ContextMenus are connected the `KryptonContextMenu` takes precedence.
	- The ButtonSpec's `Type` property does not need setting to "Context" to display the menu.
* Resolved [#619](https://github.com/Krypton-Suite/Standard-Toolkit/issues/619), KButton and KListbox unclear text color in certain scenarios
* Resolved [#1516](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1516), Theme Office 2010 Black Dark Mode causes a crash
* Resolved [#1328](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1328), Tentative adjustment to bring PaletteMode and the theme dictionary in line.
* Resolved [#1388](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1388), `KryptonButton` and `KryptonDropButton` Dropdown arrow color does not react to theme changes and is not visible.
* Resolved [#1424](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1424), **[Breaking Change]** `KryptonMessageBox` does not obey tab characters like `MessageBox`
* Resolved [#1383](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1383), Closing last Page in undocked page group prevents addition of further Pages via `KryptonDockingManager.AddToWorkspace`
* Resolved [#1381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1381), **[Regression]** Docking Persistence broken since build ##.23.10.303
* Version bump `80.xx.xx.xxx` -> `85.xx.xx.xx`

=======

## 2024-03-04 - Build 2403 (Version 80 - Patch 2) - March 2024
* Resolved [#1314](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1314), **[Regression]** CheckedListBox CheckedIndices NullRef

=======

## 2023-11-17 - Build 2311 (Version 80 - Patch 1) - November 2023
* Resolved issue where an assertion is made when using `KryptonThemeComboBox` or `KryptonRibbonGroupThemeComboBox`
* Resolved issue where `Sparkle` themes would crash when using certain `ButtonSpecs`
* Resolved [#1174](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1174), Unable to adjust height of `KryptonForm` when `KryptonRibbon` is added
	- _Note:_ This disables features from [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117), until further testing is completed
	* Backed-out [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117), Is it possible to have the KForm back colour as the KPanel colour

=======

## 2023-11-14 - Build 2311 - November 2023
* Resolved [#1093](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1093), `KryptonManager` - Setting the `GlobalPaletteMode` to 'Global' throws a error
* Implemented [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117), Is it possible to have the KForm back colour as the KPanel colour
* Resolved [#1153](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1153), Whilst investigating #1152 found that "Start drag" in certain application causes an exception.
* Resolved [#1152](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1152), Unable to resize control dragged from Navigator via KryptonDockingManager.FloatingWindowAdding event.
* Resolved [#1146](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1146), Krypton.Navigator throws exception in Initialise when attempting to EndInit().
* Implemented [#1009](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1009), Powered by Krypton Toolkit button
* Implemented [#508](https://github.com/Krypton-Suite/Standard-Toolkit/issues/508), Application Menus should fit well when using dark/light modes
* Resolved an issue where the ribbon tab text is illegible when using black (dark mode) themes
* Resolved [#1037](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1037), Borderless Krypton Form, Maximized, Top Most = True > Fullscreen does not overlap on task bar.
* Implemented [#956](https://github.com/Krypton-Suite/Standard-Toolkit/issues/956), Update `Readme.md` images to reflect the toolkit
* Added the ability to specify the message text alignment in a `KryptonMessagebox`
	- Default value is `MiddleLeft`
* Implemented [#1126](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1126), `KryptonRibbonGroupThemeComboBox` needs to be part of the ribbon designer
* Resolved [#1125](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1072), KryptonThemeCombox displays extra string (Todays alpha)
* Implemented [#1089](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1089), `KryptonProgressBar` in StatusStrips
* Resolved [#1072](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1072), Clicking "RootItem->Items" in a designer causes an exception to be thrown
* Resolved [#1109](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1109), KryptonPropertyGrid 'jagged' text
* Resolved [#1108](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1108), KryptonPropertyGrid background colour/text don't display correctly for some (most) themes
* Resolved [#288](https://github.com/Krypton-Suite/Standard-Toolkit/issues/288), Should the default "Theme fonts" now be changed to Segoe UI 9 pt
* Resolved [#703](https://github.com/Krypton-Suite/Standard-Toolkit/issues/703), Can BaseFontSize be moved up to IPalette? (and/or be easily modified in kryptonPalette)
* Resolved [#1105](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1105), Label text colour in black themes is illegible
* Resolved [#10](https://github.com/Krypton-Suite/Standard-Toolkit/issues/10), Substantial performance issue with AutoSize=true KryptonButtons
* Implemented [#1087](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1087), Add the ability to add custom integrated toolbar images to custom palettes
* Implemented [#1080](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1080), Use constants for design time strings
* Resolved [#1075](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1075), KryptonProgressBar does not change colours when the theme is changed
* Implemented [#910](https://github.com/Krypton-Suite/Standard-Toolkit/issues/910), Progress Bar - Border Rounding
* Resolved [#1076](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1076), Toolbar Commands show up in the designer as drag droppable components
* Implemented [#909](https://github.com/Krypton-Suite/Standard-Toolkit/issues/909), Can the colour picker support alpha blends
* Implemented [#943](https://github.com/Krypton-Suite/Standard-Toolkit/issues/943), ThemeManager should be in a single file
* Implemented [#1050](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1050), Automatically upgrade incompatible theme (XML) files
* Implemented [#962](https://github.com/Krypton-Suite/Standard-Toolkit/issues/962), Can ButtonSpecs support dropdown menus
* Implemented [#1069](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1069), Include Tooltips for other "Krypton Menu item" types (i.e. not just text!)
* Implemented [#1015](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1015), Show a tooltips on a `KryptonContextMenuItem`
* Implemented [#1057](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1057), Move `DefineFonts` etc up to `PaletteBase`
* Implemented [#981](https://github.com/Krypton-Suite/Standard-Toolkit/issues/981), "ReadMe.md" landing page could do with a "Table of contents" at the beginning
* New `KryptonLanguageManager.Strings` is now `KryptonLanguageManager.GeneralToolkitStrings`
* New `ShowSplitOption` for `KryptonButton`, allows a krypton/context menu to be shown
* Implemented [#1023](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1023), Please remove "sealed" from `KryptonWrapLabel` and `KryptonLinkWrapLabel`
* Resolved [#1020](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1020), Cannot add a `KryptonPage` to a `KryptonNavigator`
* Added ability to embed links into the `KryptonMessageBox` content. The new options are:-
	- `ContentAreaType` - Defines content area type of a `KryptonMessageBox`, default is normal
	- `LinkLabelCommand` - Specifies a `KryptonCommand` if using the `MessageBoxContentAreaType.LinkLabel` type.
	- `LinkLaunchArgument` - Specifies the `ProcessStartInfo` if a `LinkLabelCommand` has not been defined.
	- `ContentLinkArea` - Specifies the area of a link, if using the `MessageBoxContentAreaType.LinkLabel` type.
* Added `KryptonLanguageManager` to the `KryptonManager` action list
* Resolved [#1008](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1008), Krypton Save/Open file dialogs are not accessible from the toolbox
* Implemented [#1007](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1007), A way to alter all of the strings in the toolkit to language specific strings
* Implemented [#1006](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1006), Make converter strings localisable
	- **Note:** Components that use these strings may need to be 'refreshed' once changes have been made
* Implemented [#602](https://github.com/Krypton-Suite/Standard-Toolkit/issues/602), ToolStrip embedded into the non client area
* Implemented [#894](https://github.com/Krypton-Suite/Standard-Toolkit/issues/894), `KryptonPropertyGrid` needs to have full Krypton support
* Resolved [#999](https://github.com/Krypton-Suite/Standard-Toolkit/issues/999), Incorrect project file names while building
* Part resolved [#66](https://github.com/Krypton-Suite/Standard-Toolkit/issues/66), to allow ribbon designer in vs 17.5x and .net48 project types.
* Resolved [#838](https://github.com/Krypton-Suite/Standard-Toolkit/issues/838),
Cannot add items to KryptonGroupBox in WinForms Designer
* Resolved [#991](https://github.com/Krypton-Suite/Standard-Toolkit/pull/991),  Remove code that disallowed setting of the DataGridView Border
* Implemented [#896](https://github.com/Krypton-Suite/Standard-Toolkit/issues/896), Is it possible to modify the `FolderBrowserDialog` to use Krypton
* Implemented [#267](https://github.com/Krypton-Suite/Standard-Toolkit/issues/267), "Open / Save File Dialog" are in the Main Forms elements - Where is Kryptons' Standard themed equivalent.
* New `KryptonThemeSelector`, to allow switching between themes easily
* Resolved [#986](https://github.com/Krypton-Suite/Standard-Toolkit/issues/986), `ViewBuilderOutlookBase` - Stream cannot be null to initialize a bitmap (thanks to [Angelo](https://github.com/AngeloCresta))
* Add action list options to `KryptonCommand`
* Implemented [#686](https://github.com/Krypton-Suite/Standard-Toolkit/issues/686), Extend ToolTipManager designer to also allow "open/close" interval properties.
* Implemented [#950](https://github.com/Krypton-Suite/Standard-Toolkit/issues/950), Please add `-t:rebuild` to the msbuild command line
	- **Note:** This option is _only_ available for building `nightly` binaries
* Removed .NET Core 3.1 and .NET 5 references from NuGet package descriptions
* Add `AllowFormIntegrate` option to `KryptonRibbon`s action list, for easier access
* Resolved [#929](https://github.com/Krypton-Suite/Standard-Toolkit/issues/929), `KryptonRibbon` 'disappears' while running the application
* Resolved [#916](https://github.com/Krypton-Suite/Standard-Toolkit/issues/916), After a designer "Reset click" on the `CornerRoundingRadius`, it is still showing as a modified value, and still being written out into the designer file
* Resolved [#877](https://github.com/Krypton-Suite/Standard-Toolkit/issues/877), `CornerRoundingRadius` needs to have proper designer code to prevent it showing up as modified all the time
* Resolved [#923](https://github.com/Krypton-Suite/Standard-Toolkit/issues/923), The default theme is set to `Sparkle - Blue`
* Resolved [#915](https://github.com/Krypton-Suite/Standard-Toolkit/issues/915), Button Designer adds values it shouldn't
* Resolved [#885](https://github.com/Krypton-Suite/Standard-Toolkit/issues/885), Why does the "alpha" branch think it is behind "master"
* Implemented [#904](https://github.com/Krypton-Suite/Standard-Toolkit/issues/904), `*.Nightly` builds are no longer visible in Visual Studio
* Resolved [#905](https://github.com/Krypton-Suite/Standard-Toolkit/issues/905), NuGet description for `Lite` versions is incorrect
* Implemented [#813](https://github.com/Krypton-Suite/Standard-Toolkit/issues/813), Border colours need to match the themes
* Complete [#827](https://github.com/Krypton-Suite/Standard-Toolkit/issues/827), **[Breaking Change]** Expose IPalette / PaletteBase as a public interface in KryptonManager
* Resolved [#891](https://github.com/Krypton-Suite/Standard-Toolkit/issues/891), `LabelStyle` does not appear to have a default designer value
* Implemented [#887](https://github.com/Krypton-Suite/Standard-Toolkit/issues/887), A 'LinkLabel' version of the `KryptonWrapLabel`
* Fixed the display of the initial selected theme in the "ThemeSelection ComboBox"
* Resolved [#876](https://github.com/Krypton-Suite/Standard-Toolkit/issues/876), `Office 365 - Black` does not display text correctly
* Resolved [#874](https://github.com/Krypton-Suite/Standard-Toolkit/issues/874), 80.xx Canary NuGet text is incorrect
* Implemented [#866](https://github.com/Krypton-Suite/Standard-Toolkit/issues/866), `KryptonMessageBox` Option to use system icons
* New `Application` `KryptonMessageBoxIcon` type, specify your application icon to show in the `KryptonMessageBox` (recommended image size is 32 x 32)
* Resolved [#867](https://github.com/Krypton-Suite/Standard-Toolkit/issues/867), KryptonMessageBox does not show help button
* Implemented [#791](https://github.com/Krypton-Suite/Standard-Toolkit/issues/791), Help ButtonSpec - the icon should not be replaced if assigned KCommand does not have one
* Resolved [#728](https://github.com/Krypton-Suite/Standard-Toolkit/issues/728), Bring MessageBox States in-line with latest .Net 6
* Resolved [#861](https://github.com/Krypton-Suite/Standard-Toolkit/issues/861), Ribbon QAT Button downscaled when disabling HiDPI
* Implemented [#854](https://github.com/Krypton-Suite/Standard-Toolkit/issues/854), Please remove "2019" from the build sequence etc.
* Resolved [#848](https://github.com/Krypton-Suite/Standard-Toolkit/issues/848), KryptonTreeView Font Issues
* Implemented [#385](https://github.com/Krypton-Suite/Standard-Toolkit/issues/385), `KryptonColorButton` Modify Recent Colors
* Implemented [#535](https://github.com/Krypton-Suite/Standard-Toolkit/issues/535), A way to set the TextBox / ComboBox / RichTextBox Cue font Rendering hint style
* Resolved [#789](https://github.com/Krypton-Suite/Standard-Toolkit/issues/789), Remove shadows for context menus
* Resolved [#819](https://github.com/Krypton-Suite/Standard-Toolkit/issues/819), KryptonForm Enhancement - A option to disable/enable the close button.
* Resolved [#808](https://github.com/Krypton-Suite/Standard-Toolkit/issues/808), Ribbon Button Image padding is too small
* Applied toolbox images to `KryptonColorDialog`, `KryptonFontDialog` & `KryptonPrintDialog`
* New `Cut`, `Copy`, `Paste`, `Select All` & `Clear Clipboard` strings
* Added full license information to NuGet packages
* Renamed `Office 365` to `Microsoft 365` to fall in line with naming schemes
* Separate symbols packages for **Canary** & **Nightly** builds
* Removed support for .NET Core 3.1, in accordance with its official release cadence
* Removed support for .NET 5, in accordance with its official release cadence
* Support for .NET 8
* Version bump `70.xx.xx.xx` -> `80.xx.xx.xx`

=======

## 2022-11-08 - Build 2211 - November 2022
* Resolved [#817](https://github.com/Krypton-Suite/Standard-Toolkit/issues/817), DLL files in **Signed** NuGet packages are not signed. By default, all assemblies are now signed, negating the need for `Signed` NuGet packages (**Note:** You may need to sign your assemblies after upgrading. To find out how to do this, click [here](https://learn.microsoft.com/en-us/dotnet/standard/assembly/strong-named).)
* Removed `DisableCloseButton` option on `KryptonForm`, as feature was not properly implemented
* Resolved [#809](https://github.com/Krypton-Suite/Standard-Toolkit/issues/809), `Office 2010 - Silver (Light Mode)` - `KryptonDataGridView` throw exception on hover
* `KryptonMessageBox` using the `KryptonMessageBoxIcon.Shield` option, will now display the correct image for the version of the OS
* New `Apply`, `Back`, `Exit`, `Finish`, `Next` & `Previous` strings for custom situations
* Resolved [#800](https://github.com/Krypton-Suite/Standard-Toolkit/issues/800), `KryptonTextBox` not raising `KeyUp` event
* New `Collapse` & `Expand` strings for use in expandable footers, or in other custom situations
* Removed Visual Studio 2019 solution (*.sln) files, as 2022 is considered to be stable enough
* Resolved [#777](https://github.com/Krypton-Suite/Standard-Toolkit/issues/777), KryptonTreeView can throw exception on changing palette via KryptonManager. Note: unknown can still be set when deleting selected node
* Resolved [#774](https://github.com/Krypton-Suite/Standard-Toolkit/issues/774), `KryptonTableLayoutPanel` throwing exception when a form is minimized (thanks to [ZXBITLES](https://github.com/ZXBITLES))
* New `CustomColorPreviewShape` property for `KryptonColorButton` to allow configuration of a custom colour preview shape
* Updated the `KryptonTaskDialog` to use the `KryptonMessageBoxIcon` instead of the standard `System.Windows.Forms.MessageBoxIcon`
* Resolved [#764](https://github.com/Krypton-Suite/Standard-Toolkit/issues/764), `Development-Workflow.md` needs to state what the builds are (i.e. nightly)
* Implemented [#761](https://github.com/Krypton-Suite/Standard-Toolkit/issues/761), Can TFM also include .NET Framework 4.8.1?
* Resolved `ArgumentNullException` when saving `palette.xml` files with serialized Image objects (thanks to [hopla](https://github.com/hopla))
	- When the deserialized Image class is of type Bitmap, keep this bitmap, as opposed to painting it on a new in-memory bitmap. This keeps the original Format (as opposed, changing it to MemoryBMP.
	- When the format of an Image is of Format MemoryBMP, save the image as format BMP.
* Implemented [#756](https://github.com/Krypton-Suite/Standard-Toolkit/issues/756), Add `[AllowNull]` to a controls `Text` field
* Resolved [#738](https://github.com/Krypton-Suite/Standard-Toolkit/issues/738), "Office 2010 - Blue (Dark Mode)": Form title text cannot be read
* Implemented [#728](https://github.com/Krypton-Suite/Standard-Toolkit/issues/728), Bring MessageBox `States` inline with latest .Net 6
* Made enumeration `SchemeOfficeColors` public, so they can be used to make external themes
* Resolved [#689](https://github.com/Krypton-Suite/Standard-Toolkit/issues/689), A way to remove border shadows on `KryptonToolTips`
* Resolved [#666](https://github.com/Krypton-Suite/Standard-Toolkit/issues/666), KryptonTextBox `Validate` / `Validating` / `KeyUp` events are invoked twice
* Resolved [#660](https://github.com/Krypton-Suite/Standard-Toolkit/issues/660), Krypton DomainUpDown control: Nearly impossible to place the cursor via the mouse
* Resolved [#748](https://github.com/Krypton-Suite/Standard-Toolkit/issues/748), Navigator text is removed via designer changes for Navigator tabs
* Resolved [#739](https://github.com/Krypton-Suite/Standard-Toolkit/issues/739), KryptonButton - Image stretches with increased border rounding
* Resolved [#688](https://github.com/Krypton-Suite/Standard-Toolkit/issues/688), KryptonComboBox / KryptonNumericUpDown / KryptonDomainUpDown Anchor Sizing no as expected when anchored Top & Bottom
* Resolved [#722](https://github.com/Krypton-Suite/Standard-Toolkit/issues/722), KryptonRichTextBox disabled colour cannot be set
* Resolved [#662](https://github.com/Krypton-Suite/Standard-Toolkit/issues/662), Can not "Control / Set Disabled" Background Colour Of Comboboxes, in DropDown mode
* Resolved [#737](https://github.com/Krypton-Suite/Standard-Toolkit/issues/737), `Office 2013 - Dark Grey` for `PaletteMode` in designer causes a crash
* Resolved [#578](https://github.com/Krypton-Suite/Standard-Toolkit/issues/578), ComboBox Center no longer draws text centered
* Resolved [#20](https://github.com/Krypton-Suite/Standard-Toolkit/issues/20), Selected text in ComboBox is drawn in a different font
* Resolved [#308](https://github.com/Krypton-Suite/Standard-Toolkit/issues/308), Panel AntiAlias Border Problem
* Resolved [#734](https://github.com/Krypton-Suite/Standard-Toolkit/issues/734), Disabled Text in NumericUpDown Not visible
* Resolved [#715](https://github.com/Krypton-Suite/Standard-Toolkit/issues/715), v65.22.4.94 - PaletteSparkleBlueBase.GetContentPadding: Specified argument was out of the range of valid values. Parameter name: style
* Implemented the `PlacementModeConverter` for `PlacementMode` enum type
* Implemented [#551](https://github.com/Krypton-Suite/Standard-Toolkit/issues/551), `DropShadow` should now be off and deprecated
* Version bump `65.xx.xx.xxx` -> `70.xx.xx.xxx`
* Support for `.NET 7`

=======

## 2022-06-01 - Build 2206 - June 2022
* Improvements to all 'Black/Blue (Dark Mode)' themes
* Silver dark/light mode themes are now implemented
* Full/Lite NuGet packages - as support for .NET 5 ended in May, there are now 2 types of NuGet package.
	- Full - Supports every framework from .NET Framework 4.6.2 to .NET 6
	- Lite - Supports .NET Framework 4.8, .NET Core 3.1 and .NET 6
* Fixed grid cell selection colours for dark/light mode themes
* Blue dark mode themes now have a darker alternate colour
* Added new `GetPaletteModeManager()` method to the `ThemeManager` API, to return the current `PaletteModeManager` of the selected `KryptonManager`
* Update documentation for `PaletteMode` and `PaletteModeManager`
* Resolved [#701](https://github.com/Krypton-Suite/Standard-Toolkit/issues/701), `CueHint` in `KryptonPalette` does not work
* Resolved [#697](https://github.com/Krypton-Suite/Standard-Toolkit/issues/697), Number 9 is handled in ribbon textbox/richtextbox.
* Resolved [#693](https://github.com/Krypton-Suite/Standard-Toolkit/issues/693), Docked controls are rendered with smaller size which hides the caption/title text.
* Resolved [#653](https://github.com/Krypton-Suite/Standard-Toolkit/issues/653), Page Drag&Drop/Floating exception
* Resolved bug where the colour value for `PaletteBackStyle.ControlToolTip` was 'out of range' in certain themes
* Implemented [#691](https://github.com/Krypton-Suite/Standard-Toolkit/issues/691), Update Project landing pages with links to Help file downloads

=======

## 2022-04-04 - Build 2204 - April 2022
* Resolved [#678](https://github.com/Krypton-Suite/Standard-Toolkit/issues/678), Dropdown list background & text colour are the same (Office 2010 - Black (Dark Mode))
* Resolved [#673](https://github.com/Krypton-Suite/Standard-Toolkit/issues/673), `KryptonRibbonGroup` does not have entries for either `StateContextPressed` or `StateContextTracking`
* Resolved [#665](https://github.com/Krypton-Suite/Standard-Toolkit/issues/665), Breadcrumb - Cannot Add items to Root via the designer
* Implemented [#640](https://github.com/Krypton-Suite/Standard-Toolkit/issues/640), KForm add an option to align the title text.
* Resolved [#646](https://github.com/Krypton-Suite/Standard-Toolkit/issues/646), When the ButtonSpecs are left aligned the System Menu icon does not react to the click event any more
* Resolved [#642](https://github.com/Krypton-Suite/Standard-Toolkit/issues/642), Blurred window get left behind when focus is regained
* Resolved [#634](https://github.com/Krypton-Suite/Standard-Toolkit/issues/634), Opacity no longer works
* Resolved [#633](https://github.com/Krypton-Suite/Standard-Toolkit/issues/633), Source contains licensing code
* Resolved [#630](https://github.com/Krypton-Suite/Standard-Toolkit/issues/630), Ribbon accelerator overlay has shadowing
* Resolved [#611](https://github.com/Krypton-Suite/Standard-Toolkit/issues/611), `KryptonContextMenu`: Cannot add `ComboBoxItem`
* Implemented [#610](https://github.com/Krypton-Suite/Standard-Toolkit/issues/610), `KryptonContextMenuRadioButton` - no way to assign a method/event through the item editor
* Resolved [#609](https://github.com/Krypton-Suite/Standard-Toolkit/issues/609), `KryptonContextMenu`: Item text unreadable with certain themes
	- At the moment, only the 'Black/Blue (Dark Mode)' themes are being worked on
* Resolved [#607](https://github.com/Krypton-Suite/Standard-Toolkit/issues/607), `KryptonMessageBox` Certain length of the first line of text can push the text on the following out of the visible area (thanks to [giduac](https://github.com/giduac))
* Some fixes for [#603](https://github.com/Krypton-Suite/Standard-Toolkit/issues/603), Title Bar Images Stretched/Cropped
* Resolved [#596](https://github.com/Krypton-Suite/Standard-Toolkit/issues/596), ActionLists do not reflect the recommended or possible settings in the designer properties
* Resolved [#590](https://github.com/Krypton-Suite/Standard-Toolkit/issues/590), Button text colour in certain themes is unreadable
* Resolved [#587](https://github.com/Krypton-Suite/Standard-Toolkit/issues/587), `KryptonLabel` adds the `Paint` method by default
* Resolved [#584](https://github.com/Krypton-Suite/Standard-Toolkit/issues/584), Help icons need resizing (Source images)
* Resolved [#580](https://github.com/Krypton-Suite/Standard-Toolkit/issues/580), No such help icon is available for `Professional` themes
* Implemented [#573](https://github.com/Krypton-Suite/Standard-Toolkit/issues/573), `KTextBox` & `KRichTextBox` support Color.Transparent for a back colour
* Resolved [#399](https://github.com/Krypton-Suite/Standard-Toolkit/issues/399), DataGridView columns can have buttonspecs added, but they are never visible.
* Resolved [#70](https://github.com/Krypton-Suite/Standard-Toolkit/issues/70), If the BlurWhenFocusLost is set and the app is underneath another, then it will still have the blur overlay topmost (i.e. on top of the other app which is currently fullscreen!)
* Resolved [#69](https://github.com/Krypton-Suite/Standard-Toolkit/issues/69), The MessageBox display clears the blur underneath (Due to the code thinking it has lost focus, even when the messageBox is owned)
* Resolved [#68](https://github.com/Krypton-Suite/Standard-Toolkit/issues/68), When Blurring on Windows 10 with a large screen, the offset of the blur overlay is wrong
* Resolved [#39](https://github.com/Krypton-Suite/Standard-Toolkit/issues/39), KryptonNumericUpDown loses selection (visually) each time the control is painted
* Version bump `60.xx.xx.xxx` -> `65.xx.xx.xxx`

=======

## 2022-02-01 - Build 2202 - February 2022
* Add links to the NuGet version information in the package descriptions
* Updated NuGet package information to aid deployment to GitHub
* Versions have now been changed to the following format `Major.yy.MM.dayofyear`, i.e. `60.22.02.32`, but for convenience, builds will still be referenced as `yyMM` in documentation
* Overhauled `run.cmd`
* Build scripts are now stored in the `Scripts` folder, though it is now recommended to utilise `run.cmd`
* Resolved [#601](https://github.com/Krypton-Suite/Standard-Toolkit/issues/601), Form icon is displaying stretched width
* Resolved [#596](https://github.com/Krypton-Suite/Standard-Toolkit/issues/596), ActionLists do not reflect the recommended or possible settings in the designer properties
* Resolved [#562](https://github.com/Krypton-Suite/Standard-Toolkit/issues/562), Help Icon is not clearly visible in a lot of themes
* Resolved [#380](https://github.com/Krypton-Suite/Standard-Toolkit/issues/380), MDI Child form not fully maximizing not merging on the ribbon
* Resolved [#571](https://github.com/Krypton-Suite/Standard-Toolkit/issues/571), CenterScreen start on Form is no longer respected
* Resolved [#441](https://github.com/Krypton-Suite/Standard-Toolkit/issues/441), Wrong Ribbon Form Height when Windows is using scaling; e.g. 200% dpi
* Implement [#493](https://github.com/Krypton-Suite/Standard-Toolkit/issues/493), Invoke `PaletteState.Pressed` for all controlbox items in all office palettes
* Resolved [#487](https://github.com/Krypton-Suite/Standard-Toolkit/issues/487), The position of the KryptonForm Control Buttons are too low, when no desktop scaling preference is applied
* Implemented [#53](https://github.com/Krypton-Suite/Standard-Toolkit/issues/53), Need images of what this toolkit can give a developer on landing page
* Resolved [#34](https://github.com/Krypton-Suite/Standard-Toolkit/issues/34), KryptonRibbon.RibbonAppButton.AppButtonMenuItems has error
* Resolved [#204](https://github.com/Krypton-Suite/Standard-Toolkit/issues/204), When A drop Button is disabled, it should also colour the drop item as disabled
* Resolved [#310](https://github.com/Krypton-Suite/Standard-Toolkit/issues/310), Unsupported PaletteBackStyles are showing in the designer and causing it to crash
* Resolved [#545](https://github.com/Krypton-Suite/Standard-Toolkit/issues/545), KWrapLabel does not have a Target to use when the mnemonic is triggered.
* Resolved [#452](https://github.com/Krypton-Suite/Standard-Toolkit/issues/452), KryptonDockingManager - KryptonPage - MinimumSize not working
* Resolved [#533](https://github.com/Krypton-Suite/Standard-Toolkit/issues/533), KDataGridView: can column high lighting be turned of.
* Resolved [#538](https://github.com/Krypton-Suite/Standard-Toolkit/issues/538), `KryptonListBox` error inside the IDE/ActionLists for `KryptonListBox` and `KryptonListView`
* Resolved [#530](https://github.com/Krypton-Suite/Standard-Toolkit/issues/530), "Ctrl+C" in Error KMessageBoxes no longer works
* Resolved [#525](https://github.com/Krypton-Suite/Standard-Toolkit/issues/525), Undesired behaviour in KryptonGroupBox
* Resolved [#520](https://github.com/Krypton-Suite/Standard-Toolkit/issues/520), KTooltips default to using the KryptonIcon when nothing is set by the developer
* Resolved some minor issues regarding some dark themes
* Resolved tracking colours in `Office 2010 - Black (Dark Mode)`
* Resolved [#500](https://github.com/Krypton-Suite/Standard-Toolkit/issues/500), `KryptonThemeComboBox` Themes List empty at Runtime
* Implemented [#492](https://github.com/Krypton-Suite/Standard-Toolkit/issues/492), Remove `Office 2013` due to redundancy with `Office 2013 White` theme
* Resolved [#361](https://github.com/Krypton-Suite/Standard-Toolkit/issues/361), Nightlies version needs to have something that allows installers to upgrade correctly

=======

## 2022-01-05 - Build 2201 - January 2022
* Complete [#517](https://github.com/Krypton-Suite/Standard-Toolkit/issues/517), Warning CS3008: Identifier '_toolTipValues' is not CLS-compliant
* Resolved [#515](https://github.com/Krypton-Suite/Standard-Toolkit/issues/515), Office 365 Dark mode does not have enough contrast for disabled text
* Resolved [#511](https://github.com/Krypton-Suite/Standard-Toolkit/issues/511), KryptonRibbonGroupComboBox does not have tooltips
* Resolved [#382](https://github.com/Krypton-Suite/Standard-Toolkit/issues/382), RibbonGroupNumericUpDown does not have tooltips
* Resolved [#453](https://github.com/Krypton-Suite/Standard-Toolkit/issues/453), KryptonDataGridView's cell cannot display multiple lines when DefaultCellStyle.WrapMode set true
* Resolved [#499](https://github.com/Krypton-Suite/Standard-Toolkit/issues/499), `KDataGridView` Cell Borders
* Resolved [#502](https://github.com/Krypton-Suite/Standard-Toolkit/issues/502),KNumericUpDowner, when told to display 1 decimal place, does not display a 0 when needed
* Resolved issue while running `build-2019.cmd` would cause build errors
* Resolved [#491](https://github.com/Krypton-Suite/Standard-Toolkit/issues/491), Krypton.Toolkit.KryptonMessageBox wrong form height when YesNo or AbortRetryIgnore buttons selected (thanks to [mbsysde99](https://github.com/mbsysde99))
* Resolved [#488](https://github.com/Krypton-Suite/Standard-Toolkit/issues/488), `KryptonTextBox` DoubleClick event does not fire
* Resolved [#484](https://github.com/Krypton-Suite/Standard-Toolkit/issues/484), Using todays alpha, Active form does not show the title text when using QAT
* Resolved [#483](https://github.com/Krypton-Suite/Standard-Toolkit/issues/483), `KryptonForm` loses visibility when the form border property is set to ***None*** using certain themes (thanks to [mbsysde99](https://github.com/mbsysde99))
* Resolved [#478](https://github.com/Krypton-Suite/Standard-Toolkit/issues/478), `ThemesComboBox` has a double list
* Resolved [#473](https://github.com/Krypton-Suite/Standard-Toolkit/issues/473), The `KryptonListbox` component does not fire the double-click event
* Resolved [#459](https://github.com/Krypton-Suite/Standard-Toolkit/issues/459), Respect Maximum size for Child Forms and controls
* Resolved `WINDOWSLOGO` Option for `KryptonMessageBox` does not work for Windows 10
* Resolved combobox dropdown colours while using dark/light theme modes
* Implemented [#469](https://github.com/Krypton-Suite/Standard-Toolkit/issues/469), Instate these for all O2k10 & O2k13/365 palettes!
* Resolved [#449](https://github.com/Krypton-Suite/Standard-Toolkit/issues/449), `WINDOWSLOGO` Option for KMessagebox does not work for Windows 11
* Resolved [#402](https://github.com/Krypton-Suite/Standard-Toolkit/issues/402), KryptonInputBox cutting off prompt text
* Resolved [#433](https://github.com/Krypton-Suite/Standard-Toolkit/issues/433), `Office 2010 - Black (Light Mode)` does not do anything
* Resolved [#416](https://github.com/Krypton-Suite/Standard-Toolkit/issues/416), Artefacts when displaying the full colour dialog
* Resolved [#415](https://github.com/Krypton-Suite/Standard-Toolkit/issues/415), Colour Dialog right margin is too wide
* Resolved [#422](https://github.com/Krypton-Suite/Standard-Toolkit/issues/422), FontDialog ColourButton replacement has dot's in corners in dark theme
* Implemented [#396](https://github.com/Krypton-Suite/Standard-Toolkit/issues/396), Change the color button from automatically launching the dialog, and I can respond to an event to instead open my own dialog
* Implemented [#372](https://github.com/Krypton-Suite/Standard-Toolkit/issues/372), Office 2007 Themes last ribbon tab is not fully selectable
* Implemented [#395](https://github.com/Krypton-Suite/Standard-Toolkit/issues/395), Add ability to add form icon to common dialogs
* Implemented [#396](https://github.com/Krypton-Suite/Standard-Toolkit/issues/396), Change the color button from automatically launching the dialog, and I can respond to an event to instead open my own dialog?
* Implemented [#404](https://github.com/Krypton-Suite/Standard-Toolkit/issues/404), `KryptonInputBox` to have a default button
* Resolved [#237](https://github.com/Krypton-Suite/Standard-Toolkit/issues/237), Office 365 - Black (Dark mode) Messes up combo boxes
* Resolved [#403](https://github.com/Krypton-Suite/Standard-Toolkit/issues/403), Krypton.Toolkit.Nightly `Version="6.2109.272-alpha"` has removed code that was in 270
* Complete [#118](https://github.com/Krypton-Suite/Standard-Toolkit/issues/118), Fix Compile Warnings and Messages
* Implemented [#85](https://github.com/Krypton-Suite/Standard-Toolkit/issues/85), Update the project names from `2019` to `2022`
* Updated code header year from `2021` to `2022`

=======

## 2021-11-08 - Build 2111 - November 2021
* Implemented [#49](https://github.com/Krypton-Suite/Standard-Toolkit/issues/49), Support for .NET 6
* Implements [#384](https://github.com/Krypton-Suite/Standard-Toolkit/issues/384), FontDialog only has 16 colours, Bring back the ColourChooser button
* Resolved [#381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/381), Allow Common Dialog Titles to be changed
* Resolved [#383](https://github.com/Krypton-Suite/Standard-Toolkit/issues/383), KFontDialog shows checked items that are not applied
* Resolved [#382](https://github.com/Krypton-Suite/Standard-Toolkit/issues/382), `RibbonGroupNumericUpDown` does not have tooltips
* Resolved [#14](https://github.com/Krypton-Suite/Theme-Palettes/issues/14), Palette Designer Messageboxes are RTL
* Resolved [#376](https://github.com/Krypton-Suite/Standard-Toolkit/issues/376), Sparkle - ## (Dark mode) and Sparkle - ## (Light mode) do not change anything when selected
* New `KryptonThemeComboBox` control, switch between themes with a few clicks
* Resolved [#364](https://github.com/Krypton-Suite/Standard-Toolkit/issues/364), Multi-Monitor Splitter dragging madness
* New `KryptonColorButton` action list options
* The `KryptonColorButton` now uses the `KryptonColorDialog`
* Resolved [#361](https://github.com/Krypton-Suite/Standard-Toolkit/issues/361), Nightlies version needs to have something that allows installers to upgrade correctly
* Resolved [#358](https://github.com/Krypton-Suite/Standard-Toolkit/issues/358), Since the "Nightlies" have been created, loading the Standard toolkit project causes Visual Studio to go ape
* Resolved [#336](https://github.com/Krypton-Suite/Standard-Toolkit/issues/336), "Office 2010 And 365 Minimise Icons Change Size" on hover
* Implement [#335](https://github.com/Krypton-Suite/Standard-Toolkit/issues/335), Create 'Nightly' NuGet packages When Merges into alpha are done
* Resolved [#315](https://github.com/Krypton-Suite/Standard-Toolkit/issues/315), When adding a new KContextmenu, How do you change it's name etc
- Sort out the Callback actions that were removed
* Resolved [#323](https://github.com/Krypton-Suite/Standard-Toolkit/issues/323), `KryptonCheckButton` Click removes image
- Sort out the Callback actions that were removed
* Resolved [#316](https://github.com/Krypton-Suite/Standard-Toolkit/issues/316), Restore `KryptonKInputBox` layout to initial intent.
* Resolved [#333](https://github.com/Krypton-Suite/Standard-Toolkit/issues/333), Krypton Form - ButtonSpec (And Icon), are Cut Off when maximised
* Resolved [#313](https://github.com/Krypton-Suite/Standard-Toolkit/issues/313), KryptonMessagebox is not RTL compliant
* Resolved [#320](https://github.com/Krypton-Suite/Standard-Toolkit/issues/320), `KryptonColorButton` "SelectedRect" will not do anything but a "Square"
* Implemented [#304](https://github.com/Krypton-Suite/Standard-Toolkit/issues/304), [Feature Request]: Track Bar "Back Color" needed in State#### Designer control(s)
* Implemented [#304](https://github.com/Krypton-Suite/Standard-Toolkit/issues/304),[Feature Request]: Track Bar "Back Color" needed in State#### Designer control(s)
	- Add "DrawBackground" as a visible Designer element
* Implemented [#227](https://github.com/Krypton-Suite/Standard-Toolkit/issues/227), [Bug]: OverrideFocus Designer settings do not work or Prevent Ribbon MouseOver Highlighting
	- The Fix works in "All Themes" apart from `Office2k7` and `Sparkle`, where it is diminished but still highlights with white!
* Implemented [#291](https://github.com/Krypton-Suite/Standard-Toolkit/issues/291), Build: can the echo time have a time zone, so that when used for PR's it can be checked against the user locale
* Implemented [#290](https://github.com/Krypton-Suite/Standard-Toolkit/issues/290), Should V6 only support "MS Supported" Net Frameworks?
	- The toolkit will only work with projects using .NET Framework 4.6.2 or higher
	- `lite` NuGet packages are no longer being supported or maintained for the forseeable future
	- For more information, please visit [here](https://dotnet.microsoft.com/platform/support/policy/dotnet-framework)
* Implemented [#282](https://github.com/Krypton-Suite/Standard-Toolkit/issues/282), `KryptonScrollbars` need to have smart tags
* Resolved [#245](https://github.com/Krypton-Suite/Standard-Toolkit/issues/245), `TableLayoutPanel` should be "Kryptonised"
* Implemented [#269](https://github.com/Krypton-Suite/Standard-Toolkit/issues/269), "Print Dialog" is in the Main Forms elements - Where is Kryptons' Standard themed equivalent
  - This *does not do* the PrintDlgEx, as that is buried too deep in the OS.
* Resolved [#281](https://github.com/Krypton-Suite/Standard-Toolkit/issues/281), `KryptonScrollbar`: Scroll value cannot be set
* Resolved [#274](https://github.com/Krypton-Suite/Standard-Toolkit/issues/274), KRadioButton should use the Label(Panel) style by default
* Resolved [#273](https://github.com/Krypton-Suite/Standard-Toolkit/issues/273), KCheckBox should use the Label(Panel) style by default
* Resolved [#271](https://github.com/Krypton-Suite/Standard-Toolkit/issues/271), CueHint text is "Bottom" clipped by default
  - And add `TextV` to allow control for multi-line text boxes
* Implemented [#265](https://github.com/Krypton-Suite/Standard-Toolkit/issues/265), "Color Dialog" is in the Main Forms elements - Where is Kryptons' Standard themed equivalent
* Resolved [#261](https://github.com/Krypton-Suite/Standard-Toolkit/issues/261), `KryptonPalette` does not work (6.0.2108.x)
* Implemented [#256](https://github.com/Krypton-Suite/Standard-Toolkit/issues/256), Sort out palette image resources
* Implemented [#243](https://github.com/Krypton-Suite/Standard-Toolkit/issues/243), "Font Dialog" is in the Main Forms elements - Where is Kryptons' Standard themed equivalent
* Resolved [#254](https://github.com/Krypton-Suite/Standard-Toolkit/issues/254), VisualStudio Themes should not be listed yet, as they cause an exception when used
* Resolved [#242](https://github.com/Krypton-Suite/Standard-Toolkit/issues/242), `KryptonMessageBox` display is off the left of the display area
* Resolved [#234](https://github.com/Krypton-Suite/Standard-Toolkit/issues/234), `KryptonDataGridViewBinary`#### Classes should **not** be in the standard toolkit
* Implement [#212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/212), The `KryptonPropertyGrid` should be moved into the `Standard-Toolkit`
* Centralised `AsssemblyInfo` and `Version` information
* Implemented [#223](https://github.com/Krypton-Suite/Standard-Toolkit/issues/223), Can the build also echo out the DateTime, after the time elapsed
* Resolved [#159](https://github.com/Krypton-Suite/Standard-Toolkit/issues/159), Office 365 tracking colours are wrong
* Resolved [#120](https://github.com/Krypton-Suite/Standard-Toolkit/issues/120), `ButtonSpec` thinks that the default of `inherit` on Style is not the default
* Resolved [#92](https://github.com/Krypton-Suite/Standard-Toolkit/issues/92), **Many** Items cannot be Dragged and dropped onto a Form in Designer (Second patch)
	- As the "Designers" use text based references, then only need to include dlls derived from  `ParentControlDesigner`
	- Resolves [Bug]: **Many** Items cannot be Dragged and dropped onto a Form in Designer
* Resolved [#64](https://github.com/Krypton-Suite/Standard-Toolkit/issues/64), `KryptonContextMenu` Designer has layout issues
* Resolved [#51](https://github.com/Krypton-Suite/Standard-Toolkit/issues/51), Minimize icon in form titlebar is not disabled
* Remove the `Typeof` in the designer attributes and use text to prevent "Pre-Loading" of the wrong `System.Designer.dll` in Multi-Target projects
* Sort out the reference assemblies information to ensure correct loading of controls in designer
* Remove references to V2.0.0.0 of the System.Design when using the `MultilineStringEditor`
* Remove nullable messages caused by `CS8618`
* New UAC option for `KryptonButton`
* All `using` statements are now kept in one global file for each project, as per the C# 10 specification

=======

## 2021-08-03 Build 2108.1 - August 2021 (Canary Update 1)
* Resolved [#230](https://github.com/Krypton-Suite/Standard-Toolkit/issues/230), ThemeManager does not populate with the new "Light / Dark" themes
* Resolved [#229](https://github.com/Krypton-Suite/Standard-Toolkit/issues/229), Cannot set button text programatically
* Resolved [#225](https://github.com/Krypton-Suite/Standard-Toolkit/issues/225), Cannot set text of a button in designer

=======

## 2021-08-02 Build 2108 - August 2021 (Canary)
* Implement [#207](https://github.com/Krypton-Suite/Standard-Toolkit/issues/207), 'DarkMode' for `KryptonRichTextBox`/`KryptonTextBox` (Sparkle theme updates to come)
* Resolved [#150](https://github.com/Krypton-Suite/Standard-Toolkit/issues/150), Change the Default type of Theme for A KryptonLabel to be "Normal- Panel"
	- Make sure that the `NormalPanel` is the default style
	- Also Resolved the WrapLabel Style not being set correctly
* Resolved [#202](https://github.com/Krypton-Suite/Standard-Toolkit/issues/202), `KryptonGroup` transparency
* Remove the internal class Called `KryptonDataGridViewIconColumn` from design use [#27](https://github.com/Krypton-Suite/Standard-Toolkit/issues/27)
	- Put back the removed `CLSCompliant` and `ComVisible` assembly flags for backwards compatibility
	- Update the projects to comply/use the latest analysers
* RichTextBox now allows CueHint Text
* ComboBox now allows CueHint Text
* Implement [#197](https://github.com/Krypton-Suite/Standard-Toolkit/issues/197), Rounding should use `float` or `double` instead of `int`. Rounding now accepts `float` values
* New logo for both canary and stable builds
* Resolved [#138](https://github.com/Krypton-Suite/Standard-Toolkit/issues/138), `KryptonListView` throws a `System.Resources.MissingManifestResourceException`
	- If you want a standard List then use ListBox or CheckedListBox
	- If you want a Details view then use a `DataGrid`
	- This implements LargeIcons / Small Icons / Tiles with and without checkboxes as allowed
* New `KryptonMessageBoxIcon` to replace the default `MessageBoxIcon` option
* Updated `KryptonMessageBox` icons
* Implement [#162](https://github.com/Krypton-Suite/Standard-Toolkit/issues/162), Default rounding of control corners
* New the `KryptonButton` will now change its text, based on the `DialogResult` property
* Implement [#154](https://github.com/Krypton-Suite/Standard-Toolkit/issues/154), Ability to alter both a `KryptonManager` and a `KryptonPalette` from within a `KryptonForm`
* Implement [#149](https://github.com/Krypton-Suite/Standard-Toolkit/issues/149), Change the default theme from `Office 2010 - Blue` to `Office 365 - Blue`
* Implement [#147](https://github.com/Krypton-Suite/Standard-Toolkit/issues/147), Update `csproj` files to handle `AssemblyInfo` data
* New `KryptonInputBoxManager` control, now you can configure a `KryptonInputBox` through the designer
* Improved the `KryptonInputBox` to take advantage of the `KryptonTextBox` ***CueHint*** features
* New `KryptonWebBrowser`Control allowing `KryptonContext` menus [#113](https://github.com/Krypton-Suite/Standard-Toolkit/issues/113)
* Resolved [#122](https://github.com/Krypton-Suite/Standard-Toolkit/issues/122), Placing a `KryptonStatusStrip` in the Designer causes an exception
* Implemented [#117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/117), Consolidate `using` statements
* Resolved [#106](https://github.com/Krypton-Suite/Standard-Toolkit/issues/106), 'MethodImplOptions' does not contain a definition for 'AggressiveInlining'
* Resolved [#102](https://github.com/Krypton-Suite/Standard-Toolkit/issues/102), `KryptonInputBox` does not compile
* Resolved [#99](https://github.com/Krypton-Suite/Standard-Toolkit/issues/99), There is no need to replace some functions in `KryptonCheckButtonActionList`
* Documentation [#97](https://github.com/Krypton-Suite/Standard-Toolkit/issues/97), Add descriptions & documentation
* Resolved [#92](https://github.com/Krypton-Suite/Standard-Toolkit/issues/92), **Many** Items cannot be Dragged and dropped onto a Form in Designer
* Resolved [#91](https://github.com/Krypton-Suite/Standard-Toolkit/issues/91), Cannot add items to KryptonGroupBox in designer
* Resolved [#84](https://github.com/Krypton-Suite/Standard-Toolkit/issues/84), Multi-Line Text Box does not support Hint
* Implemented [#82](https://github.com/Krypton-Suite/Standard-Toolkit/issues/82), ComboBox Should be able to have *Hint's*
* Implemented [#81](https://github.com/Krypton-Suite/Standard-Toolkit/issues/81), Can the "6.x.Lite" version only support .net48;net5.0-windows;net6.0-windows
* Implemented [#79](https://github.com/Krypton-Suite/Standard-Toolkit/issues/79), Customise 'Hint' Font
* Resolved [#77](https://github.com/Krypton-Suite/Standard-Toolkit/issues/77), When using KryptonInputBox focus is not on the edit box when it is shown
* Resolved [#71](https://github.com/Krypton-Suite/Standard-Toolkit/issues/71), No design support while using Krypton.Ribbon in .NET 5/6
* Resolved [#54](https://github.com/Krypton-Suite/Standard-Toolkit/issues/54), .Net5 WinForm Project - Dropping a KLabel onto a KGroup or KGroupBox causes an Exception in Designer

=======

## 2021-06-26 - Build 2106.2 - June 2021 (Update 2)
* Resolved [#167](https://github.com/Krypton-Suite/Standard-Toolkit/issues/167), Latest Canary of `KryptonInputBox` is not usable!!

=======

## 2021-06-04 - Build 2106.1 - June 2021 (Update 1)
* Fix NuGet package descriptions
* Disabled `KryptonListView`, `KryptonToolStrip` and `KryptonStatusStrip` - to return in a future update

=======

## 2021-06-02 - Build 2106.1 - June 2021 (Update 1 - Canary)
* Resolved [#129](https://github.com/Krypton-Suite/Standard-Toolkit/issues/129), Canary NuGet description for `Lite` is incorrect

=======

## 2021-06-01 - Build 2106 - June 2021
* Allow the user to set a `ActivePage` on the `KryptonWorkspace` control
* Implemented [#43](https://github.com/Krypton-Suite/Standard-Toolkit/issues/43), Access corner rounding features via Action Lists
* Implemented [#60](https://github.com/Krypton-Suite/Standard-Toolkit/issues/60), As this project is now a long way from the original "ComponentFactory" - The BSD-3 License Header needs to change
* Removed `Krypton.Toolkit.Values` namespace to comply with the general namespace. ***NOTE: This is likely to cause errors in your projects. To solve, please do a find/replace with `Krypton.Toolkit.Values` to `Krypton.Toolkit` in your source code.***
* The `MultilineStringEditor` now inherits a `KryptonForm`

=======

## 2021-03-27 - Build 2103.1 - March 2021 (Update 1)
* Remove the Auto Versioning on Each build
* Resolved [#52](https://github.com/Krypton-Suite/Standard-Toolkit/issues/52), Nuget Package descriptions need to be consistent (And published!)

=======
## 2021-03-01 - Build 2103 - March 2021
* Resolved [#35](https://github.com/Krypton-Suite/Standard-Toolkit/issues/35), It is still not possible to create a `KryptonStatusStrip` on a form at design time.
* Resolved `KryptonPalette` component, courtesy of 'gwni'
* Added properties to action lists such as `ShortFont`, `LongFont`, `Font`, `SelectedColour` etc. More to come.
* Resolved bug where the `KryptonTextBox` does not repaint after altering the `Hint` property
* Implemented [#14](https://github.com/Krypton-Suite/Standard-Toolkit/issues/14), Access Fonts via Action Lists
* Removed `KryptonStatusStrip` & `KryptonToolStrip` (these are now part of the `Krypton.Toolkit.Suite.Extended.Tool.Strip.Items` module, as of build **2104**)

=======

## 2021-01-05 - Build 2101 - January 2021

* Updated year references from `2020` to `2021`
* Resolved [#19](https://github.com/Krypton-Suite/Standard-Toolkit/issues/19), "Office 365 Silver" theme `StatusBar` drag glyph, is not visible
* Resolved [#20](https://github.com/Krypton-Suite/Standard-Toolkit/issues/20), Selected text in ComboBox is drawn in a different font

=======

## 2020-11-01 - Build 2011 - November 2020
* Shortened namespaces, the toolkit will now use namespaces such as `Krypton.Ribbon` instead of the older `ComponentFactory.Krypton.Ribbon` to align with package names
* Support for .NET 5
* Versions now begin with `5.550`
* Long term support (LTS) packages are available for developers who need extra time to migrate their projects over to the new namespace format or .NET 5
* ***Important! If you're upgrading from a older legacy version, please uninstall your currently installed packages BEFORE intalling these packages. Please also follow [this](https://github.com/Krypton-Suite/Standard-Toolkit-Online-Help/blob/master/Source/Documentation/Standard%20Toolkit%20Migration%20Guide.md) guidence, as the toolkit now uses different namespaces!***

=======

## 2020-08-12 - Build 2008 - August 2020
* Resolved [#16](https://github.com/Krypton-Suite/Standard-Toolkit/issues/16), Taskbar Width is offset in Docking operations. Fix courtesy of [sneusse](https://github.com/sneusse).

=======

## 2020-06-01 - Build 2006 - June 2020
* Implemented [#8](https://github.com/Krypton-Suite/Standard-Toolkit/issues/8), Is it possible to only minimize FloatingWindow in DockingManager?
* Resolved [#9](https://github.com/Krypton-Suite/Standard-Toolkit/issues/9), Cannot place `KryptonStatusStrip` on a Form
* Resolved [#12](https://github.com/Krypton-Suite/Standard-Toolkit/issues/12), AllowButtonSpecToolTipPriority
	- If the parent Item has tooltips, and the button spec has tooltips, then the default is show both when hovering over the button spec. This can be disabled by setting AllowButtonSpecTooltipPriority to true, so that only 1 tooltip is displayed when hovering over any part of the control.

=======

## 2020-03-01 - Build 2003 - March 2020
* Resolved [#39](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/39), System.ArgumentException: 'Parameter is not valid.'
* Resolved [#30](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/30), `AutoHiddenGroup elements do not properly process AppearanceChanged events`. Credit to [MGRussell](https://github.com/MGRussell)
* Resolved `SerializationException: Type 'System.Windows.Forms.Cursor' in Assembly 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' is not marked as serializable.`, credit to [Carko](https://github.com/Carko)
* Updated website URL in code files
* Updated minor version number from `490` to `500` in code files

=======

## 2020-02-07 - Build 2002.1 - February 2020 (Update 1)
* Resolved [#28](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/28), KryptonForm crashing Visual Studio
* Added support for `KryptonInputBox` textbox text alignment

=======

## 2020-02-01 - Build 2002 - February 2020

* Support for all frameworks .NET 3.5 to 4.8 inclusive
* Support for .NET Core LTS (currently 3.1)
* Changed `490` to `500`
* Builds from now on will be labelled as `YYMM`
