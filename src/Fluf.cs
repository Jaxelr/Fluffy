using System;
using System.Collections.Generic;

namespace Fluffy;

public abstract class Fluf<T> where T : class
{
    internal readonly List<(Func<T, bool> rule, string error)> Rules = [];

    public Fluf<T> Define(Func<T, bool> rule, string errorMessage)
    {
        Rules.Add((rule, errorMessage));

        return this;
    }

    public Fluf<T> Define(Func<T, bool> rule) =>
        Define(rule, $"Generic error message for Type {typeof(T).Name}");

    public (bool validation, IEnumerable<string> errors) Resolve(T poco)
    {
        List<string> errors = [];
        bool valid = true;

        foreach ((var rule, string error) in Rules)
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
