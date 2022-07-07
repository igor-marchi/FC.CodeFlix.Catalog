using FC.CodeFlix.Catalog.Domain.Exceptions;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Tests.Domain.Entity.Category;

public class CategoryTests
{
    [Fact(DisplayName = nameof(Instatiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instatiate()
    {
        // Arrange
        var validDate = new
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        // Act
        var category = new DomainEntity.Category(validDate.Name, validDate.Description);
        var datetimeAfter = DateTime.Now;

        // Assert
        Assert.NotNull(category);
        Assert.Equal(validDate.Name, category.Name);
        Assert.Equal(validDate.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < datetimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var validDate = new
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validDate.Name, validDate.Description, isActive);
        var datetimeAfter = DateTime.Now;

        Assert.NotNull(category);
        Assert.Equal(validDate.Name, category.Name);
        Assert.Equal(validDate.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < datetimeAfter);
        Assert.Equal(category.IsActive, isActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        var categoryDate = new
        {
            Description = "category description"
        };

        Action action = () => new DomainEntity.Category(name!, categoryDate.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var categoryDate = new
        {
            Name = "category Name"
        };

        Action action = () => new DomainEntity.Category(categoryDate.Name, null!);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be null", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThen3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("a")]
    [InlineData("ab")]
    public void InstantiateErrorWhenNameIsLessThen3Characters(string invalidName)
    {
        var categoryDate = new
        {
            Description = "category description"
        };

        Action action = () => new DomainEntity.Category(invalidName, categoryDate.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be at least 3 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThen255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThen255Characters()
    {
        var categoryDate = new
        {
            Description = "category description"
        };

        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category(invalidName, categoryDate.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThen10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThen10_000Characters()
    {
        var categoryDate = new
        {
            Name = "category name"
        };

        var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category(categoryDate.Name, invalidDescription);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
    }
}
