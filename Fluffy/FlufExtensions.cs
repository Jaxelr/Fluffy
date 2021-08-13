using System;

namespace Fluffy
{
    public static class FlufExtensions
    {
        public static bool ApplyRule<T>(this T poco, Func<T, bool> func) => func(poco);

        public static (bool validation, string? error) Resolve<T>(this T poco)
        {
            foreach ((var rule, string error) in rules)
            {
                if (!rule(poco)
                {
                    return (false, error);
                }
            }

            return (true, null);
        }
    }
}