# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A Japanese electronic medical record (電子カルテ / "Karte") **class library** — patient records, orders, SOAP charts, ophthalmology (眼科) workflows, device integrations. It builds to `MedicalLibrary.dll` and is consumed by exactly three external apps: `EyeCenter.exe`, `NidekARK1.exe`, `CanonRKF1.exe`; it is **not** a standalone application. Code unreachable from those apps was removed in 2026-07 (see `docs/cleanup-plan.md`). OpeOrder.exe was retired in 2026-07 (フェーズ3) and its dedicated code deleted; the surgical-order UI still reachable from EyeCenter (`FormOpeOrder`, `OpeOrderData`/`OpeOrderMaster`, `OpeOrderPathTemplate`/`PathMaster`) is intentionally retained.

- **No `Main` and no entry point.** Launch/interop helpers for external exes live in `Utility/Launcher.cs`, `InnoProgram.cs`.
- **No test suite, no CI, no linter.** There is nothing to "run a single test."
- Target: **.NET Framework 4.8** (legacy — not .NET Core/5+). C# + Windows Forms. Old-style (non-SDK) MSBuild `.csproj`.

## Build

The only platform is **x86** (Shinseikai deployment; output goes to `C:\shinseikai\`):

```
msbuild MedicalLibrary.csproj /p:Configuration=Release /p:Platform=x86
```

- `dotnet build` is **not** supported (non-SDK csproj, GAC/HintPath references). Use `msbuild` (VS 2022+) only.
- x86 is required — the native Oracle ODP.NET client is 32-bit.
- Reference `HintPath`s climb ~7 levels up to sibling `Karte`/`Shinseikai` folders and an Oracle 11.2 client folder. Builds assume those external DLLs/folders are present on the machine.
- The former `INNO` compile symbol and the AnyCPU/IJI configurations were removed in 2026-07: the INNO (Shinseikai) code paths are now unconditional — `StdEntity.Db` is `DB.Db3` and the DB link is `@INNO.WORLD`.

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
- **DB access is via global static singletons** `DB.Db1`/`Db2`/`Db3` (`Utility/DB.cs`) with one shared `OracleCommand` (`BindByName=true`); entities reach the DB through the static `StdEntity.Db` (= `Db3`). Not thread-safe by design — Open/Close/`Parameters.Clear` ordering matters.
- **Hardcoded absolute paths** in `Utility/Env.cs` (`C:\macs`, `c:\karte`, `c:\innokarte`, `c:\shinseikai`) and **hardcoded Oracle credentials** in `Utility/LibSettings.cs` (serialized to/from `Setting.xml`). The app assumes this on-disk layout.

## Conventions

Additional project rules are in `.claude/rules/` (loaded automatically):
- `coding-guidelines.md` — minimal, surgical changes; surface assumptions before implementing.
- `commit.md` — commit messages use emoji prefixes (`✨ feat`, `🐛 fix`, `📝 docs`, `♻️ refactor`, `✅ test`), described in Japanese.
- `response-style.md` — return diffs/patches, not prose; keep changes minimal; apply edits directly.
