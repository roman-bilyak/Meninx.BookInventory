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
        private readonly IRepository<Category> _categoryRepository;

        public BooksController
        (
            IRepository<Book> bookRepository,
            IRepository<Category> categoryRepository
        )
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: api/books
        public async Task<IHttpActionResult> GetBooksAsync([FromUri(Name = "")] GetBooksRequest request)
        {
            try
            {
                ISpecification<Book> specification = new Specification<Book>()
                    .ApplyQuery(request.Query)
                    .ApplyPaging(request.Limit, request.Offset)
                    .ApplySorting(request.SortBy, request.SortOrder);

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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/books/{id}
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> GetBookAsync(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/books
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> PostBookAsync(BookCreateDto bookCreateDto)
        {
            try
            {
                if (bookCreateDto.CategoryId.HasValue)
                {
                    Category category = await _categoryRepository.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value);
                    if (category == null)
                    {
                        ModelState.AddModelError(nameof(BookCreateDto.CategoryId), "Category not found.");
                    }
                }

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
                    PublicationYear = int.Parse(bookCreateDto.PublicationYear),
                    Quantity = int.Parse(bookCreateDto.Quantity),
                    CategoryId = bookCreateDto.CategoryId.GetValueOrDefault()
                };

                book = await _bookRepository.AddAsync(book);
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Books/{id}
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBookAsync(Guid id, BookUpdateDto bookUpdateDto)
        {
            try
            {
                if (bookUpdateDto.CategoryId.HasValue)
                {
                    Category category = await _categoryRepository.SingleOrDefaultAsync(bookUpdateDto.CategoryId.Value);
                    if (category == null)
                    {
                        ModelState.AddModelError(nameof(BookUpdateDto.CategoryId), "Category not found.");
                    }
                }

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
                book.PublicationYear = int.Parse(bookUpdateDto.PublicationYear);
                book.Quantity = int.Parse(bookUpdateDto.Quantity);
                book.CategoryId = bookUpdateDto.CategoryId.GetValueOrDefault();

                book = await _bookRepository.UpdateAsync(book);
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/books/{id}
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> DeleteBookAsync(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}