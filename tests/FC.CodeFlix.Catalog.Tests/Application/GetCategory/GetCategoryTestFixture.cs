﻿using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.Tests.Common;
using Moq;

namespace FC.CodeFlix.Catalog.Tests.Application.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture>
{ }

public class GetCategoryTestFixture : BaseFixture
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

    public Category GetValidCategory()
        => new(GetValidCategoryName(), GetValidCategoryDescription());

    public Mock<ICategoryRepository> GetRepositoryMock() => new();
}
