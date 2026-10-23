# ThemeSwapRepro

Run from the **repository root**:

```
dotnet run --project "Source/TestHarnesses/ThemeSwapRepro/ThemeSwapRepro.csproj" -c Debug
```

It opens three forms with a `KryptonThemeComboBox` each and repeatedly toggles themes across forms to reproduce WM_COMMAND faults.

Logs are written to `%TEMP%\ThemeSwapRepro.log`.
