using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.CreateCategory
{
    public class CreateCategoryInput : IRequest<CreateCategoryOutput>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public CreateCategoryInput(string name, string description = "", bool isActive = true)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
        }
    }
}
