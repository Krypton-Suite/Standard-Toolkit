@echo off

if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin" goto vs16prev
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin" goto vs16ent
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin" goto vs16pro
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin" goto vs16com
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin" goto vs16build

echo "Unable to detect suitable environment. Check if VS 2019 is installed."
pause

:vs16prev
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin
goto purge

:vs16ent
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin
goto purge

:vs16pro
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin
goto purge

:vs16com
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin
goto purge

:vs16build
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin
goto purge

:build
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% build.proj /fl /flp:logfile=build.log
goto pack

:purge
echo You are about to delete the Bin folder; do you want to continue? (Y/N)
set INPUT=
set /P INPUT=Type input: %=%
if /I "%INPUT%"=="y" goto yes
if /I "%INPUT%"=="n" goto no

:yes
rd /s /q "Bin"
goto build

:no
goto build

:pack
build.cmd Pack

pause