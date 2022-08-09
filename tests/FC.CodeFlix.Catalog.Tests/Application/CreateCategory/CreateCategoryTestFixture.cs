using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.Tests.Common;
using Moq;

namespace FC.CodeFlix.Catalog.Tests.Application.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{ }

public class CreateCategoryTestFixture : BaseFixture
{
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

    public CreateCategoryInput GetValidCreateCategoryInput()
    {
        return new CreateCategoryInput(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                GetRandomBoolean()
            );
    }

    public CreateCategoryInput GetInvalidInputCategoryShortName()
    {
        var shortNameInvalidCreateCategoryInput = GetValidCreateCategoryInput();
        shortNameInvalidCreateCategoryInput.Name = Faker.Commerce.ProductName()[..2];

        return shortNameInvalidCreateCategoryInput;
    }

    public CreateCategoryInput GetInvalidInputCategoryTooLongName()
    {
        var tooLongNameInvalidCreateCategoryInput = GetValidCreateCategoryInput();
        tooLongNameInvalidCreateCategoryInput.Name = Faker.Commerce.ProductName();

        while (tooLongNameInvalidCreateCategoryInput.Name.Length <= 255)
            tooLongNameInvalidCreateCategoryInput.Name = $"{tooLongNameInvalidCreateCategoryInput.Name}{Faker.Commerce.ProductName()}";

        return tooLongNameInvalidCreateCategoryInput;
    }

    public CreateCategoryInput GetInvalidInputCategoryNullDescription()
    {
        var nullDescriptionInvalidCreateCategoryInput = GetValidCreateCategoryInput();
        nullDescriptionInvalidCreateCategoryInput.Description = null!;
        return nullDescriptionInvalidCreateCategoryInput;
    }

    public CreateCategoryInput GetInvalidInputCategoryTooLongDescription()
    {
        var tooLongDescriptionInvalidCreateCategoryInput = GetValidCreateCategoryInput();
        tooLongDescriptionInvalidCreateCategoryInput.Description = Faker.Commerce.ProductDescription();

        while (tooLongDescriptionInvalidCreateCategoryInput.Description.Length <= 10_000)
            tooLongDescriptionInvalidCreateCategoryInput.Description = $"{tooLongDescriptionInvalidCreateCategoryInput.Description}{Faker.Commerce.ProductDescription()}";

        return tooLongDescriptionInvalidCreateCategoryInput;
    }

    public Mock<ICategoryRepository> GetRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWork() => new();
}
