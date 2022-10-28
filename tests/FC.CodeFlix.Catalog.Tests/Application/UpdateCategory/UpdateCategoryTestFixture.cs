using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCase.Category.UpdateCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.Tests.Common;
using Moq;

namespace FC.CodeFlix.Catalog.Tests.Application.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>
{ }

public class UpdateCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];

        return categoryDescription;
    }

    public bool GetRandomBoolean()
        => (new Random()).NextDouble() < 0.5;

    public Category GetExampleCategory()
    {
        return new Category(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );
    }

    public UpdateCategoryInput GetValidInput(Guid? id = null)
    {
        return new UpdateCategoryInput(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );
    }

    public UpdateCategoryInput GetInvalidInputCategoryShortName()
    {
        var shortNameInvalidCategoryInput = GetValidInput();
        shortNameInvalidCategoryInput.Name = Faker.Commerce.ProductName()[..2];

        return shortNameInvalidCategoryInput;
    }

    public UpdateCategoryInput GetInvalidInputCategoryTooLongName()
    {
        var tooLongNameInvalidCategoryInput = GetValidInput();
        tooLongNameInvalidCategoryInput.Name = Faker.Commerce.ProductName();

        while (tooLongNameInvalidCategoryInput.Name.Length <= 255)
            tooLongNameInvalidCategoryInput.Name = $"{tooLongNameInvalidCategoryInput.Name}{Faker.Commerce.ProductName()}";

        return tooLongNameInvalidCategoryInput;
    }

    public UpdateCategoryInput GetInvalidInputCategoryTooLongDescription()
    {
        var tooLongDescriptionInvalidCategoryInput = GetValidInput();
        tooLongDescriptionInvalidCategoryInput.Description = Faker.Commerce.ProductDescription();

        while (tooLongDescriptionInvalidCategoryInput.Description.Length <= 10_000)
            tooLongDescriptionInvalidCategoryInput.Description = $"{tooLongDescriptionInvalidCategoryInput.Description}{Faker.Commerce.ProductDescription()}";

        return tooLongDescriptionInvalidCategoryInput;
    }
}
