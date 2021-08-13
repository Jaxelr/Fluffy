using System;
using System.Collections.Generic;

namespace Fluffy
{
    public class Fluf<T>
    {
        internal readonly List<(Func<T, bool> rule, string error)> rules = new();

        public Fluf<T> Define(Func<T, bool> rule, string error)
        {
            rules.Add((rule, error));

            return this;
        }

        public Fluf<T> Define(Func<T, bool> rule)
        {
            rules.Add((rule, $"Generic error message for rule {rule}"));

            return this;
        }

        public (bool validation, string? error) Resolve(T poco)
        {
            foreach ((var rule, string error) in rules)
            {
                if (!rule(poco))
                {
                    return (false, error);
                }
            }

            return (true, null);
        }
    }
}