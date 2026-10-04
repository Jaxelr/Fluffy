using Fluffy;

namespace Sample.Usage;

public class PocoValidator : Fluf<Poco>
{
    public PocoValidator()
    {
        _ = Define(x => x.Id == 1, "Id does not match 1");
        _ = Define(x => x.Name == "User", "User is not user");
    }
}
