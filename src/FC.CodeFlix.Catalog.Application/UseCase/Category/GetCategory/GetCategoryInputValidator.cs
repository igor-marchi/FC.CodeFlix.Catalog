using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCase.Category.GetCategory;

public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
{
    public GetCategoryInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
