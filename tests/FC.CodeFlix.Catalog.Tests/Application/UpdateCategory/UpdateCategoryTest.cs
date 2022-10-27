using FC.CodeFlix.Catalog.Application.UseCase.Category.Common;
using FC.CodeFlix.Catalog.Domain.Entity;
using FluentAssertions;
using Moq;
using UseCase = FC.CodeFlix.Catalog.Application.UseCase.Category.UpdateCategory;

namespace FC.CodeFlix.Catalog.Tests.Application.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(UpdateCategory))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategory(Category category, UseCase.UpdateCategoryInput input)
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        repositoryMock
            .Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
