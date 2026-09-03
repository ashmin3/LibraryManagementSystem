using LibraryManagementSystem.Models.Author;
using LibraryManagementSystem.Models.AuthorBook;
using LibraryManagementSystem.Models.Book;
using LibraryManagementSystem.Models.BorrowRecord;
using LibraryManagementSystem.Models.Category;
using LibraryManagementSystem.Models.Member;
using LibraryManagementSystem.Models.MemberProfile;
using LibraryManagementSystem.Models.User;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) :base(options) 
        { 

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Member => MemberProfile (one to one relationship)
            modelBuilder.Entity<Members>()
                .HasOne(mp => mp.MemberProfiles)
                .WithOne(m => m.Members)
                .HasForeignKey<MemberProfiles>(m=>m.MemberId);

            //category=> book(1:n)
            modelBuilder.Entity<Categorys>()
                .HasMany(b => b.Books)
                .WithOne(c => c.Categorys)
                .HasForeignKey(f => f.CategorysId);

            //member => borrowedrecord
            modelBuilder.Entity<Members>()
                .HasMany(x => x.BorrowedRecords)
                .WithOne(m => m.Members)
                .HasForeignKey(f => f.MembersId);

            // Book => Borrowedrecord
            modelBuilder.Entity<Books>()
                .HasMany(br => br.BorrowedRecords)
                .WithOne(b => b.Books)
                .HasForeignKey(f => f.BooksId);

            // authorbook
            modelBuilder.Entity<Authors>()
                .HasMany(x => x.AuthorBooks)
                .WithOne(x => x.Authors)
                .HasForeignKey(f => f.AuthorsId);

            modelBuilder.Entity<Books>()
               .HasMany(x => x.AuthorBooks)
               .WithOne(x => x.Books)
               .HasForeignKey(f => f.BooksId);




            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Categorys> Categories { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<BorrowedRecords> BorrowedRecords { get; set; }
        public DbSet<AuthorBooks> AuthorBooks { get; set; }
        public DbSet<MemberProfiles> MemberProfiles { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
