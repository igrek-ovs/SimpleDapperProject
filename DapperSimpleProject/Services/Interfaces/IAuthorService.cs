using DapperSimpleProject.models;

namespace DapperSimpleProject.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}
