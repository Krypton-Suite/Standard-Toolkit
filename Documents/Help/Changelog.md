# <img src="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Krypton.png"> Standard Toolkit - ChangeLog

## 2022-04-04 - Build 2204 - April 2022
* Resolved [#630](https://github.com/Krypton-Suite/Standard-Toolkit/issues/630), Ribbon accelerator overlay has shadowing
* Resolved [#611](https://github.com/Krypton-Suite/Standard-Toolkit/issues/611), `KryptonContextMenu`: Cannot add `ComboBoxItem`
* Implemented [#610](https://github.com/Krypton-Suite/Standard-Toolkit/issues/610), `KryptonContextMenuRadioButton` - no way to assign a method/event through the item editor
* Resolved [#609](https://github.com/Krypton-Suite/Standard-Toolkit/issues/609), `KryptonContextMenu`: Item text unreadable with certain themes
    - At the moment, only the 'Black (Dark Mode)' themes are being worked on
* Resolved [#607](https://github.com/Krypton-Suite/Standard-Toolkit/issues/607), `KryptonMessageBox` Certain length of the first line of text can push the text on the following out of the visible area (thanks to [giduac](https://github.com/giduac))
* Some fixes for [#603](https://github.com/Krypton-Suite/Standard-Toolkit/issues/603), Title Bar Images Stretched/Cropped 
* Resolved [#596](https://github.com/Krypton-Suite/Standard-Toolkit/issues/596), ActionLists do not reflect the recommended or possible settings in the designer properties
* Resolved [#590](https://github.com/Krypton-Suite/Standard-Toolkit/issues/590), Button text colour in certain themes is unreadable
* Resolved [#587](https://github.com/Krypton-Suite/Standard-Toolkit/issues/587), `KryptonLabel` adds the `Paint` method by default
* Resolved [#584](https://github.com/Krypton-Suite/Standard-Toolkit/issues/584), Help icons need resizing (Source images)
* Resolved [#580](https://github.com/Krypton-Suite/Standard-Toolkit/issues/580), No such help icon is available for `Professional` themes
* Resolved [#70](https://github.com/Krypton-Suite/Standard-Toolkit/issues/70), If the BlurWhenFocusLost is set and the app is underneath another, then it will still have the blur overlay topmost (i.e. on top of the other app which is currently fullscreen!)
* Resolved [#69](https://github.com/Krypton-Suite/Standard-Toolkit/issues/69), The MessageBox display clears the blur underneath (Due to the code thinking it has lost focus, even when the messageBox is owned)
* Resolved [#68](https://github.com/Krypton-Suite/Standard-Toolkit/issues/68), When Blurring on Windows 10 with a large screen, the offset of the blur overlay is wrong

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
* Resolved for [#129](https://github.com/Krypton-Suite/Standard-Toolkit/issues/129), Canary NuGet description for `Lite` is incorrect

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
* Resolved for [#35](https://github.com/Krypton-Suite/Standard-Toolkit/issues/35), It is still not possible to create a `KryptonStatusStrip` on a form at design time.
* Resolved for `KryptonPalette` component, courtesy of 'gwni'
* Added properties to action lists such as `ShortFont`, `LongFont`, `Font`, `SelectedColour` etc. More to come.
* Resolved bug where the `KryptonTextBox` does not repaint after altering the `Hint` property
* Implemented [#14](https://github.com/Krypton-Suite/Standard-Toolkit/issues/14), Access Fonts via Action Lists
* Removed `KryptonStatusStrip` & `KryptonToolStrip` (these are now part of the `Krypton.Toolkit.Suite.Extended.Tool.Strip.Items` module, as of build **2104**)

=======

## 2021-01-05 - Build 2101 - January 2021

* Updated year references from `2020` to `2021`
* Resolved for [#19](https://github.com/Krypton-Suite/Standard-Toolkit/issues/19), "Office 365 Silver" theme `StatusBar` drag glyph, is not visible
* Resolved for [#20](https://github.com/Krypton-Suite/Standard-Toolkit/issues/20), Selected text in ComboBox is drawn in a different font

=======

## 2020-11-01 - Build 2011 - November 2020
* Shortened namespaces, the toolkit will now use namespaces such as `Krypton.Ribbon` instead of the older `ComponentFactory.Krypton.Ribbon` to align with package names
* Support for .NET 5
* Versions now begin with `5.550`
* Long term support (LTS) packages are available for developers who need extra time to migrate their projects over to the new namespace format or .NET 5
* ***Important! If you're upgrading from a older legacy version, please uninstall your currently installed packages BEFORE intalling these packages. Please also follow [this](https://github.com/Krypton-Suite/Standard-Toolkit-Online-Help/blob/master/Source/Documentation/Standard%20Toolkit%20Migration%20Guide.md) guidence, as the toolkit now uses different namespaces!***

=======

## 2020-08-12 - Build 2008 - August 2020
* Resolved for [#16](https://github.com/Krypton-Suite/Standard-Toolkit/issues/16), Taskbar Width is offset in Docking operations. Fix courtesy of [sneusse](https://github.com/sneusse).

=======

## 2020-06-01 - Build 2006 - June 2020
* Implemented [#8](https://github.com/Krypton-Suite/Standard-Toolkit/issues/8), Is it possible to only minimize FloatingWindow in DockingManager?
* Resolved for [#9](https://github.com/Krypton-Suite/Standard-Toolkit/issues/9), Cannot place `KryptonStatusStrip` on a Form
* Resolved for [#12](https://github.com/Krypton-Suite/Standard-Toolkit/issues/12), AllowButtonSpecToolTipPriority 
    - If the parent Item has tooltips, and the button spec has tooltips, then the default is show both when hovering over the button spec. This can be disabled by setting AllowButtonSpecTooltipPriority to true, so that only 1 tooltip is displayed when hovering over any part of the control.

=======

## 2020-03-01 - Build 2003 - March 2020
* Resolved for [#39](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/39), System.ArgumentException: 'Parameter is not valid.'
* Resolved for [#30](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/30), `AutoHiddenGroup elements do not properly process AppearanceChanged events`. Credit to [MGRussell](https://github.com/MGRussell)
* Resolved for `SerializationException: Type 'System.Windows.Forms.Cursor' in Assembly 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' is not marked as serializable.`, credit to [Carko](https://github.com/Carko)
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
