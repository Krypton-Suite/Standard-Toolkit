:: Last updated: Thursday 23rd May, 2024 @ 18:00

@echo off

title Krypton Toolkit Build System

cls

@echo Welcome to the Krypton Toolkit Build system, version: 2.1. Please select an option below.

@echo ==============================================================================================

echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
::echo 4. Rebuild project
::echo 4. Clean, Build and create NuGet packages
echo 4. Debug project
echo 5. NuGet Tools
echo 6. End

set /p answer="Enter number (1 - 6): "
if %answer%==1 (goto cleanproject)
if %answer%==2 (goto buildproject)
if %answer%==3 (goto createnugetpackages)
::if %answer%==4 (goto rebuildproject)
::if %answer%==4 (goto buildandcreatenugetpackages)
if %answer%==4 (goto debugproject)
if %answer%==5 (goto nugettools)
if %answer%==6 (goto exitbuildsystem)

@echo Invalid input, please try again.

pause

goto mainmenu

:: ===================================================================================================

:mainmenu

cls

echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
::echo 4. Rebuild project
::echo 4. Clean, Build and create NuGet packages
echo 4. Debug project
echo 5. NuGet Tools
echo 6. End

set /p answer="Enter number (1 - 6): "
if %answer%==1 (goto cleanproject)
if %answer%==2 (goto buildproject)
if %answer%==3 (goto createnugetpackages)
::if %answer%==4 (goto rebuildproject)
::if %answer%==4 (goto buildandcreatenugetpackages)
if %answer%==4 (goto debugproject)
if %answer%==5 (goto nugettools)
if %answer%==6 (goto exitbuildsystem)

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

set /p answer="Enter number (1 - 2): "
if %answer%==1 (goto debug)
if %answer%==2 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto debugmenu

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
echo Deleting the 'build-log.log' file
del /f "Logs\build-log.log"
echo Deleted the 'build-log.log' file
echo Deleting the 'build-log.binlog' file
del /f "Logs\build-log.binlog"
echo Deleted the 'build-log.binlog' file

pause

goto mainmenu

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


:: ===================================================================================================