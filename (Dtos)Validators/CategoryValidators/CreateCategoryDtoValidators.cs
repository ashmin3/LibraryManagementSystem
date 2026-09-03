using FluentValidation;
using LibraryManagementSystem.Dtos.Category;

namespace LibraryManagementSystem._Dtos_Validators.CategoryValidators
{
    public class CreateCategoryDtoValidators : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(3, 1000);
        }
    }
}
