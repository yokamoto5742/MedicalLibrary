# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A Japanese electronic medical record (電子カルテ / "Karte") **class library** — patient records, orders, DPC diagnoses, billing (医事会計), SOAP charts, drug/injection instructions, device integrations. It builds to `MedicalLibrary.dll` and is consumed by external apps (e.g. `ProasAgent.exe`); it is **not** a standalone application.

- **No `Main` and no entry point** — `Class1.cs` is an empty stub. Launch/interop helpers for external exes live in `Utility/Launcher.cs`, `MacsProgram.cs`, `InnoProgram.cs`.
- **No test suite, no CI, no linter.** There is nothing to "run a single test."
- Target: **.NET Framework 4.0, Client Profile** (legacy — not .NET Core/5+). C# + Windows Forms. Old-style (non-SDK) MSBuild `.csproj`.

## Build

Primary configuration is **Release|x86** (Shinseikai/INNO deployment):

```
msbuild MedicalLibrary.csproj /p:Configuration=Release /p:Platform=x86
```

- `dotnet build` is **not** supported (legacy .NET 4.0 Client Profile, non-SDK csproj, GAC/HintPath references). Use `msbuild` (VS 2022) only.
- x86 is required for INNO/Oracle builds — the native Oracle ODP.NET client is 32-bit.
- Reference `HintPath`s and output dirs climb ~7 levels up to sibling `Karte`/`Shinseikai`/`macs` and an Oracle 11.2 client folder. Builds assume those external DLLs/folders are present on the machine.

### `INNO` compile symbol changes runtime behavior

The `INNO` symbol (set in the **x86** configurations) is an architectural switch, not just an output-path change:
- Adds a third DB connection `DB.Db3` (`DBConnectionString3`), and `Entity/StdEntity.cs` repoints the shared `StdEntity.Db` from `Db1` to `Db3`.
- DB link becomes `@INNO.WORLD` (vs `@IJI.WORLD` for AnyCPU/non-INNO).
- INNO = the Shinseikai / innokarte hospital deployment; AnyCPU (Release → `Karte`) = the IJI deployment.

## Editing rules

- **Preserve each file's existing encoding.** Files are a mix of Shift-JIS/CP932 and UTF-8-with-BOM, all containing Japanese comments/`<summary>` docs. Do not re-encode — it corrupts the Japanese text.
- Comments and XML doc comments are written in **Japanese**; match that when adding docs.

## Code style (deviations from C# defaults)

- **`snake_case` for method parameters and local variables** (`connection_string`, `param_list`, `con_str`).
- `snake_case` private static fields (`legacy_home`, `db_link`); **`ALL_CAPS` public properties** for env constants (`LEGACY_HOME`, `AGENT_HOME`, `DB_LINK`).
- Types, methods, and public singletons are PascalCase. 4-space indent, Allman braces.
- No `.editorconfig` or analyzer ruleset (x86 configs even set `CodeAnalysisIgnoreBuiltInRules=true`).

## Architecture & gotchas

- **Layered by top-level folder / namespace:** `Entity/` (domain model, `MedicalLibrary.Entity`), `Boundary/` (WinForms UI: `Form*` dialogs, `Ctrl*`/`Std*` controls), `Agent/` (feature/business-logic modules + forms), `Utility/` (infrastructure: `DB.cs`, `Env.cs`, `LibSettings.cs`).
- **DB access is via global static singletons** `DB.Db1`/`Db2` (`Utility/DB.cs`) with one shared `OracleCommand` (`BindByName=true`); entities reach the DB through the static `StdEntity.Db`. Not thread-safe by design — Open/Close/`Parameters.Clear` ordering matters.
- **Hardcoded absolute paths** in `Utility/Env.cs` (`C:\macs`, `c:\karte`, `c:\innokarte`, `c:\shinseikai`) and **hardcoded Oracle credentials** in `Utility/LibSettings.cs` (serialized to/from `Setting.xml`). The app assumes this on-disk layout.

## Conventions

Additional project rules are in `.claude/rules/` (loaded automatically):
- `coding-guidelines.md` — minimal, surgical changes; surface assumptions before implementing.
- `commit.md` — commit messages use emoji prefixes (`✨ feat`, `🐛 fix`, `📝 docs`, `♻️ refactor`, `✅ test`), described in Japanese.
- `response-style.md` — return diffs/patches, not prose; keep changes minimal; apply edits directly.
