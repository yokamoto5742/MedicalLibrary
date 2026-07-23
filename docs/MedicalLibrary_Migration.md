# MedicalLibrary.dll 別電子カルテ移植調査

別の電子カルテ環境で `MedicalLibrary.dll` を再利用するために変更が必要な箇所の調査結果。
環境依存が焼き込まれている順に記載する。

調査日: 2026-07-06（2026-07-19 更新: `INNO` コンパイルシンボル削除・3アプリ専用化フェーズ1〜3完了後のコードベースに合わせて改訂）

> **前回調査からの主な変化**
> - `#if INNO` シンボルと AnyCPU/IJI ビルド構成は削除済み。INNO（Shinseikai）向けコードパスが**無条件**になった。移植時はビルド構成の切り替えではなく、以下に挙げる箇所の**コード直接書き換え**が必要。
> - 未使用コード削減（460→316ファイル）により、DBリンク依存ファイルは 11 → 7 に減少。`MacsProgram.cs`・`FormMedicalSupport.cs` 等は削除済み。

---

## 1. 必須変更（コードに環境が固定されている箇所）

### 1-1. `Utility/Env.cs:116-125` — パスとDBリンクのハードコード

```csharp
private static void init()
{
    legacy_home = @"C:\macs";
    agent_home = @"C:\macs\utility";
    karte_home = @"c:\karte";
    inno_home = @"c:\innokarte";
    shin_home = @"c:\shinseikai";

    db_link = "@INNO.WORLD";   // 旧 #if INNO 分岐は削除され固定値
}
```

- 新環境のインストールパスと、新カルテDBへの Oracle DBリンク名に変更が必要。
- ここが全体の起点：`AppFile.FilePath` の設定ファイル探索も、SQL の DBリンクもここに依存する。
- `DB_LINK`・`KARTE_HOME` 等には setter があるため、呼び出し側アプリの起動時に上書きする方法も取れる。

### 1-2. 旧 `#if INNO` 分岐だった箇所（現在は INNO 前提で固定）

コンパイルシンボルは廃止済み。以下は現在のコードで INNO 前提が直書きされているため、移植時に直接修正する。

| ファイル | 現在の状態 | 移植時の対応 |
|---|---|---|
| `Entity/StdEntity.cs:10` | 全エンティティの接続先が `DB.Db3` に固定 | 移植先の接続先（Db1〜Db3 のどれを使うか）に合わせて変更 |
| `Utility/LibSettings.cs:500-528` | `Init()` が **InnoUketsukeLib を無条件に初期化**し、`Db2`/`Db3` のみ `Init`（`Db1`/`DBConnectionString1` は初期化されない） | InnoUketsukeLib 初期化の削除または差し替え。使用するDBの初期化を追加・変更 |
| `Utility/AppFile.cs:16-48` | 設定ファイル探索順: カレント → `SHIN_HOME` → `KARTE_HOME` → `AGENT_HOME` → `INNO_HOME` → `LEGACY_HOME` | 新環境のフォルダ構成に合わせて探索順・対象を変更 |

補足: `InnoUketsukeLib` の呼び出しは `[MethodImpl(NoInlining)]` の別メソッドに分離され try/catch で包まれているため、**DLL が存在しない環境でも例外は握りつぶされて動作は継続する**（`LibSettings.cs:486-514`、`Staff.cs:372-400`）。ただし認証がDB照会のみに退化するため、そのままにせず §3 の通り差し替えること。

### 1-3. `Utility/LibSettings.cs:26-138` — 接続文字列と施設固有パスのデフォルト値

- Oracle認証情報（`User Id=macs;Password=system;...`、`User Id=medb;Password=system;Data Source=inno_orcl;` 等）
- UNCパス（`\\lily\...`、`\\pdfkartenas2\...`、`\\172.17.10.20\...`）
- 外部アプリパス（`c:\shinseikai\ProasAgent.exe`（オーダー転送）、`c:\shinseikai\ProasKaikeiApi.exe`、医事会計exe）
- ローカルフォルダ（`c:\shinseikai\Schema`、`c:\shinseikai\MedicalLibrary\soapimg`）

これらは `MedicalLibrary_Settings.xml` で上書き可能なため、**コード変更なしでも設定ファイル配布で対応可**。
ただしデフォルト値が旧環境を指したままなのは危険なので、新環境の値への書き換えを推奨。

---

## 2. 連携先カルテのDBスキーマ依存（最大の作業量）

`Env.DB_LINK` 経由でカルテ側テーブルを直接 JOIN している SQL が **4ファイル**に存在する
（2026-07 の未使用コード削除で `DrugAdv.cs`・`OushinPat.cs`・`MWMOrder.cs`・`OpeNursingData.cs` は削除済み。
2026-07-23 に `EyeKensa.cs`・`EyeOpe.cs`・`EyeSummary.cs` の `M_PATIENT` 結合は `PatBase.GetDict` 経由の2段階取得に切替済みで解消）。

| ファイル | 参照しているカルテ側テーブル |
|---|---|
| `Agent/BillPay.cs`（496, 593行） | `M_PATIENT` |
| `Agent/ComeReportData.cs`（331〜903行、7箇所） | `NTオーダーヘッダー`、`D_ORDER_HEADER` |
| `Agent/ComeReportOrder.cs`（283〜658行、8箇所） | `D_ORDER_HEADER`、`D_ORDER_DETAIL`、`M_PATIENT` |
| `Entity/DiagDPC.cs`（139行） | `D_NYUIN`（`Db == DB.Db2` のときのみリンク付与） |

例（`Agent/BillPay.cs:496`）:

```csharp
" from BILL_PAY tp, M_PATIENT" + Env.DB_LINK + " tm " +
```

`M_PATIENT`、`D_ORDER_HEADER`、`NTオーダーヘッダー` 等は**現行カルテ／医事システムのテーブル名**。
DBリンク名の変更だけでは済まず、**移植先カルテのスキーマが異なる場合はこれらの SQL 全体の書き換え**が必要になる。

---

## 3. 外部アプリ・プロセス名依存

| 箇所 | 内容 |
|---|---|
| `Utility/InnoProgram.cs` | InnoKarte 固有の連携。`c:\InnoKarte\InnoKarte.exe` のパスハードコード（22行）に加え、**UI Automation でウィンドウタイトル「SOAP入力」「外来患者一覧」「入院一覧」や AutomationId（`BtnKarte` 等）を検索**しており、移植先カルテの画面構成に全面依存。同等クラスへの差し替えが必要（旧 `MacsProgram.cs` は削除済み） |
| `Entity/PatBase.cs:749` | `DeletePatCSV` 内の `processName = "InnoKarte"` ハードコードと、`Env.INNO_HOME + @"\Pat.csv"` による患者情報CSV連携。移植先カルテのプロセス名・連携方式に変更 |
| `Utility/Launcher.cs:308` | `c:\shinseikai\PdfView\PdfView.exe` のフルパスハードコード |
| `Entity/Staff.cs:372-400` | **InnoUketsukeLib によるスタッフ認証**（`Verify`）。DLL 欠如時は例外スルーで `M_USR` テーブル照会のみに退化する。Shinseikai 固有 DLL のため移植先の認証方式に差し替え |
| `Utility/LibSettings.cs:486-514` | `Init()` 冒頭の InnoUketsukeLib 初期化。認証差し替えと同時に削除 |

---

## 4. ビルド設定（`MedicalLibrary.csproj`）

ビルド構成は **x86（Debug / Release）のみ**（AnyCPU/IJI 構成は削除済み）。参照 `HintPath` はリポジトリから **7階層上** の兄弟フォルダを指す。

- 参照 `HintPath` の修正
  - `InnoUketsukeLib.dll`（`..\..\..\..\..\..\..\Shinseikai\`）— Shinseikai 固有のため、参照ごと外すか差し替える（§3 の認証差し替えとセット）
  - `itextsharp.dll` / `Interop.Excel.dll`（`..\..\..\..\..\..\..\Karte\`）
  - `Oracle.DataAccess.dll`（`..\..\..\..\..\..\..\app\Administrator\product\11.2.0\client\odp.net\bin\2.x\`、ODP.NET 2.112 / x86）— 移植先の Oracle クライアント構成に合わせる（新しいクライアントを使う場合は消費アプリ側の bindingRedirect でも対応可）
- 出力先ディレクトリ
  - Debug: `..\..\..\..\..\..\..\shinseikai\`（`C:\shinseikai\` 直出力を想定）
  - Release: `bin\x86\Release\`

新環境のフォルダ構成に合わせて修正する。`dotnet build` 非対応（非SDK形式 csproj）のため `msbuild` を使用すること。

---

## 移植前の確認事項

移植先の電子カルテについて以下を確認すること。

1. **Oracle を使用しているか**（DB アクセスは ODP.NET / Oracle 前提。x86 ネイティブクライアント必須）
2. **DBリンクで参照するテーブル構成（`M_PATIENT`、`D_ORDER_HEADER` 等）は現行と同一か**
3. **カルテ画面との連携方式**（`InnoProgram.cs` の UI Automation、`Pat.csv` 連携に相当する手段があるか）

同一であれば変更は主に §1・§3・§4 で済むが、異なる場合は §2 の SQL 書き換えが作業の本体になる。
