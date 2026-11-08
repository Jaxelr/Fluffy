using System;
using System.Collections.Generic;

namespace Fluffy;

public static class FlufExtensions
{
    public static bool ApplyRule<T>(this T poco, Func<T, bool> func) => func(poco);

    public static (bool validation, IEnumerable<string> errors) Resolve<T>(this T poco) where T : class
    {
        ArgumentNullException.ThrowIfNull(poco);

        return Fluf.CreateValidator<T>()?.Resolve(poco) ?? (true, []);
    }
}
