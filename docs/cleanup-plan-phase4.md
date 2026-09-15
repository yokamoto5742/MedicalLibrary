# MedicalLibrary 削減計画 フェーズ4（2026-09-15）

> **ステータス: 実施済み（2026-09-15）** — コード削除・ビルド/テスト検証・ドキュメント更新まで完了。実行時スモークテスト（手順6）と本番機の確認（手順0）は未実施。結果は **10章** を参照。

## 1. 目的と方針

MedicalLibrary.dll の利用アプリ **EyeCenter.exe（EyeData.exe）/ NidekARK1.exe / CanonRKF1.exe** の3本から
到達しないコードを **ファイル単位で** 削除する。

| 決定事項（2026-09-15 確認済み） | 内容 |
|---|---|
| NidekARK1.exe / CanonRKF1.exe | 今後も使用する → 専用コード（`NidekARK1ListForm` / `CanonRKF1Form`）は保持 |
| 削除粒度 | **ファイル単位のみ**。保持ファイル内の未使用メソッド・未使用型は削除しない |
| OpeOrder 系 | 削除対象とする（フェーズ3で「EyeCenter から到達可能」として保持したが、実際には到達しないことを確認） |

## 2. 規模

| | 現状 | 削除 | 削除後 |
|---|---|---|---|
| .cs ファイル | 316 | **251** | 65 |
| .resx ファイル | 81 | **72** | 9 |
| .cs 行数 | 112,880 | **94,486（84%）** | 18,394 |
| csproj の Compile/EmbeddedResource 項目 | 397 | **323** | 74 |
| MedicalLibrary.dll（Release\|x86） | 2,494,976 byte | | 378,880 byte（試行ビルド実測） |

フォルダ別の削除数:

| フォルダ | .cs | .resx | 行数 |
|---|---|---|---|
| Agent | 22 | 5 | 11,238 |
| Boundary | 154 | 67 | 60,566 |
| Entity | 73 | 0 | 22,456 |
| Utility | 2 | 0 | 226 |

## 3. 解析方法

ソース上の名前検索ではなく、**ビルド済みバイナリの IL（中間言語）を解析する到達可能性解析** で判定した（System.Reflection.Metadata による自作ツール）。

1. **起点**: 各アプリ exe が MedicalLibrary に対して持つ TypeRef / MemberRef（直接参照している型・メソッド・フィールド）
2. **閉包**: ライブラリ内のメソッド本体の IL を走査し、call / newobj / ldfld / ldftn / ldtoken 等の参照、ローカル変数・シグネチャ・基底型・インターフェイスの型を推移的に追跡
3. **保守側の扱い**: 到達した型の virtual メソッド（WinForms のオーバーライド等）・静的コンストラクタ・全プロパティアクセサを到達扱い
4. **コンパイル依存の閉包**: ファイル単位で残す場合、保持ファイル内の *未使用* メソッドが参照する型もコンパイルに必要になる。保持ファイルの全メンバーを起点に再解析し、不動点まで反復した（→ `KarteLog.cs` / `AppDateTime.cs` の2ファイルが追加で必要と判明）

補助確認:
- MedicalLibrary / EyeCenter ともにリフレクション（`Type.GetType` / `Activator` / `InvokeMember` によるライブラリ型の動的生成）、`XmlSerializer` は無い
- IL に現れない `const` のインライン化: EyeCenter が使うのは `WinAPI.WM_COPYDATA` のみ（保持ファイル）。EyeCenter を削減版 DLL でコンパイルして問題ないことを確認済み
- `#if` 条件コンパイルはライブラリに存在しない（Debug/Release で解析結果は変わらない）

## 4. 保持するファイル（65 .cs + 9 .resx）

| 保持理由 | ファイル |
|---|---|
| EyeCenter が使用（Agent） | `EyeDict` `EyeDoc` `EyeIV` `EyeKensa` `EyeKensa2` `EyeKensaBase` `EyeKensaItemMaster` `EyeKensaMaster` `EyeLensMeter` `EyeOpe` `EyeOpeDoctor` `EyeOpePass` `EyeOpeRecord` `EyeOpeRsv` `EyeRefKrt` `EyeSummary` `NidekARK1` `NidekARK1Settings` |
| NidekARK1.exe / CanonRKF1.exe が使用 | `Agent/NidekARK1ListForm`（.cs / .designer.cs / .resx）、`Agent/CanonRKF1Form`（.cs / .designer.cs / .resx） |
| EyeCenter が使用（Boundary） | `FormFindPat` `FormString1` `LoginChange` `LoginPrompt` `StdControlPat1`（各 .cs / .designer.cs / .resx）、`StdForm1`（.cs / .resx） |
| EyeCenter が使用（Entity） | `Allergy` `BaseInfo` `Dict` `InfectionData` `LoginUser` `PatBase` `PatContact` `PatIn` `PatIns` `Staff` `StdClass` `StdEntity` `StdKarte1` `StdMaster1` |
| EyeCenter が使用（Utility） | `AppDataGridView` `AppFile` `AppFont` `AppStat` `AppString` `DateTimeAgent` `DB` `Env` `InnoProgram` `Launcher` `LibSettings` `LibUtility` `TableData` `WinAPI` |
| **コンパイル依存のみ**（実行時は未到達） | `Entity/KarteLog.cs`（`LibUtility.Log` が参照）、`Utility/AppDateTime.cs`（`PatBase.GetInfo1` の引数型 `AppDateTime.LANG`） |
| プロジェクト標準 | `Properties/AssemblyInfo.cs`、`Properties/Resources.Designer.cs` + `Properties/Resources.resx`（中身は VS テンプレートのみで未使用だが、VS 管理ファイルのため保持） |

参考: 保持ファイル内に同居する未使用型（ファイル単位方針のため残す）
- `BaseInfo.cs` の `BaseInfoFixed`、`PatIn.cs` の `PatInDPCWard` / `PatInRoomGroup`、`PatIns.cs` の `PatInsSEQ`

## 5. 削除対象（機能別の概要）

正確なパス一覧は **付録A**（これを正とする）。

| 機能 | Boundary | Entity / Agent / Utility |
|---|---|---|
| カルテ本体UI・ハブ | `FormControl` `FormPat` `FormPatList` `FormPatBoard` `FormBaseInfo` `FormAddressGroup` `FormDeptStat` | `PatBoard` `BaseInfoMaster` `AddressGroup` `PatDept` |
| オーダ（処方・注射・看護・手術指示・会計伝票） | `FormOrder` `FormBaseOrder` `FormNursingOrder` **`FormOpeOrder`** `FormOrderKaikei` `FormExec` `CtrlOrder1` `CtrlOrderPanel1` `CtrlOrderSheetGridView1` `CtrlOrderView1` `CtrlDrugYoho1` `CtrlInjectionYoho1` `CtrlVoucherPanel1` | `OrderHeader` `OrderDetail` `OrderMaster` `OrderSetMaster` `PatOrder` `PatOrderDetail` `PreOrder` `PreBarcode` `BaseOrderData` `BaseOrderMaster` `BaseOrderPathTemplate` `NursingOrderData` `NursingOrderMaster` `NursingOrderPathTemplate` **`OpeOrderData` `OpeOrderMaster` `OpeOrderPathTemplate`** `Voucher` `VoucherComp` `VoucherCompGroup` `VoucherIji` `VoucherLink` |
| クリニカルパス | `FormPath` `FormPathTree` | `PathCategoryMaster` **`PathMaster`** `PathMasterTask` `PathMasterTaskDetail` |
| SOAP・カルテ記載 | `FormSoap` `FormSchema1` `FormKarteTemplate1` `FormKarteMessage1` `FormKarteMessage2` `FormProblem1` `FormPostIt1` `FormMemo` `FormPDFViewer1` `CtrlSoapImgBox1` `CtrlSoapPanel1` `CtrlSoapWrite1` `CtrlKarteTemplate1` `CtrlProblemGridView1` `CtrlPostItGridView1` | `Soap` `SoapDetail` `SoapHeader` `SoapOrderHeader` `SchemaImage` `KarteTemplate` `KarteTemplateComp` `KarteTemplateCompGroup` `KarteTemplateLink` `KarteTemplateWrite` `KarteMessage` `ProblemData` `ProblemMaster` `PostIt` `Memo` `PdfDoc` `StdKarte2` |
| 病名・DPC | `FormDiag` `FormDiagNan` `FormDiagSearch` `FormDiagView` `FormFindDiag` `FormDiagDPC` `FormDPCCheck1` `FormDPCData1` `FormDPCData2` `FormDPCList` `FormDPCList2` `CtrlDiagGridView1` `CtrlDiagDPCGridView1` | `Diag` `DiagMaster` `DiagNan` `DiagDPC` `DPCData` `DPCHeader` `DPCICDNotice` `DPCItem1` `DPCItem2` `DPCOpeMaster` |
| 入院・病棟・退院 | `FormByotoLayout` `FormByotoList` `FormInCal2` `FormFindPatIn` `FormDischargeSummary` `CtrlInHistoryBox1` `CtrlInHistoryBox2` `CtrlInHistoryGridView1` | `ByotoBed` `ByotoRoom` `ByotoPat`（全行コメントアウト済み） `PatOut` `PatOutKarteStatus` `PatOutKoui` `DischargeSummary` |
| 予約 | `FormRsv` `FormRsvDetail` `FormRsvs` `FormPatRsv` | `RsvData` `RsvMaster` |
| バイタル・検査・麻酔/手術記録 | `FormVital` `FormVitalDaily` `FormKensa` `FormAnesRecord` `FormFindOpe` | `Vital` `VitalMaster` `Kensa` `OpeRecord` |
| 患者情報ダイアログ・汎用コントロール | `FormPatContact` `FormFindPatIns` `FormFindStaff` `FormConfirm` `FormDateSelector` `FormSelector` `FormInputString1` `StdControlFont1` `CtrlAllergy1` `CtrlInfection1` `CtrlAlphaBox1` `CtrlDateBox1` `CtrlDateBox2` `CtrlDeptBox1` `CtrlDoctorBox1` `CtrlNumBox1` `CtrlPanel1` `CtrlSectionBox1` `CtrlTextBox1` `CtrlTimeBox1` | `Cust1` `Cust2` `Cust3` |
| 会計・請求（Agent） | — | `Bill` `BillMakerSettings` `BillPay` `FormBillPDFList` `Invoice` `PosDemand` `PosRegHistoryDetail` |
| 来院報告（Agent） | — | `ComeReportData` `ComeReportOrder` `ComeReportSchema` `ComeReportSchemaItem` `ComeReportSettings` `FormComeReportPat` `FormComeReportSchema` `FormComeReportTab` `FormComeReportTemplate` |
| その他 | — | `Agent/Q26`、`Utility/AppColor`、`Utility/CsvWriter` |

※ Form / Ctrl は `.cs` と対になる `.designer.cs`（または `.Designer.cs`）・`.resx` も削除する。

### フェーズ3の記述との食い違い（OpeOrder 系）

フェーズ3では「FormPat→FormOpeOrder、FormPath→OpeOrderPathTemplate / PathMaster は EyeCenter から到達可能」として保持した。
しかし、到達の前提だった **`MedicalLibrary.Boundary.FormPat` / `FormControl` 自体を EyeCenter が使っていない**
（EyeCenter は同名の自前クラス `EyeCenter.FormPat` / `EyeCenter.FormControl` を持つ）。IL 解析・EyeCenter ソース検索とも OpeOrder 系への参照は 0 件。

## 6. 試行で確認済みの事項

`git archive HEAD` でスクラッチ領域に展開したコピーに対して、本計画どおりの削除を行い検証した。
（MedicalLibrary は `docs/agentlab-merge-study` ブランチ HEAD `2709788`。master `8737de2` との差分は docs のみでコードは同一）

| # | 検証 | 結果 |
|---|---|---|
| T1 | 削除版 MedicalLibrary の Release\|x86 Rebuild | **成功**（エラー 0） |
| T2 | 削除版 DLL に対する IL 参照解決（リポジトリビルドの EyeData.exe / NidekARK1.exe / CanonRKF1.exe、および `C:\Shinseikai\EyeData` 配備済みの EyeData.exe / NidekARK1.exe（2018年ビルド）/ CanonRKF1.exe（2018年ビルド）） | **未解決参照 0**。到達集合も削除前と同一（型115・メソッド550） |
| T3 | EyeCenter HEAD（`6229e73`）を削除版 DLL に対して Debug\|x86 Rebuild | **成功** |
| T4 | 陰性対照: `KarteLog.cs` / `AppDateTime.cs` も削除した場合 | 解析どおり CS0246 `AppDateTime` 等で失敗 → コンパイル依存の検出が機能していることを確認 |

**未実施**（本適用時に行う）: NidekARK1 / CanonRKF1 の Rebuild、EyeCenter.Tests、実行時スモークテスト。

## 7. 実施手順

### 手順0: 事前確認

- [ ] MedicalLibrary の作業ツリーがクリーンであること（現在 `docs/agentlab-merge-study` ブランチにいるため、master に戻す）
- [ ] **本番機・配備先に、上記3アプリ以外で MedicalLibrary.dll を参照する exe が無いこと**（解析対象外のアプリがあると実行時に MissingMethodException / TypeLoadException になる）
- [ ] 現行 DLL のバックアップ: `C:\Shinseikai\EyeData\MedicalLibrary.dll` → 別名コピー
  - 注意: MedicalLibrary の **Debug 構成の出力先は `C:\Shinseikai\EyeData\` 直下**。Debug ビルドした時点で配備中の DLL が上書きされる

### 手順1: ブランチ作成

```bash
cd MedicalLibrary
git switch master
git switch -c refactor/cleanup-phase4
```

### 手順2: ファイル削除

付録A のコードブロックを `del-list.txt` として保存（`#` 行はコメント）。

```bash
grep -v '^#' del-list.txt | grep -c .          # → 323 であること
grep -v '^#' del-list.txt | xargs git rm --quiet --
find Agent Boundary Entity Utility Properties -name '*.cs'   | wc -l   # → 65
find Agent Boundary Entity Utility Properties -name '*.resx' | wc -l   # → 9
```

### 手順3: csproj から該当項目を削除

`MedicalLibrary.csproj` は **UTF-8（BOM付き）・CRLF**。エンコーディングと改行を維持するため、バイト列のまま処理する次のスクリプトを使う
（Visual Studio で「プロジェクトから除外」を 323 回行っても同じ結果になる）。

```perl
# csproj_remove.pl — usage: perl csproj_remove.pl del-list.txt MedicalLibrary.csproj
use strict;
my %d;
open(my $l, '<', $ARGV[0]) or die "list: $!";
while (my $p = <$l>) { $p =~ s/\r?\n$//; next if $p =~ /^#/ || $p eq ''; $p =~ tr/\x2f/\x5c/; $d{$p} = 1; }
close($l);
open(my $c, '<:raw', $ARGV[1]) or die "csproj: $!";
my $x = do { local $/; <$c> };
close($c);
my $n = 0;
$x =~ s{[ \t]*<(Compile|EmbeddedResource) Include="([^"]+)"(?: />|>.*?</\1>)\r?\n}{ exists $d{$2} ? do { $n++; '' } : $& }gse;
open(my $o, '>:raw', $ARGV[1]) or die "write: $!";
print $o $x;
close($o);
print "removed $n items\n";
```

```bash
LC_ALL=C perl csproj_remove.pl del-list.txt MedicalLibrary.csproj                # → removed 323 items
grep -cE '<Compile Include|<EmbeddedResource Include' MedicalLibrary.csproj      # → 74
file MedicalLibrary.csproj                                                        # → UTF-8 (with BOM), CRLF のまま
```

`<Reference>` / `<PackageReference>`（itextsharp 等）はこの手順では変更しない（→ 9章）。

### 手順4: MedicalLibrary のビルド

```bash
msbuild MedicalLibrary.csproj -t:Rebuild -p:Configuration=Release -p:Platform=x86
```

- [ ] エラー 0
- [ ] 配備用 DLL が必要な場合は、その後 Debug\|x86 でもビルド（出力先 `C:\Shinseikai\EyeData\`）

### 手順5: 利用アプリのビルド・テスト

- [ ] EyeCenter: `msbuild EyeCenter.sln /p:Configuration=Debug /p:Platform=x86` → 成功
- [ ] EyeCenter.Tests: `dotnet test EyeCenter.Tests/EyeCenter.Tests.csproj` → 全件成功
- [ ] NidekARK1 / CanonRKF1: 各リポジトリで Rebuild → 成功
  （両リポジトリは csproj に未コミット変更あり。ビルドのみ行い、コミットはしない）

### 手順6: 実行時スモークテスト（DB 接続環境）

削除版 DLL を配置した状態で、EyeCenter から MedicalLibrary を経由する機能を一通り通す。

| 対象 | 操作 | 経由する主な保持コード |
|---|---|---|
| EyeCenter 起動 | `-u <職員番号>` 起動・ログイン画面・ユーザー切替 | `LoginPrompt` `LoginChange` `LoginUser` `LibSettings` `Env` `DB` |
| 患者 | 患者検索ダイアログ・患者情報表示（アレルギー・感染症・連絡先・入院歴） | `FormFindPat` `StdControlPat1` `PatBase` `Allergy` `InfectionData` `PatContact` `PatIn` `PatIns` |
| 検査 | 検査入力・保存・履歴・検索、レフケラ取込 | `EyeKensa` `EyeKensa2` `EyeKensaMaster` `EyeKensaItemMaster` `EyeRefKrt` `EyeLensMeter` `NidekARK1` |
| 手術 | 手術予約・カレンダー・記録入力・術者/パス | `EyeOpe` `EyeOpeRsv` `EyeOpeRecord` `EyeOpeDoctor` `EyeOpePass` |
| サマリー・IV | サマリー入力・検索、IV | `EyeSummary` `EyeIV` |
| 帳票 | 印刷・Excel 出力（オペ録・申し送り書） | `EyeDoc` `TableData` `DateTimeAgent` |
| 外部連携 | 受付連携（InnoUketsukeLib）・ウィンドウ連携 | `InnoProgram` `Launcher` `WinAPI` `Staff` |
| NidekARK1.exe | 起動・受信一覧表示 | `NidekARK1ListForm` |
| CanonRKF1.exe | 起動・受信データ表示 | `CanonRKF1Form` |

### 手順7: ドキュメント更新

| ファイル | 更新内容 |
|---|---|
| `MedicalLibrary/CLAUDE.md` 7行目 | 「OpeOrder 系を EyeCenter から到達可能として保持」の記述を削除し、フェーズ4で削除した旨に修正。存在しない `docs/cleanup-plan.md` への参照を本ファイルに差し替え |
| `MedicalLibrary/CLAUDE.md` 40行目 | Boundary 等の説明（「Form* dialogs, Ctrl* controls」）を実態に合わせる |
| `MedicalLibrary/README.md` | 機能説明（SOAP・請求 `Bill`・来院報告 `ComeReportOrder` 等）、ディレクトリ構成、211行目の OpeOrder 保持の記述、214行目の `docs/cleanup-plan.md` 参照を修正 |
| `MedicalLibrary/docs/CHANGELOG.md` | フェーズ4の削除を追記 |
| 本ファイル | 冒頭ステータスを「実施済み」に更新し、実施結果（コミット・検証結果）を追記 |
| `EyeCenter/CLAUDE.md` 13行目 | `FormControl` は EyeCenter 自身のクラスである旨に修正（MedicalLibrary 側の `FormControl` は削除される） |
| `EyeCenter/CLAUDE.md` 33行目 | 「OpeOrder を起動する箇所がある」の記述が現状と合っているか確認し修正 |

### 手順8: コミット

途中状態でビルドが壊れないよう、**コード削除は1コミットにまとめる**（機能別に分けると、削除済みの型を未削除の未使用コードが参照してビルドが通らない中間状態が生じる）。

1. `♻️ refactor: 3アプリから到達しないコード323ファイルを削除（フェーズ4）` — ファイル削除 + csproj
2. `📝 docs: フェーズ4の削減内容をドキュメントに反映` — CLAUDE.md / README.md / CHANGELOG.md / 本ファイル
3. （EyeCenter リポジトリ）`📝 docs: MedicalLibrary 削減に合わせて CLAUDE.md を修正`

### 手順9: 配備

- EyeCenter の `deploy.ps1` / `deploy-production.ps1` は `bin` 配下の `*.dll` をコピーするため、EyeCenter のビルド出力に含まれる削除版 `MedicalLibrary.dll` がそのまま配備される（スクリプト変更不要）
- 配備済みの NidekARK1.exe / CanonRKF1.exe（2018年ビルド）は削除版 DLL と互換（T2 で確認済み）。再配備は不要

### ロールバック

- コード: `git revert <手順8-1 のコミット>`
- 実行環境: 手順0でバックアップした `MedicalLibrary.dll` を `C:\Shinseikai\EyeData\` に戻す（exe 側の変更は不要）

## 8. リスクと対策

| リスク | 評価 | 対策 |
|---|---|---|
| 解析対象外のアプリが MedicalLibrary.dll を使っている | 中（本番機の全体は未確認） | 手順0で配備先を確認。問題時は DLL を戻すだけで復旧可能 |
| リフレクション・シリアライズ等、IL に現れない利用 | 低（ライブラリ・EyeCenter とも該当コード無しを確認） | 手順6のスモークテスト |
| EyeCenter が削除ファイル内の `const` / enum 値をインライン化して使用 | 無し | T3 で EyeCenter のコンパイル成功を確認済み |
| 保持ファイル内の未使用メソッドが削除型を参照してビルド不可 | 無し | コンパイル依存閉包で `KarteLog` / `AppDateTime` を保持、T1 で確認済み |
| Debug ビルドで配備中 DLL が意図せず上書きされる | 中 | 手順0でバックアップ。検証ビルドは Release 構成で行う |
| csproj の文字化け・改行変化 | 低 | 手順3のスクリプトはバイト列のまま処理。`file` コマンドで確認 |

## 9. 対象外（今回はやらないこと）

ファイル単位削除の方針により、以下は変更しない。必要なら別フェーズで検討する。

- 保持ファイル内の未使用メソッド（例: `Launcher` の12メソッド中10、`DateTimeAgent` の31メソッド中19、`WinAPI` の DllImport 28件中21、`PatIn` の11メソッド）と未使用型（`BaseInfoFixed` など）
- 参照 DLL の整理: 保持コードは **itextsharp を使用しなくなる**（`<Reference Include="itextsharp">` と `<PackageReference Include="iTextSharp">` は削除候補）。Interop.Excel / InnoUketsukeLib / Oracle.ManagedDataAccess / Microsoft.VisualBasic / UIAutomation は引き続き使用
- `Properties/Resources.*`（中身が VS テンプレートのみの未使用リソース）
- リポジトリ直下の `ai1.ico` / `ai2.ico` / `gif-load.gif` / `code_review.md`（csproj 未登録）
- csproj の `<None Include="docs\cleanup-plan.md" />` / `<None Include="docs\DLL移植調査.md" />`（いずれもファイルが存在しない）

## 10. 実施結果（2026-09-15）

### コミット

| リポジトリ | コミット | 内容 |
|---|---|---|
| MedicalLibrary（`refactor/cleanup-phase4`、master `8737de2` から分岐） | `9f3cd41` | `♻️ refactor: 3アプリから到達しないコード323ファイルを削除（フェーズ4）` — 323ファイル削除 + csproj |
| MedicalLibrary | 本コミットの次 | `📝 docs: フェーズ4の削減内容をドキュメントに反映` — CLAUDE.md / README.md / CHANGELOG.md / 本ファイル |
| EyeCenter（master） | `a28fa9d` | `📝 docs: MedicalLibrary 削減に合わせて CLAUDE.md を修正` |

- 本ファイルは master に存在しなかったため、`docs/agentlab-merge-study` の `2aaec1e` から取り込んで docs コミットに含めた
- 作業開始時点で csproj に未コミットの `<None Include>` 変更（`docs\cleanup-plan.md` 等の置換）があったが、本作業とは別の変更のためコミットに含めず作業ツリーに残した

### 検証結果

| 手順 | 検証 | 結果 |
|---|---|---|
| 0 | 開発PC `C:\Shinseikai` 配下で MedicalLibrary を参照するバイナリの検索 | `EyeData.exe` / `NidekARK1.exe` / `CanonRKF1.exe` のみ |
| 0 | 現行 DLL のバックアップ | `C:\Shinseikai\EyeData\MedicalLibrary.dll.bak-phase4-20260915`（2,614,272 byte） |
| 2 | 削除件数・残存件数 | 323件削除、残存 .cs 65 / .resx 9 |
| 3 | csproj 項目数・エンコーディング | removed 323 items、残存 74、UTF-8（BOM付き）・CRLF のまま |
| 4 | MedicalLibrary Release\|x86 Rebuild | **成功**（エラー 0・警告 0）、DLL 379,904 byte |
| 4 | MedicalLibrary Debug\|x86 Rebuild | **成功**（`C:\Shinseikai\EyeData\MedicalLibrary.dll` を削減版 400,384 byte に置換） |
| 5 | EyeCenter.sln Debug\|x86 Rebuild | **成功** |
| 5 | EyeCenter.Tests（`dotnet test`） | **成功**（合格 89 / 失敗 0） |
| 5 | NidekARK1 / CanonRKF1 Release\|x86 Rebuild | **成功**（両リポジトリの未コミット変更には触れていない） |

### 未実施

- **手順0: 本番機・配備先で、3アプリ以外に MedicalLibrary.dll を参照する exe が無いことの確認**（開発PCでは確認済み）
- **手順6: 実行時スモークテスト**（DB 接続環境で GUI 操作が必要）
- 手順9: 配備

## 付録A: 削除対象パス一覧（323件: .cs 251 + .resx 72）

```text
# Agent/ (.cs 22 / .resx 5)
Agent/Bill.cs
Agent/BillMakerSettings.cs
Agent/BillPay.cs
Agent/ComeReportData.cs
Agent/ComeReportOrder.cs
Agent/ComeReportSchema.cs
Agent/ComeReportSchemaItem.cs
Agent/ComeReportSettings.cs
Agent/FormBillPDFList.cs
Agent/FormBillPDFList.designer.cs
Agent/FormComeReportPat.cs
Agent/FormComeReportPat.designer.cs
Agent/FormComeReportSchema.cs
Agent/FormComeReportSchema.designer.cs
Agent/FormComeReportTab.cs
Agent/FormComeReportTab.designer.cs
Agent/FormComeReportTemplate.cs
Agent/FormComeReportTemplate.designer.cs
Agent/Invoice.cs
Agent/PosDemand.cs
Agent/PosRegHistoryDetail.cs
Agent/Q26.cs
Agent/FormBillPDFList.resx
Agent/FormComeReportPat.resx
Agent/FormComeReportSchema.resx
Agent/FormComeReportTab.resx
Agent/FormComeReportTemplate.resx
# Boundary/ (.cs 154 / .resx 67)
Boundary/CtrlAllergy1.cs
Boundary/CtrlAllergy1.Designer.cs
Boundary/CtrlAlphaBox1.cs
Boundary/CtrlDateBox1.cs
Boundary/CtrlDateBox1.Designer.cs
Boundary/CtrlDateBox2.cs
Boundary/CtrlDeptBox1.cs
Boundary/CtrlDiagDPCGridView1.cs
Boundary/CtrlDiagGridView1.cs
Boundary/CtrlDoctorBox1.cs
Boundary/CtrlDrugYoho1.cs
Boundary/CtrlDrugYoho1.Designer.cs
Boundary/CtrlInfection1.cs
Boundary/CtrlInfection1.Designer.cs
Boundary/CtrlInHistoryBox1.cs
Boundary/CtrlInHistoryBox2.cs
Boundary/CtrlInHistoryBox2.Designer.cs
Boundary/CtrlInHistoryGridView1.cs
Boundary/CtrlInjectionYoho1.cs
Boundary/CtrlInjectionYoho1.Designer.cs
Boundary/CtrlKarteTemplate1.cs
Boundary/CtrlKarteTemplate1.Designer.cs
Boundary/CtrlNumBox1.cs
Boundary/CtrlOrder1.cs
Boundary/CtrlOrder1.Designer.cs
Boundary/CtrlOrderPanel1.cs
Boundary/CtrlOrderSheetGridView1.cs
Boundary/CtrlOrderView1.cs
Boundary/CtrlOrderView1.Designer.cs
Boundary/CtrlPanel1.cs
Boundary/CtrlPostItGridView1.cs
Boundary/CtrlProblemGridView1.cs
Boundary/CtrlSectionBox1.cs
Boundary/CtrlSoapImgBox1.cs
Boundary/CtrlSoapPanel1.cs
Boundary/CtrlSoapPanel1.Designer.cs
Boundary/CtrlSoapWrite1.cs
Boundary/CtrlSoapWrite1.Designer.cs
Boundary/CtrlTextBox1.cs
Boundary/CtrlTimeBox1.cs
Boundary/CtrlVoucherPanel1.cs
Boundary/FormAddressGroup.cs
Boundary/FormAddressGroup.Designer.cs
Boundary/FormAnesRecord.cs
Boundary/FormAnesRecord.designer.cs
Boundary/FormBaseInfo.cs
Boundary/FormBaseInfo.designer.cs
Boundary/FormBaseOrder.cs
Boundary/FormBaseOrder.designer.cs
Boundary/FormByotoLayout.cs
Boundary/FormByotoLayout.designer.cs
Boundary/FormByotoList.cs
Boundary/FormByotoList.designer.cs
Boundary/FormConfirm.cs
Boundary/FormConfirm.designer.cs
Boundary/FormControl.cs
Boundary/FormDateSelector.cs
Boundary/FormDateSelector.designer.cs
Boundary/FormDeptStat.cs
Boundary/FormDeptStat.designer.cs
Boundary/FormDiag.cs
Boundary/FormDiag.designer.cs
Boundary/FormDiagDPC.cs
Boundary/FormDiagDPC.Designer.cs
Boundary/FormDiagNan.cs
Boundary/FormDiagNan.Designer.cs
Boundary/FormDiagSearch.cs
Boundary/FormDiagSearch.designer.cs
Boundary/FormDiagView.cs
Boundary/FormDiagView.Designer.cs
Boundary/FormDischargeSummary.cs
Boundary/FormDischargeSummary.Designer.cs
Boundary/FormDPCCheck1.cs
Boundary/FormDPCCheck1.Designer.cs
Boundary/FormDPCData1.cs
Boundary/FormDPCData1.designer.cs
Boundary/FormDPCData2.cs
Boundary/FormDPCData2.Designer.cs
Boundary/FormDPCList.cs
Boundary/FormDPCList.Designer.cs
Boundary/FormDPCList2.cs
Boundary/FormDPCList2.Designer.cs
Boundary/FormExec.cs
Boundary/FormExec.designer.cs
Boundary/FormFindDiag.cs
Boundary/FormFindDiag.Designer.cs
Boundary/FormFindOpe.cs
Boundary/FormFindOpe.Designer.cs
Boundary/FormFindPatIn.cs
Boundary/FormFindPatIn.Designer.cs
Boundary/FormFindPatIns.cs
Boundary/FormFindPatIns.Designer.cs
Boundary/FormFindStaff.cs
Boundary/FormFindStaff.designer.cs
Boundary/FormInCal2.cs
Boundary/FormInCal2.Designer.cs
Boundary/FormInputString1.cs
Boundary/FormInputString1.Designer.cs
Boundary/FormKarteMessage1.cs
Boundary/FormKarteMessage1.designer.cs
Boundary/FormKarteMessage2.cs
Boundary/FormKarteMessage2.Designer.cs
Boundary/FormKarteTemplate1.cs
Boundary/FormKarteTemplate1.Designer.cs
Boundary/FormKensa.cs
Boundary/FormKensa.designer.cs
Boundary/FormMemo.cs
Boundary/FormMemo.Designer.cs
Boundary/FormNursingOrder.cs
Boundary/FormNursingOrder.designer.cs
Boundary/FormOpeOrder.cs
Boundary/FormOpeOrder.designer.cs
Boundary/FormOrder.cs
Boundary/FormOrder.designer.cs
Boundary/FormOrderKaikei.cs
Boundary/FormOrderKaikei.designer.cs
Boundary/FormPat.cs
Boundary/FormPat.designer.cs
Boundary/FormPatBoard.cs
Boundary/FormPatBoard.designer.cs
Boundary/FormPatContact.cs
Boundary/FormPatContact.Designer.cs
Boundary/FormPath.cs
Boundary/FormPath.designer.cs
Boundary/FormPathTree.cs
Boundary/FormPathTree.designer.cs
Boundary/FormPatList.cs
Boundary/FormPatList.designer.cs
Boundary/FormPatRsv.cs
Boundary/FormPatRsv.designer.cs
Boundary/FormPDFViewer1.cs
Boundary/FormPDFViewer1.designer.cs
Boundary/FormPostIt1.cs
Boundary/FormPostIt1.Designer.cs
Boundary/FormProblem1.cs
Boundary/FormProblem1.Designer.cs
Boundary/FormRsv.cs
Boundary/FormRsv.designer.cs
Boundary/FormRsvDetail.cs
Boundary/FormRsvDetail.Designer.cs
Boundary/FormRsvs.cs
Boundary/FormRsvs.designer.cs
Boundary/FormSchema1.cs
Boundary/FormSchema1.Designer.cs
Boundary/FormSelector.cs
Boundary/FormSelector.designer.cs
Boundary/FormSoap.cs
Boundary/FormSoap.designer.cs
Boundary/FormVital.cs
Boundary/FormVital.designer.cs
Boundary/FormVitalDaily.cs
Boundary/FormVitalDaily.Designer.cs
Boundary/StdControlFont1.cs
Boundary/StdControlFont1.designer.cs
Boundary/CtrlAllergy1.resx
Boundary/CtrlDateBox1.resx
Boundary/CtrlDrugYoho1.resx
Boundary/CtrlInfection1.resx
Boundary/CtrlInHistoryBox2.resx
Boundary/CtrlInjectionYoho1.resx
Boundary/CtrlKarteTemplate1.resx
Boundary/CtrlOrder1.resx
Boundary/CtrlOrderView1.resx
Boundary/CtrlSoapPanel1.resx
Boundary/CtrlSoapWrite1.resx
Boundary/FormAddressGroup.resx
Boundary/FormAnesRecord.resx
Boundary/FormBaseInfo.resx
Boundary/FormBaseOrder.resx
Boundary/FormByotoLayout.resx
Boundary/FormByotoList.resx
Boundary/FormConfirm.resx
Boundary/FormDateSelector.resx
Boundary/FormDeptStat.resx
Boundary/FormDiag.resx
Boundary/FormDiagDPC.resx
Boundary/FormDiagNan.resx
Boundary/FormDiagSearch.resx
Boundary/FormDiagView.resx
Boundary/FormDischargeSummary.resx
Boundary/FormDPCCheck1.resx
Boundary/FormDPCData1.resx
Boundary/FormDPCData2.resx
Boundary/FormDPCList.resx
Boundary/FormDPCList2.resx
Boundary/FormExec.resx
Boundary/FormFindDiag.resx
Boundary/FormFindOpe.resx
Boundary/FormFindPatIn.resx
Boundary/FormFindPatIns.resx
Boundary/FormFindStaff.resx
Boundary/FormInCal2.resx
Boundary/FormInputString1.resx
Boundary/FormKarteMessage1.resx
Boundary/FormKarteMessage2.resx
Boundary/FormKarteTemplate1.resx
Boundary/FormKensa.resx
Boundary/FormMemo.resx
Boundary/FormNursingOrder.resx
Boundary/FormOpeOrder.resx
Boundary/FormOrder.resx
Boundary/FormOrderKaikei.resx
Boundary/FormPat.resx
Boundary/FormPatBoard.resx
Boundary/FormPatContact.resx
Boundary/FormPath.resx
Boundary/FormPathTree.resx
Boundary/FormPatList.resx
Boundary/FormPatRsv.resx
Boundary/FormPDFViewer1.resx
Boundary/FormPostIt1.resx
Boundary/FormProblem1.resx
Boundary/FormRsv.resx
Boundary/FormRsvDetail.resx
Boundary/FormRsvs.resx
Boundary/FormSchema1.resx
Boundary/FormSelector.resx
Boundary/FormSoap.resx
Boundary/FormVital.resx
Boundary/FormVitalDaily.resx
Boundary/StdControlFont1.resx
# Entity/ (.cs 73 / .resx 0)
Entity/AddressGroup.cs
Entity/BaseInfoMaster.cs
Entity/BaseOrderData.cs
Entity/BaseOrderMaster.cs
Entity/BaseOrderPathTemplate.cs
Entity/ByotoBed.cs
Entity/ByotoPat.cs
Entity/ByotoRoom.cs
Entity/Cust1.cs
Entity/Cust2.cs
Entity/Cust3.cs
Entity/Diag.cs
Entity/DiagDPC.cs
Entity/DiagMaster.cs
Entity/DiagNan.cs
Entity/DischargeSummary.cs
Entity/DPCData.cs
Entity/DPCHeader.cs
Entity/DPCICDNotice.cs
Entity/DPCItem1.cs
Entity/DPCItem2.cs
Entity/DPCOpeMaster.cs
Entity/KarteMessage.cs
Entity/KarteTemplate.cs
Entity/KarteTemplateComp.cs
Entity/KarteTemplateCompGroup.cs
Entity/KarteTemplateLink.cs
Entity/KarteTemplateWrite.cs
Entity/Kensa.cs
Entity/Memo.cs
Entity/NursingOrderData.cs
Entity/NursingOrderMaster.cs
Entity/NursingOrderPathTemplate.cs
Entity/OpeOrderData.cs
Entity/OpeOrderMaster.cs
Entity/OpeOrderPathTemplate.cs
Entity/OpeRecord.cs
Entity/OrderDetail.cs
Entity/OrderHeader.cs
Entity/OrderMaster.cs
Entity/OrderSetMaster.cs
Entity/PatBoard.cs
Entity/PatDept.cs
Entity/PathCategoryMaster.cs
Entity/PathMaster.cs
Entity/PathMasterTask.cs
Entity/PathMasterTaskDetail.cs
Entity/PatOrder.cs
Entity/PatOrderDetail.cs
Entity/PatOut.cs
Entity/PatOutKarteStatus.cs
Entity/PatOutKoui.cs
Entity/PdfDoc.cs
Entity/PostIt.cs
Entity/PreBarcode.cs
Entity/PreOrder.cs
Entity/ProblemData.cs
Entity/ProblemMaster.cs
Entity/RsvData.cs
Entity/RsvMaster.cs
Entity/SchemaImage.cs
Entity/Soap.cs
Entity/SoapDetail.cs
Entity/SoapHeader.cs
Entity/SoapOrderHeader.cs
Entity/StdKarte2.cs
Entity/Vital.cs
Entity/VitalMaster.cs
Entity/Voucher.cs
Entity/VoucherComp.cs
Entity/VoucherCompGroup.cs
Entity/VoucherIji.cs
Entity/VoucherLink.cs
# Utility/ (.cs 2 / .resx 0)
Utility/AppColor.cs
Utility/CsvWriter.cs
```
