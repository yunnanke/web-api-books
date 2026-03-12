using API_Books.api.Models;
using API_Books.api.Services;

namespace API_Books.api.Service
{
    public class InMemoryBookService : IBookService 
    {
        private readonly List<Book> _books = new()
        {
        new() { ID = 1, Title = "1984", Author = "George Orwell", Pages = 328 },
        new() { ID = 2, Title = "The Hobbit", Author = "J.R.R. Tolkien", Pages = 310 }
        };
        //private int nextId = _books.Max(x => x.ID) + 1;

        public List<Book> GetAll() => _books;

        public Book? GetById(int id) => _books.FirstOrDefault(x => x.ID == id);

        public Book Create(Book book)
        {
            int maxId = _books.Any()? _books.Max(x => x.ID) : 0;
            book.ID = maxId + 1;

            _books.Add(book);
            return book;
        }

        public bool Delete(int id)
        {
            var book = _books.FirstOrDefault(x => x.ID == id);
            if (book == null) {  return false; }
            _books.Remove(book);
            return true;
        }
    }
}
