using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.AuthorBook;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.AuthorBook;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class AuthorBookService  :IAuthorBookService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<AuthorBookService> _logger;
        private readonly IMapper _mapper;
        public AuthorBookService(LibraryDbContext libraryDbContext, ILogger<AuthorBookService> logger, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
            _logger = logger;
        } 

        public async Task<List<AuthorBookDto>> Get_All_Author_Book(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var author_book = await _libraryDbContext.AuthorBooks.AsNoTracking().Skip(skip).Take(pagesize).Select(x=> new AuthorBookDto
            {
                Id=x.Id,
                AuthorsName=x.Authors.Name,
                BooksName=x.Books.Title
            }).ToListAsync();

           

            _logger.LogInformation("Author book retrived Successfully");

            return author_book; 

        } 

        public async Task<AuthorBookDto?> Get_By_Id(int id)
        {
            var author_book = await _libraryDbContext.AuthorBooks.AsNoTracking().Select(x=> new AuthorBookDto
            {
                Id=x.Id,
                AuthorsName=x.Authors.Name,
                BooksName=x.Books.Title

            }).FirstOrDefaultAsync(x => x.Id == id);
        
            _logger.LogInformation("Author_Book With id : " + id + "Retrived");

            return author_book;
        }

        public async Task<AuthorBookDtoint> Create_Author_Book(CreateAuthorBookDto created)
        {
            var author_book = _mapper.Map<AuthorBooks>(created);

            await _libraryDbContext.AuthorBooks.AddAsync(author_book);

            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("AuthorBookCreatedSuccessfullu");

            return _mapper.Map<AuthorBookDtoint>(author_book);


        }

        public async Task<bool> Update_Author_Book(int id , UpdateAuthorBookDto updated)
        {
            var author_book = await _libraryDbContext.AuthorBooks.FirstOrDefaultAsync(x => x.Id == id);
            if(author_book == null)
            {
                return false;
            }

            _mapper.Map(updated, author_book);
            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("updated data");

            return true;
        }

        public async Task<bool> Delete_Author_Book(int id)
        {
            var author_book = await _libraryDbContext.AuthorBooks.FirstOrDefaultAsync(x => x.Id == id);
            if (author_book == null)
            {
                return false;
            }

            _libraryDbContext.AuthorBooks.Remove(author_book);
            await _libraryDbContext.SaveChangesAsync();
            _logger.LogInformation("updated data");

            return true;
        }

        public async Task<List<AuthorBookDto>> Search(int page, int pagesize, string? field,string? order,string? search, string? authorsname, string? booksname)
        {
            int skip = (page - 1) * pagesize;
            IQueryable<AuthorBooks> queries = _libraryDbContext.AuthorBooks;
            field = field?.ToLower();
            order = order?.ToLower();

            if (field == "id")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Id)
                    : queries.OrderByDescending(x => x.Id);
            }
            else if (field == "authorsid")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Authors.Name)
                    : queries.OrderByDescending(x => x.Authors.Name);
            }
            else if (field == "booksid")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Books.Title)
                    : queries.OrderByDescending(x => x.Books.Title);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(x => x.AuthorsId.ToString().Contains(search) || x.BooksId.ToString().Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(authorsname))
            {
                queries = queries.Where(x => x.Authors.Name == authorsname);
            }
            if (!string.IsNullOrWhiteSpace(booksname))
            {
                queries = queries.Where(x =>x.Books.Title == booksname);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new AuthorBookDto
            {
                Id=x.Id,
                AuthorsName=x.Authors.Name,
                BooksName=x.Books.Title
            }).ToListAsync();


        }




    }
}
