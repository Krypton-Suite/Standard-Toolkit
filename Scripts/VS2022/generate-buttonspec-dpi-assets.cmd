@echo off
setlocal
cd /d "%~dp0..\.."

echo Building Krypton.Toolkit (net472)...
dotnet build "Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit 2022.csproj" -c Debug -f net472
if errorlevel 1 exit /b 1

echo Building ButtonSpecDpiAssetGenerator...
dotnet build "Source\TestHarnesses\ButtonSpecDpiAssetGenerator\ButtonSpecDpiAssetGenerator.csproj" -c Debug
if errorlevel 1 exit /b 1

echo Generating ButtonSpec @2x/@3x assets...
dotnet run --project "Source\TestHarnesses\ButtonSpecDpiAssetGenerator\ButtonSpecDpiAssetGenerator.csproj" -c Debug --no-build -- "Source\Krypton Components\Krypton.Toolkit\Resources\ButtonSpecs"
if errorlevel 1 exit /b 1

echo Rebuilding Krypton.Toolkit to embed new resources...
dotnet build "Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit 2022.csproj" -c Debug -f net472
exit /b %errorlevel%
