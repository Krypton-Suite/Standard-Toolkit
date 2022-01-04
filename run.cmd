@echo off

if exist Bin ( goto purge ) else ( goto no )

:purge
echo You are about to delete the Bin folder; do you want to continue? (Y/N)
set INPUT=
set /P INPUT=Type input: %=%
if /I "%INPUT%"=="y" goto yes
if /I "%INPUT%"=="n" goto no

:yes
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

:no
echo Do you now want to build the repository? (y/n)
set INPUT=
set /PINPUT=Type input: %=%
if /I "%INPUT%"=="y" goto buildproject
if /I "%INPUT%"=="n" goto break

:buildproject
buildsolution.cmd

echo Do you now want to create NuGet packages? (REMINDER: If you are creating Nightly packages, please ensure that the 'CurrentDate' adheres to the correct ISO format, i.e. yyyyMMdd) (y/n)
set INPUT=
set /PINPUT=Type input: %=%
if /I "%INPUT%"=="y" goto createpackages
if /I "%INPUT%"=="n" goto break

:createpackages
echo Do you want to pack using Visual Studio 2019 or 2022? (2019/2022)
set INPUT=
set /P INPUT=Type 2019 or 2022: %=%
if /I "%INPUT%"=="2019" goto vs2019pack
if /I "%INPUT%"=="2022" goto vs2022pack

:vs2019pack
build-2019.cmd Pack

:vs2022pack
build-2022.cmd Pack

pause

:break
pause