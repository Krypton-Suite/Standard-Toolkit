# MenuItem Theme Settings Repro

Run:

```powershell
dotnet run --project "Source/TestHarnesses/MenuItemThemeSettingsRepro/MenuItemThemeSettingsRepro.csproj" -c Debug
```

Validation:

1. Leave `Border only` unchecked.
2. Open a drop-down or context menu and hover items. Drop-down and context menu item highlights should use the lime `MenuItemSelected` override.
3. Check `Border only`, then hover top-level, drop-down, and context menu items. The red `MenuItemBorder` should appear while the normal highlight background remains visible.
