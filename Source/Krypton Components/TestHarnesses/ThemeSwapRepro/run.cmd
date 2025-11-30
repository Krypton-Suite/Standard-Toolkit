@echo off
setlocal enableextensions enabledelayedexpansion

REM Usage: run.cmd [cycles] [waitSeconds]
set "CYCLES=%~1"
if "%CYCLES%"=="" set "CYCLES=30"
set "WAIT=%~2"
if "%WAIT%"=="" set "WAIT=60"

set "SCRIPT_DIR=%~dp0"
set "LOGDIR=%SCRIPT_DIR%logs"
if not exist "%LOGDIR%" mkdir "%LOGDIR%" >nul 2>&1
set "THEME_SWAP_LOG=%LOGDIR%\ThemeSwapRepro.log"

set "TSR_CYCLES=%CYCLES%"
set "THEME_SWAP_LOG=%THEME_SWAP_LOG%"

echo Building ThemeSwapRepro (cycles=%CYCLES%, wait=%WAIT%s)
call dotnet build "%SCRIPT_DIR%ThemeSwapRepro.csproj" -c Debug
if errorlevel 1 goto :eof

echo Launching ThemeSwapRepro.exe
start "ThemeSwapRepro" "%SCRIPT_DIR%bin\Debug\net8.0-windows\ThemeSwapRepro.exe"

echo Waiting %WAIT% seconds for automation to complete...
timeout /t %WAIT% /nobreak >nul

echo.
echo ===== Log at "%THEME_SWAP_LOG%" =====
if exist "%THEME_SWAP_LOG%" (
  type "%THEME_SWAP_LOG%"
) else (
  echo Log file not found yet. It may still be writing. Look under "%LOGDIR%".
)

endlocal
exit /b 0
