:: Last updated: Wednesday 21st August, 2024 @ 19:00

@echo off

title Krypton Toolkit Build System

cls

@echo Welcome to the Krypton Toolkit Build system, version: 2.4. Please select an option below.
echo:
@echo ==============================================================================================
echo:
echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. Build and Pack Toolkit
echo 5. Debug project
echo 6. NuGet Tools
::echo 7. Miscellaneous tasks
echo 7. End
echo:
set /p answer="Enter number (1 - 7): "
if %answer%==1 (goto cleanproject)
if %answer%==2 (goto buildproject)
if %answer%==3 (goto createnugetpackages)
if %answer%==4 (goto buildandpacktoolkit)
if %answer%==5 (goto debugproject)
if %answer%==6 (goto nugettools)
if %answer%==7 (goto exitbuildsystem)

@echo Invalid input, please try again.

pause

goto mainmenu

:: ===================================================================================================

:mainmenu

cls

echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. Build and Pack Toolkit
echo 5. Debug project
echo 6. NuGet Tools
::echo 7. Miscellaneous tasks
echo 7. End
echo:
set /p answer="Enter number (1 - 8): "
if %answer%==1 (goto cleanproject)
if %answer%==2 (goto buildproject)
if %answer%==3 (goto createnugetpackages)
if %answer%==4 (goto buildandpacktoolkit)
if %answer%==5 (goto debugproject)
if %answer%==6 (goto nugettools)
::if %answer%==7 (goto miscellaneoustasks)
if %answer%==7 (goto exitbuildsystem)

@echo Invalid input, please try again.

pause

goto mainmenu

:buildmenu
cls

echo 1. Build nightly version
echo    a. Rebuild project
echo 2. Build canary version
echo 3. Build stable version
echo 4. Go back to main menu
echo:
set /p answer="Enter number or letter (1 - 4, a - *): "
if %answer%==1 (goto buildnightly)
if %answer%==a (goto rebuildproject)
if %answer%==2 (goto buildcanary)
if %answer%==3 (goto buildstable)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto buildmenu

:packmenu
cls

echo 1. Pack nightly version
echo 2. Pack canary version
echo 3. Pack stable version
echo 4. Go back to main menu
echo:
set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packnightly)
if %answer%==2 (goto packcanary)
if %answer%==3 (goto packstable)
if %answer%==4 (goto mainmenu)

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
echo 4. Go to main mainmenu
echo:
set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto buildandpacknightly)
if %answer%==2 (goto buildandpackcanary)
if %answer%==3 (goto buildandpackstable)
if %answer%==4 (goto mainmenu)

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
goto nugettools

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

cd Scripts

build-nightly.cmd

cd ..

:buildcanary
cls

cd Scripts

build-canary.cmd

cd ..

:buildinstaller
cls

cd Scripts

build-installer.cmd
cls

cd Scripts

cd ..

:buildstable
cls

cd Scripts

build-stable.cmd

cd ..

:: ===================================================================================================

:packnightly
cls

cd Scripts

build-nightly.cmd Pack

:packcanary
cls

cd Scripts

build-canary.cmd Pack

:packinstaller
cls

cd Scripts

build-installer.cmd Pack

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

:: ===================================================================================================

:packstablelite
cls

cd Scripts

build-stable.cmd PackLite

:packstablefull
cls

cd Scripts

build-stable.cmd PackAll

:packstableboth
cls

cd Scripts

build-stable.cmd Pack

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

cd Scripts

build-nightly.cmd

:: ===================================================================================================

:nugettools
cls

cd Scripts

:: ===================================================================================================

update-nuget.cmd

:buildandcreatenugetpackages
cls

echo 1. Build nightly packages
echo 2. Build canary packages
echo 3. Build stable packages
echo 4. Build stable (lite) packages
echo 5. Go back to main menu
echo:
set /p answer="Enter number (1 - 5): "

if %answer%==1 (goto buildnightlypackages)
if %answer%==2 (goto buildcanarypackages)
if %answer%==3 (goto buildstablepackages)
if %answer%==4 (goto buildstablelitepackages)
if %answer%==5 (goto mainmenu)

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

cd Scripts

build-nightly-custom.cmd

pause

cls

echo Step 3: Pack

build-nightly-custom.cmd Pack

pause

:: ===================================================================================================

:rebuildproject
cls

cd Scripts

rebuild-build-nightly.cmd

:: ===================================================================================================

:buildandpacknightly
cls

:: goto clearproject

cd Scripts

:: build-nightly.cmd

build-nightly.cmd Pack

:buildandpackcanary
cls

cd Scripts

build-canary.cmd Pack

:buildandpackstable
cls

cd Scripts

build-stable.cmd Pack

:: ===================================================================================================

:clearlogfiles

:clearbinaries