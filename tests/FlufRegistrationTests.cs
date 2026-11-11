using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Fluffy.Tests;

public class FlufRegistrationTests
{
    [Fact]
    public void RegisterValidatorsScansEachAssemblyOnlyOnce()
    {
        var assembly = new CountingAssembly(typeof(object), typeof(CountingValidator<List<int>>));

        Fluf.RegisterValidators(assembly, assembly);
        Fluf.RegisterValidators(assembly);
        var (validation, errors) = new List<int>().Resolve();

        Assert.False(validation);
        Assert.Equal("Validation failed", Assert.Single(errors));
        Assert.Equal(1, assembly.ScanCount);
    }

    [Fact]
    public void ResolveCreatesFreshValidatorForEachCall()
    {
        var assembly = new CountingAssembly(typeof(CountingValidator<List<string>>));
        var values = new List<string>();
        Fluf.RegisterValidators(assembly);
        Assert.Equal(0, CountingValidator<List<string>>.InstanceCount);

        var (firstValidation, firstErrors) = values.Resolve();
        var (secondValidation, secondErrors) = values.Resolve();

        Assert.False(firstValidation);
        Assert.False(secondValidation);
        Assert.Equal("Validation failed", Assert.Single(firstErrors));
        Assert.Equal("Validation failed", Assert.Single(secondErrors));
        Assert.Equal(2, CountingValidator<List<string>>.InstanceCount);
    }

    [Fact]
    public void RegisterValidatorsRejectsDuplicatesWithoutChangingRegistrations()
    {
        var existingAssembly = new CountingAssembly(typeof(CountingValidator<Uri>));
        Fluf.RegisterValidators(existingAssembly);
        var conflictingAssembly = new CountingAssembly(typeof(CountingValidator<string>), typeof(StringValidator));

        var exception = Assert.Throws<InvalidOperationException>(() => Fluf.RegisterValidators(conflictingAssembly));
        var (unregisteredValidation, unregisteredErrors) = "value".Resolve();
        var (existingValidation, existingErrors) = new Uri("https://example.com").Resolve();

        Assert.Contains(typeof(string).FullName!, exception.Message);
        Assert.True(unregisteredValidation);
        Assert.Empty(unregisteredErrors);
        Assert.False(existingValidation);
        Assert.Equal("Validation failed", Assert.Single(existingErrors));
    }

    [Fact]
    public void RegisterValidatorsRejectsValidatorWithoutPublicParameterlessConstructor()
    {
        var assembly = new CountingAssembly(typeof(ConfiguredValidator<Version>));

        var exception = Assert.Throws<InvalidOperationException>(() => Fluf.RegisterValidators(assembly));

        Assert.Contains(typeof(ConfiguredValidator<Version>).FullName!, exception.Message);
        Assert.Contains("public parameterless constructor", exception.Message);
    }

    [Fact]
    public void ResolveWithoutRegisteredValidatorReturnsValidWithNoErrors()
    {
        var value = new StringBuilder("unregistered");

        var (validation, errors) = value.Resolve();

        Assert.True(validation);
        Assert.Empty(errors);
    }

    [Fact]
    public void ResolveWithNullThrows()
    {
        StringBuilder value = null!;

        var exception = Assert.Throws<ArgumentNullException>(() => value.Resolve());

        Assert.Equal("poco", exception.ParamName);
    }
}
