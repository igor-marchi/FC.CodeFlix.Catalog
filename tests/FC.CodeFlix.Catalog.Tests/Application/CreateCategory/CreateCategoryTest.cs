using FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;

using UseCase = FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory;

namespace FC.CodeFlix.Catalog.Tests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var useCase = new UseCase.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetValidCreateCategoryInput();

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
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategoryWithOnlyName()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var useCase = new UseCase.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(_fixture.GetValidCategoryName());

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
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be("");
        output.IsActive.Should().BeTrue();
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategoryWithOnlyNameAndDescription()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var useCase = new UseCase.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription()
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
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().BeTrue();
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAggregate))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(nameof(GetInvalidInputs))]
    public async void ThrowWhenCantInstantiateAggregate(CreateCategoryInput input, string exceptionMessage)
    {
        var useCase = new UseCase.CreateCategory(
            _fixture.GetRepositoryMock().Object,
            _fixture.GetUnitOfWork().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().
            ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }

    private static IEnumerable<Object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryTestFixture();

        var invalidInputLists = new List<object[]>();

        // name não pode ser menor que 3 caracteres
        var shortNameInvalidCreateCategoryInput = fixture.GetValidCreateCategoryInput();
        shortNameInvalidCreateCategoryInput.Name = fixture.Faker.Commerce.ProductName()[..2];

        invalidInputLists.Add(new object[]
        {
            shortNameInvalidCreateCategoryInput,
            "Name should be at least 3 characters long"
        });

        // nome não pode ser maior que 255 caracteres
        var tooLongNameInvalidCreateCatogoryInput = fixture.GetValidCreateCategoryInput();
        tooLongNameInvalidCreateCatogoryInput.Name = fixture.Faker.Commerce.ProductName();

        while (tooLongNameInvalidCreateCatogoryInput.Name.Length <= 255)
            tooLongNameInvalidCreateCatogoryInput.Name = $"{tooLongNameInvalidCreateCatogoryInput.Name}{fixture.Faker.Commerce.ProductName()}";

        invalidInputLists.Add(new object[]
        {
            tooLongNameInvalidCreateCatogoryInput,
            "Name should be less or equal 255 characters long"
        });

        // description não pode ser nula
        var nullDescriptionInvalidCreateCatogoryInput = fixture.GetValidCreateCategoryInput();
        nullDescriptionInvalidCreateCatogoryInput.Description = null!;
        invalidInputLists.Add(new object[]
        {
            nullDescriptionInvalidCreateCatogoryInput,
            "Description should not be null"
        });

        // description não deve ser maior que 10.000 caracteres
        var tooLongDescriptionInvalidCreateCatogoryInput = fixture.GetValidCreateCategoryInput();
        tooLongDescriptionInvalidCreateCatogoryInput.Description = fixture.Faker.Commerce.ProductDescription();

        while (tooLongDescriptionInvalidCreateCatogoryInput.Description.Length <= 10_000)
            tooLongDescriptionInvalidCreateCatogoryInput.Description = $"{tooLongDescriptionInvalidCreateCatogoryInput.Description}{fixture.Faker.Commerce.ProductDescription()}";

        invalidInputLists.Add(new object[]
        {
            tooLongDescriptionInvalidCreateCatogoryInput,
            "Description should be less or equal 10000 characters long"
        });

        return invalidInputLists;
    }
}
