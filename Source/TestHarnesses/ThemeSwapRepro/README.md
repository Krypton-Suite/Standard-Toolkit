# Run with:

  dotnet run --project "Source/TestHarnesses/ThemeSwapRepro/ThemeSwapRepro.csproj" -c Debug

It opens 3 forms with a `KryptonThemeComboBox` each and repeatedly toggles themes across forms to reproduce WM_COMMAND faults.
Logs written to %TEMP%\ThemeSwapRepro.log
