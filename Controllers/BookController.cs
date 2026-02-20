using Microsoft.AspNetCore.Mvc;
using API_Books.api.Models;

namespace API_Books.api.Controllers;

[ApiController]
[Route("API_Books")]
public class BooksController : ControllerBase
{
    private static readonly List<Book> _books = new()
    {
        new() { ID = 1, Title = "1984", Author = "George Orwell", Pages = 328 },
        new() { ID = 2, Title = "The Hobbit", Author = "J.R.R. Tolkien", Pages = 310 }
    };

    [HttpGet]
    public IActionResult Get() => Ok(_books);

    [HttpGet("{id}")]
    public IActionResult GetById(int id) =>
        _books.FirstOrDefault(b => b.ID == id) is { } book
            ? Ok(book)
            : NotFound();

    [HttpPut]
    public IActionResult Create([FromBody] Book newbook)
    {
        if (newbook == null)
        {
            return BadRequest("Invalid book data.");
        }

        newbook.ID = _books.Count > 0 ? _books.Max(b => b.ID) + 1 : 1;

        _books.Add(newbook);
        return CreatedAtAction(nameof(GetById), new { id = newbook.ID }, newbook);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Book updatebook)
    {
        if (updatebook == null || updatebook.ID != id)
        {
            return BadRequest("ID missmatch or invalid data.");
        }
        var extendingIndex = _books.FindIndex(b => b.ID == id);
        if (extendingIndex == -1)
        {
            return NotFound();
        }

        updatebook.ID = id;
        _books[extendingIndex] = updatebook;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var bookToRemove = _books.FirstOrDefault(b => b.ID == id);
        if (bookToRemove == null)
        {
            return NotFound();
        }
        _books.Remove(bookToRemove);
        return NoContent();
    }
}