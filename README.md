# <img src="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Krypton.png?raw=true"> Standard Toolkit


<!-- Start Document Outline -->

* [NuGet Information](#nuget-information)
	* [Nightly](#nightly)
	* [Canary](#canary)
	* [Stable](#stable)
	* [Documentation](#documentation)
		* [Contributing to the Standard Toolkit](#contributing-to-the-standard-toolkit)
	* [Package Support Information](#package-support-information)
		* [Release Cadence](#release-cadence)
		* [Package Descriptions](#package-descriptions)
		* [Installing Pre-Release Versions](#installing-pre-release-versions)
	* [Supporters](#supporters)
	* [Toolkit Example images](#toolkit-example-images)
* [Discord Server](#discord-server)
* [Version History](#version-history)
* [Breaking Changes](#breaking-changes)
	* [V90.00 (2024-11-12 - Build 2411 - November 2024)](v90-24-11-12--build-2411---november-2024)
		* [Support for .NET 6/7](#support-for-net-67)
		* [`KryptonButton` Properties](#kryptonbutton-properties)
		* [API Changes](#api-changes)
		* [`KryptonInputBox`](#kryptoninputbox)
		* [Building the Toolkit](#building-the-toolkit)
	* [V85.00 (2024-06-24 - Build 2406 - June 2024)](#v85-2024-06-24---build-2406---june-2024)
	* [V80.00 (2023-11-14 - Build 2311 - November 2023)](#v80-2023-11-14---build-2311---november-2023)
		* [Support for .NET Core 3.1 and .NET 5](#support-for-net-core-31-and-net-5)
		* [KryptonMessageBoxButtons](#kryptonmessageboxbuttons)
		* [Palette usages](#palette-usages)
		* [Depreciation of `KryptonManager.Strings`](#depreciation-of-kryptonmanagerstrings)
* [Known Issues & Workarounds](#known-issues--workarounds)
	* [Introduction](#introduction)
	* [What is this Repository About?](#what-is-this-repository-about)
	* [Contributing to this project](#contributing-to-this-project)
	* [Individual Components](#individual-components)
		* [Krypton Toolkit](#krypton-toolkit)
		* [Krypton Ribbon](#krypton-ribbon)
		* [Krypton Navigator](#krypton-navigator)
		* [Krypton Workspace](#krypton-workspace)
		* [Krypton Docking](#krypton-docking)

<!-- End Document Outline -->

<hr/>


# NuGet Information

## Nightly

| Module Name | Current Version | Github License | 
|---|---|---|
| <img src="https://img.shields.io/badge/Module-Toolkit-000080.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Toolkit.Nightly?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Toolkit.Nightly/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Docking-000080.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Docking.Nightly?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Docking.Nightly/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Navigator-000080.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Navigator.Nightly?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Navigator.Nightly/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Ribbon-000080.svg?style=flat-square" />         | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Ribbon.Nightly?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Ribbon.Nightly/)                 | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Workspace-000080.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Workspace.Nightly?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Workspace.Nightly/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |

## Canary

| Module Name | Current Version | Github License | 
|---|---|---|
| <img src="https://img.shields.io/badge/Module-Toolkit-yellow.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Toolkit.Canary?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Toolkit.Canary/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Docking-yellow.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Docking.Canary?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Docking.Canary/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Navigator-yellow.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Navigator.Canary?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Navigator.Canary/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Ribbon-yellow.svg?style=flat-square" />         | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Ribbon.Canary?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Ribbon.Canary/)                 | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Workspace-yellow.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/vpre/Krypton.Workspace.Canary?color=informational&label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Workspace.Canary/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |

## Stable

| Module Name | Current Version | Github License | 
|---|---|---|
| <img src="https://img.shields.io/badge/Module-Toolkit-brightgreen.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/v/Krypton.Toolkit?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Toolkit/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Toolkit (Lite)-brightgreen.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/v/Krypton.Toolkit.Lite?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Toolkit.Lite/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Docking-brightgreen.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/v/Krypton.Docking?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Docking/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Docking (Lite)-brightgreen.svg?style=flat-square" />        | [![Nuget](https://img.shields.io/nuget/v/Krypton.Docking.Lite?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Docking.Lite/)               | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Navigator-brightgreen.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/v/Krypton.Navigator?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Navigator/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Navigator (Lite)-brightgreen.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/v/Krypton.Navigator.Lite?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Navigator.Lite/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Ribbon-brightgreen.svg?style=flat-square" />         | [![Nuget](https://img.shields.io/nuget/v/Krypton.Ribbon?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Ribbon/)                 | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Ribbon (Lite)-brightgreen.svg?style=flat-square" />         | [![Nuget](https://img.shields.io/nuget/v/Krypton.Ribbon.Lite?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Ribbon.Lite/)                 | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Workspace-brightgreen.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/v/Krypton.Workspace?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Workspace/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |
| <img src="https://img.shields.io/badge/Module-Workspace (Lite)-brightgreen.svg?style=flat-square" />      | [![Nuget](https://img.shields.io/nuget/v/Krypton.Workspace.Lite?label=Version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Krypton.Workspace.Lite/)           | ![GitHub](https://img.shields.io/github/license/Krypton-Suite/Standard-Toolkit.svg?style=flat-square) |

Keep up-to-date [here](https://github.com/Krypton-Suite/Krypton-Toolkit-Suite-Version-Dashboard)

=======

## Documentation

The online help will give an overview of what the toolkit is capable of.

<a href="https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/index.html"><img src="https://img.shields.io/badge/Documentation-Online Help-9cf.svg?style=flat-square" alt="Online Help" /></a>

If you require the full API reference, you can download the latest version by clicking the link below.

<a href="https://github.com/Krypton-Suite/Help-Files/releases"><img src="https://img.shields.io/badge/Documentation-API Reference-9cf.svg?style=flat-square" alt="API Reference" /></a>

#### Contributing to the Standard Toolkit

If you are interested in contributing to the Standard Toolkit, please read this [article](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Contributing/Contributing-Guidelines.html) first.

=======

## Package Support Information

Full information about support can be found [here](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Support/Package%20Support%20Information.html)

### Release Cadence

See [Krypton Toolkit release cadence](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Support/Package%20Version%20Descriptions.html)

### Package Descriptions

To find out more about the differences between `Nightly`, `Canary` and `Stable` packages, please read this [article](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Support/Package%20Version%20Descriptions.html).

### Installing Pre-Release Versions

To find out how to install either `Canary` or `Nightly` versions, please check out this [article](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Support/How%20to%20Install%20Pre%20Release%20Packages.html).

=======

## Supporters

Development of the Krypton Standard Toolkit is supported by these generous organisations:

<table>
<tr>
	<td width="200px">
		<a href="https://www.jetbrains.com/">
		<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Supporter-Logos/jetbrains-logo.png?raw=true" /></center>
		</a>
	</td>
	<td width="200px">
		<a href="https://www.yourkit.com/">
		<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Supporter-Logos/yourkit-logo.png?raw=true" /></center>
		</a>
	</td>
</tr>
<tr>
	<td width="200px">
		JetBrains provides cutting-edge IDE and developer productivity tools.
	</td>
	<td width="200px">
		YourKit provides a market-leading intelligent <a href="https://www.yourkit.com/features/">Java Profiler</a> and <a href="https://www.yourkit.com/dotnet/features/">.NET Profiler</a>.
	</td>
</tr>
</table>

=======

## Toolkit Example images
Follow the links to see the different objects and layouts that this framework allows you to do: [Toolkit Demos](https://github.com/Krypton-Suite/Standard-Toolkit-Demos)

=======

# Discord Server

<a href="https://discord.gg/CRjF6fY" alt="Join our Krypton Toolkit community Discord server"><img src="https://img.shields.io/badge/Discord-Join%20our%20server-7289DA?logo=discord&style=flat-square" /></a>

=======

<!--# Project & item templates

<a href="https://tinyurl.com/StandardToolkitTemplates" alt="Download project & item templates"><img src="https://img.shields.io/badge/Templates-Download%20project%20%26%20item%20templates-blueviolet?style=flat-square" /></a>

=======-->

# Version History

<a href="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Documents/Changelog/Changelog.md"><img src="https://img.shields.io/badge/Version History-Changelog-brightgreen.svg?style=flat-square" /></a>

=======

# Breaking Changes

## V95.00 (2025-02-01 - Build 2502 - February 2025)
* Resolved [#1212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1842), **[Breaking Change]** `KColorButton` 'drop-down' arrow should be drawn
    - Create Scaled Drop Glyph and use for colour button and comboDrops
    - Remove the `PaletteRedirectDropDownButton`
    - Remove `KryptonPaletteImagesDropDownButton`
    - **Breaking Change**: Remove `DropDownButtonImages` from designers 

## V90.00 (2024-11-12 - Build 2411 - November 2024)
There are list of changes that have occurred during the development of the V90.00 version
- [#632](https://github.com/Krypton-Suite/Standard-Toolkit/issues/632), **[Breaking Change]** `KryptonPropertyGrid` should have a customisable back colour.
   - `KryptonPropertyGrid` now uses the State### sets like the rest of the controls.
   - Any build breaks in the designers can just be deleted, as the the colouring will be done by the `State####` equivalents
- [#1435](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1435), **Breaking Change** Take KMB back to the Winform override (Remove Checkbox etc)
- and [#1432](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1432), **Breaking Change placeholder** Copy `KryptonMessageBox` to `KryptonMessageBoxDep`
  - The introduction of new Parameters elements to the `KryptonMessageBox` is now supported in the `KryptonMessageBoxDep` class
  - This is so that the `KryptonMessageBox` gets back to being a drop in replacement for the WinForm `MessageBox`
  - And a start of the introduction of the `KryptonMessageDialog` implementation of the UWP `MessageDialog`
- [#1424](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1424), **Breaking Change** `KryptonMessageBox` does not obey tab characters like `MessageBox`
  - The optional `ContentAlignment` for a `KryptonMessageBox.Show` command is no longer possible.
- [#1356](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1356), AppButton colours don't change while switching themes
	- See https://github.com/Krypton-Suite/Standard-Toolkit/issues/1356#issuecomment-2039412890
	- `RibbonAppButton` has become `RibbonFileAppButton` 
	- Addition `RibbonFileAppTab` to hold the tab text (Defaults to `File`)
	- Colours for the `FileAppTab` have been moved into the `StateCommon` area
- [#1206](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1206), Remove the `Font Size` (as it is already covered by the actual font !)
- [#1224](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1224), Move `GlobalPaletteMode` into `GlobalPalette` and rename 
  - BaseFont is now part of the KryptonManager class, and will override the applied palette font(s)
  - `CustomPalette` must be derived from the `KryptonCustomPaletteBase` class
  - `BasePaletteMode` has been removed from `KryptonCustomPaletteBase` class
- [#124](https://github.com/Krypton-Suite/Standard-Toolkit/issues/124), When setting AllowFormChrome = false, then the Form Bar should still be Theme rendered
  - `AllowFormChrome` has been removed and replaced with `UseThemeFormChromeBorderWidth` to better explain what it is doing
  - It means that a theme can get closer to "Material Design", and that the Title bar can still be themed (And rounded)
- [#215](https://github.com/Krypton-Suite/Standard-Toolkit/issues/215), `KryptonTreeView` Multi Node Select
  - Designer values named `State####Pressed` have changed to `State#####MultiSelect` to reflect usage
  - New ReeView Designer value `MultiSelect` allows drawing of selected items and retrieval via `CheckedNodes`
- [#1268](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1268),  Many Krypton Controls have a `CornerRoundingRadius` that overrides the State#### Node Rounding values. Please remove!
  - `CornerRoundingRadius` overrides **ONLY** the `StateCommon.Border.Rounding` which is incorrect.
  - All `CornerRoundingRadius` have been removed
- [#1269](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1269),  Remove AllowFormIntegrate to give consistent experience on all supported OS's 
  - Please check the images in the issue.
  - To fix: just remove `AllowFormIntegrate` from your deisgner files
- [#1266](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1266), Since V 5.400, the QAT button is supposed to perform the close, therefore the Close Form button should not be visible
  - The RibbonAppButton has a new Designer field for setting the "Form Close Visible" to off by default

### Support for .NET 6/7
As of V90.00, support for .NET 6 and 7 has been removed due to their release cadences.


### `KryptonButton` Properties
Some properties previously found in the root such as, `ShowSplitOption`, `UseAsADialogButton`, `UseAsUACElevationButton` and `UACShieldIconSize` are now located in the `Values` section.

### API Changes
If using `KryptonAboutToolkit`, please note that this has been superceded by `KryptonAboutBox`. Or if you use `KryptonThemeBrowserForm`, it has now been moved to `KryptonThemeBrowser` as the public facing API.

### `KryptonInputBox`
The `KryptonInputBox` now uses the new `KryptonInputBoxData` API, to handle data.

### Building the Toolkit
As of V90.00 support for longer path names **will** need to be enabled if you want to build the toolkit yourself. For more details on how to do this, please follow the instructions [here](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Contributing/Allowing-for-Longer-Path-and-File-Names.html).

## V85.00 (2025-02-01 - Build 2502 (Patch 5) - February 2025)
* Resolved [#1212](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1842), **[Breaking Change]** `KColorButton` 'drop-down' arrow should be drawn
    - Create Scaled Drop Glyph and use for colour button and comboDrops
    - Remove the `PaletteRedirectDropDownButton`
    - Remove `KryptonPaletteImagesDropDownButton`
    - **Breaking Change**: Remove `DropDownButtonImages` from designers 

## V85.00 (2024-06-24 - Build 2406 - June 2024)
There are a list of changes that have occurred during the development of the V85.00 version

* [#1302](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1302), **[Breaking Change]** Font being used by "Professional" theme is pants ! 
	- The Option to use `SystemDefault` no longer exists a font rendering hint
* [#1508](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1508), **[Breaking Change]** ButtonSpec does not open assigned context menu when clicked.
	- Added property `ShowDrop`, which displays a drop-down arrow on the button.
	- When a `KryptonContextMenu` is connected the menu is shown when the button is clicked.
	- When a WinForms `ContextMenuStrip` is connected the menu is shown when the button is clicked.
	- When both type of the above ContextMenus are connected the `KryptonContextMenu` takes precedence.
	- The ButtonSpec's `Type` property does not need setting to "Context" to display the menu.
* [#1424](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1424), **[Breaking Change]** `KryptonMessageBox` does not obey tab characters like `MessageBox`   
   
## V80.00 (2023-11-14 - Build 2311 - November 2023)
There are list of changes that have occurred during the development of the V80.00 version

### Support for .NET Core 3.1 and .NET 5
As of V80.00, support for .NET Core 3.1 and .NET 5 has been removed due to their release cadences. It is strongly advised that you migrate your application to .NET 8, as the latest LTS version, or the slightly older .NET 6, if you require a more supported version. If you do not make these mitigations, the packages **will** fail to install when upgrading, if your project is configured to use either .NET Core 3.1 and .NET 5.

### KryptonMessageBoxButtons
- https://github.com/Krypton-Suite/Standard-Toolkit/issues/728:
Bring MessageBox States inline with latest .Net 6 by using a new `KryptonMessageBoxButtons` type, which is effectively the same as .NET 6 enum version of `MessageBoxButtons` but backward compatible with .NET Framework 4.6.x onwards.

### Palette usages
- `KryptonPalette` has become `KryptonCustomPaletteBase` to better signify it's usage.
- `IPalette` has been removed, and the usage of `PaletteBase` throughout the toolkit is used; to ensure consistent usage.

### Depreciation of `KryptonManager.Strings`
In a effort to support translations, `KryptonManager.Strings` is now obsolete. As such, the new `KryptonLanguageManager` will handle such strings.

=======

# Known Issues & Workarounds
- [#1109](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1109) - KryptonPropertyGrid 'jagged' text
  - Some controls can display `Jagged text` when drawing. (i.e. ComboBox contents / PropertyGrid / Extended controls )
  This is due to the usage of the Legacy API `Application.SetCompatibleTextRenderingDefault` having a value of `true`. Please set it to false.

- [#665](https://github.com/Krypton-Suite/Standard-Toolkit/issues/665) - Workaround add `net48` or `net481` to your `<TargetFrameworks>` if using .NET 6 or 7

- .NET 6/7 Designer issues - If you are experiencing designer issues with your project, please refer to [this](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Support/Designer%20Fix.html) for more information

- ***URGENT:*** If anyone experiences "Double events" such as the one found [here](https://github.com/Krypton-Suite/Standard-Toolkit/issues/666), please can you report it ASAP, so that they can be investigated. Thanks! 

=======

## Introduction
The Krypton Suite contains user interface components designed to make it quick and easy for developers to create professional looking applications. It provides the essential building blocks needed to create a consistent look and feel across all your products. You can use the built-in palettes to achieve the same appearance as industry standard applications such as Microsoft Office `2007/2010/2013` & Microsoft 365, Visual Studio 2010. Alternatively you can create your own custom palettes to create a completely unique user interface. The Krypton Suite consists of five products called ***Krypton Toolkit***, ***Krypton Ribbon***, ***Krypton Navigator***, ***Krypton Workspace*** and ***Krypton Docking***.

## What is this Repository About?
- Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & 
Ahmed-Abdelhameed have been fixing and adding more capabilities to this toolkit.
- There is also an Extensions project, which takes these base controls and add more useful complete controls (Currently outside the scope of this help). To find out more, please head to this [link](https://github.com/Krypton-Suite/Extended-Toolkit)
- All .Net Versions from 4.6.2 are catered for (interim releases, i.e. releases in-between Long Term Support (LTS) versions of .NET will **only** be supported for the duration of that particular version, usually 24 months.)
- New versions of NuGet packages can be obtained via this [link](https://www.nuget.org/profiles/Krypton_Suite), or via your package manager by searching `Krypton.`.
- New, major versions are released annually, with patches if needed released throughout that period. Version 90 is expected to release in November 2024.
- For tips on how to build the toolkit for yourself, please read the following [article](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Contributing/How-to-Build.html).

## Contributing to this project

If you want to contribute to this project, please follow [these](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/Source/Help/Output/articles/Contributing.html) guidelines. All contributions are welcome!

## Individual Components

### Krypton Toolkit
The Krypton Toolkit provides a set of basic user interface components for free. You can distribute the signed Krypton Toolkit assembly without charge or royalty with your own products. The Krypton Toolkit is great resource for speeding up development of professional looking applications. It works in tandem with the **MenuStrip**, **StatusStrip** and **ToolStrip** controls that come with .NET Framework controls. Using the Krypton Toolkit you can create a great looking application in just minutes.

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonToolkitExampleForm.gif?raw=true" /></center>

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonToolkitExampleGroup.gif?raw=true" /></center>

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonToolkitProgressBarExample.gif?raw=true" /></center>

### Krypton Ribbon
The Krypton Ribbon is designed to mimic the look, feel and operation of the ribbon control seen in the Microsoft Office `2007/2010/2013/365` applications such as Word and Excel. It provides advanced capabilities including the quick access toolbar, contextual tabs and auto shrinking groups. With rich design time support and sample code you can be up and running with the ribbon in no time at all. It integrates with the Krypton Toolkit architecture to ensure a consistent look and feel.

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonRibbonExample.gif?raw=true" /></center>

### Krypton Navigator
The Krypton Navigator is a user interface control that provides the user with a variety of ways to navigate around a set of pages. Think of it as a traditional TabControl on steroids. It has many different modes of operation allowing you to achieve exactly the right operation for your application. It integrates with the Krypton Toolkit architecture to ensure a consistent look and feel.

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonNavigatorExample.gif?raw=true" /></center>

### Krypton Workspace
The Krypton Workspace allows a document area to be created that the user can customise by dragging and dropping pages into new positions. Similar to the Visual Studio document area but with far greater flexibility and functionality. Each cell within the workspace uses an instance of the Krypton Navigator allowing a wide range of options for organising and displaying pages. It integrates with the Krypton Toolkit architecture to ensure a consistent look and feel.

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonWorkspaceExample.gif?raw=true" /></center>

### Krypton Docking
The Krypton Docking set of components allow the user to drag and drop docking pages into new locations in order to customise the organisation of the application content. It allows this in a way similar to that of Visual Studio 2008/2010. Each docking area uses an instance of the Krypton Workspace allowing a wide range of options for organising and displaying pages. It integrates with the Krypton Toolkit architecture to ensure a consistent look and feel.

<center><img src="https://github.com/Krypton-Suite/Documentation/blob/main/Assets/Standard-Toolkit/KryptonDockingExampleCustomised.gif?raw=true" /></center>