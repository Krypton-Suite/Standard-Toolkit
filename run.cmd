:: Last updated: Sunday 23rd April, 2023 @ 10:00

@echo off

title Krypton Toolkit Build System

cls

@echo Welcome to the Krypton Toolkit Build system, version: 1.8c. Please select an option below.

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

echo 1. Build nightly version using Visual Studio 2022
echo    a. Rebuild project
echo 2. Build canary version using Visual Studio 2022
echo 3. Build stable version using Visual Studio 2022
echo 4. Go back to main menu

set /p answer="Enter number or letter (1 - 4, a - *): "
if %answer%==1 (goto buildnightlyusingvisualstudio2022)
if %answer%==a (goto rebuildproject)
if %answer%==2 (goto buildcanaryusingvisualstudio2022)
if %answer%==3 (goto buildstableusingvisualstudio2022)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto buildmenu

:packmenu
cls

echo 1. Pack nightly version using Visual Studio 2022
echo 2. Pack canary version using Visual Studio 2022
echo 3. Pack stable version using Visual Studio 2022
echo 4. Go back to main menu

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packnightlyusingvisualstudio2022)
if %answer%==2 (goto packcanaryusingvisualstudio2022)
if %answer%==3 (goto packstableusingvisualstudio2022)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packmenu

:debugmenu
cls

echo 1. Debug using Visual Studio 2022
echo 2. Go back to main mainmenu

set /p answer="Enter number (1 - 2): "
if %answer%==1 (goto debugusingvisualstudio2022)
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
echo Deleting the 'build.log' file
del /f build.log
echo Deleted the 'build.log' file

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

:buildnightlyusingvisualstudio2022
cls

cd Scripts

build-nightly-2022.cmd

cd ..

:buildcanaryusingvisualstudio2022
cls

cd Scripts

build-canary-2022.cmd

cd ..

:buildinstallerusingvisualstudio2022
cls

cd Scripts

build-installer-2022.cmd
cls

cd Scripts

cd ..

:buildstableusingvisualstudio2022
cls

cd Scripts

build-stable-2022.cmd

cd ..

:: ===================================================================================================

:packnightlyusingvisualstudio2022
cls

cd Scripts

build-nightly-2022.cmd Pack

:packcanaryusingvisualstudio2022
cls

cd Scripts

build-canary-2022.cmd Pack

:packinstallerusingvisualstudio2022
cls

cd Scripts

build-installer-2022.cmd Pack

:packstableusingvisualstudio2022
cls

echo 1. Produce 'Lite' stable packages
echo 2. Produce 'full' stable packages
echo 3. Produce 'full/lite' stable packages
echo 4. Go back to main menu

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packstableusingvisualstudio2022lite)
if %answer%==2 (goto packstableusingvisualstudio2022full)
if %answer%==3 (goto packstableusingvisualstudio2022both)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packstableusingvisualstudio2022

:: ===================================================================================================

:packstableusingvisualstudio2022lite
cls

cd Scripts

build-stable-2022.cmd PackLite

:packstableusingvisualstudio2022full
cls

cd Scripts

build-stable-2022.cmd PackAll

:packstableusingvisualstudio2022both
cls

cd Scripts

build-stable-2022.cmd Pack

:: ===================================================================================================

:debugusingvisualstudio2022
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

build-nightly-2022.cmd

:: ===================================================================================================

:nugettools
cls

cd Scripts

:: ===================================================================================================

update-nuget.cmd

:buildandcreatenugetpackages
cls

echo 1. Build nightly packages using Visual Studio 2022
echo 2. Build canary packages using Visual Studio 2022
echo 3. Build stable packages using Visual Studio 2022
echo 4. Build stable (lite) packages using Visual Studio 2022
echo 5. Go back to main menu

set /p answer="Enter number (1 - 5): "

if %answer%==1 (goto buildnightlypackagesusingvisualstudio2022)
if %answer%==2 (goto buildcanarypackagesusingvisualstudio2022)
if %answer%==3 (goto buildstablepackagesusingvisualstudio2022)
if %answer%==4 (goto buildstablelitepackagesusingvisualstudio2022)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

:: ===================================================================================================

:buildnightlypackagesusingvisualstudio2022
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

build-nightly-2022-custom.cmd

pause

cls

echo Step 3: Pack

build-nightly-2022-custom.cmd Pack

pause

:: ===================================================================================================

:rebuildproject
cls

cd Scripts

rebuild-build-nightly-2022.cmd

:: ===================================================================================================


:: ===================================================================================================