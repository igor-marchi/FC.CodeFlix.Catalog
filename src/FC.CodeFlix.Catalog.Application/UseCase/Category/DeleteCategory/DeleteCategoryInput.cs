using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.DeleteCategory;

public class DeleteCategoryInput : IRequest
{
    public Guid Id { get; set; }

    public DeleteCategoryInput(Guid id)
    {
        Id = id;
    }
}
