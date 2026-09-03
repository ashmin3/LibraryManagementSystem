using FluentValidation;
using LibraryManagementSystem.Dtos.AuthorBook;

namespace LibraryManagementSystem._Dtos_Validators.AuthorBookValidators
{
    public class UpdateAuthorBookDtoValidators :AbstractValidator<UpdateAuthorBookDto>
    {
        public UpdateAuthorBookDtoValidators()
        {
            RuleFor(x => x.AuthorsId)
              .NotEmpty();

            RuleFor(x => x.BooksId)
                .NotEmpty();
        }
    }
}
