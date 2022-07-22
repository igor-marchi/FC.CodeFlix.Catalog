using Bogus;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.Tests.Domain.Entity.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName;

        Action action = () => DomainValidation.NotNull(value, "Value");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThrowWhenNull()
    {
        string? value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNull(value, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null");
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty or null");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOrEmptyOk()
    {
        var target = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanMin), parameters: 10)]
    public void MinLengthThrowWhenLess(string target, int minLenght)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MinLenght(target, minLenght, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should be at least {minLenght} characters long");
    }

    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
    public void MinLengthOk(string target, int minLenght)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MinLenght(target, minLenght, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthThrowWhenGreater(string target, int maxLenght)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MaxLenght(target, maxLenght, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should be less or equal {maxLenght} characters long"); ;
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
    public void MaxLengthOk(string target, int maxLenght)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MaxLenght(target, maxLenght, fieldName);

        action.Should().NotThrow(); ;
    }

    private static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests = 5)
    {
        yield return new object[] { "123", 10 };

        var Faker = new Faker();
        for (int index = 0; index < (numberOfTests - 1); index++)
        {
            var value = Faker.Commerce.ProductName();
            var minLenght = value.Length + (new Random().Next(1, 200));

            yield return new object[] { value, minLenght };
        }
    }

    private static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests = 5)
    {
        yield return new object[] { "1234", 3 };

        var Faker = new Faker();
        for (int index = 0; index < (numberOfTests - 1); index++)
        {
            var value = Faker.Commerce.ProductName();
            var minLenght = value.Length - (new Random().Next(1, 5));

            yield return new object[] { value, minLenght };
        }
    }

    private static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "12345", 3 };

        var Faker = new Faker();
        for (int index = 0; index < (numberOfTests - 1); index++)
        {
            var value = Faker.Commerce.ProductName();
            var maxLenght = value.Length - (new Random().Next(1, 5));

            yield return new object[] { value, maxLenght };
        }
    }

    private static IEnumerable<object[]> GetValuesLessThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "12345", 5 };

        var Faker = new Faker();
        for (int index = 0; index < (numberOfTests - 1); index++)
        {
            var value = Faker.Commerce.ProductName();
            var maxLenght = value.Length + (new Random().Next(0, 5));

            yield return new object[] { value, maxLenght };
        }
    }
}
