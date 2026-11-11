# Fluffy

> [!NOTE]
> This library is at the alpha stage.

A thinly veiled validation library

## Build and run

`Fluffy.slnx` is the XML solution containing all three projects.

```shell
dotnet build Fluffy.slnx --configuration Release
dotnet run --project example --configuration Release
```

## Tests

`tests` contains an xUnit project with a small set of tests for validation
results, failed-rule messages, the default error message, and `ApplyRule`.

```shell
dotnet test Fluffy.slnx --configuration Release
```

## Contributing

Contributions from the community are welcome. Feel free to open an issue or
submit a pull request.

## Usage

Define validation rules with `Fluf<T>.Define`, register assemblies once at startup,
and call `poco.Resolve()` to obtain a validation result and failed-rule messages.
The sample provides `Poco` and `PocoValidator`:

```csharp
Fluf.RegisterValidators(typeof(PocoValidator).Assembly);

var poco = new Poco()
{
    Id = 2,
    Name = "User not"
};
var (validation, errors) = poco.Resolve();

foreach (string error in errors)
{
    Console.WriteLine(error);
}
```

`ApplyRule` evaluates a single predicate directly against an object:

```csharp
bool matches = poco.ApplyRule(x => x.Name == "User");
```
