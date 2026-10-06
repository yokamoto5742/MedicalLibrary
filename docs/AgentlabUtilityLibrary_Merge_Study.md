# AgentlabUtilityLibrary 統合可否 調査結果

作成日: 2026-09-12

`MedicalLibrary` に `AgentlabUtilityLibrary`(以下 AUL)を取り込み、単一DLLで運用できるかを検証した記録。

**目的(当初)**: 電子カルテを別システムへ差し替える際に、編集対象を1つのDLLに集約する。

## 結論

| 問い | 回答 |
|---|---|
| 技術的に統合可能か | **可能**(実際にビルドを通して確認済み) |
| 統合すべきか | **不要**。現状のまま別々で問題ない |
| 当初の目的は達成されているか | **すでに達成済み** |

3アプリ(EyeCenter / NidekARK1 / CanonRKF1)の電子カルテ結合点は **すでに MedicalLibrary.dll の中だけ** に閉じている。
AUL はこの3アプリの経路に一切登場しないため、統合しても「触るDLLが1つ」という状態は改善しない。

## 1. 前提調査

### 1.1 相互参照

| 方向 | 件数 |
|---|---|
| MedicalLibrary → AUL | **0** |
| AUL → MedicalLibrary | **0** |

両者は現在まったく無関係。

### 1.2 AUL の利用者

| アプリ | AUL 参照 | 配布先 |
|---|---|---|
| EyeCenter (EyeData.exe) | なし | `C:\Shinseikai\EyeData` |
| NidekARK1.exe | なし | `C:\Shinseikai\EyeData` |
| CanonRKF1.exe | なし | `C:\Shinseikai\EyeData` |
| **Agree.exe (眼科同意書)** | **あり** | `C:\Shinseikai\EyeAgree` |

`C:\Shinseikai\EyeData\` に `AgentlabUtilityLibrary.dll` は配置されていない。

### 1.3 3アプリの直接DBアクセス

`Oracle.ManagedDataAccess` / `OracleConnection` / `System.Data.OleDb` の直接参照を全ソースで検索した結果:

```
EyeCenter   : 0 件
NidekARK1   : 0 件
CanonRKF1   : 0 件
```

接続文字列・DBリンク・パス・認証はすべて MedicalLibrary 内に集約済み。差し替え時に触る箇所は以下の4ファイル + 1参照。

```
Utility/Env.cs          パス・DBリンク(@INNO.WORLD)
Utility/LibSettings.cs  接続文字列 x3 (Setting.xml)
Utility/DB.cs           Db1/Db2/Db3
Entity/StdEntity.cs     StdEntity.Db (= Db3)
InnoUketsukeLib 参照    認証(M_USR / AppInit)
```

### 1.4 Agree が実際に呼ぶ AUL API

AUL は公開型 86 個を持つが、Agree が使うのは **4型・7メンバーのみ**。

| API | MedicalLibrary の同等物 |
|---|---|
| `Env.LEGACY_HOME` / `AGENT_HOME` / `DB_LINK` | `MedicalLibrary.Utility.Env` に **同名で存在** |
| `Dict.DeptDict` / `Dict.StaffDict` | `MedicalLibrary.Entity.Dict` に **存在** |
| `DBConn.GetOpenDBConn()` | `MedicalLibrary.Utility.DB.Db2` (OPEN DB) |
| `Barcode128.CODE` | なし(EyeCenter に別コピーが存在) |

### 1.5 規模と性質の比較

| 項目 | MedicalLibrary | AgentlabUtilityLibrary |
|---|---|---|
| ソース | 316ファイル | 62ファイル / 9,936行 |
| 公開型 | 349 | 86 |
| 名前空間 | `MedicalLibrary.{Agent,Boundary,Entity,Utility}` | `AgentlabUtilityLibrary` (フラット) |
| 名前空間の記法 | ブロック形式 | **file-scoped (C#10)** × 61ファイル |
| DBアクセス | `Oracle.ManagedDataAccess` | `System.Data.OleDb` + `Provider=MSDAORA.1` |
| 接続先DB | macs / open / medb(inno) | macs / open (**同一DB**) |
| 文字コード | CP932 と UTF-8(BOM有) の混在 | ASCII と UTF-8(BOM無) の混在 |
| コードの性質 | 日本語コメント・XMLドキュメント有り | **逆コンパイル由来**(コメント無し、`text2`/`flag2` 等の機械生成変数名) |
| ターゲット | .NET Framework 4.8 / x86 / 非SDK csproj | 同左 |

同名の公開型が **34件** 存在する(`DB` `Env` `Dict` `Launcher` `WinAPI` `PatIn` `PatOut` `PatIns` `PatOrder` `LoginUser` `TableData` 他)。ただし名前空間が異なるため衝突しない。

## 2. ビルド検証(実測)

`MedicalLibrary.csproj` に AUL の全ソースを取り込んだ実験用 csproj を作成し、MSBuild でビルドした。

```
msbuild _merge_test.csproj /p:Configuration=Release /p:Platform=x86
```

| ビルド | 結果 | 出力サイズ |
|---|---|---|
| ベース(現状の MedicalLibrary) | 成功 | 2,494,976 bytes |
| 統合版 | **成功(エラー0件、警告のみ)** | 2,674,688 bytes |

### 2.1 統合に必要だった変更(3点)

| # | 変更 | 理由 |
|---|---|---|
| 1 | `<LangVersion>10.0</LangVersion>` を追加 | AUL の61ファイルが file-scoped namespace(C#10)。MedicalLibrary は既定の C#7.3 |
| 2 | `<Reference Include="System.Net.Http" />` を追加 | AUL の csproj にあった参照 |
| 3 | **.resx 7件に `<LogicalName>` を明示** | 後述(最重要) |

### 2.2 最重要の落とし穴: .resx のリソース名

MedicalLibrary の `RootNamespace` は `MedicalLibrary` のため、AUL の resx をそのまま取り込むとマニフェストリソース名が

```
MedicalLibrary.AgentlabUtilityLibrary.FormDate.resources
```

になる。一方フォームの型は `AgentlabUtilityLibrary.FormDate` なので、`ComponentResourceManager` が探す名前と一致しない。

**結果: ビルドは成功するのに、実行時に `MissingManifestResourceException` で落ちる。**

対象は7ファイル(`FormDate` `InfoShareForm` `LoginChange` `LoginPrompt` `PatFindForm` `SendMessageForm` `StringForm`)。
各 `EmbeddedResource` に以下を付与することで解決することを確認した。

```xml
<EmbeddedResource Include="External\Agentlab\FormDate.resx">
  <LogicalName>AgentlabUtilityLibrary.FormDate.resources</LogicalName>
</EmbeddedResource>
```

生成された DLL のリソース名を確認:

```
AgentlabUtilityLibrary.FormDate.resources        <- 正しい
AgentlabUtilityLibrary.LoginPrompt.resources
AgentlabUtilityLibrary.PatFindForm.resources
MedicalLibrary.Boundary.LoginPrompt.resources    <- 既存と共存
```

### 2.3 問題にならなかった項目

| 懸念 | 検証結果 |
|---|---|
| 同名型34件の衝突 | 名前空間が異なるためエラーなし |
| 文字コード混在による文字化け | 問題なし。AUL は UTF-8(BOM無)として、MedicalLibrary の CP932 ファイルは既定コードページとして、同一コンパイル内で正しく解釈される。生成DLL内の日本語リテラル(`検索` `保険`)が正しい UTF-16 で格納されていることを確認 |
| `InnoUketsukeLib.dll` のバージョン差異 | AUL 同梱版(99,840 bytes)と MedicalLibrary 参照版(98,304 bytes)は別物だが、**新しい参照版で AUL の `LoginPrompt.cs` もコンパイルが通る**(使用 API は `AppInit.g_AppInit.Init()` と `M_USR.g_Usr1.GetData()` のみ) |
| C#10 固有機能への依存 | file-scoped namespace のみ。`record` / `init` / パターン強化などは不使用 |

> 検証用 csproj は削除済み。リポジトリに変更は残していない。

## 3. 統合しない判断の根拠

### 3.1 目的が改善されない

統合すると単一DLL内に **OleDb と ODP.NET の2世代のDBアクセス層が同居** する。
DLLファイル数は1つになるが、電子カルテ結合点は `Utility/DB.cs` と `AgentlabUtilityLibrary/DB.cs` + `DBConn.cs` の2系統のまま残り、差し替え時に触る箇所は減らない。

### 3.2 3アプリに不要コードが同梱される

EyeCenter / NidekARK1 / CanonRKF1 が一切使わない 9,936行(86型中81型が未使用)が `MedicalLibrary.dll` に常時含まれることになる。
`docs/cleanup-plan.md` の3アプリ専用化方針(460→316ファイルに削減)と衝突する。

### 3.3 統合先がすでに存在する

Agree が必要とする4型のうち `Env` と `Dict` は MedicalLibrary に同等物が揃っており、移植自体が不要。

## 4. 残っている重複(参考)

MedicalLibrary の運用には影響しないが、記録として残す。

| 対象 | 状況 |
|---|---|
| `Barcode128` | AUL と EyeCenter に同内容のコピーが2つ存在(MedicalLibrary には無し) |
| マスタ読込(`DeptDict` / `StaffDict` 相当) | MedicalLibrary と AUL に2つ存在。`Agree/docs/agree_oracle_client_removal_plan.md` の A案(Agree 内に自前実装)が完了すると **3つ目** が生まれる |

Agree 側を整理する場合は、AUL を廃止して MedicalLibrary 参照に寄せる選択肢がある(Agree 側の判断事項)。

## 5. 今回の対応

**MedicalLibrary に対する変更は無し。** 現状の構成を維持する。
