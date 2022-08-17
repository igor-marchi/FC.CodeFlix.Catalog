using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput, GetCategoryOutput>
{ }
