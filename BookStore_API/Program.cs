using BookStore_API.Data;
using BookStore_API.Repositories;
using BookStore_API.Services;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting connection string and services
builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<AuthorRepository>();
builder.Services.AddSingleton<GenreRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<ReviewRepository>();
builder.Services.AddSingleton<BookRepository>();
builder.Services.AddSingleton<AuthorAndBookRepository>();
builder.Services.AddSingleton<GenreAndBookRepository>();
builder.Services.AddSingleton<BookReviewUserRepository>();



//Background Services
builder.Services.AddHostedService<BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
