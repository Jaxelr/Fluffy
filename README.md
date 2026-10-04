# Fluffy

A thinly veiled validation library

## Build and run

The library and sample target .NET 10 and require the .NET 10 SDK.

```shell
dotnet build Fluffy.sln --configuration Release
dotnet run --project example --configuration Release
```

GitHub Actions restores and builds the solution in Release mode on pushes to
`main` and on pull requests. There are currently no automated test projects.

## Contributing

Repository formatting and Git defaults come from
[Jaxelr/dotfiles](https://github.com/Jaxelr/dotfiles). Follow `.editorconfig`,
`AGENTS.md`, and `.github/copilot-instructions.md`; code-style warnings are
enabled during builds through `Directory.Build.props`.

```shell
dotnet format Fluffy.sln --severity info
dotnet format Fluffy.sln --verify-no-changes --severity info --no-restore
```

## Usage

In essence i would want the library to function similarly to FluentValidation, in terms of API definition, but with a minimal footprint

```csharp

var obj = new Poco() { Id = 1, Value = "Test" };

PocoEnforcer<Poco> enforcer(obj);

var result = enforcer.Resolve();


class PocoEnforcer : Validator<Poco>
{
	PocoEnforcer()
	{
		Define(x => x.Id == 1);
		Define(x => x.Value != "Test");
	}
}

```

But also including a bit of helpers to check for certain custom scenarios.

__Note:__ This is on alpha stage
