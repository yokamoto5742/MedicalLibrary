# MedicalLibrary コードレビュー（2026-09-24）

## 0. レビューの前提

| 項目 | 内容 |
|---|---|
| 対象 | `master`（`6bd008c`）の `.cs` 58ファイル（designer を除く）、約18,600行 |
| 観点 | 可読性・メンテナンス性、KISS（できるだけ単純に） |
| 行番号 | すべて `master` の行番号 |
| 範囲外 | ビルド・実行による確認はしていない（DB接続環境がないため）。指摘はすべてコードを読んで判断したもの |

**フェーズ5ブランチとの関係**

`refactor/dead-code-removal`（フェーズ5のメンバー単位削除、約6,000行）は **`master` にマージされていません**。
その後 `master` には InnoUketsukeLib 統合などのコミットが5件入っており、競合するのは `Staff.cs` / `LibSettings.cs` / `CanonRKF1Form.cs` / `AssemblyInfo.cs` / `MedicalLibrary.csproj` の5ファイルです。

各指摘には次のどちらかを付けています。

- **【残】** フェーズ5をマージした後も残る（こちらが本題）
- **【P5】** フェーズ5のマージで対象コードごと消える

**public API を変えるときの注意**

MedicalLibrary.dll は EyeCenter / NidekARK1 / CanonRKF1 から参照されています。public メソッドの引数や戻り値の型を変えると、再ビルドしていない EXE では `MissingMethodException` になります。シグネチャを変える修正は、**3アプリを再ビルドして再配備するまでをひとまとまりとして**扱ってください。以下の修正案は、できるだけシグネチャを変えない形にしています。

---

## 1. 総評

- **重複コードが最大の問題です。** 左右の眼ごとの処理（`NidekARK1.Convert`、`EyeRefKrt.Read`）、DBパラメータの登録（6か所）、入院履歴のSQL（6メソッド）、和暦の変換（4か所）などで、ほぼ同じコードが並んでいます。直すときに片方だけ直してしまう事故が起きやすい構造です（実際に `PatBase.cs:650` で起きています。§2-5）。
- **DB層に例外への備えがありません。** 共有の `OracleCommand` を使っているのに `try/finally` がないため、SQLが1回失敗すると、そのあとの同じ接続でのSQLまで失敗が続きます（§3-1）。
- **ユーザーが入力した文字列をそのままSQLに入れている箇所が4つあります。** 患者検索画面で `'` を入力するだけで ORA エラーになります（§4-1）。
- コメントアウトされたコードが12ファイルに19ブロック残っています。git に履歴があるので削除して問題ありません。

一方で、`StdClass.GetList` の LOB の扱い（`GetOracleValue` を1回だけ呼んで Dispose する）や、DBリンクを越えた結合をやめたことへのコメントは、理由がきちんと書かれていてよくできています。

---

## 2. 不具合（修正を推奨）

優先度: 🔴 実運用で起こりうる / 🟡 条件がそろうと起こる / 🟢 軽微

### 2-1 🔴【残】`LoginUser.SetUser` が前のユーザーの情報を残す
`Entity/LoginUser.cs:489-536`、呼び出し元 `Boundary/LoginChange.cs:33`

`SetUser` は `id` を先に書き換えますが、`name` / `dept_id` / `doctor_id` / `id2` / `name2` はクリアしません。

- `LoginChange` でユーザーを切り替えたとき、新しいユーザーの科コード・医師コードが 0 または NULL だと、**前のユーザーの科・医師が残ります**。
- 新しいIDが `M_USR` に存在しないと、**IDは新しいのに氏名は前のユーザーのまま**になります。

```csharp
// SetUser の先頭（int.TryParse の直後）
Clear();
id = _id;
```

### 2-2 🔴【残】`Env` のセッターで設定した値が、別のゲッターを呼ぶと初期値に戻る
`Utility/Env.cs:115-124`

`init()` は**6つのフィールドをすべて**初期値で上書きします。そのため、たとえば `Env.DB_LINK = "@X"` と設定しても、そのあと値が空の別のプロパティ（`SHIN_HOME` など。`AppFile.FilePath` が最初に読みます）を読んだ時点で `DB_LINK` が `@INNO.WORLD` に戻ります。`DB_LINK` のセッターのコメントには「アプリによっては初期設定時に変更する」と書かれており、その用途が成り立っていません。

KISS で直すなら、遅延初期化をやめて初期値を直接持たせます（約110行 → 10行）。

```csharp
public static class Env
{
    public static string LEGACY_HOME { get; } = @"C:\macs";
    public static string AGENT_HOME { get; } = @"C:\macs\utility";
    public static string KARTE_HOME { get; set; } = @"c:\karte";
    public static string INNO_HOME { get; set; } = @"c:\innokarte";
    public static string SHIN_HOME { get; set; } = @"c:\shinseikai";

    /// <summary>アプリによっては初期設定時に変更する場合がある</summary>
    public static string DB_LINK { get; set; } = "@INNO.WORLD";
}
```

### 2-3 🔴【残】利用者が入力した文字列に `'` があると SQL エラー（§4-1 と同じ原因）
`Entity/PatBase.cs:626-652`（患者検索 `FormFindPat`）、`Agent/EyeOpe.cs:533-601`、`Agent/EyeSummary.cs:247-290`

患者名に `'` を入れると ORA-01756 で検索が失敗します。修正案は §4-1 にまとめました。

### 2-4 🟡【残】手術検索で区切りの空白が2つ続くと、全件がヒットする
`Agent/EyeOpe.cs:553-567`（術式）、`573-587`（医師）

`ope.Split(' ', '　')` は空の要素を残します。`"白内障  硝子体"` のように空白が2つ続くと `OPE_NAME like '%%'` が **OR** でつながるため、条件が効かずに全件ヒットします（`and` でつないでいる `diag` は結果に影響しません）。

```csharp
foreach (string s in ope.Split(new[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries))
```

### 2-5 🟡【残】カナ検索で、全角空白の判定に `name` を見ている
`Entity/PatBase.cs:650`

```csharp
else if (name.Contains("　"))   // ← kana.Contains("　") が正しい
```
コピーしたあとの直し忘れです。カナに全角空白を入れても、半角空白に置き換えた条件が追加されません。

### 2-6 🟡【残】`BaseInfo` で `Name1` に2回代入していて、`Name2` が設定されない
`Entity/BaseInfo.cs:80-81`、`151-152`

```csharp
obj.Name1 = tmp.DataDict["ITEM_TITLE1"].ToString();
obj.Name1 = tmp.DataDict["ITEM_TITLE2"].ToString();   // ← obj.Name2
```

### 2-7 🟡【残】`NidekARK1.FileSave` で、存在を確認するフォルダと作成するフォルダが違う
`Agent/NidekARK1.cs:501-505`

存在を確認しているのは固定の `c:\transfile\data` ですが、作成するのは `Settings.TargetFile` のフォルダです（正しい行がコメントアウトされています）。`NidekARK1.xml` で出力先を別のドライブにしていて、そのフォルダがない場合は、`StreamWriter` で例外になります。

```csharp
string dir = Path.GetDirectoryName(Settings.TargetFile);
Directory.CreateDirectory(dir);   // 既にあれば何もしない
File.WriteAllText(Settings.TargetFile, s, Encoding.Default);
```

### 2-8 🟡【残】`NidekARK1` のファイル名が短いと一覧表示で落ちる
`Agent/NidekARK1.cs:40-43`、`59-62`

`f.Substring(f.Length - 18, 8)` は、ファイル名が18文字未満だと `ArgumentOutOfRangeException` になります。フォルダに `a.xml` のようなファイルが1つあるだけで、`GetList` の並べ替えの途中で落ちます。`if (f.Length >= 18)` を条件に加えてください。

### 2-9 🟡【残】`AppString.ConcatLists` が空の要素で `,,` を作り、`max` 引数を無視している
`Utility/AppString.cs:203-244`

- 区切り文字を足してから要素が空かどうかを見ているため、`["1","","2"]` は `"1,,2"` になり、`IN (1,,2)` で ORA-00936 になります。`PatBase.GetDict(List<StdClass>)` は `PATIENT_ID` が NULL の行から `""` を拾うので、実際に起こりえます。
- `max` 引数は使われておらず、1000 が直書きされています。List 版は `max` を渡してもいません。

```csharp
public static List<string> ConcatLists(List<string> list, string delimiter, string quote = "", int max = 1000)
{
    List<string> items = list.Where(x => x.Length > 0).Select(x => quote + x + quote).ToList();
    List<string> lists = new List<string>();

    for (int i = 0; i < items.Count; i += max)
    {
        lists.Add(string.Join(delimiter, items.Skip(i).Take(max)));
    }

    return lists;
}
```

### 2-10 🟡【残】`TableData(DataGridView)` が空のセルで NullReferenceException
`Utility/TableData.cs:61`

`cell.Value.ToString()` は、値が `null` のセル（未入力のセルや、`AllowUserToAddRows` の新規行）で落ちます。`Convert.ToString(cell.Value)` を使い、`row.IsNewRow` の行は飛ばしてください。

### 2-11 🟡【残】`CanonRKF1Form` で受信データが空のとき、例外メッセージがデータとして保存される
`Agent/CanonRKF1Form.cs:131-155`

`ReadExisting()` が空文字を返すと `rsv[rsv.Length - 1]` で例外になります。catch 側で `ex.Message` を返して `status = Close` にするため、例外メッセージが受信データとして `RsvBox` に追加され、そのまま `ref.dat` に保存されます。

```csharp
string rsv = port.ReadExisting();

if (rsv.Length == 0)
{
    return "";
}
```

### 2-12 🟡【残】`DateTimeAgent.DateFormat` が8桁の不正な日付で例外を出す
`Utility/DateTimeAgent.cs:113-244`

長さが8かどうかしか確認しておらず、そのあと `DateTime.Parse` を呼んでいるため、`"00000000"` のような値で `FormatException` になります。生年月日の表示（`BirthStringJ`、患者検索の一覧）からも呼ばれています。§5-4 の書き換えで、あわせて直せます。

### 2-13 🟢【残】`EyeOpeRsv.Save()` の `catch { throw ex; }`
`Agent/EyeOpeRsv.cs:25-32`

スタックトレースが失われるだけで、何の役にも立っていません。try/catch ごと削除してください。

### 2-14 🟢【残】`EyeLensMeter.ReadData` の null チェックが意味をなしていない
`Agent/EyeLensMeter.cs:39-53`

`line` は `""` で初期化されているので `line != null` は常に true です。ファイルが空だと `s[1]` で落ちます。`if (s.Length < 20) return;` のように要素数で判定してください。

### 2-15 🟢【残】`EyeKensaItemMaster.ListByKensaId` が存在しない検査IDで落ちる
`Agent/EyeKensaItemMaster.cs:98`

`Select(...)[0]` は、該当する行がないと `IndexOutOfRangeException` になります。

### 2-16 🟢【残】`LibSettings.Read` が設定を読むたびにフォルダ内のファイルを削除する
`Utility/LibSettings.cs:446-457`

SOAP 画像のテンポラリフォルダ（`c:\shinseikai\MedicalLibrary\soapimg`）の中身を、`Init` のたびにすべて削除しています。SOAP 機能はフェーズ4で削除済みなので、この処理は不要です。設定を読む処理が、ファイル削除という副作用も持っている状態になっています。

### 2-17 🟢【残】`StdControlPat1` の患者IDを `double.TryParse` で判定している
`Boundary/StdControlPat1.cs:194-196`

`"1e3"` や `"1.5"` が数字として通ってしまいます。`long.TryParse` を使ってください。

### 2-18 🟢【P5】`StdDbClass.SelectSQL` の句の順序が逆
`Entity/StdClass.cs:465` — `OrderByState + GroupByState` の順で連結しているため、両方を指定すると不正なSQLになります。フェーズ5でメソッドごと削除されます。

### 2-19 🟢【P5】`PatIn.GetHistory` のコメントが逆
`Entity/PatIn.cs:565-574` — `PROCESS=19`（退院確定）の分岐に「現在の入院」、そうでない分岐に「過去の入院」と書かれています。フェーズ5で削除されます。

---

## 3. DB層の構造の問題

### 3-1 🔴【残】`try/finally` がないため、1回の失敗がそのあとのSQLにも波及する
`Entity/StdClass.cs:15-104`（`GetList`）、`UpdateSQL` / `InsertSQL`、`Utility/DB.cs:182-233`

`DB` は `OracleCommand` を1つだけ持ち、`Close()` の中で `Parameters.Clear()` を実行します。途中で例外が起きると `Close()` が呼ばれないため、

- 前のSQLのパラメータが残り、**同じ `DB` を使う次のSQLが ORA-01036 / ORA-01008 で失敗し続けます**
- `OracleDataReader` が閉じられません

```csharp
public static List<StdClass> GetList(DB db, string sql_command, List<StdDbColumn> param_list = null, Action<string> progress = null)
{
    List<StdClass> list = new List<StdClass>();

    db.Open();

    try
    {
        db.Command.CommandText = sql_command;
        // パラメータ登録（§3-2 の AddParameters）
        using (OracleDataReader reader = db.Command.ExecuteReader())
        {
            // 既存の読み取り処理
        }
    }
    finally
    {
        db.Close();
    }

    return list;
}
```

### 3-2 🔴【残】パラメータを登録するコードが6か所にある
`StdClass.cs:25-54`、`573-617`、`619-647`、`699-746`、`748-776`、`DB.cs:188-219`（フェーズ5後も6か所。`SelectSQL` / `DeleteSQL` の分はフェーズ5で消えます）

どれも20〜30行ある同じ if-else の連なりです。さらに、NUMBER の分岐の中にある `obj.Value != null && ... Length > 0` の判定は、外側で既に同じ判定をしているため（`mode == 0` のとき以外は）意味がありません。また `obj.Value` が null だと、外側の `ToString()` で落ちます。

```csharp
/// <summary>
/// パラメータリストを OracleCommand に登録する。
/// </summary>
/// <param name="skip_empty">true: 値が空のパラメータは登録しない</param>
internal static void AddParameters(OracleCommand command, List<StdDbColumn> param_list, bool skip_empty)
{
    foreach (StdDbColumn obj in param_list)
    {
        bool empty = obj.Value == null || obj.Value == DBNull.Value || obj.Value.ToString().Length == 0;

        if (skip_empty && empty) continue;

        switch (obj.DataType)
        {
            case StdDbType.NUMBER:
                command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = empty ? DBNull.Value : obj.Value;
                break;
            case StdDbType.VARCHAR2:
                command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                break;
            case StdDbType.CHAR:
                command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                break;
            case StdDbType.DATE:
                command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                break;
        }
    }
}
```
呼び出し側は `AddParameters(Db.Command, this.ParamList, true)` / `AddParameters(Db.Command, param_list, mode == 1)` のように書けます。**約150行減ります。**
（TEXT 型の列は DataList のループの中で SQL 文字列に直接埋め込んでいるので、この関数の対象外です）

### 3-3 🟡【残】`Open()` を入れ子で呼ぶと接続が閉じなくなる
`Utility/DB.cs:74-99`、`Entity/Dict.cs:304` / `447`

`ToClose` は bool が1つあるだけです。`Dict.InitDict` は外側で `db.Open()` を呼び、その内側の `StdClass.GetList` がもう一度 `Open()` を呼びます。このとき接続は既に開いているので `ToClose = false` に上書きされ、**最後の `db.Close()` で接続が閉じません**。その後も `Open()` は「開いているので何もしない」を繰り返すため、`Db3` の接続はアプリを終了するまで開いたままになります。

KISS で直すなら、`Dict.InitDict` の外側の `db.Open()` / `db.Close()` を削除します（ODP.NET はコネクションプールを持っているので、クエリのたびに開いて閉じてもコストは小さいです）。

### 3-4 🟢【残】`StdDbClass` の既定の接続が、初期化されない `Db1` になっている
`Entity/StdClass.cs:247`、`Utility/LibSettings.cs:501-502`

`LibSettings.Init` が初期化するのは `Db2` / `Db3` だけです。今の呼び出し元はすべて `obj.Db = DB.Db2` を明示しているので問題は起きていませんが、書き忘れると接続文字列が空のまま実行されます。既定値を `DB.Db2` にするか、`StdDbClass(DB db)` コンストラクタだけを使うようにしてください。

### 3-5 🟢【残】`mode` の 0/1 という値の意味がわかりにくい
`UpdateSQL(bool execute, int mode, bool close)` の `mode` は、実際には「空の値をスキップするか」という bool です。呼び出し元はすべて既定値のままなので、シグネチャを変えずに XML コメントで意味を補うだけで十分です。

---

## 4. SQL の組み立てとセキュリティ

### 4-1 🔴【残】自由入力の文字列をそのまま連結している
| 箇所 | 入力元 |
|---|---|
| `Entity/PatBase.cs:626-652` `GetListByNameKanaBirth` | 患者検索画面の氏名・カナ |
| `Agent/EyeOpe.cs:533-601` `GetList` | 病名・術式・医師・記録 |
| `Agent/EyeSummary.cs:247-290` `Find` | 病名・分類1〜3 |
| `Agent/EyeOpeRsv.cs:70`、`203` | 枠（`Load` だけはバインド変数を使っていて、書き方が揃っていない） |

`StdClass.GetList` は既に `param_list` を受け取れるので、LIKE 検索もバインド変数にできます。

```csharp
conds.Add("P_NAME like :NAME");
param_list.Add(new StdDbColumn("NAME", StdDbType.VARCHAR2, "%" + name + "%"));
```
患者ID・日付・コードのように数値として検証済みの値は、連結のままでも実害はありません。ただ `AppString.IsNumber`（【P5】）は正規表現が `^...$` で固定されておらず、`"1 or 1=1"` も通してしまうので、検証の役目を果たしていません。

### 4-2 🟢【残】ソースに直接書かれた認証情報
- `Utility/LibSettings.cs:25-34` — 接続文字列の既定値（`Password=system`）。設定ファイルがないときの既定値として残す理由がなければ、空文字にしてください。
- `Entity/Staff.cs:399-400` — AES の鍵と IV。旧 InnoUketsukeLib から移植したものなので、変更すると既存のパスワードを復号できなくなります。**現状のままで仕方ない**ものとして記録だけしておきます。
- 【P5】`Launcher.Dicom` の `USER=test&PASSWORD=test`、`LoginUser.IsDPC` のコメントにある実在の職員名は、フェーズ5で削除されます。

---

## 5. 重複の解消（KISS）

効果の大きいものから並べています。

### 5-1【残】`NidekARK1.Convert` の右眼と左眼（約140行 × 2）
`Agent/NidekARK1.cs:177-319` と `321-461` は、`"Data/R/"` と `"Data/L/"` が違うだけの完全なコピーです。

```csharp
public static string Convert(string file)
{
    // ヘッダ部分は既存のまま
    s += "<R>\r\n" + ConvertEye(doc, "R") + "\r\n\r\n";
    s += "<L>\r\n" + ConvertEye(doc, "L");
    // PD 部分は既存のまま
}

static string ConvertEye(XmlDocument doc, string eye)
{
    string root = "Data/" + eye + "/";
    // 既存の右眼ブロックの "Data/R/" を root に置き換えたもの
}
```
あわせて次も整理できます。
- `doc.SelectNodes(...) != null` は常に true なので不要です。
- 空の `finally { }` は削除できます。
- `xe.SelectSingleNode("X").InnerText.PadRight(n)` が約40回出てくるので、`static string Text(XmlNode n, string name, int pad)` にまとめます。

**約150行減ります。**

### 5-2【残】`EyeRefKrt.Read` の右眼と左眼、角膜屈折力の平均（DAVE）の計算4か所
`Agent/EyeRefKrt.cs:61-153` と `156-249`

左眼のインデックスは右眼に **+45** しただけです（7→52、37→82、39→84、49→94、50→95、51→96）。DAVE の計算（`f*2 - Math.Floor(f*2)` から続く部分）は4か所にコピーされています。

```csharp
static void ReadEye(string[] data, int offset, EyeRefKrtElement e) { ... }   // 右眼は offset=0、左眼は offset=45
static string CalcDAve(string d1, string d2) { ... }
```
**約120行減ります。** あわせて `StreamReader` を `using` にし、`ReadLine()` が null（空ファイル）の場合の判定も入れてください。

### 5-3【残】`PatIn` の `GetDeptList` / `GetDoctorList` / `GetRoomList`（3種類 × オーバーロード2つ）
`Entity/PatIn.cs:1040-1467`

- **日付を指定する版の3つ**は、`PROCESS in (...)`、NULL を除外する条件、列の3点しか違いません。
- **履歴版の3つ**は、`pat_info` の if/else で、SQL の違いが `M_PATIENT` との結合の有無だけです。

```csharp
// 履歴版の共通部分
string pat_cols = pat_info ? ", tm.P_KANA, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " : "";
string pat_from = pat_info ? ", M_PATIENT tm" : "";
string pat_where = pat_info ? " and td.P_ID = tm.P_ID " : "";
```
これを使えば if/else が消えて、3メソッドとも共通の private メソッドを呼ぶだけになります。**約250行減ります。** `seq_list` から `"(id,seq)"` を作る `ConvertAll` も6か所にあるので、あわせて1つにまとめます。

### 5-4【残】`DateTimeAgent.DateFormat`
`Utility/DateTimeAgent.cs:113-244`

- `DateTime.Parse(org_date.Insert(4, "/").Insert(7, "/"))` が13回出てきます → 先頭で `TryParseExact(org_date, "yyyyMMdd", ...)` を1回だけ呼びます（§2-12 もこれで直ります）。
- 令和の回避策が4か所、元号をアルファベットにする置換が2か所にあります → `static string Era(DateTime dt, bool alphabet, out int year)` にまとめます。
- `JtoW` の2つのオーバーロードで元号の基準年を二重に持っています → `static readonly int[] era_base = { 0, 1867, 1911, 1925, 1988, 2018 };` にまとめます。

**約100行減ります。** 変換の結果が変わらないことを確かめられるよう、先に EyeCenter.Tests へ `DateFormat` の全種類の期待値テストを追加することを勧めます（純粋な関数なので、DB がなくてもテストできます）。

### 5-5【残】`LibSettings.Read`（約20個のブロックが同じ形）
`Utility/LibSettings.cs:233-316`、`334-443`

```csharp
static string ReadText(XmlNode node, string xpath, string default_value)
{
    XmlNode n = node.SelectSingleNode(xpath);
    return n != null ? n.InnerText : default_value;
}

Current.DBConnectionString1 = ReadText(xmlDoc, "LibSettings/DBConnectionString1", Current.DBConnectionString1);
```
**約120行減ります。** さらに、フェーズ4で削除した機能の設定項目（`OrderXmlExe`、`ReceExe`、`ReceApiExe`、`OrderReceApiInterval`、`SchemaFolder`、`SoapImage*`、`Pdf*`、`Anes*`、`Bill*` / `InvoicePdfFolder`、`DPC*`、`Proas`、`LogFolderPath` / `LogServerFolderPath`）は、3アプリから参照されていないことを確認したうえで削除できます。XML に残っているキーは無視されるだけなので、設定ファイルを直す必要はありません。

`BaseInfoCodes` の6つのプロパティ（`LibSettings.cs:527-676`）は、既にある `CodeByName` を使えば1行ずつになります。

```csharp
public string Diag { get { return CodeByName("Diag"); } }
```

### 5-6【残】「更新して0件なら追加する」という保存処理が7クラスにある
`EyeKensa` / `EyeKensa2` / `EyeOpeDoctor` / `EyeOpePass` / `EyeOpeRecord` / `EyeOpeRsv` / `EyeSummary` の `Save()`

どのクラスも `StdReturn sr = new StdReturn();` と書いた直後に上書きしていて、戻り値が `void` なので **`sr.Errs` は誰にも見られていません**。`StdDbClass` に次のメソッドを1つ追加すると、各 `Save()` はそれぞれ数行短くなります。

```csharp
/// <summary>
/// key_list を条件に Update し、対象が無ければ key_list を加えて Insert する。
/// </summary>
public StdReturn UpdateOrInsert(List<StdDbColumn> key_list)
{
    foreach (StdDbColumn key in key_list)
    {
        this.WhereList.Add(key.Name + " = " + key.Value);
    }

    StdReturn sr = this.UpdateSQL();

    if (sr.ErrExist || sr.IntValue > 0)
    {
        return sr;
    }

    this.DataList.AddRange(key_list);
    return this.InsertSQL();
}
```
（`EyeOpeRsv` の `OPE_WAKU` は文字列のキーで引用符が必要なので、先に数値キーの6クラスに適用してください）

### 5-7【残】その他の小さな重複
| 箇所 | 内容 | 修正案 |
|---|---|---|
| `Agent/EyeDict.cs:97-170` | `OpeTime` の行を探す処理が3か所 | `static DataRow FindOpeTime(string ope_kind, string ope_date)` |
| `Agent/EyeDict.cs:185-188` | `CalcGrape` が `CalcVisdine` と同じ式を持っている | `return 30.0 - CalcVisdine(height, weight);` |
| `Agent/EyeDoc.cs:159-190`、`193-246` | 患者情報3項目・アレルギー3群の処理がコピー | `(名前, コード)` の配列でループする／`GroupCode` をキーにした辞書 |
| `Agent/EyeDoc.cs:357` | `FileName.Split('\\')[...Length - 1]` | `Path.GetFileName(FileName)` |
| `Agent/EyeKensa.cs:148-161` | `if (pat)` と `else` で SQL がまったく同じ | if/else を削除 |
| `Agent/EyeKensaItemMaster.cs:32-50`、`100-117` | `DataRow` からの変換が2か所（【P5】で3→2か所） | `static EyeKensaItemMaster FromRow(DataRow r)` |
| `Agent/EyeOpe.cs:408-424` | 冒頭で `IsDate` を確認済みなので、`else` 側に来ることがない | 分岐を削除 |
| `Agent/EyeOpe.cs:330-451` | `GetListByKindDateTimes` と `GetListByKindDates` の後半が同じ | 共通の private メソッドにまとめる |
| `Entity/PatIn.cs:32-247` | 入院・退院・適用日ごとに Int/String/Short/Value と時間帯の名前（朝/昼/夕/未定）が3組 | `static string ZoneName(string code)` と日付変換の共通関数 |
| `Entity/Dict.cs:12-218` | 遅延読み込みのゲッターが10個 | `static bool loaded` と `EnsureLoaded()` にまとめる |
| `Entity/Staff.cs:376-460` | `Verify` が `M_USR` を2回検索している | パスワードと属性を1回のSQLでまとめて取得する |
| `Entity/PatBase.cs:732-735`、`Entity/LoginUser.cs:416-419` | `line.Split(',')` を50回呼んでいて、列が50未満だと落ちる。`StreamReader` が using になっていない | `string[] cols = line.Split(',');` を1回だけにして、`using` と列数の確認を入れる |
| `Utility/InnoProgram.cs:44-58`、`80-94` | ウィンドウをタイトルで探すループが2回ある。`FindFirst` が null のときに NRE | `static IntPtr FindWindowByTitle(params string[] titles)` |
| `Boundary/StdControlPat1.cs:34-84` | 3つのモードで名前・カナの Size/Location が同じ | 共通部分を if の外に出す |
| `Boundary/StdControlPat1.cs:145-150` | 表示をクリアした直後に全項目へ代入し直している | クリアを削除 |
| `Boundary/FormFindPat.cs:149-157` | 女性の行を赤くする処理が `AppDataGridView.SexColor` と重複 | `SexColor` を呼ぶ |
| `Agent/NidekARK1Settings.cs:32-70` | `Init()` が常に false を返す | `void` にする（public なので、3アプリの再ビルドとあわせて） |
| `Entity/StdClass.cs:1042-1110` | `Err` / `Msg` を自前で結合している、`? true : false` | `string.Join(Environment.NewLine, Errs)`、`Errs.Count > 0` |
| `Utility/AppFile.cs:73-91` | `f.Attributes == FileAttributes.Directory` の完全一致で判定している（隠し属性などが付いたフォルダはファイル扱いになる） | 呼び出し元はすべてファイルパスを渡しているので `Path.GetDirectoryName` で足りる |

### 5-8【残】コメントアウトされたコード（12ファイル19ブロック）
`LibUtility.cs`（3）、`InfectionData.cs`（3）、`Dict.cs`（2）、`AppFile.cs`（2）、`AppString.cs`（2）、`EyeOpe.cs`、`StdForm1.cs`、`KarteLog.cs`、`LoginUser.cs`、`PatIns.cs`、`LibSettings.cs`、`TableData.cs`（各1）

いずれも git の履歴で追えるので、削除してください。一部はフェーズ5で消えます。

---

## 6. 進め方の提案

各ステップは1コミットで1カテゴリにし、ビルドと EyeCenter.Tests が通ることを確認してから次に進みます。

```
1. フェーズ5ブランチを master にマージする（競合は5ファイル）
   → 確認: ML / EyeCenter / NidekARK1 / CanonRKF1 のビルドと、EyeCenter.Tests がすべて通る
2. §2 の不具合を修正する（シグネチャを変えない修正だけ）
   → 確認: ビルドと、該当する画面の手動確認
3. §3-1 / §3-2 の DB 層（try/finally と AddParameters）
   → 確認: 検索・保存・削除を一通り実行するスモークテスト
4. §4-1 の自由入力 SQL のバインド変数化
   → 確認: 氏名に「'」を入れても検索できる
5. §5-1〜5-5 の大きな重複の解消（先に純粋な関数のテストを追加する）
   → 確認: 追加したテストが修正の前後で同じ結果になる
6. §5-6〜5-8 の小さな整理
```

ステップ1〜4で**不具合と堅牢性の問題**がなくなり、ステップ5で**約700行**、ステップ6で**約200行**減る見込みです（フェーズ5の約6,000行とは別）。
