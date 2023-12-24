# <img src="https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Krypton.png?raw=true"> Standard Toolkit - ChangeLog

=======

## 2024-11-xx - Build 2411 - November 2024
* Resolved [#1247](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1247),`VisualMessageBoxForm` Throws Exception when run from Example Code
* Implemented [#1220](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1220), Is it time to bring over `KryptonToast`s
    - [#1240](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1240), New fade in/out ability for `KryptonForm`s
        - **Note:** The developer must explicitly enable this feature, as it is turned off by default
    - [#1237](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1237), Core 'Toast' UI
* Implemented [#1224](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1224), **[Breaking Change]** Move `GlobalPaletteMode` into `GlobalPalette` and rename 
* Implemented [#1223](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1223), Move `UseKryptonFileDialogs` to a better designer location
* Implemented [#1222](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1222), Remove `CustomPalette` (Should be part of the palette definition)
* Implemented [#1204](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1204), Build on `KryptonCommandLinkButtons`
    - [#1218](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1218), Default 'arrow' images, depending on OS version
    - [#1217](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1217), Add support for text alignment
    - [#1216](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1216), Add support for fonts
* Resolved [#996](https://github.com/Krypton-Suite/Standard-Toolkit/issues/996), DataGridView ComboBox Adding list over and over
* Resolved [#1207](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1207), Microsoft 365 - Black (Dark Mode) Drop button is not visible
* Resolved [#1206](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1206), Remove the Font Size (as it is already covered by the actual font !)
* Resolved [#1197](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1197), `KryptonTaskDialog` Footer Images
* Resolved [#1189](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1189), The Context and Next/Pervious buttons of the `KryptonDockableNavigator` cannot be used
* Implemented [#1187](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1187), Bring over the `KryptonCommandLinkButtons`
* Resolved [#1176](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1176), KryptonProgressBar: small values escape drawing area
* Resolved [#1169](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1169), Button Spec Krypton Context Menu (Canary)
* Implemented [#1166](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1166), Use a struct to contain `KryptonMessageBox` data
* Implemented [#1161](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1161), A proper about box
* New adjusting the size of a `KryptonComboBox` also changes the `DropDownWidth`
    - Note: The `DropDownWidth` can still be set independently from the `Size` property
* Resolved [#1091](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1091), Krypton File Dialogs Missing Buttons
* Implemented [#1009](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1009), Powered by Krypton Toolkit button
    - Use `KryptonAboutToolkit.Show();` to invoke
* New `KryptonLanguageManager` is now integrated into `KryptonManager` as `ToolkitStrings`
* Removed support for .NET 6 and 7, in accordance with their official release cadences
* Support for .NET 9
* Version bump `80.xx.xx.xx` -> `90.xx.xx.xx`

=======

## 2023-11-17 - Build 2311 (Patch 1) - November 2023
* Resolved issue where an assertion is made when using `KryptonThemeComboBox` or `KryptonRibbonGroupThemeComboBox`
* Resolved issue where `Sparkle` themes would crash when using certain `ButtonSpecs`
* Resolved [#1174](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1174), Unable to adjust height of `KryptonForm` when `KryptonRibbon` is added
    - _Note:_ This disables features from [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117), until further testing is completed
    * Backed-out [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117),  Is it possible to have the KForm back colour as the KPanel colour

=======

## 2023-11-14 - Build 2311 - November 2023
* Implemented [#1117](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1117),  Is it possible to have the KForm back colour as the KPanel colour
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
* Complete [#827](https://github.com/Krypton-Suite/Standard-Toolkit/issues/827), Expose IPalette / PaletteBase as a public interface in KryptonManager
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
