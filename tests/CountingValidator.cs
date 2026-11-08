namespace Fluffy.Tests;

internal sealed class CountingValidator<T> : Fluf<T> where T : class
{
    public CountingValidator()
    {
        InstanceCount++;
        _ = Define(_ => false, "Validation failed");
    }

    public static int InstanceCount { get; private set; }
}
