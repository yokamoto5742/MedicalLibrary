# InnoUketsukeLib 統合版 MedicalLibrary.dll 本番環境テスト手順

対象: InnoUketsukeLib.dll を統合した MedicalLibrary.dll（計画と変更内容は `docs/merge-plan-innouketsukelib.md`）
配備先: `C:\Shinseikai\EyeData\`
所要時間: 約30分

## 0. 準備

### 0.1 用意するもの

| # | 用意するもの | 用途 |
|---|---|---|
| P1 | 統合版 `MedicalLibrary.dll`（`MedicalLibrary\bin\x86\Release\MedicalLibrary.dll`、383,488 byte） | 配備する DLL |
| P2 | テスト用職員 **A**: `M_USR` にあり、パスワードが分かっている職員コード | ログイン成功の確認 |
| P3 | テスト用職員 **B**: A とは別の職員コードとパスワード | 2人ログインの確認 |
| P4 | `M_USR` に **無い** 職員コード（例: `99999`） | 失敗の確認 |

> **ブランチに注意**: 本変更は master に入っており、フェーズ5（`refactor/dead-code-removal`）は含まない。
> フェーズ5と合わせて配備する場合は、先にフェーズ5を master にマージしてから Release\|x86 でビルドし直した DLL を P1 とする。

### 0.2 事前確認

- [ ] 本番機で、EyeData.exe / NidekARK1.exe / CanonRKF1.exe 以外に InnoUketsukeLib.dll・SettingsCommon.xml を使うアプリが無い
- [ ] 3アプリがすべて終了している（DLL がロック中だと差し替えられない）
- [ ] 必要なら DB 管理者に依頼し、有効な職員の `PASSWORD` が復号できることを確認しておく（統合後は、復号できない職員はログインできなくなる）

## 1. バックアップ

コマンドプロンプトで実行する:

```bat
cd /d C:\Shinseikai\EyeData
copy MedicalLibrary.dll   MedicalLibrary.dll.bak-merge-20260924
copy InnoUketsukeLib.dll  InnoUketsukeLib.dll.bak-merge-20260924
copy SettingsCommon.xml   SettingsCommon.xml.bak-merge-20260924
```

- [ ] バックアップ3ファイルができている

## 2. 配備

1. P1 の `MedicalLibrary.dll` を `C:\Shinseikai\EyeData\` に上書きコピーする
2. 次のコマンドでサイズが 383,488 byte になっていることを確認する

   ```bat
   dir C:\Shinseikai\EyeData\MedicalLibrary.dll
   ```

- [ ] サイズが一致した
- [ ] InnoUketsukeLib.dll と SettingsCommon.xml は **まだ消さない**（手順4で消して確認する）

## 3. テスト（InnoUketsukeLib.dll を残した状態）

### 3.1 ログイン画面を出す

EyeData.exe を **直接** 起動する（電子カルテ経由や、`-u` 付きのショートカットは使わない）。

- ログイン画面が出ない場合は、`pat.csv` のログインユーザーで自動的にログインしている。
  `pat.csv` は作業フォルダ → `C:\shinseikai` → `C:\karte` などの順に探される。電子カルテを使っていない時間帯・端末でテストするか、
  電子カルテが終了していることを確かめてから `pat.csv` を一時的に別名に変える（テスト後は必ず元に戻す）。

### 3.2 ログインテスト

| # | 職員コード | パスワード | 期待する結果 | 結果 |
|---|---|---|---|---|
| T1 | A | A の正しいパスワード | ログイン画面が閉じ、EyeData のメイン画面が開く | ☐ |
| T2 | A | 誤ったパスワード（例: 正しいパスワードの末尾に `x` を足す） | 「ログインに失敗しました」と表示され、ログイン画面に戻る | ☐ |
| T3 | A | 空欄 | 失敗する（A のパスワードが空でない場合） | ☐ |
| T4 | P4（無いコード） | 何でもよい | 失敗する | ☐ |
| T5 | `abc`（数字以外） | 何でもよい | 失敗する（例外メッセージは出ない） | ☐ |
| T6 | A | `AGENT_PASSWORD` | **失敗する**（旧 DLL では通っていた） | ☐ |
| T7 | A と B（ログイン画面で2人ログインに切り替える） | 両方正しい | ログインできる | ☐ |
| T8 | A と B（2人ログインに切り替える） | B だけ誤り | 失敗する | ☐ |

T1 の後、患者を1人開き、検査・手術・サマリーの画面が今までどおり表示されることも確認する。

- [ ] 患者表示・検査・手術・サマリーに異常なし

### 3.3 検査機器アプリ

| # | 操作 | 期待する結果 | 結果 |
|---|---|---|---|
| T9 | NidekARK1.exe を起動する | 受信一覧が今までどおり表示される | ☐ |
| T10 | CanonRKF1.exe を起動する | 受信データ画面が今までどおり表示される | ☐ |

## 4. テスト（InnoUketsukeLib.dll を外した状態）

統合版が InnoUketsukeLib.dll 無しで動くことを確かめる。3アプリをすべて終了してから、次を実行する:

```bat
cd /d C:\Shinseikai\EyeData
ren InnoUketsukeLib.dll InnoUketsukeLib.dll.unused
ren SettingsCommon.xml  SettingsCommon.xml.unused
```

| # | 操作 | 期待する結果 | 結果 |
|---|---|---|---|
| T11 | EyeData.exe を直接起動し、A の正しいパスワードでログインする | ログインできる（エラーのダイアログが出ない） | ☐ |
| T12 | A に誤ったパスワードを入れる | 失敗する | ☐ |
| T13 | NidekARK1.exe / CanonRKF1.exe を起動する | 今までどおり表示される | ☐ |

すべて合格したら、`.unused` の2ファイルとバックアップの2ファイル（`InnoUketsukeLib.dll.bak-*`・`SettingsCommon.xml.bak-*`）は、しばらく置いてから削除してよい。

## 5. 不具合があったとき（切り戻し）

3アプリをすべて終了してから、次を実行する:

```bat
cd /d C:\Shinseikai\EyeData
copy /y MedicalLibrary.dll.bak-merge-20260924   MedicalLibrary.dll
copy /y InnoUketsukeLib.dll.bak-merge-20260924  InnoUketsukeLib.dll
copy /y SettingsCommon.xml.bak-merge-20260924   SettingsCommon.xml
```

切り戻した後、T1 でログインできることを確認する。

### よくある症状

| 症状 | 考えられる原因 |
|---|---|
| 正しいパスワードでも全員ログインできない | `MedicalLibrary_Settings.xml` の `DBConnectionString3` で DB に接続できていない。統合版は例外が出るとメッセージを出さずにログイン失敗にする。T1 の後の患者表示も失敗するなら、接続の問題 |
| 特定の職員だけログインできない | その職員の `M_USR.PASSWORD` が復号できない値になっている（旧 DLL では照合を飛ばしてログインできていた）。パスワードを再設定する |
| `AGENT_PASSWORD` でログインしていた運用がある | 統合版では使えない。正規のパスワードでログインする |

## 6. 記録

| 項目 | 記入 |
|---|---|
| 実施日時 | |
| 実施者 | |
| 配備した DLL のサイズ | |
| T1〜T13 の結果 | |
| 備考 | |
