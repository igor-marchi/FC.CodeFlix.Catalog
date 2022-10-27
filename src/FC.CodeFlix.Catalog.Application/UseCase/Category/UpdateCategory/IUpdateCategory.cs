using FC.CodeFlix.Catalog.Application.UseCase.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.UpdateCategory;
public interface IUpdateCategory : IRequestHandler<UpdateCategoryInput, CategoryModelOutput>
{ }
