<div align="center">


# 🛡️ SGuardLimiter

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120?logo=c-sharp)](https://learn.microsoft.com/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%2F11-lightgrey?logo=windows)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/License-Learning%20Only-yellow)](#免责声明)
[![Build](https://img.shields.io/badge/Build-Release-success?logo=github)](https://github.com/)

</div>

---

> 🎯 **ACE 反作弊系统限制工具** —— 自动检测 ACE 进程并限制其 CPU 使用与优先级，保护你的电脑性能。  
> 原理参考华硕官方视频示例。

📺 [【补档2】关闭 ACE 反作弊系统 保护你的电脑性能](https://www.bilibili.com/video/BV1ca1WBUE8j/?spm_id_from=333.337.search-card.all.click)

---

## 📚 目录

- [🛠️ 功能说明](#🛠️-功能说明)
- [🚀 使用方法](#🚀-使用方法)
  - [方式一：直接运行](#✅-方式一直接运行)
  - [方式二：自行编译](#🧱-方式二自行编译)
- [🧠 技术细节](#🧠-技术细节)
- [🧰 开发工具](#🧰-开发工具)
- [⚠️ 注意事项](#⚠️-注意事项)
- [📜 免责声明](#📜-免责声明)
- [👤 作者信息](#👤-作者信息)

---

## 🛠️ 功能说明

- 自动检测并修改以下 ACE 相关进程：
  - `SGuard64.exe`
  - `SGuardSvc64.exe`
- 若 30 秒内未检测到目标进程，程序会自动退出。
- 检测到后自动执行：
  - 设置优先级为 **低（Idle）**
  - 绑定到系统最后一个CPU，如 **CPU 31**

---

## 🚀 使用方法

### ✅ 方式一：直接运行

1. 下载发布包，解压 `ACE限制.7z`。
2. 右键以 **管理员身份运行** `ACE限制.exe`。
3. 程序启动后会自动检测并限制目标进程。
4. 设置完成后程序会自动退出。

---

### 🧱 方式二：自行编译

#### 🧩 环境依赖

- [.NET SDK 8.0](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)
- Windows 10 / 11

#### 🧮 编译步骤

##### 初始化新控制台项目（.NET 8）
```
dotnet new console -n SGuardLimiter --framework net8.0
```

##### 将本项目的 Program.cs 覆盖新生成的 Program.cs

##### 然后执行编译和打包
```
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true
```

📦 发布路径：
```
SGuardLimiter\bin\Release\net8.0\win-x64\publish\SGuardLimiter.exe
```

👑 右键以 管理员身份运行 该可执行文件。

🧠 说明

--self-contained true：让程序无需安装 .NET 运行时即可运行

-p:PublishSingleFile=true：打包为单独 .exe 文件

-p:IncludeAllContentForSelfExtract=true：保证资源完整加载


🧰 开发工具

Visual Studio 2022 / VS Code	代码编写与调试

.NET 8 SDK	编译与发布

PowerShell / CMD	执行构建命令

Windows 任务计划程序	自启任务注册

Windows 11 环境	程序测试与验证

⚠️ 注意事项

⚙️ 程序必须以 管理员权限 运行，否则无法修改进程属性。

⏱️ 程序启动后若 30 秒内未检测到目标进程，会自动退出。

🎮 建议在启动游戏后再运行本程序，以确保检测到目标进程。

💾 程序不会常驻系统，仅在成功设置后自动退出。

📜 免责声明

本项目仅供个人学习与研究使用，禁止用于任何商业或非法行为。

开发者保留对本项目的最终解释权。

使用者在使用本项目时，必须遵守中华人民共和国（含台湾地区）及使用者所在地区的法律法规。

任何违反法律法规的行为与开发者无关。

使用本项目所产生的风险与后果由使用者自行承担，
开发者不对任何直接或间接损失负责。

若发现有第三方利用本项目从事收费或商业行为，
其产生的责任与本项目及作者无关。

👤 作者信息

作者：Alic3r3L1cwhk

GitHub：https://github.com/Alic3r3L1cwhk


<div align="center">
💡 如果本项目对你有帮助，请考虑点一个 ⭐Star 支持我！

</div>
