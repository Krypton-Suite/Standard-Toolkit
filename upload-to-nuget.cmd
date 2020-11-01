@echo off

echo You are about to publish new updates to NuGet. Have You set your API key correctly? (Y/N)
set INPUT=
set /P INPUT=Type response: %=%
if /I "%INPUT%"=="y" goto apikeycheck
if /I "%INPUT%"=="n" goto no

:apikeycheck
echo The API key is now entered. Have You set your version correctly? (Y/N)
set INPUT=
set /P INPUT=Type response: %=%
if /I "%INPUT%"=="y" goto versioncheck
if /I "%INPUT%"=="n" goto no

:versioncheck
echo The version is now correct. Do you want to publish to NuGet? (Y/N)
set INPUT=
set /P INPUT=Type response: %=%
if /I "%INPUT%"=="y" goto publish
if /I "%INPUT%"=="n" goto no

:publish
dotnet nuget push Krypton.Docking.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Docking.Lite.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Navigator.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Navigator.Lite.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Ribbon.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Ribbon.Lite.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Toolkit.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Toolkit.Lite.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Workspace.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Workspace.Lite.5.550.2011.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

echo All NuGet packages have now been published!

:no
pause