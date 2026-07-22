# Oracle.DataAccess 読み込みエラー対策(本番デプロイ時)

作成日: 2026-07-20

## 症状

本番環境(`C:\Shinseikai\EyeData\Oracle.DataAccess.dll` 2010/02/28版)で EyeCenter を起動すると、以下のエラーで起動に失敗する。

```
'MedicalLibrary.Utility.DB' のタイプ初期化子が例外をスローしました。
[InnerException]
ファイルまたはアセンブリ 'Oracle.DataAccess, Version=4.122.21.1,
Culture=neutral, PublicKeyToken=89b483f429c47342'、またはその依存関係の1つが
読み込めませんでした。見つかったアセンブリのマニフェスト定義はアセンブリ参照に
一致しません。(HRESULT からの例外: 0x80131040)
[InnerException]
ファイルまたはアセンブリ 'Oracle.DataAccess, Version=2.112.1.0, ...'(同上)
[StackTrace]
場所 MedicalLibrary.Utility.LibSettings.Init(Boolean forced)
場所 EyeCenter.MainForm.MainForm_Load(Object sender, EventArgs e)
```

テスト環境(開発機)では正常に動作する。

## 原因

DLL 本体ではなく、**開発機用の `EyeCenter.exe.config` が本番に持ち込まれていること**が原因。

- `MedicalLibrary.dll` は `Oracle.DataAccess, Version=2.112.1.0`(ODP.NET 11.2用)を参照している(`MedicalLibrary.csproj`)。本番の2010/02/28版DLLはこのバージョンであり、本来そのまま一致する。
- 開発機には Oracle 21c しかないため、2026-07-06 に EyeCenter の App.config へ **「2.112.1.0 → 4.122.21.1」の bindingRedirect** を追加して動かしている(テスト環境で動くのはこのため)。
- この `EyeCenter.exe.config` ごと本番へデプロイすると、本番でも 2.112 の読み込み要求が **4.122.21.1 にリダイレクトされ**、実ファイルは 2.112 のため「マニフェスト定義がアセンブリ参照に一致しない (0x80131040)」となる。
- エラーメッセージが 4.122.21.1 を要求していることが証拠。本番機に 21c は存在しないため、この要求は config の bindingRedirect 由来しかあり得ない。

## 対策(本番側の作業のみ、再ビルド不要)

1. 本番の `EyeCenter.exe` と同じフォルダにある `EyeCenter.exe.config` を開き、`Oracle.DataAccess` の `<dependentAssembly>`(bindingRedirect)ブロックを削除する。config がこの redirect 目的だけで存在するなら、ファイルごと削除してよい。
2. 開発機から一緒にコピーした可能性のある **21c系ファイルがあれば削除**する。
   - `Oracle.DataAccess.dll`(4.122.x版)
   - `System.Buffers.dll`
   - `System.Memory.dll`
   - `System.Numerics.Vectors.dll`
3. 念のため2010年版DLLのバージョンを確認する。

   ```powershell
   (Get-Item C:\Shinseikai\EyeData\Oracle.DataAccess.dll).VersionInfo.FileVersion
   ```

   - `2.112.x` → そのままでOK。
   - `2.111.x`(11.1世代)だった場合 → このローカルコピー自体を削除する。11.2クライアントが入っている本番機なら GAC の 2.112.1.0 に解決される。

## 今後のデプロイ運用

環境ごとに構成が異なるため、以下の区別を徹底する。

| 環境 | Oracle.DataAccess | exe.config |
|---|---|---|
| 開発機(Oracle 21c) | 4.122.21.1 + 依存DLL(System.Buffers 等) | bindingRedirect **必要** |
| 本番 C:\Shinseikai(Oracle 11.2) | 2.112.1.0(GAC/既存のまま) | bindingRedirect **禁止** |

- 本番へは **`EyeCenter.exe` と `MedicalLibrary.dll` のみ**を配布する。config と Oracle 関連 DLL は触らない。
- `MedicalLibrary.dll` は 2.112.1.0 参照のままビルドされているため、**両環境で同一バイナリが使用できる**(環境差の吸収は消費アプリ側の config のみで行う)。
