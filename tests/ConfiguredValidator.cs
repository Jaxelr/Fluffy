using System;

namespace Fluffy.Tests;

internal sealed class ConfiguredValidator<T> : Fluf<T> where T : class
{
    public ConfiguredValidator(Func<T, bool> rule)
    {
        _ = Define(rule);
    }
}
