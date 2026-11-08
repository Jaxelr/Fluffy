using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace Fluffy;

public static class Fluf
{
    private static readonly Lock RegistrationLock = new();
    private static readonly HashSet<Assembly> RegisteredAssemblies = [];
    private static Dictionary<Type, Func<object>> factories = [];

    public static void RegisterValidators(params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);

        lock (RegistrationLock)
        {
            var pendingAssemblies = assemblies.Distinct()
                .Where(assembly => !RegisteredAssemblies.Contains(assembly))
                .ToArray();

            if (pendingAssemblies.Length == 0)
            {
                return;
            }

            var registrations = new Dictionary<Type, Func<object>>(Volatile.Read(ref factories));

            foreach (var assembly in pendingAssemblies)
            {
                ArgumentNullException.ThrowIfNull(assembly);
                RegisterAssembly(assembly, registrations);
            }

            Volatile.Write(ref factories, registrations);
            RegisteredAssemblies.UnionWith(pendingAssemblies);
        }
    }

    internal static Fluf<T>? CreateValidator<T>() where T : class =>
        Volatile.Read(ref factories).TryGetValue(typeof(T), out var factory) ? (Fluf<T>) factory() : null;

    private static void RegisterAssembly(Assembly assembly, Dictionary<Type, Func<object>> registrations)
    {
        foreach (var type in assembly.GetTypes().Where(type => !type.IsAbstract && !type.ContainsGenericParameters))
        {
            var validatedType = GetValidatedType(type);

            if (validatedType is not null)
            {
                RegisterFactory(type, validatedType, registrations);
            }
        }
    }

    private static Type? GetValidatedType(Type validatorType)
    {
        for (var baseType = validatorType.BaseType; baseType is not null; baseType = baseType.BaseType)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(Fluf<>))
            {
                return baseType.GenericTypeArguments[0];
            }
        }

        return null;
    }

    private static void RegisterFactory(Type validatorType, Type validatedType, Dictionary<Type, Func<object>> registrations)
    {
        if (registrations.ContainsKey(validatedType))
        {
            throw new InvalidOperationException($"Multiple validators were found for type '{validatedType.FullName}'.");
        }

        var constructor = validatorType.GetConstructor(Type.EmptyTypes)
            ?? throw new InvalidOperationException($"Validator '{validatorType.FullName}' must have a public parameterless constructor.");
        var factory = Expression.Lambda<Func<object>>(Expression.New(constructor)).Compile();

        registrations.Add(validatedType, factory);
    }
}
