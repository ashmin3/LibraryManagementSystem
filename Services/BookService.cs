using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        public BookService(LibraryDbContext libraryDbContext,IMapper mapper,ILogger<BookService> logger)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDto>> Get_All_Books(int page ,int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var books= await _libraryDbContext.Books.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var map_books = _mapper.Map<List<BookDto>>(books);

            _logger.LogInformation("Books retrived successfully");

            return map_books;
        }

        public async Task<BookDto> Get_Books_By_Id(int id)
        {
            var books = await _libraryDbContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(books == null)
            {
                throw new Exception("Invalid Book Id");
            }

            var book_map = _mapper.Map<BookDto>(books);

            _logger.LogInformation("Books retrived successfully");

            return book_map;
        }

        public async Task<BookDto> Create_Book(CreateBookDto created)
        {
            var book = _mapper.Map<Books>(created);

            await _libraryDbContext.Books.AddAsync(book);

            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Book created Successfully");

            return _mapper.Map<BookDto>(book);
        }


        public async Task<bool> Update_Books(int id, UpdateBookDto update)
        {
            var book = await _libraryDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            
            if (book == null)
            {
                return false;
            }

            _mapper.Map(update, book);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete_Books(int id)
        {
            var book = await _libraryDbContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return false;
            }

             _libraryDbContext.Books.Remove(book);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<BookDto>> Search(int page ,int pagesize,string? field, string? order ,string? search , int? id, string? title)
        {
            int skip = (page - 1) * pagesize;
            IQueryable<Books> queries = _libraryDbContext.Books;
            field = field?.ToLower();
            order = order?.ToLower();

            if (field == "id")
            {
            queries = order == "asc"
                ? queries.OrderBy(x => x.Id)
                : queries.OrderByDescending(x => x.Id);
            }
            else if(field == "title")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Title)
                    : queries.OrderByDescending(x => x.Title);
            }
            else if (field == "isbn")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.ISBN)
                    : queries.OrderByDescending(x => x.ISBN);
            }

            if (!string.IsNullOrWhiteSpace(search)){
                queries = queries.Where
                   (x => x.Id.ToString().Contains(search) 
                   || x.Title.Contains(search) 
                   || x.ISBN.ToString().Contains(search));
            }

            if (id.HasValue)
            {
                queries = queries.Where(x => x.Id == id.Value);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                queries = queries.Where(x => x.Title == title);
            }

            return await  queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new BookDto
            {
                Id=x.Id,
                Title=x.Title,
                ISBN=x.ISBN,
                PublishedYear=x.PublishedYear,
                CategorysId=x.CategorysId
            }).ToListAsync();

        
        }
    }
}
