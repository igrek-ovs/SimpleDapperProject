using Dapper;
using DapperSimpleProject.models;
using DapperSimpleProject.Services.Interfaces;
using System.Data.SqlClient;

namespace DapperSimpleProject.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly string _connectionString;

        public AuthorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Author>("SELECT * FROM Authors");
            }
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddAsync(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("INSERT INTO Authors (Name) VALUES (@Name)", new { Name = author.Name });
            }
        }

        public async Task UpdateAsync(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("UPDATE Authors SET Name = @Name WHERE Id = @Id", new { Name = author.Name, Id = author.Id });
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("DELETE FROM Authors WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
