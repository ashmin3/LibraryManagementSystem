using AutoMapper;
using FluentValidation;
using LibraryManagementSystem.Dtos.AuthorBook;
using System.Data;

namespace LibraryManagementSystem._Dtos_Validators.AuthorBookValidators
{
    public class CreateAuthorBookDtoValidators : AbstractValidator<CreateAuthorBookDto>
    {
        public CreateAuthorBookDtoValidators()
        {
            RuleFor(x => x.AuthorsId)
                .NotEmpty();

            RuleFor(x => x.BooksId)
                .NotEmpty();
        }
    }
}
