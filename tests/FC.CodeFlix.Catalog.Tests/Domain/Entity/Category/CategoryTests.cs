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
}
