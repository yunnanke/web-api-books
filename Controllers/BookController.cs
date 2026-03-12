using Microsoft.AspNetCore.Mvc;
using API_Books.api.Models;
using API_Books.api.Services;

namespace API_Books.api.Controllers;

[ApiController]
[Route("API_Books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<List<Book>> Get() => _bookService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Book> Get(int id)
    {
        var book = _bookService.GetById(id);
        if (book == null) { return NotFound(); }
        return book;
    }

    [HttpPost]
    public ActionResult<Book> Post( Book newbook)
    {
        if (newbook == null || string.IsNullOrWhiteSpace(newbook.Title) || newbook.Pages <= 0)
        {
            return BadRequest("Invalid book data.");
        }
        var created = _bookService.Create(newbook);

        return CreatedAtAction(nameof(Get), new { id = created.ID }, created);
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if(!_bookService.Delete(id)) { return NotFound(); }
        return NoContent();
    }
}