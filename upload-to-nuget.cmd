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
dotnet nuget push Krypton.Docking.Canary.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Docking.Canary.Lite.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Navigator.Canary.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Navigator.Canary.Lite.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Ribbon.Canary.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Ribbon.Canary.Lite.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Toolkit.Canary.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Toolkit.Canary.Lite.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Workspace.Canary.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

dotnet nuget push Krypton.Workspace.Canary.Lite.6.0.2006.3.nupkg -k <#API-KEY#> -s https://api.nuget.org/v3/index.json

echo All NuGet packages have now been published!

:no
pause