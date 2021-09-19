using System;

namespace Fluffy
{
    public static class FlufExtensions
    {
        public static bool ApplyRule<T>(this T poco, Func<T, bool> func) => func(poco);
    }
}