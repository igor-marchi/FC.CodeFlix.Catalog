using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.DeleteCategory;

public interface IDeleteCategory
    : IRequestHandler<DeleteCategoryInput>
{
}
