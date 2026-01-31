# Bundled WebView2 assemblies

This folder contains the Microsoft WebView2 managed assemblies used by the KryptonWebView2 control. The project references these DLLs directly when present; no NuGet PackageReference is used.

## Required files

- `Microsoft.Web.WebView2.Core.dll`
- `Microsoft.Web.WebView2.WinForms.dll`
- `WebView2Loader.dll` (optional; used at runtime by WebView2)

## How to populate

From the repository root, run:

```
Scripts\WebVew2\Populate-BundledWebView2.cmd
```

This downloads the latest stable Microsoft.Web.WebView2 NuGet package and copies the assemblies here. Without these DLLs, the project builds but KryptonWebView2 is not compiled (WEBVIEW2_AVAILABLE is not set). After populating, rebuild to include WebView2 support.

## License

The WebView2 SDK is licensed under the MIT License. See the package license for details.
