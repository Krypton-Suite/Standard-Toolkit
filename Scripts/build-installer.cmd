@echo off

if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin" goto vs17prev
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin" goto vs17ent
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin" goto vs17pro
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin" goto vs17com
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin" goto vs17build

echo "Unable to detect suitable environment. Check if VS 2022 is installed."

pause
goto exitbatch

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

@echo Started: %date% %time% %zone%
@echo
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% ../Project Files/installer.proj /fl /flp:logfile=../Logs/installer-build-log.log /bl:../Logs/installer-build-log.binlog /clp:Summary;ShowTimestamp /v:quiet

@echo Build Completed: %date% %time% %zone%

pause

@echo Do you want to return to complete another task? (Y/N)
set /p answer="Enter input: "
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:run
main-menu.cmd