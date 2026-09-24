# InnoUketsukeLib.dll 統合計画（2026-09-24）

> **ステータス: 実装済み（2026-09-24）** — コード変更・ビルド／参照解決の検証・ドキュメント更新まで完了。本番環境での手動テストと配備（手順5・6）は未実施。手動テストの手順は `docs/merge-test-procedure.md` を参照。結果は **8章** を参照。

## 1. 目的と方針

MedicalLibrary.dll が参照している InnoUketsukeLib.dll（`C:\Users\yokam\source\repos\InnoUketsukeLib`）を MedicalLibrary.dll に取り込み、
統合後の MedicalLibrary.dll だけで **EyeData.exe（EyeCenter）/ NidekARK1.exe / CanonRKF1.exe** の3本が動くようにする。

| 決定事項（2026-09-24 確認済み） | 内容 |
|---|---|
| 統合範囲 | **認証処理のみ移植**。InnoUketsukeLib の19ファイルは取り込まない |
| DB 接続 | InnoUketsukeLib 独自の接続（`SettingsCommon.xml`）はやめ、MedicalLibrary の `DB.Db3` を使う |
| 照合中の例外 | 照合を飛ばさず **ログイン失敗** にする（従来は照合を飛ばしてログインを通していた） |
| `AGENT_PASSWORD` | パスワードに `AGENT_PASSWORD` と入力すると照合なしで認証される抜け道を **削除** |
| 作業ブランチ | 作業ブランチは切らず、**master で直接作業** する |

## 2. 調査結果

### 2.1 MedicalLibrary が InnoUketsukeLib を使っていた箇所

使っていたのは次の2か所だけで、3アプリのソースは InnoUketsukeLib を直接参照していない（EyeCenter.csproj にコメントが1行あるだけ）。

| 呼び出し元 | 呼び出し先 | 内容 |
|---|---|---|
| `Utility/LibSettings.cs` `Init()` | `InnoUketsukeLib.Utility.AppInit.g_AppInit.Init()` | 作業ディレクトリの `SettingsCommon.xml` を読み、InnoUketsukeLib 独自の `OracleConnection` を初期化する |
| `Entity/Staff.cs` `Verify()` | `InnoUketsukeLib.Entity.M_USR.g_Usr1.GetData(int, string)` | ログイン時のパスワード照合 |

### 2.2 `M_USR.GetData` の処理

1. 職員コードが 0 なら `false` を返す
2. `M_USR` を `M_DEPT` / `M_DR` / `M_SHIKAKU` と外部結合（`(+)`）して1行取得する。行が無ければ `false` を返す
3. 入力パスワードが `"AGENT_PASSWORD"` なら照合せず `true` を返す
4. `M_USR.PASSWORD` を AES で復号し、入力と一致すれば `true` を返す
   - 方式は AES-128 / CBC / PKCS7、鍵と IV は固定の UTF-8 文字列、暗号文は Base64、平文は UTF-16LE

照合に必要なのは `PASSWORD` 列だけである。結合先はすべて外部結合なので、結合をやめても取得できる行は変わらない。

### 2.3 DB 接続先

| 設定 | ファイル | 接続先 |
|---|---|---|
| InnoUketsukeLib | `SettingsCommon.xml`（作業ディレクトリ） | MEDB@INNO_ORCL |
| MedicalLibrary `DB.Db3` | `MedicalLibrary_Settings.xml` `DBConnectionString3` | medb@inno_orcl |

接続先は同じ。ただし開発機では2つのファイルでパスワードが一致しない。EyeData は普段から `Db3` でデータを取得しているので、`Db3` の認証情報に揃える。

### 2.4 旧実装で例外が出たときの挙動

`Staff.Verify` は、InnoUketsukeLib の呼び出しで例外が出ると `LibUtility.Except(ex, false)` の後にそのまま処理を続け、
**職員コードが `M_USR` にあるだけでログインを通していた**。例外になる主な場合:

- InnoUketsukeLib.dll が無い（FileNotFoundException）
- DB エラーのうち、InnoUketsukeLib が握りつぶさないもの（ORA-12170 / 3113 / 12541 以外）
- `M_USR.PASSWORD` が Base64 として不正、または復号に失敗する値

なお `PASSWORD` が NULL または空文字の場合、例外にはならずに空文字へ復号され、空のパスワードで照合が通る。この挙動は旧実装から変えていない（7章）。

## 3. 変更内容

| ファイル | 変更 |
|---|---|
| `Entity/Staff.cs` | `VerifyInnoUketsukeLib` を削除し、`VerifyPassword(int, string)` と `DecryptPassword(string)` を追加（private static）。`Verify` を変更: 職員コードが数値でない、または 0 なら即座に失敗を返す。例外が出たら失敗を返す。`using System.Runtime.CompilerServices` を `using System.Security.Cryptography` に差し替え |
| `Utility/LibSettings.cs` | `InitInnoUketsukeLib()` とその呼び出しを削除し、使われなくなった `using System.Runtime.CompilerServices` も削除 |
| `MedicalLibrary.csproj` | `<Reference Include="InnoUketsukeLib">` を削除 |
| `README.md` | 依存 DLL の一覧から InnoUketsukeLib.dll を削除 |
| `docs/CHANGELOG.md` | `[Unreleased]` に今回の変更を追記 |

`VerifyPassword` の SQL は、バインド変数を使って `select PASSWORD from M_USR where CODE = :CODE` とした。

public API は変えていない（削除・追加したのは private メンバーだけ）。したがって3アプリの再ビルドは不要。
`AesCryptoServiceProvider` は System.Core にあり、参照の追加は不要。

## 4. 挙動の変化

| ケース | 旧 | 新 |
|---|---|---|
| 正しいパスワード | 成功 | 成功 |
| 誤ったパスワード | 失敗 | 失敗 |
| `M_USR` に無い職員コード | 失敗 | 失敗 |
| 職員コードが数値でない、または 0 | 失敗 | 失敗 |
| パスワードに `AGENT_PASSWORD` | **成功（照合なし）** | **失敗** |
| 照合中の例外（DB 障害・`PASSWORD` の値が不正） | **成功（照合を飛ばす）** | **失敗**（エラー表示もログも無し） |
| `PASSWORD` が NULL または空で、空のパスワードを入力 | 成功 | 成功（変更なし） |
| `SettingsCommon.xml` | 読み込む | 読み込まない |
| InnoUketsukeLib.dll | 読み込む | 読み込まない |

## 5. 検証（実施済み）

| # | 検証 | 結果 |
|---|---|---|
| V1 | MedicalLibrary の Release\|x86 Rebuild | **成功**（警告 0・エラー 0） |
| V2 | ビルドした DLL の AssemblyRef | InnoUketsukeLib が **無い** ことを確認 |
| V3 | 復号結果の一致: 旧 DLL の `M_USR.Encrypt` で暗号化した6種類の文字列（英数・記号・日本語・40文字・空文字）を、新 `DecryptPassword` と旧 `M_USR.Decrypt` で復号して比較 | **6件すべて一致** |
| V4 | 復号の境界値 | 空文字 → 空文字（例外なし。旧実装と同じ）、不正な Base64 → FormatException（→ ログイン失敗） |
| V5 | EyeCenter / NidekARK1 / CanonRKF1 の HEAD を新 DLL に対して Debug\|x86 Rebuild（`ReferencePath` で新 DLL を指定し、出力先は一時フォルダ） | **3本とも成功**。出力に InnoUketsukeLib.dll が含まれない |
| V6 | `C:\Shinseikai\EyeData` にデプロイ済みの EyeData.exe / NidekARK1.exe / CanonRKF1.exe の TypeRef / MemberRef を、新 DLL を置いたフォルダで解決 | 新たな未解決は **0件**。EyeData.exe で解決できない7件は、デプロイ中の DLL でも同じく出る（ジェネリック型のメンバーで、この方法では型引数の情報が足りず解決できないもの）。検証中に InnoUketsukeLib が読み込まれないことも確認 |

**未実施**: DB に接続した状態での実行時テスト（→ 手順6）。EyeCenter.Tests は認証処理を対象にしていないため実行していない。

## 6. 手順

### 手順1〜4（実施済み）

1. `Entity/Staff.cs` にパスワード照合を移植する
2. `Utility/LibSettings.cs` から InnoUketsukeLib の初期化を削除する
3. `MedicalLibrary.csproj` から参照を削除する
4. README / CHANGELOG / 本ファイルを更新し、5章の検証を行う

### 手順5: 配備前の確認

- [ ] **ブランチの確認**: 本変更は master（`d1020f4` 起点）に入れた。フェーズ5（`refactor/dead-code-removal`、master 未マージ）は含まれない。
  現在 `C:\Shinseikai\EyeData\MedicalLibrary.dll`（2026-09-23）はフェーズ5のビルドなので、
  master のビルドを配備すると **フェーズ5の削除が元に戻った DLL になる**（3アプリの動作に影響はない。V6 で確認済み）。
  フェーズ5と合わせて配備する場合は、先に `refactor/dead-code-removal` を master にマージしてからビルドする
- [ ] 本番機で、3アプリ以外に InnoUketsukeLib.dll / SettingsCommon.xml を使うアプリが無いこと（ほかにあれば、その2ファイルは残す）
- [ ] 本番の `M_USR` で、有効な職員の `PASSWORD` が正しく復号できること（復号できない職員は、今後ログインできなくなる）

### 手順6: 配備と手動テスト

`docs/merge-test-procedure-innouketsukelib.md` の手順で行う。

- 配備物は MedicalLibrary.dll だけ。3アプリの exe は差し替え不要
- InnoUketsukeLib.dll と SettingsCommon.xml は、テストが終わってから削除する（切り戻しに備え、テスト中は残しておく）

### 手順7: 後片付け（任意）

| 対象 | 内容 |
|---|---|
| `EyeCenter/EyeCenter.csproj` 104行目 | Oracle 参照のコメントにある「MedicalLibrary / InnoUketsukeLib と同じ 1 本の DLL」から InnoUketsukeLib を外す（Shift-JIS のためバイトを壊さない方法で編集する） |
| InnoUketsukeLib リポジトリ | アーカイブする。CLAUDE.md の「MedicalLibrary.dll 経由で利用される」を「2026-09-24 に MedicalLibrary.dll に統合済み」に改める |

## 7. 残っている課題（今回は変更していない）

- `M_USR.PASSWORD` が NULL または空の職員は、空のパスワードでログインできる（旧実装から同じ）
- `Staff.Verify` の後半にある職員情報の取得 SQL は、職員コードを文字列連結で組み立てている。ただし前半で `int.TryParse` に成功したときしかここに来ないので、SQL インジェクションは起きない
- 照合で例外が出たときの `LibUtility.Except(ex, false)` は、メッセージ表示もログ出力もしない。DB 障害でログインできないときも「ログインに失敗しました」としか表示されない

## 8. 実施結果

| 項目 | 内容 |
|---|---|
| 実施日 | 2026-09-24 |
| ブランチ | master（起点 `d1020f4`） |
| 変更ファイル | `Entity/Staff.cs` `Utility/LibSettings.cs` `MedicalLibrary.csproj` `README.md` `docs/CHANGELOG.md`、本ファイル、`docs/merge-test-procedure-innouketsukelib.md` |
| MedicalLibrary.dll（Release\|x86） | 383,488 byte |
| 検証 | 5章の V1〜V6 すべて合格 |
| 未実施 | 手順5・6（本番機の確認、配備、手動テスト） |
