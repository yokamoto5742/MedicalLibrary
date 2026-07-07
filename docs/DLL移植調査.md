# MedicalLibrary.dll 別電子カルテ移植調査

別の電子カルテ環境で `MedicalLibrary.dll` を再利用するために変更が必要な箇所の調査結果。
環境依存が焼き込まれている順に記載する。

調査日: 2026-07-06

---

## 1. 必須変更（コンパイル時に環境が固定される箇所）

### 1-1. `Utility/Env.cs:116-129` — パスとDBリンクのハードコード

```csharp
legacy_home = @"C:\macs";
agent_home = @"C:\macs\utility";
karte_home = @"c:\karte";
inno_home = @"c:\innokarte";
shin_home = @"c:\shinseikai";

#if INNO
    db_link = "@INNO.WORLD";
#else
    db_link = "@IJI.WORLD";
#endif
```

- 新環境のインストールパスと、新カルテDBへの Oracle DBリンク名に変更が必要。
- ここが全体の起点：`AppFile.FilePath` の設定ファイル探索も、SQL の DBリンクもここに依存する。

### 1-2. `#if INNO` コンパイルシンボルで分岐する4ファイル

別カルテ用のビルド構成（新シンボル追加 or 実行時設定化）が必要。

| ファイル | 内容 |
|---|---|
| `Utility/DB.cs:16-18` | `Db3` の有無 |
| `Entity/StdEntity.cs:10-14` | 全エンティティの接続先が `Db3` / `Db1` のどちらか |
| `Utility/LibSettings.cs:492-527` | `DBConnectionString3`、初期化するDB、**InnoUketsukeLib の初期化呼び出し** |
| `Utility/AppFile.cs:19-69` | 設定ファイルの探索パス順 |

### 1-3. `Utility/LibSettings.cs:25-112` — 接続文字列と施設固有パスのデフォルト値

- Oracle認証情報（`User Id=macs;Password=system;...` 等）
- UNCパス（`\\lily\...`、`\\pdfkartenas2\...`、`\\172.17.10.20\...`）
- 外部アプリパス（`c:\shinseikai\ProasAgent.exe`（オーダー転送）、医事会計exe）

これらは `MedicalLibrary_Settings.xml` で上書き可能なため、**コード変更なしでも設定ファイル配布で対応可**。
ただしデフォルト値が旧環境を指したままなのは危険なので、新環境の値への書き換えを推奨。

---

## 2. 連携先カルテのDBスキーマ依存（最大の作業量）

`Env.DB_LINK` 経由でカルテ側テーブルを直接 JOIN している SQL が **11ファイル**に存在する。

- `Agent/ComeReportData.cs`
- `Agent/ComeReportOrder.cs`
- `Agent/BillPay.cs`
- `Agent/DrugAdv.cs`
- `Agent/EyeOpe.cs`
- `Agent/EyeSummary.cs`
- `Agent/EyeKensa.cs`
- `Agent/OushinPat.cs`
- `Agent/MWMOrder.cs`
- `Agent/OpeNursingData.cs`
- `Entity/DiagDPC.cs`

例（`Agent/BillPay.cs:508`）:

```csharp
" from BILL_PAY tp, M_PATIENT" + Env.DB_LINK + " tm "
```

`M_PATIENT`、`D_ORDER_HEADER`、`NTオーダーヘッダー`、`IM01RC` 等は**現行カルテ／医事システムのテーブル名**。
DBリンク名の変更だけでは済まず、**移植先カルテのスキーマが異なる場合はこれらの SQL 全体の書き換え**が必要になる。

---

## 3. 外部アプリ・プロセス名依存

| 箇所 | 内容 |
|---|---|
| `Utility/MacsProgram.cs` / `Utility/InnoProgram.cs` | MACS / InnoKarte 固有のプロセス連携。移植先カルテ用の同等クラスに差し替え |
| `Entity/PatBase.cs:799`、`Agent/FormMedicalSupport.cs:95` | `Process.GetProcessesByName("InnoKarte")` のプロセス名ハードコード |
| `Utility/Launcher.cs:369` | `c:\shinseikai\PdfView\PdfView.exe` のフルパスハードコード |
| `Entity/Staff.cs:378-387` | **InnoUketsukeLib によるスタッフ認証**。Shinseikai 固有 DLL のため移植先の認証方式に差し替え |

---

## 4. ビルド設定（`MedicalLibrary.csproj`）

- 参照 `HintPath` の修正
  - `InnoUketsukeLib.dll`（`..\..\Shinseikai\`）
  - `itextsharp.dll` / `Interop.Excel.dll`（`..\..\Karte\`）
  - Oracle ODP.NET（Oracle 11.2 クライアントの固定パス、x86）
- 出力先ディレクトリ

新環境のフォルダ構成に合わせて修正する。特に `InnoUketsukeLib` は Shinseikai 固有のため、参照ごと外すか差し替える。

---

## 移植前の確認事項

移植先の電子カルテについて以下を確認すること。

1. **Oracle を使用しているか**（DB アクセスは ODP.NET / Oracle 前提）
2. **DBリンクで参照するテーブル構成（`M_PATIENT` 等）は現行と同一か**

同一であれば変更は主に §1・§3・§4 で済むが、異なる場合は §2 の SQL 書き換えが作業の本体になる。
