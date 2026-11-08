using System;
using Fluffy;

namespace Sample.Usage;

internal static class Program
{
    private static void Main()
    {
        Fluf.RegisterValidators(typeof(PocoValidator).Assembly);

        var poco = new Poco()
        {
            Id = 2,
            Name = "User not"
        };
        var (validation, errors) = poco.Resolve();

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
