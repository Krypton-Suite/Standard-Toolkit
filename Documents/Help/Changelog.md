# <img src="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Krypton.png"> Standard Toolkit - Changelog

## 2101-0x-0x - Build 2108.1 - xxx 2021 (Canary)
* Fixed [#92](https://github.com/Krypton-Suite/Standard-Toolkit/issues/92) (Second patch)
  - As the "Designers" use text based references, then only need to include dlls derived from  `ParentControlDesigner`
  - Resolves [Bug]: **Many** Items cannot be Dragged and dropped onto a Form in Designer
* Remove the `Typeof` in the designer attributes and use text to prevent "Pre-Loading" of the wrong system.Designer dll in Multi-Target projects
* Sort out the reference assemblies information to ensure correct loading of controls in designer
* Remove references to V2.0.0.0 of the System.Design when using the `MultilineStringEditor`
* Remove nullable messages caused by `CS8618`
* Implemented [#223](https://github.com/Krypton-Suite/Standard-Toolkit/issues/223), Can the build also echo out the DateTime, after the time elapsed

=======

## 2021-08-01 Build 2108 - August 2021 (Canary)
* Implement [#207](https://github.com/Krypton-Suite/Standard-Toolkit/issues/207), 'DarkMode' for `KryptonRichTextBox`/`KryptonTextBox` (Sparkle theme updates to come)
* Fixed [#150](https://github.com/Krypton-Suite/Standard-Toolkit/issues/150)
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
* New `KryptonMessageBoxManager` control, now you can configure a `KryptonMessageBox` through the designer
* Fixed [#122](https://github.com/Krypton-Suite/Standard-Toolkit/issues/122), Placing a `KryptonStatusStrip` in the Designer causes an exception
* Implemented [#117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/117), Consolidate `using` statements
* Fixed [#106](https://github.com/Krypton-Suite/Standard-Toolkit/issues/106), 'MethodImplOptions' does not contain a definition for 'AggressiveInlining'
* Fixed [#102](https://github.com/Krypton-Suite/Standard-Toolkit/issues/102), `KryptonInputBox` does not compile
* Fixed [#99](https://github.com/Krypton-Suite/Standard-Toolkit/issues/99), There is no need to replace some functions in `KryptonCheckButtonActionList`
* Documentation [#97](https://github.com/Krypton-Suite/Standard-Toolkit/issues/97), Add descriptions & documentation
* Implemented [#93](https://github.com/Krypton-Suite/Standard-Toolkit/issues/93), For a `KryptonMessageBox`: Please describe difference between ***owner*** and ***parentWindow*** parameters
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
  * If the parent Item has tooltips, and the button spec has tooltips, then the default is show both when hovering over the button spec. This can be disabled by setting AllowButtonSpecTooltipPriority to true, so that only 1 tooltip is displayed when hovering over any part of the control.

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