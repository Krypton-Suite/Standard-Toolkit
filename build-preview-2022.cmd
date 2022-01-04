@echo off

if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin" goto vs17prev
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin" goto vs17ent
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin" goto vs17pro
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin" goto vs17com
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin" goto vs17build

echo "Unable to detect suitable environment. Check if VS 2022 is installed."

pause

:vs17prev
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin
goto build

:vs17ent
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin
goto build

:vs17pro
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin
goto build

:vs17com
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin
goto build

:vs17build
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin
goto build

:build
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% preview.proj /fl /flp:logfile=build.log

echo Plese alter file '{Path}\Directory.Build.props' before executing 'publish.cmd' script!

pause