@echo off

if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin" goto vs16prev
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin" goto vs16ent
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin" goto vs16pro
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin" goto vs16com
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin" goto vs16build

echo "Unable to detect suitable environment. Check if VS 2019 is installed."

pause
goto exitbatch

:vs16prev
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin"
goto build

:vs16ent
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin"
goto build

:vs16pro
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin"
goto build

:vs16com
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"
goto build

:vs16build
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin"
goto build

:build
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Rebuild Started: %date% %time% %zone%
@echo
set targets=Rebuild
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /m -t:%targets% "%~dp0nightly.proj" /fl /flp:logfile="%~dp0..\..\Logs\nightly-build-log.log" /bl:"%~dp0..\..\Logs\nightly-build-log.binlog"  /clp:Summary;ShowTimestamp /v:quiet

:: -t:rebuild

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
cd ../..

run.cmd

:exitbatch