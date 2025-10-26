# HOW TO CONTRIBUTE

当プロジェクトに関心を寄せていただき、ありがとうございます。このプロジェクトはオープン ソースであるため、誰でもプロジェクトに貢献することができます。
皆様がプロジェクトへの貢献を円滑に行えるよう、以下の事項を守っていただけますようお願いします。

**English:**
Thank you for your interest in this project. This project is open source, so anyone can contribute to the project.
To ensure that your contributions to the project go smoothly, please follow the guidelines below.

## Getting Started

- このプロジェクトに貢献する為には[GitHub アカウント](https://github.com/signup/free)が必要です。
- ソースコードの変更を伴う場合は、[For Developer](#for-developer)の手順に従って開発環境をセットアップしてください。

**English:**
- A [GitHub account](https://github.com/signup/free) is required to contribute to this project.
- If you need to make changes to the source code, please set up a development environment according to the steps in [For Developer](#for-developer).

## For Developer

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

#### Recommended

下記のいずれかの環境を利用することを推奨します。[Rider](https://www.jetbrains.com/rider/)やその他のエディタを利用する場合は、各エディタの設定に従ってください。

- Visual Studio 2022 以降
  - 当プロジェクトはオープンソースのため、[Visual Studio 2022 Community](https://visualstudio.microsoft.com/downloads/)が利用可能です。
  - .NET 10.0 (10.0.100以降) SDK がインストール内容に含まれているか確認してください。
  - ソース ジェネレーター部分をデバッグするためには、.NET Compiler Platform SDK を追加でインストールしてください。
- [Visual Studio Code](https://code.visualstudio.com/) + [EditorConfig 拡張機能](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig) + [C# Dev Kit](https://learn.microsoft.com/visualstudio/subscriptions/vs-c-sharp-dev-kit)
  - .NET 10.0 SDK は個別にインストールしてください。
  - Gitのインストールが必要です。(Visual Studio Codeの初回実行時、インストールを促されます)
  - C# Dev Kitがライセンス上利用できない場合は、[C# 拡張機能](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)を代わりに使用できます。
- Dev Container
  - Visual Studio Codeの[Dev Containers 拡張機能](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)を利用して、Docker コンテナー内で開発を行うことができます。
    - Dockerを利用できる環境が必要です。
  - もしくは、[GitHub Codespaces](https://github.com/features/codespaces)を利用することもできます。

**English:**
We recommend using one of the following environments. If you use [Rider](https://www.jetbrains.com/rider/) or other editors, please follow the settings for each editor.

- Visual Studio 2022 or later
  - Since this project is open source, [Visual Studio 2022 Community](https://visualstudio.microsoft.com/downloads/) is available.
  - Make sure .NET 10.0 (10.0.100 or later) SDK is included in the installation.
  - To debug source generator parts, additionally install the .NET Compiler Platform SDK.
- [Visual Studio Code](https://code.visualstudio.com/) + [EditorConfig extension](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig) + [C# Dev Kit](https://learn.microsoft.com/visualstudio/subscriptions/vs-c-sharp-dev-kit)
  - Install .NET 10.0 SDK separately.
  - Git installation is required. (You will be prompted to install it when you run Visual Studio Code for the first time)
  - If C# Dev Kit is not available due to licensing, you can use the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) instead.
- Dev Container
  - You can develop in a Docker container using the [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) for Visual Studio Code.
    - An environment where Docker can be used is required.
  - Alternatively, you can use [GitHub Codespaces](https://github.com/features/codespaces).

### Development Commands

[.NET CLI](https://docs.microsoft.com/dotnet/core/tools/)(SDKに付属)を利用して、下記のコマンドを実行できます。

**English:**
You can run the following commands using the [.NET CLI](https://docs.microsoft.com/dotnet/core/tools/) (included with the SDK).

```powershell
# 必要なパッケージの復元(test, buildコマンド内で行われるため、これらの呼び出し時には不要)
# Restore required packages (not necessary when calling these commands as it is done within test and build commands)
> dotnet restore

# コードフォーマットのチェックと修正
# Check and fix code formatting
> dotnet format

# 単体テストの実行
# Run unit tests
> dotnet test

# ビルド
# Build
> dotnet build
```

## Send issue

- 重複する Issue がないかどうか、はじめに検索してください。
- 機能要望(新機能の追加、既存機能の変更など)には、**必ず** Issue を作成してください。
  - 小さなバグ修正やリファクタリングなどは、Issue を作成せずに直接 Pull Request を送っても構いません。ただし、規模が大きい場合は、事前に Issue を作成し、了解を取ってから作業を始めてください。
- Issue を送るのに、事前の連絡は必要ありません。
- Issue のタイトルと本文はできるだけ英語で書いてください。
  - 英語に慣れていない場合は日本語を使ってください。不正確な英語では、英語話者・日本語話者のどちらにも伝わりません。
- バグを Issue で報告する場合、バグを再現する為の説明、エラーの情報、環境を書いてください。
- 本文は明確に記述し、1 行のみの Issue を送ることは避けてください。

**English:**
- First, search to see if there are any duplicate issues.
- **Always** create an issue for feature requests (adding new features, changing existing features, etc.).
  - For small bug fixes and refactoring, you can send a Pull Request directly without creating an issue. However, if the scope is large, please create an issue in advance and get approval before starting work.
- No prior communication is required to send an issue.
- Please write the issue title and body in English if possible.
  - If you are not comfortable with English, please use Japanese. Inaccurate English will not be understood by either English speakers or Japanese speakers.
- When reporting a bug in an issue, please write a description to reproduce the bug, error information, and environment.
- Write the body clearly and avoid sending issues with only one line.

## Making Changes

- コードやドキュメントを変更する場合は、`main`ブランチから、トピック・ブランチを作ってください。(`issue_999`, `hotfix/some_bug`など)
- 変更の為にテストが必要ならば、そのテストも追加または変更してください。
- commit は合理的(ロジック単位)に分けてください。また目的と関係のないコードの変更は含めないでください(コードフォーマットの変更、不要コードの削除など)。
- commit メッセージが正しいフォーマットであることを確認してください。commit メッセージはできるだけ英語でお願いします。
- commit メッセージには、下記の修飾子を先頭につけてください。([angular.js/DEVELOPERS.md](https://github.com/angular/angular.js/blob/master/DEVELOPERS.md#type)に準じます)
  - **feat**: 新機能
  - **fix**: バグ修正
  - **docs**: ドキュメントの修正のみ
  - **style**: コードの意味を変更しない修正 (スペース・フォーマット・セミコロンのフォーマットなど)
  - **refactor**: バグや機能追加ではないコード修正
  - **perf**: コードの高速化に寄与する修正
  - **test**: テストの追加、または修正
  - **build**: ビルド構成の変更
  - **ci**: CI/CD構成の変更
  - **chore**: その他の変更 (ライブラリの更新など)

**Git コミットメッセージの例:**
```text
修飾子(サブカテゴリ): コミットの概要
<ここは空行>
3行目以降に、このコミットの詳細を記述します。
```

**English:**
- When changing code or documentation, please create a topic branch from the `main` branch (`issue_999`, `hotfix/some_bug`, etc.).
- If tests are needed for the changes, please add or modify those tests as well.
- Please divide commits rationally (logical units). Also, do not include code changes unrelated to the purpose (code formatting changes, deletion of unnecessary code, etc.).
- Make sure the commit message is in the correct format. Please write commit messages in English if possible.
- Please prefix commit messages with the following modifiers (based on [angular.js/DEVELOPERS.md](https://github.com/angular/angular.js/blob/master/DEVELOPERS.md#type)):
  - **feat**: New features
  - **fix**: Bug fixes
  - **docs**: Documentation changes only
  - **style**: Changes that do not affect the meaning of the code (whitespace, formatting, semicolon formatting, etc.)
  - **refactor**: Code changes that are neither bug fixes nor feature additions
  - **perf**: Changes that contribute to code performance improvements
  - **test**: Adding or modifying tests
  - **build**: Changes to build configuration
  - **ci**: Changes to CI/CD configuration
  - **chore**: Other changes (library updates, etc.)

**Git commit message example:**
```text
modifier(subcategory): commit summary
<empty line here>
Describe the details of this commit from the third line onwards.
```

### Coding Style

- Lint ルールか、すでにあるコードのスタイルに準じます。
  - [EditorConfig](https://editorconfig.org/) を適用しているため、それに対応したエディタを使うことを推奨します。
  - コードのフォーマットは `dotnet format` コマンドで自動修正できます。
- ファイルのエンコーディングは UTF-8 とします。
- 非 ASCII 文字(日本語など)を変数名,メンバー名に使用しないでください。
  - XMLドキュメンテーションコメントを含む、コードのコメント部分に非 ASCII 文字を使うのは構いません。
- リソースファイルのデフォルト言語は英語とし、日本語のリソースファイルは `Resources.ja-jp.resx` として作成してください。

**English:**
- Follow lint rules or existing code style.
  - We recommend using an editor that supports [EditorConfig](https://editorconfig.org/) as it is applied.
  - Code formatting can be automatically corrected with the `dotnet format` command.
- File encoding should be UTF-8.
- Do not use non-ASCII characters (Japanese, etc.) for variable names and member names.
  - It is okay to use non-ASCII characters in code comment sections, including XML documentation comments.
- The default language for resource files should be English, and Japanese resource files should be created as `Resources.ja-jp.resx`.

### CI/CD

- このプロジェクトは、[GitHub Actions](https://github.com/features/actions)を利用して CI/CD を行っています。
- **すべての** Pull Request と`main`ブランチへの push に対して、自動的に下記チェック処理が実行されます。
  - Pull Requestのマージには、チェック処理に合格している必要があります。

**English:**
- This project uses [GitHub Actions](https://github.com/features/actions) for CI/CD.
- The following check processes are automatically executed for **all** Pull Requests and pushes to the `main` branch.
  - Pull Request merging requires passing the check processes.

| Job Name | Description (日本語) | Description (English) |
| --- | --- | --- |
|[Lint](./.github/workflows/dotnet.yml#L16)|コードフォーマットのチェック|Code formatting check|
|[Validate NuGet Lock Files](./.github/workflows/dotnet.yml#L28)|NuGet パッケージのロックファイル(`packages.lock.json`)の検証|Validation of NuGet package lock files (`packages.lock.json`)|
|[Debug Build & Test](./.github/workflows/dotnet.yml#L43)|デバッグ ビルドと単体テストの実行(Windows, MacOS, Linuxと.NET 8.0/9.0の各環境で実施)|Debug build and unit test execution (performed on Windows, MacOS, Linux and .NET 8.0/9.0 environments)|

## Thanks

このガイドは、[MMP/CONTRIBUTING.md · sn0w75/MMP](https://github.com/sn0w75/MMP/blob/master/CONTRIBUTING.md)(現在リンク切れ)を参考にして作成しました。

**English:**
This guide was created with reference to [MMP/CONTRIBUTING.md · sn0w75/MMP](https://github.com/sn0w75/MMP/blob/master/CONTRIBUTING.md) (currently broken link).
