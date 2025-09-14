# WebView2 SDK Setup for KryptonWebView2

## Overview

The KryptonWebView2 control requires the Microsoft WebView2 SDK assemblies to be available for compilation. This document explains how to set up the WebView2 SDK without using NuGet dependencies.

## Setup Options

### Option 1: Manual Download (Recommended)

1. **Download WebView2 SDK**
   - Visit: https://developer.microsoft.com/en-us/microsoft-edge/webview2/
   - Download the latest WebView2 SDK
   - Extract the downloaded package

2. **Copy Required Assemblies**
   - Create a `WebView2SDK` folder at the repository root (same level as `Source` folder)
   - Copy the following files from the extracted SDK to `WebView2SDK`:
     ```
     WebView2SDK/
     ├── Microsoft.Web.WebView2.Core.dll
     ├── Microsoft.Web.WebView2.WinForms.dll
     └── WebView2Loader.dll
     ```

3. **Build the Solution**
   - Rebuild the entire solution
   - The KryptonWebView2 control should now compile successfully

### Option 2: Using NuGet (Temporary)

If you want to quickly get the assemblies without manual download:

1. **Install NuGet Package**
   ```cmd
   dotnet add package Microsoft.Web.WebView2 --version 1.0.2440.47
   ```

2. **Copy Assemblies**
   - Find the NuGet packages folder (usually `%USERPROFILE%\.nuget\packages\`)
   - Navigate to `microsoft.web.webview2\1.0.2440.47\lib\net45\`
   - Copy the required DLLs to the `WebView2SDK` folder
   - Remove the NuGet package reference if desired

3. **Clean Up**
   ```cmd
   dotnet remove package Microsoft.Web.WebView2
   ```

### Option 3: Using Setup Script

Run the provided setup script:
```cmd
Setup-WebView2SDK.cmd
```

This script will:
- Create the WebView2SDK directory
- Provide instructions for manual assembly copying
- Guide you through the setup process

## File Structure After Setup

Your repository should look like this:
```
Standard-Toolkit/
├── Source/
│   └── Krypton Components/
│       └── Krypton.Toolkit/
│           └── Controls Toolkit/
│               └── KryptonWebView2.cs
├── WebView2SDK/                    ← Created by you
│   ├── Microsoft.Web.WebView2.Core.dll
│   ├── Microsoft.Web.WebView2.WinForms.dll
│   └── WebView2Loader.dll
└── Setup-WebView2SDK.cmd
```

## Verification

After setup, verify that the KryptonWebView2 control compiles:

1. **Build the Solution**
   ```cmd
   dotnet build "Source/Krypton Components/Krypton Toolkit Suite 2022 - VS2022.sln"
   ```

2. **Check for Errors**
   - No compilation errors related to WebView2 types
   - KryptonWebView2 appears in the toolbox
   - Test form compiles successfully

## Troubleshooting

### "WebView2 types not found" Error
- Ensure the WebView2SDK folder exists at the repository root
- Verify all three DLL files are present in WebView2SDK
- Check that the project file references point to the correct paths
- Rebuild the solution completely

### Assembly Loading Errors
- Ensure the DLLs are the correct architecture (AnyCPU or x64)
- Check that the DLLs are not corrupted
- Verify the file permissions allow reading

### Designer Issues
- Reset the Visual Studio toolbox
- Close and reopen Visual Studio
- Ensure the control compiles without errors first

## Runtime Requirements

Remember that end users need the WebView2 Runtime installed:

- **Evergreen Distribution**: Users install from Microsoft's website
- **Fixed Version**: Bundle with your application
- **Download**: https://developer.microsoft.com/en-us/microsoft-edge/webview2/

## Support

If you encounter issues:

1. Check the WebView2 documentation: https://docs.microsoft.com/en-us/microsoft-edge/webview2/
2. Verify your setup matches the requirements
3. Test with the provided KryptonWebView2Test form
4. Report issues to the Krypton Toolkit repository

## Notes

- The WebView2SDK folder should be added to `.gitignore` if you don't want to commit the assemblies
- The assemblies are referenced with `Private=False` so they won't be copied to output
- This setup allows the control to compile without NuGet dependencies
- Runtime WebView2 functionality requires the WebView2 Runtime to be installed on target systems
