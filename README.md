# Fluffy

A thinly veiled validation library

In essence i would want the library to function similarly to FluentValidation, but with a minimal footprint

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

__Note__ This is on alpha stage
