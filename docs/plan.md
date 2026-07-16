# InnoUketsukeLib.dll が依然として必要な理由

## 原因

`Utility/LibSettings.cs:490-502` と `Entity/Staff.cs:375-388` は、InnoUketsukeLib への参照を**呼び出し箇所と同じメソッド内の try/catch** で囲んでいる。

```csharp
// LibSettings.Init()
try
{
    InnoUketsukeLib.Utility.AppInit.g_AppInit.Init();
}
catch (Exception ex) { LibUtility.Except(ex, false); }
```

```csharp
// Staff.Verify()
try
{
    if (!InnoUketsukeLib.Entity.M_USR.g_Usr1.GetData(i, pw)) return obj;
}
catch (Exception ex) { LibUtility.Except(ex, false); }
```

.NET の JIT は、メソッドが**最初に呼び出された時点**でそのメソッド全体の IL 中の型参照を解決する。try/catch は「実行中に発生した例外」を捕まえる仕組みであり、**参照アセンブリが見つからずメソッド自体をコンパイルできない**ことによる `FileNotFoundException` / `TypeLoadException` は、try ブロックの中身が実行される前——つまり `Init()` や `Verify()` を**呼び出した側**——で発生する。

- `LibSettings.Init()` は本ライブラリ内からは呼ばれておらず、外部の `EyeCenter.exe` 等から呼ばれている（呼び出し側に try/catch があるかは不明）。
- `Staff.Verify()` の呼び出し元 `Boundary/LoginPrompt.cs:50,61` の `loginButton_Click` には try/catch が**ない**ため、DLL が無いとログイン処理自体で例外が発生する。

「try/catch で囲んだので DLL がなくても動くはず」という想定は誤りで、**同一メソッド内の try/catch では防げない**ケースである。

## 対応方針（案）

InnoUketsukeLib を使う処理を**別メソッドに切り出し**（`[MethodImpl(MethodImplOptions.NoInlining)]` を付与）、その**呼び出し自体**を呼び出し元でtry/catchする。こうすることで、型解決の失敗が別メソッドの呼び出し時点にとどまり、呼び出し元の try/catch で捕捉できるようになる。

対象:
1. `Utility/LibSettings.cs` — `InnoUketsukeLib.Utility.AppInit.g_AppInit.Init()` 呼び出し部分
2. `Entity/Staff.cs` — `InnoUketsukeLib.Entity.M_USR.g_Usr1.GetData(i, pw)` 呼び出し部分

## 未確認事項

- `MedicalLibrary.csproj` の参照自体を外す場合は、`docs/DLL移植調査.md` にある通り `Staff.Verify()` のスタッフ認証代替手段が別途必要になる。DLL 参照を残したまま「呼び出し元で例外を吸収できるようにする」対応と、DLL 参照ごと外す対応のどちらを目指すか要確認。
