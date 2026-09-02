# Unit test scripts

## Designer serialization defaults (`UnitTest-DesignerSerializationDefaults.ps1`)

Fresh Toolbox controls must not show nested `Storage` objects as **Modified** (issue #4325).

Requires Debug `net472` output (`Bin\Debug\net472\Krypton.Toolkit.dll`).

```cmd
dotnet build "Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit 2022.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DesignerSerializationDefaults.ps1
```

The script instantiates toolbox controls, walks `TypeDescriptor` content properties, and fails if core drop targets have `IsDefault == false` or unexpected `ShouldSerializeValue == true`.
