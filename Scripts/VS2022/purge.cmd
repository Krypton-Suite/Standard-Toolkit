@echo off

echo You are about to delete the Bin folder; do you want to continue? (Y/N)
set INPUT=
set /P INPUT=Type input: %=%
if /I "%INPUT%"=="y" goto yes
if /I "%INPUT%"=="n" goto no

:yes
echo Deleting the 'Bin' folder
rd /s /q "..\Bin"
echo Deleted the 'Bin' folder
echo Deleting the 'Krypton.Docking\obj' folder
rd /s /q "..\Source\Krypton Components\Krypton.Docking\obj"
echo Deleted the 'Krypton.Docking\obj' folder
echo Deleting the 'Krypton.Navigator\obj' folder
rd /s /q "..\Source\Krypton Components\Krypton.Navigator\obj"
echo Deleted the 'Krypton.Navigator\obj' folder
echo Deleting the 'Krypton.Ribbon\obj' folder
rd /s /q "..\Source\Krypton Components\Krypton.Ribbon\obj"
echo Deleted the 'Krypton.Ribbon\obj' folder
echo Deleting the 'Krypton.Toolkit\obj' folder
rd /s /q "..\Source\Krypton Components\Krypton.Toolkit\obj"
echo Deleted the 'Krypton.Toolkit\obj' folder
echo Deleting the 'Krypton.Workspace\obj' folder
rd /s /q "..\Source\Krypton Components\Krypton.Workspace\obj"
echo Deleted the 'Krypton.Workspace\obj' folder
if exist "..\Logs" ( goto deletelogsdirectory ) else echo Directory 'Logs' not found

:deletelogsdirectory
echo Deleting the 'Logs' directory
del /f "..\Logs"

:no
pause