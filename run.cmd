@echo off

cls

echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. End

set /p answer="Enter number (1 - 4):"
if %answer%==1 (goto mainmenuchoice1)
if %answer%==2 (goto mainmenuchoice2)
if %answer%==3 (goto mainmenuchoice3)
if %answer%==4 (goto mainmenuchoice4)

@echo Invalid input, please try again.

pause

goto mainmenu

:: ===================================================================================================

:mainmenu
cls
echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. End

set /p answer="Enter number (1 - 4):"
if %answer%==1 (goto mainmenuchoice1)
if %answer%==2 (goto mainmenuchoice2)
if %answer%==3 (goto mainmenuchoice3)
if %answer%==4 (goto mainmenuchoice4)

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

set /p answer="Enter number (1 - 9):"
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
echo 1. Build nightly NuGet packages
echo 2. Build canary NuGet packages
echo 3. Build stable NuGet packages
echo 4. Build signed NuGet packages
echo 5. Go back to main menu

set /p answer="Enter number (1 - 5):"
if %answer%==1 (goto packnightly)
if %answer%==2 (goto packcanary)
if %answer%==3 (goto packstable)
if %answer%==4 (goto packsigned)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto packmenu

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

:mainmenuchoice1
goto cleanproject

:mainmenuchoice2
goto buildmenu

:mainmenuchoice3
goto packmenu

:mainmenuchoice4
exit

:: ===================================================================================================

:buildnightlyusingvisualstudio2019
cd Scripts

build-nightly-2019.cmd

:buildnightlyusingvisualstudio2022
cd Scripts

build-nightly-2022.cmd

:buildcanaryusingvisualstudio2019
cd Scripts

build-canary-2019.cmd

:buildcanaryusingvisualstudio2022
cd Scripts

build-canary-2022.cmd

:buildinstallerusingvisualstudio2019
cd Scripts

build-installer-2019.cmd

:buildinstallerusingvisualstudio2022
cd Scripts

build-installer-2022.cmd

:buildsignedusingvisualstudio2019
cd Scripts

build-signed-2019.cmd

:buildsignedusingvisualstudio2022
cd Scripts

build-signed-2022.cmd

:buildstableusingvisualstudio2019
cd Scripts

build-stable-2019.cmd

:buildstableusingvisualstudio2022
cd Scripts

build-stable-2022.cmd

:: ===================================================================================================

:packcanary
cd Scripts

pack-canary.cmd

:packinstaller
cd Scripts

pack-installer.cmd

:packnightly
cd Scripts

pack-nightly.cmd

:packsigned
cd Scripts

pack-signed.cmd

:packstable
cd Scripts

pack-stable.cmd