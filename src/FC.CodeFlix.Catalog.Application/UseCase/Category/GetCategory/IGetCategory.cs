using FC.CodeFlix.Catalog.Application.UseCase.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOutput>
{ }
