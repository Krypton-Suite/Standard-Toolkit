:: Last updated: Saturday 6th December, 2025 @ 10:00

@echo off

setlocal EnableExtensions

title Krypton Toolkit Build System

set "VS_VERSION="
set "VS_SCRIPTS_DIR="

goto selectvsversion

:: ===================================================================================================

:selectvsversion
cls

@echo Welcome to the Krypton Toolkit Build system, version: 3.0a.
@echo Please select the Visual Studio toolset to target.
echo:
@echo ==============================================================================================
echo:
echo 1. Visual Studio 2022 (Scripts\VS2022)
echo 2. Visual Studio 2026 (Scripts\Current)
echo 3. End
echo:
set /p answer="Enter number (1 - 3): "
if "%answer%"=="1" (goto usevs2022)
if "%answer%"=="2" (goto usevscurrent)
if "%answer%"=="3" (goto exitbuildsystem)

@echo Invalid input, please try again.

pause

goto selectvsversion

:usevs2022
call :configurevsversion VS2022
if errorlevel 1 (goto selectvsversion)
goto mainmenu

:usevscurrent
call :configurevsversion Current
if errorlevel 1 (goto selectvsversion)
goto mainmenu

:configurevsversion
set "VS_VERSION=%~1"
set "VS_SCRIPTS_DIR=Scripts\%~1"
if not exist "%VS_SCRIPTS_DIR%" (
    echo.
    echo ERROR: Could not find "%VS_SCRIPTS_DIR%".
    echo Please ensure the scripts are available before continuing.
    echo.
    pause
    set "VS_VERSION="
    set "VS_SCRIPTS_DIR="
    exit /b 1
)
echo.
echo Using %VS_VERSION% scripts located at "%VS_SCRIPTS_DIR%".
echo.
exit /b 0

:: ===================================================================================================

:mainmenu

cls

if "%VS_SCRIPTS_DIR%"=="" (goto selectvsversion)

echo Current Visual Studio target: %VS_VERSION%
echo Script directory..........: %VS_SCRIPTS_DIR%
echo:
echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. Build and Pack Toolkit
echo 5. Debug project
echo 6. NuGet Tools
echo 7. Create Archives (ZIP/TAR)
echo 8. WebView2 SDK Tools
echo 9. Change Visual Studio target
echo 10. End
echo:
set /p answer="Enter number (1 - 10): "
if "%answer%"=="1" (goto cleanproject)
if "%answer%"=="2" (goto buildproject)
if "%answer%"=="3" (goto createnugetpackages)
if "%answer%"=="4" (goto buildandpacktoolkit)
if "%answer%"=="5" (goto debugproject)
if "%answer%"=="6" (goto nugettools)
if "%answer%"=="7" (goto createarchives)
if "%answer%"=="8" (goto webview2menu)
if "%answer%"=="9" (goto selectvsversion)
if "%answer%"=="10" (goto exitbuildsystem)

@echo Invalid input, please try again.

pause

goto mainmenu

:buildmenu
cls

echo 1. Build nightly version
echo    a. Rebuild project
echo 2. Build canary version
echo 3. Build stable version
echo 4. Build long term stable version (LTS)
echo 5. Go back to main menu
echo:
set /p answer="Enter number or letter (1 - 5, a - *): "
if %answer%==1 (goto buildnightly)
if %answer%==a (goto rebuildproject)
if %answer%==2 (goto buildcanary)
if %answer%==3 (goto buildstable)
if %answer%==4 (goto buildlts)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto buildmenu

:packmenu
cls

echo 1. Pack nightly version
echo 2. Pack canary version
echo 3. Pack stable version
echo 4. Pack long term stable version (LTS)
echo 5. Go back to main menu
echo:
set /p answer="Enter number (1 - 5): "
if %answer%==1 (goto packnightly)
if %answer%==2 (goto packcanary)
if %answer%==3 (goto packstable)
if %answer%==4 (goto packlts)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packmenu

:debugmenu
cls

echo 1. Debug
echo 2. Go back to main mainmenu
echo:
set /p answer="Enter number (1 - 2): "
if %answer%==1 (goto debug)
if %answer%==2 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto debugmenu

:buildandpacktoolkitmenu

cls

echo 1. Build and pack nightly
echo 2. Build and pack canary
echo 3. Build and pack stable
echo 4. Build and pack long term stable (LTS)
echo 5. Go to main mainmenu
echo:
set /p answer="Enter number (1 - 5): "
if %answer%==1 (goto buildandpacknightly)
if %answer%==2 (goto buildandpackcanary)
if %answer%==3 (goto buildandpackstable)
if %answer%==4 (goto buildandpacklts)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto buildandpacktoolkitmenu

:miscellaneoustasksmenu

cls

echo 1. Install prerequisites
echo 2. Update prerequisites
echo 3. Go to main menu
echo:
set /p answer="Enter number (1 - 3): "
if %answer%==1 (goto installprerequisites)
if %answer%==2 (goto updateprerequisites)
if %answer%==3 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto miscellaneoustasksmenu

:: ===================================================================================================

:clearscreen
cls

:hold
pause

:cleanproject
cls

echo Deleting the 'Bin' folder
rd /s /q "Bin"
echo Deleted the 'Bin' folder
echo Deleting the 'Krypton.Docking\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Docking\obj"
echo Deleted the 'Krypton.Docking\obj' folder
echo Deleting the 'Krypton.Navigator\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Navigator\obj"
echo Deleted the 'Krypton.Navigator\obj' folder
echo Deleting the 'Krypton.Ribbon\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Ribbon\obj"
echo Deleted the 'Krypton.Ribbon\obj' folder
echo Deleting the 'Krypton.Toolkit\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Toolkit\obj"
echo Deleted the 'Krypton.Toolkit\obj' folder
echo Deleting the 'Krypton.Workspace\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Workspace\obj"
echo Deleted the 'Krypton.Workspace\obj' folder
echo Deleting the 'Logs' folder
del /f "Logs"

pause

goto mainmenu

:clearproject

echo Deleting the 'Bin' folder
rd /s /q "Bin"
echo Deleted the 'Bin' folder
echo Deleting the 'Krypton.Docking\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Docking\obj"
echo Deleted the 'Krypton.Docking\obj' folder
echo Deleting the 'Krypton.Navigator\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Navigator\obj"
echo Deleted the 'Krypton.Navigator\obj' folder
echo Deleting the 'Krypton.Ribbon\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Ribbon\obj"
echo Deleted the 'Krypton.Ribbon\obj' folder
echo Deleting the 'Krypton.Toolkit\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Toolkit\obj"
echo Deleted the 'Krypton.Toolkit\obj' folder
echo Deleting the 'Krypton.Workspace\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Workspace\obj"
echo Deleted the 'Krypton.Workspace\obj' folder
echo Deleting the 'Logs' folder
del /f "Logs"

:: ===================================================================================================

:cleanproject
goto cleanproject

:buildproject
goto buildmenu

:createnugetpackages
goto packmenu

:debugproject
goto debugmenu

:nugettools
goto createarchives

:createarchives
cls

echo 1. Create ZIP archive (Nightly)
echo 2. Create TAR archive (Nightly)
echo 3. Create both ZIP and TAR archives (Nightly)
echo 4. Create ZIP archive (Canary)
echo 5. Create TAR archive (Canary)
echo 6. Create both ZIP and TAR archives (Canary)
echo 7. Create ZIP archive (Stable)
echo 8. Create TAR archive (Stable)
echo 9. Create both ZIP and TAR archives (Stable)
echo 10. Update NuGet tools
echo 11. Go back to main menu
echo:
set /p answer="Enter number (1 - 11): "
if %answer%==1 (goto createzipnightly)
if %answer%==2 (goto createtarnightly)
if %answer%==3 (goto createallarchivesnightly)
if %answer%==4 (goto createzipcanary)
if %answer%==5 (goto createtarcanary)
if %answer%==6 (goto createallarchivescanary)
if %answer%==7 (goto createzipstable)
if %answer%==8 (goto createtarstable)
if %answer%==9 (goto createallarchivesstable)
if %answer%==10 (goto updatenuget)
if %answer%==11 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto createarchives

:: ===================================================================================================

:webview2menu

cls

cd Scripts/WebVew2/

WebView2Setup.cmd

:; ===================================================================================================

:updatenuget
cls


call "%VS_SCRIPTS_DIR%\update-nuget.cmd"


pause

goto mainmenu

:: ===================================================================================================

:createzipnightly
cls


call "%VS_SCRIPTS_DIR%\build-nightly.cmd" CreateNightlyZip


pause

goto mainmenu

:createtarnightly
cls


call "%VS_SCRIPTS_DIR%\build-nightly.cmd" CreateNightlyTar


pause

goto mainmenu

:createallarchivesnightly
cls


call "%VS_SCRIPTS_DIR%\build-nightly.cmd" CreateAllArchives


pause

goto mainmenu

:: ===================================================================================================

:createzipcanary
cls


call "%VS_SCRIPTS_DIR%\build-canary.cmd" CreateCanaryZip


pause

goto mainmenu

:createtarcanary
cls


call "%VS_SCRIPTS_DIR%\build-canary.cmd" CreateCanaryTar


pause

goto mainmenu

:createallarchivescanary
cls


call "%VS_SCRIPTS_DIR%\build-canary.cmd" CreateAllCanaryArchives


pause

goto mainmenu

:: ===================================================================================================

:createzipstable
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" CreateReleaseZip


pause

goto mainmenu

:createtarstable
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" CreateReleaseTar


pause

goto mainmenu

:createallarchivesstable
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" CreateAllReleaseArchives


pause

goto mainmenu

:: ===================================================================================================

:buildandpacktoolkit
goto buildandpacktoolkitmenu

:miscellaneoustasks
goto miscellaneoustasksmenu

:exitbuildsystem
@echo Exiting the build system, have a good day. Bye!

pause

exit

:: ===================================================================================================

:buildnightly
cls


call "%VS_SCRIPTS_DIR%\build-nightly.cmd"
goto buildmenu


:buildcanary
cls


call "%VS_SCRIPTS_DIR%\build-canary.cmd"
goto buildmenu


:buildinstaller
cls


call "%VS_SCRIPTS_DIR%\build-installer.cmd"
cls
goto buildmenu


:buildstable
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd"
goto buildmenu

:buildlts
cls

call "%VS_SCRIPTS_DIR%\build-lts.cmd"
goto buildmenu


:: ===================================================================================================

:packnightly
cls


call "%VS_SCRIPTS_DIR%\build-nightly.cmd" Pack
goto packmenu

:packcanary
cls


call "%VS_SCRIPTS_DIR%\build-canary.cmd" Pack
goto packmenu

:packinstaller
cls


call "%VS_SCRIPTS_DIR%\build-installer.cmd" Pack
goto packmenu

:packstable
cls

echo 1. Produce 'Lite' stable packages
echo 2. Produce 'full' stable packages
echo 3. Produce 'full/lite' stable packages
echo 4. Go back to main menu
echo:
set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packstablelite)
if %answer%==2 (goto packstablefull)
if %answer%==3 (goto packstableboth)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packstable

:packltsmenu

cls

echo 1. Produce 'Lite' LTS packages
echo 2. Produce 'full' LTS packages
echo 3. Produce 'full/lite' LTS packages
echo 4. Go back to main menu

echo:
set /p answer="Enter number (1 - 4): "

if %answer%==1 (goto packltslite)
if %answer%==2 (goto packltsfull)
if %answer%==3 (goto packltsboth)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packltsmenu

:: ===================================================================================================

:packstablelite
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" PackLite
goto packmenu

:packstablefull
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" PackAll
goto packmenu

:packstableboth
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" Pack
goto packmenu

:packltslite
cls

call "%VS_SCRIPTS_DIR%\build-lts.cmd" PackLite
goto packltsmenu

:packltsfull
cls

call "%VS_SCRIPTS_DIR%\build-lts.cmd" PackAll
goto packltsmenu

:packltsboth
cls

call "%VS_SCRIPTS_DIR%\build-lts.cmd" Pack
goto packltsmenu

:: ===================================================================================================

:debug
cls

echo Deleting the 'Bin' folder
rd /s /q "Bin"
echo Deleted the 'Bin' folder
echo Deleting the 'Krypton.Docking\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Docking\obj"
echo Deleted the 'Krypton.Docking\obj' folder
echo Deleting the 'Krypton.Navigator\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Navigator\obj"
echo Deleted the 'Krypton.Navigator\obj' folder
echo Deleting the 'Krypton.Ribbon\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Ribbon\obj"
echo Deleted the 'Krypton.Ribbon\obj' folder
echo Deleting the 'Krypton.Toolkit\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Toolkit\obj"
echo Deleted the 'Krypton.Toolkit\obj' folder
echo Deleting the 'Krypton.Workspace\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Workspace\obj"
echo Deleted the 'Krypton.Workspace\obj' folder
echo Deleting the 'build.log' file
del /f build.log
echo Deleted the 'build.log' file

cls


call "%VS_SCRIPTS_DIR%\build-nightly.cmd"

:: ===================================================================================================

:nugettools
cls


call "%VS_SCRIPTS_DIR%\update-nuget.cmd"

:buildandcreatenugetpackages
cls

echo 1. Build nightly packages
echo 2. Build canary packages
echo 3. Build stable packages
echo 4. Build stable (lite) packages
echo 5. Build LTS packages
echo 6. Go back to main menu
echo:
set /p answer="Enter number (1 - 6): "

if %answer%==1 (goto buildnightlypackages)
if %answer%==2 (goto buildcanarypackages)
if %answer%==3 (goto buildstablepackages)
if %answer%==4 (goto buildstablelitepackages)
if %answer%==5 (goto buildltspackages)
if %answer%==6 (goto mainmenu)

@echo Invalid input, please try again.

pause

:: ===================================================================================================

:buildnightlypackages
cls

echo Step 1: Clean

echo Deleting the 'Bin' folder
rd /s /q "Bin"
echo Deleted the 'Bin' folder
echo Deleting the 'Krypton.Docking\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Docking\obj"
echo Deleted the 'Krypton.Docking\obj' folder
echo Deleting the 'Krypton.Navigator\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Navigator\obj"
echo Deleted the 'Krypton.Navigator\obj' folder
echo Deleting the 'Krypton.Ribbon\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Ribbon\obj"
echo Deleted the 'Krypton.Ribbon\obj' folder
echo Deleting the 'Krypton.Toolkit\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Toolkit\obj"
echo Deleted the 'Krypton.Toolkit\obj' folder
echo Deleting the 'Krypton.Workspace\obj' folder
rd /s /q "Source\Krypton Components\Krypton.Workspace\obj"
echo Deleted the 'Krypton.Workspace\obj' folder
echo Deleting the 'build.log' file
del /f build.log
echo Deleted the 'build.log' file

cls

echo Step 2: Build


call "%VS_SCRIPTS_DIR%\build-nightly-custom.cmd"

pause

cls

echo Step 3: Pack

call "%VS_SCRIPTS_DIR%\build-nightly-custom.cmd" Pack

pause

:: ===================================================================================================

:rebuildproject
cls


call "%VS_SCRIPTS_DIR%\rebuild-build-nightly.cmd"

:: ===================================================================================================

:buildandpacknightly
cls

:: goto clearproject


:: build-nightly.cmd

call "%VS_SCRIPTS_DIR%\build-nightly.cmd" Pack

pause

goto mainmenu

:buildandpackcanary
cls


call "%VS_SCRIPTS_DIR%\build-canary.cmd" Pack

pause

goto mainmenu

:buildandpackstable
cls


call "%VS_SCRIPTS_DIR%\build-stable.cmd" Pack

pause

goto mainmenu

:buildandpacklts
cls


call "%VS_SCRIPTS_DIR%\build-lts.cmd" Pack

pause

goto mainmenu

:: ===================================================================================================

:webview2tools
cls

echo WebView2 SDK Tools
echo.
echo 1. Setup WebView2 SDK
echo 2. Update WebView2 SDK
echo 3. Check WebView2 Version
echo 4. Go back to main menu
echo:
set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto setupwebview2sdk)
if %answer%==2 (goto updatewebview2sdk)
if %answer%==3 (goto checkwebview2version)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto webview2tools

:: ===================================================================================================

:setupwebview2sdk
cls

echo Setting up WebView2 SDK for KryptonWebView2 control...
echo This will install the latest stable WebView2 SDK version.
echo.

cd Scripts

Setup-WebView2SDK.cmd

cd ..

pause

goto webview2tools

:: ===================================================================================================

:updatewebview2sdk
cls

echo Updating WebView2 SDK to latest version...
echo This will check for updates and install the newest stable version.
echo.

cd Scripts

Update-WebView2SDK.cmd

cd ..

pause

goto webview2tools

:: ===================================================================================================

:checkwebview2version
cls

echo Checking WebView2 SDK version...
echo.

cd Scripts

Check-WebView2Version.cmd

cd ..

pause

goto webview2tools

:clearlogfiles

:clearbinaries
