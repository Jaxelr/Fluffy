# Agent guidance

## Repository

- Fluffy is a small .NET 10 validation library with no external dependencies.
- `src` contains the library; `example` contains the console sample.
- `Fluffy.sln` includes both projects.
- Follow `.editorconfig` and `.github/copilot-instructions.md` for formatting,
  naming, complexity, and commit conventions.
- Keep one type per file, use file-scoped namespaces, and preserve public APIs
  and validation behavior when making style-only changes.
- Do not add code comments unless explicitly requested.

## Development

```shell
dotnet restore Fluffy.sln
dotnet build Fluffy.sln --configuration Release --no-restore
dotnet format Fluffy.sln --verify-no-changes --severity info --no-restore
dotnet run --project example --configuration Release --no-build
```

The sample reports two validation errors, prints `Done`, and waits for input.
There are currently no test projects. Verify style-only changes with the build,
formatting check, and sample; add tests when introducing behavior changes.
Do not treat a successful solution-level test command as evidence of test coverage.

## Changes

- Keep changes focused and update affected documentation in the same change set.
- Use semantic commits and make separate commits for distinct logical changes.
- Obtain explicit permission before committing or pushing. Permission to commit
  does not grant permission to push.
- Import shared repository dotfiles from `Jaxelr/dotfiles`; do not copy personal
  or machine-specific Git configuration into the repository.
