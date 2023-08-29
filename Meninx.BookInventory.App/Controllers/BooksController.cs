using Meninx.BookInventory.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Meninx.BookInventory.App.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IRepository<Book> _bookRepository;

        public BooksController
        (
            IRepository<Book> bookRepository
        )
        {
            _bookRepository = bookRepository;
        }

        // GET: api/books
        public async Task<IHttpActionResult> GetBooksAsync(string query, int limit, int offset, string sortBy, string sortOrder)
        {
            ISpecification<Book> specification = new Specification<Book>()
                .ApplyQuery(query)
                .ApplyPaging(limit, offset)
                .ApplySorting(sortBy, sortOrder);

            List<Book> books = await _bookRepository.ListAsync(specification);

            IEnumerable<BookDto> result = books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                ISBN = x.ISBN,
                PublicationYear = x.PublicationYear,
                Quantity = x.Quantity,
                CategoryId = x.CategoryId
            });

            return Json(result);
        }

        // GET: api/books/{id}
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> GetBookAsync(Guid id)
        {
            Book book = await _bookRepository.SingleOrDefaultAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            BookDto result = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Quantity = book.Quantity,
                CategoryId = book.CategoryId
            };

            return Json(result);
        }

        // POST: api/books
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> PostBookAsync(BookCreateDto bookCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookCreateDto.Title,
                Author = bookCreateDto.Author,
                ISBN = bookCreateDto.ISBN,
                PublicationYear = bookCreateDto.PublicationYear,
                Quantity = bookCreateDto.Quantity,
                CategoryId = bookCreateDto.CategoryId
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            BookDto result = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Quantity = book.Quantity,
                CategoryId = book.CategoryId
            };

            return Json(result);
        }

        // PUT: api/Books/{id}
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBookAsync(Guid id, BookUpdateDto bookUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = await _bookRepository.SingleOrDefaultAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = bookUpdateDto.Title;
            book.Author = bookUpdateDto.Author;
            book.ISBN = bookUpdateDto.ISBN;
            book.PublicationYear = bookUpdateDto.PublicationYear;
            book.Quantity = bookUpdateDto.Quantity;
            book.CategoryId = bookUpdateDto.CategoryId;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();

            BookDto result = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Quantity = book.Quantity,
                CategoryId = book.CategoryId
            };

            return Json(result);
        }

        // DELETE: api/books/{id}
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> DeleteBookAsync(Guid id)
        {
            Book book = await _bookRepository.SingleOrDefaultAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}