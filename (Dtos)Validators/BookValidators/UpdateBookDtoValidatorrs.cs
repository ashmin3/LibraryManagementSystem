using FluentValidation;
using LibraryManagementSystem.Dtos.Book;

namespace LibraryManagementSystem._Dtos_Validators.BookValidators
{
    public class UpdateBookDtoValidatorrs :AbstractValidator<UpdateBookDto>
    {

        public UpdateBookDtoValidatorrs() {

            RuleFor(x => x.Title)
                   .NotEmpty()
                   .Length(3, 100);


            RuleFor(x => x.ISBN)
                .NotEmpty();
        }
    }
}
