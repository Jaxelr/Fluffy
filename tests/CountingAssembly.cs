using System;
using System.Reflection;

namespace Fluffy.Tests;

internal sealed class CountingAssembly : Assembly
{
    private readonly Type[] types;

    public CountingAssembly(params Type[] types)
    {
        this.types = types;
    }

    public int ScanCount { get; private set; }

    public override Type[] GetTypes()
    {
        ScanCount++;

        return types;
    }
}
