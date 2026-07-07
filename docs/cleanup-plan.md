# MedicalLibrary 削減計画（2026-07-07）

次回の電子カルテ移行に備え、MedicalLibrary.dll の利用アプリを
**EyeCenter.exe / OpeOrder.exe / NidekARK1.exe / CanonRKF1.exe** の4本に絞り、
それ以外のコード・ファイルを削除する。

## 調査方法と結果

各アプリのソース（EyeCenter / NidekARK1 / CanonRKF1 は兄弟リポジトリに存在）から
MedicalLibrary の参照型を抽出し、ライブラリ内 460 個の .cs ファイルに対して
参照の推移的閉包（到達可能性解析）を実施した。

- **NidekARK1.exe** → `Agent.NidekARK1ListForm` のみ使用
- **CanonRKF1.exe** → `Agent.CanonRKF1Form` のみ使用
- **EyeCenter.exe** → Agent / Boundary / Entity / Utility を広く使用（FormControl 経由でカルテ本体UIを開くため保持範囲が大きい）
- **OpeOrder.exe** → **ソースがこのPCに存在しない**（C:\Shinseikai にも未配置）。
  `FormOpeOrder` / `OpeOrderDayListForm` / `OpeOrderWeekListForm` / `FormOpeNursingList` /
  `OpeOrderExcelPlan` / `OpeOrderExcelWeekPlan` / `OpeOrderSettings` を起点（シード）として推定した。

解析はコメント・文字列内の型名も「参照あり」とみなす保守的（安全側＝残しすぎる方向）なもの。
リフレクション（`Activator.CreateInstance` / `Type.GetType` 等）による動的生成は無いことを確認済み。

**結果: 削除可能 146 ファイル（.cs 109 + .resx 37）/ 保持 351 .cs ファイル**

## 削除対象（モジュール別）

| モジュール | 対象 |
|---|---|
| 服薬指導 | `Agent/DrugAdv*.cs`, `Agent/FormDrugAdv*` |
| FCR（CR装置連携） | `Agent/FCRData.cs`, `Agent/FCRForm.*`, `Agent/FCROrder.cs` |
| MC500（計測器連携） | `Agent/MC500*.cs` |
| MWM（モダリティワークリスト） | `Agent/MWMForm.*` |
| 往診 | `Agent/OushinListForm.*`, `Agent/OushinPat.cs` |
| 患者ラベル | `Agent/PatLabel*.cs` |
| 会計・レセプト | `Agent/FormBillMaker1.*`, `Agent/FormBillPay*.*`, `Agent/ReceBill*.*`, `Agent/ReceMsgForm.*`, `Agent/ReceStatListForm.*`, `Agent/PosRegHistory.cs`, `Boundary/FormVoucher.*` |
| 外来報告 | `Agent/FormComeReport*.*` |
| DPC管理 | `Boundary/FormDPC*.*`, `Entity/DPCICD.cs`, `Entity/DPCMaster.cs`, `Entity/DPCDiag2.cs`, `Entity/DPCOpe*.cs` |
| アラート | `Agent/Alert.cs`, `Agent/FormAlert.*` |
| その他 Agent | `Agent/FormQ26.*`, `Agent/FormInspectData.*`, `Agent/Inspect.cs`, `Agent/FormMedicalSupport.*`, `Agent/PatOrderSettings.cs`, `Agent/PatOrderSheetForm.*` |
| その他 Boundary | `Boundary/FormInCal1.*`, `Boundary/FormLibSettings.*`, `Boundary/FormOpeRecord.*`, `Boundary/FormPreOrderAgree.*`, `Boundary/FormRsvPatList.*`, `Boundary/StdForm2.*`, `Boundary/CtrlPatInsBox1.cs`, `Boundary/CtrlQualBox1.cs`, `Boundary/DataGridViewCheckBoxLabelCell.cs` |
| その他 Entity | `Entity/Byoto.cs`, `Entity/InfectionMaster.cs`, `Entity/Intro.cs`, `Entity/PathTag.cs`, `Entity/PreOrderAgree.cs`, `Entity/RsvNameMaster.cs` |
| その他 | `Utility/Enc.cs`, `Class1.cs` |

### 完全なファイルリスト（109 .cs）

```
Agent/Alert.cs
Agent/DrugAdv.cs
Agent/DrugAdvAgree.cs
Agent/DrugAdvComment.cs
Agent/DrugAdvDischarge.cs
Agent/DrugAdvNode.cs
Agent/DrugAdvSettings.cs
Agent/DrugAdvTemplate.cs
Agent/FCRData.cs
Agent/FCRForm.cs
Agent/FCRForm.designer.cs
Agent/FCROrder.cs
Agent/FormAlert.Designer.cs
Agent/FormAlert.cs
Agent/FormBillMaker1.cs
Agent/FormBillMaker1.designer.cs
Agent/FormBillPayCheckIn.cs
Agent/FormBillPayCheckIn.designer.cs
Agent/FormBillPayList.cs
Agent/FormBillPayList.designer.cs
Agent/FormComeReportAchieve.cs
Agent/FormComeReportAchieve.designer.cs
Agent/FormComeReportList.cs
Agent/FormComeReportList.designer.cs
Agent/FormComeReportOutSide.cs
Agent/FormComeReportOutSide.designer.cs
Agent/FormDrugAdv.cs
Agent/FormDrugAdv.designer.cs
Agent/FormDrugAdvComment.cs
Agent/FormDrugAdvComment.designer.cs
Agent/FormDrugAdvList.cs
Agent/FormDrugAdvList.designer.cs
Agent/FormDrugAdvTemplate.cs
Agent/FormDrugAdvTemplate.designer.cs
Agent/FormInspectData.Designer.cs
Agent/FormInspectData.cs
Agent/FormMedicalSupport.Designer.cs
Agent/FormMedicalSupport.cs
Agent/FormQ26.cs
Agent/FormQ26.designer.cs
Agent/Inspect.cs
Agent/MC500.cs
Agent/MC500DataListForm.cs
Agent/MC500DataListForm.designer.cs
Agent/MC500Form.cs
Agent/MC500Form.designer.cs
Agent/MC500Panel.cs
Agent/MC500Panel.designer.cs
Agent/MC500Panels.cs
Agent/MC500Panels.designer.cs
Agent/MWMForm.cs
Agent/MWMForm.designer.cs
Agent/OushinListForm.cs
Agent/OushinListForm.designer.cs
Agent/OushinPat.cs
Agent/PatLabelForm.cs
Agent/PatLabelForm.designer.cs
Agent/PatLabelLightForm.cs
Agent/PatLabelLightForm.designer.cs
Agent/PatLabelSeqForm.cs
Agent/PatLabelSeqForm.designer.cs
Agent/PatLabelSettings.cs
Agent/PatOrderSettings.cs
Agent/PatOrderSheetForm.cs
Agent/PatOrderSheetForm.designer.cs
Agent/PosRegHistory.cs
Agent/ReceBillForm.cs
Agent/ReceBillForm.designer.cs
Agent/ReceBillSettings.cs
Agent/ReceMsgForm.cs
Agent/ReceMsgForm.designer.cs
Agent/ReceStatListForm.cs
Agent/ReceStatListForm.designer.cs
Boundary/CtrlPatInsBox1.cs
Boundary/CtrlQualBox1.cs
Boundary/DataGridViewCheckBoxLabelCell.cs
Boundary/FormDPCICD.cs
Boundary/FormDPCICD.designer.cs
Boundary/FormDPCIdChange.cs
Boundary/FormDPCIdChange.designer.cs
Boundary/FormDPCManager.cs
Boundary/FormDPCManager.designer.cs
Boundary/FormInCal1.Designer.cs
Boundary/FormInCal1.cs
Boundary/FormLibSettings.Designer.cs
Boundary/FormLibSettings.cs
Boundary/FormOpeRecord.Designer.cs
Boundary/FormOpeRecord.cs
Boundary/FormPreOrderAgree.cs
Boundary/FormPreOrderAgree.designer.cs
Boundary/FormRsvPatList.Designer.cs
Boundary/FormRsvPatList.cs
Boundary/FormVoucher.cs
Boundary/FormVoucher.designer.cs
Boundary/StdForm2.Designer.cs
Boundary/StdForm2.cs
Class1.cs
Entity/Byoto.cs
Entity/DPCDiag2.cs
Entity/DPCICD.cs
Entity/DPCMaster.cs
Entity/DPCOpe.cs
Entity/DPCOpe1.cs
Entity/DPCOpe2.cs
Entity/InfectionMaster.cs
Entity/Intro.cs
Entity/PathTag.cs
Entity/PreOrderAgree.cs
Entity/RsvNameMaster.cs
Utility/Enc.cs
```

### 完全なファイルリスト（37 .resx）

```
Agent/FCRForm.resx
Agent/FormAlert.resx
Agent/FormBillMaker1.resx
Agent/FormBillPayCheckIn.resx
Agent/FormBillPayList.resx
Agent/FormComeReportAchieve.resx
Agent/FormComeReportList.resx
Agent/FormComeReportOutSide.resx
Agent/FormDrugAdv.resx
Agent/FormDrugAdvComment.resx
Agent/FormDrugAdvList.resx
Agent/FormDrugAdvTemplate.resx
Agent/FormInspectData.resx
Agent/FormMedicalSupport.resx
Agent/FormQ26.resx
Agent/MC500DataListForm.resx
Agent/MC500Form.resx
Agent/MC500Panel.resx
Agent/MC500Panels.resx
Agent/MWMForm.resx
Agent/OushinListForm.resx
Agent/PatLabelForm.resx
Agent/PatLabelLightForm.resx
Agent/PatLabelSeqForm.resx
Agent/PatOrderSheetForm.resx
Agent/ReceBillForm.resx
Agent/ReceMsgForm.resx
Agent/ReceStatListForm.resx
Boundary/FormDPCICD.resx
Boundary/FormDPCIdChange.resx
Boundary/FormDPCManager.resx
Boundary/FormInCal1.resx
Boundary/FormLibSettings.resx
Boundary/FormOpeRecord.resx
Boundary/FormPreOrderAgree.resx
Boundary/FormRsvPatList.resx
Boundary/FormVoucher.resx
```

## 実装手順

```
0. git init + 全ファイルコミット
   → 検証: git status がクリーン（このリポジトリは現在 git 管理外。削除前の退避先として必須）
1. OpeOrder.exe の実バイナリを病院環境から入手し、ILSpy 等で
   MedicalLibrary の参照型を列挙 → シード（FormOpeOrder 等7型）と突き合わせ
   → 検証: 参照型がすべて保持リストに含まれること。入手不能なら現シードのまま進み、リリース前に必ず確認
2. MedicalLibrary.csproj から削除対象の <Compile> / <EmbeddedResource> 項目を除去し、
   上記 146 ファイルを削除
   → 検証: msbuild MedicalLibrary.csproj /p:Configuration=Release /p:Platform=x86 が成功
   （エラーが出た場合＝依存の見落とし。該当ファイルを git checkout で戻して再ビルド）
3. 生成 DLL で EyeCenter / NidekARK1 / CanonRKF1 をリビルドし、起動・主要画面を確認
   → 検証: 3アプリのビルド成功＋起動確認（EyeCenter は FormControl からカルテを開くところまで）
4. コミット（🐛/♻️ プレフィックス、日本語）
```

### フェーズ2（任意・別コミット）

ビルドが通った後に検討する追加削減。フェーズ1と混ぜない。

- `Utility/Launcher.cs` 内の不要ランチャメソッド（ProasAgent 等、4アプリ以外を起動するもの）の削除
- 保持ファイル内の未使用メンバー（FormControl のメニュー項目等）の削減
- AnyCPU / IJI（非INNO）ビルド構成の削除 — **要判断**: 4アプリはすべて Shinseikai/INNO 配備であり
  IJI 構成は不要になるはずだが、IJI 側の運用が完全終了しているかの確認が先
- csproj の参照（HintPath で7階層上を見る外部 DLL）のうち不要になったものの整理
- 到達可能性解析の再実行（今回の解析は `Mode` / `Kind` / `Item` / `STATUS` / `Shape` など
  汎用名の誤マッチで残しすぎている可能性があり、2回目でさらに削れる余地がある）

## 注意点

1. **git 管理外** — 現状このリポジトリに VCS がない。一括削除の前に必ず git init + コミット。
2. **OpeOrder.exe のソース不在** — 保持範囲は推定。手順1の実バイナリ検証をリリース条件とする。
3. **同居型** — `FormBillMaker1.cs` 等5ファイルはフォーム内に `Mode` 等の入れ子 enum を持つが、
   これらは他から参照されておらず削除可能と確認済み。逆方向（保持side）の同種誤判定は安全側。
4. **エンコーディング** — 削除自体は影響しないが、csproj や残存ファイルを編集する際は
   Shift-JIS / UTF-8(BOM) 混在の既存エンコーディングを保持すること。
5. **designer / resx はセットで削除** — フォーム本体・designer.cs・resx の3点を揃えて消す。
   csproj の `<EmbeddedResource>` `<DependentUpon>` も対で除去。
6. **設定XML** — 削除対象の Settings クラス（DrugAdvSettings / PatLabelSettings /
   ReceBillSettings / PatOrderSettings）は LibSettings のメンバーではなく、
   XmlSerializer は未知要素を無視するため、既存 Setting.xml が残っていても安全。
7. **INNO シンボル / x86 構成は維持** — 4アプリは Shinseikai 配備（DB.Db3 / @INNO.WORLD）。
8. **ビルド環境依存** — msbuild(VS2022) + 32bit Oracle クライアント + 兄弟フォルダの外部 DLL が前提。
   開発PCは Oracle 21c + bindingRedirect 構成（docs/BUILD_REQUIREMENTS.md 参照）。
