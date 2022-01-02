@echo off

echo Clean project (1)
echo Build Krypton Toolkit (2)
echo Create NuGet packages (3)
echo End (4)

:mainmenu
goto clearscreen
echo 1. Clean project
echo 2. Build Krypton Toolkit
echo 3. Create NuGet packages
echo 4. End

set /p answer="Enter number:"
if %answer%==1 (goto mainmenuchoice1)
if %answer%==2 (goto mainmenuchoice2)
if %answer%==3 (goto mainmenuchoice3)
if %answer%==4 (goto mainmenuchoice4)

:buildmenu
echo 1. Build nightly version using Visual Studio 2019
echo 2. Build nightly version using Visual Studio 2022
echo 3. Build canary version using Visual Studio 2019
echo 4. Build canary version using Visual Studio 2022
echo 5. Build stable version using Visual Studio 2019
echo 6. Build stable version using Visual Studio 2022
echo 7. Build signed version using Visual Studio 2019
echo 8. Build signed version using Visual Studio 2022
echo 9. Go back to main menu

set /p answer="Enter number:"
if %answer%==1 (goto buildnightlyusingvisualstudio2019)
if %answer%==2 (goto buildnightlyusingvisualstudio2022)
if %answer%==3 (goto buildcanaryusingvisualstudio2019)
if %answer%==4 (goto buildcanaryusingvisualstudio2022)
if %answer%==5 (goto buildstableusingvisualstudio2019)
if %answer%==6 (goto buildstableusingvisualstudio2022)
if %answer%==7 (goto buildsignedusingvisualstudio2019)
if %answer%==8 (goto buildsignedusingvisualstudio2022)
if %answer%==9 (goto mainmenu)

:packmenu
echo 1. Build nightly NuGet packages using Visual Studio 2019
echo 2. Build nightly NuGet packages using Visual Studio 2022
echo 3. Build canary NuGet packages using Visual Studio 2019
echo 4. Build canary NuGet packages using Visual Studio 2022
echo 5. Build stable NuGet packages using Visual Studio 2019
echo 6. Build stable NuGet packages using Visual Studio 2022
echo 7. Build signed NuGet packages using Visual Studio 2019
echo 8. Build signed NuGet packages using Visual Studio 2022
echo 9. Go back to main menu

:clearscreen
cls

:hold
pause

:: ===================================================================================================

:cleanproject
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

goto mainmenu

:mainmenuchoice1
goto clearscreen
goto cleanproject

:mainmenuchoice2
goto clearscreen
goto buildmenu

:mainmenuchoice3
goto clearscreen
goto packmenu

:mainmenuchoice4
exit

:buildnightlyusingvisualstudio2019
build-nightly-2019.cmd

:buildnightlyusingvisualstudio2022
build-nightly-2022.cmd

:buildcanaryusingvisualstudio2019
build-canary-2019.cmd

:buildcanaryusingvisualstudio2022
build-canary-2022.cmd

:buildinstallerusingvisualstudio2019
build-installer-2019.cmd

:buildinstallerusingvisualstudio2022
build-installer-2022.cmd

:buildsignedusingvisualstudio2019
build-signed-2019.cmd

:buildsignedusingvisualstudio2022
build-signed-2022.cmd

:buildstableusingvisualstudio2019
build-stable-2019.cmd

:buildstableusingvisualstudio2022
build-stable-2022.cmd