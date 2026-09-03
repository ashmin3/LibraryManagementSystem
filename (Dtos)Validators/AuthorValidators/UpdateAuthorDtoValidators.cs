using FluentValidation;
using LibraryManagementSystem.Dtos.Author;

namespace LibraryManagementSystem._Dtos_Validators.AuthorValidators
{
    public class UpdateAuthorDtoValidators : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorDtoValidators()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
             .Length(3, 50);

            RuleFor(x => x.Biography)
                .Length(3, 100000);
        }
    }
}
