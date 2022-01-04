# <img src="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Krypton.png"> Standard Toolkit - Changelog

## 2022-01-04 - Build 2201 - January 2022
* Complete [#517](https://github.com/Krypton-Suite/Standard-Toolkit/issues/517), Warning CS3008: Identifier '_toolTipValues' is not CLS-compliant
* Fixed [#515](https://github.com/Krypton-Suite/Standard-Toolkit/issues/515), Office 365 Dark mode does not have enough contrast for disabled text
* Fixed [#511](https://github.com/Krypton-Suite/Standard-Toolkit/issues/511), KryptonRibbonGroupComboBox does not have tooltips
* Fixed [#382](https://github.com/Krypton-Suite/Standard-Toolkit/issues/382), RibbonGroupNumericUpDown does not have tooltips
* Fixed [#453](https://github.com/Krypton-Suite/Standard-Toolkit/issues/453), KryptonDataGridView's cell cannot display multiple lines when DefaultCellStyle.WrapMode set true
* Fixed [#499](https://github.com/Krypton-Suite/Standard-Toolkit/issues/499), `KDataGridView` Cell Borders
* Fixed [#502](https://github.com/Krypton-Suite/Standard-Toolkit/issues/502),KNumericUpDowner, when told to display 1 decimal place, does not display a 0 when needed
* Fixed issue while running `build-2019.cmd` would cause build errors
* Fixed [#491](https://github.com/Krypton-Suite/Standard-Toolkit/issues/491), Krypton.Toolkit.KryptonMessageBox wrong form height when YesNo or AbortRetryIgnore buttons selected (thanks to [mbsysde99](https://github.com/mbsysde99))
* Fixed [#488](https://github.com/Krypton-Suite/Standard-Toolkit/issues/488), `KryptonTextBox` DoubleClick event does not fire
* Fixed [#484](https://github.com/Krypton-Suite/Standard-Toolkit/issues/484), Using todays alpha, Active form does not show the title text when using QAT
* Fixed [#483](https://github.com/Krypton-Suite/Standard-Toolkit/issues/483), `KryptonForm` loses visibility when the form border property is set to ***None*** using certain themes (thanks to [mbsysde99](https://github.com/mbsysde99))
* Fixed [#478](https://github.com/Krypton-Suite/Standard-Toolkit/issues/478), `ThemesComboBox` has a double list
* Fixed [#473](https://github.com/Krypton-Suite/Standard-Toolkit/issues/473), The `KryptonListbox` component does not fire the double-click event
* Fixed [#459](https://github.com/Krypton-Suite/Standard-Toolkit/issues/459), Respect Maximum size for Child Forms and controls
* Fixed `WINDOWSLOGO` Option for `KryptonMessageBox` does not work for Windows 10
* Fixed combobox dropdown colours while using dark/light theme modes
* Implemented [#469](https://github.com/Krypton-Suite/Standard-Toolkit/issues/469), Instate these for all O2k10 & O2k13/365 palettes!
* Fixed [#449](https://github.com/Krypton-Suite/Standard-Toolkit/issues/449), `WINDOWSLOGO` Option for KMessagebox does not work for Windows 11
* Fixed [#402](https://github.com/Krypton-Suite/Standard-Toolkit/issues/402), KryptonInputBox cutting off prompt text
* Fixed [#433](https://github.com/Krypton-Suite/Standard-Toolkit/issues/433), `Office 2010 - Black (Light Mode)` does not do anything
* Fixed [#416](https://github.com/Krypton-Suite/Standard-Toolkit/issues/416), Artefacts when displaying the full colour dialog
* Fixed [#415](https://github.com/Krypton-Suite/Standard-Toolkit/issues/415), Colour Dialog right margin is too wide
* Fixed [#422](https://github.com/Krypton-Suite/Standard-Toolkit/issues/422), FontDialog ColourButton replacement has dot's in corners in dark theme
* Implemented [#396](https://github.com/Krypton-Suite/Standard-Toolkit/issues/396), Change the color button from automatically launching the dialog, and I can respond to an event to instead open my own dialog
* Implemented [#372](https://github.com/Krypton-Suite/Standard-Toolkit/issues/372), Office 2007 Themes last ribbon tab is not fully selectable
* Implemented [#395](https://github.com/Krypton-Suite/Standard-Toolkit/issues/395), Add ability to add form icon to common dialogs
* Implemented [#396](https://github.com/Krypton-Suite/Standard-Toolkit/issues/396), Change the color button from automatically launching the dialog, and I can respond to an event to instead open my own dialog?
* Implemented [#404](https://github.com/Krypton-Suite/Standard-Toolkit/issues/404), `KryptonInputBox` to have a default button
* Fixed [#237](https://github.com/Krypton-Suite/Standard-Toolkit/issues/237), Office 365 - Black (Dark mode) Messes up combo boxes
* Fixed [#403](https://github.com/Krypton-Suite/Standard-Toolkit/issues/403), Krypton.Toolkit.Nightly `Version="6.2109.272-alpha"` has removed code that was in 270 
* Complete [#118](https://github.com/Krypton-Suite/Standard-Toolkit/issues/118), Fix Compile Warnings and Messages
* Implemented [#85](https://github.com/Krypton-Suite/Standard-Toolkit/issues/85), Update the project names from `2019` to `2022`
* Updated code header year from `2021` to `2022`

=======

## 2021-11-08 - Build 2111 - November 2021
* Implemented [#49](https://github.com/Krypton-Suite/Standard-Toolkit/issues/49), Support for .NET 6
* Implements [#384](https://github.com/Krypton-Suite/Standard-Toolkit/issues/384), FontDialog only has 16 colours, Bring back the ColourChooser button
* Fixed [#381](https://github.com/Krypton-Suite/Standard-Toolkit/issues/381), Allow Common Dialog Titles to be changed
* Fixed [#383](https://github.com/Krypton-Suite/Standard-Toolkit/issues/383), KFontDialog shows checked items that are not applied
* Fixed [#382](https://github.com/Krypton-Suite/Standard-Toolkit/issues/382), `RibbonGroupNumericUpDown` does not have tooltips
* Fixed [#14](https://github.com/Krypton-Suite/Theme-Palettes/issues/14), Palette Designer Messageboxes are RTL
* Fixed [#376](https://github.com/Krypton-Suite/Standard-Toolkit/issues/376), Sparkle - ## (Dark mode) and Sparkle - ## (Light mode) do not change anything when selected
* New `KryptonThemeComboBox` control, switch between themes with a few clicks
* Fixed [#364](https://github.com/Krypton-Suite/Standard-Toolkit/issues/364), Multi-Monitor Splitter dragging madness
* New `KryptonColorButton` action list options
* The `KryptonColorButton` now uses the `KryptonColorDialog`
* Fixed [#361](https://github.com/Krypton-Suite/Standard-Toolkit/issues/361), Nightlies version needs to have something that allows installers to upgrade correctly
* Fixed [#358](https://github.com/Krypton-Suite/Standard-Toolkit/issues/358), Since the "Nightlies" have been created, loading the Standard toolkit project causes Visual Studio to go ape
* Fixed [#336](https://github.com/Krypton-Suite/Standard-Toolkit/issues/336), "Office 2010 And 365 Minimise Icons Change Size" on hover
* Implement [#335](https://github.com/Krypton-Suite/Standard-Toolkit/issues/335), Create 'Nightly' NuGet packages When Merges into alpha are done
* Fixed [#315](https://github.com/Krypton-Suite/Standard-Toolkit/issues/315), When adding a new KContextmenu, How do you change it's name etc
- Sort out the Callback actions that were removed
* Fixed [#323](https://github.com/Krypton-Suite/Standard-Toolkit/issues/323), `KryptonCheckButton` Click removes image
- Sort out the Callback actions that were removed
* Fixed [#316](https://github.com/Krypton-Suite/Standard-Toolkit/issues/316), Restore `KryptonKInputBox` layout to initial intent.
* Fixed [#333](https://github.com/Krypton-Suite/Standard-Toolkit/issues/333), Krypton Form - ButtonSpec (And Icon), are Cut Off when maximised
* Fixed [#313](https://github.com/Krypton-Suite/Standard-Toolkit/issues/313), KryptonMessagebox is not RTL compliant
* Fixed [#320](https://github.com/Krypton-Suite/Standard-Toolkit/issues/320), `KryptonColorButton` "SelectedRect" will not do anything but a "Square"
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
* Fixed [#245](https://github.com/Krypton-Suite/Standard-Toolkit/issues/245), `TableLayoutPanel` should be "Kryptonised"
* Implemented [#269](https://github.com/Krypton-Suite/Standard-Toolkit/issues/269), "Print Dialog" is in the Main Forms elements - Where is Kryptons' Standard themed equivalent
  - This *does not do* the PrintDlgEx, as that is buried too deep in the OS.
* Fixed [#281](https://github.com/Krypton-Suite/Standard-Toolkit/issues/281), `KryptonScrollbar`: Scroll value cannot be set 
* Fixed [#274](https://github.com/Krypton-Suite/Standard-Toolkit/issues/274), KRadioButton should use the Label(Panel) style by default 
* Fixed [#273](https://github.com/Krypton-Suite/Standard-Toolkit/issues/273), KCheckBox should use the Label(Panel) style by default
* Fixed [#271](https://github.com/Krypton-Suite/Standard-Toolkit/issues/271), CueHint text is "Bottom" clipped by default
  - And add `TextV` to allow control for multi-line text boxes
* Implemented [#265](https://github.com/Krypton-Suite/Standard-Toolkit/issues/265), "Color Dialog" is in the Main Forms elements - Where is Kryptons' Standard themed equivalent
* Fixed [#261](https://github.com/Krypton-Suite/Standard-Toolkit/issues/261), `KryptonPalette` does not work (6.0.2108.x)
* Implemented [#256](https://github.com/Krypton-Suite/Standard-Toolkit/issues/256), Sort out palette image resources
* Implemented [#243](https://github.com/Krypton-Suite/Standard-Toolkit/issues/243), "Font Dialog" is in the Main Forms elements - Where is Kryptons' Standard themed equivalent
* Fixed [#254](https://github.com/Krypton-Suite/Standard-Toolkit/issues/254), VisualStudio Themes should not be listed yet, as they cause an exception when used
* Fixed [#242](https://github.com/Krypton-Suite/Standard-Toolkit/issues/242), `KryptonMessageBox` display is off the left of the display area
* Fixed [#234](https://github.com/Krypton-Suite/Standard-Toolkit/issues/234), `KryptonDataGridViewBinary`#### Classes should **not** be in the standard toolkit
* Implement [#212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/212), The `KryptonPropertyGrid` should be moved into the `Standard-Toolkit`
* Centralised `AsssemblyInfo` and `Version` information
* Implemented [#223](https://github.com/Krypton-Suite/Standard-Toolkit/issues/223), Can the build also echo out the DateTime, after the time elapsed
* Fixed [#159](https://github.com/Krypton-Suite/Standard-Toolkit/issues/159), Office 365 tracking colours are wrong
* Fixed [#120](https://github.com/Krypton-Suite/Standard-Toolkit/issues/120), `ButtonSpec` thinks that the default of `inherit` on Style is not the default
* Fixed [#92](https://github.com/Krypton-Suite/Standard-Toolkit/issues/92), **Many** Items cannot be Dragged and dropped onto a Form in Designer (Second patch)
    - As the "Designers" use text based references, then only need to include dlls derived from  `ParentControlDesigner`
    - Resolves [Bug]: **Many** Items cannot be Dragged and dropped onto a Form in Designer
* Fixed [#64](https://github.com/Krypton-Suite/Standard-Toolkit/issues/64), `KryptonContextMenu` Designer has layout issues
* Fixed [#51](https://github.com/Krypton-Suite/Standard-Toolkit/issues/51), Minimize icon in form titlebar is not disabled
* Remove the `Typeof` in the designer attributes and use text to prevent "Pre-Loading" of the wrong `System.Designer.dll` in Multi-Target projects
* Sort out the reference assemblies information to ensure correct loading of controls in designer
* Remove references to V2.0.0.0 of the System.Design when using the `MultilineStringEditor`
* Remove nullable messages caused by `CS8618`
* New UAC option for `KryptonButton`
* All `using` statements are now kept in one global file for each project, as per the C# 10 specification

=======

## 2021-08-03 Build 2108.1 - August 2021 (Canary Update 1)
* Fixed [#230](https://github.com/Krypton-Suite/Standard-Toolkit/issues/230), ThemeManager does not populate with the new "Light / Dark" themes
* Fixed [#229](https://github.com/Krypton-Suite/Standard-Toolkit/issues/229), Cannot set button text programatically
* Fixed [#225](https://github.com/Krypton-Suite/Standard-Toolkit/issues/225), Cannot set text of a button in designer 

=======

## 2021-08-02 Build 2108 - August 2021 (Canary)
* Implement [#207](https://github.com/Krypton-Suite/Standard-Toolkit/issues/207), 'DarkMode' for `KryptonRichTextBox`/`KryptonTextBox` (Sparkle theme updates to come)
* Fixed [#150](https://github.com/Krypton-Suite/Standard-Toolkit/issues/150), Change the Default type of Theme for A KryptonLabel to be "Normal- Panel"
    - Make sure that the `NormalPanel` is the default style
    - Also Fixed the WrapLabel Style not being set correctly
* Fixed [#202](https://github.com/Krypton-Suite/Standard-Toolkit/issues/202), `KryptonGroup` transparency
* Remove the internal class Called `KryptonDataGridViewIconColumn` from design use [#27](https://github.com/Krypton-Suite/Standard-Toolkit/issues/27)
    - Put back the removed `CLSCompliant` and `ComVisible` assembly flags for backwards compatibility
    - Update the projects to comply/use the latest analysers
* RichTextBox now allows CueHint Text
* ComboBox now allows CueHint Text
* Implement [#197](https://github.com/Krypton-Suite/Standard-Toolkit/issues/197), Rounding should use `float` or `double` instead of `int`. Rounding now accepts `float` values
* New logo for both canary and stable builds
* Fixed [#138](https://github.com/Krypton-Suite/Standard-Toolkit/issues/138), `KryptonListView` throws a `System.Resources.MissingManifestResourceException`
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
* Fixed [#122](https://github.com/Krypton-Suite/Standard-Toolkit/issues/122), Placing a `KryptonStatusStrip` in the Designer causes an exception
* Implemented [#117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/117), Consolidate `using` statements
* Fixed [#106](https://github.com/Krypton-Suite/Standard-Toolkit/issues/106), 'MethodImplOptions' does not contain a definition for 'AggressiveInlining'
* Fixed [#102](https://github.com/Krypton-Suite/Standard-Toolkit/issues/102), `KryptonInputBox` does not compile
* Fixed [#99](https://github.com/Krypton-Suite/Standard-Toolkit/issues/99), There is no need to replace some functions in `KryptonCheckButtonActionList`
* Documentation [#97](https://github.com/Krypton-Suite/Standard-Toolkit/issues/97), Add descriptions & documentation
* Fixed [#92](https://github.com/Krypton-Suite/Standard-Toolkit/issues/92), **Many** Items cannot be Dragged and dropped onto a Form in Designer
* Fixed [#91](https://github.com/Krypton-Suite/Standard-Toolkit/issues/91), Cannot add items to KryptonGroupBox in designer
* Fixed [#84](https://github.com/Krypton-Suite/Standard-Toolkit/issues/84), Multi-Line Text Box does not support Hint
* Implemented [#82](https://github.com/Krypton-Suite/Standard-Toolkit/issues/82), ComboBox Should be able to have *Hint's*
* Implemented [#81](https://github.com/Krypton-Suite/Standard-Toolkit/issues/81), Can the "6.x.Lite" version only support .net48;net5.0-windows;net6.0-windows
* Implemented [#79](https://github.com/Krypton-Suite/Standard-Toolkit/issues/79), Customise 'Hint' Font
* Fixed [#77](https://github.com/Krypton-Suite/Standard-Toolkit/issues/77), When using KryptonInputBox focus is not on the edit box when it is shown
* Fixed [#71](https://github.com/Krypton-Suite/Standard-Toolkit/issues/71), No design support while using Krypton.Ribbon in .NET 5/6 
* Fixed [#54](https://github.com/Krypton-Suite/Standard-Toolkit/issues/54), .Net5 WinForm Project - Dropping a KLabel onto a KGroup or KGroupBox causes an Exception in Designer

=======

## 2021-06-26 - Build 2106.2 - June 2021 (Update 2)
* Fixed [#167](https://github.com/Krypton-Suite/Standard-Toolkit/issues/167), Latest Canary of `KryptonInputBox` is not usable!! 

=======

## 2021-06-04 - Build 2106.1 - June 2021 (Update 1)
* Fix NuGet package descriptions
* Disabled `KryptonListView`, `KryptonToolStrip` and `KryptonStatusStrip` - to return in a future update

=======

## 2021-06-02 - Build 2106.1 - June 2021 (Update 1 - Canary)
* Fix for [#129](https://github.com/Krypton-Suite/Standard-Toolkit/issues/129), Canary NuGet description for `Lite` is incorrect

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
* Fix [#52](https://github.com/Krypton-Suite/Standard-Toolkit/issues/52), Nuget Package descriptions need to be consistent (And published!)

=======
## 2021-03-01 - Build 2103 - March 2021
* Fix for [#35](https://github.com/Krypton-Suite/Standard-Toolkit/issues/35), It is still not possible to create a `KryptonStatusStrip` on a form at design time.
* Fix for `KryptonPalette` component, courtesy of 'gwni'
* Added properties to action lists such as `ShortFont`, `LongFont`, `Font`, `SelectedColour` etc. More to come.
* Fixed bug where the `KryptonTextBox` does not repaint after altering the `Hint` property
* Implemented [#14](https://github.com/Krypton-Suite/Standard-Toolkit/issues/14), Access Fonts via Action Lists
* Removed `KryptonStatusStrip` & `KryptonToolStrip` (these are now part of the `Krypton.Toolkit.Suite.Extended.Tool.Strip.Items` module, as of build **2104**)

=======

## 2021-01-05 - Build 2101 - January 2021

* Updated year references from `2020` to `2021`
* Fix for [#19](https://github.com/Krypton-Suite/Standard-Toolkit/issues/19), "Office 365 Silver" theme `StatusBar` drag glyph, is not visible
* Fix for [#20](https://github.com/Krypton-Suite/Standard-Toolkit/issues/20), Selected text in ComboBox is drawn in a different font

=======

## 2020-11-01 - Build 2011 - November 2020
* Shortened namespaces, the toolkit will now use namespaces such as `Krypton.Ribbon` instead of the older `ComponentFactory.Krypton.Ribbon` to align with package names
* Support for .NET 5
* Versions now begin with `5.550`
* Long term support (LTS) packages are available for developers who need extra time to migrate their projects over to the new namespace format or .NET 5
* ***Important! If you're upgrading from a older legacy version, please uninstall your currently installed packages BEFORE intalling these packages. Please also follow [this](https://github.com/Krypton-Suite/Standard-Toolkit-Online-Help/blob/master/Source/Documentation/Standard%20Toolkit%20Migration%20Guide.md) guidence, as the toolkit now uses different namespaces!***

=======

## 2020-08-12 - Build 2008 - August 2020
* Fix for [#16](https://github.com/Krypton-Suite/Standard-Toolkit/issues/16), Taskbar Width is offset in Docking operations. Fix courtesy of [sneusse](https://github.com/sneusse).

=======

## 2020-06-01 - Build 2006 - June 2020
* Implemented [#8](https://github.com/Krypton-Suite/Standard-Toolkit/issues/8), Is it possible to only minimize FloatingWindow in DockingManager?
* Fix for [#9](https://github.com/Krypton-Suite/Standard-Toolkit/issues/9), Cannot place `KryptonStatusStrip` on a Form
* Fix for [#12](https://github.com/Krypton-Suite/Standard-Toolkit/issues/12), AllowButtonSpecToolTipPriority 
    - If the parent Item has tooltips, and the button spec has tooltips, then the default is show both when hovering over the button spec. This can be disabled by setting AllowButtonSpecTooltipPriority to true, so that only 1 tooltip is displayed when hovering over any part of the control.

=======

## 2020-03-01 - Build 2003 - March 2020
* Fix for [#39](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/39), System.ArgumentException: 'Parameter is not valid.'
* Fix for [#30](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/30), `AutoHiddenGroup elements do not properly process AppearanceChanged events`. Credit to [MGRussell](https://github.com/MGRussell)
* Fix for `SerializationException: Type 'System.Windows.Forms.Cursor' in Assembly 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' is not marked as serializable.`, credit to [Carko](https://github.com/Carko)
* Updated website URL in code files
* Updated minor version number from `490` to `500` in code files

=======

## 2020-02-07 - Build 2002.1 - February 2020 (Update 1)
* Fix [#28](https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core/issues/28), KryptonForm crashing Visual Studio
* Added support for `KryptonInputBox` textbox text alignment

=======

## 2020-02-01 - Build 2002 - February 2020

* Support for all frameworks .NET 3.5 to 4.8 inclusive
* Support for .NET Core LTS (currently 3.1)
* Changed `490` to `500`
* Builds from now on will be labelled as `YYMM`
