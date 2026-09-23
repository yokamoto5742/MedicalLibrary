# MedicalLibrary

## 概要

眼科向け電子カルテシステムの .NET クラスライブラリです。患者基本情報、ログイン、眼科検査・手術予約/記録・サマリー、検査機器（Nidek / Canon）連携など、眼科特有のワークフローを提供します。

このプロジェクトはスタンドアロンアプリケーションではなく、以下の3つの外部アプリケーションで使用されるコンポーネントライブラリです：

- **EyeCenter.exe** — 主要な電子カルテクライアント
- **NidekARK1.exe** — 眼科検査機（Nidek）連携
- **CanonRKF1.exe** — 眼科検査機（Canon）連携

ビルド成果物は `MedicalLibrary.dll` で、これらのアプリケーション内部で参照・使用されます。

## 前提条件

### 開発環境

- **OS**: Windows 11 / Windows Server 2022 以降（開発マシンは32bit対応が必要）
- **.NET Framework**: 4.8（.NET Core/5+ は非対応）
- **ビルドツール**: Visual Studio 2022 以降（`msbuild` コマンドライン対応）
- **データベース**: Oracle Database 11g R2（クライアント32bit ODP.NET 11.2）

### 依存関係

以下の外部DLLが必要です（HintPath参照）：

- Oracle ODP.NET 11.2 32bit クライアント（`C:\app\Administrator\product\11.2.0\client\odp.net\bin\2.x\Oracle.DataAccess.dll`）
- InnoUketsukeLib.dll（`..\..\..\..\Shinseikai\InnoUketsukeLib.dll`）
- Interop.Excel.dll（`..\..\..\..\Karte\Interop.Excel.dll`）
- System.Windows.Forms, System.Drawing, System.Data 他標準ライブラリ

また、実行時には以下のディレクトリ構造が必要です：

```
C:\macs\                  — マスタデータ
C:\karte\                 — 基本システムデータ
C:\innokarte\             — イノシステムカルテデータ
C:\shinseikai\            — Shinseikai環境データ
```

## ビルド

### リリースビルド（推奨）

```bash
msbuild MedicalLibrary.csproj /p:Configuration=Release /p:Platform=x86
```

出力先: `bin\x86\Release\MedicalLibrary.dll`

### デバッグビルド

```bash
msbuild MedicalLibrary.csproj /p:Configuration=Debug /p:Platform=x86
```

出力先: `C:\shinseikai\MedicalLibrary.dll`（絶対パス）

### ビルドに関する注意

- **x86 プラットフォームのみ対応**。Oracle ODP.NET ネイティブクライアントが32bitのため、x64/AnyCPUでのビルドは不可能です
- `dotnet build` は非対応です。非SDK形式の `.csproj` ファイルおよびHintPath参照を使用しているため、Visual Studio の `msbuild` コマンドを使用してください
- ビルドには上記の外部DLLおよびOracle クライアントフォルダがマシン上に存在している必要があります

## 使用方法

このライブラリはクラスライブラリであり、単独では実行されません。外部アプリケーション（EyeCenter.exe など）が `MedicalLibrary.dll` を参照して使用します。

### 基本的な使用パターン

```csharp
// 患者情報の取得（PatBase: Entity/PatBase.cs）
var pat = MedicalLibrary.Entity.PatBase.Load(pt_id);

// 手術記録の取得（EyeOpeRecord: Agent/EyeOpeRecord.cs）
var ope = MedicalLibrary.Agent.EyeOpeRecord.Load(ope_id);
```

### DB アクセス

データベースアクセスは以下のグローバル静的シングルトンを経由します（`Utility/DB.cs`）：

- `MedicalLibrary.Utility.DB.Db1` / `Db2` / `Db3` — 接続ごとの静的シングルトン
- `MedicalLibrary.Entity.StdEntity.Db` — `DB.Db3` を指す既定の接続

すべてのエンティティは同じ `OracleCommand` インスタンスを共有するため、**スレッドセーフではありません**。`Open()`/`Close()`/`Command.Parameters.Clear()` の順序が重要です。

```csharp
// SQL を直接実行する例（DB.ExecuteNonQuery は内部で Open/Close を行う）
var db = MedicalLibrary.Utility.DB.Db3;
db.ExecuteNonQuery("update ... where ...", param_list);
```

## プロジェクト構造

```
MedicalLibrary/
├── Entity/              — ドメインモデル（MedicalLibrary.Entity）
│   ├── PatBase.cs      — 患者基本情報
│   ├── LoginUser.cs    — ログインユーザー
│   ├── Staff.cs        — 職員
│   └── ...
├── Boundary/            — WinForms UI コンポーネント（EyeCenter が使用）
│   ├── FormFindPat.cs  — 患者検索ダイアログ
│   ├── LoginPrompt.cs  — ログイン画面（LoginChange.cs: ユーザー切替）
│   ├── StdControlPat1.cs — 患者情報表示コントロール
│   └── ...
├── Agent/               — 業務ロジック・機能モジュール
│   ├── EyeDict.cs      — 眼科辞書・マスタ
│   ├── EyeDoc.cs       — 帳票（オペ録・申し送り書）
│   ├── EyeOpe.cs       — 手術管理
│   ├── EyeOpeRecord.cs — 手術記録
│   ├── EyeKensa.cs     — 検査処理
│   ├── NidekARK1ListForm.cs — NidekARK1.exe 用画面
│   ├── CanonRKF1Form.cs — CanonRKF1.exe 用画面
│   └── ...
├── Utility/             — インフラ・ユーティリティ
│   ├── DB.cs           — Oracle DB アクセス（DB.Db1/Db2/Db3）
│   ├── Env.cs          — 環境変数・パス定義
│   ├── LibSettings.cs  — 設定管理（Setting.xml）
│   ├── AppString.cs    — 文字列ユーティリティ
│   ├── AppFile.cs      — ファイル操作ユーティリティ
│   ├── Launcher.cs     — 外部プロセス起動
│   └── ...
├── Properties/          — アセンブリ情報・リソース
├── docs/
│   ├── CHANGELOG.md    — 変更履歴
│   └── cleanup-plan-phase4.md — フェーズ4削減計画・実施結果
├── MedicalLibrary.csproj
├── CLAUDE.md           — 開発ガイドライン（プロジェクト固有）
└── README.md           — このファイル
```

## 主要モジュール

### Entity（ドメインモデル）

**PatBase** — 患者基本情報（Id, Name, Kana, Birth など）
```csharp
var pat = PatBase.Load(pt_id);
Console.WriteLine($"患者名: {pat.Name}");
```

**LoginUser** / **Staff** — ログインユーザー・職員情報

### Agent（業務ロジック）

**EyeDict** — 眼科データ辞書・マスタデータの取得・管理

**EyeDoc** — 帳票。オペ録・申し送り書の印刷・Excel 出力

**EyeOpeRecord** — 手術記録。手術内容・所見などを管理

**EyeKensa** — 検査情報。視力検査、眼圧検査など各種検査データ

**EyeOpe** — 手術スケジュール・待機一覧・院内完結フロー

**NidekARK1ListForm** / **CanonRKF1Form** — 検査機器連携アプリ（NidekARK1.exe / CanonRKF1.exe）の画面

### Utility（インフラ）

**DB.cs** — Oracle データベースアクセス。グローバル DB.Db1/Db2/Db3 シングルトンで接続管理

**LibSettings.cs** — `Setting.xml` 経由の設定保存・読み込み（Oracle認証情報など）

**Env.cs** — 環境変数・マシン固有のパス（`C:\shinseikai` 等）定義

**AppString** / **AppFile** — 汎用ユーティリティ

## 開発情報

### コード規約

プロジェクト固有の規約については `CLAUDE.md` および `.claude/rules/` を参照：

- **パラメータ・ローカル変数**: `snake_case`（例：`connection_string`, `param_list`）
- **プライベート静的フィールド**: `snake_case`（例：`legacy_home`）
- **パブリック定数/プロパティ**: `ALL_CAPS`（例：`AGENT_HOME`, `LEGACY_HOME`）
- **型・メソッド・パブリックシングルトン**: `PascalCase`
- **インデント**: 4スペース、Allman ブレース
- **コメント・XMLドキュメント**: 日本語で記述

### ファイルエンコーディング

ファイルは **Shift-JIS** と **UTF-8-with-BOM** が混在しています。既存ファイルのエンコーディングを変更しないでください。日本語テキストが破損します。

### テスト・CI・リンター

**テストスイートはありません**。ユニットテスト、統合テストの実行フレームワークは存在しません。

**CIパイプラインはありません**。GitHub Actions など自動ビルド・テスト環境は構成されていません。

**コードリンター・アナライザーはありません**。`.editorconfig` や StyleCop などの静的解析ツールは導入されていません（`.csproj` で`CodeAnalysisIgnoreBuiltInRules=true`）。

### コード削減履歴

1. **フェーズ1〜3（2026年7月）**: 3アプリ（EyeCenter, NidekARK1, CanonRKF1）専用化。460ファイル → 316ファイルに削減
2. **OpeOrder.exe 廃止（2026年7月）**: OpeOrder.exe は実行不可にし、専用コード27ファイルを削除
3. **INNO コンパイルシンボル削除（2026年7月）**: 旧システムの条件分岐を整理。INNO（Shinseikai）コードパスは現在無条件で有効
4. **フェーズ4（2026年9月）**: ビルド済みバイナリの IL 到達可能性解析により、3アプリから到達しないコードをファイル単位で削除。.cs 316 → 65、.resx 81 → 9（計323ファイル削除）。手術オーダー UI（`FormOpeOrder`, `OpeOrderData`, `OpeOrderMaster` 等）も EyeCenter から到達しないため削除

詳細は `docs/cleanup-plan-phase4.md` を参照してください。

## トラブルシューティング

### ビルド失敗："Oracle.DataAccess が見つからない"

**原因**: Oracle ODP.NET 11.2 クライアントが `C:\app\Administrator\product\11.2.0\client\` に存在しない

**対応**:
1. Oracle Database 11.2 Client（32bit） をインストール、または
2. `.csproj` の HintPath を環境に合わせて修正

### ビルド失敗："InnoUketsukeLib.dll が見つからない"

**原因**: `..\..\..\..\Shinseikai\` の相対パスが正しくない、またはInnoUketsukeLib.dllが存在しない

**対応**:
1. 兄弟フォルダ `Shinseikai` が `MedicalLibrary` と同じレベルに存在するか確認
2. 存在しない場合、`.csproj` の HintPath を修正するか、InnoUketsukeLib.dll を配置

### x64/AnyCPU ビルルが失敗する

**原因**: このプロジェクトは x86 のみに対応しています

**対応**: x86 プラットフォームでビルドしてください
```bash
msbuild MedicalLibrary.csproj /p:Configuration=Release /p:Platform=x86
```

### 実行時："C:\shinseikai にアクセス不可"

**原因**: 環境パス `C:\shinseikai` が存在しない、またはアクセス権限がない

**対応**:
1. ディレクトリを作成、または
2. `Utility/Env.cs` の定数を環境に合わせて修正

## ライセンス

社内利用を前提とした非公開プロジェクトです。ライセンスファイルは存在しません。
