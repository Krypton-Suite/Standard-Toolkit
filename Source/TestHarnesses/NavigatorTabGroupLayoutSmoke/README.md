# NavigatorTabGroupLayoutSmoke

Round-trip smoke for caption tab-group and workspace layout persistence.

Uses a plain `Form` host (not `KryptonForm`) so the harness stays independent of VisualForm Win32 static init.

```cmd
dotnet run --project "Source\TestHarnesses\NavigatorTabGroupLayoutSmoke\NavigatorTabGroupLayoutSmoke.csproj" -c Debug
```

Exit code `0` means PASS.
