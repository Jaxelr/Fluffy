using System;
using Fluffy;

namespace Sample.Usage;

internal static class Program
{
    private static void Main()
    {
        var poco = new Poco()
        {
            Id = 2,
            Name = "User not"
        };
        var enforcer = new PocoValidator();

        var (validation, errors) = enforcer.Resolve(poco);

        _ = poco.ApplyRule(d => d.Name == "Pandy");

        if (!validation)
        {
            foreach (string error in errors)
            {
                Console.WriteLine(error);
            }
        }

        Console.Write("Done");

        _ = Console.Read();
    }
}
