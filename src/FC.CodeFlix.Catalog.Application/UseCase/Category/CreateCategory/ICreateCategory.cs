using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CreateCategoryOutput>
{
    public Task<CreateCategoryOutput> Handle(CreateCategoryInput createCategoryInput, CancellationToken cancellationToken);
}
