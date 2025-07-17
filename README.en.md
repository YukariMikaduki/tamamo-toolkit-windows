# TamamoToolkit.Windows

[![Platforms](https://img.shields.io/badge/platform-net6.0--windows_|_net8.0--windows-blue.svg?logo=githubpages)](https://github.com/YukariMikaduki/tamamo-toolkit-windows)
[![NuGet Package](https://img.shields.io/nuget/v/TamamoToolkit.Windows.svg?logo=nuget)](https://www.nuget.org/packages/TamamoToolkit.Windows)
[![License](https://img.shields.io/github/license/YukariMikaduki/tamamo-toolkit-windows.svg?logo=github)](https://github.com/YukariMikaduki/tamamo-toolkit-windows/blob/main/LICENSE)

- [Project URL](https://github.com/YukariMikaduki/tamamo-toolkit-windows)
- [NuGet Package](https://www.nuget.org/packages/TamamoToolkit.Windows)

## README  

This module is a collection of utilities integrated to facilitate daily development work, including but not limited to:
- Common used DllImport functions
	- `TamamoToolkit.DllImport` namespace
- Various extension methods for code simplification
	- `TamamoToolkit.Extensions` namespace
	- `TamamoToolkit.Utils` namespace

## v2.1.1 Update Details

- Fixed a bug: This bug would cause pixel row tearing in WriteableBitmap when updating with an array if the original image stride was not a multiple of 4.

## [More Changelog](https://github.com/YukariMikaduki/tamamo-toolkit-windows/blob/main/CHANGELOG.en.md)