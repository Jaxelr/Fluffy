using Xunit;

namespace Fluffy.Tests;

public class FlufTests
{
    [Fact]
    public void ResolvePassingRulesReturnsValidWithNoErrors()
    {
        var validator = new StringValidator();
        _ = validator.Define(value => value.Length > 0, "Value is empty")
            .Define(value => value == "expected", "Value does not match");

        var (validation, errors) = validator.Resolve("expected");

        Assert.True(validation);
        Assert.Empty(errors);
    }

    [Fact]
    public void ResolveMixedRulesReturnsOnlyFailedMessagesInOrder()
    {
        var validator = new StringValidator();
        _ = validator.Define(value => value == "expected", "Value does not match")
            .Define(value => value.Length > 0, "Value is empty")
            .Define(value => value.Length > 10, "Value is too short");
        string[] expectedErrors = ["Value does not match", "Value is too short"];

        var (validation, errors) = validator.Resolve("actual");

        Assert.False(validation);
        Assert.Equal(expectedErrors, errors);
    }

    [Fact]
    public void DefineWithoutMessageUsesGenericErrorMessage()
    {
        var validator = new StringValidator();
        _ = validator.Define(value => value.Length == 0);

        var (validation, errors) = validator.Resolve("actual");

        Assert.False(validation);
        Assert.Equal("Generic error message for Type String", Assert.Single(errors));
    }

    [Theory]
    [InlineData("expected", true)]
    [InlineData("different", false)]
    public void ApplyRuleReturnsPredicateResult(string value, bool expected)
    {
        bool result = value.ApplyRule(candidate => candidate == "expected");

        Assert.Equal(expected, result);
    }
}
