@echo off

echo You are about to delete the Bin folder; do you want to continue? (Y/N)
set INPUT=
set /P INPUT=Type input: %=%
if /I "%INPUT%"=="y" goto yes
if /I "%INPUT%"=="n" goto no

:yes
rd /s /q "Bin"
rd /s /q "Source\Krypton Components\Krypton.Docking\obj"
rd /s /q "Source\Krypton Components\Krypton.Navigator\obj"
rd /s /q "Source\Krypton Components\Krypton.Ribbon\obj"
rd /s /q "Source\Krypton Components\Krypton.Toolkit\obj"
rd /s /q "Source\Krypton Components\Krypton.Workspace\obj"

:no
echo Do you now want to build the repository? (y/n)
set INPUT=
set /PINPUT=Type input: %=%
if /I "%INPUT%"=="y" goto buildproject
if /I "%INPUT%"=="n" goto break

:buildproject
buildsolution.cmd

echo Do you now want to create NuGet packages? (y/n)
set INPUT=
set /PINPUT=Type input: %=%
if /I "%INPUT%"=="y" goto createpackages
if /I "%INPUT%"=="n" goto break

:createpackages
build.cmd Pack

pause

:break
pause