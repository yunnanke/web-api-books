using API_Books.api.Models;

namespace API_Books.api.Services
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book? GetById(int id);
        Book Create(Book book);
        bool Delete(int  id);
    }
}
