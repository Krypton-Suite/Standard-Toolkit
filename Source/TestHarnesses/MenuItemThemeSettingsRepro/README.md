# MenuItem Theme Settings Repro

Run:

```powershell
dotnet run --project "Source/TestHarnesses/MenuItemThemeSettingsRepro/MenuItemThemeSettingsRepro.csproj" -c Debug
```

Validation:

1. Leave `Border only` unchecked, then hover and press the top-level menu. The top-level selected and pressed colors should use the configured `MenuItemSelectedGradient###` and `MenuItemPressedGradient###` values.
2. Open a drop-down menu and hover items. Drop-down and context menu item highlights should use `MenuItemSelected`.
3. Check `Border only`, then hover top-level, drop-down, and context menu items. The red `MenuItemBorder` should appear while the normal highlight background remains visible.
