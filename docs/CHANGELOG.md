# 変更履歴

このプロジェクトのすべての重要な変更は、このファイルに記録されます。

フォーマットは [Keep a Changelog](https://keepachangelog.com/ja/1.1.0/) に基づいており、
バージョン番号は [Semantic Versioning](https://semver.org/lang/ja/) に従っています。

## [Unreleased]

### 変更
- InnoUketsukeLib.dll を MedicalLibrary.dll に統合（詳細は `docs/merge-plan-innouketsukelib.md`）
  - ログイン時のパスワード照合（`M_USR.PASSWORD` の AES 復号・照合）を `Entity/Staff.cs` に移植し、`DB.Db3` で照会するよう変更
  - 照合中に例外（DB 障害・復号失敗など）が発生した場合は、照合を飛ばさずログイン失敗とするよう変更
  - パスワード `AGENT_PASSWORD` で照合なしに認証される抜け道を廃止

### 削除
- InnoUketsukeLib.dll への参照と、`LibSettings.Init` での InnoUketsukeLib 初期化（`SettingsCommon.xml` は読み込まれなくなった）

## [1.2.1] - 2026-09-23

### 追加
- レフケラ検査の値をエクスポート機能を追加
- TableData クラスに CSV・Excel 出力の進捗通知と中止処理を追加
  - Excel 出力は .xlsx 形式での保存に対応
  - 進捗通知コールバックにより、UI の進捗表示が可能
  - OperationCanceledException で出力中止を実装

### 変更
- 検索処理に進捗通知の引数を追加
- 検査結果検索の速度を向上
- Utility/TableData.cs の SelectSaveFile メソッドに filter パラメータを追加し、保存ダイアログのファイルフィルタをカスタマイズ可能に
- Utility/TableData.cs の ExcelOpen メソッドを ExcelWrite に改名し、Excel 出力ファイルの保存処理を統合

### 削除
- `docs/cleanup-plan-phase4.md` を削除

## [1.2.0] - 2026-09-15

### 削除
- EyeCenter.exe / NidekARK1.exe / CanonRKF1.exe から到達しないコードをファイル単位で削除（フェーズ4、`docs/cleanup-plan-phase4.md`）
  - .cs 251ファイル・.resx 72ファイル（計323ファイル、約9.4万行）を削除し、csproj から該当項目を除外
  - 主な削除機能: カルテ本体UI（`FormPat` / `FormControl` 等）、オーダ・会計伝票、手術オーダ（`FormOpeOrder` / `OpeOrderData` / `OpeOrderMaster` / `OpeOrderPathTemplate` / `PathMaster`）、クリニカルパス、SOAP・カルテ記載、病名・DPC、入院・病棟、予約、バイタル、請求（`Bill` / `BillPay`）、来院報告（`ComeReport*`）、`AppColor` / `CsvWriter`
  - MedicalLibrary.dll（Release|x86）が約2.5MBから約380KBに縮小

## [1.1.0] - 2026-07-04

### 追加
- エクスポート画面に「患者（患者マスタ）」を追加（電子カルテ移行時の患者マスタ移行のバックアップ用。EYE系全テーブルに登場する PATIENT_ID の基本情報を出力）
- エクスポート画面に「手術予約（EYE_OPE）」を追加（手術記録エクスポートに含まれない記録未作成の予約も含め全件出力）
- エクスポートの出力文字コードに UTF-8（BOM付き）オプションを追加（既定は従来どおり Shift-JIS）
- エクスポート完了時に件数照合用マニフェスト（対象・ファイル名・件数・出力日時）を出力

### 修正
- 手術記録・サマリーCSVの各行末に余分な空カラム（末尾カンマ）が出力されていたのを修正
- サマリーCSVで項目値に半角スペースが2つ以上含まれる場合に3トークン目以降が欠落していたのを修正
- 手術記録・検査CSVから常時空のPDF関連カラム（PDF_SAVE / PDF_DATE / PDF_TIME）を削除

## [1.0.0] - 2026-06-30
- 初版リリース
