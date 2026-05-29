@echo off

set "DOTNET_ROLL_FORWARD_TO_PRERELEASE=1"

for /f "delims=" %%S in ('dir /b /ad /o-n "%ProgramFiles%\dotnet\sdk\11.*" 2^>nul') do (
    set "DOTNET_11_SDK_VERSION=%%S"
    set "DOTNET_11_SDK_ROOT=%ProgramFiles%\dotnet\sdk\%%S"
    set "MSBuildSDKsPath=%ProgramFiles%\dotnet\sdk\%%S\Sdks"
    set "DOTNET_MSBUILD_SDK_RESOLVER_CLI_DIR=%ProgramFiles%\dotnet"
    set "DOTNET_MSBUILD_SDK_RESOLVER_SDKS_DIR=%ProgramFiles%\dotnet\sdk\%%S\Sdks"
    set "DOTNET_MSBUILD_SDK_RESOLVER_SDKS_VER=%%S"
    exit /b 0
)

echo "Unable to detect .NET 11 SDK under %ProgramFiles%\dotnet\sdk."
exit /b 1
