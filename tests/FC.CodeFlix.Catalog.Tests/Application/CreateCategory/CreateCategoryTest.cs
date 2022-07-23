using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using UseCase = FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory;

namespace FC.CodeFlix.Catalog.Tests.Application.CreateCategory;

public class CreateCategoryTest
{
    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new UseCase.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var categoryInputData = new
        {
            Name = "Category name",
            Description = "Category description",
            IsActive = true
        };

        var input = new UseCase.CreateCategoryInput(
            categoryInputData.Name,
            categoryInputData.Description,
            categoryInputData.IsActive
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Name.Should().Be(categoryInputData.Name);
        output.Description.Should().Be(categoryInputData.Description);
        output.IsActive.Should().Be(categoryInputData.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }
}
