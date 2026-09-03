using FluentValidation;
using LibraryManagementSystem.Dtos.User;

namespace LibraryManagementSystem._Dtos_Validators.UsersValidators
{
    public class CreateUserDtoValidators : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.UserName)
               .NotEmpty()
               .Length(3, 50);

            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress()
               .Length(3, 50);

            RuleFor(x => x.Password)
               .NotEmpty()
               .Length(3, 50);

            RuleFor(x => x.Roles)
               .NotEmpty()
               .Length(3, 50);

        }
    }
}
