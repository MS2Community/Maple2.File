# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Is

Strongly-typed MapleStory2 .m2d file parsing library. Reads encrypted game data files and provides typed access to game entities (items, NPCs, quests, maps, skills, etc.).

## Build Commands

```bash
dotnet build                           # Build entire solution
dotnet test                            # Run all tests (MSTest)
dotnet test --filter "FullyQualifiedName~XBlockParserTest"  # Run single test class
dotnet pack Maple2.File.Parser         # Pack the NuGet package
```

Tests require a `MS2_DATA_FOLDER` environment variable (or `.env` file) pointing to extracted MapleStory2 data.

## Architecture

### Projects

- **Maple2.File.IO** — Low-level encrypted .m2d/.m2h file reading, decryption (AES), memory-mapped files. Multi-targets `net8.0` and `netstandard2.1`.
- **Maple2.File.Flat** — Interface definitions for all game entity types (`IMapEntity`, `ISpawnPointNPC`, `IPortal`, etc.). ~252 files across model libraries.
- **Maple2.File.Parser** — High-level parsers. Published as NuGet `Maple2.File.Parser.Tadeucci`. Contains:
  - `Flat/` — `FlatTypeIndex`, `FlatType`, `FlatProperty` for the model type hierarchy
  - `MapXBlock/` — `XBlockParser` for map entity data with runtime IL class generation
  - `Xml/` — Strongly-typed XML model classes for game data
  - `Tools/` — `Filter` (locale/feature), `Sanitizer`, `HierarchyMap`, `ILEmitter`
  - Root-level parsers: `ItemParser`, `NpcParser`, `QuestParser`, `SkillParser`, `TableParser` (60+ methods), etc.
- **Maple2.File.Generator** — Roslyn source generator for XML serialization. Embedded as analyzer in Parser project. Custom attributes: `M2dArray`, `M2dNullable`, `M2dVector3`, `M2dEnum`, `M2dColor`, `M2dFeatureLocale`.
- **Maple2.File.Unity** — Lightweight `netstandard2.1` subset for Unity. Shares source files via linked references from Parser.
- **Maple2.File.Cli** — Interactive CLI explorer for flat type data.
- **Maple2.File.Tests** — MSTest unit tests.
- **Maple2.File.Generator.Debugger** — For debugging the source generator.

### Key Patterns

**Encrypted pack files**: `M2dReader` opens .m2d/.m2h pairs, decrypts via `CryptoManager`, returns `PackFileEntry[]`. Use `GetXmlReader()`, `GetBytes()`, `GetString()` to access content.

**Runtime IL class generation**: `RuntimeClassLookup` uses `Reflection.Emit` to create concrete classes from `IMapEntity` interfaces at runtime. Types are cached after first generation.

**Locale/feature filtering**: Call `Filter.Load(reader, locale, env)` before XML parsing. Locales: TW, TH, NA, CN, JP, KR. Environments: Dev, Qa, DevStage, Stage, Live.

**XML parser pattern**: Each parser takes an `M2dReader`, has a `Parse()` method returning typed tuples, and uses `XmlSerializer` with source-generated helpers.

**XML sanitization**: `Sanitizer` fixes malformed game XML (empty attributes, invalid booleans, UTF-8 BOM issues) before deserialization.

## Code Style

- C# 12, .NET 8.0 SDK (see `global.json`)
- K&R brace style (no opening brace on new line)
- 4-space indentation
- `camelCase` for private fields, `_camelCase` for private static fields, `PascalCase` for private constants/static readonly
- `ALL_UPPER` for public/internal constants
- Prefer explicit types over `var` for built-in types
- Space after cast
- Nullable reference types enabled
- Overflow checking enabled in Parser
