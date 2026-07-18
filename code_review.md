# コードレビュー: 患者マスタ分離取得・DBタイムアウト対応

対象: 未コミットの変更(Agent/EyeKensa.cs, Agent/EyeOpe.cs, Agent/EyeSummary.cs, Entity/PatBase.cs, Utility/DB.cs, Utility/LibSettings.cs, Utility/LibUtility.cs)

観点: 可読性・メンテナンス性・KISS原則

---

## 総評

変更の方向性は良い。DBリンク越しの inner join をやめて眼科DB単独で検索し、患者情報を `PatBase.GetDict` で一括取得する構成は、負荷・ハング対策として妥当で、意図を説明するコメントも付いている。ORA-01795対策、`LibUtility.GetExceptionMessage` の null ガード、`LibSettings` の InnerException 保持も適切な修正。

一方で、**同一ロジックが3ファイルにコピペされている**点が最大のメンテナンス性リスク。以下、優先度順に指摘する。

---

## 優先度: 高

### 1. 患者ID収集→GetDict→除外 のロジックが3箇所に重複

`EyeKensa.cs:220-233` / `EyeOpe.cs:609-622` / `EyeSummary.cs:295-306` に、まったく同じ「PATIENT_ID を重複排除しながら収集して `PatBase.GetDict` を呼ぶ」ブロックがコピペされている。将来この方式を修正するとき(例: 取得列の追加、除外仕様の変更)に3箇所の同期修正が必要になり、修正漏れの温床になる。

`PatBase` に検索結果リストから直接辞書を作るオーバーロードを1つ追加すれば、呼び出し側は1行になる:

```csharp
/// <summary>
/// 検索結果リストの PATIENT_ID 列から患者情報の辞書を取得する。
/// </summary>
public static Dictionary<string, PatBase> GetDict(List<StdClass> tmp_list, DB db = null)
{
    HashSet<string> pt_set = new HashSet<string>();

    foreach (StdClass tmp in tmp_list)
    {
        pt_set.Add(tmp.GetDataString("PATIENT_ID"));
    }

    return GetDict(new List<string>(pt_set), db);
}
```

呼び出し側(3ファイル共通):

```csharp
Dictionary<string, PatBase> pat_dict = PatBase.GetDict(tmp_list, pat_db);
```

### 2. `List` + `HashSet` の二重管理は不要

`EyeKensa.cs:220-231` ほか2箇所で、重複排除のために `HashSet` と `List` を並行して管理しているが、これは過剰。

- ID の順序は `where P_ID in (...)` の検索に影響しない
- そもそも `GetDict` 側が `ContainsKey` で重複を吸収するので、呼び出し側の重複排除自体が二重防御

`HashSet<string>` 1つで足りる(上記1のヘルパー化を行えばこの問題ごと消える)。

### 3. `EyeSummary.GetListByPats` の手動チャンク分割は既存ヘルパーで置き換え可能

`EyeSummary.cs:166-183` は `GetRange` + `Math.Min` で1000件分割を手書きしているが、`AppString.ConcatLists`(max=1000 のチャンク分割付き結合)が既に同じ役割を持ち、`PatBase.GetList`(`PatBase.cs:540`)でも使われている。同じパターンに揃えるべき:

```csharp
// ORA-01795対策: IN句の上限1000件のため分割して取得する
foreach (string pts in AppString.ConcatLists(pt_list, ","))
{
    if (pts.Length == 0) break;

    string cmd = "select * from EYE_SUMMARY where PATIENT_ID in (" + pts + ")";

    foreach (StdClass tmp in StdClass.GetList(DB.Db2, cmd))
    {
        list.Add(GetFromStdClass(tmp));
    }
}
```

手書きループより短く、既存コードと同じイディオムになる。

### 4. `DB.CommandTimeout` の public フィールドは不整合を招く

`DB.cs:23` の `CommandTimeout` が public フィールドのため、利用側が `DB.CommandTimeout = 60;` と直接代入できてしまう。その場合 **既存の Db1/Db2/Db3 には反映されず**、`SetCommandTimeout` 経由の場合と挙動が食い違う。設定経路を一本化すべき:

```csharp
public static int CommandTimeout { get; private set; } = 0;
```

また、静的フィールド初期化は宣言順に走るため、`Db1`〜`Db3`(14-16行)のコンストラクタが `CommandTimeout` の初期化子(23行)より**先に**実行される。現在は default(int)=0 と初期値 0 が偶然一致しているため無害だが、初期値を変えた瞬間に Db1〜Db3 だけ反映されないバグになる。`CommandTimeout` の宣言を `Db1`〜`Db3` より上に移動しておくこと。

### 5. `DB.InitString` はリポジトリ内で未参照(書き込みのみ)

`DB.cs:30` の `InitString` は `Init` で代入されるだけで、ライブラリ内に読み取り箇所がない。外部アプリ(EyeCenter 等)で使う具体的な予定がないなら、投機的実装なので削除すべき(coding-guidelines「依頼されたこと以上の機能を追加しない」)。使う予定があるなら、平文パスワードをメモリに保持する点は既存の `LibSettings` と同等のリスクであり、コメントで用途が明示されているため許容範囲。

---

## 優先度: 中

### 6. `db == null ? DB.Db2 : db` は `??` 演算子で

`EyeKensa.cs:214` / `EyeOpe.cs:607` / `EyeSummary.cs:293` の3箇所:

```csharp
List<StdClass> tmp_list = StdClass.GetList(db ?? DB.Db2, cmd);
```

`PatBase.GetList`(`PatBase.cs:536-539`)の `if (db == null) { db = DB.Db3; }` も同様に `db = db ?? DB.Db3;` にできるが、こちらは既存スタイルとの一貫性を優先するなら現状維持でもよい。

### 7. `ContainsKey` + インデクサの二重ルックアップ

`EyeKensa.cs:243-248` ほか2箇所。辞書を2回引いている:

```csharp
if (!pat_dict.TryGetValue(obj.PtId, out PatBase pat))
{
    continue;
}

obj._Pat = pat;
```

性能への影響は軽微なので、可読性の好みで判断してよい(既存コードベースは `ContainsKey` イディオムが多いため、一貫性を取るなら現状維持も可)。

### 8. `LibSettings.cs:547` は `throw;` が最善

```csharp
throw new Exception(ex.Message, ex);
```

InnerException を保持する意図は正しいが、元の例外をそのまま再スローする `throw;` の方がシンプルで、例外の型・スタックトレースが完全に保たれる。ラップして得られるものがない:

```csharp
catch
{
    throw;
}
```

さらに言えば、catch して `throw;` するだけなら **try-catch 自体を削除**できる(呼び出し元に伝播する動作は同じ)。ログ出力等を挟む予定がないなら削除がKISS。

### 9. `EyeKensa.LoadByKensasDates` の `pat_dict = null` パターン

`EyeKensa.cs:216` で null 初期化し、`if (pat)` ガード下でのみ参照している。現状は正しく動くが、ガードとの対応関係を読者が追う必要がある。指摘1のヘルパー化を行うなら、`pat` が false のとき空辞書を持たせる方が null を排除できて安全:

```csharp
Dictionary<string, PatBase> pat_dict = pat ? PatBase.GetDict(tmp_list, pat_db) : new Dictionary<string, PatBase>();
```

---

## 動作仕様の確認事項(バグではないが認識しておくべき点)

### 10. limit は患者マスタ除外の「前」に適用される

`ROWNUM <= limit` は眼科DB検索結果に対して適用され、その後に患者マスタ不在のIDが除外される。つまり **limit=100 でも返却件数が100件未満になり得る**(従来の inner join なら結合後に件数が決まっていた)。実運用で患者マスタ不在はほぼ無いと思われるが、XMLドキュメントコメントに一言あると親切。

### 11. `EyeSummary.Find` に `order by PATIENT_ID` が追加された(挙動変更)

従来は order by なし。limit を意味のあるものにするため必須の変更で妥当だが、既存呼び出し元の表示順が変わる。呼び出し側(EyeCenter)で問題ないか確認済みであること。

なお `EyeOpe.GetList` は、削除した inner join の M_PATIENT 列を where/order by で使っていなかったため、検索条件としては等価。問題なし。

### 12. オプション引数の追加はバイナリ互換を壊す

`LoadByKensasDates` / `GetList` / `Find` へのオプション引数追加は、C# ではコンパイル時に解決されるため、**旧 DLL に対してビルドされた EyeCenter.exe 等は再ビルドしないと MissingMethodException になる**。3アプリを同時に再ビルド・配布する運用なら問題ない。

---

## 対象外(diff の範囲外だが気づいた点 — 今回は修正しないこと)

- SQL が文字列連結で組み立てられている(日付・検索語とも)。今回の変更もその既存スタイルを踏襲しており一貫性はあるが、コードベース全体の既知の課題として記録。
- `AppString.ConcatLists(List<string>, ...)`(`AppString.cs:203-206`)は `max` 引数を配列版オーバーロードに**転送していない**(常にデフォルト1000になる)。現状すべての呼び出しが1000のため実害はないが、潜在バグ。

---

## 良い点

- 変更方式(DBリンク結合の廃止→ID一括取得)の「なぜ」がコメントに書かれており、将来の読者が意図を追える
- `PatBase.GetDict` はシンプルで責務が明確、ORA-01795対策も `GetList` に集約されている
- `LibUtility.GetExceptionMessage` の `TargetSite`/`StackTrace` null ガードと InnerException の連鎖表示は、型初期化子例外の調査に実際に効く改善
- ROWNUM の適用を `select * from (...)` のサブクエリで order by 後に行っており、Oracle の罠(order by 前の ROWNUM)を正しく回避している

## 推奨対応順

1. 指摘1+2: `PatBase.GetDict(List<StdClass>, DB)` オーバーロード追加 → 3ファイルの重複ブロック削除
2. 指摘3: `GetListByPats` を `AppString.ConcatLists` に置き換え
3. 指摘4: `CommandTimeout` をプロパティ化し宣言位置を移動
4. 指摘5: `InitString` の要否を確認(未使用なら削除)
5. 指摘8: `LibSettings` の try-catch を削除(または `throw;`)
