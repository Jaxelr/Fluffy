using Fluffy;
using System;

namespace Sample.Usage
{
    internal static class Program
    {
        private static void Main()
        {
            var poco = new Poco() { Id = 2, Name = "User not" };
            var enforcer = new PocoValidator();

            var (validation, errors) = enforcer.Resolve(poco);

            var gimme = poco.ApplyRule(d => d.Name == "Pandy");

            if (!validation)
            {
                foreach (var error in errors)
                    Console.WriteLine(error);
            }

            Console.Write("Done");

            Console.Read();
        }
    }

    public class PocoValidator : Fluf<Poco>
    {
        public PocoValidator()
        {
            Define(x => x.Id == 1, "Id does not match 1");
            Define(x => x.Name == "User", "User is not user");
        }
    }

    public record Poco
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}