using FluentValidation;
using LibraryManagementSystem.Dtos.MemberProfile;

namespace LibraryManagementSystem._Dtos_Validators.MemberProfileValidators
{
    public class CreateMemberProfileDtoValidators  :AbstractValidator<CreateMemberProfileDto>
    {
        public CreateMemberProfileDtoValidators()
        {
            RuleFor(x => x.Address)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .Length(9, 15);
        }
    }
}
