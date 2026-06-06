# V105-LTS-test Diff Source Map

Comparison: `D:\github\StdTk-105` vs `D:\github\Standard-Toolkit-260529`, excluding `.git`, `.vs`, `obj`, `bin`, and `Bin`.
Policy: source commits/PRs are evidence; restore, purge, clean, backup, tree-copy, broad revert, alignment, and catch-up commits are not direct replay sources.

Generated: 2026-06-06 07:26:03 +02:00

## Resolution Update

Updated: 2026-06-06 after author-preserving commits `8b2caf3c8`, `4fb7be4c3`, `38171ac2b`, `1d5ef7dc0`, `beb7aad2c`, `40988f8c2`, `971e374a0`, and `058708093`.

The remaining source-content gaps from the `StdTk-105` comparison were traced to original source commits and replayed without using restore, purge, catch-up, or tree-copy commits as sources. The only scoped comparison differences left are:

| Count | Category | Decision |
|---:|---|---|
| 16 | Whitespace-only differences | Kept the recovered files with trailing whitespace normalized so `git diff --check` remains valid. These include designer/resx/comment-header whitespace only; comments and source intent are otherwise preserved. |
| 2 | `.csproj.user` files | Skipped as local IDE/user state: `Krypton.Toolkit.Utilities.csproj.user` and `TestForm.csproj.user`. |
| 0 | Source-content gaps | No remaining non-whitespace content differences in the scoped source/script comparison. |

## Workflow Resolution Update

Updated: 2026-06-06 after commits `8b2caf3c8` and `4fb7be4c3`.

The `.github/workflows` comparison initially had 13 differences: seven modified workflows and six workflows missing from `V105-LTS-test`. These were recovered from source-backed providers:

| Provider | Files / Intent | Decision |
|---|---|---|
| `f26442759` | V105 `.github` sync state for automation and TestForm workflows | Replayed workflow file state for `alpha-backup-sync.yml`, PR/issue automation workflows, `build-testform.yml`, and `codeql.yml`. |
| `f2c842cd5` | PR #3620 Canary LTS Release workflow MSBuild quoting fix | Replayed build/release workflow state for `build.yml`, `canary-lts-release.yml`, `canary.yml`, `nightly.yml`, and `release.yml`. |
| `d5b29dc00` | Templates release VSIX manifest workflow source state | Replayed `templates-release.yml`; intentionally kept `actions/upload-artifact@v4` and no `Apply-TemplatesNuGetPackage.ps1` reference. |

Final workflow comparison result: 12 exact matches against `StdTk-105`, one whitespace-only residual in `auto-label-pr-backup.yml` from trailing-whitespace normalization for `git diff --check`, and zero content gaps. Restore, purge, reset, and catch-up commits were not used as workflow replay sources.

## Templates Resolution Update

Updated: 2026-06-06 after commit `38171ac2b`.

The `Templates` comparison initially showed 22 files present in `StdTk-105` and missing from `V105-LTS-test`. Blob traces included catch-up/RTM/restore holders, but the matching source provider for the full tree was PR #3517:

| Provider | Files / Intent | Decision |
|---|---|---|
| `4c4e7d592` | `* Add Krypton Visual Studio template VSIX (#3517)` | Replayed the complete `Templates` tree from this source commit. |

Final `Templates` comparison result: 20 exact matches against `StdTk-105`, two whitespace-only residuals in designer files from trailing-whitespace normalization for `git diff --check`, and zero content gaps. Restore, purge, RTM, reset, and catch-up commits were not used as template replay sources.

## Authorship Rewrite Update

Updated: 2026-06-06 after local rewrite from safety ref `backup/V105-LTS-test-before-author-fix-20260606`.

Replay commits were regrouped by source author so recovered source changes do not all appear authored by `tobitege`. Peter Wagner commits use `Peter Wagner <peterwagner@live.co.uk>`, verified from commit `d436bc4db2d852a4c52331000f65ef678227c469`.

| New Commit | Author | Scope |
|---|---|---|
| `8b2caf3c8` | `github-actions[bot] <github-actions[bot]@users.noreply.github.com>` | V105 `.github` sync workflow automation from `f26442759`. |
| `4fb7be4c3` | `Peter Wagner <peterwagner@live.co.uk>` | Workflow fixes from `f2c842cd5` and `d5b29dc00`. |
| `38171ac2b` | `Peter Wagner <peterwagner@live.co.uk>` | Visual Studio template sources from PR #3517 source commit `4c4e7d592`. |
| `1d5ef7dc0` | `Lesandro Gotardo <148582027+lesandrog@users.noreply.github.com>` | Ribbon/mouse source changes from Lesandro-authored provider commits. |
| `beb7aad2c` | `giduac <96108132+giduac@users.noreply.github.com>` | FocusLost, validation, menu/task dialog, palette border, and MDI source fixes from giduac-authored providers. |
| `40988f8c2` | `Jorge A. Avilés <102550248+mcpbcs@users.noreply.github.com>` | Composite `KryptonCustomPaletteBase.cs` recovery from #3351 plus #2706 co-author note. |
| `971e374a0` | `tobitege <10787084+tobitege@users.noreply.github.com>` | Tobitege-authored build, docking, and VisualForm source fixes. |
| `058708093` | `Peter Wagner <peterwagner@live.co.uk>` | Peter-authored script, project, accessibility, palette, TestForm, and control source fixes. |

## Origin Groups

| Count | Candidate Source |
|---:|---|
| 13 | * Update license headers |
| 7 | * Use a phased approach |
| 6 | 3493-V105-Build-fixes (#3609) |
| 6 | * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` |
| 4 | * Build enhancements |
| 3 | 2641-V110-MDI-Child-Windows-do-not-react-when-minimized (#2642) |
| 2 |  |
| 2 | * Move the public facing version of KryptonExceptionDialog to Krypton.Utilities |
| 2 | * KForm fixes |
| 1 | 3580-V105-TLS Fix docking auto-sizing (#3630) |
| 1 | 2862-V105-LTS-VisualForm flicker fix in .NET10 |
| 1 | 2801-V105-KryptonTextBox-Validating-event-fires-twice (#3272) |
| 1 | 2570-V110-Added-Tab-scrolling-with-mouse-over-Ribbon's-GroupsArea-Update (#2770) |
| 1 | 1929-V105-LTS-Fix folder browser dialog drawing (#3624) |
| 1 | * Update scripts for #3377 |
| 1 | * Rename `Krypton.Utilities` -> `Krypton.Toolkit.Utilities` |
| 1 | * Lines when using `CueHint` for `KryptonTextBox` (V110) |
| 1 | * KComboBox's should follow the DateTimePicker layout(s) |
| 1 | * Floating Window is Empty after dragging from Docked state - V105 |
| 1 | * Fix race condition |
| 1 | * Does Krypton provide the correct UIA providers for the controls it "Hides" |
| 1 | * Add VSIX package for Krypton Visual Studio templates |
| 1 | * Add missing toolbox bitmap images |
| 1 | * Ability for developers to set the highlight colour |
| 1 | * A few fixes |
| 1 | * `SaveConfigToArray` and `LoadConfigFromArray` not working correctly |
| 1 | * `KryptonManager` `TypeInitializationException` on .NET Framework |
| 1 | * `KryptonContextMenuComboBox` & `KryptonContextMenuProgressBar` need to be implemented |
| 1 | * #3012 Bugfix (V105 LTS) |
| 1 | Clarify "(none)" Font import behavior |
| 1 | Update FloatingWindowTest.cs |

## Per-File Map

| File | Kind | Candidate Source | Date |
|---|---|---|---|
| Scripts/Build/build-canary.cmd | Different | 60af54502 3493-V105-Build-fixes (#3609) | 2026-05-31 |
| Scripts/Build/build-installer.cmd | Different | 60af54502 3493-V105-Build-fixes (#3609) | 2026-05-31 |
| Scripts/Build/build-lts.cmd | Different | 60af54502 3493-V105-Build-fixes (#3609) | 2026-05-31 |
| Scripts/Build/build-nightly-custom.cmd | Different | 60af54502 3493-V105-Build-fixes (#3609) | 2026-05-31 |
| Scripts/Build/build-nightly.cmd | Different | 60af54502 3493-V105-Build-fixes (#3609) | 2026-05-31 |
| Scripts/Build/build-stable.cmd | Different | 60af54502 3493-V105-Build-fixes (#3609) | 2026-05-31 |
| Scripts/CI/Populate-WebView2.ps1 | OnlyStdTk105 | a72e12e67 * Fix race condition | 2026-05-29 |
| Scripts/CI/StandardToolkitNupkgGuard.ps1 | OnlyStdTk105 | 90584905e * Update scripts for #3377 | 2026-05-11 |
| Scripts/Current/build-canary.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Scripts/Current/build-installer.cmd | Different | 77fe50a06 * Build enhancements | 2026-05-24 |
| Scripts/Current/build-lts.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Scripts/Current/build-nightly-custom.cmd | Different | 77fe50a06 * Build enhancements | 2026-05-24 |
| Scripts/Current/build-nightly.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Scripts/Current/build-stable.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Scripts/VS2022/build-canary.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Scripts/VS2022/build-installer.cmd | Different | 77fe50a06 * Build enhancements | 2026-05-24 |
| Scripts/VS2022/build-lts.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Scripts/VS2022/build-nightly-custom.cmd | Different | 77fe50a06 * Build enhancements | 2026-05-24 |
| Scripts/VS2022/build-stable.cmd | Different | 8f9fbd794 * Use a phased approach | 2026-05-25 |
| Source/Krypton Components/Directory.Build.targets | Different | 11ff44195 * `KryptonManager` `TypeInitializationException` on .NET Framework | 2026-05-19 |
| Source/Krypton Components/Krypton Toolkit Suite 2022 - VS2022.sln | Different | 17df4d237 * Add VSIX package for Krypton Visual Studio templates | 2026-05-25 |
| Source/Krypton Components/Krypton.Docking/Dragging/DockingDragManager.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Docking/Elements Impl/KryptonDockingSpace.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Ribbon/Controls Ribbon/KryptonRibbon.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Ribbon/View Draw/ViewDrawRibbonGroupButtonText.cs | Different | ce106331c * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` | 2026-04-16 |
| Source/Krypton Components/Krypton.Toolkit.Utilities/Krypton.Toolkit.Utilities.csproj.user | OnlyStdTk105 |   |  |
| Source/Krypton Components/Krypton.Toolkit/ButtonSpec/ButtonSpecManagerBase.cs | Different | 8bf671e3f * KForm fixes | 2026-05-19 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonComboBox.cs | Different | ce106331c * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` | 2026-04-16 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonCustomPaletteBase.cs | Different | 02c493906 Clarify "(none)" Font import behavior | 2026-04-26 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonDataGridView.cs | Different | ce106331c * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` | 2026-04-16 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonExceptionDialog.cs | Different | bf37c3350 * Move the public facing version of KryptonExceptionDialog to Krypton.Utilities | 2025-12-20 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonForm.cs | Different | 8bf671e3f * KForm fixes | 2026-05-19 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonLinkWrapLabel.cs | Different | ce106331c * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` | 2026-04-16 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonRichTextBox.cs | Different | e2fcd68a5 * Lines when using `CueHint` for `KryptonTextBox` (V110) | 2026-05-09 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonSystemMenu.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonTaskDialog/KryptonTaskDialog.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonTaskDialog/KryptonTaskDialogFormProperties.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonToggleSwitch.cs | Different | 2d28c9e53 * Add missing toolbox bitmap images | 2026-05-15 |
| Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualControlBase.cs | Different | 89025dcca 2801-V105-KryptonTextBox-Validating-event-fires-twice (#3272) | 2026-03-28 |
| Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualExceptionDialogForm.cs | Different | bf37c3350 * Move the public facing version of KryptonExceptionDialog to Krypton.Utilities | 2025-12-20 |
| Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualForm.cs | Different | fb37014ec 2862-V105-LTS-VisualForm flicker fix in .NET10 | 2026-06-02 |
| Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualMultilineStringEditorForm.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualSimple.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualSimpleBase.cs | Different | 1223f2952 3580-V105-TLS Fix docking auto-sizing (#3630) | 2026-06-01 |
| Source/Krypton Components/Krypton.Toolkit/Designers/UX/KryptonContextMenuCollectionForm.cs | Different | 6849afa73 * `KryptonContextMenuComboBox` & `KryptonContextMenuProgressBar` need to be implemented | 2026-04-14 |
| Source/Krypton Components/Krypton.Toolkit/General/Accessibility/KryptonCheckedListBoxAccessibleObject.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/General/MouseControlFinder.cs | OnlyStdTk105 | 802381729 2570-V110-Added-Tab-scrolling-with-mouse-over-Ribbon's-GroupsArea-Update (#2770) | 2025-12-19 |
| Source/Krypton Components/Krypton.Toolkit/Palette Base/PaletteBorder/PaletteBorder.cs | Different | ce106331c * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` | 2026-04-16 |
| Source/Krypton Components/Krypton.Toolkit/Palette Base/PaletteBorder/PaletteFormBorder.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Professional/PaletteProfessionalSystem.cs | Different | ce106331c * Move methods out of `GlobalStaticVariables` into 'new' `GlobalStaticFunctions` | 2026-04-16 |
| Source/Krypton Components/Krypton.Toolkit/View Base/Internal/InternalSearchableExceptionWinFormsTreeView.cs | Different | 13e821c55 * Ability for developers to set the highlight colour | 2025-12-05 |
| Source/Krypton Components/PR_DESCRIPTION.md | OnlyStdTk105 | 20dc04863 * #3012 Bugfix (V105 LTS) | 2026-02-16 |
| Source/Krypton Components/TestForm/AccessibilityTest.Designer.cs | Different | a7ff2126c * Does Krypton provide the correct UIA providers for the controls it "Hides" | 2026-01-09 |
| Source/Krypton Components/TestForm/BasicToastNotificationTest.cs | Different | 522ea9bb0 * Rename `Krypton.Utilities` -> `Krypton.Toolkit.Utilities` | 2026-05-12 |
| Source/Krypton Components/TestForm/BasicToastNotificationTest.Designer.cs | Different | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/TestForm/ComboBoxDateTimePickerConsistencyDemo.cs | OnlyStdTk105 | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/TestForm/ComboBoxDateTimePickerConsistencyDemo.Designer.cs | OnlyStdTk105 | 9f330a040 * KComboBox's should follow the DateTimePicker layout(s) | 2026-01-07 |
| Source/Krypton Components/TestForm/DockingConfigSaveLoadTest.cs | OnlyStdTk105 | cf0827f25 * Update license headers | 2026-04-01 |
| Source/Krypton Components/TestForm/DockingConfigSaveLoadTest.Designer.cs | OnlyStdTk105 | 12114fb34 * `SaveConfigToArray` and `LoadConfigFromArray` not working correctly | 2026-01-11 |
| Source/Krypton Components/TestForm/DockingConfigSaveLoadTest.resx | OnlyStdTk105 | 6dbb45665 * A few fixes | 2024-02-07 |
| Source/Krypton Components/TestForm/FloatingWindowTest.cs | OnlyStdTk105 | aa5170dd0 Update FloatingWindowTest.cs | 2026-01-02 |
| Source/Krypton Components/TestForm/FloatingWindowTest.Designer.cs | OnlyStdTk105 | be4521250 * Floating Window is Empty after dragging from Docked state - V105 | 2026-01-02 |
| Source/Krypton Components/TestForm/MdiWindow.cs | OnlyStdTk105 | 4ede127fd 2641-V110-MDI-Child-Windows-do-not-react-when-minimized (#2642) | 2025-11-26 |
| Source/Krypton Components/TestForm/MdiWindow.Designer.cs | OnlyStdTk105 | 4ede127fd 2641-V110-MDI-Child-Windows-do-not-react-when-minimized (#2642) | 2025-11-26 |
| Source/Krypton Components/TestForm/MdiWindow.resx | OnlyStdTk105 | 4ede127fd 2641-V110-MDI-Child-Windows-do-not-react-when-minimized (#2642) | 2025-11-26 |
| Source/Krypton Components/TestForm/StartScreen.cs | Different | e10aa3199 1929-V105-LTS-Fix folder browser dialog drawing (#3624) | 2026-05-31 |
| Source/Krypton Components/TestForm/TestForm.csproj.user | OnlyStdTk105 |   |  |
