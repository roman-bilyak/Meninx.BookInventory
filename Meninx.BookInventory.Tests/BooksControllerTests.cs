using Meninx.BookInventory.App.Controllers;
using Meninx.BookInventory.App.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Meninx.BookInventory.Tests
{
    [TestClass]
    public class BooksControllerTests
    {
        private Mock<IRepository<Book>> _mockBookRepository;
        private Mock<IRepository<Category>> _mockCategoryRepository;
        private BooksController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockBookRepository = new Mock<IRepository<Book>>();
            _mockCategoryRepository = new Mock<IRepository<Category>>();
            _controller = new BooksController(_mockBookRepository.Object, _mockCategoryRepository.Object);
        }

        [TestMethod]
        public async Task GetBooks_WhenNoBooks_ReturnEmptyList()
        {
            // Arrange
            GetBooksRequest request = new GetBooksRequest
            {
                Query = "query",
                Limit = 10,
                Offset = 0,
                SortBy = "Title",
                SortOrder = "Asc"
            };

            List<Book> expectedBooks = new List<Book>();

            _mockBookRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Book>>(), It.IsAny<CancellationToken>()))
                .Callback((ISpecification<Book> spec, CancellationToken cancellationToken) =>
                {
                    Assert.AreEqual(request.Query, spec.Query);
                    Assert.AreEqual(request.Limit, spec.Limit);
                    Assert.AreEqual(request.Offset, spec.Offset);
                    Assert.AreEqual(request.SortBy, spec.SortBy);
                    Assert.AreEqual(request.SortOrder, spec.SortOrder);
                })
                .ReturnsAsync(expectedBooks);

            // Act
            IHttpActionResult result = await _controller.GetBooksAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<IEnumerable<BookDto>>>(result);

            IEnumerable<BookDto> dto = (result as JsonResult<IEnumerable<BookDto>>).Content;
            Assert.AreEqual(0, dto.Count());
        }

        [TestMethod]
        public async Task GetBooks_ReturnListOfBooks()
        {
            // Arrange
            GetBooksRequest request = new GetBooksRequest
            {
                Query = "query",
                Limit = 10,
                Offset = 0,
                SortBy = "Title",
                SortOrder = "Asc"
            };

            List<Book> expectedBooks = new List<Book>
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 1",
                    Author = "Author 1",
                    ISBN = "123456789",
                    PublicationYear = 2022,
                    Quantity = 5,
                    CategoryId = Guid.NewGuid()
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 2",
                    Author = "Author 2",
                    ISBN = "987654321",
                    PublicationYear = 2021,
                    Quantity = 10,
                    CategoryId = Guid.NewGuid()
                }
            };

            _mockBookRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Book>>(), It.IsAny<CancellationToken>()))
                .Callback((ISpecification<Book> spec, CancellationToken cancellationToken) =>
                {
                    Assert.AreEqual(request.Query, spec.Query);
                    Assert.AreEqual(request.Limit, spec.Limit);
                    Assert.AreEqual(request.Offset, spec.Offset);
                    Assert.AreEqual(request.SortBy, spec.SortBy);
                    Assert.AreEqual(request.SortOrder, spec.SortOrder);
                })
                .ReturnsAsync(expectedBooks);

            // Act
            IHttpActionResult result = await _controller.GetBooksAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<IEnumerable<BookDto>>>(result);

            IEnumerable<BookDto> dto = (result as JsonResult<IEnumerable<BookDto>>).Content;
            Assert.AreEqual(2, dto.Count());
        }

        [TestMethod]
        public async Task GetBooks_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            GetBooksRequest request = new GetBooksRequest
            {
                Query = "some query",
                Limit = 10,
                Offset = 0,
                SortBy = "Title",
                SortOrder = "Asc"
            };

            _mockBookRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Book>>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.GetBooksAsync(request);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }

        [TestMethod]
        public async Task GetBook_WhenBookExists_ReturnBook()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book expectedBook = new Book
            {
                Id = bookId,
                Title = "Book 1",
                Author = "Author 1",
                ISBN = "123456789",
                PublicationYear = 2023,
                Quantity = 5,
                CategoryId = Guid.NewGuid()
            };

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedBook);

            // Act
            IHttpActionResult result = await _controller.GetBookAsync(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<BookDto>>(result);

            BookDto dto = (result as JsonResult<BookDto>).Content;
            Assert.AreEqual(expectedBook.Id, dto.Id);
            Assert.AreEqual(expectedBook.Title, dto.Title);
            Assert.AreEqual(expectedBook.Author, dto.Author);
            Assert.AreEqual(expectedBook.ISBN, dto.ISBN);
            Assert.AreEqual(expectedBook.PublicationYear, dto.PublicationYear);
            Assert.AreEqual(expectedBook.Quantity, dto.Quantity);
            Assert.AreEqual(expectedBook.CategoryId, dto.CategoryId);
        }

        [TestMethod]
        public async Task GetBook_WhenBookDoesNotExist_ReturnNotFound()
        {
            // Arrange
            Guid nonExistentBookId = Guid.NewGuid();

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(nonExistentBookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book)null);

            // Act
            IHttpActionResult result = await _controller.GetBookAsync(nonExistentBookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public async Task GetBook_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.GetBookAsync(bookId);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }

        [TestMethod]
        public async Task PostBook_WhenValidData_ReturnBook()
        {
            // Arrange
            BookCreateDto bookCreateDto = new BookCreateDto
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = "123456789",
                PublicationYear = "2022",
                Quantity = "5",
                CategoryId = Guid.NewGuid()
            };

            Book expectedBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookCreateDto.Title,
                Author = bookCreateDto.Author,
                ISBN = bookCreateDto.ISBN,
                PublicationYear = int.Parse(bookCreateDto.PublicationYear),
                Quantity = int.Parse(bookCreateDto.Quantity),
                CategoryId = bookCreateDto.CategoryId.GetValueOrDefault()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = bookCreateDto.CategoryId.Value,
                    Name = "Category Name",
                    Description = "Category Description"
                });

            _mockBookRepository.Setup(x => x.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .Callback((Book book, CancellationToken cancellationToken) =>
                {
                    Assert.AreEqual(expectedBook.Title, book.Title);
                    Assert.AreEqual(expectedBook.Author, book.Author);
                    Assert.AreEqual(expectedBook.ISBN, book.ISBN);
                    Assert.AreEqual(expectedBook.PublicationYear, book.PublicationYear);
                    Assert.AreEqual(expectedBook.Quantity, book.Quantity);
                    Assert.AreEqual(expectedBook.CategoryId, book.CategoryId);
                })
                .ReturnsAsync(expectedBook);

            // Act
            IHttpActionResult result = await _controller.PostBookAsync(bookCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<BookDto>>(result);

            BookDto dto = (result as JsonResult<BookDto>).Content;
            Assert.AreEqual(expectedBook.Id, dto.Id);
            Assert.AreEqual(expectedBook.Title, dto.Title);
            Assert.AreEqual(expectedBook.Author, dto.Author);
            Assert.AreEqual(expectedBook.ISBN, dto.ISBN);
            Assert.AreEqual(expectedBook.PublicationYear, dto.PublicationYear);
            Assert.AreEqual(expectedBook.Quantity, dto.Quantity);
            Assert.AreEqual(expectedBook.CategoryId, dto.CategoryId);
        }

        [TestMethod]
        public async Task PostBook_WhenInvalidCategory_ReturnBadRequest()
        {
            // Arrange
            BookCreateDto bookCreateDto = new BookCreateDto
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = "123456789",
                PublicationYear = "2022",
                Quantity = "5",
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category)null);

            // Act
            IHttpActionResult result = await _controller.PostBookAsync(bookCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<InvalidModelStateResult>(result);
        }

        [TestMethod]
        public async Task PostBook_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            BookCreateDto bookCreateDto = new BookCreateDto
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = "123456789",
                PublicationYear = "2022",
                Quantity = "5",
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = bookCreateDto.CategoryId.Value,
                    Name = "Category Name",
                    Description = "Category Description"
                });

            _mockBookRepository.Setup(x => x.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.PostBookAsync(bookCreateDto);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }

        [TestMethod]
        public async Task PutBook_WhenValidData_ReturnBook()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            BookUpdateDto bookUpdateDto = new BookUpdateDto
            {
                Title = "Updated Book",
                Author = "Updated Author",
                ISBN = "987654321",
                PublicationYear = "2021",
                Quantity = "10",
                CategoryId = Guid.NewGuid()
            };

            Book existingBook = new Book
            {
                Id = bookId,
                Title = "Original Book",
                Author = "Original Author",
                ISBN = "123456789",
                PublicationYear = 2020,
                Quantity = 5,
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookUpdateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = bookUpdateDto.CategoryId.Value,
                    Name = "Category Name",
                    Description = "Category Description"
                });

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingBook);

            _mockBookRepository.Setup(x => x.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .Callback((Book book, CancellationToken cancellationToken) =>
                 {
                     Assert.AreEqual(bookId, book.Id);
                     Assert.AreEqual(bookUpdateDto.Title, book.Title);
                     Assert.AreEqual(bookUpdateDto.Author, book.Author);
                     Assert.AreEqual(bookUpdateDto.ISBN, book.ISBN);
                     Assert.AreEqual(int.Parse(bookUpdateDto.PublicationYear), book.PublicationYear);
                     Assert.AreEqual(int.Parse(bookUpdateDto.Quantity), book.Quantity);
                     Assert.AreEqual(bookUpdateDto.CategoryId, book.CategoryId);
                 })
                .ReturnsAsync(new Book
                {
                    Id = bookId,
                    Title = bookUpdateDto.Title,
                    Author = bookUpdateDto.Author,
                    ISBN = bookUpdateDto.ISBN,
                    PublicationYear = int.Parse(bookUpdateDto.PublicationYear),
                    Quantity = int.Parse(bookUpdateDto.Quantity),
                    CategoryId = bookUpdateDto.CategoryId.GetValueOrDefault()
                });

            // Act
            IHttpActionResult result = await _controller.PutBookAsync(bookId, bookUpdateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<BookDto>>(result);

            BookDto dto = (result as JsonResult<BookDto>).Content;
            Assert.AreEqual(bookId, dto.Id);
            Assert.AreEqual(bookUpdateDto.Title, dto.Title);
            Assert.AreEqual(bookUpdateDto.Author, dto.Author);
            Assert.AreEqual(bookUpdateDto.ISBN, dto.ISBN);
            Assert.AreEqual(int.Parse(bookUpdateDto.PublicationYear), dto.PublicationYear);
            Assert.AreEqual(int.Parse(bookUpdateDto.Quantity), dto.Quantity);
            Assert.AreEqual(bookUpdateDto.CategoryId, dto.CategoryId);
        }

        [TestMethod]
        public async Task PutBook_WhenBookDoesNotExist_ReturnNotFound()
        {
            // Arrange
            Guid nonExistentBookId = Guid.NewGuid();
            BookUpdateDto bookUpdateDto = new BookUpdateDto
            {
                Title = "Updated Book",
                Author = "Updated Author",
                ISBN = "987654321",
                PublicationYear = "2021",
                Quantity = "10",
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookUpdateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = bookUpdateDto.CategoryId.Value,
                    Name = "Category Name",
                    Description = "Category Description"
                });

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(nonExistentBookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book)null);

            // Act
            IHttpActionResult result = await _controller.PutBookAsync(nonExistentBookId, bookUpdateDto);

            // Assert
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public async Task PutBook_WhenInvalidCategory_ReturnBadRequest()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            BookUpdateDto bookUpdateDto = new BookUpdateDto
            {
                Title = "Updated Book",
                Author = "Updated Author",
                ISBN = "987654321",
                PublicationYear = "2021",
                Quantity = "10",
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookUpdateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category)null);

            // Act
            IHttpActionResult result = await _controller.PutBookAsync(bookId, bookUpdateDto);

            // Assert
            Assert.IsInstanceOfType<InvalidModelStateResult>(result);
        }

        [TestMethod]
        public async Task PutBook_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            BookUpdateDto bookUpdateDto = new BookUpdateDto
            {
                Title = "Updated Book",
                Author = "Updated Author",
                ISBN = "987654321",
                PublicationYear = "2021",
                Quantity = "10",
                CategoryId = Guid.NewGuid()
            };

            Book existingBook = new Book
            {
                Id = bookId,
                Title = "Original Book",
                Author = "Original Author",
                ISBN = "123456789",
                PublicationYear = 2020,
                Quantity = 5,
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(x => x.SingleOrDefaultAsync(bookUpdateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = bookUpdateDto.CategoryId.Value,
                    Name = "Category Name",
                    Description = "Category Description"
                });

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingBook);

            _mockBookRepository.Setup(x => x.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.PutBookAsync(bookId, bookUpdateDto);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }

        [TestMethod]
        public async Task DeleteBook_WhenBookExists_ReturnNoContent()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book existingBook = new Book
            {
                Id = bookId,
                Title = "Existing Book",
                Author = "Existing Author",
                ISBN = "123456789",
                PublicationYear = 2020,
                Quantity = 5,
                CategoryId = Guid.NewGuid()
            };

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingBook);

            // Act
            IHttpActionResult result = await _controller.DeleteBookAsync(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, (result as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task DeleteBook_WhenBookDoesNotExist_ReturnNotFound()
        {
            // Arrange
            Guid nonExistentBookId = Guid.NewGuid();

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(nonExistentBookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book)null);

            // Act
            IHttpActionResult result = await _controller.DeleteBookAsync(nonExistentBookId);

            // Assert
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public async Task DeleteBook_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book existingBook = new Book
            {
                Id = bookId,
                Title = "Existing Book",
                Author = "Existing Author",
                ISBN = "123456789",
                PublicationYear = 2020,
                Quantity = 5,
                CategoryId = Guid.NewGuid()
            };

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingBook);

            _mockBookRepository.Setup(x => x.DeleteAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.DeleteBookAsync(bookId);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }
    }
}