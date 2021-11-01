# How To: Fix the components in the designer

If you are experiencing issues while using the components in the designer, you might need to replace `<TargetFramework>net5.0-windows</TargetFramework>` with `<TargetFrameworks>net48;net5.0-windows</TargetFrameworks>` in your project configuration files.

To learn more about the `TargetFrameworks` attribute, [click here](https://docs.microsoft.com/en-us/dotnet/standard/frameworks).

## N.B: This action will produce binaries for multiple frameworks.