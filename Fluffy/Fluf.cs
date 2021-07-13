using System;
using System.Collections.Generic;

namespace Fluffy
{
    public class Fluf<T>
    {
        private readonly T local;
        private readonly List<(Func<T, bool> rule, string error)> rules;

        public Fluf(T poco)
        {
            local = poco;
            rules = new List<(Func<T, bool>, string)>();
        }

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

        public bool ApplyRule(Func<T, bool> func) => func(local);

        public (bool validation, string? error) Resolve()
        {
            foreach ((var rule, string error) in rules)
            {
                if (!rule(local))
                {
                    return (false, error);
                }
            }

            return (true, null);
        }
    }
}