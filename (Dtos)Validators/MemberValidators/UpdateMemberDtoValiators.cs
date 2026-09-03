using FluentValidation;
using LibraryManagementSystem.Dtos.Member;

namespace LibraryManagementSystem._Dtos_Validators.MemberValidators
{
    public class UpdateMemberDtoValiators :AbstractValidator<UpdateMemberDto>
    {
        public UpdateMemberDtoValiators()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 100);

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 50);
        }
    }
}
