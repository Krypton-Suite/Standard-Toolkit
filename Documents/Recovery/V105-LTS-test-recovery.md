# V105-LTS-test Recovery Ledger

Branch: `V105-LTS-test`
Base commit: `68de6697e1283eb119d1b81ba4b6a9bc5fc8baa6` (`Merge pull request #2952 from Krypton-Suite/revert-2951-master-workflows`)
Policy: replay valid PR changes from original PR merge/head commits or PR diffs; do not use restore, purge, tree-copy, or backup-restore commits as normal replay input.
Detail policy: this ledger records PR-level decisions and compact scope notes only. Per-file details stay in local scratch data or can be regenerated from the listed SHAs with `git show --name-only`.
Protected local file: `recovery-alpha-local-backup-20260603.bundle` remains untouched and untracked.

## Decision Meanings

- `pending replay`: valid candidate, not replayed yet.
- `applied`: PR content replayed and committed on `V105-LTS-test`.
- `already present`: PR content was already present in the selected base or an earlier replayed PR.
- `manual resolution`: PR content was replayed with a documented conflict resolution tied to PR intent.
- `audit catch-up contents against source PRs`: catch-up/parity PR; recover unique valid content only from source PRs, not from catch-up tree state.
- `skip by policy pending source PR proof`: restore/purge/tree-copy candidate; not replayed unless original valid source PR proof is found.

## Pre-Base Context

These merged PRs are in the Jan 30-31 window around the selected base. They are context for why the branch starts at `68de6697e`, not replay inputs after that base unless later audit finds missing V105 content.

| PR | Merged At | Base | Decision | Merge SHA | Head SHA | Head Ref | Title | Notes |
|---|---|---|---|---|---|---|---|---|
| #2940 | 01/30/2026 08:03:57 | alpha | included in base or pre-base context | 164abf0b43827b0653629518e2468b0329216a87 | 4ff9f93c91b915a527c0d6e684f0791329f91b28 | alpha-nightly-workflow-fixes | * Nightly workflow fixes | |
| #2941 | 01/30/2026 08:20:33 | alpha | included in base or pre-base context | 684146568fbb8da826e7c379fd86544208abb8e1 | c5abb45f72fb006bf853068984de7e1fb678ffcf | alpha-nightly-workflow-fixes | * Nightly build fixes | |
| #2942 | 01/30/2026 08:34:31 | alpha | included in base or pre-base context | 905de1cf6d731c2c97d0b58ae5d3e3cfb3705262 | acc6cf529d3278a70f8e342c88155e599a18b3e7 | alpha-nightly-workflow-fixes | * Review references | |
| #2947 | 01/31/2026 09:36:56 | master | included in base or pre-base context | 84455af884b8ab14decb93540c632ce6eeed5953 | 3e2c3598672c28eb65cf4725d115f3dd55f6f78e | master-workflow-updates | * Workflow updates | |
| #2945 | 01/31/2026 09:43:54 | alpha | included in base or pre-base context | fde8bae5f03ad7227a917bdbd00aa6b0a5a31d23 | 7983e7789d7de793efb194baa10085fd6e91cef9 | 2944-bug-messagebox-sizing-issues | * Messagebox sizing issues | |
| #2943 | 01/31/2026 09:45:43 | alpha | included in base or pre-base context | d75685b1ffec54b812b33ae8adf5e51bb557a936 | 2397b345f7f18b440686dc3d30685b45066e8f42 | alpha-nightly-workflow-fixes | * WebView2 Enhancements | |
| #2936 | 01/31/2026 09:46:46 | alpha | included in base or pre-base context | b0318d7c867c73ff64b759f536bb9482cb8a2e30 | 3877ffb977d9cec6bae3a67147fec14fd53bf089 | 2933-bug-docking-not-working-correctly | * Docking not working correctly | |
| #2948 | 01/31/2026 09:52:16 | gold | included in base or pre-base context | 7fa7e9df54cdf2eef1688bf0b1ed9ba3450d7131 | 84455af884b8ab14decb93540c632ce6eeed5953 | master | * Catchup 31/01/2026 | |
| #2951 | 01/31/2026 16:02:32 | master | included in base or pre-base context | de43f20a75c43c2421140bcfd903e26da6b6efd8 | 7d3c2a492fa2a939bdd6a0152f10c5f4fe8d39db | master-workflows | * Master workflows | |
| #2952 | 01/31/2026 16:07:30 | master | included in base or pre-base context | 68de6697e1283eb119d1b81ba4b6a9bc5fc8baa6 | baad883d6df9486f45e4914204d83b3818925624 | revert-2951-master-workflows | Revert "* Master workflows" | |
| #2946 | 01/31/2026 16:29:42 | alpha | included in base or pre-base context | 98e06fb5d1e3fe7b6c022a2e62a1f5de3e49c55a | 327c66e1433664a37d134a2690b028cb59de0a73 | 2921-bug-double-ribbon-is-drawn | * Ribbon MDI Fix | |
| #2953 | 01/31/2026 17:13:36 | alpha | included in base or pre-base context | 30ee016ab4b57808cead116809199e2ce5353c21 | ab96cbb7472518327dffe932ae83a7d07c381865 | revert-2943-alpha-nightly-workflow-fixes | Revert "* WebView2 Enhancements" | |
| #2950 | 01/31/2026 17:16:34 | alpha | included in base or pre-base context | 561b3ddb01dfb4cfb89545a7c2a4ae3020aa81b5 | 7d3c2a492fa2a939bdd6a0152f10c5f4fe8d39db | master-workflows | * Catchup 31/01/2026 | |
| #2955 | 01/31/2026 17:24:17 | canary | included in base or pre-base context | b20efd005a951cb74db9be1a71053738d5c05566 | 27536eba53285a139eb4df476a460f08107b31e8 | canary-workflows | * Align workflows | |
| #2961 | 01/31/2026 19:28:56 | alpha | included in base or pre-base context | 2aef9074895ff71d3c982cc9dcedee2e11c3bee1 | 327c66e1433664a37d134a2690b028cb59de0a73 | 2921-bug-double-ribbon-is-drawn | * Double ribbon is drawn | |

## Direct V105-LTS PR Ledger

| PR | Merged At | Base | Decision | Merge SHA | Head SHA | Head Ref | Title | Notes |
|---|---|---|---|---|---|---|---|---|
| #2963 | 02/01/2026 10:33:43 | V105-LTS | manual resolution | c6cd51ea8173dc831b04981f1598eccbd6aa1a42 | fb2916d15c7a5c7012c60d578c17e8a863556293 | V105-LTS-2935-bug-maximized-mdi-window-form-border-is-drawn-on-the-second-monitor | * Maximized MDI window form border is drawn on the second monitor (V105) | Applied as `2da9c7d9c`; changelog conflict resolved to PR side. |
| #2966 | 02/01/2026 10:36:17 | V105-LTS | applied | 792a98fde7a13640e22a716fa7645ebbb4762d16 | f4bbd4baa2f4a36711a792c9ea849da102093692 | V105-LTS-2962-feature-request-set-license-header-via-editorconfig | * Set license header via `editorconfig` (V105 LTS) | Applied as `34f006a0b`. |
| #2975 | 02/04/2026 07:49:14 | V105-LTS | applied | fb9e05826cd3cf0ee3c9058fd93fddc4787fec59 | 84ff47ec5e3624012d8193b1a3d9626fc67584ac | V105-LTS-2132-bug-drag-feedback-artifact | * Drag feedback artifact (V105 LTS) | Applied as `07b316d01`. |
| #2980 | 02/04/2026 08:16:45 | V105-LTS | audit catch-up contents against source PRs | 802fb2067e778275a5dddc00ff75fed7a29513e4 | d03e74a557ee673af6b102e571457f8a42c40506 | master | * Branch parity with `master` 04/02/2026 | Catch-up/parity PR; not replayed directly. |
| #2981 | 02/04/2026 08:18:14 | V105-LTS | audit catch-up contents against source PRs | 29d68da82140576ed53c730975af28a07f21ddb8 | 0de5942d53d09745c373d1dd3f33147b133b6eb6 | revert-2980-master | Revert "* Branch parity with `master` 04/02/2026" | Catch-up/parity PR; not replayed directly. |
| #2992 | 02/06/2026 07:51:26 | V105-LTS | applied | 9d29a1890e28d7dd112b67e9daf274e0cb4b3b5b | 136a997031d9fe9d4dae6797f302907958493b2a | V105-LTS-2927-bug-populate-from-base-task-freezes-visual-studio | * "Populate from Base" task freezes Visual Studio (V105 LTS) | Applied as `0eb1fd923`. |
| #3001 | 02/07/2026 09:42:17 | V105-LTS | applied | 76e45154078fbc0ec32d7891d3bfb273555dddd4 | db404dd5dde6826cd474c390e8ededccb08a801d | V105-LTS-2129-question-toolkit-9025255---drop-down-arrows | * Drop Down Arrows (V105 LTS) | Applied as `334455ebd`. |
| #2997 | 02/07/2026 09:46:40 | V105-LTS | applied | aa725564be5a01bfdd1ed1215603fb6c4644aab9 | 010118bbc4f1e054b004b6e79cf6f1e6b398316c | V105-LTS-2913-bug-krypton-toolkit-causes-low-quality-text-rendering-in-stimulsoft-preview-window | * Krypton Toolkit causes low-quality text rendering in Stimulsoft pre… | Applied as `024e7899a`. |
| #3010 | 02/08/2026 08:31:51 | V105-LTS | applied | 7b673943bb342e14104e75b1f6d38450114588f7 | e2188cbd880eae38350601612bda2865d6935406 | V105-LTS-972-bug-office-2013-microsoft-365-control-box-items-are-not-flat | * `Office 2013` & `Microsoft 365` control box items are not 'flat' (V… | Applied as `839e986c9`. |
| #3017 | 02/08/2026 17:13:55 | V105-LTS | applied | d21b4cbbf44126ba677fe0955600a2440738d8ee | 50bb4f0ebd433ad337718e86862429ccd3e17ffd | V105-LTS-3012-bug-space-between-form-close-button-and-right-edge-of-the-form | * Space between form close button and right edge of the form (V105 LTS) | Applied as `2652b9ae0`. |
| #3040 | 02/15/2026 07:45:23 | V105-LTS | applied | 6f93d4309a057eed80fbcafb58eef09cd5d65f1d | 47918099aeb01098fafac8891f4e4cd9481b71ad | V105-LTS--2103-bugfeature-request-ensure-that-kryptonform-properly-supports-rtlltr-titlebar-fix | * Fix for #2103 - Titlebar (V105 LTS) | Applied as `0af0bb94b`. |
| #3045 | 02/15/2026 10:07:18 | V105-LTS | applied | 1fa83ae33a82faf055704b572783fcdde454efa6 | d84c593de91cac4f9ae6665d67e0acc80e91127d | 3043-feature-request-canary-lts-release-workflow | * Changelog entry for #3043 | Applied as `7b1eccc94`. |
| #3049 | 02/16/2026 07:53:00 | V105-LTS | applied | 18f1805991c0fa814ae59e03ab3fc14dfef1eb29 | 1316904bb7fdc71223a4f1e3afebd65f92b79697 | V105-LTS-2968-feature-request-move-all-rtl-specific-dialogs-to-use-the-feature-fully | * Move **all** RTL specific dialogs to use the feature fully (V105 LTS) | Applied as `1cc8d75ec`. |
| #3057 | 02/17/2026 08:02:02 | V105-LTS | manual resolution | c5f0bcd2e2ada977fe6fc73be3c0051465c0f530 | e70600bb2b67ddd0f6f912c0c927f085095019e4 | V105-LTS-Workflows | * Workflows (V105 LTS) | Applied as `050126adb`; workflow conflicts resolved to PR side for WebView2 prerelease lookup. |
| #3055 | 02/17/2026 08:04:23 | V105-LTS | applied | e2f8b88850a8b6421a4ee9dd8aa17313c973ae3b | 08a148c581d12119b1accecc0b63e4e97a1606e7 | V105-LTS-776-feature-request-ability-to-set-a-number-of-custom-colours-for-kryptoncolorbutton | * Ability to set a number of custom colours for `KryptonColorButton` … | Applied as `2cfb2068a`. |
| #3062 | 02/17/2026 08:06:26 | V105-LTS | audit catch-up contents against source PRs | 11375b85b23d25f6b8112e3ebcded1f61886bf45 | fca15ddbdbb7ab9e2e50cb0139f224b815adc7ce | master | * Catchup 17/02/2025 | Catch-up/parity PR; not replayed directly. |
| #3070 | 02/18/2026 08:04:44 | V105-LTS | audit catch-up contents against source PRs | 0a92b437d51e9e9b083471aa15048d0a02bdd3ce | fb00350532a231bed105933057a863924237ec29 | master | * Catchup 18/02/2025 | Catch-up/parity PR; not replayed directly. |
| #3059 | 02/18/2026 08:11:34 | V105-LTS | manual resolution | 98e9ef38f7a4442a0c1ccdae513a2347abcdeed7 | 3928dddff52bb92a51acdd2c1bdcb7c52fda65f2 | V105-LTS-3012-bug-space-between-form-close-button-and-right-edge-of-the-form-Bugfix | * #3012 Bugfix (V105 LTS) | Applied as `9a41dd5f9`; recovered KryptonForm.cs and palette theme metric changes; workflow/context noise not replayed. |
| #3079 | 02/19/2026 07:50:41 | V105-LTS | audit catch-up contents against source PRs | c9edb0c0819504752b17efb09322b765ffacf972 | 7543041723ecefc04c1ba045d29734bb6a37fcd9 | master | * Catchup 19/02/2025 | Catch-up/parity PR; not replayed directly. |
| #3085 | 02/20/2026 07:54:18 | V105-LTS | applied | c4cd932b3b4e545641d10e24d26e3d3cccba24d9 | 7b6bdf470e9bc1d72d0610c86dcb508d998248e9 | V105-LTS-2922-bug-winforms-borderless-form-briefly-displays-system-title-bar-on-startup | * WinForms borderless form briefly displays system title bar on start… | Applied as `dd62fcb41`. |
| #3083 | 02/20/2026 07:58:48 | V105-LTS | applied | 7b3ab71e5f1fe22df129de4032cb02eda4f3011d | 3955273b6f0029e1559fd8cc108875b183c58ea9 | V105-LTS-3011-bug-regression-cannot-drag-maximized-form-from-the-top-once-again | * Cannot drag maximized form from the top (once again) (V105 LTS) | Applied as `1c7fb0637`. |
| #3081 | 02/20/2026 08:02:47 | V105-LTS | applied | 5e1ccca992cdb0899546610edb94a5a43f97f3a3 | 023c3d7790aae305a1b416ae62380cabcb2d8e92 | V105-LTS-3075-feature-request-tooltips-with-extendedinfinite-timeout | * Tooltips with extended/infinite timeout (V105 LTS) | Applied as `9dfb2f494`. |
| #3088 | 02/21/2026 09:57:12 | V105-LTS | manual resolution | 444e9b4ebafedc50050f0537cd537dbfcef45195 | 33eb0f589917494f24a3ea75cb2722cd322e79d4 | V105-LTS-3013-bug-maximized-forms-size-exceeds-the-screens-working-area | * Maximized form's size exceeds the screen's working area (V105 LTS) | Applied as `29b8a5c28`; KryptonForm conflict resolved to later PR side for maximized bounds behavior. |
| #3098 | 02/22/2026 09:41:19 | V105-LTS | applied | 3a0416210dd9ecfdfaaaaa56bd3ac2d1fca97d0f | fc7f3d89110136fe2024caeb58f82ba6addbc0da | V105-LTS-3075-feature-request-tooltips-with-extendedinfinite-timeout | * Tooltips with extended/infinite timeout (V105 LTS) | Applied as `8c4f0523b`. |
| #3105 | 02/22/2026 10:21:37 | V105-LTS | applied | 1261af4ca1bfffd242261d82602d72591b233af0 | 92661d46ff2fda11d5bca0a0aa6e167d71314403 | V105-LTS-3103-bug-name-of-the-theme-is-not-being-serialized-in-xml-files | * Name of the theme is not being serialized in XML files (V105 LTS) | Applied as `7c7418cec`. |
| #3108 | 02/23/2026 08:42:16 | V105-LTS | applied | b85f4c8cbaf92d361e954ed55e4e9c6b344fcc5c | 865179359bee79c25c493bd32fb4061a14dad9f0 | V105-LTS-3101-bug-colors-in-theme-exporting-xmls-is-missing | * Colors in theme exporting XMLs is missing (V105 LTS) | Applied as `660027b72`. |
| #3110 | 02/24/2026 07:49:53 | V105-LTS | manual resolution | 5dda46224c440e4c3ea2e47b2ad3130bf9f61aa5 | 51041afa9b41ab4fb09e7bd8c03fd1593c3bf6e9 | V105-LTS-615-feature-request-controls-should-have-min-design-heights-be-the-same | * Controls should have "min design heights" be the same (V105 LTS) | Applied as `eb5a04511`; VisualControlBase field insertion conflict resolved to PR side with comments intact. |
| #3129 | 02/28/2026 12:16:39 | V105-LTS | applied | d7e296452b5c7fae06fa0f0468d7c7f83a906a80 | ad4a284d085d5e3a255370dcb3ab10105358d5a4 | V105-LTS-3117-feature-request-disabling-focus-when-clicking-on-a-kryptonbutton | * Disabling focus when clicking on a `KryptonButton` (V105 LTS) | Applied as `f0001785b`. |
| #3131 | 02/28/2026 12:18:39 | V105-LTS | applied | cad765b1cc79f05aef73032250dae2fc80f2cbc4 | 1f94f874586b863c3088250c54d9bff1a51ec15e | V105-LTS-3123-bug-kryptoncombobox-dropdownwidth-doesnt-resize-with-the-control | * `KryptonComboBox` DropDownWidth doesn't resize with the control (V105) | Applied as `388fbc9c0`. |
| #3122 | 02/28/2026 17:12:43 | V105-LTS | applied | 1dc748ade26430cb0d5f4363d2dde274c2d158c8 | fe96ca2931ed56834c56c379fbe089c82f47932d | V105-LTS-3075-feature-request-tooltips-with-extendedinfinite-timeout-value-fix | * Fix for infinite tooltips (V105 LTS) | Applied as `d0f343239`. |
| #3136 | 03/01/2026 15:27:14 | V105-LTS | applied | b9e60a0542006673507f178dda75a1560d046c3a | c95197313ca3221db8de8965fd0d5fcee686b9f5 | V105-LTS-3124-bug-docking-loading-configuration-while-form-is-hidden-remover-splitter | * Docking: Loading configuration while form is hidden remover splitte… | Applied as `336de6f6f`. |
| #3143 | 03/02/2026 08:44:22 | V105-LTS | applied | c23bb718052ded09c49198eb80d9dbaacb8acba4 | 5ea4ebf0026a8af8fba4ad8b7125e480c956ebbd | V105-LTS-3113-feature-request-textbox-item-for-kryptoncontextmenu-items | * TextBox item for KryptonContextMenu-Items (V105 LTS) | Applied as `248e1aae0`. |
| #3145 | 03/03/2026 08:05:07 | V105-LTS | applied | 18923f1671953cdc947e30668eac8e2050c77ef1 | 89031701eae07adc8fac4057bedc4ce5bfda55a5 | V105-LTS-3113-feature-request-textbox-item-for-kryptoncontextmenu-items | * #3113 - Pt. ii (V105) | Applied as `511bc48bc`. |
| #3172 | 03/11/2026 08:02:55 | V105-LTS | applied | 53e8391fcdfc7420e5432dbaea0591a77a771953 | 079d7fbcc77d1de4a0e16845432a11c0a1828472 | V105-LTS-3163-bug-form-icon-misplaced-in-contextual-tabs | * Form Icon Misplaced in Contextual Tabs (V105 LTS) | Applied as `30f4f5386`. |
| #3206 | 03/12/2026 18:09:51 | V105-LTS | applied | 2ef34770b86b6789f72cc226a3739b8171d52f61 | f9e5f6f234a8f64a4c5acdab9a644e0480f5d0a4 | 3201-V105-Fix-potential-memory-leaks-in-KryptonDataGridViewRatingColumn | 3201-V105-Fix-potential-memory-leaks-in-KryptonDataGridViewRatingColumn | Applied as `ffacaa6e5`. |
| #3214 | 03/13/2026 04:10:44 | V105-LTS | applied | 3eaa3f388f8ec8c959e4c4c6da25db02b894982a | 7c1342539776f2440fc3008260febef321d6758e | revert-3206-3201-V105-Fix-potential-memory-leaks-in-KryptonDataGridViewRatingColumn | Revert "3201-V105-Fix-potential-memory-leaks-in-KryptonDataGridViewRatingColumn" | Applied as `4214ed1b3`. |
| #3220 | 03/13/2026 10:15:20 | V105-LTS | applied | 3728de1bd9bf0546cadf94827ccb9dc0df591d1a | 7412d4ede6a6f0300ecced945dab53c56d63ac9b | 3201-V105-Fix-potential-memory-leaks-in-KryptonDataGridViewRatingColumn | 3201 v105 fix potential memory leaks in krypton data grid view rating column | Applied as `3721dab3e`. |
| #3224 | 03/14/2026 19:09:34 | V105-LTS | audit catch-up contents against source PRs | 0f183ea89486d453e6c880a56635bc09943c8bde | d9215b816e67d92ba76cd129a73a43f4eb25adbe | master | * Catchup 14/03/2026 | Catch-up/parity PR; not replayed directly. |
| #3212 | 03/16/2026 07:41:31 | V105-LTS | applied | d1ddaa2a898019dfc67def8441e6515c986c499b | 022243a5270e47f79ade099f817d536c6b6ce768 | V105-LTS-3173-feature-request-use-pattern-matching | * Use pattern matching | Applied as `4407b31ff`. |
| #3150 | 03/18/2026 20:35:43 | V105-LTS | applied | 5321de70e3d48b55e8b3adcd46469379b209996a | de4a63f3bdc28a7a083d254278bc8c0c252015bd | V105-LTS-3013-Maximized-forms-size-exceeds-the-screens-working-area | V105-LTS #3013 : Maximized form's size exceeds the screen's working area  | Applied as `f46e2c92c`. |
| #3179 | 03/18/2026 20:38:35 | V105-LTS | applied | 2171b4aa2f8c8c337ebe44e4a7bed290a9642adf | 7f68cdb20ec03155d721c51a4586ad7404c953e2 | 3164-V105-Font-property-values-are-being-serialized-depending-of-the-current-culture-in-exported-XML-theme-file | Fixes [3164]-V105-LTS Font property values are being serialized depending of the current culture in exported XML theme | Applied as `2e7af95da`. |
| #3269 | 03/27/2026 07:47:48 | V105-LTS | audit catch-up contents against source PRs | 254aa9f59b7b96154030eb0bee0668143b6cc0d5 | a72911dda057563a2ba10bb1dc5e1e040902f7d1 | master | * Catchup 27/03/2026 | Catch-up/parity PR; not replayed directly. |
| #3274 | 03/28/2026 17:16:33 | V105-LTS | manual resolution | 38d41813782bbb18c60e7d6a2231b883160dbfd3 | c6dce2dc849e7aab2585009bb90d2a022c31eb71 | V105-testform-startscreen-paletteviewer-udate | V105-testform-startscreen-paletteviewer-udate | Applied as `9210ce87c`; StartScreen button-list conflict resolved to PR generic `CreateButton<TForm>` list while preserving the PR's PaletteViewer update. |
| #3272 | 03/28/2026 17:16:52 | V105-LTS | applied | 89025dccafc87ac95ae918472924ef77e74b70bb | db9d76a6bd6e6a9fecec7a0bc1829c12cc10c84e | 2801-V105-KryptonTextBox-Validating-event-fires-twice | 2801-V105-KryptonTextBox-Validating-event-fires-twice | Applied as `53283750a`; prerequisite #2805 was replayed from original PR source first because #3272 only removed TODO markers around that fix. |
| #3281 | 03/29/2026 06:58:03 | V105-LTS | audit catch-up contents against source PRs | cd36f24c0756397051ac4f464dfbc8e763e60ea7 | 69d3c3c6bccacd9d288fae03b3e742d992aad4ca | master | * Catchup 29/03/2026 | |
| #3254 | 03/29/2026 07:23:20 | V105-LTS | applied | db21fc64d936fd7ee7d4de6357f846f9df315f26 | 80ae22e8b55f59e3a75c084da3f1a7b01ce3e7b6 | V105-LTS-3225-bug-ribbon-large-button-image-to-text-separator-not-dpi-scaled | * Ribbon large button image-to-text separator not DPI-scaled (V105) | Applied as `16099711f`. |
| #3251 | 03/30/2026 07:38:22 | V105-LTS | applied | 7e77bdf2275e29df49bbfe7a47d5d7d8484afbe5 | ea2440d01719b271741e97fdf398afd714854daa | V105-LTS-3227-bug-kryptondockingmanagerloadconfigfromarray-throws-exception | `KryptonDockingManager.LoadConfigFromArray` throws exception (V105) | Applied as `15c55ee73`. |
| #3284 | 04/01/2026 09:58:41 | V105-LTS | applied | 1772131a37500561020debf1acfb5498828f9199 | 990c8ae4082a242b20f429dc51fab73197b267a9 | V110-LTS-missing-brackets | * Fix double bracket issue | Applied as `a98814a51`; prerequisite #2834 was replayed from original PR source first because #3284 modifies its RichTextBox formatting test. |
| #3307 | 04/05/2026 07:11:54 | V105-LTS | applied | 1514baf184c30120012857e9096b65c70c8bf1d8 | b2b7efeb672f065571c1574157a765a3915e361f | V105-LTS-3256-bug-tree-view-event-is-crashing | * Tree View Event is Crashing (V105 LTS) | Applied as `53f7e5503`. |
| #3310 | 04/11/2026 08:11:08 | V105-LTS | applied | 95b8d65d0d921ad7398e636593896596c21874bf | fe86b85ed25421d94203c677e50d646ddecd1b6c | V105-LTS-1002-feature-request-implement-actionlists-for-file-dialogs | Implement `ActionLists` for file dialogs (V105 LTS) | Applied as `e8219cfd0`. |
| #3318 | 04/12/2026 06:21:50 | V105-LTS | audit catch-up contents against source PRs | 5b67a51917d8c52b94685a1f5944280d2554b9dd | 0305e0c71733ec95be6a5f8def5cf54e5ef506d7 | master | * Catchup 12/04/2026 | |
| #3335 | 04/19/2026 07:25:24 | V105-LTS | audit catch-up contents against source PRs | b0faf03453e075fce3fe9fd58eb97b1a25c23b53 | 37fa89fabbb8e84fdb405bdf7326d325c1b1d0aa | master | * Catchup 19/04/2026 | |
| #3340 | 04/21/2026 06:41:35 | V105-LTS | applied | 78d4aa911012e5b0aaec76e02e27b424101a7ecb | 634f49cf8d139e188b755647f76d7647464286e9 | V105-LTS-3283-bug-kryptonthemecombobox-does-not-change-theme-when-selectedindex-is-changed | * `KryptonThemeComboBox` does not change theme when `SelectedIndex` (… | Applied as `5bd2c9953`. |
| #3355 | 04/26/2026 07:10:56 | V105-LTS | applied | 0b5cc68f26a386b4ab57096553c5d632fc48ea4a | 304aee0f2d78298ce8b1cfbc4f6dfb4bdc38a49a | V105-LTS-3342-bug-kryptontextbox-text-flickers-when-resizing-the-control-multiline-active | * `KryptonTextBox`, text flickers when resizing the control (multilin… | Applied as `a7c838977`. |
| #3351 | 04/27/2026 09:04:40 | V105-LTS | applied | 27f70055d3b77fc25b86f47b3470cc65e0c5d8e4 | 02c4939065acd61fc0d4944b70deb32df53b0187 | 3164-V105-Font-property-values-are-being-serialized-depending-of-the-current-culture-in-exported-XML-theme-file | v105-Fix Font null handling in XML serialization and deserialization | Applied as `71e41c772`. |
| #3374 | 04/30/2026 07:26:41 | V105-LTS | audit catch-up contents against source PRs | 221ffe00aa3bbb13a6878140ad30d41e1651aa33 | 4ce6e4d54a24a55334909f3d09d307bbeaca8fea | master | * Catchup 30/04/2026 | Catch-up/parity PR; not replayed directly. |
| #3392 | 05/03/2026 09:07:02 | V105-LTS | audit catch-up contents against source PRs | 14d8d5cc43df8d43571308130a7cb740e9e16f6a | 3b0c6d7343090ac36cc77e414a509ea33e435547 | master | * Catchup 03/05/2026 | Catch-up/parity PR; not replayed directly. |
| #3396 | 05/03/2026 11:08:58 | V105-LTS | audit catch-up contents against source PRs | 0276b046a13aefd0b05d4fd45ba57884b2697cf8 | 1dfcb12d2e7f9f387884030995f004059cddffab | master | * Catchup 03/05/2026 - ii | Catch-up/parity PR; not replayed directly. |
| #3387 | 05/05/2026 07:18:44 | V105-LTS | applied | be0c1be8a72ba27b1fc901d5f1388291529e1915 | b136cd67970a8a69a46d864ee18c6315bf62e2f6 | V105-LTS-3385-bug-memory-retention-in-krypton-controls-via-systemevents | * Memory Retention in Krypton Controls via SystemEvents (V105 LTS) | Applied as `67fdba072`. |
| #3402 | 05/06/2026 07:07:35 | V105-LTS | applied | 5f47597eaab01c8057cabc3dd280aff93ea502d0 | 80cd1bcad836485a945193379ec2925867e42398 | V105-LTS-3381-bug-vertical-text-centering-on-a-rounded-corner-kryptonbutton | * Vertical text centering on a rounded-corner `KryptonButton` (V105 LTS) | Applied as `f012c6e6d`. |
| #3415 | 05/07/2026 16:10:25 | V105-LTS | audit catch-up contents against source PRs | 84bb89e11117ad363ea8436e1c777edf15d360f0 | 70e1a0a4f80482b35200b99e40122c47e7a55728 | master | * Catchup 07/05/2026 | Catch-up/parity PR; not replayed directly. |
| #3420 | 05/07/2026 16:11:20 | V105-LTS | audit catch-up contents against source PRs | 90098ed1363b0c1f8020266141aa77cf4e053d44 | 62416d28cf108ce18e268519351c18907cf0e4c8 | revert-3415-master | Revert "* Catchup 07/05/2026" | Catch-up revert; not replayed directly. |
| #3370 | 05/07/2026 16:19:35 | V105-LTS | manual resolution | 9e675a19b3d903cbb3dfb3c362d7f0f0814422f9 | 8c24ec50aae80c2af5e193303d92089633bea863 | V105-LTS-3365-bug-kryptonlinklabel-autosize-not-shrinking | * `KryptonLinkLabel` 'Autosize' Not Shrinking (V105 LTS) | Applied as `6383fb1b0`; VisualSimpleBase conflict resolved to PR sizing logic with `layoutProposedSize` comments intact. |
| #3427 | 05/10/2026 07:08:29 | V105-LTS | applied | f80d7050f07a18c8749cff8623daab6e0e0cd113 | 59b7573c8b4c3936351a57dd229f8046c8d9d2e8 | V105-LTS-3382-bug-lines-when-using-cuehint-for-kryptontextbox | * Lines when using `CueHint` for `KryptonTextBox` (V105 LTS) | Applied as `c65531a9a`. |
| #3441 | 05/12/2026 06:51:54 | V105-LTS | applied | dd06620845f9f8fcc88a4e9f06b2d21e9cc59124 | 3313cdbfb34767fb6d62d469434059dc7ad1da49 | V105-LTS-3383-bug-white-inner-border-artifacts-appear-on-kryptonbutton-when-hovering-statetracking-rounding-issue | * White inner border artifacts appear on `KryptonButton` when hoverin… | Applied as `580185153`. |
| #3473 | 05/19/2026 09:51:05 | V105-LTS | applied | 50071f1ef103120f812ff76be270fbc5e267e1d1 | cae65720f0384547349940b88723920574da758f | V105-LTS-3367-bug-kryptontextbox-buttonspecs-flicker-on-mouse-hover | * `KryptonTextBox` ButtonSpecs Flicker on mouse hover (V105 LTS) | Applied as `5e58fdbbe`. |
| #3471 | 05/20/2026 07:26:54 | V105-LTS | applied | 9884513b774580ce11c5b5320b03f7145a25c7c5 | ba4f1d07d05a9d93fcec7c4127c4619b015bb5a2 | V105-LTS-3451-bug-unable-to-add-child-controls-into-headergroup-v1 | * Unable to add child controls into `KryptonHeaderGroup` (V105 LTS) | Applied as `ce67e37a1`. |
| #3510 | 05/24/2026 09:52:20 | V105-LTS | audit catch-up contents against source PRs | eae56f350fba516cd8154405ec08806faf3390f1 | bc8614ec15141230130b7628d6b17b7495070e44 | master | * Catchup 24/05/2026 | Catch-up/parity PR; not replayed directly. |
| #3498 | 05/25/2026 06:54:07 | V105-LTS | manual resolution | 65b7640a89e9b0ef51bb85084c22af2791c34040 | dc99b9a63cc7ab1a324b44ae66ae7a2db710709e | V105-LTS-3282-bug-kryptontreeview-items-flicker-when-selectedclicked | * KryptonTreeView items flicker when selected/clicked (V105) | Applied as `d635646c3`; Directory.Build.props conflict resolved to include PR multi-target output/intermediate paths. |
| #3520 | 05/26/2026 06:32:40 | V105-LTS | applied | 69cf9f46d41d3c6051a9a3f521fd4a67ae6ed072 | 7425e86171d977dafbcb18cb0fea913e736a147a | 3090-V105-LTS-Fix-ModernBuild | #3090-V105-LTS Fix ModernBuild: support VS2022/Current script profiles | Applied as `fd1702b78`. |
| #3533 | 05/26/2026 18:01:26 | V105-LTS | audit catch-up contents against source PRs | da3d90e6d5ff4ce93fb90e3033aa38a4dc9ba722 | 0a1343c02d71157301a511e5218f6f47771275d4 | master | * Catchup 26/05/2026 | Catch-up/parity PR; not replayed directly. |
| #3542 | 05/28/2026 06:55:20 | V105-LTS | applied | d1a09183f39625c4cbcd6ddd113de8827abc1a0d | b325306dd324e5e04e4ed08c40d2b2777edeed68 | 3227-docking-followup | 3227-V105-Docking fix followup | Applied as `3af9553e8`. |
| #3548 | 05/28/2026 13:46:38 | V105-LTS | applied | 6994b00c125b8f7aef85d234e1a94ab6a27a311a | 7e8bbb1646c6721890f3a6122a601f323e8c77ce | V105-LTS-3514-feature-request-include-readmemd-in-nuget-packages-changelogentry | * Adds changelog entry for #3514 (V105) | Applied as `c9ec0efab`. |
| #3552 | 05/28/2026 14:33:38 | V105-LTS | audit catch-up contents against source PRs | e19487369d55967cddcd1a74b3cddc43577aea7e | 3304d3185f304fcff847eb5fc1708ce9ac4eb32d | master | * Catchup 28/05/2026 | Catch-up/parity PR; not replayed directly. |
| #3565 | 05/28/2026 17:01:50 | V105-LTS | audit catch-up contents against source PRs | 93384f45e084aea8e9e37ebe69e4623ffa29ed2f | bbbd41467eed22b7f7869dbe7fb318f5ba467240 | master | * Catchup 28/05/2026 - ii | Catch-up/parity PR; not replayed directly. |
| #3570 | 05/29/2026 06:26:35 | V105-LTS | applied from PR head | 5fa6792904695bc3cf5e6a1bd270a2eb61d38246 | 7e0e631f305e19946eaf64cc71c7e81fec4f83a1 | 3447-V105-toolbox-bitmaps | 3447-V105-Add toolbox bitmaps | Merge object was missing locally; applied available PR head as `3e7984113`. |
| #3577 | 05/29/2026 12:48:35 | V105-LTS | audit catch-up contents against source PRs | 65f12cba15418ea704c8521619ea9524e73e36e0 | cc5a40fdf303aa64e079c7896863da41e54f5e75 | master | * Catchup - 29/05/2026 | Catch-up/parity PR; not replayed directly. |
| #3605 | 05/31/2026 07:44:43 | V105-LTS | applied | 62c1718de5cd96f3cb47c15bf76ef86b15c76b7b | d3eebf3c52c8d11a06b725649002b53c37c2ae20 | 3598-V105-fix-leak | 3598-V105-lts Fix KryptonContextMenu disposal leaks | Applied as `f1ef8ab48`. |
| #3624 | 05/31/2026 17:25:54 | V105-LTS | applied | e10aa319944c983bc90f8b7616d21034587a5dcd | 333b0870d78e4fbee60ed1bef4b9875fc6716de1 | 1929-V105-LTS-kfolderbrowser-drawing | 1929-V105-LTS-Fix folder browser dialog drawing | Applied as `525163b25`. |
| #3623 | 05/31/2026 18:02:35 | V105-LTS | applied | 6717b60a181160460acbdeddbeb36469683f1a3a | 7069020a072b20a4330e99226fb5aa52938648ce | 1976-V105-LTS-menuitem-theme-settings | 1976-V105-LTS-Fix MenuItem theme settings | Applied as `225e0bd2b`. |
| #3622 | 05/31/2026 18:35:09 | V105-LTS | applied | 12a19158b5e15e8df84253b96fed4e01853f8010 | 468822b58ff7ffab2374598530fd5babc4e56202 | 397-V105-LTS-context-menu-colours | 397-V105-LTS-Fix normal context menu palette colours | Applied as `9d77ea426`. |
| #3609 | 05/31/2026 19:00:11 | V105-LTS | manual resolution | 60af545025c41cf22e7784f3fcd6ea53d650185b | d6fc0ef6ba7b409804fc5dd55d7caaba1f29e967 | 3493-V105-Build-system-fixer-after-restore | 3493-V105-Build-fixes | Applied as `083771d70`; script/project conflicts resolved to direct PR side, with #3520 ModernBuild ScriptProfile verified present. |
| #3620 | 05/31/2026 19:54:27 | V105-LTS | empty/not applicable | 4bd6c1ad53ac5b3892b1dbfa6024d5cb67f3395a | aba0e63e4a9c10b8f18a1bc569aeb07780aa226e | V105-LTS-3618-bug-canary-lts-release-workflow-fails | * Canary LTS Release workflow fails (V105 LTS) | Inspected and skipped: recovered workflows do not use the vulnerable `dotnet msbuild "$proj"` version probe; `canary.yml` is absent and was not resurrected. |
| #3630 | 06/01/2026 06:39:27 | V105-LTS | manual resolution | 1223f295299e0ae62250b75ce29e5017381ea637 | a0844fef6269709d9cf1c456712155960e8c11d9 | 3580-V105-tls-fix-dock-sizing | 3580-V105-TLS Fix docking auto-sizing | Applied as `7a5f8e94b`; changelog conflict resolved to include #3580 only, not skipped #3620's #3618 line. |
| #3633 | 06/01/2026 08:19:33 | V105-LTS | applied | 16df965d6f22eca8215e48c5ca612d902fbf150e | ad0903c820f04c8c8dcb06b5d517f7c0d42fd7d5 | V105-LTS-3550-feature-request-auto-complete-issues-changelogentry | * Auto complete issues (V105 LTS) | Applied as `c948ae2dc`. |
| #3643 | 06/01/2026 14:49:24 | V105-LTS | skip by policy | e93194441a4dca6dda43810710199dcfd8de1af2 | b6d5fb16e30d845c9aacfd1c6ff020cc323e3267 | 3641-V105-LTS-restore-catchup | 3641-V105-LTS-restore-catchup | Restore/catch-up PR; not replayed directly. |
| #3652 | 06/02/2026 14:49:41 | V105-LTS | applied | d1b80e88ad684d79733d6dda6b829a2a60f1f2b1 | 24beaf406a6ae90d1fd3cf338909f155b98d31d4 | 2926-V105-scrollbar-display-fix-followup | 2926-V105-LTS-Scrollbar display fix followup | Applied as `120b92d32`. |
| #3656 | 06/02/2026 14:50:00 | V105-LTS | applied | accf1d5fc0d2418c1bab6944605b09f58af08dcd | fb37014ecf5b56f2e1d76e6bf621ed1cd1ffa6b1 | 2862-V105-LTS-visualform-resize-flicker-fix | 2862-V105-LTS-VisualForm flicker fix in .NET10 | Applied as `968dc8221`. |

## Master PRs For Catch-Up Audit

These master PRs are not replay inputs by default. They are checked only when a V105 catch-up PR claims to bring master content into V105-LTS.

| PR | Merged At | Base | Decision | Merge SHA | Head SHA | Head Ref | Title | Notes |
|---|---|---|---|---|---|---|---|---|
| #2947 | 01/31/2026 09:36:56 | master | pending replay | 84455af884b8ab14decb93540c632ce6eeed5953 | 3e2c3598672c28eb65cf4725d115f3dd55f6f78e | master-workflow-updates | * Workflow updates | |
| #2951 | 01/31/2026 16:02:32 | master | pending replay | de43f20a75c43c2421140bcfd903e26da6b6efd8 | 7d3c2a492fa2a939bdd6a0152f10c5f4fe8d39db | master-workflows | * Master workflows | |
| #2952 | 01/31/2026 16:07:30 | master | apply/audit as revert PR | 68de6697e1283eb119d1b81ba4b6a9bc5fc8baa6 | baad883d6df9486f45e4914204d83b3818925624 | revert-2951-master-workflows | Revert "* Master workflows" | |
| #2970 | 02/03/2026 08:29:47 | master | pending replay | a59b26c4a45840668c08680b8fae8e2bd2fa05a0 | 0010153b1fa690425e0b369523a8b69e5ee86411 | master-automated-alpha-branch-backup | * Automated `alpha` backup | |
| #2976 | 02/04/2026 08:10:27 | master | pending replay | d03e74a557ee673af6b102e571457f8a42c40506 | 272ef9a5b9bc553d86703c0a95412e5d45201f60 | master-alpha-sync-backup-fixes | * Alpha backup workflow improvements | |
| #2983 | 02/05/2026 07:39:39 | master | pending replay | b3f7450306f65475dbbb4a74cc49625c6bd1cb4f | 5e7afebf35898ccf3016f6dc6099dce72c40b062 | master-alpha-sync-backup-fixes | * Add features to backup | |
| #3002 | 02/07/2026 09:40:49 | master | pending replay | fefa1155cd04acb75901fee1f0e9c4c1f4fd1da5 | ba61ace0f1639fbc2a7de5b9f81353543c8a745a | master-backup-sync-workflow | * Sync workflow fixes | |
| #3019 | 02/09/2026 07:53:00 | master | pending replay | 31c34d28adc9dd675a4f4423d4f92bcc3cc0d7f1 | eebd68ba495f2488ed812e4b09956e88bc273f2c | main-bug-report-template-update | * Update labels | |
| #3028 | 02/14/2026 08:46:14 | master | pending replay | 2e71ae7aae7a8df879f757c2032b0ecec72138ca | 5b8dbc1c49c87c892432825206a18fbd5f319ce4 | master-workflows-net-11-support | * Update workflows to support .NET 11 | |
| #3044 | 02/15/2026 09:15:26 | master | pending replay | fca15ddbdbb7ab9e2e50cb0139f224b815adc7ce | 48046942634dbea9a142a56d77d1900df909ff7c | 3043-feature-request-canary-lts-release-workflow | * Canary LTS release workflow | |
| #3063 | 02/18/2026 08:00:33 | master | pending replay | fb00350532a231bed105933057a863924237ec29 | eb0f5eddffe0358b219cb0f95fca247782a87ce1 | master-webview2-sdk-workflow-block-fix | * Fix WebView2 SDK block | |
| #3071 | 02/19/2026 07:46:32 | master | pending replay | 7543041723ecefc04c1ba045d29734bb6a37fcd9 | 25c36f57a1f661355d25176ed49c8030d4ca1cb5 | master-get-version-workflow-fix | * Fix `Get Version` workflow step | |
| #3120 | 02/27/2026 09:12:04 | master | pending replay | 4c24593e56222635446830eec17ddf02cab307dd | 065cecedd638cb1c65e21352743318fbd2160f4f | master-canary-release-workflow | * Canary release workflow | |
| #3162 | 03/10/2026 07:54:51 | master | pending replay | 0bc78f0b637361ceedab299bf905fcdd12947d2b | bcc648f58153864bb15515ed210eb013c39737ee | master-build-workflows | * Fix warnings for .NET 10 | |
| #3182 | 03/11/2026 08:01:41 | master | pending replay | f582ccef0f4d97441d72da5cf9c1e1a315475dc7 | 8d78d3842843892f4e45069eb19ecc0f1c1d26d7 | master-build-workflows | * Runners update | |
| #3188 | 03/11/2026 09:09:12 | master | pending replay | 0377cb10d4b99f23701a4c9c8d061c8eb9f0c290 | 31f7f4f23c9d757396602dee81b60a54a08af50d | master-build-workflows | * Fix for WebView2 SDK | |
| #3192 | 03/11/2026 09:20:04 | master | pending replay | 6857ce1db5f1521e9719e5d60a1dc6b2cf970923 | bff687d840b58acfef456cfaae2f1204e4e41bf6 | master-build-workflows | * Remove errant argument | |
| #3196 | 03/11/2026 09:34:11 | master | skip by policy pending source PR proof | 89fa69626438614bb65d1c71937b8d2706187b55 | 138863addcc8e34f0014af13a935b1d860900e9d | master-build-workflows | * Restore workflows | |
| #3200 | 03/12/2026 07:54:57 | master | pending replay | d2daf397b13b0f35fb4731910f72b0892b7024b8 | 0f876a2afe037f21111adbb486de2948ae3010a8 | master-build-workflows | * Fix errant paths & switches | |
| #3211 | 03/13/2026 08:31:15 | master | pending replay | d9215b816e67d92ba76cd129a73a43f4eb25adbe | 48c72d7abae191476fd12972bf80124eae7e10ef | master-build-workflows | * Make steps more robust | |
| #3222 | 03/18/2026 08:53:27 | master | pending replay | 29db2d3a5dcf59b0fd9a72b54fc226edb782f256 | a20b08c902e4aa05dd0f6a1982e88aed74391d74 | master-build-workflows | * Apply NuGet/setup-nuget updates | |
| #3233 | 03/19/2026 18:30:31 | master | pending replay | 03bbe6c12e5ed10eff72741dd3b7a4792f236ded | 7d6d31308e2c35d8dfab9c1bf00df57b84a3bfd1 | alert-autofix-10 | Potential fix for code scanning alert no. 10: Workflow does not contain permissions | |
| #3239 | 03/21/2026 10:46:31 | master | pending replay | 4102f8f3907aa0bfe7377e9638ffe803bac2b9ae | 4b34483a1033f91b3fa5484e4961ad86d1735af7 | alert-autofix-5 | Potential fix for code scanning alert no. 5: Workflow does not contain permissions | |
| #3238 | 03/21/2026 10:47:06 | master | pending replay | adcc8008b68c946ec61f53706daed0aa27ac96d7 | 198e0a2f53a7b6a475928f2431e6df7909d8e247 | alert-autofix-1 | Potential fix for code scanning alert no. 1: Workflow does not contain permissions | |
| #3237 | 03/21/2026 10:47:39 | master | pending replay | bf58cec360d31574098c8945aa248d9a0c2d9d7f | 557e718a080a6e3e7f8c6c2ea2e8a5030823c190 | alert-autofix-2 | Potential fix for code scanning alert no. 2: Workflow does not contain permissions | |
| #3236 | 03/21/2026 10:48:03 | master | pending replay | 1d6314f4f616e3f7525e2ed4a96490c3202cc372 | 1996758719deac8a65e57fd0ee8a1002cd23098d | alert-autofix-3 | Potential fix for code scanning alert no. 3: Workflow does not contain permissions | |
| #3235 | 03/21/2026 10:48:21 | master | pending replay | 34b00ad657b852a7b44e2cb93c7bc73fbcf18dab | 7fce3c92bce8bf3c5f71982fc776070e51fc618a | alert-autofix-4 | Potential fix for code scanning alert no. 4: Workflow does not contain permissions | |
| #3232 | 03/24/2026 18:38:06 | master | pending replay | a872e6808970b6d0e001bfc6af22970f4e7ba88b | b1ee98d443e1068fd084755a2c72e24394116fea | PWagner1-patch-1 | Create SECURITY.md | |
| #3257 | 03/25/2026 12:49:59 | master | pending replay | f8e1a2c9ac5153c5013215e4750ec5fa74bd8f89 | 00f9c7ae455ac11b0a838166bd7120b551932971 | master-dependabot-config | * Use `master` branch for dependabot | |
| #3265 | 03/27/2026 07:43:51 | master | pending replay | a72911dda057563a2ba10bb1dc5e1e040902f7d1 | 35ee7020484f5a5b086b40cb56bf505ff093e955 | master-codeql-fixes | * Improve CodeQL workflow | |
| #3276 | 03/29/2026 06:52:54 | master | pending replay | 2029b08546662c996a9663e05cd0e15b8b4f9146 | 8321b21538d7045c035a18d4df38ed3ddce8ec63 | dependabot/github_actions/master/NuGet/setup-nuget-3.0.0 | Bump NuGet/setup-nuget from 2.0.2 to 3.0.0 | |
| #3275 | 03/29/2026 06:53:39 | master | pending replay | 69d3c3c6bccacd9d288fae03b3e742d992aad4ca | 7eed70e0cebd1c871cfd69d49a6644c64091d780 | dependabot/github_actions/master/github/codeql-action-4 | Bump github/codeql-action from 3 to 4 | |
| #3313 | 04/12/2026 06:16:28 | master | pending replay | 0305e0c71733ec95be6a5f8def5cf54e5ef506d7 | a840c2db47bd8c5cb9fcbd7d9bfafdadd1b55ce8 | dependabot/github_actions/master/actions/github-script-9 | Bump actions/github-script from 8 to 9 | |
| #3331 | 04/19/2026 07:20:07 | master | pending replay | 37fa89fabbb8e84fdb405bdf7326d325c1b1d0aa | 424d7e89f1dabdf19a9bdccbbb9f03a741247998 | dependabot/github_actions/master/NuGet/setup-nuget-3.1.0 | Bump NuGet/setup-nuget from 3.0.0 to 3.1.0 | |
| #3358 | 04/26/2026 06:35:13 | master | pending replay | eb4d838e882f0b946b952e570140892cfd738491 | 8e81420047a252d7b439195bceedc596c369fc11 | dependabot/github_actions/master/NuGet/setup-nuget-4 | Bump NuGet/setup-nuget from 3.1.0 to 4 | |
| #3368 | 04/30/2026 07:22:18 | master | pending replay | 4ce6e4d54a24a55334909f3d09d307bbeaca8fea | 0e0c372878c17674c8b7d997194e3f90223cc778 | master-3341-fixes | * Fixes #3341 wrong paths | |
| #3388 | 05/03/2026 09:02:52 | master | pending replay | 3b0c6d7343090ac36cc77e414a509ea33e435547 | 21c827d50d940bcd564d1bb6d3968420b624f484 | dependabot/github_actions/master/actions/checkout-6 | Bump actions/checkout from 4 to 6 | |
| #3379 | 05/03/2026 11:04:59 | master | pending replay | 1dfcb12d2e7f9f387884030995f004059cddffab | 1ea291ba13f0353a0c84f23314d76f7f50b0aa48 | master-alpha-backup-sync-workflow-fixes | * Fix backup sync workflow | |
| #3401 | 05/07/2026 06:45:53 | master | pending replay | 0868daad51b8af490234aee65ee1b2f313ee06dd | 72fd39732e857f0a433bada589fcb192a38b69ab | master-testform-github-workflow | * `TestForm` build workflow | |
| #3416 | 05/07/2026 16:06:25 | master | pending replay | 70e1a0a4f80482b35200b99e40122c47e7a55728 | 9e6e423894fe6cd6f5d95f00e4c37727ddeda24f | dependabot/github_actions/master/actions/cache-5 | Bump actions/cache from 4 to 5 | |
| #3424 | 05/10/2026 07:09:45 | master | pending replay | a0ecab0c0e29daddf419714423438094c5c2f570 | 3e4371d0f92c2d958da774031de05a02d2edbae2 | 3421-bug-build-testform-workflow-fails-to-run-correctly | * `build-testform` workflow fails to run correctly | |
| #3432 | 05/10/2026 08:30:04 | master | pending replay | 0af3736882ae28d57c3e37816ef70f0aee5ab0b9 | 70e0aedb474fcd107402a1a02875b1b227bface8 | master-webview2-fix | * Fix for WebView2 | |
| #3436 | 05/10/2026 09:18:46 | master | pending replay | f2b9d44f0e63f577d9006d63834c5106c61dcaeb | 3e7f540460bdc67cf340ae27df65d9e1fdf4ae7d | master-webview2-fix | * Adjust pinning | |
| #3450 | 05/12/2026 06:46:56 | master | pending replay | f4f0d2d0e691e7944afda1f9b9f9b089ff5eb49d | 90584905ec4ac45e4bafaa2f94eef43836a6e0cf | 3377-bug-kryptonstandardtoolkit-nuget-package-does-not-contain-all-binaries-scripts | * Update scripts for #3377 | |
| #3457 | 05/14/2026 10:48:38 | master | pending replay | fd84e7e33864cfbfb557e52b0f813d047fcd7e1b | 537ecf978b2e4eba01857d203679bca25ad0d256 | master-3455-tweaks | * 3455 Tweaks | |
| #3505 | 05/24/2026 08:45:51 | master | pending replay | d07312c9f68dfa4bd9d76b120ebe2fe615fa0de8 | e13b304591964178e50458d56f2fda496335e9df | 3502-V110-fix-webview2-ci-build-upstream | 3502-master-fix-webview2-ci-build-upstream | |
| #3488 | 05/24/2026 08:46:54 | master | pending replay | bc8614ec15141230130b7628d6b17b7495070e44 | 2002eafe3fef8ce37aabbab0a0c6768ed5a6c46b | master-lts-canary-workflow | * Fix Update canary-lts-release.yml | |
| #3511 | 05/25/2026 16:39:47 | master | pending replay | c9216202ba8b4505b43da0832fcef97e7720d65e | 1a4cb4b7197d474124f423e681962f317f67fd60 | master-build-enhancements | * Build enhancements | |
| #3518 | 05/25/2026 16:50:14 | master | pending replay | 80d6846607081cdfbd8f0911aab42ff36fda3fc7 | aeea2b7c95160a88ee0d89c8b8bd83e2ba901463 | master-3514-feature-request-include-readmemd-in-nuget-packages | * Include `README.md` in NuGet packages | |
| #3519 | 05/26/2026 06:17:07 | master | pending replay | f6ee426b92f0479cabccf36067cb716858e253cf | 17df4d237ca89dbd683b00cdaa513cab6e9e24b9 | 3517-feature-request-add-visx-package-for-itemproject-templates | * Add VSIX package for Krypton Visual Studio templates | |
| #3524 | 05/26/2026 07:24:18 | master | pending replay | 0a2fdd80fa856953ccab862a4b09a654aa561fd6 | d5b29dc002da13b840d1ca8e61c4d46b64d54c40 | 3517-feature-request-add-visx-package-for-itemproject-templates | * Fix VSIX manifest encoding for templates release (VSSDK1048) | |
| #3525 | 05/26/2026 10:17:40 | master | pending replay | a34cc90cb30b6d42aff2adf9e6146218ba0a7082 | 4c4e7d592bae26eaed39fcd6f3d403ac6e7e1668 | 3517-feature-request-add-visx-package-for-itemproject-templates | * Add Krypton Visual Studio template VSIX (#3517) | |
| #3529 | 05/26/2026 17:38:56 | master | skip by policy pending source PR proof | 0a1343c02d71157301a511e5218f6f47771275d4 | 723f038abec44f947d5cf90e8de01c8ec5bffbb5 | master-ci-pack-fix | * CI: align NuGet restore with Pack/Build (fix NETSDK1005) | |
| #3534 | 05/27/2026 06:27:03 | master | pending replay | 90855d64cbcc1a1f951b4175fd6cd32fc6d65dbf | ce05b6088b7358084735b5def863246f445664c3 | master-alpha-backup-chore-label-fix | * Scope the **Auto-label PR backup workflow** | |
| #3536 | 05/27/2026 13:37:18 | master | skip by policy pending source PR proof | b74ed9903167f19408d61c40098a5554f5b3b36f | b196cac49aaf081173dd16c11ed36ab73451b3bc | masster-ci-restore-fix | * Fixes release packaging failure caused by inconsistent restore/pack… | |
| #3551 | 05/28/2026 13:49:27 | master | pending replay | 9e98c98a9070cf2b925da2a5c2db8f46761526d2 | 5850efb66b180419c7632fb135d422752770d573 | 3550-feature-request-auto-complete-issues | * Auto complete issues | |
| #3556 | 05/28/2026 14:33:04 | master | pending replay | 3304d3185f304fcff847eb5fc1708ce9ac4eb32d | f160314ffc8adfb62a2b1dd4b6844c119d886ece | 3550-feature-request-auto-complete-issues | * Fix hardcoded branch names | |
| #3559 | 05/28/2026 14:55:09 | master | pending replay | bbbd41467eed22b7f7869dbe7fb318f5ba467240 | ef62525020c6a04c9ad314c79d86ed0e62599590 | 3557-bug-nightly-build-caching-issue | * Fix nightly WebView2 cache failures | |
| #3572 | 05/29/2026 09:25:51 | master | pending replay | 67c5b631571292f0005c52723416a0d059cb8cfd | 27722a0c4302e7045f025c3c6e57c4a02cf3741d | master-nightly-workflow-fix | * Fix failing workflows | |
| #3573 | 05/29/2026 10:36:30 | master | pending replay | 233acd83a99a3828e992bbf5314920ccfc30d041 | a72e12e67a0b6443cd6a3e45111e9126fee04850 | master-nightly-workflow-fix | * Fix race condition | |
| #3578 | 05/29/2026 11:49:32 | master | pending replay | cc5a40fdf303aa64e079c7896863da41e54f5e75 | c8e95d2ce1d0fea9fb7c8e06df0f296d1dd5c79b | master-nightly-workflow-fix | * Fix NETSDK1005 error | |
| #3581 | 05/29/2026 14:05:23 | master | pending replay | bae75620c4a5b3fa42d69d96f3b584276895c997 | d0216b052e65b6ae1d9f4906c0710762f548b9b3 | 3566-feature-request-use-correct-nuget-versions-in-vs-templates | * Use correct NuGet versions in VS templates | |
| #3588 | 05/29/2026 14:48:27 | master | apply/audit as revert PR | 7346eabfdbfea40160b1e8f460fa558162a4f48c | 9a210acb35d1879fb7807d97e1a400e51ceb910f | master-revert-accidental-merge | * Revert accidental merge | |
| #3589 | 05/29/2026 16:53:48 | master | pending replay | 586674dd17b505398216a4de263c2e7cb9d74b01 | 07f3bb6e2f71a57489fa0d19865fdb9959ae0fe9 | dependabot/github_actions/master/softprops/action-gh-release-3 | Bump softprops/action-gh-release from 2 to 3 | |
| #3590 | 05/29/2026 16:54:21 | master | pending replay | 45d12f3f2fef477742f89e60bac831655590619d | 97b684dfb819f82111843641f0b12d9998eedd2c | dependabot/github_actions/master/actions/upload-artifact-7 | Bump actions/upload-artifact from 4 to 7 | |
| #3604 | 05/31/2026 06:36:34 | master | pending replay | 816f9c5989f1bf97108a9c734c99b6e835801423 | 3b0fed1e52511e7916f924efa678bdf3ec00757d | dependabot/github_actions/master/actions/upload-artifact-7 | Bump actions/upload-artifact from 4 to 7 | |
| #3603 | 05/31/2026 06:37:17 | master | pending replay | d4587e0c8892f8e359f5735e937075e945fc2d7e | cba7a62cc0c6fc1a6fb08cff0c000f8fda2c0f8a | dependabot/github_actions/master/softprops/action-gh-release-3 | Bump softprops/action-gh-release from 2 to 3 | |
| #3677 | 06/05/2026 07:08:25 | master | pending replay | 5981070b31ebcfa534d3909f5476700d0a0cdc3c | f29d7b3872b0d9e5da80796a0fc137b4dd2c5516 | 3610-feature-request-harden-workflows-master | * Implements #3610 | |

## Pre-Jan Source PRs Recovered During Replay

These PRs are outside the Jan 30+ audit window but were required because later audited PRs depended on them and the trustworthy base did not contain them.

| PR | Merged At | Source | Result Commit | Reason |
|---|---|---|---|---|
| #2805 | 2025-12-29 | dc1148b360a27c55de17db798d3465cdead03043 | 55af3efde | Original V105 source for issue #2801 validation-forwarding implementation; required before #3272 cleanup. |
| #2834 | 2026-01-06 | def4b978e9bb1f8eb49a55d5a2e13785dbe44a06 | 601a055a2 | Original V105 source for issue #2832 RichTextBox formatting preservation and test form; required before #3284 cleanup. |

## Replay Log

| Order | PR | Source | Result Commit | Decision | Scope | Notes |
|---|---|---|---|---|---|---|
| 1 | #2963 | `c6cd51ea8173dc831b04981f1598eccbd6aa1a42` | `2da9c7d9c` | manual resolution | VisualForm + changelog | Changelog conflict resolved to PR side; code replayed from merge commit. |
| 2 | #2966 | 792a98fde7a13640e22a716fa7645ebbb4762d16 | 34f006a0b | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 3 | #2975 | fb9e05826cd3cf0ee3c9058fd93fddc4787fec59 | 07b316d01 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 4 | #2992 | 9d29a1890e28d7dd112b67e9daf274e0cb4b3b5b | 0eb1fd923 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 5 | #3001 | 76e45154078fbc0ec32d7891d3bfb273555dddd4 | 334455ebd | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 6 | #2997 | aa725564be5a01bfdd1ed1215603fb6c4644aab9 | 024e7899a | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 7 | #3010 | 7b673943bb342e14104e75b1f6d38450114588f7 | 839e986c9 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 8 | #3017 | d21b4cbbf44126ba677fe0955600a2440738d8ee | 2652b9ae0 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 9 | #3040 | 6f93d4309a057eed80fbcafb58eef09cd5d65f1d | 0af0bb94b | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 10 | #3045 | 1fa83ae33a82faf055704b572783fcdde454efa6 | 7b1eccc94 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 11 | #3049 | 18f1805991c0fa814ae59e03ab3fc14dfef1eb29 | 1cc8d75ec | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 12 | #3057 | `c5f0bcd2e2ada977fe6fc73be3c0051465c0f530` | `050126adb` | manual resolution | workflows | Workflow conflicts resolved to PR side for WebView2 prerelease lookup. |
| 13 | #3055 | e2f8b88850a8b6421a4ee9dd8aa17313c973ae3b | 2cfb2068a | applied | KryptonColorButton custom colours | Clean cherry-pick from merge commit. |
| 14 | #3062 | 11375b85b23d25f6b8112e3ebcded1f61886bf45 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 15 | #3070 | 0a92b437d51e9e9b083471aa15048d0a02bdd3ce | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 16 | #3059 | 98e9ef38f7a4442a0c1ccdae513a2347abcdeed7 | 9a41dd5f9 | manual resolution | KryptonForm #3012 fix + palette theme metrics | Recovered KryptonForm.cs plus palette theme metric changes; workflow/context noise not replayed. |
| 17 | #3079 | c9edb0c0819504752b17efb09322b765ffacf972 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 18 | #3085 | c4cd932b3b4e545641d10e24d26e3d3cccba24d9 | dd62fcb41 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 19 | #3083 | 7b3ab71e5f1fe22df129de4032cb02eda4f3011d | 1c7fb0637 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 20 | #3081 | 5e1ccca992cdb0899546610edb94a5a43f97f3a3 | 9dfb2f494 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 21 | #3088 | 444e9b4ebafedc50050f0537cd537dbfcef45195 | 29b8a5c28 | manual resolution | VisualForm + KryptonForm maximized bounds | KryptonForm conflict resolved to later PR side. |
| 22 | #3098 | 3a0416210dd9ecfdfaaaaa56bd3ac2d1fca97d0f | 8c4f0523b | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 23 | #3105 | 1261af4ca1bfffd242261d82602d72591b233af0 | 7c7418cec | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 24 | #3108 | b85f4c8cbaf92d361e954ed55e4e9c6b344fcc5c | 660027b72 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 25 | #3110 | 5dda46224c440e4c3ea2e47b2ad3130bf9f61aa5 | eb5a04511 | manual resolution | min design heights | VisualControlBase field insertion conflict resolved to PR side with comments intact. |
| 26 | #3129 | d7e296452b5c7fae06fa0f0468d7c7f83a906a80 | f0001785b | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 27 | #3131 | cad765b1cc79f05aef73032250dae2fc80f2cbc4 | 388fbc9c0 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 28 | #3122 | 1dc748ade26430cb0d5f4363d2dde274c2d158c8 | d0f343239 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 29 | #3136 | b9e60a0542006673507f178dda75a1560d046c3a | 336de6f6f | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 30 | #3143 | c23bb718052ded09c49198eb80d9dbaacb8acba4 | 248e1aae0 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 31 | #3145 | 18923f1671953cdc947e30668eac8e2050c77ef1 | 511bc48bc | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 32 | #3172 | 53e8391fcdfc7420e5432dbaea0591a77a771953 | 30f4f5386 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 33 | #3206 | 2ef34770b86b6789f72cc226a3739b8171d52f61 | ffacaa6e5 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 34 | #3214 | 3eaa3f388f8ec8c959e4c4c6da25db02b894982a | 4214ed1b3 | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 35 | #3220 | 3728de1bd9bf0546cadf94827ccb9dc0df591d1a | 3721dab3e | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 36 | #3224 | 0f183ea89486d453e6c880a56635bc09943c8bde | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 37 | #3212 | d1ddaa2a898019dfc67def8441e6515c986c499b | 4407b31ff | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 38 | #3150 | 5321de70e3d48b55e8b3adcd46469379b209996a | f46e2c92c | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 39 | #3179 | 2171b4aa2f8c8c337ebe44e4a7bed290a9642adf | 2e7af95da | applied | PR merge delta | Clean cherry-pick from merge commit. |
| 40 | #3269 | 254aa9f59b7b96154030eb0bee0668143b6cc0d5 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 41 | #3274 | 38d41813782bbb18c60e7d6a2231b883160dbfd3 | 9210ce87c | manual resolution | TestForm start screen + PaletteViewer | StartScreen button-list conflict resolved to PR generic `CreateButton<TForm>` list; PaletteViewer replayed from PR merge commit. |
| 42 | #2805 | dc1148b360a27c55de17db798d3465cdead03043 | 55af3efde | prerequisite source replay | validation forwarding | Pre-Jan original V105 PR recovered because #3272 depended on this implementation and the trustworthy base did not contain it. |
| 43 | #3272 | 89025dccafc87ac95ae918472924ef77e74b70bb | 53283750a | applied | validation TODO cleanup | Applied after #2805; source commit only removed TODO markers around the validation-forwarding fix. |
| 44 | #3254 | db21fc64d936fd7ee7d4de6357f846f9df315f26 | 16099711f | applied | ribbon separator DPI | Clean cherry-pick from merge commit. |
| 45 | #3251 | 7e77bdf2275e29df49bbfe7a47d5d7d8484afbe5 | 15c55ee73 | applied | docking LoadConfigFromArray | Clean cherry-pick from merge commit. |
| 46 | #2834 | def4b978e9bb1f8eb49a55d5a2e13785dbe44a06 | 601a055a2 | prerequisite source replay | RichTextBox formatting preservation | Pre-Jan original V105 PR recovered because #3284 modified this test/fix and the trustworthy base did not contain it. |
| 47 | #3284 | 1772131a37500561020debf1acfb5498828f9199 | a98814a51 | applied | TestForm bracket/style cleanup | Applied after #2834; clean replay of follow-up changes. |
| 48 | #3307 | 1514baf184c30120012857e9096b65c70c8bf1d8 | 53f7e5503 | applied | tree view event crash | Clean cherry-pick from merge commit. |
| 49 | #3310 | 95b8d65d0d921ad7398e636593896596c21874bf | e8219cfd0 | applied | file dialog ActionLists | Clean cherry-pick from merge commit. |
| 50 | #3318 | 5b67a51917d8c52b94685a1f5944280d2554b9dd | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 51 | #3335 | b0faf03453e075fce3fe9fd58eb97b1a25c23b53 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 52 | #3340 | 78d4aa911012e5b0aaec76e02e27b424101a7ecb | 5bd2c9953 | applied | KryptonThemeComboBox selected index | Clean cherry-pick from merge commit. |
| 53 | #3355 | 0b5cc68f26a386b4ab57096553c5d632fc48ea4a | a7c838977 | applied | KryptonTextBox multiline resize flicker | Clean cherry-pick from merge commit. |
| 54 | #3351 | 27f70055d3b77fc25b86f47b3470cc65e0c5d8e4 | 71e41c772 | applied | font XML null handling | Clean cherry-pick from merge commit. |
| 55 | #3374 | 221ffe00aa3bbb13a6878140ad30d41e1651aa33 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 56 | #3392 | 14d8d5cc43df8d43571308130a7cb740e9e16f6a | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 57 | #3396 | 0276b046a13aefd0b05d4fd45ba57884b2697cf8 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 58 | #3387 | be0c1be8a72ba27b1fc901d5f1388291529e1915 | 67fdba072 | applied | SystemEvents retention | Clean cherry-pick from merge commit. |
| 59 | #3402 | 5f47597eaab01c8057cabc3dd280aff93ea502d0 | f012c6e6d | applied | KryptonButton rounded text centering | Clean cherry-pick from merge commit. |
| 60 | #3415 | 84bb89e11117ad363ea8436e1c777edf15d360f0 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 61 | #3420 | 90098ed1363b0c1f8020266141aa77cf4e053d44 | n/a | audit catch-up contents against source PRs | catch-up revert | Not replayed directly. |
| 62 | #3370 | 9e675a19b3d903cbb3dfb3c362d7f0f0814422f9 | 6383fb1b0 | manual resolution | KryptonLabel/KryptonLinkLabel autosize | VisualSimpleBase conflict resolved to PR sizing logic with comments intact. |
| 63 | #3427 | f80d7050f07a18c8749cff8623daab6e0e0cd113 | c65531a9a | applied | KryptonTextBox cue hint lines | Clean cherry-pick from merge commit. |
| 64 | #3441 | dd06620845f9f8fcc88a4e9f06b2d21e9cc59124 | 580185153 | applied | KryptonButton hover inner border | Clean cherry-pick from merge commit. |
| 65 | #3473 | 50071f1ef103120f812ff76be270fbc5e267e1d1 | 5e58fdbbe | applied | KryptonTextBox ButtonSpecs hover flicker | Clean cherry-pick from merge commit. |
| 66 | #3471 | 9884513b774580ce11c5b5320b03f7145a25c7c5 | ce67e37a1 | applied | KryptonHeaderGroup child controls | Clean cherry-pick from merge commit. |
| 67 | #3510 | eae56f350fba516cd8154405ec08806faf3390f1 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 68 | #3498 | 65b7640a89e9b0ef51bb85084c22af2791c34040 | d635646c3 | manual resolution | KryptonTreeView selection flicker | Directory.Build.props conflict resolved to include PR multi-target output/intermediate paths. |
| 69 | #3520 | 69cf9f46d41d3c6051a9a3f521fd4a67ae6ed072 | fd1702b78 | applied | ModernBuild script profiles | Clean cherry-pick from merge commit. |
| 70 | #3533 | da3d90e6d5ff4ce93fb90e3033aa38a4dc9ba722 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 71 | #3542 | d1a09183f39625c4cbcd6ddd113de8827abc1a0d | 3af9553e8 | applied | docking follow-up | Clean cherry-pick from merge commit. |
| 72 | #3548 | 6994b00c125b8f7aef85d234e1a94ab6a27a311a | c9ec0efab | applied | #3514 changelog | Clean cherry-pick from merge commit. |
| 73 | #3552 | e19487369d55967cddcd1a74b3cddc43577aea7e | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 74 | #3565 | 93384f45e084aea8e9e37ebe69e4623ffa29ed2f | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 75 | #3570 | 7e0e631f305e19946eaf64cc71c7e81fec4f83a1 | 3e7984113 | applied from PR head | toolbox bitmaps | Merge object missing locally; replayed available PR head commit. |
| 76 | #3577 | 65f12cba15418ea704c8521619ea9524e73e36e0 | n/a | audit catch-up contents against source PRs | catch-up PR | Not replayed directly; source PRs remain the recovery source. |
| 77 | #3605 | 62c1718de5cd96f3cb47c15bf76ef86b15c76b7b | f1ef8ab48 | applied | KryptonContextMenu disposal leaks | Clean cherry-pick from merge commit. |
| 78 | #3624 | e10aa319944c983bc90f8b7616d21034587a5dcd | 525163b25 | applied | folder browser dialog drawing | Clean cherry-pick from merge commit. |
| 79 | #3623 | 6717b60a181160460acbdeddbeb36469683f1a3a | 225e0bd2b | applied | MenuItem theme settings | Clean cherry-pick from merge commit. |
| 80 | #3622 | 12a19158b5e15e8df84253b96fed4e01853f8010 | 9d77ea426 | applied | context menu colours | Clean cherry-pick from merge commit. |
| 81 | #3609 | 60af545025c41cf22e7784f3fcd6ea53d650185b | 083771d70 | manual resolution | build script fixes | Script/project conflicts resolved to direct PR side; ModernBuild ScriptProfile verified present. |
| 82 | #3620 | 4bd6c1ad53ac5b3892b1dbfa6024d5cb67f3395a | n/a | empty/not applicable | workflow MSBuild quoting | Recovered workflows do not use the vulnerable version-probe command; canary.yml stayed absent. |
| 83 | #3630 | 1223f295299e0ae62250b75ce29e5017381ea637 | 7a5f8e94b | manual resolution | docking autosizing | Changelog conflict resolved to #3580 only. |
| 84 | #3633 | 16df965d6f22eca8215e48c5ca612d902fbf150e | c948ae2dc | applied | auto-complete changelog | Clean cherry-pick from merge commit. |
| 85 | #3643 | e93194441a4dca6dda43810710199dcfd8de1af2 | n/a | skip by policy | restore catch-up | Restore/catch-up PR; not replayed directly. |
| 86 | #3652 | d1b80e88ad684d79733d6dda6b829a2a60f1f2b1 | 120b92d32 | applied | scrollbar display follow-up | Clean cherry-pick from merge commit. |
| 87 | #3656 | accf1d5fc0d2418c1bab6944605b09f58af08dcd | 968dc8221 | applied | VisualForm .NET10 flicker | Clean cherry-pick from merge commit. |

## Verification Log

| Check | Result | Notes |
|---|---|---|
