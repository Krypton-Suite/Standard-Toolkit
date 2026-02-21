# Bundled WebView2 assemblies

This folder contains the Microsoft WebView2 managed assemblies used by the KryptonWebView2 control. The project references these DLLs directly so no NuGet restore or CI setup is required for WebView2.

## Required files

- `Microsoft.Web.WebView2.Core.dll`
- `Microsoft.Web.WebView2.WinForms.dll`
- `WebView2Loader.dll` (optional; used at runtime by WebView2)

## How to populate

From the repository root, run:

```
Scripts\WebVew2\Populate-BundledWebView2.cmd
```

This always downloads the **latest stable** Microsoft.Web.WebView2 NuGet package and copies the assemblies here. After running once, you can commit the DLLs so CI and other developers don't need to run the script. CI workflows also populate this folder with the latest version on each run.

## Updating

To update to a newer WebView2 version, run the same script again (it will overwrite with the latest version from NuGet), then rebuild and commit.

## License

The WebView2 SDK is licensed under the MIT License. See the package license for details.
