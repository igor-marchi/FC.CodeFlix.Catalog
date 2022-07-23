namespace FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory;

public interface ICreateCategory
{
    public Task<CreateCategoryOutput> Handle(CreateCategoryInput createCategoryInput, CancellationToken cancellationToken);
}
