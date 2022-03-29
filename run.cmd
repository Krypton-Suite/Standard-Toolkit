:: Last updated: Tuesday 29th March 2022 @ 10:00 AM

@echo off

cls

echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. Debug project
echo 5. End

set /p answer="Enter number (1 - 5): "
if %answer%==1 (goto mainmenuchoice1)
if %answer%==2 (goto mainmenuchoice2)
if %answer%==3 (goto mainmenuchoice3)
if %answer%==4 (goto mainmenuchoice4)
if %answer%==5 (goto mainmenuchoice5)

@echo Invalid input, please try again.

pause

goto mainmenu

:: ===================================================================================================

:mainmenu
cls

echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. Debug project
echo 5. End

set /p answer="Enter number (1 - 5): "
if %answer%==1 (goto mainmenuchoice1)
if %answer%==2 (goto mainmenuchoice2)
if %answer%==3 (goto mainmenuchoice3)
if %answer%==4 (goto mainmenuchoice4)
if %answer%==5 (goto mainmenuchoice5)

@echo Invalid input, please try again.

pause

goto mainmenu

:buildmenu
cls

echo 1. Build nightly version using Visual Studio 2019
echo 2. Build nightly version using Visual Studio 2022
echo 3. Build canary version using Visual Studio 2019
echo 4. Build canary version using Visual Studio 2022
echo 5. Build stable version using Visual Studio 2019
echo 6. Build stable version using Visual Studio 2022
echo 7. Build signed version using Visual Studio 2019
echo 8. Build signed version using Visual Studio 2022
echo 9. Go back to main menu

set /p answer="Enter number (1 - 9): "
if %answer%==1 (goto buildnightlyusingvisualstudio2019)
if %answer%==2 (goto buildnightlyusingvisualstudio2022)
if %answer%==3 (goto buildcanaryusingvisualstudio2019)
if %answer%==4 (goto buildcanaryusingvisualstudio2022)
if %answer%==5 (goto buildstableusingvisualstudio2019)
if %answer%==6 (goto buildstableusingvisualstudio2022)
if %answer%==7 (goto buildsignedusingvisualstudio2019)
if %answer%==8 (goto buildsignedusingvisualstudio2022)
if %answer%==9 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto buildmenu

:packmenu
cls

echo 1. Pack nightly version using Visual Studio 2019
echo 2. Pack nightly version using Visual Studio 2022
echo 3. Pack canary version using Visual Studio 2019
echo 4. Pack canary version using Visual Studio 2022
echo 5. Pack stable version using Visual Studio 2019
echo 6. Pack stable version using Visual Studio 2022
echo 7. Pack signed version using Visual Studio 2019
echo 8. Pack signed version using Visual Studio 2022
echo 9. Go back to main menu

set /p answer="Enter number (1 - 9): "
if %answer%==1 (goto packnightlyusingvisualstudio2019)
if %answer%==2 (goto packnightlyusingvisualstudio2022)
if %answer%==3 (goto packcanaryusingvisualstudio2019)
if %answer%==4 (goto packcanaryusingvisualstudio2022)
if %answer%==5 (goto packstableusingvisualstudio2019)
if %answer%==6 (goto packstableusingvisualstudio2022)
if %answer%==7 (goto packsignedusingvisualstudio2019)
if %answer%==8 (goto packsignedusingvisualstudio2022)
if %answer%==9 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packmenu

:debugmenu
cls

echo 1. Debug using Visual Studio 2019
echo 2. Debug using Visual Studio 2022
echo 3. Go back to main mainmenu

set /p answer="Enter number (1 - 3): "
if %answer%==1 (goto debugusingvisualstudio2019)
if %answer%==2 (goto debugusingvisualstudio2022)
if %answer%==3 (goto mainmenu)

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

:mainmenuchoice1
goto cleanproject

:mainmenuchoice2
goto buildmenu

:mainmenuchoice3
goto packmenu

:mainmenuchoice4
goto debugmenu

:mainmenuchoice5
exit

:: ===================================================================================================

:buildnightlyusingvisualstudio2019
cls

cd Scripts

build-nightly-2019.cmd

:buildnightlyusingvisualstudio2022
cls

cd Scripts

build-nightly-2022.cmd

:buildcanaryusingvisualstudio2019
cls

cd Scripts

build-canary-2019.cmd

:buildcanaryusingvisualstudio2022
cls

cd Scripts

build-canary-2022.cmd

:buildinstallerusingvisualstudio2019
cls

cd Scripts

build-installer-2019.cmd

:buildinstallerusingvisualstudio2022
cls

cd Scripts

build-installer-2022.cmd

:buildsignedusingvisualstudio2019
cls

cd Scripts

build-signed-2019.cmd

:buildsignedusingvisualstudio2022
cls

cd Scripts

build-signed-2022.cmd

:buildstableusingvisualstudio2019
cls

cd Scripts

build-stable-2019.cmd

:buildstableusingvisualstudio2022
cls

cd Scripts

build-stable-2022.cmd

:: ===================================================================================================

:packnightlyusingvisualstudio2019
cls

cd Scripts

build-nightly-2019.cmd Pack

:packnightlyusingvisualstudio2022
cls

cd Scripts

build-nightly-2022.cmd Pack

:packcanaryusingvisualstudio2019
cls

cd Scripts

build-canary-2019.cmd Pack

:packcanaryusingvisualstudio2022
cls

cd Scripts

build-canary-2022.cmd Pack

:packinstallerusingvisualstudio2019
cls

cd Scripts

build-installer-2019.cmd Pack

:packinstallerusingvisualstudio2022
cls

cd Scripts

build-installer-2022.cmd Pack

:packsignedusingvisualstudio2019
cls

echo 1. Produce 'Lite' signed packages
echo 2. Produce 'full' signed packages
echo 3. Produce 'full/lite' signed packages
echo 4. Go back to main menu

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packsignedusingvisualstudio2019lite)
if %answer%==2 (goto packsignedusingvisualstudio2019full)
if %answer%==3 (goto packsignedusingvisualstudio2019both)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packsignedusingvisualstudio2019

:packsignedusingvisualstudio2022
cls

echo 1. Produce 'Lite' signed packages
echo 2. Produce 'full' signed packages
echo 3. Produce 'full/lite' signed packages
echo 4. Go back to main menu

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packsignedusingvisualstudio2022lite)
if %answer%==2 (goto packsignedusingvisualstudio2022full)
if %answer%==3 (goto packsignedusingvisualstudio2022both)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packsignedusingvisualstudio2022

:packstableusingvisualstudio2019
cls

echo 1. Produce 'Lite' stable packages
echo 2. Produce 'full' stable packages
echo 3. Produce 'full/lite' stable packages
echo 4. Go back to main menu

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto packstableusingvisualstudio2019lite)
if %answer%==2 (goto packstableusingvisualstudio2019full)
if %answer%==3 (goto packstableusingvisualstudio2019both)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packstableusingvisualstudio2019

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

:packsignedusingvisualstudio2019lite
cls

cd Scripts

build-signed-2019.cmd PackLite

:packsignedusingvisualstudio2019full
cls

cd Scripts

build-signed-2019.cmd PackAll

:packsignedusingvisualstudio2019both
cls

cd Scripts

build-signed-2019.cmd Pack

:packsignedusingvisualstudio2022lite
cls

cd Scripts

build-signed-2022.cmd PackLite

:packsignedusingvisualstudio2022full
cls

cd Scripts

build-signed-2022.cmd PackAll

:packsignedusingvisualstudio2022both
cls

cd Scripts

build-signed-2022.cmd Pack

:packstableusingvisualstudio2019lite
cls

cd Scripts

build-stable-2019.cmd PackLite

:packstableusingvisualstudio2019full
cls

cd Scripts

build-stable-2019.cmd PackAll

:packstableusingvisualstudio2019both
cls

cd Scripts

build-stable-2019.cmd Pack

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

:debugusingvisualstudio2019
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

build-nightly-2019.cmd

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