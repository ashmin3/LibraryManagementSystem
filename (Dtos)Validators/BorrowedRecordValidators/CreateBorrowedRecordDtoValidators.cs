using FluentValidation;
using LibraryManagementSystem.Dtos.BorrowRecords;

namespace LibraryManagementSystem._Dtos_Validators.BorrowedRecordValidators
{
    public class CreateBorrowedRecordDtoValidators : AbstractValidator<CreateBorrowedRecordDto>
    {
        public CreateBorrowedRecordDtoValidators()
        {
            RuleFor(x => x.Status)
                .NotEmpty();

            RuleFor(x => x.BorrowDate)
                .NotEmpty();

            RuleFor(x => x.DueDate)
                .NotEmpty();

            RuleFor(x => x.ReturnDate)
                .NotEmpty();
        }
    }
}
