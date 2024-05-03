using Dapper;
using DapperSimpleProject.models;
using DapperSimpleProject.Services.Interfaces;
using System.Data.SqlClient;

namespace DapperSimpleProject.Services
{
    public class BookService : IBookService
    {
        private readonly string _connectionString;

        public BookService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Book>("SELECT * FROM Books");
            }
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddAsync(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("INSERT INTO Books (Title, AuthorId) VALUES (@Title, @AuthorId)", book);
            }
        }

        public async Task UpdateAsync(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("UPDATE Books SET Title = @Title, AuthorId = @AuthorId WHERE Id = @Id", book);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("DELETE FROM Books WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Book>> GetBooksAndAuthorsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"SELECT b.*, a.* FROM Books b
                      INNER JOIN Authors a ON b.AuthorId = a.Id";
                var result = await connection.QueryAsync<Book, Author, Book>(
                    query,
                    (book, author) =>
                    {
                        book.Author = author;
                        return book;
                    },
                    splitOn: "Id");
                return result;
            }
        }
    }
}
