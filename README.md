# Fluffy

> [!NOTE]
> This library is in the alpha stage.

A thinly veiled validation library

## Build and run

The library, sample, and tests target .NET 10 and require the .NET 10 SDK.
`Fluffy.slnx` is the XML solution containing all three projects.

```shell
dotnet build Fluffy.slnx --configuration Release
dotnet run --project example --configuration Release
```

GitHub Actions restores, builds, and tests the solution in Release mode and
checks formatting and code style on pushes to `main` and on pull requests.
`.gitattributes` enforces CRLF checkouts for text files on every platform, with
LF for shell scripts, to match `.editorconfig` during CI formatting checks.

## Tests

`tests\Fluffy.Tests` is an xUnit project with a small set of tests for validation
results, failed-rule messages, the default error message, and `ApplyRule`.

```shell
dotnet test Fluffy.slnx --configuration Release
```

## Contributing

Repository formatting and Git defaults come from
[Jaxelr/dotfiles](https://github.com/Jaxelr/dotfiles). Follow `.editorconfig`,
`AGENTS.md`, and `.github/copilot-instructions.md`; code-style warnings are
enabled during builds through `Directory.Build.props`.

```shell
dotnet format Fluffy.slnx --severity info
dotnet format Fluffy.slnx --verify-no-changes --severity info --no-restore
```

## Usage

Define validation rules with `Fluf<T>.Define` and call `Resolve` to obtain a
validation result and the messages for every failed rule. The sample provides
`Poco` and `PocoValidator`:

```csharp
var poco = new Poco()
{
    Id = 2,
    Name = "User not"
};
var enforcer = new PocoValidator();

var (validation, errors) = enforcer.Resolve(poco);

foreach (string error in errors)
{
    Console.WriteLine(error);
}
```

`ApplyRule` evaluates a single predicate directly against an object:

```csharp
bool matches = poco.ApplyRule(x => x.Name == "User");
```
