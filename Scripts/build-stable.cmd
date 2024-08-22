@echo off

if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin" goto vs17prev
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin" goto vs17ent
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin" goto vs17pro
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin" goto vs17com
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin" goto vs17build

echo "Unable to detect suitable environment. Check if VS 2022 is installed."

goto end

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
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Started to build Stable release
@echo:
@echo Started: %date% %time% %zone%
@echo:
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% build.proj /fl /flp:logfile=../Logs/stable-build-log.log /bl:../Logs/stable-build-log.binlog /clp:Summary;ShowTimestamp /v:quiet
@echo:
@echo Stable release build completed: %date% %time% %zone%
@echo:
@echo You can find the build Logs in ../Logs
@echo:
pause

@echo Do you want to return to complete another task? (Y/N)
@echo:
set /p answer="Enter input: "
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:run
main-menu.cmd

:end
pause