## v2.2.0 更新内容

- [TamamoToolkit](https://www.nuget.org/packages/TamamoToolkit) 包的依赖升级为2.2.0版本

## v2.1.4 更新内容

- [TamamoToolkit](https://www.nuget.org/packages/TamamoToolkit) 包的依赖升级为2.1.0版本

## v2.1.3 更新内容

- 添加一个扩展方法：获取 BitmapSource 的像素数组

## v2.1.2 更新内容

- 修复了一个BUG：该BUG曾导致将 BitmapSource 保存为PNG、JPG或BMP时，偶发保存路径拒绝访问的问题

## v2.1.1 更新内容

- 修复了一个BUG：该BUG曾导致用数组更新 WriteableBitmap 时，若图像原始步长不为4的倍数，图像行撕裂的问题

## v2.1.0 更新内容

- 添加将 BitmapSource 保存为PNG、JPG或BMP的扩展方法

## v2.0.1 更新内容

- 项目正式更名为 **TamamoToolkit.Windows**，所有命名空间均已调整
	- Chaldea.Components -> TamamoToolkit
	- Chaldea.Components.DllImport -> TamamoToolkit.DllImport
	- Chaldea.Components.Extensions -> TamamoToolkit.Extensions
	- Chaldea.Components.Utils -> TamamoToolkit.Utils

## v2.0.0 更新内容

- 首次发布，为确保一致性，故版本号从 v2.0.0 开始