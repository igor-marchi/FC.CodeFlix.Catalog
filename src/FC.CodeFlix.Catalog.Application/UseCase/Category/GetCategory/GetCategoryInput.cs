using FC.CodeFlix.Catalog.Application.UseCase.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.GetCategory;

public class GetCategoryInput : IRequest<CategoryModelOutput>
{
    public Guid Id { get; set; }

    public GetCategoryInput(Guid id)
    {
        Id = id;
    }
}
