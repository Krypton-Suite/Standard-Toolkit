@echo off
setlocal EnableExtensions

goto setmenu

:setmenu

cls

echo 1. Update NuGet.exe (nuget update -Self)
echo 2. Clear NuGet HTTP cache (nuget locals http-cache -clear)
echo 3. Go back to main menu
echo 4. End
echo:
set /p answer="Enter number (1 - 4): "
if "%answer%"=="1" (goto updatenuget)
if "%answer%"=="2" (goto clearnugethttpcache)
if "%answer%"=="3" (goto backtorun)
if "%answer%"=="4" (goto end)

@echo Invalid input, please try again.

pause

goto setmenu

:backtorun
:: Exit code 2: caller (run.cmd) branches to main menu. Do not start a nested run.cmd
:: here; that stops the parent batch and the menu is never reached reliably.
endlocal
exit /b 2

:updatenuget
cls

where nuget >nul 2>&1
if errorlevel 1 (
    echo ERROR: nuget.exe was not found on PATH.
    echo Install the NuGet CLI or add it to your PATH, then try again.
    echo.
    pause
    goto setmenu
)

nuget update -Self

pause

goto setmenu

:clearnugethttpcache
cls

where nuget >nul 2>&1
if errorlevel 1 (
    echo ERROR: nuget.exe was not found on PATH.
    echo.
    pause
    goto setmenu
)

nuget locals http-cache -clear

pause

goto setmenu

:end
@echo Exiting the build system, have a good day. Bye!

pause

endlocal
exit /b 0
