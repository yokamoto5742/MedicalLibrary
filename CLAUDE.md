# CLAUDE.md

このファイルは、本リポジトリのコードを操作する際に Claude Code (claude.ai/code) へガイダンスを提供するためのものです。

## 概要

日本の電子カルテ用**クラスライブラリ**です。患者情報、オーダ、SOAPカルテ、眼科ワークフロー、検査機器連携などを扱います。`MedicalLibrary.dll` としてビルドされ、外部の3つのアプリケーション（`EyeCenter.exe`、`NidekARK1.exe`、`CanonRKF1.exe`）からのみ参照されます。本リポジトリ自体は単体で動作するスタンドアロンアプリケーションでは**ありません**。これらのアプリから参照されていないコードは、2026年7月（フェーズ1〜3、OpeOrder.exe の廃止を含む）および 2026年9月（フェーズ4: ILベースの到達可能性解析に基づき、OpeOrder/SOAP/オーダ/DPC/医事会計のUIおよびエンティティを含む323ファイルをファイル単位で削除。詳細は `docs/cleanup-plan-phase4.md` を参照）に削除されました。なお、残されたファイル内に存在する未使用のメソッドや型については、意図的にそのまま残蔽されています。

* **`Main` メソッドおよびエントリポイントは存在しません。** 外部EXEの起動・連携用ヘルパーは `Utility/Launcher.cs` および `InnoProgram.cs` に配置されています。
* **テストスイート、CI、リンターはありません。** 単体テストを実行する環境は用意されていません。
* ターゲット: **.NET Framework 4.8**（レガシー環境であり、.NET Core/5+ ではありません）。C# + Windows Forms構成です。旧形式（非SDK形式）の MSBuild `.csproj` を使用しています。

## ビルド

プラットフォームは **x86** のみです（本番環境へのデプロイを想定、出力先は `C:\shinseikai\`）:

```
msbuild MedicalLibrary.csproj /p:Configuration=Release /p:Platform=x86

```

* `dotnet build` は**サポートされていません**（非SDKスタイルのcsproj、GAC/HintPath参照のため）。Visual Studio 2022以降の `msbuild` のみを使用してください。
* ネイティブの Oracle ODP.NET クライアントが32ビットであるため、x86構成が必須です。
* 参照アセンブリの `HintPath` は、相対パスで約7階層上の隣接する `Karte`/`Shinseikai` フォルダ、および Oracle 11.2 クライアントフォルダを参照しています。ビルド環境のマシン上にこれらの外部DLLやフォルダが存在することを前提としています。
* かつて存在した `INNO` コンパイルシンボルおよび AnyCPU/IJI 構成は 2026年7月に削除されました。現在は INNO（新星会）向けのコードパスが無条件で適用されます（`StdEntity.Db` は `DB.Db3`、DBリンクは `@INNO.WORLD`）。

## 編集ルール

* **各ファイルの既存エンコーディングを維持してください。** ファイルは Shift-JIS (CP932) と UTF-8 (BOM付き) が混在しており、いずれも日本語コメントや `<summary>` ドキュメントを含んでいます。文字コードを変更しないでください（日本語の文字化けの原因となります）。
* コメントや XML ドキュメントコメントは**日本語**で記述されています。ドキュメントを追加する際もそれに合わせてください。

## コードスタイル（C#のデフォルト規則からの相違点）

* **メソッドの引数およびローカル変数には `snake_case` を使用します**（例: `connection_string`、`param_list`、`con_str`）。
* private static フィールドには `snake_case`（例: `legacy_home`、`db_link`）、環境定数の public プロパティには **`ALL_CAPS`**（例: `LEGACY_HOME`、`AGENT_HOME`、`DB_LINK`）を使用します。
* 型名、メソッド名、public シングルトンは PascalCase です。インデントは半角スペース4つ、波括弧はオールマンスタイル（Allman braces）を採用しています。
* `.editorconfig` やアナライザーのルールセットはありません（x86構成では `CodeAnalysisIgnoreBuiltInRules=true` に設定されています）。

## アーキテクチャと留意事項

* **トップレベルフォルダ／名前空間によるレイヤー構造:**
* `Entity/`（ドメインモデル、`MedicalLibrary.Entity`）
* `Boundary/`（EyeCenter で使用される WinForms UI: `FormFindPat`、`FormString1`、`LoginPrompt`/`LoginChange`、`StdControlPat1`、`StdForm1`）
* `Agent/`（眼科機能モジュール群 `Eye*`、および検査機器アプリ用の `NidekARK1ListForm`/`CanonRKF1Form`）
* `Utility/`（インフラストラクチャ層: `DB.cs`、`Env.cs`、`LibSettings.cs`）


* **DBアクセスはグローバルな静的シングルトン経由:** `Utility/DB.cs` 内の `DB.Db1`/`Db2`/`Db3` を使用し、1つの共有 `OracleCommand`（`BindByName=true`）を用います。エンティティは静的な `StdEntity.Db`（＝`Db3`）経由でDBにアクセスします。設計上スレッドセーフではないため、Open/Close/`Parameters.Clear` の実行順序に注意が必要です。
* **絶対パスのハードコード:** `Utility/Env.cs` 内のパス設定（`C:\macs`、`c:\karte`、`c:\innokarte`、`c:\shinseikai`）や、`Utility/LibSettings.cs` 内の **Oracle 認証情報**（`Setting.xml` との間でシリアライズ/デシリアライズ）がハードコードされています。このディスク配置構成が前提となっています。

## 開発規約

追加のプロジェクトルールは `.claude/rules/` に格納されています（自動的に読み込まれます）:

* `coding-guidelines.md` — 影響を最小限に抑えた局所的な変更を行うこと。実装前に前提事項を明示すること。
* `commit.md` — コミットメッセージには絵文字プレフィックス（`✨ feat`、`🐛 fix`、`📝 docs`、`♻️ refactor`、`✅ test`）を使用し、日本語で記述すること。
* `response-style.md` — 長文の説明ではなく差分/パッチ形式で出力すること。変更は最小限にとどめ、直接編集を適用すること。
