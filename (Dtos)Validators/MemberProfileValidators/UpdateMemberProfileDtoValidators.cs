using FluentValidation;
using LibraryManagementSystem.Dtos.MemberProfile;

namespace LibraryManagementSystem._Dtos_Validators.MemberProfileValidators
{
    public class UpdateMemberProfileDtoValidators : AbstractValidator<UpdateMemberProfileDto>
    {
        public UpdateMemberProfileDtoValidators()
        {
            RuleFor(x => x.Address)
              .NotEmpty();

            RuleFor(x => x.Phone)
                .Length(9, 15);
        }
    }
}
