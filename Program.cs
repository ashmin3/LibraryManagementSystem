using FluentValidation;

using LibraryManagementSystem._Dtos_Validators.AuthorBookValidators;
using LibraryManagementSystem._Dtos_Validators.AuthorValidators;
using LibraryManagementSystem._Dtos_Validators.BookValidators;
using LibraryManagementSystem._Dtos_Validators.BorrowedRecordValidators;
using LibraryManagementSystem._Dtos_Validators.CategoryValidators;
using LibraryManagementSystem._Dtos_Validators.MemberProfileValidators;
using LibraryManagementSystem._Dtos_Validators.MemberValidators;
using LibraryManagementSystem._Dtos_Validators.UsersValidators;

using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Mappings;
using LibraryManagementSystem.Middleware;
using LibraryManagementSystem.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Text;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();


            builder.Services.AddScoped<IAuthService, AuthServices>();
            builder.Services.AddScoped<IUserService, UserServices>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorServices>();
            builder.Services.AddScoped<IMemberService, MemberServices>();
            builder.Services.AddScoped<IMemberProfileServices, MemberProfileServices>();
            builder.Services.AddScoped<IBorrowRecordServices, BorrowedRecordServices>();
            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<IAuthorBookService, AuthorBookService>();



            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );


            builder.Services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme
            )
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!
                        )
                    )
                };
            });

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserMapping>();
                cfg.AddProfile<BookMapping>();
                cfg.AddProfile<AuthorMapping>();
                cfg.AddProfile<MemberMapping>();
                cfg.AddProfile<MemberProfileMappings>();
                cfg.AddProfile<BorrowedRecordMappings>();
                cfg.AddProfile<CategoryMapping>();
                cfg.AddProfile<AuthorBookMappings>();
            });


            builder.Services.AddValidatorsFromAssemblyContaining<
                CreateAuthorBookDtoValidators
            >();


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",

                        Type = SecuritySchemeType.Http,

                        Scheme = "bearer",

                        BearerFormat = "JWT",

                        In = ParameterLocation.Header,

                        Description = "Enter your JWT token."
                    }
                );

                options.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecuritySchemeReference(
                                "Bearer",
                                document
                            )
                        ] = []
                    }
                );
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}