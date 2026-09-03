using FluentValidation;
using LibraryManagementSystem.Dtos.User;

namespace LibraryManagementSystem._Dtos_Validators.UsersValidators
{
    public class UpdateUsersDtoValidators : AbstractValidator<UpdateUsersDto>
    {
        public UpdateUsersDtoValidators()
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

            
        }
    }
}
