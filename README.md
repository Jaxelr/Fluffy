# Fluffy

> [!NOTE]
> This library is at the alpha stage.

A thinly veiled validation library

## Build and run

The library, sample, and tests target .NET 10 and require the .NET 10 SDK.
`Fluffy.slnx` is the XML solution containing all three projects.

```shell
dotnet build Fluffy.slnx --configuration Release
dotnet run --project example --configuration Release
```

## Tests

`tests\Fluffy.Tests` is an xUnit project with a small set of tests for validation
results, failed-rule messages, the default error message, and `ApplyRule`.

```shell
dotnet test Fluffy.slnx --configuration Release
```

## Contributing

Contributions from the community are welcome. Feel free to open an issue or
submit a pull request.

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
