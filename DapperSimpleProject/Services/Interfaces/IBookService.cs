using DapperSimpleProject.models;

namespace DapperSimpleProject.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetBooksAndAuthorsAsync();
    }
}
