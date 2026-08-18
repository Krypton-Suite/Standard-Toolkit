# Theme provider sample

Class library that advertises `[assembly: KryptonThemeProvider]` so `KryptonThemeCatalog.DiscoverThemes()` can pick it up after `Assembly.LoadFrom`.

This assembly does not add new `PaletteMode` values (the enum is closed). `GetThemes()` is empty so it never replaces core or `Krypton.Themes` palettes. Use it as a template: return `KryptonThemeDescriptor` instances for modes your assembly actually implements.

```csharp
[assembly: KryptonThemeProvider(typeof(SampleThemeProvider))]
```

Build:

```powershell
dotnet build ".\Source\TestHarnesses\ThemeProviderSample\ThemeProviderSample.csproj" -c Debug
```
