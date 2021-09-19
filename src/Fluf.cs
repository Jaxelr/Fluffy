using System;
using System.Collections.Generic;

namespace Fluffy
{
    public abstract class Fluf<T> where T : class
    {
        internal readonly List<(Func<T, bool> rule, string error)> rules = new();

        public Fluf<T> Define(Func<T, bool> rule, string errorMessage)
        {
            rules.Add((rule, errorMessage));

            return this;
        }

        public Fluf<T> Define(Func<T, bool> rule)
        {
            rules.Add((rule, $"Generic error message for Type {typeof(T).Name}"));

            return this;
        }

        public (bool validation, IEnumerable<string> errors) Resolve(T poco)
        {
            List<string> errors = new();
            bool valid = true;

            foreach ((var rule, string error) in rules)
            {
                if (!rule(poco))
                {
                    valid = false;
                    errors.Add(error);
                }
            }

            return (valid, errors);
        }
    }
}